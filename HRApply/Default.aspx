<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ApplyPromote.Apply" %>

<%@ Register Src="~/UserControl/Language.ascx" TagPrefix="uc1" TagName="Language" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>臺北醫學大學--教師新聘升等系統
    </title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/all.min.css" rel="stylesheet" />
    <link href="Content/mdb-4.0.0_colors.min.css" rel="stylesheet" />
    <link href="Content/style.css" rel="stylesheet" />
</head>
<body class="skin-1">
    <form id="formAccount" class="form-horizontal" runat="server">
        <nav class="navbar navbar-expand-lg navbar-dark fixed-top">
            <a class="navbar-brand sys-name font-weight-bold" href="#">
                <!--logo圖-->
                <img alt="" id="logo" src="image/Logo.png" width="130" />
                <!--系統名稱-->
                教師新聘升等系統
            </a>
            <button class="navbar-toggler" type="button" id="navbarSideCollapse" data-toggle="collapse">
                <span class="navbar-toggler-icon"></span>
            </button>

            <!--navbar開始-->
            <div class="navbar-collapse offcanvas-collapse" id="navbarsMenu">
                <!--選單項開始；不需選單項，可移除或註解-->
                <!--選單字數最好中文4~8字內。字數很長的話，選單項最好不要>3個-->
                <ul class="navbar-nav mr-auto">
                    <li class="nav-item">
                        <a class="nav-link" href="ManageLogin.aspx"><%=Resources.Language.Default_Controls_Link_AcademicReview%></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="AuditLogin.aspx"><%=Resources.Language.Default_Controls_Link_ExternalReviewers%></a>
                    </li>
                </ul>
                <!--選單項結束-->
                <hr class="d-lg-none d-block" />
                <uc1:Language runat="server" ID="Language" />

            </div>
            <!--navbar 結束-->
        </nav>

        <div class="container body-content">
            <div class="row">
                <!-- 內文  -->
                <div class="col-lg-6 col-sm-12 mt-4">
                    <!-- 公告訊息 -->
                    <div class="col-12 mt-3 mb-2">
                        <h4 class="border-bottom py-2"><%=Resources.Language.Default_Label_Attention%></h4>
                        <div class="h6">
                            <ul>
                                <li><%=Resources.Language.Default_Label_Attention_Content_1%><a href="AccountApply.aspx"><%=Resources.Language.Default_Controls_Link_SignUp%></a></li>
                                <li><%=Resources.Language.Default_Label_Attention_Conten_2%><a href="GetPassword.aspx"><%=Resources.Language.Default_Controls_Link_ForgetPassword%></a></li>
                                <li><%=Resources.Language.Default_Label_Attention_Conten_3%><a href="http://www.tmu.edu.tw/v3/app/super_pages.php?ID=research&Sn=13"><%=Resources.Language.Default_Controls_Link_TMU_ForgetPassword%></a></li>
                            </ul>
                        </div>
                    </div>
                    <!--col end -->
                    <!-- 服務說明 -->
                    <div class="col-12 mt-3 mb-2">
                        <h4 class="border-bottom py-2"><%=Resources.Language.Default_Label_About%></h4>
                        <div class="h6">
                            <ol>
                                <li><a href="https://docs.google.com/document/d/1V0hxLFDTGiwJXmgwBPq_sJOjHB-Vh88jUEEBKSUgJ9s/pub"><%=Resources.Language.Default_Controls_Link_UserGuide%></a></li>
                                <li><%=Resources.Language.Default_Label_Attention_Conten_4%></li>
                            </ol>
                        </div>
                    </div>
                    <!--col end -->
                    <!-- 聯絡資訊 -->
                    <div class="col-12 mt-3 mb-2">
                        <h4 class="border-bottom py-2"><%=Resources.Language.Default_Label_Contact%></h4>
                        <div class="h6">
                            <ul>
                                <li><%=Resources.Language.Default_Label_Attention_Conten_6%></li>
                                <li><%=Resources.Language.Default_Label_Attention_Conten_7%>
                                    <a href="http://glbsys.tmu.edu.tw/oitmanagement/login.aspx"><%=Resources.Language.Default_Label_Attention_Conten_8%></a>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <!--col end -->
                </div>

                <div class="col-lg-6 col-sm-12 mt-4">
                    <!-- 系統登入 -->
                    <div class="col-12 mt-3 mb-2 h-100 p-4 bg-light border rounded-3">
                        <h4 class="py-2"><%=Resources.Language.Default_Label_Login%></h4>

                        <!-- 自訂留下幾個按鈕 -->

                        <asp:LinkButton ID="lkb_UnTMU" Text="<%$ Resources:Language ,Default_Controls_Button_NotTMU%>" runat="server" OnClick="lkb_UnTMU_Click" class="btn btn-primary" Style="background-color: #0066cc; border: none" Width="120" />
                        <asp:LinkButton ID="lkb_TMU" Text="<%$ Resources:Language ,Default_Controls_Button_TMU%>" runat="server" OnClick="lkb_TMU_Click" class="btn btn-primary" Style="background-color: #adadad; border: none" Width="120" />
                        <hr />
                        <div runat="server" id="dv_UnTMU" visible="true">
                            <table runat="server" id="tb1" class="table">
                                <tr>
                                    <td style="width: 20%">Email</td>
                                    <td>
                                        <asp:TextBox ID="ApplyerEmail" runat="server" onFocus="ApplyerEmail_Click" AutoPostBack="true" TabIndex="1" class="form-control"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td><%=Resources.Language.Default_Label_Login_Password%></td>
                                    <td>
                                        <asp:TextBox ID="ApplyerPwd" runat="server" TextMode="password" TabIndex="2" class="form-control"></asp:TextBox></td>
                                </tr>
                            </table>
                            <table id="SelectKind1" class="table" runat="server">
                                <tr>
                                    <td style="width: 20%"><%=Resources.Language.Default_Label_Login_SelectKind%></td>
                                    <td>
                                        <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="3" class="form-control">
                                            <asp:ListItem Text="<%$ Resources:Language ,Default_Controls_Select_Option_EmploymentApplication%>" Value="1" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><%=Resources.Language.Default_Label_Login_Appliction%></td>
                                    <td>
                                        <!--新聘目前無申請途徑-->
                                    </td>
                                </tr>
                                <tr>
                                    <td><%=Resources.Language.Default_Label_Login_SelectType%></td>
                                    <td><a href="http://tmu-hr.tmu.edu.tw/zh_tw/T3/T4" target="_blank"><%=Resources.Language.Default_Controls_Link_Material%></a><br />
                                        <asp:DropDownList ID="ddlApplicationType" runat="server" TabIndex="5" class="form-control">
                                
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div runat="server" id="dv_TMU" visible="false">
                            <table runat="server" id="Table2" class="table">
                                <tr>
                                    <td style="width: 25%"><%=Resources.Language.Default_Label_LoginIdentity%></td>
                                    <td>
                                        <b>
                                            <asp:Label ID="txt_identity" runat="server" Font-Size="24"></asp:Label></b>
                                        <asp:LinkButton ID="btnLogout" runat="server" Text="<%$ Resources:Language ,Default_Label_SwitchAccounts%>" OnClick="btnLogout_Click" class="btn btn-link" Style="font-size: 14pt; font-weight: bold; color: red" />
                                    </td>
                                </tr>
                            </table>
                            <table id="SelectKind" class="table" runat="server">
                                <tr>
                                    <td><%=Resources.Language.Default_Label_Login_SelectKind%></td>
                                    <td>
                                        <asp:DropDownList ID="ApplyKindNo" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ApplyKindNo_SelectedIndexChanged" TabIndex="3" class="form-control">
                                             <asp:ListItem Text="<%$ Resources:Language ,Default_Controls_Select_Option_EmploymentApplication%>" Value="1" />
                                             <asp:ListItem Text="<%$ Resources:Language ,Default_Controls_Select_Option_Promotion%>" Value="2" />
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td><%=Resources.Language.Default_Label_Login_Appliction%></td>
                                    <td>
                                        <asp:DropDownList ID="ApplyWayNo" runat="server"  Visible="false" TabIndex="4" class="form-control">
                                        </asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td><%=Resources.Language.Default_Label_Login_SelectType%></td>
                                    <td><a href="http://tmu-hr.tmu.edu.tw/zh_tw/T3/T4" target="_blank"><%=Resources.Language.Default_Controls_Link_Material%></a><br />
                                        <asp:DropDownList ID="ApplyAttributeNo" runat="server" TabIndex="5" class="form-control">
                                        </asp:DropDownList>
                                       
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%-- nav div 方式登入--%>


                        <asp:Button ID="UnTMUSend" runat="server" Text="<%$ Resources:Language ,Default_Controls_Button_Login%>" OnClick="UnTMUSend_Click" class="btn btn-primary" Width="120" Style="float: right" />
                        <asp:Button ID="TMUSend" runat="server" OnClick="TMUSend_Click" class="btn btn-primary" Visible="false" Width="120" Text="<%$ Resources:Language ,Default_Controls_Button_Login%>" Style="float: right" />

                    </div>
                    <!--col end -->
                </div>
            </div>
        </div>
    </form>

    <!-- 頁腳 -->
    <div class="footer fixed-bottom mt-3">
        <label class="small pt-2 text-center">
            <%=Resources.Language.MasterPage_Footer_Info1%><br />
            <%=Resources.Language.MasterPage_Footer_Info2%><a href="http://glbsys.tmu.edu.tw/oitmanagement/login.aspx" style="color: white" ><%=Resources.Language.MasterPage_Footer_Info3%></a><br/>
            <%=Resources.Language.MasterPage_Footer_Info4%>
        </label>
    </div>
    <!-- 頁腳end -->

    <div class="toolbar">
        <ul class="list-unstyled">
            <li class="toolbar-item">
                <a href="#" class="top btn btn-dark" data-toggle="tooltip" data-original-title="Top"><i class="fas fa-arrow-up"></i></a>
            </li>
        </ul>
    </div>
    <div id="backDrop" class=""></div>

    <script type="text/javascript" src="Scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="Scripts/popper.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap.min.js"></script>
</body>
</html>

