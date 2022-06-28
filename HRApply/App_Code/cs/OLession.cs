using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class OLession
    {        

        public string strLessionYear = "prm_year";

        public string strLessionSemester = "prm_smester";

        public string strLessionIdno = "prm_idno";

        public string strLessionDeptLevel = "prm_deplevel";

        public string strLessionClass = "prm_class";

        public string strLessionHours = "prm_hours";

        public string strLessionUserId = "upd_name";

        public string strLessionUpdateDate = "upd_dt";


        [DataMember(Name = "LessionYear")]
        public String LessionYear { get; set; }

        [DataMember(Name = "LessionSemester")]
        public String LessionSemester { get; set; }

        [DataMember(Name = "LessionIdno")]
        public String LessionIdno { get; set; }

        [DataMember(Name = "LessionDeptLevel")]
        public String LessionDeptLevel { get; set; }

        [DataMember(Name = "LessionClass")]
        public String LessionClass { get; set; }

        [DataMember(Name = "LessionHours")]
        public String LessionHours { get; set; }

        [DataMember(Name = "LessionUserId")]
        public String LessionUserId { get; set; }

        [DataMember(Name = "LessionUpdateDate")]
        public String LessionUpdateDate { get; set; }
    }
}
