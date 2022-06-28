    using System;
using System.Runtime.Serialization;


namespace ApplyPromote
{
    public class VThesisScoreCount
    {

        public string strPT_Year = "PT_Year";

        public string strPT_EmpId = "PT_EmpId";

        public string strPT_InPerson = "PT_InPerson";

        public string strPT_FSCI = "PT_FSCI";

        public string strPT_FSSCI = "PT_FSSCI";

        public string strPT_FEI = "PT_FEI";

        public string strPT_FNSCI = "PT_FNSCI";

        public string strPT_FOther = "PT_FOther";

        public string strPT_NFCSCI = "PT_NFCSCI";

        public string strPT_NFCSSCI = "PT_NFCSSCI";

        public string strPT_NFCEI = "PT_NFCEI";

        public string strPT_NFCNSCI = "PT_NFCNSCI";

        public string strPT_NFCOther = "PT_NFCOther";

        public string strPT_NFOCSCI = "PT_NFOCSCI";

        public string strPT_NFOCSSCI = "PT_NFOCSSCI";

        public string strPT_NFOCEI = "PT_NFOCEI";

        public string strPT_NFOCNSCI = "PT_NFOCNSCI";

        public string strPT_NFOCOther = "PT_NFOCOther";

        public string strPT_Update = "PT_Update";


        [DataMember(Name = "PT_Year")]
        public String PT_Year { get; set; }

        [DataMember(Name = "PT_EmpId")]
        public String PT_EmpId { get; set; }

        [DataMember(Name = "PT_InPerson")]
        public String PT_InPerson { get; set; }

        [DataMember(Name = "PT_FSCI")]
        public Int16 PT_FSCI { get; set; }

        [DataMember(Name = "PT_FSSCI")]
        public Int16 PT_FSSCI { get; set; }

        [DataMember(Name = "PT_FEI")]
        public Int16 PT_FEI { get; set; }

        [DataMember(Name = "PT_FNSCI")]
        public Int16 PT_FNSCI { get; set; }

        [DataMember(Name = "PT_FOther")]
        public Int16 PT_FOther { get; set; }

        [DataMember(Name = "PT_NFCSCI")]
        public Int16 PT_NFCSCI { get; set; }

        [DataMember(Name = "PT_NFCSSCI")]
        public Int16 PT_NFCSSCI { get; set; }

        [DataMember(Name = "PT_NFCEI")]
        public Int16 PT_NFCEI { get; set; }

        [DataMember(Name = "PT_NFCNSCI")]
        public Int16 PT_NFCNSCI { get; set; }

        [DataMember(Name = "PT_NFCOther")]
        public Int16 PT_NFCOther { get; set; }

        [DataMember(Name = "PT_NFOCSCI")]
        public Int16 PT_NFOCSCI { get; set; }

        [DataMember(Name = "PT_NFOCSSCI")]
        public Int16 PT_NFOCSSCI { get; set; }

        [DataMember(Name = "PT_NFOCEI")]
        public Int16 PT_NFOCEI { get; set; }

        [DataMember(Name = "PT_NFOCNSCI")]
        public Int16 PT_NFOCNSCI { get; set; }

        [DataMember(Name = "PT_NFOCOther")]
        public Int16 PT_NFOCOther { get; set; }

        [DataMember(Name = "PT_Update")]
        public DateTime PT_Update { get; set; }


    }
}
