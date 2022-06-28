using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VTeacherCa
    {

        public string strCaSn = "CaSn";

        public string strEmpSn = "EmpSn";

        public string strCaNumberCN = "CaNumberCN";

        public string strCaNumber = "CaNumber";

        public string strCaPublishSchool = "CaPublishSchool";

        public string strCaStartYM = "CaStartYM";

        public string strCaEndYM = "CaEndYM";

        public string strCaUpload = "CaUpload";

        public string strCaUploadName = "CaUploadName";

        [DataMember(Name = "CaSn")]
        public Int32 CaSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "CaNumberCN")]
        public String CaNumberCN { get; set; }

        [DataMember(Name = "CaNumber")]
        public String CaNumber { get; set; }

        [DataMember(Name = "CaPublishSchool")]
        public String CaPublishSchool { get; set; }

        [DataMember(Name = "CaStartYM")]
        public String CaStartYM { get; set; }

        [DataMember(Name = "CaEndYM")]
        public String CaEndYM { get; set; }

        [DataMember(Name = "CaUploadName")]
        public Boolean CaUpload { get; set; }


        [DataMember(Name = "CaUploadName")]
        public String CaUploadName { get; set; }

    }
}
