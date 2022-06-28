using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VTeacherTmuLesson
    {

        public string strLessonSn = "LessonSn";

        public string strEmpSn = "EmpSn";

        public string strLessonYear = "LessonYear";

        public string strLessonSemester = "LessonSemester";

        public string strLessonDeptLevel = "LessonDeptLevel";

        public string strLessonClass = "LessonClass";

        public string strLessonCreditHours = "LessonCreditHours";

        public string strLessonHours = "LessonHours";

        public string strLessonEvaluate = "LessonEvaluate";

        public string strLessonUserId = "LessonUserId";

        public string strLessonUpdateDate = "LessonUpdateDate";

        [DataMember(Name = "LessonSn")]
        public int LessonSn { get; set; }

        [DataMember(Name = "EmpSn")]
        public int EmpSn { get; set; }

        [DataMember(Name = "LessonYear")]
        public String LessonYear { get; set; }

        [DataMember(Name = "LessonSemester")]
        public String LessonSemester { get; set; }

        [DataMember(Name = "LessonDeptLevel")]
        public String LessonDeptLevel { get; set; }

        [DataMember(Name = "LessonClass")]
        public String LessonClass { get; set; }
        
        [DataMember(Name = "LessonCreditHours")]
        public String LessonCreditHours { get; set; }

        [DataMember(Name = "LessonHours")]
        public String LessonHours { get; set; }


        [DataMember(Name = "LessonEvaluate")]
        public String LessonEvaluate { get; set; }

        [DataMember(Name = "LessonUserId")]
        public String LessonUserId { get; set; }

        [DataMember(Name = "LessonUpdateDate")]
        public String LessonUpdateDate { get; set; }
    }
}
