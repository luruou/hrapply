<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Language.ascx.cs" Inherits="UserControl_Language" %>

<ul class="navbar-nav flex-wrap ml-md-auto">
    <!--語系切換開始，用不到就註解或移除-->
    <li class="nav-item">
        <asp:LinkButton ID="lbtnZHTW" runat="server" OnClick="lbtnZHTW_Click" CssClass="nav-link p-2" rel="noopener"> <small class="ml-2">中文</small></asp:LinkButton>
    </li>
    <li class="nav-item">
        <asp:LinkButton ID="lbtnENUS" runat="server" OnClick="lbtnENUS_Click" CssClass="nav-link p-2" rel="noopener"> <small class="ml-2">ENG</small></asp:LinkButton>
    </li>
    <!--語系切換結束-->
</ul>
