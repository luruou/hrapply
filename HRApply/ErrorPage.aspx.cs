using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Exception exc = Server.GetLastError();
        this.phError.Controls.Add(new LiteralControl(exc.InnerException.Message));
        this.phError.Controls.Add(new LiteralControl("<br/>"));
        this.phError.Controls.Add(new LiteralControl(exc.InnerException.ToString()));

        Server.ClearError();
    }
}