using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAuditPeroid
    {

        public string strAuditTerm = "AuditPeroidTerm";

        public string strAuditPeroidPointStage = "AuditPeroidPointStage";

        public string strAuditPeroidBeginDate = "AuditPeroidBeginDate";

        public string strAuditPeroidEndDate = "AuditPeroidEndDate";

        [DataMember(Name = "AuditPeroidTerm")]
        public String AuditPeroidTerm { get; set; }

        [DataMember(Name = "AuditPeroidPointStage")]
        public String AuditPeroidPointStage { get; set; }

        [DataMember(Name = "AuditPeroidBeginDate")]
        public DateTime AuditPeroidBeginDate { get; set; }

        [DataMember(Name = "AuditPeroidEndDate")]
        public DateTime AuditPeroidEndDate { get; set; }
    }
}
