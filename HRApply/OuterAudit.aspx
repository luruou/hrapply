<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OuterAudit.aspx.cs" Inherits="ApplyPromote.OuterAudit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/jquery-1.11.1.min.js" type="text/javascript"></script>
<script src="js/jquery.validate.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function copyTextData(textBoxID) {
        if (document.getElementById(textBoxID).value == "") {
            document.getElementById(textBoxID).value = document.getElementById('<%=AuditExecuteCommentsA.ClientID%>').value;
        }
    }

    function ismaxlength(obj) {
        var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : ""
        if (obj.getAttribute && obj.value.length > mlength)
            obj.value = obj.value.substring(0, mlength)
    }

    function countWTotalScore(textTBWTotalScore) {
        var subject = parseFloat(document.getElementById('<%=TBWSSubject.ClientID%>').value, 10);
        var method = parseFloat(document.getElementById('<%=TBWSMethod.ClientID%>').value);
        var contribute = parseFloat(document.getElementById('<%=TBWSContribute.ClientID%>').value);
        var achievement = parseFloat(document.getElementById('<%=TBWSAchievement.ClientID%>').value);
        document.getElementById(textTBWTotalScore).value = subject + method + contribute + achievement;
        //1289行加總HIDDEN TBWTotalScoreHidden
        //var TBWTotalScoreHidden = document.getElementById("TBWTotalScore");
        //TBWTotalScoreHidden.value = document.getElementById(textTBWTotalScore);
        $("#<%=this.TBWTotalScoreHidden.ClientID%>").val($("#<%=this.TBWTotalScore.ClientID%>").val());
        //alert($("#<%=this.TBWTotalScoreHidden.ClientID%>").val());
    }

</script>
<style>
    img {
        image-rendering: -moz-crisp-edges;
        image-rendering: -o-crisp-edges;
        image-rendering: -webkit-optimize-contrast;
        image-rendering: crisp-edges;
        -ms-interpolation-mode: nearest-neighbor;
    }
</style>
<head id="Head1" runat="server">
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
    </style>
    <title></title>
</head>

<body>
    <form id="formApplyView" runat="server" enctype="multipart/form-data">
        <asp:Label ID="ExecuteSn" runat="server" Visible="false" />
        <%--標題和導覽列--%>
        <nav class="navbar navbar-expand-lg navbar-light bg-light" style="position: fixed; width: 100%;">
            <img src="image/Logo.png" width="130" style="float: left;" />
            <a class="navbar-brand" href="#" style="color: white; font-weight: bold; font-size: 23px">教師新聘升等系統</a>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
            </div>

            <ul class="nav navbar-nav float-xs-right">
                <%-- 放右上按鈕--%>
                <li class="nav-item"><a href="AuditLogin.aspx" runat="server" onclick="deleteCookie_Click" class="btn btn-large btn-danger">登出</a></li>
            </ul>
        </nav>
        <%--標題和導覽列end--%>
        <br />
        <br />
        <br />
        <asp:Label ID="AppAttributeNo" runat="server" Text="Label" Visible="false" />
        <div style="width: 80%; margin: auto">
            <asp:TextBox ID="TBEmpSn" runat="server" value="1" AutoPostBack="True" Visible="false"></asp:TextBox><br />
            <fieldset>
                <h2><b>檢視上傳資料與簽核資料</b></h2>
                <hr />
                <div style="font-size: 20px; background-color: #ffed97; border-radius: 10px; height: 80px;">
                    <div style="padding: 10px">
                        <asp:Label ID="OuterName" runat="server" Text="" /><asp:Label ID="OuterDate" runat="server" Text="" /><br />
                        <asp:Label ID="AuditWayName" runat="server" />─<asp:Label ID="AuditKindName" runat="server" />─<font color="red"><asp:Label ID="AuditAttributeName" runat="server" Text="Label" /></font>
                    </div>
                </div>
                <br />
                <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                    StaticSelectedStyle-CssClass="selectedTab" CssClass="tabs"
                    OnMenuItemClick="Menu1_MenuItemClick">
                    <Items>
                        <asp:MenuItem Text="申請者資料" Value="0" Selected="true" />
                        <asp:MenuItem Text="簽核" Value="1" />
                    </Items>
                </asp:Menu>

                <div class="tabContents" runat="server">

                    <asp:MultiView ID="MultiViewAudit" runat="server" ActiveViewIndex="0">
                        <asp:View ID="ViewTeachBase" runat="server">
                            <asp:HyperLink ID="ModifyEmpBase" runat="server" Visible="false"></asp:HyperLink>
                            <asp:Table ID="BaseTable" runat="server" class="table table-bordered table-condensed">
                                <asp:TableRow ID="TableRow1" runat="Server" Visible="false">
                                    <asp:TableCell Width="10%" Style="text-align: right">
                                    <font>身份証號:</font></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpIdno" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow2" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    <font >生日(民國):</font></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBirthDay" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow3" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    護照號碼:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpPassportNo" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow ID="TableRow4" runat="Server">
                                    <asp:TableCell Width="10%" Style="text-align: right">
                                    英文名:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpNameENFirst" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow ID="TableRow5" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                    英文姓:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpNameENLast" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow ID="TableRow6" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                    姓名:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpNameCN" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>

                                </asp:TableRow>
                                <asp:TableRow ID="TableRow7" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                    性別:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpSex" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow8" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    國籍:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpCountry" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpHomeTownTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    籍貫:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpHomeTown" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpBornProvinceTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    出生地:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBornProvince" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpBornCityTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    出生地-縣:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBornCity" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow9" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                    應徵單位:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppUnit" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow10" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                    應徵職稱:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppJobTitle" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow11" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                     職別:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppJobType" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow12" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                        <font>
                                            <asp:Label ID="AppKindName" runat="server" Text="Label"></asp:Label></font>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppAttributeName" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow13" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                    法規依據:</asp:TableCell>
                                    <asp:TableCell>
                                        <%--依教師聘任升等實施辦法第(四)條第 --%>依教師聘任升等實施辦法第(二)條第
                                    <asp:Label ID="ItemLabel" runat="server"></asp:Label>
                                        &nbsp;項第 
                                    <asp:Label ID="LawNumNoLabel" runat="server"></asp:Label>
                                        款送審。 
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow14" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    電話(宅):</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpTelPri" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow15" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    電話(公):</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpTelPub" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow16" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    E-Mail:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpEmail" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow17" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    戶籍地址:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpTownAddressCode" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="EmpTownAddress" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow18" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    通訊地址:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpAddressCode" runat="server" Text="Label"></asp:Label>
                                        <asp:Label ID="EmpAddress" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow19" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    手機:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpCell" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppENowJobOrgTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                   現任機關及職務:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppENowJobOrg" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppENoteTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    備註:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppENote" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppERecommendorsTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    推薦人姓名:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppERecommendors" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="AppERecommendYearTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    推薦日期 民國: </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppERecommendYear" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="RecommendUploadTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                   推薦書上傳:
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
                                    <asp:TableCell Style="text-align: right">
                                    <font >學術專長及研究:</font></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="EmpExpertResearch" runat="server" Text="Label"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpPhotoTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    照片上傳:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="EmpPhotoUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="EmpPhotoUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox>
                                        <asp:Image ID="EmpPhotoImage" runat="server" Visible="false" Height="100px" Width="80px" />

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpIdnoTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    身份證上傳:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="EmpIdnoUploadCB" runat="server" title="取消勾可刪除" />
                                        <asp:TextBox ID="EmpIdnoUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="EmpIdnoHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="EmpDegreeTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    最高學歷證件上傳:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="EmpDegreeUploadCB" runat="server" title="取消勾可刪除" />
                                        <asp:TextBox ID="EmpDegreeUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text=""></asp:TextBox><asp:HyperLink ID="EmpDegreeHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow20" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    教師資格審查資料切結書:</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="AppDeclarationUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="AppDeclarationUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox>
                                        <asp:HyperLink ID="AppDeclarationHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="TableRow45" runat="Server" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    <font color="red">論文積分表:</font></asp:TableCell>
                                    <asp:TableCell>
                                        <asp:CheckBox ID="ThesisScoreUploadCB" runat="server" Enabled="false" />
                                        <asp:TextBox ID="ThesisScoreUploadFUName" runat="server" ReadOnly="true"
                                            Style="display: none;" Text="" MaxLength="100"></asp:TextBox>
                                        <asp:HyperLink ID="ThesisScoreUploadHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="TableRow21" runat="Server">
                                    <asp:TableCell Style="text-align: right">教師學歷資料:
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:GridView ID="GVTeachEdu" runat="server" AutoGenerateColumns="False"
                                            CellPadding="2" DataKeyNames="EduSn" DataSourceID="DSTeacherEdu" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White"
                                            OnRowDataBound="GVTeachEdu_RowDataBound" EnableModelValidation="True" CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
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

                                                <asp:TemplateField HeaderText="修業期間(起迄)" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_EduStartYM" runat="server" Text='<%#Bind("EduStartYM")%>'></asp:Label>~
                                                        <asp:Label ID="lb_EduEndYM" runat="server" Text='<%#Bind("EduEndYM")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--                                                <asp:BoundField DataField="EduStartYM" HeaderText="修業期間(起"
                                                    SortExpression="EduStartYM" />
                                                <asp:BoundField DataField="EduEndYM" HeaderText=" ~迄)"
                                                    SortExpression="EduEndYM" />--%>
                                                <%--                                                <asp:BoundField DataField="EduDegree" HeaderText="學位"
                                                    SortExpression="EduDegree" />
                                                <asp:BoundField DataField="EduDegreeType" HeaderText="修業別"
                                                    SortExpression="EduDegreeType" />--%>

                                                <asp:TemplateField HeaderText="學位(修業別)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="EduDegree" runat="server" Text='<%#Bind("EduDegree")%>' />(<asp:Label ID="EduDegreeType" runat="server" Text='<%#Eval("EduDegreeType").ToString().Substring(0,1)%>'></asp:Label>)
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                                <asp:SessionParameter DefaultValue="1" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <br />
                                        <asp:Table ID="AboutFgn" Width="100%" runat="server" Visible="false">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    國外學歷(<a href="http://hr1.tmu.edu.tw/File/upgrade/upgrade3/辦理驗證程序-持國外學歷送審教師資格應先辦理驗證程序.doc">持國外學歷需辦理驗證項目</a>)<br />
                                                    <asp:CheckBox ID="AppDFgnEduDeptSchoolAdmitCB" runat="server" Enabled="false" />
                                                    <font>1.是否為<a href="javascript:window.open('http://fsedu.cloud.ncnu.edu.tw/home.aspx')">教育部國際及兩岸教育司</a>編印之冊列學校</font><br />
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
                                                    4.<a href="http://hr1.tmu.edu.tw/File/upgrade/upgrade3/國外學歷送審教師資格修業情形一覽.doc">國外學歷修業情形一覽表</a>（修業期限：累計在當地學校修業時間： 碩士學位須滿8個月；博士學位須滿16個月；連續修讀碩、博士學位須滿24個月）。
                                    <asp:TextBox ID="AppDFgnSelectCourseUploadFUName" runat="server" ReadOnly="true"
                                        Style="display: none;" Text=""></asp:TextBox>
                                                    <asp:HyperLink ID="AppDFgnSelectCourseHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                    <br />
                                                    <asp:CheckBox ID="AppDFgnEDRecordUploadCB" runat="server" Enabled="false" />
                                                    5.<a href="http://hr1.tmu.edu.tw/File/upgrade/upgrade3/入出境證明-申請表及相關事項.pdf">個人出入境紀錄</a>，可至<a href="http://www.immigration.gov.tw/aspcode/show_menu22.asp?url_disno=76">內政部入出國及移民署網站</a>查詢。
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
                                    <asp:TableCell Style="text-align: right">教師經歷資料:
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:Label runat="Server" ID="LbTeachExp" Visible="false">無</asp:Label>
                                        <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False"
                                            CellPadding="2" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp"
                                            OnRowDataBound="GVTeachExp_RowDataBound" EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None" Visible="false" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">
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
                                                <asp:BoundField DataField="ExpJobTitle" HeaderText="職稱"
                                                    SortExpression="ExpJobTitle" />
                                                <%--<asp:BoundField DataField="ExpStartYM" HeaderText="起訖日期(起"
                                                    SortExpression="ExpStartYM" />
                                                <asp:BoundField DataField="ExpEndYM" HeaderText=" ~迄)"
                                                    SortExpression="ExpEndYM" />--%>
                                                <asp:TemplateField HeaderText="起訖日期">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpStartYM" runat="server" Text='<%#Bind("ExpStartYM")%>' />
                                                        <asp:Label ID="ExpEndYM" runat="server" Text='<%#Bind("ExpEndYM")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="ExpUploadName" HeaderText="上傳檔案"
                                                    SortExpression="ExpUploadName" />--%>
                                                <asp:TemplateField HeaderText="下載">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpUploadName" runat="server" Text='<%#Bind("ExpUploadName")%>' />
                                                        <asp:HyperLink ID="HyperLinkExp" CssClass="far fa-save" Text="下載" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                        <asp:GridView ID="GVTeachTmuExp" runat="server" AutoGenerateColumns="False"
                                            CellPadding="2" DataKeyNames="ExpStartDate" DataSourceID="DSTeacherTmuExp" OnRowDataBound="GVTeachTmuExp_RowDataBound" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White"
                                            EnableModelValidation="True" CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpStartDate" runat="server" Text='<%#Bind("ExpStartDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ExpPosName" HeaderText="經歷"
                                                    SortExpression="ExpPosName" />
                                                <asp:BoundField DataField="ExpUnitName" HeaderText="單位" SortExpression="ExpUnitName" />
                                                <asp:BoundField DataField="ExpTitleName" HeaderText="職稱"
                                                    SortExpression="ExpTitleName" />
                                                <asp:BoundField DataField="ExpStartEndDate" HeaderText="起訖日期"
                                                    SortExpression="ExpStartEndDate" />
                                                <asp:BoundField DataField="ExpQid" HeaderText="教師證書字號"
                                                    SortExpression="ExpQid" />
                                                <%--<asp:BoundField DataField="ExpUploadName" HeaderText="上傳檔案"
                                                    SortExpression="ExpUploadName" />--%>
                                                <asp:TemplateField HeaderText="上傳檔案">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ExpUploadName" runat="server" Text='<%#Bind("ExpUploadName")%>' />
                                                        <asp:HyperLink ID="HyperLinkTmuExp" CssClass="far fa-save" Text="下載" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSTeacherTmuExp" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:TmuConnectionString %>"
                                            SelectCommand="
                                    SELECT [ExpSn],[EmpSn],b.unt_name_full AS ExpUnitName,c.tit_name AS ExpTitleName, d.pos_name AS ExpPosName,ExpQid,[ExpStartDate]+'~'+[ExpEndDate] AS ExpStartEndDate,[ExpUploadName] ,ExpStartDate,ExpEndDate
                                    FROM [TeacherTmuExp] AS a LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit] AS b ON a.ExpUnitId = b.unt_id  COLLATE Chinese_Taiwan_Stroke_CI_AS
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_titlecode] AS c ON a.ExpTitleId = c.tit_id  COLLATE Chinese_Taiwan_Stroke_CI_AS
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_position] AS d ON a.ExpPosId = d.pos_id  COLLATE Chinese_Taiwan_Stroke_CI_AS WHERE EmpSn = @EmpSn   ORDER BY a.[ExpStartDate]">

                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="2" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ExpSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="TeachLessonTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                教師授課時數表:
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:Label runat="Server" ID="LbTeachLesson" Visible="false">無</asp:Label>
                                        <asp:GridView ID="GVTeachLesson" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CellPadding="2" DataKeyNames="LessonSn" DataSourceID="DSTeacherLesson"
                                            ForeColor="Black" GridLines="None"
                                            BorderColor="Tan" BorderWidth="1px" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LessonSn" runat="server" Text='<%#Bind("LessonSn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LessonDeptLevel" HeaderText="系所級別"
                                                    SortExpression="LessonDeptLevel" />
                                                <asp:BoundField DataField="LessonClass" HeaderText="課目" SortExpression="LessonClass" />
                                                <asp:BoundField DataField="LessonHours" HeaderText="時數"
                                                    SortExpression="LessonHours" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSTeacherLesson" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:TmuConnectionString%>"
                                            SelectCommand="
                                    SELECT [LessonSn],[EmpSn],[LessonYear],[LessonSemester],[LessonDeptLevel],[LessonClass],[LessonHours] FROM [TeacherTmuLesson] WHERE EmpSn = @EmpSn  ">
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
                                    <asp:TableCell Style="text-align: right">教師證發放資料:
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
                                                <asp:BoundField DataField="CaNumberCN" HeaderText="教師字號"
                                                    SortExpression="CaNumberCN" />
                                                <asp:BoundField DataField="CaNumber" HeaderText="教師證號"
                                                    SortExpression="CaNumber" />
                                                <asp:BoundField DataField="CaPublishSchool" HeaderText="發證學校"
                                                    SortExpression="CaPublishSchool" />
                                                <asp:BoundField DataField="CaStartYM" HeaderText="起算年月"
                                                    SortExpression="CaStartYM" />
                                                <asp:BoundField DataField="CaEndYM" HeaderText="通過日期"
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
                                    <asp:TableCell Style="text-align: right">學術獎勵、榮譽事項 :
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:Label runat="Server" ID="LbTeachHonour" Visible="false">無</asp:Label>
                                        <asp:GridView ID="GVTeachHonour" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="HorSn" DataSourceID="DSTeachHonour" EnableModelValidation="True"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White">

                                            <Columns>
                                                <asp:BoundField DataField="HorDescription" HeaderText="榮譽事項"
                                                    SortExpression="HorDescription" />
                                                <asp:BoundField DataField="HorYear" HeaderText="日期" SortExpression="HorYear" />
                                            </Columns>
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
                                    <asp:TableCell Width="9%" Style="text-align: right; color: red">個人研究重點與成果：
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:HyperLink ID="link_AppReasearchResult" CssClass="far fa-save" runat="server"></asp:HyperLink>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow22" runat="Server">
                                    <asp:TableCell Width="9%" Style="text-align: right">教師上傳論文&積分:
                                    </asp:TableCell>
                                    <asp:TableCell Style="text-align: left">
                                        <asp:GridView ID="GVThesisScore" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="ThesisSn" DataSourceID="DSThesisScore" OnRowDataBound="GVThesisScore_RowDataBound"
                                            EnableModelValidation="True" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White"
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
                                                        <asp:Label ID="AppSn" runat="server" Text='<%#Bind("AppSn")%>'></asp:Label>
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
                                                <asp:BoundField DataField="ThesisResearchResult" HeaderText="代表性研究成果名稱<br/>作者本人「+」，通訊作「*」，貢獻相同作者「#」"
                                                    SortExpression="ThesisResearchResult" HtmlEncode="False" />
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
                                                <asp:BoundField DataField="ThesisName" HeaderText="上傳檔名"
                                                    SortExpression="ThesisName" />
                                                <asp:BoundField DataField="ThesisJournalRefCount" HeaderText="期刊引用/排名"
                                                    SortExpression="ThesisJournalRefCount" />
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
                                                <asp:TemplateField HeaderText="下載">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLinkThesis" CssClass="far fa-save" Text="下載" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSThesisScore" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                            DeleteCommand="DELETE FROM ThesisScore WHERE (ThesisSn = @ThesisSn)"
                                            SelectCommand="SELECT ThesisSn, b.AppSn, a.SnNo, a.EmpSn, RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, ThesisUploadName, ThesisJournalRefCount,IsRepresentative, ThesisSummaryCN, ThesisCoAuthorUploadName, ThesisBuildDate, ThesisModifyDate	 FROM ThesisScore a LEFT JOIN ApplyAudit b  ON b.EmpSn = a.EmpSn and a.AppSn= b.AppSn Where a.AppSn  =@AppSn  ORDER BY SnNo">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                                <asp:SessionParameter DefaultValue="105" Name="AppYear" SessionField="AppYear" />
                                                <asp:SessionParameter DefaultValue="2" Name="AppSemester" SessionField="AppSemester" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ThesisSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>
                                        <br />
                                        <asp:GridView ID="GVThesisScoreCoAuthor" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="ThesisSn" DataSourceID="DSThesisScoreCoAuthor" OnRowDataBound="GVThesisScoreCoAuthor_RowDataBound"
                                            EnableModelValidation="True" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White"
                                            CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="None">
                                            <Columns>

                                                <asp:BoundField DataField="ThesisName" HeaderText="研究論文中「代表著作」"
                                                    SortExpression="ThesisName" />
                                                <asp:BoundField DataField="ThesisSummaryCN" HeaderText="中文摘要"
                                                    SortExpression="ThesisSummaryCN" />
                                                <%--<asp:BoundField DataField="ThesisCoAuthorUploadName" HeaderText="合著檔案"
                                                    SortExpression="ThesisCoAuthorUploadName" />--%>
                                                <asp:TemplateField HeaderText="合著人證明" HeaderStyle-Width="6%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lb_ThesisCoAuthorUploadName" Text='<%#Bind("ThesisCoAuthorUploadName")%>' runat="server"></asp:Label>
                                                        <asp:HyperLink ID="HyperLinkThesis" CssClass="far fa-save" Text="下載" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ThesisCoAuthorUploadName">
                                                    <HeaderStyle CssClass="hiddencol" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="ThesisCoAuthorUploadName" runat="server" Text='<%#Bind("ThesisCoAuthorUploadName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="hiddencol" />
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="DSThesisScoreCoAuthor" runat="server"
                                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                            DeleteCommand="DELETE FROM ThesisScore WHERE (ThesisSn = @ThesisSn)"
                                            SelectCommand="SELECT ThesisSn,  EmpSn, RRNo, ThesisName, ThesisUploadName, ThesisJournalRefCount,IsRepresentative, ThesisSummaryCN, ThesisCoAuthorUploadName, ThesisBuildDate, ThesisModifyDate FROM ThesisScore Where EmpSn = @EmpSn and AppSn = @AppSn and IsRepresentative = 'True' ">
                                            <SelectParameters>
                                                <asp:SessionParameter DefaultValue="0" Name="AppSn" SessionField="AppSn" />
                                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="ThesisSn" />
                                            </DeleteParameters>
                                        </asp:SqlDataSource>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow23" runat="Server">
                                    <asp:TableCell Style="text-align: right">
                                    積分（小數兩位):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppPThesisAccuScore" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="TbRow_RPI">
                                    <asp:TableCell Style="text-align: right">
                                    研究表現指數(RPI):
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label ID="AppRPI" runat="server" Text=""></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="RPIDiscountTableRow" Visible="false">
                                    <asp:TableCell Style="text-align: right">
                                    教師優良表現<br />論文積分折抵:
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Table ID="RPIDiscountTable" Width="100%" runat="server" class="table table-bordered table-condensed">
                                            <asp:TableRow ID="TableRow25" runat="Server">
                                                <asp:TableCell>
                                                    獲獎折抵 總分：<asp:Label ID="RPIDiscount" runat="server"></asp:Label><br />
                                                    1.<asp:CheckBox ID="RPIDiscountScore1" runat="server" title="師鐸獎：60分" Enabled="false" />師鐸獎：60分
                                            &nbsp;&nbsp;&nbsp                                            
                                            <asp:HyperLink ID="RPIDiscountScore1HyperLink" runat="server" Visible="false"></asp:HyperLink><br />
                                                    <br />

                                                    2.教師優良教師：<br />
                                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore2" runat="server" Enabled="false">
                                                        <asp:ListItem Value="請選擇"></asp:ListItem>
                                                        <asp:ListItem Value="60">校級：60分</asp:ListItem>
                                                        <asp:ListItem Value="30">院級：30分</asp:ListItem>
                                                    </asp:DropDownList><br />
                                                    &nbsp;&nbsp;&nbsp;                                            
                                            <asp:HyperLink ID="RPIDiscountScore2HyperLink" runat="server" Visible="false"></asp:HyperLink><br></br>

                                                    3.優良導師：<br />
                                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore3" runat="server" Enabled="false">
                                                        <asp:ListItem Value="請選擇"></asp:ListItem>
                                                        <asp:ListItem Value="60">校級：60分</asp:ListItem>
                                                        <asp:ListItem Value="30">院級：30分</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:HyperLink ID="RPIDiscountScore3HyperLink" runat="server" Visible="false"></asp:HyperLink><br></br>
                                                    4.執行人體試驗<br />
                                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="RPIDiscountScore4" runat="server" Enabled="false">
                                                        <asp:ListItem Value="請選擇"></asp:ListItem>
                                                        <asp:ListItem Value="60">研究者自行發起(Investigator initiated Trial)，且完成臨床試驗資料庫(Clinicaltrials.gov)登錄者：60分</asp:ListItem>
                                                        <asp:ListItem Value="45">主持新醫療技術、新藥品人體試驗一期(Phase I)、新醫療器材人體試驗第三等級(Class 3)者：45分</asp:ListItem>
                                                        <asp:ListItem Value="30">主持新藥品人體試驗二期(Phase II)、新醫療器材人體試驗第二等級(Class 2)者：30分</asp:ListItem>
                                                        <asp:ListItem Value="15">主持新藥品人體試驗三期(Phase III)者：15分</asp:ListItem>
                                                    </asp:DropDownList><br />
                                                    &nbsp;&nbsp;&nbsp;
                                            <asp:HyperLink ID="RPIDiscountScore4HyperLink" runat="server" Visible="false"></asp:HyperLink><br/>
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
                                <asp:TableRow runat="Server" ID="SelfTableRow" Visible="false">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table ID="SelfTable" Width="70%" runat="server" Style="background-color: #ffffcc;" BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="0" CellSpacing="0">
                                            <asp:TableRow ID="TableRow" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 教學經驗:<br />(包括曾擔任課程) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfTeachExperience" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow26" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 研究經驗: </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfReach" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow27" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 發展潛力分析: </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfDevelope" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow28" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 專長評估:<br />(60字為限) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfSpecial" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow29" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 尚待加強部份: </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfImprove" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow30" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 對本校及本單位預期貢獻:<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfContribute" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow31" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 與其他領域合作能力:<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfCooperate" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow32" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 研究及教學計劃:<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfTeachPlan" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="TableRow33" runat="Server">
                                                <asp:TableCell Style="text-align: right"> 個人生涯展望:<br />(請詳細填寫) </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="EmpSelfLifePlan" runat="server" TextMode="Multiline" Height="100px" Width="400px"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>

                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow ID="TableRow34" runat="Server">
                                    <asp:TableCell ColumnSpan="2">
                                <font color="red">其他</font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow35" runat="Server">
                                    <asp:TableCell ColumnSpan="2">
                                        <asp:Table ID="Table1" Width="100%" runat="server" BorderColor="black" BorderWidth="1" GridLines="Both" CellPadding="0" CellSpacing="0">
                                            <asp:TableRow ID="ExpTableRow" runat="Server" Visible="false">
                                                <asp:TableCell Width="10%" Style="text-align: right"> 經歷服務證明: </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="經歷服務證明上傳">
                                                        <asp:CheckBox ID="ExpServiceCaUploadCB" runat="server" title="取消勾可刪除" Enabled="false" />
                                                        <asp:TextBox ID="ExpServiceCaUploadFUName" runat="server" Text="" ReadOnly="true" Style="display: none;"></asp:TextBox><asp:HyperLink ID="ExpServiceCaHyperLink" runat="server" Visible="false"></asp:HyperLink>

                                                        <asp:CustomValidator
                                                            ID="ExpServiceCaUploadCBRFV" runat="server" ClientValidationFunction="ExpServiceCaUploadCBV"
                                                            ErrorMessage="請上傳檔案"></asp:CustomValidator></span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppPPMTableRow" runat="Server" Visible="false">
                                                <asp:TableCell Style="text-align: right">
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
                                                <asp:TableCell Style="text-align: right">
                                        教育部教師資格證書影本:<br /> 	
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
                                                <asp:TableCell Style="text-align: right">
                                        醫師證書:<br /> 	
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
                                                <asp:TableCell Style="text-align: right">
                                        教學成果:
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="教學成果上傳">
                                                        <asp:CheckBox ID="AppOtherTeachingUploadCB" runat="server" Enabled="false" />
                                                        <asp:TextBox ID="AppOtherTeachingUploadFUName" runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppOtherTeachingHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                        <br />
                                                        (教學評議分數、教師成長活動...等，依各學院審查標準提供)
                                                    </span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="OtherServiceTableRow" runat="Server" Visible="false">
                                                <asp:TableCell Style="text-align: right">
                                        服務成果:	
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <span title="服務成果上傳">
                                                        <asp:CheckBox ID="AppOtherServiceUploadCB" runat="server" Enabled="false" />
                                                        <asp:TextBox ID="AppOtherServiceUploadFUName" runat="server" ReadOnly="true"
                                                            Style="display: none;" Text=""></asp:TextBox>
                                                        <asp:HyperLink ID="AppOtherServiceHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                        <br />
                                                        (校內外服務佐證資料...等，依各學院審查標準提供) 
                                                    </span>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppDegreeThesisTableRow" runat="Server" Visible="false">
                                                <asp:TableCell Style="text-align: right"> <font color="red">
                                        學位論文著作:</font>
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="AppDegreeThesisName" runat="server"></asp:Label>
                                                    <asp:Label ID="AppDegreeThesisNameEng" runat="server"></asp:Label>
                                                    <asp:HyperLink ID="AppDegreeThesisHyperLink" runat="server" Visible="false"></asp:HyperLink>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow ID="AppThesisOral" runat="Server" Visible="false">
                                                <asp:TableCell Style="text-align: right">
                                    論文指導教師及<br/>口試委員名:
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:GridView ID="GVThesisOral" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="ThesisOralSn" OnRowDataBound="GVThesisOral_RowDataBound"
                                                        EnableModelValidation="True" HeaderStyle-BackColor="#0080ff" HeaderStyle-ForeColor="White"
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
                                                <asp:TableCell Style="text-align: right"> 著作出版刊物:<br /> 	
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
                                <asp:TableRow ID="TableRow36" runat="Server">
                                    <asp:TableCell>
                                        <asp:Label ID="EmpBaseMessage" runat="server" Text="" ForeColor="red"></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow37" runat="Server">
                                    <asp:TableCell colspan="2">

                                        <asp:Label ID="MessageLabel" runat="server" Text="" ForeColor="Red"></asp:Label>

                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:View>
                        <asp:View ID="ViewAuditExecuting" runat="server">
                            <asp:Label ID="NoAuthority" runat="server" Text="您目前無需審核此教師!" Visible="false" />
                            <asp:Table ID="TableAuditExecute" runat="Server" BorderColor="black"
                                BorderWidth="1" GridLines="Both" HorizontalAlign="Center"
                                Style="font-weight: bold;" Width="85%">

                                <asp:TableRow ID="TableRow42" runat="Server" Style="text-align: center;">
                                    <asp:TableCell ID="TableCell3" runat="Server" Font-Size="X-Large" ForeColor="Red" ColumnSpan="6">甲表 </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow38" runat="Server" Style="text-align: center;">
                                    <asp:TableCell ID="TableCell1" runat="Server"> 送審等級 </asp:TableCell>
                                    <asp:TableCell>
                                        <font color="blue">
                                            <asp:Label ID="AuditAppJobTitle" runat="server"
                                                Text="" /></font>
                                    </asp:TableCell><asp:TableCell>姓名</asp:TableCell><asp:TableCell>
                                        <font color="blue">
                                            <asp:Label ID="AuditEmpNameCN" runat="server"
                                                Text="" /></font>
                                    </asp:TableCell><asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">學院<br/>系所</asp:TableCell><asp:TableCell>
                                        <font color="blue">
                                            <asp:Label ID="AuditAppUnitLevel1" runat="server" Text="" /><br />
                                            <asp:Label ID="AuditAppUnit" runat="server" Text="" />
                                        </font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow39" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;"> 代表著作名稱 </asp:TableCell><asp:TableCell ColumnSpan="5" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" HorizontalAlign="Left">
                                        <font color="blue">
                                            <asp:Label ID="AuditAppPublication" runat="server" Text="" /></font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow44" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;"> 學位論文名稱 </asp:TableCell><asp:TableCell ColumnSpan="5" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" HorizontalAlign="Left">
                                        <font color="blue">
                                            <asp:Label ID="AuditAppPublication2" runat="server" Text="" /></font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow40" runat="Server">
                                    <asp:TableCell ColumnSpan="6" Style="padding-top: 5px; padding-left: 5px;" Width="100%" runat="server">
                                        <div style="text-align: center;">
                                            <asp:Label runat="server" ID="lb_tittle" Text="表(甲)審查意見(僅提供本校評審用，意見以一百至三百字為原則，字數上限為500字)"></asp:Label>
                                        </div>
                                        <br />
                                        <asp:Label runat="server" ID="AuditExecuteMemo" Style="text-align: left" />
                                        <font color="blue" style="text-align: center;">
                                            <asp:TextBox ID="AuditExecuteCommentsA" TextMode="MultiLine" MaxLength="500" runat="server" Height="300px" Text="請輸入意見或決議" Width="95%"></asp:TextBox>
                                        </font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="FiveLevelDiscriptionWayNo1a" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="4">代表著作評分項目及標準</asp:TableCell><asp:TableCell ColumnSpan="2" RowSpan="2" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                                    五年內或前一等級
                                    至本次申請等級時<br/>
                                    個人學術與專業之<br/>
                                    整體成就<br/>
                                    教    授50%<br/>
                                    副 教 授40%<br/>
                                    助理教授30%<br/>
                                    講　　師20%<br/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="FiveLevelDiscriptionWayNo1b" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell>項   目</asp:TableCell><asp:TableCell>
	                                研 究 主 題 <br/> 
                                  教    授 5%<br/>
                                  副 教 授10%<br/>
                                  助理教授20%<br/>
                                  講　　師25%<br/>
                                    </asp:TableCell><asp:TableCell>
	                                研究方法及能力 <br/> 
                                  教    授10%<br/>
                                  副 教 授20%<br/>
                                  助理教授25%<br/>
                                  講　　師30%<br/>
                                    </asp:TableCell><asp:TableCell>
	                                學術及實務貢獻 <br/>
                                  教    授35%<br/>
                                  副 教 授30%<br/>
                                  助理教授25%<br/>
                                  講　　師25%<br/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="FiveLevelDiscriptionWayNo2a" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="4">代表著作(成果)評分項目及標準</asp:TableCell><asp:TableCell ColumnSpan="2" RowSpan="2"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                                    五年內或前一等級
                                    至本次申請等級時<br/>
                                    個人教學實務
                                    整體成效<br/><br/>
                                    教    授30%<br/>
                                    副 教 授40%<br/>
                                    助理教授50%<br/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="FiveLevelDiscriptionWayNo2b" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell>項   目</asp:TableCell><asp:TableCell>
                                  教 學 主 題 <br/> 
                                  教    授 20%<br/>
                                  副 教 授 10%<br/>
                                  助理教授 5%<br/>
                                    </asp:TableCell><asp:TableCell>
	                              教學方法及能力 <br/> 
                                  教    授25%<br/>
                                  副 教 授20%<br/>
                                  助理教授10%<br/>
                                    </asp:TableCell><asp:TableCell>
	                              教學及實務貢獻 <br/>
                                  教    授25%<br/>
                                  副 教 授30%<br/>
                                  助理教授35%<br/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="FiveLevelDiscriptionWayNo3a" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="4">代表著作(成果)評分項目及標準</asp:TableCell><asp:TableCell ColumnSpan="2" RowSpan="2"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
	                            五年內或前一等級<br/>
	                            至本次申請等級時<br/>
	                            技術產出<br/>
                                    教    授50%<br/>
                                    副 教 授50%<br/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="FiveLevelDiscriptionWayNo3b" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell>項   目</asp:TableCell><asp:TableCell>
                                  研發理念與<br/>學理基礎 <br/> 
                                  教    授 10%<br/>
                                  副 教 授 10%<br/>
                                    </asp:TableCell><asp:TableCell>
	                          主題內容與<br/>方法技巧 <br/> 
                                  教    授 10%<br/>
                                  副 教 授 10%<br/>
                                    </asp:TableCell><asp:TableCell>
	                          成果貢獻 <br/>
                                  教    授 30%<br/>
                                  副 教 授 30%<br/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <%--<asp:TableRow ID="FivelLevelInputData" Runat="Server" Visible="false">
                                <asp:TableCell style="padding-top:5px;padding-left:5px;padding-bottom:5px;">總評                                
                                </asp:TableCell><asp:TableCell ColumnSpan="5" style="text-align:left;">
                                        申請人在同領域同級教師的研究表現<br></br>
                                        &nbsp;&nbsp;<asp:DropDownList ID="DDFiveLevelScore" runat="server" 
                                        DataSourceID="DSFiveLevelScore" DataTextField="FLName" 
                                        DataValueField="FLNo">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="DSFiveLevelScore" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>" 
                                        SelectCommand="SELECT [FLNo], [FLName] FROM [CFiveLevelScore]">
                                    </asp:SqlDataSource>
                                </asp:TableCell></asp:TableRow>--%><asp:TableRow ID="WritingScoreDiscription1" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="4">代表著作評分項目及標準</asp:TableCell><asp:TableCell RowSpan="2"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
	                                五年內或前一等級
	                                至本次申請等級時<br/>
	                                個人學術與專業之
	                                整體成就<br/><br/>
                                    教    授50%<br/>
                                    副 教 授40%<br/>
                                    助理教授30%<br/>
                                    講　　師20%<br/>
                                    </asp:TableCell><asp:TableCell RowSpan="2">總 分<br /><br /><font color="red">研究項目各職級及格
                                分數如下：<br/>
                                教授、副教授：80 分<br/>
                                助理教授：75 分<br/>
                                講師：70 分<br/><br/>
                                        ※請滑鼠左鍵點總分進行自動加總
                                                                                         </font>                    
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="WritingScoreDiscription2" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell>項   目</asp:TableCell><asp:TableCell>
	                                研 究 主 題 <br/> <br/> 
                                  教    授 5%<br/>
                                  副 教 授10%<br/>
                                  助理教授20%<br/>
                                  講　　師25%<br/>
                                    </asp:TableCell><asp:TableCell>
	                                研究方法及能力 <br/> <br/> 
                                  教    授10%<br/>
                                  副 教 授20%<br/>
                                  助理教授25%<br/>
                                  講　　師30%<br/>
                                    </asp:TableCell><asp:TableCell>
	                                學術及實務貢獻 <br/><br/> 
                                  教    授35%<br/>
                                  副 教 授30%<br/>
                                  助理教授25%<br/>
                                  講　　師25%<br/>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="WritingScoreDiscription3" runat="Server" Visible="false" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="6" HorizontalAlign="Left"><font size="4">著作評分項目：包括研究主題、文字與結構、研究方法及參考資料、學術或應用價值等項。<br/>
                                                【＊註：如有五年內且前一等級至本次申請等級時個人學術與專業之整體成就可參考納入評分】<br/>
                                                 <font color ="RED">各職級及格分數如下：助理教授：75分；講師：70分</font><br/></font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="WritingScoreInputData" Style="text-align: center;">
                                    <asp:TableCell>得<br />分</asp:TableCell><asp:TableCell>
                                        <asp:TextBox ID="TBWSSubject"
                                            runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="cv1" ControlToValidate="TBWSSubject" runat="server"
                                            ErrorMessage="本欄位只接受數字" Text="*" ValidationExpression="^[0-9]+(\.[0-9]{1,1})?$" />
                                    </asp:TableCell><asp:TableCell>
                                        <asp:TextBox ID="TBWSMethod"
                                            runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="cv2" ControlToValidate="TBWSMethod" runat="server"
                                            ErrorMessage="本欄位只接受數字" Text="*" ValidationExpression="^[0-9]+(\.[0-9]{1,1})?$" />
                                    </asp:TableCell><asp:TableCell>
                                        <asp:TextBox ID="TBWSContribute"
                                            runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="cv3" ControlToValidate="TBWSContribute" runat="server"
                                            ErrorMessage="本欄位只接受數字" Text="*" ValidationExpression="^[0-9]+(\.[0-9]{1,1})?$" />
                                    </asp:TableCell><asp:TableCell>
                                        <asp:TextBox ID="TBWSAchievement"
                                            runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="cv4" ControlToValidate="TBWSAchievement" runat="server"
                                            ErrorMessage="本欄位只接受數字" Text="*" ValidationExpression="^[0-9]+(\.[0-9]{1,1})?$" />
                                    </asp:TableCell><asp:TableCell>
                                        <asp:TextBox ID="TBWTotalScore"
                                            runat="server"></asp:TextBox>
                                        <asp:HiddenField ID="TBWTotalScoreHidden" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="CommonNote" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="6" HorizontalAlign="Left"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" ID="WayNo1" Visible="false">
                                ※審查評定基準：<br />
                                教　　授：應在該學術領域內有獨特及持續性著作並有重要具體之貢獻者。<br />
                                副 教 授：應在該學術領域內有持續性著作並有具體之貢獻者。<br />
                                助理教授：應有相當於博士論文水準之著作並有獨立研究之能力者。<br />
                                講　　師：應有相當於碩士論文水準之著作。<br /><br />

                                ※附註: <br />
                                    1.以整理、增刪、組合或編排他人著作而成之編著不得送審。<br />
                                      2.送審著作不得為學位論文或其論文之一部分。惟若未曾以該學位論文送審任一等級教師資格或屬學位論文延續性研究者送審者，經出版並提出說明，由專業審查認定著作具相當程度創新者，不在此限。<br />
                                      3.『5年內或前一等級至本次申請等級時個人學術與專業之整體成就』包含代表作。<br /><br />
                                    ※以著作/技術報告/教學實務報告送審者：送五位校外學者專家審查，外審作業完成後，送校教評會開會審議，其外審審查結果有四位(含)以上審查委員給予及格者，除有改變外審結果之事實外，予以通過。<br /> 
                                    </asp:TableCell><asp:TableCell ColumnSpan="6" HorizontalAlign="Left"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" ID="WayNo2" Visible="false">
                                ※審查評定基準：<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;副 教 授：應在該學術領域內有持續性著作並有具體之貢獻者<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;助理教授：應有相當於博士論文水準之著作並有獨立研究之能力者。<br />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;講　　師：應有相當於碩士論文水準之著作。<br /><br />
                                
                                ※附註:<br />
                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.以整理、增刪、組合或編排他人著作(成果)而成之編著不得送審。<br/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.以教育人員任用條例第30條之1送審副教授可以博士論文送審，惟仍須符合修正分級後副教授水準。<br/><br />
                                <b>※以學位論文送審者：送四位校外學者專家審查，外審作業完成後，送校教評會開會審議，其外審審查結果有三位(含)以上審查委員給予及格者，除有改變外審結果之事實外，予以通過。</b><br></br>
                                    </asp:TableCell><asp:TableCell ColumnSpan="6" HorizontalAlign="Left"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" ID="WayNo3" Visible="false">
                            審查評定基準：教　　授：應在該學術領域內有獨特及持續性著作(成果)並有重要具體之貢獻者。
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;副 教 授：應在該學術領域內有持續性著作(成果)並有具體之貢獻者。
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="FinalPassRow" runat="Server" Style="text-align: center;">
                                    <asp:TableCell ID="TableCell4" runat="Server"> 送審等級 </asp:TableCell><asp:TableCell>
                                        <font color="blue">
                                            <asp:Label ID="AuditAppJobTitle2" runat="server"
                                                Text="" /></font>
                                    </asp:TableCell><asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                                        <asp:Label ID="LBTotalScore" runat="server" Visible="false"><font color="blue">
                                總分：</font></asp:Label>
                                    </asp:TableCell><asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                                        <asp:TextBox ID="TBAllTotalScore" runat="server" Visible="false"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorTBAllTotalScore" ControlToValidate="TBAllTotalScore" runat="server"
                                            ErrorMessage="本欄位只接受數字" Text="*" ValidationExpression="[0-9]+" />
                                    </asp:TableCell><asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" ID="TablePass1" runat="Server" Visible="false"> <font color="blue">是否及格<br/><br/>
                                               研究項目各職級及格分數如下：<br/>
                                               教授、副教授：80 分<br/>
                                               助理教授：75 分<br/>
                                               講師：70 分<br/></font>
                                    </asp:TableCell><asp:TableCell Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;" ID="TablePass2" runat="Server" Visible="false"> <font color="blue">是否及格<br/>
                                               【各職級及格分數如下：助理教授：75分; 講師：70分】</font>
                                    </asp:TableCell><asp:TableCell>
                                        <asp:DropDownList ID="DDExecutePass" runat="server">
                                            <asp:ListItem Value="0">審核中</asp:ListItem>
                                            <asp:ListItem Value="1">及格</asp:ListItem>
                                            <asp:ListItem Value="2">不及格</asp:ListItem>
                                        </asp:DropDownList>

                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow43" runat="Server" Width="100%" Style="text-align: center;">
                                    <asp:TableCell ID="TableCell2" runat="Server" Font-Size="X-Large" ForeColor="Red" ColumnSpan="6">乙表 </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow ID="TableRow41" runat="Server" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="6" Style="padding-top: 5px; padding-left: 5px;">
                                        表(乙)審查意見(本頁僅提供送審人參考，係可公開文件，字數上限為500字)<br />
                                        <font color="blue">
                                            <asp:TextBox ID="AuditExecuteCommentsB" MaxLength="500" runat="server" Height="300px"
                                                Text=""
                                                TextMode="Multiline" Width="95%"></asp:TextBox>
                                        </font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="StrengthsWeaknessesTitle" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="4">優   點(請勾選)</asp:TableCell><asp:TableCell ColumnSpan="4"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">缺   點(請勾選)</asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow runat="Server" ID="StrengthsWeaknessesInputData" Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="4"
                                        Style="padding-top: 0px; padding-left: 5px; text-align: left;">
                                        <font color="blue">
                                            <asp:PlaceHolder ID="PHolderStrengths" runat="server"></asp:PlaceHolder>
                                            <asp:CheckBoxList ID="CkBxStrengths" runat="server" AutoPostBack="true"></asp:CheckBoxList>
                                            其他：<asp:TextBox ID="OtherStrengths" TextMode="Multiline" Width="60%" runat="server"></asp:TextBox>
                                        </font>
                                    </asp:TableCell><asp:TableCell ColumnSpan="4"
                                        Style="padding-top: 0px; padding-left: 5px; text-align: left;">
                                        <font color="blue">
                                            <asp:PlaceHolder ID="PHolderWeaknesses" runat="server"></asp:PlaceHolder>
                                            <asp:CheckBoxList ID="CkBxWeaknesses" runat="server"></asp:CheckBoxList>
                                            其他：<asp:TextBox ID="OtherWeaknesses" TextMode="Multiline" Width="60%" runat="server"></asp:TextBox>
                                        </font>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <%--<asp:TableRow ID="FiveLevelNote">
                                    <asp:TableCell ColumnSpan="6" HorizontalAlign="Left"
                                        Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">
                             <b>
                           ※總評之「分數範圍」對照如下：<br />                            
                            極優(前10%)：90-100分；優(11%-20%)：80-89分；良(21%-40%)：70-79分；普通(41%-60%)：60-69分；不良(後40%)：0-59分。<br></br>                                
                                    </asp:TableCell></asp:TableRow>--%><asp:TableRow runat="Server" ID="TableRow46" Style="text-align: left;">
                                        <asp:TableCell ColumnSpan="6"
                                            Style="padding-top: 0px; padding-left: 5px;">
                                        ※本案如經勾選缺點欄位之「非個人原創性…」、「代表作屬學位論文…」及「涉及抄襲或違反其他學術倫理情事」等3項之一者，依專科以上學校教師資格審定辦法第21條、第22條、第43條規定，應評為不及格成績。
                                        </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow Style="text-align: center;">
                                    <asp:TableCell ColumnSpan="6">
                                        <asp:Button ID="AuditFinish" runat="server" Text="審核送出" OnClick="BtnAuditFinish_Click" OnClientClick=" return confirm('請確認此審核送出!')" CssClass="btn-danger" />
                                        <asp:Button ID="AuditSave" runat="server" Text="暫存" OnClick="BtnAuditSave_Click" CssClass="btn-info" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                        </asp:View>
                        <%--                        <asp:View ID="ViewAuditorSetting" runat="server" >
                            <asp:GridView ID="GVAuditExecute" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                                AutoGenerateColumns="false" OnRowDataBound="GVAuditExecute_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorSn" runat="server" Text='<%# Bind("ExecuteAuditorSn")%>'></asp:TextBox></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ExecuteStage" HeaderText="簽核階段" ItemStyle-Width="100" />
                                    <asp:BoundField DataField="ExecuteStageNum" HeaderText="簽核階段Num" ItemStyle-Width="100" Visible="false" />
                                    <asp:BoundField DataField="ExecuteStepNum" HeaderText="簽核子階段" ItemStyle-Width="100" Visible="false" />
                                    <asp:BoundField DataField="ExecuteRoleName" HeaderText="簽核角色" ItemStyle-Width="100" />
                                    <asp:TemplateField HeaderText="簽核人員EmpId or Sn" ItemStyle-Width="100">
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorSnEmpId" runat="server" Text='<%# Bind("ExecuteAuditorSnEmpId")%>'></asp:TextBox></ItemTemplate><ItemStyle CssClass="hiddencol" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="簽核人員" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorName" runat="server" Text='<%# Bind("ExecuteAuditorName")%>'></asp:TextBox></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="簽核人Email" ItemStyle-Width="100">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextBoxAuditorEmail" runat="server" Text='<%# Bind("ExecuteAuditorEmail")%>'></asp:TextBox></ItemTemplate></asp:TemplateField><asp:BoundField DataField="ExecuteBngDate" HeaderText="開放審核日期 起" ItemStyle-Width="100" />
                                    <asp:BoundField DataField="ExecuteEndDate" HeaderText="~ 迄" ItemStyle-Width="100" />
                                    <asp:BoundField DataField="ExecuteStatus" HeaderText="審核狀態" ItemStyle-Width="100" />
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Label ID="MessageAudit" runat="server" Text="" ForeColor="Red"></asp:Label><br /><br /><br /><asp:Button ID="BtnUpdateAuditor" Visible="false" runat="server" Text="更新" OnClick="BtnUpdateAuditor_Click" CssClass="btn" />
                            <asp:Button ID="BtnStartAuditor" runat="server" Text="啟動簽呈" OnClick="BtnStartAuditor_Click" AutoPostBack="True" CssClass="btn" Visible="false" />
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:View>--%>
                    </asp:MultiView><asp:Label ID="MessageLabelAll" runat="server" Text="" Font-Size="XX-Large" ForeColor="Red"></asp:Label>
                </div>
            </fieldset>
        </div>
        <div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-66382736 #1600
            </label>
        </div>
    </form>
</body>
</html>
