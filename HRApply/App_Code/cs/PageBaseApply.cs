using System;
using System.Collections.Generic;
using System.Web;

namespace ApplyPromote
{
    public class PageBaseApply : System.Web.UI.Page
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


           window.location.href='Default.aspx'; 

        }

    </script>";

            ClientScript.RegisterClientScriptBlock(this.GetType(), "Redirect", str_Script);

        }

    }
}
