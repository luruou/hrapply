<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageSetAudit.aspx.cs" Inherits="ApplyPromote.ManageSetAudit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type='text/javascript' src='http://code.jquery.com/jquery-1.5.2.js'></script>
<script type="text/javascript">
    function reLoad() {
        window.opener.location.reload();
    }
</script>
<head runat="server">
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <link href="css/tabs.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
    <style type="text/css">
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
    <title>個人資料審查</title>
</head>

<body>
    <form id="formApplyView" runat="server" enctype="multipart/form-data">
        <%--標題和導覽列--%>
        <nav class="navbar navbar-expand-lg navbar-light bg-light" style="position: fixed; width: 100%; z-index: 1000">
            <img src="image/Logo.png" width="130" style="float: left;" />
            <a class="navbar-brand" href="#" style="color: white; font-weight: bold; font-size: 23px">教師新聘升等系統</a>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
            </div>

            <ul class="nav navbar-nav float-xs-right">
                <%-- 放右上按鈕--%>
                <li class="nav-item"><a href="Default.aspx" style="display:none" class="btn btn-large btn-danger">登出</a></li>
            </ul>
        </nav>
        <%--標題和導覽列end--%>
        <br />
        <br />
        <asp:Label ID="AppAttributeNo" runat="server" Text="Label" Visible="false" />
        <asp:Label ID="EmailEdu" runat="server" Text="" Visible="false" />
        <asp:Label ID="EmailExp" runat="server" Text="" Visible="false" />
        <%--        <div class="" style="width:60%;padding-left:100px;">--%>
        <div style="width: 80%; margin: auto">
            <asp:TextBox ID="TBEmpSn" runat="server" value="1" AutoPostBack="True" Visible="false"></asp:TextBox><br/>        
        <!--<asp:Button ID="DownloadPdf" runat="server" AutoPostBack="True" OnClick="DownloadPdf_OnClick" Text="Pdf"/>-->
            <fieldset>
                <h2><b>檢視上傳資料與簽核資料</b></h2>
                <hr />
                <div style="font-size: 20px; background-color: #ffed97; border-radius: 10px; height: 80px;">
                    <div style="padding: 10px">
                        <asp:Label ID="AuditNameCN" runat="server" />─<asp:Label ID="AuditUnit" runat="server" />─<asp:Label ID="AuditJobTitle" runat="server" />─<asp:Label ID="AuditJobType" runat="server" />
                        <br />
                        <asp:Label ID="AuditWayName" runat="server" />─<asp:Label ID="AuditKindName" runat="server" />─<asp:Label ID="AuditAttributeName" runat="server" Text="Label" />─<font color="red"><asp:Label ID="AuditStatusName" runat="server" Text="" />&nbsp;<asp:Label ID="AuditRoleName" runat="server" Text="" /></font>
                    </div>
                </div>


                <br />
                <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                    StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                    OnMenuItemClick="Menu1_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="申請者資料" Value="0" Selected="true" />
                        <asp:MenuItem Text="簽核" Value="1" />
                        <asp:MenuItem Text="關卡設定" Value="2" />

                    </Items>
                </asp:Menu>
                <asp:Menu ID="Menu2" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                    StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                    OnMenuItemClick="Menu2_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="申請者資料" Value="0" Selected="true" />
                        <asp:MenuItem Text="簽核" Value="1" />
                    </Items>
                </asp:Menu>
                <asp:Menu ID="Menu3" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                    StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                    OnMenuItemClick="Menu3_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="申請者資料" Value="0" Selected="true" />
                        <asp:MenuItem Text="簽核" Value="1" />
                        <asp:MenuItem Text="關卡設定" Value="2" />
                    </Items>
                </asp:Menu>
                <asp:Menu ID="Menu4" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                    StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                    OnMenuItemClick="Menu4_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="申請者資料" Value="0" Selected="true" />
                    </Items>
                </asp:Menu>
                <div class="tabContents">
                    <asp:MultiView ID="MultiViewAudit" runat="server" ActiveViewIndex="0">
                        <asp:View ID="ViewTeachBase" runat="server">
                            <asp:HyperLink ID="ModifyEmpBase" runat="server" Visible="false"></asp:HyperLink>
                            <asp:Table ID="BaseTable" runat="server" class="table table-bordered table-condensed">
                                <asp:TableRow runat="Server">
                                    <asp:TableCell Width="13%" style="text-align:right">
                                    <font>身份証號：</font></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpIdno" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    <font >生日(民國)：</font></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBirthDay" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    護照號碼：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpPassportNo" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    英文名：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpNameENFirst" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    英文姓：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpNameENLast" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    姓名：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpNameCN" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    性別：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpSex" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    國籍：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpCountry" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpHomeTownTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    籍貫：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpHomeTown" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpBornProvinceTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    出生地：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBornProvince" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpBornCityTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    出生地-縣：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBornCity" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    應徵單位：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppUnit" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    應徵職稱：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppJobTitle" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                     職別：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppJobType" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="IsEmpYear" runat="Server" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    現職等年資：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpYear" runat="server" value="" MaxLength="20"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                        <font>
                                            <asp:Label ID="AppKindName" runat="server" Text="Label"></asp:Label></font>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppAttributeName" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    法規依據：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="LawText" runat="server"></asp:Label>
                                        <asp:Label ID="ItemNo" runat="server"></asp:Label>
                                        &nbsp;項第 
                                    <asp:Label ID="ItemLabel" runat="server"></asp:Label>
                                        款送審。
                                        <br />
                                        <asp:Label ID="LawContent" runat="server"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    電話(宅)：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpTelPri" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    電話(公)：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpTelPub" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    E-Mail：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpEmail" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    戶籍地址：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpTownAddressCode" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="EmpTownAddress" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    通訊地址：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpAddressCode" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="EmpAddress" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    手機：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpCell" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppENowJobOrgTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                   現任機關及職務：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppENowJobOrg" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppENoteTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    備註：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppENote" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppERecommendorsTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    推薦人姓名：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppERecommendors" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppERecommendYearTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    推薦日期 民國：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppERecommendYear" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="RecommendUploadTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                   推薦書上傳：
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TableCell>
                                            <span title="推薦書上傳">
                                                <asp:CheckBox ID="RecommendUploadCB" runat="server" title="取消勾可刪除" />
                                                <asp:TextBox ID="RecommendUploadFUName" runat="server" ReadOnly="true"
                                                    Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="RecommendHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                            </span>
                                        </asp:TableCell>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpExpertResearchTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    <font >學術專長及研究：</font></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpExpertResearch" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpPhotoTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    照片上傳：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="EmpPhotoUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="EmpPhotoUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox>
                                        <asp:Image ID="EmpPhotoImage" runat="server" Visible="false" Height="100px" Width="80px" />

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpIdnoTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    身份證上傳：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="EmpIdnoUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="EmpIdnoUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="EmpIdnoHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpDegreeTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                    最高學歷證件上傳：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="EmpDegreeUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="EmpDegreeUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="EmpDegreeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    教師資格審查資料切結書：</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="AppDeclarationUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="AppDeclarationUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox>
                                        <asp:HyperLink ID="AppDeclarationHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    <font color="red">履歷表CV上傳：</font><br/>
                                        <asp:Label runat="server" ForeColor="#FF3300">(僅申請一般專任教師適用)</asp:Label> 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <span title="含基本資料、主要學歷、主要經歷、代表著作、特殊貢獻等清單"/>
                                            <asp:CheckBox ID="AppResumeUploadCB" runat="server" Enabled="false" />
                                            <asp:TextBox ID="AppResumeUploadFUName" runat="server" ReadOnly="true"
                                                Style="display: none;" Text="" MaxLength="100"></asp:TextBox>
                                            <asp:HyperLink ID="AppResumeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow3" runat="Server">
                                    <asp:TableCell style="text-align:right">
                                    <font color="red">論文積分表：</font>  <br/>                                  
                                        <asp:Label runat="server" ForeColor="#FF3300">(僅申請一般專任教師適用)</asp:Label> 
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="ThesisScoreUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="ThesisScoreUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text="" MaxLength="100"></asp:TextBox>
                                        <asp:HyperLink ID="ThesisScoreUploadHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="Server">
                                    <asp:TableCell style="text-align:right">教師學歷資料：
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:GridView ID="GVTeachEdu" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                                            CellPadding="2" DataKeyNames="EduSn" DataSourceID="DSTeacherEdu"
                                            OnRowDataBound="GVTeachEdu_RowDataBound" EnableModelValidation="True" CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="EduSn" runat="server" Text='<%#Bind("EduSn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="EduSchool" HeaderText="學校名稱" />
                                                <asp:BoundField DataField="EduDepartment" HeaderText="系所名稱"
                                                    SortExpression="EduDepartment" />

                                                <asp:TemplateField HeaderText="修業期間(起迄)" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EduStartYM" runat="server" Text='<%#Bind("EduStartYM")%>'></asp:Label>~
                                                        <asp:Label ID="EduEndYM" runat="server" Text='<%#Bind("EduEndYM")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--                                                <asp:BoundField DataField="EduStartYM" HeaderText="修業期間(起"
                                                   SortExpression="EduStartYM" />
                                                    <asp:BoundField DataField="EduEndYM" HeaderText=" ~迄)"
                                                    SortExpression="EduEndYM" />--%>

                                                <asp:TemplateField HeaderText="學位(修業別)" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EduDegree" runat="server" Text='<%#Bind("EduDegree")%>' />(<asp:Label ID="EduDegreeType" runat="server" Text='<%#Eval("EduDegreeType").ToString().Substring(0,1)%>'></asp:Label>)
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--                                             <asp:BoundField DataField="EduDegree" HeaderText="學位"
                                                    SortExpression="EduDegree" />
                                                <asp:BoundField DataField="EduDegreeType" HeaderText="修業別"
                                                    SortExpression="EduDegreeType" />--%>
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
                                                <asp:SessionParameter DefaultValue="1" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <br />
                                        <asp:Table ID="AboutFgn" runat="server" Visible="false">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    國外學歷(<a href="http://tmu-hr.tmu.edu.tw/xhr/archive/download?file=5b7f78e54f4d123f5b00031d">持國外學歷需辦理驗證項目</a>)<br />
                                                    <asp:CheckBox ID="AppDFgnEduDeptSchoolAdmitCB" runat="server" Enabled="false" />
                                                    <font>1.是否為<a href="javascript:window.open('https://www.fsedu.moe.gov.tw/')">教育部國際及兩岸教育司</a>編印之冊列學校</font><br />
                                                    <asp:CheckBox ID="AppDFgnDegreeUploadCB" runat="server" Enabled="false" />
                                                    2.國外最高學位畢業證書(須經我國駐外館處驗證）
                                    <asp:TextBox ID="AppDFgnDegreeUploadFUName" runat="server" ReadOnly="true"
                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnDegreeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    <br />

                                                    <asp:CheckBox ID="AppDFgnGradeUploadCB" runat="server" Enabled="false" />
                                                    3.國外學校歷年成績單(須經我國駐外館處驗證）
                                    <asp:TextBox ID="AppDFgnGradeUploadFUName" runat="server" ReadOnly="true"
                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnGradeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    <br />
                                                    <asp:CheckBox ID="AppDFgnSelectCourseUploadCB" runat="server" Enabled="false" />
                                                    4.<a href="http://tmu-hr.tmu.edu.tw/xhr/archive/download?file=5b7f78c64f4d123f5b000315">國外學歷修業情形一覽表</a>（修業期限：累計在當地學校修業時間： 碩士學位須滿8個月；博士學位須滿16個月；連續修讀碩、博士學位須滿24個月）。
                                    <asp:TextBox ID="AppDFgnSelectCourseUploadFUName" runat="server" ReadOnly="true"
                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnSelectCourseHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    <br />
                                                    <asp:CheckBox ID="AppDFgnEDRecordUploadCB" runat="server" Enabled="false" />
                                                    5.個人出入境紀錄，可至<a href="https://www.immigration.gov.tw/5385/7244/7250/20406/7326/77703/" target="_blank">內政部入出國及移民署網站</a>查詢。
                                    <asp:TextBox ID="AppDFgnEDRecordUploadFUName" runat="server" ReadOnly="true"
                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnEDRecordHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    <br />

                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                        <asp:Table ID="AboutJPN" runat="server" Visible="false">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    以日本論文博士需另備下列要件，使採認具博士學位：<br />
                                                    <asp:CheckBox ID="AppDFgnJPAdmissionUploadCB" runat="server" Enabled="false" />
                                                    A.入學許可註冊證：<asp:TextBox ID="AppDFgnJPAdmissionUploadFUName" runat="server"
                                                        ReadOnly="true" Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnJPAdmissionHyperLink" runat="server"
                                                        Visible="false"></asp:HyperLink>
                                                    <br />
                                                    <asp:CheckBox ID="AppDFgnJPGradeUploadCB" runat="server" Enabled="false" />
                                                    B.修畢學分成績單<asp:TextBox ID="AppDFgnJPGradeUploadFUName" runat="server" ReadOnly="true"
                                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnJPGradeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    <br />
                                                    <asp:CheckBox ID="AppDFgnJPEnrollCAUploadCB" runat="server" Enabled="false" />
                                                    C.在學證明及修業年數證明<asp:TextBox ID="AppDFgnJPEnrollCAUploadFUName"
                                                        runat="server" ReadOnly="true"
                                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnJPEnrollCAHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    <br />
                                                    <asp:CheckBox ID="AppDFgnJPDissertationPassUploadCB" runat="server" Enabled="false" />
                                                    D.通過論文資格考試證明<asp:TextBox ID="AppDFgnJPDissertationPassUploadFUName"
                                                        runat="server" ReadOnly="true"
                                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnJPDissertationPassHyperLink" runat="server"
                                                        Visible="false"></asp:HyperLink>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="TeachExpTableRow">
                                    <asp:TableCell Width="200px" style="text-align:right">教師經歷資料：
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:Label runat="Server" ID="LbTeachExp" Visible="false">無</asp:Label>
                                        <asp:GridView ID="GVTeacherTmuExp" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                                            CellPadding="4" DataKeyNames="ExpSn" DataSourceID="DSTeacherTmuExp"
                                            ForeColor="Black" GridLines="Horizontal" BackColor="White"
                                            BorderColor="#CCCCCC" BorderWidth="1px" OnRowDataBound="GVTeacherTmuExp_RowDataBound"
                                            BorderStyle="Solid" EnableModelValidation="True" CssClass="table table-bordered table-condensed">

                                            <Columns>
                                                <asp:TemplateField HeaderText="No">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No" >
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="EmpSn" runat="server" Text='<%#Bind("EmpSn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ExpUnitName" HeaderText="單位" SortExpression="ExpUnitName" />
                                                <asp:BoundField DataField="ExpPosName" HeaderText="職別"
                                                    SortExpression="ExpPosName" />
                                                <asp:BoundField DataField="ExpTitleName" HeaderText="職稱" HeaderStyle-Width="10%"
                                                    SortExpression="ExpTitleName" />
                                                <asp:BoundField DataField="ExpStartEndDate" HeaderText="起訖日期" HeaderStyle-Width="15%"
                                                    SortExpression="ExpStartEndDate" />
                                                <asp:BoundField DataField="ExpQId" HeaderText="教師證書字號"
                                                    SortExpression="ExpQId" />
                                                <asp:TemplateField HeaderText="下載">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLinkTmuExp" CssClass="far fa-save" Text="下載" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:CommandField ShowDeleteButton="True" />--%>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSTeacherTmuExp" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                            DeleteCommand="DELETE FROM TeacherTmuExp WHERE ExpSn = @ExpSn"
                                            SelectCommand="
                                    SELECT [ExpSn],[EmpSn],b.unt_name_full AS ExpUnitName,c.tit_name AS ExpTitleName, d.pos_name AS ExpPosName,ExpQId,[ExpStartDate]+'~'+[ExpEndDate] AS ExpStartEndDate,[ExpUploadName] 
                                    FROM [TeacherTmuExp] AS a LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit] AS b ON a.ExpUnitId = b.unt_id  COLLATE Chinese_Taiwan_Stroke_CI_AS
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_titlecode] AS c ON a.ExpTitleId = c.tit_id  COLLATE Chinese_Taiwan_Stroke_CI_AS
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_position] AS d ON a.ExpPosId = d.pos_id  COLLATE Chinese_Taiwan_Stroke_CI_AS WHERE EmpSn = @EmpSn  ORDER BY [ExpStartDate]">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ExpSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                        <br />
                                        <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                                            CellPadding="2" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp"
                                            OnRowDataBound="GVTeachExp_RowDataBound" EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
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
                                                <%--<asp:BoundField DataField="ExpJobTitle" HeaderText="職稱"
                                                    SortExpression="ExpJobTitle" />
                                                <asp:BoundField DataField="ExpJobTypeName" HeaderText="職別"
                                                    SortExpression="ExpJobTypeName" />--%>
                                                <asp:TemplateField HeaderText="職稱/職別" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpJobTypeName" runat="server" Text='<%#Bind("ExpJobTypeName")%>' />
                                                        <asp:Label ID="ExpJobTitle" runat="server" Text='<%#Bind("ExpJobTitle")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ExpStartEndYM" HeaderText="起訖日期" HeaderStyle-Width="15%"
                                                    SortExpression="ExpStartEndYM" />
                                                <%--<asp:BoundField DataField="ExpUploadName" HeaderText="上傳檔案"
                                                    SortExpression="ExpUploadName" />--%>
                                                <asp:TemplateField HeaderText="附件" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpUploadName" runat="server" Text='<%#Bind("ExpUploadName")%>' />
                                                        <asp:HyperLink ID="HyperLinkExp" CssClass="far fa-save" Text="下載" runat="server"></asp:HyperLink>
                                                        <%--<i class="far fa-save"></i>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSTeacherExp" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                            DeleteCommand="DELETE FROM TeacherExp WHERE ExpSn = @ExpSn"
                                            SelectCommand="SELECT A.[ExpSn], A.[EmpSn], A.[ExpOrginization], A.[ExpUnit], A.[ExpJobTitle], A.[ExpStartYM]+'~'+A.[ExpEndYM] AS [ExpStartEndYM], A.[ExpUploadName] , B.JobAttrName AS ExpJobTypeName FROM [TeacherExp] AS A LEFT OUTER JOIN 
                                     CJobType AS B ON A.ExpJobType = B.JobAttrNo  Where EmpSn = @EmpSn  ORDER BY A.[ExpStartYM]">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ExpSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="TeachLessonTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                教師授課時數表：
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:Label runat="Server" ID="LbTeachLesson" Visible="false">無</asp:Label>
                                        <asp:GridView ID="GVTeachLesson" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CellPadding="2" DataKeyNames="LessonSn" DataSourceID="DSTeacherLesson" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" 
                                            ForeColor="Black" GridLines="None" 
                                            BorderColor="Tan" BorderWidth="1px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LessonSn" runat="server" Text='<%#Bind("LessonSn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SMTR" HeaderText="學年度"
                                                    SortExpression="SMTR" />
                                                <asp:BoundField DataField="LessonDeptLevel" HeaderText="系所級別"
                                                    SortExpression="LessonDeptLevel" />
                                                <asp:BoundField DataField="LessonClass" HeaderText="課目" SortExpression="LessonClass" />
                                                <asp:BoundField DataField="LessonHours" HeaderText="授課時數"
                                                    SortExpression="LessonHours" />
                                                <asp:BoundField DataField="LessonCreditHours" HeaderText="學分數"
                                                    SortExpression="LessonCreditHours" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSTeacherLesson" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:TmuConnectionString%>"
                                            SelectCommand="
                                    SELECT [LessonSn],[EmpSn],[LessonYear]+[LessonSemester] AS SMTR,[LessonDeptLevel],[LessonClass],[LessonHours],[LessonCreditHours],[LessonEvaluate] FROM [TeacherTmuLesson] WHERE EmpSn = @EmpSn Order by LessonYear+LessonSemester Desc">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                <asp:SessionParameter DefaultValue="0" Name="LessonYear" SessionField="sYear" />
                                                <asp:SessionParameter DefaultValue="0" Name="LessonSemester" SessionField="sSemester" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="LessonSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="TeachCaTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">教師證發放資料： 
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:Label runat="Server" ID="LbTeachCa" Visible="false">無</asp:Label>
                                        <asp:GridView ID="GVTeachCa" runat="server" AutoGenerateColumns="False"
                                            DataSourceID="DSTeacherCa" DataKeyNames="CaSn" EnableModelValidation="True" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" 
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="CaSn" HeaderText="CaSn"
                                                    SortExpression="CaSn" InsertVisible="False" ReadOnly="True"
                                                    Visible="False" />
                                                <asp:BoundField DataField="EmpSn" HeaderText="EmpSn"
                                                    SortExpression="EmpSn" Visible="False" />
                                                <%--<asp:BoundField DataField="CaNumberCN" HeaderText="教師字號"
                                                    SortExpression="CaNumberCN" />
                                                <asp:BoundField DataField="CaNumber" HeaderText="教師證號"
                                                    SortExpression="CaNumber" />--%>

                                                <asp:TemplateField HeaderText="教師證字號">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CaNumberCN" runat="server" Text='<%#Bind("CaNumberCN")%>' />字
                                                        第<asp:Label ID="CaNumber" runat="server" Text='<%#Bind("CaNumber")%>' />號
                                                        <%--<i class="far fa-save"></i>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CaPublishSchool" HeaderText="發證學校"
                                                    SortExpression="CaPublishSchool" />
                                                <asp:BoundField DataField="CaStartYM" HeaderText="起算年月" HeaderStyle-Width="10%"
                                                    SortExpression="CaStartYM" />
                                                <asp:BoundField DataField="CaEndYM" HeaderText="通過日期" HeaderStyle-Width="10%"
                                                    SortExpression="CaEndYM" />
                                            </Columns>
                                        </asp:GridView>

                                        <asp:SqlDataSource ID="DSTeacherCa" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                            SelectCommand="SELECT CaSn,EmpSn, CaNumberCN, CaNumber, CaPublishSchool, CaStartYM, CaEndYM FROM TeacherCa Where EmpSn = @EmpSn">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="1" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="TeachHonourTableRow" Visible="false">
                                    <asp:TableCell style="text-align:right">學術獎勵<br/>榮譽事項：
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:Label runat="Server" ID="LbTeachHonour" Visible="false">無</asp:Label>
                                        <asp:GridView ID="GVTeachHonour" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="HorSn" DataSourceID="DSTeachHonour" EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">

                                            <Columns>
                                                <asp:BoundField DataField="HorDescription" HeaderText="榮譽事項" HeaderStyle-BackColor="#0072e3"
                                                    HeaderStyle-ForeColor="White" SortExpression="HorDescription" />
                                                <asp:BoundField DataField="HorYear" HeaderText="日期(年)" HeaderStyle-BackColor="#0072e3"
                                                    HeaderStyle-ForeColor="White" SortExpression="HorYear" />
                                            </Columns>
                                            <FooterStyle BackColor="Tan" />
                                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue"
                                                HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSTeachHonour" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                            SelectCommand="SELECT HorYear, HorDescription, HorSn, EmpSn FROM TeacherHonour Where EmpSn = @EmpSn">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="1" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell Width="9%" style="text-align:right;color:red">個人研究重點與成果：
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                                        <asp:HyperLink ID="link_AppReasearchResult" CssClass="far fa-save"  runat="server"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="Server">
                                    <asp:TableCell Width="9%" style="text-align:right">教師上傳論文<br/>&積分：
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:GridView ID="GVThesisScore" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                                            DataKeyNames="ThesisSn" DataSourceID="DSThesisScore" OnRowDataBound="GVThesisScore_RowDataBound"
                                            EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
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
                                                <asp:TemplateField ItemStyle-Width="30" HeaderText="代表著作">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblThesisSign" runat="server" ForeColor="Red"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="EmpSn" HeaderText="EmpSn" SortExpression="EmpSn"
                                                    Visible="False" />

                                                <asp:BoundField DataField="SnNo" HeaderText="序號" SortExpression="SnNo" />
                                                <asp:BoundField DataField="RRNo" HeaderText="成果類別代碼" SortExpression="RRNo" HeaderStyle-Width="80px" />
                                                <asp:BoundField DataField="ThesisResearchResult" HeaderText="代表性研究成果名稱"
                                                    SortExpression="ThesisResearchResult" />
                                                <asp:BoundField DataField="ThesisPublishYearMonth" HeaderText="論文發表年月"
                                                    SortExpression="ThesisPublishYearMonth" />
                                                <asp:BoundField DataField="ThesisC" HeaderText="論文性質分數(C)" HeaderStyle-Width="80px"
                                                    SortExpression="ThesisC" />
                                                <asp:BoundField DataField="ThesisJ" HeaderText="刊登雜誌分類分數(J)" HeaderStyle-Width="80px"
                                                    SortExpression="ThesisJ" />
                                                <asp:BoundField DataField="ThesisA" HeaderText="作者排名分數(A)" HeaderStyle-Width="80px"
                                                    SortExpression="ThesisA" />
                                                <asp:BoundField DataField="ThesisTotal" HeaderText="總分數"
                                                    SortExpression="ThesisTotal" />
                                                <%--<asp:BoundField DataField="ThesisName" HeaderText="上傳檔名"
                                                    SortExpression="ThesisName" />--%>
                                                <asp:TemplateField HeaderText="ThesisUploadName">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="ThesisUploadName" runat="server" Text='<%#Bind("ThesisUploadName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TName">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="TName" runat="server" Text='<%#Bind("ThesisName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="附件" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ThesisName" runat="server" Text='<%#Bind("ThesisName")%>'></asp:Label>
                                                        <asp:HyperLink ID="HyperLinkThesis" CssClass="far fa-save" Text="下載" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSThesisScore" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                            DeleteCommand="DELETE FROM ThesisScore WHERE (ThesisSn = @ThesisSn)"
                                            SelectCommand="SELECT ThesisSn, SnNo, EmpSn, RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, ThesisUploadName, ThesisJournalRefCount, ThesisJournalRefUploadName, IsRepresentative, ThesisSummaryCN, ThesisCoAuthorUploadName, ThesisBuildDate, ThesisModifyDate FROM ThesisScore Where EmpSn = @EmpSn and (AppSn = @AppSn or  AppSn = '0') order by SnNo">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ThesisSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                        <br />
                                        <asp:GridView ID="GVThesisScoreCoAuthor" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                                            DataKeyNames="ThesisSn" DataSourceID="DSThesisScoreCoAuthor" OnRowDataBound="GVThesisScoreCoAuthor_RowDataBound"
                                            EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="SnNo" HeaderText="序號" SortExpression="SnNo" HeaderStyle-Width="5%" />
                                                <asp:BoundField DataField="ThesisName" HeaderText="論文名稱" HeaderStyle-Width="20%"
                                                    SortExpression="ThesisName" />
                                                <asp:BoundField DataField="ThesisJournalRefCount" HeaderText="期刊引用/排名" HeaderStyle-Width="15%" SortExpression="ThesisJournalRefCount" />
                                                <asp:TemplateField HeaderText="檢附資料下載" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_ThesisJournalRef" runat="server" Visible="false" />
                                                        <asp:HyperLink ID="HyperLinkThesisJournalRef" CssClass="far fa-save" Text="下載" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="合著人證明" HeaderStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_ThesisCo" runat="server" Visible="false" />
                                                        <asp:HyperLink ID="HyperLinkThesisCo" CssClass="far fa-save"  Text="下載" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ThesisSummaryCN" HeaderText="中文摘要" HeaderStyle-Width="61%"
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
                                            SelectCommand="SELECT ThesisSn, SnNo, EmpSn, RRNo, ThesisName, ThesisUploadName, ThesisJournalRefCount, ThesisJournalRefUploadName, IsRepresentative, ThesisSummaryCN, ThesisCoAuthorUploadName, ThesisBuildDate, ThesisModifyDate FROM ThesisScore Where EmpSn = @EmpSn and  (AppSn = @AppSn or  AppSn = '0') and ( IsRepresentative = 'True' or ThesisJournalRefUploadName <> '') order by SnNo ">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                                <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ThesisSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell style=" text-align:right">
                                    積分（小數兩位)：
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppPThesisAccuScore" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="TbRow_RPI">
                                    <asp:TableCell style=" text-align:right">
                                    研究表現指數(RPI)：
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppRPI" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="RPIDiscountTableRow" Visible="false">
                                    <asp:TableCell style=" text-align:right">
                                    教師優良表現<br />論文積分折抵：
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        獲獎折抵 總分：<asp:Label runat="server" ID="RPIDiscount"></asp:Label><br />
                                        <asp:Table ID="RPIDiscountTable" runat="server" class="table table-bordered table-condensed">
                                            <asp:TableRow runat="Server">
                                                <asp:TableCell>
                                                    1.師鐸獎：<asp:Label ID="RPIDiscountScore1" runat="server" Text=""></asp:Label>
                                                    &nbsp;&nbsp;&nbsp                                            
                                            <asp:HyperLink ID="RPIDiscountScore1HyperLink" runat="server" Visible="false"></asp:HyperLink><br></br>

                                                    2.教師優良教師：
                                            &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore2" runat="server" Enabled="false">
                                                <asp:ListItem Value="無"></asp:ListItem>
                                                <asp:ListItem Value="60">校級：60分</asp:ListItem>
                                                <asp:ListItem Value="30">院級：30分</asp:ListItem>
                                            </asp:DropDownList><br />
                                                    &nbsp;&nbsp;&nbsp;                                            
                                            <asp:HyperLink ID="RPIDiscountScore2HyperLink" runat="server" Visible="false"></asp:HyperLink><br>
                                                    3.優良導師：
                                            &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore3" runat="server" Enabled="false">
                                                <asp:ListItem Value="無"></asp:ListItem>
                                                <asp:ListItem Value="60">校級：60分</asp:ListItem>
                                                <asp:ListItem Value="30">院級：30分</asp:ListItem>
                                            </asp:DropDownList><br />
                                                    <asp:HyperLink ID="RPIDiscountScore3HyperLink" runat="server" Visible="false"></asp:HyperLink><br>
                                                    4.執行人體試驗：                                        
                                            &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore4" runat="server" Enabled="false">
                                                <asp:ListItem Value="無"></asp:ListItem>
                                                <asp:ListItem Value="60">研究者自行發起(Investigator initiated Trial)，且完成臨床試驗資料庫(Clinicaltrials.gov)登錄者：60分</asp:ListItem>
                                                <asp:ListItem Value="45">主持新醫療技術、新藥品人體試驗一期(Phase I)、新醫療器材人體試驗第三等級(Class 3)者：45分</asp:ListItem>
                                                <asp:ListItem Value="30">主持新藥品人體試驗二期(Phase II)、新醫療器材人體試驗第二等級(Class 2)者：30分</asp:ListItem>
                                                <asp:ListItem Value="15">主持新藥品人體試驗三期(Phase III)者：15分</asp:ListItem>
                                            </asp:DropDownList><br />
                                                    &nbsp;&nbsp;&nbsp;
                                            <asp:HyperLink ID="RPIDiscountScore4HyperLink" runat="server" Visible="false"></asp:HyperLink><br>
                                                     5.執行產學合作計畫： &nbsp;&nbsp;&nbsp;
                                                          <asp:GridView ID="GVThesisCoop" runat="server" AutoGenerateColumns="False"
                                                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" CssClass="table table-bordered table-condensed" DataKeyNames="ID"
                                                EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GVThesisCoop_RowDataBound"
                                                HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White" Wrap="True" ShowHeaderWhenEmpty="False" EmptyDataText="無資料">
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
                                                   
                                                </Columns><HeaderStyle BackColor="#0080FF" ForeColor="White" />
                                            </asp:GridView>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="Server">
                                    <asp:TableCell style=" text-align:right">
                                    總積分：
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        (1)論文總積分：<asp:Label ID="AppPThesisAccuScore1" runat="server" Text=""></asp:Label>&nbsp;+&nbsp;(2)折抵論文積分：<asp:Label ID="RPIDiscountTotal1" runat="server" Text=""></asp:Label>&nbsp;=&nbsp;<asp:Label ID="AppRPITotalScore" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="Server" ID="SelfTableRow" Visible="false" Enabled="false">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table ID="SelfTable" Width="100%" runat="server" 
                                             BorderColor="black" BorderWidth="1" class="table table-bordered table-condensed"
                                            GridLines="Both" CellPadding="0" CellSpacing="0">
                                            <asp:TableRow ID="TableRow" runat="Server" style=" text-align:right">
                                                <asp:TableCell Width="15%"> 教學經驗：<br />(包括曾擔任課程)</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfTeachExperience" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow5" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 研究經驗：</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfReach" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow6" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 發展潛力分析：</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfDevelope" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow7" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 專長評估：<br />(60字為限) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfSpecial" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow8" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 尚待加強部份：</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfImprove" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow9" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 對本校及本單位<br />預期貢獻：<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfContribute" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow10" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 與其他領域合作能力：<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfCooperate" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow11" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 研究及教學計劃：<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfTeachPlan" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow12" runat="Server" style=" text-align:right">
                                                <asp:TableCell> 個人生涯展望：<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="EmpSelfLifePlan" runat="server" TextMode="Multiline" Height="150px" class="form-control"></asp:TextBox>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow runat="Server">
                                    <asp:TableCell ColumnSpan="2">
                                <font color="red" style="font-size:20px">其他</font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table ID="Table1" Width="100%" runat="server" BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="0" CellSpacing="0">
                                            <asp:TableRow ID="ExpTableRow" runat="Server" Visible="false">
                                                <asp:TableCell style="text-align:right"> 經歷服務證明：</asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="經歷服務證明上傳">
                                                        <asp:CheckBox ID="ExpServiceCaUploadCB" runat="server" title="取消勾可刪除" Enabled="false" />
                                                        <asp:TextBox ID="ExpServiceCaUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox><asp:HyperLink ID="ExpServiceCaHyperLink" runat="server" Visible="false"></asp:HyperLink>

                                                        <asp:CustomValidator
                                                            ID="ExpServiceCaUploadCBRFV" runat="server" ClientValidationFunction="ExpServiceCaUploadCBV"
                                                            ErrorMessage="請上傳檔案"></asp:CustomValidator></span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppPPMTableRow" runat="Server" Visible="false" Width="100%">
                                                <asp:TableCell Width="19%" style=" text-align:right">
                                        研究計劃主持:
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="研究計劃主持上傳">
                                                        <asp:CheckBox ID="AppPPMUploadCB" runat="server" Enabled="false" />
                                                        <asp:TextBox ID="AppPPMUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox>
                                                        <asp:HyperLink ID="AppPPMHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    </span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppTeacherCaTableRow" runat="Server" Visible="false">
                                                <asp:TableCell style=" text-align:right" Width="15%">
                                        教育部教師資格證書影本：<br /> 	
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="教育部教師資格證書影本上傳">
                                                        <asp:CheckBox ID="AppTeacherCaUploadCB" runat="server" Enabled="false" />
                                                        <asp:TextBox ID="AppTeacherCaUploadFUName" runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppTeacherCaHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    </span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppDrCaTableRow" runat="Server" Visible="false">
                                                <asp:TableCell style=" text-align:right">
                                        醫師證書：<br /> 	
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="醫師證書上傳">
                                                        <asp:CheckBox ID="AppDrCaUploadCB" runat="server" Enabled="false" />
                                                        <asp:TextBox ID="AppDrCaUploadFUName" runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppDrCaHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    </span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="OtherTeachingTableRow" runat="Server" Visible="false">
                                                <asp:TableCell style=" text-align:right">
                                        教學成果：
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="教學成果上傳">
                                                        <asp:CheckBox ID="AppOtherTeachingUploadCB" runat="server" Enabled="false" />
                                                        <asp:TextBox ID="AppOtherTeachingUploadFUName" runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppOtherTeachingHyperLink" runat="server" Visible="false"></asp:HyperLink>
														<br/>(教學評議分數、教師成長活動...等，依各學院審查標準提供)
                                                    </span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="OtherServiceTableRow" runat="Server">
                                                <asp:TableCell style=" text-align:right">
                                        服務成果：	
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="服務成果上傳">
                                                        <asp:CheckBox ID="AppOtherServiceUploadCB" runat="server" Enabled="false" />
                                                        <asp:TextBox ID="AppOtherServiceUploadFUName" runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppOtherServiceHyperLink" runat="server" Visible="false"></asp:HyperLink>
														<br/>(校內外服務佐證資料...等，依各學院審查標準提供) 
                                                    </span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppDegreeThesisTableRow" runat="Server" Visible="false">
                                                <asp:TableCell style=" text-align:right">
                                        學位論文著作：
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="AppDegreeThesisName" runat="server"></asp:Label>&nbsp;
                                        <asp:Label ID="AppDegreeThesisNameEng" runat="server"></asp:Label>&nbsp;
                                        <asp:HyperLink ID="AppDegreeThesisHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppThesisOral" runat="Server" Visible="false">
                                                <asp:TableCell style=" text-align:right">
                                    論文指導教師及<br/>口試委員名：
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:GridView ID="GVThesisOral" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="ThesisOralSn" OnRowDataBound="GVThesisOral_RowDataBound"
                                                        EnableModelValidation="True" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                                                        CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                                <HeaderStyle CssClass="hiddencol" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="ThesisOralSn" runat="server" Text='<%#Bind("ThesisOralSn")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="hiddencol" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="學位送審別">
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
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppPublicationTableRow" runat="Server" Visible="false">
                                                <asp:TableCell> 著作出版刊物：<br /> 	
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:CheckBox ID="AppPublicationUploadCB" runat="server" title="取消勾可刪除" />
                                                    <asp:TextBox ID="AppPublicationUploadFUName" runat="server" ReadOnly="true"
                                                        Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="AppPublicationHyperLink" runat="server" Visible="false"></asp:HyperLink><asp:FileUpload ID="AppPublicationUploadFU" runat="server" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TeachingEvaluate" runat="Server" Visible="false">
                                    <asp:TableCell style="text-align:right">
                                <font color="red">教師評鑑資料：</font>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <font color="black">實際課堂每週時數：(不含導師、行政工作：已含臨床教學時數)</font>
                                        <asp:GridView ID="GVTeachEvaluate" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="school_year"
                                            EnableModelValidation="True" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White" 
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>

                                                <asp:TemplateField HeaderText="學期(學年度)" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="school_year" runat="server" Text='<%#Bind("school_year")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="cred_1smtr" HeaderText="上學期"
                                                    SortExpression="cred_1smtr" />
                                                <asp:BoundField DataField="cred_2smtr" HeaderText="下學期"
                                                    SortExpression="cred_2smtr" />
                                                <asp:BoundField DataField="cred_clinical" HeaderText="臨床教學時數"
                                                    SortExpression="cred_clinical" />
                                                <asp:BoundField DataField="subTotal" HeaderText="合計"
                                                    SortExpression="subTotal" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="ApplyComments" runat="Server" Visible="false">
                                    <asp:TableCell Width="19%">
                                    <font color="red" style="font-size:20px">其他審核評語意見</font>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        <asp:GridView ID="GVApplyComments" runat="server" AutoGenerateColumns="False"
                                            EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="ExecuteRoleName" HeaderText="審查角色" HeaderStyle-Width="10%"
                                                    SortExpression="ExecuteRoleName" />
                                                <asp:BoundField DataField="ExecuteAuditorName" HeaderText="審查者" HeaderStyle-Width="10%"
                                                    SortExpression="ExecuteAuditorName" />
                                                <asp:BoundField DataField="ExecuteCommentsA" HeaderText="評語"
                                                    SortExpression="ExecuteCommentsA" />
                                                <asp:BoundField DataField="ExecuteReturnReason" HeaderText="退回意見" HeaderStyle-Width="10%"
                                                    SortExpression="ExecuteReturnReason" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="PromoteComments" runat="Server" Visible="false">
                                    <asp:TableCell>
                                    <font color="red" style="font-size:20px">其他審核<br/>評語意見</font>
                                    </asp:TableCell>
                                    <asp:TableCell ColumnSpan="5">
                                        <asp:GridView ID="GVPromoteComments" runat="server" AutoGenerateColumns="False"
                                            EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="ExecuteRoleName" HeaderText="審查角色"
                                                    SortExpression="ExecuteRoleName" />
                                                <asp:BoundField DataField="ExecuteAuditorName" HeaderText="審查者"
                                                    SortExpression="ExecuteAuditorName" />
                                                <asp:BoundField DataField="ExecuteCommentsA" HeaderText="評語"
                                                    SortExpression="ExecuteCommentsA" />
                                                <asp:BoundField DataField="ExecuteTeachingScore" HeaderText="教學分數"
                                                    SortExpression="ExecuteTeachingScore" />
                                                <asp:BoundField DataField="ExecuteServiceScore" HeaderText="服務分數"
                                                    SortExpression="ExecuteServiceScore" />
                                                <asp:BoundField DataField="ExecuteReturnReason" HeaderText="退回原因"
                                                    SortExpression="ExecuteReturnReason" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="trQuestionnaire" runat="Server" Visible="false">
                                     <asp:TableCell>
                                    何招募平台知悉徵才訊息：
                                    </asp:TableCell>
                                    <asp:TableCell ID="tcQuestionnaire" ColumnSpan="5">
                                       
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBaseMessage" runat="server" Text="" ForeColor="red"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server">
                                    <asp:TableCell colspan="2">

                                        <asp:Label ID="MessageLabel" runat="server" Text="" ForeColor="Red"></asp:Label>

                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:View>
                        <asp:View ID="ViewAuditExecuting" runat="server">
                            <h2><asp:Label ID="NoAuthority" runat="server" Text="您目前無需審核此教師!" Visible="false" /></h2>
                            <asp:Table ID="TbPassExecute" runat="Server" BorderColor="black"
                                BorderWidth="1" GridLines="Both" HorizontalAlign="Center"
                                Style="font-weight: bold; text-align: center;" Width="85%">
                                <asp:TableRow ID="TableRow1" runat="Server">
                                    <asp:TableCell ID="TableCell1" runat="Server" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;"> 送審等級 </asp:TableCell>
                                    <asp:TableCell ID="TableCell2" runat="Server">
                                        <font color="blue">
                                            <asp:Label ID="PassAppJobTitle" runat="server" /></font>
                                    </asp:TableCell><asp:TableCell ID="TableCell3" runat="Server">姓名</asp:TableCell><asp:TableCell ID="TableCell4" runat="Server">
                                        <font color="blue">
                                            <asp:Label ID="PassEmpNameCN" runat="server" /></font>
                                    </asp:TableCell><asp:TableCell ID="TableCell5" runat="Server">學院<br/>系所</asp:TableCell><asp:TableCell ID="TableCell6" runat="Server">
                                        <font color="blue">
                                            <asp:Label ID="PassAppUnit" runat="server" /></font>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow2" runat="Server">
                                    <asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;"> 代表著作<br />名稱 </asp:TableCell><asp:TableCell ColumnSpan="5" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" HorizontalAlign="Left">
                                        <font color="blue">
                                            <asp:Label ID="PassAuditAppPublication"
                                                runat="server" /></font>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="OtherApplyComments" runat="Server" Visible="false">
                                    <asp:TableCell>
                                    <font color="red">其他審核評語意見</font>
                                    </asp:TableCell><asp:TableCell ColumnSpan="5">
                                        <asp:GridView ID="GVApplyOtherComments" runat="server" AutoGenerateColumns="False"
                                            EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="ExecuteRoleName" HeaderText="審查角色"
                                                    SortExpression="ExecuteRoleName" />
                                                <asp:BoundField DataField="ExecuteAuditorName" HeaderText="審查者"
                                                    SortExpression="ExecuteAuditorName" />
                                                <asp:BoundField DataField="ExecuteCommentsA" HeaderText="評語"
                                                    SortExpression="ExecuteCommentsA" />
                                                <asp:BoundField DataField="ExecuteReturnReason" HeaderText="退回原因"
                                                    SortExpression="ExecuteReturnReason" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="OtherPromoteComments" runat="Server" Visible="false">
                                    <asp:TableCell>
                                    <font color="red">其他審核評語意見</font>
                                    </asp:TableCell><asp:TableCell ColumnSpan="5">
                                        <asp:GridView ID="GVPromoteOtherComments" runat="server" AutoGenerateColumns="False"
                                            EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="ExecuteRoleName" HeaderText="審查角色"
                                                    SortExpression="ExecuteRoleName" />
                                                <asp:BoundField DataField="ExecuteAuditorName" HeaderText="審查者"
                                                    SortExpression="ExecuteAuditorName" />
                                                <asp:BoundField DataField="ExecuteCommentsA" HeaderText="評語"
                                                    SortExpression="ExecuteCommentsA" />
                                                <asp:BoundField DataField="ExecuteTeachingScore" HeaderText="教學分數"
                                                    SortExpression="ExecuteTeachingScore" />
                                                <asp:BoundField DataField="ExecuteServiceScore" HeaderText="服務分數"
                                                    SortExpression="ExecuteServiceScore" />
                                                <asp:BoundField DataField="ExecuteReturnReason" HeaderText="退回原因"
                                                    SortExpression="ExecuteReturnReason" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="RowPassExecuteCommentsA" runat="Server" Visible="false">
                                    <asp:TableCell ID="Step0" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" Visible="false"> 其他意見</asp:TableCell><asp:TableCell ID="Step1" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" Visible="false"> 主管評議</asp:TableCell><asp:TableCell ID="Step2" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" Visible="false"> 七人小組意見<br/>系教評決議</asp:TableCell><asp:TableCell ID="Step3" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" Visible="false"> 小組意見<br/>院教評決議</asp:TableCell><asp:TableCell ID="Step4" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" Visible="false"> 小組意見<br/>校教評決議</asp:TableCell><asp:TableCell ColumnSpan="5">
                                        <font color="blue">
                                            <asp:Label ID="Label1" runat="server" Text="" />
                                            <asp:TextBox ID="PassExecuteCommentsA" runat="server" Height="100px"
                                                Text="請輸入意見或決議"
                                                TextMode="Multiline" Width="95%"></asp:TextBox>
                                        </font>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="RowPassExecuteScore" runat="Server" Visible="false">
                                    <asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;"> 教學分數<br />服務分數</asp:TableCell><asp:TableCell ColumnSpan="5" HorizontalAlign="left" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                                        <font color="black">教學總評分：<asp:TextBox ID="TeachingScore" runat="server" MaxLength="5"></asp:TextBox>
                                            服務總評分：<asp:TextBox ID="ServiceScore" runat="server" MaxLength="5"></asp:TextBox>
                                        </font>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="PassRow" runat="Server">
                                    <asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">是否通過<br />(或推薦)</asp:TableCell><asp:TableCell ColumnSpan="5" HorizontalAlign="Left" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                                        <font color="blue">
                                            <asp:DropDownList ID="DDExecutePass" runat="server">
                                            </asp:DropDownList>
                                            &nbsp;
                                <asp:Label ID="LbReturnReason" runat="server" MaxLength="100"></asp:Label>
                                            <asp:TextBox ID="ReturnReason" runat="server" MaxLength="100"></asp:TextBox>
                                        </font>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="StartOutAudit" runat="Server" Visible="false">
                                    <asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">外審開始</asp:TableCell><asp:TableCell ColumnSpan="5" HorizontalAlign="Left" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                                        <font color="blue">
                                            <asp:DropDownList ID="DDExecutePass1" runat="server">
                                                <asp:ListItem Value="0">不啟動</asp:ListItem>
                                                <asp:ListItem Value="1">啟動</asp:ListItem>
                                            </asp:DropDownList>
                                        </font>
                                    </asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow4" runat="Server">
                                    <asp:TableCell ColumnSpan="6" Style="text-align: center;">
                                        <asp:Button ID="PassSubmit" OnClick="BtnAuditPass_Click" runat="server" Text="簽核送出" class="btn btn-primary" />
                                    </asp:TableCell></asp:TableRow></asp:Table><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /></asp:View><asp:View ID="ViewAuditorSetting" runat="server">
                            <asp:GridView ID="GVAuditExecute" runat="server" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                                AutoGenerateColumns="False" OnRowDataBound="GVAuditExecute_RowDataBound" EnableModelValidation="True" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorSn" runat="server" Text='<%# Bind("ExecuteAuditorSn")%>'></asp:TextBox></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ExecuteStage" HeaderText="簽核階段" ItemStyle-Width="100">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExecuteStageNum" HeaderText="簽核階段Num" ItemStyle-Width="100" Visible="false">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExecuteStepNum" HeaderText="簽核子階段" ItemStyle-Width="100" Visible="false">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ExecuteRoleName" HeaderText="簽核角色" ItemStyle-Width="100">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="簽核人員EmpId or Sn" ItemStyle-Width="100">
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorSnEmpId" runat="server" Text='<%# Bind("ExecuteAuditorSnEmpId")%>'></asp:TextBox></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="簽核人員" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorName" runat="server" Text='<%# Bind("ExecuteAuditorName")%>'></asp:TextBox></ItemTemplate><ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="簽核人Email" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorEmail" runat="server" Text='<%# Bind("ExecuteAuditorEmail")%>'></asp:TextBox></ItemTemplate><ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ExecuteStatus" HeaderText="審核狀態" ItemStyle-Width="100">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="審核日期" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:Label ID="EDate" runat="server" Text='<%# Bind("EDate")%>'></asp:Label></ItemTemplate><ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="EDate" HeaderText="審核日期" ItemStyle-Width="100" ><ItemStyle Width="100px" /></asp:BoundField>--%>
                                    <asp:BoundField DataField="EComments" HeaderText="審核評語" ItemStyle-Width="100" Visible="false">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="審核評語">
                                        <ItemTemplate>
                                            <asp:Label ID="lbComments" runat="server" Text=""></asp:Label><br /><asp:LinkButton ID="lkbComments" runat="server" PostBackUrl="">檢視</asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><HeaderStyle BackColor="#3AC0F2" ForeColor="White" />
                            </asp:GridView>
                            <br />
                            <%--<asp:Label ID="MessageAudit" runat="server" Text="" ForeColor="Red"></asp:Label>--%><br /><br /><br /><asp:Button ID="BtnUpdateAuditor" runat="server" Text="更新" OnClick="BtnUpdateAuditor_Click" />
                            <asp:Button ID="BtnStartAuditor" runat="server" Text="啟動簽呈" OnClick="BtnStartAuditor_Click" AutoPostBack="True" Visible="false" />
                            <asp:Button ID="ButnDeleteAuditor" runat="server" Text="刪除關卡" OnClick="ButnDeleteAuditor_Click" />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:View>

                    </asp:MultiView>


                    <%--<asp:Label ID="MessageLabelAll" runat="server" Text="" ForeColor="Red"></asp:Label>--%></div></fieldset>    </div><div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-66382736 #1600
            </label>
        </div>
    </form>
</body>
</html>