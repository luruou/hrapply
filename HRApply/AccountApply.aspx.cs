using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class AccountApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AccountApply_Click(object sender, EventArgs e)
        {
            CRUDObject crudObject = new CRUDObject();
            VAccountForApply vAccountForApply = new VAccountForApply();
            vAccountForApply.AcctApplyEmail = ApplyerEmail.Text.ToString();
            vAccountForApply.AcctApplyPassword = ApplyerPwd.Text.ToString();
            vAccountForApply.AcctApplyId = ApplyerId.Text.ToString().ToUpper();
            vAccountForApply.AcctApplyStatus = true;

            if (!crudObject.AccountForApplyExist(vAccountForApply))
            {
                if (crudObject.Insert(vAccountForApply))
                {
                    IsForeignCB.Checked = false;
                    
                    try
                    {
                        //Email
                        VSendEmail vSendEmail = new VSendEmail();
                        vSendEmail.MailToAccount = vAccountForApply.AcctApplyEmail;
                        vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
                        vSendEmail.MailSubject = "「教師聘任升等作業系統」--申請帳號成功通知";
                        vSendEmail.ToAccountName = "";
                        vSendEmail.MailContent = "<br><br> 新聘申請帳號成功!<br><font color=red>請盡速完成您的申請</font><br><br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/Default.aspx\">按此進入申請</a>  您的帳號:" + vAccountForApply.AcctApplyEmail + " <br/><br>您的密碼:" + vAccountForApply.AcctApplyPassword + " <br><br><br>台北醫大學 人資處 怡慧(2028) 伊芝(2066)<br>";
                        Mail mail = new Mail();
                        mail.SendEmail(vSendEmail);
                        //申請帳號成功!
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('帳號申請成功!!');", true);
                    }
                    catch (Exception ex)
                    {
                        string error = ex.ToString();
                        //
                        //申請帳號失敗!
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('帳號申請失敗，請洽資訊人員！');", true);

                    }
                    ApplyerEmail.Text = "";
                    ApplyerId.Text = "";
                }
            }
            else
            {
                //申請帳號存在
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('此帳號已存在!!');", true);

            }
        }
    }
}
