using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VThesisOral
    {
        public string strThesisOralSn = "ThesisOralSn";

        public string strThesisOralAppSn = "ThesisOralAppSn";

        public string strThesisOralType = "ThesisOralType";

        public string strThesisOralName = "ThesisOralName";

        public string strThesisOralTitle = "ThesisOralTitle";

        public string strThesisOralUnit = "ThesisOralUnit";

        public string strThesisOralOther = "ThesisOralOther";

        [DataMember(Name = "ThesisOralSn")]
        public Int32 ThesisOralSn { get; set; }

        [DataMember(Name = "ThesisOralAppSn")]
        public Int32 ThesisOralAppSn { get; set; }

        [DataMember(Name = "ThesisOralType")]
        public String ThesisOralType { get; set; }

        [DataMember(Name = "ThesisOralName")]
        public String ThesisOralName { get; set; }

        [DataMember(Name = "ThesisOralTitle")]
        public String ThesisOralTitle { get; set; }

        [DataMember(Name = "ThesisOralUnit")]
        public String ThesisOralUnit { get; set; }

        [DataMember(Name = "ThesisOralOther")]
        public String ThesisOralOther { get; set; }

    }
}
