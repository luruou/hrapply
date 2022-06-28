<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromotePrint2.aspx.cs" Inherits="ApplyPromote.PromotePrint2" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
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
    <title></title>
    
        <style type="text/css">   
        .auto-style1 {
            height: 98px;
        }     
        body {
            font-family: 微軟正黑體;
            z-index:1000;
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
        <div style="width: 80%; margin: auto">

        <div align="center"><span style="font-family:DFKai-sb;"><b><h4>臺北醫學大學 105 學年度 第二學期 <asp:Label ID="AppJobTypeName" runat="server" Text="Label"></asp:Label>教師升等申請表</h4></b></span></div>
        <asp:Table ID="Tb0" Runat="Server"  class="table table-bordered table-condensed" >

            <asp:TableRow Runat="Server">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;width:150px"> 姓名 </asp:TableCell>
                <asp:TableCell Runat="Server">
                    <font color="blue"><asp:Label ID="EmpNameCN" runat="server" Text=""></asp:Label></font>
                </asp:TableCell>
                <asp:TableCell Runat="Server" >升等職稱</asp:TableCell>
                <asp:TableCell Runat="Server">
                    <font color="blue"><asp:Label ID="AppJobTitle" runat="server" Text="Label"></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Runat="Server">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;">現任專職機構名稱及職別</asp:TableCell>
                <asp:TableCell Runat="Server" ColumnSpan="3" HorizontalAlign="Left">
                    <font color="blue"><asp:Label ID="EmpUnit" runat="server" value=""></asp:Label><asp:Label ID="EmpNowEJobTitle" runat="server"  value="" MaxLength="20"></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>  
            <asp:TableRow Runat="Server">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 升等類型 </asp:TableCell>
                <asp:TableCell Runat="Server" ColumnSpan="3" HorizontalAlign="Left">
                    <font color="blue"><asp:Label ID="AppAttributeName" runat="server" Text=""></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Runat="Server">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 適用法規 </asp:TableCell>
                <asp:TableCell Runat="Server" ColumnSpan="3" HorizontalAlign="Left">
                    <font color="blue"><asp:Label ID="LawText" runat="server" ></asp:Label>
                                    <asp:Label ID="ItemLabel" runat="server"></asp:Label>
                                    &nbsp;項第 
                                    <asp:Label ID="LawNumNoLabel" runat="server"></asp:Label>                                 
                                    款送審。 <br />
                                    <asp:Label ID="LawContent" runat="server"></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <asp:Table ID="Tb1" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="85%" >
            <asp:TableRow ID="TableRow1" Runat="Server" BackColor="Yellow">
                <asp:TableCell ID="TableCell1" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 教師年資 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" Runat="Server">
                <asp:TableCell ID="TableCell2" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp" 
                            ForeColor="Black"  BackColor="White" 
                            BorderColor="#CCCCCC" BorderWidth="1px" 
                            BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" GridLines="Both">
                            
                            <Columns>
                                <asp:TemplateField  HeaderText="No" ItemStyle-Width="100" >
                                <HeaderStyle CssClass="hiddencol" />
                                <ItemTemplate >
                                    <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="hiddencol" />
                                </asp:TemplateField> 
                                <asp:TemplateField  HeaderText="No" ItemStyle-Width="100" ><HeaderStyle CssClass="hiddencol" /><ItemStyle CssClass="hiddencol" /></asp:TemplateField> 
                                <asp:BoundField DataField="ExpPosName" HeaderText="經歷" 
                                    SortExpression="ExpPosName" />
                                <asp:BoundField DataField="ExpUnitName" HeaderText="單位" SortExpression="ExpUnitName" />
                                <asp:BoundField DataField="ExpTitleName" HeaderText="職稱" 
                                    SortExpression="ExpTitleName" />
                                <asp:BoundField DataField="ExpStartEndDate" HeaderText="起訖日期" 
                                    SortExpression="ExpStartEndDate" />             

                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>" 
                            DeleteCommand="DELETE FROM TeacherTmuExp WHERE ExpSn = @ExpSn"
                            SelectCommand="
                            SELECT [ExpSn],[EmpSn],b.unt_name_full AS ExpUnitName,c.tit_name AS ExpTitleName, d.pos_name AS ExpPosName,[ExpStartDate]+'~'+[ExpEndDate] AS ExpStartEndDate ,[ExpUploadName] 
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
                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <asp:Table ID="Tb2" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="85%" >
            <asp:TableRow Runat="Server" BackColor="Yellow">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 學歷 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Runat="Server">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                           <asp:GridView ID="GVTeachEdu" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="2" DataKeyNames="EduSn" DataSourceID="DSTeacherEdu" 
                                    EnableModelValidation="True" CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="Both"
                                    >
                                    <Columns>
                                        <asp:TemplateField  HeaderText="No" ItemStyle-Width="100" >
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemTemplate >
                                            <asp:Label ID="EduSn" runat="server" Text='<%#Bind("EduSn")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="hiddencol" />
                                        </asp:TemplateField> 
                                        
                                        <asp:BoundField DataField="EduSchool" HeaderText="學校名稱" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="EduDepartment" HeaderText="院系所" ItemStyle-HorizontalAlign="Center" 
                                            SortExpression="EduDepartment" />
                                        <asp:BoundField DataField="EduDegree" HeaderText="學位" ItemStyle-HorizontalAlign="Center" 
                                            SortExpression="EduDegree" />
                                        <asp:BoundField DataField="EduStartEndYM" HeaderText="起迄年月" ItemStyle-HorizontalAlign="Center" 
                                            SortExpression="EduStartEndYM" />
                                        
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="DSTeacherEdu" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>" 
                                    
                                    SelectCommand="SELECT a.EduSn, a.[EduSchool], a.[EduDepartment], b.DegreeName as EduDegree, a.[EduStartYM]+'~'+ a.[EduEndYM]  AS EduStartEndYM FROM [TeacherEdu] AS a 
                                     LEFT OUTER JOIN CDegree AS b ON a.EduDegree = b.DegreeNo LEFT OUTER JOIN CDegreeType AS c ON a.EduDegreeType = c.DegreeTypeNo Where EmpSn = @EmpSn">
                                    <SelectParameters>
                                        <asp:SessionParameter DefaultValue="9" Name="EmpSn" SessionField="EmpSn" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         
         <asp:Table ID="Tb4" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="85%" >
            <asp:TableRow Runat="Server" BackColor="Yellow">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 經歷 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Runat="Server">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                                           <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp" 
                                    ForeColor="Black"  BackColor="White" 
                                    BorderColor="#CCCCCC" BorderWidth="1px" 
                                    BorderStyle="None" EnableModelValidation="True" CssClass="table table-bordered table-condensed" GridLines="Both">
                                    
                                    <Columns>
                                        <asp:TemplateField  HeaderText="No" ItemStyle-Width="100" >
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemTemplate >
                                            <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
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
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="DSTeacherExp" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>" 
                                    DeleteCommand="DELETE FROM TeacherTmuExp WHERE ExpSn = @ExpSn"
                                    SelectCommand="
                                    SELECT [ExpSn],[EmpSn],b.unt_name_full AS ExpUnitName,c.tit_name AS ExpTitleName, d.pos_name AS ExpPosName,[ExpStartDate]+'~'+[ExpEndDate] AS ExpStartEndDate,[ExpUploadName] 
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
                         

                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         
        <asp:Table ID="Tb6" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="85%" >
            <asp:TableRow Runat="Server" BackColor="Yellow">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 授課情形</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow Runat="Server">
                <asp:TableCell Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                   <asp:GridView ID="GVTeachLesson" runat="server" AutoGenerateColumns="False" 
                                CellPadding="4" DataKeyNames="LessonSn" DataSourceID="DSTeacherLesson" 
                                BackColor="White" BorderColor="#CCCCCC" BorderWidth="1px" Wrap="True" 
                                ForeColor="Black"  CssClass="table table-bordered table-condensed"  EnableModelValidation="True" GridLines="Both">
                                <Columns>
                                    <asp:TemplateField  HeaderText="No" ItemStyle-Width="100" >
                                    <HeaderStyle CssClass="hiddencol" />
                                    <ItemTemplate >
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

                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="DSTeacherLesson" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:TmuConnectionString%>"
                                DeleteCommand="DELETE FROM TeacherTmuLesson WHERE LessonSn = @LessonSn"                                    
                                SelectCommand="
                                SELECT [LessonSn],[EmpSn],[LessonYear]+[LessonSemester] AS SMTR,[LessonDeptLevel],[LessonClass],[LessonHours],[LessonCreditHours],[LessonEvaluate] FROM [TeacherTmuLesson] WHERE EmpSn = @EmpSn">
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
         </asp:Table>
        </div>
    </form>
</body>
</html>