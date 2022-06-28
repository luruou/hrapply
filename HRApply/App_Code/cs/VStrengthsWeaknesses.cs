using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VStrengthsWeaknesses
    {

        public string strSWSn = "SWSn";

        public string strWaydNo = "WaydNo";

        public string strKindNo = "KindNo";

        public string strAttributeNo = "AttributeNo";

        public string strSWType = "SWType";

        public string strSWContent = "SWContent";

        public string strStatus = "Status";

        [DataMember(Name = "SWSn")]
        public Int32 SWSn { get; set; }


        [DataMember(Name = "WaydNo")]
        public String WaydNo { get; set; }

        [DataMember(Name = "KindNo")]
        public String KindNo { get; set; }

        [DataMember(Name = "AttributeNo")]
        public String AttributeNo { get; set; }

        [DataMember(Name = "SWType")]
        public String SWType { get; set; }

        [DataMember(Name = "SWContent")]
        public String SWContent { get; set; }

        [DataMember(Name = "Status")]
        public Boolean Status { get; set; }

    }
}
