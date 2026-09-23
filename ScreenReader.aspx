<%@ Page Title="" Language="C#" MasterPageFile="~/LoginMaster.master" AutoEventWireup="true" CodeFile="ScreenReader.aspx.cs" Inherits="JantaDarbar_ScreenReader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <link href="Style/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Style/assets/Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
  <meta charset="utf-8" content="" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="menu/jquery.min.js" type="text/javascript"></script>
    <script src="menu/script.js"></script>
    <script src="js/menu.js"></script>
    <link rel="stylesheet" type="text/css" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/animate.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/font.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/li-scroller.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/slick.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/jquery.fancybox.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/theme.css" />
    <link rel="stylesheet" type="text/css" href="assets/css/style.css" />
    <style>
        
        .rowdata.th
        {
          
            
        }
     
.card {
    margin: 2.5%;
    padding: 2.5%;
    color: #171717;
    border-radius: 16px;
    background-color: white;
    display: flex;
    flex-direction: column;
    justify-content: center;
   
    box-shadow: -5px 10px 5px rgba(0,0,0,0.5), 0 1px 2px rgba(0,0,0,0.24);
    transition: all 0.5s cubic-bezier(.25,.8,.25,1);
}

.card1 {
    margin: 2.5%;
    padding: 2.5%;
    color: #171717;
    border-radius: 16px;
    background-color: white;
    display: flex;
    flex-direction: column;
    justify-content: center;
    width:30em;
    height:6em;
    box-shadow: -5px 10px 5px rgba(0,0,0,0.5), 0 1px 2px rgba(0,0,0,0.24);
    transition: all 0.5s cubic-bezier(.25,.8,.25,1);
}


.card:hover {
    box-shadow: -15px 15px 28px #171717, 0 10px 10px #171717;
    transform: translateY(-25px);;
}
.card1:hover {
    box-shadow: -15px 15px 28px #171717, 0 10px 10px #171717;
    transform: translateY(-25px);;
}
.bod {
   background-image: linear-gradient(190deg, red, yellow)
}
</style>
    <style>
        #testim
        {
            text-align: center;
            font-family: 'Hammersmith One' , sans-serif;
            text-shadow: 3px 3px 0px rgba(90, 90, 90, 1);
            text-transform: uppercase;
            font-size: 55px;
            color: white;
        }
        
        .testimonials1
        {
            font-size: 14px;
            color: White: line-height: 1.4;
            padding: 13px;
            padding-top: 5px;
            text-align: justify;
            font-family: Arial;
        }
        
        
        #top
        {
            height: 50px;
            width: 100%;
            position: relative;
            top: 20px;
        }
        
        
        #wrap
        {
            display: block;
            width: 160px;
            height: 160px;
            position: relative;
            bottom: 60px;
            left: 2px;
            float: right;
        }
        
        .bubble-bottom
        {
            top: 20px;
            background: #e6f9ff;
            border-radius: 25px;
            height: 80%;
            color: Black;
        }
        
        #cred
        {
            text-align: left;
            color: #fff;
            text-shadow: 21%;
            font-size: 20px;
            border-bottom: 10px solid #B14646;
            padding-left: 20px;
            padding-bottom: 15px;
        }
        
        #credit
        {
            background: none;
            position: relative;
        }
        
        @media screen and (max-width: 550px)
        {
            #testim
            {
                font-size: 35px;
            }
            #testimonials
            {
                font-size: 18px;
            }
            p#cred
            {
                font-size: 14px;
            }
        
            p#testimonials1
            {
                font-size: 17px;
            }
        
            #top
            {
                height: 30px;
            }
        }
        
        
        
        /*speech bubble*/
        .bubble-bottom
        {
            display: inline-block;
            position: relative;
            -webkit-transform: translateZ(0);
            -ms-transform: translateZ(0);
            transform: translateZ(0);
            box-shadow: 0 0 1px rgba(0, 0, 0, 0);
        }
        
        
        .bubble-bottom:hover:before
        {
            bottom: -10px;
        }
        
    </style>
    <link href='https://fonts.googleapis.com/css?family=Hammersmith+One' rel='stylesheet'
        type='text/css'>
    <style type="text/css">
        .baseBlock1
        {
            background-color: #b52856;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 100px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 30px 30px rgba(0, 0, 0, 0.3);
            border-radius: 10%;
        }
        
        .baseBlock
        {
            background-color: #28a1b5;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 100px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 30px 30px rgba(0, 0, 0, 0.3);
            border-radius: 10%;
        }
        
        .baseBlock2
        {
            background-color: #bb9234;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 100px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 30px 30px rgba(0, 0, 0, 0.3);
            border-radius: 10%;
        }
        
        .baseBlock3
        {
            background-color: #28b574;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 100px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 30px 30px rgba(0, 0, 0, 0.3);
            border-radius: 10%;
        }
        
        .baseBlock4
        {
            background-color: #4428b5;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 100px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 30px 30px rgba(0, 0, 0, 0.3);
            border-radius: 10%;
        }
        .baseBlock5
        {
            background-color: #971dd6eb;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 100px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 30px 30px rgba(0, 0, 0, 0.3);
            border-radius: 10%;
        }
        
         .baseBlock6
        {
            background-color: #0c4150;
            margin: 0px 0px 15px 0px;
            padding: 0 0 15px 0px;
            border-radius: 5px;
            overflow: hidden;
            min-height: 100px;
            -moz-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            -o-transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            transition: all 0.3s cubic-bezier(0.165, 0.84, 0.44, 1);
            box-shadow: 0 30px 30px rgba(0, 0, 0, 0.3);
            border-radius: 10%;
        }
        
        .baseBlock:hover, .baseBlock4:hover, .baseBlock3:hover, .baseBlock2:hover, .baseBlock1:hover, .baseBlock5:hover
        {
            -webkit-transform: translate(0, -8px);
            -moz-transform: translate(0, -8px);
            -ms-transform: translate(0, -8px);
            -o-transform: translate(0, -8px);
            transform: translate(0, -8px);
            box-shadow: 0 0px 0px rgba(0, 0, 0, 0);
        }
        
        .card-text
        {
            color: White;
            font-weight: 400; /*padding-left: 2em;*/
            font-family: 'Gill Sans' , 'Gill Sans MT' , Calibri, 'Trebuchet MS' , sans-serif;
            font-size: 17px;
            text-align: center;
        }
        
        .card-title
        {
            color: White;
            text-align: center;
            font-size: 20px;
        }
        
        span
        {
        }
        
        .count-title
        {
            font-size: 40px;
            font-weight: normal;
            margin-top: 10px;
            margin-bottom: 0;
            text-align: center;
        }
        
        .count-text
        {
            font-size: 13px;
            font-weight: normal;
            margin-top: 10px;
            margin-bottom: 0;
            text-align: center;
        }
        
        .fa-2x
        {
            margin: 0 auto;
            float: none;
            display: table;
            color: #ffffff;
        }
        
        h2
        {
            color: #ffffff;
        }
        i
        {
            color: #ffffff;
        }
    </style>
    <script type="text/javascript">
        (function ($) {
            $.fn.countTo = function (options) {
                options = options || {};

                return $(this).each(function () {
                    // set options for current element
                    var settings = $.extend({}, $.fn.countTo.defaults, {
                        from: $(this).data('from'),
                        to: $(this).data('to'),
                        speed: $(this).data('speed'),
                        refreshInterval: $(this).data('refresh-interval'),
                        decimals: $(this).data('decimals')
                    }, options);

                    // how many times to update the value, and how much to increment the value on each update
                    var loops = Math.ceil(settings.speed / settings.refreshInterval),
                        increment = (settings.to - settings.from) / loops;

                    // references & variables that will change with each update
                    var self = this,
                        $self = $(this),
                        loopCount = 0,
                        value = settings.from,
                        data = $self.data('countTo') || {};

                    $self.data('countTo', data);

                    // if an existing interval can be found, clear it first
                    if (data.interval) {
                        clearInterval(data.interval);
                    }
                    data.interval = setInterval(updateTimer, settings.refreshInterval);

                    // initialize the element with the starting value
                    render(value);

                    function updateTimer() {
                        value += increment;
                        loopCount++;

                        render(value);

                        if (typeof (settings.onUpdate) == 'function') {
                            settings.onUpdate.call(self, value);
                        }

                        if (loopCount >= loops) {
                            // remove the interval
                            $self.removeData('countTo');
                            clearInterval(data.interval);
                            value = settings.to;

                            if (typeof (settings.onComplete) == 'function') {
                                settings.onComplete.call(self, value);
                            }
                        }
                    }

                    function render(value) {
                        var formattedValue = settings.formatter.call(self, value, settings);
                        $self.html(formattedValue);
                    }
                });
            };

            $.fn.countTo.defaults = {
                from: 0,               // the number the element should start at
                to: 0,                 // the number the element should end at
                speed: 1000,           // how long it should take to count between the target numbers
                refreshInterval: 100,  // how often the element should be updated
                decimals: 0,           // the number of decimal places to show
                formatter: formatter,  // handler for formatting the value before rendering
                onUpdate: null,        // callback method for every time the element is updated
                onComplete: null       // callback method for when the element finishes updating
            };

            function formatter(value, settings) {
                return value.toFixed(settings.decimals);
            }
        } (jQuery));

        jQuery(function ($) {
            // custom formatting example
            $('.count-number').data('countToOptions', {
                formatter: function (value, options) {
                    return value.toFixed(options.decimals).replace(/\B(?=(?:\d{3})+(?!\d))/g, ',');
                }
            });

            // start all the timers
            $('.timer').each(count);

            function count(options) {
                var $this = $(this);
                options = $.extend({}, options || {}, $this.data('countToOptions') || {});
                $this.countTo(options);
            }
        });

    </script>
    <style type="text/css">
    .header-center{
        text-align:center;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>
  <div class="col-lg-12" style="padding-bottom:10px;background-color:White" >
    <p style="border: 1px ridge #ECEFF5;background-color:#F4F6FB;	color:#052033;	font-size:20px;	padding:5px;width:100%;	line-height:30px;	text-align:center;	font-weight:bold;">
                    Screen Reader Access</p>
    <p style="text-align:justify;color:black !important">
        The Department of Information Technology website complies with World Wide Web Consortium (W3C) Web Content Accessibility Guidelines (WCAG) 2.0 level AA. This will enable people with visual impairments access the website using assistive technologies, such as screen readers. The information of the website is accessible with different screen readers, such as JAWS.
    </p>
     <p style="font-weight:bold;color:black !important">Following table lists the information about different screen readers:</p>
    
     <style type="text/css">
        table.tableSReader { border-collapse:collapse; }
        table.tableSReader td, table.myTable th { border:1px solid black;padding:5px; }

        </style>
    <table class="tableSReader slink" width="750px">
        <tr style="background-color:#F4F6FB;">
            <td style="width:250px;text-align:center;font-weight:bold;color:black !important">Screen Reader</td>
            <td style="width: 350px;text-align:center;font-weight:bold;color:black !important">Website</td>
            <td style="text-align:center;font-weight:bold;color:black !important">Free / Commercial</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">Non Visual Desktop Access (NVDA)</td>
            <td style="width: 350px;"><a href="http://sourceforge.net/projects/nvda/files/releases/2012.3.1/nvda_2012.3.1.exe/download" style="text-decoration:none;" target="_blank"> http://www.nvda-project.org/ </a></td>
            <td style="color:black">Free</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">Screen Access For All (SAFA)</td>
            <td style="width: 350px;"><a href="http://www.nabdelhi.org/NAB_SAFA.htm" style="text-decoration:none;" target="_blank">http://www.nabdelhi.org/NAB_SAFA.htm</a></td>
            <td style="color:black">Free</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">System Access To Go</td>
            <td style="width: 350px;"><a href="http://www.satogo.com/" style="text-decoration:none;" target="_blank">http://www.satogo.com/</a></td>
            <td style="color:black">Free</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">Thunder</td>
            <td style="width: 350px;"><a href=" 	http://www.screenreader.net/index.php?pageid=2 " style="text-decoration:none;" target="_blank"> 	http://www.screenreader.net/index.php?pageid=2 </a></td>
            <td style="color:black">Free</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">WebAnywhere</td>
            <td style="width: 350px;"><a href=" 	http://webanywhere.cs.washington.edu/wa.php " style="text-decoration:none;" target="_blank"> 	http://webanywhere.cs.washington.edu/wa.php </a></td>
            <td style="color:black">Free</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">Hal</td>
            <td style="width: 350px;"><a href="http://www.yourdolphin.co.uk/productdetail.asp?id=5" style="text-decoration:none;" target="_blank">http://www.yourdolphin.co.uk/productdetail.asp?id=5</a></td>
            <td style="color:black">Commercial</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">JAWS</td>
            <td style="width: 350px;"><a href="http://www.freedomscientific.com/jaws-hq.asp " style="text-decoration:none;" target="_blank">http://www.freedomscientific.com/jaws-hq.asp </a></td>
            <td style="color:black">Commercial</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">Supernova</td>
            <td style="width: 350px;"><a href="http://www.yourdolphin.co.uk/productdetail.asp?id=1" style="text-decoration:none;" target="_blank">http://www.yourdolphin.co.uk/productdetail.asp?id=1</a></td>
            <td style="color:black">Commercial</td>
        </tr>
        <tr>
            <td style="width:250px;color:black !important">Window-Eyes</td>
            <td style="width: 350px;"><a href=" 	http://www.gwmicro.com/Window-Eyes/ " style="text-decoration:none;" target="_blank"> 	http://www.gwmicro.com/Window-Eyes/ </a></td>
            <td style="color:black">Commercial</td>
        </tr>
    </table>
</div>
    </center>
</asp:Content>

