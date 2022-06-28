<%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" EnableEventValidation="false" EnableSessionState="true" AutoEventWireup="true" CodeFile="ManageList.aspx.cs" Inherits="ApplyPromote._ManageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btn1").click(function () {
                //alert("click");
                debugger;
                $("#ContentPlaceHolder1_searchCondition").slideToggle("slow");
            });
        });
    </script>
<style>

        img {
            image-rendering: -moz-crisp-edges;
            image-rendering: -o-crisp-edges;
            image-rendering: -webkit-optimize-contrast;
            image-rendering: crisp-edges;
            -ms-interpolation-mode: nearest-neighbor;
        }
</style>
    
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div class="container">
            <asp:Label ID="AcctRoleName" Visible="false" runat="server" />&nbsp;
        <asp:Label ID="LoginName" runat="server" Visible="false"></asp:Label>
            <asp:CheckBox ID="CBNowAudit" runat="server"  />
            <asp:Label ID="LBNowAudit" runat="server"><font color="red">僅顯示現在需審核資料</font></asp:Label>
            <div style="font-size: 16px; background-color: #f0f0f0; border-radius: 10px;">
                <div style="padding: 10px">
                    <div class="row" id="SelUnit" runat="server" visible="false">
                        <div class="col-lg-2">
                            <asp:Label runat="Server">請選擇欲考評的單位</asp:Label>
                        </div>
                        <div class="col-lg-10">
                            <asp:DropDownList ID="approveUnt" runat="server"  class="form-control" Width="15%">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" id="TableRow0" runat="Server">
                        <div class="col-lg-2">請選學年學期</div>
                        <div class="col-lg-10">
                            <asp:DropDownList ID="SelYear" runat="server" class="form-control"  AutoPostBack="true" Width="15%">
                                <asp:ListItem Value="111" selected="true">111</asp:ListItem>
                                <asp:ListItem Value="110">110</asp:ListItem>
                                <asp:ListItem Value="109">109</asp:ListItem>
                                <asp:ListItem Value="108">108</asp:ListItem>
                                <asp:ListItem Value="107">107</asp:ListItem>
                                <asp:ListItem Value="106">106</asp:ListItem>
                                <asp:ListItem Value="105">105</asp:ListItem>
                                <asp:ListItem Value="104">尚未派發學年學期</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="SelSemester" runat="server" class="form-control"  Width="15%">
                                <asp:ListItem Value="2">2</asp:ListItem>
                                <asp:ListItem Value="1" selected="true">1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" id="SelectTableHR" runat="server" visible="false">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-10"></div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">篩選條件</div>
                        <div class="col-lg-10">
                            <asp:DropDownList ID="SelectMethod" runat="server" class="form-control" Width="15%">
                                <asp:ListItem Value="all">所有聘單</asp:ListItem>
                                <asp:ListItem Value="1">申請中</asp:ListItem>
                                <asp:ListItem Value="2">審核中</asp:ListItem>
                                <asp:ListItem Value="3">退回本人</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="SelectKind" runat="server" class="form-control" Width="15%">
                                <asp:ListItem Value="1">新聘</asp:ListItem>
                                <asp:ListItem Value="2">升等</asp:ListItem>
                            </asp:DropDownList>
                            <asp:HiddenField ID="SelHidden1" runat="server"></asp:HiddenField>
                            <%--<input type="button" id="btn1" style="background-color: #f0f0f0;" class value="更多篩選條件"/>--%>
                            <button id="btn1" type="button" class="btn btn-default" aria-label="Left Align" title="更多篩選條件">
                                <span class="fas fa-angle-down" aria-hidden="true"></span>
                            </button>
                            <hr />
                        </div>
                    </div>
                    <%--                    <a id="btn1" href="" style="border-bottom: 1px solid #1c4a8e; padding:5px 0;">更多篩選條件</a>
                    <span class="glyphicon glyphicon-triangle-bottom" style="float:right; cursor: pointer;"></span>--%>
                    <div id="searchCondition" runat="server" style="display:none" >
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-10">
                                <asp:DropDownList ID="SelConjunct1" runat="server" class="form-control" Width="15%"
                                    OnSelectedIndexChanged="SelConjunct1_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="-">─</asp:ListItem>
                                    <asp:ListItem Value="and">And</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="SelItem1" runat="server" class="form-control" Width="15%"
                                    OnSelectedIndexChanged="SelConjunct1_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="unt_name_full">聘任單位</asp:ListItem>
                                    <asp:ListItem Value="b.EmpIdno">申請身分證</asp:ListItem>
                                    <asp:ListItem Value="b.EmpNameCN">姓名</asp:ListItem>
                                    <asp:ListItem Value="AppJobTypeNo">專兼任別</asp:ListItem>
                                    <asp:ListItem Value="AppJobTitleNo">應聘等級</asp:ListItem>
                                    <asp:ListItem Value="AppAttributeNo">審查性質</asp:ListItem>
                                    <asp:ListItem Value="AppStage">申請狀態</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="SelChoose1" runat="server" Visible="false" class="form-control" Width="15%">
                                </asp:DropDownList>
                                <asp:TextBox ID="TxtBox1" runat="server" class="form-control" Visible="false" placeholder="請輸入查詢資料" Width="15%"></asp:TextBox>
                                <asp:HiddenField ID="SelHidden2" runat="server"></asp:HiddenField>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-10">

                                <asp:DropDownList ID="SelConjunct2" runat="server" OnSelectedIndexChanged="SelConjunct2_SelectedIndexChanged" AutoPostBack="true" class="form-control" Width="15%">
                                    <asp:ListItem Value="-">─</asp:ListItem>
                                    <asp:ListItem Value="and">And</asp:ListItem>
                                    <asp:ListItem Value="or">Or</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="SelItem2" runat="server" OnSelectedIndexChanged="SelConjunct2_SelectedIndexChanged" AutoPostBack="true" class="form-control" Width="15%">
                                    <asp:ListItem Value="unt_name_full">聘任單位</asp:ListItem>
                                    <asp:ListItem Value="b.EmpIdno">申請身分證</asp:ListItem>
                                    <asp:ListItem Value="b.EmpNameCN">姓名</asp:ListItem>
                                    <asp:ListItem Value="AppJobTypeNo">專兼任別</asp:ListItem>
                                    <asp:ListItem Value="AppJobTitleNo">應聘等級</asp:ListItem>
                                    <asp:ListItem Value="AppAttributeNo">審查性質</asp:ListItem>
                                    <asp:ListItem Value="AppStage">申請狀態</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="SelChoose2" runat="server" Visible="false" class="form-control" Width="15%">
                                </asp:DropDownList>
                                <asp:TextBox ID="TxtBox2" runat="server" Visible="false" placeholder="請輸入查詢資料" class="form-control" Width="15%"></asp:TextBox>
                                <asp:HiddenField ID="SelHidden3" runat="server"></asp:HiddenField>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-2"></div>
                            <div class="col-lg-10">
                                <asp:DropDownList ID="SelConjunct3" runat="server" OnSelectedIndexChanged="SelConjunct3_SelectedIndexChanged" AutoPostBack="true" class="form-control" Width="15%">
                                    <asp:ListItem Value="-">─</asp:ListItem>
                                    <asp:ListItem Value="and">And</asp:ListItem>
                                    <asp:ListItem Value="or">Or</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="SelItem3" runat="server" OnSelectedIndexChanged="SelConjunct3_SelectedIndexChanged" AutoPostBack="true" class="form-control" Width="15%">
                                    <asp:ListItem Value="b.EmpIdno">申請身分證</asp:ListItem>
                                    <asp:ListItem Value="b.EmpNameCN">申請人姓名</asp:ListItem>
                                    <asp:ListItem Value="AppAttributeNo">送審類型</asp:ListItem>
                                    <asp:ListItem Value="unt_name_full">單位</asp:ListItem>
                                    <asp:ListItem Value="AppJobTitleNo">送審等級</asp:ListItem>
                                    <asp:ListItem Value="AppJobTypeNo">專兼任別</asp:ListItem>
                                    <asp:ListItem Value="AppStage">申請狀態</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="SelChoose3" runat="server" Visible="false" class="form-control" Width="15%">
                                </asp:DropDownList>
                                <asp:TextBox ID="TxtBox3" runat="server" Visible="false" placeholder="請輸入查詢資料" class="form-control" Width="15%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-2"></div>
                <div class="col-lg-10">
                    <asp:Button ID="SearchGo" runat="server" Text="查詢" OnClick="SearchGo_OnClick" class="btn btn-primary" Width="120px" />
                    <asp:Button ID="BtnExport" runat="server" Text="匯出下方資料" OnClick="BtnExport_OnClick" class="btn btn-success" Width="120px" />
                    <asp:Label ID="lb_btnText" runat="server" Text="(含email,聯絡方式)" />
                </div>
            </div>

            <table>
                <tbody>
                    <tr>
                        <td>
                            <asp:Button ID="BtnOutput" runat="server" CssClass="hiddencol" Text="匯出教師一覽表" OnClick="BtnOutput_OnClick" Visble="false" class="btn btn-success" Width="100%" /></td>
                        <td>
                            <asp:Label ID="LbOutput" CssClass="hiddencol" runat="server">(不包括申請填寫中資料)</asp:Label></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>

        </div>

        <div style="width: 90%; margin: auto">
            <h3>
                <asp:Label ID="LbSelectMethod" runat="server" Text="審核中的資料"></asp:Label></h3>
            <hr />
            <div style="text-align: right">
                <asp:Label ID="lb_count" Visible="false" runat="server" />
            </div>
            <asp:GridView ID="GVAuditData" runat="server" HeaderStyle-BackColor="#0072e3" HeaderStyle-ForeColor="White"
                AutoGenerateColumns="False" OnRowDataBound="GVApplyData_RowDataBound" EnableModelValidation="True" Style="text-align: center" class="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="CollegeName" HeaderText="學院" ItemStyle-Width="150" />
                    <asp:BoundField DataField="DeptName" HeaderText="系所" ItemStyle-Width="150" />
                    <asp:BoundField DataField="UntName" HeaderText="聘任單位" ItemStyle-Width="150" />
                    <asp:BoundField DataField="EmpNameCN" HeaderText="姓名" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AppJobAttribute" HeaderText="專兼任別" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AppJobTitle" HeaderText="應聘等級" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AppAttribute" HeaderText="審查性質" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AppKind" HeaderText="新聘升等" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AppStage" HeaderText="申請狀態" ItemStyle-Width="100" />

                    <asp:TemplateField HeaderText="Email" ItemStyle-Width="100">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="LbEmail" runat="server" Text='<%# Bind("AppEmail")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="連絡電話" ItemStyle-Width="100">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="LbTels" runat="server" Text='<%# Bind("AppTels")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="生日" ItemStyle-Width="100">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="LbBirthday" runat="server" Text='<%# Bind("AppBirthDay")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="身分證號" ItemStyle-Width="100">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="LbEmpIdno" runat="server" Text='<%# Bind("AppEmpIdno")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="聘單單號" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="lb_AppSn" runat="server" Text='<%# Bind("AppSn")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField  HeaderText=" 建單時間" ItemStyle-Width="110" >
                        <ItemTemplate>  
                           <asp:Label ID="lb_AppBuildDate" runat="server" Text='<%# Bind("AppBuildDate")%>'    ></asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField  HeaderText="最後修改時間" ItemStyle-Width="120" >
                        <ItemTemplate>  
                           <asp:Label ID="lb_AppModifyDate" runat="server" Text='<%# Bind("AppModifyDate")%>'    ></asp:Label></ItemTemplate></asp:TemplateField>--%>
                    <asp:TemplateField HeaderText=" 建單時間/最後修改時間" ItemStyle-Width="250">
                        <ItemTemplate>
                            <asp:Label ID="lb_AppBuildDate" runat="server" Text='<%# Bind("AppBuildDate")%>'></asp:Label><br />
                            <asp:Label ID="lb_AppModifyDate" runat="server" Text='<%# Bind("AppModifyDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="檢視&設定&簽核" ItemStyle-Width="120">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLinkApplyData" runat="server" class="btn btn-primary" Style="color: white">進入</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EmpSn" ItemStyle-Width="100">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="LbEmpSn" runat="server" Text='<%# Bind("EmpSn")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="AuditRecord" ItemStyle-Width="100">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="LbAuditRecord" runat="server" Text='<%# Bind("AuditRecord")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AppSn" ItemStyle-Width="100" Visible="false">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="LbAppSn" runat="server" Text='<%# Bind("AppSn")%>'></asp:Label>
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
                </Columns>
            </asp:GridView>
            <div style="text-align: left">
                <font color="red">
                    <asp:Label ID="ApplyDataMessage" runat="server" ForeColor="red" Text=""></asp:Label></font>
            </div>
            <asp:GridView ID="GVOutputData" runat="server" HeaderStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black"
                AutoGenerateColumns="false" OnRowDataBound="GVOutputData_RowDataBound" Visible="false">
                <Columns>
                    <asp:BoundField HeaderText="序號"></asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="姓名" ItemStyle-Width="100" />
                    <asp:BoundField DataField="UntName" HeaderText="應徵單位" ItemStyle-Width="100" />
                    <asp:BoundField DataField="TitleName" HeaderText="應徵職稱" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AttrName" HeaderText="職別" ItemStyle-Width="100" />
                    <asp:BoundField DataField="NowJob" HeaderText="現職" ItemStyle-Width="100" />
                    <asp:BoundField DataField="TotalScore" HeaderText="研究論文積分" ItemStyle-Width="100" />
                    <asp:BoundField DataField="Degree" HeaderText="最高學歷" ItemStyle-Width="100" />
                    <asp:BoundField DataField="Expert" HeaderText="學術研究專長" ItemStyle-Width="100" />
                    <asp:BoundField DataField="Sex" HeaderText="性別" ItemStyle-Width="100" />
                    <asp:BoundField DataField="BirthDay" HeaderText="年齡" ItemStyle-Width="100" />
                    <asp:BoundField DataField="AuditWay" HeaderText="部定資格/審查方式" ItemStyle-Width="100" />
                </Columns>
            </asp:GridView>
            <asp:GridView ID="GVOutputDataForHR" runat="server" HeaderStyle-BackColor="Yellow" HeaderStyle-ForeColor="Black"
                AutoGenerateColumns="true" OnRowDataBound="GVOutputDataForHR_RowDataBound" Visible="false">
            </asp:GridView>
        </div>
    </div>

</asp:Content>
