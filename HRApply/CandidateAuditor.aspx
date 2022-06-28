<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="CandidateAuditor.aspx.cs"  MaintainScrollPositionOnPostback="true"Inherits="ApplyPromote.CandidateAuditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<body>
    <div>
        <div style="width:90%;margin:auto">                
        <h2>人才資料</h2>
        <hr />
        <asp:Button ID="OuterSchool" Text="服務機構維護" runat="server" CssClass="btn btn-primary btn-sm" data-toggle="tooltip" OnClientClick="window.open('OuterService.aspx','','top=100,left=100,width=1000,height=900,toolbar=no,scrollbars=yes'); return false;" Font-Size="Larger" />
        <asp:Button ID="OuterAdd" Text="外審人才新增" runat="server" CssClass="btn btn-primary btn-sm" data-toggle="tooltip" OnClientClick="window.open('OuterAdd.aspx','','top=100,left=100,width=1000,height=900,toolbar=no,scrollbars=yes'); return false;" Font-Size="Larger" /><br />
        <br />
        <asp:DropDownList ID="AuditorRealm" runat="server" AutoPostBack="True" DataTextField="AuditorRealmName"
            DataValueField="AuditorRealmSn" OnSelectedIndexChanged="AuditorRealm_Click" class="form-control">
        </asp:DropDownList>
        <br /><br />
        <%--<asp:GridView ID="gv_AuditorOuter" runat="server"  HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"  AutoGenerateColumns="False" HorizontalAlign="Center" Width="100%" HeaderStyle-Height="24"  
        GridLines="Horizontal" EmptyDataText="No Data"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ForeColor="Black">--%>

        <asp:GridView ID="gv_AuditorOuter" runat="server" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White" AutoGenerateColumns="False" HorizontalAlign="Center" Width="100%" HeaderStyle-Height="24"
            EmptyDataText="No Data" BackColor="White" BorderColor="Black" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ForeColor="Black"
            OnRowDataBound="gv_AuditorOuter_RowDataBound" class="table table-bordered"
            OnRowCancelingEdit="gv_AuditorOuter_RowCancelingEdit"
            OnRowEditing="gv_AuditorOuter_RowEditing"
            OnRowUpdating="gv_AuditorOuter_RowUpdating">
            <%--GridLines="Horizontal" 隱藏表格內直線--%>
            <Columns>
                <asp:TemplateField HeaderText="" Visible="false">
                    <HeaderTemplate>
                        <asp:CheckBox runat="server" ID="chkAll" Text="全選" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkItem" runat="server" />
                    </ItemTemplate>
                 <FooterStyle Width="4%" HorizontalAlign="Center" />
                 <HeaderStyle Width="4%" HorizontalAlign="Center" />
                 <ItemStyle Width="4%" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AppSn" runat="server" Text="聘單單號"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AppSn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AppSn") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lb_AppSn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AppSn") %>'></asp:Label>
                    <asp:TextBox ID="txt_AppSn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AppSn") %>' Visible="false"></asp:TextBox>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>              
                
                
            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_APPName" runat="server" Text="送審人"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_APPName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EmpNameCN") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lb_APPName" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "EmpNameCN") %>'></asp:Label>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>   

            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AuditorName" runat="server" Text='外審委員'></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AuditorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorName") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddl_AuditorName" runat="server" CssClass="form-control" Width="120" AutoPostBack="true" OnSelectedIndexChanged="ddl_AuditorName_SelectedIndexChanged" >
                    </asp:DropDownList>
                    <asp:Label ID="lb_AuditorName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorName") %>' Visible="false"></asp:Label>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>  


            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AuditorJobTitle" runat="server" Text="職稱"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AuditorJobTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorJobTitle") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_AuditorJobTitle" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorJobTitle") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>           

            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AuditorUnit" runat="server" Text='服務機構'></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AuditorUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorUnit") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddl_AuditorUnit" runat="server" CssClass="form-control" Width="200" AutoPostBack="true" OnSelectedIndexChanged="ddl_AuditorUnit_SelectedIndexChanged1" >
                    </asp:DropDownList>
                    <asp:Label ID="lb_AuditorUnit" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorUnit") %>' Visible="false"></asp:Label>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AuditorExperience" runat="server" Text="專長"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AuditorExperience" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorExperience") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_AuditorExperience" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorExperience") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>
                

            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AuditorEmail" runat="server" Text="電子信箱"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AuditorEmail" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorEmail") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_AuditorEmail" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorEmail") %>'></asp:TextBox>
                    <asp:Label ID="lb_AuditorEmail" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorEmail") %>'></asp:Label>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>

                

            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AcctPassword" runat="server" Text="密碼"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AcctPassword" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AcctPassword") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Label ID="lb_AcctPassword" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "AcctPassword") %>'></asp:Label>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="">
                <HeaderTemplate>
                    <asp:Label ID="tit_AuditorTel" runat="server" Text="電話"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lb_AuditorTel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorTel") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txt_AuditorTel" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorTel") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterStyle Width="10%" HorizontalAlign="Center" />
                <HeaderStyle Width="10%" HorizontalAlign="Center" />
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>
			
                                         
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                        <asp:Label ID="tit_DutyEdit" runat="server" Text="作業選項" ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbEdit" runat="server" CausesValidation="False"  Width="45px" CommandName="Edit" CssClass="btn btn-primary btn-sm" data-toggle="tooltip" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <i class="fa fa-floppy-o"></i>
                        <asp:LinkButton ID="lkb_SaveUpdate" runat="server" CausesValidation="false" CommandName="Update" Width="50px"  data-toggle="tooltip" Text="存檔"></asp:LinkButton>
                        &nbsp;
                        <i class="fa fa-ban"></i><asp:LinkButton ID="lkb_Cancel" runat="server"  CommandName="Cancel" Width="50px" data-toggle="tooltip"  Text='取消'></asp:LinkButton>
                    </EditItemTemplate>                    
                    <ItemStyle Width="10%"  HorizontalAlign="Center" />
                    <FooterStyle Width="10%"  HorizontalAlign="Center" />
                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                </asp:TemplateField>    

<%--                <asp:BoundField DataField="AuditorExperience" HeaderText="專長" ItemStyle-Width="100" />
                <asp:BoundField DataField="AuditorEmail" HeaderText="電子信箱" ItemStyle-Width="150" />
                <asp:BoundField DataField="AuditorTel" HeaderText="電話" ItemStyle-Width="100" />--%>
            </Columns>
            
        </asp:GridView>
            </div>
    </div>
</body>
</asp:Content>