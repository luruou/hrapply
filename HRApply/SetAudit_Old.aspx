<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetAudit_Old.aspx.cs" Inherits="ApplyPromote.SetAudit_Old" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type='text/javascript' src='http://code.jquery.com/jquery-1.5.2.js'></script>
<head runat="server">
   <style type="text/css">
        html
        {
            background-color: Silver;
        }
/* 
     "." 是為了讓<tag>內部的屬性
     "class、CssClass、StaticSelectedStyle-CssClass"
    可以引用。
*/

        .tabs
        {
            position: relative;
            top: 1px;
            left: 10px;
        }
        .tab
        {
            border: solid 1px black;
            background-color: #eeeeee;
            padding: 2px 10px;
            margin:2px;
        }
        .selectedTab
        {
            background-color: White;
            border-bottom: solid 1px white;
        }
        .tabContents
        {
            border: solid 1px black;
            padding: 10px;
            background-color: White;
           text-align: left;
       }
       .hiddencol
        {
            display:none;
        }
        .viscol
        {
            display:block;
        }
       </style>
    <title></title>
</head>

<body>
    <a href="ApplyerList.aspx">回到清單</a>
    <form id="formApplyView" runat="server" enctype="multipart/form-data">
    
    <div>
        <asp:TextBox ID="TBEmpSn" runat="server" value="1" AutoPostBack="True" Visible="false"></asp:TextBox>
        <a href="javascript:history.back()">回上一頁</a>
        <fieldset>
            <legend>簽核關卡設定</legend>

        <div class="tabContents">
    
                <asp:MultiView ID="MultiViewAudit" runat="server" ActiveViewIndex="0" >
                    <asp:View ID="ViewAuditorSetting" runat="server">
                       <asp:GridView ID="GVAuditExecute" runat="server"  HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                          AutoGenerateColumns="false" onrowdatabound="GVAuditExecute_RowDataBound">
                            <Columns>
                                <asp:TemplateField  HeaderText="No" ItemStyle-Width="100" >
                                <HeaderStyle CssClass="hiddencol" />
                                <ItemTemplate>
                                   <asp:TextBox ID="TextBoxAuditorSn" runat="server" Text='<%# Bind("ExecuteAuditorSn")%>'    ></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle CssClass="hiddencol" />
                                </asp:TemplateField>    
                                <asp:BoundField DataField="ExecuteStage" HeaderText="簽核階段" ItemStyle-Width="100" />
                                <asp:BoundField DataField="ExecuteStageNum" HeaderText="簽核階段Num" ItemStyle-Width="100" Visible="false" />
                                <asp:BoundField DataField="ExecuteStepNum" HeaderText="簽核子階段" ItemStyle-Width="100" Visible="false" />                                
                                <asp:BoundField DataField="ExecuteRoleName" HeaderText="簽核角色" ItemStyle-Width="100" />
                                <asp:TemplateField  HeaderText="簽核人員EmpId or Sn" ItemStyle-Width="100" >
                                <HeaderStyle CssClass="hiddencol" />
                                <ItemTemplate>
                                   <asp:TextBox ID="TextBoxAuditorSnEmpId" runat="server" Text='<%# Bind("ExecuteAuditorSnEmpId")%>'    ></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle CssClass="hiddencol" />
                                </asp:TemplateField>                                
                                <asp:TemplateField  HeaderText="簽核人員" ItemStyle-Width="100">
                                <ItemTemplate>
                                   <asp:TextBox ID="TextBoxAuditorName" runat="server" Text='<%# Bind("ExecuteAuditorName")%>'></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField  HeaderText="簽核人Email" ItemStyle-Width="100">
                                <ItemTemplate>
                                   <asp:TextBox ID="TextBoxAuditorEmail" runat="server" Text='<%# Bind("ExecuteAuditorEmail")%>'></asp:TextBox>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ExecuteBngDate" HeaderText="開放審核日期 起" ItemStyle-Width="100" />
                                <asp:BoundField DataField="ExecuteEndDate" HeaderText="~ 迄" ItemStyle-Width="100" />
                                <asp:BoundField DataField="ExecuteStatus" HeaderText="審核狀態" ItemStyle-Width="100" />
                            </Columns>
                        </asp:GridView>
                        <br/>
                        <asp:Label ID="MessageAudit" runat="server" Text=""  ForeColor="Red"></asp:Label>
                        <br/>
                        <br/><br/>
                        <asp:Button ID="BtnUpdateAuditor" runat="server" Text="更新" onclick="BtnUpdateAuditor_Click"/>
                        <asp:Button ID="BtnStartAuditor" runat="server" Text="啟動簽呈" onclick="BtnStartAuditor_Click" AutoPostBack="True" />
                        <br/><br/>
                        <br/>
                        <br/>
                    </asp:View>
                </asp:MultiView>
                
                <asp:Label Id="MessageLabelAll" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </fieldset>
    </div>
    </form>
</body>

</html>
