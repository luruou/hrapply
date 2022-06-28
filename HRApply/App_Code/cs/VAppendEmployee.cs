using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAppendEmployee
    {
        public string strAppESn = "AppESn";

        public string strAppSn = "AppSn";

        public string strAppNowJobOrg = "AppNowJobOrg";

        public string strAppNote = "AppNote";

        public string strAppRecommendors = "AppRecommendors";

        public string strAppRecommendYear = "AppRecommendYear";

        public string strAppRecommendUpload = "AppRecommendUpload";

        public string strAppRecommendUploadName = "AppRecommendUploadName";

        [DataMember(Name = "AppESn")]
        public Int32 AppESn { get; set; }

        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "AppNowJobOrg")]
        public String AppNowJobOrg { get; set; }

        [DataMember(Name = "AppNote")]
        public String AppNote { get; set; }

        [DataMember(Name = "AppRecommendors")]
        public String AppRecommendors { get; set; }

        [DataMember(Name = "AppRecommendYear")]
        public String AppRecommendYear { get; set; }

        [DataMember(Name = "AppRecommendUpload")]
        public Boolean AppRecommendUpload { get; set; }

        [DataMember(Name = "AppRecommendUploadName")]
        public String AppRecommendUploadName { get; set; }

    }
}
