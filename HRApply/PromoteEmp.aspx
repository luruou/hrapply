<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromoteEmp.aspx.cs" Inherits="ApplyPromote.PromoteEmp" ValidateRequest="false" MaintainScrollPositionOnPostback="true" %>

<%@ Implements Interface="System.Web.UI.ICallbackEventHandler" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">


<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <link href="css/tabs.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/jquery.validate.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>

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
    <title></title>
</head>

<body>
    <div id="divProgress" style="text-align: center; display: none; position: fixed; top: 50%; left: 50%;">
        <asp:Image ID="imgLoading" runat="server" ImageUrl="~/image/loading.gif" />
        <br />
        <font color="#1B3563" size="2px">資料處理中</font>
    </div>
    <div id="divMaskFrame" style="background-color: #F2F4F7; display: none; left: 0px; position: absolute; top: 0px;">
    </div>
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
                <asp:TextBox ID="TbEmpSn" runat="server" value="" Visible="false"></asp:TextBox>
                <asp:TextBox ID="TbKindNo" runat="server" Visible="false"></asp:TextBox>
                <asp:Button ID="BtnApplyList" runat="server" Text="申請單總表" OnClick="BtnApplyList_Click" AutoPostBack="true" Visible="true" class="btn btn-success" />
                <asp:Label ID="AppWayName" runat="server" value=""></asp:Label>&nbsp─&nbsp
                <asp:Label ID="AppAttributeName" runat="server"></asp:Label><br />
            </h4>
            <div style="font-size: 16px; background-color: #ffed97; border-radius: 10px; height: 40px;">
                <div style="padding: 10px">
                    <p style="color: #0066cc">※所有上傳檔案後記得要『暫存』，「送出申請」請至第一個頁面執行，一但送出後就不能修改。</p>
                </div>
            </div>
            <br />
            <p style="color: red">*為必填</p>
        </div>
        <div class="container">
            <asp:Panel ID="PanelStatus" runat="server" BackImageUrl="~/image/loading.gif">
            </asp:Panel>
            <div class="row">
                <div style="width: 100%">
                    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">
                        <Items>
                            <asp:MenuItem Text="基本資料" Value="0" Selected="true" />
                            <asp:MenuItem Text="學歷資料" Value="1" />
                            <asp:MenuItem Text="所有經歷資料" Value="2" />
                            <asp:MenuItem Text="授課進度表" Value="3" />
                            <asp:MenuItem Text="升等積分計分表&上傳論文" Value="4" />
                            <asp:MenuItem Text="學位論文相關" Value="5" />
                        </Items>
                    </asp:Menu>
                    <asp:Menu ID="Menu2" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">
                        <Items>
                            <asp:MenuItem Text="基本資料" Value="0" Selected="true" />
                            <asp:MenuItem Text="學歷資料" Value="1" />
                            <asp:MenuItem Text="所有經歷資料" Value="2" />
                            <asp:MenuItem Text="授課進度表" Value="3" />
                            <asp:MenuItem Text="升等積分計分表&上傳論文" Value="4" />
                        </Items>
                    </asp:Menu>
                    <asp:Menu ID="Menu3" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                        StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                        OnMenuItemClick="Menu1_MenuItemClick" Visible="false">
                        <Items>
                            <asp:MenuItem Text="基本資料" Value="0" Selected="true" />
                            <asp:MenuItem Text="學歷資料" Value="1" />
                            <asp:MenuItem Text="所有經歷資料" Value="2" />
                            <asp:MenuItem Text="授課進度表" Value="3" />
                            <asp:MenuItem Text="升等積分計分表&上傳論文" Value="4" />
                            <asp:MenuItem Text="迴避名單" Value="5" />
                        </Items>
                    </asp:Menu>
                    <div class="tabContents">
                        <table id="TableManager" runat="server" visible="false" class="table table-bordered table-condensed">
                            <tr>
                                <td>
                                    <asp:Label ID="AuditNameCN" runat="server" />─<asp:Label ID="AuditUnit" runat="server" />─<asp:Label ID="AuditJobTitle" runat="server" />─<asp:Label ID="AuditJobType" runat="server" />
                                    <br />
                                    <asp:Label ID="AuditWayName" runat="server" />─<asp:Label ID="AuditKindName" runat="server" />─<asp:Label ID="AuditAttributeName" runat="server" Text="Label" />─<font color="red"><asp:Label ID="AuditStatusName" runat="server" Text="" /></font>
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <asp:MultiView ID="MVall" runat="server" ActiveViewIndex="0"
                            OnActiveViewChanged="MultiView1_ActiveViewChanged">
                            <asp:View ID="ViewTeachBase" runat="server">
                                <table id="TableBaseData" runat="server" class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="text-align: right">學年度 / 學期：                                        
                                        </td>
                                        <td>
                                            <asp:TextBox ID="VYear" runat="server" Visible="false"></asp:TextBox>
                                            <asp:Label runat="server" ID="VYearAndSemester" Text="目前由人力資源處承辦人維護"></asp:Label>
                                            <asp:TextBox ID="VSemester" runat="server" Visible="false"></asp:TextBox>
                                            <asp:Button ID="BtnSaveYear" runat="server" Visible="false" ValidationGroup="1" OnClick="BtnSaveYear_Click" Text="學年學期修改" AutoPostBack="Ture" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="21%" style="text-align: right">員工編號：</td>
                                        <td>
                                            <asp:Label ID="EmpId" runat="server" title="員工編號" value="" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">身份証號/居留證號：</td>
                                        <td>
                                            <asp:Label ID="EmpIdno" runat="server" title="身份証號" value="" Enabled="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">生日：</td>
                                        <td>
                                            <asp:Label ID="EmpBirthDay" runat="server" title="生日(民國)" value="" MaxLength="7"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">護照號碼：</td>
                                        <td>
                                            <asp:Label ID="EmpPassportNo" runat="server" title="護照號碼" value="" MaxLength="9"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 英文姓：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNameENLast" runat="server" title="英文姓*" value="" MaxLength="50" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 英文名：</td>
                                        <td>
                                            <asp:TextBox ID="EmpNameENFirst" runat="server" title="英文名*" value="" MaxLength="50" class="form-control"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="text-align: right">姓名：</td>
                                        <td>
                                            <asp:Label ID="EmpNameCN" runat="server" title="姓名" value="" MaxLength="50"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="text-align: right">性別：</td>
                                        <td>
                                            <asp:Label ID="EmpSex" runat="server" title="性別" value="" MaxLength="20"></asp:Label>
                                            <input id="EmpSexNo" type="hidden" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">E-Mail：</td>
                                        <td>
                                            <asp:TextBox ID="EmpEmail" runat="server" title="E-Mail" Enabled="false" value="" MaxLength="30" class="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">國籍：</td>
                                        <td>
                                            <asp:Label ID="EmpCountry" runat="server" title="國籍" value="" MaxLength="20"></asp:Label>
                                            <input id="EmpCountryNo" type="hidden" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 現任單位：</td>
                                        <td>
                                            <asp:DropDownList ID="DDEmpUnit" runat="server" OnSelectedIndexChanged="DDEmpUnit_OnClick" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <!--<asp:Label ID="EmpUnit" runat="server" title="現任單位" value="" MaxLength="20"></asp:Label>
                                        <asp:Label ID="EmpUnitNo" runat="server" vible=false ></asp:Label>-->

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 現任職別：</td>
                                        <td>
                                            <!--<asp:Label ID="EmpNowEJobTitle" runat="server"  value="" MaxLength="20"></asp:Label>                             
                                        <asp:Label  id="EmpNowEJobTitleNo" runat="server"  Visible ="false" />-->
                                            <asp:DropDownList ID="DDEmpNowEJobTitle" runat="server" OnSelectedIndexChanged="DDEmpTitle_OnClick" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">現職等年資：</td>
                                        <td>
                                            <asp:Label ID="EmpYear" runat="server" value="" MaxLength="20"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">教師證字號：</td>
                                        <td>
                                            <asp:Label ID="EmpTeachNo" runat="server"
                                                value="" MaxLength="20"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 升等職別：</td>
                                        <td>
                                            <asp:DropDownList ID="AppJobTypeNo" runat="server" class="form-control"
                                                DataSourceID="DSJobAttribute" DataTextField="JobAttrName"
                                                DataValueField="JobAttrNo">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSJobAttribute" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT [JobAttrNo], [JobAttrName] FROM [CJobType]"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 升等職稱：</td>
                                        <td>
                                            <asp:DropDownList ID="AppJobTitleNo" runat="server" AutoPostBack="True" class="form-control"
                                                DataSourceID="DSJobTitle" DataTextField="JobTitleName"
                                                DataValueField="JobTitleNo"
                                                OnSelectedIndexChanged="AppJobTitleNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="DSJobTitle" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT [JobTitleNo], [JobTitleName] FROM [CJobTitle] Where JobTitleNo != '1'"></asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 升等類型：
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
                                        <td style="text-align: right"><font color="red">*</font> 法規依據：</td>
                                        <td>依教師聘任升等實施辦法第(<asp:Label ID="lbchose" Text="二" runat="server" />)條第<asp:Label ID="ItemNo" runat="server"></asp:Label>項 第<asp:Label ID="ItemLabel" runat="server"></asp:Label>
                                            款 請下方選擇：<br />
                                            <asp:DropDownList ID="ELawNum" runat="server"   class="form-control"  ></asp:DropDownList>
                                            <%--<asp:RadioButtonList ID="ELawNum" runat="server"  RepeatLayout="Flow" ></asp:RadioButtonList>--%>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="text-align: right"><font color="red">*</font> 教師資格審查資料切結書：</td>
                                        <td>
                                            <span title="教師資格審查資料切結書上傳">
                                                <asp:CheckBox ID="AppDeclarationUploadCB" runat="server" title="取消勾可刪除" />
                                                <asp:TextBox ID="AppDeclarationUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text="" MaxLength="100"></asp:TextBox>
                                                <asp:HyperLink ID="AppDeclarationHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="AppDeclarationUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label3" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="2">其他
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Table ID="Table1" Width="100%" runat="server" BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="0" CellSpacing="0" class="table table-bordered table-condensed">
                                                <asp:TableRow ID="AppExpTableRow" runat="Server">
                                                    <asp:TableCell Width="20%" Style="text-align: right"> <font color="red">*</font> 經歷服務證明：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="經歷服務證明上傳">
                                                            <asp:CheckBox ID="ExpServiceCaUploadCB" runat="server" title="取消勾可刪除" />

                                                            <asp:TextBox ID="ExpServiceCaUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox><asp:HyperLink ID="ExpServiceCaHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="ExpServiceCaUploadFU" runat="server" />
                                                        </span>
                                                        <br />
                                                        <asp:Label ID="Label4" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="AppPPMTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell Style="text-align: right"><font color="red">*</font> 研究計劃主持：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="研究計劃主持上傳">
                                                            <asp:CheckBox ID="AppPPMUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="AppPPMUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox><asp:HyperLink ID="AppPPMHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="AppPPMUploadFU" runat="server" />
                                                        </span>
                                                        <br />
                                                        <asp:Label ID="Label2" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="AppTeacherCaTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell Style="text-align: right"><font color="red">*</font> 現任職等部定證書影本：<br /> 	
                                             	
                                                    </asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="現任職等部定證書影本上傳*">
                                                            <asp:CheckBox ID="AppTeacherCaUploadCB" runat="server" title="取消勾可刪除" />

                                                            <asp:TextBox ID="AppTeacherCaUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="AppTeacherCaHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="AppTeacherCaUploadFU" runat="server" />
                                                        </span>
                                                        <br />
                                                        <asp:Label ID="Label5" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="OtherTeachingTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell Style="text-align: right"><font color="red">*</font> 教學成果：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="教學成果上傳">
                                                            <asp:CheckBox ID="AppOtherTeachingUploadCB" runat="server" title="取消勾可刪除" />

                                                            <asp:TextBox ID="AppOtherTeachingUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="AppOtherTeachingHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="AppOtherTeachingUploadFU" runat="server" />
                                                        </span>
                                                        <br />
                                                        (教學評議分數、教師成長活動...等，依各學院審查標準提供)
                                                        <%--<br />
                                                        <asp:Label ID="Label6" runat="server"><font color="red">(上傳檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>--%>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow ID="OtherServiceTableRow" runat="Server" Visible="false">
                                                    <asp:TableCell Style="text-align: right"><font color="red">*</font> 服務成果：</asp:TableCell>
                                                    <asp:TableCell>
                                                        <span title="服務成果上傳">
                                                            <asp:CheckBox ID="AppOtherServiceUploadCB" runat="server" title="取消勾可刪除" />

                                                            <asp:TextBox ID="AppOtherServiceUploadFUName" runat="server" ReadOnly="true"
                                                                Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="AppOtherServiceHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="AppOtherServiceUploadFU" runat="server" /><br />
                                                            (校內外服務佐證資料...等，依各學院審查標準提供) 
                                                            <%--<br />
                                                            <asp:Label ID="Label7" runat="server"><font color="red">(上傳檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>--%>
                                                        </span>
                                                    </asp:TableCell>
                                                </asp:TableRow>

                                                <asp:TableRow ID="AppClinicalRow" runat="Server" Visible="false">
                                                    <asp:TableCell ColumnSpan="2">
                                                        <asp:Table runat="Server" Width="100%">

                                                            <asp:TableRow ID="TableRow1" runat="Server">
                                                                <asp:TableCell ID="TableCell1" runat="Server" Style="text-align: right" Width="20%"><font color="red">*</font> 身份證上傳：</asp:TableCell><asp:TableCell>
                                                                    <span title="身份證上傳">
                                                                        <asp:CheckBox ID="EmpIdnoUploadCB" runat="server" title="取消勾可刪除" />
                                                                        <asp:TextBox ID="EmpIdnoUploadFUName" runat="server" ReadOnly="true"
                                                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="EmpIdnoHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="EmpIdnoUploadFU" runat="server" />
                                                                    </span>
                                                                    <br />
                                                                    <asp:Label ID="Label8" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="TableRow2" runat="Server">
                                                                <asp:TableCell ID="TableCell2" runat="Server" Style="text-align: right"><font color="red">*</font> 最高學歷證件上傳：</asp:TableCell><asp:TableCell>
                                                                    <span title="畢業證書,學位證書或文憑影本(國外學歷須經我國駐外館處驗證)">
                                                                        <asp:CheckBox ID="EmpDegreeUploadCB" runat="server" title="取消勾可刪除" />
                                                                        <asp:TextBox ID="EmpDegreeUploadFUName" runat="server" ReadOnly="true"
                                                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="EmpDegreeHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="EmpDegreeUploadFU" runat="server" />
                                                                    </span>
                                                                    <br />
                                                                    <asp:Label ID="Label9" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="TableRow3" runat="Server">
                                                                <asp:TableCell Style="text-align: right"><font color="red">*</font> 醫師證書：<br /> 	
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <span title="只有醫師需上傳證書">
                                                                        <asp:CheckBox ID="AppDrCaUploadCB" runat="server" title="取消勾可刪除" />
                                                                        <asp:TextBox ID="AppDrCaUploadFUName" runat="server" ReadOnly="true"
                                                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="AppDrCaHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="AppDrCaUploadFU" runat="server" />
                                                                    </span>
                                                                    <br />
                                                                    <asp:Label ID="Label1" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="TableRow4" runat="Server">
                                                                <asp:TableCell Style="text-align: right"><font color="red">*</font> 著作出版刊物：<br /> 	
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <span title="著作出版等刊物或個人事蹟等相關資料">
                                                                        <asp:CheckBox ID="AppPublicationUploadCB" runat="server" title="取消勾可刪除" />
                                                                        <asp:TextBox ID="AppPublicationUploadFUName" runat="server" ReadOnly="true"
                                                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="AppPublicationHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="AppPublicationUploadFU" runat="server" />
                                                                    </span>
                                                                    <br />
                                                                    <asp:Label ID="Label10" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                            <asp:TableRow ID="RecommendUploadTableRow" runat="Server">
                                                                <asp:TableCell Style="text-align: right"><font color="red">*</font> 推薦書二份合併上傳：<br /> 	
                                                                </asp:TableCell>
                                                                <asp:TableCell>
                                                                    <span title="推薦書上傳">
                                                                        <asp:CheckBox ID="RecommendUploadCB" runat="server" title="取消勾可刪除" />
                                                                        <asp:TextBox ID="RecommendUploadFUName" runat="server" ReadOnly="true"
                                                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="RecommendHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="RecommendUploadFU" runat="server" />
                                                                    </span>
                                                                    <br />
                                                                    <asp:Label ID="Label11" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
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
                                        <td colspan="2">
                                            <asp:Button ID="EmpBaseModifySave" runat="server" OnClick="EmpBaseModifySave_Click" Text="修改" class="btn btn-warning" Style="color: white" AutoPostBack="Ture" Visible="false" />
                                            <asp:Button ID="EmpBaseSave" runat="server" OnClick="EmpBaseSave_Click" Text="送出申請" AutoPostBack="Ture" class="btn btn-primary" />
                                            <!--<span title = '因業務作業關係，2月1日暫不提供送件功能'><asp:Button ID="EmpBaseSavee" runat="server" onclick="EmpBaseSave_Click" Text="送出申請" AutoPostBack="Ture" Enabled="false"/></span>-->
                                            &nbsp;<asp:Button ID="EmpBaseTempSave" runat="server" OnClientClick="turnoffvalidate()" OnClick="EmpBaseTempSave_Click" Text="暫存" AutoPostBack="Ture" class="btn" ForeColor="White" Style="background-color: #d26900" />
                                            &nbsp;<asp:Button ID="BtnPromotePrint" runat="server" Text="升等申請表預覽" OnClick="BtnPromotePrint_Click" AutoPostBack="Ture" class="btn btn-success" />
                                            <%--&nbsp;<font color='blue'>『送出申請』、『印申請表』前注意是否有「<font color='red'>*</font>錯誤提示」，確認資料上傳！</font>--%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewTeachEdu" runat="server">
                                <div>
                                    <div style="float: right">
                                        <%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalTeachEdu">新增學歷</button>--%>
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
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    X
                                                </button>
                                                <br />
                                                <br />
                                                <table border="1" width="100%" cellspacing="0" cellpadding="0" class="table table-bordered table-condensed">
                                                    <tr>
                                                        <td style="text-align: right" width="15%">修課地點：</td>
                                                        <td>
                                                            <asp:DropDownList ID="TeachEduLocal" runat="server" DataSourceID="DSCountry" class="form-control" DataTextField="code_ddesc" DataValueField="code_no" OnSelectedIndexChanged="TeachEduLocal_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            &nbsp;<font color="red" size="2">(請由大學學歷填至最高學歷)</font>
                                                            <asp:TextBox ID="TBIntEduSn" Visible="false" runat="server"></asp:TextBox><br />
                                                            <asp:SqlDataSource ID="DSCountry" runat="server" ConnectionString="<%$ ConnectionStrings:HrConnectionString %>"
                                                                SelectCommand="SELECT [code_ddesc], [code_no] FROM [s10_code_d] WHERE ([code_kind] = @code_kind) ORDER BY [code_ddesc]">
                                                                <SelectParameters>
                                                                    <asp:Parameter DefaultValue="NAT" Name="code_kind" Type="String" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
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
                                            <asp:DropDownList runat="server" ID="TeachEduStartYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="TeachEduStartMonth">
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
                                                            <asp:DropDownList runat="server" ID="TeachEduEndYear" />&nbsp;年&nbsp;
                                            <asp:DropDownList runat="server" ID="TeachEduEndMonth">
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
                                            <%--<font color="red" size="2">如：民國79年2月 ~ 民國89年10月，請輸入07902 ~ 08910</font>--%> </td>
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
                                                            <asp:Button ID="TeachEduUpdate" runat="server" Text="儲存" class="btn btn-primary" Style="color: white" OnClick="TeachEduUpdate_Click" Visible="false" />
                                                            <asp:Button ID="TeachEduClear" runat="server" Text="清除" OnClick="TeachEduClear_Click" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <%--                                    <tr style="display:none">
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
                                                CellPadding="2" DataKeyNames="EduSn" DataSourceID="DSTeacherEdu"
                                                ForeColor="Black" GridLines="Horizontal" BackColor="White"
                                                OnRowDataBound="GVTeachEdu_RowDataBound" BorderColor="#CCCCCC" BorderWidth="1px"
                                                BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">

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
                                                            <asp:LinkButton ID="lnkTeacherEduMod" runat="server" Text="修改" OnClick="TeachEduModData_Click" class="btn btn-warning" Style="color: white" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" ItemStyle-ForeColor="White" HeaderStyle-Width="8%" />
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
                                            <asp:Label runat="server" ID="lb_NoTeacherEdu" Visible="false" Text="尚無填寫資料。" />
                                        </td>
                                    </tr>
                                </table>


                            </asp:View>
                            <asp:View ID="ViewTeachExp" runat="server">
                                <div>
                                    <asp:CheckBox ID="CBNoTeachExp" runat="server" AutoPostBack="true" OnCheckedChanged="CBNoTeachExp_OnClick" />
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
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    X
                                                </button>
                                                <br />
                                                <br />
                                                <asp:Table ID="TbTeachExp" runat="server" class="table table-bordered table-condensed">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="17%" CssClass="text-right">機關/單位：</asp:TableCell><asp:TableCell>
                                                            <asp:TextBox ID="TeachExpOrginization" runat="server" MaxLength="80" Width="45%" class="form-control"></asp:TextBox>
                                                            &nbsp;/&nbsp;<asp:TextBox ID="TeachExpUnit" runat="server" MaxLength="80" Width="50%" class="form-control"></asp:TextBox>
                                                            <asp:TextBox ID="TBIntExpSn" Visible="false" runat="server"></asp:TextBox>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell CssClass="text-right">職稱：</asp:TableCell><asp:TableCell>
                                                            <asp:TextBox ID="TeachExpJobTitle" runat="server" MaxLength="50" class="form-control"></asp:TextBox>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell CssClass="text-right">職別：</asp:TableCell><asp:TableCell>
                                                            <asp:DropDownList ID="TeachExpJobType" runat="server" class="form-control"
                                                                DataSourceID="DSTeachExpJobAttribute" DataTextField="TeachExpJobAttrName"
                                                                DataValueField="TeachExpJobAttrNo">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="DSTeachExpJobAttribute" runat="server"
                                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                                SelectCommand="SELECT [JobAttrNo] AS TeachExpJobAttrNo, [JobAttrName] AS TeachExpJobAttrName  FROM [CJobType]"></asp:SqlDataSource>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell CssClass="text-right">起訖日期：</asp:TableCell><asp:TableCell>
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
                                                        ~ 民國 
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
                                                        <asp:TableCell CssClass="text-right">
                                        服務證明上傳：</asp:TableCell>
                                                        <asp:TableCell>
                                                            <span title="服務證明上傳" style="text-align: right">
                                                                <asp:CheckBox ID="TeachExpUploadCB" runat="server" title="取消勾可刪除" />
                                                                <asp:TextBox ID="TeachExpUploadFUName" runat="server" ReadOnly="true"
                                                                    Style="display: none;" Text=""></asp:TextBox>
                                                                <asp:HyperLink ID="TeachExpHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                                <asp:FileUpload ID="TeachExpUploadFU" runat="server" />
                                                            </span>
                                                            <br />
                                                            <asp:Label ID="Label12" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow>
                                                        <asp:TableCell colspan="2" Style="text-align: center">
                                                            <asp:Button ID="TeachExpSave" runat="server" Text="新增" OnClick="TeachExpSave_Click" class="btn btn-primary" />
                                                            <asp:Button ID="TeachExpUpdate" Visible="false" runat="server" Text="儲存" class="btn btn-primary" Style="color: white" OnClick="TeachExpUpdate_Click" />
                                                            <asp:Button ID="TeachExpCancel" runat="server" Text="取消" OnClick="TeachExpCancel_Click" Visible="false" />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <%--                                                <asp:TableRow>
                                                    <asp:TableCell colspan="2" Style="text-align: center">
                                                        <asp:Label ID="MessageLabelExp" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                    </asp:TableCell>
                                                </asp:TableRow>--%>
                                                </asp:Table>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <table style="width: 100%" class="table table-bordered table-condensed">
                                    <%--                                    <tr>
                                        <td>
                                            <%--<asp:Button runat="server" ID="CBNoTeachExpSave" Text="儲存此勾選" Visible="false" OnClick="CBNoTeachExp_OnClick" class="btn btn-primary" />
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="text-align: center"></td>
                                    </tr>
                                    <caption>
                                        學年度 
                                    </caption>
                            <tr>
                                <td style="text-align: center">
                                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>--%>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered table-condensed" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp" EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GVTeachExp_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ExpOrginization" HeaderText="機關" SortExpression="ExpOrginization" />
                                                    <asp:BoundField DataField="ExpUnit" HeaderText="單位" SortExpression="ExpUnit" />
                                                    <asp:BoundField DataField="ExpJobTitle" HeaderText="職稱" SortExpression="ExpJobTitle" />
                                                    <asp:BoundField DataField="ExpJobTypeName" HeaderText="職別" SortExpression="ExpJobTypeName" />
                                                    <asp:BoundField DataField="ExpStartEndYM" HeaderText="起訖日期" HeaderStyle-Width="13%" SortExpression="ExpStartEndYM" />
                                                    <%--<asp:BoundField DataField="ExpUploadName" HeaderText="上傳檔案" SortExpression="ExpUploadName" />--%>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="5%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lb_ExpUploadName" runat="server" Text='<%#Bind("ExpUploadName")%>' />
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
                                                            <asp:LinkButton ID="lnkTeacherExpMod" runat="server" class="btn btn-warning" Style="color: white" OnClick="TeachExpModData_Click" Text="修改" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" HeaderStyle-Width="5%" ControlStyle-CssClass="btn btn-danger" ItemStyle-ForeColor="White" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSTeacherExp" runat="server" ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>" DeleteCommand="DELETE FROM TeacherExp WHERE ExpSn = @ExpSn" SelectCommand="SELECT A.[ExpSn], A.[EmpSn], A.[ExpOrginization], A.[ExpUnit], A.[ExpJobTitle], A.[ExpStartYM]+'~'+A.[ExpEndYM] AS [ExpStartEndYM], A.[ExpUploadName] , B.JobAttrName AS ExpJobTypeName FROM [TeacherExp] AS A LEFT OUTER JOIN 
                                     CJobType AS B ON A.ExpJobType = B.JobAttrNo  Where EmpSn = @EmpSn ORDER BY A.[ExpStartYM]">
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
                                <br />
                                <div>
                                    ※自動匯入<u>校內經歷</u>應與本校<a href='http://depsys.tmu.edu.tw/tchinfo_public/' target="_blank">人才庫</a>內容一致，若有疑義請洽人資處。
                                                <div style="float: right;">
                                                    <asp:Button ID="TmuExpImport" runat="server" Text="匯入本校經歷" OnClientClick=" return confirm('確認重新匯入！將刪除『此處』已有資料。')" OnClick="TmuExpImport_Click" class="btn btn-success" />
                                                </div>
                                </div>

                                <hr />
                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVTeacherTmuExp" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" DataKeyNames="ExpSn" DataSourceID="DSTeacherTmuExp"
                                                ForeColor="Black" GridLines="Horizontal" BackColor="White"
                                                BorderColor="#CCCCCC" BorderWidth="1px" OnRowDataBound="GVTeacherTmuExp_RowDataBound"
                                                OnRowCommand="GVTeacherTmuExp_RowCommand"
                                                BorderStyle="Solid" EnableModelValidation="True" CssClass="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ExpNo">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100px">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="EmpSn" runat="server" Text='<%#Bind("EmpSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ExpPosName" HeaderText="經歷" HeaderStyle-Width="15%"
                                                        SortExpression="ExpPosName" />
                                                    <asp:BoundField DataField="ExpUnitName" HeaderText="單位" HeaderStyle-Width="15%" SortExpression="ExpUnitName" />
                                                    <asp:BoundField DataField="ExpTitleName" HeaderText="職稱" HeaderStyle-Width="10%" SortExpression="ExpTitleName" />
                                                    <asp:BoundField DataField="ExpStartEndDate" HeaderText="起訖日期" HeaderStyle-Width="10%" SortExpression="ExpStartEndDate" />
                                                    <asp:BoundField DataField="ExpQId" HeaderText="教師證書字號" HeaderStyle-Width="10%" SortExpression="ExpQId" />
                                                    <asp:BoundField DataField="ExpUploadName" HeaderText="上傳檔案" HeaderStyle-Width="10%" SortExpression="ExpUploadName" />
                                                    <asp:TemplateField HeaderText="上傳/下載">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="TeachTmuExpUploadFU" runat="server" Width="120px" /><br />
                                                            <asp:Button ID="TeachTmuExpUpload" class="btn btn-primary" runat="server" CommandName="Upload" Text="上傳" />
                                                            <asp:LinkButton ID="TeachTmuExpDownload" runat="server" class="btn btn-success" CommandName="Download" Text="下載" Style="color: white" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" ItemStyle-ForeColor="White" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSTeacherTmuExp" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                DeleteCommand="DELETE FROM TeacherTmuExp WHERE ExpSn = @ExpSn"
                                                SelectCommand="
                                    SELECT [ExpSn],[EmpSn],b.unt_name_full AS ExpUnitName,c.tit_name AS ExpTitleName, d.pos_name AS ExpPosName,ExpQId,[ExpStartDate]+'~'+[ExpEndDate] AS ExpStartEndDate,[ExpUploadName] 
                                    FROM [TeacherTmuExp] AS a LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit] AS b ON a.ExpUnitId = b.unt_id  COLLATE Chinese_Taiwan_Stroke_CI_AS
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_titlecode] AS c ON a.ExpTitleId = c.tit_id  COLLATE Chinese_Taiwan_Stroke_CI_AS
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_position] AS d ON a.ExpPosId = d.pos_id  COLLATE Chinese_Taiwan_Stroke_CI_AS WHERE EmpSn = @EmpSn  ORDER BY A.[ExpStartDate]">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="ExpSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <asp:Label runat="server" ID="lb_NoTeacherTmuExp" Visible="false" Text="尚無填寫資料。" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>

                            <asp:View ID="ViewTeachLesson" runat="server">
                                <div>
                                    <div style="float: right">
                                        <asp:Button ID="TmuLessonImport" runat="server" Text="匯入本校授課進度表" OnClientClick=" return confirm('確認重新匯入！將刪除『此處』已有資料。')" OnClick="TmuLessonImport_Click" class="btn btn-success" />
                                        <asp:Button runat="server" ID="teachLessonAdd" OnClick="teachLessonAdd_Click" class="btn btn-primary" Text="新增授課進度表"></asp:Button>
                                    </div>
                                </div>
                                <br />
                                <hr />
                                ※請執行上方匯入功能，以更新匯入<a href='https://newacademic.tmu.edu.tw/' target="_blank">教師授課進度表</a>105年至今日資料。
                                <div class="modal fade" id="modalTeachLesson">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    X
                                                </button>
                                                <br />
                                                <br />
                                                <table width="100%" class="table table-bordered table-condensed">
                                                    <tr>
                                                        <td width="15%" style="text-align: right">學年度：</td>
                                                        <td>
                                                            <%--<asp:TextBox ID="TeachLessonYear" runat="server" MaxLength="5" Width="60px" class="form-control"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_TeachLessonYear" runat="server" Width="60px" />-
                                            <%--<asp:TextBox ID="TeachLessonSemester" runat="server" MaxLength="1" Width="40px" class="form-control"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddl_TeachLessonSemester" runat="server" Width="40px">
                                                                <asp:ListItem Text="1" Value="1" />
                                                                <asp:ListItem Text="2" Value="2" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" style="text-align: right">系所級別：</td>
                                                        <td>
                                                            <asp:TextBox ID="TeachLessonDepLevel" runat="server" MaxLength="100" class="form-control"></asp:TextBox>
                                                            <asp:TextBox ID="TbLessonSn" Visible="false" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" style="text-align: right">課目：</td>
                                                        <td>
                                                            <asp:TextBox ID="TeachLessonClass" runat="server" MaxLength="100" class="form-control"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" style="text-align: right">授課時數：</td>
                                                        <td>
                                                            <asp:TextBox ID="TeachLessonHours" runat="server" MaxLength="10" class="form-control" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox><br />
                                                            <font color="red">(限填數字)</font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" style="text-align: right">學分數：</td>
                                                        <td>
                                                            <asp:TextBox ID="TeachLessonCreditHours" runat="server" MaxLength="10" class="form-control" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox><br />
                                                            <font color="red">(限填數字)</font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center">
                                                            <asp:Button ID="TeachLessonSave" runat="server" Text="新增" OnClick="TeachLessonSave_Click"
                                                                class="btn btn-primary" />
                                                            <asp:Button ID="TeachLessonUpdate" runat="server" Text="更新" class="btn btn-primary" Style="color: white" OnClick="TeachLessonUpdate_Click" Visible="false" />
                                                            <asp:Button ID="TeachLessonCancel" runat="server" Text="取消" Visible="false" />
                                                        </td>
                                                    </tr>
                                                    <tr style="display:none">
                                                        <td colspan="2" style="text-align: center">
                                                            <asp:Label ID="MessageLabelLesson" runat="server" Text="" ForeColor="Red"></asp:Label>
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
                                            <asp:GridView ID="GVTeachLesson" runat="server" AutoGenerateColumns="False"
                                                CellPadding="2" DataKeyNames="LessonSn" DataSourceID="DSTeacherLesson"
                                                GridLines="Horizontal" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White"
                                                BorderColor="#CCCCCC" BorderWidth="1px"
                                                BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="LessonSn" runat="server" Text='<%#Bind("LessonSn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SMTR" HeaderText="學年度" HeaderStyle-Width="8%"
                                                        SortExpression="SMTR" />
                                                    <asp:BoundField DataField="LessonDeptLevel" HeaderText="系所級別"
                                                        SortExpression="LessonDeptLevel" />
                                                    <asp:BoundField DataField="LessonClass" HeaderText="課目" SortExpression="LessonClass" />
                                                    <asp:BoundField DataField="LessonHours" HeaderText="授課時數" HeaderStyle-Width="10%"
                                                        SortExpression="LessonHours" />
                                                    <asp:BoundField DataField="LessonCreditHours" HeaderText="學分數" HeaderStyle-Width="8%"
                                                        SortExpression="LessonCreditHours" />
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTeacherLessonMod" runat="server" Text="修改" class="btn btn-warning" Style="color: white" OnClick="TeachLessonModData_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" HeaderStyle-Width="5%" ItemStyle-ForeColor="White" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSTeacherLesson" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:TmuConnectionString%>"
                                                DeleteCommand="DELETE FROM TeacherTmuLesson WHERE LessonSn = @LessonSn"
                                                SelectCommand="
                                    SELECT [LessonSn],[EmpSn],[LessonYear]+[LessonSemester] AS SMTR,[LessonDeptLevel],[LessonClass],[LessonHours],[LessonCreditHours],[LessonEvaluate] FROM [TeacherTmuLesson] WHERE EmpSn = @EmpSn Order by LessonYear+LessonSemester Desc ">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                    <asp:SessionParameter DefaultValue="0" Name="LessonYear" SessionField="sYear" />
                                                    <asp:SessionParameter DefaultValue="0" Name="LessonSemester" SessionField="sSemester" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="LessonSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <asp:Label ID="lb_NoTeachLesson" runat="server" Text="尚無填寫資料。" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="ViewThesisScore" runat="server">
								   <!-- Modal -->
                                <div class="modal fade" id="modalThesisCoop" tabindex="-1" role="dialog" aria-labelledby="modalThesisCoop" aria-hidden="true">
                                    <div class="modal-dialog modal-lg" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title">産學合作</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span></button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:HiddenField ID="hfID" runat="server"/>
                                                <asp:HiddenField ID="hfUploadFileName" runat="server"/>
                                                <asp:Table ID="TBThesisCoopInsert" runat="server" class="table table-bordered table-condensed" Width="100%">

                                                    <asp:TableRow ID="TableRow14" runat="server">
                                                        <asp:TableCell ID="TableCell5" runat="server" Style="text-align: right">
                                                            計畫名稱、計畫執行期間、產學合作實收金額：
                                                        </asp:TableCell><asp:TableCell ID="TableCell6" runat="server" class="style4">
                                                            <asp:TextBox ID="txtProjectContent" runat="server" Height="100px" CssClass="form-control"
                                                                Text="" TextMode="Multiline" Width="100%" HtmlEncode="false"></asp:TextBox>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow15" runat="server">
                                                        <asp:TableCell ID="TableCell20" runat="server" Style="text-align: right">
                                                            實收金額分類(T)：
                                                        </asp:TableCell><asp:TableCell ID="TableCell21" runat="server">
                                                            <%--<asp:TextBox ID="AppThesisPublishYearMonth" runat="server" MaxLength="5"></asp:TextBox>--%>

                                                            <asp:DropDownList runat="server" ID="ddlClassification" CssClass="form-control">
                                                          <%--      <asp:ListItem Text="產學合作計畫實收台幣超過50萬~100萬元(含100萬元)：10分" Value="10" />
                                                                <asp:ListItem Text="產學合作計畫實收台幣超過100萬~300萬元(含300萬元)：30分" Value="30" />
                                                                <asp:ListItem Text="產學合作計畫實收台幣超過300萬~500萬元(含500萬元)：50分" Value="50" />
                                                                <asp:ListItem Text="產學合作計畫實收台幣超過500萬~700萬元(含700萬元)：70分" Value="70" />
                                                                <asp:ListItem Text="產學合作計畫實收台幣超過700萬~1,000萬元(含1,000萬元)：100分" Value="100" />
                                                                <asp:ListItem Text="產學合作計畫實收台幣超過1,000萬：130分" Value="130" />--%>
                                                            </asp:DropDownList>

                                                            <%--<span style="color: rgb(255, 0, 0); font-family: 'Times New Roman'; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">如：民國93年08月，請輸入09308</span>--%>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow16" runat="server">
                                                        <asp:TableCell ID="TableCell22" runat="server" Style="text-align: right">校方佔有研發成果智慧財產權比例分類(I)：
                                                        </asp:TableCell><asp:TableCell ID="TableCell23" runat="server">
                                                            <asp:DropDownList runat="server" ID="ddlRD" CssClass="form-control">
                                                             <%--   <asp:ListItem Text="30%未滿" Value="1" />
                                                                <asp:ListItem Text="30%以上" Value="2" />--%>
                                                            </asp:DropDownList>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow17" runat="server">
                                                        <asp:TableCell ID="TableCell24" runat="server" Style="text-align: right">
                                        貢獻比例(C)：
                                                        </asp:TableCell><asp:TableCell ID="TableCell25" runat="server">

                                                            <div class="input-group mb-2">

                                                                <asp:TextBox ID="txtContribute" runat="server" Text="0" class="form-control" />
                                                                <div class="input-group-prepend">
                                                                    <div class="input-group-text">%</div>
                                                                </div>
                                                            </div>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                   <%-- <asp:TableRow ID="TableRow18" runat="server">
                                                        <asp:TableCell ID="TableCell26" runat="server" Style="text-align: right">
                                                            總分數(T) x (I) x (C)：
                                                        </asp:TableCell><asp:TableCell ID="TableCell27" runat="server" class="style4">
                                                           <span></span>
                                                        </asp:TableCell>
                                                    </asp:TableRow>--%>
                                                    <asp:TableRow ID="TableRow19" runat="server">
                                                        <asp:TableCell ID="TableCell28" runat="server" Style="text-align: right">
                                        上傳檔案：
                                                        </asp:TableCell><asp:TableCell ID="TableCell29" runat="server">

                                                            <asp:CheckBox ID="ThesisCoopUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="ThesisCoopUploadFUName" runat="server" ReadOnly="true" Text="" Visible="false"></asp:TextBox>
                                                            <asp:HyperLink ID="ThesisCoopHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="ThesisCoopUploadFU" runat="server" accept=".pdf"/><br />
                                                            <br />
                                                            <asp:Label ID="Label7" runat="server"><font color="red">(檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="ThesisCoopCreate" runat="server" class="btn btn-primary coop-create" OnClick="ThesisCoopCreate_Click" Text="新增" />
                                                <asp:Button ID="ThesisCoopUpdate" runat="server" class="btn btn-primary coop-update" OnClick="ThesisCoopUpdate_Click" Text="更新" />
                                                <button type="button" class="btn btn-danger" data-dismiss="modal" aria-label="Close">取消</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
							
                                <font color="red"><asp:Label runat="server" Text="(合併)個人研究重點與成果&論文積分表：" ID="lb_ReasearchResult" /></font>
                                <asp:HyperLink ID="link_AppReasearchResult" runat="server" Visible="false"></asp:HyperLink>
                                <asp:FileUpload ID="AppReasearchResultUpload" runat="server" />
                                <asp:LinkButton runat="server" Text="儲存" ID="saveReasearchResult" OnClick="saveReasearchResult_Click" class="btn btn-primary" Style="color: white" />
                                <br/><br/>
                                <div align="center" style="background-color: #0072e3; font-size: 14pt; color: white"><b>研究論文統計( 表A )</b></div>
                                <table id="SCITotal" width="100%"
                                    style="font-size: 11pt; width: 100%; line-height: 120%;" class="table table-bordered table-condensed">
                                    
                                    <tr>
                                        <td colspan="5">SCIE、SSCI、EI之期刊論文資料，可就近至各大學圖書館、國家實驗研究院科技政策研究與資訊中心等查閱或上網檢索。上述SCIE、SSCI、EI期刊以最新版本為準。
                                        </td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" width="8%"></td>
                                        <td colspan="3" style="text-align:center">
                                            <p align="center">
                                                <b><u><span style="font-size: 11pt">SCIE、SSCI、EI期刊論文</span></u></b>
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
                                            <span style="font-size: 11pt">SCIE論文</span></td>
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
                                            <asp:TextBox ID="txt_FSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_FSSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_FSEI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                            <asp:TextBox ID="txt_FNSCI" runat="server" MaxLength="5" Visible="False"
                                                placeholder="0" Text="" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_FSOther" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <span style="font-size: 11pt">非第一作者之通訊作者<br/>
                                                論文篇數<br/>
                                            </span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFCSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFCSSCI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_NFCEI" runat="server" MaxLength="5" placeholder="0" Text="" class="form-control" Style="text-align: center"  OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}"></asp:TextBox>
                                            <asp:TextBox ID="txt_NFCNSCI" runat="server" MaxLength="5" Visible="False"
                                                placeholder="0" Text="" Style="text-align: center" ></asp:TextBox>
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
                                            <asp:Button ID="Button1" runat="server" OnClick="ThesisScoreReCount_onClick" Style="float: right" class="btn btn-success" Text="統計數量儲存" />
                                        </td>
                                    </tr>
                                </table>
                                <div align="center" style="background-color: #0072e3; font-size: 14pt; color: white"><b>研究論文積分統計( 表B )</b></div>

                                <div class="modal fade" id="modalThesisScore">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    X
                                                </button>
                                                <br />
                                                <br />
                                                <asp:Table ID="TBThesisScoreInsert" runat="server" class="table table-bordered table-condensed" Width="100%">
                                                    <%--<asp:TableRow ID="TableRow0" runat="server" Style="background-color: yellow;">
                                                        <asp:TableCell ID="TableCell0" runat="server" ColumnSpan="2">
                                       <div align="center" ><asp:Label runat="server">新增一篇論文</asp:Label></div>
                                                        </asp:TableCell>
                                                    </asp:TableRow>--%>
                                                    <asp:TableRow ID="TableR2" runat="server">
                                                        <asp:TableCell ID="TableC3" runat="server" style="text-align:right">
                                                            序號：<asp:TextBox ID="TBIntThesisSn" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:TextBox ID="TBSnNo" runat="server" Visible="false"></asp:TextBox>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableC4" runat="server" class="style4">
                                                            <asp:DropDownList ID="RRNo" runat="server" DataSourceID="DSRR"
                                                                DataTextField="RRNameAbbr" DataValueField="RRNo">
                                                            </asp:DropDownList>
                                                            <asp:SqlDataSource ID="DSRR" runat="server"
                                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                                SelectCommand="SELECT [RRNo], [RRNameAbbr], [RRName] FROM [CResearchResult] where RRNo not in ('5')"></asp:SqlDataSource><br/>(研究成果分類)
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableR3" runat="server">
                                                        <asp:TableCell ID="TableC5" runat="server" style="text-align:right">
                                                            研究成果名稱：
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableC6" runat="server" class="style4">
                                        請注意：作者本人請加「+」，通訊作者請加「*」，貢獻相同作者請加「#」<br />
                                                            <asp:TextBox ID="AppThesisResearchResult" runat="server" Height="100px"
                                                                Text="" TextMode="Multiline" Width="100%" HtmlEncode="false"></asp:TextBox>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow7" runat="server">
                                                        <asp:TableCell ID="TableCell7" runat="server" style="text-align:right">
                                                            論文發表年月：
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell8" runat="server">
                                                            <%--<asp:TextBox ID="AppThesisPublishYearMonth" runat="server" MaxLength="5"></asp:TextBox>--%>
                                            民國
                                            <asp:DropDownList ID="ddl_AppThesisPublishYear" runat="server" />
                                                            &nbsp;年&nbsp;
                                            
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
                                            </asp:DropDownList>&nbsp;月&nbsp;
                                            <%--<span style="color: rgb(255, 0, 0); font-family: 'Times New Roman'; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: left; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none; background-color: rgb(255, 255, 255);">如：民國93年08月，請輸入09308</span>--%>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow8" runat="server">
                                                        <asp:TableCell ID="TableCell9" runat="server" style="text-align:right">論文積分：
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell10" runat="server" class="style4">
                                                            性質分數(C)：
                                            <asp:TextBox ID="AppThesisC" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}" runat="server" value="1" class="form-control" Width="80%"></asp:TextBox><br />
                                                            雜誌分類排名(J)：
                                            <asp:TextBox ID="AppThesisJ" runat="server" value="1" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}" class="form-control" Width="75%"></asp:TextBox><br />
                                                            作者排名(A)：<asp:TextBox ID="AppThesisA" OnKeyPress="if(((event.keyCode>=48)&&(event.keyCode <=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}" class="form-control" runat="server" Width="80%" value="1"></asp:TextBox><br />
                                                            <br /><%--<asp:Button ID="BtnExecuteEqual" runat="server" OnClick="BtnExecuteEqual_Click" Text="乘積計算" class="btn btn-primary" Style="width: 150px" />--%>
                                                            <input type="button" ID="BtnExecuteEqual" value="乘積計算" class="btn btn-primary" Style="width: 150px" />
                                                            <asp:Label ID="LabelTotalThesisScore" runat="server"></asp:Label><br/>
                                                            <font color="red">(請按此計數)</font>
                                                            計算公式：C×J×A 
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow9" runat="server">
                                                        <asp:TableCell ID="TableCell11" runat="server" style="text-align:right">
                                        <font color="red">『論文』</font>成果檔案上傳：
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell12" runat="server">
                                                            論文名稱： 
                                        <asp:TextBox ID="AppThesisName" runat="server" Text="" class="form-control" Width="83%" />
                                                            <br />
                                                            <asp:CheckBox ID="AppThesisUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="AppThesisUploadFUName" runat="server" ReadOnly="true" Text=""
                                                                Visible="false"></asp:TextBox>
                                                            <asp:HyperLink ID="AppThesisHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <span title="成果檔案上傳">
                                                                <asp:FileUpload ID="AppThesisUploadFU" runat="server" />
                                                            </span>
                                                            <br />
                                                            <asp:Label ID="Label14" runat="server"><font color="red">(如有接受函請併入上傳檔案pdf, <br/>檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow10" runat="server">
                                                        <asp:TableCell ID="TableCell13" runat="server" style="text-align:right">
                                                            IF/排名/領域別：
                                                        </asp:TableCell><asp:TableCell ID="TableCell14" runat="server" class="style4">
                                                            IF/排名/領域別名稱： 
                                                            <asp:TextBox ID="AppThesisJournalRefCount" runat="server" Width="80%" class="form-control"></asp:TextBox>
                                                            <br />
                                                            <asp:CheckBox ID="AppThesisJournalRefUploadCB" runat="server" title="取消勾可刪除" />
                                                            <asp:TextBox ID="AppThesisJournalRefUploadFUName" runat="server"
                                                                ReadOnly="true" Text="" Visible="false"></asp:TextBox>
                                                            <asp:HyperLink ID="AppThesisJournalRefHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                            <asp:FileUpload ID="AppThesisJournalRefUploadFU" runat="server" /><br/>
                                                            <asp:Label ID="Label15" runat="server"><font color="red">(請檢附資料庫查詢畫面，無SCIE分數者免附, <br/>檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow11" runat="server">
                                                        <asp:TableCell ID="TableCell15" runat="server" style="text-align:right">
                                        此篇是否選為代表著作：<%--<font color="red">●</font>--%>
                                                        </asp:TableCell><asp:TableCell ID="TableCell16" runat="server">
                                                            <asp:CheckBox ID="RepresentCB" name="RepresentCB" runat="server" />
                                                            <%--<input type="checkbox" id="RepresentCB" />--%>
                                                            <br />
                                                            <div id="dvPassport" style="display: none">
                                                            <asp:Table ID="RepresentTable" name="RepresentTable" runat="server" Visible="true">
                                                                <asp:TableRow>
                                                                    <asp:TableCell style="text-align:center" Width="15%">
                                                                        中文<br/>摘要：
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:TextBox ID="ThesisSummaryCN" runat="server" Height="100px"
                                                                            Text=""
                                                                            TextMode="Multiline" Width="400px"></asp:TextBox>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell style="text-align:right">
                                                                        合著人<br/>證明：</asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <span title="合著人證明(正本)上傳">
                                                                            <asp:CheckBox ID="ThesisCoAuthorUploadCB" runat="server" title="取消勾可刪除" />
                                                                            <asp:TextBox ID="ThesisCoAuthorUploadFUName" runat="server" Text=""
                                                                                ReadOnly="true" Visible="false"></asp:TextBox>
                                                                            <asp:HyperLink ID="ThesisCoAuthorHyperLink" runat="server"></asp:HyperLink>
                                                                            <asp:FileUpload ID="ThesisCoAuthorUploadFU" runat="server" />
                                                                        </span>
                                                                        <br />
                                                                        <asp:Label ID="Label13" runat="server"><font color="red">(限正本pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                                </div>
                                                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow12" runat="server">
                                                        <asp:TableCell ID="TableCell17" runat="server" style="text-align:right">
                                                            此篇是否選取計算RPI：<%--<font color="Green">○</font>--%>
                                                        </asp:TableCell><asp:TableCell ID="TableCell18" runat="server">
                                                            <asp:CheckBox ID="CountRPICB" runat="server" />
                                                        </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow13" runat="server">
                                                        <asp:TableCell ID="TableCell19" runat="server" colspan="2" Style="text-align: center">
                                                            <asp:Button ID="ThesisScoreSave" class="btn btn-primary" runat="server" OnClick="ThesisScoreSave_Click"
                                                                Text="新增" />
                                                            <asp:Button ID="ThesisScoreUpdate" runat="server" class="btn btn-primary" Style="color: white" OnClick="ThesisScoreUpdate_Click" Text="更新" Visible="false" />
                                                            <asp:Button ID="ThesisScoreCancel" runat="server" class="btn btn-danger"
                                                                OnClick="ThesisScoreCancel_Click" Visible="false" Text="取消" />
                                                        </asp:TableCell></asp:TableRow><%--<asp:TableRow ID="TableRow14" runat="server">
                                                        <asp:TableCell ID="TableCell20" runat="server" colspan="2" Style="text-align: center">
                                                            <asp:Label ID="MessageLabelThesis" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                        </asp:TableCell></asp:TableRow>--%></asp:Table></div></div></div></div><table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="ThesisScoreImport" runat="server" class="btn btn-primary"
                                                OnClick="ThesisScoreImport_Click"
                                                OnClientClick=" return confirm('確認重新匯入5年內所有論文至此? 將刪除『此處』已有資料。')" Text="批次匯入論文" />
                                            <input type="button" id="ThesisScoreAppend" onclick="ThesisScoreSelectClick()" value="選取論文匯入" class="btn btn-warning" style="color: white" />
                                            <asp:Button ID="ThesisScoreInsert" runat="server" OnClick="ThesisScoreInsert_Click" Text="新增一篇論文" class="btn btn-success" /><%--(新論文放置第一筆，再調整順序)--%>
                                            <br />
                                            ※本項匯入功能，資料由<a href="http://rdsys.tmu.edu.tw/sci/login.asp" target="_blank">教師著作目錄系統</a>登錄論文資料(論文、專利)提供。<%--<div
                                                style="background-color: #FFFF6F">
                                                由「修改」進入後，可上傳接受函pdf檔案，微調修改(C)(J)(A)....按「更新」儲存
                                            </div>--%><br/><br/></td></tr><tr>
                                        <td>
                                            <span style="color: black">1.依本校「教師著作升等研究部分最低標準施行要點」規定，以本校最新版之論文歸類計分(研究表現指數計分表)方式行之。SCIE、SSCI、EI之期刊論文資料，可就近至各大學圖書館、國科會科技政策研究與資訊中心等查閱或上網檢索。目前SCIE、SSCI、EI期刊以<font color='red'>2020</font>版本為準。<br /> 2.五年內<font color="red">(2017.2.1至2022.4.30)</font>且<font color="red">符合前職等教師資格</font>之論文計分，最高採計<font
                                                    color="red">15</font>篇。<br /> <font color="red">3.</font>若有文章2016年底前接受發表於本校所發行之國際期刊(JECM)者，可提一篇其計分方式以學術論文刊登雜誌加權分數(J)=4分計算。<br /> 4.填寫說明：<br /> <font color="red">★</font>1.學術論文必須填寫所有作者(按期刊所刊登之原排序)、著作名稱、期刊名稱、<font color="red">年月</font>、卷期、起迄頁數。 <br /><font color="red">★</font>2.專利必須填寫專利名稱、發明人、證書號碼、國別、專利期限。<br /> <font color="red">★</font>3.技術移轉必須填寫技術名稱、技轉金額及對象、<font color="red">年月</font>。<br /> <font color="red">★</font>4.刊登雜誌分類排名<font color="red">以最新版本之SCIE及SSCI資料為準。</font><br /> <font color="red">★</font>5.代表著作請於「研究成果分類代碼欄」<font color="red">註明「代表著作」</font><br /> </span></td></tr><tr>
                                        <td style="text-align: center">
                                            <asp:GridView ID="GVThesisScore" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" CssClass="table table-bordered table-condensed"
                                                DataKeyNames="SnNo" DataSourceID="DSThesisScore"
                                                EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal"
                                                OnPreRender="GVThesisScore_PreRender" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" OnRowDataBound="GVThesisScore_RowDataBound" Wrap="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisSn" runat="server" Text='<%#Bind("ThesisSn")%>'></asp:Label></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSnNo" runat="server" Text='<%#Bind("SnNo")%>'></asp:Label></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="IsRepresentative" runat="server"
                                                                Text='<%#Bind("IsRepresentative")%>'></asp:TextBox></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="IsCountRPI" runat="server" Text='<%#Bind("IsCountRPI")%>'></asp:TextBox></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="選為代表著作" ItemStyle-Width="30">
                                                        <HeaderStyle />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblThesisSign" runat="server" ForeColor="Red"></asp:Label></ItemTemplate><ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="選取計算RPI" ItemStyle-Width="30" Visible ="false">
                                                        <HeaderStyle />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCountRPI" runat="server" ForeColor="green"></asp:Label></ItemTemplate><ItemStyle />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmpSn" HeaderText="EmpSn" SortExpression="EmpSn"
                                                        Visible="False" />
                                                    <asp:TemplateField HeaderText="排序">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButtonSnNoUp" runat="server" ImageUrl="./image/arrow_up.png" OnCommand="ButtonSnNoUp_Click" CommandName="Sort" CommandArgument='<%#Eval("ThesisSn") +","+ Eval("SnNo") %>' />
                                                            <asp:ImageButton ID="ImageButtonSnNoDown" runat="server" ImageUrl="./image/arrow_down.png" OnCommand="ButtonSnNoDown_Click" CommandName="Sort" CommandArgument='<%#Eval("ThesisSn")+","+ Eval("SnNo") %>' OnClick="ButtonSnNoDown_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="SnNo" HeaderText="序號" SortExpression="SnNo" />
                                                    <asp:BoundField DataField="RRNo" HeaderText="成果類別代碼" SortExpression="RRNo" Visible="false"  />
                                                    <asp:BoundField DataField="ThesisResearchResult" HeaderText="代表性研究成果名稱</br>作者本人「+」，通訊作「*」，貢獻相同作者「#」 "
                                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Left"
                                                        SortExpression="ThesisResearchResult" />
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
                                                            <asp:HyperLink ID="HyperLinkThesis" CssClass="far fa-save" runat="server"></asp:HyperLink></ItemTemplate></asp:TemplateField><asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkThesisScoreMod" runat="server" class="btn btn-warning" Style="color: white" OnClick="ThesisScoreModData_Click" Text="修改" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkThesisDelete" runat="server" class="btn btn-danger" Style="color: white"
                                                                OnClick="ThesisScoreDeleteData_Click"
                                                                OnClientClick="return confirm('確認要是否要刪除？');" Text="刪除"></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns></asp:GridView><asp:SqlDataSource ID="DSThesisScore" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                SelectCommand="SELECT a.ThesisSn, SnNo, EmpSn, RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, CASE   WHEN b.indexB > 0 THEN SUBSTRING(ThesisUploadName,1,b.indexB)+'.pdf'	ELSE  ThesisUploadName  END as ThesisUploadName,ThesisJournalRefCount,IsRepresentative,IsCountRPI, ThesisBuildDate, ThesisModifyDate FROM ThesisScore a LEFT JOIN  (SELECT ThesisSn, CHARINDEX('_'+(SELECT EmpNameCN from EmployeeBase where EmpSn=@EmpSn),ThesisUploadName,0)-1 as indexB from ThesisScore where EmpSn=@EmpSn)  b ON a.ThesisSn = b.ThesisSn Where a.EmpSn = @EmpSn  and (a.AppSn = @AppSn or  a.AppSn = '0') ORDER BY SnNo">
                                                <SelectParameters>
                                                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                    <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                                </SelectParameters>
                                                <DeleteParameters>
                                                    <asp:Parameter Name="ThesisSn" />
                                                </DeleteParameters>
                                            </asp:SqlDataSource>
                                            <br />
                                            <asp:GridView ID="GVThesisScoreCoAuthor" runat="server"
                                                AutoGenerateColumns="False" BorderWidth="0px" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" CssClass="table table-bordered table-condensed" DataKeyNames="ThesisSn"
                                                DataSourceID="DSThesisScoreCoAuthor" EnableModelValidation="True"
                                                GridLines="None" OnRowDataBound="GVThesisScoreCoAuthor_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="SnNo" HeaderText="序號" SortExpression="SnNo" HeaderStyle-Width="5%" />
                                                    <asp:BoundField DataField="ThesisName" HeaderText="論文名稱"
                                                        SortExpression="ThesisName" />
                                                    <asp:BoundField DataField="ThesisJournalRefCount" HeaderText="IF/排名/領域別"
                                                        SortExpression="ThesisJournalRefCount" />
                                                    <asp:TemplateField HeaderText="檢附資料下載" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkThesisJournalRef" CssClass="far fa-save" runat="server"></asp:HyperLink></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="合著人證明" HeaderStyle-Width="8%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLinkThesisCo" CssClass="far fa-save" runat="server">下載</asp:HyperLink></ItemTemplate></asp:TemplateField><asp:BoundField DataField="ThesisSummaryCN" HeaderText="中文摘要"
                                                        SortExpression="ThesisSummaryCN" />
                                                    <asp:TemplateField HeaderText="ThesisCoAuthorUploadName">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisCoAuthorUploadName" runat="server"
                                                                Text='<%#Bind("ThesisCoAuthorUploadName")%>'></asp:Label></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ThesisJournalRefUploadName">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisJournalRefUploadName" runat="server"
                                                                Text='<%#Bind("ThesisJournalRefUploadName")%>'></asp:Label></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="DSThesisScoreCoAuthor" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                DeleteCommand="DELETE FROM ThesisScore WHERE (ThesisSn = @ThesisSn)"
                                                SelectCommand="SELECT ThesisSn, SnNo, EmpSn, RRNo, ThesisName, ThesisUploadName, ThesisJournalRefCount, ThesisJournalRefUploadName, IsRepresentative, ThesisSummaryCN, ThesisCoAuthorUploadName, ThesisBuildDate, ThesisModifyDate FROM ThesisScore Where EmpSn = @EmpSn and (AppSn = @AppSn or  AppSn = '0') and ( IsRepresentative = 'True' or ThesisJournalRefUploadName &lt;&gt; '') Order By SnNo">
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
                                <table style="font-size: 12pt; width: 100%;" class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="width: 15%; text-align: right">(1) 論文總積分： </td><td>
                                            <asp:Label ID="AppPThesisAccuScore" runat="server" Text=""></asp:Label><br />總計<asp:Label ID="PaperCount" runat="server" ForeColor="#FF3300"></asp:Label>篇，超過<asp:Label ID="PaperOver" runat="server" ForeColor="#FF3300"></asp:Label>篇<br /> (最高採計15篇, 各項研究成果分數之總和)</td></tr><tr>
                                        <td style="text-align: right">折抵論文積分：</td><td>
                                            <asp:CheckBox ID="RPIDiscountNo" runat="server" Text=""></asp:CheckBox>&nbsp;無下列資料請打勾<br /> 獲獎折抵(請檢附PDF文件，無折抵者免附)<br /> 1.<asp:CheckBox ID="RPIDiscountScore1" runat="server" title="師鐸獎：60分" />&nbsp;師鐸獎：60分
                                        &nbsp;&nbsp;&nbsp;<asp:CheckBox ID="RPIDiscountScore1UploadCB" runat="server" title="取消勾可刪除" />
                                            <asp:TextBox ID="RPIDiscountScore1UploadFUName" runat="server" ReadOnly="true"
                                                Style="display: none;" Text="" MaxLength="100"></asp:TextBox><asp:HyperLink ID="RPIDiscountScore1HyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="RPIDiscountScore1UploadFU" runat="server" /><br /><br />
                                            2.教師優良教師：
                                            &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore2" runat="server" class="form-control">
                                                <asp:ListItem Value="請選擇">請選擇</asp:ListItem><asp:ListItem Value="60">校級：60分</asp:ListItem><asp:ListItem Value="30">院級：30分</asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="RPIDiscountScore2UploadCB" runat="server" title="取消勾可刪除" />
                                            <asp:TextBox ID="RPIDiscountScore2UploadFUName" runat="server" ReadOnly="true"
                                                Style="display: none;" Text="" MaxLength="100"></asp:TextBox><asp:HyperLink ID="RPIDiscountScore2HyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="RPIDiscountScore2UploadFU" runat="server" /><br /><br />
                                            3.優良導師：
                                            &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore3" runat="server" class="form-control">
                                                <asp:ListItem Value="請選擇">請選擇</asp:ListItem><asp:ListItem Value="60">校級：60分</asp:ListItem><asp:ListItem Value="30">院級：30分</asp:ListItem></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="RPIDiscountScore3UploadCB" runat="server" title="取消勾可刪除" />
                                            <asp:TextBox ID="RPIDiscountScore3UploadFUName" runat="server" ReadOnly="true"
                                                Style="display: none;" Text="" MaxLength="100"></asp:TextBox><asp:HyperLink ID="RPIDiscountScore3HyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="RPIDiscountScore3UploadFU" runat="server" /><br /><br />
                                            4.執行人體試驗：
                                            &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore4" runat="server" class="form-control">
                                                <asp:ListItem Value="請選擇">請選擇</asp:ListItem>
                                                <asp:ListItem Value="60">研究者自行發起(Investigator initiated Trial)，且完成臨床試驗資料庫(Clinicaltrials.gov)登錄者：60分</asp:ListItem>
                                                <asp:ListItem Value="45">主持新醫療技術、新藥品人體試驗一期(Phase I)、新醫療器材人體試驗第三等級(Class 3)者：45分</asp:ListItem>
                                                <asp:ListItem Value="30">主持新藥品人體試驗二期(Phase II)、新醫療器材人體試驗第二等級(Class 2)者：30分</asp:ListItem>
                                                <asp:ListItem Value="15">主持新藥品人體試驗三期(Phase III)者：15分</asp:ListItem>
                                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="RPIDiscountScore4UploadCB" runat="server" title="取消勾可刪除" />
                                            <asp:TextBox ID="RPIDiscountScore4UploadFUName" runat="server" ReadOnly="true"
                                                Style="display: none;" Text="" MaxLength="100"></asp:TextBox><asp:HyperLink ID="RPIDiscountScore4HyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="RPIDiscountScore4UploadFU" runat="server" /><br />
                                             5.執行產學合作計畫：<br />
                                           
                                            <asp:LinkButton ID="lnkThesisCoopCreate" runat="server" class="btn btn-primary" Style="color: white" OnClick="lnkThesisCoopCreate_Click" Text="新增" />
                                            <asp:GridView ID="GVThesisCoop" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" CssClass="table table-bordered table-condensed" DataKeyNames="ID"
                                                EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GVThesisCoop_RowDataBound"
                                                HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" Wrap="True">
                                                <Columns>
                                                    <asp:BoundField DataField="ProjectContent" HeaderText="計畫名稱、計畫執行期間、產學合作實收金額" />
                                                    <asp:BoundField DataField="Classification" HeaderText="實收金額分類(T)" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="RD" HeaderText="校方佔有研發成果智慧財產權比例分類(I)"  HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"  />
                                                    <asp:TemplateField HeaderText="實收金額分類(T)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblThesisCoopClassification" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="校方佔有研發成果智慧財產權比例分類(I)">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblThesisCoopRD" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Contribute" HeaderText="貢獻比例(C)" DataFormatString="{0:N0}%"  />
                                                    <asp:BoundField DataField="Total" HeaderText="總分數(T) x (I) x (C)" DataFormatString="{0:N0}" />
                                                    <asp:BoundField DataField="UploadFileName" HeaderText="上傳檔案" />
                                                    <asp:TemplateField HeaderText="下載">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="ThesisCoopHyperLink" CssClass="far fa-save" runat="server"></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkThesisCoopMod" runat="server" class="btn btn-warning" Style="color: white" OnClick="ThesisCoopModData_Click" Text="修改" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkThesisDelete" runat="server" class="btn btn-danger" Style="color: white"
                                                               OnClick="ThesisCoopDeleteData_Click"
                                                                OnClientClick="return confirm('確認要是否要刪除？');" Text="刪除"></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><HeaderStyle BackColor="#0080FF" ForeColor="White" />
                                            </asp:GridView>
											<asp:Label ID="Label16" runat="server"><font color="red">(檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label></td></tr><tr>
                                        <td style="text-align: right">(2)折抵總積分：</td><td>
                                            <asp:Label ID="RPIDiscountTotal" runat="server" Text=""></asp:Label><font color="red">(最高採計180分)</font></td></tr><tr>
                                        <td style="text-align: right">總積分：</td><td>(1)論文總積分：<asp:Label ID="AppPThesisAccuScore1" runat="server" Text=""></asp:Label>&nbsp;+&nbsp;(2)折抵論文積分：<asp:Label ID="RPIDiscountTotal1" runat="server" Text=""></asp:Label>&nbsp;=&nbsp;<asp:Label ID="AppRPITotalScore" runat="server" Text=""></asp:Label></td></tr><tr>
                                        <td id="TableC1" runat="server" style="text-align: right">研究年資：</td><td id="TableC2" runat="server">
                                            <asp:DropDownList ID="AppResearchYear" runat="server" class="form-control">
                                                <asp:ListItem Value="7">滿5年以上(選最佳7篇)</asp:ListItem><asp:ListItem Value="4">滿4年(選最佳4篇)</asp:ListItem><asp:ListItem Value="2">滿3年(選最佳2篇)</asp:ListItem><asp:ListItem Value="1">未滿3年(選最佳1篇)</asp:ListItem></asp:DropDownList></td></tr><tr style="display:none">

                                        <td style="text-align: right">已選RPI積分小計：</td><td><font color="green">
                                            <asp:Label ID="ComputeRPI" runat="server" Text="" Width="59px"></asp:Label></font><br/>(自動計數值供使用者參考)<br/><asp:TextBox ID="AppRPI" runat="server" Text="" class="form-control"></asp:TextBox></td></tr><tr>
                                        <td style="text-align: right">論文折抵暨總分：</td><td>
                                            <asp:Button ID="SaveAppRPI" runat="server" OnClick="AppRPIDiscountSave_Click" PostBack="True" Text="計算儲存" class="btn btn-primary" /><br/>
                                            <font color="red">(請按此儲存折抵暨總分)</font> </td></tr></table></asp:View><asp:View ID="ViewDegreeThesis" runat="server">
                                <asp:Table runat="server" ID="AboutDegree" Width="100%" border="1" CellSpacing="0" CellPadding="0" bgcolor="lightgoldenrodyellow" class="table table-bordered table-condensed">
                                    <asp:TableRow ID="TableRow6" runat="server">
                                        <asp:TableCell Style="text-align: right" Width="13%">
                                    學位論文著作：
                                        </asp:TableCell><asp:TableCell>
                                            <span title="學位論文上傳">著作題名：&nbsp;
                                    <asp:TextBox ID="AppDegreeThesisName" runat="server" Text="" Width="88%" class="form-control"></asp:TextBox><br />
                                                <br />
                                                英文題名：&nbsp;
                                                <asp:TextBox ID="AppDegreeThesisNameEng" runat="server" Width="88%" Text="" class="form-control"></asp:TextBox><br />
                                                <br />
                                                <asp:CheckBox ID="AppDegreeThesisUploadCB" runat="server" title="取消勾可刪除" />
                                                <asp:Label ID="AppDegreeThesisUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:Label>
                                                <asp:HyperLink ID="AppDegreeThesisHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="AppDegreeThesisUploadFU" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="Label17" runat="server"><font color="red">(限pdf, 檔名請勿使用／, ？, ＊, ［, ］, ' , &, " , ?等符號)</font></asp:Label>
                                        </asp:TableCell></asp:TableRow><asp:TableRow ID="AboutFgn" runat="server" Visible="false">
                                        <asp:TableCell Style="text-align: right">
                                    國外學歷：
                                        </asp:TableCell><asp:TableCell>
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
                                            <asp:FileUpload ID="AppDFgnSelectCourseUploadFU" runat="server" /><br />（修業期限：累計在當地學校修業時間： 碩士學位須滿8個月；博士學位須滿16個月；連續修讀碩、博士學位須滿24個月）。<br />
                                            <asp:CheckBox ID="AppDFgnEDRecordUploadCB" runat="server" title="取消勾可刪除" />
                                            5.個人出入境紀錄，可至<a href="https://www.immigration.gov.tw/5385/7244/7250/20406/7326/77703/" target="_blank">內政部入出國及移民署網站</a>查詢。
                                        <asp:TextBox ID="AppDFgnEDRecordUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox>
                                            <asp:HyperLink ID="AppDFgnEDRecordHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            <asp:FileUpload ID="AppDFgnEDRecordUploadFU" runat="server" /><br />

                                            <asp:Table ID="AboutJPN" runat="server" Visible="false" class="table table-bordered table-condensed">
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
                                        </asp:TableCell></asp:TableRow><asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" Style="text-align: center">
                                            <asp:Button ID="DegreeThesisSave" runat="server" Text="儲存" Width="120px" OnClick="DegreeThesisSave_Click" class="btn btn-primary" />
                                        </asp:TableCell></asp:TableRow><%--                                    <asp:TableRow>
                                        <asp:TableCell ColumnSpan="2" Style="text-align: center">
                                            <asp:Label ID="MessageLabelDegreeThesis" runat="server" Text="" ForeColor="Red"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>--%></asp:Table><br /><div>
                                         ※摘錄「教育部辦理專科以上學校教師著作審查委員遴選原則」：<br/> (一)審查委員具有下列情形之一者，應迴避審查。<br/> 1.送審人之研究指導教授。<br/> 2.送審人代表著作之合著人或共同研究人。<br/> 3.與送審人有密切合作關係者。<br/> 4.與送審者有親屬關係或有特殊事宜者。<br/> (二)審查委員之遴選為顧及公平性與平衡性，宜盡量兼顧下列原則：<br/> 1.同一案件之審查委員儘可能避免均由同一學校之教授擔任。<br/> 2.送審人畢業學校之教授盡可能迴避，（尤其是畢業時間十年以內，且為同一系所者）。<br/> 3.與送審人為同校系且同時期畢業者盡可能迴避審查。<br/> 4.曾與送審者共同參與相關研究者，盡可能迴避審查。<br /><br/><font color="red">※本表如未填寫視同無迴避名單提供</font><br/> <div style="float: right;">
                                        <asp:Button runat="server" ID="avoidListAdd" OnClick="avoidListAdd_Click" class="btn btn-primary" Text="新增迴避名單"></asp:Button>
                                    </div></div><br /><hr /><div class="modal fade" id="modalAvoidList">
                                    <div class="modal-dialog modal-lg">
                                        <!-- Modal content-->
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    X</button><br /><br /><table width="100%" class="table table-bordered table-condensed">
                                                    <tr>
                                                        <td style="text-align: right;">身分別：<asp:TextBox ID="TBIntThesisOralSn" Visible="false" runat="server"></asp:TextBox></td><td>
                                                            <asp:DropDownList ID="ThesisOralType" runat="server" class="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="15%" style="text-align: right">姓名：</td><td>
                                                            <asp:TextBox ID="ThesisOralName" runat="server" class="form-control"></asp:TextBox></td></tr><tr>
                                                        <td style="text-align: right">職稱：</td><td>
                                                            <asp:TextBox ID="ThesisOralTitle" runat="server" class="form-control"></asp:TextBox></td></tr><tr>
                                                        <td style="text-align: right">服務單位：</td><td>
                                                            <asp:TextBox ID="ThesisOralUnit" runat="server" class="form-control"></asp:TextBox></td></tr><tr>
                                                        <td style="text-align: right">其他備註：</td><td>
                                                            <asp:TextBox ID="ThesisOralOther" runat="server" class="form-control"></asp:TextBox><br /><asp:Label ID="AvoidReason" runat="server" Visible="false" Text="(迴避理由)" ForeColor="Red" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align: center">
                                                            <asp:Button ID="ThesisOralSave" runat="server" Text="新增" OnClick="ThesisOralSave_Click" class="btn btn-primary" />
                                                            <asp:Button ID="ThesisOralUpdate" runat="server" Text="更新" class="btn btn-primary" Style="color: white" OnClick="ThesisOralUpdate_Click" Visible="false" />
                                                            <asp:Button ID="ThesisOralCancel" runat="server" Text="取消" OnClick="ThesisOralCancel_Click" Visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <table width="100%" class="table table-bordered table-condensed">
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:GridView ID="GVThesisOral" runat="server" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="table table-bordered table-condensed" DataKeyNames="ThesisOralSn" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" OnRowDataBound="GVThesisOral_RowDataBound" EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                        <HeaderStyle CssClass="hiddencol" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisOralSn" runat="server" Text='<%#Bind("ThesisOralSn")%>'></asp:Label></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="身分別" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ThesisOralType" runat="server" Text='<%#Bind("ThesisOralType")%>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="ThesisOralName" HeaderText="姓名" HeaderStyle-Width="10%"
                                                        SortExpression="ThesisOralName" />
                                                    <asp:BoundField DataField="ThesisOralTitle" HeaderText="職稱" HeaderStyle-Width="10%"
                                                        SortExpression="ThesisOralTitle" />
                                                    <asp:BoundField DataField="ThesisOralUnit" HeaderText="服務單位"
                                                        SortExpression="ThesisOralUnit" />
                                                    <asp:BoundField DataField="ThesisOralOther" HeaderText="其他備註"
                                                        SortExpression="ThesisOralOther" />
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ThesisOralMod" runat="server" class="btn btn-warning" Style="color: white" OnClick="ThesisOralMod_Click" Text="修改" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="ThesisOralDel" runat="server" CssClass="btn btn-danger" Style="color: white" OnClick="ThesisOralDel_Click"
                                                                Text="刪除" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:Label runat="server" Visible="false" ID="lb_NoThesisOral" Text="尚無填寫資料。" />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </asp:View></asp:MultiView></div></div></div></div><div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-66382736 #1600
            </label>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#RepresentCB").click(function () {
                if ($(this).is(":checked")) {
                    $("#dvPassport").show();
                } else {
                    $("#dvPassport").hide();
                }
            });
            $("#BtnExecuteEqual").click(function () {
                if ($('#AppThesisC').val() == "" || $('#AppThesisC').val() == null || $('#AppThesisC').val() == undefined)
                    $('#AppThesisC').val("1.0");
                if ($('#AppThesisJ').val() == "" || $('#AppThesisJ').val() == null || $('#AppThesisJ').val() == undefined)
                    $('#AppThesisJ').val("1.0");
                if ($('#AppThesisA').val() == "" || $('#AppThesisA').val() == null || $('#AppThesisA').val() == undefined)
                    $('#AppThesisA').val("1.0");
                //alert($('#AppThesisC').val());
                //alert($('#AppThesisJ').val());
                //alert($('#AppThesisA').val());
                var a = $('#AppThesisC').val() * $('#AppThesisJ').val() * $('#AppThesisA').val() * 1.0;
                $('#LabelTotalThesisScore').text(a);
            });
        });
        $(document).ready(function () {
            if ($("#RepresentCB").is(":checked")) {
                $("#dvPassport").show();
            } else {
                $("#dvPassport").hide();
            }
        });
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
                else if ($(this).attr('id') == "AppThesisUploadFU" || $(this).attr('id') == "AppThesisJournalRefUploadFU" || $(this).attr('id') ==  "ThesisCoAuthorUploadFU")
                {
                    if (file.type != 'application/pdf') {
                        alert('檔案格式限pdf'); //顯示警告
                        $(this).val('');
                    }
                }
                else if (file.type != 'application/pdf' && file.type != 'image/png' && file.type != 'image/jpg' && file.type != 'image/jpeg' && file.type != 'application/msword' && file.type != 'application/vnd.openxmlformats-officedocument.wordprocessingml.document') {
                    //假如檔案格式不等於 png 、jpg、jpeg、pdf、doc、docx
                    //alert('type=' + file.type);
                    alert('檔案格式不符合規範'); //顯示警告
                    $(this).val('');
                }
            }
        });


        $(document).on('change', ':text', function () {  //選取類型為file且值發生改變的
            if ($(this).attr('id') == "TeachLessonHours" || $(this).attr('id') === "TeachLessonCreditHours") {
                var text = document.getElementById('txtJob').value
                alert(text);
                if (text.match(/[0-9]/)) {
                    alert('本欄限輸入數字');
                    $(this).val('');
                }
            }
        });

        function ThesisScoreSelectClick() {
            var r = confirm('匯入選取資料，需確認資料是否重覆！')
            if (r == true) {
                window.open('FunSelectThesisScore.aspx', '_blank', 'toolbar=0,menubar=1,location=0,scrollbars=1,resizable=1,height=800,width=1000');
            } else {
                return false;
            }
        }

        function txtKeyNumber() {
            if (!(((window.event.keyCode >= 48) && (window.event.keyCode <= 57)) ||
            (window.event.keyCode == 13) || (window.event.keyCode == 46) ||
            (window.event.keyCode == 45)))
                //這段是判斷如果輸入的不是數字或小數點!那將無法輸入文字
            {
                window.event.keyCode = 0;
            }
        }
    </script>
</body>
</html>
