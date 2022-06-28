<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyEmp.aspx.cs" Inherits="ApplyPromote.ApplyEmp" MaintainScrollPositionOnPostback="true" %>

<%@ Implements Interface="System.Web.UI.ICallbackEventHandler" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/jquery-1.11.1.min.js" type="text/javascript"></script>
<script src="js/jquery.validate.min.js" type="text/javascript"></script>


<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <link href="css/tabs.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 98px;
        }

        body {
            font-family: 微軟正黑體;
            z-index: 1000;
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
    <form id="formAll" runat="server" enctype="multipart/form-data">

        <%--標題和導覽列--%>
        <nav class="navbar navbar-expand-lg navbar-light bg-light" style="position: fixed; width: 100%; z-index: 1000">
            <img src="image/Logo.png" width="130" style="float: left;" />
            <a class="navbar-brand" href="#" style="color: white; font-weight: bold; font-size: 23px">教師新聘升等系統</a>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
            </div>

            <ul class="nav navbar-nav float-xs-right">
                <%-- 放右上按鈕--%>
                    <li class="nav-item"><asp:LinkButton ID="lkb_logout" runat="server" Text="退出本系統" OnClick="lkb_logout_Click" class="btn btn-large btn-danger" /> </li>
            </ul>
        </nav>
        <%--標題和導覽列end--%>


        <div class="container">
            <br />
            <br />
            <br />
            <br />
            <h4>
                <asp:HiddenField ID="AcctRole" runat="server" />
                <asp:TextBox ID="TbAppSn" runat="server" value="" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TbEmpSn" runat="server" value="" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TbKindNo" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TbWayNo" runat="server" value="" Visible="false"></asp:TextBox>
                <asp:Button ID="BtnReturnBack" runat="server" Text="返回上一頁" OnClick="BtnReturnBack_Click" CssClass="btn btn-danger"
                    AutoPostBack="Ture" OnClientClick="turnoffvalidate()" Visible="false" />
                <%--<asp:Button ID="BtnApplyMore"  runat="server" text="申請其他系所" OnClick="BtnApplyMore_Click" AutoPostBack="Ture"  OnClientClick = "turnoffvalidate()" Visible="false" Font-Bold="True" Font-Italic="False" Font-Size="Medium" Height="36px" Width="184px"></asp:Button>--%>

                <asp:Button ID="BtnApplyMore" runat="server" Text="申請單總表" OnClick="BtnApplyMore_Click" CssClass="btn btn-success" AutoPostBack="Ture" Visible="false" OnClientClick="turnoffvalidate()"></asp:Button>
                <hr />
                <font size="4">
                    <asp:Label ID="AuditNameCN" runat="server" />&nbsp;─&nbsp;
                <asp:Label ID="AuditUnit" runat="server" />&nbsp;─&nbsp;
                <asp:Label ID="AuditJobTitle" runat="server" />&nbsp;─&nbsp;<asp:Label ID="AuditJobType" runat="server" />
                    <br />
                    <asp:Label ID="AuditWayName" runat="server" />&nbsp;─&nbsp;
                <asp:Label ID="AuditKindName" runat="server" />&nbsp;─&nbsp;
                <asp:Label ID="AuditAttributeName" runat="server" Text="Label" />&nbsp;─&nbsp;
                    <font color="red">
                        <asp:Label ID="AuditStatusName" runat="server" Text="" />&nbsp;</font>
                </font>
                <asp:PlaceHolder ID="Reminder" runat="server">
                    <div style="font-size: 16px; background-color: #ffed97; border-radius: 10px; height: 40px;">
                        <div style="padding: 10px">
                            <p style="color: #0066cc">※非醫學系醫療科部合聘教師請先洽系所承辦人。</p>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </h4>
        </div>
        <div class="container">
            <asp:Panel ID="PanelStatus" runat="server">
            </asp:Panel>

            <div class="row">
                <div style="width:100%">
                    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">
                        <Items>
                            <asp:MenuItem Text="基本資料" Value="0" Selected="true" />
                            <asp:MenuItem Text="評議表" Value="1" />
                            <asp:MenuItem Text="學歷資料" Value="2" />
                            <asp:MenuItem Text="經歷資料" Value="3" />
                            <asp:MenuItem Text="教師證書" Value="4" />
                            <asp:MenuItem Text="學術獎勵、榮譽事項" Value="5" />
                            <asp:MenuItem Text="上傳論文&積分" Value="6" />
                            <asp:MenuItem Text="學位論文相關" Value="7" />
                        </Items>
                    </asp:Menu>
                    <asp:Menu ID="Menu2" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">

                        <Items>
                            <asp:MenuItem Text="基本資料" Value="0" Selected="true" />
                            <asp:MenuItem Text="評議表" Value="1" />
                            <asp:MenuItem Text="學歷資料" Value="2" />
                            <asp:MenuItem Text="經歷資料" Value="3" />
                            <asp:MenuItem Text="教師證書" Value="4" />
                            <asp:MenuItem Text="學術獎勵、榮譽事項" Value="5" />
                            <asp:MenuItem Text="上傳論文&積分" Value="6" />
                        </Items>
                    </asp:Menu>
                    <asp:Menu ID="Menu3" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">
                        <Items>
                            <asp:MenuItem Text="基本資料" Value="0" Selected="true" />
                            <asp:MenuItem Text="評議表" Value="1" />
                            <asp:MenuItem Text="學歷資料" Value="2" />
                            <asp:MenuItem Text="經歷資料" Value="3" />
                            <asp:MenuItem Text="教師證書" Value="4" />
                            <asp:MenuItem Text="學術獎勵、榮譽事項" Value="5" />
                            <asp:MenuItem Text="上傳論文&積分" Value="6" />
                            <asp:MenuItem Text="迴避名單" Value="7" />
                        </Items>
                    </asp:Menu>
                    <div class="tabContents">
                        <asp:MultiView ID="MVall" runat="server" ActiveViewIndex="0"
                            OnActiveViewChanged="MultiView1_ActiveViewChanged">
                            <asp:View ID="ViewTeachBase" runat="server">
                                <table id="TableBaseData" runat="server" class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="text-align: right" width="20%">學年度 / 學期：                                         
                                        </td>
                                        <td>
                                            <asp:TextBox ID="VYear" Width="60px" runat="server" Visible="false">
                                            </asp:TextBox><asp:Label runat="server" ID="VYearAndSemester" Text="目前由人資承辦人維護">
                                            </asp:Label><asp:TextBox Width="60px" ID="VSemester" runat="server" Visible="false">
                                            </asp:TextBox>
                                            <asp:Button ID="BtnSaveYear" runat="server" Visible="false" ValidationGroup="1" class="btn btn-primary" OnClick="BtnSaveYear_Click" Text="學年學期修改" AutoPostBack="Ture" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" width="20%">
                                            <font color="red">*</font> 應徵單位：</td>
                                        <td>
                                            <asp:DropDownList ID="AppUnitNo" runat="server" OnSelectedIndexChanged="AppUnitNo_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 應徵職稱：</td>
                                        <td>
                                            <asp:DropDownList ID="AppJobTitleNo" runat="server" AutoPostBack="True" class="form-control"
                                                DataTextField="JobTitleName"
                                                DataValueField="JobTitleNo"
                                                OnSelectedIndexChanged="AppJobTitleNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSJobTitle" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT [JobTitleNo], [JobTitleName] FROM [CJobTitle]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 應徵職別：</td>
                                        <td>
                                            <asp:DropDownList ID="AppJobTypeNo" runat="server" AutoPostBack="True" class="form-control"
                                                DataTextField="JobAttrName"
                                                DataValueField="JobAttrNo"
                                                OnSelectedIndexChanged="AppJobTypeNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSJobAttribute" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT [JobAttrNo], [JobAttrName] FROM [CJobType]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 新聘類型：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="AppAttributeNo" runat="server" class="form-control"
                                                DataTextField="AttributeName" DataValueField="AttributeNo" OnSelectedIndexChanged="AppAttributeNo_Click" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <font size="2">應檢附表單說明</font>(<a href="javascript:window.open('http://tmu-hr.tmu.edu.tw/zh_tw/T3/T4')">參</a>)
                                          <asp:SqlDataSource ID="DSAuditAttribute" runat="server"
                                              ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                              SelectCommand="SELECT [AttributeNo], [AttributeName] FROM [CAuditAttribute] WHERE ([KindNo] = @KindNo)">
                                              <SelectParameters>
                                                  <asp:FormParameter DefaultValue="" FormField="AppKindNo" Name="KindNo"
                                                      Type="String" />
                                              </SelectParameters>
                                          </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 法規依據：</td>
                                        <td>
                                            <asp:Label ID="lbchose" Text="依教師聘任升等實施辦法第(二" runat="server" />)條 第<asp:Label ID="ItemNo" runat="server" />項 第<asp:Label ID="ItemLabel" runat="server"></asp:Label>
                                            款送審。請下方選擇：<br />
                                            <asp:DropDownList ID="ELawNum" runat="server"   class="form-control" ></asp:DropDownList>
                                            <%--<asp:RadioButtonList ID="ELawNum" runat="server"  RepeatLayout="Flow"></asp:RadioButtonList>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">身份証號/居留證號：</td>
                                        <td>
                                            <asp:TextBox ID="EmpIdno" runat="server" title="身份証號/居留證號" value="" Enabled="false" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 生日：</td>
                                        <td>
                                            民國&nbsp;
                                            <asp:DropDownList ID="ddl_EmpBirthYear" runat="server" />年&nbsp;
                                            <asp:DropDownList ID="ddl_EmpBirthMonth" runat="server" >
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月&nbsp; 
                                            <asp:DropDownList ID="ddl_EmpBirthDate" runat="server" />日
                                            <%--<asp:TextBox ID="EmpBirthDay" runat="server" title="生日(民國)" value="" MaxLength="7" class="form-control"></asp:TextBox>--%>
                                            <%--<font size="2">民國93年08月01日，請輸入0930801</font>--%>
                                            <%--<asp:RegularExpressionValidator ID="EmpBirthDayREV" runat="server"
                                                ErrorMessage="請輸入生日七位數"
                                                ControlToValidate="EmpBirthDay"
                                                ValidationExpression="^[0-9]{7,7}$" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 護照號碼：</td>
                                        <td>
                                            <asp:TextBox ID="EmpPassportNo" runat="server" title="護照號碼" value="" MaxLength="9" class="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 英文名：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNameENFirst" runat="server" title="英文名" value="" MaxLength="20" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font>英文姓：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNameENLast" runat="server" title="英文姓" value="" MaxLength="50" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 姓名：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNameCN" runat="server" title="姓名" value="" MaxLength="50" class="form-control"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 性別：</td>
                                        <td>
                                            <asp:DropDownList ID="EmpSex" runat="server" class="form-control">
                                                <asp:ListItem Value="請選擇">請選擇</asp:ListItem>
                                                <asp:ListItem Value="M">男</asp:ListItem>
                                                <asp:ListItem Value="F">女</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 國籍：</td>
                                        <td>
                                            <asp:DropDownList ID="EmpCountry" runat="server" class="form-control"
                                                DataSourceID="DSCountry" DataTextField="code_ddesc" DataValueField="code_no">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSCountry" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:HrConnectionString %>"
                                                SelectCommand="SELECT [code_ddesc], [code_no] FROM [s10_code_d] WHERE ([code_kind] = @code_kind) ORDER BY [code_ddesc]">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="NAT" Name="code_kind" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 出生地-縣市：</td>
                                        <td>
                                            <asp:DropDownList ID="EmpBornCity" runat="server" DataSourceID="DSCity" class="form-control"
                                                DataTextField="cty_name" DataValueField="cty_id">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSCity" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:HrConnectionString %>"
                                                SelectCommand="SELECT [cty_id], [cty_name] FROM [s90_city]"></asp:SqlDataSource>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 電話(宅)：</td>
                                        <td>
                                            <asp:TextBox ID="EmpTelPri" runat="server" title="電話(宅)" value="" class="form-control"
                                                MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 電話(公)：</td>
                                        <td>
                                            <asp:TextBox ID="EmpTelPub" runat="server" title="電話(公)" value="" MaxLength="20" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">E-Mail：</td>
                                        <td>
                                            <asp:TextBox ID="EmpEmail" runat="server" title="E-Mail" Enabled="false" value="" MaxLength="30" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 戶籍地址：</td>
                                        <td>
                                            <asp:TextBox ID="EmpTownAddressCode" runat="server" title="戶籍地址郵遞區號" value="" Width="9%" MaxLength="5" class="form-control"></asp:TextBox>
                                            <asp:TextBox ID="EmpTownAddress" runat="server" title="戶籍地址" value="" MaxLength="100" Width="90%" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 通訊地址：</td>
                                        <td>
                                            <asp:TextBox ID="EmpAddressCode" runat="server" title="通訊地址郵遞區號" value="" Width="9%" MaxLength="5" class="form-control"></asp:TextBox>
                                            <asp:TextBox ID="EmpAddress" runat="server" title="通訊地址" value="" MaxLength="100" Width="90%" class="form-control" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 手機：</td>
                                        <td>
                                            <asp:TextBox ID="EmpCell" runat="server" title="手機" value="" MaxLength="10" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 現任機關及職務：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNowJobOrg" runat="server" title="現任機關及職務" class="form-control"
                                                value="" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 備註：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNote" runat="server" title="備註" value="" MaxLength="100" class="form-control"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 推薦人姓名：</td>
                                        <td>
                                            <asp:TextBox ID="AppRecommendors" runat="server" title="推薦人姓名" value="" MaxLength="30" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 推薦日期：
                                        </td>
                                        <td>民國&nbsp;
                                            <asp:DropDownList ID="ddl_AppRecommendYear" runat="server" />年
  <%--                                          <asp:TextBox ID="AppRecommendYear" runat="server" title="推薦日期" value="" MaxLength="3" class="form-control"></asp:TextBox>
                                            <font size="2" color="red">如：民國 93年，請輸入093 </font>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">推薦書(函)上傳： 
                                        </td>
                                        <td>
                                            <asp:Label ID="RecommandNote" runat="server"></asp:Label>
                                            <asp:CheckBox ID="RecommendUploadCB" runat="server" /><font color="green">『請將推薦函檢附在未裝訂資料冊中，或由推薦人直接寄到系所』，再由系所補檔。</font><br>
                                            <asp:TextBox ID="RecommendUploadFUName" runat="server" ReadOnly="true"
                                                Style="display: none;" Text=""></asp:TextBox>
                                            <asp:HyperLink ID="RecommendHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <span title="推薦書上傳">
                                                <asp:FileUpload ID="RecommendUploadFU" runat="server" /><br />
                                                <asp:Label ID="Label10" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>

                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 學術專長及研究：</td>
                                        <td>
                                            <asp:TextBox ID="EmpExpertResearch" runat="server" title="學術專長及研究" MaxLength="50" class="form-control"
                                                value=""></asp:TextBox>
                                            <font color="red" size="2">(字數限制為25字) </font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 照片上傳：</td>
                                        <td>
                                            <span title="照片上傳">
                                                <asp:CheckBox ID="EmpPhotoUploadCB" runat="server" Enabled="false" />
                                                <asp:TextBox ID="EmpPhotoUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text=""></asp:TextBox>
                                                <asp:Image ID="EmpPhotoImage" runat="server" Height="100px" Visible="false" Width="80px" />
                                                <asp:FileUpload ID="EmpPhotoUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label9" runat="server"><font color="red">(限jpg, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 身份證上傳：</td>
                                        <td>
                                            <span title="身份證上傳">
                                                <asp:CheckBox ID="EmpIdnoUploadCB" runat="server" Enabled="false" />
                                                <asp:TextBox ID="EmpIdnoUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text=""></asp:TextBox>
                                                <asp:HyperLink ID="EmpIdnoHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="EmpIdnoUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label8" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 最高學歷證件上傳：</td>
                                        <td>
                                            <span title="畢業證書,學位證書或文憑影本(國外學歷須經我國駐外館處驗證)">
                                                <asp:CheckBox ID="EmpDegreeUploadCB" runat="server" Enabled="false" />
                                                <asp:TextBox ID="EmpDegreeUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text=""></asp:TextBox>
                                                <asp:HyperLink ID="EmpDegreeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="EmpDegreeUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label7" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 教師資格審查資料<br/>切結書：</td>
                                        <td>
                                            <span title="教師資格審查資料切結書上傳">
                                                <asp:CheckBox ID="AppDeclarationUploadCB" runat="server" Enabled="false" />
                                                <asp:TextBox ID="AppDeclarationUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text="" MaxLength="100"></asp:TextBox>
                                                <asp:HyperLink ID="AppDeclarationHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="AppDeclarationUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label6" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="2">其他
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Table ID="Table1" Width="100%" runat="server" BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="0" CellSpacing="0">
                                                <asp:TableRow ID="AppPPMTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell style="text-align: right" Width="10%">
                                                <font color="red">*</font> 研究計劃主持： 	
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="研究計劃主持上傳">
                                                            <asp:CheckBox ID="AppPPMUploadCB" runat="server" Enabled="false" />
                                                            <asp:TextBox ID="AppPPMUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox>
                                                            <asp:HyperLink ID="AppPPMHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="AppPPMUploadFU" runat="server" />
                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="AppTeacherCaTableRow" runat="Server" Visible="false" Width="100%">
                                                    <asp:TableCell style="text-align: right" Width="20%">
                                                <font color="red">*</font> 教育部教師資格證書影本/現職等部定證書：<br /> 	
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="教育部教師資格證書影本上傳">
                                                            <asp:CheckBox ID="AppTeacherCaUploadCB" runat="server" Enabled="false" />
                                                            <asp:TextBox ID="AppTeacherCaUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox>
                                                            <asp:HyperLink ID="AppTeacherCaHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="AppTeacherCaUploadFU" runat="server" />

                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="AppDrCaTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell  style="text-align: right" Width="20%">
                                                <font color="red">*</font> 醫師證書：<br /> 	
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="只有醫師需上傳證書">
                                                            <asp:CheckBox ID="AppDrCaUploadCB" runat="server" Enabled="false" />
                                                            <asp:TextBox ID="AppDrCaUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox>
                                                            <asp:HyperLink ID="AppDrCaHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="AppDrCaUploadFU" runat="server" />
                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="OtherTeachingTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell style="text-align: right">
                                                <font color="red">*</font> 教學成果：
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="教學成果上傳">
                                                            <asp:CheckBox ID="AppOtherTeachingUploadCB" runat="server" Enabled="false" />
                                                            <asp:TextBox ID="AppOtherTeachingUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox>
                                                            <asp:HyperLink ID="AppOtherTeachingHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="AppOtherTeachingUploadFU" runat="server" />
                                                        </span><br/>
                                                        (一學年之授課內容、時數及授課課表、教學評議分數、教師成長活動)
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="OtherServiceTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell style="text-align: right">
                                                <font color="red">*</font> 服務成果：
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="服務成果上傳">
                                                            <asp:CheckBox ID="AppOtherServiceUploadCB" runat="server" Enabled="false" />
                                                            <asp:TextBox ID="AppOtherServiceUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox>
                                                            <asp:HyperLink ID="AppOtherServiceHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="AppOtherServiceUploadFU" runat="server" />
                                                        </span>
                                                        <br />(聘書、醫院或診所服務證明...依個人需要) 	
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="ResumeTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell>
                                        <font color="red">*</font> 履歷表CV上傳：                                        
                                        <asp:Label runat="server" ForeColor="#FF3300">(僅申請一般專任教師適用)</asp:Label> 
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="含基本資料、主要學歷、主要經歷、代表著作、特殊貢獻等清單">
                                                            <asp:CheckBox ID="AppResumeUploadCB" runat="server" Enabled="false" />
                                                            <asp:TextBox ID="AppResumeUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text="" MaxLength="100"></asp:TextBox>
                                                            <asp:HyperLink ID="AppResumeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="AppResumeUploadFU" runat="server" />
                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="ThesisScoreTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell>
                                        <font color="red">*</font> 論文積分表：
                                        <asp:Label runat="server" ForeColor="#FF3300">(僅申請一般專任教師適用)</asp:Label> 
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="論文積分表">
                                                            <asp:CheckBox ID="ThesisScoreUploadCB" runat="server" Enabled="false" />
                                                            <asp:TextBox ID="ThesisScoreUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text="" MaxLength="100"></asp:TextBox>
                                                            <asp:HyperLink ID="ThesisScoreUploadHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="ThesisScoreUploadFU" runat="server" />
                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                            </asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td colspan="2">
                                            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="auto-style1">
                                            <asp:Button ID="EmpBaseModifySave" runat="server" OnClick="EmpBaseModifySave_Click" Text="修改" AutoPostBack="Ture" Visible="false" />
                                            <asp:Button ID="EmpBaseSave" runat="server" OnClick="EmpBaseSave_Click" Text="送出申請" AutoPostBack="Ture" class="btn btn-primary" />
                                            &nbsp;
                                        <asp:Button ID="EmpBaseTempSave" runat="server" OnClientClick="turnoffvalidate()" OnClick="EmpBaseTempSave_Click" Text="暫存" AutoPostBack="Ture" class="btn" ForeColor="White" Style="background-color: #d26900" />
                                            &nbsp;
                                        <asp:Button ID="BtnApplyPrint" runat="server" Text="履歷表下載" OnClick="BtnResumeDownload_Click" OnClientClick="turnoffvalidate()" AutoPostBack="Ture" class="btn btn-success" />
                                            &nbsp;
                                        <asp:Button ID="BtnThesisScore" runat="server" Text="論文積分表下載" OnClick="BtnThesisScore1Download_Click" OnClientClick="turnoffvalidate()" AutoPostBack="Ture" class="btn btn-success" />
                                            &nbsp;
                                        <%--<font color='blue'>『送出申請』、『印履歷表』前注意是否有「<font color='red'>紅色</font>錯誤提示」，確認資料上傳！</font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewSelfReview" runat="server">
                                <table id="Table2" runat="server" class="table table-bordered table-condensed">

                                    <tr>
                                        <td style="text-align:right" width="15%">教學經驗：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfTeachExperience" runat="server" TextMode="Multiline"
                                                class="form-control"
                                                placeholder="請輸入資料(包括曾擔任課程)"
                                                Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >研究經驗：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfReach" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料" class="form-control" Text=""
                                                Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >發展潛力分析：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfDevelope" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料" class="form-control"
                                                Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >專長評估：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfSpecial" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(60字為限)" class="form-control"
                                                Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >尚待加強部份：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfImprove" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料" class="form-control"
                                                Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >對本校及本單位<br />預期貢獻：
                                            </td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfContribute" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)" class="form-control"
                                                Height="100px" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >與其他領域<br />合作能力：
                                            </td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfCooperate" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)" class="form-control"
                                                Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >研究及教學計劃：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfTeachPlan" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)" class="form-control"
                                                Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right" >個人生涯展望：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfLifePlan" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)" class="form-control"
                                                Height="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="SelfReviewModifySave" runat="server" Text="修改" OnClick="EmpBaseModifySave_Click" Visible="false" />
                                            <asp:Button ID="SelfReviewSave" runat="server" Text="儲存" OnClick="SelfReviewSave_Click" class="btn btn-primary" Width="100" Style="float: right" />
                                        </td>
                                    </tr>

                                </table>
                            </asp:View>
                            <asp:View ID="ViewTeachEdu" runat="server">
                                <div>
                                    <div style="float: right">
                                        <asp:Button runat="server" ID="teachEduAdd" OnClick="teachEduAdd_Click" class="btn btn-primary" Text="新增學歷"></asp:Button>
                                    </div>
                                </div>
                                <br />
                                <hr />
                                <div class="modal fade" id="modalTeachEdu">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            
                                            <div class="modal-body">
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">X
                                              </button>
                                                <br/>
                                                <br/>
                                                <table border="1" width="100%" cellspacing="0" cellpadding="0" class="table table-bordered table-condensed">
                                                    <tr>
                                                        <td style="text-align: right">修課地點：</td>
                                                        <td>
                                                            <asp:DropDownList ID="TeachEduLocal" runat="server" DataSourceID="DSCountry" class="form-control"
                                                                DataTextField="code_ddesc" DataValueField="code_no"
                                                                OnSelectedIndexChanged="TeachEduLocal_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            &nbsp;<font color="red" size="2">(請由大學學歷填至最高學歷)</font>
                                                            <asp:TextBox ID="TBIntEduSn" Visible="false" runat="server"></asp:TextBox><br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">學校名稱：</td>
                                                        <td>
                                                            <asp:TextBox ID="TeachEduSchool" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">系所：</td>
                                                        <td>
                                                            <asp:TextBox ID="TeachEduDepartment" runat="server" MaxLength="30" class="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">學位：</td>
                                                        <td>
                                                            <asp:DropDownList ID="TeachEduDegree" runat="server" DataSourceID="DSDegree" class="form-control"
                                                                DataTextField="DegreeName" DataValueField="DegreeNo">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="DSDegree" runat="server"
                                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                                SelectCommand="SELECT [DegreeNo], [DegreeName] FROM [CDegree]"></asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">修業日期：</td>
                                                        <td>民國 
                                            <asp:DropDownList runat="server" ID="ddl_TeachEduStartYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="ddl_TeachEduStartMonth">
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月&nbsp; 
                            <%--<asp:TextBox ID="TeachEduStartYM" runat="server" Width="80px" MaxLength="5" class="form-control"></asp:TextBox>--%>
                                                            &nbsp;~ 民國 
                                                            
                                                            <asp:DropDownList runat="server" ID="ddl_TeachEduEndYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="ddl_TeachEduEndMonth">
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月
                            <%--<asp:TextBox ID="TeachEduEndYM" runat="server" Width="80px" MaxLength="5" class="form-control"></asp:TextBox>--%>
                                                            <%--&nbsp;<font color="red" size="2">如：民國79年2月 ~ 民國89年10月，請輸入07902 ~ 08910</font>--%> </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right">修業別：</td>
                                                        <td>
                                                            <asp:DropDownList ID="TeachEduDegreeType" runat="server" DataSourceID="DSDegreeType" class="form-control"
                                                                DataTextField="DegreeTypeName" DataValueField="DegreeTypeNo">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="DSDegreeType" runat="server"
                                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                                SelectCommand="SELECT [DegreeTypeNo], [DegreeTypeName] FROM [CDegreeType]"></asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center">
                                                            <asp:Button ID="TeachEduSave" runat="server" Text="新增" OnClick="TeachEduSave_Click" class="btn btn-primary" Width="100" />
                                                            <asp:Button ID="TeachEduUpdate" runat="server" Text="更新" OnClick="TeachEduUpdate_Click" class="btn btn-primary" Width="100" Style="color: white" Visible="false" />
                                                            <asp:Button ID="TeachEduClear" runat="server" Text="清除" OnClick="TeachEduClear_Click" Visible="false" />
                                                        </td>
                                                    </tr>
<%--                                                    <tr>
                                                        <td colspan="2" style="text-align: center">
                                                            <asp:Label ID="MessageLabelEdu" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:GridView ID="GVTeachEdu" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="EduSn" DataSourceID="DSTeacherEdu"
                                                ForeColor="Black" GridLines="Horizontal"
                                                OnRowDataBound="GVTeachEdu_RowDataBound" BackColor="White"
                                                BorderColor="#CCCCCC" BorderWidth="1px" EnableModelValidation="True" CssClass="table table-bordered"
                                                HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="EduSn" runat="server" Text='<%#Bind("EduSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="EduSchool" HeaderText="學校名稱" />
                                                    <asp:BoundField DataField="EduDepartment" HeaderText="系所名稱"
                                                        SortExpression="EduDepartment" />
<%--                                                    <asp:BoundField DataField="EduStartYM" HeaderText="修業期間(起"
                                                        SortExpression="EduStartYM" />
                                                    <asp:BoundField DataField="EduEndYM" HeaderText=" ~迄)"
                                                        SortExpression="EduEndYM" />--%>
                                                    <asp:TemplateField HeaderText="修業期間(起迄)" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EduStartYM" runat="server" Text='<%#Bind("EduStartYM")%>'></asp:Label>~
                                                            <asp:Label ID="EduEndYM" runat="server" Text='<%#Bind("EduEndYM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="EduDegree" HeaderText="學位" HeaderStyle-Width="10%" 
                                                        SortExpression="EduDegree" />
                                                    <asp:BoundField DataField="EduDegreeType" HeaderText="修業別" HeaderStyle-Width="8%" 
                                                        SortExpression="EduDegreeType" />
                                                    <asp:TemplateField HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTeacherEduMod" runat="server" Text="修改" OnClick="TeachEduModData_Click" class="btn btn-warning" Style="color: white"  />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" ItemStyle-ForeColor="White"  HeaderStyle-Width="8%" />
                                                    <asp:TemplateField HeaderText="Local" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="LabEduLocal" runat="server" Text='<%#Bind("EduLocal")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSTeacherEdu" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT a.[EduSn], a.[EduLocal], a.[EmpSn], a.[EduSchool], a.[EduDepartment], b.DegreeName as EduDegree, c.DegreeTypeName as EduDegreeType, a.[EduStartYM], a.[EduEndYM] FROM [TeacherEdu] AS a 
                                     LEFT OUTER JOIN CDegree AS b ON a.EduDegree = b.DegreeNo LEFT OUTER JOIN CDegreeType AS c ON a.EduDegreeType = c.DegreeTypeNo Where EmpSn = @EmpSn"
                                                DeleteCommand="DELETE FROM TeacherEdu WHERE (EduSn = @EduSn)">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="EduSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <asp:Label ID="lb_NoTeachEdu" runat="server" Text="尚無填寫資料。" Visible="false" />
                                        </td>
                                    </tr>
                                </table>


                            </asp:View>
                            <asp:View ID="ViewTeachExp" runat="server">
                                <div>
                                    <asp:CheckBox ID="CBNoTeachExp" runat="server" />
                                    <font color="red">無其他行政/校外經歷者請勾選此項</font>
                                    <div style="float: right;">
                                        <asp:Button runat="server" ID="teachExpAdd" OnClick="teachExpAdd_Click" class="btn btn-primary" Text="新增經歷"></asp:Button>
                                    </div>
                                </div>
                                <hr />
                                <div class="modal fade" id="modalTeachExp">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">X
                                              </button>
                                                <br/>
                                                <br/>
                                <table class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:Table ID="TbTeachExp" runat="server" Style="width: 100%">
                                                <asp:TableRow>
                                                    <asp:TableCell style=" text-align:right" Width="19%">機關：</asp:TableCell><asp:TableCell>
                                                        <asp:TextBox ID="TeachExpOrginization" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="TBIntExpSn" Visible="false" runat="server"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style=" text-align:right">起訖日期：</asp:TableCell><asp:TableCell>
                                                        民國
                                    <%--<asp:TextBox ID="TeachExpStartYM" runat="server" Width="80px" MaxLength="5" class="form-control"></asp:TextBox>--%>
                                                        <asp:DropDownList runat="server" ID="ddl_TeachExpStartYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="ddl_TeachExpStartMonth">
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月&nbsp;
                                                        &nbsp;~ 民國 
                                    <%--<asp:TextBox ID="TeachExpEndYM" runat="server" Width="80px" MaxLength="5" class="form-control"></asp:TextBox>
                                                        &nbsp;<font color="red" size="2">如：民國79年2月 ~ 民國89年10月，請輸入07902 ~ 08910</font>--%>
                                                        <asp:DropDownList runat="server" ID="ddl_TeachExpEndYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="ddl_TeachExpEndMonth">
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style=" text-align:right">單位名稱：</asp:TableCell><asp:TableCell>
                                                        <asp:TextBox ID="TeachExpUnit" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style=" text-align:right">職稱：</asp:TableCell><asp:TableCell>
                                                        <asp:TextBox ID="TeachExpJobTitle" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style=" text-align:right">
                                                        服務證明上傳：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="服務證明上傳">
                                                            <asp:CheckBox ID="TeachExpUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="TeachExpUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox>
                                                            <asp:HyperLink ID="TeachExpHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="TeachExpUploadFU" runat="server" />
                                                        </span>
                                                        <br />
                                                        <asp:Label ID="Label5" runat="server"><font color="red">(檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell colspan="2" Style="text-align: center">
                                                        <asp:Button ID="TeachExpSave" runat="server" Text="新增" OnClick="TeachExpSave_Click" class="btn btn-primary" Width="100" />
                                                        <asp:Button ID="TeachExpUpdate" runat="server" Text="更新" OnClick="TeachExpUpdate_Click" class="btn btn-primary" Style="color: white" Width="100" Visible="false" />
                                                        <asp:Button ID="TeachExpCancel" runat="server" Text="取消" OnClick="TeachExpCancel_Click" Visible="false" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
<%--                                                <asp:TableRow>
                                                    <asp:TableCell colspan="2" Style="text-align: center">
                                                        <asp:Label ID="MessageLabelExp" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>--%>

                                            </asp:Table>
                                        </td>
                                    </tr>
                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table style="width: 100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp"
                                                GridLines="Horizontal" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" 
                                                BorderColor="#CCCCCC" BorderWidth="1px"
                                                OnRowDataBound="GVTeachExp_RowDataBound" BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ExpOrginization" HeaderText="機關"
                                                        SortExpression="ExpOrginization" />
                                                    <asp:BoundField DataField="ExpUnit" HeaderText="單位" SortExpression="ExpUnit" />
                                                    <asp:BoundField DataField="ExpJobTitle" HeaderText="職稱" HeaderStyle-Width="10%"
                                                        SortExpression="ExpJobTitle" />

<%--                                                    <asp:BoundField DataField="ExpStartYM" HeaderText="起訖日期(起"
                                                        SortExpression="ExpStartYM" />
                                                    <asp:BoundField DataField="ExpEndYM" HeaderText=" ~迄)"
                                                        SortExpression="ExpEndYM" />--%>
                                                    
                                                    <asp:TemplateField HeaderText="起訖日期" ItemStyle-Width="13%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ExpStartYM" runat="server" Text='<%#Bind("ExpStartYM")%>'></asp:Label>~
                                                            <asp:Label ID="ExpEndYM" runat="server" Text='<%#Bind("ExpEndYM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="ExpUploadName" HeaderText="上傳檔案" Visible="false"
                                                        SortExpression="ExpUploadName" />--%>
                                                    
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" Visible="false" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_ExpUploadName" runat="server" Text='<%#Bind("ExpUploadName")%>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="上傳附件" HeaderStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkExp" class="btn btn-success" Style="color: white" runat="server">檢視</asp:HyperLink>
                                                            <asp:Label ID="lb_NoUploadExp" runat="server" Visible="false" Text="無附件" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTeacherExpMod" runat="server" Text="修改" class="btn btn-warning" Style="color: white" OnClick="TeachExpModData_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" HeaderStyle-Width="5%" ItemStyle-ForeColor="White" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSTeacherExp" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                DeleteCommand="DELETE FROM TeacherExp WHERE ExpSn = @ExpSn"
                                                SelectCommand="SELECT [ExpSn], [EmpSn], [ExpOrginization], [ExpStartYM], [ExpEndYM], [ExpUnit], [ExpJobTitle],[ExpUploadName] FROM [TeacherExp] Where EmpSn = @EmpSn ORDER BY [ExpStartYM]">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="ExpSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <asp:Label runat="server" ID="lb_NoTeachExp" Text="尚無填寫資料。" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewTeachCa" runat="server">
                                <div>
                                    <asp:CheckBox ID="CBNoTeachCa" runat="server" />
                                            <font color="red">無相關資料者請勾選此項</font><asp:TextBox ID="TBIntCaSn" Visible="false" runat="server"></asp:TextBox>
                                    <div style="float: right;">
                                        <asp:Button runat="server" ID="teachCaAdd" OnClick="teachCaAdd_Click" class="btn btn-primary" Text="新增證書"></asp:Button>
                                    </div>
                                </div>
                                <hr />
                                <div class="modal fade" id="modalTeachCa">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">X
                                              </button>
                                                <br/>
                                                <br/>
                                <table class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:Table ID="TbTeachCa" runat="server" Style="width: 100%">
                                                <asp:TableRow>
                                                    <asp:TableCell style=" text-align:right" Width="19%">
                                                        教師字號：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="TeachCaNumberCN"  runat="server" MaxLength="6" class="form-control"></asp:TextBox>
                                                        &nbsp;<font color="red" size="2">(教師字號請填：講)</font>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style="text-align: right">
                                                        教師證號：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="TeachCaNumber" runat="server" MaxLength="10" class="form-control"></asp:TextBox><font color="red" size="2">(教師證號請填：000001)</font>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style="text-align: right">
                                                        發證學校：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="TeachCaPublishSchool" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style="text-align: right">
                                                        起資年月：</asp:TableCell>
                                                    <asp:TableCell>
                                                        民國 
                                        <%--<asp:TextBox ID="TeachCaStartYM" runat="server" Width="12%" MaxLength="5" class="form-control"></asp:TextBox>
                                                        &nbsp;&nbsp;<font color="red" size="2">如：民國93年08月，請輸入09308</font>--%>
                                                         <asp:DropDownList runat="server" ID="ddl_TeachCaStartYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="ddl_TeachCaStartMonth">
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月&nbsp;
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style=" text-align:right">
                                        通過日期：</asp:TableCell>
                                                    <asp:TableCell>
                                                        民國 
                                        <%--<asp:TextBox ID="TeachCaEndYM" runat="server" Width="12%" MaxLength="5" class="form-control"></asp:TextBox>
                                                        &nbsp; <font color="red" size="2">如：民國93年08月，請輸入09308</font>--%>
                                                         <asp:DropDownList runat="server" ID="ddl_TeachCaEndYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="ddl_TeachCaEndMonth">
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月&nbsp;
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style="text-align: right">
                                                        相關證明上傳：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="相關證明上傳">
                                                            <asp:CheckBox ID="TeachCaUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="TeachCaUploadFUName" runat="server" ReadOnly="true" Style="display: none;" Text=""></asp:TextBox>
                                                            <asp:HyperLink ID="TeachCaHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="TeachCaUploadFU" runat="server" />
                                                        </span>
                                                        <br />
                                                        <asp:Label ID="Label4" runat="server"><font color="red">(檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell colspan="2" Style="text-align: center">
                                                        <asp:Button ID="TeachCaSave" runat="server" Text="新增" OnClick="TeachCaSave_Click" class="btn btn-primary" Width="100" />
                                                        <asp:Button ID="TeachCaUpdate" runat="server" Text="更新" class="btn btn-primary" Style="color: white" OnClick="TeachCaUpdate_Click" Visible="false" Width="100" />
                                                        <asp:Button ID="TeachCaCancel" runat="server" Text="取消" OnClick="TeachCaCancel_Click" Visible="false" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
<%--                                                <asp:TableRow>
                                                    <asp:TableCell colspan="2" Style="text-align: center">
                                                        <asp:Label ID="MessageLabelCa" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>--%>
                                            </asp:Table>
                                        </td>
                                    </tr>
                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVTeachCa" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataSourceID="DSTeacherCa" OnRowDataBound="GVTeachCa_RowDataBound"
                                                ForeColor="Black" GridLines="Horizontal" DataKeyNames="CaSn"
                                                BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" CssClass="table table-bordered table-condensed" BorderStyle="None" EnableModelValidation="True" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="CaSn" runat="server" Text='<%#Bind("CaSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmpSn" HeaderText="EmpSn"
                                                        SortExpression="EmpSn" Visible="False" />
                                                    <asp:BoundField DataField="CaNumberCN" HeaderText="教師字號" HeaderStyle-Width="10%"
                                                        SortExpression="CaNumberCN" />
                                                    <asp:BoundField DataField="CaNumber" HeaderText="教師證號"
                                                        SortExpression="CaNumber" />
                                                    <asp:BoundField DataField="CaPublishSchool" HeaderText="發證學校"
                                                        SortExpression="CaPublishSchool" />
                                                    <asp:BoundField DataField="CaStartYM" HeaderText="起算年月" HeaderStyle-Width="10%"
                                                        SortExpression="CaStartYM" />
                                                    <asp:BoundField DataField="CaEndYM" HeaderText="通過日期" HeaderStyle-Width="10%"
                                                        SortExpression="CaEndYM" />
                                                    <%--<asp:BoundField DataField="CaUploadName" HeaderText="上傳檔案"
                                                        SortExpression="CaUploadName" />--%>

                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" Visible="false" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_CaUploadName" runat="server" Text='<%#Bind("CaUploadName")%>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="上傳附件" HeaderStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkCa" class="btn btn-success" Style="color: white" runat="server">檢視</asp:HyperLink>
                                                            <asp:Label ID="lb_NoUploadCa" runat="server" Visible="false" Text="無附件" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTeacherCaMod" runat="server" Text="修改" class="btn btn-warning" Style="color: white" OnClick="TeachCaModData_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" HeaderStyle-Width="5%" ItemStyle-ForeColor="White" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSTeacherCa" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                DeleteCommand="DELETE FROM TeacherCa WHERE CaSn = @CaSn"
                                                SelectCommand="SELECT CaSn,EmpSn, CaNumberCN, CaNumber, CaPublishSchool, CaStartYM, CaEndYM,CaUploadName FROM TeacherCa Where EmpSn = @EmpSn">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="CaSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <asp:Label ID="lb_NoTeachCa" runat="server" Text="尚無填寫資料。" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewTeachHonor" runat="server">
                                <div>
                                    <asp:CheckBox ID="CBNoHonour" runat="server" />
                                    <font color="red">無相關資料者請勾選此項</font>
                                    <div style="float: right;">
                                        <asp:Button runat="server" ID="teachHonorAdd" OnClick="teachHonorAdd_Click" class="btn btn-primary" Text="新增榮譽"></asp:Button>
                                    </div>
                                </div>
                                <hr />
                                <div class="modal fade" id="modalTeachHonour">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">X
                                              </button>
                                                <br/>
                                                <br/>
                                 <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:Table ID="TbHonour" runat="server" Style="width: 100%">
                                                <asp:TableRow>
                                                    <asp:TableCell style="text-align:right" Width="10%">
                                        日期：</asp:TableCell>
                                                    <asp:TableCell>
                                                        民國
                                                        <asp:DropDownList runat="server" ID="ddl_TeachHorYear" />&nbsp;年
                                        <%--<asp:TextBox ID="TeachHorYear" runat="server" Width="10%" MaxLength="5" class="form-control"></asp:TextBox>
                                                        &nbsp;&nbsp;<font color="red" size="2">如：民國93年，請輸入093</font>--%><asp:TextBox ID="TBIntHorSn" Visible="false" runat="server"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell style="text-align:right">
                                        事項：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <font color="red">限150字</font>
                                                        <asp:TextBox ID="TeachHorDescription" runat="server" Height="100px" class="form-control"
                                                            Text=""
                                                            TextMode="Multiline" MaxLength="300"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" Style="text-align: center">
                                                        <asp:Button ID="TeachHornourSave" runat="server" Text="新增" OnClick="TeachHornorSave_Click" class="btn btn-primary" Width="100" />
                                                        <asp:Button ID="TeachHornourUpdate" runat="server" Text="更新" class="btn btn-primary" Style="color: white" OnClick="TeachHornorUpdate_Click" Visible="false" Width="100" />
                                                        <asp:Button ID="TeachHornourCancel" runat="server" Text="取消" OnClick="TeachHornourCancel_Click" Visible="false" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
<%--                                                <asp:TableRow>
                                                    <asp:TableCell ColumnSpan="2" Style="text-align: center">
                                                        <asp:Label ID="MessageLabelHonour" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>--%>
                                            </asp:Table>
                                        </td>
                                    </tr>

                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table width="100%" class="table table-bordered table-condensed">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVTeachHonour" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" CssClass="table table-bordered table-condensed"
                                                DataKeyNames="HorSn" DataSourceID="DSTeachHonour" EnableModelValidation="True"
                                                ForeColor="Black" GridLines="Horizontal" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="HorSn" runat="server" Text='<%#Bind("HorSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="HorDescription" HeaderText="榮譽事項"
                                                        SortExpression="HorDescription" />
                                                    <asp:BoundField DataField="HorYear" HeaderText="民國年" SortExpression="HorYear" HeaderStyle-Width="8%" />
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTeacherHorMod" runat="server" class="btn btn-warning" Style="color: white"
                                                                OnClick="TeachHorModData_Click" Text="修改" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" HeaderStyle-Width="5%" ItemStyle-ForeColor="White" />
                                                </Columns>
                                            </asp:GridView>
                                                <asp:SqlDataSource ID="DSTeachHonour" runat="server"
                                                    ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                    DeleteCommand="DELETE FROM TeacherHonour WHERE (HorSn = @HorSn)"
                                                    SelectCommand="SELECT HorYear, HorDescription, HorSn, EmpSn FROM TeacherHonour Where EmpSn = @EmpSn">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                    </SelectParameters>
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="HorSn" />
                                                    </DeleteParameters>
                                                </asp:SqlDataSource>
                                                <asp:Label ID="lb_NoTeachHonour" runat="server" Visible="false" Text="尚無填寫資料。" />
                                            </td>
                                        </tr>
                                    </table>
                            </asp:View>

                            <asp:View ID="ViewThesisScore" runat="server">
                                
                                <font color="red"><asp:Label runat="server" Text="個人研究重點與成果：" ID="lb_ReasearchResult" /></font>
                                <asp:HyperLink ID="link_AppReasearchResult" runat="server" Visible="false"></asp:HyperLink>
                                <asp:FileUpload ID="AppReasearchResultUpload" runat="server" />
                                <asp:LinkButton runat="server" Text="儲存" ID="saveReasearchResult" OnClick="saveReasearchResult_Click" class="btn btn-primary" Style="color: white" />
                                <br/><br/>
                                <div align="center" style="background-color: #0072e3; font-size: 14pt"><b style="color: white">研究論文統計( 表A )</b></div>
                                <table id="SCITotal" width="100%"
                                    style="font-size: 11pt; width: 100%; line-height: 120%;" class="table table-bordered table-condensed">
                                    
                                    <tr>
                                        <td colspan="5">SCI、SSCI、EI之期刊論文資料，可就近至各大學圖書館、國家實驗研究院科技政策研究與資訊中心等查閱或上網檢索。上述SCI、SSCI、EI期刊以最新版本為準。
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" width="8%"></td>
                                        <td colspan="3" style="text-align:center">
                                            <p align="center">
                                                <b><u><span style="font-size: 11pt">SCI、SSCI、EI期刊論文</span></u></b>
                                            </p>
                                            (包括填表說明六(一)之1.所列四類論文：正式論文、簡報型論文、病例報告、綜合評論)
                                        </td>
                                        <td align="center" rowspan="2" width="10%">
                                            <p align="left">
                                                <span style="font-size: 11pt">其他學術期刊論文(左列3類以外之期刊論文)</span>
                                            </p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%" style="text-align: center">
                                            <span style="font-size: 11pt">SCI論文</span></td>
                                        <td width="10%" style="text-align: center">
                                            <span style="font-size: 11pt">SSCI論文</span></td>
                                        <td width="10%" style="text-align: center">
                                            <span style="font-size: 11pt">EI論文</span></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span style="font-size: 11pt">第一作者<br/>
                                                論文篇數<br/>
                                                <br>
                                            </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_FSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_FSSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_FSEI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                            <asp:TextBox ID="txt_FNSCI" runat="server" MaxLength="5" Visible="False"
                                                placeholder="0" Text="" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_FSOther" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span style="font-size: 11pt">非第一作者之通訊作者<br/>
                                                論文篇數<br/>
                                            </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFCSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFCSSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFCEI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                            <asp:TextBox ID="txt_NFCNSCI" runat="server" MaxLength="5" Visible="False"
                                                placeholder="0" Text="" Style="text-align: center"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFCOther" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span style="font-size: 11pt">非第一或通訊作者<br/>
                                                之其他序位作者<br/>
                                                論文篇數<br/>
                                            </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFOCSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFOCSSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFOCEI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                            <asp:TextBox ID="txt_NFOCNSCI" runat="server" MaxLength="5" Visible="False"
                                                placeholder="0" Text="" Style="text-align: right"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFOCOther" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span style="font-size: 11pt">總篇(件)數<br />
                                                (以上三項總和)<br />

                                            </span>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="txt_SCI" runat="server" ForeColor="Black" MaxLength="5"
                                                Width="40px" Text="0"></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="txt_SSCI" runat="server" MaxLength="5" Width="40px" Text="0"></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="txt_EI" runat="server" MaxLength="5" Width="40px" Text="0"></asp:Label>
                                            <asp:Label ID="txt_NSCI" runat="server" MaxLength="5" Visible="False" Style="text-align: right"
                                                Width="40px" Text="0"></asp:Label>
                                        </td>
                                        <td style="text-align: center">
                                            <asp:Label ID="txt_Others" runat="server" MaxLength="5" Width="40px" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <font color="red" style="text-align:center">★表A限填數字</font>
                                            <asp:Button ID="Button1" runat="server" OnClick="ThesisScoreReCount_onClick" Style="float: right" class="btn btn-success" Text="統計數量儲存" />
                                        </td>
                                    </tr>
                                </table>
                                <div align="center" style="background-color: #0072e3; font-size: 14pt"><b style="color: white">研究論文積分統計( 表B )</b></div>
                                <br/>
                                <span style="color: black">1.依本校「教師著作升等研究部分最低標準施行要點」規定，以本校最新版之論文歸類計分(研究表現指數計分表)方式行之。SCI、SSCI、EI之期刊論文資料，可就近至各大學圖書館、國科會科技政策研究與資訊中心等查閱或上網檢索。目前SCI、SSCI、EI期刊以<font color='red'>最新</font>版本為準。<br />
                                    2.五年內且<font color="red">符合前職等教師資格</font>之論文計分，最高採計<font
                                        color="red">15</font>篇。<br />
                                    3.填寫說明：<br />
                                    <font color="red">★</font>1.學術論文必須填寫所有作者(按期刊所刊登之原排序)、著作名稱、期刊名稱、<font color="red">年月</font>、卷期、起迄頁數。
                            <br />
                                    <font color="red">★</font>2.專利必須填寫專利名稱、發明人、證書號碼、國別、專利期限。<br />
                                    <font color="red">★</font>3.技術移轉必須填寫技術名稱、技轉金額及對象、<font color="red">年月</font>。<br />
                                    <font color="red">★</font>4.刊登雜誌分類排名<font color="red">以最新版本之SCI及SSCI資料為準。</font><br />
                                    <font color="red">★</font>5.代表著作請於「研究成果分類代碼欄」<font color="red">註明「代表著作」</font><br />
                                </span>
                                <div style="float:right">
                                    <asp:Button ID="ThesisScoreInsert" runat="server" OnClick="ThesisScoreInsert_Click" Text="新增論文" class="btn btn-primary" />
                                </div><br/>
                                <hr/>
                                <div class="modal fade" id="modalThesisScore">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">X
                                              </button>
                                                <br/>
                                                <br/>
                                <table width="100%" class="table table-bordered table-condensed">

                                    <tr>
                                        <td width="22%" style="text-align:right">研究年資：
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="AppResearchYear" runat="server" class="form-control">
                                                <asp:ListItem Value="1">未滿3年(選最佳1篇)</asp:ListItem>
                                                <asp:ListItem Value="2">滿3年(選最佳2篇)</asp:ListItem>
                                                <asp:ListItem Value="4">滿4年(選最佳4篇)</asp:ListItem>
                                                <asp:ListItem Value="7">滿5年以上(選最佳7篇)</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">序號：<asp:TextBox ID="TBIntThesisSn" Visible="false" runat="server"></asp:TextBox><asp:TextBox ID="TBSnNo" Visible="false" runat="server"></asp:TextBox></td>
                                        <td class="style4">
                                            <asp:DropDownList ID="RRNo" runat="server" DataSourceID="DSRR" class="form-control"
                                                DataTextField="RRNameAbbr" DataValueField="RRNo">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSRR" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT [RRNo], [RRNameAbbr], [RRName] FROM [CResearchResult]"></asp:SqlDataSource><br/>
                                            (研究成果分類)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">研究成果名稱：
                                        </td>
                                        <td>
                                            請注意：作者本人請加「+」，通訊作者請加「*」，貢獻相同作者請加「#」<br />
                                            <asp:TextBox ID="AppThesisResearchResult" runat="server" Height="130px"
                                                Text=""
                                                TextMode="Multiline" class="form-control" MaxLength="1000"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">論文發表年月：
                                        </td>
                                        <td>
                                            <%--<asp:TextBox ID="AppThesisPublishYearMonth" runat="server" MaxLength="5" class="form-control"></asp:TextBox>
                                            &nbsp;<span
                                                style="color: rgb(255, 0, 0); font-family: 'Times New Roman'; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">如：民國93年08月，請輸入09308</span>--%>
                                            
                                            民國 
                                            <asp:DropDownList runat="server" ID="ddl_AppThesisPublishYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="ddl_AppThesisPublishMonth">
                                                <asp:ListItem Text="1" Value="01" />
                                                <asp:ListItem Text="2" Value="02" />
                                                <asp:ListItem Text="3" Value="03" />
                                                <asp:ListItem Text="4" Value="04" />
                                                <asp:ListItem Text="5" Value="05" />
                                                <asp:ListItem Text="6" Value="06" />
                                                <asp:ListItem Text="7" Value="07" />
                                                <asp:ListItem Text="8" Value="08" />
                                                <asp:ListItem Text="9" Value="09" />
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="11" Value="11" />
                                                <asp:ListItem Text="12" Value="12" />
                                            </asp:DropDownList>&nbsp;月
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align:right">
                                            論文積分：
                                        </td>
                                        <td>
                                            論文性質分類加權分數(C)：
                                            <asp:TextBox ID="AppThesisC" runat="server" Width="64.5%" value="1" class="form-control"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                            <br/>
                                            刊登雜誌分類排名加權分數(J)：
                                            <asp:TextBox ID="AppThesisJ" runat="server" Width="60%" value="1" class="form-control"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                            <br/>
                                            作者排名加權分數(A)：
                                            <asp:TextBox ID="AppThesisA" runat="server" Width="70%" class="form-control" value="1"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">分數：
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnExecuteEqual" runat="server" OnClick="BtnExecuteEqual_Click" Text="乘積計算" class="btn btn-primary" Style="width: 150px" />
                                            <asp:Label ID="LabelTotalThesisScore" runat="server"></asp:Label><br/>
                                            <font color="red">(請按此計數)</font>
                                            計算公式： C×J×A 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right"><font color="red">『論文』</font>成果<br/>檔案上傳：
                                        </td>
                                        <td>論文名稱：
                                    <asp:TextBox ID="AppThesisName" Width="85%" runat="server" Text="" class="form-control" /><br />
                                            <asp:CheckBox ID="AppThesisUploadCB" runat="server" title="取消勾可刪除" />
                                            <asp:TextBox ID="AppThesisUploadFUName" runat="server" Text="" ReadOnly="true" Visible="false" class="form-control"></asp:TextBox>
                                            <asp:HyperLink ID="AppThesisHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <span title="成果檔案上傳">
                                                <asp:FileUpload ID="AppThesisUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label1" runat="server"><font color="red">(如有接受函請併入上傳檔案, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">IF/排名：<br />
                                           
                                        </td>
                                        <td>IF/排名名稱：
                                            <asp:TextBox ID="AppThesisJournalRefCount" Width="80%"  runat="server" class="form-control" onkeyup="return gIsDigit(value)"></asp:TextBox><br />
                                            <asp:CheckBox ID="AppThesisJournalRefUploadCB" runat="server" title="取消勾可刪除" />
                                            <asp:TextBox ID="AppThesisJournalRefUploadFUName" runat="server" Text="" ReadOnly="true" Visible="false"></asp:TextBox>
                                            <asp:HyperLink ID="AppThesisJournalRefHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <asp:FileUpload ID="AppThesisJournalRefUploadFU" runat="server" /><br />
                                            <asp:Label ID="Label3" runat="server"><font color="red">(請檢附資料庫查詢畫面，無SCI分數者免附, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">此篇是否選為代表著作：
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="RepresentCB" runat="server" /><br />
                                            <asp:Table ID="RepresentTable" runat="server" Style="display: none;">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                中文摘要 
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <asp:TextBox ID="ThesisSummaryCN" runat="server" Height="100px"
                                                            Text=""
                                                            TextMode="Multiline" class="form-control"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                合著人證明(正本pdf)</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="合著人證明(正本)上傳">
                                                            <asp:CheckBox ID="ThesisCoAuthorUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="ThesisCoAuthorUploadFUName" runat="server" Text="" ReadOnly="true" Visible="false"></asp:TextBox>
                                                            <asp:HyperLink ID="ThesisCoAuthorHyperLink" runat="server"></asp:HyperLink>
                                                            <asp:FileUpload ID="ThesisCoAuthorUploadFU" runat="server" />
                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">此篇是否選取計算RPI：
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="CountRPICB" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Button ID="ThesisScoreSave" runat="server" OnClick="ThesisScoreSave_Click" Text="新增" class="btn btn-primary" />
                                            <asp:Button ID="ThesisScoreUpdate" runat="server" Text="更新" OnClick="ThesisScoreUpdate_Click" Visible="false" class="btn btn-primary" Style="color: white" />
                                            <asp:Button ID="ThesisScoreCancel" runat="server" Text="取消" OnClick="ThesisScoreCancel_Click" Visible="false" class="btn btn-danger" Style="color: white" />
                                        </td>
                                    </tr>
<%--                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Label ID="MessageLabelThesis" runat="server" ForeColor="Red" Text=""></asp:Label>
                                        </td>
                                    </tr>--%>
                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVThesisScore" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="SnNo" DataSourceID="DSThesisScore"
                                                BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" Wrap="True" OnPreRender="GVThesisScore_PreRender"
                                                ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GVThesisScore_RowDataBound"
                                                CssClass="table table-bordered table-condensed" BorderStyle="None" EnableModelValidation="True"
                                                HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisSn" runat="server" Text='<%#Bind("ThesisSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="SnNo" runat="server" Text='<%#Bind("SnNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="IsRepresentative" runat="server" Text='<%#Bind("IsRepresentative")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="IsCountRPI" runat="server" Text='<%#Bind("IsCountRPI")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="選為代表著作" ItemStyle-Width="5%">
                                                        <HeaderStyle />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblThesisSign" runat="server" ForeColor="Red"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="選取計算RPI" ItemStyle-Width="5%">
                                                        <HeaderStyle />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCountRPI" runat="server" ForeColor="green"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SnNo" HeaderText="序號" SortExpression="SnNo" HeaderStyle-Width="3%" />
                                                    <asp:BoundField DataField="EmpSn" HeaderText="EmpSn" SortExpression="EmpSn"
                                                        Visible="False" />
                                                    <asp:TemplateField HeaderText="排序">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonSnNoUp" runat="server" ImageUrl="./image/arrow_up.png" OnCommand="ButtonSnNoUp_Click" CommandName="Sort" CommandArgument='<%#Eval("ThesisSn") +","+ Eval("SnNo") %>' />
                                                            <asp:ImageButton ID="ImageButtonSnNoDown" runat="server" ImageUrl="./image/arrow_down.png" OnCommand="ButtonSnNoDown_Click" CommandName="Sort" CommandArgument='<%#Eval("ThesisSn")+","+ Eval("SnNo") %>' OnClick="ButtonSnNoDown_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="RRNo" HeaderText="成果類別代碼" SortExpression="RRNo" />
                                                    <asp:BoundField DataField="ThesisResearchResult" HeaderText="代表性研究成果名稱"
                                                        SortExpression="ThesisResearchResult">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ThesisPublishYearMonth" HeaderText="論文發表年月"
                                                        SortExpression="ThesisPublishYearMonth" />
                                                    <asp:BoundField DataField="ThesisC" HeaderText="論文性質分數(C)"
                                                        SortExpression="ThesisC" />
                                                    <asp:BoundField DataField="ThesisJ" HeaderText="刊登雜誌分類分數(J)"
                                                        SortExpression="ThesisJ" />
                                                    <asp:BoundField DataField="ThesisA" HeaderText="作者排名分數(A)"
                                                        SortExpression="ThesisA" />
                                                    <asp:BoundField DataField="ThesisTotal" HeaderText="總分數"
                                                        SortExpression="ThesisTotal" />
                                                    <asp:BoundField DataField="ThesisUploadName" HeaderText="上傳檔案"
                                                        SortExpression="ThesisUploadName" />

                                                    <asp:TemplateField HeaderText="下載">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkThesis" runat="server" class="btn btn-success" Style="color: white">下載</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkThesisScoreMod" runat="server"
                                                                OnClick="ThesisScoreModData_Click" Text="修改" class="btn btn-warning" Style="color: white" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkThesisDelete" runat="server" class="btn btn-danger" Style="color: white" OnClick="ThesisScoreDeleteData_Click" Text="刪除" OnClientClick="return confirm('確認要是否要刪除？');"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSThesisScore" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                DeleteCommand="DELETE FROM ThesisScore WHERE (ThesisSn = @ThesisSn);
UPDATE ApplyAudit SET AppThesisAccuScore = (SELECT  ISNULL(SUM(CONVERT(INT,ThesisTotal)),'0')   FROM ThesisScore Where a.EmpSn = @EmpSn and (a.AppSn = @AppSn or a.AppSn = '0' ) )"
                                                SelectCommand="SELECT a.ThesisSn, SnNo, EmpSn, RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, CASE   WHEN b.indexB > 0 THEN SUBSTRING(ThesisUploadName,1,b.indexB)+'.pdf'	ELSE  ThesisUploadName  END as ThesisUploadName,  ThesisJournalRefCount,IsRepresentative,IsCountRPI, ThesisBuildDate, ThesisModifyDate FROM ThesisScore  a   LEFT JOIN  (SELECT ThesisSn, CHARINDEX('_'+(SELECT EmpNameCN from EmployeeBase where EmpSn=@EmpSn),ThesisUploadName,0)-1 as indexB from ThesisScore where EmpSn=@EmpSn)  b ON a.ThesisSn = b.ThesisSn    Where a.EmpSn = @EmpSn and (a.AppSn = @AppSn or a.AppSn = '0' ) Order By SnNo">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                    <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="ThesisSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <asp:Label runat="server" ID="lb_NoThesisScore" Visible="false" Text="尚無填寫資料。" />
                                            <br />

                                            <asp:GridView ID="GVThesisScoreCoAuthor" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="ThesisSn" DataSourceID="DSThesisScoreCoAuthor" OnRowDataBound="GVThesisScoreCoAuthor_RowDataBound"
                                                EnableModelValidation="True"
                                                CssClass="table table-bordered table-condensed" BorderWidth="0px" GridLines="None"
                                                HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:BoundField DataField="SnNo" HeaderText="序號" SortExpression="SnNo" />
                                                    <asp:BoundField DataField="ThesisName" HeaderText="論文名稱"
                                                        SortExpression="ThesisName" />
                                                    <asp:BoundField DataField="ThesisJournalRefCount" HeaderText="IF/排名"
                                                        SortExpression="ThesisJournalRefCount" />
                                                    <asp:TemplateField HeaderText="檢附資料下載">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkThesisJournalRef" class="btn btn-success" Style="color: white" runat="server">下載</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="合著人證明">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkThesisCo" runat="server">下載</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ThesisSummaryCN" HeaderText="中文摘要"
                                                        SortExpression="ThesisSummaryCN" />
                                                    <asp:TemplateField HeaderText="ThesisCoAuthorUploadName">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisCoAuthorUploadName" runat="server" Text='<%#Bind("ThesisCoAuthorUploadName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ThesisJournalRefUploadName">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisJournalRefUploadName" runat="server" Text='<%#Bind("ThesisJournalRefUploadName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSThesisScoreCoAuthor" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                DeleteCommand="DELETE FROM ThesisScore WHERE (ThesisSn = @ThesisSn)"
                                                SelectCommand="SELECT ThesisSn, SnNo, EmpSn, RRNo, ThesisName, ThesisUploadName, ThesisJournalRefCount, ThesisJournalRefUploadName, IsRepresentative, ThesisSummaryCN, ThesisCoAuthorUploadName, ThesisBuildDate, ThesisModifyDate FROM ThesisScore Where EmpSn=@EmpSn and (AppSn = @AppSn or AppSn = '0') and (IsRepresentative = 'True' or ThesisJournalRefUploadName <> '') Order By SnNo">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                    <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="ThesisSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td width="20%" style="text-align:right">積分：
                                        </td>
                                        <td>
                                            <asp:Label ID="AppPThesisAccuScore" runat="server" Text=""></asp:Label><br/>(最高採計15篇, 以上各項研究成果分數小數後兩位之總和)
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right">RPI研究表現指數：
                                    <font color="green">
                                        <asp:Label ID="ComputeRPI" runat="server" Text="" ></asp:Label></font><br/>
                                           (自動計數值供使用者參考)
                                        </td>
                                        <td>
                                            <asp:TextBox ID="AppRPI" runat="server" placeholder="0" Text="" class="form-control" Style="width: 75%"></asp:TextBox>
                                            &nbsp;<asp:Button ID="SaveAppRPI" runat="server" OnClick="AppRPISave_Click" Text="儲存RPI" Style="width: 22%" class="btn btn-primary" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewDegreeThesis" runat="server">
                                <asp:Table runat="server" ID="AboutDegree" width="100%" class="table table-bordered table-condensed">
                                    <asp:TableRow runat="server">
                                        <asp:TableCell style="text-align: right" Width="13%">
                                    學位論文著作：
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <span title="學位論文上傳">著作題名(中)： &nbsp;
                                                <asp:TextBox ID="AppDegreeThesisName" runat="server" Text="" Width="86%" class="form-control"></asp:TextBox><br />&nbsp;<font color="red">(限100字)</font><br />
                                                著作題名(英)： &nbsp;
                                                <asp:TextBox ID="AppDegreeThesisNameEng" runat="server" Text="" Width="86%" class="form-control"></asp:TextBox><br />&nbsp;<font color="red">(限100字)</font><br />
                                                <asp:CheckBox ID="AppDegreeThesisUploadCB" runat="server" title="取消勾可刪除" />
                                                <asp:TextBox ID="AppDegreeThesisUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox>
                                                <asp:HyperLink ID="AppDegreeThesisHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="AppDegreeThesisUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label2" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow ID="AboutFgn" runat="server" Visible="false">
                                        <asp:TableCell style="text-align:right">
                                    國外學歷：
                                        </asp:TableCell>
                                        <asp:TableCell>
                                            <a href="http://tmu-hr.tmu.edu.tw/xhr/archive/download?file=5b7f78e54f4d123f5b00031d">持國外學歷需辦理驗證項目</a>
                                            <br/>
                                            <asp:CheckBox ID="AppDFgnEduDeptSchoolAdmitCB" runat="server" />
                                            <font color="red">1.是否為<a href="javascript:window.open('https://www.fsedu.moe.gov.tw/')">教育部國際及兩岸教育司</a>編印之冊列學校</font><br />
                                            <asp:CheckBox ID="AppDFgnDegreeUploadCB" runat="server" title="取消勾可刪除" />
                                            2.國外最高學位畢業證書(須經我國駐外館處驗證）
                                        <asp:TextBox ID="AppDFgnDegreeUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox>
                                            <asp:HyperLink ID="AppDFgnDegreeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <asp:FileUpload ID="AppDFgnDegreeUploadFU" runat="server" /><br />

                                            <asp:CheckBox ID="AppDFgnGradeUploadCB" runat="server" title="取消勾可刪除" />
                                            3.國外學校歷年成績單(須經我國駐外館處驗證）
                                        <asp:TextBox ID="AppDFgnGradeUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox>
                                            <asp:HyperLink ID="AppDFgnGradeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <asp:FileUpload ID="AppDFgnGradeUploadFU" runat="server" /><br />
                                            <asp:CheckBox ID="AppDFgnSelectCourseUploadCB" runat="server" title="取消勾可刪除" />
                                            4.<a href="http://tmu-hr.tmu.edu.tw/xhr/archive/download?file=5b7f78c64f4d123f5b000315">國外學歷修業情形一覽表</a>
                                        <asp:TextBox ID="AppDFgnSelectCourseUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox>
                                            <asp:HyperLink ID="AppDFgnSelectCourseHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <asp:FileUpload ID="AppDFgnSelectCourseUploadFU" runat="server" />
                                            <br/>（修業期限：累計在當地學校修業時間： 碩士學位須滿8個月；博士學位須滿16個月；連續修讀碩、博士學位須滿24個月）。
                                            <br />
                                            <asp:CheckBox ID="AppDFgnEDRecordUploadCB" runat="server" title="取消勾可刪除" />
                                            5.個人出入境紀錄，可至<a href="https://www.immigration.gov.tw/5385/7244/7250/20406/7326/77703/" target="_blank">內政部入出國及移民署網站</a>查詢。
                                        <asp:TextBox ID="AppDFgnEDRecordUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox>
                                            <asp:HyperLink ID="AppDFgnEDRecordHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <asp:FileUpload ID="AppDFgnEDRecordUploadFU" runat="server" /><br />

                                            <asp:Table ID="AboutJPN" runat="server" Visible="false">
                                                <asp:TableRow>
                                                    <asp:TableCell>
                                                        以日本論文博士需另備下列要件，使採認具博士學位：<br />
                                                        <asp:CheckBox ID="AppDFgnJPAdmissionUploadCB" runat="server" title="取消勾可刪除" />
                                                        A.入學許可註冊證：<asp:TextBox ID="AppDFgnJPAdmissionUploadFUName" runat="server"
                                                            ReadOnly="true" Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppDFgnJPAdmissionHyperLink" runat="server"
                                                            Visible="false"></asp:HyperLink>
                                                        <asp:FileUpload ID="AppDFgnJPAdmissionUploadFU" runat="server" />
                                                        <br />
                                                        <asp:CheckBox ID="AppDFgnJPGradeUploadCB" runat="server" title="取消勾可刪除" />
                                                        B.修畢學分成績單<asp:TextBox ID="AppDFgnJPGradeUploadFUName" runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppDFgnJPGradeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                        <asp:FileUpload ID="AppDFgnJPGradeUploadFU" runat="server" /><br />
                                                        <asp:CheckBox ID="AppDFgnJPEnrollCAUploadCB" runat="server" title="取消勾可刪除" />
                                                        C.在學證明及修業年數證明<asp:TextBox ID="AppDFgnJPEnrollCAUploadFUName"
                                                            runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppDFgnJPEnrollCAHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                        <asp:FileUpload ID="AppDFgnJPEnrollCAUploadFU" runat="server" /><br />
                                                        <asp:CheckBox ID="AppDFgnJPDissertationPassUploadCB" runat="server" title="取消勾可刪除" />
                                                        D.通過論文資格考試證明<asp:TextBox ID="AppDFgnJPDissertationPassUploadFUName"
                                                            runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppDFgnJPDissertationPassHyperLink" runat="server"
                                                            Visible="false"></asp:HyperLink>
                                                        <asp:FileUpload ID="AppDFgnJPDissertationPassUploadFU" runat="server" />
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" Style="text-align: center">
                                            <asp:Button ID="DegreeThesisSave" class="btn btn-primary" runat="server" Text="儲存" Width="120px" OnClick="DegreeThesisSave_Click" />
                                        </asp:TableCell>
                                    </asp:TableRow>
                                    <asp:TableRow style="display:none">
                                        <asp:TableCell ColumnSpan="2" Style="text-align: center">
                                            <asp:Label ID="MessageLabelDegreeThesis" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                
                                <div style="float:right;">
                                    <asp:Button runat="server" ID="thesisOralAdd" OnClick="thesisOralAdd_Click" class="btn btn-primary" Text="新增迴避名單" ></asp:Button>
                                </div>
                                <br/>
                                <hr/>
                                <div class="modal fade" id="modalThesisOral">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">X
                                              </button>
                                                <br/>
                                                <br/>
                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="text-align: right" width="15%">身分別：<asp:TextBox ID="TBIntThesisOralSn" Visible="false" runat="server"></asp:TextBox></td>
                                        <td>
                                            <asp:DropDownList ID="ThesisOralType" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">姓名：</td>
                                        <td>
                                            <asp:TextBox ID="ThesisOralName" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">職稱：</td>
                                        <td>
                                            <asp:TextBox ID="ThesisOralTitle" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">服務單位：</td>
                                        <td>
                                            <asp:TextBox ID="ThesisOralUnit" runat="server" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">其他備註：</td>
                                        <td>
                                            <asp:TextBox ID="ThesisOralOther" runat="server" class="form-control"></asp:TextBox><br/><asp:Label ID="AvoidReason" runat="server" Visible="false" Text="(迴避理由)" ForeColor="Red" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Button ID="ThesisOralSave" runat="server" Text="新增" class="btn btn-primary" OnClick="ThesisOralSave_Click" />
                                            <asp:Button ID="ThesisOralUpdate" runat="server" Text="更新" class="btn btn-primary" Style="color: white" OnClick="ThesisOralUpdate_Click" Visible="false" />
                                            <asp:Button ID="ThesisOralCancel" runat="server" Text="取消" class="btn btn-danger" Style="color: white" OnClick="ThesisOralCancel_Click" Visible="false" />
                                        </td>
                                    </tr>
<%--                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <asp:Label ID="MessageLabelThesisOral" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>--%>
                                    </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVThesisOral" runat="server" AutoGenerateColumns="False"
                                                CellPadding="2"
                                                ForeColor="Black" GridLines="None" DataKeyNames="ThesisOralSn" OnRowDataBound="GVThesisOral_RowDataBound"  BorderColor="#CCCCCC" BorderWidth="1px"
                                                BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisOralSn" runat="server" Text='<%#Bind("ThesisOralSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="身分別" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisOralType" runat="server" Text='<%#Bind("ThesisOralType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ThesisOralName" HeaderText="姓名"
                                                        SortExpression="ThesisOralName" />
                                                    <asp:BoundField DataField="ThesisOralTitle" HeaderText="職稱"
                                                        SortExpression="ThesisOralTitle" />
                                                    <asp:BoundField DataField="ThesisOralUnit" HeaderText="服務單位"
                                                        SortExpression="ThesisOralUnit" />
                                                    <asp:BoundField DataField="ThesisOralOther" HeaderText="其他備註"
                                                        SortExpression="ThesisOralOther" />
                                                    <asp:TemplateField HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ThesisOralMod" runat="server" class="btn btn-warning" Style="color: white" OnClick="ThesisOralMod_Click"
                                                                Text="修改" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ThesisOralDel" runat="server" class="btn btn-danger" Style="color: white" OnClick="ThesisOralDel_Click"
                                                                Text="刪除" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label ID="lb_NoThesisOral" runat="server" Visible="false" Text="尚無填寫資料。" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>

        <div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 "http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-2736-1661 #2066 
            </label>
        </div>
    </form>


</body>
<script type="text/javascript">
    $(document).on('change', ':file', function () {  //選取類型為file且值發生改變的
        for (var i = 0; i < this.files.length; i++) {
            var file = this.files[0]; //定義file=發生改的file
            name = file.name; //name=檔案名稱
            size = file.size; //size=檔案大小
            type = file.type; //type=檔案型態
            nameArray = name.split(".")
            //alert('lenth:' + nameArray.length + ',  name:' + nameArray[0])
            if (nameArray[0].toString().match(/[^A-Za-z0-9\u4e00-\u9fa5]/)) {
                alert('檔案名稱請勿使用特殊符號');
                $(this).val('');  //將檔案欄設為空白
            }
            else if (nameArray.length != 2) {
                alert('檔案名稱請勿使用特殊符號');
                $(this).val('');  //將檔案欄設為空白
            }
            else if ($(this).attr('id') == "AppThesisUploadFU" || $(this).attr('id') == "AppThesisJournalRefUploadFU" || $(this).attr('id') == "ThesisCoAuthorUploadFU") {
                if (file.type != 'application/pdf') {
                    alert('檔案格式限pdf'); //顯示警告
                    $(this).val('');
                }
            }
            else if (file.type != 'application/pdf' && file.type != 'image/png' && file.type != 'image/jpg' && file.type != 'image/jpeg') {
                //假如檔案格式不等於 png 、jpg、jpeg、pdf
                //alert('type=' + file.type);
                alert('檔案格式不符合規範'); //顯示警告
                $(this).val(''); //將檔案欄設為空
            }
        }
    });

    $(document).ready(function () {
        if ($("#RepresentCB").is(":checked")) {
            $("#dvPassport").show();
        } else {
            $("#dvPassport").hide();
        }
    });
    function SelChange() {
        var year = Number(document.getElementById("ddl_EmpBirthYear").value);
        var month =  Number(document.getElementById("ddl_EmpBirthMonth").value);
        var date = document.getElementById("ddl_EmpBirthDate");
        //alert("onChange");
        var maxDay = getDaysOfMonth(year + 1911, month);
        //alert(maxDay);
        date.options.length = 0;
        for (var i = 1; i <= maxDay; i++) {
            var opt = document.createElement('option');
            if (i < 10)
                opt.value = "0" + i;
            else
                opt.value = i;
            opt.innerHTML = i;
            if (i == 1) {
                opt.selected = "true";
            }

            date.appendChild(opt);
        }

        function getDaysOfMonth(year, month) {
            switch (month) {
                case 1: case 3: case 5: case 7:
                case 8: case 10: case 12:
                    return 31;
                case 4: case 6: case 9: case 11:
                    return 30;
                default:
                    return 31 - (new Date(year, month - 1, 31)).getDate();
            }
        }

    }

</script>
</html>
