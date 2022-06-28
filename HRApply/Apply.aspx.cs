using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Collections;
using System.Xml;

namespace ApplyPromote
{
    public partial class Apply : System.Web.UI.Page
    {
        string applyID;
        string password;
        string msg;
        GetSettings getSettings;
        public ArrayList untArray = new ArrayList { "E0109", "E0110", "E0111", "E0112", "E0113", "E0114", "E0115", "E0116", "E0117", "E0118", "E0119", "E0120", "E0121", "E0122", "E0123", "E0124", "E0125", "E0126" };

        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Redirect("http://hr2sys.tmu.edu.tw/HrApply/Default.aspx");
            //HttpCookie cookie = new HttpCookie("Token", "");

            //cookie.HttpOnly = true;
            //cookie.Expires = DateTime.Now.AddYears(-1);
            //cookie.Domain = Request.Url.Host;
            //Response.SetCookie(cookie);
            //Response.Cookies.Clear();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LastPage"] != null)
            {
                switch (Session["LastPage"].ToString())
                {
                    case "TMU":
                        dv_UnTMU.Visible = false;
                        dv_TMU.Visible = true;
                        UnTMUSend.Visible = false;
                        TMUSend.Visible = true;
                        break;
                    case "UnTMU":
                        dv_UnTMU.Visible = true;
                        dv_TMU.Visible = false;
                        UnTMUSend.Visible = true;
                        TMUSend.Visible = false;
                        break;
                }
            }

            #region Google驗證變更
            string SystemToken = "";
            string domainname = "";
            string systemname = "";
            Session["ManageEmpId"] = "";
            //新聘類型 1已具部定教師資格 2學位 3著作 4臨床教師新聘
            //升等類型 1著作送審升等 2學位送審升等 3臨床教師升等
            getSettings = new GetSettings();
            getSettings.Execute();
            if (!IsPostBack)
            {
                Session["ManageEmpId"] = "";
                Session["ApplyerID"] = "";
                ApplyAttributeNo.DataValueField = "AttributeNo";
                ApplyAttributeNo.DataTextField = "AttributeName";
                ApplyAttributeNo.DataSource = crudObject.GetAuditAttributeByKindNo(1).DefaultView;
                ApplyAttributeNo.DataBind();
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

                        string callbackurl = "Default.aspx";//TMU Token返回位置
                        //string sslport = "false";
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
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('抱歉您的身份非本校教職員!');", true);
                                return;
                            }
                            else
                            {
                                Session["AcctAuditorSnEmpId"] = applyerData.Split(',')[0];
                                Session["AcctAuditorSnEmpName"] = applyerData.Split(',')[1];
                                txt_identity.Text = Session["AcctAuditorSnEmpName"].ToString() + "(" + Session["LoginEmail"].ToString() + ")";

                            }
                            #endregion
                        }
                    }
                }
                catch (Exception exp)
                {
                    Response.Write(exp.Message.ToString());
                }
            }
            else
            {
                if (this.ViewState["ApplyerPwd"] != null)
                {
                    ApplyerPwd.Text = this.ViewState["ApplyerPwd"].ToString();
                }

            }
            if (Session["index"] != null) Session["index"] = "";
            #endregion

            //if (Session["AcctAuditorSnEmpId"] != null && Session["AcctAuditorSnEmpName"] != null)
            //{
            //    //Response.Write("<script language='javascript'>$('#nav-profile-tab').css('border-color', '#dee2e6 #dee2e6 #fff'</script>);");

            //    txt_identity.Text = Session["AcctAuditorSnEmpName"].ToString() + "(" + Session["LoginEmail"].ToString() + ")";

            //}
            //else
            //{
            //    //Response.Write("<script language='javascript'>alert('11111')</script>);");

            //}
        }
        protected void Send_Click(object sender, EventArgs e)
        {
            string strEmail = "";
            if (ApplyerEmail.Text.ToString().Contains("@"))
            {
                strEmail = @ApplyerEmail.Text;
            }
            else
            {
                strEmail = @ApplyerEmail.Text.ToString() + "@" + "tmu.edu.tw";
            }
            GetSettings setting = new GetSettings();
            //製作LoginMail的Cookie，因Session會清掉
            setting.LoginMail = Server.UrlEncode(strEmail.ToString());

            if (Session["ApplyerID"] != null)
            {
                applyID = Session["ApplyerID"].ToString();
            }
        }

        protected void ApplyKindNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strErrMsg;
            DataTable dt;
            if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("1")) //新聘
            {
                if (!crudObject.GetDuringOpenDate("1"))
                {
                    dt = crudObject.GetOpenUnit();
                    strErrMsg = "※下列系所目前開放教師聘任申請\\n";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        strErrMsg += dt.Rows[i]["unt_name_full"].ToString() + "\\n";
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
                    //Send.Enabled = true;
                }
            }

            ApplyAttributeNo.DataValueField = "AttributeNo";
            ApplyAttributeNo.DataTextField = "AttributeName";
            ApplyAttributeNo.DataSource = crudObject.GetAuditAttributeByKindNo(Convert.ToInt32(ApplyKindNo.SelectedValue.ToString())).DefaultView;
            ApplyAttributeNo.DataBind();
            if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("2"))
            {
                ApplyWayNo.Visible = true;
            }
            else
            {
                ApplyWayNo.Visible = false;
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["LoginEmail"] = null;
            Session.Abandon();
            Response.Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=http://hr2sys.tmu.edu.tw/HrApply/Default.aspx");
        }

        protected void UnTMUSend_Click(object sender, EventArgs e)
        {
            GetSettings setting = new GetSettings();
            string strErrMsg = "您輸入的帳號密碼不正確，請重新輸入或請申請後登入。";
            string strEmail = "";
            if (ApplyerEmail.Text != "" && ApplyerPwd.Text != "")
            {
                if (ApplyerEmail.Text.ToString().Contains("@"))
                {
                    strEmail = @ApplyerEmail.Text;
                }
                else
                {
                    strEmail = @ApplyerEmail.Text.ToString() + "@" + "tmu.edu.tw";
                }
                //製作LoginMail的Cookie，因Session會清掉
                setting.LoginMail = Server.UrlEncode(strEmail.ToString());
                //從申請帳號中取得身份證號
                crudObject = new CRUDObject();
                VAccountForApply vAccountForApply = new VAccountForApply();
                vAccountForApply.AcctApplyEmail = strEmail;
                vAccountForApply.AcctApplyPassword = ApplyerPwd.Text;
                vAccountForApply = crudObject.GetAccountForApply(vAccountForApply);//有申請過的

                if (ApplyerPwd.Text.Equals("HR"))
                {
                    vAccountForApply = crudObject.GetAccountForApply(strEmail);//有申請過的
                }
                if (vAccountForApply == null)
                {
                    crudObject = new CRUDObject();
                    if (ApplyerPwd.Text.Equals("HR"))
                    {
                        applyID = crudObject.GetVEmployeeFromTmuHrByEmail(strEmail);
                        if (applyID == null)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
                        return;
                    }
                }
                else
                {
                    applyID = vAccountForApply.AcctApplyId;
                }
                this.ViewState["ApplyerPwd"] = ApplyerPwd.Text.ToString();
                ApplyerPwd.Attributes["value"] = ApplyerPwd.Text;
                Session["ApplyerID"] = applyID;

                string parameters = "";
                DESCrypt DES = new DESCrypt();
                parameters = "ApplyerID=" + DES.Encrypt(applyID) + "&ApplyKindNo=" + DropDownList1.SelectedValue.ToString() + "&ApplyWayNo=1&ApplyAttributeNo=" + DropDownList3.SelectedValue.ToString();
                Session["parameters"] = parameters;
                Response.Redirect("~/MessageAgree.aspx?&KindNo=1");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
                return;
            }
        }

        protected void TMUSend_Click(object sender, EventArgs e)
        {
            if (Session["LoginEmail"] != null)
            {
                GetSettings setting = new GetSettings();
                string strEmail = Session["LoginEmail"].ToString() + "@" + "tmu.edu.tw";
                //製作LoginMail的Cookie，因Session會清掉
                setting.LoginMail = Server.UrlEncode(strEmail.ToString());
                //從申請帳號中取得身份證號
                crudObject = new CRUDObject();
                VAccountForApply vAccountForApply = new VAccountForApply();
                vAccountForApply.AcctApplyEmail = strEmail;
                vAccountForApply.AcctApplyPassword = ApplyerPwd.Text;
                vAccountForApply = crudObject.GetAccountForApply(vAccountForApply);//有申請過的
                if (vAccountForApply == null)
                {
                    applyID = crudObject.GetVEmployeeFromTmuHrByEmail(strEmail);
                }
                else
                {
                    applyID = vAccountForApply.AcctApplyId;
                }

                string parameters = "";
                //parameters = Uri.EscapeDataString(parameters);
                if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("2")) //升等
                {
                    parameters = "";
                    DESCrypt DES = new DESCrypt();
                    parameters = "ApplyerID=" + DES.Encrypt(applyID) + "&ApplyKindNo=" + ApplyKindNo.SelectedValue.ToString() + "&ApplyWayNo=1&ApplyAttributeNo=" + ApplyAttributeNo.SelectedValue.ToString();
                    Session["parameters"] = parameters;
                    Response.Redirect("~/MessageAgree.aspx?&KindNo=3");
                }
                else //新聘
                {
                    parameters = "";
                    DESCrypt DES = new DESCrypt();
                    parameters = "ApplyerID=" + DES.Encrypt(applyID) + "&ApplyKindNo=" + ApplyKindNo.SelectedValue.ToString() + "&ApplyWayNo=1&ApplyAttributeNo=" + ApplyAttributeNo.SelectedValue.ToString();
                    Session["parameters"] = parameters;
                    Response.Redirect("~/MessageAgree.aspx?&KindNo=1");
                }
            }
            else
            {
                //TMU驗證位置
                string oauthURL = "http://glbsys.tmu.edu.tw/OAuth/OAuthCheck.aspx?domainname={0}&systemname={1}&callbackurl={2}&sslport={3}";
                string domainname = "hr2sys.tmu.edu.tw";//主機
                string systemname = "HrApply";//系統
                string callbackurl = "Default.aspx";//TMU Token返回位置
                string sslport = "false";
                oauthURL = string.Format(oauthURL,
                                HttpUtility.HtmlDecode(domainname),
                                HttpUtility.HtmlDecode(systemname),
                                HttpUtility.HtmlDecode(callbackurl),
                                HttpUtility.HtmlDecode(sslport));

                HttpContext.Current.Response.Redirect(oauthURL);
            }
        }

        protected void lkb_TMU_Click(object sender, EventArgs e)
        {
            //lkb_TMU.Attributes.Clear();
            //lkb_UnTMU.Attributes.Clear();
            //lkb_TMU.Attributes.Add("style", "'background-color:#adadad;border:none' Width='120' ");
            //lkb_UnTMU.Attributes.Add("style", "'background-color:#0066cc;border:none' Width='120' ");
            Session["LastPage"] = "TMU";
            if (txt_identity.Text == "")
                Response.Redirect("http://glbsys.tmu.edu.tw/OAuth/OAuthCheck.aspx?domainname=hr2sys.tmu.edu.tw&systemname=HrApply&callbackurl=default.aspx&sslport=false");

            switch (Session["LastPage"].ToString())
            {
                case "TMU":
                    dv_UnTMU.Visible = false;
                    dv_TMU.Visible = true;
                    UnTMUSend.Visible = false;
                    TMUSend.Visible = true;
                    break;
                case "UnTMU":
                    dv_UnTMU.Visible = true;
                    dv_TMU.Visible = false;
                    UnTMUSend.Visible = true;
                    TMUSend.Visible = false;
                    break;
            }
        }

        protected void lkb_UnTMU_Click(object sender, EventArgs e)
        {
            //lkb_TMU.Attributes.Clear();
            //lkb_UnTMU.Attributes.Clear();
            //lkb_TMU.Attributes.Add("background-color", "'#0066cc ");
            //lkb_UnTMU.Attributes.Add("background-color", "#adadad");
            Session["LastPage"] = "UnTMU";
            switch (Session["LastPage"].ToString())
            {
                case "TMU":
                    dv_UnTMU.Visible = false;
                    dv_TMU.Visible = true;
                    UnTMUSend.Visible = false;
                    TMUSend.Visible = true;
                    break;
                case "UnTMU":
                    dv_UnTMU.Visible = true;
                    dv_TMU.Visible = false;
                    UnTMUSend.Visible = true;
                    TMUSend.Visible = false;
                    break;
            }
        }
    }
}
