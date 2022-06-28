using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class OTeacherExp
    {
        //a.fcu_empid, a.fcu_idno,a.fcu_posid, b.pos_name as fcu_pos_name, a.fcu_unit, c.unt_name_full as fcu_unit_name, a.fcu_titid, d.tit_name as fcu_tit_name, a.fcu_bdate, a.fcu_edate

        public string strFcuEmpId = "fcu_empid";

        public string strFcuIdno = "fcu_idno";

        public string strFcuPosid = "fcu_posid";

        public string strFcuPosName = "fcu_pos_name";

        public string strFcuUnit = "fcu_unit";

        public string strFcuUnitName = "fcu_unit_name";

        public string strFcuTitId = "fcu_titid";

        public string strFcuTitName = "fcu_tit_name";

        public string strExpStartYM = "fcu_bdate";

        public string strExpEndYM = "fcu_edate";

        public string strExpUploadName = "upd_name";


        [DataMember(Name = "FcuEmpId")]
        public String FcuEmpId { get; set; }

        [DataMember(Name = "FcuIdno")]
        public String FcuIdno { get; set; }

        [DataMember(Name = "FcuPosid")]
        public String FcuPosid { get; set; }

        [DataMember(Name = "FcuPosName")]
        public String FcuPosName { get; set; }

        [DataMember(Name = "FcuUnit")]
        public String FcuUnit { get; set; }

        [DataMember(Name = "FcuUnitName")]
        public String FcuUnitName { get; set; }

        [DataMember(Name = "FcuTitId")]
        public String FcuTitId { get; set; }

        [DataMember(Name = "FcuTitName")]
        public String FcuTitName { get; set; }

        [DataMember(Name = "ExpStartYM")]
        public String ExpStartYM { get; set; }

        [DataMember(Name = "ExpEndYM")]
        public String ExpEndYM { get; set; }

        [DataMember(Name = "ExpUploadName")]
        public String ExpUploadName { get; set; }

    }
}
