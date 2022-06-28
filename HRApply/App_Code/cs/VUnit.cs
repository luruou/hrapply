using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VUnit
    {
        public string strUntId = "unt_id";

        public string strUntId2 = "unt_id2";

        public string strUntLevelb = "unt_levelb";

        public string strDptId = "dpt_id";

        public string strUntNameFull = "unt_name_full";

        public string strUntSupEmpId = "unt_empid";

        public string strUntEmpSupName = "emp_name";

        public string strUntSupEmpEmail = "emp_email";

        [DataMember(Name = "UntId")]
        public String UntId { get; set; }

        [DataMember(Name = "UntId2")]
        public String UntId2 { get; set; }

        [DataMember(Name = "UntLevelb")]
        public String UntLevelb { get; set; }

        [DataMember(Name = "DptId")]
        public String DptId { get; set; }

        [DataMember(Name = "UntNameFull")]
        public String UntNameFull { get; set; }

        [DataMember(Name = "UntSupEmpId")]
        public String UntSupEmpId { get; set; }

        [DataMember(Name = "UntSupEmpName")]
        public String UntEmpSupName { get; set; }

        [DataMember(Name = "UntSupEmpEmail")]
        public String UntSupEmpEmail { get; set; }
    }
}
