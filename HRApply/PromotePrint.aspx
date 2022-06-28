<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromotePrint.aspx.cs" Inherits="ApplyPromote.PromotePrint" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

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
    <%--<link href="css/tabs.css" rel="stylesheet" />--%>
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
            background-color:#005ab5;
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
    <title>升等申請表</title>
</head>

<body>

    <form id="formAccount" runat="server" enctype="multipart/form-data">
        <br/>
        <div style="width: 80%; margin: auto">

            <div align="center"><span >
                <h4><b>臺北醫學大學
                    <asp:Label ID="year" runat="server" Text="" Font-Bold="true" />
                    學年度 第
                    <asp:Label ID="semester" runat="server" Text="" Font-Bold="true" />
                    學期
                    <asp:Label ID="AppJobTypeName" runat="server" Text="兼任" Font-Bold="true"  />教師升等申請表</b></h4>

            </span></div>
            <asp:Table ID="Tb0" runat="Server"  HorizontalAlign="Center"
                Style=" text-align: center;"  CssClass="table table-bordered table-condensed">
                 <asp:TableRow>
                    <asp:TableCell runat="Server" BackColor="#2894ff" ForeColor="White" Width="20%"> 姓名 </asp:TableCell>
                    <asp:TableCell runat="Server" Width="20%">
                        <font color="black">
                            <asp:Label ID="EmpNameCN" runat="server" Text=""></asp:Label></font>
                        <asp:Label id="lb_NoEmpNameCN" runat="server" Text="尚無填寫資料。" Visible="false" />
                    </asp:TableCell>
                    <asp:TableCell runat="Server"  BackColor="#2894ff" ForeColor="White" Width="18%">升等職稱</asp:TableCell>
                    <asp:TableCell runat="Server">
                        <font color="black">
                            <asp:Label ID="AppJobTitle" runat="server" Text="Label"></asp:Label></font>
                        <asp:Label id="lb_NoAppJobTitle" runat="server" Text="尚無選填資料。" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server" BackColor="#2894ff" ForeColor="White" Width="20%">現任專職機構<br/>名稱及職別</asp:TableCell>
                    <asp:TableCell runat="Server" ColumnSpan="3" HorizontalAlign="Center">
                        <font color="black">
                            <asp:Label ID="EmpUnit" runat="server" value=""></asp:Label><asp:Label ID="EmpNowEJobTitle" runat="server" value="" MaxLength="20"></asp:Label></font>
                        <asp:Label id="lb_NoEmpUnit" runat="server" Text="尚無選填資料。" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server" BackColor="#2894ff" ForeColor="White" Width="20%"> 升等類型 </asp:TableCell>
                    <asp:TableCell runat="Server" ColumnSpan="3">
                        <font color="black">
                            <asp:Label ID="AppAttributeName" runat="server" Text=""></asp:Label></font>
                        <asp:Label id="lb_NoAppAttributeName" runat="server" Text="尚無選填資料。" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server" BackColor="#2894ff" ForeColor="White" Width="20%"> 適用法規 </asp:TableCell>
                    <asp:TableCell runat="Server" ColumnSpan="3">
                        <div id="div_law" runat="server">
                        <font color="black">
                            <asp:Label ID="LawText" runat="server"></asp:Label>
                            <asp:Label ID="ItemLabel" runat="server"></asp:Label>
                            &nbsp;項第 
                                    <asp:Label ID="LawNumNoLabel" runat="server"></asp:Label>
                            款送審。
                            <br />
                            <asp:Label ID="LawContent" runat="server"></asp:Label></font>
                        </div>
                        <asp:Label id="lb_NoLaw" runat="server" Text="尚無選填資料。" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
<%--              <asp:Table ID="Tb1" runat="Server" HorizontalAlign="Center"
                Style=" text-align: center;"  CssClass="table table-bordered table-condensed">
              <asp:TableRow ID="TableRow1" runat="Server">
                    <asp:TableCell ID="TableCell1" BackColor="#005ab5" Font-Size="16" Font-Bold="true" ForeColor="White" runat="Server"> 教師年資 </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="Server">
                    <asp:TableCell ID="TableCell2" runat="Server">
--%>
            <div>
            <div style="float:left">
                        <font size="5"> <b>教師年資 </b></font>
                </div><br />
                        <hr/>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CellPadding="2" DataKeyNames="ExpSn" DataSourceID="DSTeacherTmuEdu"
                            HeaderStyle-BackColor="#2894ff" HeaderStyle-ForeColor="White"
                            BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" GridLines="Both">                            
                            <Columns>
                                <asp:BoundField DataField="ExpPosName" HeaderText="經歷" HeaderStyle-Width="13%"
                                    SortExpression="ExpPosName" />
                                <asp:BoundField DataField="ExpUnitName" HeaderText="單位" SortExpression="ExpUnitName" />
                                <asp:BoundField DataField="ExpTitleName" HeaderText="職稱" HeaderStyle-Width="8%"
                                    SortExpression="ExpTitleName" />
                                <asp:BoundField DataField="ExpStartEndDate" HeaderText="起訖日期" HeaderStyle-Width="15%"
                                    SortExpression="ExpStartEndDate" />
                                <asp:BoundField DataField="ExpQid" HeaderText="教師證書字號" HeaderStyle-Width="16%"
                                    SortExpression="ExpQid" />

                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="DSTeacherTmuEdu" runat="server"
                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                            SelectCommand="
                                    SELECT [ExpSn],[EmpSn],b.unt_name_full AS ExpUnitName,c.tit_name AS ExpTitleName, d.pos_name AS ExpPosName,ExpQid,[ExpStartDate]+'~'+[ExpEndDate] AS ExpStartEndDate,[ExpUploadName] 
                                    FROM [TeacherTmuExp] AS a LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit] AS b ON a.ExpUnitId = b.unt_id  COLLATE Chinese_Taiwan_Stroke_CI_AS 
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_titlecode] AS c ON a.ExpTitleId = c.tit_id  COLLATE Chinese_Taiwan_Stroke_CI_AS
                                                              LEFT OUTER JOIN [PAPOVA].[TmuHr].[dbo].[s90_position] AS d ON a.ExpPosId = d.pos_id  COLLATE Chinese_Taiwan_Stroke_CI_AS WHERE EmpSn = @EmpSn">

                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="ExpSn" />
                            </DeleteParameters>
                        </asp:SqlDataSource>
                        <asp:Label runat="server" ID="lb_NoTMUTeachExp" Text="尚無填寫資料。" Visible="false" />
                </div>
            <br />
<%--                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>--%>
<%--            <asp:Table ID="Tb2" runat="Server" HorizontalAlign="Center"
                Style=" text-align: center;"  CssClass="table table-bordered table-condensed">
                <asp:TableRow runat="Server" >
                    <asp:TableCell  BackColor="#005ab5" Font-Size="16" Font-Bold="true" ForeColor="White" runat="Server"> 學歷 </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server">--%>
            <div>
            <div style="float:left">
                        <font size="5"> <b>學歷 </b></font>
                </div><br />
                        <hr/>
                        <asp:GridView ID="GVTeachEdu" runat="server" AutoGenerateColumns="False"
                            CellPadding="2" DataKeyNames="EduSn" DataSourceID="DSTeacherEdu" HeaderStyle-BackColor="#2894ff" HeaderStyle-ForeColor="White"
                            BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" BorderWidth="0px" GridLines="Both">
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="100" Visible="false">
                                    <HeaderStyle CssClass="hiddencol" />
                                    <ItemTemplate>
                                        <asp:Label ID="EduSn" runat="server" Text='<%#Bind("EduSn")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="hiddencol" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="EduSchool" HeaderText="學校名稱" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EduDepartment" HeaderText="院系所" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="EduDepartment" />
                                <asp:BoundField DataField="EduDegree" HeaderText="學位" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="EduDegree"  HeaderStyle-Width="8%" />
                                <asp:BoundField DataField="EduStartEndYM" HeaderText="起迄年月" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="EduStartEndYM"  HeaderStyle-Width="15%" />

                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="DSTeacherEdu" runat="server"
                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                            SelectCommand="SELECT a.EduSn, a.[EduSchool], a.[EduDepartment], b.DegreeName as EduDegree, a.[EduStartYM]+'~'+ a.[EduEndYM]  AS EduStartEndYM FROM [TeacherEdu] AS a 
                                     LEFT OUTER JOIN CDegree AS b ON a.EduDegree = b.DegreeNo LEFT OUTER JOIN CDegreeType AS c ON a.EduDegreeType = c.DegreeTypeNo Where EmpSn = @EmpSn">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="6" Name="EmpSn" SessionField="EmpSn" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:Label ID="lb_NoTeachEdu" runat="server" Text="尚未填寫資料。" Visible="false" />
                </div>
            <br />
<%--                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>--%>

<%--            <asp:Table ID="Tb4" runat="Server" HorizontalAlign="Center"
                Style=" text-align: center;"  CssClass="table table-bordered table-condensed">
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server" BackColor="#005ab5" Font-Size="16" Font-Bold="true" ForeColor="White"> 其他行政經歷 </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server">--%>
            <div>
            <div style="float:left">
                        <font size="5"> <b>其他行政經歷 </b></font>
                </div><br />
                        <hr/>
                        <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp" HeaderStyle-BackColor="#2894ff" HeaderStyle-ForeColor="White"
                            BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" BorderWidth="0px" GridLines="Both">

                            <Columns>
                                <asp:BoundField DataField="ExpOrginization" HeaderText="機關"
                                    SortExpression="ExpOrginization" />
                                <asp:BoundField DataField="ExpUnit" HeaderText="單位" SortExpression="ExpUnit" />
                                <asp:BoundField DataField="ExpJobTitle" HeaderText="職稱" HeaderStyle-Width="13%"
                                    SortExpression="ExpJobTitle" />
                                <asp:BoundField DataField="ExpJobTypeName" HeaderText="職別" HeaderStyle-Width="8%"
                                    SortExpression="ExpJobTypeName" />
                                <asp:BoundField DataField="ExpStartEndYM" HeaderText="起訖日期" HeaderStyle-Width="15%"
                                    SortExpression="ExpStartEndYM" />

                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="DSTeacherExp" runat="server"
                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                            DeleteCommand="DELETE FROM TeacherTmuExp WHERE ExpSn = @ExpSn"
                            SelectCommand="SELECT A.[ExpSn], A.[EmpSn], A.[ExpOrginization], A.[ExpUnit], A.[ExpJobTitle], A.[ExpStartYM]+'~'+A.[ExpEndYM] AS [ExpStartEndYM], A.[ExpUploadName] , B.JobAttrName AS ExpJobTypeName FROM [TeacherExp] AS A LEFT OUTER JOIN 
                                     CJobType AS B ON A.ExpJobType = B.JobAttrNo  Where EmpSn = @EmpSn">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="ExpSn" />
                            </DeleteParameters>
                        </asp:SqlDataSource>
                        <asp:Label runat="server" ID="lb_NoTeachExp" Text="尚無填寫資料。" Visible="false" />
            </div>
            <br />

<%--                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>--%>

<%--            <asp:Table ID="Tb6" runat="Server" HorizontalAlign="Center"
                Style=" text-align: center;"  CssClass="table table-bordered table-condensed">
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server" BackColor="#005ab5" Font-Size="16" Font-Bold="true" ForeColor="White"> 授課情形</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="Server">
                    <asp:TableCell runat="Server" Style="padding-top: 5px; padding-left: 5px; padding-bottom: 5px;">--%>
            <div>
            <div style="float:left">
                        <font size="5"> <b>授課情形 </b></font>
                </div><br />
                        <hr/>
                        <asp:GridView ID="GVTeachLesson" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" DataKeyNames="LessonSn" DataSourceID="DSTeacherLesson" HeaderStyle-BackColor="#2894ff" HeaderStyle-ForeColor="White"
                            BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" BorderWidth="0px" GridLines="Both">
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="100" Visible="false">
                                    <HeaderStyle CssClass="hiddencol" />
                                    <ItemTemplate>
                                        <asp:Label ID="LessonSn" runat="server" Text='<%#Bind("LessonSn")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="hiddencol" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SMTR" HeaderText="學年度"  HeaderStyle-Width="10%"
                                    SortExpression="SMTR" />
                                <asp:BoundField DataField="LessonDeptLevel" HeaderText="系所級別"
                                    SortExpression="LessonDeptLevel" />
                                <asp:BoundField DataField="LessonClass" HeaderText="課目" SortExpression="LessonClass" />
                                <asp:BoundField DataField="LessonHours" HeaderText="授課時數" HeaderStyle-Width="13%"
                                    SortExpression="LessonHours" />

                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="DSTeacherLesson" runat="server"
                            ConnectionString="<%$ ConnectionStrings:TmuConnectionString%>"
                            DeleteCommand="DELETE FROM TeacherTmuLesson WHERE LessonSn = @LessonSn"
                            SelectCommand="
                                SELECT [LessonSn],[EmpSn],[LessonYear]+[LessonSemester] AS SMTR,[LessonDeptLevel],[LessonClass],[LessonHours],[LessonCreditHours],[LessonEvaluate] FROM [TeacherTmuLesson] WHERE EmpSn = @EmpSn  order by SMTR desc ">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                                <asp:SessionParameter DefaultValue="0" Name="LessonYear" SessionField="sYear" />
                                <asp:SessionParameter DefaultValue="0" Name="LessonSemester" SessionField="sSemester" />
                            </SelectParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="LessonSn" />
                            </DeleteParameters>
                        </asp:SqlDataSource>
                        
                        <asp:Label runat="server" ID="lb_NoTeachLesson" Text="尚無填寫資料。" Visible="false" />
<%--                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>--%>
                </div>
            <br />
        </div>
    </form>
</body>
</html>
