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

namespace ApplyPromote
{
    public partial class OuterAudit : PageBaseManage
    {
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

        GetSettings getSettings;

        string empSn = "";

        string selStrList = "";

        string selWeakList = "";

        int appSn = 0;

        //著作
        static string AuditPublicationMemo = "說明：<br/>1.審查意見請勿僅以送審人投稿期刊之等級、排名、Impact Factor等項目為審查基準。<br/>2.審查意見請分別就代表作及參考作具體審查及撰寫審查意見，並請勾選優缺點。<br/>3.前述意見建議以條列方式敘述。<br/>4.本案審定結果如為不通過，審查意見得提供送審人作為行政處分之依據，併予敘明。";
        static string AuditDegreeMemo= "說明：<br/>1.審查意見請分別就代表作及參考作具體審查及撰寫審查意見，並請勾選優缺點。<br/>2.前述意見建議以條列方式敘述。<br/>3.本案審定結果如為不通過，審查意見得提供送審人作為行政處分之依據，併予敘明。";

        //頁籤切換
        public enum MSearchType
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewAuditExecuting = 1

        }

        public enum OSearchType
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewAuditExecuting = 1
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
            if (Session["OuterName"] != null)
            {
                OuterName.Text = "外審委員: " + Session["OuterName"].ToString();
            }
            if (Request.QueryString["OuterName"] != null)
            {
                OuterName.Text = "外審委員: " + Request.QueryString["OuterName"].ToString();
                if(Request.QueryString["EDate"]!=null)
                    OuterDate.Text = "          審畢日期:" + Request.QueryString["EDate"].ToString();
            }
            if (Request.QueryString["SnEmpId"] != null)
                Session["AcctAuditorSnEmpId"] = Request.QueryString["SnEmpId"].ToString();
            Session["Kind"] = "1";

            GetSettings setting = new GetSettings();
            Session["AppYear"] = setting.LoadYear.ToString().Trim();
            Session["AppSemester"] = setting.LoadSemester.ToString().Trim();

            crudObject = new CRUDObject();
            vEmp = crudObject.GetEmpBsaseObjByEmpSn(Request.QueryString["EmpSn"].ToString());
            //Session["AppSn"] = Request.QueryString["AppSn"].ToString();
            if(Session["AppSn"]!=null)
                appSn = Convert.ToInt32(Session["AppSn"].ToString());
            if (Request.QueryString["AppSn"] != null)
            {
                appSn = Convert.ToInt32(Request.QueryString["AppSn"].ToString());
                Session["AppSn"] = Request.QueryString["AppSn"].ToString();
            }
            if (Request.QueryString["AcctAuditorSnEmpId"] != null)
                Session["AcctAuditorSnEmpId"] = Request.QueryString["AcctAuditorSnEmpId"].ToString();

            if (!this.IsPostBack && (vEmp != null || vAuditExecute != null))
            {

                //載入ApplyAudit共用延伸資料                   
                vApplyAudit = crudObject.GetApplyAuditObj(appSn);
                vAuditExecute = new VAuditExecute();
                vAuditExecute.AppSn = vApplyAudit.AppSn;
                vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                vAuditExecute.ExecuteStatus = false;

                if( Request.QueryString["SnEmpId"] != null)
                    vAuditExecute = crudObject.GetExecuteAuditorDataHR(vAuditExecute); //只取未完成的第一筆
                else
                    vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆
            }
            //StrengthsWeaknesses 一定都要顯示
            if (vAuditExecute == null )
            {
                //載入ApplyAudit共用延伸資料                   
                vApplyAudit = crudObject.GetApplyAuditObj(appSn);
                vAuditExecute = new VAuditExecute();

                vAuditExecute.AppSn = vApplyAudit.AppSn;
                vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                vAuditExecute.ExecuteStatus = false;


                if (Request.QueryString["SnEmpId"] == null)
                    vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute);
                else
                    vAuditExecute = crudObject.GetExecuteAuditorDataHR(vAuditExecute);
                if (vAuditExecute != null)
                {
                    selStrList = vAuditExecute.ExecuteStrengths;
                    selWeakList = vAuditExecute.ExecuteWeaknesses;

                    Session["OuterKind"] = vApplyAudit.AppKindNo;
                    Session["OuterAttribute"] = vApplyAudit.AppAttributeNo;

                    AddStrengthControls(vAuditExecute.ExecuteStrengths);
                    AddWeaknessControls(vAuditExecute.ExecuteWeaknesses);
                }
                else
                {
                    Response.Write("<script>alert('您該帳號需審核資料已完成!');location.href='AuditLogin.aspx';</script>");
                }
            }
            else
            {
                selStrList = vAuditExecute.ExecuteStrengths;
                selWeakList = vAuditExecute.ExecuteWeaknesses;

                Session["OuterKind"] = vApplyAudit.AppKindNo;
                Session["OuterAttribute"] = vApplyAudit.AppAttributeNo;

                AddStrengthControls(vAuditExecute.ExecuteStrengths);
                AddWeaknessControls(vAuditExecute.ExecuteWeaknesses);
            }
            //控制應顯示資料(新聘)
            if (vApplyAudit.AppKindNo.Equals(chkApply))
            {

                //1.已具部定教育資格
                if (vApplyAudit.AppAttributeNo.ToString().Equals(chkTeacher))
                {
                    AppPPMTableRow.Visible = true;
                    AppTeacherCaTableRow.Visible = false;
                    AuditExecuteMemo.Text = AuditPublicationMemo;
                    AuditExecuteMemo.Attributes.Add("text-align","left");

                } //2.學位                
                else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkDegree))
                {
                    OtherTeachingTableRow.Visible = false;
                    OtherServiceTableRow.Visible = false;
                    AppTeacherCaTableRow.Visible = false;
                    AppDegreeThesisTableRow.Visible = true;
                    AppThesisOral.Visible = false;
                    AuditExecuteMemo.Text = AuditDegreeMemo;

                } //3.著作
                else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkPublication))
                {
                    AppPPMTableRow.Visible = true;
                    AppTeacherCaTableRow.Visible = false;
                    OtherServiceTableRow.Visible = false;
                    OtherTeachingTableRow.Visible = false;
                    AuditExecuteMemo.Text = AuditPublicationMemo;
                }
                else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkClinical))
                {
                    AppDrCaTableRow.Visible = true;
                    AuditExecuteMemo.Text = AuditPublicationMemo;
                }
            }
            //(升等)
            else if (vApplyAudit.AppKindNo.Equals(chkPromote))
            {
                TbRow_RPI.Visible = false;
                if (vApplyAudit.AppAttributeNo.ToString().Equals("1"))
                {
                    OtherServiceTableRow.Visible = true;
                    OtherTeachingTableRow.Visible = true;
                    AppTeacherCaTableRow.Visible = true;
                    RPIDiscountTableRow.Visible = true;
                    AppPPMTableRow.Visible = true;
                    AuditExecuteMemo.Text = AuditPublicationMemo;
                }
                else if (vApplyAudit.AppAttributeNo.ToString().Equals(chkDegree))
                {
                    OtherServiceTableRow.Visible = true;
                    OtherTeachingTableRow.Visible = true;
                    RPIDiscountTableRow.Visible = true;
                    AppDegreeThesisTableRow.Visible = true;
                    AppThesisOral.Visible = false;
                    AuditExecuteMemo.Text = AuditDegreeMemo;
                }
                else if (vApplyAudit.AppAttributeNo.ToString().Equals("3"))
                {
                    OtherServiceTableRow.Visible = true;
                    ExpTableRow.Visible = true;
                    AppPublicationTableRow.Visible = true;
                    AppDrCaTableRow.Visible = true;
                    EmpIdnoTableRow.Visible = false;
                    RecommendUploadTableRow.Visible = false;
                    AuditExecuteMemo.Text = AuditPublicationMemo;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AcctRole"] == null)
                {
                    ShowSessionTimeOut();
                    Response.Redirect("~/AuditLogin.aspx");
                }
                string accountRole = Session["AcctRole"].ToString(); //???
                if (Session["index"] == null || Session["index"].ToString().Equals(""))
                {
                    Session["index"] = "0";
                    MultiViewAudit.ActiveViewIndex = 0;//起始頁面設定
                }
                else
                {
                    MultiViewAudit.ActiveViewIndex = 0;//Convert.ToInt32(Session["index"].ToString())-1;//起始頁面設定，顯示上次動作的頁面
                }

                AuditExecuteCommentsA.Attributes.Add("maxlength", "2500");
                AuditExecuteCommentsA.Attributes.Add("onkeyup", "return ismaxlength(this)");
                AuditExecuteCommentsB.Attributes.Add("maxlength", "2500");
                AuditExecuteCommentsB.Attributes.Add("onkeyup", "return ismaxlength(this)");

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
                    //PassEmpNameCN.Text = EmpNameCN.Text.ToString();
                    AuditEmpNameCN.Text = EmpNameCN.Text.ToString();
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
                        //EmpPhotoUploadFUName.Text = vEmp.EmpPhotoUploadName;
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
                        EmpIdnoTableRow.Visible = false;
                        string openFile = location + vEmp.EmpIdnoUploadName;
                        EmpIdnoHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
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
                        EmpDegreeTableRow.Visible = false;
                        string openFile = location + vEmp.EmpDegreeUploadName;
                        EmpDegreeHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                        EmpDegreeHyperLink.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeHyperLink.Visible = true;
                        EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                    }
                    else
                    {
                        EmpDegreeHyperLink.Visible = false;
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

                    EmpSelfTeachExperience.Text = vEmp.EmpSelfTeachExperience;
                    EmpSelfReach.Text = vEmp.EmpSelfReach;
                    EmpSelfDevelope.Text = vEmp.EmpSelfDevelope;
                    EmpSelfSpecial.Text = vEmp.EmpSelfSpecial;
                    EmpSelfImprove.Text = vEmp.EmpSelfImprove;
                    EmpSelfContribute.Text = vEmp.EmpSelfContribute;
                    EmpSelfCooperate.Text = vEmp.EmpSelfCooperate;
                    EmpSelfTeachPlan.Text = vEmp.EmpSelfTeachPlan;
                    EmpSelfLifePlan.Text = vEmp.EmpSelfLifePlan;


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
                            EmpBornCityTableRow.Visible = false;
                            AppENowJobOrgTableRow.Visible = true;
                            AppENoteTableRow.Visible = false;
                            AppERecommendorsTableRow.Visible = false;
                            AppERecommendYearTableRow.Visible = false;
                            RecommendUploadTableRow.Visible = false;
                            EmpExpertResearchTableRow.Visible = true;
                            EmpPhotoTableRow.Visible = false;
                            EmpIdnoTableRow.Visible = false;
                            EmpDegreeTableRow.Visible = false;

                            if (vApplyAudit.AppAttributeNo.Equals("1"))
                            {
                                AppPPMTableRow.Visible = true;
                            }
                            if (vApplyAudit.AppAttributeNo.Equals("3")) //著作
                            {
                                //RepresentTableRow.Visible = true;
                                AppPPMTableRow.Visible = true;
                            }
                        }
                        else
                        {
                            GVTeachTmuExp.Visible = true;
                            TeachLessonTableRow.Visible = true;
                            RPIDiscountTableRow.Visible = true;
                            if (vApplyAudit.AppAttributeNo.Equals("1")) //著作
                            {
                                //RepresentTableRow.Visible = true;
                                AppPPMTableRow.Visible = true;
                            }
                            if (vApplyAudit.AppAttributeNo.Equals("3")) //臨床教師
                            {
                                AppPublicationTableRow.Visible = true;
                                AppDrCaTableRow.Visible = true;
                                EmpIdnoTableRow.Visible = false;
                                RecommendUploadTableRow.Visible = false;
                                EmpDegreeTableRow.Visible = false;
                            }
                        }
                        if (vApplyAudit.AppAttributeNo.Equals("2")) //學位
                        {
                            AppDegreeThesisTableRow.Visible = true;
                            LBTotalScore.Visible = true;
                            TBAllTotalScore.Visible = true;

                        }

                        //教師資格切結書
                        if (vApplyAudit.AppDeclarationUploadName != null && !vApplyAudit.AppDeclarationUploadName.Equals(""))
                        {
                            //EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                            AppDeclarationUploadCB.Checked = true;
                            string openFile = location + vApplyAudit.AppDeclarationUploadName;
                            AppDeclarationHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                            AppDeclarationHyperLink.Text = vApplyAudit.AppDeclarationUploadName;
                            AppDeclarationHyperLink.Visible = true;
                            AppDeclarationUploadFUName.Text = vApplyAudit.AppDeclarationUploadName;
                        }
                        else
                        {
                            AppDeclarationHyperLink.Visible = false;
                        }


                        //顯示升等途徑 AppWayName
                        AuditWayName.Text = crudObject.GetAuditWayName(vApplyAudit.AppWayNo).Rows[0][0].ToString();



                        //應徵類別
                        AppKindName.Text = crudObject.GetKindName(vApplyAudit.AppKindNo).Rows.Count == 0 ? "" : crudObject.GetKindName(vApplyAudit.AppKindNo).Rows[0][0].ToString();
                        AuditKindName.Text = AppKindName.Text.ToString() + ":";


                        //應徵單位
                        AppUnit.Text = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows.Count == 0 ? "" : crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                        //PassAppUnit.Text = AppUnit.Text.ToString();
                        AuditAppUnitLevel1.Text = crudObject.GetLevel1UnitName(vApplyAudit.AppUnitNo).Rows.Count == 0 ? "" : crudObject.GetLevel1UnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                        AuditAppUnit.Text = AppUnit.Text.ToString();

                        //應徵職稱
                        AppJobTitle.Text = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);


                        //應徵職別
                        AppJobType.Text = crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows.Count == 0 ? "" : crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows[0][0].ToString();
                        //PassAppJobTitle.Text = AppJobTitle.Text.ToString();
                        AuditAppJobTitle.Text = AppJobTitle.Text.ToString();
                        AuditAppJobTitle2.Text = AppJobTitle.Text.ToString();


                        //指定申請類別 著作 學位
                        AppAttributeName.Text = crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows.Count == 0 ? "" : crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows[0][1].ToString();
                        AuditAttributeName.Text = AppAttributeName.Text.ToString();

                        //法規第幾項
                        String[] num = { "零", "一", "二", "三", "四", "五" };
                        ItemLabel.Text = num[Int32.Parse(vApplyAudit.AppAttributeNo)];

                        //載入法規依據第幾條
                        LawNumNoLabel.Text = num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)];

                        //論文代表著作
                        //AppPublication.Text = vApplyAudit.AppPublicationUploadName; 不用
                        //AuditAppPublication.Text = AppPublication.Text.ToString(); 不用

                        //論文積分相關
                        Session["ResearchYear"] = "1";
                        Session["ThesisAccuScore"] = "0";
                        Session["ResearchYear"] = vApplyAudit.AppResearchYear;


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
                                string openFile = location + vAppendPromote.ExpServiceCaUploadName;
                                ExpServiceCaHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                                ExpServiceCaHyperLink.Text = vAppendPromote.ExpServiceCaUploadName;
                                ExpServiceCaHyperLink.Visible = true;
                                ExpServiceCaUploadFUName.Text = vAppendPromote.ExpServiceCaUploadName;
                            }
                            //20180419 追加計算折抵總分
                            int RPIDiscount = 0;

                            if (vAppendPromote.RPIDiscountScore1 != null &&!String.IsNullOrEmpty(vAppendPromote.RPIDiscountScore1) && !vAppendPromote.RPIDiscountScore1.Equals("0"))
                            {
                                RPIDiscountScore1.Checked = true;
                                //RPIDiscount = RPIDiscount + 60;
                            }
                            else
                            {
                                RPIDiscountScore1.Checked = false;
                            }

                            for (int i = 0; i < RPIDiscountScore2.Items.Count; i++)
                            {
                                if (vAppendPromote.RPIDiscountScore2.ToString().Equals(RPIDiscountScore2.Items[i].Value))
                                {
                                    RPIDiscountScore2.Items[i].Selected = true;
                                    //RPIDiscount = RPIDiscount + Convert.ToInt32(RPIDiscountScore2.SelectedValue);
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
                                    //RPIDiscount = RPIDiscount + Convert.ToInt32(RPIDiscountScore3.SelectedValue);
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
                                    //RPIDiscount = RPIDiscount + Convert.ToInt32(RPIDiscountScore4.SelectedValue);
                                }
                                else
                                {
                                    RPIDiscountScore4.Items[i].Selected = false;
                                }
                            }

                            GVThesisCoop.DataSource = thesisCoopService.GetDataTableByAppSn(vApplyAudit.AppSn);
                            GVThesisCoop.DataBind();
                            if(int.TryParse(vAppendPromote.RPIDiscountTotal, out RPIDiscount))
                                this.RPIDiscount.Text = RPIDiscount.ToString() + " 分<br/>";

                            //師鐸獎
                            if (vAppendPromote.RPIDiscountScore1Name != null && !vAppendPromote.RPIDiscountScore1Name.Equals(""))
                            {
                                string openFile = location + vAppendPromote.RPIDiscountScore1Name;
                                RPIDiscountScore1HyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                                RPIDiscountScore1HyperLink.Text = vAppendPromote.RPIDiscountScore1Name;
                                RPIDiscountScore1HyperLink.Visible = true;
                            }

                            //教師優良教師
                            if (vAppendPromote.RPIDiscountScore4Name != null && !vAppendPromote.RPIDiscountScore2Name.Equals(""))
                            {
                                string openFile = location + vAppendPromote.RPIDiscountScore2Name;
                                RPIDiscountScore2HyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                                RPIDiscountScore2HyperLink.Text = vAppendPromote.RPIDiscountScore2Name;
                                RPIDiscountScore2HyperLink.Visible = true;
                            }

                            //優良導師
                            if (vAppendPromote.RPIDiscountScore3Name != null && !vAppendPromote.RPIDiscountScore3Name.Equals(""))
                            {
                                string openFile = location + vAppendPromote.RPIDiscountScore3Name;
                                RPIDiscountScore3HyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                                RPIDiscountScore3HyperLink.Text = vAppendPromote.RPIDiscountScore3Name;
                                RPIDiscountScore3HyperLink.Visible = true;
                            }
                            //執行人體試驗
                            if (vAppendPromote.RPIDiscountScore4Name != null && !vAppendPromote.RPIDiscountScore4Name.Equals(""))
                            {
                                string openFile = location + vAppendPromote.RPIDiscountScore4Name;
                                RPIDiscountScore4HyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                                RPIDiscountScore4HyperLink.Text = vAppendPromote.RPIDiscountScore4Name;
                                RPIDiscountScore4HyperLink.Visible = true;
                            }
                        }


                        //下載推薦函
                        if (vApplyAudit.AppRecommendUploadName != null && !vApplyAudit.AppRecommendUploadName.Equals(""))
                        {
                            //RecommendUploadTableRow.Visible = false;
                            RecommendUploadCB.Checked = true;
                            string openFile = location + vApplyAudit.AppRecommendUploadName;
                            RecommendHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
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

                        //下載研究計畫主持人證明
                        if (vApplyAudit.AppPPMUploadName != null && !vApplyAudit.AppPPMUploadName.Equals(""))
                        {
                            AppPPMTableRow.Visible = true;
                            AppPPMUploadCB.Checked = true;
                            string openFile = location + vApplyAudit.AppPPMUploadName;
                            AppPPMHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                            AppPPMHyperLink.Text = vApplyAudit.AppPPMUploadName;
                            AppPPMHyperLink.Visible = true;
                        }
                        //醫師證書                        
                        if (vApplyAudit.AppDrCaUploadName != null && !vApplyAudit.AppDrCaUploadName.Equals(""))
                        {
                            AppDrCaTableRow.Visible = true;
                            AppDrCaUploadCB.Checked = true;
                            string openFile = location + vApplyAudit.AppDrCaUploadName;
                            AppDrCaHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                            AppDrCaHyperLink.Text = vApplyAudit.AppDrCaUploadName;
                            AppDrCaHyperLink.Visible = true;
                        }

                        //教育部教師資格證書影
                        if (vApplyAudit.AppTeacherCaUploadName != null && !vApplyAudit.AppTeacherCaUploadName.Equals(""))
                        {
                            AppTeacherCaTableRow.Visible = false;
                            AppTeacherCaUploadCB.Checked = true;
                            string openFile = location + vApplyAudit.AppTeacherCaUploadName;
                            AppTeacherCaHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                            AppTeacherCaHyperLink.Text = vApplyAudit.AppTeacherCaUploadName;
                            AppTeacherCaHyperLink.Visible = true;
                        }
                        //教學
                        if (vApplyAudit.AppOtherTeachingUploadName != null && !vApplyAudit.AppOtherTeachingUploadName.Equals(""))
                        {
                            //OtherTeachingTableRow.Visible = false;
                            AppOtherTeachingUploadCB.Checked = true;
                            string openFile = location + vApplyAudit.AppOtherTeachingUploadName;
                            AppOtherTeachingHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                            AppOtherTeachingHyperLink.Text = vApplyAudit.AppOtherTeachingUploadName;
                            AppOtherTeachingHyperLink.Visible = true;
                        }

                        //服務
                        if (vApplyAudit.AppOtherServiceUploadName != null && !vApplyAudit.AppOtherServiceUploadName.Equals(""))
                        {
                            //OtherServiceTableRow.Visible = false;
                            AppOtherServiceUploadCB.Checked = true;
                            string openFile = location + vApplyAudit.AppOtherServiceUploadName;
                            AppOtherServiceHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                            AppOtherServiceHyperLink.Text = vApplyAudit.AppOtherServiceUploadName;
                            AppOtherServiceHyperLink.Visible = true;
                        }

                        //下載著作出版等刊物或個人事蹟等相關資料
                        if (vApplyAudit.AppPublicationUploadName != null && !vApplyAudit.AppPublicationUploadName.Equals(""))
                        {
                            //AppPublicationTableRow.Visible = false;
                            AppPublicationUploadCB.Checked = true;
                            string openFile  = location + vApplyAudit.AppPublicationUploadName;
                            AppPublicationHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
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
                                        string openFile = location + vAppendDegree.AppDDegreeThesisUploadName;
                                        AppDegreeThesisHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                                        AppDegreeThesisHyperLink.Text = vAppendDegree.AppDDegreeThesisUploadName;
                                        AppDegreeThesisHyperLink.Visible = true;
                                        AppDegreeThesisName.Text = vAppendDegree.AppDDegreeThesisName;
                                        AppDegreeThesisNameEng.Text = vAppendDegree.AppDDegreeThesisNameEng;
                                        AuditAppPublication.Text = AppDegreeThesisName.Text.ToString();
                                        AuditAppPublication2.Text = AppDegreeThesisName.Text.ToString();
                                    }
                                }

                                //若是有國外經歷資料須載入 AboutFgn true
                                DataTable dtTable = crudObject.GetAllVTeacherEduByEmpSn(vApplyAudit.EmpSn);
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    if ("TWN".Equals(dtTable.Rows[i][1].ToString()) && switchFgn!=null && String.IsNullOrEmpty(switchFgn))
                                    {
                                        switchFgn = "TWN";
                                    }
                                    else
                                    {
                                        AboutFgn.Visible = true;
                                        if(!"TWN".Equals(dtTable.Rows[i][1].ToString()))
                                            switchFgn = dtTable.Rows[i][1].ToString();
                                        if ("JPN".Equals(dtTable.Rows[i][1].ToString()))
                                        {
                                            switchFgn = "JPN";
                                            AboutJPN.Visible = true;
                                        }
                                        
                                    }
                                }

                                //載入外國學歷 以學位送審才需要
                                LoadFgn(vAppendDegree, switchFgn, location);
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
                                    //LbTeachExp.Visible = true;
                                }
                            }
                            else
                            {
                                //教師校內經歷資料(升等)                            
                                if (GVTeachTmuExp.Rows.Count > 0)
                                {
                                    GVTeachTmuExp.Visible = true;
                                    GVTeachTmuExp.DataBind();
                                }
                                else
                                {
                                    //LbTeachExp.Visible = true;
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
                                    //LbTeachCa.Visible = true;
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
                                    //LbTeachHonour.Visible = true;
                                }

                            }

                            //教師上傳論文積分表
                            GVThesisScore.DataBind();


                            //教師授課進度
                            if (GVTeachLesson.Rows.Count > 0)
                            {
                                TeachLessonTableRow.Visible = true;
                                GVTeachLesson.DataBind();
                            }
                            else
                            {
                                //升等
                                if (vApplyAudit.AppKindNo.Equals(chkPromote))
                                {
                                    TeachLessonTableRow.Visible = true;
                                    //LbTeachLesson.Visible = true;
                                }
                            }

                        }
                    }

                    //撈取(職員或外審委員)審核資料,先將原有資料帶出
                    if (Session["AcctAuditorSnEmpId"] == null)
                    {

                        Response.Redirect("~/AuditLogin.aspx"); //Time out控制 改
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
                        vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                        vAuditExecute.ExecuteStatus = false;
                        if (Request.QueryString["SnEmpId"] != null)
                        {
                            vAuditExecute = crudObject.GetExecuteAuditorDataHR(vAuditExecute);//管理端回來看資料
                            AuditFinish.Visible = false;
                            AuditSave.Visible = false;
                        }
                        else
                            vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆

                        AppAttributeNo.Text = vApplyAudit.AppAttributeNo;

                        //外審人員 Stage = 6 and Step = 1 只顯示 TableAuditExecute 
                        if (vAuditExecute != null)
                        {
                            Session["ExecuteStage"] = vAuditExecute.ExecuteStage;
                            Session["ExecuteStep"] = vAuditExecute.ExecuteStep;
                            //外審畫面
                            if ((vAuditExecute.ExecuteStage.Equals("6") && vAuditExecute.ExecuteStep.Equals("1")))
                            {
                                ExecuteSn.Text = vAuditExecute.ExecuteSn.ToString();
                                Session["ExecuteSn"] = vAuditExecute.ExecuteSn; //外審 update資料需要用
                                //TablePassExecute.Visible = false;
                                TableAuditExecute.Visible = true;

                                //舊資料帶出
                                AuditExecuteCommentsA.Text = vAuditExecute.ExecuteCommentsA;
                                AuditExecuteCommentsB.Text = vAuditExecute.ExecuteCommentsB;
                                TBWSSubject.Text = vAuditExecute.ExecuteWSSubject;
                                TBWSMethod.Text = vAuditExecute.ExecuteWSMethod;
                                TBWSContribute.Text = vAuditExecute.ExecuteWSContribute;
                                TBWSAchievement.Text = vAuditExecute.ExecuteWSAchievement;
                                TBWTotalScore.Text = vAuditExecute.ExecuteWTotalScore;
                                this.TBWTotalScoreHidden.Value = vAuditExecute.ExecuteWTotalScore;
                                TBAllTotalScore.Text = vAuditExecute.ExecuteAllTotalScore;
                                this.DDExecutePass.SelectedValue = vAuditExecute.ExecutePass;
                                //FiveLevel
                                //if (DDFiveLevelScore != null && !!DDFiveLevelScore.Equals(""))
                                //{
                                //    for (int i = 0; i < DDFiveLevelScore.Items.Count; i++)
                                //    {
                                //        if (vAuditExecute.ExecuteLevelScore.ToString().Equals(DDFiveLevelScore.Items[i].Value))
                                //        {
                                //            DDFiveLevelScore.Items[i].Selected = true;
                                //            AppAttributeName.Text = DDFiveLevelScore.Items[i].ToString();
                                //        }
                                //        else
                                //        {
                                //            DDFiveLevelScore.Items[i].Selected = false;
                                //        }
                                //    }
                                //}


                                //控制報表欄位顯示或隱藏--外審
                                VAuditReportCompose vAuditReportCompose = new VAuditReportCompose();
                                vAuditReportCompose.ARWaydNo = vApplyAudit.AppWayNo; ; //學術研究 改動態
                                vAuditReportCompose.ARKindNo = vApplyAudit.AppKindNo; // 改動態
                                vAuditReportCompose.ARAttributeNo = vApplyAudit.AppAttributeNo;//vApplyAudit.AppAttributeNo;
                                vAuditReportCompose.ARStage = vAuditExecute.ExecuteStage; //Stage
                                if (vApplyAudit.AppWayNo.Trim().Equals("2") || vApplyAudit.AppWayNo.Trim().Equals("3"))
                                {
                                    vAuditReportCompose.ARAttributeNo = "x";//vApplyAudit.AppAttributeNo;
                                    vAuditReportCompose.ARStage = "x"; //Stage
                                }
                           DataTable dtAuditReport = crudObject.GetAuditReport(vAuditReportCompose);


                           //if ((Boolean)dtAuditReport.Rows[0]["ARFiveLevel"])
                           //{
                               //FivelLevelInputData.Visible = true;
                               //    FiveLevelNote.Visible = true;
                               //    //下方說明附註
                               //    if (vApplyAudit.AppWayNo.Trim().Equals("1"))
                               //    {
                           //}
                           //if (vApplyAudit.AppWayNo.Trim().Equals("1"))
                           //{
                               //        FiveLevelDiscriptionWayNo2a.Visible = true;
                               //        FiveLevelDiscriptionWayNo2b.Visible = true;
                               //        FiveLevelDiscriptionWayNo2b.Visible = true;
                           //} 
                           String kind = Session["OuterKind"].ToString();
                           String AppAttribute = Session["OuterAttribute"].ToString();
                           if ((kind.Equals("1") && AppAttribute.Equals("3")) || (kind.Equals("2") && AppAttribute.Equals("1")))
                           {
                               Session["Kind"] = "1";
                               TableRow44.Visible = false;
                               TableRow39.Visible = true;
                               WritingScoreDiscription1.Visible = true;
                               WritingScoreDiscription2.Visible = true;
                               WritingScoreDiscription3.Visible = false;
                               //CommonNote.Visible = true;
                               WayNo1.Visible = true;
                               WritingScoreInputData.Visible = true;
                               TablePass1.Visible = true;

                                }
                           else
                           {
                               Session["Kind"] = "2";
                               TableRow44.Visible = true;
                               TableRow39.Visible = false;
                               TableRow45.Visible = true;
                               //CommonNote2.Visible = true;
                               WayNo2.Visible = true;
                               WritingScoreDiscription1.Visible = false;
                               WritingScoreDiscription2.Visible = false;
                               WritingScoreDiscription3.Visible = true;
                               WritingScoreInputData.Visible = false;
                               TablePass2.Visible = true;
                               }
                           }
                            if (vApplyAudit.AppWayNo.Trim().Equals("3"))
                            {
                                this.WayNo3.Visible = true;
                                //        FiveLevelDiscriptionWayNo3a.Visible = true;
                                //        FiveLevelDiscriptionWayNo3b.Visible = true;                                        
                            }
                            //}
                            //else
                            //{
                            //FivelLevelInputData.Visible = true;
                            //FiveLevelNote.Visible = true;
                            //}

                            //if ((Boolean)dtAuditReport.Rows[0]["ARWritingScore"])
                            //{
                            //    WritingScoreDiscription1.Visible = true;
                            //    WritingScoreDiscription2.Visible = true;
                            //    WritingScoreInputData.Visible = true;
                            //}
                            //else
                            //{
                            //WritingScoreDiscription1.Visible = true;
                            //WritingScoreDiscription2.Visible = true;
                            //WritingScoreInputData.Visible = true;
                            //}

                            //StrengthsWeaknesses 一定都要顯示

                            //AddStrengthControls( vAuditExecute.ExecuteStrengths);

                            //AddWeaknessControls(vAuditExecute.ExecuteWeaknesses);


                        }
                        //else
                        //{
                        //    TablePassExecute.Visible = true;
                        //    TableAuditExecute.Visible = false;
                        //    //是系所主管教評 七人小組意見系教評決議
                        //    if (vAuditExecute.ExecuteStage.Equals("2") && vAuditExecute.ExecuteStep.Equals("2"))
                        //    {
                        //        RowPassExecuteCommentsA.Visible = true;
                        //    }
                        //    //填寫教學分數,服務分數
                        //    if (vAuditExecute.ExecuteStage.Equals("5") && vAuditExecute.ExecuteStep.Equals("1"))
                        //    {
                        //        RowPassExecuteCommentsA.Visible = true;
                        //    }

                        //}
                        //}
                        else
                        {
                            //TablePassExecute.Visible = false;
                            TableAuditExecute.Visible = false;
                        }
                    }
                    //管理角色M || 校內一般審查者A 才載入資料並顯示

                    if (Session["AcctRole"] != null && ("A".Equals(Session["AcctRole"].ToString())))
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
                                                   new DataColumn("ExecuteBngDate",System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteEndDate",System.Type.GetType("System.String")),
                                                   new DataColumn("ExecuteStatus",System.Type.GetType("System.String"))
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
                                dtTable.Rows[i][9].ToString(),
                                dtTable.Rows[i][10].ToString(),
                                GetAuditStatus((Boolean)dtTable.Rows[i][12])
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

                        //GVAuditExecute.DataSource = dt;
                        //GVAuditExecute.DataBind();
                        //GVAuditExecute.Visible = true;
                    }
                }
                else
                {
                    //MessageLabel.Text = "抱歉，目前無資料,請洽資訊人員!!";
                    //BtnStartAuditor.Enabled = false;
                    //BtnUpdateAuditor.Enabled = false;
                }
                AuditExecuteCommentsB.Attributes["onclick"] = "copyTextData(this.id)";
                TBWTotalScore.Attributes["onclick"] = "countWTotalScore(this.id)";

            }
        }

        //載入外國學歷 以學位送審才需要
        protected void LoadFgn(VAppendDegree vAppendDegree, string switchFgn, string location)
        {
            if ("TWN".Equals(switchFgn))
            {

            }
            else
            {
                AboutFgn.Visible = true;
                if (vAppendDegree == null) return;
                AppDFgnEduDeptSchoolAdmitCB.Checked = vAppendDegree.AppDFgnEduDeptSchoolAdmit;

                //國外最高學位畢業證書
                if (vAppendDegree.AppDFgnDegreeUploadName != null && !vAppendDegree.AppDFgnDegreeUploadName.Equals(""))
                {
                    AppDFgnDegreeUploadCB.Checked = true;
                    string openFile = location + vAppendDegree.AppDFgnDegreeUploadName;
                    AppDFgnDegreeHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                    AppDFgnDegreeHyperLink.Text = vAppendDegree.AppDFgnDegreeUploadName;
                    AppDFgnDegreeHyperLink.Visible = true;
                }

                //國外學校歷年成績單
                AppDFgnGradeUploadCB.Checked = vAppendDegree.AppDFgnGradeUpload;
                if (AppDFgnGradeUploadCB.Checked)
                {
                    string openFile = location + vAppendDegree.AppDFgnGradeUploadName;
                    AppDFgnGradeHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                    AppDFgnGradeHyperLink.Text = vAppendDegree.AppDFgnGradeUploadName;
                    AppDFgnGradeHyperLink.Visible = true;
                }

                //國外學歷修業情形一覽表
                AppDFgnSelectCourseUploadCB.Checked = vAppendDegree.AppDFgnSelectCourseUpload;
                if (AppDFgnSelectCourseUploadCB.Checked)
                {
                    string openFile = location + vAppendDegree.AppDFgnSelectCourseUploadName;
                    AppDFgnSelectCourseHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                    AppDFgnSelectCourseHyperLink.Text = vAppendDegree.AppDFgnSelectCourseUploadName;
                    AppDFgnSelectCourseHyperLink.Visible = true;
                }

                //個人出入境紀錄
                AppDFgnEDRecordUploadCB.Checked = vAppendDegree.AppDFgnEDRecordUpload;
                if (AppDFgnEDRecordUploadCB.Checked)
                {
                    string openFile = location + vAppendDegree.AppDFgnEDRecordUploadName;
                    AppDFgnEDRecordHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                    AppDFgnEDRecordHyperLink.Text = vAppendDegree.AppDFgnEDRecordUploadName;
                    AppDFgnEDRecordHyperLink.Visible = true;
                }


                if ("JPN".Equals(switchFgn))
                {
                    switchFgn = "JPN";
                    AppDFgnJPAdmissionUploadCB.Checked = vAppendDegree.AppDFgnJPAdmissionUpload;

                    //A.入學許可註冊證
                    AppDFgnJPAdmissionUploadCB.Checked = vAppendDegree.AppDFgnJPAdmissionUpload;
                    if (AppDFgnJPAdmissionUploadCB.Checked)
                    {
                        string openFile = location + vAppendDegree.AppDFgnJPAdmissionUploadName;
                        AppDFgnJPAdmissionHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                        AppDFgnJPAdmissionHyperLink.Text = vAppendDegree.AppDFgnJPAdmissionUploadName;
                        AppDFgnJPAdmissionHyperLink.Visible = true;
                    }

                    //B.修畢學分成績單
                    AppDFgnJPGradeUploadCB.Checked = vAppendDegree.AppDFgnJPGradeUpload;
                    if (AppDFgnJPGradeUploadCB.Checked)
                    {
                        string openFile = location + vAppendDegree.AppDFgnJPGradeUploadName;
                        AppDFgnJPGradeHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                        AppDFgnJPGradeHyperLink.Text = vAppendDegree.AppDFgnJPGradeUploadName;
                        AppDFgnJPGradeHyperLink.Visible = true;
                    }

                    //C.在學證明及修業年數證明
                    AppDFgnJPEnrollCAUploadCB.Checked = vAppendDegree.AppDFgnJPEnrollCAUpload;
                    if (AppDFgnJPEnrollCAUploadCB.Checked)
                    {
                        string openFile = location + vAppendDegree.AppDFgnJPEnrollCAUploadName;
                        AppDFgnJPEnrollCAHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                        AppDFgnJPEnrollCAHyperLink.Text = vAppendDegree.AppDFgnJPEnrollCAUploadName;
                        AppDFgnJPEnrollCAHyperLink.Visible = true;
                    }

                    //D.通過論文資格考試證明
                    AppDFgnJPDissertationPassUploadCB.Checked = vAppendDegree.AppDFgnJPDissertationPassUpload;
                    if (AppDFgnJPDissertationPassUploadCB.Checked)
                    {
                        string openFile = location + vAppendDegree.AppDFgnJPDissertationPassUploadName;
                        AppDFgnJPDissertationPassHyperLink.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                        AppDFgnJPDissertationPassHyperLink.Text = vAppendDegree.AppDFgnJPDissertationPassUploadName;
                        AppDFgnJPDissertationPassHyperLink.Visible = true;
                    }
                }
            }
        }

        private String GetAuditStatus(Boolean status)
        {
            String strData = "";
            if (status)
            {
                strData = "完成";
            }
            else
            {
                strData = "未完成";
            }
            return strData;
        }

        protected void GVTeachEdu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labEduLocal;
            string location = Global.FileUpPath + vEmp.EmpSn + "/";
            ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
            String[] strDegree = convertDTtoStringArray.GetStringArray(crudObject.GetAllDegreeName());
            String[] strDegreeType = convertDTtoStringArray.GetStringArray(crudObject.GetAllDegreeTypeName());
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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
                    LoadFgn(vAppendDegree, switchFgn, location);
                }
            }
        }

        protected void GVTeachExp_RowDataBound(object sender, GridViewRowEventArgs e)
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
                hyperLnkTeachExp = (HyperLink)e.Row.FindControl("HyperLinkExp");
                ExpUploadName = (Label)e.Row.FindControl("ExpUploadName");
                string openFile = Global.FileUpPath + empSn + "/" + ExpUploadName.Text;
                hyperLnkTeachExp.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
            }
        }

        protected void GVTeachTmuExp_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ExpUploadName = (Label)e.Row.FindControl("ExpUploadName");
                string openFile = Global.FileUpPath + empSn + "/" + ExpUploadName.Text;
                hyperLnkTeachExp.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
                if (ExpUploadName == null || ExpUploadName.Text == "" || ExpUploadName.Text.Equals("&nbsp;"))
                {
                    hyperLnkTeachExp.Text = "無資料";
                    hyperLnkTeachExp.NavigateUrl = "";
                }
            }
        }
        protected void GVThesisScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            HyperLink hyperLnkThesis;
            Label labelThesisUploadName;

            Label lblAppSn;
            Label lblThesisSign;
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
                labelThesisUploadName = (Label)e.Row.FindControl("ThesisUploadName");
                string openFile = Global.FileUpPath + empSn + "/" + e.Row.Cells[9].Text;
                hyperLnkThesis.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkThesis = (HyperLink)e.Row.FindControl("HyperLinkThesis");
                lblAppSn = (Label)e.Row.FindControl("AppSn");
                lblThesisSign = (Label)e.Row.FindControl("lblThesisSign");

                //if (Session["PublicationUploadName"]!=null&& Session["PublicationUploadName"].ToString().Equals(e.Row.Cells[10].Text))
                //{
                //    lblThesisSign.Text = "●";
                //}
                labelThesisUploadName = (Label)e.Row.FindControl("ThesisUploadName");
                lbThesisName = (Label)e.Row.FindControl("TName");
                txtIsRepresentative = (TextBox)e.Row.FindControl("IsRepresentative");
                if (txtIsRepresentative.Text.ToString().Equals("True"))
                {
                    lblThesisSign.Text = "●";
                    if (lbThesisName != null)
                    {
                        if (AuditAppPublication.Text.ToString().Equals("") || AuditAppPublication2.Text.ToString().Equals(""))
                        {
                            if (!AuditAppPublication.Text.ToString().Equals(""))
                                AuditAppPublication.Text += " 、 <br/> <br/>" + lbThesisName.Text.ToString();
                            else
                                AuditAppPublication.Text = lbThesisName.Text.ToString();
                            AuditAppPublication2.Text = AppDegreeThesisName.Text;
                        }
                        else
                        {
                            AuditAppPublication.Text = lbThesisName.Text.ToString() + " 、 <br/> <br/>" + AuditAppPublication.Text;
                            AuditAppPublication2.Text = AppDegreeThesisName.Text ;
                        }
                    }
                }

                string openFile = Global.FileUpPath + empSn + "/" + labelThesisUploadName.Text.ToString();
                if ( openFile.IndexOf("_" + EmpNameCN.Text.Trim()) >0 )
                    openFile = openFile.Substring(0, openFile.IndexOf("_" + EmpNameCN.Text.Trim())) + ".pdf";

                hyperLnkThesis.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";

                Session["ThesisAccuScore"] = float.Parse(e.Row.Cells[12].Text) + float.Parse(Session["ThesisAccuScore"].ToString());

            }
            AppPThesisAccuScore.Text = Session["ThesisAccuScore"].ToString();
            if (Session["ResearchYear"] != null && !Session["ResearchYear"].ToString().Equals(""))
            {
                float floatYear = float.Parse(Session["ResearchYear"].ToString());
                double floatRPI = Double.Parse(Session["ThesisAccuScore"].ToString()) * 100f / (75f * floatYear);

                vApplyAudit = crudObject.GetApplyAuditObj(int.Parse(Session["AppSn"].ToString()));
                AppRPI.Text = vApplyAudit.AppRPIScore;//Math.Round(floatRPI, 2, MidpointRounding.AwayFromZero).ToString();  //管理端看到是從資料庫撈出來但這邊不知道為什麼用四捨五入積分
            }


        }

        protected void GVThesisScoreCoAuthor_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            HyperLink hyperLnkThesis;
            string empSn = "1";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                hyperLnkThesis = (HyperLink)e.Row.FindControl("HyperLinkThesis");
                //string openFile = Global.FileUpPath + empSn + "/" +  e.Row.Cells[2].Text ;
                Label ThesisCoAuthorUploadName = (Label)e.Row.FindControl("ThesisCoAuthorUploadName");
                string openFile = Global.FileUpPath + empSn + "/" + ThesisCoAuthorUploadName.Text ;
                hyperLnkThesis.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";
            }
        }

        protected void GVAuditExecute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TextBox txtBoxAuditorId;
            TextBox txtBoxAuditorName;
            TextBox txtBoxAuditorEmail;
            string strParameter = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                txtBoxAuditorId = (TextBox)e.Row.FindControl("TextBoxAuditorSnEmpId");
                txtBoxAuditorName = (TextBox)e.Row.FindControl("TextBoxAuditorName");
                txtBoxAuditorEmail = (TextBox)e.Row.FindControl("TextBoxAuditorEmail");
                string strStage = DataBinder.Eval(e.Row.DataItem, "ExecuteStageNum").ToString();
                string strStep = DataBinder.Eval(e.Row.DataItem, "ExecuteStepNum").ToString();
                strParameter = "ObjId=" + txtBoxAuditorId.ClientID + "&ObjName=" + txtBoxAuditorName.ClientID + "&ObjEmail=" + txtBoxAuditorEmail.ClientID + "&Stage=" + strStage + "&Step=" + strStep + "&AttributeNo=" + AppAttributeNo.Text.ToString();
                txtBoxAuditorName.Attributes["onclick"] = "window.open('FunSelectAuditor.aspx?" + strParameter + " ','mywin','100','100','no','center');return true;";
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
            //VAuditExecute vAuditExecute;
            //int i = 0;
            //foreach (GridViewRow row in GVAuditExecute.Rows)
            //{
            //    //因為在GridView需指定後才能抓到值
            //    TextBox strExecuteSn = (TextBox)row.FindControl("TextBoxAuditorSn");
            //    TextBox txtBoxAuditorSnEmpId = (TextBox)row.FindControl("TextBoxAuditorSnEmpId");
            //    TextBox txtBoxAuditorEmail = (TextBox)row.FindControl("TextBoxAuditorEmail");
            //    TextBox txtBoxAuditorName = (TextBox)row.FindControl("TextBoxAuditorName");

            //    if (!string.IsNullOrEmpty(txtBoxAuditorSnEmpId.Text))
            //    {
            //        vAuditExecute = new VAuditExecute();
            //        vAuditExecute.ExecuteSn = Convert.ToInt32(strExecuteSn.Text.ToString());
            //        vAuditExecute.ExecuteAuditorSnEmpId = txtBoxAuditorSnEmpId.Text.ToString();
            //        vAuditExecute.ExecuteAuditorEmail = txtBoxAuditorEmail.Text.ToString();
            //        vAuditExecute.ExecuteAuditorName = txtBoxAuditorName.Text.ToString();

            //        crudObject.UpdateExecuteAuditorEmp(vAuditExecute);
            //    }
            //    i++;
            //}
        }

        ////啟動簽核寄發Email通知,產生帳號資料
        //protected void BtnStartAuditor_Click(object sender, EventArgs e)
        //{
        //    CRUDObject crudObject = new CRUDObject();

            //判斷第一順位簽核人員資料
            //GridViewRow row = (GridViewRow)GVAuditExecute.Rows[0];

            //TextBox strExecuteSn = (TextBox)row.FindControl("TextBoxAuditorSn");

            //TextBox txtBoxAuditorSnEmpId = (TextBox)row.FindControl("TextBoxAuditorSnEmpId");
            //TextBox txtBoxAuditorEmail = (TextBox)row.FindControl("TextBoxAuditorEmail");
            //TextBox txtBoxAuditorName = (TextBox)row.FindControl("TextBoxAuditorName");
            //Session["NextAcctAuditorSnEmpId"] = txtBoxAuditorSnEmpId.Text.ToString();
            //Session["NextAcctAuditorEmail"] = txtBoxAuditorEmail.Text.ToString();
            //Session["NextAcctAuditorName"] = txtBoxAuditorName.Text.ToString();
            //Session["NextAcctRoleName"] = "承辦人員";

        //    //寫入第一位簽核者(都是校內員工） Login帳號 及寄信通知
        //    if (Session["NextAcctAuditorSnEmpId"] != null && Session["AppSn"] != null)
        //    {
        //        int appSn = Convert.ToInt32(Session["AppSn"].ToString());
        //        VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(appSn); //vApplyAudit.AppSn 
        //        Boolean isTaipeiUniversity = true;
        //        if (vAuditExecuteNextOne != null)
        //        {
        //            if ((vAuditExecuteNextOne.ExecuteStage.Equals("6") && vAuditExecuteNextOne.ExecuteStep.Equals("1")))
        //            {
        //                isTaipeiUniversity = false;
        //            }
        //            //Session["NextAcctAuditorSnEmpId"] = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
        //            //Session["NextAcctAuditorEmail"] = vAuditExecuteNextOne.ExecuteAuditorEmail;
        //            //Session["NextAcctAuditorName"] = vAuditExecuteNextOne.ExecuteAuditorName;
        //            //Session["NextAcctRoleName"] = vAuditExecuteNextOne.ExecuteRoleName;
        //            if (GenerateAccountAndSendEmail(isTaipeiUniversity, vAuditExecuteNextOne))
        //            {
        //                MessageLabelAll.Text = " 啟動成功, 送審『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核!!";
        //            }

        //        }
        //        else
        //        {
        //            MessageLabelAll.Text = " 無法送審下一階段,請洽資訊人員處理!!";
        //        }

        //    }
        //    else
        //    {
        //        //MessageAudit.Text = "啟動失敗,請確認第一順位的簽核人員是否完成設定!";
        //    }
        //}

        ////審畢,進下一階段人員審核 Session["AuditorSnEmpId"].ToString();GenerateAccoutnAndSendEmail
        //protected void BtnAuditPass_Click(object sender, EventArgs e)
        //{
        //    Boolean executePass = false;

        //    vAuditExecute = new VAuditExecute();
        //    vAuditExecute.AppSn = Convert.ToInt32(Session["AppSn"].ToString());
        //    vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
        //    vAuditExecute.ExecuteStatus = false;
        //    vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆 Submit時被清掉 再重新讀取一次

        //    vAuditExecute.ExecutePass = DDExecutePass.SelectedValue.ToString();
        //    string passStatus = DDExecutePass.SelectedValue.ToString();
        //    executePass = this.crudObject.UpdateExecuteAuditDataFinish(vAuditExecute);
        //    if (executePass)
        //    {
        //        switch (vAuditExecute.ExecutePass)
        //        {
        //            case "0":
        //                MessageLabelAll.Text = "您尚未完成審查,請盡速完成!!";
        //                break;
        //            case "1":
        //                MessageLabelAll.Text = "審查完成!!!";
        //                GoNextAuditExecuteStageStep();
        //                break;
        //            case "2":
        //                MessageLabelAll.Text = "審查不通過!!!";
        //                break;
        //            case "3":
        //                MessageLabelAll.Text = "審查退回!!";//1.處理狀態 2.Email給退回的院祕
        //                ReturnAuditExecuteStageStep();
        //                break;

        //        }
        //    }
        //    else
        //    {
        //        MessageLabelAll.Text = "簽審動作失敗,請洽資訊人員!!";
        //    }
        //}

        //外審委員暫存
        protected void BtnAuditSave_Click(object sender, EventArgs e)
        {
            string error = null;
            //if ((TBWSSubject.Visible && TBWSMethod.Visible && TBWSContribute.Visible && TBWSAchievement.Visible)|| TBAllTotalScore.Visible)
            //{
                if (AppJobTitle.Text != null)
                {
                    switch (AppJobTitle.Text)
                    {
                        case "教授":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 5)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 10)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 35)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 50)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床教授":
                            break;
                        case "副教授":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 10)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 20)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 30)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 40)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床副教授":
                            break;
                        case "助理教授":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 20)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 25)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 25)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 30)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床助理教授":
                            break;
                        case "講師":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 25)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 30)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 25)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 20)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床講師":
                            break;
                    }
                if(OtherStrengths.Text.Length > 150 || OtherWeaknesses.Text.Length > 150)
                {
                    if (OtherStrengths.Text.Length > 150)
                        error += "優點(其他)請勿超過150字、";
                    if (OtherWeaknesses.Text.Length > 150)
                        error += "缺點(其他)請勿超過150字、";

                }

                }
                if (error != null)
                    Response.Write("<Script language='JavaScript'>alert('" + error.Substring(0, error.Length - 1) + "!');</Script>");
                else
                {

                    if (Request.QueryString["SnEmpId"] != null)
                    {
                        vAuditExecute = crudObject.GetExecuteAuditorDataHR(vAuditExecute);//管理端回來看資料
                    }
                    else
                        vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆
                    VAuditExecute vAuditExecuteUpdate = new VAuditExecute();
                    vAuditExecuteUpdate.ExecuteSn = Convert.ToInt32(Session["ExecuteSn"].ToString());
                    vAuditExecuteUpdate.ExecuteCommentsA = AuditExecuteCommentsA.Text.ToString();
                    vAuditExecuteUpdate.ExecuteCommentsB = AuditExecuteCommentsB.Text.ToString();


            vAuditExecuteUpdate.ExecuteStrengths = ProcessCheckedData(GetSelectedStrengthData(CkBxStrengths), OtherStrengths.Text.ToString());
            vAuditExecuteUpdate.ExecuteWeaknesses = ProcessCheckedData(GetSelectedWeaknessData(CkBxWeaknesses), OtherWeaknesses.Text.ToString());
            vAuditExecuteUpdate.ExecuteAllTotalScore = TBAllTotalScore.Text.ToString();
            //vAuditExecuteUpdate.ExecuteLevelScore = DDFiveLevelScore.SelectedValue.ToString();

            vAuditExecuteUpdate.ExecuteWSSubject = TBWSSubject.Text.ToString();
            vAuditExecuteUpdate.ExecuteWSMethod = TBWSMethod.Text.ToString();
            vAuditExecuteUpdate.ExecuteWSContribute = TBWSContribute.Text.ToString();
            vAuditExecuteUpdate.ExecuteWSAchievement = TBWSAchievement.Text.ToString();
            vAuditExecuteUpdate.ExecuteWTotalScore = TBWTotalScore.Text.ToString();
            Boolean boolAuditExecuteUpdate = crudObject.UpdateExecuteAuditData(vAuditExecuteUpdate);
            if (boolAuditExecuteUpdate)
            {
                MessageLabelAll.Text = "您的審核資料已暫存成功!!";
            }
            else
            {
                MessageLabelAll.Text = "您的審核資料暫存失敗,請洽資訊人員處理!!";
            }

                    Response.Write("<Script language='JavaScript'>alert('" + MessageLabelAll.Text + "');</Script>");
                }
            //}
        }

        //外審委員審畢,進下一階段人員審核 
        protected void BtnAuditFinish_Click(object sender, EventArgs e)
        {

            string error = null;
            //if ((TBWSSubject.Visible && TBWSMethod.Visible && TBWSContribute.Visible && TBWSAchievement.Visible)|| TBAllTotalScore.Visible)
            //{
                if (AppJobTitle.Text != null && AuditAttributeName.Text != "已具部定教師資格")
                {
                    switch (AppJobTitle.Text)
                    {
                        case "教授":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 5)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 10)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 35)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 50)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床教授":
                            break;
                        case "副教授":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 10)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 20)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 30)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 40)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床副教授":
                            break;
                        case "助理教授":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 20)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 25)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 25)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 30)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床助理教授":
                            break;
                        case "講師":
                            if (!String.IsNullOrEmpty(TBWSSubject.Text) && float.Parse(TBWSSubject.Text) > 25)
                                error += "研究主題得分超出、";
                            if (!String.IsNullOrEmpty(TBWSMethod.Text) && float.Parse(TBWSMethod.Text) > 30)
                                error += "研究方法及能力得分超出、";
                            if (!String.IsNullOrEmpty(TBWSContribute.Text) && float.Parse(TBWSContribute.Text) > 25)
                                error += "學術及實務貢獻得分超出、";
                            if (!String.IsNullOrEmpty(TBWSAchievement.Text) && float.Parse(TBWSAchievement.Text) > 20)
                                error += "整體成就得分超出、";
                            break;
                        case "臨床講師":
                            break;
                    }
                }

            if (OtherStrengths.Text.Length > 150 || OtherWeaknesses.Text.Length > 150)
            {
                if (OtherStrengths.Text.Length > 150)
                    error += "優點(其他)請勿超過150字、";
                if (OtherWeaknesses.Text.Length > 150)
                    error += "缺點(其他)請勿超過150字、";

            }
            if (error != null)
                    Response.Write("<Script language='JavaScript'>alert('" + error.Substring(0, error.Length - 1) + "!');</Script>");
                else
                {
                    vAuditExecute = new VAuditExecute();
                    vAuditExecute.AppSn = Convert.ToInt32(Request.QueryString["AppSn"].ToString());
                    if (Session["AcctAuditorSnEmpId"]!=null)
                        vAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                    else
                        vAuditExecute.ExecuteAuditorSnEmpId = Request.QueryString["SnEmpId"].ToString();
                    vAuditExecute.ExecuteStatus = false;

                    if (Request.QueryString["SnEmpId"] != null)
                        vAuditExecute = crudObject.GetExecuteAuditorDataHR(vAuditExecute);//管理端回來看資料
                    else
                        vAuditExecute = crudObject.GetExecuteAuditorData(vAuditExecute); //只取未完成的第一筆
                    Session["ExecuteSn"] = vAuditExecute.ExecuteSn;
                    Session["ExecuteStage"] = vAuditExecute.ExecuteStage;
                    Session["ExecuteStep"] = vAuditExecute.ExecuteStep;

                    VAuditExecute vAuditExecuteUpdate = new VAuditExecute();
            vAuditExecuteUpdate.ExecuteSn = Convert.ToInt32(Session["ExecuteSn"].ToString());

            vAuditExecuteUpdate.ExecuteStage = Session["ExecuteStage"].ToString();
            vAuditExecuteUpdate.ExecuteStep = Session["ExecuteStep"].ToString();

            vAuditExecuteUpdate.ExecuteCommentsA = AuditExecuteCommentsA.Text.ToString();
            vAuditExecuteUpdate.ExecuteCommentsB = AuditExecuteCommentsB.Text.ToString();


            vAuditExecuteUpdate.ExecuteStrengths = ProcessCheckedData(GetSelectedStrengthData(CkBxStrengths), OtherStrengths.Text.ToString());
            vAuditExecuteUpdate.ExecuteWeaknesses = ProcessCheckedData(GetSelectedWeaknessData(CkBxWeaknesses), OtherWeaknesses.Text.ToString());

            //vAuditExecuteUpdate.ExecuteLevelScore = DDFiveLevelScore.SelectedValue.ToString();

            vAuditExecuteUpdate.ExecuteWSSubject = TBWSSubject.Text.ToString();
            vAuditExecuteUpdate.ExecuteWSMethod = TBWSMethod.Text.ToString();
            vAuditExecuteUpdate.ExecuteWSContribute = TBWSContribute.Text.ToString();
            vAuditExecuteUpdate.ExecuteWSAchievement = TBWSAchievement.Text.ToString();
            vAuditExecuteUpdate.ExecuteWTotalScore = TBWTotalScore.Text.ToString();
            vAuditExecuteUpdate.ExecutePass = DDExecutePass.SelectedValue.ToString();
            vAuditExecuteUpdate.ExecuteStatus = false;

            //因為評分總分在前端做加總 (1044行)，因此要抓取前端Value
            TBWTotalScore.Text = this.TBWTotalScoreHidden.Value;
            if (TBAllTotalScore.Visible != true && AuditAttributeName.Text != "已具部定教師資格")
            {
                //判斷是否審過  0審核中 1及格 2不及格; TBWTotalScore得分總分
                if (!string.IsNullOrEmpty(TBWSSubject.Text.ToString()) &&
                    !string.IsNullOrEmpty(TBWSMethod.Text.ToString()) &&
                    !string.IsNullOrEmpty(TBWSContribute.Text.ToString()) &&
                    !string.IsNullOrEmpty(TBWSAchievement.Text.ToString()) &&


                    !string.IsNullOrEmpty(AuditExecuteCommentsA.Text.ToString()) &&
                    !string.IsNullOrEmpty(AuditExecuteCommentsB.Text.ToString()) &&
                    
                    !string.IsNullOrEmpty(TBWTotalScore.Text.ToString()) &&
                    DDExecutePass.SelectedValue.ToString() != "0")
                {
                    //vAuditExecute.ExecutePass = DDExecutePass.SelectedValue.ToString(); 改過
                    if (float.Parse(TBWTotalScore.Text.ToString()) > 0)
                    {
                        vAuditExecuteUpdate.ExecuteStatus = true;
                        vAuditExecuteUpdate.ExecuteAllTotalScore = TBWTotalScore.Text.ToString();
                    }

                    ////判斷是否審過  舊版用百分比 現今已不使用
                    ////if (!string.IsNullOrEmpty(DDFiveLevelScore.SelectedValue.ToString()))
                    ////{
                    ////    if (Convert.ToInt32(DDFiveLevelScore.SelectedValue.ToString()) < 4)
                    ////    {
                    ////        vAuditExecuteUpdate.ExecuteStatus = true;
                    ////    }
                    ////}
                    //if (!string.IsNullOrEmpty(TBAllTotalScore.Text.ToString()))
                    //{
                    //    if (Convert.ToInt32(TBAllTotalScore.Text.ToString()) > 70)
                    //    {
                    //        vAuditExecuteUpdate.ExecuteStatus = true;
                    //        vAuditExecuteUpdate.ExecuteAllTotalScore = TBAllTotalScore.Text.ToString();
                    //        //for (int i = 0; i < TBTotalScorePass.Items.Count; i++)
                    //        //{
                    //        //    if (TBTotalScorePass.Items[i].Value.Equals("1"))
                    //        //    {
                    //        //        TBTotalScorePass.Items[i].Selected = true;
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        TBTotalScorePass.Items[i].Selected = false;
                    //        //    }
                    //        //}
                    //    }
                    //}

                            //送出儲存資料 OPEN IT
                            Boolean boolAuditExecuteUpdate = crudObject.UpdateExecuteAuditDataFinish(vAuditExecuteUpdate);
                            if (boolAuditExecuteUpdate)
                            {
                                MessageLabelAll.Text = "審核完成!!! 審畢日期:" + DateTime.Now.ToString("yyyy/MM/dd tt hh:mm:ss");
                            }
                            else
                            {
                                MessageLabelAll.Text = "簽審失敗,請洽資訊人員處理!!";
                            }

                            Response.Write("<Script language='JavaScript'>alert('" + MessageLabelAll.Text + "');</Script>");
                            //判斷所有外審是否都審完
                            int appSn = Convert.ToInt32(Session["AppSn"].ToString());
                            string exeStage = Session["ExecuteStage"].ToString();
                            string exeStep = Session["ExecuteStep"].ToString();
                            ArrayList arrayList = this.crudObject.GetExecuteAuditorOutter(appSn, exeStage, exeStep);
                           // if (arrayList.Count == 0) GoNextAuditExecuteStageStep();

                    //Context.Response.Clear();
                    //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
                    //Context.Response.Charset = "utf-8";
                    //Context.Response.AddHeader("content-disposition", "attachment;filename=export.pdf");
                    //Context.Response.ContentType = "application/x-pdf";

                    //Context.Response.Write("<html><head><style>\n");
                    //Context.Response.Write(@"TD {mso-number-format:\@;mso-ignore:colspan;}\n");
                    //Context.Response.Write("@br {mso-data-placement:same-cell;})");
                    //Context.Response.Write("</style></head><body>");
                    //System.IO.StringWriter myStreamWriter = new System.IO.StringWriter();
                    //HtmlTextWriter myHtmlTextWriter = new HtmlTextWriter(myStreamWriter);
                    ////this.TableAuditExecute.AllowPaging = false;
                    //this.TableAuditExecute.RenderControl(myHtmlTextWriter);
                    //Context.Response.Write(myStreamWriter.ToString());
                    //Context.Response.End();
                    ////this.TableAuditExecute.AllowPaging = true;
                }
                else
                {
                    MessageLabelAll.Text = "";
                    if (string.IsNullOrEmpty(TBWSSubject.Text.ToString()))
                        MessageLabelAll.Text += "研究主題分數未填!  ";
                    if (string.IsNullOrEmpty(TBWSMethod.Text.ToString()))
                        MessageLabelAll.Text += "研究方法及能力分數未填!  ";
                    if (string.IsNullOrEmpty(TBWSContribute.Text.ToString()))
                        MessageLabelAll.Text += "學術及實務貢獻未填!  ";
                    if (string.IsNullOrEmpty(TBWSAchievement.Text.ToString()))
                        MessageLabelAll.Text += "整體成就分數未填!  ";

                    if (string.IsNullOrEmpty(AuditExecuteCommentsA.Text.ToString()))
                        MessageLabelAll.Text += "表(甲)審查意見表未填!  ";
                    if (string.IsNullOrEmpty(AuditExecuteCommentsB.Text.ToString()))
                        MessageLabelAll.Text += "表(乙)審查意見表未填!  ";

                    if (string.IsNullOrEmpty(TBWTotalScore.Text.ToString()))
                        MessageLabelAll.Text += "評分分數未填!  ";
                    if (DDExecutePass.SelectedValue.ToString() == "0")
                        MessageLabelAll.Text += "請選擇是否及格!  ";
                    Response.Write("<Script language='JavaScript'>alert('" + MessageLabelAll.Text + "');</Script>");
                }
            }
            else //送出判定外審時的防呆需要分開，此為學位送審
            {
                //判斷是否審過  0審核中 1及格 2不及格; 總分是否為空值
                if (!string.IsNullOrEmpty(AuditExecuteCommentsA.Text.ToString()) &&
                    !string.IsNullOrEmpty(AuditExecuteCommentsB.Text.ToString()) &&
                    (!string.IsNullOrEmpty(TBAllTotalScore.Text.ToString()) || AuditAttributeName.Text == "已具部定教師資格") &&
                    DDExecutePass.SelectedValue.ToString() != "0")
                {
                    //vAuditExecute.ExecutePass = DDExecutePass.SelectedValue.ToString(); 改過
                    if (AuditAttributeName.Text != "已具部定教師資格" && Convert.ToInt32(TBAllTotalScore.Text.ToString()) > 0  )
                    {
                        vAuditExecuteUpdate.ExecuteStatus = true;
                        vAuditExecuteUpdate.ExecuteAllTotalScore = TBAllTotalScore.Text.ToString();
                    }

                    ////判斷是否審過  舊版用百分比 現今已不使用
                    ////if (!string.IsNullOrEmpty(DDFiveLevelScore.SelectedValue.ToString()))
                    ////{
                    ////    if (Convert.ToInt32(DDFiveLevelScore.SelectedValue.ToString()) < 4)
                    ////    {
                    ////        vAuditExecuteUpdate.ExecuteStatus = true;
                    ////    }
                    ////}
                    //if (!string.IsNullOrEmpty(TBAllTotalScore.Text.ToString()))
                    //{
                    //    if (Convert.ToInt32(TBAllTotalScore.Text.ToString()) > 70)
                    //    {
                    //        vAuditExecuteUpdate.ExecuteStatus = true;
                    //        vAuditExecuteUpdate.ExecuteAllTotalScore = TBAllTotalScore.Text.ToString();
                    //        //for (int i = 0; i < TBTotalScorePass.Items.Count; i++)
                    //        //{
                    //        //    if (TBTotalScorePass.Items[i].Value.Equals("1"))
                    //        //    {
                    //        //        TBTotalScorePass.Items[i].Selected = true;
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        TBTotalScorePass.Items[i].Selected = false;
                    //        //    }
                    //        //}
                    //    }
                    //}

                            //送出儲存資料 OPEN IT
                            Boolean boolAuditExecuteUpdate = crudObject.UpdateExecuteAuditDataFinish(vAuditExecuteUpdate);
                            if (boolAuditExecuteUpdate)
                            {
                                MessageLabelAll.Text = "審核完成!!! 審畢日期:" + DateTime.Now.ToString("yyyy/MM/dd tt hh:mm:ss");
                            }
                            else
                            {
                                MessageLabelAll.Text = "簽審失敗,請洽資訊人員處理!!";
                            }

                    Response.Write("<Script language='JavaScript'>alert('" + MessageLabelAll.Text + "');</Script>");
                    //判斷所有外審是否都審完
                    int appSn = Convert.ToInt32(Session["AppSn"].ToString());
                    string exeStage = Session["ExecuteStage"].ToString();
                    string exeStep = Session["ExecuteStep"].ToString();
                    ArrayList arrayList = this.crudObject.GetExecuteAuditorOutter(appSn, exeStage, exeStep);
                    if (arrayList.Count == 0) GoNextAuditExecuteStageStep();

                    //Context.Response.Clear();
                    //Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
                    //Context.Response.Charset = "utf-8";
                    //Context.Response.AddHeader("content-disposition", "attachment;filename=export.pdf");
                    //Context.Response.ContentType = "application/x-pdf";

                    //Context.Response.Write("<html><head><style>\n");
                    //Context.Response.Write(@"TD {mso-number-format:\@;mso-ignore:colspan;}\n");
                    //Context.Response.Write("@br {mso-data-placement:same-cell;})");
                    //Context.Response.Write("</style></head><body>");
                    //System.IO.StringWriter myStreamWriter = new System.IO.StringWriter();
                    //HtmlTextWriter myHtmlTextWriter = new HtmlTextWriter(myStreamWriter);
                    ////this.TableAuditExecute.AllowPaging = false;
                    //this.TableAuditExecute.RenderControl(myHtmlTextWriter);
                    //Context.Response.Write(myStreamWriter.ToString());
                    //Context.Response.End();
                    ////this.TableAuditExecute.AllowPaging = true;
                }
                else
                {
                    MessageLabelAll.Text = "";
                    if (string.IsNullOrEmpty(AuditExecuteCommentsA.Text.ToString()))
                        MessageLabelAll.Text += "表(甲)審查意見表未填!  ";
                    if (string.IsNullOrEmpty(AuditExecuteCommentsB.Text.ToString()))
                        MessageLabelAll.Text += "表(乙)審查意見表未填!  ";

                            if (string.IsNullOrEmpty(TBAllTotalScore.Text.ToString()) && AuditAttributeName.Text != "已具部定教師資格")
                                MessageLabelAll.Text += "總分分數未填!  ";
                            if (DDExecutePass.SelectedValue.ToString() == "0")
                                MessageLabelAll.Text += "請選擇是否及格!  ";
                            Response.Write("<Script language='JavaScript'>alert('" + MessageLabelAll.Text + "');</Script>");
                        }
                    }
                }
            //}
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
                    MessageLabelAll.Text += " 此申請案件已審完畢!!";
                }


            }
            else
            {
                Boolean isTaipeiUniversity = false; //外審獨立出來了!
                if (vAuditExecuteNextOne != null)
                {
                    //if(vAuditExecuteNextOne.ExecuteStage.Equals("6") && vAuditExecuteNextOne.ExecuteStep.Equals("1"))
                    //{
                    //    isTaipeiUniversity = false;
                    //}

                    //若是外審的
                    if (!isTaipeiUniversity)
                    {
                        ArrayList arrayList = this.crudObject.GetExecuteAuditorOutter(appSn, vAuditExecuteNextOne.ExecuteStage, vAuditExecuteNextOne.ExecuteStep);
                        foreach (VAuditExecute vAuditExecuteOutter in arrayList)
                        {
                            if (GenerateAccountAndSendEmail(isTaipeiUniversity, vAuditExecuteOutter))
                            {
                                MessageLabelAll.Text = " 送審下一階段『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核!!\r\n";
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
                            MessageLabelAll.Text = " 送審下一階段『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核!!\r\n";
                        }

                    }
                }
                else
                {
                    MessageLabelAll.Text = " 無法送審下一階段,請洽資訊人員處理!!";
                }

            }
        }

        //退回院秘層級Stage = "3" Step = "1" 此段流程為固定[注意]
        private void ReturnAuditExecuteStageStep()
        {
            int appSn = Convert.ToInt32(Session["AppSn"].ToString());
            //更新 ApplyAudit 中的 審核Stage與Step的狀態
            VApplyAudit vApplyAuditUpdate = new VApplyAudit();
            vApplyAuditUpdate.AppSn = appSn;
            vApplyAuditUpdate = crudObject.GetApplyAuditObjByAppSn(appSn);

            VAuditExecute vAuditExecute = new VAuditExecute();
            vAuditExecute.AppSn = appSn;
            vAuditExecute.ExecuteStep = vApplyAuditUpdate.AppStep;
            vAuditExecute.ExecuteStage = vApplyAuditUpdate.AppStage;
            vAuditExecute.ExecutePass = "3";
            vAuditExecute.ExecuteStatus = false;
            crudObject.UpdateAuditExecuteStatus(vAuditExecute);


            vApplyAuditUpdate.AppStage = "0"; //退回本人
            vApplyAuditUpdate.AppStep = "0"; //退回本人
            vApplyAuditUpdate.AppStatus = false;
            if (crudObject.UpdateApplyAuditStageStepStatus(vApplyAuditUpdate))
            {
                VEmployeeBase vEmployeeBase = new VEmployeeBase();
                vEmployeeBase = crudObject.GetEmpBsaseObjByEmpSn("" + vApplyAudit.EmpSn);
                //Email  
                //VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(appSn);
                //ReturnAuditSendEmail(vEmployeeBase);寄信

            }

        }

        private string GetSelectedStrengthData(CheckBoxList ckBxStrengths)
        {
            //Response.Write("selected items --> " + selStrList);
            return selStrList;
        }


        private string GetSelectedWeaknessData(CheckBoxList ckBxStrengths)
        {
            //Response.Write("selected items --> " + selWeakList);
            return selWeakList;
        }

        protected void EmpSex_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void AddStrengthControls(string executeSW)
        {
            PlaceHolder pholderStrengths = this.PHolderStrengths;
            DataTable dtSW;
            //Hashtable hashTable = new Hashtable();
            VStrengthsWeaknesses vStrengthsWeaknesses = new VStrengthsWeaknesses();
            vStrengthsWeaknesses.WaydNo = vApplyAudit.AppWayNo; //學術研究 教學實務 產學應用
            vStrengthsWeaknesses.KindNo = vApplyAudit.AppKindNo; ; // 改動態
            if (vApplyAudit.AppWayNo.Equals("1")) vStrengthsWeaknesses.KindNo = "1";
            vStrengthsWeaknesses.AttributeNo = "1"; // 目前都設1  因為著作&學位優點一樣   未來可能 著作送審1  學位送審2
            vStrengthsWeaknesses.SWType = "S"; //優點

            dtSW = crudObject.GetStrengthsWeaknesses(vStrengthsWeaknesses);

            //for (int i = 0; i < dtSW.Rows.Count; i++)
            //{
            //    if ((Boolean)dtSW.Rows[i][2])
            //    {
            //        hashTable.Add(dtSW.Rows[i][1].ToString(), Convert.ToInt32(dtSW.Rows[i][0].ToString()));
            //    }

            //}

            CheckBoxList checkBoxList = new CheckBoxList();
            checkBoxList.ID = "chbxStrengths";


            ListItem listItem;
            foreach (DataRow dr in dtSW.Rows)
            {
                listItem = new ListItem(dr["SWContent"].ToString(), dr["SWSn"].ToString());
                if (!string.IsNullOrEmpty(executeSW))
                    listItem.Selected = executeSW.IndexOf(dr["SWSn"].ToString()) > -1;
                //if (!string.IsNullOrEmpty(executeSW))
                //{
                //    if (executeSW.IndexOf(entry.Value.ToString()) > -1)
                //    {
                //        listItem.Selected = true;
                //    }
                //    else
                //    {
                //        listItem.Selected = false;
                //    }
                //}
                checkBoxList.Items.Add(listItem);
            }
            //pnlCheckbox為Panel控制項
            pholderStrengths.Controls.Add(checkBoxList);
            //為自訂的checkbox掛上event
            checkBoxList.SelectedIndexChanged += new EventHandler(CheckSBoxList_CheckedChanged);

            int intNum = 0;
            string other = executeSW.Split(',')[executeSW.Split(',').Length - 1];
            bool result = int.TryParse(other, out intNum); //intNum = other
            if (result != true) OtherStrengths.Text = other;
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
            //Response.Write("選取S:" + selStrList);
        }

        private void AddWeaknessControls(string executeSW)
        {
            PlaceHolder pholderWeaknesses = this.PHolderWeaknesses;
            DataTable dtSW;
            //Hashtable hashTable = new Hashtable();
            VStrengthsWeaknesses vStrengthsWeaknesses = new VStrengthsWeaknesses();
            vStrengthsWeaknesses.WaydNo = vApplyAudit.AppWayNo; //學術研究 教學實務 產學應用
            vStrengthsWeaknesses.KindNo = vApplyAudit.AppKindNo; ; // 
            if (vApplyAudit.AppWayNo.Equals("1")) vStrengthsWeaknesses.KindNo = "1";
            String kind = Session["OuterKind"].ToString();
            String AppAttribute= Session["OuterAttribute"].ToString();
            if ((kind.Equals("1") && AppAttribute.Equals("3")) || (kind.Equals("2") && AppAttribute.Equals("1")) )
                vStrengthsWeaknesses.AttributeNo = "1"; //vApplyAudit.AppAttributeNo;目前1著作2學位  因為學位&著作缺點評比量不同
            else
                vStrengthsWeaknesses.AttributeNo = "2"; //vApplyAudit.AppAttributeNo;目前1著作2學位  因為學位&著作缺點評比量不同 (正式機先設2上線)
            vStrengthsWeaknesses.SWType = "W"; //缺點

            dtSW = crudObject.GetStrengthsWeaknesses(vStrengthsWeaknesses);

            CheckBoxList checkBoxList = new CheckBoxList();
                checkBoxList.ID = "chbxWeaknesses";

            ListItem listItem;
            foreach (DataRow dr in dtSW.Rows)
            {
                listItem = new ListItem(dr["SWContent"].ToString(), dr["SWSn"].ToString());
                if (!string.IsNullOrEmpty(executeSW))
                    listItem.Selected = executeSW.IndexOf(dr["SWSn"].ToString()) > -1;
                checkBoxList.Items.Add(listItem);
            }
                //pnlCheckbox為Panel控制項
                pholderWeaknesses.Controls.Add(checkBoxList);
                //pholderCheckbox.Controls.Add(new LiteralControl("</br>"));
                //為自訂的checkbox掛上event
                checkBoxList.SelectedIndexChanged += new EventHandler(CheckWBoxList_CheckedChanged);
            int intNum = 0;
            string other = executeSW.Split(',')[executeSW.Split(',').Length - 1];
            bool result = int.TryParse(other, out intNum); //intNum = other
            if (result != true) OtherWeaknesses.Text = other;
        }


        void CheckWBoxList_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxList cbxList = (CheckBoxList)sender;
            selWeakList = "";
            foreach (ListItem listItem in cbxList.Items)
            {
                if (listItem.Selected)
                {
                    selWeakList += listItem.Value + ",";
                }
            }
            //Response.Write("選取W:" + selWeakList);
        }

        public Boolean GenerateAccountAndSendEmail(Boolean isTaipeiUniversity, VAuditExecute vAuditExecuteNextOne)
        {
            crudObject = new CRUDObject();
            //取得密碼
            Mail mail = new Mail();

            //Email
            VSendEmail vSendEmail = new VSendEmail();
            vSendEmail.MailToAccount = vAuditExecuteNextOne.ExecuteAuditorEmail;
            vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
            vSendEmail.MailSubject = "「教師聘任升等作業系統」有申請文件--請盡速簽核";
            vSendEmail.ToAccountName = vAuditExecuteNextOne.ExecuteRoleName + " " + vAuditExecuteNextOne.ExecuteAuditorName;
            try
            {
                //先確認是否為校內人員
                string empIdno = "";
                int acctSn = 0;
                empIdno = crudObject.GetVEmployeeFromTmuHrByEmail(vAuditExecuteNextOne.ExecuteAuditorEmail);
                //Email抓取申請者資料顯示在Email中
                VApplyerData vApplyerData = crudObject.GetApplyDataForEmail(vAuditExecuteNextOne.AppSn);

                if (empIdno != null)
                {
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
                    vSendEmail.MailContent = "<br> 「教師聘任升等作業系統」需您的核准!<br><font color=red>請再完成您的審核</font>,申請者才能進入下一階段的審核.<br> " + strTableData + " <br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入審核</a>  您的帳號:" + vAccountForManage.AcctEmail + " 密碼:校內使用的密碼 <br/><br><br><br><br>人資處 家羚(2022) 怡慧(2028) 伊芝(2066)<br>";
                }
                //else
                //{
                //    //取得密碼
                //    GeneratorPwd generatorPwd = new GeneratorPwd();
                //    generatorPwd.Execute();
                //    string newPwd = generatorPwd.GetPwd();
                //    //校外審核者一律新增 新的帳號 一次僅審核一位
                //    VAccountForAudit vAccountForAudit = new VAccountForAudit();
                //    vAccountForAudit.AcctAppSn = Session["AppSn"].ToString();
                //    vAccountForAudit.AcctAuditorSnEmpId = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                //    vAccountForAudit.AcctEmail = vAuditExecuteNextOne.ExecuteAuditorEmail;
                //    vAccountForAudit.AcctPassword = newPwd;
                //    vAccountForAudit.AcctStatus = true;
                //    crudObject.Insert(vAccountForAudit);
                //    string strTableData = "<table border=1 cellspacing=0>" +
                //    "<tbody><tr>" +
                //    "<th>聘任單位</th><th >姓名</th><th >專兼任別</th><th >應聘等級</th><th >審查性質</th><th >新聘升等</th><th >申請狀態</th></tr>" +
                //    "<tr><td>" + vApplyerData.UntNameFull + "</td><td>" + vApplyerData.EmpNameCN + "</td><td>" + vApplyerData.JobAttrName + "</td><td>" + vApplyerData.JobTitleName + "</td><td>" + vApplyerData.AttributeName + "</td><td>" + vApplyerData.KindName + "</td><td>" + vApplyerData.AuditProgressName + "</td></tr></tbody></table>";
                //    vSendEmail.MailContent = "<br> 「教師聘任升等作業系統」需您的核准!<br><font color=red>請在三天內完成您的審核</font>,申請者才能進入下一階段的審核.<br> " + strTableData + " <br>感謝!<br><br><br><br><a href=\"https://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入審核</a>  您的帳號:" + vAccountForAudit.AcctEmail + " 密碼:" + vAccountForAudit.AcctPassword + " <br/><br><br><br><br>人資處 怡慧(2028) 亭吟(2066)<br>";
                //}

                //更新 ApplyAudit 中的 審核Stage與Step的狀態
                VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                vApplyAuditUpdate.AppSn = vAuditExecuteNextOne.AppSn;
                vApplyAuditUpdate.AppStage = vAuditExecuteNextOne.ExecuteStage;
                vApplyAuditUpdate.AppStep = vAuditExecuteNextOne.ExecuteStep;
                vApplyAuditUpdate.AppStatus = true;
                crudObject.UpdateApplyAuditStageStep(vApplyAuditUpdate);

                //寄發Email通知
                //return (Boolean)mail.SendEmail(vSendEmail);
                return true;
            }
            catch (Exception e)
            {
                MessageLabelAll.Text = e.ToString();
                return false;
            }

        }

        private void deleteCookie_Click()
        {

            Response.Cookies.Clear();
        }

        public Boolean ReturnAuditSendEmail(VEmployeeBase vEmployeeBase)
        {
            try
            {

                //寄發Email通知
                Mail mail = new Mail();
                VSendEmail vSendEmail = new VSendEmail();

                //MailMessage mailObj = new MailMessage();
                vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
                vSendEmail.MailToAccount = vEmployeeBase.EmpEmail;
                vSendEmail.MailSubject = "台北醫學大學新聘申請文件--退回學院補件通知";
                vSendEmail.MailContent = vEmployeeBase.EmpNameCN + " 您好，<br><br> 系所有新聘申請退回補件!<br><font color=red>請協助確認新申內容填寫問題，</font><br><br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/Apply.aspx\">按此進入填寫</a>  <br/><br><br><br><br>人資處 怡慧(2028) 伊芝(2066)<br>";
                return (Boolean)mail.SendEmail(vSendEmail);

            }
            catch (Exception e)
            {
                MessageLabelAll.Text = e.ToString();
                return false;
            }


        }

        protected String getHyperLink(string strFileName)
        {
            String strHyperLink = "";
            String openFile = Global.FileUpPath + Request.QueryString["EmpSn"].ToString() + "/" + strFileName;
            //String openFile = "../DocuFile/HRApplyDoc/" + Session["EmpSn"].ToString() + "/" + strFileName;
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
    }
}
