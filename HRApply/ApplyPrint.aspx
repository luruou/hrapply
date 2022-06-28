<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplyPrint.aspx.cs" Inherits="ApplyPromote.ApplyPrint" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript">
<style>
 .grid th{
    text-align:center;
  }

        img {
            image-rendering: -moz-crisp-edges;
            image-rendering: -o-crisp-edges;
            image-rendering: -webkit-optimize-contrast;
            image-rendering: crisp-edges;
            -ms-interpolation-mode: nearest-neighbor;
        }
</style>


</script>
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <link href="css/tabs.css" rel="stylesheet" />
 </head>
<body style="background-color: rgb(230, 233, 236);">

    <form id="formAccount" runat="server">
        <div class="container">

        <div align="center"><span style="font-family:DFKai-sb;"><b><h4>臺北醫學大學 105 學年度 第一學期 教師履歷表</h4></b></span></div>
        <asp:Table ID="Tb1" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="900px" >
            <asp:TableRow ID="TableRow1" Runat="Server">
                <asp:TableCell ID="TableCell1" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;width:95px"> 應徵單位 </asp:TableCell>
                <asp:TableCell ID="TableCell2" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;width:160px">
                    <font color="blue"><asp:Label ID="AppUnit" runat="server" Text=""></asp:Label></font>
                </asp:TableCell>
                <asp:TableCell ID="TableCell3" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;width:100px">應徵職稱/職別</asp:TableCell>
                <asp:TableCell ID="TableCell4" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;width:100px">
                    <font color="blue"><asp:Label ID="AppJobTitle" runat="server" Text=""></asp:Label>/<asp:Label ID="AppJobType" runat="server" Text="Label"></asp:Label></font>
                </asp:TableCell>
                <asp:TableCell ID="TableCell5" Runat="Server" RowSpan="4"  style="width:140px"><asp:Image ID="EmpPhotoImage" runat="server" Height="170px" Width="140px"/> </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" Runat="Server">
                <asp:TableCell ID="TableCell6" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 中文姓名 </asp:TableCell>
                <asp:TableCell ID="TableCell7" Runat="Server">
                    <font color="blue"><asp:Label ID="EmpNameCN" runat="server" Text=""></asp:Label></font>
                </asp:TableCell>
                <asp:TableCell ID="TableCell8" Runat="Server" >英文姓名</asp:TableCell>
                <asp:TableCell ID="TableCell9" Runat="Server">
                    <font color="blue"><asp:Label ID="EmpNameENFirst" runat="server" Text="Label"></asp:Label>&nbsp;<asp:Label ID="EmpNameENLast" runat="server" Text="Label"></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>  
            <asp:TableRow ID="TableRow3" Runat="Server">
                <asp:TableCell ID="TableCell10" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 新聘類型 </asp:TableCell>
                <asp:TableCell ID="TableCell11" Runat="Server" ColumnSpan="3" HorizontalAlign="Left">
                    <font color="blue"><asp:Label ID="AppAttributeName" runat="server" Text=""></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" Runat="Server">
                <asp:TableCell ID="TableCell12" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 適用法規 </asp:TableCell>
                <asp:TableCell ID="TableCell13" Runat="Server" ColumnSpan="3" HorizontalAlign="Left">
                    <font color="blue"><asp:Label ID="LawText" runat="server" ></asp:Label>
                                    <asp:Label ID="ItemLabel" runat="server"></asp:Label>
                                    &nbsp;項第 
                                    <asp:Label ID="LawNumNoLabel" runat="server"></asp:Label>                                 
                                    款送審。 <br />
                                    <asp:Label ID="LawContent" runat="server"></asp:Label></font>
                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <asp:Table ID="Tb2" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="900px">
            <asp:TableRow ID="TableRow5" Runat="Server" BackColor="Yellow">
                <asp:TableCell ID="TableCell14" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 學歷 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow6" Runat="Server">
                <asp:TableCell ID="TableCell15" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
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
         <asp:Table ID="Tb3" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="900px" >
            <asp:TableRow ID="TableRow7" Runat="Server" BackColor="Yellow">
                <asp:TableCell ID="TableCell16" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 現職 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8" Runat="Server">
                <asp:TableCell ID="TableCell17" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                <asp:Label ID="AppENowJobOrg" runat="server" Text="Label"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <asp:Table ID="Tb4" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="900px" >
            <asp:TableRow ID="TableRow9" Runat="Server" BackColor="Yellow">
                <asp:TableCell ID="TableCell18" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 經歷 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow10" Runat="Server">
                <asp:TableCell ID="TableCell19" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                <asp:GridView ID="GVTeachExp" runat="server" AutoGenerateColumns="False" 
                        CellPadding="2" DataKeyNames="ExpSn" DataSourceID="DSTeacherExp" 
                        EnableModelValidation="True" 
                        CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="Both">
                        <Columns>
                            <asp:TemplateField  HeaderText="No" ItemStyle-Width="100" >
                            <HeaderStyle CssClass="hiddencol" />
                            <ItemTemplate >
                                <asp:Label ID="ExpSn" runat="server" Text='<%#Bind("ExpSn")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle CssClass="hiddencol" />
                            </asp:TemplateField> 
                            <asp:BoundField DataField="ExpOrginization" HeaderText="服務機關" 
                                SortExpression="ExpOrginization" />
                            <asp:BoundField DataField="ExpUnit" HeaderText="服務部門/系所" SortExpression="ExpUnit" />
                            <asp:BoundField DataField="ExpJobTitle" HeaderText="職稱" 
                                SortExpression="ExpJobTitle" />
                            <asp:BoundField DataField="ExpStartEndYM" HeaderText="起訖年月" 
                                SortExpression="ExpStartEndYM" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="DSTeacherExp" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"                         
                        SelectCommand="SELECT [ExpSn], [EmpSn], [ExpOrginization], [ExpStartYM]+'~'+[ExpEndYM] AS ExpStartEndYM, [ExpUnit], [ExpJobTitle] FROM [TeacherExp] Where EmpSn = @EmpSn">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="9" Name="EmpSn" SessionField="EmpSn" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
         <asp:Table ID="Tb5" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="900px" >
            <asp:TableRow ID="TableRow11" Runat="Server" BackColor="Yellow">
                <asp:TableCell ID="TableCell20" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 專長 </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow12" Runat="Server">
                <asp:TableCell ID="TableCell21" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                <asp:Label ID="EmpExpertResearch" runat="server" Text="Label"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
        <asp:Table ID="Tb6" Runat="Server" BorderColor="black" 
            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
            style="font-weight: bold;text-align:center;" width="900px" >
            <asp:TableRow ID="TableRow13" Runat="Server" BackColor="Yellow">
                <asp:TableCell ID="TableCell22" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;font-family:PMingLiU;"> 學術獎勵、榮譽事項</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow14" Runat="Server">
                <asp:TableCell ID="TableCell23" Runat="Server" style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 
                    <asp:GridView ID="GVTeachHonour" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="HorSn" DataSourceID="DSTeachHonour" EnableModelValidation="True"
                    CssClass="table table-striped table-condensed" BorderWidth="0px" GridLines="Both"
                    >
                        
                        <Columns>
                            <asp:BoundField DataField="HorDescription" HeaderText="名稱" 
                                SortExpression="HorDescription" />
                            <asp:BoundField DataField="HorYear" HeaderText="日期" SortExpression="HorYear" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="DSTeachHonour" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"                        
                        SelectCommand="SELECT HorYear, HorDescription, HorSn, EmpSn FROM TeacherHonour Where EmpSn = @EmpSn">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="9" Name="EmpSn" SessionField="EmpSn" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                </asp:TableCell>
            </asp:TableRow>
         </asp:Table>
        </div>
    </form>
</body>
</html>