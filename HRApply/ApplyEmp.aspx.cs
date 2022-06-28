using System;
using System.Web;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Configuration;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace ApplyPromote
{


    public partial class ApplyEmp : PageBaseApply, System.Web.UI.ICallbackEventHandler
    {

        string tooltip;

        //臨床學科直接詳填
        public ArrayList untArray = new ArrayList { "E0109", "E0110", "E0111", "E0112", "E0113", "E0114", "E0115", "E0116", "E0117", "E0118", "E0119", "E0120", "E0121", "E0122", "E0123", "E0124", "E0125", "E0126", "E0102", "E0130", "E0131" };

        public void RaiseCallbackEvent(String eventArgument)
        {
            string filename = eventArgument;
        }

        public string GetCallbackResult()
        {
            return tooltip;
        }

        //上傳檔案 基本資料
        static public ArrayList fileLists = new ArrayList();

        //上傳檔案 論文

        VEmployeeBase vEmp = new VEmployeeBase();

        VApplyAudit vApplyAudit;

        VApplyAudit vApplyAuditOld;

        VAppendPublication vAppendPublication;

        //論文積分
        float totalThesisScore = 1.0f;

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

        //判斷國別
        static string switchFgn = "";

        //醫學系
        static string chkUnitNo = "E0100";

        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();

        GetSettings getSettings = new GetSettings();

        double intfillInput = 0;
        double intfillselfnput = 0;
        double intfillCheckBox = 0;
        double intfillCheckBoxTotal = 0;
        static double fillInputRate = 0;
        static double fillSelfRate = 0;
        static double fillCheckBoxRate = 0;
        static string fillTeachEdu = "0";
        static string fillTeachExp = "0";
        static string fillTeachCa = "0";
        static string fillHounor = "0";
        static string fillThesis = "0";
        int fillThesisOral = 6;
        int fillThesisMom = 0;
        int fillThesisSon = 0;
        Boolean isThesisNotUpload;
        int countRPI = 0;
        String countThesisScorePaper = "";
        //頁籤切換
        public enum SearchType
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewSelfReview = 1,
            ViewTeachEdu = 2,
            ViewTeachExp = 3,
            ViewTeachCa = 4,
            ViewTeachHonor = 5,
            ViewThesisScore = 6,
            ViewDegreeThesis = 7
        }


        //上傳檔案
        NameValueCollection fileCollection = new NameValueCollection();


        protected void Page_PreRender(object sender, EventArgs e)
        {
            //            if (!IsPostBack)
            //            {
            //                this.ViewState["postBackTimes"] = -1;
            //            }
            //            else
            //            {
            //                //if (!this.ToolkitScriptManager.IsInAsyncPostBack)
            //                //{
            //                    this.ViewState["postBackTimes"] = Convert.ToInt16(this.ViewState["postBackTimes"]) - 1; 
            //                //}
            //            }
            //            string gobackjs = @"function MyBack() 
            //                        {history.go(" + Convert.ToString(this.ViewState["postBackTimes"]) + @");}";
            //ToolkitScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "back", gobackjs, true);            
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {
            DESCrypt DES = new DESCrypt();
            if (Request["ApplyerID"] != null && Session["LoginEmail"] != null)
            {
                try
                {
                    EmpIdno.Text = DES.Decrypt(Request["ApplyerID"].ToString());
                    ViewState["ApplyerID"] = EmpIdno.Text.ToString();
                    Session["ApplyerID"] = EmpIdno.Text.ToString();
                    Session["EmpIdno"] = EmpIdno.Text.ToString();
                    if (Request["AppSn"] != null)
                    {
                        TbAppSn.Text = DES.Decrypt(Request["AppSn"].ToString());
                        Session["AppSn"] = TbAppSn.Text.ToString();
                        string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text.ToString());
                        Session["Parameters"] = parameters;
                    }
                    else
                    {
                        TbAppSn.Text = "";
                        Session["AppSn"] = null;
                        Session["Parameters"] = "";
                    }
                    //新聘判斷是否為多單
                    if (crudObject.GetApplyListCntByIdno(EmpIdno.Text.ToString()) > 1)
                    {
                        BtnReturnBack.Visible = true;
                    }
                    else
                    {
                        BtnApplyMore.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('抱歉您無權限進入修改'); history.back();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('抱歉您無權限進入修改'); location.href='Default.aspx';", true);
                    }
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
            }
            //學位送審的頁籤:著作論文相關,論文指導&口試委員名單
            ViewState["ApplyAttributeNo"] = null;
            if (Request["ApplyAttributeNo"] != null && !Request["ApplyAttributeNo"].ToString().Equals(""))
            {
                ViewState["ApplyAttributeNo"] = Request["ApplyAttributeNo"].ToString();
            }


            vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text.ToString());
            Session["EmpIdno"] = EmpIdno.Text.ToString();
            if (vEmp != null)
            {
                Session["EmpSn"] = vEmp.EmpSn;
                TbEmpSn.Text = vEmp.EmpSn.ToString();
                if (TbAppSn.Text.ToString().Equals(""))//只有一筆資料或者無資料
                {
                    vApplyAudit = crudObject.GetApplyAuditObjByIdno(EmpIdno.Text.ToString());
                }
                else
                {
                    vApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text.ToString()));
                }
                if (vApplyAudit != null && ViewState["ApplyAttributeNo"] == null)
                {
                    countThesisScorePaper = "" + crudObject.GetVThesisScoreTotalCount(vApplyAudit.EmpSn, vApplyAudit.AppSn);
                    Session["AppSn"] = vApplyAudit.AppSn;
                    if (vApplyAudit.AppAttributeNo.Equals(chkDegree))
                    {
                        ViewState["ApplyAttributeNo"] = vApplyAudit.AppAttributeNo;
                        MVall.SetActiveView(ViewDegreeThesis);
                        Menu1.Visible = true;
                        Menu2.Visible = false;
                        Menu3.Visible = false;
                    }
                    else
                    {
                        MVall.Views.Remove(ViewDegreeThesis);
                        Menu1.Visible = false;
                        Menu2.Visible = true;
                        Menu3.Visible = false;
                    }
                    //EmpAttributeNo.Enabled = false;
                }
                else
                {
                    if (ViewState["ApplyAttributeNo"] != null && ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree))
                    {
                        MVall.SetActiveView(ViewDegreeThesis);
                        Menu1.Visible = true;
                        Menu2.Visible = false;
                        Menu3.Visible = false;
                    }
                    else
                    {
                        MVall.Views.Remove(ViewDegreeThesis);
                        Menu1.Visible = false;
                        Menu2.Visible = true;
                        Menu3.Visible = false;
                    }
                }
            }
            else
            {
                if (ViewState["ApplyAttributeNo"] != null && ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree))
                {
                    MVall.SetActiveView(ViewDegreeThesis);
                    Menu1.Visible = true;
                    Menu2.Visible = false;
                }
                else
                {
                    MVall.Views.Remove(ViewDegreeThesis);
                    Menu2.Visible = true;
                    Menu1.Visible = false;
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            isThesisNotUpload = false; //是否有未上傳論文積分附件
            if (Request["ApplyerID"] != null && Session["LoginEmail"] != null)
            {
                if (Request["ManageEmpId"] != null)
                {
                    EmpBaseModifySave.Visible = true;
                    SelfReviewModifySave.Visible = true;
                    SelfReviewSave.Visible = false;
                    Session["ManageEmpId"] = Request["ManageEmpId"].ToString();
                    //BtnReturnBack.Visible = true;
                }
                else
                {
                    //返回鍵顯示 返回時清空Session["ManageEmpId"] 
                    if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                    {
                        //BtnReturnBack.Visible = true;
                        EmpBaseModifySave.Visible = true;
                        SelfReviewModifySave.Visible = true;
                    }
                }
                //取得基本資料EmpSn 抓出 ApplyAudit
                getSettings = new GetSettings();
                getSettings.Execute();

                //if (EmpIdno.Text.ToString() != null) LoadDataBtn_Click(sender, e);

                AddELawNumControls(chkApply, "06", "");
                vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text.ToString());
                if (vEmp != null)
                {
                    //流程：先抓當學期新聘資料>再抓前一次申請資料>抓員工基本資料EmployeeBase>EmpHr資料
                    if (TbAppSn.Text.ToString().Equals(""))//只有一筆資料或者無資料
                    {
                        vApplyAudit = crudObject.GetApplyAuditObjByIdno(EmpIdno.Text.ToString());
                    }
                    else
                    {
                        vApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text.ToString()));
                        ViewState["AppStage"] = vApplyAudit.AppStage;
                        ViewState["AppStep"] = vApplyAudit.AppStep;
                    }
                    if (Object.Equals(null, vApplyAudit))
                    {
                        //讀取歷史資料最後一次申請資料
                        vApplyAudit = crudObject.GetApplyAuditObjLastOne(vEmp.EmpSn);
                        if (!Object.Equals(null, vApplyAudit))
                            vApplyAudit.AppStatus = false; //無論前一次狀態是成功或失敗;
                    }
                    //顯示目前申請完成送出
                    if (vApplyAudit != null && vApplyAudit.AppStatus)
                    {
                        Table table = new Table();                  //create one object of type Table
                        table.ID = "Table_Item";
                        table.Attributes.Add("border", "0px");     //自己加一個屬性
                        table.Style["font-size"] = "12pt";         //設定Style
                        table.Style["text-align"] = "center";      //設定Style
                        table.Style["font-weight"] = "bold";      //設定Style
                        table.Style["width"] = "100%";
                        table.Style["height"] = "30pt";
                        table.CssClass = "table table-bordered table-condensed table-hover";

                        TableRow tRow = new TableRow();     //create a new object of type TableRow
                        table.Rows.Add(tRow);
                        ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
                        String[] strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());
                        String[] strAuditText = { "未審", "審過", "審不過", "EmpBaseSave", "完成" };

                        DataTable dtTable = crudObject.GetAllAuditStageByEmpSn(vApplyAudit.AppSn);
                        TableCell Cell = new TableCell();
                        string strCell = "";

                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        strCell = "申請完成";
                        Cell.BackColor = System.Drawing.Color.YellowGreen;
                        Cell.Text = strCell;
                        tRow.Cells.Add(Cell);
                        Cell = new TableCell();
                        Cell.Width = 150;

                        string tmpStage = "0";
                        string strInAudit = "";
                        int cnt = dtTable.Rows.Count - 1;

                        for (int i = 0; i <= dtTable.Rows.Count - 1; i++)
                        {
                            if (i == 0) { tmpStage = dtTable.Rows[i][0].ToString(); }


                            //if(i==0)tmpStage = dtTable.Rows[i][0].ToString();
                            if (vApplyAudit.AppStage.Equals(dtTable.Rows[i][0].ToString()) && vApplyAudit.AppStep.Equals(dtTable.Rows[i][1].ToString()))
                            {
                                strInAudit = strStatus[Int32.Parse(dtTable.Rows[i][0].ToString())] + "<br>" + dtTable.Rows[i][2].ToString() + "審核中...";

                            }

                            ////階段中的文字
                            //if (string.IsNullOrEmpty(strInAudit))
                            //{

                            Cell.Text = strStatus[Int32.Parse(tmpStage)];
                            //}
                            //else
                            //{
                            //    Cell.Text = strInAudit;
                            //}
                            //階段顏色
                            if (vApplyAudit.AppStage.Equals(dtTable.Rows[i][0].ToString()) || Convert.ToInt32(dtTable.Rows[i][0].ToString()) < Convert.ToInt32(vApplyAudit.AppStage))
                            {
                                Cell.BackColor = System.Drawing.Color.YellowGreen;
                            }
                            else
                            {
                                Cell.BackColor = System.Drawing.Color.Yellow;
                            }
                            if (!tmpStage.Equals(dtTable.Rows[i][0].ToString()))
                            {
                                strCell = Cell.Text;
                                col = Cell.BackColor;
                                if (vApplyAudit.AppStage.Equals(tmpStage))
                                {
                                    Cell.Text = strInAudit;
                                    strInAudit = "";
                                }
                                else
                                {
                                    Cell.Text = strCell;
                                }

                                //寫入CELL
                                Cell.BackColor = col;
                                tRow.Cells.Add(Cell);
                                //清空CELL
                                Cell = new TableCell();
                                Cell.Width = 150;
                                Cell.Text = "";

                                tmpStage = dtTable.Rows[i][0].ToString();
                                strCell = "";
                            }
                        }
                        if (vApplyAudit.AppStage.Equals(dtTable.Rows[cnt][0].ToString()) || Convert.ToInt32(dtTable.Rows[cnt][0].ToString()) < Convert.ToInt32(vApplyAudit.AppStage))
                        {
                            Cell.BackColor = System.Drawing.Color.YellowGreen;
                        }
                        else
                        {
                            Cell.BackColor = System.Drawing.Color.Yellow;
                        }
                        //階段中的文字
                        if (string.IsNullOrEmpty(strInAudit))
                        {
                            strCell = strStatus[Int32.Parse(tmpStage)];
                        }
                        else
                        {
                            strCell = strInAudit;
                            strInAudit = "";
                        }

                        //寫入CELL
                        Cell.Text = strCell;
                        tRow.Cells.Add(Cell);

                        //for (int i = 0; i < 8; i++)
                        //{

                        //    Cell = new TableCell();
                        //    if (i == 0)
                        //    {
                        //        Cell.Text = "申請完成";
                        //        Cell.BackColor = System.Drawing.Color.YellowGreen;
                        //    }
                        //    else
                        //    {
                        //        Cell.Text = strStatus[i];
                        //        Cell.BackColor = System.Drawing.Color.Yellow;
                        //    }

                        //    tRow.Cells.Add(Cell);

                        //    //Label VV = new Label();
                        //    //VV.Text = "第1欄";

                        //}
                        Label lblStatus = new Label();
                        lblStatus.Text = "審核狀態：<font color='green'>綠色</font>表已完成階段";
                        PanelStatus.Controls.Add(lblStatus);
                        PanelStatus.Controls.Add(table);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            Session.Timeout = 60;
            Session["ThesisAccuScore"] = "0";
            Session["ThesisRPIScore"] = "0";
            Session["ResearchYear"] = "1";

            if (!IsPostBack)
            {
                if (Session["LoginEmail"] != null)
                {
                    ViewState["AppJobTitleNo"] = "";
                    //Session["ThesisScore"] initial
                    Session["ThesisScore"] = 1;

                    //MessageLabel initial
                    MessageLabel.Text = "";
                    //MessageLabelSelfReview.Text = "";
                    //MessageLabelEdu.Text = "";
                    //MessageLabelExp.Text = "";
                    //msg = "";
                    //MessageLabelCa.Text = "";
                    //msg = "";
                    MessageLabel.Text = "";
                    Session["index"] = "";
                    allDDL_refresh();
                    ddl_EmpBirthYear.Attributes.Add("onChange", "SelChange()");
                    ddl_EmpBirthMonth.Attributes.Add("onChange", "SelChange()");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
                }
            }
            //if (Request["ApplyerID"] != null && Session["LoginEmail"] != null)
            //{
            //if (Session["index"] == null || Session["index"].ToString().Equals(""))
            //{
            //    MVall.ActiveViewIndex = (int)SearchType.ViewTeachBase;
            //    Session["index"] = "0";
            //}
            //else
            //{
            //    MVall.ActiveViewIndex = Convert.ToInt32(Session["index"].ToString());
            //}
            if (Session["index"] == null || Session["index"].ToString().Equals(""))
            {
                MVall.ActiveViewIndex = (int)SearchType.ViewTeachBase;

                Session["index"] = "0";
            }
            else
            {
                SearchType mSearchType = (SearchType)MVall.ActiveViewIndex;
                MVall.ActiveViewIndex = (int)mSearchType;
            }
            MessageLabel.Text = " ";
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
            //}
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

            if (Request["ApplyKindNo"] != null && Request["ApplyWayNo"] != null && Request["ApplyAttributeNo"] != null && vApplyAudit == null)
            {
                ViewState["ApplyKindNo"] = Request["ApplyKindNo"].ToString();  //類別: 1新聘 2升等
                ViewState["ApplyWayNo"] = Request["ApplyWayNo"].ToString(); //途徑
                ViewState["ApplyAttributeNo"] = Request["ApplyAttributeNo"].ToString(); //類型 
                TbKindNo.Text = ViewState["ApplyKindNo"].ToString();
                //新聘類型:1已具部定教師資格 2未具部定教師資格_學位送審 3未具部定教師資格_著作送審 4臨床教師新聘	
                //升等類型:1著作送審升等 2學位送審升等 3臨床教師升等

                if (!IsPostBack)
                {

                    //應徵科系

                    AppUnitNo.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                    AppUnitNo.DataTextField = "unt_name_full";      //在此輸入的是資料表的欄位名稱
                    AppUnitNo.DataSource = crudObject.GetOpenUnit().DefaultView;
                    AppUnitNo.DataBind();
                    AppUnitNo.Items.Insert(0, "請選擇");

                    EmpCountry.DataBind();
                    EmpCountry.Items.Insert(0, "請選擇");
                    EmpBornCity.DataBind();
                    EmpBornCity.Items.Insert(0, "請選擇");

                    String[] num = { "零", "一", "二", "三", "四", "五" };
                    //AppJobTitleNo.DataValueField = "JobTitleNo";
                    //AppJobTitleNo.DataTextField = "JobTitleName";
                    //AppJobTitleNo.DataSource = crudObject.GetAllJobTitleNoName();
                    //AppJobTitleNo.DataBind();
                    //AppJobTitleNo.Items.Insert(0, "請選擇");
                    ItemLabel.Text = num[1];  //num[Int32.Parse(AppEJobTitleNo.SelectedValue.ToString())];


                    //AppELawNumNo.DataValueField = "LawItemNo";
                    //AppELawNumNo.DataTextField = "LawItemNoCN";
                    //AppELawNumNo.DataSource = crudObject.GetApplyTeacherLaw(AppEJobTitleNo.SelectedValue.ToString()).DefaultView;
                    //AppELawNumNo.DataBind();
                    //AppELawNumNo.DataValueField = "LawItemNo";
                    //AppELawNumNo.DataTextField = "LawItemNoCN";
                    //AppELawNumNo.DataSource = crudObject.GetApplyTeacherLaw(AppEJobTitleNo.SelectedValue.ToString()).DefaultView;
                    //AppELawNumNo.DataBind();
                    //AppELawNumNo.Items.Insert(0, "請選擇");
                    //Local預設在台灣
                    TeachEduLocal.DataBind();
                    for (int i = 0; i < TeachEduLocal.Items.Count - 1; i++)
                    {
                        if ("TWN".Equals(TeachEduLocal.Items[i].Value))
                        {
                            TeachEduLocal.Items[i].Selected = true;
                        }
                        else
                        {
                            TeachEduLocal.Items[i].Selected = false;
                        }
                    }
                }
                AppJobTypeNo.DataValueField = "JobAttrNo";
                AppJobTypeNo.DataTextField = "JobAttrName";
                AppJobTypeNo.DataSource = crudObject.GetAllJobTypeNoName();
                AppJobTypeNo.DataBind();
                AppJobTypeNo.Items.Insert(0, "請選擇");


                //新聘類型 已具部定教師資格1 學位2 著作3 臨床教師新聘4
                AppAttributeNo.DataValueField = "status";
                AppAttributeNo.DataTextField = "note";
                AppAttributeNo.DataSource = crudObject.GetApplyHrAttribute().DefaultView;
                AppAttributeNo.DataBind();
                //新聘類型 指定申請類別 著作 學位
                for (int i = 0; i < AppAttributeNo.Items.Count; i++)
                {
                    if (ViewState["ApplyAttributeNo"].ToString().Equals(AppAttributeNo.Items[i].Value))
                    {
                        AppAttributeNo.Items[i].Selected = true;
                    }
                    else
                    {
                        AppAttributeNo.Items[i].Selected = false;
                    }
                }


                //
                TeachExpSave.Enabled = false;
                TeachCaSave.Enabled = false;
                TeachHornourSave.Enabled = false;

                if (vApplyAudit != null) //若有舊資料就抓新聘這邊的基本資料檔
                {
                    if (vApplyAudit.AppStatus && Session["AcctRole"].ToString() != "M")
                    {
                        AppUnitNo.Enabled = false;
                    }
                    LoadDataBtn_Click(sender, e);
                }
                else
                {
                    VEmpTmuHr vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(EmpIdno.Text);
                    if (vEmpTmuHr != null)
                    {
                        if (vEmpTmuHr.EmpBdate.ToString() != "" && vEmpTmuHr.EmpBdate.ToString().Length == 7)
                        {
                            ddl_EmpBirthYear.SelectedValue = vEmpTmuHr.EmpBdate.ToString().Substring(0, 3);
                            ddl_EmpBirthMonth.SelectedValue = vEmpTmuHr.EmpBdate.ToString().Substring(3, 2);
                            ddl_EmpBirthDate.SelectedValue = vEmpTmuHr.EmpBdate.ToString().Substring(5, 2);
                            //EmpBirthDay.Text = vEmpTmuHr.EmpBdate.ToString();
                        }
                        EmpNameCN.Text = vEmpTmuHr.EmpName.ToString();
                        EmpNameENFirst.Text = vEmpTmuHr.EmpElname.ToString();
                        EmpSex.Text = vEmpTmuHr.EmpSex.ToString();
                        EmpCountry.Text = vEmpTmuHr.EmpNation.ToString();
                        EmpTelPri.Text = vEmpTmuHr.EmpPtel.ToString();
                        EmpEmail.Text = vEmpTmuHr.EmpEmail.ToString();
                        EmpTownAddressCode.Text = vEmpTmuHr.EmpMzcode1.ToString();
                        EmpTownAddress.Text = vEmpTmuHr.EmpMadr1.ToString();
                        EmpAddressCode.Text = vEmpTmuHr.EmpPzcode.ToString();
                        EmpAddress.Text = vEmpTmuHr.EmpPadrs.ToString();
                        EmpCell.Text = vEmpTmuHr.EmpDgd.ToString();
                        EmpNowJobOrg.Text = vEmpTmuHr.EmpUntFullName.ToString();

                        AuditNameCN.Text = vEmpTmuHr.EmpName.ToString();
                        //Response.Write("<Script language='JavaScript'>alert('"+ EmpEmail.Text + "');</Script>");
                        String identity = Request.QueryString["identity"];
                        if (Request.QueryString["identity"] != "Manager")
                        {
                            GetSettings setting = new GetSettings();
                            EmpEmail.Text = setting.LoginMail;
                        }
                        else
                        {
                            VYear.Visible = true;
                            VSemester.Visible = true;
                            BtnSaveYear.Visible = true;
                            VYearAndSemester.Text = "&nbsp;/&nbsp;";
                        }

                    }
                }
            }
            else
            {

                String identity = Request.QueryString["identity"];
                if (Request.QueryString["identity"] != "Manager")
                {
                    GetSettings setting = new GetSettings();
                    EmpEmail.Text = setting.LoginMail;
                }
                else
                {
                    VYear.Visible = true;
                    VSemester.Visible = true;
                    BtnSaveYear.Visible = true;
                    VYearAndSemester.Text = "&nbsp;/&nbsp;";
                }
                if (vApplyAudit != null)
                {
                    if (vApplyAudit.AppStatus && Session["AcctRole"].ToString() != "M")
                    {
                        AppUnitNo.Enabled = false;
                    }
                    ViewState["ApplyKindNo"] = vApplyAudit.AppKindNo;
                    ViewState["ApplyWayNo"] = vApplyAudit.AppWayNo;
                    countThesisScorePaper = "" + crudObject.GetVThesisScoreTotalCount(vApplyAudit.EmpSn, vApplyAudit.AppSn);
                    if (Request["ApplyAttributeNo"] != null && !Request["ApplyAttributeNo"].ToString().Equals(""))
                    {
                        ViewState["ApplyAttributeNo"] = Request["ApplyAttributeNo"].ToString();
                    }
                    else
                    {
                        ViewState["ApplyAttributeNo"] = vApplyAudit.AppAttributeNo;
                    }
                    if (Session["isLoadDataBtn"] == null || Session["isLoadDataBtn"].ToString().Equals(""))
                    {
                        LoadDataBtn_Click(sender, e);
                    }
                    else
                    {
                        Session["isLoadDataBtn"] = "";
                    }
                    TbKindNo.Text = ViewState["ApplyKindNo"].ToString();
                    AddELawNumControls(chkApply, vApplyAudit.AppJobTitleNo, vApplyAudit.AppLawNumNo);
                    //論文積分相關
                    Session["ResearchYear"] = vApplyAudit.AppResearchYear;
                    if (Session["ResearchYear"] != null && !Session["ResearchYear"].ToString().Equals(""))
                    {
                        for (int i = 0; i < AppResearchYear.Items.Count; i++)
                        {
                            if (vApplyAudit.AppResearchYear.Equals(AppResearchYear.Items[i].Value))
                            {
                                AppResearchYear.Items[i].Selected = true;
                            }
                            else
                            {
                                AppResearchYear.Items[i].Selected = false;
                            }
                        }
                    }
                }
            }

            if (ViewState["ApplyAttributeNo"] != null)
            {
                OtherServiceTableRow.Visible = true;

                //1.已具部定教育資格
                if (ViewState["ApplyAttributeNo"].ToString().Equals(chkTeacher))
                {
                    //醫學系 副教授以上
                    if (ViewState["AppJobTitleNo"] != null && !ViewState["AppJobTitleNo"].Equals(""))
                    {
                        if (Int32.Parse(ViewState["AppJobTitleNo"].ToString()) >= 3 && AppUnitNo.SelectedValue.ToString().Substring(0, 1).Equals("E")) //醫學院副教授以上職等
                            AppPPMTableRow.Visible = true;
                    }
                    AppTeacherCaTableRow.Visible = true;
                } //2.學位                
                else if (ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree))
                {
                    OtherTeachingTableRow.Visible = true;
                    AppTeacherCaTableRow.Visible = true;
                    // Create a table to store employee values.
                    DataTable dt = new DataTable("ThesisOral");
                    DataRow TempDataRow;

                    // Define the columns for the dt table.  EmpCountry.Items.Insert(0, "請選擇");
                    //dt.Columns.Add(
                    //   new DataColumn("ThesisOralTypeName", typeof(string)));
                    //dt.Columns.Add(
                    //   new DataColumn("ThesisOralType", typeof(string)));

                    //// Populate the dt table.
                    //TempDataRow = dt.NewRow();
                    //TempDataRow[0] = "論文指導教師";
                    //TempDataRow[1] = "1";
                    //dt.Rows.Add(TempDataRow);

                    //TempDataRow = dt.NewRow();
                    //TempDataRow[0] = "口試委員";
                    //TempDataRow[1] = "2";
                    //dt.Rows.Add(TempDataRow);

                    //ThesisOralType.DataSource = dt.DefaultView;
                    ThesisOralType.Items.Clear();
                    ThesisOralType.Items.Add(new ListItem("論文指導教師", "1"));
                    ThesisOralType.Items.Add(new ListItem("口試委員", "2"));
                    ThesisOralType.DataBind();

                } //3.著作
                else if (ViewState["ApplyAttributeNo"].ToString().Equals(chkPublication))
                {
                    AppPPMTableRow.Visible = true;
                    AppTeacherCaTableRow.Visible = true;
                    OtherTeachingTableRow.Visible = true;
                    // Create a table to store employee values.
                    ThesisOralType.Items.Clear();
                    ThesisOralType.Items.Add(new ListItem("迴避名單", "3"));
                    ThesisOralType.DataBind();
                    AvoidReason.Visible = true;
                }
                else if (ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical))
                {
                    AppDrCaTableRow.Visible = true;
                    RecommandNote.Text = "僅有醫學院者需附上";
                }

            }

        }

        protected void BtnApplyMore_Click(object sender, EventArgs e)
        {
            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
            DESCrypt DES = new DESCrypt();
            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=1&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
            ////parameters = Uri.EscapeDataString(parameters);
            //原為新增其他聘單
            //String strUrl = "ApplyShortEmp.aspx?" + parameters;
            //Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + strUrl + "';", true);
            Response.Redirect("~/ApplyList.aspx?" + parameters);
        }

        protected void AppAttributeNo_Click(object sender, EventArgs e)
        {
            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
            DESCrypt DES = new DESCrypt();
            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text.ToString()) + "&ApplyKindNo=" + ViewState["ApplyKindNo"].ToString() + "&ApplyWayNo=" + ViewState["ApplyWayNo"].ToString() + "&ApplyAttributeNo=" + ViewState["ApplyAttributeNo"].ToString();
            //parameters = Uri.EscapeDataString(parameters);
            String strUrl = "ApplyEmp.aspx?" + parameters;
            Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "window.location='" + strUrl + "';", true);
        }

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MVall.ActiveViewIndex = index;
            Session["index"] = index;
        }

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }

        protected void EmpPhotoUploadFU_Change(object sender, EventArgs e)
        {
            //EmpPhotoUploadCB.Checked = true;
            //EmpPhotoUploadFUName.Text = EmpPhotoUploadFU.FileName.ToString();

        }

        protected void EmpBornProvince_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //暫存功能
        protected void EmpBaseTempSave_Click(object sender, EventArgs e)
        {
            TransferDataToEmpObject();
            SaveEmpObjectToDB(sender, e);
            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
            DESCrypt DES = new DESCrypt();
            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&ApplyKindNo=" + ViewState["ApplyKindNo"].ToString() + "&ApplyWayNo=" + ViewState["ApplyWayNo"].ToString() + "&ApplyAttributeNo=" + ViewState["ApplyAttributeNo"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:var retValue=alert('您的資料已暫存成功!');", true);


        }

        //自評儲存
        protected void SelfReviewSave_Click(object sender, EventArgs e)
        {
            TransferDataToEmpObject();
            SaveEmpObjectToDB(sender, e);
            LoadDataBtn_Click(sender, e);

        }

        public void DrawingChart(object sender, EventArgs e)
        {

            GVTeachEdu.DataBind();
            if (GVTeachEdu.Rows.Count == 0)
                lb_NoTeachEdu.Visible = true;
            else
                lb_NoTeachEdu.Visible = false;
            GVTeachExp.DataBind();
            if (GVTeachExp.Rows.Count == 0)
                lb_NoTeachExp.Visible = true;
            else
                lb_NoTeachExp.Visible = false;
            GVTeachCa.DataBind();
            if (GVTeachCa.Rows.Count == 0)
                lb_NoTeachCa.Visible = true;
            else
                lb_NoTeachCa.Visible = false;
            GVTeachHonour.DataBind();
            if (GVTeachHonour.Rows.Count == 0)
                lb_NoTeachHonour.Visible = true;
            else
                lb_NoTeachHonour.Visible = false;
            GVThesisScore.DataBind();
            if (GVThesisScore.Rows.Count == 0)
                lb_NoThesisScore.Visible = true;
            else
                lb_NoThesisScore.Visible = false;
            GVThesisScoreCoAuthor.DataBind();
            //載入學位論文[論文指導教師,口試委員],著作[迴避名單]
            if (vApplyAudit.AppAttributeNo.Equals(chkDegree) || vApplyAudit.AppAttributeNo.Equals(chkPublication))
            {
                GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
                GVThesisOral.DataBind();
                if (GVThesisOral.Rows.Count == 0)
                    lb_NoThesisOral.Visible = true;
                else
                    lb_NoThesisOral.Visible = false;
            }

            fillTeachEdu = (GVTeachEdu.Rows.Count > 0) ? "100" : "0";

            if (CBNoTeachExp.Checked)
            {
                fillTeachExp = "100";
            }
            else
            {
                fillTeachExp = (GVTeachExp.Rows.Count > 0) ? "100" : "0";
            }
            if (CBNoTeachCa.Checked)
            {
                fillTeachCa = "100";
            }
            else
            {
                fillTeachCa = (GVTeachCa.Rows.Count > 0) ? "100" : "0";
            }
            if (CBNoHonour.Checked)
            {
                fillHounor = "100";
            }
            else
            {
                fillHounor = (GVTeachHonour.Rows.Count > 0) ? "100" : "0";
            }
            if (vApplyAudit.AppResearchYear != null && !vApplyAudit.AppResearchYear.Equals(""))
            {
                fillThesisMom = Convert.ToInt32(vApplyAudit.AppResearchYear);
            }
            else
            {
                fillThesisMom = 7;
            }
            fillThesisSon = GVThesisScore.Rows.Count;
            fillThesis = ((fillThesisSon * 100) / fillThesisMom).ToString();
            if (vApplyAudit.AppAttributeNo.Equals(chkDegree))
            {
                fillThesisOral = GVThesisOral.Rows.Count;
            }
            if (!EmpSelfTeachExperience.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfReach.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfDevelope.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfSpecial.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfImprove.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfContribute.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfCooperate.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfTeachPlan.Text.Equals("")) intfillselfnput++;
            if (!EmpSelfLifePlan.Text.Equals("")) intfillselfnput++;
            fillSelfRate = Math.Round(intfillselfnput / 9d * 100);

        }

        private void TransferDataToEmpObject()
        {
            if (vEmp == null) vEmp = new VEmployeeBase();
            vEmp.EmpIdno = EmpIdno.Text.ToString();
            //vEmp.EmpBirthDay = EmpBirthDay.Text.ToString();
            vEmp.EmpBirthDay = ddl_EmpBirthYear.SelectedValue + ddl_EmpBirthMonth.SelectedValue + ddl_EmpBirthDate.SelectedValue;
            vEmp.EmpPassportNo = EmpPassportNo.Text.ToString();
            vEmp.EmpNameENFirst = EmpNameENFirst.Text.ToString();
            vEmp.EmpNameENLast = EmpNameENLast.Text.ToString();
            vEmp.EmpNameCN = EmpNameCN.Text.ToString();
            vEmp.EmpSex = EmpSex.Text.ToString();
            vEmp.EmpCountry = EmpCountry.Text.ToString();
            vEmp.EmpBornCity = EmpBornCity.Text.ToString();
            vEmp.EmpTelPub = EmpTelPub.Text.ToString();
            vEmp.EmpTelPri = EmpTelPri.Text.ToString();
            vEmp.EmpEmail = EmpEmail.Text.ToString();
            vEmp.EmpTownAddressCode = EmpTownAddressCode.Text.ToString();
            vEmp.EmpTownAddress = EmpTownAddress.Text.ToString();
            vEmp.EmpAddressCode = EmpAddressCode.Text.ToString();
            vEmp.EmpAddress = EmpAddress.Text.ToString();
            vEmp.EmpCell = EmpCell.Text.ToString();
            vEmp.EmpNowJobOrg = EmpNowJobOrg.Text.ToString();
            vEmp.EmpNote = EmpNote.Text.ToString();
            vEmp.EmpExpertResearch = EmpExpertResearch.Text.ToString();
            String identity = Request.QueryString["identity"];
            if (identity != "Manager")
            {
                GetSettings setting = new GetSettings();
                vEmp.EmpEmail = setting.LoginMail;
            }

            if (EmpPhotoUploadFU.HasFile)
            {
                if (EmpPhotoUploadFU.FileName != null && checkName(EmpPhotoUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    EmpPhotoUploadCB.Checked = true;
                    //fileLists.Add(EmpPhotoUploadFU);
                    vEmp.EmpPhotoUploadName = EmpPhotoUploadFU.FileName;
                    EmpPhotoUploadFU.PostedFile.SaveAs(Session["FolderPath"] + EmpPhotoUploadFU.FileName);
                    //vEmp.EmpPhotoUpload = EmpPhotoUploadCB.Checked;
                }
            }
            else
            {
                vEmp.EmpPhotoUploadName = EmpPhotoUploadFUName.Text.ToString();
                //vEmp.EmpPhotoUpload = EmpPhotoUploadCB.Checked;
            }

            if (EmpIdnoUploadFU.HasFile)
            {
                if (EmpIdnoUploadFU.FileName != null && checkName(EmpIdnoUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    EmpIdnoUploadCB.Checked = true;
                    //fileLists.Add(EmpIdnoUploadFU);
                    //vEmp.EmpIdUpload = EmpIdnoUploadCB.Checked;
                    vEmp.EmpIdnoUploadName = EmpIdnoUploadFU.FileName;
                    EmpIdnoUploadFU.PostedFile.SaveAs(Session["FolderPath"] + EmpIdnoUploadFU.FileName);
                }
            }
            else
            {
                vEmp.EmpIdnoUploadName = EmpIdnoUploadFUName.Text.ToString();
                //vEmp.EmpIdUpload = EmpIdnoUploadCB.Checked;
            }


            if (EmpDegreeUploadFU.HasFile)
            {
                if (EmpDegreeUploadFU.FileName != null && checkName(EmpDegreeUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    EmpDegreeUploadCB.Checked = true;
                    //fileLists.Add(EmpDegreeUploadFU);
                    //vEmp.EmpDegreeUpload = EmpDegreeUploadCB.Checked;
                    vEmp.EmpDegreeUploadName = EmpDegreeUploadFU.FileName;
                    EmpDegreeUploadFU.PostedFile.SaveAs(Session["FolderPath"] + EmpDegreeUploadFU.FileName);
                }
            }
            else
            {
                vEmp.EmpDegreeUploadName = EmpDegreeUploadFUName.Text.ToString();
                //vEmp.EmpDegreeUpload = EmpDegreeUploadCB.Checked;
            }
            if (EmpSelfTeachExperience.Text != "")
                vEmp.EmpSelfTeachExperience = EmpSelfTeachExperience.Text.ToString();
            if (EmpSelfReach.Text != "")
                vEmp.EmpSelfReach = EmpSelfReach.Text.ToString();
            if (EmpSelfDevelope.Text != "")
                vEmp.EmpSelfDevelope = EmpSelfDevelope.Text.ToString();
            if (EmpSelfSpecial.Text != "")
                vEmp.EmpSelfSpecial = EmpSelfSpecial.Text.ToString();
            if (EmpSelfImprove.Text != "")
                vEmp.EmpSelfImprove = EmpSelfImprove.Text.ToString();
            if (EmpSelfContribute.Text != "")
                vEmp.EmpSelfContribute = EmpSelfContribute.Text.ToString();
            if (EmpSelfCooperate.Text != "")
                vEmp.EmpSelfCooperate = EmpSelfCooperate.Text.ToString();
            if (EmpSelfTeachPlan.Text != "")
                vEmp.EmpSelfTeachPlan = EmpSelfTeachPlan.Text.ToString();
            if (EmpSelfLifePlan.Text != "")
                vEmp.EmpSelfLifePlan = EmpSelfLifePlan.Text.ToString();




            if (CBNoTeachExp.Checked)
            {
                vEmp.EmpNoTeachExp = true;
                //TeachExpSave.Visible = false;
                //TbTeachExp.Visible = false;
            }
            else
            {
                vEmp.EmpNoTeachExp = false;
                //TeachExpSave.Visible = true;
                //TbTeachExp.Visible = true;
            }
            if (CBNoTeachCa.Checked)
            {
                vEmp.EmpNoTeachCa = true;
                //TeachCaSave.Visible = false;
                //TbTeachCa.Visible = false;
            }
            else
            {
                vEmp.EmpNoTeachCa = false;
                //TeachCaSave.Visible = true;
                //TbTeachCa.Visible = true;
            }
            if (CBNoHonour.Checked)
            {
                vEmp.EmpNoHonour = true;
                //TeachHornourSave.Visible = false;
                //TbHonour.Visible = false;
            }
            else
            {
                vEmp.EmpNoHonour = false;
                //TeachHornourSave.Visible = true;
                //TbHonour.Visible = true;
            }
            //vEmp.EmpStatus = empStatus;
            //vEmp.EmpBuildDate = DateTime.Now;
        }

        //資料存入BackupDB 備份原本資料 送出後異動備份
        private void SaveEmpObjectToBackupDB(object sender, EventArgs e)
        {//2017/01/23修改 因該是TbEmpSn但原為TbAppSn
            VEmployeeBase oldEmp = crudObject.GetEmpBaseObjByEmpSn(TbEmpSn.Text.ToString());
            //資料存入BackupDB 備份原本資料 更新最後異動者
            if (Session["AcctAuditorSnEmpId"] != null && !Session["AcctAuditorSnEmpId"].Equals(""))
            {
                vEmp.UpdateUserId = Session["AcctAuditorSnEmpId"].ToString();
            }
            else
            {
                vEmp.UpdateUserId = Session["EmpIdno"].ToString(); //走不到這邊
            }
            if (crudObject.InsertBackup(oldEmp))
            {
                MessageLabel.Text = "異動資料：成功!!";
            }
            else
            {
                MessageLabel.Text = "異動資料：失敗，請洽資訊人員!!";
            }

            if (VYear.Visible == true && VSemester.Visible == true)
            {
                vApplyAudit.strAppSn = Request["AppSn"];
                vApplyAudit.strAppYear = VYear.Text.Trim();
                vApplyAudit.strAppSemester = VSemester.Text.Trim();
            }
            //Update 原本資料
            if (crudObject.Update(vEmp) && crudObject.UpdateTime(vApplyAudit))
            {
                InsertAppendData();
                MessageLabel.Text = "異動資料：成功!! ";
                ProcessUploadFiles(vEmp.EmpSn);
            }
            else
            {
                MessageLabel.Text = "異動資料：失敗，請洽資訊人員!!";
            }
        }

        //基本資料存入DB
        private void SaveEmpObjectToDB(object sender, EventArgs e)
        {
            vEmp.EmpSn = crudObject.GetEmpBaseEmpSn(vEmp.EmpIdno);
            vEmp.UpdateUserId = vEmp.EmpIdno;
            if (vEmp.EmpSn > 0)
            {
                VEmployeeBase oldEmp = crudObject.GetEmpBaseObjByEmpSn(TbEmpSn.Text.ToString());

                //資料存入BackupDB 備份原本資料 更新最後異動者
                if (oldEmp.EmpStatus)
                { //預設為False送件後才為True
                    vEmp.UpdateUserId = vEmp.EmpIdno;
                    if (crudObject.InsertBackup(oldEmp))
                    {
                        MessageLabel.Text = "異動資料：成功!!";
                    }
                    else
                    {
                        MessageLabel.Text = "異動資料：失敗，請洽資訊人員!!";
                    }
                }

                //Update 原本資料
                if (crudObject.Update(vEmp))
                {
                    InsertAppendData();
                    MessageLabel.Text = "儲存資料：成功!! ";
                    ProcessUploadFiles(vEmp.EmpSn);
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('儲存資料：失敗，請洽資訊人員!!');", true);
                    MessageLabel.Text = "儲存資料：失敗，請洽資訊人員";
                    return;
                }
            }
            else
            {
                vEmp.UpdateUserId = vEmp.EmpIdno;
                crudObject.Insert(vEmp);
                vEmp.EmpSn = crudObject.GetEmpBaseEmpSn(vEmp.EmpIdno);
                if (vEmp.EmpSn > 0)
                {
                    InsertAppendData(); //寫入延伸檔
                    MessageLabel.Text = "儲存資料：成功!!";
                    //處裡檔案上傳
                    ProcessUploadFiles(vEmp.EmpSn);

                    //畫面資料欄未清空
                    //ProcessClearFiled();

                    //LoadDataBtn_Click(sender, e);

                    //建立AccountForAuditor 可login至系統查看


                    //發Email通知 login url 與 帳號密碼
                    //if (String.IsNullOrEmpty(vEmp.EmpEmail))
                    //{
                    //    SendEmailForInfomation(vEmp.EmpEmail);
                    //}
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(Panel1, this.GetType(), "訊息", "javascript:alert('儲存失敗!!');", true);
                    MessageLabel.Text = "儲存資料：失敗，請洽資訊人員";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('儲存資料：失敗，請洽資訊人員!!');", true);
                    return;
                }
            }

        }

        //Email通知 更改密碼
        private void SendEmailForInfomation(string strEmail)
        {

        }

        //寫入 延伸的資料檔
        private void InsertAppendData()
        {
            //判斷 ApplyAudit是否已存在
            vApplyAudit = new VApplyAudit();
            //寫入新聘,升等共用延伸資料檔
            vApplyAudit.EmpSn = vEmp.EmpSn;
            vApplyAudit.EmpIdno = Session["ApplyerID"].ToString();
            vApplyAudit.AppYear = getSettings.NowYear;
            getSettings.Execute();
            vApplyAudit.AppSemester = getSettings.NowSemester;
            vApplyAudit.AppKindNo = ViewState["ApplyKindNo"].ToString(); //新聘
            vApplyAudit.AppWayNo = ViewState["ApplyWayNo"].ToString(); //途徑
            vApplyAudit.AppAttributeNo = ViewState["ApplyAttributeNo"].ToString(); //類型  AppEAttributeNo.SelectedValue.ToString()
            vApplyAudit.AppUnitNo = AppUnitNo.SelectedValue;
            vApplyAudit.AppJobTitleNo = AppJobTitleNo.SelectedValue; //1講師,2助理教授,3副教授,4教授 剛好是法令條款 若有法令異動時UI須修正
            vApplyAudit.AppJobTypeNo = AppJobTypeNo.SelectedValue.ToString();
            vApplyAudit.AppLawNumNo = ELawNum.SelectedValue.ToString();

            vApplyAudit.AppRecommendors = AppRecommendors.Text.ToString();
            //vApplyAudit.AppRecommendYear = AppRecommendYear.Text.ToString();
            vApplyAudit.AppRecommendYear = ddl_AppRecommendYear.SelectedValue.ToString();

            vApplyAudit.AppSelfTeachExperience = EmpSelfTeachExperience.Text.ToString();
            vApplyAudit.AppSelfReach = EmpSelfReach.Text.ToString();
            vApplyAudit.AppSelfDevelope = EmpSelfDevelope.Text.ToString();
            vApplyAudit.AppSelfSpecial = EmpSelfSpecial.Text.ToString();
            vApplyAudit.AppSelfImprove = EmpSelfImprove.Text.ToString();
            vApplyAudit.AppSelfContribute = EmpSelfContribute.Text.ToString();
            vApplyAudit.AppSelfCooperate = EmpSelfCooperate.Text.ToString();
            vApplyAudit.AppSelfTeachPlan = EmpSelfTeachPlan.Text.ToString();
            vApplyAudit.AppSelfLifePlan = EmpSelfLifePlan.Text.ToString();

            //履歷表CV上傳 寫入上傳
            if (AppResumeUploadFU.HasFile)
            {
                if (AppResumeUploadFU.FileName != null && checkName(AppResumeUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    AppResumeUploadCB.Checked = true;
                    //fileLists.Add(AppResumeUploadFU);
                    vApplyAudit.AppResumeUploadName = AppResumeUploadFU.FileName;
                    //vApplyAudit.AppDeclarationUpload = AppDeclarationUploadCB.Checked;
                    AppResumeUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppResumeUploadFU.FileName);

                }
            }
            else
            {
                vApplyAudit.AppResumeUploadName = AppResumeUploadFUName.Text.ToString();
                //vApplyAudit.AppDeclarationUpload = AppDeclarationUploadCB.Checked;
            }

            //論文積分表
            if (ThesisScoreUploadFU.HasFile)
            {
                if (ThesisScoreUploadFU.FileName != null && checkName(ThesisScoreUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    ThesisScoreUploadCB.Checked = true;
                    //fileLists.Add(ThesisScoreUploadFU);
                    vApplyAudit.ThesisScoreUploadName = ThesisScoreUploadFU.FileName;
                    //vApplyAudit.AppDeclarationUpload = AppDeclarationUploadCB.Checked;
                    ThesisScoreUploadFU.PostedFile.SaveAs(Session["FolderPath"] + ThesisScoreUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.ThesisScoreUploadName = ThesisScoreUploadFUName.Text.ToString();
                //vApplyAudit.AppDeclarationUpload = AppDeclarationUploadCB.Checked;
            }

            //推薦函寫入上傳
            if (RecommendUploadFU.HasFile)
            {
                if (RecommendUploadFU.FileName != null && checkName(RecommendUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    RecommendUploadCB.Checked = true;
                    //fileLists.Add(RecommendUploadFU);
                    vApplyAudit.AppRecommendUploadName = RecommendUploadFU.FileName;
                    RecommendUploadFU.PostedFile.SaveAs(Session["FolderPath"] + RecommendUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.AppRecommendUploadName = RecommendUploadFUName.Text.ToString();
                //vEmp.EmpRecommendUpload = RecommendUploadCB.Checked;
            }

            vApplyAudit.AppRecommendors = AppRecommendors.Text.ToString();
            //vApplyAudit.AppRecommendYear = AppRecommendYear.Text.ToString();
            vApplyAudit.AppRecommendYear = ddl_AppRecommendYear.SelectedValue.ToString();

            //教師資格切結書 寫入上傳
            if (AppDeclarationUploadFU.HasFile)
            {
                if (AppDeclarationUploadFU.FileName != null && checkName(AppDeclarationUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    AppDeclarationUploadCB.Checked = true;
                    //fileLists.Add(AppDeclarationUploadFU);
                    vApplyAudit.AppDeclarationUploadName = AppDeclarationUploadFU.FileName;
                    //vApplyAudit.AppDeclarationUpload = AppDeclarationUploadCB.Checked;
                    AppDeclarationUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDeclarationUploadFU.FileName);

                }
            }
            else
            {
                vApplyAudit.AppDeclarationUploadName = AppDeclarationUploadFUName.Text.ToString();
                //vApplyAudit.AppDeclarationUpload = AppDeclarationUploadCB.Checked;
            }

            //服務 寫入上傳
            if (AppOtherServiceUploadFU.HasFile)
            {
                if (AppOtherServiceUploadFU.FileName != null && checkName(AppOtherServiceUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    AppOtherServiceUploadCB.Checked = true;
                    //fileLists.Add(AppOtherServiceUploadFU);
                    vApplyAudit.AppOtherServiceUploadName = AppOtherServiceUploadFU.FileName;
                    AppOtherServiceUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppOtherServiceUploadFU.FileName);
                    //vApplyAudit.AppOtherServiceUpload = AppOtherServiceUploadCB.Checked;
                }
            }
            else
            {
                vApplyAudit.AppOtherServiceUploadName = AppOtherServiceUploadFUName.Text.ToString();
                //vApplyAudit.AppOtherServiceUpload = AppOtherServiceUploadCB.Checked;
            }

            //教學 寫入上傳
            if (AppOtherTeachingUploadFU.HasFile)
            {
                if (AppOtherTeachingUploadFU.FileName != null && checkName(AppOtherTeachingUploadFU.FileName))
                {

                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    AppOtherTeachingUploadCB.Checked = true;
                    //fileLists.Add(AppOtherTeachingUploadFU);
                    vApplyAudit.AppOtherTeachingUploadName = AppOtherTeachingUploadFU.FileName;
                    //vApplyAudit.AppOtherTeachingUpload = AppOtherTeachingUploadCB.Checked;
                    AppOtherTeachingUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppOtherTeachingUploadFU.FileName);

                }
            }
            else
            {
                vApplyAudit.AppOtherTeachingUploadName = AppOtherTeachingUploadFUName.Text.ToString();
                //vApplyAudit.AppOtherTeachingUpload = AppOtherTeachingUploadCB.Checked;
            }

            //醫師證書 寫入上傳
            if (AppDrCaUploadFU.HasFile)
            {
                if (AppDrCaUploadFU.FileName != null && checkName(AppDrCaUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    AppDrCaUploadCB.Checked = true;
                    //fileLists.Add(AppDrCaUploadFU);
                    vApplyAudit.AppDrCaUploadName = AppDrCaUploadFU.FileName;
                    //vApplyAudit.AppDrCaUpload = AppDrCaUploadCB.Checked;
                    AppDrCaUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDrCaUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.AppDrCaUploadName = AppDrCaUploadFUName.Text.ToString();
                //vApplyAudit.AppDrCaUpload = AppDrCaUploadCB.Checked;
            }

            //教育部教師資格證書影 寫入上傳
            if (AppTeacherCaUploadFU.HasFile)
            {
                if (AppTeacherCaUploadFU.FileName != null && checkName(AppTeacherCaUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    AppTeacherCaUploadCB.Checked = true;
                    //fileLists.Add(AppTeacherCaUploadFU);
                    vApplyAudit.AppTeacherCaUploadName = AppTeacherCaUploadFU.FileName;
                    vApplyAudit.AppTeacherCaUpload = AppTeacherCaUploadCB.Checked;
                    AppTeacherCaUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppTeacherCaUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.AppTeacherCaUploadName = AppTeacherCaUploadFUName.Text.ToString();
                //vApplyAudit.AppTeacherCaUpload = AppTeacherCaUploadCB.Checked;
            }


            //計畫主持人 寫入上傳
            if (AppPPMUploadFU.HasFile)
            {
                if (AppPPMUploadFU.FileName != null && checkName(AppPPMUploadFU.FileName))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    AppPPMUploadCB.Checked = true;
                    //fileLists.Add(AppPPMUploadFU);
                    vApplyAudit.AppPPMUploadName = AppPPMUploadFU.FileName;
                    //vApplyAudit.AppPPMUpload = AppPPMUploadCB.Checked;
                    AppPPMUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppPPMUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.AppPPMUploadName = AppPPMUploadFUName.Text.ToString();
                //vApplyAudit.AppPPMUpload = AppPPMUploadCB.Checked;
            }

            //從代表著作頁籤寫入
            //if (!String.IsNullOrEmpty(AppPublication.SelectedItem.ToString()))
            //{
            //    vApplyAudit.AppPublicationUploadName = AppPublication.SelectedItem.ToString();
            //    vApplyAudit.AppPublicationUpload = true;
            //}

            //vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.AppRPIScore].ToString();

            //vApplyAudit.AppResearchYear = AppResearchYear.SelectedValue;
            //vApplyAudit.AppBuildDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);

            //是否已寫入ApplyAudit
            VApplyAudit oldVApplyAudit = null;
            if (!TbAppSn.Text.ToString().Equals(""))
                oldVApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text.ToString()));
            //暫存
            if (Session["AppSn"] != null && oldVApplyAudit == null)
            {
                oldVApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(Session["AppSn"].ToString()));
            }

            if (oldVApplyAudit != null && (!TbAppSn.Text.ToString().Equals("") || Session["AppSn"] != null))
            {
                vApplyAudit.AppSn = oldVApplyAudit.AppSn;
                vApplyAudit.AppStage = oldVApplyAudit.AppStage;
                vApplyAudit.AppStep = oldVApplyAudit.AppStep;
                //資料存入BackupDB 備份原本資料 更新最後異動者
                if (Session["AcctAuditorSnEmpId"] != null && !Session["AcctAuditorSnEmpId"].Equals(""))
                {
                    vApplyAudit.AppUserId = Session["AcctAuditorSnEmpId"].ToString();
                    crudObject.InsertBackup(oldVApplyAudit);
                }
                else
                {
                    vApplyAudit.AppUserId = Session["EmpIdno"].ToString();
                }
                if (!crudObject.Update(vApplyAudit))
                {
                    MessageLabel.Text += "1.共用延伸資料檔更新失敗，請洽資訊人員!";
                }
            }
            else
            {   //用不到
                vApplyAudit.AppStage = "0"; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)            
                vApplyAudit.AppStep = "0";
                if (crudObject.Insert(vApplyAudit, Session["ApplyerID"].ToString()))
                {
                    //寫入成功回傳序號
                    vApplyAudit.AppSn = crudObject.GetApplyAuditAppSn(vEmp.EmpSn);
                }
                else
                {
                    MessageLabel.Text += "1.共用延伸資料檔寫入失敗，請洽資訊人員!";
                }
            }

         
        }


        /**
         * 送出申請
         *1.將EmployeeBase status update 1
         *2.判斷AppEAttributeNo 產生審核檔案 
         **/
        protected void EmpBaseSave_Click(object sender, EventArgs e)
        {
            String strErrMsg = "您所選擇的系所 " + AppUnitNo.SelectedItem.Text.ToString().Trim() + "\\n目前尚未開放申請. 如有問題，請洽人資處承辦人(分機2028或分機2066)";
            CRUDObject crudObject = new CRUDObject();
            VApplyAudit oldVApplyAudit = null;
            oldVApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text.ToString())); //撈取案件狀態是否退回本人 OR 系所是否開啟
            if (crudObject.GetOpenOrCloseUnit(AppUnitNo.SelectedValue.ToString().Trim(), AppJobTypeNo.SelectedValue.ToString())
                || (oldVApplyAudit.AppStage.Equals("2") && oldVApplyAudit.AppStep.Equals("3"))
                || (!oldVApplyAudit.AppStage.Equals("0") && !oldVApplyAudit.AppStep.Equals("0") && oldVApplyAudit.AppStatus.Equals(false)))
            {
                string strPopup = "以下頁籤未完成，請確認欄位填寫完整才能送出：\\n\\n";
                //基本資料通用欄位警示文字
                if (Convert.ToInt32(fillThesis) > 100) fillThesis = "100";
                //臨床新聘
                if (ViewState["ApplyAttributeNo"] != null && ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical)) isThesisNotUpload = false;
                #region 舊版檢測
                //DrawingChart(sender, e);
                ////顯示使用者完成度百分比--未完成者 fillInputRate < 100 || fillSelfRate < 100 || fillCheckBoxRate < 100 ||
                //if (
                //    !fillTeachEdu.Equals("100") || !fillTeachExp.Equals("100") || !fillTeachCa.Equals("100") ||
                //    !fillHounor.Equals("100") ||
                //    ((AppDegreeThesisName.Text.ToString().Equals("") || !AppDegreeThesisUploadCB.Checked) && ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree))
                //    || ((ViewState["ApplyAttributeNo"].ToString().Equals(chkPublication) || ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree)) && fillThesisOral < 1)
                //    || GVThesisScore.Rows.Count > 15 || isThesisNotUpload || (GVThesisScore.Rows.Count == 0 && !ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical)))
                //{


                //    //if (fillInputRate < 100) strPopup += "基本欄位：" + fillInputRate + "%\\n";
                //    if (fillSelfRate < 100) strPopup += "自我評議\\n";
                //    //if (fillCheckBoxRate < 100) strPopup += "上傳檔案：" + fillCheckBoxRate + "%\\n";
                //    if (!fillTeachEdu.Equals("100")) strPopup += "學歷資料\\n"; // +fillTeachEdu + "%\\n";
                //    if (!fillTeachExp.Equals("100")) strPopup += "經歷資料\\n"; // +fillTeachExp + "%\\n";
                //    if (!fillTeachCa.Equals("100")) strPopup += "教師證資料\\n"; // +fillTeachCa + "%\\n";
                //    if (!fillHounor.Equals("100")) strPopup += "學術獎勵、榮譽事項\\n"; //+ fillHounor + "%\\n";
                //                                                               //if (!fillThesis.Equals("100")) strPopup += "上傳論文&積分：實繳篇數/按研究年資應繳篇數 -- " + fillThesisSon + "/" + fillThesisMom + "\\n"; // + fillThesis +"%"
                //    if (ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree))
                //    {
                //        if (AppDegreeThesisName.Text.ToString().Equals(""))
                //        {
                //            strPopup += "學位論文「代表著作」資料：著作題名\\n";
                //        }
                //        if (!AppDegreeThesisUploadCB.Checked)
                //        {
                //            strPopup += "學位論文「代表著作」資料：上傳論文\\n";
                //        }
                //    }
                //    if (ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree))
                //    {
                //        if (fillThesisOral < 1)
                //        {
                //            strPopup += "論文指導教師&口試委員必填\\n";
                //        }
                //    }
                //    if (isThesisNotUpload) strPopup += "研究論文有尚『成果檔案』未上傳\\n";
                //    if (GVThesisScore.Rows.Count == 0 && !ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical)) strPopup += "研究論文積分不得為0篇\\n";
                //    if (GVThesisScore.Rows.Count > 15) strPopup += "研究論文積分不得超過15篇\\n";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strPopup + "');", true);
                //    return;

                //}
                #endregion
                string checkSheet = "";
                checkSheet = checkSheets();
                if (checkSheet != "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strPopup + checkSheet + "');", true);
                    return;
                }
                else
                {
                    #region 檢測無誤送出

                    //再次確認密碼正確才能送出

                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "prompt", "passwordPrompt()", true);

                    //EmpBaseTempSave_Click(sender,e);

                    TransferDataToEmpObject();
                    SaveEmpObjectToDB(sender, e);

                    crudObject = new CRUDObject();
                    VUnit vUnit;
                    VUnit sUnit;
                    VAuditExecute vAuditExecute;
                    VAuditPeroid vAuditPeroid;

                    //取得密碼
                    GeneratorPwd generatorPwd = new GeneratorPwd();
                    string nowTerm;
                    ArrayList arrayList;

                    string strAuditPointDepartment; //是否E0100 醫學系
                    string strAuditPointAttribute;  //是否是著作 2升等 3著作 (著作boolAuditPointAttribute=True)      
                    Boolean boolAuditPointAttribute = false;
                    vUnit = crudObject.GetVUnit(AppUnitNo.SelectedValue); //若是三級單位 往上串一層級 判斷是否是醫學系或其它科系
                    strAuditPointDepartment = vUnit.UntId2;

                    if (!chkUnitNo.Equals(strAuditPointDepartment))//判斷是否是醫學系或其它科系 有無系教評承辦人 有，E0100 無，OTHER
                    {
                        strAuditPointDepartment = "OTHER";
                    }
                    strAuditPointAttribute = AppAttributeNo.SelectedValue.ToString(); //新聘類型
                    if (chkPublication.Equals(strAuditPointAttribute)) boolAuditPointAttribute = true;

                    //撈取樣板產生該申請的簽核檔案 AuditExecute 
                    arrayList = crudObject.GetAllAuditPointRole(strAuditPointDepartment, boolAuditPointAttribute);
                    GetSettings getSettings = new GetSettings();
                    getSettings.Execute();
                    //nowTerm = getSettings.GetSemester(); //1:上學期 2:下學期

                    string tmpUnit;

                    //被退件的教師確認是否已產生稽核檔案:因為申請者被退件重新送件時,不用再次產生VAuditExecute檔
                    if (this.crudObject.GetExecuteAuditorAnyOne(vApplyAudit.AppSn))
                    {
                        //退回本人詳細填寫後重送
                        if (vApplyAudit.AppStage.Equals("2") && vApplyAudit.AppStep.Equals("3"))
                        {
                            vAuditExecute = new VAuditExecute();
                            vAuditExecute = crudObject.GetExecuteReturnSelf(vApplyAudit.AppSn); //只取未完成的第一筆 Submit時被清掉
                            vAuditExecute.ExecuteStatus = true;
                            vAuditExecute.ExecutePass = "1";
                            crudObject.UpdateAuditExecuteStatus(vAuditExecute);
                        }

                    }
                    else
                    {
                        foreach (VAuditPointRole vAuditPointRole in arrayList)
                        {
                            vAuditExecute = new VAuditExecute();
                            vAuditExecute.AppSn = vApplyAudit.AppSn;
                            vAuditExecute.ExecuteRoleName = vAuditPointRole.AuditPointRoleName;
                            vAuditExecute.ExecuteStage = vAuditPointRole.AuditPointStage;
                            vAuditExecute.ExecuteStep = vAuditPointRole.AuditPointStep;

                            //系統自動對應簽核人員 透過部門 與 職階找到對應的主管
                            tmpUnit = "";
                            if (!"".Equals(vAuditPointRole.AuditPointRoleLevel.Trim()))
                            {
                                //三級主管判斷
                                if ("3".Equals(vAuditPointRole.AuditPointRoleLevel.Trim()))
                                {
                                    tmpUnit = vUnit.UntId; //必須在前台選擇學科
                                }
                                //二級主管判斷
                                if ("2".Equals(vAuditPointRole.AuditPointRoleLevel.Trim()))
                                {
                                    tmpUnit = vUnit.UntId2;
                                }
                                //一級主管判斷
                                if ("1".Equals(vAuditPointRole.AuditPointRoleLevel.Trim()))
                                {
                                    tmpUnit = vUnit.DptId;
                                }
                                if (tmpUnit != null || !tmpUnit.Equals(""))
                                {
                                    sUnit = crudObject.GetVUnit(tmpUnit, vAuditPointRole.AuditPointRoleLevel); //撈取第二次
                                    if (sUnit != null)
                                    {
                                        vAuditExecute.ExecuteAuditorSnEmpId = sUnit.UntSupEmpId;
                                        DataTable dt = crudObject.GetEmpNameEmail(sUnit.UntSupEmpId);
                                        if (dt != null)
                                        {
                                            vAuditExecute.ExecuteAuditorName = dt.Rows[0][0].ToString();
                                            vAuditExecute.ExecuteAuditorEmail = sUnit.UntSupEmpEmail;
                                        }
                                    }
                                }
                            }

                            //自動帶入承辦人AppEUnitNo.SelectedValue vAuditPointRole.AuditPointSn
                            if (vAuditPointRole.AuditPointSn.Equals("1") || vAuditPointRole.AuditPointSn.Equals("3"))
                            {
                                DataTable dtRole;
                                //20180730 因E0000醫學院僅存 院經理 & 院教評承辦人(以前只要醫學院皆同一系所承辦人)，所以變更為僅E0000才進行搜尋
                                if ((vAuditPointRole.AuditPointSn.Equals("3")) && AppUnitNo.SelectedValue.Substring(0, 1).Equals("E") && AppUnitNo.SelectedValue.Equals("E0000"))
                                {
                                    dtRole = crudObject.GetUnderTakerByDeptPointSn("E0000", vAuditPointRole.AuditPointSn);
                                }
                                else
                                {
                                    dtRole = crudObject.GetUnderTakerByDeptPointSn(AppUnitNo.SelectedValue, vAuditPointRole.AuditPointSn);
                                }
                                vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                            }
                            if (vAuditPointRole.AuditPointSn.Equals("4") || vAuditPointRole.AuditPointSn.Equals("5"))
                            {
                                DataTable dtRole;
                                //20201022 除通識教育中心外 D2300，其餘院級皆取第一值+0000(ex: E0000、F0000、10000)
                                string dpt_id = AppUnitNo.SelectedValue;
                                if (dpt_id.Substring(0, 3) == "D23")
                                    dpt_id = "D2300";
                                else
                                    dpt_id = dpt_id.Substring(0, 1) + "0000";

                                dtRole = crudObject.GetUnderTakerByDeptPointSn(dpt_id, vAuditPointRole.AuditPointSn);
                                //dtRole = crudObject.GetUnderTakerByDeptPointSn(AppUnitNo.SelectedValue.Substring(0, 1) + "0000", vAuditPointRole.AuditPointSn);
                                if (dtRole != null)
                                {
                                    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                                }
                                //else
                                //{
                                //    dtRole = crudObject.GetUnderTakerByDeptPointSn(AppUnitNo.SelectedValue, vAuditPointRole.AuditPointSn);
                                //    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                //    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                //    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                                //}
                            }

                            //以下Code請參照AuditPointRole AuditPointSn AuditPointStage AuditPointStep
                            //人資處 7 (Stage=4 Step=1) 091058 ; 9 (Stage=4 Step=3) 085020;11 (Stage=7 Step=1) 091058 (Stage=7 Step=2) 085020
                            if (vAuditPointRole.AuditPointSn.Equals("6") || //校教評承辦人
                                vAuditPointRole.AuditPointSn.Equals("7") || //副人資長
                                vAuditPointRole.AuditPointSn.Equals("8") || //人資長
                                vAuditPointRole.AuditPointSn.Equals("9") || //人資處承辦人
                                vAuditPointRole.AuditPointSn.Equals("11") || //校教評承辦人
                                vAuditPointRole.AuditPointSn.Equals("12") || //副人資長
                                vAuditPointRole.AuditPointSn.Equals("13") //人資長
                                )
                            {
                                DataTable dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId(vAuditPointRole.AuditPointRoleLevel);
                                vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                            }

                            //讀取每一簽核人員開放權限的起始與終止日
                            vAuditPeroid = crudObject.GetAuditPeriod(vAuditExecute.ExecuteStage);
                            vAuditExecute.ExecuteBngDate = vAuditPeroid.AuditPeroidBeginDate;
                            vAuditExecute.ExecuteEndDate = vAuditPeroid.AuditPeroidEndDate;
                            vAuditExecute.ExecuteStatus = false;
                            if (vAuditPointRole.AuditPointStage.Equals("2") && vAuditPointRole.AuditPointStep.Equals("3"))
                            {
                                if (!vApplyAudit.AppJobTitleNo.Substring(3, 3).Equals("400") && //臨床教授
                                  !vApplyAudit.AppJobTitleNo.Substring(3, 3).Equals("500") && //專案教授
                                  !vApplyAudit.AppJobTypeNo.Equals("2") && //兼職
                                  !untArray.Contains(vApplyAudit.AppUnitNo)) //醫療學科
                                {
                                    crudObject.Insert(vAuditExecute);
                                }
                                else
                                {
                                    //繁式流程不需重回本人填寫，跳過步驟

                                }
                            }
                            else
                            {
                                crudObject.Insert(vAuditExecute);
                            }
                        }// end foreach

                        //更新 ApplyAudit 中的 審核Stage與Step的狀態
                        //VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                        //vApplyAuditUpdate.AppSn = vApplyAudit.AppSn;
                        //if (chkUnitNo.Equals(strAuditPointDepartment))
                        //{
                        //    vApplyAuditUpdate.AppStage = "1";
                        //    vApplyAuditUpdate.AppStep = "1";
                        //}
                        //else
                        //{
                        //    vApplyAuditUpdate.AppStage = "2";
                        //    vApplyAuditUpdate.AppStep = "1";
                        //}
                        //crudObject.UpdateApplyAuditStageStep(vApplyAuditUpdate);
                    }
                    //送出Email給HR
                    //Email
                    ////////VSendEmail vSendEmail = new VSendEmail();
                    ////////vSendEmail.MailToAccount = "yijhih@tmu.edu.tw";
                    ////////vSendEmail.MailFromAccount = "yijhih@tmu.edu.tw";
                    ////////vSendEmail.MailSubject = vEmp.EmpNameCN + "由「教師聘任升等作業系統」 申請『" + vUnit.UntNameFull + "』系所的教師";
                    ////////vSendEmail.ToAccountName = "系統管理者";
                    ////////vSendEmail.MailContent = "請確認送出新聘申請文件，" + vEmp.EmpNameCN + "的文件已進行簽核！！&nbsp;&nbsp; <a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入查看</a> ";
                    //////////寄發Email通知
                    ////////Mail mail = new Mail();
                    //////////if (mail.SendEmail(vSendEmail))
                    //////////{
                    ////////ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您已成功送出申請，已通知審核者並進入應聘審查流程!');", true);
                    //////////}

                    //送出Email給第一位審查員
                    VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(vApplyAudit.AppSn); //vApplyAudit.AppSn 

                    //////////Email
                    VApplyerData vApplyerData = crudObject.GetApplyDataForEmail(vApplyAudit.AppSn);

                    if (vAuditExecuteNextOne != null && vApplyerData != null)
                    {
                        ////////    vSendEmail = new VSendEmail();
                        ////////    vSendEmail.MailToAccount = vAuditExecuteNextOne.ExecuteAuditorEmail;
                        ////////    vSendEmail.MailFromAccount = "yijhih@tmu.edu.tw";
                        ////////    vSendEmail.MailSubject = "「教師聘任升等作業系統」有申請文件--請盡速簽核";
                        ////////    vSendEmail.ToAccountName = vAuditExecuteNextOne.ExecuteRoleName + " " + vAuditExecuteNextOne.ExecuteAuditorName;
                        try
                        {
                            ////////        int acctSn = 0;
                            ////////        //在AccountForManage是否存在
                            ////////        acctSn = crudObject.GetAccountForManageAcctSn(vAuditExecuteNextOne.ExecuteAuditorEmail);
                            ////////        VAccountForManage vAccountForManage = new VAccountForManage();
                            ////////        if (acctSn == 0)
                            ////////        {
                            ////////            //新增一筆校內管理者資料 權限為A 僅有稽核權限
                            ////////            //判斷若是送給本人詳填資料時，不用寫入稽核權限
                            ////////            if (vAuditExecuteNextOne.ExecuteStage.Equals("2") && vAuditExecuteNextOne.ExecuteStep.Equals("3"))
                            ////////            {

                            ////////            }
                            ////////            else
                            ////////            {
                            ////////                vAccountForManage.AcctEmail = vAuditExecuteNextOne.ExecuteAuditorEmail;
                            ////////                vAccountForManage.AcctEmpId = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                            ////////                vAccountForManage.AcctPassword = "123456";
                            ////////                vAccountForManage.AcctRole = "A";
                            ////////                vAccountForManage.AcctStatus = true;
                            ////////                crudObject.Insert(vAccountForManage);
                            ////////            }
                            ////////        }
                            ////////        else
                            ////////        {
                            ////////            vAccountForManage = crudObject.GetAccountForManage(vAuditExecuteNextOne.ExecuteAuditorEmail);

                            ////////        }
                            ////////        string strTableData = "<table border=1 cellspacing=0>" +
                            ////////        "<tbody><tr>" +
                            ////////        "<th>聘任單位</th><th >姓名</th><th >專兼任別</th><th >應聘等級</th><th >審查性質</th><th >新聘升等</th><th >申請狀態</th></tr>" +
                            ////////        "<tr><td>" + vApplyerData.UntNameFull + "</td><td>" + vApplyerData.EmpNameCN + "</td><td>" + vApplyerData.JobAttrName + "</td><td>" + vApplyerData.JobTitleName + "</td><td>" + vApplyerData.AttributeName + "</td><td>" + vApplyerData.KindName + "</td><td>" + vApplyerData.AuditProgressName + "</td></tr></tbody></table>";
                            ////////        vSendEmail.MailContent = "<br> 「教師聘任升等作業系統」需您的核准!<br><font color=red>請在三天內完成您的審核</font>,申請者才能進入下一階段的審核.<br> " + strTableData + " <br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入審核</a>  您的帳號:" + vAccountForManage.AcctEmail + " 密碼:校內使用的密碼 <br/><br><br><br><br>人資處 林怡慧(2028) 劉伊芝(2066)  <br>";


                            //更新 ApplyAudit 中的 審核Stage與Step的狀態
                            VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                            vApplyAuditUpdate.AppSn = vAuditExecuteNextOne.AppSn;
                            vApplyAuditUpdate.AppStage = vAuditExecuteNextOne.ExecuteStage;
                            vApplyAuditUpdate.AppStep = vAuditExecuteNextOne.ExecuteStep;
                            vApplyAuditUpdate.AppStatus = true;
                            crudObject.UpdateApplyAuditStageStepStatus(vApplyAuditUpdate); //改變申請狀態，完成申請，進入審查流程

                            //更新 EmployeeBase Status 為 True
                            crudObject.UpdateEmployeeStatus(vApplyerData.EmpSn);

                            //////////寄發Email通知
                            ////////mail.SendEmail(vSendEmail);
                        }
                        catch (Exception ex)
                        {
                            MessageLabel.Text = ex.ToString();

                        }
                    }
                    //LoadDataBtn_Click(sender, e);
                    DESCrypt DES = new DESCrypt();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您已成功送出申請，已通知審核者並進入應聘流程!\\n可查看審核狀態!');", true);
                    string parameters = "ApplyerID=" + DES.Encrypt(EmpIdno.Text.ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text.ToString()) + "&ApplyKindNo=" + ViewState["ApplyKindNo"].ToString() + "&ApplyWayNo=" + ViewState["ApplyWayNo"].ToString() + "&ApplyAttributeNo=" + ViewState["ApplyAttributeNo"].ToString();
                    //Response.Redirect("~/ApplyEmp.aspx?" + parameters);   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您已成功送出申請，已通知審核者並進入應聘流程!\\n可查看審核狀態!'); window.location='ApplyEmp.aspx?" + parameters + "';", false);
                    #endregion
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
                return;
            }

        }

        //開放給其他有權限異動者
        protected void EmpBaseModifySave_Click(object sender, EventArgs e)
        {
            TransferDataToEmpObject();
            SaveEmpObjectToBackupDB(sender, e);
        }

        //
        protected void BtnReturnBack_Click(object sender, EventArgs e)
        {
            string parameters = "";
            if (Session["AcctRole"] != null && Session["AcctRole"].ToString().Contains("M"))
            {
                parameters += "EmpSn=" + Session["EmpSn"].ToString();
                parameters += "&AppSn=" + Session["AppSn"].ToString();
                Response.Redirect("~/ManageSetAudit.aspx?" + parameters);
            }
            else
            {
                //判斷是否為多單
                if (Session["ApplyerID"] != null && crudObject.GetApplyListCntByIdno(Session["ApplyerID"].ToString()) > 1)
                {
                    DESCrypt DES = new DESCrypt();
                    if (vApplyAudit != null)
                        parameters = "ApplyerID=" + DES.Encrypt(Session["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=1&ApplyWayNo=" + vApplyAudit.AppWayNo + "&ApplyAttributeNo=" + vApplyAudit.AppAttributeNo;

                    else
                    {
                        parameters = "ApplyerID=" + DES.Encrypt(Session["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=1&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
                    }
                    Response.Redirect("~/ApplyList.aspx?" + parameters); //返回清單
                }
                else
                {
                    Response.Redirect("~/Default.aspx?" + parameters); //返回首頁
                }
            }
        }


        protected void BtnApplyPrint_Click(object sender, EventArgs e)
        {
            string parameters = "EmpSn=" + Session["EmpSn"].ToString();
            parameters += "&AppSn=" + Session["AppSn"].ToString();
            Response.Redirect("~/ApplyPrint.aspx?" + parameters);
            string path = "ApplyPrint.aspx?" + parameters;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + path + "','_blank','toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1,height=800,width=1000') ;", true);
        }

        protected void BtnResumeDownload_Click(object sender, EventArgs e)
        {
            ResumePDF resumePDF = new ResumePDF();
            String strMessage = "";
            resumePDF.Output(this, Int32.Parse(Session["AppSn"].ToString()), ref strMessage);
            if (!strMessage.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('履歷資料下載有問題....請登出後重新登入....');", true);
            }
        }

        //下載論文積分表 ThesisScrore1PDF tsPDF = new ThesisScrore1PDF();
        protected void BtnThesisScore1Download_Click(object sender, EventArgs e)
        {
            String strMessage = "";
            ThesisScore1PDF thesisScrore1PDF = new ThesisScore1PDF();
            thesisScrore1PDF.Output(this, Int32.Parse(Session["AppSn"].ToString()), ref strMessage);
            if (!strMessage.Equals(""))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('論文積分下載有問題....請登出後重新登入....');", true);
            }
        }


        //處裡所有上傳檔案
        protected void ProcessUploadFiles(int subfolder)
        {
            string location = Server.MapPath(Global.FileUpPath + subfolder + "/");
            string strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath + subfolder + "\\";
            //string location = Server.MapPath("../DocuFile/HRApplyDoc/" + subfolder +"/");
            //string strFolderPath = Request.PhysicalApplicationPath + "..\\DocuFile\\HRApplyDoc\\" + subfolder + "\\";
            Session["FolderPath"] = strFolderPath;
            string[] str = { "／", "？", "＊", "［", " ］", "'", "&", "?" };
            bool exists = false;
            //建立使用者所屬資料夾
            DirectoryInfo DIFO = new DirectoryInfo(strFolderPath);
            DIFO.Create();
            string fileName;
            int filesize = 0;
            //upload
            if (fileLists.Count > 0)
            {
                foreach (System.Web.UI.WebControls.FileUpload file in fileLists)
                {
                    try
                    {
                        if (file.PostedFile != null)
                        {
                            fileName = System.IO.Path.GetFileName(file.PostedFile.FileName);
                            //大小限制
                            filesize = file.PostedFile.ContentLength;
                            if (filesize > 30100000 || filesize <= 0)
                            {
                                MessageLabel.Text += "<br>上傳失敗:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;
                                Response.Write("<script>alert('上傳檔案失敗!檔案大小過大，檔案請勿超過20M，請重新上傳');</script>");
                            }
                            if (file.PostedFile.FileName != null)
                                for (int i = 0; i < str.Length; i++)
                                {
                                    exists = file.PostedFile.FileName.Contains(str[i]);
                                    if (exists)
                                    {
                                        Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                                    }
                                }
                            else
                            {
                                file.PostedFile.SaveAs(strFolderPath + fileName);
                                MessageLabel.Text += "<br>上傳成功:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageLabel.Text = ex.ToString();
                        //這邊可加入上傳失敗訊息
                        MessageLabel.Text += "<br>上傳失敗:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;

                    }
                }
                fileLists.Clear();
                //這邊可以再加入上傳成功檔案清單
            }
        }

        //不限制檔案上傳大小
        protected void ProcessUploadFilesBig(int subfolder)
        {
            string location = Server.MapPath(Global.FileUpPath + subfolder + "/");
            string strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath + subfolder + "\\";
            //string location = Server.MapPath("../DocuFile/HRApplyDoc/" + subfolder + "/");
            //string strFolderPath = Request.PhysicalApplicationPath + "..\\DocuFile\\HRApplyDoc\\" + subfolder + "\\";
            Session["FolderPath"] = strFolderPath;
            string[] str = { "／", "？", "＊", "［", " ］", "'", "&", "?" };
            bool exists = false;
            //建立使用者所屬資料夾
            DirectoryInfo DIFO = new DirectoryInfo(strFolderPath);
            DIFO.Create();
            string fileName;
            int filesize = 0;
            //upload
            if (fileLists.Count > 0)
            {
                foreach (System.Web.UI.WebControls.FileUpload file in fileLists)
                {
                    try
                    {
                        if (file.PostedFile != null)
                        {
                            fileName = System.IO.Path.GetFileName(file.PostedFile.FileName);
                            //大小限制
                            filesize = file.PostedFile.ContentLength;
                            if (filesize <= 0)
                            {
                                MessageLabel.Text += "<br>上傳失敗:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;
                                Response.Write("<script>alert('上傳檔案失敗:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType + " 請重新上傳');</script>");
                            }
                            if (file.PostedFile.FileName != null)
                                for (int i = 0; i < str.Length; i++)
                                {
                                    exists = file.PostedFile.FileName.Contains(str[i]);
                                    if (exists)
                                    {
                                        Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                                    }
                                }
                            {
                                file.PostedFile.SaveAs(strFolderPath + fileName);
                                MessageLabel.Text += "<br>上傳成功:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;

                                Response.Write("<script>alert('上傳檔案成功:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType + " 請重新上傳');</script>");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageLabel.Text = ex.ToString();
                        //這邊可加入上傳失敗訊息
                        MessageLabel.Text += "<br>上傳失敗:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;

                        Response.Write("<script>alert('上傳檔案失敗:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType + " 請重新上傳');</script>");
                    }
                }
                fileLists.Clear();
                //這邊可以再加入上傳成功檔案清單
            }
        }

        //查詢已存資料
        protected void LoadDataBtn_Click(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (EmpIdno.Text.Equals(""))
            {
                MessageLabel.Text = "目前無您的資料：請輸入相關資料並開始新聘申請!";
            }
            else
            {


                CRUDObject crudObject = new CRUDObject();
                string tEmpIdno = EmpIdno.Text.ToString();
                VEmployeeBase vEmp;
                vEmp = crudObject.GetEmpBsaseObj(tEmpIdno);
                if (vEmp != null)
                {
                    Session["EmpSn"] = vEmp.EmpSn;
                    //EmpId.Text = vEmp.EmpId;

                    if (vEmp.EmpBirthDay != "" && vEmp.EmpBirthDay.ToString().Length == 7)
                    {
                        ddl_EmpBirthYear.SelectedValue = vEmp.EmpBirthDay.Substring(0, 3);
                        ddl_EmpBirthMonth.SelectedValue = vEmp.EmpBirthDay.Substring(3, 2);
                        ddl_EmpBirthDate.SelectedValue = vEmp.EmpBirthDay.Substring(5, 2);
                        //EmpBirthDay.Text = vEmp.EmpBirthDay;
                    }
                    EmpPassportNo.Text = vEmp.EmpPassportNo;
                    EmpNameENFirst.Text = vEmp.EmpNameENFirst;
                    EmpNameENLast.Text = vEmp.EmpNameENLast;
                    EmpNameCN.Text = vEmp.EmpNameCN;
                    AuditNameCN.Text = vEmp.EmpNameCN;

                    //應徵科系
                    AppUnitNo.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                    AppUnitNo.DataTextField = "unt_name_full";      //在此輸入的是資料表的欄位名稱

                    //Load ApplyAudit來確認是否管理端
                    crudObject.GetApplyAuditObjByIdno(tEmpIdno);
                    if (Session["ManageEmpId"] != null && Session["ManageEmpId"].ToString() != "" || (vApplyAudit.AppStatus != false))
                        AppUnitNo.DataSource = crudObject.GetOpenUnitUp().DefaultView;
                    else
                        AppUnitNo.DataSource = crudObject.GetOpenUnit().DefaultView;
                    AppUnitNo.DataBind();
                    if (vApplyAudit.AppYear != null && vApplyAudit.AppSemester != null)
                    {
                        VYear.Text = vApplyAudit.AppYear;
                        VSemester.Text = vApplyAudit.AppSemester;
                    }
                    EmpCountry.DataBind();
                    EmpBornCity.DataBind();

                    //AppJobTitleNo.DataValueField = "JobTitleNo";
                    //AppJobTitleNo.DataTextField = "JobTitleName";
                    //AppJobTitleNo.DataSource = crudObject.GetAllJobTitleNoName();
                    //AppJobTitleNo.DataBind();


                    AppJobTypeNo.DataValueField = "JobAttrNo";
                    AppJobTypeNo.DataTextField = "JobAttrName";
                    AppJobTypeNo.DataSource = crudObject.GetAllJobTypeNoName();
                    AppJobTypeNo.DataBind();
                    //新聘類型 已具部定教師資格1 學位2 著作3 臨床教師新聘4
                    AppAttributeNo.DataValueField = "status";
                    AppAttributeNo.DataTextField = "note";
                    AppAttributeNo.DataSource = crudObject.GetApplyHrAttribute().DefaultView;
                    AppAttributeNo.DataBind();
                    //AppELawNumNo.DataValueField = "LawItemNo";
                    //AppELawNumNo.DataTextField = "LawItemNoCN";
                    //AppELawNumNo.DataSource = crudObject.GetApplyTeacherLaw(AppEJobTitleNo.SelectedValue.ToString()).DefaultView;
                    //AppELawNumNo.DataBind();
                    //AddELawNumControls(vApplyAudit.AppJobTitleNo, vApplyAudit.AppLawNumNo);

                    //EmpSex.Text = vEmp.EmpSex;
                    //EmpCountry.SelectedValue = vEmp.EmpCountry; 
                    //EmpHomeTown.Text = vEmp.EmpHomeTown;
                    //EmpBornProvince.Text = vEmp.EmpBornCity;
                    //EmpBornCity.Text = vEmp.EmpBornCity;
                    for (int i = 0; i < EmpSex.Items.Count; i++)
                    {
                        if (vEmp.EmpSex.Equals(EmpSex.Items[i].Value))
                        {
                            EmpSex.Items[i].Selected = true;
                        }
                        else
                        {
                            EmpSex.Items[i].Selected = false;
                        }
                    }
                    //國籍
                    if (vEmp.EmpCountry == null || vEmp.EmpCountry.Equals(""))
                    {
                        EmpCountry.Items.Insert(0, "請選擇");
                    }
                    for (int i = 0; i < EmpCountry.Items.Count; i++)
                    {
                        if (vEmp.EmpCountry.Equals(EmpCountry.Items[i].Value))
                        {
                            EmpCountry.Items[i].Selected = true;
                        }
                        else
                        {
                            EmpCountry.Items[i].Selected = false;
                        }
                    }

                    //預設城市
                    if (vEmp.EmpBornCity == null || vEmp.EmpBornCity.Equals(""))
                    {
                        EmpBornCity.Items.Insert(0, "請選擇");
                    }
                    for (int i = 0; i < EmpBornCity.Items.Count; i++)
                    {
                        if (vEmp.EmpBornCity.Equals(EmpBornCity.Items[i].Value))
                        {
                            EmpBornCity.Items[i].Selected = true;
                        }
                        else
                        {
                            EmpBornCity.Items[i].Selected = false;
                        }
                    }


                    EmpTelPub.Text = vEmp.EmpTelPub;
                    EmpTelPri.Text = vEmp.EmpTelPri;
                    EmpEmail.Text = vEmp.EmpEmail;
                    EmpTownAddressCode.Text = vEmp.EmpTownAddressCode;
                    EmpTownAddress.Text = vEmp.EmpTownAddress;
                    EmpAddressCode.Text = vEmp.EmpAddressCode;
                    EmpAddress.Text = vEmp.EmpAddress;
                    EmpCell.Text = vEmp.EmpCell;
                    EmpExpertResearch.Text = vEmp.EmpExpertResearch;
                    String identity = Request.QueryString["identity"];
                    if (identity != "Manager")
                    {
                        GetSettings setting = new GetSettings();
                        EmpEmail.Text = setting.LoginMail;
                    }

                    //string location = "../DocuFile/HRApplyDoc/" + vEmp.EmpSn + "/";
                    string location = Global.FileUpPath + vEmp.EmpSn + "/";

                    //上傳照片
                    //if (vEmp.EmpPhotoUpload)
                    //{
                    //    EmpPhotoUploadCB.Checked = vEmp.EmpPhotoUpload;
                    //}
                    //else
                    //{
                    //    EmpPhotoUploadCB.Checked = true;

                    //}
                    //上傳照片
                    if (vEmp.EmpPhotoUpload) EmpPhotoUploadCB.Checked = vEmp.EmpPhotoUpload;
                    if (vEmp.EmpPhotoUploadName != null && !vEmp.EmpPhotoUploadName.Equals(""))
                    {
                        //EmpPhotoUploadFUName.Text = vEmp.EmpPhotoUploadName;
                        EmpPhotoUploadCB.Checked = true;
                        EmpPhotoImage.ImageUrl = location + vEmp.EmpPhotoUploadName;

                        //file.PostedFile.SaveAs(strFolderPath + fileName);
                        EmpPhotoImage.Visible = true;
                        EmpPhotoUploadFU.Visible = true;
                        EmpPhotoUploadFUName.Text = vEmp.EmpPhotoUploadName;
                    }
                    else
                    {
                        EmpPhotoUploadFU.Visible = true;
                        EmpPhotoImage.Visible = false;
                    }

                    //上傳身分證
                    //if(vEmp.EmpIdUpload)EmpIdUploadCB.Checked = vEmp.EmpIdUpload;
                    if (vEmp.EmpIdnoUploadName != null && !vEmp.EmpIdnoUploadName.Trim().Equals(""))
                    {
                        //EmpIdUploadFUName.Text = vEmp.EmpIdUploadName;
                        EmpIdnoUploadCB.Checked = true;
                        EmpIdnoHyperLink.NavigateUrl = getHyperLink(vEmp.EmpIdnoUploadName);
                        EmpIdnoHyperLink.Text = vEmp.EmpIdnoUploadName;
                        EmpIdnoHyperLink.Visible = true;
                        EmpIdnoUploadFU.Visible = true;
                        EmpIdnoUploadFUName.Text = vEmp.EmpIdnoUploadName;
                    }
                    else
                    {
                        EmpIdnoUploadFU.Visible = true;
                        EmpIdnoHyperLink.Visible = false;
                    }

                    //最高學歷證件上傳 上傳畢業證書
                    if (vEmp.EmpDegreeUploadName != null && !vEmp.EmpDegreeUploadName.Equals(""))
                    {
                        //EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeUploadCB.Checked = true;
                        EmpDegreeHyperLink.NavigateUrl = getHyperLink(vEmp.EmpDegreeUploadName);
                        EmpDegreeHyperLink.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeHyperLink.Visible = true;
                        EmpDegreeUploadFU.Visible = true;
                        EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                    }
                    else
                    {
                        EmpDegreeUploadFU.Visible = true;
                        EmpDegreeHyperLink.Visible = false;
                    }


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

                    EmpNowJobOrg.Text = vEmp.EmpNowJobOrg;
                    EmpNote.Text = vEmp.EmpNote;


                    //TeachExp TeachCa TeachHornour 狀態資料帶出
                    if (!this.IsPostBack)
                    {
                        if (vEmp.EmpNoTeachExp) { CBNoTeachExp.Checked = true; }
                        if (vEmp.EmpNoTeachCa) { CBNoTeachCa.Checked = true; }
                        if (vEmp.EmpNoHonour) { CBNoHonour.Checked = true; }
                    }
                    //載入ApplyAudit共用延伸資料                   

                    if (TbAppSn.Text.ToString().Equals(""))//只有一筆資料或者無資料
                    {
                        vApplyAudit = crudObject.GetApplyAuditObjByIdno(EmpIdno.Text.ToString());
                    }
                    else
                    {
                        vApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text.ToString()));
                    }



                    if (vApplyAudit.ReasearchResultUploadName != null && !vApplyAudit.ReasearchResultUploadName.Equals(""))
                    {
                        link_AppReasearchResult.NavigateUrl = getHyperLink(vApplyAudit.ReasearchResultUploadName);
                        link_AppReasearchResult.Text = vApplyAudit.ReasearchResultUploadName;
                        link_AppReasearchResult.Visible = true;
                    }

                    if (Object.Equals(null, vApplyAudit))
                    {
                        //讀取歷史資料最後一次申請資料
                        vApplyAudit = crudObject.GetApplyAuditObjLastOne(vEmp.EmpSn);
                        if (!Object.Equals(null, vApplyAudit))
                            vApplyAudit.AppStatus = false; //無論前一次狀態是成功或失敗;
                    }

                    //指定單位
                    if (!Object.Equals(null, vApplyAudit))
                    {

                        //論文積分相關
                        Session["ResearchYear"] = vApplyAudit.AppResearchYear;

                        if (Session["ResearchYear"] != null && !Session["ResearchYear"].ToString().Equals(""))
                        {
                            for (int i = 0; i < AppResearchYear.Items.Count - 1; i++)
                            {
                                if (vApplyAudit.AppResearchYear.Equals(AppResearchYear.Items[i].Value))
                                {
                                    AppResearchYear.Items[i].Selected = true;
                                }
                                else
                                {
                                    AppResearchYear.Items[i].Selected = false;
                                }
                            }
                        }

                        //顯示新聘類型 AppEAttributeNo ViewState["ApplyAttributeNo"].ToString()
                        for (int i = 0; i < AppAttributeNo.Items.Count; i++)
                        {
                            if (ViewState["ApplyAttributeNo"].ToString().Equals(AppAttributeNo.Items[i].Value))
                            {
                                AppAttributeNo.Items[i].Selected = true;
                            }
                            else
                            {
                                AppAttributeNo.Items[i].Selected = false;
                            }
                        }



                        //應徵單位 開放時間不存在之單位
                        bool existUnit = false;
                        for (int i = 0; i < AppUnitNo.Items.Count - 1; i++)
                        {
                            if (vApplyAudit.AppUnitNo.Equals(AppUnitNo.Items[i].Value))
                            {
                                AppUnitNo.Items[i].Selected = true;
                                existUnit = true;
                                //應徵單位
                                AuditUnit.Text = crudObject.GetUnitName(AppUnitNo.SelectedValue.ToString()).Rows.Count == 0 ? "" : crudObject.GetUnitName(AppUnitNo.SelectedValue.ToString()).Rows[0][0].ToString();
                            }
                            else
                            {
                                AppUnitNo.Items[i].Selected = false;
                            }
                        }

                        //若該申請人員，本學年新申請，加入請選擇
                        if (crudObject.GetApplyAuditObjByIdno(vApplyAudit.EmpIdno) == null)
                        {
                            AppUnitNo.Items.Insert(0, "請選擇");
                        }
                        else
                        {
                            //萬一開放時間被關閉時
                            if (!existUnit && !vApplyAudit.AppUnitNo.Trim().Equals(""))
                            {
                                string untName = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                                AppUnitNo.Items.Insert(0, new ListItem(untName, vApplyAudit.AppUnitNo));
                                //應徵單位
                                AuditUnit.Text = crudObject.GetUnitName(AppUnitNo.SelectedValue.ToString()).Rows.Count == 0 ? "" : crudObject.GetUnitName(AppUnitNo.SelectedValue.ToString()).Rows[0][0].ToString();
                            }
                        }


                        //
                        //if (!AppUnitNo.SelectedValue.ToString().Equals("請選擇"))
                        //{
                        //    AppJobTitleNo.DataValueField = "JobTitleNo";
                        //    AppJobTitleNo.DataTextField = "JobTitleName";
                        //    AppJobTitleNo.DataSource = crudObject.GetJobTitleOpenDate(AppUnitNo.SelectedValue.ToString()).DefaultView;
                        //    AppJobTitleNo.DataBind();
                        //}
                        //
                        AppJobTitleNo.DataValueField = "JobTitleNo";
                        AppJobTitleNo.DataTextField = "JobTitleName";
                        AppJobTitleNo.DataSource = crudObject.GetAllJobTitleNoName();
                        AppJobTitleNo.DataBind();

                        //應徵職稱
                        if (ViewState["AppJobTitleNo"] == null || ViewState["AppJobTitleNo"].Equals(""))
                        {
                            if (vApplyAudit.AppJobTitleNo == null || vApplyAudit.AppJobTitleNo.Equals(""))
                            {
                                AppJobTitleNo.Items.Insert(0, "請選擇");
                            }
                            else
                            {

                                for (int i = 0; i < AppJobTitleNo.Items.Count; i++)
                                {
                                    if (vApplyAudit.AppJobTitleNo.Equals(AppJobTitleNo.Items[i].Value))
                                    {
                                        AppJobTitleNo.Items[i].Selected = true;
                                        AuditJobTitle.Text = crudObject.GetJobTitleName(AppJobTitleNo.SelectedValue.ToString());
                                    }
                                    else
                                    {
                                        AppJobTitleNo.Items[i].Selected = false;
                                    }
                                }
                                AddELawNumControls(chkApply, vApplyAudit.AppJobTitleNo, vApplyAudit.AppLawNumNo);

                            }
                        }
                        else
                        {
                            for (int i = 0; i < AppJobTitleNo.Items.Count; i++)
                            {
                                if (ViewState["AppJobTitleNo"].ToString().Equals(AppJobTitleNo.Items[i].Value))
                                {
                                    AppJobTitleNo.Items[i].Selected = true;
                                    AuditJobTitle.Text = crudObject.GetJobTitleName(AppJobTitleNo.SelectedValue.ToString());
                                }
                                else
                                {
                                    AppJobTitleNo.Items[i].Selected = false;
                                }
                            }
                            AddELawNumControls(chkApply, ViewState["AppJobTitleNo"].ToString(), vApplyAudit.AppLawNumNo);
                        }

                        //應徵專兼任別--職別
                        if (vApplyAudit.AppJobTypeNo == null || vApplyAudit.AppJobTypeNo.Equals(""))
                        {
                            AppJobTypeNo.Items.Insert(0, "請選擇");
                        }
                        else
                        {
                            for (int i = 0; i < AppJobTypeNo.Items.Count; i++)
                            {
                                if (vApplyAudit.AppJobTypeNo.Equals(AppJobTypeNo.Items[i].Value))
                                {
                                    AppJobTypeNo.Items[i].Selected = true;
                                    AuditJobType.Text = crudObject.GetJobTypeName(AppJobTypeNo.SelectedValue.ToString()).Rows.Count == 0 ? "" : crudObject.GetJobTypeName(AppJobTypeNo.SelectedValue.ToString()).Rows[0][0].ToString();
                                }
                                else
                                {
                                    AppJobTypeNo.Items[i].Selected = false;
                                }
                            }
                        }

                        //兼任 且不是臨床
                        if (AppJobTypeNo.SelectedValue.ToString().Equals("2") && !ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical))
                        {
                            RecommandNote.Text = "(二份)申請口腔醫學院/通識教育中心兼任教師無需檢附 <br/>";
                        }
                        else
                        {
                            RecommandNote.Text = "";
                        }

                        //升等途徑
                        AuditWayName.Text = crudObject.GetAuditWayName(vApplyAudit.AppWayNo).Rows[0][0].ToString();
                        AuditKindName.Text = crudObject.GetKindName(vApplyAudit.AppKindNo).Rows.Count == 0 ? "" : crudObject.GetKindName(vApplyAudit.AppKindNo).Rows[0][0].ToString();
                        AuditAttributeName.Text = (crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows.Count == 0 ? "" : Request.QueryString["ApplyAttributeNo"] == null ? crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows[0][1].ToString() : AppAttributeNo.SelectedItem.ToString());
                        ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
                        //String[] strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());
                        String[] strStatus = { "待送出", "審核中", "審核中", "審核中", "審核中", "審核中", "審核中", "審核中", "審核中", "審核結束" };
                        AuditStatusName.Text = strStatus[Int32.Parse(vApplyAudit.AppStage)];

                        //載入代表著作
                        //string tmp = vApplyAudit.AppPublicationUploadName;
                        //Session["PublicationUploadName"] = tmp;
                        if (GVThesisScore.Rows.Count > 0)
                            AppPThesisAccuScore.Text = vApplyAudit.AppThesisAccuScore;
                        else
                            AppPThesisAccuScore.Text = "0";
                        AppRPI.Text = vApplyAudit.AppRPIScore;


                        //教師資格切結書

                        if (vApplyAudit.AppDeclarationUploadName != null && !vApplyAudit.AppDeclarationUploadName.Equals(""))
                        {
                            AppDeclarationUploadCB.Checked = true;
                            AppDeclarationHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppDeclarationUploadName);
                            AppDeclarationHyperLink.Text = vApplyAudit.AppDeclarationUploadName;
                            AppDeclarationHyperLink.Visible = true;
                            AppDeclarationUploadFU.Visible = true;
                            AppDeclarationUploadFUName.Text = vApplyAudit.AppDeclarationUploadName;
                        }
                        else
                        {
                            AppDeclarationUploadFU.Visible = true;
                            AppDeclarationHyperLink.Visible = false;
                        }

                        //教師履歷CV
                        if (vApplyAudit.AppResumeUploadName != null && !vApplyAudit.AppResumeUploadName.Equals(""))
                        {
                            AppResumeUploadCB.Checked = true;
                            AppResumeHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppResumeUploadName);
                            AppResumeHyperLink.Text = vApplyAudit.AppResumeUploadName;
                            AppResumeHyperLink.Visible = true;
                            AppResumeUploadFU.Visible = true;
                            AppResumeUploadFUName.Text = vApplyAudit.AppResumeUploadName;
                        }
                        else
                        {
                            AppResumeUploadFU.Visible = true;
                            AppResumeHyperLink.Visible = false;
                        }


                        //論文積分表
                        if (vApplyAudit.ThesisScoreUploadName != null && !vApplyAudit.ThesisScoreUploadName.Equals(""))
                        {
                            ThesisScoreUploadCB.Checked = true;

                            ThesisScoreUploadHyperLink.NavigateUrl = getHyperLink(vApplyAudit.ThesisScoreUploadName);
                            ThesisScoreUploadHyperLink.Text = vApplyAudit.ThesisScoreUploadName;
                            ThesisScoreUploadHyperLink.Visible = true;
                            ThesisScoreUploadFU.Visible = true;
                            ThesisScoreUploadFUName.Text = vApplyAudit.ThesisScoreUploadName;
                        }
                        else
                        {
                            ThesisScoreUploadFUName.Visible = true;
                            ThesisScoreUploadHyperLink.Visible = false;
                        }


                        AppRecommendors.Text = vApplyAudit.AppRecommendors;
                        //AppRecommendYear.Text = vApplyAudit.AppRecommendYear;
                        if (vApplyAudit.AppRecommendYear != "")
                            ddl_AppRecommendYear.SelectedValue = vApplyAudit.AppRecommendYear;

                        //下載推薦函
                        if (vApplyAudit.AppRecommendUploadName != null && !vApplyAudit.AppRecommendUploadName.Equals(""))
                        {
                            RecommendUploadCB.Checked = true;
                            RecommendHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppRecommendUploadName);
                            RecommendHyperLink.Text = vApplyAudit.AppRecommendUploadName;
                            RecommendHyperLink.Visible = true;
                            RecommendUploadFU.Visible = true;
                            RecommendUploadFUName.Text = vApplyAudit.AppRecommendUploadName;
                        }
                        else
                        {
                            RecommendUploadFU.Visible = true;
                            RecommendHyperLink.Visible = false;
                        }
                        //載入其他

                        //教學
                        if (vApplyAudit.AppOtherTeachingUpload) AppOtherTeachingUploadCB.Checked = vApplyAudit.AppOtherTeachingUpload;
                        if (vApplyAudit.AppOtherTeachingUploadName != null && !vApplyAudit.AppOtherTeachingUploadName.Equals(""))
                        {
                            OtherTeachingTableRow.Visible = true;
                            AppOtherTeachingUploadCB.Checked = true;
                            AppOtherTeachingHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppOtherTeachingUploadName);
                            AppOtherTeachingHyperLink.Text = vApplyAudit.AppOtherTeachingUploadName;
                            AppOtherTeachingHyperLink.Visible = true;
                            AppOtherTeachingUploadFU.Visible = true;
                            AppOtherTeachingUploadFUName.Text = vApplyAudit.AppOtherTeachingUploadName;
                        }
                        else
                        {
                            AppOtherTeachingUploadFUName.Visible = true;
                            AppOtherTeachingHyperLink.Visible = false;
                        }

                        //服務
                        if (vApplyAudit.AppOtherServiceUploadName != null && !vApplyAudit.AppOtherServiceUploadName.Equals(""))
                        {
                            AppOtherServiceUploadCB.Checked = true;
                            OtherServiceTableRow.Visible = true;
                            vApplyAudit.AppOtherServiceUpload = true;
                            AppOtherServiceHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppOtherServiceUploadName);
                            AppOtherServiceHyperLink.Text = vApplyAudit.AppOtherServiceUploadName;
                            AppOtherServiceHyperLink.Visible = true;
                            AppOtherServiceUploadFU.Visible = true;
                            AppOtherServiceUploadFUName.Text = vApplyAudit.AppOtherServiceUploadName;
                        }
                        else
                        {
                            AppOtherServiceUploadFUName.Visible = true;
                            AppOtherServiceHyperLink.Visible = false;
                        }

                        //下載研究計畫主持人證明

                        if (vApplyAudit.AppPPMUploadName != null && !vApplyAudit.AppPPMUploadName.Equals(""))
                        {
                            AppPPMTableRow.Visible = true;
                            AppPPMUploadCB.Checked = true;
                            AppPPMHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppPPMUploadName);
                            AppPPMHyperLink.Text = vApplyAudit.AppPPMUploadName;
                            AppPPMHyperLink.Visible = true;
                            AppPPMUploadFU.Visible = true;
                            AppPPMUploadFUName.Text = vApplyAudit.AppPPMUploadName;
                        }
                        else
                        {
                            AppPPMUploadFU.Visible = true;
                            AppPPMHyperLink.Visible = false;
                        }

                        //醫師證書
                        if (vApplyAudit.AppDrCaUploadName != null && !vApplyAudit.AppDrCaUploadName.Equals(""))
                        {
                            AppDrCaTableRow.Visible = true;
                            AppDrCaUploadCB.Checked = true;
                            AppDrCaHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppDrCaUploadName);
                            AppDrCaHyperLink.Text = vApplyAudit.AppDrCaUploadName;
                            AppDrCaHyperLink.Visible = true;
                            AppDrCaUploadFU.Visible = true;
                            AppDrCaUploadFUName.Text = vApplyAudit.AppDrCaUploadName;
                        }
                        else
                        {
                            AppDrCaUploadFU.Visible = true;
                            AppDrCaHyperLink.Visible = false;
                        }

                        //教育部教師資格證書影                        
                        if (vApplyAudit.AppTeacherCaUploadName != null && !vApplyAudit.AppTeacherCaUploadName.Equals(""))
                        {
                            //AppTeacherCaTableRow.Visible = true;
                            AppTeacherCaUploadCB.Checked = true;
                            AppTeacherCaHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppTeacherCaUploadName);
                            AppTeacherCaHyperLink.Text = vApplyAudit.AppTeacherCaUploadName;
                            AppTeacherCaHyperLink.Visible = true;
                            AppTeacherCaUploadFU.Visible = true;
                            AppTeacherCaUploadFUName.Text = vApplyAudit.AppTeacherCaUploadName;
                        }
                        else
                        {
                            AppTeacherCaUploadFU.Visible = true;
                            AppTeacherCaHyperLink.Visible = false;
                        }



                        //下載合著人證明(好像真的不用)
                        //AppSummaryCN.Text = vApplyAudit.AppSummaryCN;
                        //if (vApplyAudit.AppCoAuthorUploadName != null && !vApplyAudit.AppCoAuthorUploadName.Trim().Equals(""))
                        //{
                        //    AppCoAuthorUploadCB.Checked = true;
                        //    AppCoAuthorHyperLink.NavigateUrl = location + vApplyAudit.AppCoAuthorUploadName;
                        //    AppCoAuthorHyperLink.Text = vApplyAudit.AppCoAuthorUploadName;
                        //    AppCoAuthorHyperLink.Visible = true;
                        //    AppCoAuthorUploadFU.Visible = true;
                        //    AppCoAuthorUploadFUName.Text = vApplyAudit.AppCoAuthorUploadName;
                        //}
                        //else
                        //{
                        //    AppCoAuthorUploadFU.Visible = true;
                        //    AppCoAuthorHyperLink.Visible = false;
                        //}




                        //這是為了將職稱代碼順利的對應到法規條文 20170508臨床系列皆更動
                        //30000	教授	4
                        //30400	臨床教授	4->1
                        //40000	副教授	3
                        //40400	臨床副教授	3->1
                        //50000	助理教授	2
                        //50400	臨床助理教授	2->1
                        //60000	講師	1
                        //60400	臨床講師	1
                        //轉換法規條文邏輯 代碼取第一位數 用7 去減

                        //法規第幾項
                        String[] num = { "零", "一", "二", "三", "四", "五" };

                        if (!AppJobTitleNo.SelectedValue.ToString().Equals("") && !AppJobTitleNo.SelectedValue.ToString().Equals("請選擇"))
                        {
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("030400"))
                            {
                                ItemNo.Text = num[1];
                                lbchose.Text = "依臨床教師聘任辦法第(三";
                            }
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("040400"))
                            {
                                ItemNo.Text = num[1];
                                lbchose.Text = "依臨床教師聘任辦法第(四";
                            }
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("050400"))
                            {
                                ItemNo.Text = num[1];
                                lbchose.Text = "依臨床教師聘任辦法第(五";
                            }
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("060400"))
                            {
                                lbchose.Text = "依臨床教師聘任辦法第(六";
                                ItemNo.Text = num[0];
                            }
                            if (!AppJobTitleNo.SelectedValue.ToString().Equals("030400") &&
                               !AppJobTitleNo.SelectedValue.ToString().Equals("040400") &&
                               !AppJobTitleNo.SelectedValue.ToString().Equals("050400") &&
                               !AppJobTitleNo.SelectedValue.ToString().Equals("060400"))
                                ItemNo.Text = num[(7 - Int32.Parse(AppJobTitleNo.SelectedValue.ToString().Substring(1, 1)))];
                        }


                        //法規第幾款
                        ItemLabel.Text = num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)];


                        //載入法規依據第幾條
                        //for (int i = 0; i < AppELawNumNo.Items.Count - 1; i++)
                        //{
                        //    if (vApplyAudit.AppLawNumNo.Equals(AppELawNumNo.Items[i].Value))
                        //    {
                        //        AppELawNumNo.Items[i].Selected = true;
                        //    }
                        //    else
                        //    {
                        //        AppELawNumNo.Items[i].Selected = false;
                        //    }
                        //}

                        //法規說明
                        //string law = "";
                        //law = crudObject.GetApplyTeacherLaw(AppEJobTitleNo.SelectedValue.ToString(), AppELawNumNo.SelectedValue.ToString());
                        //if (law.Length > 49)
                        //{
                        //    law = law.Substring(0, 49) + "<br/>" + law.Substring(50);
                        //}

                        //AppLawNumMessage.Text = law;



                        VAppendDegree vAppendDegree = new VAppendDegree();
                        //載入學位論文
                        if (vApplyAudit.AppKindNo.Equals(chkApply) && AppAttributeNo.SelectedValue.Equals(chkDegree))
                        {
                            vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                            if (vAppendDegree != null)
                            {
                                AppDegreeThesisName.Text = vAppendDegree.AppDDegreeThesisName;
                                AppDegreeThesisNameEng.Text = vAppendDegree.AppDDegreeThesisNameEng;
                                if (vAppendDegree.AppDDegreeThesisUploadName != null && !vAppendDegree.AppDDegreeThesisUploadName.Equals(""))
                                {
                                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                    AppDegreeThesisUploadCB.Checked = true;
                                    AppDegreeThesisHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDDegreeThesisUploadName);
                                    AppDegreeThesisHyperLink.Text = vAppendDegree.AppDDegreeThesisUploadName;
                                    AppDegreeThesisHyperLink.Visible = true;
                                    AppDegreeThesisUploadFU.Visible = true;
                                    AppDegreeThesisUploadFUName.Text = vAppendDegree.AppDDegreeThesisUploadName;
                                    if (AppDegreeThesisUploadFU.HasFile)
                                        AppDegreeThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDegreeThesisUploadFU.FileName);

                                }
                                else
                                {
                                    AppDegreeThesisUploadFU.Visible = true;
                                    AppDegreeThesisHyperLink.Visible = false;
                                }
                            }

                            //載入外國學歷 以學位送審才需要
                            if (switchFgn != null && !switchFgn.Equals(""))
                            {
                                LoadFgn(vAppendDegree, switchFgn, location);
                            }
                        }

                        try
                        {
                            DataTable dt = crudObject.GetThesisScoreCount(vEmp.EmpIdno);
                            if (dt.Rows.Count > 0)
                            {
                                //載入SCI Score
                                txt_FSCI.Text = dt.Rows[0]["PT_FSCI"].ToString();
                                txt_FSSCI.Text = dt.Rows[0]["PT_FSSCI"].ToString();
                                txt_FSEI.Text = dt.Rows[0]["PT_FEI"].ToString();
                                txt_FNSCI.Text = dt.Rows[0]["PT_FNSCI"].ToString();
                                txt_FSOther.Text = dt.Rows[0]["PT_FOther"].ToString();

                                txt_NFCSCI.Text = dt.Rows[0]["PT_NFCSCI"].ToString();
                                txt_NFCSSCI.Text = dt.Rows[0]["PT_NFCSSCI"].ToString();
                                txt_NFCEI.Text = dt.Rows[0]["PT_NFCEI"].ToString();
                                txt_NFCNSCI.Text = dt.Rows[0]["PT_NFCNSCI"].ToString();
                                txt_NFCOther.Text = dt.Rows[0]["PT_NFCOther"].ToString();

                                txt_NFOCSCI.Text = dt.Rows[0]["PT_NFOCSCI"].ToString();
                                txt_NFOCSSCI.Text = dt.Rows[0]["PT_NFOCSSCI"].ToString();
                                txt_NFOCEI.Text = dt.Rows[0]["PT_NFOCEI"].ToString();
                                txt_NFOCNSCI.Text = dt.Rows[0]["PT_NFOCNSCI"].ToString();
                                txt_NFOCOther.Text = dt.Rows[0]["PT_NFOCOther"].ToString();
                            }

                            txt_SCI.Text = Convert.ToString(Convert.ToUInt16(txt_FSCI.Text) + Convert.ToUInt16(txt_NFCSCI.Text) + Convert.ToUInt16(txt_NFOCSCI.Text));
                            txt_SSCI.Text = Convert.ToString(Convert.ToUInt16(txt_FSSCI.Text) + Convert.ToUInt16(txt_NFCSSCI.Text) + Convert.ToUInt16(txt_NFOCSSCI.Text));
                            txt_EI.Text = Convert.ToString(Convert.ToUInt16(txt_FSEI.Text) + Convert.ToUInt16(txt_NFCEI.Text) + Convert.ToUInt16(txt_NFOCEI.Text));
                            txt_NSCI.Text = Convert.ToString(Convert.ToUInt16(txt_FNSCI.Text) + Convert.ToUInt16(txt_NFCNSCI.Text) + Convert.ToUInt16(txt_NFOCNSCI.Text));
                            txt_Others.Text = Convert.ToString(Convert.ToUInt16(txt_FSOther.Text) + Convert.ToUInt16(txt_NFCOther.Text) + Convert.ToUInt16(txt_NFOCOther.Text));
                        }
                        catch
                        {

                        }

                        if (Session["AppSn"] != null && !Session["AppSn"].ToString().Equals(""))
                        {
                            GVThesisOral.DataSource = crudObject.GetThesisOralList(Int32.Parse(Session["AppSn"].ToString()));
                            GVThesisOral.DataBind();
                            if (GVThesisOral.Rows.Count == 0)
                                lb_NoThesisOral.Visible = true;
                            else
                                lb_NoThesisOral.Visible = false;
                        }

                        //計算完成度
                        //fillTeachEdu = (GVTeachEdu.Rows.Count > 1) ? "100" : "50";
                        //fillTeachExp = (GVTeachExp.Rows.Count > 1) ? "100" : "50";
                        //fillTeachCa = (GVTeachCa.Rows.Count > 0) ? "100" : "0";
                        //fillHounor = (GVTeachHonour.Rows.Count > 0) ? "100" : "0";
                        //fillThesis = (GVThesisScore.Rows.Count > 3) ? "100" : "75";
                        //fillThesis = (GVThesisScore.Rows.Count > 1) ? "50" : "25";

                        DrawingChart(sender, e);

                    }

                    //

                    MessageLabel.Text += "您的基本資料載入!!";

                    ////顯示目前簽核狀態
                    //if (vApplyAudit != null && vApplyAudit.AppStatus)
                    //{
                    //    //送出 Button Enabled false
                    //    EmpBaseSave.Enabled = false;
                    //    EmpBaseTempSave.Enabled = false;
                    //    Reminder.Visible = false;
                    //    SelfReviewSave.Enabled = false;

                    //    //判斷有權限修改
                    //    if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                    //    {
                    //        TeachEduSave.Enabled = true;
                    //        TeachExpSave.Enabled = true;
                    //        TeachCaSave.Enabled = true;
                    //        TeachHornourSave.Enabled = true;
                    //        ThesisScoreSave.Enabled = true;
                    //        ThesisOralSave.Enabled = true;
                    //        DegreeThesisSave.Enabled = true;
                    //    }
                    //    else
                    //    {
                    //        //送出後才無法修改
                    //        if (vApplyAudit.AppStatus)
                    //        {
                    //            TeachEduSave.Enabled = false;
                    //            TeachExpSave.Enabled = false;
                    //            TeachCaSave.Enabled = false;
                    //            TeachHornourSave.Enabled = false;
                    //            ThesisScoreSave.Enabled = false;
                    //            ThesisOralSave.Enabled = false;
                    //            DegreeThesisSave.Enabled = false;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    EmpBaseSave.Enabled = true;
                    //}
                    //若在系統開放期間
                    if (crudObject.GetDuringOpenDate("1"))
                    {
                        //核對現在是否開放的系所
                        DataTable dt = crudObject.GetOpenUnit();


                        //顯示目前簽核狀態
                        if (vApplyAudit != null && vApplyAudit.AppStatus)
                        {

                            //送出 Button Enabled false
                            if (vApplyAudit.AppStatus)
                            {
                                EmpBaseSave.Enabled = false;
                                EmpBaseTempSave.Enabled = false;
                                SelfReviewSave.Enabled = false;
                                TeachEduSave.Enabled = false;
                                TeachExpSave.Enabled = false;
                                TeachCaSave.Enabled = false;
                                TeachHornourSave.Enabled = false;
                                ThesisScoreSave.Enabled = false;
                                ThesisOralSave.Enabled = false;
                                DegreeThesisSave.Enabled = false;
                            }
                            EmpBaseSave.Enabled = true;
                            Boolean flag = false;
                            for (int i = 0; i < dt.Rows.Count; i++) //現在開放的系所
                            {
                                if (vApplyAudit.AppUnitNo.Equals(dt.Rows[i]["unt_id"].ToString()))
                                {
                                    EmpBaseSave.Enabled = true;
                                    EmpBaseTempSave.Enabled = true;
                                    SelfReviewSave.Enabled = true;
                                    TeachEduSave.Enabled = true;
                                    TeachExpSave.Enabled = true;
                                    TeachCaSave.Enabled = true;
                                    TeachHornourSave.Enabled = true;
                                    ThesisScoreSave.Enabled = true;
                                    ThesisOralSave.Enabled = true;
                                    DegreeThesisSave.Enabled = true;
                                    flag = true;
                                }
                            }
                            if (crudObject.IsAuditExecuteReturn(vApplyAudit.AppSn) && !vApplyAudit.AppStatus) //目前退回且未送出
                            {
                                EmpBaseSave.Enabled = true;
                                EmpBaseTempSave.Enabled = true;
                                SelfReviewSave.Enabled = true;
                                TeachEduSave.Enabled = true;
                                TeachExpSave.Enabled = true;
                                TeachCaSave.Enabled = true;
                                TeachHornourSave.Enabled = true;
                                ThesisScoreSave.Enabled = true;
                                ThesisOralSave.Enabled = true;
                                DegreeThesisSave.Enabled = true;
                            }
                            else
                            {
                                if (flag && !vApplyAudit.AppStatus) //現在開放且未送出
                                {
                                    EmpBaseSave.Enabled = true;
                                    EmpBaseTempSave.Enabled = true;
                                    SelfReviewSave.Enabled = true;
                                    TeachEduSave.Enabled = true;
                                    TeachExpSave.Enabled = true;
                                    TeachCaSave.Enabled = true;
                                    TeachHornourSave.Enabled = true;
                                    ThesisScoreSave.Enabled = true;
                                    ThesisOralSave.Enabled = true;
                                    DegreeThesisSave.Enabled = true;
                                }
                                else
                                {
                                    EmpBaseSave.Enabled = false;
                                    EmpBaseTempSave.Enabled = false;
                                    SelfReviewSave.Enabled = false;
                                    TeachEduSave.Enabled = false;
                                    TeachExpSave.Enabled = false;
                                    TeachCaSave.Enabled = false;
                                    TeachHornourSave.Enabled = false;
                                    ThesisScoreSave.Enabled = false;
                                    ThesisOralSave.Enabled = false;
                                    DegreeThesisSave.Enabled = false;

                                }
                            }
                        }
                    }
                    else
                    {
                        //若撈取審核資料中有退件者 Status = '3'
                        if (crudObject.IsAuditExecuteReturn(vApplyAudit.AppSn))
                        {
                            EmpBaseSave.Enabled = true;
                            EmpBaseTempSave.Enabled = true;
                            SelfReviewSave.Enabled = true;
                            TeachEduSave.Enabled = true;
                            TeachExpSave.Enabled = true;
                            TeachCaSave.Enabled = true;
                            TeachHornourSave.Enabled = true;
                            ThesisScoreSave.Enabled = true;
                            ThesisOralSave.Enabled = true;
                            DegreeThesisSave.Enabled = true;
                        }
                        else
                        {
                            EmpBaseSave.Enabled = false;
                            EmpBaseTempSave.Enabled = false;
                            SelfReviewSave.Enabled = false;
                            TeachEduSave.Enabled = false;
                            TeachExpSave.Enabled = false;
                            TeachCaSave.Enabled = false;
                            TeachHornourSave.Enabled = false;
                            ThesisScoreSave.Enabled = false;
                            ThesisOralSave.Enabled = false;
                            DegreeThesisSave.Enabled = false;
                        }
                    }
                    //判斷有權限修改
                    if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                    {
                        EmpBaseSave.Visible = false;
                        EmpBaseTempSave.Visible = false;
                        SelfReviewSave.Enabled = true;
                        TeachEduSave.Enabled = true;
                        TeachExpSave.Enabled = true;
                        TeachCaSave.Enabled = true;
                        TeachHornourSave.Enabled = true;
                        ThesisScoreSave.Enabled = true;
                        ThesisOralSave.Enabled = true;
                        DegreeThesisSave.Enabled = true;
                        BtnApplyMore.Visible = false;
                        Reminder.Visible = false;
                    }
                    //DegreeThesisSave.Enabled = true;
                    //ThesisOralSave.Enabled = true;
                    //EmpBaseSave.Enabled = true;
                    //EmpBaseTempSave.Enabled = true;
                    //TeachEduSave.Enabled = true;
                    //ThesisScoreSave.Enabled = true;
                    if (vApplyAudit.AppStage.Equals("2") && vApplyAudit.AppStep.Equals("3")) //退回本人詳填
                    {
                        EmpBaseSave.Enabled = true;
                        EmpBaseTempSave.Enabled = true;
                        SelfReviewSave.Enabled = true;
                        TeachEduSave.Enabled = true;
                        TeachExpSave.Enabled = true;
                        TeachCaSave.Enabled = true;
                        TeachHornourSave.Enabled = true;
                        ThesisScoreSave.Enabled = true;
                        ThesisOralSave.Enabled = true;
                        DegreeThesisSave.Enabled = true;
                    }
                }
                else
                {
                    MessageLabel.Text = "抱歉，您未曾申請過資料!!";
                }
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
                if (vAppendDegree != null)
                {
                    AppDFgnEduDeptSchoolAdmitCB.Checked = vAppendDegree.AppDFgnEduDeptSchoolAdmit;

                    //國外最高學位畢業證書
                    if (vAppendDegree.AppDFgnDegreeUploadName != null && !vAppendDegree.AppDFgnDegreeUploadName.Equals(""))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnDegreeUploadCB.Checked = true;
                        AppDFgnDegreeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnDegreeUploadName);
                        AppDFgnDegreeHyperLink.Text = vAppendDegree.AppDFgnDegreeUploadName;
                        AppDFgnDegreeHyperLink.Visible = true;
                        AppDFgnDegreeUploadFU.Visible = true;
                        AppDFgnDegreeUploadFUName.Text = vAppendDegree.AppDFgnDegreeUploadName;
                        if (!String.IsNullOrEmpty(AppDFgnDegreeUploadFU.FileName))
                            AppDFgnDegreeUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnDegreeUploadFU.FileName);
                    }
                    else
                    {
                        AppDFgnDegreeUploadFU.Visible = true;
                        AppDFgnDegreeHyperLink.Visible = false;
                    }

                    //國外學校歷年成績單
                    AppDFgnGradeUploadCB.Checked = vAppendDegree.AppDFgnGradeUpload;
                    if (vAppendDegree.AppDFgnGradeUploadName != null && !vAppendDegree.AppDFgnGradeUploadName.Equals(""))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnGradeUploadCB.Checked = true;
                        AppDFgnGradeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnGradeUploadName);
                        AppDFgnGradeHyperLink.Text = vAppendDegree.AppDFgnGradeUploadName;
                        AppDFgnGradeHyperLink.Visible = true;
                        AppDFgnGradeUploadFU.Visible = true;
                        AppDFgnGradeUploadFUName.Text = vAppendDegree.AppDFgnGradeUploadName;
                        if (!String.IsNullOrEmpty(AppDFgnGradeUploadFU.FileName))
                            AppDFgnGradeUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnGradeUploadFU.FileName);
                    }
                    else
                    {
                        AppDFgnGradeUploadFU.Visible = true;
                        AppDFgnGradeHyperLink.Visible = false;
                    }

                    //國外學歷修業情形一覽表
                    AppDFgnSelectCourseUploadCB.Checked = vAppendDegree.AppDFgnSelectCourseUpload;
                    if (vAppendDegree.AppDFgnSelectCourseUploadName != null && !vAppendDegree.AppDFgnSelectCourseUploadName.Equals(""))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnSelectCourseUploadCB.Checked = true;
                        AppDFgnSelectCourseHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnSelectCourseUploadName);
                        AppDFgnSelectCourseHyperLink.Text = vAppendDegree.AppDFgnSelectCourseUploadName;
                        AppDFgnSelectCourseHyperLink.Visible = true;
                        AppDFgnSelectCourseUploadFU.Visible = true;
                        AppDFgnSelectCourseUploadFUName.Text = vAppendDegree.AppDFgnSelectCourseUploadName;

                        if (!String.IsNullOrEmpty(AppDFgnSelectCourseUploadFU.FileName))
                            AppDFgnSelectCourseUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnSelectCourseUploadFU.FileName);
                    }
                    else
                    {
                        AppDFgnSelectCourseUploadFU.Visible = true;
                        AppDFgnSelectCourseHyperLink.Visible = false;
                    }

                    //個人出入境紀錄
                    AppDFgnEDRecordUploadCB.Checked = vAppendDegree.AppDFgnEDRecordUpload;
                    if (vAppendDegree.AppDFgnEDRecordUploadName != null && !vAppendDegree.AppDFgnEDRecordUploadName.Equals(""))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnEDRecordUploadCB.Checked = true;
                        AppDFgnEDRecordHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnEDRecordUploadName);
                        AppDFgnEDRecordHyperLink.Text = vAppendDegree.AppDFgnEDRecordUploadName;
                        AppDFgnEDRecordHyperLink.Visible = true;
                        AppDFgnEDRecordUploadFU.Visible = true;
                        AppDFgnEDRecordUploadFUName.Text = vAppendDegree.AppDFgnEDRecordUploadName;
                        if (!String.IsNullOrEmpty(AppDFgnEDRecordUploadFU.FileName))
                            AppDFgnEDRecordUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnEDRecordUploadFU.FileName);
                    }
                    else
                    {
                        AppDFgnEDRecordUploadFU.Visible = true;
                        AppDFgnEDRecordHyperLink.Visible = false;
                    }


                    if ("JPN".Equals(switchFgn))
                    {
                        switchFgn = "JPN";

                        //A.入學許可註冊證
                        if (vAppendDegree.AppDFgnJPAdmissionUploadName != null && !vAppendDegree.AppDFgnJPAdmissionUploadName.Equals(""))
                        {
                            AppDFgnJPAdmissionUploadCB.Checked = true;
                            AppDFgnJPAdmissionHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPAdmissionUploadName);
                            AppDFgnJPAdmissionHyperLink.Text = vAppendDegree.AppDFgnJPAdmissionUploadName;
                            AppDFgnJPAdmissionHyperLink.Visible = true;
                            AppDFgnJPAdmissionUploadFU.Visible = true;
                            AppDFgnJPAdmissionUploadFUName.Text = vAppendDegree.AppDFgnJPAdmissionUploadName;
                        }
                        else
                        {
                            AppDFgnJPAdmissionUploadFU.Visible = true;
                            AppDFgnJPAdmissionHyperLink.Visible = false;
                        }

                        //B.修畢學分成績單

                        if (vAppendDegree.AppDFgnJPGradeUploadName != null && !vAppendDegree.AppDFgnJPGradeUploadName.Equals(""))
                        {
                            AppDFgnJPGradeUploadCB.Checked = true;
                            AppDFgnJPGradeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPGradeUploadName);
                            AppDFgnJPGradeHyperLink.Text = vAppendDegree.AppDFgnJPGradeUploadName;
                            AppDFgnJPGradeHyperLink.Visible = true;
                            AppDFgnJPGradeUploadFU.Visible = true;
                            AppDFgnJPGradeUploadFUName.Text = vAppendDegree.AppDFgnJPGradeUploadName;
                        }
                        else
                        {
                            AppDFgnJPGradeUploadFU.Visible = true;
                            AppDFgnJPGradeHyperLink.Visible = false;
                        }

                        //C.在學證明及修業年數證明
                        AppDFgnJPEnrollCAUploadCB.Checked = vAppendDegree.AppDFgnJPEnrollCAUpload;
                        if (vAppendDegree.AppDFgnJPEnrollCAUploadName != null && !vAppendDegree.AppDFgnJPEnrollCAUploadName.Equals(""))
                        {
                            AppDFgnJPEnrollCAUploadCB.Checked = true;
                            AppDFgnJPEnrollCAHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPEnrollCAUploadName);
                            AppDFgnJPEnrollCAHyperLink.Text = vAppendDegree.AppDFgnJPEnrollCAUploadName;
                            AppDFgnJPEnrollCAHyperLink.Visible = true;
                            AppDFgnJPEnrollCAUploadFU.Visible = true;
                            AppDFgnJPEnrollCAUploadFUName.Text = vAppendDegree.AppDFgnJPEnrollCAUploadName;
                        }
                        else
                        {
                            AppDFgnJPEnrollCAUploadFU.Visible = true;
                            AppDFgnJPEnrollCAHyperLink.Visible = false;
                        }

                        //D.通過論文資格考試證明
                        AppDFgnJPDissertationPassUploadCB.Checked = vAppendDegree.AppDFgnJPDissertationPassUpload;
                        if (vAppendDegree.AppDFgnJPDissertationPassUploadName != null && !vAppendDegree.AppDFgnJPDissertationPassUploadName.Equals(""))
                        {
                            AppDFgnJPDissertationPassUploadCB.Checked = true;
                            AppDFgnJPDissertationPassHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPDissertationPassUploadName);
                            AppDFgnJPDissertationPassHyperLink.Text = vAppendDegree.AppDFgnJPDissertationPassUploadName;
                            AppDFgnJPDissertationPassHyperLink.Visible = true;
                            AppDFgnJPDissertationPassUploadFU.Visible = true;
                            AppDFgnJPDissertationPassUploadFUName.Text = vAppendDegree.AppDFgnJPDissertationPassUploadName;
                        }
                        else
                        {
                            AppDFgnJPDissertationPassUploadFU.Visible = true;
                            AppDFgnJPDissertationPassHyperLink.Visible = false;
                        }
                    }
                }//end of vAppendDegree
            }//end of TWN
        }

        //寫入 教師學歷資料(必填)
        protected void TeachEduSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (TeachEduSchool.Text == "" || TeachEduDepartment.Text == "")
                {
                    if (TeachEduSchool.Text == "")
                        msg += "學校名稱未填寫! ";
                    if (TeachEduDepartment.Text == "")
                        msg += "系所名稱為填寫! ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachEdu').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherEdu vTeachEdu = new VTeacherEdu();
                    vTeachEdu.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vTeachEdu.EduLocal = TeachEduLocal.SelectedValue.ToString();
                    vTeachEdu.EduSchool = TeachEduSchool.Text.ToString();
                    vTeachEdu.EduDepartment = TeachEduDepartment.Text.ToString();
                    vTeachEdu.EduDegree = TeachEduDegree.SelectedValue.ToString();
                    //vTeachEdu.EduStartYM = TeachEduStartYM.Text.ToString();
                    vTeachEdu.EduStartYM = ddl_TeachEduStartYear.SelectedValue + ddl_TeachEduStartMonth.SelectedValue;
                    //vTeachEdu.EduEndYM = TeachEduEndYM.Text.ToString();
                    vTeachEdu.EduEndYM = ddl_TeachEduEndYear.SelectedValue + ddl_TeachEduEndMonth.SelectedValue;
                    vTeachEdu.EduDegreeType = TeachEduDegreeType.SelectedValue.ToString();
                    CRUDObject crudObject = new CRUDObject();
                    if (crudObject.Insert(vTeachEdu))
                    {
                        msg = "新增成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }
                    TeachEduSave.Visible = true;
                    TeachEduUpdate.Visible = false;
                    TeachEduSchool.Text = "";
                    TeachEduDepartment.Text = "";
                    //TeachEduStartYM.Text = "";
                    //TeachEduEndYM.Text = "";
                    TeachEduLocal.DataBind();
                    TeachEduDegree.DataBind();
                    TeachEduDegreeType.DataBind();
                    //寫入後載入下方的DataGridView
                    GVTeachEdu.DataBind();
                    if (GVTeachEdu.Rows.Count == 0)
                        lb_NoTeachEdu.Visible = true;
                    else
                        lb_NoTeachEdu.Visible = false;
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        //載入修改資料 教師學歷資料
        protected void TeachEduModData_Click(object sender, EventArgs e)
        {

            TeachEduSave.Visible = false;
            TeachEduUpdate.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            Label lblEduSn = (Label)row.FindControl("EduSn");
            int intEduSn = Convert.ToInt32(lblEduSn.Text.ToString()); // here we are
            TBIntEduSn.Text = intEduSn.ToString();
            VTeacherEdu vTeacherEdu = crudObject.GetVTeacherEdu(intEduSn);

            for (int i = 0; i < TeachEduLocal.Items.Count - 1; i++)
            {
                if (vTeacherEdu.EduLocal.Equals(TeachEduLocal.Items[i].Value))
                {
                    TeachEduLocal.Items[i].Selected = true;
                }
                else
                {
                    TeachEduLocal.Items[i].Selected = false;
                }
            }
            TeachEduSchool.Text = vTeacherEdu.EduSchool;
            TeachEduDepartment.Text = vTeacherEdu.EduDepartment;
            for (int i = 0; i < TeachEduDegree.Items.Count; i++)
            {
                if (vTeacherEdu.EduDegree.Equals(TeachEduDegree.Items[i].Value))
                {
                    TeachEduDegree.Items[i].Selected = true;
                }
                else
                {
                    TeachEduDegree.Items[i].Selected = false;
                }
            }
            //TeachEduStartYM.Text = vTeacherEdu.EduStartYM;
            vTeacherEdu.EduStartYM = vTeacherEdu.EduStartYM.PadLeft(5, '0');
            ddl_TeachEduStartYear.SelectedValue = vTeacherEdu.EduStartYM.Substring(0, 3);
            ddl_TeachEduStartMonth.SelectedValue = vTeacherEdu.EduStartYM.Substring(3, 2);
            //TeachEduEndYM.Text = vTeacherEdu.EduEndYM;
            vTeacherEdu.EduEndYM = vTeacherEdu.EduEndYM.PadLeft(5, '0');
            ddl_TeachEduEndYear.SelectedValue = vTeacherEdu.EduEndYM.Substring(0, 3);
            ddl_TeachEduEndMonth.SelectedValue = vTeacherEdu.EduEndYM.Substring(3, 2);

            for (int i = 0; i < TeachEduDegreeType.Items.Count; i++)
            {
                if (vTeacherEdu.EduDegreeType.Equals(TeachEduDegreeType.Items[i].Value))
                {
                    TeachEduDegreeType.Items[i].Selected = true;
                }
                else
                {
                    TeachEduDegreeType.Items[i].Selected = false;
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachEdu').modal('show'); });</script>", false);
        }


        //更新 教師學歷資料(必填)
        protected void TeachEduUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                VTeacherEdu vTeachEdu = new VTeacherEdu();
                vTeachEdu.EduSn = Convert.ToInt32(TBIntEduSn.Text.ToString());
                vTeachEdu.EduLocal = TeachEduLocal.SelectedValue.ToString();
                vTeachEdu.EduSchool = TeachEduSchool.Text.ToString();
                vTeachEdu.EduDepartment = TeachEduDepartment.Text.ToString();
                vTeachEdu.EduDegree = TeachEduDegree.SelectedValue.ToString();
                //vTeachEdu.EduStartYM = TeachEduStartYM.Text.ToString();
                vTeachEdu.EduStartYM = ddl_TeachEduStartYear.SelectedValue + ddl_TeachEduStartMonth.SelectedValue;
                //vTeachEdu.EduEndYM = TeachEduEndYM.Text.ToString();
                vTeachEdu.EduEndYM = ddl_TeachEduEndYear.SelectedValue + ddl_TeachEduEndMonth.SelectedValue;
                vTeachEdu.EduDegreeType = TeachEduDegreeType.SelectedValue.ToString();
                CRUDObject crudObject = new CRUDObject();
                if (crudObject.Update(vTeachEdu))
                {
                    msg = "更新成功!!";
                }
                else
                {
                    msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                }
            }
            TeachEduSave.Visible = true;
            TeachEduUpdate.Visible = false;
            TeachEduClear.Visible = false;
            TeachEduSchool.Text = "";
            TeachEduDepartment.Text = "";
            //TeachEduStartYM.Text = "";
            //TeachEduEndYM.Text = "";
            TeachEduLocal.DataBind();
            TeachEduDegree.DataBind();
            TeachEduDegreeType.DataBind();
            //寫入後載入下方的DataGridView
            GVTeachEdu.DataBind();
            if (GVTeachEdu.Rows.Count == 0)
                lb_NoTeachEdu.Visible = true;
            else
                lb_NoTeachEdu.Visible = false;
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);

        }

        protected void TeachEduClear_Click(object sender, EventArgs e)
        {
            TeachEduSave.Visible = true;
            TeachEduUpdate.Visible = false;
            TeachEduClear.Visible = false;
            TeachEduSchool.Text = "";
            TeachEduDepartment.Text = "";
            //TeachEduStartYM.Text = "";
            //TeachEduEndYM.Text = "";
            TeachEduLocal.DataBind();
            TeachEduDegree.DataBind();
            TeachEduDegreeType.DataBind();
            //寫入後載入下方的DataGridView
            GVTeachEdu.DataBind();
            if (GVTeachEdu.Rows.Count == 0)
                lb_NoTeachEdu.Visible = true;
            else
                lb_NoTeachEdu.Visible = false;
        }

        //刪除 教師學歷資料(no use)
        protected void TeachEduDelete_Click(object sender, EventArgs e)
        {
            var delLink = (Control)sender;
            GridViewRow row = (GridViewRow)delLink.NamingContainer;
            int intEduSn = Convert.ToInt32(row.Cells[0].Text); // here we are
            VTeacherEdu vTeachEdu = new VTeacherEdu();
            vTeachEdu.EduSn = intEduSn;
            crudObject.Delete(vTeachEdu);
        }

        //寫入 教師經歷資料 
        protected void TeachExpSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (TeachExpOrginization.Text == "" || TeachExpJobTitle.Text == "")
                {
                    if (TeachExpOrginization.Text == "")
                        msg += "機關名稱未填寫! ";
                    if (TeachExpJobTitle.Text == "")
                        msg += "職稱未填寫! ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachExp').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherExp vTeachExp = new VTeacherExp();
                    vTeachExp.AppSn = Int32.Parse(TbAppSn.Text);
                    vTeachExp.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vTeachExp.ExpOrginization = TeachExpOrginization.Text.ToString();
                    vTeachExp.ExpUnit = TeachExpUnit.Text.ToString();
                    vTeachExp.ExpJobTitle = TeachExpJobTitle.Text.ToString();
                    //vTeachExp.ExpStartYM = TeachExpStartYM.Text.ToString();
                    vTeachExp.ExpStartYM = ddl_TeachExpStartYear.SelectedValue + ddl_TeachExpStartMonth.SelectedValue;
                    //vTeachExp.ExpEndYM = TeachExpEndYM.Text.ToString();
                    vTeachExp.ExpEndYM = ddl_TeachExpEndYear.SelectedValue + ddl_TeachExpEndMonth.SelectedValue;
                    if (TeachExpUploadFU.HasFile)
                    {
                        if (TeachExpUploadFU.FileName != null && checkName(TeachExpUploadFU.FileName))
                        {
                            TeachExpUploadCB.Checked = true;
                            //fileLists.Add(TeachExpUploadFU);
                            vTeachExp.ExpUploadName = TeachExpUploadFU.FileName;
                            //vTeachExp.ExpUpload = TeachExpUploadCB.Checked;
                            ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                            TeachExpUploadFU.PostedFile.SaveAs(Session["FolderPath"] + TeachExpUploadFU.FileName);
                        }
                    }
                    else
                    {
                        vTeachExp.ExpUploadName = TeachExpUploadFUName.Text.ToString();
                        //vTeachExp.ExpUpload = TeachExpUploadCB.Checked;
                    }

                    CRUDObject crudObject = new CRUDObject();
                    if (crudObject.Insert(vTeachExp))
                    {
                        //寫入後載入下方的DataGridView
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        GVTeachExp.DataBind();
                        msg = "新增成功!!";

                        if (GVTeachExp.Rows.Count == 0)
                            lb_NoTeachExp.Visible = true;
                        else
                            lb_NoTeachExp.Visible = false;
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }
                    TeachExpSave.Visible = true;
                    TeachExpUpdate.Visible = false;
                    TeachExpOrginization.Text = "";
                    //TeachExpStartYM.Text = "";
                    //TeachExpEndYM.Text = "";
                    TeachExpUnit.Text = "";
                    TeachExpJobTitle.Text = "";
                    TeachExpUploadCB.Checked = false;
                    TeachExpUploadFUName.Text = "";
                    TeachExpHyperLink.Text = "";
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        //載入修改資料 教師經歷資料
        protected void TeachExpModData_Click(object sender, EventArgs e)
        {
            TeachExpSave.Visible = false;
            TeachExpUpdate.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            Label lblExpSn = (Label)row.FindControl("ExpSn");
            int intExpSn = Convert.ToInt32(lblExpSn.Text.ToString()); // here we are
            TBIntExpSn.Text = intExpSn.ToString();
            VTeacherExp vTeachExp = crudObject.GetVTeacherExp(intExpSn);
            TeachExpOrginization.Text = vTeachExp.ExpOrginization;
            //TeachExpStartYM.Text = vTeachExp.ExpStartYM;
            vTeachExp.ExpStartYM = vTeachExp.ExpStartYM.PadLeft(5, '0');
            ddl_TeachExpStartYear.SelectedValue = vTeachExp.ExpStartYM.Substring(0, 3);
            ddl_TeachExpStartMonth.SelectedValue = vTeachExp.ExpStartYM.Substring(3, 2);
            //TeachExpEndYM.Text = vTeachExp.ExpEndYM;
            vTeachExp.ExpEndYM = vTeachExp.ExpEndYM.PadLeft(5, '0');
            ddl_TeachExpEndYear.SelectedValue = vTeachExp.ExpEndYM.Substring(0, 3);
            ddl_TeachExpEndMonth.SelectedValue = vTeachExp.ExpEndYM.Substring(3, 2);
            TeachExpUnit.Text = vTeachExp.ExpUnit;
            TeachExpJobTitle.Text = vTeachExp.ExpJobTitle;


            string location = Global.FileUpPath + vEmp.EmpSn + "/";
            if (vTeachExp.ExpUploadName != null && !vTeachExp.ExpUploadName.Equals(""))
            {
                TeachExpUploadCB.Checked = true;
                TeachExpHyperLink.NavigateUrl = location + vTeachExp.ExpUpload;
                TeachExpHyperLink.Text = vTeachExp.ExpUploadName;
                TeachExpHyperLink.Visible = true;
                TeachExpUploadFU.Visible = true;
                TeachExpUploadFUName.Text = vTeachExp.ExpUploadName;
            }
            else
            {
                TeachExpUploadCB.Checked = false;
                TeachExpUploadFU.Visible = true;
                TeachExpHyperLink.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachExp').modal('show'); });</script>", false);
        }

        //更新 教師經歷資料
        protected void TeachExpUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                VTeacherExp vTeachExp = new VTeacherExp();
                vTeachExp.ExpSn = Convert.ToInt32(TBIntExpSn.Text.ToString());
                vTeachExp.ExpOrginization = TeachExpOrginization.Text.ToString();
                //vTeachExp.ExpStartYM = TeachExpStartYM.Text.ToString();
                vTeachExp.ExpStartYM = ddl_TeachExpStartYear.SelectedValue + ddl_TeachExpStartMonth.SelectedValue;
                //vTeachExp.ExpEndYM = TeachExpEndYM.Text.ToString();
                vTeachExp.ExpEndYM = ddl_TeachExpEndYear.SelectedValue + ddl_TeachExpEndMonth.SelectedValue;
                vTeachExp.ExpUnit = TeachExpUnit.Text.ToString();
                vTeachExp.ExpJobTitle = TeachExpJobTitle.Text.ToString();
                if (TeachExpUploadFU.HasFile)
                {
                    if (TeachExpUploadFU.FileName != null && checkName(TeachExpUploadFU.FileName))
                    {
                        TeachExpUploadCB.Checked = true;
                        //fileLists.Add(TeachExpUploadFU);
                        vTeachExp.ExpUploadName = TeachExpUploadFU.FileName;
                        //vTeachExp.ExpUpload = TeachExpUploadCB.Checked;
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        TeachExpUploadFU.PostedFile.SaveAs(Session["FolderPath"] + TeachExpUploadFU.FileName);
                    }
                }
                else
                {
                    vTeachExp.ExpUploadName = TeachExpUploadFUName.Text.ToString();
                    //vTeachExp.ExpUpload = TeachExpUploadCB.Checked;
                }

                CRUDObject crudObject = new CRUDObject();
                if (crudObject.Update(vTeachExp))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    //寫入後載入下方的DataGridView
                    GVTeachExp.DataBind();
                    msg = "更新成功!!";
                }
                else
                {
                    msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                }
            }
            TeachExpSave.Visible = true;
            TeachExpUpdate.Visible = false;
            TeachExpCancel.Visible = false;
            TeachExpOrginization.Text = "";
            //TeachExpStartYM.Text = "";
            //TeachExpEndYM.Text = "";
            TeachExpUnit.Text = "";
            TeachExpJobTitle.Text = "";
            TeachExpUploadCB.Checked = false;
            TeachExpUploadFUName.Text = "";
            TeachExpHyperLink.Visible = false;
            TeachExpHyperLink.Text = "";
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }



        protected void TeachExpCancel_Click(object sender, EventArgs e)
        {
            TeachExpSave.Visible = true;
            TeachExpUpdate.Visible = false;
            TeachExpCancel.Visible = false;
            TeachExpOrginization.Text = "";
            //TeachExpStartYM.Text = "";
            //TeachExpEndYM.Text = "";
            TeachExpUnit.Text = "";
            TeachExpJobTitle.Text = "";
            TeachExpUploadCB.Checked = false;
            TeachExpUploadFUName.Text = "";
        }

        //寫入 教師證發放資料 
        protected void TeachCaSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (TeachCaNumberCN.Text == "" || TeachCaNumber.Text == "" || TeachCaPublishSchool.Text == "")
                {
                    if (TeachCaNumberCN.Text == "")
                        msg += "教師字號未填寫! ";
                    if (TeachCaNumber.Text == "")
                        msg += "教師證號未填寫! ";
                    if (TeachCaPublishSchool.Text == "")
                        msg += "發證學校未填寫! ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachCa').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherCa vTeachCa = new VTeacherCa();
                    vTeachCa.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vTeachCa.CaNumberCN = TeachCaNumberCN.Text.ToString();
                    vTeachCa.CaNumber = TeachCaNumber.Text.ToString();
                    vTeachCa.CaPublishSchool = TeachCaPublishSchool.Text.ToString();
                    //vTeachCa.CaStartYM = TeachCaStartYM.Text.ToString();
                    vTeachCa.CaStartYM = ddl_TeachCaStartYear.SelectedValue + ddl_TeachCaStartMonth.SelectedValue;
                    //vTeachCa.CaEndYM = TeachCaEndYM.Text.ToString();
                    vTeachCa.CaEndYM = ddl_TeachCaEndYear.SelectedValue + ddl_TeachCaEndMonth.SelectedValue;
                    if (TeachCaUploadFU.FileName != null && !TeachCaUploadFU.FileName.Equals(""))
                    {
                        if (TeachCaUploadFU.FileName != null && checkName(TeachCaUploadFU.FileName))
                        {
                            TeachCaUploadCB.Checked = true;
                            //fileLists.Add(TeachCaUploadFU);
                            vTeachCa.CaUploadName = TeachCaUploadFU.FileName;
                            //vTeachCa.CaUpload = TeachCaUploadCB.Checked;
                            ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                            TeachCaUploadFU.PostedFile.SaveAs(Session["FolderPath"] + TeachCaUploadFU.FileName);
                        }
                    }
                    else
                    {
                        vTeachCa.CaUploadName = TeachCaUploadFUName.Text.ToString();
                        //vTeachCa.CaUpload = TeachCaUploadCB.Checked;
                    }

                    CRUDObject crudObject = new CRUDObject();
                    if (crudObject.Insert(vTeachCa))
                    {
                        //寫入後載入下方的DataGridView
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        GVTeachCa.DataBind();
                        msg = "新增成功!!";
                        if (GVTeachCa.Rows.Count == 0)
                            lb_NoTeachCa.Visible = true;
                        else
                            lb_NoTeachCa.Visible = false;
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }
                    TeachCaSave.Visible = true;
                    TeachCaUpdate.Visible = false;
                    TeachCaCancel.Visible = false;
                    TeachCaNumberCN.Text = "";
                    TeachCaNumber.Text = "";
                    TeachCaPublishSchool.Text = "";
                    //TeachCaStartYM.Text = "";
                    //TeachCaEndYM.Text = "";
                    TeachCaUploadCB.Checked = false;
                    TeachExpHyperLink.Visible = false;
                    TeachCaHyperLink.Text = "";
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        //載入修改 教師證發放資料
        protected void TeachCaModData_Click(object sender, EventArgs e)
        {
            TeachCaSave.Visible = false;
            TeachCaUpdate.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            Label lblCaSn = (Label)row.FindControl("CaSn");
            int intCaSn = Convert.ToInt32(lblCaSn.Text.ToString()); // here we are
            TBIntCaSn.Text = intCaSn.ToString();
            VTeacherCa vTeachCa = crudObject.GetVTeacherCa(intCaSn);

            TeachCaNumberCN.Text = vTeachCa.CaNumberCN;
            TeachCaNumber.Text = vTeachCa.CaNumber;
            TeachCaPublishSchool.Text = vTeachCa.CaPublishSchool;
            //TeachCaStartYM.Text = vTeachCa.CaStartYM;
            vTeachCa.CaStartYM.PadLeft(5, '0');
            ddl_TeachCaStartYear.SelectedValue = vTeachCa.CaStartYM.Substring(0, 3);
            ddl_TeachCaStartMonth.SelectedValue = vTeachCa.CaStartYM.Substring(3, 2);
            //TeachCaEndYM.Text = vTeachCa.CaEndYM;
            vTeachCa.CaEndYM.PadLeft(5, '0');
            ddl_TeachCaEndYear.SelectedValue = vTeachCa.CaEndYM.Substring(0, 3);
            ddl_TeachCaEndMonth.SelectedValue = vTeachCa.CaEndYM.Substring(3, 2);
            string location = Global.FileUpPath + vEmp.EmpSn + "/";
            if (vTeachCa.CaUploadName != null && !vTeachCa.CaUploadName.Equals(""))
            {
                TeachCaUploadCB.Checked = true;
                TeachCaHyperLink.NavigateUrl = location + vTeachCa.CaUpload;
                TeachCaHyperLink.Text = vTeachCa.CaUploadName;
                TeachCaHyperLink.Visible = true;
                TeachCaUploadFU.Visible = true;
                TeachCaUploadFUName.Text = vTeachCa.CaUploadName;
            }
            else
            {
                TeachCaUploadCB.Checked = false;
                TeachCaUploadFU.Visible = true;
                TeachCaHyperLink.Visible = false;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachCa').modal('show'); });</script>", false);
        }

        //更新 教師證發放資料 
        protected void TeachCaUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);

            }
            else
            {
                VTeacherCa vTeachCa = new VTeacherCa();
                vTeachCa.CaSn = Convert.ToInt32(TBIntCaSn.Text.ToString());
                vTeachCa.CaNumberCN = TeachCaNumberCN.Text.ToString();
                vTeachCa.CaNumber = TeachCaNumber.Text.ToString();
                vTeachCa.CaPublishSchool = TeachCaPublishSchool.Text.ToString();
                vTeachCa.CaStartYM = ddl_TeachCaStartYear.SelectedValue + ddl_TeachCaStartMonth.SelectedValue;
                vTeachCa.CaEndYM = ddl_TeachCaEndYear.SelectedValue + ddl_TeachCaEndMonth.SelectedValue;
                if (TeachCaUploadFU.FileName != null && !TeachCaUploadFU.FileName.Equals(""))
                {
                    if (TeachCaUploadFU.FileName != null && checkName(TeachCaUploadFU.FileName))
                    {
                        TeachCaUploadCB.Checked = true;
                        //fileLists.Add(TeachCaUploadFU);
                        vTeachCa.CaUploadName = TeachCaUploadFU.FileName;
                        //vTeachCa.CaUpload = TeachCaUploadCB.Checked;
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        TeachCaUploadFU.PostedFile.SaveAs(Session["FolderPath"] + TeachCaUploadFU.FileName);
                    }
                }
                else
                {
                    vTeachCa.CaUploadName = TeachCaUploadFUName.Text.ToString();
                    //vTeachCa.CaUpload = TeachCaUploadCB.Checked;
                }


                CRUDObject crudObject = new CRUDObject();
                if (crudObject.Update(vTeachCa))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    //寫入後載入下方的DataGridView
                    GVTeachCa.DataBind();
                    msg = "更新成功!!";
                }
                else
                {
                    msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                }
                TeachCaSave.Visible = true;
                TeachCaUpdate.Visible = false;
                TeachCaCancel.Visible = false;
                TeachCaNumberCN.Text = "";
                TeachCaNumber.Text = "";
                TeachCaPublishSchool.Text = "";
                //TeachCaStartYM.Text = "";
                //TeachCaEndYM.Text = "";
                TeachCaUploadCB.Checked = false;
                TeachExpHyperLink.Visible = false;
                TeachCaHyperLink.Text = "";
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void TeachCaCancel_Click(object sender, EventArgs e)
        {
            TeachCaSave.Visible = true;
            TeachCaUpdate.Visible = false;
            TeachCaCancel.Visible = false;
            TeachCaNumberCN.Text = "";
            TeachCaNumber.Text = "";
            TeachCaPublishSchool.Text = "";
            //TeachCaStartYM.Text = "";
            //TeachCaEndYM.Text = "";
        }


        //寫入 學術獎勵、榮譽事項 
        protected void TeachHornorSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (TeachHorDescription.Text == "" || TeachHorDescription.Text.Length > 150)
                {
                    if (TeachHorDescription.Text == "")
                        msg += "學術獎勵、榮譽事項未填寫! ";

                    if (TeachHorDescription.Text.Length > 150)
                        msg += "榮譽事項超過150字! ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachHonour').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherHonour vTeachHor = new VTeacherHonour();
                    vTeachHor.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vTeachHor.HorYear = ddl_TeachHorYear.SelectedValue;
                    vTeachHor.HorDescription = TeachHorDescription.Text.ToString();

                    CRUDObject crudObject = new CRUDObject();
                    if (crudObject.Insert(vTeachHor))
                    {
                        //寫入後載入下方的DataGridView
                        GVTeachHonour.DataBind();
                        msg = "新增成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        //載入 學術獎勵、榮譽事項 
        protected void TeachHorModData_Click(object sender, EventArgs e)
        {
            TeachHornourSave.Visible = false;
            TeachHornourUpdate.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            Label lblHorSn = (Label)row.FindControl("HorSn");
            int intHorSn = Convert.ToInt32(lblHorSn.Text.ToString()); // here we are
            TBIntHorSn.Text = intHorSn.ToString();
            VTeacherHonour vTeacherHonour = crudObject.GetVTeacherHonour(intHorSn);
            TeachHorDescription.Text = vTeacherHonour.HorDescription;
            ddl_TeachHorYear.SelectedValue = vTeacherHonour.HorYear;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachHonour').modal('show'); });</script>", false);
        }

        //更新 學術獎勵、榮譽事項 
        protected void TeachHornorUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {

                if (TeachHorDescription.Text.Length > 150)
                {
                    if (TeachHorDescription.Text.Length > 150)
                        msg += "榮譽事項超過150字! ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachHonour').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherHonour vTeachHor = new VTeacherHonour();
                    vTeachHor.HorSn = Convert.ToInt32(TBIntHorSn.Text.ToString());
                    vTeachHor.HorYear = ddl_TeachHorYear.SelectedValue;
                    vTeachHor.HorDescription = TeachHorDescription.Text.ToString();

                    CRUDObject crudObject = new CRUDObject();
                    if (crudObject.Update(vTeachHor))
                    {
                        //寫入後載入下方的DataGridView
                        GVTeachHonour.DataBind();
                        msg = "更新成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                    }
                    TeachHornourSave.Visible = true;
                    TeachHornourUpdate.Visible = false;
                    TeachHornourCancel.Visible = false;
                    TeachHorDescription.Text = "";
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void TeachHornourCancel_Click(object sender, EventArgs e)
        {
            TeachHornourSave.Visible = true;
            TeachHornourUpdate.Visible = false;
            TeachHornourCancel.Visible = false;
            TeachHorDescription.Text = "";
            //TeachHorYear.Text = "";
        }

        //寫入 教師上傳論文積分表 
        protected void ThesisScoreSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool insertflage = false;
            CRUDObject crudObject = new CRUDObject();
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {

                if (AppThesisResearchResult.Text.Length > 1000 || AppThesisName.Text.Length > 300 || AppThesisJournalRefCount.Text.Length > 350)
                {
                    if (AppThesisResearchResult.Text.Length > 1000)
                        msg = "研究成果名稱含符號，請在1000字數已內!";
                    if (AppThesisName.Text.Length > 300)
                        msg = "論文名稱含符號，請在300字數已內!";
                    if (AppThesisJournalRefCount.Text.Length > 350)
                        msg = "IF/排名名稱含符號，請在350字數已內!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);
                }
                else
                {
                    try
                    {
                        double d = Double.Parse(LabelTotalThesisScore.Text.ToString());
                        VThesisScore vThesisScore = new VThesisScore();
                        vThesisScore.AppSn = Int32.Parse(TbAppSn.Text);
                        vThesisScore.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                        //先取得前一筆的序號                    2018/03/02 修正檢測empsn 為檢測 appsn
                        int baseNum = 1;
                        while (crudObject.GetThesisScoreFinalSnNo(TbAppSn.Text, baseNum))
                        {
                            baseNum++;
                        }
                        vThesisScore.SnNo = baseNum;
                        //vThesisScore.ResearchYear = ResearchYear.Text.ToString();
                        //vThesisScore.ThesisPublishYearMonth = AppThesisPublishYearMonth.Text.ToString();
                        vThesisScore.ThesisPublishYearMonth = ddl_AppThesisPublishYear.SelectedValue + ddl_AppThesisPublishMonth.SelectedValue;
                        vThesisScore.RRNo = RRNo.Text.ToString();
                        vThesisScore.ThesisResearchResult = AppThesisResearchResult.Text.ToString();
                        vThesisScore.ThesisC = AppThesisC.Text.ToString();
                        vThesisScore.ThesisJ = AppThesisJ.Text.ToString();
                        vThesisScore.ThesisA = AppThesisA.Text.ToString();
                        vThesisScore.ThesisTotal = Math.Round(d, 3, MidpointRounding.AwayFromZero).ToString();
                        vThesisScore.ThesisName = AppThesisName.Text.ToString();
                        vThesisScore.ThesisBuildDate = DateTime.Now;
                        vThesisScore.ThesisModifyDate = DateTime.Now;




                        if (AppThesisUploadFU.HasFile)
                        {
                            if (AppThesisUploadFU.FileName != null && checkName(AppThesisUploadFU.FileName))
                            {
                                AppThesisUploadCB.Checked = true;
                                //fileLists.Add(AppThesisUploadFU);
                                vThesisScore.ThesisUploadName = AppThesisUploadFU.FileName; //第一次新增時要改檔名
                                                                                            //vThesisScore.ThesisUpload = AppThesisUploadCB.Checked;
                                ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

                                AppThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisUploadFU.FileName);
                            }
                        }
                        else
                        {
                            vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString();
                            vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName;
                            //vThesisScore.ThesisUpload = AppThesisUploadCB.Checked;
                        }
                        //期刊引用排名
                        if (!AppThesisJournalRefCount.Text.ToString().Equals(""))
                        {
                            vThesisScore.ThesisJournalRefCount = AppThesisJournalRefCount.Text.ToString();
                        }

                        //請檢附資料庫查詢畫面，無SCI分數者免附。
                        if (AppThesisJournalRefUploadFU.HasFile)
                        {
                            if (AppThesisJournalRefUploadFU.FileName != null && checkName(AppThesisJournalRefUploadFU.FileName))
                            {
                                AppThesisJournalRefUploadCB.Checked = true;
                                //fileLists.Add(AppThesisJournalRefUploadFU);
                                vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFU.FileName;
                                ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

                                AppThesisJournalRefUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisJournalRefUploadFU.FileName);
                            }
                        }
                        else
                        {
                            vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFUName.Text.ToString();
                        }

                        //若此為代表著作
                        if (RepresentCB.Checked)
                        {
                            vThesisScore.IsRepresentative = RepresentCB.Checked;
                            //合著人寫入上傳
                            if (ThesisCoAuthorUploadFU.HasFile)
                            {
                                if (ThesisCoAuthorUploadFU.FileName != null && checkName(ThesisCoAuthorUploadFU.FileName))
                                {
                                    ThesisCoAuthorUploadCB.Checked = true;
                                    //fileLists.Add(ThesisCoAuthorUploadFU);
                                    vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFU.FileName;
                                    vThesisScore.ThesisSummaryCN = ThesisSummaryCN.Text.ToString();
                                    //vApplyAudit.AppCoAuthorUpload = AppPCoAuthorUploadCB.Checked;
                                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                    ThesisCoAuthorUploadFU.PostedFile.SaveAs(Session["FolderPath"] + ThesisCoAuthorUploadFU.FileName);
                                }
                            }
                            else
                            {
                                vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFUName.Text.ToString();
                                vThesisScore.ThesisSummaryCN = ThesisSummaryCN.Text.ToString();
                                //vAppendPublication.AppPCoAuthorUpload = AppPCoAuthorUploadCB.Checked;
                            }
                            vThesisScore.ThesisSummaryCN = ThesisSummaryCN.Text.ToString();
                        }
                        //勾選RPI要計分的
                        if (CountRPICB.Checked)
                        {
                            countRPI = crudObject.GetThesisCountRPI(vThesisScore.AppSn);
                            if (countRPI < Convert.ToInt32(AppResearchYear.SelectedValue.ToString()))
                            {
                                vThesisScore.IsCountRPI = CountRPICB.Checked;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('計算RPI，超出按『研究年資』可選篇數！');", true);
                            }
                        }
                        if (crudObject.Insert(vThesisScore))
                        {
                            msg = "論文積分檔新增成功!!";
                            int ThesisSn = crudObject.GetVThesisScoreSn();
                            int ThesisCnt = crudObject.GetVThesisScoreTotalCount(vThesisScore.EmpSn, vThesisScore.AppSn);
                            //檔名修改
                            string orgThesisUploadName = vThesisScore.ThesisUploadName;
                            if (!orgThesisUploadName.EndsWith(""))
                            {
                                if (AppThesisUploadFU.FileName != null && checkName(AppThesisUploadFU.FileName))
                                {
                                    vThesisScore.ThesisSn = ThesisSn;
                                    vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName.Substring(0, vThesisScore.ThesisUploadName.IndexOf(".pdf")) + "_" + vEmp.EmpNameCN + "_" + vThesisScore.SnNo + ".pdf";
                                    crudObject.Update(vThesisScore);
                                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

                                    AppThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisUploadFU.FileName);
                                }
                                //string location = Server.MapPath(Global.FileUpPath + TbEmpSn.Text.ToString() + "/");
                                ////string location = Server.MapPath("../DocuFile/HRApplyDoc/" + TbEmpSn.Text.ToString() + "/"); 
                                //string fromFile = location + orgThesisUploadName;
                                //string toFile = location + vThesisScore.ThesisUploadName;
                                //try
                                //{
                                //    File.Move(fromFile, toFile);
                                //}
                                //catch (Exception ex)
                                //{
                                //}
                            }
                        }
                        else
                        {
                            msg = "1.寫入論文積分檔失敗，請洽資訊人員! ";
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = "請確認所有欄位都正確填寫!!";
                        ex.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);
                    }
                }
                //Update ApplyAudit
                vApplyAudit.AppResearchYear = AppResearchYear.SelectedValue;

                if (GVThesisScore.Rows.Count > 0)
                    vApplyAudit.AppThesisAccuScore = AppPThesisAccuScore.Text.ToString();
                else
                    AppPThesisAccuScore.Text = "0";
                vApplyAudit.AppRPIScore = AppRPI.Text.ToString();
                if (!crudObject.Update(vApplyAudit))
                {
                    MessageLabel.Text += "2.更新申請檔失敗，請洽資訊人員!";
                }
                ThesisScoreSave.Visible = true;
                ThesisScoreUpdate.Visible = false;
                //AppThesisPublishYearMonth.Text = "";
                RRNo.DataBind();
                AppThesisResearchResult.Text = "";
                AppThesisC.Text = "";
                AppThesisJ.Text = "";
                AppThesisA.Text = "";
                LabelTotalThesisScore.Text = "";
                AppThesisUploadCB.Checked = false;
                AppThesisJournalRefUploadCB.Checked = false;
                AppThesisUploadFUName.Text = "";
                RepresentCB.Checked = false;
                AppThesisJournalRefHyperLink.Visible = false;
                ThesisCoAuthorHyperLink.Visible = false;
                AppThesisHyperLink.Visible = false;
                RepresentCB.Checked = false;
                CountRPICB.Checked = false;
                ThesisCoAuthorUploadCB.Checked = false;
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ButtonSnNoUp_Click(object sender, EventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            int nowThesisSn = int.Parse(imgbtn.CommandArgument.Split(',')[0].ToString());
            int nowSnNo = int.Parse(imgbtn.CommandArgument.Split(',')[1].ToString());
            //取得前一筆資料的ThesisSn序號並更新前一筆資料排序
            int ThesisSn = crudObject.GetThesisScoreGetThesisSn(vApplyAudit.EmpSn, vApplyAudit.AppSn, (nowSnNo - 1));
            crudObject.GetThesisScoreUpdateSnNo(ThesisSn, nowSnNo);
            //更新此資料的ThesisSn排序
            crudObject.GetThesisScoreUpdateSnNo(nowThesisSn, nowSnNo - 1);

        }

        protected void ButtonSnNoDown_Click(object sender, EventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            int nowThesisSn = int.Parse(imgbtn.CommandArgument.Split(',')[0].ToString());
            int nowSnNo = int.Parse(imgbtn.CommandArgument.Split(',')[1].ToString());

            //取得後一筆資料的ThesisSn序號並更新前一筆資料排序
            int ThesisSn = crudObject.GetThesisScoreGetThesisSn(vApplyAudit.EmpSn, vApplyAudit.AppSn, (nowSnNo + 1));
            crudObject.GetThesisScoreUpdateSnNo(ThesisSn, nowSnNo);
            //更新此資料的ThesisSn排序
            crudObject.GetThesisScoreUpdateSnNo(nowThesisSn, nowSnNo + 1);

        }

        //刪除後重新排序
        protected void ThesisScoreDeleteData_Click(object sender, EventArgs e)
        {
            if (Session["EmpSn"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {

                var deleteLink = (Control)sender;
                GridViewRow row = (GridViewRow)deleteLink.NamingContainer;
                TBIntThesisSn.Text = ((Label)row.FindControl("ThesisSn")).Text.ToString();
                TBSnNo.Text = ((Label)row.FindControl("SnNo")).Text.ToString(); //論文序號
                crudObject.DeleteThesisScoreBySn(Convert.ToInt32(TBIntThesisSn.Text.ToString()));
                DataTable dt = crudObject.GetVThesisScoreAll(Convert.ToInt32(Session["EmpSn"].ToString()));
                //
                int h = 0;
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    h = k + 1;
                    crudObject.UpdateThesisScoreSn(Convert.ToInt32(dt.Rows[k][0].ToString()), h);
                }
                DSThesisScore.DataBind();
            }
        }

        //載入 教師上傳論文積分表 
        protected void ThesisScoreModData_Click(object sender, EventArgs e)
        {
            Session["ThesisAccuScore"] = "0";
            Session["ThesisRPIScore"] = "0";
            ThesisScoreSave.Visible = false;
            ThesisScoreUpdate.Visible = true;
            ThesisScoreCancel.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            TBIntThesisSn.Text = ((Label)row.FindControl("ThesisSn")).Text.ToString();
            TBSnNo.Text = ((Label)row.FindControl("SnNo")).Text.ToString(); //論文序號
            VThesisScore vThesisScore = crudObject.GetVThesisScore(Convert.ToInt32(TBIntThesisSn.Text.ToString()));

            for (int i = 0; i < RRNo.Items.Count; i++)
            {
                if (vThesisScore.RRNo.Equals(RRNo.Items[i].Value))
                {
                    RRNo.Items[i].Selected = true;
                }
                else
                {
                    RRNo.Items[i].Selected = false;
                }
            }
            AppThesisName.Text = vThesisScore.ThesisName;
            AppThesisResearchResult.Text = vThesisScore.ThesisResearchResult;
            //AppThesisPublishYearMonth.Text = vThesisScore.ThesisPublishYearMonth;
            vThesisScore.ThesisPublishYearMonth.PadLeft(5, '0');
            ddl_AppThesisPublishYear.SelectedValue = vThesisScore.ThesisPublishYearMonth.Substring(0, 3);
            ddl_AppThesisPublishMonth.SelectedValue = vThesisScore.ThesisPublishYearMonth.Substring(3, 2);
            AppThesisC.Text = vThesisScore.ThesisC;
            AppThesisJ.Text = vThesisScore.ThesisJ;
            AppThesisA.Text = vThesisScore.ThesisA;
            LabelTotalThesisScore.Text = vThesisScore.ThesisTotal;
            AppResearchYear.SelectedValue = vApplyAudit.AppResearchYear;
            AppThesisJournalRefCount.Text = vThesisScore.ThesisJournalRefCount.ToString();
            for (int i = 0; i < AppResearchYear.Items.Count - 1; i++)
            {
                if (vApplyAudit.AppResearchYear.Equals(AppResearchYear.Items[i].Value))
                {
                    AppResearchYear.Items[i].Selected = true;
                }
                else
                {
                    AppResearchYear.Items[i].Selected = false;
                }
            }
            string location = Global.FileUpPath + vEmp.EmpSn + "/";
            //上傳期刊引用論文 
            if (vThesisScore.ThesisJournalRefUploadName != null && !vThesisScore.ThesisJournalRefUploadName.Equals(""))
            {
                AppThesisJournalRefUploadCB.Checked = true;
                AppThesisJournalRefHyperLink.NavigateUrl = location + vThesisScore.ThesisJournalRefUploadName;
                AppThesisJournalRefHyperLink.Text = vThesisScore.ThesisJournalRefUploadName;
                AppThesisJournalRefHyperLink.Visible = true;
                AppThesisJournalRefUploadFU.Visible = true;
                AppThesisJournalRefUploadFUName.Visible = false;
                AppThesisJournalRefUploadFUName.Text = vThesisScore.ThesisJournalRefUploadName;
            }
            else
            {
                AppThesisJournalRefUploadFU.Visible = true;
                AppThesisJournalRefUploadFUName.Visible = false;
            }


            //上傳論文
            if (vThesisScore.ThesisUploadName != null && !vThesisScore.ThesisUploadName.Equals(""))
            {
                AppThesisUploadCB.Checked = true;
                AppThesisHyperLink.NavigateUrl = getHyperLink(vThesisScore.ThesisUploadName);
                AppThesisHyperLink.Text = vThesisScore.ThesisUploadName;
                AppThesisHyperLink.Visible = true;
                AppThesisUploadFU.Visible = true;
                AppThesisUploadFUName.Visible = false;
                AppThesisUploadFUName.Text = vThesisScore.ThesisUploadName;
            }
            else
            {
                AppThesisUploadFU.Visible = true;
                AppThesisUploadFUName.Visible = false;
            }
            //此篇為代表
            if (vThesisScore.IsRepresentative)
            {
                RepresentTable.Visible = true;
                RepresentTable.Style.Clear();//显示
                RepresentCB.Checked = true;
                ThesisSummaryCN.Text = vThesisScore.ThesisSummaryCN;
                if (vThesisScore.ThesisCoAuthorUploadName != null && !vThesisScore.ThesisCoAuthorUploadName.Equals(""))
                {
                    ThesisCoAuthorUploadCB.Checked = true;
                    ThesisCoAuthorHyperLink.NavigateUrl = location + vThesisScore.ThesisCoAuthorUploadName;
                    ThesisCoAuthorHyperLink.Text = vThesisScore.ThesisCoAuthorUploadName;
                    ThesisCoAuthorHyperLink.Visible = true;
                    ThesisCoAuthorUploadFU.Visible = true;
                    ThesisCoAuthorUploadFUName.Visible = false;
                    ThesisCoAuthorUploadFUName.Text = vThesisScore.ThesisCoAuthorUploadName;
                }
                else
                {
                    ThesisCoAuthorUploadFU.Visible = true;
                    ThesisCoAuthorUploadFUName.Visible = false;
                }
            }
            //勾選RPI要計分的
            if (vThesisScore.IsCountRPI)
            {
                CountRPICB.Checked = true;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);

        }

        protected void RepresentCB_OnCheckedChanged(object sender, EventArgs e)
        {
            if (RepresentCB.Checked)
            {
                RepresentTable.Visible = true;
            }
            else
            {
                RepresentTable.Visible = false;
            }
        }

        //更新 教師上傳論文積分表 
        protected void ThesisScoreUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            bool insertflag = false;
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (AppThesisResearchResult.Text.Length > 1000 || AppThesisName.Text.Length > 300 || AppThesisJournalRefCount.Text.Length > 350)
                {
                    if (AppThesisResearchResult.Text.Length > 1000)
                        msg = "研究成果名稱含符號，請在1000字數已內!";
                    if (AppThesisName.Text.Length > 300)
                        msg = "論文名稱含符號，請在300字數已內!";
                    if (AppThesisJournalRefCount.Text.Length > 350)
                        msg = "IF/排名名稱含符號，請在350字數已內!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);
                }
                else
                {
                    double d = Double.Parse(LabelTotalThesisScore.Text.ToString());
                    CRUDObject crudObject = new CRUDObject();
                    VThesisScore vThesisScore = new VThesisScore();
                    //vThesisScore.ResearchYear = ResearchYear.Text.ToString(); 
                    vThesisScore.AppSn = Int32.Parse(Session["AppSn"].ToString());
                    vThesisScore.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vThesisScore.ThesisSn = Convert.ToInt32(TBIntThesisSn.Text.ToString());
                    vThesisScore.SnNo = Convert.ToInt32(TBSnNo.Text.ToString());
                    //vThesisScore.ThesisPublishYearMonth = AppThesisPublishYearMonth.Text.ToString();
                    vThesisScore.ThesisPublishYearMonth = ddl_AppThesisPublishYear.SelectedValue + ddl_AppThesisPublishMonth.SelectedValue;
                    vThesisScore.RRNo = RRNo.Text.ToString();
                    vThesisScore.ThesisResearchResult = AppThesisResearchResult.Text.ToString();
                    vThesisScore.ThesisC = AppThesisC.Text.ToString();
                    vThesisScore.ThesisJ = AppThesisJ.Text.ToString();
                    vThesisScore.ThesisA = AppThesisA.Text.ToString();
                    vThesisScore.ThesisTotal = Math.Round(d, 3, MidpointRounding.AwayFromZero).ToString();
                    vThesisScore.ThesisName = AppThesisName.Text.ToString();
                    //vThesisScore.ThesisUpload = AppThesisUploadCB.Checked;
                    vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString(); //bug 須修復 抓不到資料
                                                                                           //AppThesisJournalRefCount IF排名
                    if (!AppThesisJournalRefCount.Text.ToString().Equals(""))
                    {
                        vThesisScore.ThesisJournalRefCount = AppThesisJournalRefCount.Text.ToString();
                    }
                    vThesisScore.ThesisModifyDate = DateTime.Now;
                    string orgThesisUploadName = "";
                    //勾選RPI要計分的
                    if (CountRPICB.Checked)
                    {
                        countRPI = crudObject.GetThesisCountRPI(Int32.Parse(Session["AppSn"].ToString()));
                        if (countRPI < Convert.ToInt32(AppResearchYear.SelectedValue.ToString()))
                        {
                            vThesisScore.IsCountRPI = CountRPICB.Checked;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('計算RPI，超出按『研究年資』可選篇數！');", true);
                        }
                    }

                    if (AppThesisUploadFU.HasFile)
                    {
                        if (AppThesisUploadFU.FileName != null && checkName(AppThesisUploadFU.FileName))
                        {
                            AppThesisUploadCB.Checked = true;
                            fileLists.Add(AppThesisUploadFU);
                            vThesisScore.ThesisUploadName = AppThesisUploadFU.FileName;
                            orgThesisUploadName = vThesisScore.ThesisUploadName;
                            //檔名修改
                            if (vThesisScore.ThesisUploadName != null && vThesisScore.ThesisUploadName.IndexOf(".pdf") > 0)
                                vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName.Substring(0, vThesisScore.ThesisUploadName.IndexOf(".pdf")) + "_" + vEmp.EmpNameCN + "_" + vThesisScore.SnNo + ".pdf";
                            //vThesisScore.ThesisUpload = AppThesisUploadCB.Checked;
                            ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

                            if (checkName(AppThesisUploadFU.FileName))
                                AppThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisUploadFU.FileName);

                            //string location = Server.MapPath(Global.FileUpPath + vEmp.EmpSn + "/");
                            ////string location = Server.MapPath("../DocuFile/HRApplyDoc/" + vEmp.EmpSn + "/"); 
                            //string fromFile = location + orgThesisUploadName;
                            //string toFile = location + vThesisScore.ThesisUploadName;
                            //try
                            //{
                            //    File.Move(fromFile, toFile);
                            //}
                            //catch (Exception ex)
                            //{
                            //}
                        }
                    }
                    else
                    {
                        //Response.Write("<script>alert('檔案更新失敗!請重新上傳');</script>");
                        vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString();
                        //檔名修改
                        //vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName;
                        //vThesisScore.ThesisUpload = AppThesisUploadCB.Checked;
                    }

                    //請檢附資料庫查詢畫面，無SCI分數者免附。
                    if (AppThesisJournalRefUploadFU.HasFile)
                    {
                        if (AppThesisJournalRefUploadFU.FileName != null && checkName(AppThesisJournalRefUploadFU.FileName))
                        {
                            AppThesisJournalRefUploadCB.Checked = true;
                            //fileLists.Add(AppThesisJournalRefUploadFU);
                            vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFU.FileName;
                            ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                            AppThesisJournalRefUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisJournalRefUploadFU.FileName);
                        }
                    }
                    else
                    {
                        vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFUName.Text.ToString();
                    }
                    //若此為代表著作
                    if (RepresentCB.Checked)
                    {
                        vThesisScore.IsRepresentative = RepresentCB.Checked;
                        //合著人寫入上傳
                        if (ThesisCoAuthorUploadFU.HasFile)
                        {

                            if (ThesisCoAuthorUploadFU.FileName != null && checkName(ThesisCoAuthorUploadFU.FileName))
                            {
                                ThesisCoAuthorUploadCB.Checked = true;
                                //fileLists.Add(ThesisCoAuthorUploadFU);
                                vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFU.FileName;
                                //vApplyAudit.AppCoAuthorUpload = AppCoAuthorUploadCB.Checked;
                                ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                ThesisCoAuthorUploadFU.PostedFile.SaveAs(Session["FolderPath"] + ThesisCoAuthorUploadFU.FileName);
                            }
                        }
                        else
                        {
                            vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFUName.Text.ToString();
                            //vApplyAudit.AppCoAuthorUpload = AppCoAuthorUploadCB.Checked;
                        }
                        vThesisScore.ThesisSummaryCN = ThesisSummaryCN.Text.ToString();
                    }


                    if (crudObject.Update(vThesisScore))
                    {

                        //寫入延伸檔 ApplyAudit AppPublicationUpload AppPublicationUploadName
                        //vApplyAudit.AppPublicationUpload = true;
                        //vApplyAudit.AppPublicationUploadName = AppThesisUploadFU.FileName;
                        //寫入後載入下方的DataGridView
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

                        msg = "更新成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                    }
                    vApplyAudit.AppResearchYear = AppResearchYear.SelectedValue;
                    if (GVThesisScore.Rows.Count > 0)
                        vApplyAudit.AppThesisAccuScore = AppPThesisAccuScore.Text.ToString();
                    else
                        AppPThesisAccuScore.Text = "0";
                    vApplyAudit.AppRPIScore = AppRPI.Text.ToString();
                    if (!crudObject.UpdatePartial(vApplyAudit))
                    {
                        MessageLabel.Text += "2.更新申請檔失敗，請洽資訊人員!";
                    }

                }
            }
            ThesisScoreSave.Visible = true;
            ThesisScoreUpdate.Visible = false;
            ThesisScoreCancel.Visible = false;
            //AppThesisPublishYearMonth.Text = "";
            RRNo.DataBind();
            AppThesisResearchResult.Text = "";
            AppThesisC.Text = "";
            AppThesisJ.Text = "";
            AppThesisA.Text = "";
            LabelTotalThesisScore.Text = "";
            AppThesisUploadCB.Checked = false;
            AppThesisJournalRefUploadCB.Checked = false;
            AppThesisUploadFUName.Text = "";
            AppThesisJournalRefCount.Text = "";
            RepresentCB.Checked = false;
            AppThesisJournalRefHyperLink.Visible = false;
            ThesisCoAuthorHyperLink.Visible = false;
            AppThesisHyperLink.Visible = false;
            RepresentCB.Checked = false;
            ThesisCoAuthorUploadCB.Checked = false;
            CountRPICB.Checked = false;

            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ThesisScoreCancel_Click(object sender, EventArgs e)
        {
            ThesisScoreSave.Visible = true;
            ThesisScoreUpdate.Visible = false;
            ThesisScoreCancel.Visible = false;
            //AppThesisPublishYearMonth.Text = "";
            RRNo.DataBind();
            AppThesisResearchResult.Text = "";
            AppThesisC.Text = "";
            AppThesisJ.Text = "";
            AppThesisA.Text = "";
            LabelTotalThesisScore.Text = "";
            AppThesisUploadCB.Checked = false;
            AppThesisUploadFUName.Text = "";
            AppThesisJournalRefCount.Text = "";
            RepresentCB.Checked = false;
        }

        protected void AppRPISave_Click(object sender, EventArgs e)
        {
            vApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(Session["AppSn"].ToString()));
            vApplyAudit.AppRPIScore = AppRPI.Text.ToString();
            if (AppRPI.Text.Length > 10)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('RPI研究表現指數含小數點，請填10位數以內!');", true);
            }
            else
            {
                if (crudObject.UpdatePartial(vApplyAudit))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('RPI更新成功!');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('RPI更新失敗!');", true);
                }
            }
        }

        protected void ThesisC_TextChanged(object sender, EventArgs e)
        {
            LabelTotalThesisScore.Text = calculate().ToString();
            //GVThesisScore.DataBind();

        }

        protected void ThesisJ_TextChanged(object sender, EventArgs e)
        {
            LabelTotalThesisScore.Text = calculate().ToString();
            //GVThesisScore.DataBind();
        }

        protected void ThesisA_TextChanged(object sender, EventArgs e)
        {
            LabelTotalThesisScore.Text = calculate().ToString();
            //GVThesisScore.DataBind();
        }
        protected void BtnExecuteEqual_Click(object sender, EventArgs e)
        {
            LabelTotalThesisScore.Text = calculate().ToString();
            //GVThesisScore.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);
        }
        private float calculate()
        {
            Session["ThesisAccuScore"] = "0";
            float total = 1f;
            float C = 1f;
            float J = 1f;
            float A = 1f;
            if (!AppThesisC.Text.ToString().Equals("")) C = float.Parse(AppThesisC.Text.ToString());
            if (!AppThesisJ.Text.ToString().Equals("")) J = float.Parse(AppThesisJ.Text.ToString());
            if (!AppThesisA.Text.ToString().Equals("")) A = float.Parse(AppThesisA.Text.ToString());
            total = C * J * A;
            return total;
        }
        protected void GVTeachEdu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labEduLocal;
            string location = "";
            if (Session["EmpSn"] != null)
            {
                location = Global.FileUpPath + Session["EmpSn"].ToString() + "/";
            }

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
                if (vApplyAudit != null && switchFgn != null && !switchFgn.Equals(""))
                {
                    VAppendDegree vAppendDegree = new VAppendDegree();
                    //載入學位論文
                    if (vApplyAudit.AppKindNo.Equals(chkApply) && AppAttributeNo.SelectedValue.Equals(chkDegree))
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
            Label lb_ExpUploadName;
            Label lb_NoUploadExp;
            string empSn = "1";
            string strFileName = "";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkTeachExp = (HyperLink)e.Row.FindControl("HyperLinkExp");
                lb_ExpUploadName = (Label)e.Row.FindControl("lb_ExpUploadName");
                lb_NoUploadExp = (Label)e.Row.FindControl("lb_NoUploadExp");
                //strFileName = e.Row.Cells[5].Text;
                strFileName = lb_ExpUploadName.Text;
                hyperLnkTeachExp.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLnkTeachExp.Text = "無資料";
                    hyperLnkTeachExp.Visible = false;
                    lb_NoUploadExp.Visible = true;
                }
            }
        }

        protected void GVTeachCa_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink hyperLnkTeachCa;
            Label lb_NoUploadCa, lb_CaUploadName;
            string empSn = "1";
            string strFileName = "";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkTeachCa = (HyperLink)e.Row.FindControl("HyperLinkCa");
                lb_NoUploadCa = (Label)e.Row.FindControl("lb_NoUploadCa");
                lb_CaUploadName = (Label)e.Row.FindControl("lb_CaUploadName");
                strFileName = lb_CaUploadName.Text;
                hyperLnkTeachCa.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLnkTeachCa.Text = "無資料";
                    hyperLnkTeachCa.Visible = false;
                    lb_NoUploadCa.Visible = true;
                }

            }
            if (GVTeachCa.Rows.Count == 0)
                lb_NoTeachCa.Visible = true;
            else
                lb_NoTeachCa.Visible = false;
        }

        protected void GVThesisScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            HyperLink hyperLnkThesis;
            Label lblAppSn;
            Label lblThesisSign;
            Label lblCountRPI;
            TextBox txtIsRepresentative;
            TextBox txtIsCountRPI;
            string empSn = "1";
            String SnNo;
            ImageButton imageButtonSnNoUp = null;
            ImageButton imageButtonSnNoDown = null;
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkThesis = (HyperLink)e.Row.FindControl("HyperLinkThesis");
                lblAppSn = (Label)e.Row.FindControl("AppSn");
                lblThesisSign = (Label)e.Row.FindControl("lblThesisSign");
                lblCountRPI = (Label)e.Row.FindControl("lblCountRPI");
                imageButtonSnNoUp = (ImageButton)e.Row.FindControl("ImageButtonSnNoUp");
                imageButtonSnNoDown = (ImageButton)e.Row.FindControl("ImageButtonSnNoDown");
                //if (Session["PublicationUploadName"]!=null&& Session["PublicationUploadName"].ToString().Equals(e.Row.Cells[10].Text))
                //{
                //    lblThesisSign.Text = "●";
                //}


                txtIsRepresentative = (TextBox)e.Row.FindControl("IsRepresentative");
                if (txtIsRepresentative.Text.ToString().Equals("True"))
                {
                    lblThesisSign.Text = "●";
                }
                txtIsCountRPI = (TextBox)e.Row.FindControl("IsCountRPI");
                if (txtIsCountRPI.Text.ToString().Equals("True"))
                {
                    lblCountRPI.Text = "○";
                }

                String strFileName = e.Row.Cells[16].Text;
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;")) isThesisNotUpload = true;
                hyperLnkThesis.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLnkThesis.Text = "無資料";
                    hyperLnkThesis.Enabled = false;
                }
                Session["ThesisAccuScore"] = float.Parse(e.Row.Cells[15].Text) + float.Parse(Session["ThesisAccuScore"].ToString());

                if (Convert.ToBoolean(txtIsCountRPI.Text))
                {
                    Session["ThesisRPIScore"] = float.Parse(e.Row.Cells[15].Text) + float.Parse(Session["ThesisRPIScore"].ToString());
                }
                SnNo = this.GVThesisScore.DataKeys[e.Row.RowIndex].Value.ToString();
                if (SnNo != null && SnNo.Equals("1"))
                {
                    imageButtonSnNoUp.Visible = false;
                }
                if (SnNo != null && SnNo.Equals(countThesisScorePaper))
                {
                    imageButtonSnNoDown.Visible = false;
                }
            }
            if (GVThesisScore.Rows.Count > 0)
                AppPThesisAccuScore.Text = Session["ThesisAccuScore"].ToString();
            else
                AppPThesisAccuScore.Text = "0";
            if (Session["ThesisAccuScore"] != null && Session["AppSn"] != null)
            {
                CRUDObject crudObject = new CRUDObject();
                crudObject.UpdThsisScore(Session["ThesisAccuScore"].ToString(), Session["AppSn"].ToString());
            }
        }

        //有勾選的篇數才計算
        protected void GVThesisScore_PreRender(object sender, EventArgs e)
        {
            if (Session["ResearchYear"] != null && !Session["ResearchYear"].ToString().Equals(""))
            {
                float floatYear = float.Parse(Session["ResearchYear"].ToString());
                double floatRPI = Double.Parse(Session["ThesisRPIScore"].ToString()) * 100f / (75f * floatYear);
                ComputeRPI.Text = Math.Round(floatRPI, 3, MidpointRounding.AwayFromZero).ToString();
            }
        }

        protected void GVThesisScoreCoAuthor_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labelThesisCoAuthorUploadName;
            Label labelThesisJournalRefUploadName;
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
                //string openFile = "UploadFiles/" + empSn + "/" + labelThesisCoAuthorUploadName.Text.ToString();
                //hyperLnkThesis.NavigateUrl = "javascript:void(window.open('" + openFile + "','_blank','height=800','width=800') )";

                String strFileName = labelThesisCoAuthorUploadName.Text.ToString();
                hyperLnkThesisCo.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLnkThesisCo.Text = "無資料";
                    hyperLnkThesisCo.Enabled = false;
                }

                strFileName = labelThesisJournalRefUploadName.Text.ToString();
                hyperLinkThesisJournalRef.NavigateUrl = getHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLinkThesisJournalRef.Text = "無資料";
                    hyperLinkThesisJournalRef.Enabled = false;
                }

            }
        }

        protected void AppUnitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppJobTitleNo.DataValueField = "JobTitleNo";
            AppJobTitleNo.DataTextField = "JobTitleName";
            AppJobTitleNo.DataSource = crudObject.GetJobTitleOpenDate(AppUnitNo.SelectedValue.ToString()).DefaultView;
            AppJobTitleNo.DataBind();

            //vApplyAudit.AppUnitNo = AppUnitNo.SelectedValue.ToString(); //沒有使用ajax 變動時立即儲存
            if (Session["AppSn"] != null && !Session["AppSn"].Equals("")) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
            Session["isLoadDataBtn"] = "N";

            //新聘類型：自動切臨床
            if (AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("400"))
            {
                AppAttributeNo.SelectedValue = "4";
                AppAttributeNo.SelectedItem.Text = "臨床教師新聘";
            }

            TransferDataToEmpObject();
            if (crudObject.GetApplyAuditObjByIdno(EmpIdno.Text.ToString()) != null)
            {
                if (TbAppSn.Text.ToString().Equals(""))
                {
                    if (crudObject.GetApplyAuditIsExist(vApplyAudit.EmpIdno, vApplyAudit.AppUnitNo) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('重覆申請單位!!'); ", true);
                        return;
                    }
                    else
                    {
                        //判斷是否是申請其他系所
                        if (Request["ApplyMore"] != null && Request["ApplyMore"].ToString().Equals("True"))
                        {
                            //暫存 新增單子
                            InsertAppendData();
                        }
                        else
                        {
                            vApplyAudit.AppUnitNo = AppUnitNo.SelectedValue.ToString();
                            if (Session["AppSn"] != null && !Session["AppSn"].Equals("")) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
                            crudObject.Update(vApplyAudit);
                        }
                    }
                }
                else
                {
                    vApplyAudit.AppUnitNo = AppUnitNo.SelectedValue.ToString();
                    if (Session["AppSn"] != null) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
                    crudObject.Update(vApplyAudit);
                }
            }
            else
            {
                SaveEmpObjectToDB(sender, e);
            }

            //判斷流程走到哪
            int intStage = 0;
            int intStep = 0;
            if (vApplyAudit.AppStage != null) intStage = Int32.Parse(vApplyAudit.AppStage);
            if (vApplyAudit.AppStep != null) intStep = Int32.Parse(vApplyAudit.AppStep);
            if (intStage <= 2 && intStep <= 2)
            {
                if (!AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("400") && //臨床教授
                  !AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("500") && //專案教授
                  !AppJobTypeNo.SelectedValue.ToString().Equals("2") && //兼職
                  !untArray.Contains(AppUnitNo.SelectedValue.ToString())) //醫療學科
                {
                    ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                    DESCrypt DES = new DESCrypt();
                    string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt("" + vApplyAudit.AppSn);
                    //parameters = Uri.EscapeDataString(parameters);
                    String strUrl = "ApplyShortEmp.aspx?" + parameters;
                    Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "alert('請先填簡式資料，待初審過才需填寫繁式資料!!');window.location='" + strUrl + "';", true);
                }
            }

        }

        //職稱選定
        protected void AppJobTitleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] num = { "零", "一", "二", "三", "四" };
            ItemLabel.Text = num[(7 - Int32.Parse(AppJobTitleNo.SelectedValue.ToString().Substring(1, 1)))];
            //AppELawNumNo.DataValueField = "LawItemNo";
            //AppELawNumNo.DataTextField = "LawItemNoCN";
            //AppELawNumNo.DataSource = crudObject.GetApplyTeacherLaw(AppEJobTitleNo.SelectedValue.ToString()).DefaultView;
            //AppELawNumNo.DataBind();
            AddELawNumControls(chkApply, AppJobTitleNo.SelectedValue.ToString(), "");
            ViewState["AppJobTitleNo"] = AppJobTitleNo.SelectedValue.ToString();
            AppJobTypeNo.DataValueField = "JobAttrNo";
            AppJobTypeNo.DataTextField = "JobAttrName";
            AppJobTypeNo.ClearSelection();
            AppJobTypeNo.DataSource = crudObject.GetJobTypeOpenDate(AppUnitNo.SelectedValue.ToString(), AppJobTitleNo.SelectedValue.ToString()).DefaultView;
            AppJobTypeNo.DataBind();
            //新聘類型：自動切臨床
            if (AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("400"))
            {
                AppAttributeNo.SelectedValue = "4";
                AppAttributeNo.SelectedItem.Text = "臨床教師新聘";
            }

            //新聘類型：專案教授只有專任
            if (AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("500"))
            {
                AppJobTypeNo.SelectedValue = "1";
                AppJobTypeNo.SelectedItem.Text = "專任";
            }

            TransferDataToEmpObject();
            vApplyAudit.AppJobTitleNo = AppJobTitleNo.SelectedValue.ToString();
            vApplyAudit.AppJobTypeNo = AppJobTypeNo.SelectedValue.ToString(); //沒有使用ajax 變動時立即儲存
            if (Session["AppSn"] != null) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
            crudObject.Update(vApplyAudit);
            Session["isLoadDataBtn"] = "N";
            //
            //判斷流程走到哪
            int intStage = 0;
            int intStep = 0;
            if (vApplyAudit.AppStage != null) intStage = Int32.Parse(vApplyAudit.AppStage);
            if (vApplyAudit.AppStep != null) intStep = Int32.Parse(vApplyAudit.AppStep);
            if (intStage <= 2 && intStep <= 2)
            {
                if (!AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("400") && //臨床教授
                  !AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("500") && //專案教授
                  !AppJobTypeNo.SelectedValue.ToString().Equals("2") &&  //兼職
                  !untArray.Contains(AppUnitNo.SelectedValue.ToString())) //醫療學科
                {
                    ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                    DESCrypt DES = new DESCrypt();
                    string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt("" + vApplyAudit.AppSn) + "&ApplyKindNo=" + ViewState["ApplyKindNo"].ToString() + "&ApplyWayNo=" + ViewState["ApplyWayNo"].ToString() + "&ApplyAttributeNo=" + ViewState["ApplyAttributeNo"].ToString();
                    String strUrl = "ApplyShortEmp.aspx?" + parameters;
                    Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "alert('請先填簡式資料，待初審過才需填寫繁式資料!!');window.location='" + strUrl + "';", true);
                }
            }
        }


        //職別
        protected void AppJobTypeNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] num = { "零", "一", "二", "三", "四" };
            ItemLabel.Text = num[(7 - Int32.Parse(AppJobTitleNo.SelectedValue.ToString().Substring(1, 1)))];
            //AppELawNumNo.DataValueField = "LawItemNo";
            //AppELawNumNo.DataTextField = "LawItemNoCN";
            //AppELawNumNo.DataSource = crudObject.GetApplyTeacherLaw(AppEJobTitleNo.SelectedValue.ToString()).DefaultView;
            //AppELawNumNo.DataBind();
            AddELawNumControls(chkApply, AppJobTitleNo.SelectedValue.ToString(), "");
            ViewState["AppJobTitleNo"] = AppJobTitleNo.SelectedValue.ToString();

            TransferDataToEmpObject();
            vApplyAudit.AppJobTitleNo = AppJobTitleNo.SelectedValue.ToString();
            vApplyAudit.AppJobTypeNo = AppJobTypeNo.SelectedValue.ToString(); //沒有使用ajax 變動時立即儲存
            if (Session["AppSn"] != null && !Session["AppSn"].ToString().Equals("")) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
            crudObject.Update(vApplyAudit);
            Session["isLoadDataBtn"] = "N";
            //
            //判斷流程走到哪
            int intStage = 0;
            int intStep = 0;
            if (vApplyAudit.AppStage != null) intStage = Int32.Parse(vApplyAudit.AppStage);
            if (vApplyAudit.AppStep != null) intStep = Int32.Parse(vApplyAudit.AppStep);
            if (intStage <= 2 && intStep <= 2)
            {
                if (!AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("400") && //臨床教授
                  !AppJobTitleNo.SelectedValue.ToString().Substring(3, 3).Equals("500") && //專案教授
                  !AppJobTypeNo.SelectedValue.ToString().Equals("2") &&  //兼職
                  !untArray.Contains(AppUnitNo.SelectedValue.ToString())) //醫療學科
                {
                    ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                    DESCrypt DES = new DESCrypt();
                    string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt("" + vApplyAudit.AppSn);
                    String strUrl = "ApplyShortEmp.aspx?" + parameters;
                    Page.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), "return", "alert('請先填簡式資料，待初審過才需填寫繁式資料!!');window.location='" + strUrl + "';", true);
                }
            }
        }

        private void AddELawNumControls(string kindNo, string jobTitleNo, string selectedNum)
        {
            if (jobTitleNo.Equals(""))
            {

            }
            else
            {
                //String titleNo = "" + (7 - Int32.Parse(jobTitleNo.ToString().Substring(1, 1))); //這是為了將職稱代碼順利的對應到法規條文

                DataTable dtELaw = crudObject.GetTeacherLaw(kindNo, jobTitleNo);

                ELawNum.DataSource = dtELaw;
                ELawNum.DataTextField = "LawContent";
                ELawNum.DataValueField = "LawItemNo";
                ELawNum.DataBind();
                for (int i = 0; i < dtELaw.Rows.Count; i++)
                {
                    if (dtELaw.Rows[i][0].Equals(selectedNum))
                    {
                        ELawNum.Items[i].Selected = true;
                    }
                    else
                    {
                        ELawNum.Items[i].Selected = false;
                    }
                }
            }
        }

        protected void DegreeThesisSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            string location = "";
            VAppendDegree vAppendDegree;
            bool insertflag = false;
            if (Session["EmpSn"] == null || AppDegreeThesisName.Text.Length > 100 || AppDegreeThesisNameEng.Text.Length > 100)
            {
                if (Session["EmpSn"] == null)
                {
                    msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                    EmpBaseTempSave_Click(sender, e);
                }
                if (AppDegreeThesisName.Text.Length > 100)
                    msg = "著作題名(中)字數請勿超過100字!";
                if (AppDegreeThesisNameEng.Text.Length > 100)
                    msg = "著作題名(英)字數請勿超過100字!";
            }
            else
            {
                location = Server.MapPath(Global.FileUpPath + vApplyAudit.AppSn + "/");
                //location = Server.MapPath("../DocuFile/HRApplyDoc/" + vApplyAudit.AppSn + "/"); 
                vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                if (vAppendDegree == null)
                {
                    insertflag = true;
                    vAppendDegree = new VAppendDegree();
                }
                vAppendDegree.AppSn = vApplyAudit.AppSn;
                if (AppDegreeThesisUploadFU.HasFile)
                {
                    if (AppDegreeThesisUploadFU.FileName != null && checkName(AppDegreeThesisUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDegreeThesisUploadCB.Checked = true;
                        //fileLists.Add(AppDegreeThesisUploadFU);
                        vAppendDegree.AppDDegreeThesisName = AppDegreeThesisName.Text;
                        vAppendDegree.AppDDegreeThesisNameEng = AppDegreeThesisNameEng.Text;
                        vAppendDegree.AppDDegreeThesisUploadName = AppDegreeThesisUploadFU.FileName;
                        //vAppendDegree.AppDDegreeThesisUpload = AppDegreeThesisUploadCB.Checked;
                        AppDegreeThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDegreeThesisUploadFU.FileName);

                    }
                }
                else
                {
                    vAppendDegree.AppDDegreeThesisName = AppDegreeThesisName.Text;
                    vAppendDegree.AppDDegreeThesisNameEng = AppDegreeThesisNameEng.Text;
                    vAppendDegree.AppDDegreeThesisUploadName = AppDegreeThesisUploadFUName.Text.ToString();
                    //vAppendDegree.AppDDegreeThesisUpload = AppDegreeThesisUploadCB.Checked;
                }

                //國外學歷寫入
                vAppendDegree.AppDFgnEduDeptSchoolAdmit = AppDFgnEduDeptSchoolAdmitCB.Checked;

                if (AppDFgnDegreeUploadFU.HasFile)
                {
                    if (AppDFgnDegreeUploadFU.FileName != null && checkName(AppDFgnDegreeUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnDegreeUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnDegreeUploadFU);
                        vAppendDegree.AppDFgnDegreeUploadName = AppDFgnDegreeUploadFU.FileName;
                        //vAppendDegree.AppDFgnDegreeUpload = AppDFgnDegreeUploadCB.Checked;
                        AppDFgnDegreeUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnDegreeUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnDegreeUploadName = AppDFgnDegreeUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnDegreeUpload = AppDFgnDegreeUploadCB.Checked;
                }

                if (AppDFgnGradeUploadFU.HasFile)
                {
                    if (AppDFgnGradeUploadFU.FileName != null && checkName(AppDFgnGradeUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnGradeUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnGradeUploadFU);
                        vAppendDegree.AppDFgnGradeUploadName = AppDFgnGradeUploadFU.FileName;
                        //vAppendDegree.AppDFgnGradeUpload = AppDFgnGradeUploadCB.Checked;
                        AppDFgnGradeUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnGradeUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnGradeUploadName = AppDFgnGradeUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnGradeUpload = AppDFgnGradeUploadCB.Checked;
                }

                if (AppDFgnSelectCourseUploadFU.HasFile)
                {
                    if (AppDFgnSelectCourseUploadFU.FileName != null && checkName(AppDFgnSelectCourseUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnSelectCourseUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnSelectCourseUploadFU);
                        vAppendDegree.AppDFgnSelectCourseUploadName = AppDFgnSelectCourseUploadFU.FileName;
                        //vAppendDegree.AppDFgnSelectCourseUpload = AppDFgnSelectCourseUploadCB.Checked;
                        AppDFgnSelectCourseUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnSelectCourseUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnSelectCourseUploadName = AppDFgnSelectCourseUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnSelectCourseUpload = AppDFgnSelectCourseUploadCB.Checked;
                }

                if (AppDFgnEDRecordUploadFU.HasFile)
                {
                    if (AppDFgnEDRecordUploadFU.FileName != null && checkName(AppDFgnEDRecordUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnEDRecordUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnEDRecordUploadFU);
                        vAppendDegree.AppDFgnEDRecordUploadName = AppDFgnEDRecordUploadFU.FileName;
                        //vAppendDegree.AppDFgnEDRecordUpload = AppDFgnEDRecordUploadCB.Checked;
                        AppDFgnEDRecordUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnEDRecordUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnEDRecordUploadName = AppDFgnEDRecordUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnEDRecordUpload = AppDFgnEDRecordUploadCB.Checked;
                }

                if (AppDFgnJPAdmissionUploadFU.HasFile)
                {
                    if (AppDFgnJPAdmissionUploadFU.FileName != null && checkName(AppDFgnJPAdmissionUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnJPAdmissionUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnJPAdmissionUploadFU);
                        vAppendDegree.AppDFgnJPAdmissionUploadName = AppDFgnJPAdmissionUploadFU.FileName;
                        //vAppendDegree.AppDFgnJPAdmissionUpload = AppDFgnJPAdmissionUploadCB.Checked;
                        AppDFgnJPAdmissionUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnJPAdmissionUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPAdmissionUploadName = AppDFgnJPAdmissionUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnJPAdmissionUpload = AppDFgnJPAdmissionUploadCB.Checked;
                }


                if (AppDFgnJPGradeUploadFU.HasFile)
                {
                    if (AppDFgnJPGradeUploadFU.FileName != null && checkName(AppDFgnJPGradeUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnJPGradeUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnJPGradeUploadFU);
                        vAppendDegree.AppDFgnJPGradeUploadName = AppDFgnJPGradeUploadFU.FileName;
                        //vAppendDegree.AppDFgnJPGradeUpload = AppDFgnJPGradeUploadCB.Checked;
                        AppDFgnJPGradeUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnJPGradeUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPGradeUploadName = AppDFgnJPGradeUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnJPGradeUpload = AppDFgnJPGradeUploadCB.Checked;
                }

                if (AppDFgnJPEnrollCAUploadFU.HasFile)
                {
                    if (AppDFgnJPEnrollCAUploadFU.FileName != null && checkName(AppDFgnJPEnrollCAUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnJPEnrollCAUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnJPEnrollCAUploadFU);
                        vAppendDegree.AppDFgnJPEnrollCAUploadName = AppDFgnJPEnrollCAUploadFU.FileName;
                        //vAppendDegree.AppDFgnJPEnrollCAUpload = AppDFgnJPEnrollCAUploadCB.Checked;
                        AppDFgnJPEnrollCAUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnJPEnrollCAUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPEnrollCAUploadName = AppDFgnJPEnrollCAUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnJPEnrollCAUpload = AppDFgnJPEnrollCAUploadCB.Checked;
                }

                if (AppDFgnJPDissertationPassUploadFU.HasFile)
                {
                    if (AppDFgnJPDissertationPassUploadFU.FileName != null && checkName(AppDFgnJPDissertationPassUploadFU.FileName))
                    {
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        AppDFgnJPDissertationPassUploadCB.Checked = true;
                        //fileLists.Add(AppDFgnJPDissertationPassUploadFU);
                        vAppendDegree.AppDFgnJPDissertationPassUploadName = AppDFgnJPDissertationPassUploadFU.FileName;
                        //vAppendDegree.AppDFgnJPDissertationPassUpload = AppDFgnJPDissertationPassUploadCB.Checked;
                        AppDFgnJPDissertationPassUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppDFgnJPDissertationPassUploadFU.FileName);
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPDissertationPassUploadName = AppDFgnJPDissertationPassUploadFUName.Text.ToString();
                    //vAppendDegree.AppDFgnJPDissertationPassUpload = AppDFgnJPDissertationPassUploadCB.Checked;
                }

                if (insertflag)
                {
                    if (crudObject.Insert(vAppendDegree))
                    {
                        msg = "學位論文上傳成功!! ";
                    }
                }
                else
                {
                    if (crudObject.Update(vAppendDegree))
                    {
                        msg = "學位論文上傳更新成功!! ";
                    }
                }
                if (fileLists.Count > 0) ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));


                //載入學位論文 AppDegreeThesisHyperLink
                vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                AppDegreeThesisName.Text = vAppendDegree.AppDDegreeThesisName;
                AppDegreeThesisNameEng.Text = vAppendDegree.AppDDegreeThesisNameEng;
                if (vAppendDegree.AppDDegreeThesisUploadName != null && !vAppendDegree.AppDDegreeThesisUploadName.Equals(""))
                {
                    //EmpIdUploadFUName.Text = vEmp.EmpIdUploadName;
                    AppDegreeThesisUploadCB.Checked = true;
                    AppDegreeThesisHyperLink.NavigateUrl = location + vAppendDegree.AppDDegreeThesisUploadName;
                    AppDegreeThesisHyperLink.Text = vAppendDegree.AppDDegreeThesisUploadName;
                    AppDegreeThesisHyperLink.Visible = true;
                    AppDegreeThesisUploadFU.Visible = true;
                    AppDegreeThesisUploadFUName.Text = vAppendDegree.AppDDegreeThesisUploadName;
                }
                else
                {
                    AppDegreeThesisUploadFU.Visible = true;
                    AppDegreeThesisHyperLink.Visible = false;
                }

                //載入外國學歷 以學位送審才需要
                if (AboutFgn.Visible)
                {
                    LoadFgn(vAppendDegree, switchFgn, location);
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }


        //寫入 學位論文:論文指導口試委員
        protected void ThesisOralSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {

                //if (ThesisOralName.Text == "" || ThesisOralTitle.Text == "" || ThesisOralUnit.Text == "" || ThesisOralOther.Text == "")
                if (ThesisOralName.Text == "" || ThesisOralTitle.Text == "" || ThesisOralUnit.Text == "" || ThesisOralName.Text.Length > 50 || ThesisOralTitle.Text.Length > 50 || ThesisOralUnit.Text.Length > 50 || ThesisOralOther.Text.Length > 100)
                {
                    if (ThesisOralName.Text == "")
                        msg += "姓名未填寫! ";
                    if (ThesisOralTitle.Text == "")
                        msg += "職稱未填寫! ";
                    if (ThesisOralUnit.Text == "")
                        msg += "服務單位未填寫! ";
                    if (ThesisOralName.Text.Length > 50)
                        msg += "姓名請勿超出50字! ";
                    if (ThesisOralTitle.Text.Length > 50)
                        msg += "職稱請勿超出50字! ";
                    if (ThesisOralUnit.Text.Length > 50)
                        msg += "服務單位請勿超出50字! ";
                    if (ThesisOralUnit.Text.Length > 100)
                        msg += "其他備註請勿超出100字! ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalAvoidList').modal('show'); });</script>", false);
                }
                else
                {

                    VThesisOral vThesisOral = new VThesisOral();
                    vThesisOral.ThesisOralAppSn = vApplyAudit.AppSn;
                    vThesisOral.ThesisOralType = ThesisOralType.SelectedValue.ToString();
                    vThesisOral.ThesisOralName = ThesisOralName.Text.ToString();
                    vThesisOral.ThesisOralTitle = ThesisOralTitle.Text.ToString();
                    vThesisOral.ThesisOralUnit = ThesisOralUnit.Text.ToString();
                    vThesisOral.ThesisOralOther = ThesisOralOther.Text.ToString();
                    if (crudObject.Insert(vThesisOral))
                    {
                        msg = "新增成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }

                }
                ThesisOralSave.Visible = true;
                ThesisOralUpdate.Visible = false;
                ThesisOralName.Text = "";
                ThesisOralTitle.Text = "";
                ThesisOralUnit.Text = "";
                ThesisOralOther.Text = "";
                GVThesisOral.DataSource = crudObject.GetThesisOralList(Int32.Parse(Session["AppSn"].ToString()));
                GVThesisOral.DataBind();
                if (GVThesisOral.Rows.Count == 0)
                    lb_NoThesisOral.Visible = true;
                else
                    lb_NoThesisOral.Visible = false;
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ThesisOralMod_Click(object sender, EventArgs e)
        {
            ThesisOralSave.Visible = false;
            ThesisOralUpdate.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            Label lblThesisOralSn = (Label)row.FindControl("ThesisOralSn");
            int intThesisOralSn = Convert.ToInt32(lblThesisOralSn.Text.ToString()); // here we are
            VThesisOral vThesisOral = crudObject.GetAppThesisOral(intThesisOralSn);
            TBIntThesisOralSn.Text = "" + intThesisOralSn;
            for (int i = 0; i < ThesisOralType.Items.Count - 1; i++)
            {
                if (vThesisOral.ThesisOralType.Equals(ThesisOralType.Items[i].Value))
                {
                    ThesisOralType.Items[i].Selected = true;
                }
                else
                {
                    ThesisOralType.Items[i].Selected = false;
                }
            }
            ThesisOralName.Text = vThesisOral.ThesisOralName;
            ThesisOralTitle.Text = vThesisOral.ThesisOralTitle;
            ThesisOralUnit.Text = vThesisOral.ThesisOralUnit;
            ThesisOralOther.Text = vThesisOral.ThesisOralOther;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisOral').modal('show'); });</script>", false);
        }

        protected void ThesisOralDel_Click(object sender, EventArgs e)
        {
            string msg = "";
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            Label lblThesisOralSn = (Label)row.FindControl("ThesisOralSn");
            int intThesisOralSn = Convert.ToInt32(lblThesisOralSn.Text.ToString()); // here we are
            if (crudObject.DeleteThesisOral(intThesisOralSn))
            {
                msg = "刪除一筆資料成功!";
            }
            GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
            GVThesisOral.DataBind();
            if (GVThesisOral.Rows.Count == 0)
                lb_NoThesisOral.Visible = true;
            else
                lb_NoThesisOral.Visible = false;
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ThesisOralUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            CRUDObject crudObject = new CRUDObject();
            VThesisOral vThesisOral = new VThesisOral();
            vThesisOral.ThesisOralSn = Convert.ToInt32(TBIntThesisOralSn.Text.ToString());
            vThesisOral.ThesisOralAppSn = vApplyAudit.AppSn;
            vThesisOral.ThesisOralType = ThesisOralType.SelectedValue;
            vThesisOral.ThesisOralName = ThesisOralName.Text;
            vThesisOral.ThesisOralTitle = ThesisOralTitle.Text;
            vThesisOral.ThesisOralUnit = ThesisOralUnit.Text;
            vThesisOral.ThesisOralOther = ThesisOralOther.Text;
            if (crudObject.Update(vThesisOral))
            {
                //寫入後載入下方的DataGridView
                msg = "更新成功!!";
            }
            else
            {
                msg = "抱歉，資料更新失敗，請洽資訊人員! ";
            }

            ThesisOralSave.Visible = true;
            ThesisOralUpdate.Visible = false;
            ThesisOralName.Text = "";
            ThesisOralTitle.Text = "";
            ThesisOralUnit.Text = "";
            ThesisOralOther.Text = "";
            GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
            GVThesisOral.DataBind();
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ThesisOralCancel_Click(object sender, EventArgs e)
        {
            ThesisOralSave.Visible = true;
            ThesisOralUpdate.Visible = false;
            ThesisOralCancel.Visible = false;
            ThesisOralName.Text = "";
            ThesisOralTitle.Text = "";
            ThesisOralUnit.Text = "";
            ThesisOralOther.Text = "";
            GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
            GVThesisOral.DataBind();

        }

        protected void TeachEduLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("TWN".Equals(TeachEduLocal.SelectedValue))
            {
                switchFgn = "TWN";
            }
            else
            {
                AboutFgn.Visible = true;
                if ("JPN".Equals(TeachEduLocal.SelectedValue))
                {
                    AboutJPN.Visible = true;
                    switchFgn = "JPN";
                }
            }
        }


        protected void GVThesisOral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labThesisOralType;
            String[] strDegreeThesisType = { "", "論文指導教師", "口試委員", "應迴避人" };
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                labThesisOralType = (Label)e.Row.FindControl("ThesisOralType");
                labThesisOralType.Text = strDegreeThesisType[Convert.ToInt32(labThesisOralType.Text.ToString())];
            }
        }

        //SCI Paper總數計算後重新顯示並儲存 新聘key只能用身分證
        protected void ThesisScoreReCount_onClick(object sender, EventArgs e)
        {
            if (txt_FSCI.Text.Length > 3 || txt_FSSCI.Text.Length > 3 || txt_FSEI.Text.Length > 3 ||
                txt_FNSCI.Text.Length > 3 || txt_FSOther.Text.Length > 3 || txt_NFCSCI.Text.Length > 3 ||
                txt_NFCSSCI.Text.Length > 3 || txt_NFCEI.Text.Length > 3 || txt_NFCNSCI.Text.Length > 3 ||
                txt_NFCOther.Text.Length > 3 || txt_NFOCSCI.Text.Length > 3 || txt_NFOCSSCI.Text.Length > 3 ||
                txt_NFOCEI.Text.Length > 3 || txt_NFOCNSCI.Text.Length > 3 || txt_NFOCOther.Text.Length > 3
                )
                Response.Write("<script>alert('填寫值請勿超過3位數!');</script>");
            else
            {
                try
                {
                    vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text);
                    //初始化所有SCI Paper總數計算未填寫為0
                    txt_FSCI.Text = txt_FSCI.Text.ToString() == "" ? "0" : txt_FSCI.Text.ToString();
                    txt_FSSCI.Text = txt_FSSCI.Text.ToString() == "" ? "0" : txt_FSSCI.Text.ToString();
                    txt_FSEI.Text = txt_FSEI.Text.ToString() == "" ? "0" : txt_FSEI.Text.ToString();
                    txt_FNSCI.Text = txt_FNSCI.Text.ToString() == "" ? "0" : txt_FNSCI.Text.ToString();
                    txt_FSOther.Text = txt_FSOther.Text.ToString() == "" ? "0" : txt_FSOther.Text.ToString();
                    txt_NFCSCI.Text = txt_NFCSCI.Text.ToString() == "" ? "0" : txt_NFCSCI.Text.ToString();
                    txt_NFCSSCI.Text = txt_NFCSSCI.Text.ToString() == "" ? "0" : txt_NFCSSCI.Text.ToString();
                    txt_NFCEI.Text = txt_NFCEI.Text.ToString() == "" ? "0" : txt_NFCEI.Text.ToString();
                    txt_NFCNSCI.Text = txt_NFCNSCI.Text.ToString() == "" ? "0" : txt_NFCNSCI.Text.ToString();
                    txt_NFCOther.Text = txt_NFCOther.Text.ToString() == "" ? "0" : txt_NFCOther.Text.ToString();
                    txt_NFOCSCI.Text = txt_NFOCSCI.Text.ToString() == "" ? "0" : txt_NFOCSCI.Text.ToString();
                    txt_NFOCSSCI.Text = txt_NFOCSSCI.Text.ToString() == "" ? "0" : txt_NFOCSSCI.Text.ToString();
                    txt_NFOCEI.Text = txt_NFOCEI.Text.ToString() == "" ? "0" : txt_NFOCEI.Text.ToString();
                    txt_NFOCNSCI.Text = txt_NFOCNSCI.Text.ToString() == "" ? "0" : txt_NFOCNSCI.Text.ToString();
                    txt_NFOCOther.Text = txt_NFOCOther.Text.ToString() == "" ? "0" : txt_NFOCOther.Text.ToString();

                    txt_SCI.Text = Convert.ToString(Convert.ToUInt16(txt_FSCI.Text) + Convert.ToUInt16(txt_NFCSCI.Text) + Convert.ToUInt16(txt_NFOCSCI.Text));
                    txt_SSCI.Text = Convert.ToString(Convert.ToUInt16(txt_FSSCI.Text) + Convert.ToUInt16(txt_NFCSSCI.Text) + Convert.ToUInt16(txt_NFOCSSCI.Text));
                    txt_EI.Text = Convert.ToString(Convert.ToUInt16(txt_FSEI.Text) + Convert.ToUInt16(txt_NFCEI.Text) + Convert.ToUInt16(txt_NFOCEI.Text));
                    txt_NSCI.Text = Convert.ToString(Convert.ToUInt16(txt_FNSCI.Text) + Convert.ToUInt16(txt_NFCNSCI.Text) + Convert.ToUInt16(txt_NFOCNSCI.Text));
                    txt_Others.Text = Convert.ToString(Convert.ToUInt16(txt_FSOther.Text) + Convert.ToUInt16(txt_NFCOther.Text) + Convert.ToUInt16(txt_NFOCOther.Text));


                    VThesisScoreCount vThesisScoreCount = new VThesisScoreCount();
                    String nowdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                    String writeYear = "" + (Int32.Parse(nowdate.Substring(0, 4)) - 1);
                    vThesisScoreCount.PT_Year = writeYear;
                    vThesisScoreCount.PT_EmpId = vEmp.EmpIdno;
                    vThesisScoreCount.PT_InPerson = vEmp.EmpEmail.Split('@')[0].ToString();
                    vThesisScoreCount.PT_FSCI = Convert.ToInt16(txt_FSCI.Text.ToString());
                    vThesisScoreCount.PT_FSSCI = Convert.ToInt16(txt_FSSCI.Text.ToString());
                    vThesisScoreCount.PT_FEI = Convert.ToInt16(txt_FSEI.Text.ToString());
                    vThesisScoreCount.PT_FNSCI = Convert.ToInt16(txt_FNSCI.Text.ToString());
                    vThesisScoreCount.PT_FOther = Convert.ToInt16(txt_FSOther.Text.ToString());
                    vThesisScoreCount.PT_NFCSCI = Convert.ToInt16(txt_NFCSCI.Text.ToString());
                    vThesisScoreCount.PT_NFCSSCI = Convert.ToInt16(txt_NFCSSCI.Text.ToString());
                    vThesisScoreCount.PT_NFCEI = Convert.ToInt16(txt_NFCEI.Text.ToString());
                    vThesisScoreCount.PT_NFCNSCI = Convert.ToInt16(txt_NFCNSCI.Text.ToString());
                    vThesisScoreCount.PT_NFCOther = Convert.ToInt16(txt_NFCOther.Text.ToString());
                    vThesisScoreCount.PT_NFOCSCI = Convert.ToInt16(txt_NFOCSCI.Text.ToString());
                    vThesisScoreCount.PT_NFOCSSCI = Convert.ToInt16(txt_NFOCSSCI.Text.ToString());
                    vThesisScoreCount.PT_NFOCEI = Convert.ToInt16(txt_NFOCEI.Text.ToString());
                    vThesisScoreCount.PT_NFOCNSCI = Convert.ToInt16(txt_NFOCNSCI.Text.ToString());
                    vThesisScoreCount.PT_NFOCOther = Convert.ToInt16(txt_NFOCOther.Text.ToString());
                    DataTable dt = crudObject.GetThesisScoreCount(vEmp.EmpIdno);//新聘寫入身分證號
                    if (dt.Rows.Count > 0)
                    {
                        crudObject.Update(vThesisScoreCount);
                    }
                    else
                    {
                        crudObject.Insert(vThesisScoreCount);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('SCI Paper總數計算失敗!');</script>");
                    throw ex;
                }
            }
        }

        protected void CBNoTeachExp_OnCheckedChanged(object sender, EventArgs e)
        {
            if (CBNoTeachExp.Checked)
            {
                TeachExpSave.Enabled = false;
            }
            else
            {
                TeachExpSave.Enabled = true;
            }
        }


        protected void CBNoTeachCa_OnCheckedChanged(object sender, EventArgs e)
        {
            if (CBNoTeachCa.Checked)
            {
                TeachCaSave.Enabled = false;
            }
            else
            {
                TeachCaSave.Enabled = true;
            }
        }

        protected void CBNoHonour_OnCheckedChanged(object sender, EventArgs e)
        {
            if (CBNoHonour.Checked)
            {
                TeachHornourSave.Enabled = false;
            }
            else
            {
                TeachHornourSave.Enabled = true;
            }
        }

        protected String getHyperLink(string strFileName)
        {
            String strHyperLink = "";
            //String openFile = "../DocuFile/HRApplyDoc/" + Session["EmpSn"].ToString() + "/" + strFileName;
            String openFile = Global.FileUpPath + Session["EmpSn"].ToString() + "/" + strFileName;
            if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
            {
                strHyperLink = "javascript:void(window.open('" + Global.FileUpPath + "NoUploadFile.pdf','_blank','location=no,height=700,width=300') )";
            }
            else
            {
                strHyperLink = "javascript:void(window.open('" + openFile + "','_blank','location=no',height='700',width='300') )";
            }
            return strHyperLink;
        }

        protected bool checkName(string strFileName)
        {
            string[] str = { "／", "？", "＊", "［", " ］", "'", "&", "?" };
            bool exists = false;
            if (strFileName != null)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    exists = strFileName.Contains(str[i]);
                    if (exists)
                    {
                        Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                        return false;
                    }
                }
                return true;
            }
            else
            {
                Response.Write("<script>alert('檔案上傳失敗!請重新上傳');</script>");
                return false;
            }
        }

        protected void BtnSaveYear_Click(object sender, EventArgs e)
        {
            ConnectionStringSettings connstring = WebConfigurationManager.ConnectionStrings["tmuConnectionString"];
            using (SqlConnection conn1 = new SqlConnection(connstring.ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conn1;
                command.CommandType = CommandType.Text;
                conn1.Open();
                try
                {
                    command.CommandText = "update ApplyAudit set AppYear=@AppYear, AppSemester=@AppSemester "
                                + " where AppSn=@AppSn ";
                    SqlParameter[] paramArray ={
                                        new SqlParameter("@AppYear",VYear.Text.Trim()),
                                        new SqlParameter("@AppSemester",VSemester.Text.Trim()),
                                        new SqlParameter("@AppSn",Session["AppSn"].ToString()),
                                        };
                    for (int i = 0; i < paramArray.Length; i++)
                    {
                        command.Parameters.Add(paramArray[i]);
                    }
                    command.ExecuteNonQuery();
                    Response.Write("<script>alert('修改成功!');location.href='" + Request.Url.ToString() + "'; </script>");
                }
                catch (SqlException e1)
                {
                    Response.Write("<Script language='JavaScript'>alert('更換學年學期失敗');</Script>");
                }
            }
        }


        protected void lkb_logout_Click(object sender, EventArgs e)
        {
            Session["LoginEmail"] = null;
            Session.Abandon();
            Response.Redirect("http://hr2sys.tmu.edu.tw/HRApply/default.aspx");
        }

        protected string checkSheets()
        {
            string errorMsg = "";
            string empBase = ""; //基本資料
            string empSelf = ""; //評議表
            string empTeach = ""; //學歷資料
            string empTeachExp = ""; //經歷資料
            string empTeachCa = ""; //教師證書
            string empTeachHor = ""; //學術獎勵、榮譽事項
            string empThesisScore = ""; //上傳論文&積分
            string empAppDegreeThesis = ""; //學位論文相關

            #region 基本資料檢測
            if (AppJobTypeNo.SelectedValue == "請選擇" || ELawNum.SelectedValue == "" || EmpPassportNo.Text == "" || EmpNameENFirst.Text == "" || EmpNameENLast.Text == "" || EmpSex.SelectedValue == "請選擇" || EmpTelPri.Text == "" || EmpTelPub.Text == "" || EmpTownAddressCode.Text == "" || EmpTownAddress.Text == "" || EmpTownAddress.Text == "" || EmpAddressCode.Text == "" || EmpAddress.Text == "" || EmpNowJobOrg.Text == "" || AppRecommendors.Text == "" || EmpExpertResearch.Text == "" || EmpPhotoImage.ImageUrl == "" || EmpIdnoHyperLink.Text == "" || EmpDegreeHyperLink.Text == "" || AppDeclarationHyperLink.Text == "")
            {
                if (AppJobTypeNo.SelectedValue == "請選擇")
                    empBase += "應徵職別未選、";
                if (ELawNum.SelectedValue == "")
                    empBase += "法規依據未選、";
                //if (EmpBirthDay.Text == "")
                //    empBase += "生日(民國)未填、";
                if (EmpPassportNo.Text == "")
                    empBase += "護照號碼未填、";
                if (EmpNameENFirst.Text == "")
                    empBase += "英文名未填、";
                if (EmpNameENLast.Text == "")
                    empBase += "英文姓未填、";
                if (EmpSex.SelectedValue == "請選擇")
                    empBase += "性別未選、";
                if (EmpBornCity.SelectedValue == "請選擇")
                    empBase += "出生地-縣市未選、";
                if (EmpTelPri.Text == "")
                    empBase += "電話(宅)未填、";
                if (EmpTelPub.Text == "")
                    empBase += "電話(公)未填、";
                if (EmpTownAddressCode.Text == "")
                    empBase += "戶籍地址郵遞區號未填、";
                if (EmpTownAddress.Text == "")
                    empBase += "戶籍地址未填、";
                if (EmpAddressCode.Text == "")
                    empBase += "通訊地址郵遞區號未填、";
                if (EmpAddress.Text == "")
                    empBase += "通訊地址未填、";
                if (EmpTelPub.Text == "")
                    empBase += "手機未填、";
                if (AppRecommendors.Text == "")
                    empBase += "推薦人姓名未填、";
                if (EmpNowJobOrg.Text == "")
                    empBase += "現任機關及職務未填、";
                //if (AppRecommendYear.Text == "")
                //    empBase += "推薦日期(民國)未填、";
                if (EmpExpertResearch.Text == "")
                    empBase += "學術專長及研究未填、";
                if (EmpPhotoImage.ImageUrl == "")
                    empBase += "照片未上傳、";
                if (EmpIdnoHyperLink.Text == "")
                    empBase += "身份證上傳未上傳、";
                if (EmpDegreeHyperLink.Text == "")
                    empBase += "最高學歷證件上傳未上傳、";
                if (AppDeclarationHyperLink.Text == "")
                    empBase += "教師資格審查資料切結書未上傳、";
                if (empBase != "")
                    errorMsg += "基本資料-" + empBase.Substring(0, empBase.Length - 1) + "\\n\\n";
            }
            #region 基本資料-其他檢測
            empBase = "";
            switch (AppAttributeNo.SelectedValue)
            {
                case "1":
                    if (AppTeacherCaHyperLink.Text == "")
                        empBase += "基本資料(其他)-教育部教師資格證書影 / 現職等部定證書(pdf)未上傳、";
                    if (AppOtherServiceHyperLink.Text == "")
                        empBase += "基本資料(其他)-服務成果(pdf)未上傳、";
                    break;
                case "2":
                    if (AppTeacherCaHyperLink.Text == "")
                        empBase += "基本資料(其他)-教育部教師資格證書影 / 現職等部定證書(pdf)未上傳、";
                    if (AppOtherTeachingHyperLink.Text == "")
                        empBase += "基本資料(其他)-教學成果(pdf)未上傳、";
                    if (AppOtherServiceHyperLink.Text == "")
                        empBase += "基本資料(其他)-服務成果(pdf)未上傳、";
                    break;
                case "3":
                    if (AppPPMHyperLink.Text == "")
                        empBase += "基本資料(其他)-研究計劃主持(pdf)未上傳、";
                    if (AppTeacherCaHyperLink.Text == "")
                        empBase += "基本資料(其他)-教育部教師資格證書影/現職等部定證書(pdf)未上傳、";
                    if (AppOtherTeachingHyperLink.Text == "")
                        empBase += "基本資料(其他)-教學成果(pdf)未上傳、";
                    if (AppOtherServiceHyperLink.Text == "")
                        empBase += "基本資料(其他)-服務成果(pdf)未上傳、";
                    break;
                case "4":
                    if (AppPPMTableRow.Visible == true && AppPPMHyperLink.Text == "")
                        empBase += "基本資料(其他)-研究計劃主持(pdf)未上傳、";
                    if (AppDrCaHyperLink.Text == "")
                        empBase += "基本資料(其他)-醫師證書(pdf)未上傳、";
                    if (OtherTeachingTableRow.Visible == true && AppOtherTeachingHyperLink.Text == "")
                        empBase += "基本資料(其他)-教學成果(pdf)未上傳、";
                    if (AppOtherServiceHyperLink.Text == "")
                        empBase += "基本資料(其他)-服務成果(pdf)未上傳、";
                    break;
            }
            if (empBase != "")
                errorMsg += empBase.Substring(0, empBase.Length - 1) + "\\n";
            #endregion

            #endregion
            #region 評議表資料檢測
            if (EmpSelfTeachExperience.Text == "" || EmpSelfReach.Text == "" || EmpSelfDevelope.Text == "" || EmpSelfSpecial.Text == "" || EmpSelfImprove.Text == "" || EmpSelfContribute.Text == "" || EmpSelfCooperate.Text == "" || EmpSelfTeachPlan.Text == "" || EmpSelfLifePlan.Text == "")
            {
                if (EmpSelfTeachExperience.Text == "")
                    empSelf += "教學經驗未填、";
                if (EmpSelfReach.Text == "")
                    empSelf += "研究經驗未填、";
                if (EmpSelfDevelope.Text == "")
                    empSelf += "發展潛力分析未填、";
                if (EmpSelfSpecial.Text == "")
                    empSelf += "專長評估未填、";
                if (EmpSelfImprove.Text == "")
                    empSelf += "尚待加強部份未填、";
                if (EmpSelfContribute.Text == "")
                    empSelf += "對本校及本單位預期貢獻未填、";
                if (EmpSelfCooperate.Text == "")
                    empSelf += "與其他領域合作能力未填、";
                if (EmpSelfTeachPlan.Text == "")
                    empSelf += "研究及教學計劃未填、";
                if (EmpSelfLifePlan.Text == "")
                    empSelf += "個人生涯展望未填、";

                if (empSelf != "")
                    errorMsg += "評議表-" + empSelf.Substring(0, empSelf.Length - 1) + "\\n";
            }

            #endregion
            #region 學歷資料檢測
            if (GVTeachEdu.Rows.Count <= 0)
            {
                empTeach += "學歷資料未填";
                errorMsg += "學歷資料-" + empTeach + "\\n";
            }
            #endregion
            #region 經歷資料檢測
            if (CBNoTeachExp.Checked == false && GVTeachExp.Rows.Count <= 0)
            {
                empTeachExp += "經歷資料未填或未勾選無相關資料";
                errorMsg += "經歷資料-" + empTeachExp + "\\n";
            }
            #endregion
            #region 教師證書檢測
            if (CBNoTeachCa.Checked == false && GVTeachCa.Rows.Count <= 0)
            {
                empTeachCa += "證書資料未填或未勾選無相關資料";
                errorMsg += "教師證書-" + empTeachCa + "\\n";
            }
            #endregion
            #region 學術獎勵、榮譽事項檢測
            if (CBNoHonour.Checked == false && GVTeachHonour.Rows.Count <= 0)
            {
                empTeachHor += "資料未填或未勾選無相關資料";
                errorMsg += "學術獎勵、榮譽事項-" + empTeachHor + "\\n";
            }
            #endregion
            #region 上傳論文&積分檢測
            if (AppAttributeNo.SelectedValue == "3" && GVThesisScore.Rows.Count <= 0)
            {
                empThesisScore += "論文積分未填寫上傳";
                errorMsg += "上傳論文&積分-" + empThesisScore + "\\n";
            }
            #endregion
            #region 學位論文相關檢測
            if (AppAttributeNo.SelectedValue == "2")
            {
                if (GVThesisScore.Rows.Count <= 0 || AppDegreeThesisName.Text == "" || AppDegreeThesisNameEng.Text == "" || AppDegreeThesisHyperLink.Text == "")
                {
                    if (AppDegreeThesisName.Text == "")
                        empAppDegreeThesis += "著作題名(中)未填、";
                    if (AppDegreeThesisNameEng.Text == "")
                        empAppDegreeThesis += "著作題名(英)未填、";
                    if (AppDegreeThesisHyperLink.Text == "")
                        empAppDegreeThesis += "學位論文著作(pdf)未上傳、";
                    if (GVThesisScore.Visible == true && GVThesisScore.Rows.Count <= 0)
                        empAppDegreeThesis += "指導教師或口試委員未填、";
                    if (GVThesisOral.Visible == true && GVThesisOral.Rows.Count <= 0)
                        empAppDegreeThesis += "迴避名單未填、";
                    if (empAppDegreeThesis != "")
                        errorMsg += "學位論文相關-" + empAppDegreeThesis.Substring(0, empAppDegreeThesis.Length - 1) + "\\n";
                }
            }
            #endregion
            return errorMsg;
        }


        protected void teachEduAdd_Click(object sender, EventArgs e)
        {
            TeachEduSave.Visible = true;
            TeachEduUpdate.Visible = false;
            TeachEduLocal.SelectedValue = "TTO";
            TeachEduSchool.Text = "";
            TeachEduDepartment.Text = "";
            TeachEduDegree.SelectedValue = "20";
            ddl_TeachEduStartYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_TeachEduStartMonth.SelectedValue = "01";
            ddl_TeachEduEndYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_TeachEduEndMonth.SelectedValue = "01";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachEdu').modal('show'); });</script>", false);
        }


        protected void teachExpAdd_Click(object sender, EventArgs e)
        {
            TeachExpSave.Visible = true;
            TeachExpUpdate.Visible = false;
            TeachExpOrginization.Text = "";
            TeachExpUnit.Text = "";
            TeachExpJobTitle.Text = "";
            ddl_TeachExpStartYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_TeachExpStartMonth.SelectedValue = "01";
            ddl_TeachExpEndYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_TeachExpEndMonth.SelectedValue = "01";
            TeachExpUploadFU.Dispose();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachExp').modal('show'); });</script>", false);
        }

        protected void teachCaAdd_Click(object sender, EventArgs e)
        {
            TeachCaSave.Visible = true;
            TeachCaUpdate.Visible = false;
            TeachCaNumberCN.Text = "";
            TeachCaNumber.Text = "";
            TeachCaPublishSchool.Text = "";
            ddl_TeachCaStartYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_TeachCaStartMonth.SelectedValue = "01";
            ddl_TeachCaEndYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_TeachCaEndMonth.SelectedValue = "01";
            TeachCaUploadFU.Dispose();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachCa').modal('show'); });</script>", false);
        }

        protected void teachHonorAdd_Click(object sender, EventArgs e)
        {
            TeachHornourSave.Visible = true;
            TeachHornourUpdate.Visible = false;
            TeachHorDescription.Text = "";
            ddl_TeachHorYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachHonour').modal('show'); });</script>", false);
        }

        protected void ThesisScoreInsert_Click(object sender, EventArgs e)
        {
            AppResearchYear.SelectedValue = "1";
            RRNo.SelectedValue = "1";
            AppThesisResearchResult.Text = "";
            ddl_AppThesisPublishYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_AppThesisPublishMonth.SelectedValue = "01";
            AppThesisC.Text = "";
            AppThesisJ.Text = "";
            AppThesisA.Text = "";
            LabelTotalThesisScore.Text = "";
            AppThesisName.Text = "";
            AppThesisUploadCB.Checked = false;
            AppThesisUploadFU.Dispose();
            AppThesisJournalRefCount.Text = "";
            AppThesisJournalRefUploadCB.Checked = false;
            AppThesisJournalRefUploadFU.Dispose();
            RepresentCB.Checked = false;
            CountRPICB.Checked = false;
            AppThesisHyperLink.Text = "";
            AppThesisJournalRefHyperLink.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);
        }

        protected void thesisOralAdd_Click(object sender, EventArgs e)
        {
            ThesisOralSave.Visible = true;
            ThesisOralUpdate.Visible = false;
            ThesisOralName.Text = "";
            ThesisOralTitle.Text = "";
            ThesisOralUnit.Text = "";
            ThesisOralOther.Text = "";
            ThesisOralType.SelectedValue = "1";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisOral').modal('show'); });</script>", false);
        }

        private void allDDL_refresh()
        {
            ddl_EmpBirthYear.Items.Clear();
            ddl_EmpBirthDate.Items.Clear();
            ddl_AppRecommendYear.Items.Clear();
            ddl_TeachEduStartYear.Items.Clear();
            ddl_TeachEduEndYear.Items.Clear();
            ddl_TeachExpStartYear.Items.Clear();
            ddl_TeachExpEndYear.Items.Clear();
            ddl_TeachCaStartYear.Items.Clear();
            ddl_TeachCaEndYear.Items.Clear();
            ddl_TeachHorYear.Items.Clear();
            ddl_AppThesisPublishYear.Items.Clear();
            int nowY = DateTime.Now.Year - 1911;
            for (int i = nowY; i >= 30; i--)
            {
                if (i.ToString().Length < 3)
                {
                    ddl_EmpBirthYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_AppRecommendYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_TeachEduStartYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_TeachEduEndYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_TeachExpStartYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_TeachCaStartYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_TeachCaEndYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_TeachHorYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                    ddl_AppThesisPublishYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    ddl_EmpBirthYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_AppRecommendYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_TeachEduStartYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_TeachEduEndYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_TeachExpStartYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_TeachCaStartYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_TeachCaEndYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_TeachHorYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    ddl_AppThesisPublishYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            for (int i = 160; i >= 30; i--)
                ddl_TeachExpEndYear.Items.Add(new ListItem(i.ToString(), i.ToString("000")));
            for (int i = 1; i <= 31; i++)
            {
                if (i.ToString().Length < 2)
                {
                    ddl_EmpBirthDate.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                }
                else
                {
                    ddl_EmpBirthDate.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
            }
            ddl_EmpBirthYear.DataBind();
            ddl_AppRecommendYear.DataBind();
            ddl_TeachEduStartYear.DataBind();
            ddl_TeachEduEndYear.DataBind();
            ddl_TeachExpStartYear.DataBind();
            ddl_TeachExpEndYear.DataBind();
            ddl_TeachCaStartYear.DataBind();
            ddl_TeachCaEndYear.DataBind();
            ddl_TeachHorYear.DataBind();
            ddl_AppThesisPublishYear.DataBind();
        }
        protected void saveReasearchResult_Click(object sender, EventArgs e)
        {
            if (AppReasearchResultUpload.HasFile)
            {
                if (AppReasearchResultUpload.FileName != null && checkName(AppReasearchResultUpload.FileName))
                {
                    string strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath + Session["EmpSn"].ToString() + "\\";

                    //fileLists.Add(AppReasearchResultUpload);
                    //vEmp.EmpIdUpload = EmpIdnoUploadCB.Checked;
                    AppReasearchResultUpload.PostedFile.SaveAs(strFolderPath + AppReasearchResultUpload.FileName);
                    vApplyAudit.ReasearchResultUploadName = AppReasearchResultUpload.FileName;
                    crudObject.Update(vApplyAudit);
                }
            }
        }
    }
}
