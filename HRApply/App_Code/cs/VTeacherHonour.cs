using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VTeacherHonour
    {
        public string strHorSn = "HorSn";

        public string strEmpSn = "EmpSn";

        public string strHorYear = "HorYear";

        public string strHorDescription = "HorDescription";

        [DataMember(Name = "HorSn")]
        public Int32 HorSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "HorYear")]
        public String HorYear { get; set; }

        [DataMember(Name = "HorDescription")]
        public String HorDescription { get; set; }

    }
}
