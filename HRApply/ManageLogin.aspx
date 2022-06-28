<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageLogin.aspx.cs" Inherits="ApplyPromote.ManageLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type='text/javascript' src='http://code.jquery.com/jquery-1.5.2.js'></script>
<head runat="server">
 <title>臺北醫學大學-教師新聘升等系統</title>
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
    <form id="formAccount" class="login-form" action="#" runat="server">
        <div class="container ">
            <h2>【學術審查】入口</h2>
            <hr />
            <titletextstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
            <instructiontextstyle font-italic="True" forecolor="Black" />
            <textboxstyle font-size="0.8em" />
            <loginbuttonstyle backcolor="#FFFBFF" bordercolor="#CCCCCC" borderstyle="Solid" borderwidth="1px" font-names="Verdana" font-size="0.8em" forecolor="#284775" />
            <layouttemplate>
               <div style="text-align:center;font-size:larger;">現在登入身份：<br/>
                   <asp:Label ID="txt_identity" runat="server" Font-Bold="True"></asp:Label> 
                   <asp:Button ID="btnLogout" runat="server" OnClick="btnLogout_Click" Text="(切換帳號)" class="btn btn-link" style="font-size:14pt;font-weight:bold;"  />
                  <br/> 
                   <asp:Button ID="Send" runat="server" OnClick="Send_Click" class="btn  btn-default" style="font-size:14pt;font-weight:bold;" Text="進入系統" />
               </div>
        </layouttemplate>


<%--            <div class="form-horizontal" style="display: none">

                <div class="control-group">
                    <div class="control-label">帳號</div>
                    <div class="controls">
                        <asp:TextBox ID="AcctEmailAccount" runat="server" Text=""></asp:TextBox>

                    </div>
                </div>

                <div class="control-group">
                    <div class="control-label">密碼</div>
                    <div class="controls">
                        <asp:TextBox ID="AcctPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmpPwdRVValidator" runat="server" ControlToValidate="AcctPassword" ErrorMessage="請輸入密碼"></asp:RequiredFieldValidator>

                    </div>
                </div>

                <div class="controls">
                    <asp:Button ID="Send" runat="server" Text="登入" OnClick="Send_Click" CssClass="btn btn-success" />

                </div>
                <div class="control-label">
                    <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" Text=""></asp:Label>
                </div>
            </div>--%>
            <br />

            <h2>注意事項</h2>
            <hr />
            <ul>
                <li>
                    <h4>本系統整合TMU & Google驗証，學術審查請使用完整之TMU Email帳號登入<br />
                        (如：oit@tmu.edu.tw)。
                    </h4>
                </li>
                <li>
                    <h4>新聘升等申請，請由<a href="Default.aspx">申請首頁</a>登入</h4>
                </li>
            </ul>
            <br />
            <h2>聯絡資訊</h2>
            <hr />
            <ul>
                <li>
                    <h4>資料&操作問題：人資處 林小姐 2028<a class="icon-envelope" href="mailto:up_group@tmu.edu.tw?Subject=新聘升等系統問題"></a></h4>
                </li>
                <li>
                    <h4>系統問題：資訊處 (02)6638-2736 #1600</h4>
                </li>
                <li>
                    <h4>障礙排除請至<a target="_blank" href="http://glbsys.tmu.edu.tw/oitmanagement/login.aspx" style="color: blue">資訊服務平台</a></h4>
                </li>
            </ul>
        </div>

        <div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-6638-2736 #1600
                    <br />
                障礙排除請至<a href="http://glbsys.tmu.edu.tw/oitmanagement/login.aspx" style="color: white">資訊服務平台</a>
            </label>
        </div>
        <br />
        <br />
    </form>
</body>
</html>
