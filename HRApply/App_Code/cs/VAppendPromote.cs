using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAppendPromote
    {

        public string strAppPSn = "AppPSn";

        public string strAppSn = "AppSn";

        public string strNowJobYear = "NowJobYear";

        public string strNowJobUnit = "NowJobUnit";
       
        public string strNowJobTitle = "NowJobTitle";

        public string strNowJobPosId = "NowJobPosId";

        public string strExpServiceCaUploadName = "ExpServiceCaUploadName";

        public string strPBLClassUploadName = "PBLClassUploadName";

        public string strRPIDiscountNo = "RPIDiscountNo";
        
        public string strRPIDiscountScore1 = "RPIDiscountScore1";

        public string strRPIDiscountScore1Name = "RPIDiscountScore1Name";

        public string strRPIDiscountScore2 = "RPIDiscountScore2";

        public string strRPIDiscountScore2Name = "RPIDiscountScore2Name";

        public string strRPIDiscountScore3 = "RPIDiscountScore3";

        public string strRPIDiscountScore3Name = "RPIDiscountScore3Name";

        public string strRPIDiscountScore4 = "RPIDiscountScore4";

        public string strRPIDiscountScore4Name = "RPIDiscountScore4Name";

        public string strRPIDiscountScore5 = "RPIDiscountScore5";

        public string strRPIDiscountTotal = "RPIDiscountTotal";

        public string strReasearchResultUploadName = "ReasearchResultUploadName";

        [DataMember(Name = "AppPSn")]
        public Int32 AppPSn { get; set; }

        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "NowJobYear")]
        public String NowJobYear { get; set; }

        [DataMember(Name = "NowJobUnit")]
        public String NowJobUnit { get; set; }

        [DataMember(Name = "NowJobTitle")]
        public String NowJobTitle { get; set; }

        [DataMember(Name = "NowJobPosId")]
        public String NowJobPosId { get; set; }

        [DataMember(Name = "ExpServiceUploadCaName")]
        public String ExpServiceCaUploadName { get; set; }

        [DataMember(Name = "PBLClassUploadName")]
        public String PBLClassUploadName { get; set; }

        [DataMember(Name = "RPIDiscountNo")]
        public Boolean RPIDiscountNo { get; set; }	

        [DataMember(Name = "RPIDiscountScore1")]
        public String RPIDiscountScore1 { get; set; }

        [DataMember(Name = "RPIDiscountScore1Name")]
        public String RPIDiscountScore1Name { get; set; }

        [DataMember(Name = "RPIDiscountScore2")]
        public String RPIDiscountScore2 { get; set; }

        [DataMember(Name = "RPIDiscountScore2Name")]
        public String RPIDiscountScore2Name { get; set; }

        [DataMember(Name = "RPIDiscountScore3")]
        public String RPIDiscountScore3 { get; set; }

        [DataMember(Name = "RPIDiscountScore3Name")]
        public String RPIDiscountScore3Name { get; set; }

        [DataMember(Name = "RPIDiscountScore4")]
        public String RPIDiscountScore4 { get; set; }

        [DataMember(Name = "RPIDiscountScore4Name")]
        public String RPIDiscountScore4Name { get; set; }

        [DataMember(Name = "RPIDiscountScore5")]
        public String RPIDiscountScore5 { get; set; }

        [DataMember(Name = "RPIDiscountTotal")]
        public String RPIDiscountTotal { get; set; }

        [DataMember(Name = "ReasearchResultUploadName")]
        public String ReasearchResultUploadName { get; set; }

    }
}
