using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAuditOutter
    {
        public string strAuditorSn = "AuditorSn";

        public string strAuditorSnEmpId = "AuditorSnEmpId";

        public string strAuditorName = "AuditorName";

        public string strAuditorUnit = "AuditorUnit";

        public string strAuditorJobTitle = "AuditorJobTitle";

        public string strExecuteAccept = "ExecuteAccept";

        public string strAuditorExperience = "AuditorExperience";

        public string strAuditorEmail = "AuditorEmail";

        public string strAuditorTel = "AuditorTel";

        public string strAuditorCell = "AuditorCell";

        public string strAuditorBankAccount1 = "AuditorBankAccount1";

        public string strAuditorBankAccount2 = "AuditorBankAccount2";

        public string strAuditorBuildDate = "AuditorBuildDate";

        public string strAuditorModifyDate = "AuditorModifyDate";

        [DataMember(Name = "AuditorSn")]
        public Int32 AuditorSn { get; set; }

        [DataMember(Name = "AuditorSnEmpId")]
        public String AuditorSnEmpId { get; set; }

        [DataMember(Name = "AuditorName")]
        public String AuditorName { get; set; }

        [DataMember(Name = "AuditorUnit")]
        public String AuditorUnit { get; set; }

        [DataMember(Name = "AuditorJobTitle")]
        public String AuditorJobTitle { get; set; }

        [DataMember(Name = "AuditorExperience")]
        public Boolean AuditorExperience { get; set; }

        [DataMember(Name = "AuditorEmail")]
        public String AuditorEmail { get; set; }

        [DataMember(Name = "AuditorTel")]
        public String AuditorTel { get; set; }

        [DataMember(Name = "AuditorCell")]
        public String AuditorCell { get; set; }

        [DataMember(Name = "AuditorBankAccount1")]
        public String AuditorBankAccount1 { get; set; }

        [DataMember(Name = "AuditorBankAccount2")]
        public String AuditorBankAccount2 { get; set; }

        [DataMember(Name = "AuditorEducation")]
        public String AuditorEducation { get; set; }

        [DataMember(Name = "AuditorBuildDate")]
        public DateTime AuditorBuildDate { get; set; }

        [DataMember(Name = "AuditorModifyDate")]
        public DateTime AuditorModifyDate { get; set; }
    }
}
