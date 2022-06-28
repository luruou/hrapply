<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <link href="Content/jquery-ui-timepicker-addon.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/modernizr-2.6.2.js"></script>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/jquery.datetimepicker.min.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
        .hiddencol {
            display: none;
        }

        body {
            font-family: 微軟正黑體;
        }

        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: #005ab5;
            color: white;
            text-align: center;
        }

        .bold {
            font-weight: bold;
        }
        img {
            image-rendering: -moz-crisp-edges;
            image-rendering: -o-crisp-edges;
            image-rendering: -webkit-optimize-contrast;
            image-rendering: crisp-edges;
            -ms-interpolation-mode: nearest-neighbor;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light" style="position: fixed; width: 100%;z-index:1000">
            <img src="image/Logo.png" width="130" style="float: left;" />
            <a class="navbar-brand" href="#" style="color: white; font-weight: bold; font-size: 24px">教師新聘升等系統</a>

            <div class="collapse navbar-collapse container" id="navbarSupportedContent">
        
            </div>
     
        </nav>
        <div class=""  style="padding-bottom:100px">
            <div>
            <br />
            <br />
            <br />
            <br />
                 ERROR:
    <asp:PlaceHolder ID="phError" runat="server"></asp:PlaceHolder>
            </div>
        </div>

        <%--Footer--%>

        <div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-66382736 #1600
            </label>
        </div>
    </form>
</body>
</html>
