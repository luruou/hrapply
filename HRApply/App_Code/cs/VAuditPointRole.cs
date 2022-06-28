using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAuditPointRole
    {

        public string strAuditPointDepartment = "AuditPointDepartment";

        public string strAuditPointAttribute = "AuditPointAttribute";

        public string strAuditPointSn = "AuditPointSn";

        public string strAuditPointRoleName = "AuditPointRoleName";

        public string strAuditPointRoleLevel = "AuditPointRoleLevel";

        public string strAuditPointStage = "AuditPointStage";

        public string strAuditPointStep = "AuditPointStep";

        [DataMember(Name = "AuditPointDepartment")]
        public String AuditPointDepartment { get; set; }

        [DataMember(Name = "AuditPointAttribute")]
        public String AuditPointAttribute { get; set; }

        [DataMember(Name = "AuditPointSn")]
        public String AuditPointSn { get; set; }

        [DataMember(Name = "AuditPointRoleName")]
        public String AuditPointRoleName { get; set; }

        [DataMember(Name = "AuditPointRoleLevel")]
        public String AuditPointRoleLevel { get; set; }

        [DataMember(Name = "AuditPointStage")]
        public String AuditPointStage { get; set; }

        [DataMember(Name = "AuditPointStep")]
        public String AuditPointStep { get; set; }

    }

}
