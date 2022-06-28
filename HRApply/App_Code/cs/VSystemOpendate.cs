using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VSystemOpendate
    {

        public string strSmtr = "Smtr";

        public string strSemester = "Semester";

        public string strKindNo = "KindNo";

        public string strTypeNo = "TypeNo";

        public string strApplyBeginTime = "ApplyBeginTime";

        public string strApplyEndTime = "ApplyEndTime";

        public string strAuditBeginTime = "AuditBeginTime";

        public string strAuditEndTime = "AuditEndTime";

        public string strAdminBeginTime = "AdminBeginTime";

        public string strAdminEndTime = "AdminEndTime";

        [DataMember(Name = "Smtr")]
        public String Smtr { get; set; }

        [DataMember(Name = "Semester")]
        public String Semester { get; set; }

        [DataMember(Name = "KindNo")]
        public String KindNo { get; set; }

        [DataMember(Name = "TypeNo")]
        public String TypeNo { get; set; }

        [DataMember(Name = "ApplyBeginTime")]
        public DateTime ApplyBeginTime { get; set; }

        [DataMember(Name = "ApplyEndTime")]
        public DateTime ApplyEndTime { get; set; }

        [DataMember(Name = "AuditBeginTime")]
        public DateTime AuditBeginTime { get; set; }

        [DataMember(Name = "AuditEndTime")]
        public DateTime AuditEndTime { get; set; }

        [DataMember(Name = "AdminBeginTime")]
        public DateTime AdminBeginTime { get; set; }

        [DataMember(Name = "AdminEndTime")]
        public DateTime AdminEndTime { get; set; }

    }
}
