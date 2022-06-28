using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VApplyAudit
    {

        public VApplyAudit()
        {
            this.Coops = new List<VThesisCoop>();
            this.Questionnaires = new List<VApplyQuestionnaire>();
        }
        public string strAppSn = "AppSn";

        public string strEmpSn = "EmpSn";

        public string strEmpIdno = "EmpIdno";

        public string strAppYear = "AppYear";

        public string strAppSemester = "AppSemester";

        public string strAppTeachNo = "AppTeachNo";

        public string strAppKindNo = "AppKindNo";

        public string strAppWayNo = "AppWayNo";

        public string strAppAttributeNo = "AppAttributeNo";

        public string strAppUnitNo = "AppUnitNo";

        public string strAppJobTitleNo = "AppJobTitleNo";

        public string strAppJobTypeNo = "AppJobTypeNo";

        public string strAppLawNumNo = "AppLawNumNo";

        public string strAppJournalUpload = "AppJournalUpload";

        public string strAppJournalUploadName = "AppJournalUploadName";

        public string strAppPublicationName = "AppPublicationName";

        public string strAppPublicationUploadName = "AppPublicationUploadName";

        public string strAppDeclarationUpload = "AppDeclarationUpload";

        public string strAppDeclarationUploadName = "AppDeclarationUploadName";

        public string strAppPPMUpload = "AppPPMUpload";

        public string strAppPPMUploadName = "AppPPMUploadName";

        public string strAppOtherServiceUpload = "AppOtherServiceUpload";

        public string strAppOtherServiceUploadName = "AppOtherServiceUploadName";

        public string strAppOtherTeachingUpload = "AppOtherTeachingUpload";

        public string strAppOtherTeachingUploadName = "AppOtherTeachingUploadName";

        public string strAppDrCaUpload = "AppDrCaUpload";

        public string strAppDrCaUploadName = "AppDrCaUploadName";

        public string strAppTeacherCaUpload = "AppTeacherCaUpload";

        public string strAppTeacherCaUploadName = "AppTeacherCaUploadName";

        public string strAppDegreeName = "AppDegreeName";

        public string strAppDegreeUploadName = "AppDegreeUploadName";

        public string strAppSummaryCN = "AppSummaryCN";

        public string strAppResearchYear = "AppResearchYear";

        public string strAppThesisAccuScore = "AppThesisAccuScore";

        public string strAppRPIScore = "AppRPIScore";

        public string strAppBuildDate = "AppBuildDate";

        public string strAppModifyDate = "AppModifyDate";

        public string strAppUserId = "AppUserId";

        public string strAppStage = "AppStage";

        public string strAppStep = "AppStep";

        public string strAppStatus = "AppStatus";

        public string strAppRecommendors = "AppRecommendors";

        public string strAppRecommendYear = "AppRecommendYear";

        public string strAppRecommendUploadName = "AppRecommendUploadName";

        public string strAppResumeUploadName = "AppResumeUploadName";

        public string strThesisScoreUploadName = "ThesisScoreUploadName";

        public string strAppSelfTeachExperience = "AppSelfTeachExperience";

        public string strAppSelfReach = "AppSelfReach";

        public string strAppSelfDevelope = "AppSelfDevelope";

        public string strAppSelfSpecial = "AppSelfSpecial";

        public string strAppSelfImprove = "AppSelfImprove";

        public string strAppSelfContribute = "AppSelfContribute";

        public string strAppSelfCooperate = "AppSelfCooperate";

        public string strAppSelfTeachPlan = "AppSelfTeachPlan";

        public string strAppSelfLifePlan = "AppSelfLifePlan";

        public string strReasearchResultUploadName = "ReasearchResultUploadName";



        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "EmpIdno")]
        public String EmpIdno { get; set; }

        [DataMember(Name = "AppYear")]
        public String AppYear { get; set; }

        [DataMember(Name = "AppSemester")]
        public String AppSemester { get; set; }

        [DataMember(Name = "AppTeachNo")]
        public String AppTeachNo { get; set; }

        [DataMember(Name = "AppKindNo")]
        public String AppKindNo { get; set; }

        [DataMember(Name = "AppWayNo")]
        public String AppWayNo { get; set; }

        [DataMember(Name = "AppAttributeNo")]
        public String AppAttributeNo { get; set; }

        [DataMember(Name = "AppUnitNo")]
        public String AppUnitNo { get; set; }

        [DataMember(Name = "AppJobTitleNo")]
        public String AppJobTitleNo { get; set; }

        [DataMember(Name = "AppJobTypeNo")]
        public String AppJobTypeNo { get; set; }

        //[DataMember(Name = "AppLawKindNo")]
        //public String AppLawKindNo { get; set; }

        [DataMember(Name = "AppLawNumNo")]
        public String AppLawNumNo { get; set; }

        [DataMember(Name = "AppJournalUpload")]
        public Boolean AppJournalUpload { get; set; }

        [DataMember(Name = "AppJournalUploadName")]
        public String AppJournalUploadName { get; set; }

        [DataMember(Name = "AppPublicationName")]
        public String AppPublicationName { get; set; }

        [DataMember(Name = "AppPublicationUploadName")]
        public String AppPublicationUploadName { get; set; }

        [DataMember(Name = "AppDeclarationUpload")]
        public Boolean AppDeclarationUpload { get; set; }

        [DataMember(Name = "AppDeclarationUploadName")]
        public String AppDeclarationUploadName { get; set; }

        [DataMember(Name = "AppPPMUpload")]
        public Boolean AppPPMUpload { get; set; }

        [DataMember(Name = "AppPPMUploadName")]
        public String AppPPMUploadName { get; set; }

        [DataMember(Name = "AppOtherTeachingUpload")]
        public Boolean AppOtherTeachingUpload { get; set; }

        [DataMember(Name = "AppOtherTeachingUploadName")]
        public String AppOtherTeachingUploadName { get; set; }

        [DataMember(Name = "AppOtherServiceUpload")]
        public Boolean AppOtherServiceUpload { get; set; }

        [DataMember(Name = "AppOtherServiceUploadName")]
        public String AppOtherServiceUploadName { get; set; }

        [DataMember(Name = "AppDrCaUpload")]
        public Boolean AppDrCaUpload { get; set; }

        [DataMember(Name = "AppDrCaUploadName")]
        public String AppDrCaUploadName { get; set; }

        [DataMember(Name = "AppTeacherCaUpload")]
        public Boolean AppTeacherCaUpload { get; set; }

        [DataMember(Name = "AppTeacherCaUploadName")]
        public String AppTeacherCaUploadName { get; set; }

        [DataMember(Name = "AppDegreeName")]
        public String AppDegreeName { get; set; }

        [DataMember(Name = "AppDegreeUploadName")]
        public String AppDegreeUploadName { get; set; }

        [DataMember(Name = "AppSummaryCN")]
        public String AppSummaryCN { get; set; }

        [DataMember(Name = "AppResearchYear")]
        public String AppResearchYear { get; set; }

        [DataMember(Name = "AppThesisAccuScore")]
        public String AppThesisAccuScore { get; set; }

        [DataMember(Name = "AppRPIScore")]
        public String AppRPIScore { get; set; }

        [DataMember(Name = "AppBuildDate")]
        public DateTime AppBuildDate { get; set; }

        [DataMember(Name = "AppModifyDate")]
        public DateTime AppModifyDate { get; set; }

        [DataMember(Name = "AppUserId")]
        public String AppUserId { get; set; }

        [DataMember(Name = "AppStatus")]
        public Boolean AppStatus { get; set; }

        [DataMember(Name = "AppStage")]
        public String AppStage { get; set; }

        [DataMember(Name = "AppStep")]
        public String AppStep { get; set; }

        [DataMember(Name = "AppResumeUploadName")]
        public String AppResumeUploadName { get; set; }

        [DataMember(Name = "ThesisScoreUploadName")]
        public String ThesisScoreUploadName { get; set; }

        [DataMember(Name = "AppRecommendors")]
        public String AppRecommendors { get; set; }

        [DataMember(Name = "AppRecommendYear")]
        public String AppRecommendYear { get; set; }

        [DataMember(Name = "AppRecommendUploadName")]
        public String AppRecommendUploadName { get; set; }

        [DataMember(Name = "AppSelfTeachExperience")]
        public String AppSelfTeachExperience { get; set; }

        [DataMember(Name = "AppSelfReach")]
        public String AppSelfReach { get; set; }

        [DataMember(Name = "AppSelfDevelope")]
        public String AppSelfDevelope { get; set; }

        [DataMember(Name = "AppSelfSpecial")]
        public String AppSelfSpecial { get; set; }

        [DataMember(Name = "AppSelfImprove")]
        public String AppSelfImprove { get; set; }

        [DataMember(Name = "AppSelfContribute")]
        public String AppSelfContribute { get; set; }

        [DataMember(Name = "AppSelfCooperate")]
        public String AppSelfCooperate { get; set; }

        [DataMember(Name = "AppSelfTeachPlan")]
        public String AppSelfTeachPlan { get; set; }

        [DataMember(Name = "AppSelfLifePlan")]
        public String AppSelfLifePlan { get; set; }

        [DataMember(Name = "ReasearchResultUploadName")]
        public String ReasearchResultUploadName { get; set; }

        public List<VThesisCoop> Coops { get; set; }

        public List<VApplyQuestionnaire> Questionnaires { get;set;}
    }
}
