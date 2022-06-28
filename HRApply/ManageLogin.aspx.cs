using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ApplyPromote
{
    public partial class ManageLogin : System.Web.UI.Page
    {
        CRUDObject crudObject;
        static string  AcctAuditorSnEmpId;
        static string AcctRole;

        protected void Page_Load(object sender, EventArgs e)
        {
            string SystemToken = "";
            string domainname = "";
            string systemname = "";
            //email = this.AcctEmailAccount.Text.ToString();
            //password = this.AcctPassword.Text.ToString();
            crudObject = new CRUDObject();

            if (!this.IsPostBack)
            {
                try
                {
                    if (Request["SystemToken"] != "" && Request["SystemToken"] != null && Request["domainname"] != "" && Request["domainname"] != null && Request["systemname"] != "" && Request["systemname"] != null)
                    {
                        SystemToken = Request["SystemToken"].ToString();
                        domainname = Request["domainname"].ToString();
                        systemname = Request["systemname"].ToString();
                        //利用SystemToken再次詢問
                        string tokenURL = "http://glbsys.tmu.edu.tw/OAuth/GoogleAccount.asmx/SystemTokenDecode?" +
                        "domainname={0}&systemname={1}&callbackurl={2}&SystemToken={3}";

                        string callbackurl = "ManageLogin.aspx";//TMU Token返回位置
                        string sslport = "true";
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

                        if (vemail != null && vemail != "")
                        {
                            string[] _user = vemail.Split(new char[] { '@' });
                            Session["LoginEmail"] = _user[0].ToString();
                            string applyerData;

                            #region 取得登入者資訊
                            applyerData = crudObject.GetVEmployeeIdFromTmuHrByEmail(vemail);
                            if (applyerData == null)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('抱歉!您尚未開通本系統審核權限，請洽人資處承辦處理');", true);
                                return;
                            }
                            else
                            {
                                AcctAuditorSnEmpId = applyerData.Split(',')[0];
                                //Session["AcctAuditorSnEmpId"] = applyerData.Split(',')[0];
                                Session["AcctAuditorSnEmpName"] = applyerData.Split(',')[1];
                                txt_identity.Text = Session["AcctAuditorSnEmpName"].ToString() +"(" + Session["LoginEmail"].ToString() + ")";
                            }
                            #endregion
                        }

                        else
                        {
                            //TMU驗證位置
                            string oauthURL = "http://glbsys.tmu.edu.tw/OAuth/OAuthCheck.aspx?domainname={0}&systemname={1}&callbackurl={2}&sslport={3}";
                            //domainname = "10.20.16.127";//主機
                            domainname = "hr2sys.tmu.edu.tw";//主機
                            systemname = "HrApply";//系統
                            callbackurl = "ManageLogin.aspx";//TMU Token返回位置
                            sslport = "true";
                            oauthURL = string.Format(oauthURL,
                                            HttpUtility.HtmlDecode(domainname),
                                            HttpUtility.HtmlDecode(systemname),
                                            HttpUtility.HtmlDecode(callbackurl),
                                            HttpUtility.HtmlDecode(sslport));

                            HttpContext.Current.Response.Redirect(oauthURL);
                        }
                    }
                    else
                    {
                        //TMU驗證位置
                        string oauthURL = "http://glbsys.tmu.edu.tw/OAuth/OAuthCheck.aspx?domainname={0}&systemname={1}&callbackurl={2}&sslport={3}";
                        //domainname = "10.20.16.127";//主機
                        domainname = "hr2sys.tmu.edu.tw";//主機
                        systemname = "HrApply";//系統
                        string callbackurl = "ManageLogin.aspx";//TMU Token返回位置
                        string sslport = "true";
                        oauthURL = string.Format(oauthURL,
                                        HttpUtility.HtmlDecode(domainname),
                                        HttpUtility.HtmlDecode(systemname),
                                        HttpUtility.HtmlDecode(callbackurl),
                                        HttpUtility.HtmlDecode(sslport));

                        HttpContext.Current.Response.Redirect(oauthURL);
                    }
                }
                catch (Exception exp)
                {
                    Response.Write(exp.Message.ToString());
                }


            }

        }

        protected void Send_Click(object sender, EventArgs e)
        {
            if (Session["LoginEmail"] != null)
            {
                #region 取得登入者權限
                //讀取帳號 Role設定
                VAccountForManage vAccountForManage = crudObject.GetAccountForManage(Session["LoginEmail"].ToString() + "@tmu.edu.tw");
                Session["AcctRoleName"] = crudObject.GetUnderTakerRole(AcctAuditorSnEmpId);
                //Session["AcctRoleName"] = crudObject.GetUnderTakerRole(Session["AcctAuditorSnEmpId"].ToString());
                if (vAccountForManage == null)
                {
                    //判斷若是承辦員就有權限進入
                    if (AcctAuditorSnEmpId != null && crudObject.GetUnderTakerByEmpId(AcctAuditorSnEmpId))
                    //if (Session["AcctAuditorSnEmpId"] != null && crudObject.GetUnderTakerByEmpId(Session["AcctAuditorSnEmpId"].ToString()))
                    {
                        Session["AcctRole"] = "A";
                        Response.Redirect("~/ManageList.aspx");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('請確認您的帳號密碼，\\n或請系統管理者已設定您為『承辦員』，\\n才有權限進入!!');", true);
                    }
                }
                else
                {
                    DESCrypt DES = new DESCrypt();
                    AcctAuditorSnEmpId = vAccountForManage.AcctEmpId;
                    //Session["AcctAuditorSnEmpId"] = vAccountForManage.AcctEmpId;
                    if (vAccountForManage.AcctRole.Contains("M"))
                    {
                        AcctRole = "M";
                        //Session["AcctRole"] = "M";
                        Response.Redirect("~/ManageListHR.aspx?AuditorSnId=" + DES.Encrypt(AcctAuditorSnEmpId) + "&AcctRole=" + DES.Encrypt(AcctRole));
                    }
                    else
                    {
                       AcctRole = "A";
                        //Session["AcctRole"] = "A";
                        Response.Redirect("~/ManageList.aspx?AuditorSnId=" + DES.Encrypt(AcctAuditorSnEmpId) + "&AcctRole=" + DES.Encrypt(AcctRole));
                    }
                    //Response.Redirect("~/ManageList.aspx");
                }

                #endregion
            }
        }

        //private bool ValidateLogin(string strUsername, string strPassword)
        //{
        //    //其帳號所設定的角色進入所屬的頁面
        //    CRUDObject crudObject = new CRUDObject();

        //    TMUPop3Net1.TMUPop3Net1 POP = new TMUPop3Net1.TMUPop3Net1("accchk.tmu.edu.tw", strUsername.ToLower().Replace("@tmu.edu.tw", ""), strPassword, 106);
        //    string applyerData;
        //    string msg = POP.CheckPass();
        //    if (msg == "+OK" || strPassword.Equals("hr"))
        //    {
        //        applyerData = crudObject.GetVEmployeeIdFromTmuHrByEmail(strUsername);
        //        if (applyerData == null)                {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('抱歉您的身份非本校教職員!');", true);
        //            return false;
        //        }
        //        else
        //        {
        //            Session["AcctAuditorSnEmpId"] = applyerData.Split(',')[0];
        //            Session["AcctAuditorSnEmpName"] = applyerData.Split(',')[1];
        //            return true;
        //        }
        //        //VAccountForManage vAccountForManage = crudObject.GetAccountForManage(email);
        //        //if (vAccountForManage != null)
        //        //{
        //        //    Session["AcctAuditorSnEmpId"] = vAccountForManage.AcctEmpId;
        //        //    Session["AcctRole"] = vAccountForManage.AcctRole;
        //        //    return true;
        //        //}
        //        //else
        //        //{
        //        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "訊息", "alert('您無使用此系統權限!');", true);
        //        //    return false;
        //        //}
        //    }
        //    else
        //    {

        //        Page.ClientScript.RegisterStartupScript(Page.GetType(), "訊息", "alert('帳號密碼不正確,請重新輸入!');", true);
        //        return false;
        //    }

        //}

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["LoginEmail"] = null;
            Session.Abandon();
            Response.Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://hr2sys.tmu.edu.tw/HrApply/Default.aspx");
        }
    }
}
