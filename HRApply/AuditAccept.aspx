<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditAccept.aspx.cs" Inherits="ApplyPromote.AuditAccept" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
    <form id="formAccount" runat="server">
    <nav class="navbar navbar-expand-lg navbar-light bg-light" style="position: fixed; width: 100%;">
            <img src="image/Logo.png" width="130" style="float: left;" />
            <a class="navbar-brand" href="#" style="color: white; font-weight: bold; font-size: 23px">教師新聘升等系統</a>
        </nav>
    <br />
    <br />
    <br />
        
            <h2><b>【外部審查委員】入口</b></h2>
            <hr />
            
        <div class="container">
            <div class="row">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-8">
                    <div class="control-group">
                        帳號：<asp:Label ID="LbAcctEmailAccount" runat="server" ></asp:Label>
                    </div><br/>
                    <div class="control-group">
                        是否同意審查：&nbsp;&nbsp;
                           <asp:DropDownList ID="AuditAgree" runat="server">
                                <asp:ListItem Value="請選擇">請選擇</asp:ListItem>
                                <asp:ListItem Value="1">同意</asp:ListItem>
                                <asp:ListItem Value="0">拒絕</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="EmpSexRFV" runat="server"
                             ControlToValidate="AuditAgree"
                             ErrorMessage="未選取！"  InitialValue="請選擇"></asp:RequiredFieldValidator>
                    </div>                    
                    <br/>
                    <div class="controls">
                        <asp:Button ID="Send" runat="server" Text="送出" OnClick="Send_Click" CssClass="btn btn-success" />

                    </div>
                    <div class="control-label">
                        <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </div>
                </div>
            </div>
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
