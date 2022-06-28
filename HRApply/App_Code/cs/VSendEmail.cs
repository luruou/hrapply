using System;
using System.Runtime.Serialization;


/// <summary>
/// VRefExamDept 的摘要描述
/// </summary>
/// 

namespace ApplyPromote 
{

    public class VSendEmail
    {

        // TODO: 在此加入建構函式的程式碼
        //
        public string strMailSubject = "mailSubject";

        public string strMailFromAccount = "mailFromAccount";

        public string strMailToAccount = "mailToAccount";

        public string strToAccountName = "toAccountName";

        public string strMailContent = "mailContent";

        [DataMember(Name = "mailSubject")]
        public String MailSubject { get; set; }

        [DataMember(Name = "mailFromAccount")]
        public String MailFromAccount { get; set; }

        [DataMember(Name = "mailToAccount")]
        public String MailToAccount { get; set; }

        [DataMember(Name = "toAccountName")]
        public String ToAccountName { get; set; }

        [DataMember(Name = "mailContent")]
        public String MailContent { get; set; }
    }
}