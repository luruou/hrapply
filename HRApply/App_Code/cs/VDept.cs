using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VDept
    {
        public string strUntId = "unt_id";

        public string strUntName = "unt_name";

        public string strUntLevel = "level";

        public string strUntSupName = "sup_name";

        public string strUntFirId = "fir_id";

        public string strUntFirName = "fir_name";


        [DataMember(Name = "UntId")]
        public String UntId { get; set; }

        [DataMember(Name = "UntName")]
        public String UntName { get; set; }

        [DataMember(Name = "UntLevel")]
        public String UntLevel { get; set; }

        [DataMember(Name = "UntSupName")]
        public String UntSupName { get; set; }

        [DataMember(Name = "UntFirId")]
        public String UntFirId { get; set; }

        [DataMember(Name = "UntFirName")]
        public String UntFirName { get; set; }

    }
}
