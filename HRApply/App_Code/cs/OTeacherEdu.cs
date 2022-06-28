using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class OTeacherEdu
    {
        //s10_education.edu_xmcname, s10_education.edu_xddname, s10_education.edu_edate,  s10_education.edu_xdlid,s90_xedlevel.xdl_name as edu_xdl_name

        public string strEmpId = "edu_empid";

        public string strEduIdno = "edu_idno";

        public string strEduNation = "edu_nation";

        public string strEduXmcname = "edu_xmcname";

        public string strEduXddname = "edu_xddname";

        public string strEduBdate = "edu_bdate";

        public string strEduEdate = "edu_edate";

        public string strEduXdlid = "edu_xdlid";

        public string strEduXdlName = "edu_xdlid_name";


        [DataMember(Name = "EmpId")]
        public String EmpId { get; set; }

        [DataMember(Name = "EduIdno")]
        public String EduIdno { get; set; }

        [DataMember(Name = "EduNation")]
        public String EduNation { get; set; }

        [DataMember(Name = "EduXmcname")]
        public Int32 EduXmcname { get; set; }

        [DataMember(Name = "EduXddname")]
        public Int32 EduXddname { get; set; }

        [DataMember(Name = "EduBdate")]
        public String EduBdate { get; set; }

        [DataMember(Name = "EduEdate")]
        public String EduEdate { get; set; }

        [DataMember(Name = "EduXdlid")]
        public String EduXdlid { get; set; }

        [DataMember(Name = "EduXdlName")]
        public String EduXdlName { get; set; }

    }
}
