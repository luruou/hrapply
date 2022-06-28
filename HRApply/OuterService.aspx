<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="OuterService.aspx.cs" Inherits="OuterService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="AddDiv"  style="width:60%;margin:auto;text-align:left">
    <asp:Button ID="AddService" runat="server" Text="新增服務機構" OnClick="AddService_Click" />
        <table ID="tb_AddService"  runat="server" visible="false" >
            <tr>
                <td>
                    <asp:Label ID="lb_AddService" runat="server" Text="服務機構:"/><asp:Label ID="Label1" runat="server" Text="(必填)" ForeColor="Red"/>
                </td>
                <td>                    
                    <asp:TextBox ID ="txt_AddService" runat="server"/>
                </td>
                <td>
                    <asp:Button ID="btn_Send" runat="server" Text="送出" OnClick="btn_Send_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="gv_OuterService" runat="server" 
        AutoGenerateColumns="False" CssClass="table table-condensed table-bordered" 
            HorizontalAlign="Center" Width="60%" HeaderStyle-Height="24"  
        GridLines="Horizontal" EmptyDataText="No Data" 
                onrowdatabound="gv_OuterService_RowDataBound" 
                onrowcancelingedit="gv_OuterService_RowCancelingEdit" 
                onrowediting="gv_OuterService_RowEditing" 
                onrowupdating="gv_OuterService_RowUpdating" 
        BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ForeColor="Black" >
        <rowstyle  />
            <alternatingrowstyle  />
            <Columns>
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                        <asp:Label ID="tit_ServiceNM" runat="server" Text='服務機構' ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lb_ServiceNM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorRealmName") %>'></asp:Label>   
                    </ItemTemplate>                       
                    <EditItemTemplate>
                        <asp:TextBox ID="txt_ServiceNM" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorRealmName") %>'></asp:TextBox>
                    </EditItemTemplate>                                
                    <FooterStyle Width="30%" HorizontalAlign="Center" />
                    <HeaderStyle Width="30%" HorizontalAlign="Center" />
                    <ItemStyle Width="30%" HorizontalAlign="Center" />
                </asp:TemplateField>    
                                         
                <asp:TemplateField HeaderText="">
                    <HeaderTemplate>
                        <asp:Label ID="tit_DutyEdit" runat="server" Text="作業選項" ></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lkbEdit" runat="server" CausesValidation="False"  CommandName="Edit" CssClass="btn btn-primary btn-sm" data-toggle="tooltip" Text="編輯"></asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <i class="fa fa-floppy-o"></i>
                        <asp:LinkButton ID="lkb_SaveUpdate" runat="server" CausesValidation="false" CommandName="Update"   data-toggle="tooltip" Text="存檔"></asp:LinkButton>
                        &nbsp;
                        <i class="fa fa-ban"></i><asp:LinkButton ID="lkb_Cancel" runat="server"  CommandName="Cancel"  data-toggle="tooltip"  Text='取消'></asp:LinkButton>
                    </EditItemTemplate>                    
                    <ItemStyle Width="10%"  HorizontalAlign="Center" />
                    <FooterStyle Width="10%"  HorizontalAlign="Center" />
                    <HeaderStyle Width="10%" HorizontalAlign="Center" />
                </asp:TemplateField>           
                
                <asp:TemplateField Visible="false">                
                    <ItemTemplate>   
                        <asp:Label ID="lb_ServiceSn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AuditorRealmSn") %>' ></asp:Label>
                    </ItemTemplate> 
                    <FooterStyle Width="4%"  HorizontalAlign="Center" />
                    <HeaderStyle Width="4%" HorizontalAlign="Center" />
                    <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                             
                </asp:TemplateField>
                </Columns>
        </asp:GridView>
</asp:Content>

