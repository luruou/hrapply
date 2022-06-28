using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_Language : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    protected void lbtnZHTW_Click(object sender, EventArgs e)
    {
        SetLanguage("zh-TW");
    }

    protected void lbtnENUS_Click(object sender, EventArgs e)
    {
        SetLanguage("en-US");
    }

    protected void SetLanguage(string language)
    {
        HttpCookie cookie = new HttpCookie("Lang");
        cookie.Value = language;
        cookie.Expires = DateTime.Now.AddDays(2);
        Response.Cookies.Add(cookie);
        Response.Redirect(Page.Request.Url.ToString(), true);
    }
}