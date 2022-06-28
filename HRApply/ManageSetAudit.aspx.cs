using System;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Linq;

namespace ApplyPromote
{
    public partial class ManageSetAudit : PageBaseManage
    {
        static string AcctAuditorSnEmpId, AcctRole;
        //判斷國別
        string switchFgn = "";

        //新聘已具部定教師資格
        static string chkTeacher = "1";

        //新聘申請學位送審
        static string chkDegree = "2";

        //新聘申請著作送審
        static string chkPublication = "3";

        //新聘臨床教師新聘4
        static string chkClinical = "4";

        //申請新聘
        static string chkApply = "1";

        //申請升等
        static string chkPromote = "2";
        //上傳檔案 基本資料
        static public ArrayList fileLists = new ArrayList();

        //上傳檔案 論文

        VEmployeeBase vEmp = new VEmployeeBase();

        VApplyAudit vApplyAudit;

        VAppendEmployee vAppendEmployee;

        VAppendPublication vAppendPublication;

        VAppendDegree vAppendDegree;

        VAuditExecute vAuditExecute;

        VAuditExecute vAuditExecuteNextOne;

        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();
        QuestionnaireService questionnaireService = new QuestionnaireService(); 
        GetSettings getSettings;

        string empSn = "";

        string selStrList = "";

        string selWeakList = "";

        String[] strStatus;

        int appSn;

        //Email給外審委員資料        

        //頁籤切換
        public enum SearchType1
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewAuditExecuting = 1,
            ViewAuditorSetting = 2

        }

        public enum SearchType2
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewAuditExecuting = 1
        }

        public enum SearchType3
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewAuditExecuting = 1,
            ViewAuditorSetting = 2

        }

        public enum SearchType4
        {
            NotSet = -1,
            ViewTeachBase = 0

        }
        //上傳檔案
        NameValueCollection fileCollection = new NameValueCollection();

        private ThesisCoopService thesisCoopService = new ThesisCoopService();

        public Dictionary<string, string> thesisCoopClassificationItems = new Dictionary<string, string>();
        public Dictionary<string, string> thesisCoopRDItems = new Dictionary<string, string>();

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["postBackTimes"] = -1;
                Session["index"] = null;
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {

            if (Session["AcctRole"] == null && Session["LoginEmail"] == null)
            {
                ShowSessionTimeOut();
                Response.Redirect("~/ManageLogin.aspx");
            }
            else
            {
                crudObject = new CRUDObject();
                vEmp = crudObject.GetEmpBsaseObjByEmpSn(Request.QueryString["EmpSn"].ToString());
                Session["EmpSn"] = Request.QueryString["EmpSn"].ToString();
                Session["AppSn"] = Request.QueryString["AppSn"].ToString();
                appSn = Convert.ToInt32(Request.QueryString["AppSn"].ToString());
                if (Session["AcctAuditorSnEmpId"] != null)
                    AcctAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString();
                if (Session["AcctRole"] != null)
                    AcctRole = Session["AcctRole"].ToString();

                //if (AcctAuditorSnEmpId == null || AcctAuditorSnEmpId=="")
                if (Session["AcctAuditorSnEmpId"] == null || Session["AcctAuditorSnEmpId"].ToString().Equals(""))
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert(' Session Time Out，\\請登出後後重新輸入!!');", true);
                }
                if (!this.IsPostBack && vEmp != null)
                {

                    //載入ApplyAudit共用延伸資料                   
                    vApplyAudit = crudObject.GetApplyAuditObj(appSn);
                    vAuditExecute = new VAuditExecute();
                    vAuditExecute.AppSn = vApplyAudit.AppSn;
                    if (Session["AcctAuditorSnEmpId"] != null && Session["AcctAuditorSnEmpId"].ToString() != "")
                        vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                    else if (AcctAuditorSnEmpId != null && AcctAuditorSnEmpId != "")
                        vAuditExecute.ExecuteAuditorSnEmpId = AcctAuditorSnEmpId; //目前抓自己AccountForAudit中的 ID
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert(' Session Time Out，\\請登出後後重新輸入!!');", true);
                        return;
                    }
                    vAuditExecute.ExecuteStatus = false; //尚未審核
                    vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆
                    string accountRole = Session["AcctRole"].ToString();
                    if (vApplyAudit != null && vApplyAudit.AppStatus) //未送出僅能看基本資料
                    {
                        if (vAuditExecute != null)
                        {
                            if ("M".Equals(accountRole))
                            {
                                MultiViewAudit.ActiveViewIndex = (int)SearchType1.ViewTeachBase;
                                //MultiViewAudit.SetActiveView(ViewAuditorSetting);
                                //MultiViewAudit.SetActiveView(ViewAuditExecuting);
                                Menu1.Visible = true;
                                Menu2.Visible = false;
                                Menu3.Visible = false;
                                Menu4.Visible = false;
                            }
                            else
                            {
                                MultiViewAudit.ActiveViewIndex = (int)SearchType2.ViewTeachBase;
                                MultiViewAudit.Views.Remove(ViewAuditorSetting);
                                Menu1.Visible = false;
                                Menu2.Visible = true;
                                Menu3.Visible = false;
                                Menu4.Visible = false;
                            }
                        }
                        else
                        {
                            if ("M".Equals(accountRole))
                            {
                                vAuditExecute = crudObject.GetExecuteAuditorNextOne(vApplyAudit.AppSn); //只取未完成的第一筆
                                MultiViewAudit.ActiveViewIndex = (int)SearchType3.ViewTeachBase;
                                NoAuthority.Visible = true;
                                TbPassExecute.Visible = false;
                                Menu1.Visible = false;
                                Menu2.Visible = false;
                                Menu3.Visible = true;
                                Menu4.Visible = false;
                            }
                            else
                            {
                                MultiViewAudit.ActiveViewIndex = (int)SearchType4.ViewTeachBase;
                                MultiViewAudit.Views.Remove(ViewAuditorSetting);
                                MultiViewAudit.Views.Remove(ViewAuditExecuting);
                                Menu1.Visible = false;
                                Menu2.Visible = false;
                                Menu3.Visible = false;
                                Menu4.Visible = true;
                            }
                        }

                    }
                    else
                    {
                        MultiViewAudit.Views.Remove(ViewAuditorSetting);
                        MultiViewAudit.Views.Remove(ViewAuditExecuting);
                        Menu1.Visible = false;
                        Menu2.Visible = false;
                        Menu3.Visible = false;
                        Menu4.Visible = true;
                    }
                    //控制應顯示資料
                    if (vApplyAudit.AppKindNo.Equals(chkApply))
                    {

                        //1.已具部定教育資格
                        if (vApplyAudit.AppAttributeNo.ToString().Equals(chkTeacher))
                        {
                            AppPPMTableRow.Visible = true;
                            AppTeacherCaTableRow.Visible = true;

                        } //2.學位                
                        else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkDegree))
                        {

                            AppTeacherCaTableRow.Visible = true;
                            AppDegreeThesisTableRow.Visible = true;
                            OtherTeachingTableRow.Visible = true;
                            OtherServiceTableRow.Visible = true;
                            AppThesisOral.Visible = true;

                        } //3.著作
                        else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkPublication))
                        {
                            AppPPMTableRow.Visible = true;
                            AppTeacherCaTableRow.Visible = true;
                            OtherServiceTableRow.Visible = true;
                            OtherTeachingTableRow.Visible = true;
                        }
                        else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkClinical))
                        {
                            AppDrCaTableRow.Visible = true;
                        }
                        SelfTableRow.Visible = true;
                    }
                    else if (vApplyAudit.AppKindNo.Equals(chkPromote))
                    {
                        TbRow_RPI.Visible = false;
                        if (vApplyAudit.AppAttributeNo.ToString().Equals("1"))
                        {
                            RPIDiscountTableRow.Visible = true;
                            AppPPMTableRow.Visible = true;
                        }
                        else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkDegree))
                        {
                            RPIDiscountTableRow.Visible = true;
                            AppDegreeThesisTableRow.Visible = true;
                            AppThesisOral.Visible = true;
                        }
                        else if (vApplyAudit.AppAttributeNo.ToString().Equals("3"))
                        {
                            AppPublicationTableRow.Visible = true;
                            AppDrCaTableRow.Visible = true;
                            EmpIdnoTableRow.Visible = true;
                            RecommendUploadTableRow.Visible = true;
                        }
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["AcctRole"] == null && Session["LoginEmail"] == null)
                {
                    ShowSessionTimeOut();
                    Response.Redirect("~/ManageLogin.aspx");
                }
                else
                {
                    MultiViewAudit.ActiveViewIndex = 0;
                    Session["index"] = "0";
                    MessageLabel.Text = " ";
                    //Session["ThesisScore"] initial
                    Session["ThesisScore"] = 1;

                    //MessageLabel initial
                    MessageLabel.Text = "";

                    if (Request.QueryString["EmpSn"] != null)
                    {
                        Session["EmpSn"] = Request.QueryString["EmpSn"];
                        getSettings = new GetSettings();
                        getSettings.Execute();
                        Session["sYear"] = getSettings.GetYear();
                        Session["sSemester"] = getSettings.GetSemester();
                        empSn = Request.QueryString["EmpSn"];
                        LoadDataBtn_Click(sender, e);
                    }
                }

            }
            else
            {
                if (Session["index"] != null && !Session["index"].ToString().Equals(""))
                {
                    MultiViewAudit.ActiveViewIndex = Convert.ToInt32(Session["index"].ToString());
                }
            }

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



        protected void Page_LoadComplete(object sender, EventArgs e)
        {


        }

        protected void GVThesisCoop_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblClassification = (Label)e.Row.FindControl("lblThesisCoopClassification");
                Label lblRD = (Label)e.Row.FindControl("lblThesisCoopRD");

                string classification = e.Row.Cells[1].Text;
                string rd = e.Row.Cells[2].Text;
                lblClassification.Text = thesisCoopClassificationItems[classification];
                lblRD.Text = thesisCoopRDItems[rd];


                HyperLink hyperLnkThesisCoop = (HyperLink)e.Row.FindControl("ThesisCoopHyperLink");
                hyperLnkThesisCoop.NavigateUrl = getHyperLink(e.Row.Cells[7].Text);
                if (String.IsNullOrEmpty(e.Row.Cells[7].Text) || e.Row.Cells[7].Text == "&nbsp;")
                {
                    hyperLnkThesisCoop.Text = "無資料";
                    hyperLnkThesisCoop.Enabled = false;
                }

            }
        }


        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiViewAudit.ActiveViewIndex = index;
            Session["index"] = index;
        }

        protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiViewAudit.ActiveViewIndex = index;
            Session["index"] = index;
        }

        protected void Menu3_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiViewAudit.ActiveViewIndex = index;
            Session["index"] = index;
        }

        protected void Menu4_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiViewAudit.ActiveViewIndex = index;
            Session["index"] = index;
        }







        //查詢已存資料
        protected void LoadDataBtn_Click(object sender, EventArgs e)
        {

            if (EmpIdno.Text.Equals(""))
            {
                MessageLabel.Text = "載入失敗：請輸入身分證字號...........";
            }
            else
            {
                if (thesisCoopClassificationItems.Count == 0)
                {
                    thesisCoopClassificationItems.Add("10", "產學合作計畫實收台幣超過50萬~100萬元(含100萬元)：10分");
                    thesisCoopClassificationItems.Add("30", "產學合作計畫實收台幣超過100萬~300萬元(含300萬元)：30分");
                    thesisCoopClassificationItems.Add("50", "產學合作計畫實收台幣超過300萬~500萬元(含500萬元)：50分");
                    thesisCoopClassificationItems.Add("70", "產學合作計畫實收台幣超過500萬~700萬元(含700萬元)：70分");
                    thesisCoopClassificationItems.Add("100", "產學合作計畫實收台幣超過700萬~1,000萬元(含1,000萬元)：100分");
                    thesisCoopClassificationItems.Add("130", "產學合作計畫實收台幣超過1,000萬：130分");
                }
                if (thesisCoopRDItems.Count == 0)
                {
                    thesisCoopRDItems.Add("1", "30%未滿(x1)");
                    thesisCoopRDItems.Add("2", "30%以上(x2)");
                }

                CRUDObject crudObject = new CRUDObject();
                vEmp = crudObject.GetEmpBsaseObjByEmpSn(empSn);
                if (vEmp != null)
                {
                    Session["EmpSn"] = vEmp.EmpSn;
                    EmpIdno.Text = vEmp.EmpIdno;
                    EmpBirthDay.Text = vEmp.EmpBirthDay;
                    EmpPassportNo.Text = vEmp.EmpPassportNo;
                    EmpNameENFirst.Text = vEmp.EmpNameENFirst;
                    EmpNameENLast.Text = vEmp.EmpNameENLast;
                    EmpNameCN.Text = vEmp.EmpNameCN;
                    AuditNameCN.Text = vEmp.EmpNameCN;
                    PassEmpNameCN.Text = EmpNameCN.Text.ToString();
                    EmpSex.Text = "M".Equals(vEmp.EmpSex) ? "男" : "女";
                    //EmpCountry.SelectedValue = vEmp.EmpCountry; 
                    //EmpHomeTown.Text = vEmp.EmpHomeTown;
                    //EmpBornProvince.Text = vEmp.EmpBornCity;
                    //EmpBornCity.Text = vEmp.EmpBornCity;
                    EmpCountry.Text = crudObject.GetCountryName(vEmp.EmpCountry).Rows.Count == 0 ? "" : crudObject.GetCountryName(vEmp.EmpCountry).Rows[0][0].ToString();
                    EmpHomeTown.Text = crudObject.GetHomeTownName(vEmp.EmpHomeTown).Rows.Count == 0 ? "" : crudObject.GetHomeTownName(vEmp.EmpHomeTown).Rows[0][0].ToString();
                    EmpBornProvince.Text = crudObject.GetHomeTownName(vEmp.EmpBornProvince).Rows.Count == 0 ? "" : crudObject.GetHomeTownName(vEmp.EmpBornProvince).Rows[0][0].ToString();
                    EmpBornCity.Text = crudObject.GetBornCityName(vEmp.EmpBornCity).Rows.Count == 0 ? "" : crudObject.GetBornCityName(vEmp.EmpBornCity).Rows[0][0].ToString();
                    EmpTelPub.Text = vEmp.EmpTelPub;
                    EmpTelPri.Text = vEmp.EmpTelPri;
                    EmpEmail.Text = vEmp.EmpEmail;
                    GetSettings setting = new GetSettings();
                    setting.LoginMail = Server.UrlEncode(vEmp.EmpEmail);
                    EmpTownAddressCode.Text = vEmp.EmpTownAddressCode;
                    EmpTownAddress.Text = vEmp.EmpTownAddress;
                    EmpAddressCode.Text = vEmp.EmpAddressCode;
                    EmpAddress.Text = vEmp.EmpAddress;
                    EmpCell.Text = vEmp.EmpCell;
                    EmpExpertResearch.Text = vEmp.EmpExpertResearch;

                    string location = Global.FileUpPath + vEmp.EmpSn + "/";

                    //上傳照片
                    if (vEmp.EmpPhotoUploadName != null && !vEmp.EmpPhotoUploadName.Equals(""))
                    {
                        EmpPhotoUploadCB.Checked = true;
                        EmpPhotoImage.ImageUrl = location + vEmp.EmpPhotoUploadName;
                        EmpPhotoImage.Visible = true;
                        EmpPhotoUploadFUName.Text = vEmp.EmpPhotoUploadName;
                    }
                    else
                    {
                        EmpPhotoImage.Visible = false;
                    }

                    //上傳身分證
                    if (vEmp.EmpIdnoUploadName != null && !vEmp.EmpIdnoUploadName.Equals(""))
                    {
                        //EmpIdUploadFUName.Text = vEmp.EmpIdUploadName;
                        EmpIdnoUploadCB.Checked = true;
                        EmpIdnoTableRow.Visible = true;
                        EmpIdnoHyperLink.NavigateUrl = getHyperLink(vEmp.EmpIdnoUploadName);
                        EmpIdnoHyperLink.Text = vEmp.EmpIdnoUploadName;
                        EmpIdnoHyperLink.Visible = true;
                    }
                    else
                    {
                        EmpIdnoHyperLink.Visible = false;
                    }

                    //上傳畢業證書
                    if (vEmp.EmpDegreeUploadName != null && !vEmp.EmpDegreeUploadName.Equals(""))
                    {
                        //EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeUploadCB.Checked = true;
                        EmpDegreeTableRow.Visible = true;
                        EmpDegreeHyperLink.NavigateUrl = getHyperLink(vEmp.EmpDegreeUploadName);
                        EmpDegreeHyperLink.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeHyperLink.Visible = true;
                        EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                    }
                    else
                    {
                        EmpDegreeHyperLink.Visible = false;
                    }



                    EmpSelfTeachExperience.Text = vEmp.EmpSelfTeachExperience;
                    EmpSelfReach.Text = vEmp.EmpSelfReach;
                    EmpSelfDevelope.Text = vEmp.EmpSelfDevelope;
                    EmpSelfSpecial.Text = vEmp.EmpSelfSpecial;
                    EmpSelfImprove.Text = vEmp.EmpSelfImprove;
                    EmpSelfContribute.Text = vEmp.EmpSelfContribute;
                    EmpSelfCooperate.Text = vEmp.EmpSelfCooperate;
                    EmpSelfTeachPlan.Text = vEmp.EmpSelfTeachPlan;
                    EmpSelfLifePlan.Text = vEmp.EmpSelfLifePlan;

                    //自評部分
                    EmpSelfTeachExperience.Text = vApplyAudit.AppSelfTeachExperience;
                    EmpSelfReach.Text = vApplyAudit.AppSelfReach;
                    EmpSelfDevelope.Text = vApplyAudit.AppSelfDevelope;
                    EmpSelfSpecial.Text = vApplyAudit.AppSelfSpecial;
                    EmpSelfImprove.Text = vApplyAudit.AppSelfImprove;
                    EmpSelfContribute.Text = vApplyAudit.AppSelfContribute;
                    EmpSelfCooperate.Text = vApplyAudit.AppSelfCooperate;
                    EmpSelfTeachPlan.Text = vApplyAudit.AppSelfTeachPlan;
                    EmpSelfLifePlan.Text = vApplyAudit.AppSelfLifePlan;

                    if (EmpSelfTeachExperience.Text.ToString().Equals(""))
                    {
                        EmpSelfTeachExperience.Text = vEmp.EmpSelfTeachExperience;
                        EmpSelfReach.Text = vEmp.EmpSelfReach;
                        EmpSelfDevelope.Text = vEmp.EmpSelfDevelope;
                        EmpSelfSpecial.Text = vEmp.EmpSelfSpecial;
                        EmpSelfImprove.Text = vEmp.EmpSelfImprove;
                        EmpSelfContribute.Text = vEmp.EmpSelfContribute;
                        EmpSelfCooperate.Text = vEmp.EmpSelfCooperate;
                        EmpSelfTeachPlan.Text = vEmp.EmpSelfTeachPlan;
                        EmpSelfLifePlan.Text = vEmp.EmpSelfLifePlan;
                    }

                    //載入ApplyAudit共用延伸資料                   
                    vApplyAudit = crudObject.GetApplyAuditObj(appSn);

                    Session["AppSn"] = vApplyAudit.AppSn; //

                    if (vApplyAudit.ReasearchResultUploadName != null && !vApplyAudit.ReasearchResultUploadName.Equals(""))
                    {
                        link_AppReasearchResult.NavigateUrl = getHyperLink(vApplyAudit.ReasearchResultUploadName);
                        link_AppReasearchResult.Text = vApplyAudit.ReasearchResultUploadName;
                    }
                    //指定性別

                    //指定單位
                    if (!Object.Equals(null, vApplyAudit))
                    {

                        if (vApplyAudit.AppKindNo.Trim().Equals(chkApply)) //新聘
                        {
                            GVTeachExp.Visible = true;
                            TeachCaTableRow.Visible = true;
                            TeachHonourTableRow.Visible = true;
                            SelfTable.Visible = true;
                            //EmpHomeTownTableRow.Visible = true;
                            //EmpBornProvinceTableRow.Visible = true;
                            EmpBornCityTableRow.Visible = true;
                            AppENowJobOrgTableRow.Visible = true;
                            AppENoteTableRow.Visible = true;
                            AppERecommendorsTableRow.Visible = true;
                            AppERecommendYearTableRow.Visible = true;
                            RecommendUploadTableRow.Visible = true;
                            EmpExpertResearchTableRow.Visible = true;
                            EmpPhotoTableRow.Visible = true;
                            EmpIdnoTableRow.Visible = true;
                            EmpDegreeTableRow.Visible = true;
                        }
                        else
                        {
                            GVTeacherTmuExp.Visible = true;
                            TeachLessonTableRow.Visible = true;
                            RPIDiscountTableRow.Visible = true;
                            EmpDegreeTableRow.Visible = true;

                        }

                        //教師資格切結書
                        if (vApplyAudit.AppDeclarationUploadName != null && !vApplyAudit.AppDeclarationUploadName.Equals(""))
                        {
                            //EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                            AppDeclarationUploadCB.Checked = true;
                            AppDeclarationHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppDeclarationUploadName);
                            AppDeclarationHyperLink.Text = vApplyAudit.AppDeclarationUploadName;
                            AppDeclarationHyperLink.Visible = true;
                            AppDeclarationUploadFUName.Text = vApplyAudit.AppDeclarationUploadName;
                        }
                        else
                        {
                            AppDeclarationHyperLink.Visible = false;
                        }

                        //教師履歷CV
                        if (vApplyAudit.AppResumeUploadName != null && !vApplyAudit.AppResumeUploadName.Equals(""))
                        {
                            AppResumeUploadCB.Checked = true;
                            AppResumeHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppResumeUploadName);
                            AppResumeHyperLink.Text = vApplyAudit.AppResumeUploadName;
                            AppResumeHyperLink.Visible = true;
                            AppResumeUploadFUName.Text = vApplyAudit.AppResumeUploadName;
                        }
                        else
                        {
                            ThesisScoreUploadFUName.Visible = true;
                            AppResumeHyperLink.Visible = false;
                        }


                        //論文積分表
                        if (vApplyAudit.ThesisScoreUploadName != null && !vApplyAudit.ThesisScoreUploadName.Equals(""))
                        {
                            ThesisScoreUploadCB.Checked = true;

                            ThesisScoreUploadHyperLink.NavigateUrl = getHyperLink(vApplyAudit.ThesisScoreUploadName);
                            ThesisScoreUploadHyperLink.Text = vApplyAudit.ThesisScoreUploadName;
                            ThesisScoreUploadHyperLink.Visible = true;
                            ThesisScoreUploadFUName.Text = vApplyAudit.ThesisScoreUploadName;
                        }
                        else
                        {
                            ThesisScoreUploadFUName.Visible = true;
                            ThesisScoreUploadHyperLink.Visible = false;
                        }



                        //顯示申請狀態是否送件
                        //AuditStatusName.Text = vApplyAudit.AppStatus ? "完成申請送件" : "申請者填寫中....";
                        //顯示Stage中文名稱 AuditExecute 目前需審核的人
                        ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
                        strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());
                        AuditStatusName.Text = strStatus[Int32.Parse(vApplyAudit.AppStage)];

                        //顯示升等途徑 AppWayName
                        AuditWayName.Text = crudObject.GetAuditWayName(vApplyAudit.AppWayNo).Rows[0][0].ToString();

                        //應徵類別
                        //AppKindName.Text = crudObject.GetKindName(vApplyAudit.AppKindNo).Rows.Count == 0 ? "" : crudObject.GetKindName(vApplyAudit.AppKindNo).Rows[0][0].ToString() + ":";
                        AppKindName.Text = crudObject.GetKindName(vApplyAudit.AppKindNo).Rows.Count == 0 ? "" : crudObject.GetKindName(vApplyAudit.AppKindNo).Rows[0][0].ToString();
                        AuditKindName.Text = AppKindName.Text.ToString();

                        //應徵單位
                        AppUnit.Text = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows.Count == 0 ? "" : crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                        PassAppUnit.Text = AppUnit.Text.ToString();
                        AuditUnit.Text = AppUnit.Text.ToString();

                        //應徵職稱
                        AppJobTitle.Text = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);
                        PassAppJobTitle.Text = AppJobTitle.Text.ToString();
                        AuditJobTitle.Text = AppJobTitle.Text.ToString();

                        //應徵職別
                        AppJobType.Text = crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows.Count == 0 ? "" : crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows[0][0].ToString();
                        AuditJobType.Text = AppJobType.Text.ToString();


                        //升等顯示年資
                        if (vApplyAudit.AppKindNo.Trim().Equals(chkPromote))
                        {
                            VEmpTmuHr vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(vEmp.EmpIdno);
                            IsEmpYear.Visible = true;
                            int diffYear = 0;
                            int diffMonth = 0;
                            String ExpStartDate = crudObject.GetTeacherTmuExp(vEmp.EmpIdno, vEmpTmuHr.EmpTitid);
                            if (ExpStartDate == null || ExpStartDate.Equals(""))
                            {
                                ExpStartDate = "04906"; //塞給北醫創校年
                            }
                            DateTime nowdate = DateTime.Now;
                            int intYear = Int32.Parse(ExpStartDate.Substring(0, 3));
                            int intMonth = Int32.Parse(ExpStartDate.Substring(3, 2));
                            int nowYear = nowdate.Year - 1911;
                            int nowMonth = nowdate.Month - 1;
                            if (nowMonth < intMonth)
                            {
                                nowYear--;
                                nowMonth = nowMonth + 12;
                            }
                            diffYear = nowYear - intYear;
                            diffMonth = nowMonth - intMonth;

                            EmpYear.Text = "" + String.Format("{0:00}", diffYear) + "/" + String.Format("{0:00}", diffMonth) + " (" + intYear + String.Format("{0:00}", intMonth) + ")";
                        }


                        //指定申請類別 著作 學位
                        AppAttributeName.Text = crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows.Count == 0 ? "" : crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows[0][1].ToString();
                        AuditAttributeName.Text = AppAttributeName.Text.ToString();

                        //法規第幾項
                        String[] num = { "零", "一", "二", "三", "四", "五" };
                        //法規第幾項
                        if (!vApplyAudit.AppJobTitleNo.ToString().Equals("") && !vApplyAudit.AppJobTitleNo.ToString().Equals("請選擇"))
                        {
                            if (vApplyAudit.AppJobTitleNo.ToString().Equals("030400"))
                            {
                                ItemNo.Text = num[1];
                                LawText.Text = "依臨床教師聘任辦法第三)條第 ";
                            }
                            if (vApplyAudit.AppJobTitleNo.ToString().Equals("040400"))
                            {
                                ItemNo.Text = num[1];
                                LawText.Text = "依臨床教師聘任辦法第(四)條第 ";
                            }
                            if (vApplyAudit.AppJobTitleNo.ToString().Equals("050400"))
                            {
                                ItemNo.Text = num[1];
                                LawText.Text = "依臨床教師聘任辦法第(五)條第 ";
                            }
                            if (vApplyAudit.AppJobTitleNo.ToString().Equals("060400"))
                            {
                                LawText.Text = "依臨床教師聘任辦法第(六)條第 ";
                                ItemNo.Text = num[0];
                            }
                            if (!vApplyAudit.AppJobTitleNo.ToString().Equals("030400") &&
                                !vApplyAudit.AppJobTitleNo.ToString().Equals("040400") &&
                                !vApplyAudit.AppJobTitleNo.ToString().Equals("050400") &&
                                !vApplyAudit.AppJobTitleNo.ToString().Equals("060400"))
                            {
                                LawText.Text = "依教師聘任升等實施辦法第(二)條第 ";
                                ItemNo.Text = num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.Substring(1, 1)))];
                            }
                        }

                        //法規第幾款
                        ItemLabel.Text = num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)];

                        //String titleNo = "" + (7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)));
                        String titleNo = vApplyAudit.AppJobTitleNo;

                        //撈取法規內容
                        //LawContent.Text = crudObject.GetTeacherLaw(vApplyAudit.AppKindNo, titleNo, vApplyAudit.AppLawNumNo);
                        LawContent.Text = crudObject.GetTeacherLaw(vApplyAudit.AppKindNo, titleNo, vApplyAudit.AppLawNumNo);

                        //論文代表著作
                        //AppPublication.Text = vApplyAudit.AppPublicationUploadName; 不用
                        //AuditAppPublication.Text = AppPublication.Text.ToString(); 不用

                        //論文積分相關
                        Session["ResearchYear"] = "1";
                        Session["ThesisAccuScore"] = "0";
                        Session["ResearchYear"] = vApplyAudit.AppResearchYear;

                        AppPThesisAccuScore.Text = vApplyAudit.AppThesisAccuScore;
                        AppPThesisAccuScore1.Text = vApplyAudit.AppThesisAccuScore;
                        AppRPITotalScore.Text = vApplyAudit.AppThesisAccuScore;
                        AppRPI.Text = vApplyAudit.AppRPIScore;

                        AppENowJobOrg.Text = vEmp.EmpNowJobOrg;
                        AppENote.Text = vEmp.EmpNote;
                        AppERecommendors.Text = vApplyAudit.AppRecommendors;
                        AppERecommendYear.Text = vApplyAudit.AppRecommendYear;
                        //載入 AppendEmployee新聘延伸資料檔
                        VAppendPromote vAppendPromote = new VAppendPromote();
                        vAppendPromote = crudObject.GetAppendPromoteObj(vApplyAudit.AppSn);

                        if (vAppendPromote != null)
                        {
                            //經歷服務證明
                            if (vAppendPromote.ExpServiceCaUploadName != null && !vAppendPromote.ExpServiceCaUploadName.Equals(""))
                            {
                                ExpTableRow.Visible = true;
                                ExpServiceCaUploadCB.Checked = true;
                                ExpServiceCaHyperLink.NavigateUrl = getHyperLink(vAppendPromote.ExpServiceCaUploadName);
                                ExpServiceCaHyperLink.Text = vAppendPromote.ExpServiceCaUploadName;
                                ExpServiceCaHyperLink.Visible = true;
                                ExpServiceCaUploadFUName.Text = vAppendPromote.ExpServiceCaUploadName;
                            }
                            int discountTotal = 0;
                            //if (!String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore1) && !String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore2) && !String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore2) && !String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore3) && !String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore4) && !String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore5))
                            //    //教師優良表現論文積分折抵 AppendPromote.RPIDiscountScore1~5 & RPIDiscountScore1Name1~4
                            //    discountTotal = Int32.Parse(vAppendPromote.RPIDiscountScore1) + Int32.Parse(vAppendPromote.RPIDiscountScore2) +
                            //Int32.Parse(vAppendPromote.RPIDiscountScore3) + Int32.Parse(vAppendPromote.RPIDiscountScore4) + Int32.Parse(vAppendPromote.RPIDiscountScore5);


                            GVThesisCoop.DataSource = thesisCoopService.GetDataTableByAppSn(vApplyAudit.AppSn);
                            GVThesisCoop.DataBind();

                            if (!int.TryParse(vAppendPromote.RPIDiscountTotal, out discountTotal))
                            {
                                RPIDiscountTable.Visible = false;
                                RPIDiscount.Text = "無";
                                RPIDiscountTotal1.Text = "0";
                            }
                            else
                            {
                                RPIDiscount.Text = discountTotal.ToString();
                                RPIDiscountTotal1.Text = discountTotal.ToString();
                                decimal t;
                                if (decimal.TryParse(AppRPITotalScore.Text, out t))
                                {
                                    AppRPITotalScore.Text = (t + (decimal)discountTotal).ToString();
                                }

                            }

                            if (vAppendPromote.RPIDiscountScore1 != null && !vAppendPromote.RPIDiscountScore1.Equals("0"))
                            {
                                RPIDiscountScore1.Text = "60分";
                            }
                            else
                            {
                                RPIDiscountScore1.Text = "無";
                            }

                            for (int i = 0; i < RPIDiscountScore2.Items.Count; i++)
                            {
                                if (vAppendPromote.RPIDiscountScore2.ToString().Equals(RPIDiscountScore2.Items[i].Value))
                                {
                                    RPIDiscountScore2.Items[i].Selected = true;
                                }
                                else
                                {
                                    RPIDiscountScore2.Items[i].Selected = false;
                                }
                            }

                            for (int i = 0; i < RPIDiscountScore3.Items.Count; i++)
                            {
                                if (vAppendPromote.RPIDiscountScore3.ToString().Equals(RPIDiscountScore3.Items[i].Value))
                                {
                                    RPIDiscountScore3.Items[i].Selected = true;
                                }
                                else
                                {
                                    RPIDiscountScore3.Items[i].Selected = false;
                                }
                            }

                            for (int i = 0; i < RPIDiscountScore4.Items.Count; i++)
                            {
                                if (vAppendPromote.RPIDiscountScore4.ToString().Equals(RPIDiscountScore4.Items[i].Value))
                                {
                                    RPIDiscountScore4.Items[i].Selected = true;
                                }
                                else
                                {
                                    RPIDiscountScore4.Items[i].Selected = false;
                                }
                            }

                            //師鐸獎
                            if (!String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore1Name))
                            {
                                RPIDiscountScore1HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore1Name);
                                RPIDiscountScore1HyperLink.Text = vAppendPromote.RPIDiscountScore1Name;
                                RPIDiscountScore1HyperLink.Visible = true;
                            }

                            //教師優良教師
                            if (!String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore4Name))
                            {
                                RPIDiscountScore2HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore2Name);
                                RPIDiscountScore2HyperLink.Text = vAppendPromote.RPIDiscountScore2Name;
                                RPIDiscountScore2HyperLink.Visible = true;
                            }

                            //優良導師
                            if (!String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore3Name))
                            {
                                RPIDiscountScore3HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore3Name);
                                RPIDiscountScore3HyperLink.Text = vAppendPromote.RPIDiscountScore3Name;
                                RPIDiscountScore3HyperLink.Visible = true;
                            }
                            //執行人體試驗
                            if (!String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore4Name))
                            {
                                RPIDiscountScore4HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore4Name);
                                RPIDiscountScore4HyperLink.Text = vAppendPromote.RPIDiscountScore4Name;
                                RPIDiscountScore4HyperLink.Visible = true;
                            }
                        }



                        //下載推薦函
                        if (vApplyAudit.AppRecommendUploadName != null && !vApplyAudit.AppRecommendUploadName.Equals(""))
                        {
                            RecommendUploadTableRow.Visible = true;
                            RecommendUploadCB.Checked = true;
                            RecommendHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppRecommendUploadName);
                            RecommendHyperLink.Text = vApplyAudit.AppRecommendUploadName;
                            RecommendHyperLink.Visible = true;
                            RecommendUploadFUName.Text = vApplyAudit.AppRecommendUploadName;
                        }


                        //下載合著人證明(好像真的不用)
                        //if (vApplyAudit.AppCoAuthorUploadName != null && !vApplyAudit.AppCoAuthorUploadName.Equals(""))
                        //{
                        //    AppPSummaryCN.Text = vApplyAudit.AppSummaryCN;
                        //    AppPCoAuthorHyperLink.NavigateUrl = location + vApplyAudit.AppCoAuthorUploadName;
                        //    AppPCoAuthorHyperLink.Text = vApplyAudit.AppCoAuthorUploadName;
                        //    AppPCoAuthorHyperLink.Visible = true;
                        //}


                        //載入其他


                        //下載研究計畫主持人證明
                        if (vApplyAudit.AppPPMUploadName != null && !vApplyAudit.AppPPMUploadName.Equals(""))
                        {
                            AppPPMTableRow.Visible = true;
                            AppPPMUploadCB.Checked = true;
                            AppPPMHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppPPMUploadName);
                            AppPPMHyperLink.Text = vApplyAudit.AppPPMUploadName;
                            AppPPMHyperLink.Visible = true;
                        }
                        //醫師證書                        
                        if (vApplyAudit.AppDrCaUploadName != null && !vApplyAudit.AppDrCaUploadName.Equals(""))
                        {
                            AppDrCaTableRow.Visible = true;
                            AppDrCaUploadCB.Checked = true;
                            AppDrCaHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppDrCaUploadName);
                            AppDrCaHyperLink.Text = vApplyAudit.AppDrCaUploadName;
                            AppDrCaHyperLink.Visible = true;
                        }

                        //教育部教師資格證書影
                        if (vApplyAudit.AppTeacherCaUploadName != null && !vApplyAudit.AppTeacherCaUploadName.Equals(""))
                        {
                            AppTeacherCaTableRow.Visible = true;
                            AppTeacherCaUploadCB.Checked = true;
                            AppTeacherCaHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppTeacherCaUploadName);
                            AppTeacherCaHyperLink.Text = vApplyAudit.AppTeacherCaUploadName;
                            AppTeacherCaHyperLink.Visible = true;
                        }
                        //教學
                        if (vApplyAudit.AppOtherTeachingUploadName != null && !vApplyAudit.AppOtherTeachingUploadName.Equals(""))
                        {
                            OtherTeachingTableRow.Visible = true;
                            AppOtherTeachingUploadCB.Checked = true;
                            AppOtherTeachingHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppOtherTeachingUploadName);
                            AppOtherTeachingHyperLink.Text = vApplyAudit.AppOtherTeachingUploadName;
                            AppOtherTeachingHyperLink.Visible = true;
                        }

                        //服務
                        if (vApplyAudit.AppOtherServiceUploadName != null && !vApplyAudit.AppOtherServiceUploadName.Equals(""))
                        {
                            OtherServiceTableRow.Visible = true;
                            AppOtherServiceUploadCB.Checked = true;
                            AppOtherServiceHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppOtherServiceUploadName);
                            AppOtherServiceHyperLink.Text = vApplyAudit.AppOtherServiceUploadName;
                            AppOtherServiceHyperLink.Visible = true;
                        }

                        //下載著作出版等刊物或個人事蹟等相關資料
                        if (vApplyAudit.AppPublicationUploadName != null && !vApplyAudit.AppPublicationUploadName.Equals(""))
                        {
                            AppPublicationTableRow.Visible = false;
                            AppPublicationUploadCB.Checked = true;
                            AppPublicationHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppPublicationUploadName);
                            AppPublicationHyperLink.Text = vApplyAudit.AppPublicationUploadName;
                            AppPublicationHyperLink.Visible = true;
                            AppPublicationUploadFU.Visible = true;
                            AppPublicationUploadFUName.Text = vApplyAudit.AppPublicationUploadName;
                        }

                        if (Session["EmpSn"] != null)
                        {

                            VAppendDegree vAppendDegree = new VAppendDegree();
                            //載入 AppendDegree學位送審延伸資料
                            if (vApplyAudit.AppAttributeNo.Equals(chkDegree))
                            {
                                vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                                if (vAppendDegree != null)
                                {
                                    if (vAppendDegree.AppDDegreeThesisUploadName != null && !vAppendDegree.AppDDegreeThesisUploadName.Equals(""))
                                    {
                                        AppDegreeThesisTableRow.Visible = true;
                                        AppDegreeThesisHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDDegreeThesisUploadName);
                                        AppDegreeThesisHyperLink.Text = vAppendDegree.AppDDegreeThesisUploadName;
                                        AppDegreeThesisHyperLink.Visible = true;
                                        AppDegreeThesisName.Text = vAppendDegree.AppDDegreeThesisName;
                                        AppDegreeThesisNameEng.Text = vAppendDegree.AppDDegreeThesisNameEng;
                                        PassAuditAppPublication.Text = AppDegreeThesisName.Text.ToString();
                                    }
                                }

                                //若是有國外經歷資料須載入 AboutFgn true
                                DataTable dtTable = crudObject.GetAllVTeacherEduByEmpSn(vApplyAudit.EmpSn);
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    if ("TWN".Equals(dtTable.Rows[i][1].ToString()))
                                    {
                                        switchFgn = "TWN";
                                    }
                                    else
                                    {
                                        AboutFgn.Visible = true;
                                        if ("JPN".Equals(dtTable.Rows[i][1].ToString()))
                                        {
                                            switchFgn = "JPN";
                                            AboutJPN.Visible = true;
                                        }
                                    }
                                }

                                //載入外國學歷 以學位送審才需要
                                if (switchFgn != null && !switchFgn.Equals(""))
                                {
                                    LoadFgn(vAppendDegree, switchFgn);
                                }


                                DataTable dt = crudObject.GetThesisOralList(Int32.Parse(Session["AppSn"].ToString()));

                                if (dt.Rows.Count > 0)
                                {
                                    AppThesisOral.Visible = true;
                                    GVThesisOral.DataSource = dt;
                                    GVThesisOral.DataBind();
                                }
                            }

                            //教師學歷資料
                            GVTeachEdu.DataBind();

                            if (vApplyAudit.AppKindNo.Equals(chkApply)) //新聘
                            {
                                //教師經歷資料(新聘)
                                if (GVTeachExp.Rows.Count > 0)
                                {
                                    GVTeachExp.Visible = true;
                                    GVTeachExp.DataBind();
                                }
                                else
                                {
                                    LbTeachExp.Visible = true;
                                }
                            }
                            else
                            {
                                //教師校內經歷資料(升等)                            
                                if (GVTeacherTmuExp.Rows.Count > 0)
                                {
                                    GVTeacherTmuExp.Visible = true;
                                    GVTeacherTmuExp.DataBind();
                                }
                                else
                                {
                                    LbTeachExp.Visible = true;
                                }

                                //升等才有
                                int beginYear = 0;
                                int endYear = 0;
                                if (Session["sSemester"].Equals("1"))
                                {
                                    beginYear = Int32.Parse(Session["sYear"].ToString()) - 4;
                                    endYear = Int32.Parse(Session["sYear"].ToString()) - 2;
                                }
                                else
                                {
                                    beginYear = Int32.Parse(Session["sYear"].ToString()) - 3;
                                    endYear = Int32.Parse(Session["sYear"].ToString()) - 1;
                                }


                                DataTable dt = crudObject.GetActucalEachWeekHour(crudObject.GetVEmployeeFromTmuHrByEmpIdno(vEmp.EmpIdno), beginYear, endYear);
                                if (dt.Rows.Count > 0)
                                {
                                    TeachingEvaluate.Visible = false;
                                    GVTeachEvaluate.DataSource = dt;
                                    GVTeachEvaluate.DataBind();
                                }
                            }




                            //教師證發放資料                             
                            if (GVTeachCa.Rows.Count > 0)
                            {
                                TeachCaTableRow.Visible = true;
                                GVTeachCa.DataBind();
                            }
                            else
                            {
                                //新聘
                                if (vApplyAudit.AppKindNo.Equals(chkApply)) //新聘
                                {
                                    TeachCaTableRow.Visible = true;
                                    LbTeachCa.Visible = true;
                                }
                            }

                            if (GVTeachHonour.Rows.Count > 0)
                            {
                                if (vApplyAudit.AppKindNo.Equals(chkApply)) //新聘
                                {
                                    TeachHonourTableRow.Visible = true;
                                    GVTeachHonour.DataBind();
                                }
                            }
                            else
                            {
                                //新聘
                                if (vApplyAudit.AppKindNo.Equals(chkApply)) //新聘
                                {
                                    TeachHonourTableRow.Visible = true;
                                    LbTeachHonour.Visible = true;
                                }

                            }


                            //教師上傳論文積分表
                            GVThesisScore.DataBind();


                            //教師授課進度
                            //升等
                            if (vApplyAudit.AppKindNo.Equals(chkPromote))
                            {
                                TeachLessonTableRow.Visible = true;
                                GVTeachLesson.DataBind();
                                if (GVTeachLesson.Rows.Count <= 0)
                                    LbTeachLesson.Visible = true;
                            }


                        }

                        if (vApplyAudit.AppKindNo.Equals(chkApply))
                        {
                            vApplyAudit.Questionnaires = this.questionnaireService.Get(vApplyAudit.AppSn);
                            if (vApplyAudit.Questionnaires.Count > 0)
                            {  
                                List<VRefQuestionnaire> vRefQuestionnaires = this.questionnaireService.GetAll();
                                foreach (VApplyQuestionnaire q in vApplyAudit.Questionnaires)
                                {
                                    VRefQuestionnaire refQ = vRefQuestionnaires.Where(o => o.ID == q.QuestionnaireID).FirstOrDefault();
                                    if (refQ != null)
                                    {
                                        this.tcQuestionnaire.Controls.Add(new LiteralControl(refQ.Item));
                                        this.tcQuestionnaire.Controls.Add(new LiteralControl(" " + q.ItemContent + "<br/>"));
                                    }
                                }
                                this.trQuestionnaire.Visible = true;
                            }
                        }



                    }

                    //撈取(職員或外審委員)審核資料,先將原有資料帶出
                    if (Session["AcctAuditorSnEmpId"] == null)
                    {
                        Response.Redirect("~/ManageLogin.aspx"); //Time out控制 改
                    }
                    else
                    {
                        //啟動按鈕是否enable 若已進入簽核 enable=false
                        //if (vApplyAudit != null)
                        //{
                        //    if (!vApplyAudit.AppStage.Equals("0") && !vApplyAudit.AppStep.Equals("0"))
                        //    {
                        //        BtnStartAuditor.Enabled = false;
                        //    }
                        //}

                        vAuditExecute = new VAuditExecute();
                        vAuditExecute.AppSn = vApplyAudit.AppSn;
                        if (Session["AcctAuditorSnEmpId"] != null && Session["AcctAuditorSnEmpId"].ToString() != "")
                            vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                        else if (AcctAuditorSnEmpId != null && AcctAuditorSnEmpId != "")
                            vAuditExecute.ExecuteAuditorSnEmpId = AcctAuditorSnEmpId; //目前抓自己AccountForAudit中的 ID
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert(' Session Time Out，\\請登出後後重新輸入!!');", true);
                            return;
                        }
                        vAuditExecute.ExecuteStatus = false;

                        vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆
                        AppAttributeNo.Text = vApplyAudit.AppAttributeNo;


                        if (vAuditExecute != null)
                        {

                            AuditRoleName.Text = crudObject.GetExecuteRoleName(vAuditExecute.AppSn); //撈取正要審核者的RoleName
                            Session["ExecuteStage"] = vAuditExecute.ExecuteStage;
                            Session["ExecuteStep"] = vAuditExecute.ExecuteStep;
                            Session["AppUnitNo"] = vApplyAudit.AppUnitNo;
                            TbPassExecute.Visible = true;

                            DDExecutePass.Items.Clear();
                            DDExecutePass.Items.Add(new ListItem("推薦(通過)", "1"));
                            DDExecutePass.Items.Add(new ListItem("不推薦(不通過)", "2"));
                            DDExecutePass.Items.Add(new ListItem("退回本人", "3"));
                            DDExecutePass.DataBind();
                            LbReturnReason.Text = "不通過(退回)原因";
                            //院經理也要可以退回
                            // 填寫教學分數,服務分數 -- 升等才需要 chkPromote (系教評,院教評,校教評)
                            if ((vAuditExecute.ExecuteStage.Equals("1") && vAuditExecute.ExecuteStep.Equals("1")) ||
                                (vAuditExecute.ExecuteStage.Equals("2") && vAuditExecute.ExecuteStep.Equals("1")) ||
                                (vAuditExecute.ExecuteStage.Equals("3") && vAuditExecute.ExecuteStep.Equals("1")) ||
                                (vAuditExecute.ExecuteStage.Equals("4") && vAuditExecute.ExecuteStep.Equals("1")))
                            {
                                //if (vApplyAudit.AppUnitNo.Substring(0,1).Equals("E"))
                                if (vApplyAudit.AppKindNo.Equals(chkPromote)) //升等
                                {
                                    RowPassExecuteScore.Visible = true;
                                    TeachingScore.Text = vAuditExecute.ExecuteTeachingScore; //若曾填過的資料也帶出來
                                    ServiceScore.Text = vAuditExecute.ExecuteServiceScore;
                                }
                            }

                            //**
                            //Step0 其他意見
                            //Step1 主管評議
                            //Step2 七人小組意見<br/>系教評決議
                            //Step3 小組意見<br/>院教評決議
                            //Step4 小組意見<br/>校教評決議

                            if (vAuditExecute.ExecuteStage.Equals("1") && vAuditExecute.ExecuteStep.Equals("2"))
                            {
                                Step1.Visible = true; //
                            }
                            else if (vAuditExecute.ExecuteStage.Equals("2") && vAuditExecute.ExecuteStep.Equals("1"))
                            {
                                Step2.Visible = true;
                            }
                            else if (vAuditExecute.ExecuteStage.Equals("3") && vAuditExecute.ExecuteStep.Equals("1"))
                            {
                                Step3.Visible = true;
                            }
                            else if (vAuditExecute.ExecuteStage.Equals("3") && vAuditExecute.ExecuteStep.Equals("3"))
                            {
                                Step1.Visible = true;
                            }
                            else if ((vAuditExecute.ExecuteStage.Equals("2") && vAuditExecute.ExecuteStep.Equals("2")) || //系所主管，院經理
                                     (vAuditExecute.ExecuteStage.Equals("3") && vAuditExecute.ExecuteStep.Equals("2"))
                                )
                            {
                                Step1.Visible = true;
                            }
                            else
                            {
                                Step0.Visible = true;
                            }


                            RowPassExecuteCommentsA.Visible = true; //所有都可以填評語

                            if (vAuditExecute.ExecuteStage.Equals("4") && vAuditExecute.ExecuteStep.Equals("4"))//發現部分審核關卡有到44 但部分關卡只到43 要再做確認
                            {
                                //PassRow0.Visible = false;
                                PassRow.Visible = false;
                                StartOutAudit.Visible = true;
                            }

                            //考評資料帶出
                            if (vAuditExecute.ExecuteCommentsA != null) PassExecuteCommentsA.Text = vAuditExecute.ExecuteCommentsA;
                            if (vAuditExecute.ExecuteReturnReason != null) ReturnReason.Text = vAuditExecute.ExecuteReturnReason;
                            if (vAuditExecute.ExecuteTeachingScore != null) TeachingScore.Text = vAuditExecute.ExecuteTeachingScore;
                            if (vAuditExecute.ExecuteServiceScore != null) ServiceScore.Text = vAuditExecute.ExecuteServiceScore;

                            //查看之前別人的評語  2017/03/09全部改成顯示所有評語

                            DataTable dt = null;
                            //新聘
                            if (vApplyAudit.AppKindNo.Equals(chkApply))
                            {
                                dt = crudObject.GetOtherApplyComments(vAuditExecute.AppSn.ToString(), vAuditExecute.ExecuteStage.ToString(), vAuditExecute.ExecuteStep.ToString());
                                OtherApplyComments.Visible = true;
                                GVApplyOtherComments.DataSource = dt;
                                GVApplyOtherComments.DataBind();
                            }
                            else
                            {
                                dt = crudObject.GetOtherPromoteComments(vAuditExecute.AppSn.ToString(), vAuditExecute.ExecuteStage.ToString(), vAuditExecute.ExecuteStep.ToString());
                                OtherPromoteComments.Visible = true;
                                GVPromoteOtherComments.DataSource = dt;
                                GVPromoteOtherComments.DataBind();

                                //查看前人的教學分數?
                            }

                            DataTable DT = null;
                            //新聘
                            if (vApplyAudit.AppKindNo.Equals(chkApply))
                            {
                                DT = crudObject.GetOtherApplyComments(vAuditExecute.AppSn.ToString(), vAuditExecute.ExecuteStage.ToString(), vAuditExecute.ExecuteStep.ToString());
                                ApplyComments.Visible = true;
                                GVApplyComments.DataSource = DT;
                                GVApplyComments.DataBind();
                            }
                            else
                            {
                                DT = crudObject.GetOtherPromoteComments(vAuditExecute.AppSn.ToString(), vAuditExecute.ExecuteStage.ToString(), vAuditExecute.ExecuteStep.ToString());
                                PromoteComments.Visible = true;
                                GVPromoteComments.DataSource = DT;
                                GVPromoteComments.DataBind();
                            }
                        }
                        else
                        {
                            vAuditExecute = new VAuditExecute();
                            vAuditExecute.AppSn = vApplyAudit.AppSn;
                            vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                            vAuditExecute.ExecuteStatus = false;
                            vAuditExecute = crudObject.GetExecuteAuditorDataViewOnly(vAuditExecute); //就算沒有審核權限也要可以看到資料
                            DataTable DT = null;
                            if (vAuditExecute != null)
                            {
                                AuditRoleName.Text = crudObject.GetExecuteRoleName(vAuditExecute.AppSn);
                                //新聘
                                if (vApplyAudit.AppKindNo.Equals(chkApply))
                                {
                                    DT = crudObject.GetOtherApplyComments(vAuditExecute.AppSn.ToString(), vAuditExecute.ExecuteStage.ToString(), vAuditExecute.ExecuteStep.ToString());
                                    ApplyComments.Visible = true;
                                    GVApplyComments.DataSource = DT;
                                    GVApplyComments.DataBind();
                                }
                                else
                                {
                                    DT = crudObject.GetOtherPromoteComments(vAuditExecute.AppSn.ToString(), vAuditExecute.ExecuteStage.ToString(), vAuditExecute.ExecuteStep.ToString());
                                    PromoteComments.Visible = true;
                                    GVPromoteComments.DataSource = DT;
                                    GVPromoteComments.DataBind();
                                }
                            }
                            if (Session["AcctRole"] != null && ("M".Equals(Session["AcctRole"].ToString())))
                            {
                                DT = crudObject.GetOtherApplyCommentsHR(vApplyAudit.AppSn.ToString());
                                ApplyComments.Visible = true;
                                GVApplyComments.DataSource = DT;
                                GVApplyComments.DataBind();
                            }
                        }
                    }




                    //管理角色M || 校內一般審查者A 才載入資料並顯示

                    if (Session["AcctRole"] != null && ("M".Equals(Session["AcctRole"].ToString())))
                    {
                        ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();

                        DataTable dtTable;
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[] {new DataColumn("ExecuteAuditorSn", System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteStage", System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteStageNum", System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteStepNum", System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteRoleName", System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteAuditorSnEmpId", System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteAuditorName", System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteAuditorEmail",System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteStatus",System.Type.GetType("System.String")),
                                                   new DataColumn("EDate",System.Type.GetType("System.String")),
                                                   new DataColumn("EComments",System.Type.GetType("System.String"))
                                                    });

                        dtTable = crudObject.GetAllAuditExecuteByEmpSn(vApplyAudit.AppSn);


                        String[] strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());
                        String[] strStep = { "0", "(1)", "(2)", "(3)" };
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            dt.Rows.Add(dtTable.Rows[i][0].ToString(),
                                strStatus[Int32.Parse(dtTable.Rows[i][1].ToString())],
                                dtTable.Rows[i][1].ToString(),
                                dtTable.Rows[i][2].ToString(),
                                dtTable.Rows[i][3].ToString(),
                                dtTable.Rows[i][4].ToString(),
                                dtTable.Rows[i][5].ToString(),
                                dtTable.Rows[i][6].ToString(),
                                //GetAuditStatus((Boolean)dtTable.Rows[i][12]),
                                GetAuditStatus(dtTable.Rows[i][12].ToString()),
                                dtTable.Rows[i][8].ToString(),
                                dtTable.Rows[i][13].ToString()

                               );
                        }

                        //0.ExecuteSn, v
                        //1.ExecuteStage, v v
                        //2.ExecuteStep, v
                        //3.ExecuteRoleName, v
                        //4.ExecuteAuditorSnEmpId, v
                        //5.emp_name, v ExecuteAuditorName
                        //6.ExecuteAuditorEmail,v
                        //7.ExecuteAccept,
                        //8.ExecuteDate,
                        //9.ExecuteBngDate, " + v
                        //10.ExecuteEndDate, v
                        //11.ExecutePass,
                        //12.ExecuteStatus v                           

                        Session["AppSn"] = Request.QueryString["AppSn"].ToString();
                        Session["AcctRole"] = "M";
                        GVAuditExecute.DataSource = dt;
                        GVAuditExecute.DataBind();
                    }

                    //ManageEmpId ApplyerID
                    //新聘
                    DESCrypt DES = new DESCrypt();
                    if (vApplyAudit.AppKindNo.Equals(chkApply)) //新聘
                    {
                        ModifyEmpBase.NavigateUrl = "ApplyEmp.aspx?ApplyerID=" + DES.Encrypt(EmpIdno.Text) + "&AppSn=" + DES.Encrypt(Session["AppSn"].ToString()) + "&ManageEmpId=" + Session["AcctAuditorSnEmpId"].ToString() + "&Identity=Manager";
                    }
                    else
                    {
                        ModifyEmpBase.NavigateUrl = "PromoteEmp.aspx?ApplyerID=" + DES.Encrypt(EmpIdno.Text) + "&ManageEmpId=" + Session["AcctAuditorSnEmpId"].ToString() + "&Identity=Manager&HRAppSn=" + Session["AppSn"].ToString();
                        if (vApplyAudit.AppAttributeNo.Equals(chkDegree))
                            ModifyEmpBase.NavigateUrl += "&ApplyAttributeNo=2";
                    }
                    ModifyEmpBase.Text = "更新資料";
                    ModifyEmpBase.Visible = true;

                }
                else
                {
                    //MessageLabel.Text = "抱歉，目前無資料,請洽資訊人員!!";
                    //BtnStartAuditor.Enabled = false;
                    //BtnUpdateAuditor.Enabled = false;
                }

            }
        }

        //載入外國學歷 以學位送審才需要
        protected void LoadFgn(VAppendDegree vAppendDegree, string switchFgn)
        {
            if ("TWN".Equals(switchFgn))
            {

            }
            else
            {
                AboutFgn.Visible = true;
                if (vAppendDegree != null)
                {
                    AppDFgnEduDeptSchoolAdmitCB.Checked = vAppendDegree.AppDFgnEduDeptSchoolAdmit;

                    //國外最高學位畢業證書
                    if (vAppendDegree.AppDFgnDegreeUploadName != null && !vAppendDegree.AppDFgnDegreeUploadName.Equals(""))
                    {
                        AppDFgnDegreeUploadCB.Checked = true;
                        AppDFgnDegreeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnDegreeUploadName);
                        AppDFgnDegreeHyperLink.Text = vAppendDegree.AppDFgnDegreeUploadName;
                        AppDFgnDegreeHyperLink.Visible = true;
                        AppDFgnDegreeUploadFUName.Text = vAppendDegree.AppDFgnDegreeUploadName;
                    }
                    else
                    {
                        AppDFgnDegreeHyperLink.Visible = false;
                    }

                    //國外學校歷年成績單

                    if (vAppendDegree.AppDFgnGradeUploadName != null && !vAppendDegree.AppDFgnGradeUploadName.Equals(""))
                    {
                        AppDFgnGradeUploadCB.Checked = true;
                        AppDFgnGradeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnGradeUploadName);
                        AppDFgnGradeHyperLink.Text = vAppendDegree.AppDFgnGradeUploadName;
                        AppDFgnGradeHyperLink.Visible = true;
                        AppDFgnGradeUploadFUName.Text = vAppendDegree.AppDFgnGradeUploadName;
                    }
                    else
                    {
                        AppDFgnGradeHyperLink.Visible = false;
                    }

                    //國外學歷修業情形一覽表
                    if (vAppendDegree.AppDFgnSelectCourseUploadName != null && !vAppendDegree.AppDFgnSelectCourseUploadName.Equals(""))
                    {
                        AppDFgnSelectCourseUploadCB.Checked = true;
                        AppDFgnSelectCourseHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnSelectCourseUploadName);
                        AppDFgnSelectCourseHyperLink.Text = vAppendDegree.AppDFgnSelectCourseUploadName;
                        AppDFgnSelectCourseHyperLink.Visible = true;
                        AppDFgnSelectCourseUploadFUName.Text = vAppendDegree.AppDFgnSelectCourseUploadName;
                    }
                    else
                    {
                        AppDFgnSelectCourseHyperLink.Visible = false;
                    }

                    //個人出入境紀錄
                    if (vAppendDegree.AppDFgnEDRecordUploadName != null && !vAppendDegree.AppDFgnEDRecordUploadName.Equals(""))
                    {
                        AppDFgnEDRecordUploadCB.Checked = true;
                        AppDFgnEDRecordHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnEDRecordUploadName);
                        AppDFgnEDRecordHyperLink.Text = vAppendDegree.AppDFgnEDRecordUploadName;
                        AppDFgnEDRecordHyperLink.Visible = true;
                        AppDFgnEDRecordUploadFUName.Text = vAppendDegree.AppDFgnEDRecordUploadName;
                    }
                    else
                    {
                        AppDFgnEDRecordHyperLink.Visible = false;
                    }


                    if ("JPN".Equals(switchFgn))
                    {
                        switchFgn = "JPN";
                        AppDFgnJPAdmissionUploadCB.Checked = vAppendDegree.AppDFgnJPAdmissionUpload;

                        //A.入學許可註冊證                        
                        if (vAppendDegree.AppDFgnJPAdmissionUploadName != null && !vAppendDegree.AppDFgnJPAdmissionUploadName.Equals(""))
                        {
                            AppDFgnJPAdmissionUploadCB.Checked = true;
                            AppDFgnJPAdmissionHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPAdmissionUploadName);
                            AppDFgnJPAdmissionHyperLink.Text = vAppendDegree.AppDFgnJPAdmissionUploadName;
                            AppDFgnJPAdmissionHyperLink.Visible = true;
                            AppDFgnJPAdmissionUploadFUName.Text = vAppendDegree.AppDFgnJPAdmissionUploadName;
                        }
                        else
                        {
                            AppDFgnJPAdmissionHyperLink.Visible = false;
                        }

                        //B.修畢學分成績單                        
                        if (vAppendDegree.AppDFgnJPGradeUploadName != null && !vAppendDegree.AppDFgnJPGradeUploadName.Equals(""))
                        {
                            AppDFgnJPGradeUploadCB.Checked = true;
                            AppDFgnJPGradeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPGradeUploadName);
                            AppDFgnJPGradeHyperLink.Text = vAppendDegree.AppDFgnJPGradeUploadName;
                            AppDFgnJPGradeHyperLink.Visible = true;
                            AppDFgnJPGradeUploadFUName.Text = vAppendDegree.AppDFgnJPGradeUploadName;
                        }
                        else
                        {
                            AppDFgnJPGradeHyperLink.Visible = false;
                        }

                        //C.在學證明及修業年數證明
                        if (vAppendDegree.AppDFgnJPEnrollCAUploadName != null && !vAppendDegree.AppDFgnJPEnrollCAUploadName.Equals(""))
                        {
                            AppDFgnJPEnrollCAUploadCB.Checked = true;
                            AppDFgnJPEnrollCAHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPEnrollCAUploadName);
                            AppDFgnJPEnrollCAHyperLink.Text = vAppendDegree.AppDFgnJPEnrollCAUploadName;
                            AppDFgnJPEnrollCAHyperLink.Visible = true;
                            AppDFgnJPEnrollCAUploadFUName.Text = vAppendDegree.AppDFgnJPEnrollCAUploadName;
                        }
                        else
                        {
                            AppDFgnJPEnrollCAHyperLink.Visible = false;
                        }

                        //D.通過論文資格考試證明                        
                        if (vAppendDegree.AppDFgnJPDissertationPassUploadName != null && !vAppendDegree.AppDFgnJPDissertationPassUploadName.Equals(""))
                        {
                            AppDFgnJPDissertationPassUploadCB.Checked = true;
                            AppDFgnJPDissertationPassHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPDissertationPassUploadName);
                            AppDFgnJPDissertationPassHyperLink.Text = vAppendDegree.AppDFgnJPDissertationPassUploadName;
                            AppDFgnJPDissertationPassHyperLink.Visible = true;
                            AppDFgnJPDissertationPassUploadFUName.Text = vAppendDegree.AppDFgnJPDissertationPassUploadName;
                        }
                        else
                        {
                            AppDFgnJPDissertationPassHyperLink.Visible = false;
                        }
                    }
                }//end of vAppendDegree

            }//end of TWN
        }

        private String GetAuditStatus(String status)
        {
            String strData = "";
            switch (status)
            {
                case "0":
                    strData = "未完成";
                    break;
                case "1":
                    strData = "完成";
                    break;
                case "3":
                    strData = "退回本人";
                    break;
            }
            return strData;
        }

        protected void GVTeachEdu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labEduLocal;
            ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
            String[] strDegree = convertDTtoStringArray.GetStringArray(crudObject.GetAllDegreeName());
            String[] strDegreeType = convertDTtoStringArray.GetStringArray(crudObject.GetAllDegreeTypeName());
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (vApplyAudit == null)
                    vApplyAudit = crudObject.GetApplyAuditObj(appSn);
                labEduLocal = (Label)e.Row.FindControl("LabEduLocal");
                if (!"TWN".Equals(labEduLocal.Text.ToString()) && !"JPN".Equals(labEduLocal.Text.ToString()))
                {
                    AboutFgn.Visible = true;
                    switchFgn = labEduLocal.Text.ToString();
                }
                if ("JPN".Equals(labEduLocal.Text.ToString()))
                {
                    AboutFgn.Visible = true;
                    AboutJPN.Visible = true;
                    switchFgn = "JPN";
                }


                //載入外國學歷 以學位送審才需要
                if (switchFgn != null && !switchFgn.Equals(""))
                {
                    VAppendDegree vAppendDegree = new VAppendDegree();
                    //載入學位論文
                    if (vApplyAudit.AppKindNo.Equals(chkApply) && vApplyAudit.AppAttributeNo.Equals(chkDegree))
                    {
                        vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                    }
                    LoadFgn(vAppendDegree, switchFgn);
                }
                if (EmailEdu.Text.ToString().Equals(""))
                {
                    EmailEdu.Text = e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + "至" + e.Row.Cells[4].Text.ToString();
                }
                else
                {
                    EmailEdu.Text = EmailEdu.Text.ToString() + "</br>" + e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + "至" + e.Row.Cells[4].Text.ToString();
                }
                //e.Row.Cells[8].Text 
            }
        }

        protected void GVTeachExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label ExpUploadName;
            HyperLink hyperLnkTeachExp;
            string empSn = "1";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkTeachExp = (HyperLink)e.Row.FindControl("HyperLinkExp");
                //string strFileName = e.Row.Cells[6].Text;
                ExpUploadName = (Label)e.Row.FindControl("ExpUploadName");
                string strFileName = ExpUploadName.Text;
                hyperLnkTeachExp.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    ExpUploadName.Text = "(無)";
                    hyperLnkTeachExp.Visible = false;
                }
                if (EmailExp.Text.ToString().Equals(""))
                {
                    EmailExp.Text = e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + " " + e.Row.Cells[4].Text.ToString() + "至" + e.Row.Cells[5].Text.ToString();
                }
                else
                {
                    EmailExp.Text = EmailExp.Text.ToString() + "</br>" + e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + "至" + e.Row.Cells[4].Text.ToString();
                }
            }
        }

        protected void GVTeacherTmuExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink hyperLnkTeachExp;
            Label ExpUploadName;
            string empSn = "1";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkTeachExp = (HyperLink)e.Row.FindControl("HyperLinkTmuExp");
                DataRowView drv = e.Row.DataItem as DataRowView;
                string strFileName = drv["ExpUploadName"].ToString();
                //ExpUploadName = (Label)e.Row.FindControl("ExpUploadName");
                //string strFileName = ExpUploadName.Text;
                //string strFileName = e.Row.Cells[7].Text;
                hyperLnkTeachExp.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLnkTeachExp.Text = "無資料";
                    hyperLnkTeachExp.Enabled = false;
                }
                if (EmailExp.Text.ToString().Equals(""))
                {
                    EmailExp.Text = e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + " " + e.Row.Cells[4].Text.ToString() + "至" + e.Row.Cells[5].Text.ToString();
                }
                else
                {
                    EmailExp.Text = EmailExp.Text.ToString() + "</br>" + e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + "至" + e.Row.Cells[4].Text.ToString();
                }
            }
        }

        protected void GVThesisScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            HyperLink hyperLnkThesis;
            Label lblThesisSign;
            Label lblThesisUploadName;
            TextBox txtIsRepresentative;
            Label lbThesisName;
            string empSn = "1";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkThesis = (HyperLink)e.Row.FindControl("HyperLinkThesis");
                lblThesisSign = (Label)e.Row.FindControl("lblThesisSign");
                lbThesisName = (Label)e.Row.FindControl("TName");
                lblThesisUploadName = (Label)e.Row.FindControl("ThesisUploadName");
                //if (Session["PublicationUploadName"]!=null&& Session["PublicationUploadName"].ToString().Equals(e.Row.Cells[10].Text))
                //{
                //    lblThesisSign.Text = "●";
                //}
                txtIsRepresentative = (TextBox)e.Row.FindControl("IsRepresentative");
                if (txtIsRepresentative.Text.ToString().Equals("True"))
                {
                    lblThesisSign.Text = "●";
                    if (lbThesisName != null)
                    {
                        if (PassAuditAppPublication.Text.ToString().Equals(""))
                        {
                            PassAuditAppPublication.Text = lbThesisName.Text.ToString();
                        }
                        else
                        {
                            PassAuditAppPublication.Text = PassAuditAppPublication.Text + "  " + lbThesisName.Text.ToString();
                        }
                    }
                }

                String strFileName = lblThesisUploadName.Text.ToString();
                hyperLnkThesis.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLnkThesis.Text = "無資料";
                    hyperLnkThesis.Enabled = false;
                }

                Session["ThesisAccuScore"] = float.Parse(e.Row.Cells[12].Text) + float.Parse(Session["ThesisAccuScore"].ToString());

            }
            //20171017 11:03 人資 亭吟通知 黃彥鈞  李境祐的論文積分表 分數兩倍化 400.68 & 417
            if (String.IsNullOrEmpty(AppPThesisAccuScore.Text))
                AppPThesisAccuScore.Text = Session["ThesisAccuScore"].ToString();


        }

        protected void GVThesisScoreCoAuthor_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Label labelThesisCoAuthorUploadName;
            Label labelThesisJournalRefUploadName;
            Label lb_ThesisJournalRef;
            Label lb_ThesisCo;
            HyperLink hyperLnkThesisCo;
            HyperLink hyperLinkThesisJournalRef;
            string empSn = "1";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkThesisCo = (HyperLink)e.Row.FindControl("HyperLinkThesisCo");
                hyperLinkThesisJournalRef = (HyperLink)e.Row.FindControl("HyperLinkThesisJournalRef");
                labelThesisCoAuthorUploadName = (Label)e.Row.FindControl("ThesisCoAuthorUploadName");
                labelThesisJournalRefUploadName = (Label)e.Row.FindControl("ThesisJournalRefUploadName");
                lb_ThesisJournalRef = (Label)e.Row.FindControl("lb_ThesisJournalRef");
                lb_ThesisCo = (Label)e.Row.FindControl("lb_ThesisCo");
                //string openFile = "UploadFiles/" + empSn + "/" + labelThesisCoAuthorUploadName.Text.ToString();
                //hyperLnkThesis.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";

                String strFileName = labelThesisCoAuthorUploadName.Text.ToString();
                hyperLnkThesisCo.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {

                    //hyperLnkThesisCo.Text = "無資料";
                    lb_ThesisCo.Text = "(無)";
                    //hyperLnkThesisCo.Enabled = false;
                    hyperLnkThesisCo.Visible = false;
                    lb_ThesisCo.Visible = true;
                }

                strFileName = labelThesisJournalRefUploadName.Text.ToString();
                hyperLinkThesisJournalRef.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    lb_ThesisJournalRef.Text = "(無)";
                    //hyperLinkThesisJournalRef.Text = "無資料";
                    //hyperLinkThesisJournalRef.Enabled = false;
                    hyperLinkThesisJournalRef.Visible = false;
                    lb_ThesisJournalRef.Visible = true;
                }

            }
        }

        protected void GVAuditExecute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int AppSn = 0;
            TextBox txtBoxAuditorId;
            TextBox txtBoxAuditorName;
            TextBox txtBoxAuditorEmail;
            TextBox txtBoxEmpSn;
            LinkButton linkbtnLink1;
            Label lbLabelLabel2;
            Label EDate;
            string strParameter = "";
            VAccountForAudit vAccountForAudit = new VAccountForAudit();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GetSettings setting = new GetSettings();
                txtBoxAuditorId = (TextBox)e.Row.FindControl("TextBoxAuditorSnEmpId");
                txtBoxAuditorName = (TextBox)e.Row.FindControl("TextBoxAuditorName");
                txtBoxAuditorEmail = (TextBox)e.Row.FindControl("TextBoxAuditorEmail");
                linkbtnLink1 = (LinkButton)e.Row.FindControl("lkbComments");
                lbLabelLabel2 = (Label)e.Row.FindControl("lbComments");
                txtBoxEmpSn = (TextBox)e.Row.FindControl("TextBoxAuditorSnEmpId");
                EDate = (Label)e.Row.FindControl("EDate");
                string strStage = DataBinder.Eval(e.Row.DataItem, "ExecuteStageNum").ToString();
                string strStep = DataBinder.Eval(e.Row.DataItem, "ExecuteStepNum").ToString();
                string EComments = DataBinder.Eval(e.Row.DataItem, "EComments").ToString();
                strParameter = "ObjId=" + txtBoxAuditorId.ClientID + "&ObjName=" + txtBoxAuditorName.ClientID + "&ObjEmail=" + txtBoxAuditorEmail.ClientID + "&Stage=" + strStage + "&Step=" + strStep + "&AttributeNo=" + AppAttributeNo.Text.ToString() + "&AppSn=" + Request.QueryString["AppSn"].ToString();
                txtBoxAuditorName.Attributes["onclick"] = "window.open('FunSelectAuditor.aspx?" + strParameter + " ','mywin','100','100','no','center');return true;";

                if (strStage == "6" && strStep == "1")
                {

                    linkbtnLink1.Visible = true;
                    lbLabelLabel2.Visible = false;
                    vAccountForAudit.AcctEmail = txtBoxAuditorEmail.Text.ToString().Trim();
                    if (vAccountForAudit != null && !String.IsNullOrEmpty(vAccountForAudit.AcctEmail) && txtBoxAuditorId.Text != null)//&& Session["EmpSn"]!=null && setting.GetYear()!=null && setting.GetSemester()!=null && vAccountForAudit.AcctAuditorSnEmpId!=null)
                    {
                        vAccountForAudit.AcctAppSn = Request.QueryString["AppSn"].ToString();
                        vAccountForAudit = crudObject.GetAccountForAuditByAppSn(vAccountForAudit);
                        if (vAccountForAudit != null)
                        {
                            if (EDate != null)
                                linkbtnLink1.PostBackUrl = "javascript:void(window.open('OuterAudit.aspx?EmpSn=" + Session["EmpSn"].ToString() + "&AppYear=" + setting.GetYear().ToString() + "&AppSemester=" + setting.GetSemester().ToString() + "&SnEmpId=" + vAccountForAudit.AcctAuditorSnEmpId + "&OuterName=" + txtBoxAuditorName.Text + "&EDate=" + EDate.Text + " ','_blank','height=800','width=800') )";
                            else
                                linkbtnLink1.PostBackUrl = "javascript:void(window.open('OuterAudit.aspx?EmpSn=" + Session["EmpSn"].ToString() + "&AppYear=" + setting.GetYear().ToString() + "&AppSemester=" + setting.GetSemester().ToString() + "&SnEmpId=" + vAccountForAudit.AcctAuditorSnEmpId + "&OuterName=" + txtBoxAuditorName.Text + "','_blank','height=800','width=800') )";
                        }
                        else
                            linkbtnLink1.Text = "未指派委員";
                    }
                    else
                        linkbtnLink1.Text = "未指派委員";
                }
                else
                {
                    lbLabelLabel2.Visible = true;
                    linkbtnLink1.Visible = false;
                    lbLabelLabel2.Text = EComments;
                }
            }


        }

        protected void GVThesisOral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labThesisOralType;
            String[] strDegreeThesisType = { "", "論文指導", "口試委員" };
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                labThesisOralType = (Label)e.Row.FindControl("ThesisOralType");
                labThesisOralType.Text = strDegreeThesisType[Convert.ToInt32(labThesisOralType.Text.ToString())];
            }
        }

        //更新簽核資料
        protected void BtnUpdateAuditor_Click(object sender, EventArgs e)
        {
            VAuditExecute vAuditExecute;
            int i = 0;
            foreach (GridViewRow row in GVAuditExecute.Rows)
            {
                //因為在GridView需指定後才能抓到值
                TextBox strExecuteSn = (TextBox)row.FindControl("TextBoxAuditorSn");
                TextBox txtBoxAuditorSnEmpId = (TextBox)row.FindControl("TextBoxAuditorSnEmpId");
                TextBox txtBoxAuditorEmail = (TextBox)row.FindControl("TextBoxAuditorEmail");
                TextBox txtBoxAuditorName = (TextBox)row.FindControl("TextBoxAuditorName");

                if (!string.IsNullOrEmpty(txtBoxAuditorSnEmpId.Text))
                {
                    vAuditExecute = new VAuditExecute();
                    vAuditExecute.ExecuteSn = Convert.ToInt32(strExecuteSn.Text.ToString());
                    vAuditExecute.ExecuteAuditorSnEmpId = txtBoxAuditorSnEmpId.Text.ToString();
                    vAuditExecute.ExecuteAuditorEmail = txtBoxAuditorEmail.Text.ToString();
                    vAuditExecute.ExecuteAuditorName = txtBoxAuditorName.Text.ToString();

                    crudObject.UpdateExecuteAuditorEmp(vAuditExecute);
                }
                i++;
            }
        }

        //啟動簽核寄發Email通知,產生帳號資料
        protected void BtnStartAuditor_Click(object sender, EventArgs e)
        {
            string msg = "";
            CRUDObject crudObject = new CRUDObject();

            //判斷第一順位簽核人員資料
            GridViewRow row = (GridViewRow)GVAuditExecute.Rows[0];
            //TextBox strExecuteSn = (TextBox)row.FindControl("TextBoxAuditorSn");
            TextBox txtBoxAuditorSnEmpId = (TextBox)row.FindControl("TextBoxAuditorSnEmpId");
            //TextBox txtBoxAuditorEmail = (TextBox)row.FindControl("TextBoxAuditorEmail");
            //TextBox txtBoxAuditorName = (TextBox)row.FindControl("TextBoxAuditorName");
            Session["NextAcctAuditorSnEmpId"] = txtBoxAuditorSnEmpId.Text.ToString();
            //Session["NextAcctAuditorEmail"] = txtBoxAuditorEmail.Text.ToString();
            //Session["NextAcctAuditorName"] = txtBoxAuditorName.Text.ToString();
            //Session["NextAcctRoleName"] = "承辦人員";

            //寫入第一位簽核者(都是校內員工） Login帳號 及寄信通知
            if (Session["NextAcctAuditorSnEmpId"] != null && Session["AppSn"] != null)
            {
                int appSn = Convert.ToInt32(Session["AppSn"].ToString());
                VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(appSn); //vApplyAudit.AppSn 
                Boolean isTaipeiUniversity = true;
                if (vAuditExecuteNextOne != null)
                {
                    if ((vAuditExecuteNextOne.ExecuteStage.Equals("6") && vAuditExecuteNextOne.ExecuteStep.Equals("1")))
                    {
                        isTaipeiUniversity = false;
                    }
                    //Session["NextAcctAuditorSnEmpId"] = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                    //Session["NextAcctAuditorEmail"] = vAuditExecuteNextOne.ExecuteAuditorEmail;
                    //Session["NextAcctAuditorName"] = vAuditExecuteNextOne.ExecuteAuditorName;
                    //Session["NextAcctRoleName"] = vAuditExecuteNextOne.ExecuteRoleName;

                    if (GenerateAccountAndSendEmail(isTaipeiUniversity, vAuditExecuteNextOne))
                    {
                        msg = " 啟動成功, 送審『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核!!";
                    }

                }
                else
                {
                    msg = " 無法送審下一階段,請洽資訊人員處理!!";
                }

            }
            else
            {
                msg = "啟動失敗,請確認第一順位的簽核人員是否完成設定!";
            }

            if (msg != "")
                Response.Write("<Script language='JavaScript'>alert('" + msg + "');window.location.assign(window.location.href);</Script>");
        }


        //審畢,進下一階段人員審核 Session["AuditorSnEmpId"].ToString();GenerateAccoutnAndSendEmail
        protected void BtnAuditPass_Click(object sender, EventArgs e)
        {
            string msg = "";


            if (PassExecuteCommentsA.Text.Length > 3000)
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('評議內容請勿超出3000字!');", true);
            else
            {
                Boolean isReturn = false; //退回系承辦
                if (Session["AppUnitNo"] == null || Session["AppUnitNo"].ToString().Equals(""))
                {
                    ShowSessionTimeOut();
                }
                String appUnitNo = Session["AppUnitNo"].ToString();
                vAuditExecute = new VAuditExecute();
                vAuditExecute.AppSn = Convert.ToInt32(Session["AppSn"].ToString());
                vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                vAuditExecute.ExecuteStatus = false;
                vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆 Submit時被清掉 再重新讀取一次


                if ((vAuditExecute.ExecuteStage.Equals("1") && vAuditExecute.ExecuteStep.Equals("2")) ||
                (vAuditExecute.ExecuteStage.Equals("2") && vAuditExecute.ExecuteStep.Equals("1")) ||
                (vAuditExecute.ExecuteStage.Equals("3") && vAuditExecute.ExecuteStep.Equals("1")) ||
                (vAuditExecute.ExecuteStage.Equals("4") && vAuditExecute.ExecuteStep.Equals("1"))
                )
                {
                    String comments = PassExecuteCommentsA.Text.ToString();
                    if ((comments.Equals("") || comments.Length == 0) && (DDExecutePass.Text.Equals("1") || (DDExecutePass.Text.Equals("2"))))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('請務必填寫教評決議內容，才能送審下一關!!');", true);
                        return;
                    }
                    String returnreason = ReturnReason.Text.ToString();
                    if ((returnreason.Equals("") || returnreason.Length == 0) && (DDExecutePass.Text.Equals("3")))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "alert('請務必填退回原因，才能退回本人!!');", true);
                        return;
                    }
                }

                if (vAuditExecute.ExecuteStage.Equals("4") && vAuditExecute.ExecuteStep.Equals("4"))//因為多副人資長那關，因此現在都改以4/4 而不是舊有的4/3
                {
                    //判斷外審委員是否設定完畢
                    if (crudObject.IsAuditExecuteOtherAuditorFilled(vAuditExecute.AppSn))
                    {
                        vAuditExecute.ExecutePass = DDExecutePass1.SelectedValue.ToString();
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "LoginFail", "alert('請確認申請者的外審委員設定完畢，才能啟動外審流程!!');", true);
                        return;
                    }
                }
                else
                {
                    vAuditExecute.ExecutePass = DDExecutePass.SelectedValue.ToString();
                    if (vAuditExecute.ExecutePass.Equals("3"))
                    {
                        isReturn = true;
                    }
                    vAuditExecute.ExecuteCommentsA = PassExecuteCommentsA.Text.ToString();
                    vAuditExecute.ExecuteReturnReason = ReturnReason.Text.ToString();
                    vAuditExecute.ExecuteTeachingScore = TeachingScore.Text.ToString();
                    vAuditExecute.ExecuteServiceScore = ServiceScore.Text.ToString();
                }

                bool executePass = this.crudObject.UpdateExecuteAuditDataFinish(vAuditExecute);
                if (executePass)
                {
                    switch (vAuditExecute.ExecutePass)
                    {
                        case "0":
                            msg = "您尚未完成簽審,請盡速完成!!";
                            break;
                        case "1":
                            msg = "簽審通過!!!";
                            GoNextAuditExecuteStageStep();
                            break;
                        case "2":
                            msg = "簽審不通過!!!";
                            VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                            vApplyAuditUpdate.AppSn = vAuditExecute.AppSn;
                            vApplyAuditUpdate.AppStage = "8"; //完成
                            vApplyAuditUpdate.AppStep = "0"; //完成
                            vApplyAuditUpdate.AppStatus = true; //完成
                            if (crudObject.UpdateApplyAuditStageStepStatus(vApplyAuditUpdate))
                            {
                                msg = "申請案件已結束!!";
                            }
                            break;
                        case "3":
                            if (isReturn)
                            {
                                //    MessageLabelAll.Text += "退回系承辦!!";//1.處理狀態 2.Email給退回本人
                                //}
                                //else
                                //{
                                msg = "退回本人!!";//1.處理狀態 2.Email給退回本人
                            }
                            ReturnAuditExecuteStageStep(isReturn);
                            break;
                    }
                }
                else
                {
                    msg = "簽審動作失敗,請洽資訊人員!!";
                }
            }
            if (msg != "")
                Response.Write("<Script language='JavaScript'>alert('" + msg + "');window.location.assign(window.location.href);</Script>");
        }


        protected string ProcessCheckedData(string selectedData, string other)
        {
            //先將other 
            selectedData += other;
            //判斷最後一字元是,就刪除
            if (selectedData.EndsWith(","))
            {
                selectedData = selectedData.Substring(0, selectedData.Length - 1);
            }
            return selectedData;
        }

        //共用送下一階段模組
        private void GoNextAuditExecuteStageStep()
        {
            string msg = "";
            int appSn = Convert.ToInt32(Session["AppSn"].ToString());
            VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(appSn);
            if (vAuditExecuteNextOne == null)
            {
                //整份單據完全審完
                //更新 ApplyAudit 中的 審核Stage與Step的狀態
                VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                vApplyAuditUpdate.AppSn = appSn;
                vApplyAuditUpdate.AppStage = "7"; //完成
                vApplyAuditUpdate.AppStep = "1"; //完成
                vApplyAuditUpdate.AppStatus = true; //完成
                if (crudObject.UpdateApplyAuditStageStepStatus(vApplyAuditUpdate))
                {
                    msg = " 此申請案件已審完畢!!";
                }


            }
            else
            {
                Boolean isTaipeiUniversity = true;
                if (vAuditExecuteNextOne != null)
                {
                    if (vAuditExecuteNextOne.ExecuteStage.Equals("6") && vAuditExecuteNextOne.ExecuteStep.Equals("1"))
                    {
                        isTaipeiUniversity = false;
                    }

                    //若是外審的
                    if (!isTaipeiUniversity)
                    {
                        ArrayList arrayList = this.crudObject.GetExecuteAuditorOutter(appSn, vAuditExecuteNextOne.ExecuteStage, vAuditExecuteNextOne.ExecuteStep);
                        foreach (VAuditExecute vAuditExecuteOutter in arrayList)
                        {
                            if (!vAuditExecuteOutter.ExecuteAuditorEmail.Equals(""))
                            {
                                if (GenerateAccountAndSendEmail(isTaipeiUniversity, vAuditExecuteOutter))
                                {
                                    msg = " 送審下一階段『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核!!\r\n";
                                }
                            }
                        }

                    }
                    else
                    {
                        //Session["NextAcctAuditorSnEmpId"] = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                        //Session["NextAcctAuditorEmail"] = vAuditExecuteNextOne.ExecuteAuditorEmail;
                        //Session["NextAcctAuditorName"] = vAuditExecuteNextOne.ExecuteAuditorName;
                        //Session["NextAcctRoleName"] = vAuditExecuteNextOne.ExecuteRoleName;
                        if (GenerateAccountAndSendEmail(isTaipeiUniversity, vAuditExecuteNextOne))
                        {
                            if (vAuditExecuteNextOne.ExecuteStage.Equals("2") && vAuditExecuteNextOne.ExecuteStep.Equals("3"))
                            {
                                msg = " 送給本人『" + vAuditExecuteNextOne.ExecuteRoleName + "』!!\r\n";
                            }
                            else
                            {
                                msg = " 送審下一階段『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核!!\r\n";
                            }
                        }

                    }
                }
                else
                {
                    msg = " 無法送審下一階段,請洽資訊人員處理!!";
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        //退回本人Stage = "0" Step = "0" 退回系所承辦Stage = "2" Step = "1" 此段流程為固定[注意]
        //20160801 退回一率退回本人
        private void ReturnAuditExecuteStageStep(Boolean isReturn)
        {
            int appSn = Convert.ToInt32(Session["AppSn"].ToString());
            //更新 ApplyAudit 中的 審核Stage與Step的狀態
            VApplyAudit vApplyAuditUpdate = new VApplyAudit();
            vApplyAuditUpdate.AppSn = appSn;
            vApplyAuditUpdate = crudObject.GetApplyAuditObjByAppSn(appSn);

            VAuditExecute vAuditExecute = new VAuditExecute();
            VAuditExecute vAuditExecuteReturn = new VAuditExecute();
            vAuditExecute.AppSn = appSn;
            vAuditExecute.ExecuteStage = vApplyAuditUpdate.AppStage;
            vAuditExecute.ExecuteStep = vApplyAuditUpdate.AppStep;
            vAuditExecute.ExecutePass = "3";
            vAuditExecute.ExecuteStatus = false;

            VAuditExecute vae = new VAuditExecute();
            //更新在此之前的ExecuteStatus 改為false 2016.08.04 從哪退回，再次送出時直接送至該關
            //DataTable dt = crudObject.GetAllAuditStageByEmpSn(appSn);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    vae.AppSn = appSn;
            //    vae.ExecuteCommentsA = dt.Rows[i][vae.strExecuteCommentsA].ToString();
            //    vae.ExecuteReturnReason = dt.Rows[i][vae.strExecuteReturnReason].ToString();
            //    vae.ExecuteStage = dt.Rows[i][vae.strExecuteStage].ToString();
            //    vae.ExecuteStep = dt.Rows[i][vae.strExecuteStep].ToString();
            //    if (Convert.ToInt16(vae.ExecuteStage) <= Convert.ToInt16(vAuditExecute.ExecuteStage))
            //    {
            //        vae.ExecutePass = "1";
            //        vae.ExecuteStatus = false;
            //        crudObject.UpdateAuditExecuteStatus(vae);
            //    }
            //}
            if (!ReturnReason.Text.ToString().Equals(""))
            {
                vAuditExecute.ExecuteReturnReason = ReturnReason.Text.ToString();
            }
            crudObject.UpdateAuditExecuteStatus(vAuditExecute);

            if (isReturn)
            {
                //vApplyAuditUpdate.AppStage = "2"; //退回系承辦
                //vApplyAuditUpdate.AppStep = "1"; //退回系承辦
                vApplyAuditUpdate.AppStatus = false;
            }
            else
            {
                //vApplyAuditUpdate.AppStage = "0"; //退回本人
                //vApplyAuditUpdate.AppStep = "0"; //退回本人
                vApplyAuditUpdate.AppStatus = false;
            }

            if (crudObject.UpdateApplyAuditStageStepStatus(vApplyAuditUpdate))
            {
                VEmployeeBase vEmployeeBase = new VEmployeeBase();
                vEmployeeBase = crudObject.GetEmpBsaseObjByEmpSn("" + vApplyAuditUpdate.EmpSn);
                //Email  
                //VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(appSn);
                //if (isReturn)
                //{
                //    //取得系所承辦員的名字 email
                //    vAuditExecuteReturn = crudObject.GetAuditExecuteByAppSn(appSn);

                //    ReturnAuditSendEmail(vAuditExecuteReturn, ReturnReason.Text.ToString(), appSn);
                //}
                //else

                //{
                //追加外審寄信阻擋
                if (vAuditExecute.ExecuteStage != null && !vAuditExecute.ExecuteStage.Equals("6"))
                {
                    ReturnAuditSendEmail(vEmployeeBase, ReturnReason.Text.ToString());
                }
                //}

            }

        }

        private string GetSelectedStrengthData(CheckBoxList ckBxStrengths)
        {
            Response.Write("selected items --> " + selStrList);
            return selStrList;
        }


        private string GetSelectedWeaknessData(CheckBoxList ckBxStrengths)
        {
            Response.Write("selected items --> " + selStrList);
            return selWeakList;
        }

        protected void EmpSex_SelectedIndexChanged(object sender, EventArgs e)
        {
        }



        void CheckSBoxList_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxList cbxList = (CheckBoxList)sender;
            selStrList = "";
            foreach (ListItem listItem in cbxList.Items)
            {
                if (listItem.Selected)
                {
                    selStrList += listItem.Value + ",";
                }
            }
            Response.Write("選取S:" + selStrList);
        }





        public Boolean GenerateAccountAndSendEmail(Boolean isTaipeiUniversity, VAuditExecute vAuditExecuteNextOne)
        {
            crudObject = new CRUDObject();

            Mail mail = new Mail();
            //Email
            VSendEmail vSendEmail = new VSendEmail();
            vSendEmail.MailToAccount = vAuditExecuteNextOne.ExecuteAuditorEmail;
            vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";


            try
            {
                //若是系教評通過，通知本人填寫資料，若是本人填寫完畢送出後繼續院教評
                string strTail = "如有任何問題, 請與我聯絡, 謝謝~<br>" +
                "******************************************************<br>" +
                "　[人資處聯絡人]<br>" +
                "　林怡慧(電話：02-27361661 分機: 2028; cmbyhlin@tmu.edu.tw)<br>" +
                "　劉伊芝(電話：02-27361661 分機: 2066; yijhih@tmu.edu.tw )<br>" +
                "****************************************************** <br>";

                //先確認是否為校內人員
                string empIdno = "";
                int acctSn = 0;
                empIdno = crudObject.GetVEmployeeFromTmuHrByEmail(vAuditExecuteNextOne.ExecuteAuditorEmail);

                //Email抓取申請者資料顯示在Email中
                VApplyerData vApplyerData = crudObject.GetApplyDataForEmail(vAuditExecuteNextOne.AppSn);

                if (empIdno != null)
                {
                    vSendEmail.MailSubject = "「教師聘任升等作業系統」有申請文件--請盡速簽核";
                    vSendEmail.ToAccountName = vAuditExecuteNextOne.ExecuteRoleName + " " + vAuditExecuteNextOne.ExecuteAuditorName;
                    //在AccountForManage是否存在
                    acctSn = crudObject.GetAccountForManageAcctSn(vAuditExecuteNextOne.ExecuteAuditorEmail);
                    VAccountForManage vAccountForManage = new VAccountForManage();
                    if (acctSn == 0)
                    {
                        //新增一筆校內管理者資料 權限為A 僅有稽核權限

                        vAccountForManage.AcctEmail = vAuditExecuteNextOne.ExecuteAuditorEmail;
                        vAccountForManage.AcctPassword = "123456";
                        vAccountForManage.AcctRole = "A";
                        vAccountForManage.AcctEmpId = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                        vAccountForManage.AcctStatus = true;
                        crudObject.Insert(vAccountForManage);
                    }
                    else
                    {
                        vAccountForManage = crudObject.GetAccountForManage(vAuditExecuteNextOne.ExecuteAuditorEmail);
                    }
                    string strTableData = "<table border=1 cellspacing=0>" +
                    "<tbody><tr>" +
                    "<th>聘任單位</th><th >姓名</th><th >專兼任別</th><th >應聘等級</th><th >審查性質</th><th >新聘升等</th><th >申請狀態</th></tr>" +
                    "<tr><td>" + vApplyerData.UntNameFull + "</td><td>" + vApplyerData.EmpNameCN + "</td><td>" + vApplyerData.JobAttrName + "</td><td>" + vApplyerData.JobTitleName + "</td><td>" + vApplyerData.AttributeName + "</td><td>" + vApplyerData.KindName + "</td><td>" + vApplyerData.AuditProgressName + "</td></tr></tbody></table>";

                    vSendEmail.MailContent = "<br> 「教師聘任升等作業系統」需您的核准!<br><font color=red>請再完成您的審核</font>,申請者才能進入下一階段的審核.<br> " + strTableData + " <br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入審核</a>  您的帳號:" + vAccountForManage.AcctEmail + " 密碼:校內使用的密碼 <br/><br><br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=4>臺北醫學大學人力資源處　敬啟</font><br>" + strTail;
                }
                else
                {
                    if (vAuditExecuteNextOne.ExecuteStage.Equals("2") && vAuditExecuteNextOne.ExecuteStep.Equals("3"))
                    {
                        //狀態異動
                        if (vApplyAudit == null) vApplyAudit = new VApplyAudit();
                        vApplyAudit.AppSn = vAuditExecuteNextOne.AppSn;
                        vApplyAudit.AppStatus = false;
                        vApplyAudit.AppStage = vAuditExecuteNextOne.ExecuteStage;
                        vApplyAudit.AppStep = vAuditExecuteNextOne.ExecuteStep;
                        crudObject.UpdateApplyAuditStageStepStatus(vApplyAudit);
                        vSendEmail.MailSubject = "「教師聘任升等作業系統」恭喜您已通過系所審核--請盡速詳填資料";
                        vSendEmail.ToAccountName = vAuditExecuteNextOne.ExecuteRoleName + " " + vAuditExecuteNextOne.ExecuteAuditorName;

                        String strTableData = "<table border=1 cellspacing=0>" +
                        "<tbody><tr>" +
                        "<th>聘任單位</th><th >姓名</th><th >專兼任別</th><th >應聘等級</th><th >審查性質</th><th >新聘升等</th><th >申請狀態</th></tr>" +
                        "<tr><td>" + vApplyerData.UntNameFull + "</td><td>" + vApplyerData.EmpNameCN + "</td><td>" + vApplyerData.JobAttrName + "</td><td>" + vApplyerData.JobTitleName + "</td><td>" + vApplyerData.AttributeName + "</td><td>" + vApplyerData.KindName + "</td><td>" + vApplyerData.AuditProgressName + "</td></tr></tbody></table>";
                        vSendEmail.MailContent = "<br> 「教師聘任升等作業系統」恭喜您已通過系所審核，已將申請單退回詳填!<br><font color=red>請盡速填完畢您的資料</font>,才能進入下一階段的審核.<br> " + strTableData + " <br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/Default.aspx\">按此進入填寫</a> <br/><br><br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=4>臺北醫學大學人力資源處　敬啟</font><br>" + strTail;

                    }
                    else
                    {
                        //外審寄信
                        //vSendEmail.MailSubject = "徵詢" + vAuditExecuteNextOne.ExecuteAuditorName + "教授是否同意受理審查北醫大" + vApplyerData.EmpNameCN + vApplyerData.KindName + "新聘老師論文資料";
                        //vSendEmail.ToAccountName = "<font color=purple><b>" + vAuditExecuteNextOne.ExecuteAuditorName + "</b>教授道鑒：</font><br/>";
                        //string LawTxt = "";
                        //
                        //LawTxt = "第二條 第";
                        //
                        //vApplyAudit = crudObject.GetApplyAuditObj(appSn);
                        ////法規第幾項
                        //String[] num = { "零", "一", "二", "三", "四", "五" };
                        //if (!vApplyAudit.AppJobTitleNo.ToString().Equals("") && !vApplyAudit.AppJobTitleNo.ToString().Equals("請選擇"))
                        //{
                        //
                        //    LawTxt = num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)))] + "項 第";
                        //}
                        //
                        ////載入法規依據第幾款
                        //LawTxt += num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)] + "款";
                        //
                        ////取得密碼
                        //GeneratorPwd generatorPwd = new GeneratorPwd();
                        //generatorPwd.Execute();
                        //string newPwd = generatorPwd.GetPwd();
                        //
                        ////校外審核者一律新增 新的帳號 一次僅審核一位
                        //VAccountForAudit vAccountForAudit = new VAccountForAudit();
                        //vAccountForAudit.AcctAppSn = Session["AppSn"].ToString();
                        //vAccountForAudit.AcctAuditorSnEmpId = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                        //vAccountForAudit.AcctEmail = vAuditExecuteNextOne.ExecuteAuditorEmail;
                        //vAccountForAudit.AcctPassword = newPwd;
                        //vAccountForAudit.AcctStatus = false;
                        //crudObject.Insert(vAccountForAudit);
                        //string strTableData = "<table border=1 cellspacing=0>" +
                        //"<tbody><tr>" +
                        //"<th bgcolor=yellow>聘任單位</th><th bgcolor=yellow>姓名</th><th bgcolor=yellow>專兼任別</th><th bgcolor=yellow>應聘等級</th><th bgcolor=yellow>審查性質</th><th bgcolor=yellow>新聘升等</th><th bgcolor=yellow>學歷</th><th bgcolor=yellow>經歷</th><th bgcolor=yellow>代表著作名稱</th></tr>" +
                        //"<tr><td>" + vApplyerData.UntNameFull + "</td><td>" + vApplyerData.EmpNameCN + "</td><td>" + vApplyerData.JobAttrName + "</td><td>" + vApplyerData.JobTitleName + "</td><td>" + vApplyerData.AttributeName + "</td><td>" + vApplyerData.KindName + "</td><td>" + EmailEdu.Text.ToString() + "</td><td>" + EmailExp.Text.ToString() + "</td><td>" + PassAuditAppPublication.Text.ToString() + "</td></tr>" +
                        //"<tr><td colspan>適用條款：" + LawTxt + vApplyerData.LawContent + "</td></tr></tbody></table></br>";
                        //
                        //vSendEmail.MailContent = "<font color=purple><b>久仰您在學術研究卓越成就，經本校教師新聘/升等論文外審小組推選　鈞長擔任「論文著作審查委員」。</font><font color=black>茲檢附送審人代表著作名稱</b></font><br> " + strTableData + "<font color=red size=10><b>敬請於收信後三天內回覆是否同意受理審查。</b></font><br><br><font color=purple>本審查案截止日期為：收件後兩周內！ <br>如蒙受理，<a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入同意受理審查</a>  您的帳號:" + vAccountForAudit.AcctEmail + " 密碼:" + vAccountForAudit.AcctPassword + " <br>因為您的協助審閱，有助提升本校教師資格審查品質。在論文外審流程安排上，如有不適當或待改進之處，尚祈時賜針砭，以臻完善。</br>耑此　敬頌</br>道祺！</font><br><br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=4>臺北醫學大學人力資源處　敬啟</font><br>" + strTail;
                    }
                }

                //更新 ApplyAudit 中的 審核Stage與Step的狀態
                VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                vApplyAuditUpdate.AppSn = vAuditExecuteNextOne.AppSn;
                vApplyAuditUpdate.AppStage = vAuditExecuteNextOne.ExecuteStage;
                vApplyAuditUpdate.AppStep = vAuditExecuteNextOne.ExecuteStep;
                vApplyAuditUpdate.AppStatus = true;
                crudObject.UpdateApplyAuditStageStep(vApplyAuditUpdate);

                //追加外審寄信阻擋
                if (vAuditExecuteNextOne.ExecuteStage != null && !vAuditExecuteNextOne.ExecuteStage.Equals("6"))
                {
                    //寄發Email通知
                    return (Boolean)mail.SendEmail(vSendEmail);
                }
                else
                    return true;
                //return true;

            }
            catch (Exception e)
            {
                return false;
            }


        }


        public Boolean ReturnAuditSendEmail(VEmployeeBase vEmployeeBase, String reason)
        {
            //撈取承辦人員的email 
            string AcctAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString();
            DataTable dt = crudObject.GetEmpNameEmail(AcctAuditorSnEmpId);
            string sEmail = "";
            if (dt.Rows[0][1] != null) sEmail = dt.Rows[0][1].ToString();

            string strTail = "如有任何問題, 請與我聯絡, 謝謝~<br>" +
                                   "******************************************************<br>" +
                                   "　" + Session["AcctRoleName"].ToString() + "  " + Session["AcctAuditorSnEmpName"].ToString() + "<br>" +
                                    "　臺北醫學大學 <br>" +
                                    "　電子信箱：" + sEmail + "<br>" +
                                    "****************************************************** <br>";
            try
            {
                //寄發Email通知
                Mail mail = new Mail();
                VSendEmail vSendEmail = new VSendEmail();

                //MailMessage mailObj = new MailMessage();
                vSendEmail.MailFromAccount = sEmail;
                vSendEmail.MailToAccount = vEmployeeBase.EmpEmail + ";up_group@tmu.edu.tw";
                vSendEmail.MailSubject = "台北醫學大學新聘申請文件--退回補件通知";
                vSendEmail.ToAccountName = "申請者 " + vEmployeeBase.EmpNameCN;
                vSendEmail.MailContent = "<br><br> 系所申請退回補件原因：<br><font color=red>" + reason + "，煩請請再次確認!</font><br><br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/Default.aspx\">按此進入補件</a>  <br/><br><br><br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<font size=4>臺北醫學大學人力資源處　敬啟</font><br>" + strTail;
                return (Boolean)mail.SendEmail(vSendEmail);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Boolean ReturnAuditSendEmail(VAuditExecute vAuditExecute, String reason, int appSn)
        {

            //Email抓取申請者資料顯示在Email中
            VApplyerData vApplyerData = crudObject.GetApplyDataForEmail(appSn);


            string strTableData = "<table border=1 cellspacing=0>" +
                "<tbody><tr>" +
                "<th bgcolor=yellow>聘任單位</th><th bgcolor=yellow>姓名</th><th bgcolor=yellow>專兼任別</th><th bgcolor=yellow>應聘等級</th><th bgcolor=yellow>審查性質</th><th bgcolor=yellow>新聘升等</th><th bgcolor=yellow>學歷</th><th bgcolor=yellow>經歷</th><th bgcolor=yellow>代表著作名稱</th></tr>" +
                "<tr><td>" + vApplyerData.UntNameFull + "</td><td>" + vApplyerData.EmpNameCN + "</td><td>" + vApplyerData.JobAttrName + "</td><td>" + vApplyerData.JobTitleName + "</td><td>" + vApplyerData.AttributeName + "</td><td>" + vApplyerData.KindName + "</td><td>" + EmailEdu.Text.ToString() + "</td><td>" + EmailExp.Text.ToString() + "</td><td>" + PassAuditAppPublication.Text.ToString() + "</td></tr>" +
                "</tbody></table></br>";

            //撈取承辦人員的email 
            string AcctAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString();
            DataTable dt = crudObject.GetEmpNameEmail(AcctAuditorSnEmpId);
            string sEmail = "";
            if (dt.Rows[0][1] != null) sEmail = dt.Rows[0][1].ToString();

            string strTail = "如有任何問題, 請與我聯絡, 謝謝~<br>" +
                                   "******************************************************<br>" +
                                   "　" + Session["AcctRoleName"].ToString() + "  " + Session["AcctAuditorSnEmpName"].ToString() + "<br>" +
                                    "　臺北醫學大學 <br>" +
                                    "　電子信箱：" + sEmail + "<br>" +
                                    "****************************************************** <br>";
            try
            {

                //寄發Email通知
                Mail mail = new Mail();
                VSendEmail vSendEmail = new VSendEmail();

                //MailMessage mailObj = new MailMessage();
                vSendEmail.MailFromAccount = sEmail;
                vSendEmail.MailToAccount = vAuditExecute.ExecuteAuditorEmail + ""; //;up_group@tmu.edu.tw
                vSendEmail.MailSubject = "台北醫學大學教師新聘升等申請文件--退回系所承辦通知";
                vSendEmail.ToAccountName = "系所承辦 " + vAuditExecute.ExecuteAuditorName;
                vSendEmail.MailContent = "<br>" + strTableData + "<br> 申請退回系所承辦原因：<br><font color=red>" + reason + "，煩請確認!</font><br><br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/Default.aspx\">按此進入處理</a>  <br/><br><br><br><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font><br>" + strTail;
                return (Boolean)mail.SendEmail(vSendEmail);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected String getHyperLink(string strFileName)
        {
            String strHyperLink = "";
            //正式機位置
            //String openFile = Global.FileUpPath + Request.QueryString["EmpSn"].ToString() + "/" + strFileName;
            //測試機位置
            //String openFile = "../DocuFile/HRApplyDoc/" + Session["EmpSn"].ToString() + "/" + strFileName;
            //測試機直接連線正式機檔案用連結
            String openFile = "http://hr2sys.tmu.edu.tw/DocuFile/HRApplyDoc/" + Request.QueryString["EmpSn"].ToString() + "/" + strFileName;
            if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
            {
                strHyperLink = "javascript:void(window.open('" + Global.FileUpPath + "NoUploadFile.pdf','_blank','height=800','width=800') )";
            }
            else
            {
                if (openFile != "&nbsp;" && openFile.IndexOf("_" + EmpNameCN.Text.Trim()) > 0)
                    openFile = openFile.Substring(0, openFile.IndexOf("_" + EmpNameCN.Text.Trim())) + ".pdf";
                strHyperLink = "javascript:void(window.open('" + openFile + "','_blank','location=no,height=800','width=800') )";
            }
            return strHyperLink;
        }

        public void DownloadPdf_OnClick(object sender, EventArgs e)
        {
            string serverIPAddress = "127.0.0.1";
            uint serverPortNumber = 40001;
            string serverPassword = String.Empty;
            string urlToConvert = "http://www.google.com";



        }
        protected void ButnDeleteAuditor_Click(object sender, EventArgs e)
        {
            String appsn = Session["AppSn"].ToString();
            if (appsn != null && appsn != "")
                crudObject.DeleteExecuteAuditorEmp(appsn);
        }
    }
}
