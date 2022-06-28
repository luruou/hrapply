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

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["ManageEmpId"] = "";
            this.ApplyerEmail.Attributes.Add("onFocus", "");
            this.ApplyerPwd.Attributes.Add("onFocus", "");
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

            string strErrMsg = "您輸入的帳號密碼不正確，請重新輸入或請申請後登入。";
            string strEmail = ApplyerEmail.Text.Contains("@") ? @ApplyerEmail.Text : @ApplyerEmail.Text + "@" + "tmu.edu.tw";
           
            //製作LoginMail的Cookie，因Session會清掉
            setting.LoginMail = Server.UrlEncode(strEmail.ToString());
            //從申請帳號中取得身份證號
            VAccountForApply vAccountForApply = new VAccountForApply
            {
                AcctApplyEmail = strEmail,
                AcctApplyPassword = ApplyerPwd.Text
            };
            vAccountForApply = crudObject.GetAccountForApply(vAccountForApply);//有申請過的


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
                    //if (msg != "+OK" && ApplyerPwd.Text != "")
                    if (!String.IsNullOrEmpty(ApplyerPwd.Text))
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

            //就進入選擇畫面                                 
            Check.Visible = false;
            SelectKind.Visible = true;
            this.ViewState["ApplyerPwd"] = ApplyerPwd.Text.ToString();
            ApplyerPwd.Attributes["value"] = ApplyerPwd.Text;
            Session["ApplyerID"] = applyID;
            Session["LoginEmail"] = ApplyerEmail.Text;

        }



        protected void Send_Click(object sender, EventArgs e)
        {
            string strEmail = ApplyerEmail.Text.Contains("@") ? @ApplyerEmail.Text : @ApplyerEmail.Text + "@" + "tmu.edu.tw";
           
            GetSettings setting = new GetSettings();
            //製作LoginMail的Cookie，因Session會清掉
            setting.LoginMail = Server.UrlEncode(strEmail.ToString());

            if (Session["ApplyerID"] != null)
            {
                applyID = Session["ApplyerID"].ToString();
            }
            string parameters = "";
            //parameters = Uri.EscapeDataString(parameters);
            if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("1")) //新聘
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
                TMUPop3Net1.TMUPop3Net1 POP = new TMUPop3Net1.TMUPop3Net1("accchk.tmu.edu.tw", strEmail.Split('@')[0], ApplyerPwd.Text, 110);
                msg = POP.CheckPass();
                if (msg == "+OK" || ApplyerPwd.Text.Equals("HR"))
                {
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
            if (ApplyKindNo.SelectedValue.ToString().Trim().Equals("1")) //新聘
            {
                if (!crudObject.GetDuringOpenDate("1"))
                {
                    DataTable dt = crudObject.GetOpenUnit();
                    string strErrMsg = "※下列系所目前開放教師聘任申請\\n";
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

            ApplyWayNo.Visible = ApplyKindNo.SelectedValue.ToString().Trim().Equals("2");
        }
    }
}
