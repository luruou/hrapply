using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class AuditAccept : System.Web.UI.Page
    {
        string email;
        string password;
        CRUDObject crudObject;
        int AppEmpSn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Request["email"].Equals("") && !Request["password"].Equals(""))
            {
                email = Request["email"].ToString();
                password = Request["password"].ToString();
                LbAcctEmailAccount.Text = email;
            }         

            crudObject = new CRUDObject();

        }

        protected void Send_Click(object sender, EventArgs e)
        {
            VAccountForAudit vAccountForAudit = new VAccountForAudit();
            vAccountForAudit.AcctEmail = Request.QueryString["email"];
            vAccountForAudit.AcctPassword = Request.QueryString["password"];
            vAccountForAudit = crudObject.GetAccountForAudit(vAccountForAudit);
            Session["AppSn"] = vAccountForAudit.AcctAppSn;
            //寫入外審委員接受狀態
            if (crudObject.UpdateAuditAcceptStatus(email, password, AuditAgree.SelectedValue.ToString().Equals("1") ? true : false) && AuditAgree.SelectedValue.ToString().Equals("1"))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('您已同受理審核資料!!');", true);
                AppEmpSn = ValidateLogin(email, password);
                if (AppEmpSn > 0)
                {
                    Session["OuterName"] = crudObject.CheckOuterNM(email, password);
                    Session["AcctRole"] = "A";
                    GetSettings setting = new GetSettings();
                    String year = setting.GetYear().ToString().Trim();
                    String semester = setting.GetSemester().ToString().Trim();
                    Response.Redirect("~/OuterAudit.aspx?EmpSn=" + AppEmpSn + "&AppYear=" + year.Trim() + "&AppSemester=" + semester.Trim() + "&AppSn=" + Session["AppSn"].ToString() + "&AcctAuditorSnEmpId=" + Session["AcctAuditorSnEmpId"].ToString());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('請確認密碼正確並且帳號在有效期限內!!');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('對於您未能受理審核資料感到遺憾!!');", true);
            }

        }

        private int ValidateLogin(string strUsername, string strPassword)
        {
            int EmpSn = 0;
            VAccountForAudit vAccountForAudit = new VAccountForAudit();
            vAccountForAudit.AcctEmail = strUsername;
            vAccountForAudit.AcctPassword = strPassword;
            vAccountForAudit = crudObject.GetAccountForAudit(vAccountForAudit);
            if (vAccountForAudit != null)
            {
                Session["AcctAuditorSnEmpId"] = vAccountForAudit.AcctAuditorSnEmpId; 
                Session["AcctRole"] = "A";
                VApplyAudit vApplyAudit = new VApplyAudit();
                EmpSn = crudObject.GetApplyAuditEmpSn(Int32.Parse(vAccountForAudit.AcctAppSn));
            }
            return EmpSn;
        }
    }
}
