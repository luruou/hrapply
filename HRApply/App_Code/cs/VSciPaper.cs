using System;
using System.Runtime.Serialization;

namespace ApplyPromote
{
    public class VSciPaper
    {
        public string strRR = "RR";

        public string strRRNo = "paperClass";

        public string strPaperName = "paper_name";

        public string strPublishYear = "pYear";

        public string strMonth = "pMonth";

        public string strImpactFactor = "Impact_Factor";

        public string strJurnalRank = "Jurnal_Rank";

        public string strAuthor = "author";

        public string strC_Score = "C_Score";

        public string strJ_Score = "J_Score";

        public string strA_Score = "A_Score";

        public string strTotalScore = "TotalScore";


        [DataMember(Name = "RR")]
        public String UntRRId { get; set; }

        [DataMember(Name = "RRNo")]
        public String RRNo = "RRNo";


        [DataMember(Name = "PaperName")]
        public String PaperName { get; set; }


        [DataMember(Name = "PublishYear")]
        public String PublishYear { get; set; }

        [DataMember(Name = "Month")]
        public String Month { get; set; }

        [DataMember(Name = "ImpactFactor")]
        public String ImpactFactor { get; set; }

        [DataMember(Name = "JurnalRank")]
        public String JurnalRank { get; set; }

        [DataMember(Name = "Author")]
        public String Author { get; set; }

        [DataMember(Name = "C_Score")]
        public String C_Score { get; set; }

        [DataMember(Name = "J_Score")]
        public String J_Score { get; set; }

        [DataMember(Name = "A_Score")]
        public String A_Score { get; set; } 

        [DataMember(Name = "TotalScore")]
        public String TotalScore { get; set; } 
    }
}
