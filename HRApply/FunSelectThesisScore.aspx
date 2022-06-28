<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FunSelectThesisScore.aspx.cs" Inherits="ApplyPromote.FunSelectThesisScore" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <link href="css/tabs.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css" integrity="sha384-50oBUHEmvpQ+1lW4y57PTFmhCaXp0ML5d60M1M7uH2+nqUivzIebhndOJK28anvf" crossorigin="anonymous">
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/jquery.validate.min.js" type="text/javascript"></script>
    <title></title>
    
    <style type="text/css">
        .auto-style1 {
            height: 98px;
        }
        
        body {
            font-family: 微軟正黑體;
            z-index:1000;
        }

        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: #005ab5;
            color: white;
            text-align: center;
        }

        .bold {
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="formSelectSCIPaper" runat="server">
    <div>
        <div>
             <asp:Label ID="lb_tittle" runat="server"  style="background-color:#FFFF6F;" ForeColor="red">※選取加入的資料將加在「表(B)研究論文積分統計」下方</asp:Label></div>
        <asp:DropDownList ID="SCIPaperYear" runat="server" 
            onselectedindexchanged="SCIPaperYear_SelectedIndexChanged" 
            AutoPostBack="True" Width="100px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:GridView ID="GVThesisScore" runat="server" AutoGenerateColumns="False" 
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                CellPadding="4" CssClass="table table-bordered table-condensed" 
                DataKeyNames="ThesisSn" DataSourceID="DSThesisScore" 
                EnableModelValidation="True" ForeColor="Black" GridLines="Horizontal"                  
                Wrap="True">
                <Columns>
                    <asp:TemplateField>
                        <headertemplate> 
                            <asp:CheckBox ID="CheckAll" runat="server" onclick="javascript: SelectAllCheckboxes(this);"  Text="全選/取消" ToolTip="按一次全選，再按一次取消全選" /> 
                        </headertemplate>
                        <itemtemplate> 
                            <asp:CheckBox ID="CheckBoxSel" runat="server" Text=""/> 
                        </itemtemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="No" ItemStyle-Width="100">
                        <HeaderStyle CssClass="hiddencol" />
                        <ItemTemplate>
                            <asp:Label ID="lblSnNo" runat="server" Text='<%#Bind("SnNo")%>'></asp:Label></ItemTemplate><ItemStyle CssClass="hiddencol" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmpSn" HeaderText="EmpSn" SortExpression="EmpSn" 
                        Visible="False" />
                    <asp:BoundField DataField="SnNo" HeaderText="序號" SortExpression="SnNo" />
                    <asp:BoundField DataField="RRNo" HeaderText="成果類別代碼" SortExpression="RRNo" />
                    <asp:BoundField DataField="ThesisResearchResult" HeaderText="代表性研究成果名稱</br>作者本人「+」，通訊作「*」，貢獻相同作者「#」 " 
                        HtmlEncode="false" ItemStyle-HorizontalAlign="Left" 
                        SortExpression="ThesisResearchResult" />
                    <asp:BoundField DataField="ThesisName" HeaderText="ThesisName"  
                        Visible="False" />
                    <asp:BoundField DataField="ThesisPublishYearMonth" HeaderText="論文發表年月" 
                        SortExpression="ThesisPublishYearMonth" />
                    <asp:BoundField DataField="ThesisC" HeaderText="論文性質分數(C)" 
                        SortExpression="ThesisC" />
                    <asp:BoundField DataField="ThesisJ" HeaderText="刊登雜誌分類分數(J)" 
                        SortExpression="ThesisJ" />
                    <asp:BoundField DataField="ThesisA" HeaderText="作者排名分數(A)" 
                        SortExpression="ThesisA" />
                    <asp:BoundField DataField="ThesisTotal" HeaderText="總分數" HeaderStyle-Width="5%"
                        SortExpression="ThesisTotal" />

                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="DSThesisScore" runat="server" 
                ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>" 
                SelectCommand="SELECT ThesisSn, SnNo, EmpSn,RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, ThesisUploadName, ThesisJournalRefCount FROM ThesisScoreTemp Where EmpSn = @EmpSn ORDER BY SnNo">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="EmpSn" SessionField="EmpSn" />
                </SelectParameters>

            </asp:SqlDataSource>
    </div>
    <asp:TextBox ID="TextBoxEmpEmail" runat="server" style="display:none"></asp:TextBox>
    <asp:Button ID="BtnSend" runat="server" OnClick="BtnSelect_Click" onClientClint="selectData()" class="btn btn-primary"  Text="選取加入"></asp:Button>
    </form>
</body>
<script type="text/javascript" language="javascript">

    function selectData() {

        //addDataToSession(strAuditorId); //用不到
        window.opener.location.reload();
        window.close();
    }
    
     function addDataToSession(strAuditorId){    
        var urltxt = "http://10.1.1.11/OutAudit/FuncSelectAuditor.asp?empId=" + strAuditorId ;  //把值傳回給自已  
	    var objHTTP ;       
	    objHTTP = new ActiveXObject("Microsoft.XMLHTTP") ;       
	    objHTTP.open("get",urltxt,false) ;    
	    objHTTP.setRequestHeader("CONTENT-TYPE","text/html; charset=big5") ;       
	    objHTTP.send() ;  
	    document.write(objHTTP.responseText)
	}
	
	function SelectAllCheckboxes(spanChk) {
	    elm = document.forms[0];
	    for (i = 0; i <= elm.length - 1; i++) {
	        if (elm[i].type == "checkbox" && elm[i].id != spanChk.id) {
	            if (elm.elements[i].checked != spanChk.checked)
	                elm.elements[i].click();
	        }
	    }
	}
</script> 

</html>
