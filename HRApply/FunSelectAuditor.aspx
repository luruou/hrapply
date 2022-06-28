<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FunSelectAuditor.aspx.cs" Inherits="ApplyPromote.FunSelectAuditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type='text/javascript' src='http://code.jquery.com/jquery-1.5.2.js'></script>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="formSelectAuditor" runat="server">
    <div>
        <asp:DropDownList ID="DeptLevelOne" runat="server" 
            onselectedindexchanged="DeptLevelOne_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="DeptLevelTwo" runat="server" 
            onselectedindexchanged="DeptLevelTwo_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="DeptLevelThree" runat="server" 
            onselectedindexchanged="DeptLevelThree_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <asp:DropDownList ID="UnitEmpList" runat="server" 
            onselectedindexchanged="UnitEmpList_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
        <br />
    </div>
    <asp:TextBox ID="TextBoxEmpEmail" runat="server" style="display:none"></asp:TextBox>
    <%--<button id="BtnSelect" onclick="selectData()">選取</button>--%>
            <asp:Button runat="server" CssClass="btn btn-default" ID="BtnSelect" OnClick="BtnSelect_Click" Text="選取" />
    </form>
</body>
<script type="text/javascript" language="javascript">

    function selectData() {
        var ObjAuditorId = "<%=Request["ObjId"].ToString()%>"; 
        var ObjAuditorName = "<%=Request["ObjName"].ToString()%>"; 
        var ObjAuditorEmail = "<%=Request["ObjEmail"].ToString()%>"; 
        //var AttributeNo = "<%=Request["AttributeNo"].ToString()%>"; 

        var emp = this.document.getElementById("UnitEmpList");
        //var empemail = this.document.getElementById("TextBoxEmpEmail");
        var empemail = emp.options[emp.selectedIndex].value;
        //var strAuditorId = emp.options[emp.selectedIndex].value;
        var strAuditorName = emp.options[emp.selectedIndex].text;
        strAuditorName = strAuditorName.replace(empemail, '');

        console.log(emp);
        console.log(empemail);
        console.log(strAuditorName);
        alert('');
        

        //opener.document.getElementById(ObjAuditorId).value = strAuditorId;
        //opener.document.getElementById(ObjAuditorName).value =  strAuditorName;
        //opener.document.getElementById(ObjAuditorEmail).value = empemail.value;
      
        //window.close();
     
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
</script> 

</html>
