function CheckCOMPID(source, arguments) {
    var objCtl = document.getElementById("EmpId");
    var strInputValue = objCtl.value;

    if (strInputValue.length == 8) {
        var arrCOMPID = new Array();
        var arrCOMPID_Sum = new Array();
        arrCOMPID_Sum.length = 8;
        arrCOMPID.length = 8;
        for (i = 0; i < arrCOMPID.length; i++) {
            arrCOMPID[i] = strInputValue.substr(i, 1);
            var subresult = 0;
            switch (i) {
                case 0: arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 1); break;
                case 1: arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 2); break;
                case 2: arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 1); break;
                case 3: arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 2); break;
                case 4: arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 1); break;
                case 5: arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 2); break;
                case 6: if (arrCOMPID[6] == 7) {
                        arrCOMPID_Sum[i] = 0; break;
                    }
                    else {
                        arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 4); break;
                    }
                case 7: arrCOMPID_Sum[i] = _AddNumStr(arrCOMPID[i] * 1); break;
            }
        }

        var ResultValue = 0;
        for (i = 0; i < arrCOMPID_Sum.length; i++) {
            ResultValue += arrCOMPID_Sum[i];
        }

        if (ResultValue % 10 == 0) {
            arguments.IsValid = true;
        }
        else {

            if (arrCOMPID[6] == 7) {
                if ((ResultValue + 1) % 10 == 0) {
                    arguments.IsValid = true;
                }
                else {
                    arguments.IsValid = false;
                }
            }
            else {
                arguments.IsValid = false;
            }
        }
    }
    else {
        arguments.IsValid = false;
    }
}

function _AddNumStr(intArrayElem) {

    var strResult = 0;
    if (intArrayElem > 9) {
        intArrayElem = intArrayElem.toString();
        strResult = intArrayElem.substr(0, 1) * 1 + intArrayElem.substr(1, 1) * 1;
    }
    else {
        strResult = intArrayElem;
    }
    return strResult;
}