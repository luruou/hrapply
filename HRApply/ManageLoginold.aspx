<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageLoginold.aspx.cs" Inherits="ApplyPromote.ManageLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<script type='text/javascript' src='http://code.jquery.com/jquery-1.5.2.js'></script>
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
</head>

<body>
    <form id="formAccount" runat="server">
       <div class="container">
            <div class="page-header">
                <h3>
                    <asp:Image ID="img_logo" ImageUrl="~/image/tmulogo.png" runat="server" Height="40px" Width="40px" />&nbsp;歡迎使用『教師聘任升等作業系統』</h3>
                <h4>【管理介面】入口</h4>
                <a href="Default.aspx" class="btn"><i class="icon-chevron-left"></i>申請首頁</a>
            </div>
            <div class="row signin_box">
              
                <div class="form-horizontal">

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
              </div>      

        </div>

    </div>
    
    </form>
</body>
</html>
