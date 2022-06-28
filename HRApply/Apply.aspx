<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Apply.aspx.cs" Inherits="ApplyPromote.Apply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <title>臺北醫學大學-教師新聘升等系統</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <script src="Scripts/jquery-3.3.1.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>


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

    <script type="text/javascript">
        //nav div 方式
        //$(document).on('click', '#nav-home-tab', function () {
        //    document.getElementById('UnTMUSend').hidden = false;
        //    document.getElementById('TMUSend').hidden = true;
        //});

        //$(document).on('click', '#nav-profile-tab', function () {
        //    //document.getElementById('UnTMUSend').hidden = true;
        //    //document.getElementById('TMUSend').hidden = false;
        //    alert(document.getElementById('txt_identity').innerText);
        //    if (document.getElementById('txt_identity').innerText == "") {
        //        document.location.href = "http://glbsys.tmu.edu.tw/OAuth/OAuthCheck.aspx?domainname=glbsys.tmu.edu.tw&systemname=HrApply&callbackurl=default.aspx&sslport=false";
                
        //    }
        //    //document.getElementById('nav-profile-tab')
        //});
        $(function () {
            //$("#nav-profile-tab").click(function () {
            //    window.location.href = "callbackurl.aspx";
            //});
            //alert(1)
            //$("#lkb_UnTMU").click(function () {
            //    alert(2)
            //    $("#lkb_UnTMU").css("background-color", "#0066cc");
            //    $("#lkb_TMU").css("background-color", "#adadad");
            //}); 

            //$("#nav-profile-tab").css("border-color", "#dee2e6 #dee2e6 #fff");
            //$("#nav-profile").visible = true;


            //$("#tabs").tabs();
        });
        
    </script>
</head>
<body>
    <form id="formAccount" class="form-horizontal" runat="server">
        <div class="navbar navbar-inverse navbar-fixed-top " style="background-color: #005ab5">
            <div class="navbar-header">
                <div class="row">
                    <div class="col-sm-12" style="float: left">
                        <img src="image/Logo.png" width="130" style="float: left;padding-top:5px" />
                        <label style="font-size: 24px; color: white; padding-left: 5px; padding-top: 10px; font-weight: bold;">教師新聘升等系統</label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <label>
                            <img src="image/List.png" style="width:17px;padding-bottom:10px"/>
                            <a href="ManageLogin.aspx" style="font-size: 20px; color: white; padding-left: 5px; padding-top: 10px;">學術審查</a>
                        </label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <label>
                            <img src="image/List.png" style="width:17px;padding-bottom:10px"/>
                            <a href="AuditLogin.aspx" style="font-size: 20px; color: white; padding-left: 5px; padding-top: 10px;">外部審查委員</a>
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <br />

        <div class="container">
            <div class="row">
                <div class="col-sm-6">
                    <div class="row-fluid ">
                        <h2><b>注意事項</b></h2>
                        <hr />
                        <ul style="font-size: 20px">
                            <li>首次使用系統，請先<a href="AccountApply.aspx">申請帳號</a>
                            </li>
                            <li>非北醫教職員忘記密碼，請按此<a href="GetPassword.aspx">忘記密碼</a>
                            </li>
                            <li>北醫教職員忘記密碼，請按此<a href="http://www.tmu.edu.tw/v3/app/super_pages.php?ID=research&Sn=13">重新申請</a>
                            </li>
                            <li><a href="https://docs.google.com/document/d/1V0hxLFDTGiwJXmgwBPq_sJOjHB-Vh88jUEEBKSUgJ9s/pub">[系統使用說明]</a>
                            </li>
                            <li>本系統閒置時間不得超過20分鐘</li>
                            <li>系統資料如有問題，請聯繫：<br />
                                資料&操作問題：人資處 林小姐 2028<br />
                                系統問題：資訊處 (02)6638-2736 #1601&nbsp; <a class="icon-envelope" href="mailto:up_group@tmu.edu.tw?Subject=新聘升等系統問題"></a></li>
                        </ul>
                    </div>
                </div>
                <div class="col-sm-6">
                    <h2><b>登入Login</b></h2>
                    <hr />
                    <%--table方式登入--%>
                    <asp:LinkButton ID="lkb_UnTMU" Text="非北醫教職員" runat="server" OnClick="lkb_UnTMU_Click" class="btn btn-primary" style="background-color:#0066cc;border:none" Width="120"/>
                    <asp:LinkButton ID="lkb_TMU" Text="北醫教職員" runat="server" OnClick="lkb_TMU_Click"  class="btn btn-primary" 
                        style="background-color:#adadad;border:none" Width="120"/>
                    <br />
                    <br />
                    <div runat="server" id="dv_UnTMU">
                        <table runat="server" id="tb1" class="table">
                            <tr>
                                <td style="width: 20%">Email帳號</td>
                                <td>
                                    <asp:TextBox ID="ApplyerEmail" runat="server" onFocus="ApplyerEmail_Click" AutoPostBack="true" TabIndex="1" class="form-control"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>密碼</td>
                                <td>
                                    <asp:TextBox ID="ApplyerPwd" runat="server" TextMode="password" TabIndex="2" class="form-control"></asp:TextBox></td>
                            </tr>
                        </table>
                        <table id="SelectKind1" class="table" runat="server">
                            <tr>
                                <td style="width: 20%">申請類別</td>
                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="3" class="form-control">
                                        <asp:ListItem Text="新聘" Value="1" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>申請途徑</td>
                                <td>
                                    <!--新聘目前無申請途徑-->
                                </td>
                            </tr>
                            <tr>
                                <td>申請類型</td>
                                <td><a href="http://hr.tmu.edu.tw/eva/archive.php?class=102" target="_blank">應檢附表單說明</a><br />
                                    <asp:DropDownList ID="DropDownList3" runat="server" TabIndex="5" class="form-control">
                                        <asp:ListItem Value="1" Text="已具部定教師資格" />
                                        <asp:ListItem Value="2" Text="未具部定教師資格_學位送審" />
                                        <asp:ListItem Value="3" Text="未具部定教師資格_著作送審" />
                                        <asp:ListItem Value="4" Text="臨床教師新聘" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div runat="server" id="dv_TMU" visible="false">
                        <table runat="server" id="Table2" class="table">
                            <tr>
                                <td style="width: 25%">登入身份</td>
                                <td>
                                    <b><asp:Label ID="txt_identity" runat="server"  Font-Size="24"></asp:Label></b>
                                    <asp:LinkButton ID="btnLogout" runat="server" Text="(切換帳號)" OnClick="btnLogout_Click" class="btn btn-link" Style="font-size: 14pt; font-weight: bold;color:red" />
                                </td>
                            </tr>
                        </table>
                        <table id="SelectKind" class="table" runat="server">
                            <tr>
                                <td>申請類別</td>
                                <td>
                                    <asp:DropDownList ID="ApplyKindNo" runat="server" DataSourceID="DSAuditKind"
                                        DataTextField="KindName" DataValueField="KindNo" AutoPostBack="true"
                                        OnSelectedIndexChanged="ApplyKindNo_SelectedIndexChanged" TabIndex="3" class="form-control">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="DSAuditKind" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                        SelectCommand="SELECT [KindNo], [KindName] FROM [CAuditKind] Where KindNo in ('1','2')"></asp:SqlDataSource>

                                </td>
                            </tr>
                            <tr>
                                <td>申請途徑</td>
                                <td>
                                    <asp:DropDownList ID="ApplyWayNo" runat="server" DataSourceID="DSAuditWay"
                                        DataTextField="WayName" DataValueField="WayNo" Visible="false" TabIndex="4" class="form-control">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="DSAuditWay" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                        SelectCommand="SELECT [WayNo], [WayName] FROM [CAuditWay]"></asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td>申請類型</td>
                                <td><a href="http://tmu-hr.tmu.edu.tw/zh_tw/T3/T4" target="_blank">應檢附表單說明</a><br />
                                    <asp:DropDownList ID="ApplyAttributeNo" runat="server"
                                        DataTextField="AttributeName"
                                        DataValueField="AttributeNo" TabIndex="5" class="form-control">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="DSAuditAttribute" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                        SelectCommand="SELECT [AttributeNo], [AttributeName] FROM [CAuditAttribute] WHERE ([KindNo] = @KindNo)">
                                        <SelectParameters>
                                            <asp:FormParameter DefaultValue="1" FormField="ApplyKindNo" Name="KindNo"
                                                Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </div>
                   <%-- nav div 方式登入--%>
                    
<%--                        <nav>
                        <div class="nav nav-tabs" id="nav-tab" role="tablist">
                            <a class="nav-item nav-link active" id="nav-home-tab" data-toggle="tab" href="#nav-home" role="tab" aria-controls="nav-home" aria-selected="true" >非北醫登入</a>
                            <a class="nav-item nav-link " id="nav-profile-tab" data-toggle="tab" href="#nav-profile" role="tab" aria-controls="nav-profile" aria-selected="false">北醫登入</a>
                            
                        </div>
                    </nav>
                    <div class="tab-content" id="nav-tabContent">
                        <div class="tab-pane fade show active" id="nav-home" role="tabpanel" aria-labelledby="nav-home-tab">
                            <br />
                            <div>Email帳號</div>
                            <div>
                                <asp:TextBox ID="ApplyerEmail" runat="server" onFocus="ApplyerEmail_Click" AutoPostBack="true" TabIndex="1" class="form-control"></asp:TextBox>
                            </div>
                            <div>密碼</div>
                            <div>
                                <asp:TextBox ID="ApplyerPwd" runat="server" TextMode="password" TabIndex="2" class="form-control"></asp:TextBox><br />
                                <br />
                            </div>

                            <div class="control-group" runat="server" style="width: 100%">
                                <table id="SelectKind1" class="table">
                                    <tr>
                                        <td>申請類別</td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="3" class="form-control">
                                                <asp:ListItem Text="新聘" Value="1" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>申請途徑</td>
                                        <td>
                                            <!--新聘目前無申請途徑-->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>申請類型</td>
                                        <td><a href="http://hr.tmu.edu.tw/eva/archive.php?class=102" target="_blank">應檢附表單說明</a><br />
                                            <asp:DropDownList ID="DropDownList3" runat="server" TabIndex="5" class="form-control">
                                                <asp:ListItem Value="1" Text="已具部定教師資格" />
                                                <asp:ListItem Value="2" Text="未具部定教師資格_學位送審" />
                                                <asp:ListItem Value="3" Text="未具部定教師資格_著作送審" />
                                                <asp:ListItem Value="4" Text="臨床教師新聘" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="nav-profile" role="tabpanel" aria-labelledby="nav-profile-tab">
                            <div style="float: left">
                                <br />
                                <titletextstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                                <instructiontextstyle font-italic="True" forecolor="Black" />
                                <textboxstyle font-size="0.8em" />
                                <loginbuttonstyle backcolor="#FFFBFF" bordercolor="#CCCCCC" borderstyle="Solid" borderwidth="1px" font-names="Verdana" font-size="0.8em" forecolor="#284775" />
                                <layouttemplate>
                                <div style="text-align:left;font-size:larger;">現在登入身份：<br/>
                                    <asp:Label ID="txt_identity" runat="server" Font-Bold="True"></asp:Label> 
                                    <asp:LinkButton ID="btnLogout" runat="server" Text="(切換帳號)" OnClick="btnLogout_Click" class="btn btn-link" style="font-size:14pt;font-weight:bold;"  />
                                    <br/> 
                                </div>
                                </layouttemplate>
                                <br />
                            </div>
                            <asp:ScriptManager ID="ScriptManager1" runat="server" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="control-group" style="width: 100%" runat="server">
                                        <table id="SelectKind" class="table">
                                            <tr>
                                                <td>申請類別</td>
                                                <td>
                                                    <asp:DropDownList ID="ApplyKindNo" runat="server" DataSourceID="DSAuditKind"
                                                        DataTextField="KindName" DataValueField="KindNo" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ApplyKindNo_SelectedIndexChanged" TabIndex="3" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="DSAuditKind" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                        SelectCommand="SELECT [KindNo], [KindName] FROM [CAuditKind] Where KindNo in ('1','2')"></asp:SqlDataSource>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>申請途徑</td>
                                                <td>
                                                    <asp:DropDownList ID="ApplyWayNo" runat="server" DataSourceID="DSAuditWay"
                                                        DataTextField="WayName" DataValueField="WayNo" Visible="false" TabIndex="4" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="DSAuditWay" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                        SelectCommand="SELECT [WayNo], [WayName] FROM [CAuditWay]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>申請類型</td>
                                                <td><a href="http://hr.tmu.edu.tw/eva/archive.php?class=102" target="_blank">應檢附表單說明</a><br />
                                                    <asp:DropDownList ID="ApplyAttributeNo" runat="server"
                                                        DataTextField="AttributeName"
                                                        DataValueField="AttributeNo" TabIndex="5" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="DSAuditAttribute" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                        SelectCommand="SELECT [AttributeNo], [AttributeName] FROM [CAuditAttribute] WHERE ([KindNo] = @KindNo)">
                                                        <SelectParameters>
                                                            <asp:FormParameter DefaultValue="1" FormField="ApplyKindNo" Name="KindNo"
                                                                Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ApplyKindNo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>--%>
                    
<%--                <div id="tabs">
                    <ul>
                        <li><a id="unTmu" href="#tabs-unTmu">非北醫教職員</a></li>
                        <li><a id="tmu" href="#tabs-tmu">北醫教職員</a></li>
                    </ul>
                    <div id="tabs-unTmu">
                        <div class="tab-pane fade show active" id="tabs-unTmu" role="tabpanel" aria-labelledby="unTmu"> 
                            <br />
                            <div>Email帳號</div>
                            <div>
                                <asp:TextBox ID="ApplyerEmail" runat="server" onFocus="ApplyerEmail_Click" AutoPostBack="true" TabIndex="1" class="form-control"></asp:TextBox>
                            </div>
                            <div>密碼</div>
                            <div>
                                <asp:TextBox ID="ApplyerPwd" runat="server" TextMode="password" TabIndex="2" class="form-control"></asp:TextBox><br />
                                <br />
                            </div>

                            <div class="control-group" runat="server" style="width: 100%">
                                <table id="SelectKind1" class="table">
                                    <tr>
                                        <td>申請類別</td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server" TabIndex="3" class="form-control">
                                                <asp:ListItem Text="新聘" Value="1" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>申請途徑</td>
                                        <td>
                                            <!--新聘目前無申請途徑-->
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>申請類型</td>
                                        <td><a href="http://hr.tmu.edu.tw/eva/archive.php?class=102" target="_blank">應檢附表單說明</a><br />
                                            <asp:DropDownList ID="DropDownList3" runat="server" TabIndex="5" class="form-control">
                                                <asp:ListItem Value="1" Text="已具部定教師資格" />
                                                <asp:ListItem Value="2" Text="未具部定教師資格_學位送審" />
                                                <asp:ListItem Value="3" Text="未具部定教師資格_著作送審" />
                                                <asp:ListItem Value="4" Text="臨床教師新聘" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="tabs-tmu">
                        
                            <div style="float: left">
                                <br />
                                <titletextstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" />
                                <instructiontextstyle font-italic="True" forecolor="Black" />
                                <textboxstyle font-size="0.8em" />
                                <loginbuttonstyle backcolor="#FFFBFF" bordercolor="#CCCCCC" borderstyle="Solid" borderwidth="1px" font-names="Verdana" font-size="0.8em" forecolor="#284775" />
                                <layouttemplate>
                                <div style="text-align:left;font-size:larger;">現在登入身份：<br/>
                                    <asp:Label ID="txt_identity" runat="server" Font-Bold="True"></asp:Label> 
                                    <asp:LinkButton ID="btnLogout" runat="server" Text="(切換帳號)" OnClick="btnLogout_Click" class="btn btn-link" style="font-size:14pt;font-weight:bold;"  />
                                    <br/> 
                                </div>
                                </layouttemplate>
                                <br />
                            </div>
                            <asp:ScriptManager ID="ScriptManager1" runat="server" />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="control-group" style="width: 100%" runat="server">
                                        <table id="SelectKind" class="table">
                                            <tr>
                                                <td>申請類別</td>
                                                <td>
                                                    <asp:DropDownList ID="ApplyKindNo" runat="server" DataSourceID="DSAuditKind"
                                                        DataTextField="KindName" DataValueField="KindNo" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ApplyKindNo_SelectedIndexChanged" TabIndex="3" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="DSAuditKind" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                        SelectCommand="SELECT [KindNo], [KindName] FROM [CAuditKind] Where KindNo in ('1','2')"></asp:SqlDataSource>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td>申請途徑</td>
                                                <td>
                                                    <asp:DropDownList ID="ApplyWayNo" runat="server" DataSourceID="DSAuditWay"
                                                        DataTextField="WayName" DataValueField="WayNo" Visible="false" TabIndex="4" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="DSAuditWay" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                        SelectCommand="SELECT [WayNo], [WayName] FROM [CAuditWay]"></asp:SqlDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>申請類型</td>
                                                <td><a href="http://hr.tmu.edu.tw/eva/archive.php?class=102" target="_blank">應檢附表單說明</a><br />
                                                    <asp:DropDownList ID="ApplyAttributeNo" runat="server"
                                                        DataTextField="AttributeName"
                                                        DataValueField="AttributeNo" TabIndex="5" class="form-control">
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="DSAuditAttribute" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                        SelectCommand="SELECT [AttributeNo], [AttributeName] FROM [CAuditAttribute] WHERE ([KindNo] = @KindNo)">
                                                        <SelectParameters>
                                                            <asp:FormParameter DefaultValue="1" FormField="ApplyKindNo" Name="KindNo"
                                                                Type="String" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ApplyKindNo" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                    </div>
                </div>--%>

                    <asp:Button ID="UnTMUSend" runat="server" Text="登入" OnClick="UnTMUSend_Click" class="btn btn-primary" Width="120" style="float:right"/>
                    <asp:Button ID="TMUSend" runat="server" OnClick="TMUSend_Click" class="btn btn-primary" Visible="false" Width="120" Text="進入系統" style="float:right"/>
                </div>

            </div>
        </div>
        <br />
        <div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-6638-2736 #1600
                    <br />
                障礙排除請至<a href="http://glbsys.tmu.edu.tw/oitmanagement/login.aspx" style="color: white">資訊服務平台</a>
            </label>
        </div>
    </form>


</body>
</html>

