using System.Text;
using System.Configuration;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace ApplyPromote
{
    public class Mail
    {
        public void Instance()
        {
        }

        public System.Boolean SendEmail(VSendEmail vSendEmail)
        {
            StringBuilder SqlText = new StringBuilder();
            ConnectionStringSettings Conn = WebConfigurationManager.ConnectionStrings["SendMailConnectionString"];
            try
            {
                using (SqlConnection SendConn = new SqlConnection(Conn.ConnectionString))
                {

                    string mailSystem = "教師聘任升等作業系統";
                    SendConn.Open();
                    SqlText.Length = 0;
                    SqlText.Append(" Insert into mail (MSystem,MFrom,MFromName,MTo,MSubject,MFormat,MBody) values");
                    SqlText.Append(" ('" + mailSystem + "','" + vSendEmail.MailFromAccount + "','臺北醫學大學',");
                    SqlText.Append(" '" + vSendEmail.MailToAccount + "','" + vSendEmail.MailSubject + "',");
                    SqlText.Append(" '0','" + vSendEmail.ToAccountName + "<br>您好， <br><br>" + vSendEmail.MailContent + "' ");
                    SqlText.Append(");");
                    SqlCommand command = new SqlCommand(SqlText.ToString(), SendConn);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (System.Exception e)
            {
                return false;
            }
        }
    }
}
