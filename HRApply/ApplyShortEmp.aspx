<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyShortEmp.aspx.cs" Inherits="ApplyPromote.ApplyEmp" MaintainScrollPositionOnPostback="true" %>

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
                <asp:TextBox ID="TbAppSn" runat="server" value="" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TbEmpSn" runat="server" value="" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TbKindNo" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TbWayNo" runat="server" value="" Visible="false"></asp:TextBox>
                <asp:Button ID="BtnReturnBack" runat="server" Text="返回上一頁" OnClick="BtnReturnBack_Click" CssClass="btn btn-danger" AutoPostBack="Ture" Visible="false" OnClientClick="turnoffvalidate()" />
                <asp:Button ID="BtnApplyMore" runat="server" Text="申請單總表" OnClick="BtnApplyMore_Click" AutoPostBack="Ture" CssClass="btn btn-success" Visible="false" OnClientClick="turnoffvalidate()"></asp:Button>
                <asp:Label ID="Label1" Visible="false" runat="server" Text="Label" Font-Size="16">(若您看到的填寫資料，比之前少，此乃正常。待初審過後，才會要求您填寫較多的資料，感謝！)</asp:Label>

                <hr />
                <div style="font-size: 18px">
                    <asp:Label ID="AuditNameCN" runat="server" />&nbsp;─&nbsp;
                    <asp:Label ID="AuditUnit" runat="server" />&nbsp;─&nbsp;
                    <asp:Label ID="AuditJobTitle" runat="server" />&nbsp;&nbsp;
                    <asp:Label ID="AuditJobType" runat="server" />
                    <br />
                    <asp:Label ID="AuditWayName" runat="server" />&nbsp;─&nbsp;
                    <asp:Label ID="AuditKindName" runat="server" />&nbsp;─&nbsp;
                    <asp:Label ID="AuditAttributeName" runat="server" Text="Label" />&nbsp;─&nbsp;
                    <asp:Label ID="AuditStatusName" runat="server" Text="" Style="color: red" />
                </div>
                <asp:PlaceHolder ID="Reminder" runat="server">
                    <div style="font-size: 16px; background-color: #ffed97; border-radius: 10px; height: 80px;">
                        <div style="padding: 10px">
                            <p style="color: #0066cc"><font color="red">※申請審查分兩階段，第一階段僅需填寫「基本資料」「學歷資料」「經歷資料」，待初審通過後，再詳填資料。</font></p>
                            <p style="color: #0066cc">※非醫學系醫療科部合聘教師請先洽系所承辦人。</p>
                        </div>
                    </div>
                </asp:PlaceHolder>
            </h4>
            <asp:Panel ID="PanelStatus" runat="server">
            </asp:Panel>

            <div class="row">
                <div style="width:100%">
                    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">
                        <Items>
                            <asp:MenuItem Text="基本資料" Value="0" Selected="true" />
                            <asp:MenuItem Text="學歷資料" Value="1" />
                            <asp:MenuItem Text="經歷資料" Value="2" />
                        </Items>
                    </asp:Menu>
                    <div class="tabContents">
                        <asp:MultiView ID="MVall" runat="server" ActiveViewIndex="0"
                            OnActiveViewChanged="MultiView1_ActiveViewChanged">
                            <asp:View ID="ViewTeachBase" runat="server">
                                <table id="TableBaseData" runat="server" class="table table-bordered table-condensed">

                                    <tr style="display:none">
                                        <td style="text-align: right" width="18%">學年度 / 學期：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="VYear" runat="server" Visible="false"></asp:TextBox><asp:Label runat="server" ID="VYearAndSemester" Text="目前由人力資源處承辦人維護"></asp:Label><asp:TextBox ID="VSemester" runat="server" Visible="false"></asp:TextBox>
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
                                            <font color="red">*</font>應徵職稱：</td>
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
                                            <asp:DropDownList ID="AppJobTypeNo" runat="server"
                                                DataTextField="JobAttrName"
                                                DataValueField="JobAttrNo" OnSelectedIndexChanged="AppJobTypeNo_SelectedIndexChanged" AutoPostBack="true" class="form-control">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSJobAttribute" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT [JobAttrNo], [JobAttrName] FROM [CJobType]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font>新聘類型：
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
                                            <font color="red">*</font>法規依據：</td>
                                        <td>
                                            <asp:Label ID="lbchose" Text="依教師聘任升等實施辦法第(二" runat="server" />)條 第<asp:Label ID="ItemNo" runat="server" />項 第<asp:Label ID="ItemLabel" runat="server"></asp:Label>
                                            款送審。請下方選擇：<br />
                                            <asp:DropDownList ID="ELawNum" runat="server"  class="form-control" ></asp:DropDownList>
                                            <%--<asp:RadioButtonList ID="ELawNum" runat="server" RepeatLayout="Flow" ></asp:RadioButtonList>--%>
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
                                            <font color="red">*</font>生日：</td>
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
<%--                                            <asp:TextBox ID="EmpBirthDay" runat="server" title="生日(民國)" value="" MaxLength="7" class="form-control"></asp:TextBox>
                                            <font size="2" style="color: red">民國93年08月01日，請輸入0930801</font>--%>
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
                                            <font color="red">*</font>手機：</td>
                                        <td>
                                            <asp:TextBox ID="EmpCell" runat="server" title="手機" value="" MaxLength="10" class="form-control"></asp:TextBox>
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
                                            <font color="red">*</font> 現任機關及職務：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNowJobOrg" runat="server" title="現任機關及職務" class="form-control"
                                                value="" MaxLength="50"></asp:TextBox>
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
                                        <td style="text-align: right">推薦書(函)上傳： 
                                        </td>
                                        <td>
                                            <asp:Label ID="RecommandNote" runat="server"></asp:Label>
                                            <asp:CheckBox ID="RecommendUploadCB" runat="server" /><font color="green">『請將推薦函檢附在未裝定資料冊中，或由推薦人直接寄到系所』。之後再由系所從上傳。</font><br>
                                            <asp:TextBox ID="RecommendUploadFUName" runat="server" ReadOnly="true"
                                                Style="display: none;" Text=""></asp:TextBox>
                                            <asp:HyperLink ID="RecommendHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <span title="推薦書上傳">
                                                <asp:FileUpload ID="RecommendUploadFU" runat="server" />

                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font> 學術專長及研究：</td>
                                        <td>
                                            <asp:TextBox ID="EmpExpertResearch" runat="server" title="學術專長及研究" MaxLength="50" class="form-control"
                                                value=""></asp:TextBox>
                                            <font size="2" style="color: red">(此欄位必填，字數限制為25字。) </font>
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
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font>履歷表CV上傳：</td>
                                        <td>
                                            <span title="含基本資料、主要學歷、主要經歷、代表著作、特殊貢獻等清單">
                                                <asp:CheckBox ID="AppResumeUploadCB" runat="server" Enabled="false" />
                                                <asp:Label ID="AppResumeUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text="" MaxLength="100"></asp:Label>
                                                <asp:HyperLink ID="AppResumeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="AppResumeUploadFU" runat="server" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font>論文積分表：<br />
                                            <span title="範本下載">
                                                <asp:HyperLink ID="H1" runat="server" Visible="false" NavigateUrl="">一般系所</asp:HyperLink>
                                                <%--<br />--%>
                                                <asp:HyperLink ID="H2" runat="server" Visible="false" NavigateUrl="">通識教育中心</asp:HyperLink>
                                                <%--<br />--%>
                                                <asp:HyperLink ID="H3" runat="server" Visible="false" NavigateUrl="">人文暨社會科學院</asp:HyperLink>
                                                <%--<br />--%>
                                            </span>
                                        </td>
                                        <td>
                                            <span title="論文積分表">
                                                <asp:CheckBox ID="ThesisScoreUploadCB" runat="server" Enabled="false" />
                                                <asp:Label ID="ThesisScoreUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text="" MaxLength="100"></asp:Label>
                                                <asp:HyperLink ID="ThesisScoreUploadHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="ThesisScoreUploadFU" runat="server" />
                                                <br/>
                                            <br />
                                                
                                            <font size="3" style="color: red">範本下載：<a href="http://tmu-hr.tmu.edu.tw/zh_tw/pl1" target="_blank"  >聘任升等相關法令規章</a></font>
                                            </span>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td style="text-align: right">
                                            <font color="red">*</font>請問您於何招募平台知悉本校徵才訊息?(可複選)：<br />
                                        </td>
                                        <td>

                                            <asp:CheckBoxList ID="cblQuestionnaire" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                            </asp:CheckBoxList>
                                            <asp:TextBox ID="txtQuestionnaireOther" runat="server" class="form-control" placeholder="其他..."></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr style="display:none">
                                        <td colspan="2">
                                            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="EmpBaseModifySave" runat="server" OnClick="EmpBaseModifySave_Click" Text="修改"
                                                AutoPostBack="Ture" Visible="false" class="btn btn-warning" />
                                            <asp:Button ID="EmpBaseSave" runat="server" OnClick="EmpBaseSave_Click" Text="送出申請"
                                                AutoPostBack="Ture" class="btn btn-primary" />
                                            &nbsp;
                                    <asp:Button ID="EmpBaseTempSave" runat="server" OnClientClick="turnoffvalidate()"
                                        OnClick="EmpBaseTempSave_Click" Text="暫存" AutoPostBack="Ture" class="btn" Style="color: white; background-color: #d26900" />
                                            &nbsp;
                                    <asp:Button ID="BtnApplyPrint" runat="server" Text="履歷表下載" OnClick="BtnResumeDownload_Click"
                                        OnClientClick="turnoffvalidate()" AutoPostBack="Ture" class="btn btn-success" />

                                        </td>
                                    </tr>
                                </table>
                                <table id="Table2" runat="server" class="table table-bordered table-condensed" visible="false">

                                    <tr>
                                        <td>教學經驗：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfTeachExperience" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(包括曾擔任課程)"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>研究經驗：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfReach" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>發展潛力分析：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfDevelope" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>專長評估：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfSpecial" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(60字為限)"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>尚待加強部份：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfImprove" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>對本校及本單位預期貢獻：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfContribute" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>與其他領域合作能力：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfCooperate" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>研究及教學計劃：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfTeachPlan" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>個人生涯展望：</td>
                                        <td>
                                            <asp:TextBox ID="EmpSelfLifePlan" runat="server" TextMode="Multiline"
                                                placeholder="請輸入資料(請詳細填寫)"
                                                Height="100px" Width="400px" MaxLength="500"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="SelfReviewModifySave" runat="server" Text="修改" class="btn btn-warning" Style="color: white" OnClick="EmpBaseModifySave_Click" Visible="false" />
                                            <asp:Button ID="SelfReviewSave" runat="server" Text="儲存" OnClick="SelfReviewSave_Click" />
                                        </td>
                                    </tr>

                                </table>
                            </asp:View>
                            <asp:View ID="ViewTeachEdu" runat="server">
                                <div>
                                    <div style="float: right;">
                                        <asp:Button runat="server" ID="teachEduAdd" OnClick="teachEduAdd_Click" class="btn btn-primary" Text="新增學歷"></asp:Button>
                                    </div>
                                </div><br />
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
                                                <table class="table table-bordered table-condensed">
                                                    <tr>
                                                        <td style="text-align: right">修課地點：</td>
                                                        <td>
                                                            <asp:DropDownList ID="TeachEduLocal" runat="server" DataSourceID="DSCountry" class="form-control"
                                                                DataTextField="code_ddesc" DataValueField="code_no">
                                                            </asp:DropDownList>
                                                        &nbsp;<font color="red" size="2">(請由大學學歷填至最高學歷)</font>
                                                        <asp:TextBox ID="TBIntEduSn" Visible="false" runat="server"></asp:TextBox><br />
                                                        <asp:SqlDataSource ID="DSCountry" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:HrConnectionString %>"
                                                            SelectCommand="SELECT [code_ddesc], [code_no] FROM [s10_code_d] WHERE ([code_kind] = @code_kind) ORDER BY [code_ddesc]">
                                                            <SelectParameters>
                                                                <asp:Parameter DefaultValue="NAT" Name="code_kind" Type="String" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource></td>
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
                                                                SelectCommand="SELECT [DegreeNo], [DegreeName] FROM [CDegree] where DegreeNo in ('50','60','70')"></asp:SqlDataSource>
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
                            <%--<asp:TextBox ID="TeachEduEndYM" runat="server" Width="80px" MaxLength="5" class="form-control"></asp:TextBox>--%>
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
                                            <%--<font color="red" size="2">如：民國79年2月 ~ 民國89年10月，請輸入07902 ~ 08910</font>--%></td>
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
                                                            <asp:Button ID="TeachEduSave" runat="server" Text="新增" OnClick="TeachEduSave_Click" class="btn btn-primary" />
                                                            <asp:Button ID="TeachEduUpdate" runat="server" Text="更新" OnClick="TeachEduUpdate_Click" class="btn btn-success" Style="color: white" Visible="false" />
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

                                <table style="width: 100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVTeachEdu" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="EduSn" DataSourceID="DSTeacherEdu"
                                                ForeColor="Black" GridLines="Horizontal"
                                                BackColor="White"
                                                BorderColor="#CCCCCC" BorderWidth="1px" EnableModelValidation="True" CssClass="table table-bordered"
                                                HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" ShowHeaderWhenEmpty="False" EmptyDataText="尚無填寫資料">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="EduSn" runat="server" Text='<%#Bind("EduSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="EduSchool" HeaderText="學校名稱" />
                                                    <asp:BoundField DataField="EduDepartment" HeaderText="系所名稱" SortExpression="EduDepartment" />
                                                    <%--                             <asp:BoundField DataField="EduStartYM" HeaderText="修業期間(起" SortExpression="EduStartYM" />
                                        <asp:BoundField DataField="EduEndYM" HeaderText=" ~迄)" SortExpression="EduEndYM" />--%>


                                                    <asp:TemplateField HeaderText="修業期間(起迄)" HeaderStyle-Width="13%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="EduStartYM" runat="server" Text='<%#Bind("EduStartYM")%>'></asp:Label>~
                                                        <asp:Label ID="EduEndYM" runat="server" Text='<%#Bind("EduEndYM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EduDegree" HeaderText="學位" SortExpression="EduDegree" HeaderStyle-Width="10%" />
                                                    <asp:BoundField DataField="EduDegreeType" HeaderText="修業別" HeaderStyle-Width="8%" SortExpression="EduDegreeType" />
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTeacherEduMod" runat="server" Text="修改" OnClick="TeachEduModData_Click" class="btn btn-warning" Style="color: white" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" HeaderStyle-Width="5%" ItemStyle-ForeColor="White" />
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
                                                <asp:Table ID="TbTeachExp" runat="server" class="table table-bordered table-condensed">
                                                <asp:TableRow>
                                                    <asp:TableCell Style="text-align: right">機關：</asp:TableCell><asp:TableCell>
                                                        <asp:TextBox ID="TeachExpOrginization" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                        <asp:TextBox ID="TBIntExpSn" Visible="false" runat="server"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Style="text-align: right">起訖日期：</asp:TableCell><asp:TableCell>
                                                            民國
                                    <%--<asp:TextBox ID="TeachExpStartYM" runat="server" Width="80px" MaxLength="5" class="form-control"></asp:TextBox>--%>
                                                            <asp:DropDownList runat="server" ID="TeachExpStartYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="TeachExpStartMonth">
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
                                                            <asp:DropDownList runat="server" ID="TeachExpEndYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="TeachExpEndMonth">
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
                                                    <asp:TableCell Style="text-align: right">單位名稱：</asp:TableCell><asp:TableCell>
                                                        <asp:TextBox ID="TeachExpUnit" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Style="text-align: right">職稱：</asp:TableCell><asp:TableCell>
                                                        <asp:TextBox ID="TeachExpJobTitle" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell Style="text-align: right">服務證明上傳：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="服務證明上傳">
                                                            <asp:CheckBox ID="TeachExpUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="TeachExpUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox>
                                                            <asp:HyperLink ID="TeachExpHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="TeachExpUploadFU" runat="server" />
                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow>
                                                    <asp:TableCell colspan="2" Style="text-align: center">
                                                        <asp:Button ID="TeachExpSave" runat="server" Text="新增" OnClick="TeachExpSave_Click" class="btn btn-primary" />
                                                        <asp:Button ID="TeachExpUpdate" runat="server" Text="更新" OnClick="TeachExpUpdate_Click" class="btn btn-success" Style="color: white" Visible="false" />
                                                        <asp:Button ID="TeachExpCancel" runat="server" Text="取消" OnClick="TeachExpCancel_Click" Visible="false" />

                                                        <%--<asp:Label ID="MessageLabelExp" runat="server" Text="" ForeColor="Red"></asp:Label>--%>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <table style="width: 100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp"
                                                ForeColor="Black" GridLines="Horizontal" BackColor="White"
                                                BorderColor="#CCCCCC" BorderWidth="1px" OnRowDataBound="GVTeachExp_RowDataBound" EnableModelValidation="True" CssClass="table table-bordered"
                                                HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White"  ShowHeaderWhenEmpty="False" EmptyDataText="尚無填寫資料">
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
                                                    <%--                                        <asp:BoundField DataField="ExpStartYM" HeaderText="起訖日期(起" 
                                            SortExpression="ExpStartYM" />
                                        <asp:BoundField DataField="ExpEndYM" HeaderText=" ~迄)" 
                                            SortExpression="ExpEndYM" />--%>
                                                    <asp:TemplateField HeaderText="修業期間(起迄)" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ExpStartYM" runat="server" Text='<%#Bind("ExpStartYM")%>'></asp:Label>~
                                                <asp:Label ID="ExpEndYM" runat="server" Text='<%#Bind("ExpEndYM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:BoundField DataField="ExpUploadName" HeaderText="上傳檔案"
                                                        SortExpression="ExpUploadName" />--%>
                                                    
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" Visible="false" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_ExpUploadName" runat="server" Text='<%#Bind("ExpUploadName")%>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="上傳附件" HeaderStyle-Width="9%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkExp" runat="server">檢視</asp:HyperLink>
                                                            <asp:Label ID="lb_NoUploadExp" runat="server" Visible="false" Text="無附件" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTeacherExpMod" runat="server" Text="修改" OnClick="TeachExpModData_Click" class="btn btn-warning" Style="color: white" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" ItemStyle-ForeColor="White" HeaderStyle-Width="5%" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSTeacherExp" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                DeleteCommand="DELETE FROM TeacherExp WHERE ExpSn = @ExpSn"
                                                SelectCommand="SELECT [ExpSn], [EmpSn], [ExpOrginization], [ExpStartYM], [ExpEndYM], [ExpUnit], [ExpJobTitle],[ExpUploadName] FROM [TeacherExp] Where EmpSn = @EmpSn">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="ExpSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
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
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽  02-6638-2736 #1600
            </label>
        </div>
    </form>
</body>
<script type="text/javascript">
    var ImageSizeLimit = 100000;  //上傳上限，單位:byte 
    var isCheckImageSize = true;  //是否檢查圖片檔案大小 
    function turnoffvalidate() {
    }

    function RecommendUploadCBV(sender, args) {
        if (document.getElementById("<%=RecommendUploadCB.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }


    function EmpDegreeUploadCBV(sender, args) {
        if (document.getElementById("<%=EmpDegreeUploadCB.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }

    function AppResumeUploadCBV(sender, args) {
        if (document.getElementById("<%=AppResumeUploadCB.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }

    function ThesisScoreUploadCBV(sender, args) {
        if (document.getElementById("<%=ThesisScoreUploadCB.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }

    function ValidateFloat(e, pnumber) {
        if (!/^\d+[.]?\d*$/.test(pnumber)) {
            $(e).val(/^\d+[.]?\d*/.exec($(e).val()));
        }
        return false;
    }

    //==================================================
    //功能名稱:Function gIsDigit
    //功能用途:檢查是否為數字
    //參    數: strVal 
    //  
    //傳 回 值:
    //   True    
    //   False  
    //==================================================
    function gIsDigit(strVal) {
        if (strVal == "")
            return false;
        for (var i = 0; i < strVal.length; i++) {
            if (strVal.charCodeAt(i) < 48 || strVal.charCodeAt(i) > 57 || strVal.charCodeAt(i) == 45)
                if (i != 0)
                    return false;
                else if (i == 0 && strVal.charCodeAt(0) != 45)
                    return false;
        }
        return true;
    }

    $(document).ready(function () {

        var file;

        $(window).load(function () {
            // Handler for .load() called.
            var isChecked = $('#RepresentCB').is(':checked') ? true : false;
            if (isChecked) {
                $('#RepresentTable').show();
            }
            else {
                $('#RepresentTable').hide();
            }
            isChecked = $('#CBNoTeachExp').is(':checked') ? true : false;
            if (isChecked) {
                $('#TbTeachExp').hide();
            }
            else {
                $('#TbTeachExp').show();
            }

        });



        $('#CBNoTeachExp').change(function () {
            var isChecked = $('#CBNoTeachExp').is(':checked') ? true : false;
            if (isChecked) {
                $('#TbTeachExp').hide();
            }
            else {
                $('#TbTeachExp').show();
            }
        });

        $('#TeachEduLocal').change(function () {
            var selLocal = $('#TeachEduLocal option:selected').val();
            if (selLocal != "TWN") {
                $('#AboutFgn').show();
                if (selLocal == "JPN") {
                    $('#AboutJPN').show();
                }
            }
        });
    })
    $(document).on('change', ':file', function () {  //選取類型為file且值發生改變的
        for (var i = 0; i < this.files.length; i++) {
            var file = this.files[0]; //定義file=發生改的file
            name = file.name; //name=檔案名稱
            size = file.size; //size=檔案大小
            type = file.type; //type=檔案型態
            nameArray = name.split(".")
            //if (nameArray[0].toString().match(/[^A-Za-z0-9\u4e00-\u9fa5]/)) {
            if (nameArray.length != 2) {
                alert('檔案格式不符合規範');
                $(this).val('');
            }
            else if (file.type != 'application/pdf' && file.type != 'image/png' && file.type != 'image/jpg' && file.type != 'image/jpeg' && file.type != 'application/msword' && file.type != 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
                //假如檔案格式不等於 png 、jpg、jpeg、pdf、doc、docx
                //alert('type=' + file.type);
                alert('檔案格式不符合規範'); //顯示警告
                $(this).val('');
            }
        }
    });
    function SelChange() {
        var year = Number(document.getElementById("ddl_EmpBirthYear").value);
        var month = Number(document.getElementById("ddl_EmpBirthMonth").value);
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
