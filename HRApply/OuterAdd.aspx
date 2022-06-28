<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="OuterAdd.aspx.cs" Inherits="OuterAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="body" style="width:60%;margin:auto;text-align:left">
        <table id="tb_outerSchool" runat="server" >
            <tr>
                <td>
                    <asp:Label ID="lb_title" runat="server" Text="委員姓名(或Email):"/><asp:Label ID="Label5" runat="server" Text="(必填)" ForeColor="Red"/>
                </td>
                <td>                    
                    <asp:TextBox ID ="txt_search" runat="server"/> <asp:Button ID="btn_Serach" runat="server" Text="查詢" OnClick="btn_Serach_Click" />
                </td>
            </tr>
            <tr>
                <td><br></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_outerNM" runat="server" Text="姓名:"/><asp:Label ID="Label1" runat="server" Text="(必填)" ForeColor="Red"/>
                </td>
                <td>                    
                    <asp:TextBox ID ="txt_outerNM" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_outerEmail" runat="server" Text="E-mail:"/><asp:Label ID="Label3" runat="server" Text="(必填)" ForeColor="Red"/>
                </td>
                <td> 
                    <asp:TextBox ID ="txt_outerEmail" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_service" runat="server" Text="服務機構:"/><asp:Label ID="Label2" runat="server" Text="(必選)" ForeColor="Red"/>
                </td>
                <td>
                    <asp:DropDownList id="ddl_service" runat="server"/>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_dept" runat="server" Text="系所/單位:"/>
                </td>
                <td>
                    <asp:TextBox ID ="txt_dept" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_jobNM" runat="server" Text="職稱:"/>
                </td>
                <td>
                    <asp:TextBox ID ="txt_jobNM" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_telphone" runat="server" Text="電話:"/>
                </td>
                <td>
                    <asp:TextBox ID ="txt_telphone" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_cellphone" runat="server" Text="行動電話:"/>
                </td>
                <td>
                    <asp:TextBox ID ="txt_cellphone" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_Experience" runat="server" Text="學術專長:"/>
                </td>
                <td>
                    <asp:TextBox ID ="txt_Experience" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_AppSn" runat="server" Text="審核聘單編號:"/><asp:Label ID="Label4" runat="server" Text="(必填)" ForeColor="Red"/>
                </td>
                <td>
                    <asp:TextBox ID ="txt_AppSn" runat="server"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_Add" runat="server" Text="新增" OnClick="btn_Add_Click" />
                </td>
            </tr>
        </table>
    </div>
    <br/>
    <asp:GridView ID="gv_OuterAdd" runat="server" AutoGenerateColumns="True" CssClass="table table-condensed table-bordered" HorizontalAlign="Center" Width="100%" HeaderStyle-Height="24"  
        GridLines="Horizontal" EmptyDataText="No Data"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" EnableModelValidation="True" ForeColor="Black" />
    
    <script type="text/javascript">
        function unLoad() {
            window.opener.location.reload();
        }
        </script>
</asp:Content>

