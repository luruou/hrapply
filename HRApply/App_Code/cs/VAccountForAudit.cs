using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAccountForAudit
    {
        public string strAcctSn = "AcctSn";

        public string strAcctAppSn = "AcctAppSn";

        public string strAcctAuditorSnEmpId = "AcctAuditorSnEmpId";

        public string strAcctEmail = "AcctEmail";

        public string strAcctPassword = "AcctPassword";

        public string strAcctStatus = "AcctStatus";

        public string strAcctBuildDate = "AcctBuildDate";

        public string strAcctModifyDate = "AcctModifyDate";

        [DataMember(Name = "AcctSn")]
        public Int32 AcctSn { get; set; }

        [DataMember(Name = "AcctAppSn")]
        public String AcctAppSn { get; set; }

        [DataMember(Name = "AcctAuditorSnEmpId")]
        public String AcctAuditorSnEmpId { get; set; } 

        [DataMember(Name = "AcctEmail")]
        public String AcctEmail { get; set; }

        [DataMember(Name = "AcctPassword")]
        public String AcctPassword { get; set; }

        [DataMember(Name = "AcctStatus")]
        public Boolean AcctStatus { get; set; }

        [DataMember(Name = "AcctBuildDate")]
        public DateTime AcctBuildDate { get; set; }

        [DataMember(Name = "AcctModifyDate")]
        public DateTime AcctModifyDate { get; set; }


    }
}
