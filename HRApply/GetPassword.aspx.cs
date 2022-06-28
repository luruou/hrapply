using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class GetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GetPassword_Click(object sender, EventArgs e)
        {
            CRUDObject crudObject = new CRUDObject();
            VAccountForApply vAccountForApply = new VAccountForApply();
            vAccountForApply.AcctApplyEmail = ApplyerEmail.Text.ToString();
            vAccountForApply.AcctApplyId = ApplyerId.Text.ToString();
            vAccountForApply.AcctApplyCell = ApplyerCell.Text.ToString();
            vAccountForApply.AcctApplyBirthday = ApplyerBirthday.Text.ToString();
            String strPwd = crudObject.GetAccountPwd(vAccountForApply);
            if (strPwd.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請確認您輸入的資料正確!!');", true);
            }else{
                //申請帳號成功!
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您的密碼已寄至您的帳號信箱!!');", true);
                try
                {
                    //Email
                    VSendEmail vSendEmail = new VSendEmail();
                    vSendEmail.MailToAccount = vAccountForApply.AcctApplyEmail;
                    vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
                    vSendEmail.MailSubject = "台北醫學大學新聘申請系統--密碼通知";
                    vSendEmail.ToAccountName = "";
                    vSendEmail.MailContent = "<br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/Default.aspx\">按此進入新聘申請</a>  您的帳號:" + vAccountForApply.AcctApplyEmail + " <br/><br>您的密碼:" + strPwd + " <br><br><br>台北醫大學 人資處 怡慧(2028) 伊芝(2066)<br>";
                    Mail mail = new Mail();
                    mail.SendEmail(vSendEmail);
                }
                catch (Exception ex)
                {
                    //

                }
            }
        }
    }
}
