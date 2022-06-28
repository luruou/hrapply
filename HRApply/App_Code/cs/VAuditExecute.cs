using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAuditExecute
    {
        public string strExecuteSn = "ExecuteSn";

        public string strAppSn = "AppSn"; 

        public string strExecuteStage = "ExecuteStage";

        public string strExecuteStep = "ExecuteStep";

        public string strExecuteRoleName = "ExecuteRoleName";

        public string strExecuteAuditorSnEmpId = "ExecuteAuditorSnEmpId";

        public string strExecuteAuditorEmail = "ExecuteAuditorEmail";

        public string strExecuteAuditorName = "ExecuteAuditorName";

        public string strExecuteAccept = "ExecuteAccept";

        public string strExecuteCommentsA = "ExecuteCommentsA";

        public string strExecuteCommentsB = "ExecuteCommentsB";

        public string strExecuteStrengths = "ExecuteStrengths";

        public string strExecuteWeaknesses = "ExecuteWeaknesses";

        public string strExecuteAllTotalScore = "ExecuteTotalScore";

        public string strExecuteLevelScore = "ExecuteLevelScore";

        public string strExecuteWSSubject = "ExecuteWSSubject";

        public string strExecuteWSMethod = "ExecuteWSMethod";

        public string strExecuteWSContribute = "ExecuteWSContribute";

        public string strExecuteWSAchievement = "ExecuteWSAchievement";

        public string strExecuteWTotalScore = "ExecuteWTotalScore";        

        public string strExecuteDate = "ExecuteDate";

        public string strExecuteBngDate = "ExecuteBngDate";

        public string strExecuteEndDate = "ExecuteEndDate";

        public string strExecutePass = "ExecutePass";

        public string strExecuteStatus = "ExecuteStatus";

        public string strExecuteReturnReason = "ExecuteReturnReason";

        public string strExecuteTeachingScore = "ExecuteTeachingScore";

        public string strExecuteServiceScore = "ExecuteServiceScore";

        public string strARTitleName = "ARTitleName";

        [DataMember(Name = "ExecuteSn")]
        public Int32 ExecuteSn { get; set; }

        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "ExecuteStage")]
        public String ExecuteStage { get; set; }

        [DataMember(Name = "ExecuteStep")]
        public String ExecuteStep { get; set; }

        [DataMember(Name = "ExecuteRoleName")]
        public String ExecuteRoleName { get; set; }

        [DataMember(Name = "ExecuteAuditorSnEmpId")]
        public String ExecuteAuditorSnEmpId { get; set; }

        [DataMember(Name = "ExecuteAuditorEmail")]
        public String ExecuteAuditorEmail { get; set; }

        [DataMember(Name = "ExecuteAuditorName")]
        public String ExecuteAuditorName { get; set; }

        [DataMember(Name = "ExecuteAccept")]
        public Boolean ExecuteAccept { get; set; }

        [DataMember(Name = "ExecuteCommentsA")]
        public String ExecuteCommentsA { get; set; }

        [DataMember(Name = "ExecuteCommentsB")]
        public String ExecuteCommentsB { get; set; }

        [DataMember(Name = "ExecuteStrengths")]
        public String ExecuteStrengths { get; set; }

        [DataMember(Name = "ExecuteWeaknesses")]
        public String ExecuteWeaknesses { get; set; }

        [DataMember(Name = "ExecuteAllTotalScore")]
        public String ExecuteAllTotalScore { get; set; }

        [DataMember(Name = "ExecuteLevelScore")]
        public String ExecuteLevelScore { get; set; }

        [DataMember(Name = "ExecuteWSSubject")]
        public String ExecuteWSSubject { get; set; }

        [DataMember(Name = "ExecuteWSMethod")]
        public String ExecuteWSMethod { get; set; }

        [DataMember(Name = "ExecuteWSContribute")]
        public String ExecuteWSContribute { get; set; }

        [DataMember(Name = "ExecuteWSAchievement")]
        public String ExecuteWSAchievement { get; set; }

        [DataMember(Name = "ExecuteWTotalScore")]
        public String ExecuteWTotalScore { get; set; }        

        [DataMember(Name = "ExecuteWritingScoreSn")]
        public int ExecuteWritingScoreSn { get; set; }

        [DataMember(Name = "ExecuteDate")]
        public DateTime ExecuteDate { get; set; }

        [DataMember(Name = "ExecuteBngDate")]
        public DateTime ExecuteBngDate { get; set; }

        [DataMember(Name = "ExecuteEndDate")]
        public DateTime ExecuteEndDate { get; set; }

        [DataMember(Name = "ExecutePass")]
        public String ExecutePass { get; set; }

        [DataMember(Name = "ExecuteStatus")]
        public Boolean ExecuteStatus { get; set; }

        [DataMember(Name = "ExecuteReturnReason")]
        public String ExecuteReturnReason { get; set; }

        [DataMember(Name = "ExecuteTeachingScore")]
        public String ExecuteTeachingScore { get; set; }

        [DataMember(Name = "ExecuteServiceScore")]
        public String ExecuteServiceScore { get; set; }

        [DataMember(Name = "ARTitleName")]
        public String ARTitleName { get; set; }
    }
}
