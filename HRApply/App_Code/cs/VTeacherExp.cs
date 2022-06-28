using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VTeacherExp
    {

        public string strExpSn = "ExpSn";

        public string strAppSn = "AppSn";

        public string strEmpSn = "EmpSn";

        public string strExpOrginization = "ExpOrginization";

        public string strExpUnit = "ExpUnit";

        public string strExpJobTitle = "ExpJobTitle";

        public string strExpJobType = "ExpJobType";

        public string strExpStartYM = "ExpStartYM";

        public string strExpEndYM = "ExpEndYM";

        public string strExpUpload = "ExpUpload";

        public string strExpUploadName = "ExpUploadName";

        [DataMember(Name = "ExpSn")]
        public Int32 ExpSn { get; set; }
        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "ExpOrginization")]
        public String ExpOrginization { get; set; }

        [DataMember(Name = "ExpUnit")]
        public String ExpUnit { get; set; }

        [DataMember(Name = "ExpJobTitle")]
        public String ExpJobTitle { get; set; }

        [DataMember(Name = "ExpJobType")]
        public String ExpJobType { get; set; }

        [DataMember(Name = "ExpStartYM")]
        public String ExpStartYM { get; set; }

        [DataMember(Name = "ExpEndYM")]
        public String ExpEndYM { get; set; }

        [DataMember(Name = "ExpUpload")]
        public Boolean ExpUpload { get; set; }

        [DataMember(Name = "ExpUploadName")]
        public String ExpUploadName { get; set; }

    }
}
