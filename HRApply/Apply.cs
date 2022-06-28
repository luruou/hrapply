using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Collections;

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
            //HttpCookie cookie = new HttpCookie("Token", "");

            //cookie.HttpOnly = true;
            //cookie.Expires = DateTime.Now.AddYears(-1);
            //cookie.Domain = Request.Url.Host;
            //Response.SetCookie(cookie);
            //Response.Cookies.Clear();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //GetSettings setting = new GetSettings();
            //setting.year = Server.UrlEncode(DDL_Smtr.Text.ToString().Trim());
            //setting.semester = Server.UrlEncode(DDL_Semester.Text.ToString().Trim());

            Session["ManageEmpId"] = "";
            this.ApplyerEmail.Attributes.Add("onFocus", "");
            this.ApplyerPwd.Attributes.Add("onFocus", "");
            //新聘類型 1已具部定教師資格 2學位 3著作 4臨床教師新聘
            //升等類型 1著作送審升等 2學位送審升等 3臨床教師升等
            //ApplyAttributeNo.DataValueField = "AttributeNo";
            //ApplyAttributeNo.DataTextField = "AttributeName";
            //ApplyAttributeNo.DataSource = crudObject.GetAuditAttributeByKindNo(1).DefaultView;
            //ApplyAttributeNo.DataBind();
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
            }
            else
            {
                if (this.ViewState["ApplyerPwd"] != null)
                {
                    ApplyerPwd.Text = this.ViewState["ApplyerPwd"].ToString();
                }

            }
            if (Session["index"] != null) Session["index"] = "";
        }

        protected void AccountApply_Click(object sender, EventArgs e)
        {
            string parameters = "ApplyerEmail=" + ApplyerEmail.Text + "&ApplyerPwd=" + ApplyerPwd.Text;
            Response.Redirect("~/AccountApply.aspx?" + parameters);

        }


        protected void ApplyerEmail_Click(object sender, EventArgs e)
        {
            ApplyerEmail.Text = "";
            if (!ApplyerEmail.Text.ToString().Equals(""))
            {
                SelectKind.Visible = false;
                Check.Visible = true;
            }
        }

        protected void ApplyerPwd_Click(object sender, EventArgs e)
        {
            if (!ApplyerPwd.Text.ToString().Equals(""))
            {
                SelectKind.Visible = false;
                Check.Visible = true;
            }
        }

        protected void Check_Click(object sender, EventArgs e)
        {
            GetSettings setting = new GetSettings();
            //setting.year = Server.UrlEncode(DDL_Smtr.Text.ToString().Trim());
            //setting.semester = Server.UrlEncode(DDL_Semester.Text.ToString().Trim());

            string strErrMsg = "您輸入的帳號密碼不正確，請重新輸入或請申請後登入。";
            string strEmail = "";
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
            //判斷是否為開放使用日2015/02/01~ 2015/03/03 
            DateTime myDate = DateTime.Now;
            string myDateString = myDate.ToString("yyyyMMdd");
            //if (Int32.Parse(myDateString) < 20150201 || Int32.Parse(myDateString) > 20150303)
            //{
            //    strErrMsg = "現在非系統開放時間[2月1日~3月10日]，無法進入系統。";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
            //    return;
            //}

            if (ApplyerPwd.Text.Equals("HR"))
            {
                vAccountForApply = crudObject.GetAccountForApply(strEmail);//有申請過的
                if (vAccountForApply != null)
                {
                    applyID = vAccountForApply.AcctApplyId;
                }
            }
            if (vAccountForApply == null)
            {
                //判斷學校的Email&pwd登入後取得身分證
                crudObject = new CRUDObject();
                TMUPop3Net1.TMUPop3Net1 POP = new TMUPop3Net1.TMUPop3Net1("accchk.tmu.edu.tw", strEmail.Split('@')[0], ApplyerPwd.Text, 110);
                msg = POP.CheckPass();

                if (msg == "+OK" || ApplyerPwd.Text.Equals("HR"))
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
                    if (msg != "+OK" && ApplyerPwd.Text != "")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
                        return;
                    }
                }

            }
            else
            {
                applyID = vAccountForApply.AcctApplyId;
            }


            ////若是本學年度新聘中有資料
            //if (crudObject.EmpBaseExist(applyID))
            //{
            //    //是當年度新聘或升等就進入
            //    string parameters = "";
            //    DESCrypt DES = new DESCrypt();
            //    parameters = "ApplyerID=" + DES.Encrypt(applyID);
            //    int intStage = 0;
            //    int intStep = 0;

            //    VApplyAudit vApplyAudit = crudObject.GetApplyAuditLastOne(applyID);//先判斷是否該學期已有有申請資料，抓出是新聘或升等
            //    if (vApplyAudit != null && vApplyAudit.AppSn > 0 &&
            //        vApplyAudit.AppYear.Equals(getSettings.GetYear()) && vApplyAudit.AppSemester.Equals(getSettings.GetSemester()))
            //    {
            //        if (vApplyAudit.AppKindNo.Trim().Equals("1")) //新聘
            //        {
            //            //確認是否為開放期間
            //            if (crudObject.GetDuringOpenDate("1"))
            //            {
            //                parameters += "&AppSn=" + DES.Encrypt(vApplyAudit.AppSn.ToString());
            //                //新聘判斷是否為多單
            //                if (crudObject.GetApplyListCntByIdno(applyID) > 1)
            //                {
            //                    Response.Redirect("~/ApplyList.aspx?" + parameters); //Button可新增新單新申請單
            //                }
            //                else
            //                {
            //                    if (vApplyAudit.AppStage != null) intStage = Int32.Parse(vApplyAudit.AppStage);
            //                    if (vApplyAudit.AppStep != null) intStep = Int32.Parse(vApplyAudit.AppStep);
            //                    string AppUnitNo = vApplyAudit.AppUnitNo.ToString();
            //                    string AppJobTitleNo = vApplyAudit.AppJobTitleNo.ToString();
            //                    string AppJobTypeNo = vApplyAudit.AppJobTypeNo.ToString();
            //                    //判斷流程走到哪
            //                    if (intStage <= 2 && intStep <= 2)
            //                    {
            //                        if (AppJobTitleNo.Equals("") || AppJobTypeNo.Equals(""))
            //                        {

            //                            Session["parameters"] = parameters;
            //                            String strUrl = "MessageAgree.aspx?&KindNo=2&RegisterStartup=True";
            //                            Response.Redirect("~/" + strUrl + parameters); //Button可新增新單新申請單
            //                            //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + strUrl + "';", true);
            //                        }
            //                        else
            //                        {
            //                            if ((AppJobTitleNo.Substring(3, 3).Equals("400")) || //臨床教授
            //                                (AppJobTitleNo.Substring(3, 3).Equals("500")) || //專案教授
            //                                (AppJobTypeNo.Equals("2")) ||
            //                                untArray.Contains(AppUnitNo) //醫療學科
            //                                ) //兼職
            //                            {
            //                                Session["parameters"] = parameters;
            //                                String strUrl = "MessageAgree.aspx?&KindNo=1&RegisterStartup=True";
            //                                Response.Redirect("~/" + strUrl + parameters); //Button可新增新單新申請單
            //                                //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + strUrl + "';", true);
            //                                //Response.Write(parameters);

            //                            }
            //                            else
            //                            {
            //                                Session["parameters"] = parameters;
            //                                String strUrl = "MessageAgree.aspx?&KindNo=2&RegisterStartup=True";

            //                                Response.Redirect("~/" + strUrl + parameters); //Button可新增新單新申請單
            //                                //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + strUrl + "';", true);
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {

            //                        Session["parameters"] = parameters;
            //                        Response.Redirect("~/MessageAgree.aspx?&KindNo=1"); //Button可新增新單新申請單
            //                    }
            //                }

            //            }
            //            else
            //            {
            //                strErrMsg = "現在非新聘系統開放時間，無法進入系統。";
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            if (vApplyAudit.AppKindNo.Trim().Equals("2") && !crudObject.GetDuringOpenDate("2"))
            //            {
            //                String strErrMsg2 = "現在非升等系統開放時間，無法進入系統。";
            //                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg2 + "');", true);
            //            }
            //            else
            //            {
            //                Session["parameters"] = parameters;
            //                String strUrl = "MessageAgree.aspx?&KindNo=3&RegisterStartup=True";
            //                Response.Redirect("~/" + strUrl + parameters); //Button可新增新單新申請單
            //                                                               //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + strUrl + "';", true);
            //            }
            //        }
            //    }
            //    else
            //    {
                    //就進入選擇畫面                                 
                    Check.Visible = false;
                    SelectKind.Visible = true;
                    this.ViewState["ApplyerPwd"] = ApplyerPwd.Text.ToString();
                    ApplyerPwd.Attributes["value"] = ApplyerPwd.Text;
                    Session["ApplyerID"] = applyID;
                //}

            //}
            //else
            //{
            //    DataTable dt = crudObject.GetOpenUnit();
            //    strErrMsg = "※目前開放教師聘任申請的系所：\\n";
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        strErrMsg += dt.Rows[i]["unt_name_full"].ToString() + "\\n";
            //    }
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
            //    Check.Visible = false;
            //    SelectKind.Visible = true;
            //    this.ViewState["ApplyerPwd"] = ApplyerPwd.Text.ToString();
            //    ApplyerPwd.Attributes["value"] = ApplyerPwd.Text;
            //    Session["ApplyerID"] = applyID;
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
            string parameters = "";
            //parameters = Uri.EscapeDataString(parameters);
            if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("1") 
                //&&
                //(
                //(setting.year == setting.LoadYear && setting.semester == setting.LoadSemester)
                //||
                //(setting.year == setting.NowYear && setting.semester == setting.NowSemester)
                //)
                ) //新聘
            {
                parameters = "";
                DESCrypt DES = new DESCrypt();
                parameters = "ApplyerID=" + DES.Encrypt(applyID) + "&ApplyKindNo=" + ApplyKindNo.SelectedValue.ToString() + "&ApplyWayNo=1&ApplyAttributeNo=" + ApplyAttributeNo.SelectedValue.ToString();
                Session["parameters"] = parameters;
                Response.Redirect("~/MessageAgree.aspx?&KindNo=1");
            }

            else if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("1"))
            {
                String strErrMsg = "現在非新聘系統開放時間，無法進入系統。";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
                return;
            }
            if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("2")) //升等
            {
                strEmail = "";
                if (ApplyerEmail.Text.ToString().Contains("@"))
                {
                    strEmail = @ApplyerEmail.Text;
                }
                else
                {
                    strEmail = @ApplyerEmail.Text.ToString() + "@" + "tmu.edu.tw";
                }
                TMUPop3Net1.TMUPop3Net1 POP = new TMUPop3Net1.TMUPop3Net1("accchk.tmu.edu.tw", strEmail.Split('@')[0], ApplyerPwd.Text, 110);
                msg = POP.CheckPass();
                if (msg == "+OK" || ApplyerPwd.Text.Equals("HR"))
                {
                    parameters = "";
                    DESCrypt DES = new DESCrypt();
                    parameters = "ApplyerID=" + DES.Encrypt(applyID) + "&ApplyKindNo=" + ApplyKindNo.SelectedValue.ToString() + "&ApplyWayNo=1&ApplyAttributeNo=" + ApplyAttributeNo.SelectedValue.ToString();
                    Session["parameters"] = parameters;
                    Response.Redirect("~/MessageAgree.aspx?&KindNo=3");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + "抱歉！您非本校教職員，無法申請升等，請確認！" + "');", true);
                }
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
                    Send.Enabled = true;
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

        //protected void DDL_Smtr_Changed(object sender, EventArgs e)
        //{

        //    GetSettings setting = new GetSettings();
        //    setting.year = Server.UrlEncode(DDL_Smtr.Text.ToString().Trim());
        //    setting.semester = Server.UrlEncode(DDL_Semester.Text.ToString().Trim());
        //}
        //protected void DDL_Semester_Changed(object sender, EventArgs e)
        //{

        //    GetSettings setting = new GetSettings();
        //    setting.year = Server.UrlEncode(DDL_Smtr.Text.ToString().Trim());
        //    setting.semester = Server.UrlEncode(DDL_Semester.Text.ToString().Trim());
        //}
    }
}
