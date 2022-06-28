<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UnderTake.aspx.cs" Inherits="ApplyPromote.UnderTake" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<body>
    <div>                
        <div style="width:90%;margin:auto">
        <h2>承辦人設定</h2>
        <hr />
        <asp:DropDownList ID="AppEUnitNo" runat="server" AutoPostBack="True" onselectedindexchanged="AppEUnitNo_Click" class="form-control" ></asp:DropDownList>
        <br /><br />

        
                
		<asp:GridView ID="GVUuderTake" runat="server"  HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"  AutoGenerateColumns="False" 
            onrowdatabound="GVUnderTake_RowDataBound" class="table table-bordered">
            <Columns>
                <asp:BoundField DataField="UntName" HeaderText="單位名稱" ItemStyle-Width="100" />
				<asp:BoundField DataField="RoleName" HeaderText="角色" ItemStyle-Width="100" />						
                <asp:TemplateField  HeaderText="簽核人員EmpId or Sn" ItemStyle-Width="100" >
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxUnSn" runat="server" Text='<%# Bind("UnSn")%>'    ></asp:TextBox>
                    </ItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxAuditorSnEmpId" runat="server" Text='<%# Bind("EmpId")%>'    ></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>                                
                    <asp:TemplateField  HeaderText="簽核人員" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxAuditorName" runat="server" Text='<%# Bind("EmpName")%>' class="form-control"></asp:TextBox>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="電子信箱" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBoxAuditorEmail" runat="server" Text='<%# Bind("EmpEmail")%>' class="form-control"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
        <div style="float:right">
            <asp:Button ID="Update" runat="server" Text="修改" OnClick="BtnUpdate_OnClick" class="btn btn-primary" Width="100"/>
        </div>
        </div>
        
    </div>
</body>
</asp:Content>