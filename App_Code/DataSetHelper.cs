using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;

    public class DataSetHelper
    {
        private DataSet ds;

        private System.Collections.ArrayList m_FieldInfo;
        private string m_FieldList;
        private System.Collections.ArrayList GroupByFieldInfo;
        private string GroupByFieldList;

        public DataSetHelper(ref DataSet DataSet)
        {
            ds = DataSet;
        }
        public DataSetHelper()
        {
            ds = null;
        }

        private void ParseFieldList(string FieldList, bool AllowRelation)
        {
            /*
             * This code parses FieldList into FieldInfo objects  and then 
             * adds them to the m_FieldInfo private member
             * 
             * FieldList systax:  [relationname.]fieldname[ alias], ...
            */
            if (m_FieldList == FieldList) return;
            m_FieldInfo = new System.Collections.ArrayList();
            m_FieldList = FieldList;
            FieldInfo Field; string[] FieldParts; string[] Fields = FieldList.Split(',');
            int i;
            for (i = 0; i <= Fields.Length - 1; i++)
            {
                Field = new FieldInfo();
                //parse FieldAlias
                FieldParts = Fields[i].Trim().Split(' ');
                switch (FieldParts.Length)
                {
                    case 1:
                        //to be set at the end of the loop
                        break;
                    case 2:
                        Field.FieldAlias = FieldParts[1];
                        break;
                    default:
                        throw new Exception("Too many spaces in field definition: '" + Fields[i] + "'.");
                }
                //parse FieldName and RelationName
                FieldParts = FieldParts[0].Split('.');
                switch (FieldParts.Length)
                {
                    case 1:
                        Field.FieldName = FieldParts[0];
                        break;
                    case 2:
                        if (AllowRelation == false)
                            throw new Exception("Relation specifiers not permitted in field list: '" + Fields[i] + "'.");
                        Field.RelationName = FieldParts[0].Trim();
                        Field.FieldName = FieldParts[1].Trim();
                        break;
                    default:
                        throw new Exception("Invalid field definition: " + Fields[i] + "'.");
                }
                if (Field.FieldAlias == null)
                    Field.FieldAlias = Field.FieldName;
                m_FieldInfo.Add(Field);
            }
        }

        private void ParseGroupByFieldList(string FieldList)
        {
            /*
            * Parses FieldList into FieldInfo objects and adds them to the GroupByFieldInfo private member
            * 
            * FieldList syntax: fieldname[ alias]|operatorname(fieldname)[ alias],...
            * 
            * Supported Operators: count,sum,max,min,first,last,avg
            */
            if (GroupByFieldList == FieldList) return;
            GroupByFieldInfo = new System.Collections.ArrayList();
            FieldInfo Field; string[] FieldParts; string[] Fields = FieldList.Split(',');
            for (int i = 0; i <= Fields.Length - 1; i++)
            {
                Field = new FieldInfo();
                //Parse FieldAlias
                FieldParts = Fields[i].Trim().Split(' ');
                switch (FieldParts.Length)
                {
                    case 1:
                        //to be set at the end of the loop
                        break;
                    case 2:
                        Field.FieldAlias = FieldParts[1];
                        break;
                    default:
                        throw new ArgumentException("Too many spaces in field definition: '" + Fields[i] + "'.");
                }
                //Parse FieldName and Aggregate
                FieldParts = FieldParts[0].Split('(');
                switch (FieldParts.Length)
                {
                    case 1:
                        Field.FieldName = FieldParts[0];
                        break;
                    case 2:
                        Field.Aggregate = FieldParts[0].Trim().ToLower();    //we're doing a case-sensitive comparison later
                        Field.FieldName = FieldParts[1].Trim(' ', ')');
                        break;
                    default:
                        throw new ArgumentException("Invalid field definition: '" + Fields[i] + "'.");
                }
                if (Field.FieldAlias == null)
                {
                    if (Field.Aggregate == null)
                        Field.FieldAlias = Field.FieldName;
                    else
                        Field.FieldAlias = Field.Aggregate + "of" + Field.FieldName;
                }
                GroupByFieldInfo.Add(Field);
            }
            GroupByFieldList = FieldList;
        }

        private DataTable CreateGroupByTable(string TableName, DataTable SourceTable, string FieldList)
        {
            /*
             * Creates a table based on aggregates of fields of another table
             * 
             * RowFilter affects rows before GroupBy operation. No "Having" support
             * though this can be emulated by subsequent filtering of the table that results
             * 
             *  FieldList syntax: fieldname[ alias]|aggregatefunction(fieldname)[ alias], ...
            */
            if (FieldList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
                //return CreateTable(TableName, SourceTable);
            }
            else
            {
                DataTable dt = new DataTable(TableName);
                ParseGroupByFieldList(FieldList);
                foreach (FieldInfo Field in GroupByFieldInfo)
                {
                    DataColumn dc = SourceTable.Columns[Field.FieldName];
                    if (Field.Aggregate == null)
                        dt.Columns.Add(Field.FieldAlias, dc.DataType, dc.Expression);
                    else
                        dt.Columns.Add(Field.FieldAlias, dc.DataType);
                }
                if (ds != null)
                    ds.Tables.Add(dt);
                return dt;
            }
        }

        private void InsertGroupByInto(DataTable DestTable, DataTable SourceTable, string FieldList, string GroupBy)
        {
            InsertGroupByInto(DestTable, SourceTable, FieldList, "", GroupBy, new string[] { "" });
        }

        private void InsertGroupByInto(DataTable DestTable, DataTable SourceTable, string FieldList,
    string RowFilter, string GroupBy, string[] MergeDataForColumns)
        {
            /*
             * Copies the selected rows and columns from SourceTable and inserts them into DestTable
             * FieldList has same format as CreateGroupByTable
            */
            if (FieldList == null)
                throw new ArgumentException("You must specify at least one field in the field list.");
            ParseGroupByFieldList(FieldList);	//parse field list
            ParseFieldList(GroupBy, false);			//parse field names to Group By into an arraylist
            DataRow[] Rows = SourceTable.Select(RowFilter, GroupBy);
            DataRow LastSourceRow = null, DestRow = null; bool SameRow; int RowCount = 0;
            foreach (DataRow SourceRow in Rows)
            {
                SameRow = false;
                if (LastSourceRow != null)
                {
                    SameRow = true;
                    foreach (FieldInfo Field in m_FieldInfo)
                    {
                        if (!ColumnEqual(LastSourceRow[Field.FieldName], SourceRow[Field.FieldName]))
                        {
                            SameRow = false;
                            break;
                        }
                    }
                    if (!SameRow)
                        DestTable.Rows.Add(DestRow);
                }
                if (!SameRow)
                {
                    DestRow = DestTable.NewRow();
                    RowCount = 0;
                }
                RowCount += 1;
                foreach (FieldInfo Field in GroupByFieldInfo)
                {
                    switch (Field.Aggregate)    //this test is case-sensitive
                    {
                        case null:        //implicit last
                        case "":        //implicit last
                        case "last":

                            bool go = false;
                            //checking if current field matches the fields in ColumnsToMegre
                            for (int i = 0; i < MergeDataForColumns.Length; i++)
                            {
                                if (Field.FieldAlias.Equals(MergeDataForColumns[i]))
                                {
                                    go = true;
                                    break;
                                }
                            }
                            if (go && DestRow[Field.FieldAlias].ToString().Length > 0)
                            {
                                if (!DestRow[Field.FieldAlias].ToString().Contains(SourceRow[Field.FieldName].ToString()))
                                    DestRow[Field.FieldAlias] = DestRow[Field.FieldAlias] + ", " + SourceRow[Field.FieldName];
                            }
                            else
                                DestRow[Field.FieldAlias] = SourceRow[Field.FieldName];
                            break;
                        case "first":
                            if (RowCount == 1)
                                DestRow[Field.FieldAlias] = SourceRow[Field.FieldName];
                            break;
                        case "count":
                            DestRow[Field.FieldAlias] = RowCount;
                            break;
                        case "sum":
                            DestRow[Field.FieldAlias] = Add(DestRow[Field.FieldAlias], SourceRow[Field.FieldName]);
                            break;
                        case "avg":
                            DestRow[Field.FieldAlias] = Avg(DestRow[Field.FieldAlias], SourceRow[Field.FieldName]);
                            break;
                        case "max":
                            DestRow[Field.FieldAlias] = Max(DestRow[Field.FieldAlias], SourceRow[Field.FieldName]);
                            break;
                        case "min":
                            if (RowCount == 1)
                                DestRow[Field.FieldAlias] = SourceRow[Field.FieldName];
                            else
                                DestRow[Field.FieldAlias] = Min(DestRow[Field.FieldAlias], SourceRow[Field.FieldName]);
                            break;
                    }
                }
                LastSourceRow = SourceRow;
            }
            if (DestRow != null)
                DestTable.Rows.Add(DestRow);
        }


        private FieldInfo LocateFieldInfoByName(System.Collections.ArrayList FieldList, string Name)
        {
            //Looks up a FieldInfo record based on FieldName
            foreach (FieldInfo Field in FieldList)
            {
                if (Field.FieldName == Name)
                    return Field;
            }
            return null;
        }

        private bool ColumnEqual(object a, object b)
        {
            /*
             * Compares two values to see if they are equal. Also compares DBNULL.Value.
             * 
             * Note: If your DataTable contains object fields, you must extend this
             * function to handle them in a meaningful way if you intend to group on them.
            */
            if ((a is DBNull) && (b is DBNull))
                return true;    //both are null
            if ((a is DBNull) || (b is DBNull))
                return false;    //only one is null
            return (a.Equals(b));    //value type standard comparison
            //return (a == b);    //value type standard comparison
        }

        public object[] MinAndMax(DataTable dataTable, string columnName)
        {
            object[] objs = new object[2];

            if (dataTable.Rows.Count > 0)
            {
                DataRow[] drs = dataTable.Select("1=1", columnName);
                objs[0] = drs[0][columnName].ToString();
                objs[1] = drs[drs.Length - 1][columnName].ToString();
                return objs;
            }
            return null;
        }

        public object Min(DataTable dataTable, string columnName)
        {
            if (dataTable.Rows.Count > 0)
            {
                DataRow[] drs = dataTable.Select("1=1", columnName);
                return drs[0][columnName].ToString();
            }
            return null;
        }

        public object Min(object a, object b)
        {
            //Returns MIN of two values - DBNull is less than all others
            if ((a is DBNull) || (b is DBNull))
                return DBNull.Value;

            DateTime dtA;
            double dblA = 0;

            if (DateTime.TryParse(a.ToString(), out dtA))
            {
                DateTime dtB;
                if (DateTime.TryParse(a.ToString(), out dtB))
                {
                    if (dtA < dtB)
                        return a;
                    else
                        return b;
                }
                else
                    return DBNull.Value;
            }
            else if (double.TryParse(a.ToString(), out dblA))
            {
                double dblB = 0;
                if (double.TryParse(b.ToString(), out dblB))
                {
                    if (dblA < dblB)
                        return a;
                    else
                        return b;
                }
                else
                    return DBNull.Value;
            }
            else
            {
                if (((IComparable)a).CompareTo(b) == -1)
                    return a;
                else
                    return b;
            }
        }

        public object Max(DataTable dataTable, string columnName)
        {
            if (dataTable.Rows.Count > 0)
            {
                DataRow[] drs = dataTable.Select("1=1", columnName + " DESC");
                return drs[0][columnName].ToString();
            }
            return null;
        }

        public object Max(object a, object b)
        {
            //Returns Max of two values - DBNull is less than all others
            if (a is DBNull)
                return b;
            if (b is DBNull)
                return a;


            DateTime dtA;
            double dblA = 0;

            if (DateTime.TryParse(a.ToString(), out dtA))
            {
                DateTime dtB;
                if (DateTime.TryParse(a.ToString(), out dtB))
                {
                    if (dtA > dtB)
                        return a;
                    else
                        return b;
                }
                else
                    return DBNull.Value;
            }
            else if (double.TryParse(a.ToString(), out dblA))
            {
                double dblB = 0;
                if (double.TryParse(b.ToString(), out dblB))
                {
                    if (dblA > dblB)
                        return a;
                    else
                        return b;
                }
                else
                    return DBNull.Value;
            }
            else
            {
                if (((IComparable)a).CompareTo(b) == 1)
                    return a;
                else
                    return b;
            }
        }

        public object Add(object a, object b)
        {
            //Adds two values - if one is DBNull, then returns the other
            if (a is DBNull)
                return b;
            if (b is DBNull)
                return a;
            return (Convert.ToDecimal(a) + Convert.ToDecimal(b));
        }

        public object Avg(object a, object b)
        {
            //Averages two values - if one is DBNull, then returns the other
            /*
            if (a is DBNull)
                return b;
            if (b is DBNull)
                return a;
             */
            double dblA = 0.00d;
            double dblB = 0.00d;

            double.TryParse(a.ToString(), out dblA);
            double.TryParse(b.ToString(), out dblB);

            return ((Convert.ToDouble(dblA) + Convert.ToDouble(dblB)) / 2);
        }

        public DataTable SelectGroupByInto(string TableName, DataTable SourceTable, string FieldList,
    string RowFilter, string GroupBy)
        {
            /* Usage: SelectGroupByInto("dtNewEmployee", dtEmployee, "Sum(salary) Sum_Salary, Emp_ID, Avg(salary) Avg_Salary", "dept_id=12", "Emp_ID");
             * The result datatable will be automatically sorted in asc order by column(s) provided in GroupBy parameter.              
             */ 
            return SelectGroupByInto(TableName, SourceTable, FieldList, RowFilter, GroupBy,  new string[0]);
        }

        public DataTable SelectGroupByInto(string TableName, DataTable SourceTable, string FieldList,
    string RowFilter, string GroupBy, string[] MergeDataForColumns)
        {
            /*
             * Selects data from one DataTable to another and performs various aggregate functions
             * along the way. See InsertGroupByInto and ParseGroupByFieldList for supported aggregate functions.
             */
            DataTable dt = CreateGroupByTable(TableName, SourceTable, FieldList);
            InsertGroupByInto(dt, SourceTable, FieldList, RowFilter, GroupBy, MergeDataForColumns);
            return dt;
        }



        private class FieldInfo
        {
            public string RelationName;
            public string FieldName;	//source table field name
            public string FieldAlias;	//destination table field name
            public string Aggregate;
        }

        public DataTable RemoveDuplicates(DataTable table, List<string> keyColumns)
        {
            Dictionary<string, string> uniquenessDict = new Dictionary<string, string>(table.Rows.Count);
            StringBuilder sb = null;
            int rowIndex = 0;
            DataRow row;
            DataRowCollection rows = table.Copy().Rows;

            while (rowIndex < rows.Count)
            {
                row = rows[rowIndex];
                sb = new StringBuilder();

                foreach (string colname in keyColumns)
                {
                    sb.Append((row[colname].ToString()));
                }

                if (uniquenessDict.ContainsKey(sb.ToString()))
                {
                    rows.Remove(row);
                }
                else
                {
                    uniquenessDict.Add(sb.ToString(), string.Empty);
                    rowIndex++;
                }
            }//while end

            DataTable dtUnique = table.Clone();
            foreach (DataRow dr in rows)
            {
                dtUnique.ImportRow(dr);
            }
            return dtUnique;
        }

        private DataTable CreateJoinTable(string TableName, DataTable SourceTable, string FieldList)
        {
            /*
             * Creates a table based on fields of another table and related parent tables
             * 
             * FieldList syntax: [relationname.]fieldname[ alias][,[relationname.]fieldname[ alias]]...
            */
            if (FieldList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
                //return CreateTable(TableName, SourceTable);
            }
            else
            {
                DataTable dt = new DataTable(TableName);
                ParseFieldList(FieldList, true);
                foreach (FieldInfo Field in m_FieldInfo)
                {
                    if (Field.RelationName == null)
                    {
                        DataColumn dc = SourceTable.Columns[Field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                    else
                    {
                        DataColumn dc = SourceTable.ParentRelations[Field.RelationName].ParentTable.Columns[Field.FieldName];
                        dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                    }
                }
                if (ds != null)
                    ds.Tables.Add(dt);
                return dt;
            }
        }

        private void InsertJoinInto(DataTable DestTable, DataTable SourceTable,
    string FieldList, string RowFilter, string Sort)
        {
            /*
            * Copies the selected rows and columns from SourceTable and inserts them into DestTable
            * FieldList has same format as CreatejoinTable
            */
            if (FieldList == null)
            {
                throw new ArgumentException("You must specify at least one field in the field list.");
                //InsertInto(DestTable, SourceTable, RowFilter, Sort);
            }
            else
            {
                ParseFieldList(FieldList, true);
                DataRow[] Rows = SourceTable.Select(RowFilter, Sort);
                foreach (DataRow SourceRow in Rows)
                {
                    DataRow DestRow = DestTable.NewRow();
                    foreach (FieldInfo Field in m_FieldInfo)
                    {
                        if (Field.RelationName == null)
                        {
                            DestRow[Field.FieldName] = SourceRow[Field.FieldName];
                        }
                        else
                        {
                            DataRow ParentRow = SourceRow.GetParentRow(Field.RelationName);
                            DestRow[Field.FieldName] = ParentRow[Field.FieldName];
                        }
                    }
                    DestTable.Rows.Add(DestRow);
                }
            }
        }

        public DataTable SelectJoinInto(string TableName, DataTable SourceTable, string FieldList, string RowFilter, string Sort)
        {
            /*
             * Selects sorted, filtered values from one DataTable to another.
             * Allows you to specify relationname.fieldname in the FieldList to include fields from
             *  a parent table. The Sort and Filter only apply to the base table and not to related tables.
            */
            DataTable dt = CreateJoinTable(TableName, SourceTable, FieldList);
            InsertJoinInto(dt, SourceTable, FieldList, RowFilter, Sort);
            return dt;
        }
    }
