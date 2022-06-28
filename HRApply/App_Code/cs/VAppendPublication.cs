using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAppendPublication
    {
        public string strAppPSn = "AppPSn";

        public string strAppSn = "AppSn";

        public string strAppPCoAuthorUpload = "AppPCoAuthorUpload";

        public string strAppPCoAuthorUploadName = "AppPCoAuthorUploadName";

        public string strAppPSummaryCN = "AppPSummaryCN";

        public string strAppJournalUpload = "AppJournalUpload";

        public string strAppJournalUploadName = "AppJournalUploadName";

        [DataMember(Name = "AppPSn")]
        public Int32 AppPSn { get; set; }

        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "AppPCoAuthorUpload")]
        public Boolean AppPCoAuthorUpload { get; set; }

        [DataMember(Name = "AppPCoAuthorUploadName")]
        public String AppPCoAuthorUploadName { get; set; }

        [DataMember(Name = "AppPSummaryCN")]
        public String AppPSummaryCN { get; set; }

        [DataMember(Name = "AppJournalUpload")]
        public Boolean AppJournalUpload { get; set; }

        [DataMember(Name = "AppJournalUploadName")]
        public String AppJournalUploadName { get; set; }

    }
}
