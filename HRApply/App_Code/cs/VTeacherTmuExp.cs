using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VTeacherTmuExp
    {

        public string strExpSn = "ExpSn";

        public string strEmpSn = "EmpSn";

        public string strExpUnitId = "ExpUnitId";

        public string strExpTitleId = "ExpTitleId";

        public string strExpPosId = "ExpPosId";

        public string strExpQId = "ExpQId";

        public string strExpStartDate = "ExpStartDate";

        public string strExpEndDate = "ExpEndDate";

        public string strExpUploadName = "ExpUploadName";

        public string strExpUserId = "ExpUserId";

        public string strExpUpdateDate = "ExpUpdateDate";

        [DataMember(Name = "ExpSn")]
        public Int32 ExpSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "ExpUnitId")]
        public String ExpUnitId { get; set; }

        [DataMember(Name = "ExpTitleId")]
        public String ExpTitleId { get; set; }

        [DataMember(Name = "ExpPosId")]
        public String ExpPosId { get; set; }

        [DataMember(Name = "ExpQId")]
        public String ExpQId { get; set; }

        [DataMember(Name = "ExpStartDate")]
        public String ExpStartDate { get; set; }

        [DataMember(Name = "ExpEndDate")]
        public String ExpEndDate { get; set; }

        [DataMember(Name = "ExpUploadName")]
        public String ExpUploadName { get; set; }

        [DataMember(Name = "ExpUserId")]
        public String ExpUserId { get; set; }

        [DataMember(Name = "ExpUpdateDate")]
        public String ExpUpdateDate { get; set; }

    }
}
