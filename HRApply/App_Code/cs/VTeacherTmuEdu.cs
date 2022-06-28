using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VTeacherTmuEdu
    {        

        public string stredu_empid = "edu_empid";

        public string stredu_idno = "edu_idno";

        public string stredu_nation = "edu_nation";

        public string stredu_xmcname = "edu_xmcname";

        public string stredu_xddname = "edu_xddname";
        
        public string stredu_bdate = "edu_bdate";

        public string stredu_edate = "edu_edate";

        public string stredu_status = "edu_status";

        public string stredu_xdlid = "edu_xdlid";


        [DataMember(Name = "edu_empid")]
        public String edu_empid { get; set; }

        [DataMember(Name = "edu_idno")]
        public String edu_idno { get; set; }

        [DataMember(Name = "edu_nation")]
        public String edu_nation { get; set; }

        [DataMember(Name = "edu_xmcname")]
        public String edu_xmcname { get; set; }

        [DataMember(Name = "edu_xddname")]
        public String edu_xddname { get; set; }

        [DataMember(Name = "edu_bdate")]
        public String edu_bdate { get; set; }

        [DataMember(Name = "edu_edate")]
        public String edu_edate { get; set; }

        [DataMember(Name = "edu_status")]
        public String edu_status { get; set; }

        [DataMember(Name = "edu_xdlid")]
        public String edu_xdlid { get; set; }

    }
}
