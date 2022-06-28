using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAuditReportCompose
    {

        public string strARSn = "ARSn";

        public string strARWaydNo = "ARWaydNo";

        public string strARKindNo = "ARKindNo";

        public string strARAttributeNo = "ARAttributeNo";

        public string strARStage = "ARStage";

        public string strARWritingScore = "ARWritingScore";

        public string strARStrengthsWeaknesses = "ARStrengthsWeaknesses";

        public string strARFiveLevel = "ARFiveLevel";

        public string strStatus = "Status";

        [DataMember(Name = "ARSn")]
        public Int32 ARSn { get; set; }

        [DataMember(Name = "ARWaydNo")]
        public String ARWaydNo { get; set; }

        [DataMember(Name = "ARKindNo")]
        public String ARKindNo { get; set; }

        [DataMember(Name = "ARAttributeNo")]
        public String ARAttributeNo { get; set; }

        [DataMember(Name = "ARStage")]
        public String ARStage { get; set; }

        [DataMember(Name = "ARWritingScore")]
        public String ARWritingScore { get; set; }

        [DataMember(Name = "ARStrengthsWeaknesses")]
        public String ARStrengthsWeaknesses { get; set; }

        [DataMember(Name = "ARFiveLevel")]
        public String ARFiveLevel { get; set; }

        [DataMember(Name = "Status")]
        public Boolean Status { get; set; }

    }
}
