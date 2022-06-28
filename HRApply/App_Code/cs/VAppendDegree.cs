using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VAppendDegree
    {
        public string strAppDSn = "AppDSn";

        public string strAppSn = "AppSn";

        public string strAppDThesisOralList = "AppDThesisOralList";

        public string strAppDDegreeThesisName = "AppDDegreeThesisName";

        public string strAppDDegreeThesisNameEng = "AppDDegreeThesisNameEng";

        public string strAppDDegreeThesisUploadName = "AppDDegreeThesisUploadName";

        public string strAppDFgnEduDeptSchoolAdmit = "AppDFgnEduDeptSchoolAdmit";

        public string strAppDFgnDegreeName = "AppDFgnDegreeName";

        public string strAppDFgnDegreeUploadName = "AppDFgnDegreeUploadName";

        public string strAppDFgnGradeUpload = "AppDFgnGradeUpload";

        public string strAppDFgnGradeUploadName = "AppDFgnGradeUploadName";

        public string strAppDFgnSelectCourseUpload = "AppDFgnSelectCourseUpload";

        public string strAppDFgnSelectCourseUploadName = "AppDFgnSelectCourseUploadName";

        public string strAppDFgnEDRecordUpload = "AppDFgnEDRecordUpload";

        public string strAppDFgnEDRecordUploadName = "AppDFgnEDRecordUploadName";

        public string strAppDFgnJPAdmissionUpload = "AppDFgnJPAdmissionUpload";

        public string strAppDFgnJPAdmissionUploadName = "AppDFgnJPAdmissionUploadName";

        public string strAppDFgnJPGradeUpload = "AppDFgnJPGradeUpload";

        public string strAppDFgnJPGradeUploadName = "AppDFgnJPGradeUploadName";

        public string strAppDFgnJPEnrollCAUpload = "AppDFgnJPEnrollCAUpload";

        public string strAppDFgnJPEnrollCAUploadName = "AppDFgnJPEnrollCAUploadName";

        public string strAppDFgnJPDissertationPassUpload = "AppDFgnJPDissertationPassUpload";

        public string strAppDFgnJPDissertationPassUploadName = "AppDFgnJPDissertationPassUploadName";

        [DataMember(Name = "AppDSn")]
        public Int32 AppDSn { get; set; }

        [DataMember(Name = "AppSn")]
        public Int32 AppSn { get; set; }

        [DataMember(Name = "AppDThesisOralList")]
        public String AppDThesisOralList { get; set; }

        [DataMember(Name = "AppDDegreeThesisName")]
        public String AppDDegreeThesisName { get; set; }

        [DataMember(Name = "AppDDegreeThesisNameEng")]
        public String AppDDegreeThesisNameEng { get; set; }

        [DataMember(Name = "AppDDegreeThesisUploadName")]
        public String AppDDegreeThesisUploadName { get; set; }

        [DataMember(Name = "AppDFgnEduDeptSchoolAdmit")]
        public Boolean AppDFgnEduDeptSchoolAdmit { get; set; }

        [DataMember(Name = "AppDFgnDegreeName")]
        public Boolean AppDFgnDegreeName { get; set; }

        [DataMember(Name = "AppDFgnDegreeUploadName")]
        public String AppDFgnDegreeUploadName { get; set; }

        [DataMember(Name = "AppDFgnGradeUpload")]
        public Boolean AppDFgnGradeUpload { get; set; }

        [DataMember(Name = "AppDFgnGradeUploadName")]
        public String AppDFgnGradeUploadName { get; set; }

        [DataMember(Name = "AppDFgnSelectCourseUpload")]
        public Boolean AppDFgnSelectCourseUpload { get; set; }

        [DataMember(Name = "AppDFgnSelectCourseUploadName")]
        public String AppDFgnSelectCourseUploadName { get; set; }

        [DataMember(Name = "AppDFgnEDRecordUpload")]
        public Boolean AppDFgnEDRecordUpload { get; set; }

        [DataMember(Name = "AppDFgnEDRecordUploadName")]
        public String AppDFgnEDRecordUploadName { get; set; }

        [DataMember(Name = "AppDFgnJPAdmissionUpload")]
        public Boolean AppDFgnJPAdmissionUpload { get; set; }

        [DataMember(Name = "AppDFgnJPAdmissionUploadName")]
        public String AppDFgnJPAdmissionUploadName { get; set; }

        [DataMember(Name = "AppDFgnJPGradeUpload")]
        public Boolean AppDFgnJPGradeUpload { get; set; }

        [DataMember(Name = "AppDFgnJPGradeUploadName")]
        public String AppDFgnJPGradeUploadName { get; set; }

        [DataMember(Name = "AppDFgnJPEnrollCAUpload")]
        public Boolean AppDFgnJPEnrollCAUpload { get; set; }

        [DataMember(Name = "AppDFgnJPEnrollCAUploadName")]
        public String AppDFgnJPEnrollCAUploadName { get; set; }

        [DataMember(Name = "AppDFgnJPDissertationPassUpload")]
        public Boolean AppDFgnJPDissertationPassUpload { get; set; }

        [DataMember(Name = "AppDFgnJPDissertationPassUploadName")]
        public String AppDFgnJPDissertationPassUploadName { get; set; }
    }
}
