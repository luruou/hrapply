<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountApply.aspx.cs" Inherits="ApplyPromote.AccountApply" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">
    function turnoffvalidate() {
        if (document.getElementById("<%=IsForeignCB.ClientID %>").checked == true) {
            ValidatorEnable(document.getElementById('<%=CustomValidator1.ClientID%>'), false);
            alert("您好：具外籍身分者，未來待完成聘任程序後，需先取得工作許可證，方可辦理報到。");
        }
    }

    function ValidateIDNO(source, arguments) {

        var objCtl = document.getElementById(source.controltovalidate);

        var strValue = objCtl.value;

        var strFst, strInt, strTwo;
        var strFstWd, strIntAll, strInt;
        var intANS = 0;
        var blnRtn = true;
        strFst = 'ABCDEFGHJKLMNPQRSTUVXYWZIO';
        strInt = '19876543211';
        strTwo = '12';

        strFstWd = strValue.substr(0, 1).toUpperCase();

        var intFst = parseInt(strFst.indexOf(strFstWd.substr(0, 1)), 10) + 10;

        if ((blnRtn) && (strValue.length != 10)) { blnRtn = false; }
        if ((blnRtn) && (intFst == 9)) { blnRtn = false; }
        if ((blnRtn) && (strTwo.indexOf(strValue.substr(1, 1)) == -1)) { blnRtn = false; }
        if (blnRtn) {
            strIntAll = intFst + strValue.substring(1, 10);
            for (var i = 0; i < 11; i++) {
                intANS += (parseInt(strInt.substr(i, 1), 10) * parseInt(strIntAll.substr(i, 1), 10));
            }
            if (intANS % 10 != 0) { blnRtn = false; }
        }

        if (!blnRtn) {
            blnRtn = gChkForeignID(strValue);
        }

        if ((!blnRtn) && (strValue != '')) {
            arguments.IsValid = false;
        }
        else {
            arguments.IsValid = true;
        }
    }

    //==================================================
    //功能名稱:Function gChkForeignID
    //功能用途:檢查是否外國人ID
    //參      數:    strVal 
    //==================================================
    function gChkForeignID(strCUST_ID) {

        if (gIsDigit(strCUST_ID.substring(0, 7)) == true && strCUST_ID.length >= 10)//ID前8碼必須為數字，ID長度必須大於10
        {
            var intMonth = new Number(strCUST_ID.substring(4, 6))
            var intDay = new Number(strCUST_ID.substring(6, 8))
            if (intMonth > 12)    //檢查月份是否合法
            {
                return false;
            }
            else {
                vntMonthDay = new Array(0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31)    //檢查日期是否合法
                if (intDay > vntMonthDay[intMonth]) {
                    return false;
                }
                else        //檢查後2碼是否為A~Z的文字
                {
                    var char_bln = true;
                    for (x = 0; x < 2; x++) {
                        if (x == 0)
                        { input01 = new String(strCUST_ID.substring(8, 9).toUpperCase()); }
                        else if (x == 1)
                        { input01 = new String(strCUST_ID.substring(9, 10).toUpperCase()); }

                        if (input01.charCodeAt(0) <= 64 || input01.charCodeAt(0) >= 91) {
                            char_bln = false;
                        }
                    }

                    if (!char_bln)
                        return false;
                    else
                        return true;

                }
            }

        }
        else {
            if (gChkForeignID_New(strCUST_ID)) {
                return true;
            } else {
                return false;
            }
        }
    }

    //檢查新的外國人統一證號 
    // Ex: AA01234563、FA12345689
    function gChkForeignID_New(strValue) {
        var strFst, strInt;
        var strFstWd, strIntAll;
        var intANS = 0, i = 0;
        var intFst1, intFst2, intNum1, intNum2, intNum3;
        var blnRtn = true;


        //檢查身份證格式是否符合 
        var re;
        re = /^[A-Za-z]{1}[A-Da-d]{1}\d{8}[A-Za-z0-9]{0,1}$/gi;

        if (!re.test(strValue)) {
            blnRtn = false;
        } else {
            strFst = 'ABCDEFGHJKLMNPQRSTUVWXYZIO';
            strInt = '19876543211';                            //特定數                
            strFstWd = strValue.toUpperCase();    //字母轉成大寫

            intFst1 = parseInt(strFst.indexOf(strFstWd.substr(0, 1)), 10) + 10;    // 取得第一個字母的對應代碼
            intFst2 = parseInt(strFst.indexOf(strFstWd.substr(1, 1)), 10) + 10;    // 取得第二個字母的對應代碼

            if (intFst1 > 29) {
                intNum1 = 3;
            } else if (intFst1 > 19) {
                intNum1 = 2;
            } else {
                intNum1 = 1;
            }

            intNum2 = intFst1 % 10;
            intNum3 = intFst2 - 10;

            strIntAll = intNum1.toString() + intNum2.toString() + intNum3.toString() + strValue.substring(2, 11);   // 字串組合 
            //alert( strIntAll );
            for (i = 0; i <= 10; i++) {
                intANS += (parseInt(strInt.substr(i, 1), 10) * parseInt(strIntAll.substr(i, 1), 10));
            }

            if (intANS % 10 != 0) {
                blnRtn = false;
            }
        }

        if (!blnRtn) {                        //身份證號有誤
            return false;
        } else {
            return true;
        }
    }

    //==================================================
    //功能名稱:Function gIsDigit
    //功能用途:檢查是否為數字
    //參    數: strVal 
    //  
    //傳 回 值:
    //   True    
    //   False  
    //==================================================
    function gIsDigit(strVal) {
        if (strVal == "")
            return false;
        for (var i = 0; i < strVal.length; i++) {
            if (strVal.charCodeAt(i) < 48 || strVal.charCodeAt(i) > 57 || strVal.charCodeAt(i) == 45)
                if (i != 0)
                    return false;
                else if (i == 0 && strVal.charCodeAt(0) != 45)
                    return false;
        }
        return true;
    }

</script>
<head id="Head1" runat="server">
    <title>臺北醫學大學-教師新聘升等系統</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <style type="text/css">
        body {
            font-family: 微軟正黑體;
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

        img {
            image-rendering: -moz-crisp-edges;
            image-rendering: -o-crisp-edges;
            image-rendering: -webkit-optimize-contrast;
            image-rendering: crisp-edges;
            -ms-interpolation-mode: nearest-neighbor;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
    <nav class="navbar navbar-expand-lg navbar-light bg-light" style="position: fixed; width: 100%;">
            <img src="image/Logo.png" width="130" style="float: left;" />
            <a class="navbar-brand" href="#" style="color: white; font-weight: bold; font-size: 23px">教師新聘升等系統</a>

            <div class="collapse navbar-collapse" id="navbarSupportedContent">
            </div>

            <ul class="nav navbar-nav float-xs-right">
                <%-- 放右上按鈕--%>
                <li class="nav-item"><a href='Default.aspx' target="_blank" style="color: white">申請首頁</a></li>
            </ul>
        </nav>
        <br />
        <br />        
        <br />
        <div class="container">
            <h2><b>【首次帳號申請】</b></h2>
            <hr />
            <div class="row">
                <div class="col-sm-4">
                    請輸入正確資訊：
                </div>
                <div class="col-sm-8">
                    <br />
                    <br />
                    <div class="form-horizontal" style="text-align: left">
                        Email帳號
                            <asp:TextBox ID="ApplyerEmail" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ApplyerEmailRFValidator" runat="server" ForeColor="Red" ControlToValidate="ApplyerEmail" ErrorMessage="請輸入Email帳號"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ForeColor="Red" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="ApplyerEmail" ErrorMessage="請確認格式Email正確!"></asp:RegularExpressionValidator><br /><br />
                        Email密碼
                            <asp:TextBox ID="ApplyerPwd" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmpPwdRVValidator" runat="server" ControlToValidate="ApplyerPwd" ErrorMessage="請輸入密碼"></asp:RequiredFieldValidator><br /><br />
                        密碼確認
                            <asp:TextBox ID="ApplyerPwdAgain" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="ApplyerPwd" ControlToValidate="ApplyerPwdAgain" ErrorMessage="密碼輸入錯誤！" ForeColor="Red"></asp:CompareValidator><br /><br />
                        身份證號/居留證號
                            <asp:TextBox ID="ApplyerId" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ApplyerId" ErrorMessage="請輸入身份證號"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" ControlToValidate="ApplyerId" runat="server" ErrorMessage="身分證號錯誤，請從新輸入！" ClientValidationFunction="ValidateIDNO"></asp:CustomValidator>
                        <br /><br />
                        是否有外籍身分
                            <asp:CheckBox ID="IsForeignCB" runat="server" onclientclick="turnoffvalidate()" /><br /><br />
                        <asp:Button ID="Check" runat="server" Text="申請" OnClientClick="turnoffvalidate()" OnClick="AccountApply_Click" CssClass="btn btn-success" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="footer">
            <label style="font-size: 14px; color: white; padding-top: 15px; text-align: center">
                &copy; 2019 <a href="http://oit.tmu.edu.tw" style="color: white">臺北醫學大學資訊處</a><br />
                操作問題請洽 02-66382736 #1600
                    <br />
                障礙排除請至<a href="http://glbsys.tmu.edu.tw/oitmanagement/login.aspx" style="color: white">資訊服務平台</a>
            </label>
        </div>
    </form>
</body>
</html>
