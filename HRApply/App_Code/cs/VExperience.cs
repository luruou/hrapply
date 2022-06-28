using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VExperience
    {        

        public string strfcu_idno = "fcu_idno";

        public string strfcu_posid = "fcu_posid";

        public string strfcu_unit = "fcu_unit";

        public string strfcu_titid = "fcu_titid";

        public string strfcu_bdate = "fcu_bdate";

        public string strfcu_edate = "fcu_edate";


        [DataMember(Name = "fcu_idno")]
        public String fcu_idno { get; set; }

        [DataMember(Name = "fcu_posid")]
        public String fcu_posid { get; set; }

        [DataMember(Name = "fcu_unit")]
        public String fcu_unit { get; set; }

        [DataMember(Name = "fcu_titid")]
        public String fcu_titid { get; set; }

        [DataMember(Name = "edu_xddname")]
        public String edu_xddname { get; set; }

        [DataMember(Name = "fcu_bdate")]
        public String fcu_bdate { get; set; }

        [DataMember(Name = "fcu_edate")]
        public String fcu_edate { get; set; }


    }
}
