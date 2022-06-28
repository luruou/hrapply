using System;
using System.Collections.Generic;
using System.Web;

namespace ApplyPromote
{
    public class PageBaseManage : System.Web.UI.Page
    {

        protected override void OnPreRender(EventArgs e)
        {

            base.OnPreRender(e);

            AutoRedirect();

        }

        public void AutoRedirect()
        {

            int int_MilliSecondsTimeOut = (this.Session.Timeout * 60000);

            string str_Script = @"

       <script type='text/javascript'> 

        intervalset = window.setInterval('Redirect()'," + int_MilliSecondsTimeOut.ToString() + @");

        function Redirect()

        {


           window.location.href='ManageLogin.aspx'; 

        }

    </script>";

            ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", str_Script);

        }

        protected void ShowSessionTimeOut()
        {
            string message = "時間逾時,請重新登入!";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

    }
}
