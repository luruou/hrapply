<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Defaultold.aspx.cs" Inherits="ApplyPromote.Apply" %>


<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript">
    function turnoffvalidate() {


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
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible" />
    <link href="css/bootstrap/css/bootstrap.css" rel="stylesheet" />
    <link href="css/bootstrap/css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="css/bootstrap/css/extraStyle.css" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            width: 231px;
        }
    </style>
</head>
<body style="line-height: 1.5em; background-color: rgb(230, 233, 236);">

    <form id="formAccount" runat="server">
        <div class="container">

            <div class="page-header">
                <h3>
                    <asp:Image ID="img_logo" ImageUrl="~/image/tmulogo.png" runat="server" Height="40px" Width="40px" />&nbsp;歡迎使用『教師聘任升等作業系統』</h3>
                <h4>【新聘升等申請】入口</h4>
            </div>

            <div class="signin_box">
                <div class="row-fluid">
                    <%--left content--%>
                    <div class="span6">
                        <div class="row-fluid">
                            <div class="span10">
                                <a href="Default.aspx" class="btn btn-block btn-info" type="button">新聘升等申請</a>
                            </div>
                        </div>
                        <br />
                        <div class="row-fluid">
                            <div class="span10">
                                <a href="ManageLogin.aspx" class="btn btn-block btn-warning" type="button">學術審查</a>
                            </div>
                        </div>
                        <br />
                        <div class="row-fluid">
                            <div class="span10">
                                <a href="AuditLogin.aspx" class="btn btn-block btn-success" type="button">外部審查委員</a>
                            </div>
                        </div>


                        <div class="row-fluid ">
                            <h4>注意事項</h4>
                            <ul>
                                <li>首次使用系統，請先<a href="AccountApply.aspx">申請帳號</a>
                                </li>
                                <li>非北醫教職員忘記密碼，請按此<a href="GetPassword.aspx">忘記密碼</a>
                                </li>
                                <li>北醫教職員忘記密碼，請按此<a href="http://www.tmu.edu.tw/v3/app/super_pages.php?ID=research&Sn=13">重新申請</a>
                                </li>
                                <li><a href="https://docs.google.com/document/d/1V0hxLFDTGiwJXmgwBPq_sJOjHB-Vh88jUEEBKSUgJ9s/pub">[系統使用說明]</a>
                                </li>
                                <li>本系統閒置時間不得超過20分鐘</li>
                                <li>
                                    <h5>系統資料如有問題，請聯繫：</h5>
                                    資料&操作問題：人資處 林小姐 2028<br />
                                    系統問題：資訊處 (02)6638-2736 #1601&nbsp; <a class="icon-envelope" href="mailto:up_group@tmu.edu.tw?Subject=新聘升等系統問題"></a></li>

                            </ul>
                        </div>
                    </div>
                    <%--right content--%>
                    <div class="span6">
                        <div class="right-content">
                            <div class="login-box">
                                <h1>登入Login</h1>
                                <div class="form-horizontal">
                                    <div class="control-group">
                                        <div>Email帳號</div>
                                        <div>
                                            <asp:TextBox ID="ApplyerEmail" runat="server" onFocus="ApplyerEmail_Click" AutoPostBack="true" TabIndex="1"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="ApplyerEmailRFV" runat="server" ControlToValidate="ApplyerEmail" ErrorMessage="請輸入Email帳號"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="control-group">
                                        <div>密碼</div>
                                        <div>
                                            <asp:TextBox ID="ApplyerPwd" runat="server" TextMode="password" TabIndex="2"></asp:TextBox><br />
                                            <asp:RequiredFieldValidator ID="EmpPwdRFV" runat="server" ControlToValidate="ApplyerPwd" EnableClientScript="true" SetFocusOnError="true" ErrorMessage="請輸入密碼"></asp:RequiredFieldValidator>
                                            <br />
                                        </div>

                                        <%--<div>學年度</div>
                                        <div>
                                                
                                            <asp:DropDownList ID="DDL_Smtr" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Smtr_Changed" >
                                                <asp:ListItem Value="106" Text="106" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="105" Text="105"></asp:ListItem>
                                            </asp:DropDownList>
                                                
                                        </div>
                                        <div>學期</div>
                                        <div>                                                
                                            <asp:DropDownList ID="DDL_Semester" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Semester_Changed" >
                                                <asp:ListItem Value="1" Text="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>

                                        <div class="control-group">
                                            <div class="controls">
                                                <asp:Button ID="Check" runat="server" Text="登入" OnClick="Check_Click" CssClass="btn" />

                                            </div>

                                        </div>

                                    </div>



                                    <table id="SelectKind" runat="server" width="288px" border="0" cellspacing="3"
                                        cellpadding="3" visible="false" class="table">
                                        <tr>
                                            <td>申請類別</td>
                                            <td>
                                                <asp:DropDownList ID="ApplyKindNo" runat="server" DataSourceID="DSAuditKind"
                                                    DataTextField="KindName" DataValueField="KindNo" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ApplyKindNo_SelectedIndexChanged" TabIndex="3">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="DSAuditKind" runat="server"
                                                    ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                    SelectCommand="SELECT [KindNo], [KindName] FROM [CAuditKind]"></asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>申請途徑</td>
                                            <td>
                                                <asp:DropDownList ID="ApplyWayNo" runat="server" DataSourceID="DSAuditWay"
                                                    DataTextField="WayName" DataValueField="WayNo" Visible="false" TabIndex="4">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="DSAuditWay" runat="server"
                                                    ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                    SelectCommand="SELECT [WayNo], [WayName] FROM [CAuditWay]"></asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>申請類型</td>
                                            <td><a href="http://tmu-hr.tmu.edu.tw/zh_tw/T3/T4" target="_blank">應檢附表單說明</a><br />
                                                <asp:DropDownList ID="ApplyAttributeNo" runat="server"
                                                    DataTextField="AttributeName"
                                                    DataValueField="AttributeNo" Width="200px" TabIndex="5">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="DSAuditAttribute" runat="server"
                                                    ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>"
                                                    SelectCommand="SELECT [AttributeNo], [AttributeName] FROM [CAuditAttribute] WHERE ([KindNo] = @KindNo)">
                                                    <SelectParameters>
                                                        <asp:FormParameter DefaultValue="1" FormField="ApplyKindNo" Name="KindNo"
                                                            Type="String" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="Send" runat="server" Text="確定" OnClick="Send_Click" CssClass="btn btn-success pull-right" /></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />



            <br />
        </div>
        <div>
            <div class="container ">
                <p class="text-muted">
                    教師聘任升等作業系統由臺北醫學大學資訊處維護. All rights reserved.  <a href="ManageLogin.aspx">管理平台</a>
                </p>
            </div>
        </div>
    </form>
</body>
</html>
