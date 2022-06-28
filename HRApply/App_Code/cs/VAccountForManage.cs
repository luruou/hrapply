using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAccountForManage
    {

        public string strAcctSn = "AcctSn";

        public string strAcctEmpId = "AcctEmpId";

        public string strAcctEmail = "AcctEmail";

        public string strAcctPassword = "AcctPassword";

        public string strAcctRole = "AcctRole";

        public string strAcctStatus = "AcctStatus";

        public string strAcctBuildDate = "AcctBuildDate";

        public string strAcctModifyDate = "AcctModifyDate";

        [DataMember(Name = "AcctSn")]
        public Int32 AcctSn { get; set; }

        [DataMember(Name = "AcctEmpId")]
        public String AcctEmpId { get; set; }

        [DataMember(Name = "AcctEmail")]
        public String AcctEmail { get; set; }

        [DataMember(Name = "AcctPassword")]
        public String AcctPassword { get; set; }

        [DataMember(Name = "AcctRole")]
        public String AcctRole { get; set; }

        [DataMember(Name = "AcctStatus")]
        public Boolean AcctStatus { get; set; }

        [DataMember(Name = "AcctBuildDate")]
        public DateTime AcctBuildDate { get; set; }

        [DataMember(Name = "AcctModifyDate")]
        public DateTime AcctModifyDate { get; set; }

    }
}
