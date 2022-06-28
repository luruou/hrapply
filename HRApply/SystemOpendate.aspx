<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="SystemOpendate.aspx.cs" Inherits="ApplyPromote.SystemOpendate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br/>
    <h2>設定系統開放時間</h2>
    <br />
    <div class="form-horizontal">
        <div>年度：<asp:TextBox ID="tbYear" runat="server" MaxLength="3" Width="47px"></asp:TextBox></div>
        <div>
            學期：<asp:TextBox ID="tbSemester" runat="server" MaxLength="1" Height="16px"
                Width="29px"></asp:TextBox>
        </div>
        <div class="control-group">
            <h5>新聘申請起訖時間</h5>
            <div>
                開放日期：<asp:TextBox ID="tbApplyBeginTime" runat="server"></asp:TextBox>
            </div>
            <div>
                結束日期：<asp:TextBox ID="tbApplyEndTime" runat="server"></asp:TextBox>
            </div>
        </div>
        <div align="center">
            <asp:Button ID="BtnOk" runat="server" Text="舊系統同步"
                CssClass="btn btn-success" enable="false" />
        </div>
        <div align="center">
            <table class="table">
                <tr>
                    <td style="text-align: left">
                        <asp:GridView ID="GVSystemOpendate" runat="server" AutoGenerateColumns="False"
                            CellPadding="4" DataKeyNames="Sn" DataSourceID="DSSystemOpen"
                            ForeColor="Black" GridLines="Horizontal"
                            BackColor="White"
                            BorderColor="#CCCCCC" BorderWidth="1px" EnableModelValidation="True" CssClass="table table-bordered" AllowSorting="True">
                            <Columns>
                                <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                                    <HeaderStyle CssClass="hiddencol" />
                                    <ItemTemplate>
                                        <asp:Label ID="Sn" runat="server" Text='<%#Bind("Sn")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="hiddencol" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="Smtr" HeaderText="年度" />
                                <asp:BoundField DataField="Semester" HeaderText="學期"
                                    SortExpression="Semester" />
                                <asp:BoundField DataField="KindName" HeaderText="新聘升等"
                                    SortExpression="KindName" />
                                <asp:BoundField DataField="JobAttrName" HeaderText="專兼任別"
                                    SortExpression="JobAttrName" />
                                <asp:BoundField DataField="ApplyBeginTime" HeaderText="申請開放日期"
                                    SortExpression="ApplyBeginTime" />
                                <asp:BoundField DataField="ApplyEndTime" HeaderText="申請結束日期"
                                    SortExpression="ApplyEndTime" />
                                <asp:CommandField ShowDeleteButton="True" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkOpendateMod" runat="server" Text="修改"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:SqlDataSource ID="DSSystemOpen" runat="server"
                            ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                            SelectCommand="SELECT a.Sn, a.Smtr, a.Semester, b.KindName, c.JobAttrName, a.ApplyBeginTime, a.ApplyEndTime, a.AuditBeginTime, a.AuditEndTime, a.AdminBeginTime, a.AdminEndTime FROM SystemOpendate AS a LEFT OUTER JOIN CAuditKind AS b ON a.KindNo = b.KindNo LEFT OUTER JOIN CJobType AS c ON a.TypeNo = c.JobAttrNo"
                            DeleteCommand="DELETE FROM SystemOpendate WHERE (Sn = @Sn)"
                            UpdateCommand="UPDATE [dbo].[SystemOpendate]
   SET [Smtr] = @Smtr
      ,[Semester] = @Semester
      ,[KindNo] = @KindNo
      ,[TypeNo] = @TypeNo
      ,[ApplyBeginTime] = @ApplyBeginTime
      ,[ApplyEndTime] = @ApplyEndTime
 WHERE Smtr=@Smtr and Semester =@Semester and KindNo = @KindNo and TypeNo=@TypeNo and  (Sn = @Sn)">
                            <DeleteParameters>
                                <asp:Parameter Name="Sn" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Smtr" />
                                <asp:Parameter Name="Semester" />
                                <asp:Parameter Name="KindNo" />
                                <asp:Parameter Name="TypeNo" />
                                <asp:Parameter Name="ApplyBeginTime" />
                                <asp:Parameter Name="ApplyEndTime" />
                                <asp:Parameter Name="Sn" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script type="text/javascript">
        $("[id$=BtnOk1]").on("click", function (e) {

            if ($('#tbYear').val() == "") {
                alert("請輸入年度");
                return false;
            }
            if ($('#tbSemester').val() == "") {
                alert("請輸入學期");
                return false;
            }

            if ($("[id$=tbApplyBeginTime]").val() == "") {
                alert("請輸入申請開放日期");
                return false;
            }

            if ($("[id$=tbApplyEndTime]").val() == "") {
                alert("請輸入申請結束日期");
                return false;
            }

            if ($("[id$=tbApplyBeginTime]").val() == "") {
                alert("請輸入審核開放日期");
                return false;
            }

            if ($("[id$=tbApplyEndTime]").val() == "") {
                alert("請輸入審核結束日期");
                return false;
            }
        });

        $("[id$=tbApplyBeginTime]").datetimepicker();
        $("[id$=tbApplyEndTime]").datetimepicker();
        $("[id$=tbAuditBeginTime]").datetimepicker();
        $("[id$=tbAuditEndTime]").datetimepicker();

    </script>
</asp:Content>
