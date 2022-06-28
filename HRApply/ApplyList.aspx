<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ApplyList.aspx.cs" Inherits="ApplyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <h2><b><%=Resources.Language.ApplyList_Label_TitleDescription%></b></h2>

        <asp:Label runat="server" Text="<%$ Resources:Language ,ApplyList_Label_Rule%>"
            Font-Bold="True" Font-Size="Medium" ForeColor="black" Style="text-align: left;" /><br />
        <br />
        <asp:Label runat="server" Text="<%$ Resources:Language ,ApplyList_Label_OpenDate%>"
            Font-Bold="True" Font-Size="Medium" ForeColor="red" Style="text-align: left;" />
        <div style="float: right">
            <asp:Button ID="BtnApplyMore" runat="server" Text="<%$ Resources:Language ,ApplyList_Controls_Button_CreateApply%>" OnClick="BtnApplyMore_Click" CssClass="btn btn-success"></asp:Button>
            <asp:Button ID="BtnPromoteMore" runat="server" Text="<%$ Resources:Language ,ApplyList_Controls_Button_CreatePromote%>" OnClick="BtnPromoteMore_Click" Visible="false" CssClass="btn btn-success"></asp:Button>
        </div>
        <br />
        <hr />
        <asp:Label runat="server" ID="lb_prompt" ForeColor="Red" Font-Size="20" Font-Bold="true" Visible="false" />

        <br />

        <asp:GridView ID="GVAuditData" runat="server" HeaderStyle-BackColor="#005ab5" HeaderStyle-ForeColor="White" Width="100%"
            AutoGenerateColumns="false" OnRowDataBound="GVApplyData_RowDataBound" CssClass="table-hover" Style="text-align: center">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_SerialNo%>" Visible="false">
                    <ItemTemplate>
                        <%#Container.DisplayIndex + 1%>
                    </ItemTemplate>
                    <HeaderStyle Wrap="False" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Language ,ApplyList_Controls_Button_CreatePromote%>" Visible="true">
                    <HeaderStyle />
                    <ItemTemplate>
                        <asp:Label ID="LbAppSn" runat="server" Text='<%# Bind("AppSn")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>
                <asp:BoundField DataField="UntName" HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_AppSn%>" ItemStyle-Width="150" />
                <asp:BoundField DataField="EmpNameCN" HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Unt%>" ItemStyle-Width="100" />
                <asp:BoundField DataField="AppJobAttribute" HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_JobAttr%>" ItemStyle-Width="100" />
                <asp:BoundField DataField="AppJobTitle" HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_JobTitle%>" ItemStyle-Width="100" />
                <asp:BoundField DataField="AppAttribute" HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Attr%>" ItemStyle-Width="100" />
                <asp:BoundField DataField="AppKind" HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Kind%>" ItemStyle-Width="100" Visible="false" />
                <asp:BoundField DataField="AppStage" HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Stage%>" ItemStyle-Width="100" />



                <asp:TemplateField HeaderText="Email" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbEmail" runat="server" Text='<%# Bind("AppEmail")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Tels%>" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbTels" runat="server" Text='<%# Bind("AppTels")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Birth%>" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbBirthday" runat="server" Text='<%# Bind("AppBirthDay")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Idno%>" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbEmpIdno" runat="server" Text='<%# Bind("AppEmpIdno")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_Build%>" Visible="true">
                    <HeaderStyle />
                    <ItemTemplate>
                        <asp:Label ID="LbAppBuildDate" runat="server" Text='<%# Bind("AppBuildDate")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="<%$ Resources:Language ,ApplyList_Controls_GV_Header_View%>">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperLinkApplyData" runat="server" class="btn btn-warning" Width="100"
                            Style="color: white; background-color: #2894ff"><%=Resources.Language.ApplyList_Controls_GV_Button_Enter%></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AppStage1" ItemStyle-Width="100" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbAppStage" runat="server" Text='<%# Bind("AppStage1")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AppStep" ItemStyle-Width="100" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbAppStep" runat="server" Text='<%# Bind("AppStep")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>



                <asp:TemplateField HeaderText="UntName" ItemStyle-Width="100" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbUntName" runat="server" Text='<%# Bind("UntName")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AppJobAttribute" ItemStyle-Width="100" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbAppJobAttribute" runat="server" Text='<%# Bind("AppJobAttribute")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AppJobTitle" ItemStyle-Width="100" Visible="false">
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemTemplate>
                        <asp:Label ID="LbAppJobTitle" runat="server" Text='<%# Bind("AppJobTitle")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle CssClass="hiddencol" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

