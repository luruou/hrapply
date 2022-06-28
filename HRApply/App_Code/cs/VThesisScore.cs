    using System;
using System.Runtime.Serialization;


namespace ApplyPromote
{
    public class VThesisScore
    {

        public string strThesisSn = "ThesisSn";

        public string strAppSn = "AppSn";

        public string strEmpSn = "EmpSn";

        public string strThesisYear = "ThesisYear";

        public string strSnNo = "SnNo";

        public string strThesisResearchResult = "ThesisResearchResult";

        public string strThesisPublishYearMonth = "ThesisPublishYearMonth";        

        public string strRRNo = "RRNo";

        public string strThesisC = "ThesisC";

        public string strThesisJ = "ThesisJ";

        public string strThesisA = "ThesisA";

        public string strThesisTotal = "ThesisTotal";

        public string strThesisName = "ThesisName";

        public string strThesisUploadName = "ThesisUploadName";

        public string strThesisJournalRefCount = "ThesisJournalRefCount";

        public string strThesisJournalRefUploadName = "ThesisJournalRefUploadName";        

        public string strIsRepresentative = "IsRepresentative";

        public string strIsCountRPI = "IsCountRPI";

        public string strThesisSummaryCN = "ThesisSummaryCN";

        public string strThesisCoAuthorUpload = "ThesisCoAuthorUpload";

        public string strThesisCoAuthorUploadName = "ThesisCoAuthorUploadName";

        public string strThesisBuildDate = "ThesisBuildDate";

        public string strThesisModifyDate = "ThesisModifyDate";

        [DataMember(Name = "ThesisSn")]
        public Int32 ThesisSn { get; set; }

        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "ThesisYear")]
        public String ThesisYear { get; set; }

        [DataMember(Name = "SnNo")]
        public Int32 SnNo { get; set; }

        [DataMember(Name = "ThesisPublishYearMonth")]
        public String ThesisPublishYearMonth { get; set; }

        [DataMember(Name = "RRNo")]
        public String RRNo { get; set; }

        [DataMember(Name = "ThesisResearchResult")]
        public String ThesisResearchResult { get; set; }

        [DataMember(Name = "ThesisC")]
        public String ThesisC { get; set; }

        [DataMember(Name = "ThesisJ")]
        public String ThesisJ { get; set; }

        [DataMember(Name = "ThesisA")]
        public String ThesisA { get; set; }

        [DataMember(Name = "ThesisTotal")]
        public String ThesisTotal { get; set; }

        [DataMember(Name = "ThesisName")]
        public String ThesisName { get; set; }

        [DataMember(Name = "ThesisUploadName")]
        public String ThesisUploadName { get; set; }


        [DataMember(Name = "ThesisJournalRefCount")]
        public String ThesisJournalRefCount { get; set; }

        [DataMember(Name = "ThesisJournalRefUploadName")]
        public String ThesisJournalRefUploadName { get; set; }

        [DataMember(Name = "IsCountRPI")]
        public Boolean IsCountRPI { get; set; }

        [DataMember(Name = "IsRepresentative")]
        public Boolean IsRepresentative { get; set; }

        [DataMember(Name = "ThesisSummaryCN")]
        public String ThesisSummaryCN { get; set; }

        [DataMember(Name = "ThesisCoAuthorUpload")]
        public Boolean ThesisCoAuthorUpload { get; set; }

        [DataMember(Name = "ThesisCoAuthorUploadName")]
        public String ThesisCoAuthorUploadName { get; set; }

        [DataMember(Name = "ThesisBuildDate")]
        public DateTime ThesisBuildDate { get; set; }

        [DataMember(Name = "ThesisModifyDate")]
        public DateTime ThesisModifyDate { get; set; }


    }
}
