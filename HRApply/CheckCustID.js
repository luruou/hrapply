function ValidateIDNO(source, arguments) {
    var objCtl = document.getElementById("EmpId");
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