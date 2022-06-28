using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VApplyerData
    {

        public string strAppSn = "AppSn";

        public string strEmpSn = "EmpSn";

        public string strAppLawNumNo = "AppLawNumNo";

        public string strEmpIdno = "EmpIdno";

        public string strEmpNameCN = "EmpNameCN";

        public string strKindName = "KindName";

        public string strUntNameFull = "unt_name_full";

        public string strAttributeName = "AttributeName";

        public string strJobTitleName = "JobTitleName";

        public string strJobAttrName = "JobAttrName";

        public string strAuditProgressName = "AuditProgressName";

        public string strLawContent = "LawContent";

        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public Int32 EmpSn { get; set; }

        [DataMember(Name = "AppLawNumNo")]
        public String AppLawNumNo { get; set; }

        [DataMember(Name = "EmpIdno")]
        public String EmpIdno { get; set; }

        [DataMember(Name = "EmpNameCN")]
        public String EmpNameCN { get; set; }

        [DataMember(Name = "KindName")]
        public String KindName { get; set; }

        [DataMember(Name = "AttributeName")]
        public String AttributeName { get; set; }

        [DataMember(Name = "UntNameFull")]
        public String UntNameFull { get; set; }

        [DataMember(Name = "JobTitleName")]
        public String JobTitleName { get; set; }

        [DataMember(Name = "JobAttrName")]
        public String JobAttrName { get; set; }

        [DataMember(Name = "AuditProgressName")]
        public String AuditProgressName { get; set; }

        [DataMember(Name = "LawContent")]
        public String LawContent { get; set; }

    }
}
