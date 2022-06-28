using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MessageAgree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


    }


    protected void Button3_Click(object sender, EventArgs e)
    {
        String parameters = "";
        if (Request.QueryString["KindNo"] == "1") {
            if (Request.QueryString["RegisterStartup"] != null)
            {
                parameters = Session["parameters"].ToString();
                //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + "ApplyEmp.aspx?" + parameters + "';", true);
                Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + "ApplyList.aspx?" + parameters + "';", true);
            }
            else
            {
                parameters = Session["parameters"].ToString();
                //Response.Redirect("~/ApplyEmp.aspx?" + parameters);
                Response.Redirect("~/ApplyList.aspx?" + parameters);
            }
        }
        if (Request.QueryString["KindNo"] == "2")
        {
            if (Request.QueryString["RegisterStartup"] != null)
            {
                parameters = Session["parameters"].ToString();
                //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + "ApplyShortEmp.aspx?" + parameters + "';", true);
                Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + "ApplyList.aspx?" + parameters + "';", true);
            }
            else
            {
                parameters = Session["parameters"].ToString();
                //Response.Redirect("~/ApplyShortEmp.aspx?" + parameters);
                Response.Redirect("~/ApplyList.aspx?" + parameters);
            }
        }
        if (Request.QueryString["KindNo"] == "3")
        {
            if (Request.QueryString["RegisterStartup"] != null)
            {
                parameters = Session["parameters"].ToString();
                //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + "PromoteEmp.aspx?" + parameters + "';", true);
                Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + "ApplyList.aspx?" + parameters + "';", true);
            }
            else
            {
                parameters = Session["parameters"].ToString();
                //Response.Redirect("~/PromoteEmp.aspx?" + parameters);
                Response.Redirect("~/ApplyList.aspx?" + parameters);
            }
        }
    }
}