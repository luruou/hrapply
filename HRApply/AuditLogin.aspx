<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditLogin.aspx.cs" Inherits="ApplyPromote.AuditLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <style>
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
    <form id="formAccount" class="login-form" action="#" runat="server">


        <nav class="navbar navbar-expand-lg navbar-light bg-light" style="position: fixed; width: 100%;">
            <img src="image/Logo.png" width="130" style="float: left;" />
            <a class="navbar-brand" href="#" style="color: white; font-weight: bold; font-size: 23px">教師新聘升等系統</a>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
            </div>

            <ul class="nav navbar-nav float-xs-right">
                <%-- 放右上按鈕--%>
                <li class="nav-item"><a href='Default.aspx' target="_blank" style="color: white">申請首頁</a></li>
            </ul>
        </nav>
        <br />
        <br />
        <br />
        <br />
        <div class="container" >
            <h2><b>【外部審查委員】入口</b></h2>
            <hr />
            <titletextstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
            <instructiontextstyle font-italic="True" forecolor="Black" />
            <textboxstyle font-size="0.8em" />
            <loginbuttonstyle backcolor="#FFFBFF" bordercolor="#CCCCCC" borderstyle="Solid" borderwidth="1px" font-names="Verdana" font-size="0.8em" forecolor="#284775" />
            <layouttemplate>
            帳號 :
            <asp:TextBox ID="AcctEmailAccount" runat="server" class="form-control" width="300"></asp:TextBox>
            <br/>
            <br/>
            密碼 :
            <asp:TextBox ID="AcctPassword" runat="server" TextMode="Password" class="form-control" width="300"></asp:TextBox>
            <br/>
            <br/>
            <asp:LinkButton ID="Send" runat="server" Text="登入" OnClick="Send_Click" class="btn btn-primary" width="100"/>    
        </layouttemplate>
        </div>

        <br />
        <div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-66382736 #1600
                    <br />
                障礙排除請至<a href="http://glbsys.tmu.edu.tw/oitmanagement/login.aspx" style="color: white">資訊服務平台</a>
            </label>
        </div>
    </form>
</body>
</html>
