using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class AuditLogin : System.Web.UI.Page
    {
        string email;
        string password;
        CRUDObject crudObject;
        int AppEmpSn = 0;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            Session["OuterName"] = null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            email = this.AcctEmailAccount.Text.ToString().Trim();
            password = this.AcctPassword.Text.ToString().Trim();
            crudObject = new CRUDObject();

        }

        protected void Send_Click(object sender, EventArgs e)
        {
                if (this.email.Contains("@") == false)
                {
                    email = this.email + "@tmu.edu.tw";
                }

            VAccountForAudit vAccountForAudit = new VAccountForAudit();
            vAccountForAudit.AcctEmail = email;
            vAccountForAudit.AcctPassword = password;

            if (this.email != "" &&  crudObject.GetAccountForAudit(vAccountForAudit) != null)
            {
                if (vAccountForAudit != null && !String.IsNullOrEmpty(vAccountForAudit.AcctAuditorSnEmpId))
                    Session["OuterName"] = crudObject.GetOuterName(vAccountForAudit.AcctAuditorSnEmpId);
                //確認外審委員是否已接受-否
                if (!IsAccept(email, password))
                {
                    Session["OuterName"] = crudObject.CheckOuterNM(email, password);
                    GetSettings setting = new GetSettings();
                    String year =  setting.NowYear.ToString().Trim();
                    String semester = setting.NowSemester.ToString().Trim();
                    string parameter = "email=" + email + "&password="+password + "&AppYear=" + year.Trim() + "&AppSemester=" + semester.Trim();
                    Response.Redirect("~/AuditAccept.aspx?" + parameter);
                }
                else
                {
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
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('請輸入正確的帳號密碼!!');", true);
            }

        }

        private bool IsAccept(string strUsername, string strPassword)
        {
            bool isAccept = false;
            VAccountForAudit vAccountForAudit = new VAccountForAudit();
            vAccountForAudit.AcctEmail = strUsername;
            vAccountForAudit.AcctPassword = strPassword;
            vAccountForAudit = crudObject.GetAccountForAudit(vAccountForAudit);
            if (vAccountForAudit != null)
            {
                isAccept = vAccountForAudit.AcctStatus;
                return isAccept;
            }
            else
                return false;
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
                if (vAccountForAudit != null && !String.IsNullOrEmpty(vAccountForAudit.AcctAuditorSnEmpId))
                    Session["OuterName"] = crudObject.GetOuterName(vAccountForAudit.AcctAuditorSnEmpId);
                //Session["OuterName"] = vAccountForAudit.
                Session["AcctAuditorSnEmpId"] = vAccountForAudit.AcctAuditorSnEmpId;
                Session["AppSn"] = vAccountForAudit.AcctAppSn;
                Session["AcctRole"] = "A";
                VApplyAudit vApplyAudit = new VApplyAudit();
                EmpSn = crudObject.GetApplyAuditEmpSn(Int32.Parse(vAccountForAudit.AcctAppSn));
            }
            return EmpSn;
        }
    }
}
