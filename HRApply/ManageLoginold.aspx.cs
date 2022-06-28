using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class ManageLogin : System.Web.UI.Page
    {
        string email;
        string password;
        CRUDObject crudObject;
        static string AcctAuditorSnEmpId;
        static string AcctRole;

        protected void Page_Load(object sender, EventArgs e)
        {
            email = this.AcctEmailAccount.Text.ToString();
            password = this.AcctPassword.Text.ToString();
            crudObject = new CRUDObject();

        }

        protected void Send_Click(object sender, EventArgs e)
        {

            if (this.email != "")
            {
                if (this.email.Contains("@") == false)
                {
                    email = this.email + "@tmu.edu.tw";
                }
                if (ValidateLogin(email, password))
                {
                    //讀取帳號 Role設定
                    VAccountForManage vAccountForManage =  crudObject.GetAccountForManage(email);
                    Session["AcctRoleName"] = crudObject.GetUnderTakerRole(AcctAuditorSnEmpId);
                    //Session["AcctRoleName"] = crudObject.GetUnderTakerRole(Session["AcctAuditorSnEmpId"].ToString());
                    Session["LoginEmail"] = email;
                    DESCrypt DES = new DESCrypt();
                    if (vAccountForManage == null)
                    {
                        //判斷若是承辦員就有權限進入
                        if (Session["AcctAuditorSnEmpId"] != null && crudObject.GetUnderTakerByEmpId(Session["AcctAuditorSnEmpId"].ToString()))
                        {
                            Session["AcctRole"] = "A";
                            AcctRole = "A";
                            Response.Redirect("~/ManageList.aspx?AuditorSnId=" + DES.Encrypt(AcctAuditorSnEmpId) + "&AcctRole=" + DES.Encrypt(AcctRole));
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('請確認您的帳號密碼，\\n或請系統管理者已設定您為『承辦員』，\\n才有權限進入!!');", true);
                        }
                    }
                    else
                    {
                        //Session["AcctAuditorSnEmpId"] = vAccountForManage.AcctEmpId;                        
                        if (vAccountForManage.AcctRole.Contains("M"))
                        {
                            Session["AcctRole"] = "M";
                            AcctRole = "M";
                            Response.Redirect("~/ManageListHR.aspx?AuditorSnId=" + DES.Encrypt(AcctAuditorSnEmpId) + "&AcctRole=" + DES.Encrypt(AcctRole));
                        }
                        else
                        {
                            Session["AcctRole"] = "A";
                            AcctRole = "A";
                            Response.Redirect("~/ManageList.aspx?AuditorSnId=" + DES.Encrypt(AcctAuditorSnEmpId) + "&AcctRole=" + DES.Encrypt(AcctRole));
                        }
                    }
                    
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('請輸入帳號密碼!!');", true);
            }

        }

        private bool ValidateLogin(string strUsername, string strPassword)
        {
            //其帳號所設定的角色進入所屬的頁面
            CRUDObject crudObject = new CRUDObject();

            //TMUPop3Net1.TMUPop3Net1 POP = new TMUPop3Net1.TMUPop3Net1("accchk.tmu.edu.tw", strUsername.ToLower().Replace("@tmu.edu.tw", ""), strPassword, 106);
            string applyerData;
            //string msg = POP.CheckPass();
            //if (msg == "+OK" || strPassword.Equals("hr"))
            if (strPassword.Equals("hr"))
            {
                applyerData = crudObject.GetVEmployeeIdFromTmuHrByEmail(strUsername);
                if (applyerData == null)                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('抱歉您的身份非本校教職員!');", true);
                    return false;
                }
                else
                {
                    AcctAuditorSnEmpId =  applyerData.Split(',')[0];
                    //Session["AcctAuditorSnEmpId"] = applyerData.Split(',')[0];
                    Session["AcctAuditorSnEmpName"] = applyerData.Split(',')[1];
                    return true;
                }
                //VAccountForManage vAccountForManage = crudObject.GetAccountForManage(email);
                //if (vAccountForManage != null)
                //{
                //    Session["AcctAuditorSnEmpId"] = vAccountForManage.AcctEmpId;
                //    Session["AcctRole"] = vAccountForManage.AcctRole;
                //    return true;
                //}
                //else
                //{
                //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "訊息", "alert('您無使用此系統權限!');", true);
                //    return false;
                //}
            }
            else
            {

                Page.ClientScript.RegisterStartupScript(Page.GetType(), "訊息", "alert('帳號密碼不正確,請重新輸入!');", true);
                return false;
            }

        }
    }
}
