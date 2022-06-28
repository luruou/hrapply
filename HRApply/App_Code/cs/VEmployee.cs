using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    [DataContract]
    public class VEmployee
    {


        public string stremp_idno = "emp_idno";

        public string stremp_id = "emp_id";

        public string stremp_passid = "emp_passid";

        public string stremp_sex = "emp_sex";

        public string stremp_name = "emp_name";

        public string stremp_ename = "emp_ename";
        
        public string stremp_bdate = "emp_bdate";

        public string stremp_untid = "emp_untid";

        public string stremp_pstate = "emp_pstate";

        public string stremp_titid = "emp_titid";

        public string stremp_indate = "emp_indate";

        public string stremp_posid = "emp_posid";

        public string stremp_poaddt = "emp_poaddt";

        public string stremp_pzcode = "emp_pzcode";

        public string stremp_padrs = "emp_padrs";

        public string stremp_ptel = "emp_ptel";

        public string stremp_email = "emp_email";

        public string stremp_nation = "emp_nation";

        public string stremp_register = "emp_register";

        public string stremp_teachno = "emp_teachno";


        [DataMember(Name = "emp_idno")]
        public String emp_idno { get; set; }

        [DataMember(Name = "emp_id")]
        public String emp_id { get; set; }

        [DataMember(Name = "emp_passid")]
        public String emp_passid { get; set; }

        [DataMember(Name = "emp_name")]
        public String emp_name { get; set; }

        [DataMember(Name = "emp_sex")]
        public String emp_sex { get; set; }

        [DataMember(Name = "emp_ename")]
        public String emp_ename { get; set; }

        [DataMember(Name = "emp_bdate")]
        public String emp_bdate { get; set; }

        [DataMember(Name = "emp_untid")]
        public String emp_untid { get; set; }

        [DataMember(Name = "emp_pstate")]
        public String emp_pstate { get; set; }

        [DataMember(Name = "emp_titid")]
        public String emp_titid { get; set; }

        [DataMember(Name = "emp_indate")]
        public String emp_indate { get; set; }

        [DataMember(Name = "emp_posid")]
        public String emp_posid { get; set; }

        [DataMember(Name = "emp_poaddt")]
        public String emp_poaddt { get; set; }

        [DataMember(Name = "emp_pzcode")]
        public String emp_pzcode { get; set; }

        [DataMember(Name = "emp_padrs")]
        public String emp_padrs { get; set; }

        [DataMember(Name = "emp_ptel")]
        public String emp_ptel { get; set; }

        [DataMember(Name = "emp_email")]
        public String emp_email { get; set; }

        [DataMember(Name = "emp_nation")]
        public String emp_nation { get; set; }

        [DataMember(Name = "emp_register")]
        public String emp_register { get; set; }

        [DataMember(Name = "emp_teachno")]
        public String emp_teachno { get; set; }

    }


}
