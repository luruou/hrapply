using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    [DataContract]
    public class VEmployeeBase
    {

        public string strEmpSn = "EmpSn";

        public string strEmpIdno = "EmpIdno";

        public string strEmpBirthDay = "EmpBirthDay";

        public string strEmpPassportNo = "EmpPassportNo";

        public string strEmpNameENFirst = "EmpNameENFirst";

        public string strEmpNameENLast = "EmpNameENLast";
        
        public string strEmpNameCN = "EmpNameCN";

        public string strEmpSex = "EmpSex";

        public string strEmpCountry = "EmpCountry";

        public string strEmpHomeTown = "EmpHomeTown";

        public string strEmpBornProvince = "EmpBornProvince";

        public string strEmpBornCity = "EmpBornCity";

        public string strEmpTelPub = "EmpTelPub";

        public string strEmpTelPri = "EmpTelPri";

        public string strEmpEmail = "EmpEmail";

        public string strEmpTownAddressCode = "EmpTownAddressCode";

        public string strEmpTownAddress = "EmpTownAddress";

        public string strEmpAddressCode = "EmpAddressCode";

        public string strEmpAddress = "EmpAddress";

        public string strEmpCell = "EmpCell";

        public string strEmpExpertResearch = "EmpExpertResearch";

        public string strEmpPhotoUpload = "EmpPhotoUpload";

        public string strEmpPhotoUploadName = "EmpPhotoUploadName";

        public string strEmpIdnoUpload = "EmpIdnoUpload";

        public string strEmpIdnoUploadName = "EmpIdnoUploadName";

        public string strEmpDegreeUpload = "EmpDegreeUpload";

        public string strEmpDegreeUploadName = "EmpDegreeUploadName";

        public string strEmpSelfTeachExperience = "EmpSelfTeachExperience";

        public string strEmpSelfReach = "EmpSelfReach";

        public string strEmpSelfDevelope = "EmpSelfDevelope";

        public string strEmpSelfSpecial = "EmpSelfSpecial";

        public string strEmpSelfImprove = "EmpSelfImprove";

        public string strEmpSelfContribute = "EmpSelfContribute";

        public string strEmpSelfCooperate = "EmpSelfCooperate";

        public string strEmpSelfTeachPlan = "EmpSelfTeachPlan";

        public string strEmpSelfLifePlan = "EmpSelfLifePlan";

        public string strEmpNoTeachExp = "EmpNoTeachExp";

        public string strEmpNoTeachCa = "EmpNoTeachCa";

        public string strEmpNoHonour = "EmpNoHonour";

        public string strEmpNowJobOrg = "EmpNowJobOrg";

        public string strEmpNote = "EmpNote";

        public string strEmpRecommendors = "EmpRecommendors";

        public string strEmpRecommendYear = "EmpRecommendYear";

        public string strEmpRecommendUpload = "EmpRecommendUpload";

        public string strEmpRecommendUploadName = "EmpRecommendUploadName";

        //public string strAppResumeUploadName = "AppResumeUploadName";

        //public string strThesisScoreUploadName = "ThesisScoreUploadName";

        public string strEmpBuildDate = "EmpBuildDate";

        public string strEmpStatus = "EmpStatus";

        public string strUpdateUserId = "UpdateUserId";


        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "EmpIdno")]
        public String EmpIdno { get; set; }

        [DataMember(Name = "EmpBirthDay")]
        public String EmpBirthDay { get; set; }

        [DataMember(Name = "EmpPassportNo")]
        public String EmpPassportNo { get; set; }

        [DataMember(Name = "EmpNameENFirst")]
        public String EmpNameENFirst { get; set; }

        [DataMember(Name = "EmpNameENLast")]
        public String EmpNameENLast { get; set; }

        [DataMember(Name = "EmpNameCN")]
        public String EmpNameCN { get; set; }

        [DataMember(Name = "EmpSex")]
        public String EmpSex { get; set; }

        [DataMember(Name = "EmpCountry")]
        public String EmpCountry { get; set; }

        [DataMember(Name = "EmpHomeTown")]
        public String EmpHomeTown { get; set; }

        [DataMember(Name = "EmpBornProvince")]
        public String EmpBornProvince { get; set; }

        [DataMember(Name = "EmpBornCity")]
        public String EmpBornCity { get; set; }

        [DataMember(Name = "EmpTelPub")]
        public String EmpTelPub { get; set; }

        [DataMember(Name = "EmpTelPri")]
        public String EmpTelPri { get; set; }

        [DataMember(Name = "EmpEmail")]
        public String EmpEmail { get; set; }

        [DataMember(Name = "EmpTownAddressCode")]
        public String EmpTownAddressCode { get; set; }

        [DataMember(Name = "EmpTownAddress")]
        public String EmpTownAddress { get; set; }

        [DataMember(Name = "EmpAddressCode")]
        public String EmpAddressCode { get; set; }

        [DataMember(Name = "EmpAddress")]
        public String EmpAddress { get; set; }

        [DataMember(Name = "EmpCell")]
        public String EmpCell { get; set; }

        [DataMember(Name = "EmpExpertResearch")]
        public String EmpExpertResearch { get; set; }

        [DataMember(Name = "EmpPhotoUpload")]
        public Boolean EmpPhotoUpload { get; set; }

        [DataMember(Name = "EmpPhotoUploadName")]
        public String EmpPhotoUploadName { get; set; }

        [DataMember(Name = "EmpIdnoUpload")]
        public Boolean EmpIdnoUpload { get; set; }

        [DataMember(Name = "EmpIdnoUploadName")]
        public String EmpIdnoUploadName { get; set; }

        [DataMember(Name = "EmpDegreeUpload")]
        public Boolean EmpDegreeUpload { get; set; }

        [DataMember(Name = "EmpDegreeUploadName")]
        public String EmpDegreeUploadName { get; set; }

        [DataMember(Name = "EmpSelfTeachExperience")]
        public String EmpSelfTeachExperience { get; set; }

        [DataMember(Name = "EmpSelfReach")]
        public String EmpSelfReach { get; set; }

        [DataMember(Name = "EmpSelfDevelope")]
        public String EmpSelfDevelope { get; set; }

        [DataMember(Name = "EmpSelfSpecial")]
        public String EmpSelfSpecial { get; set; }

        [DataMember(Name = "EmpSelfImprove")]
        public String EmpSelfImprove { get; set; }

        [DataMember(Name = "EmpSelfContribute")]
        public String EmpSelfContribute { get; set; }

        [DataMember(Name = "EmpSelfCooperate")]
        public String EmpSelfCooperate { get; set; }

        [DataMember(Name = "EmpSelfTeachPlan")]
        public String EmpSelfTeachPlan { get; set; }

        [DataMember(Name = "EmpSelfLifePlan")]
        public String EmpSelfLifePlan { get; set; }

        [DataMember(Name = "EmpNoTeachExp")]
        public Boolean EmpNoTeachExp { get; set; }

        [DataMember(Name = "EmpNoTeachCa")]
        public Boolean EmpNoTeachCa { get; set; }

        [DataMember(Name = "EmpNoHonour")]
        public Boolean EmpNoHonour { get; set; }

        [DataMember(Name = "EmpNowJobOrg")]
        public String EmpNowJobOrg { get; set; }

        [DataMember(Name = "EmpNote")]
        public String EmpNote { get; set; }

        [DataMember(Name = "EmpRecommendors")]
        public String EmpRecommendors { get; set; }

        [DataMember(Name = "EmpRecommendYear")]
        public String EmpRecommendYear { get; set; }

        [DataMember(Name = "EmpRecommendUpload")]
        public Boolean EmpRecommendUpload { get; set; }

        [DataMember(Name = "EmpRecommendUploadName")]
        public String EmpRecommendUploadName { get; set; }

        [DataMember(Name = "EmpBuildDate")]
        public DateTime EmpBuildDate { get; set; }

        [DataMember(Name = "EmpStatus")]
        public Boolean EmpStatus { get; set; }

        [DataMember(Name = "UpdateUserId")]
        public String UpdateUserId { get; set; }
    }


}
