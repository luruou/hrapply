using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class UpdateExecuteAuditAccept : System.Web.UI.Page
    {
        CRUDObject crudObject = new CRUDObject();
        VAuditExecute vAuditExecute = new VAuditExecute();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["ExecuteSn"])) 
            {
                vAuditExecute.ExecuteSn = Convert.ToInt32(Request.Form["ExecuteSn"].ToString());
                 
            }
            string Accept = "";
            if (!string.IsNullOrEmpty(Request.Form["AcceptValue"])) Accept = Request.Form["AcceptValue"];
            if (Accept.Equals("true"))
            {
                vAuditExecute.ExecuteAccept = true;
                crudObject.UpdateExecuteAuditAccept(vAuditExecute);
                AcceptMessage.Text = "確認您已接受此份審核工作，謝謝!!";
            }
            else
            {
                vAuditExecute.ExecuteAccept = false;
                crudObject.UpdateExecuteAuditAccept(vAuditExecute);
                AcceptMessage.Text = "確認您已取消此份審核工作，謝謝!!";
            }
        }
    }
}
