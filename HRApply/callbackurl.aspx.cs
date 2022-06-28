using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data.SqlClient;
using ApplyPromote;

public partial class callbackurl : System.Web.UI.Page
{
    CRUDObject crudObject = new CRUDObject();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Request["SystemToken"] != "" && Request["SystemToken"] != null && Request["domainname"] != "" &&
                Request["domainname"] != null && Request["systemname"] != "" && Request["systemname"] != null)
            {
                string SystemToken = Request["SystemToken"].ToString();
                string domainname = Request["domainname"].ToString();
                string systemname = Request["systemname"].ToString();
                loadUserinfo(SystemToken, domainname, systemname);
            }
            else
            {
                Session["LoginEmail"] = null;
                Session.Abandon();
                googleLogin();
            }
        }
    }


    protected void loadUserinfo(string SystemToken, string domainname, string systemname)
    {
        //利用SystemToken再次詢問
        string tokenURL = "http://glbsys.tmu.edu.tw/OAuth/GoogleAccount.asmx/SystemTokenDecode?" +
                          "domainname={0}&systemname={1}&callbackurl={2}&SystemToken={3}";


        string callbackurl = "Default.aspx"; //TMU Token返回位置
        string sslport = "false";
        tokenURL = string.Format(tokenURL,
            HttpUtility.HtmlDecode(domainname),
            HttpUtility.HtmlDecode(systemname),
            HttpUtility.HtmlDecode(callbackurl),
            HttpUtility.HtmlDecode(SystemToken));

        string result = "";
        System.Net.WebClient client = new System.Net.WebClient();
        System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(tokenURL);
        System.Net.HttpWebResponse webresponse = (System.Net.HttpWebResponse)request.GetResponse();
        System.IO.StreamReader streamReader = new System.IO.StreamReader(webresponse.GetResponseStream(),
        System.Text.Encoding.GetEncoding("UTF-8")); //可改不同編碼 
        result = streamReader.ReadToEnd(); //回應結果

        //將回應結果轉XML再去剖析各欄位
        System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
        xDoc.LoadXml(result.ToString());
        xDoc.RemoveChild(xDoc.FirstChild);


        XmlNodeList emailElement = xDoc.GetElementsByTagName("email");
        XmlNodeList loginCodeElement = xDoc.GetElementsByTagName("loginCode");
        XmlNodeList loginTypeElement = xDoc.GetElementsByTagName("loginType");
        string vemail = emailElement[0].InnerXml;
        string loginCode = loginCodeElement[0].InnerXml; //登入身份 1校內帳號 2校外帳號
        string loginType = loginTypeElement[0].InnerXml; //登入身份說明
        if (loginCode == "1")
        {
            string[] _user = vemail.Split(new char[] { '@' });
            Session["LoginEmail"] = _user[0].ToString();
            string applyerData;

            #region 取得登入者資訊
            applyerData = crudObject.GetVEmployeeIdFromTmuHrByEmail(vemail);
            if (applyerData == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('抱歉您的身份非本校教職員!');", true);
                return;
            }
            else
            {
                Session["AcctAuditorSnEmpId"] = applyerData.Split(',')[0];
                Session["AcctAuditorSnEmpName"] = applyerData.Split(',')[1];
                Response.Redirect("Default.aspx?#nav-profile-tab");
            }
            #endregion
        }

        
    }

    protected void googleLogin()
    {
        //TMU驗證位置
        string oauthURL = "http://glbsys.tmu.edu.tw/OAuth/OAuthCheck.aspx?" +
                          "domainname={0}&systemname={1}&callbackurl={2}&sslport={3}";
        string domainname = "hr2sys.tmu.edu.tw";//主機
        string systemname = "HrApply";//系統
        string callbackurl = "callbackurl.aspx";//TMU Token返回位置
        string sslport = "false";
        oauthURL = string.Format(oauthURL,
            HttpUtility.HtmlDecode(domainname),
            HttpUtility.HtmlDecode(systemname),
            HttpUtility.HtmlDecode(callbackurl),
            HttpUtility.HtmlDecode(sslport));

        HttpContext.Current.Response.Redirect(oauthURL);
    }
}