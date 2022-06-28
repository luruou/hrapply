using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["AuditorSnId"] != null && Request.QueryString["AcctRole"] != null)
            {
                A1.HRef = "~/ManageList.aspx?AuditorSnId=" + Request.QueryString["AuditorSnId"].ToString() + "&AcctRole=" + Request.QueryString["AcctRole"].ToString();
                A2.HRef = "~/CandidateAuditor.aspx?AuditorSnId=" + Request.QueryString["AuditorSnId"].ToString() + "&AcctRole=" + Request.QueryString["AcctRole"].ToString();
                A4.HRef = "~/UnderTake.aspx?AuditorSnId=" + Request.QueryString["AuditorSnId"].ToString() + "&AcctRole=" + Request.QueryString["AcctRole"].ToString();
            }
        }

        protected void lkb_logout_Click(object sender, EventArgs e)
        {
            Session["LoginEmail"] = null;
            Session.Abandon();
            Response.Redirect("http://hr2sys.tmu.edu.tw/HrApply/Default.aspx");
        }

    }
}
