using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAccountForApply
    {

        public string strAcctApplySn = "AcctApplySn";

        public string strAcctApplyEmail = "AcctApplyEmail";

        public string strAcctApplyPassword = "AcctApplyPassword";

        public string strAcctApplyId = "AcctApplyId";

        public string strAcctApplyBirthday = "AcctApplyBirthday";

        public string strAcctApplyCell = "AcctApplyCell";

        public string strAcctApplyStatus = "AcctApplyStatus";

        public string strAcctApplyBuildDate = "AcctApplyBuildDate";
        
        public string strAcctApplyModifyBuildDate = "AcctApplyModifyBuildDate";

        [DataMember(Name = "AcctApplySn")]
        public Int32 AcctApplySn { get; set; }

        [DataMember(Name = "AcctApplyEmail")]
        public String AcctApplyEmail { get; set; }

        [DataMember(Name = "AcctApplyPassword")]
        public String AcctApplyPassword { get; set; }

        [DataMember(Name = "AcctApplyId")]
        public String AcctApplyId { get; set; }

        [DataMember(Name = "AcctApplyBirthday")]
        public String AcctApplyBirthday { get; set; }

        [DataMember(Name = "AcctApplyCell")]
        public String AcctApplyCell { get; set; } 

        [DataMember(Name = "AcctApplyStatus")]
        public Boolean AcctApplyStatus { get; set; }

        [DataMember(Name = "AcctApplyBuildDate")]
        public DateTime AcctApplyBuildDate { get; set; }

        [DataMember(Name = "AcctApplyModifyBuildDate")]
        public DateTime AcctApplyModifyBuildDate { get; set; }
    }
}
