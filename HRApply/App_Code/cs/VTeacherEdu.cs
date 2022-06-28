using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VTeacherEdu
    {
        public string strEduSn = "EduSn";

        public string strEmpSn = "EmpSn";

        public string strEduLocal = "EduLocal";

        public string strEduSchool = "EduSchool";

        public string strEduDepartment = "EduDepartment";

        public string strEduDegree = "EduDegree";

        public string strEduDegreeType = "EduDegreeType";

        public string strEduStartYM = "EduStartYM";

        public string strEduEndYM = "EduEndYM";

        [DataMember(Name = "EduSn")]
        public Int32 EduSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "EduLocal")]
        public String EduLocal { get; set; }

        [DataMember(Name = "EduSchool")]
        public String EduSchool { get; set; }

        [DataMember(Name = "EduDepartment")]
        public String EduDepartment { get; set; }

        [DataMember(Name = "EduDegree")]
        public String EduDegree { get; set; }

        [DataMember(Name = "EduDegreeType")]
        public String EduDegreeType { get; set; }

        [DataMember(Name = "EduStartYM")]
        public String EduStartYM { get; set; }

        [DataMember(Name = "EduEndYM")]
        public String EduEndYM { get; set; }

    }
}
