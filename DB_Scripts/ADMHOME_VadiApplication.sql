/* =====================================================================================
   ADMHOME login - वादी application (Finalise -> View & Forward Application)
   Database : LandDisputeDB   (SQL Server 2016 SP1 or later - uses SEQUENCE, THROW, CREATE OR ALTER)

   Creates only NEW objects; no existing (pre-ADMHOME) table / procedure is modified:
     1. dbo.seq_HDSB_ApplicationNo           - number generator for application no.
     2. dbo.ADMHOME_VadiApplication          - one row per finalised application
     3. dbo.ADMHOME_VadiApplicationDoc       - the uploaded PDF (kept apart so list queries stay light)
     4. dbo.usp_ADMHOME_InsertVadiApplication - saves application + PDF in one transaction
                                                and returns the new application no.
     5. dbo.ADMHOME_VadiApplicationForward   - one row per recipient (district + DM and/or SP)
     6. dbo.usp_ADMHOME_ForwardVadiApplication - forwards an application to the DM and/or SP
                                                of a district

   Forward visibility: a DM (DMOPT) / SP (SSPOPT) login sees the applications forwarded to
   its own role in its own district (UserLogin.Userrole + UserLogin.District_Code).

   Application no. format : HDSB + 5 digits  ->  HDSB10001, HDSB10002, ... HDSB99999
     - taken from a SEQUENCE, so two users finalising at the same moment can never get
       the same number (no MAX()+1 race), and it is enforced by a UNIQUE constraint too.
     - capacity 89,999 applications (MAXVALUE 99999, NO CYCLE -> error instead of reuse).

   Safe to run more than once.
   ===================================================================================== */

-- procedures keep the settings they were created with; sqlcmd defaults QUOTED_IDENTIFIER to OFF
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- 1. Application number sequence ------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.sequences WHERE name = 'seq_HDSB_ApplicationNo' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE SEQUENCE dbo.seq_HDSB_ApplicationNo
        AS INT
        START WITH 10001
        INCREMENT BY 1
        MINVALUE 10001
        MAXVALUE 99999
        NO CYCLE
        NO CACHE;          -- no gaps after a SQL Server restart
END
GO

-- 2. Application table ------------------------------------------------------------------
IF OBJECT_ID('dbo.ADMHOME_VadiApplication', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ADMHOME_VadiApplication
    (
        ApplicationId            BIGINT IDENTITY(1,1) NOT NULL,
        ApplicationNo            VARCHAR(9)     NOT NULL,          -- HDSB + 5 digits

        -- वादी का विवरण (codes use the same types as dbo.VadiDetailEntry)
        vadi_Name                NVARCHAR(100)  NOT NULL,
        Vadi_Father_Husband_Name NVARCHAR(100)  NULL,
        SexAsPerAadhaar          CHAR(1)        NOT NULL,          -- M / F / O
        Vadi_District_Code       BIGINT         NOT NULL,
        Vadi_Sub_DivCode         BIGINT         NOT NULL,
        Vadi_Block_Code          BIGINT         NOT NULL,
        Vadi_Thana_code          BIGINT         NOT NULL,
        Vadi_AreaType            CHAR(1)        NULL,              -- R / U
        Vadi_Panchayat_Code      BIGINT         NULL,
        Vadi_Village_Code        BIGINT         NULL,
        Vadi_WardNo              BIGINT         NULL,
        mohalla                  NVARCHAR(100)  NULL,
        Vadi_MobileNo            VARCHAR(15)    NULL,
        PinCode                  CHAR(6)        NULL,
        Remarks                  NVARCHAR(MAX)  NULL,              -- up to 500 words

        -- workflow
        Status                   CHAR(1)        NOT NULL CONSTRAINT DF_ADMHOME_VadiApplication_Status DEFAULT ('F'),  -- F = Finalised, W = Forwarded
        CreatedBy                VARCHAR(30)    NOT NULL,          -- UserLogin.UserID
        CreatedIP                VARCHAR(50)    NULL,
        CreatedOn                DATETIME       NOT NULL CONSTRAINT DF_ADMHOME_VadiApplication_CreatedOn DEFAULT (GETDATE()),
        -- forwards (DM / SP) are kept in dbo.ADMHOME_VadiApplicationForward (section 5)

        CONSTRAINT PK_ADMHOME_VadiApplication PRIMARY KEY CLUSTERED (ApplicationId),
        CONSTRAINT UQ_ADMHOME_VadiApplication_ApplicationNo UNIQUE (ApplicationNo),
        CONSTRAINT CK_ADMHOME_VadiApplication_ApplicationNo CHECK (ApplicationNo LIKE 'HDSB[0-9][0-9][0-9][0-9][0-9]'),
        CONSTRAINT CK_ADMHOME_VadiApplication_PinCode CHECK (PinCode IS NULL OR PinCode LIKE '[1-9][0-9][0-9][0-9][0-9][0-9]'),
        CONSTRAINT CK_ADMHOME_VadiApplication_Sex CHECK (SexAsPerAadhaar IN ('M','F','O')),
        CONSTRAINT CK_ADMHOME_VadiApplication_Status CHECK (Status IN ('F','W'))
    );

    CREATE INDEX IX_ADMHOME_VadiApplication_CreatedOn ON dbo.ADMHOME_VadiApplication (CreatedOn DESC);
END
GO

-- 3. Document table ---------------------------------------------------------------------
IF OBJECT_ID('dbo.ADMHOME_VadiApplicationDoc', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ADMHOME_VadiApplicationDoc
    (
        ApplicationId  BIGINT         NOT NULL,
        FileName       NVARCHAR(260)  NOT NULL,
        ContentType    VARCHAR(100)   NOT NULL CONSTRAINT DF_ADMHOME_VadiApplicationDoc_ContentType DEFAULT ('application/pdf'),
        FileSize       INT            NOT NULL,
        FileData       VARBINARY(MAX) NOT NULL,
        UploadedOn     DATETIME       NOT NULL CONSTRAINT DF_ADMHOME_VadiApplicationDoc_UploadedOn DEFAULT (GETDATE()),

        CONSTRAINT PK_ADMHOME_VadiApplicationDoc PRIMARY KEY CLUSTERED (ApplicationId),
        CONSTRAINT FK_ADMHOME_VadiApplicationDoc_Application FOREIGN KEY (ApplicationId)
            REFERENCES dbo.ADMHOME_VadiApplication (ApplicationId),
        CONSTRAINT CK_ADMHOME_VadiApplicationDoc_FileSize CHECK (FileSize > 0 AND FileSize <= 5242880)   -- 5 MB
    );
END
GO

-- 4. Insert procedure ---------------------------------------------------------------------
CREATE OR ALTER PROCEDURE dbo.usp_ADMHOME_InsertVadiApplication
    @vadi_Name                NVARCHAR(100),
    @Vadi_Father_Husband_Name NVARCHAR(100) = NULL,
    @SexAsPerAadhaar          CHAR(1),
    @Vadi_District_Code       BIGINT,
    @Vadi_Sub_DivCode         BIGINT,
    @Vadi_Block_Code          BIGINT,
    @Vadi_Thana_code          BIGINT,
    @Vadi_AreaType            CHAR(1)       = NULL,
    @Vadi_Panchayat_Code      BIGINT        = NULL,
    @Vadi_Village_Code        BIGINT        = NULL,
    @Vadi_WardNo              BIGINT        = NULL,
    @mohalla                  NVARCHAR(100) = NULL,
    @Vadi_MobileNo            VARCHAR(15)   = NULL,
    @PinCode                  CHAR(6)       = NULL,
    @Remarks                  NVARCHAR(MAX) = NULL,
    @CreatedBy                VARCHAR(30),
    @CreatedIP                VARCHAR(50)   = NULL,
    @FileName                 NVARCHAR(260),
    @FileData                 VARBINARY(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;     -- any error rolls the whole thing back

    DECLARE @ApplicationNo VARCHAR(9) =
        'HDSB' + CAST(NEXT VALUE FOR dbo.seq_HDSB_ApplicationNo AS VARCHAR(5));
    DECLARE @ApplicationId BIGINT;

    BEGIN TRANSACTION;

        INSERT INTO dbo.ADMHOME_VadiApplication
            (ApplicationNo, vadi_Name, Vadi_Father_Husband_Name, SexAsPerAadhaar,
             Vadi_District_Code, Vadi_Sub_DivCode, Vadi_Block_Code, Vadi_Thana_code,
             Vadi_AreaType, Vadi_Panchayat_Code, Vadi_Village_Code, Vadi_WardNo, mohalla,
             Vadi_MobileNo, PinCode, Remarks, Status, CreatedBy, CreatedIP)
        VALUES
            (@ApplicationNo, @vadi_Name, NULLIF(@Vadi_Father_Husband_Name, N''), @SexAsPerAadhaar,
             @Vadi_District_Code, @Vadi_Sub_DivCode, @Vadi_Block_Code, @Vadi_Thana_code,
             NULLIF(@Vadi_AreaType, ''), @Vadi_Panchayat_Code, @Vadi_Village_Code, @Vadi_WardNo, NULLIF(@mohalla, N''),
             NULLIF(@Vadi_MobileNo, ''), NULLIF(@PinCode, ''), NULLIF(@Remarks, N''), 'F', @CreatedBy, @CreatedIP);

        SET @ApplicationId = SCOPE_IDENTITY();

        INSERT INTO dbo.ADMHOME_VadiApplicationDoc (ApplicationId, FileName, FileSize, FileData)
        VALUES (@ApplicationId, @FileName, DATALENGTH(@FileData), @FileData);

    COMMIT TRANSACTION;

    SELECT @ApplicationId AS ApplicationId, @ApplicationNo AS ApplicationNo;
END
GO

-- 5. Forward table ------------------------------------------------------------------------
-- the first version of this script had single Forwarded* columns on the application table;
-- they were never used and are replaced by this table (one row per recipient)
IF COL_LENGTH('dbo.ADMHOME_VadiApplication', 'ForwardedTo') IS NOT NULL
    ALTER TABLE dbo.ADMHOME_VadiApplication DROP COLUMN ForwardedTo, ForwardedBy, ForwardedOn, ForwardRemarks;
GO

IF OBJECT_ID('dbo.ADMHOME_VadiApplicationForward', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.ADMHOME_VadiApplicationForward
    (
        ForwardId          BIGINT IDENTITY(1,1) NOT NULL,
        ApplicationId      BIGINT         NOT NULL,
        DistrictCode       BIGINT         NOT NULL,          -- mst_Commissionary_Districts.DISTRICTCODE
        ForwardedToRole    VARCHAR(10)    NOT NULL,          -- DMOPT = DM, SSPOPT = SP
        ForwardedToUserID  VARCHAR(30)    NULL,              -- the DM / SP login found at forward time (audit)
        ForwardRemarks     NVARCHAR(500)  NULL,
        ForwardedBy        VARCHAR(30)    NOT NULL,          -- ADMHOME UserLogin.UserID
        ForwardedIP        VARCHAR(50)    NULL,
        ForwardedOn        DATETIME       NOT NULL CONSTRAINT DF_ADMHOME_VadiApplicationForward_ForwardedOn DEFAULT (GETDATE()),

        CONSTRAINT PK_ADMHOME_VadiApplicationForward PRIMARY KEY CLUSTERED (ForwardId),
        CONSTRAINT FK_ADMHOME_VadiApplicationForward_Application FOREIGN KEY (ApplicationId)
            REFERENCES dbo.ADMHOME_VadiApplication (ApplicationId),
        CONSTRAINT CK_ADMHOME_VadiApplicationForward_Role CHECK (ForwardedToRole IN ('DMOPT','SSPOPT')),
        -- the same application cannot be forwarded twice to the same DM / SP
        CONSTRAINT UQ_ADMHOME_VadiApplicationForward_Target UNIQUE (ApplicationId, DistrictCode, ForwardedToRole)
    );

    -- "Forwarded Applications" page of a DM / SP login
    CREATE INDEX IX_ADMHOME_VadiApplicationForward_Recipient
        ON dbo.ADMHOME_VadiApplicationForward (ForwardedToRole, DistrictCode, ForwardedOn DESC);
END
GO

-- 6. Forward procedure --------------------------------------------------------------------
-- Forwards @ApplicationNo to the DM and/or SP of @DistrictCode.
-- Returns one row per requested recipient: Role, UserID, Result
--   Result = 'FORWARDED' | 'ALREADY' (was forwarded to that recipient before)
CREATE OR ALTER PROCEDURE dbo.usp_ADMHOME_ForwardVadiApplication
    @ApplicationNo   VARCHAR(9),
    @DistrictCode    BIGINT,
    @ToDM            BIT,
    @ToSP            BIT,
    @ForwardRemarks  NVARCHAR(500) = NULL,
    @ForwardedBy     VARCHAR(30),
    @ForwardedIP     VARCHAR(50)   = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF ISNULL(@ToDM, 0) = 0 AND ISNULL(@ToSP, 0) = 0
        THROW 50001, 'Select DM and/or SP.', 1;

    DECLARE @ApplicationId BIGINT =
        (SELECT ApplicationId FROM dbo.ADMHOME_VadiApplication WHERE ApplicationNo = @ApplicationNo);
    IF @ApplicationId IS NULL
        THROW 50002, 'Application not found.', 1;

    IF NOT EXISTS (SELECT 1 FROM dbo.mst_Commissionary_Districts WHERE DISTRICTCODE = @DistrictCode)
        THROW 50003, 'District not found.', 1;

    DECLARE @Targets TABLE (Role VARCHAR(10) PRIMARY KEY, UserID VARCHAR(30) NULL, Result VARCHAR(10) NULL);
    IF @ToDM = 1 INSERT @Targets (Role) VALUES ('DMOPT');
    IF @ToSP = 1 INSERT @Targets (Role) VALUES ('SSPOPT');

    -- the DM / SP login of that district (one per district today)
    UPDATE t SET UserID = (SELECT TOP 1 u.UserID FROM dbo.UserLogin u
                           WHERE u.Userrole = t.Role AND u.District_Code = @DistrictCode
                           ORDER BY u.UserID)
    FROM @Targets t;

    BEGIN TRANSACTION;

        UPDATE t SET Result = 'ALREADY'
        FROM @Targets t
        WHERE EXISTS (SELECT 1 FROM dbo.ADMHOME_VadiApplicationForward f WITH (UPDLOCK, HOLDLOCK)
                      WHERE f.ApplicationId = @ApplicationId AND f.DistrictCode = @DistrictCode
                        AND f.ForwardedToRole = t.Role);

        INSERT INTO dbo.ADMHOME_VadiApplicationForward
            (ApplicationId, DistrictCode, ForwardedToRole, ForwardedToUserID, ForwardRemarks, ForwardedBy, ForwardedIP)
        SELECT @ApplicationId, @DistrictCode, t.Role, t.UserID, NULLIF(@ForwardRemarks, N''), @ForwardedBy, @ForwardedIP
        FROM @Targets t
        WHERE t.Result IS NULL;

        UPDATE @Targets SET Result = 'FORWARDED' WHERE Result IS NULL;

        UPDATE dbo.ADMHOME_VadiApplication SET Status = 'W' WHERE ApplicationId = @ApplicationId;

    COMMIT TRANSACTION;

    SELECT Role, UserID, Result FROM @Targets ORDER BY Role;
END
GO
