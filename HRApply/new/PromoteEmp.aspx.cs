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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Configuration;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace ApplyPromote
{


    public partial class PromoteEmp : PageBaseApply, System.Web.UI.ICallbackEventHandler
    {

        string tooltip;

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

        VEmpTmuHr vEmpTmuHr;

        VEmployeeBase vEmp;

        VApplyAudit vApplyAudit;


        //論文積分
        float totalThesisScore = 1.0f;


        //新聘申請著作送審
        static string chkPublication = "1";

        //新聘申請學位送審
        static string chkDegree = "2";

        //新聘臨床教師新聘4
        static string chkClinical = "3";

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

        int diffYear = 0;
        int diffMonth = 0;

        static string fillTeachEdu = "0";
        static string fillTeachExp = "0";
        static string fillTeachLesson = "0";
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
            ViewTeachEdu = 1,
            ViewTeachExp = 2,
            ViewThesisScore = 3,
            ViewTeachLesson = 4,
            ViewDegreeThesis = 5
        }


        //上傳檔案
        NameValueCollection fileCollection = new NameValueCollection();


        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ViewState["postBackTimes"] = -1;
                Session["index"] = null;
            }
            else
            {
                //if (!this.ToolkitScriptManager.IsInAsyncPostBack)
                //{
                this.ViewState["postBackTimes"] = Convert.ToInt16(this.ViewState["postBackTimes"]) - 1;
                //}
            }
            string gobackjs = @"function MyBack() 
                        {history.go(" + Convert.ToString(this.ViewState["postBackTimes"]) + @");}";
            //ToolkitScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "back", gobackjs, true);            
        }


        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (Session["LoginEmail"] != null)
            {
                if (Request["ApplyerID"] != null) EmpIdno.Text = Request["ApplyerID"].ToString();
                //學位送審的頁籤:著作論文相關,論文指導&口試委員名單 
                ViewState["ApplyAttributeNo"] = null;
                if (Request["ApplyAttributeNo"] != null && !Request["ApplyAttributeNo"].ToString().Equals(""))
                {
                    ViewState["ApplyAttributeNo"] = Request["ApplyAttributeNo"].ToString();
                }
                DESCrypt DES = new DESCrypt();
                EmpIdno.Text = DES.Decrypt(Request["ApplyerID"].ToString());
                vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text.ToString());
                if (vEmp != null)
                {
                    if (Request["HRAppSn"] != null && !Request["HRAppSn"].ToString().Equals(""))
                        vApplyAudit = crudObject.GetApplyAuditObjByHRAppSnPromote(EmpIdno.Text.ToString(), Request["HRAppSn"].ToString());
                    else if (Request["AppSn"] != null && !Request["AppSn"].ToString().Equals(""))
                        vApplyAudit = crudObject.GetApplyAuditObjByAppSnPromote(EmpIdno.Text.ToString(), DES.Decrypt(Request["AppSn"].ToString()));
                    else
                        vApplyAudit = crudObject.GetApplyAuditObjByIdnoPromote(EmpIdno.Text.ToString());
                    TbEmpSn.Text = "" + vEmp.EmpSn;
                    if (vApplyAudit != null && ViewState["ApplyAttributeNo"] == null)
                    {
                        Session["AppSn"] = vApplyAudit.AppSn;
                        countThesisScorePaper = crudObject.GetVThesisScoreTotalCount(vApplyAudit.EmpSn, vApplyAudit.AppSn).ToString();
                        if (AppAttributeNo.SelectedValue.Equals(chkDegree))
                        {
                            ViewState["ApplyAttributeNo"] = vApplyAudit.AppAttributeNo;
                            MVall.SetActiveView(ViewDegreeThesis);
                            Menu1.Visible = true;
                            Menu2.Visible = false;
                            Menu3.Visible = false;
                        }
                        else
                        {
                            if (vApplyAudit.AppAttributeNo.Equals(chkPublication))
                            {
                                MVall.SetActiveView(ViewDegreeThesis);
                                Menu1.Visible = false;
                                Menu2.Visible = false;
                                Menu3.Visible = true;
                                AboutDegree.Visible = false;
                            }
                            else
                            {

                                MVall.Views.Remove(ViewDegreeThesis);
                                Menu1.Visible = false;
                                Menu2.Visible = true;
                                Menu3.Visible = false;
                            }
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
                            if (ViewState["ApplyAttributeNo"] != null && ViewState["ApplyAttributeNo"].ToString().Equals(chkPublication))
                            {
                                MVall.SetActiveView(ViewDegreeThesis);
                                Menu1.Visible = false;
                                Menu2.Visible = false;
                                Menu3.Visible = true;
                                AboutDegree.Visible = false;
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
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
            }
        }

        protected void Page_Init(object sender, EventArgs e)

        {

            if (Session["LoginEmail"] != null)
            {

                String Identity = Request.QueryString["Identity"];
                GetSettings setting = new GetSettings();
                if (Request.QueryString["Identity"] != "Manager" && !String.IsNullOrEmpty(setting.LoginMail))
                {
                    EmpEmail.Text = setting.LoginMail;
                }
                else
                {
                    VYear.Visible = true;
                    VSemester.Visible = true;
                    BtnSaveYear.Visible = true;
                    VYearAndSemester.Text = "&nbsp;/&nbsp;";
                }
                isThesisNotUpload = false; //是否有未上傳論文積分附件
                if (Request["ApplyerID"] != null)
                {
                    DESCrypt DES = new DESCrypt();
                    if (Request["ApplyerID"] != null)
                    {
                        try
                        {

                            if (Request["ManageEmpId"] != null)
                            {
                                EmpBaseModifySave.Visible = true;
                                Session["ManageEmpId"] = Request["ManageEmpId"].ToString();
                            }
                            else
                            {
                                //返回鍵顯示 返回時清空Session["ManageEmpId"] 
                                if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                                {
                                    EmpBaseModifySave.Visible = true;
                                }
                            }

                            //取得基本資料EmpSn 抓出 ApplyAudit
                            EmpIdno.Text = DES.Decrypt(Request["ApplyerID"].ToString());
                            ViewState["ApplyerID"] = EmpIdno.Text;
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


                    Session["EmpIdno"] = EmpIdno.Text.ToString();
                    //if (EmpId.Text.ToString() != null) LoadDataBtn_Click(sender, e);                
                    //取得基本資料EmpSn 抓出 ApplyAudit
                    getSettings = new GetSettings();
                    getSettings.Execute();
                    //VYear.Text = getSettings.NowYear;
                    //VSemester.Text = getSettings.NowSemester;
                    if (Request["HRAppSn"] != null && !Request["HRAppSn"].ToString().Equals(""))
                        vApplyAudit = crudObject.GetApplyAuditObjByHRAppSnPromote(EmpIdno.Text.ToString(), Request["HRAppSn"].ToString());
                    else if (Request["AppSn"] != null && !Request["AppSn"].ToString().Equals(""))
                        vApplyAudit = crudObject.GetApplyAuditObjByAppSnPromote(EmpIdno.Text.ToString(), DES.Decrypt(Request["AppSn"].ToString()));
                    else
                        vApplyAudit = crudObject.GetApplyAuditObjByIdnoPromote(EmpIdno.Text.ToString());
                    //這是為了將職稱代碼順利的對應到法規條文
                    //AddELawNumControls(chkPromote, "05", ""); 
                    AddELawNumControls(chkPromote, "030000", "");

                    if (Request["Identity"] != null)
                    {
                        if (vApplyAudit != null && vApplyAudit.AppYear != null && vApplyAudit.AppSemester != null)
                        {
                            VYear.Text = vApplyAudit.AppYear;
                            VSemester.Text = vApplyAudit.AppSemester;
                        }
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

                        TableRow tRow = new TableRow();     //create a new object of type TableRow
                        table.Rows.Add(tRow);
                        ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
                        String[] strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());
                        String[] strAuditText = { "未審", "審過", "審不過", "退回", "完成" };

                        DataTable dtTable = crudObject.GetAllAuditStageByEmpSn(vApplyAudit.AppSn);
                        TableCell Cell = new TableCell();
                        string strCell = "";
                        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        strCell = "申請完成";
                        Cell.BackColor = System.Drawing.Color.YellowGreen;
                        Cell.Text = strCell;
                        tRow.Cells.Add(Cell);
                        Cell = new TableCell();

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
                        PanelStatus.Controls.Add(table);
                        AddELawNumControls(chkPromote, vApplyAudit.AppJobTitleNo, vApplyAudit.AppLawNumNo);
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
                    //MessageLabelThesis.Text = "";
                    MessageLabel.Text = "";
                    TeachEduStartYear.Items.Clear();
                    TeachEduEndYear.Items.Clear();
                    TeachExpStartYear.Items.Clear();
                    TeachExpEndYear.Items.Clear();
                    ddl_TeachLessonYear.Items.Clear();
                    ddl_AppThesisPublishYear.Items.Clear();
                    int nowY = DateTime.Now.Year - 1911;

                    for (int i = nowY; i >= 30; i--)
                    {
                        if (i.ToString().Length < 3)
                        {
                            TeachEduStartYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                            TeachEduEndYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                            TeachExpStartYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                            ddl_TeachLessonYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                            ddl_AppThesisPublishYear.Items.Add(new ListItem(i.ToString(), "0" + i.ToString()));
                        }
                        else
                        {
                            TeachEduStartYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                            TeachEduEndYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                            TeachExpStartYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                            ddl_TeachLessonYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                            ddl_AppThesisPublishYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                        }
                    }
                    for (int i = 160; i >= 30; i--)
                        TeachExpEndYear.Items.Add(new ListItem(i.ToString(), i.ToString("000")));

                    TeachEduStartYear.DataBind();
                    TeachEduEndYear.DataBind();
                    TeachExpStartYear.DataBind();
                    TeachExpEndYear.DataBind();
                    ddl_TeachLessonYear.DataBind();
                    ddl_AppThesisPublishYear.DataBind();
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
                }

                if (Session["index"] == null || Session["index"].ToString().Equals(""))
                {
                    MVall.ActiveViewIndex = (int)SearchType.ViewTeachBase;
                    Session["index"] = "0";
                }
                else
                {
                    MVall.ActiveViewIndex = Convert.ToInt32(Session["index"].ToString());
                }
                MessageLabel.Text = " ";
                //MessageLabelEdu.Text = " ";
                //TeachLessonHours.Attributes.Add("OnKeyPress", "txtKeyNumber();");
                //TeachLessonCreditHours.Attributes.Add("OnKeyPress", "txtKeyNumber();");

            }

        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Request["ApplyKindNo"] != null && Request["ApplyWayNo"] != null &&
                Request["ApplyAttributeNo"] != null &&
                (vApplyAudit == null))
            {
                ViewState["ApplyKindNo"] = Request["ApplyKindNo"].ToString();  //類別: 1新聘 2升等
                ViewState["ApplyWayNo"] = Request["ApplyWayNo"].ToString(); //途徑
                ViewState["ApplyAttributeNo"] = Request["ApplyAttributeNo"].ToString(); //類型 
                TbKindNo.Text = ViewState["ApplyKindNo"].ToString();
                //新聘類型:1已具部定教師資格 2未具部定教師資格_學位送審 3未具部定教師資格_著作送審 4臨床教師新聘	
                //升等類型:1著作送審升等 2學位送審升等 3臨床教師升等


                #region (!IsPostBack)
                if (!IsPostBack)
                {
                    String[] num = { "零", "一", "二", "三", "四", "五" };
                    AppJobTitleNo.DataBind();
                    AppJobTitleNo.Items.Insert(0, "請選擇");
                    ItemLabel.Text = num[0];


                    //升等類型:1著作送審升等 2學位送審升等 3臨床教師升等
                    AppAttributeNo.DataValueField = "status";
                    AppAttributeNo.DataTextField = "note";
                    AppAttributeNo.DataSource = crudObject.GetApplyPrmAttribute().DefaultView;
                    AppAttributeNo.DataBind();
                    //新聘類型 指定申請類別 著作 學位
                    for (int i = 0; i < AppAttributeNo.Items.Count; i++)
                    {
                        if (ViewState["ApplyAttributeNo"].ToString().Equals(AppAttributeNo.Items[i].Value))
                        {
                            AppAttributeNo.Items[i].Selected = true;
                            AppAttributeName.Text = AppAttributeNo.Items[i].ToString();
                        }
                        else
                        {
                            AppAttributeNo.Items[i].Selected = false;
                        }
                    }
                    AppWayName.Text = crudObject.GetAuditWayName(Request["ApplyWayNo"].ToString()).Rows[0][0].ToString();

                    //
                    //TeachExpSave.Enabled = false;



                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowProgressBar", "ShowProgressBar()", true);

                    //第一次進入抓人事資料 並存入EmployeeBase
                    vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(EmpIdno.Text);

                    #region MyRegion if (vEmpTmuHr != null)

                    if (vEmpTmuHr != null)
                    {


                        EmpId.Text = vEmpTmuHr.EmpId;
                        Session["EmpId"] = EmpId.Text;
                        EmpIdno.Text = vEmpTmuHr.EmpIdno;
                        EmpBirthDay.Text = vEmpTmuHr.EmpBdate;
                        EmpPassportNo.Text = vEmpTmuHr.EmpPassid;
                        EmpNameENFirst.Text = vEmpTmuHr.EmpEfname;
                        EmpNameENLast.Text = vEmpTmuHr.EmpElname;


                        EmpNameCN.Text = vEmpTmuHr.EmpName;
                        Session["Name"] = EmpNameCN.Text;
                        EmpSex.Text = vEmpTmuHr.EmpSex.Trim().Equals("M") ? "男" : "女";
                        EmpCountry.Text = crudObject.GetCountryName(vEmpTmuHr.EmpNation.Trim()).Rows.Count == 0 ? "" : crudObject.GetCountryName(vEmpTmuHr.EmpNation.Trim()).Rows[0][0].ToString();
                        //EmpUnit.Text = vEmpTmuHr.EmpUntFullName;


                        DDEmpUnit.Items.Clear();
                        DDEmpUnit.Items.Add(new ListItem(crudObject.GetDeptUntName(vEmpTmuHr.EmpUntid), vEmpTmuHr.EmpUntid));
                        DDEmpUnit.Items.Add(new ListItem(crudObject.GetDeptUntName(vEmpTmuHr.EmpWUntid), vEmpTmuHr.EmpWUntid));
                        Session["EmpUntid"] = vEmpTmuHr.EmpUntid.ToString();
                        Session["EmpWUntid"] = vEmpTmuHr.EmpWUntid.ToString();
                        Session["EmpTitleId"] = vEmpTmuHr.EmpTitid.ToString();
                        //其他兼職單位
                        DataTable dt = crudObject.GetOtherJob(vEmpTmuHr.EmpIdno);
                        List<string> lst = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("oth_untid")).Distinct().ToList();
                        foreach (var untid in lst)
                        {
                            if (!untid.Equals(vEmpTmuHr.EmpUntid) && !untid.Equals(vEmpTmuHr.EmpWUntid))
                                DDEmpUnit.Items.Add(new ListItem(crudObject.GetDeptUntName(untid), untid));
                        }
                        DDEmpUnit.Items[0].Selected = true;
                        DDEmpUnit.DataBind();
                        //EmpNowEJobTitle.Text = vEmpTmuHr.EmpTitidName;

                        DDEmpNowEJobTitle.Items.Clear();
                        DDEmpNowEJobTitle.Items.Add(new ListItem(crudObject.GetTitleName(vEmpTmuHr.EmpTitid), vEmpTmuHr.EmpTitid));
                        //其他兼職職務
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (!dt.Rows[j]["oth_titid"].ToString().Equals(vEmpTmuHr.EmpUntid) &&
                                !dt.Rows[j]["oth_titid"].ToString().Equals(vEmpTmuHr.EmpUntid))
                            {
                                DDEmpNowEJobTitle.Items.Add(new ListItem(crudObject.GetTitleName(dt.Rows[j]["oth_titid"].ToString()), dt.Rows[j]["oth_titid"].ToString()));
                            }
                        }
                        DDEmpNowEJobTitle.Items[0].Selected = true;

                        String ExpStartDate = crudObject.GetTeacherTmuExp(vEmpTmuHr.EmpIdno, vEmpTmuHr.EmpTitid);
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

                        EmpYear.Text = "" + String.Format("{0:00}", diffYear) + "/" + String.Format("{0:00}", diffMonth) + " (" + intYear + String.Format("{0:00}", DateTime.Now.Month) + ")";


                        EmpTeachNo.Text = vEmpTmuHr.EmpTeachno;

                        //將舊的人事資料帶出寫入 應徵教職員工資料檔
                        vEmp = new VEmployeeBase();
                        vEmp.EmpSn = crudObject.GetEmpBaseEmpSn(vEmpTmuHr.EmpIdno);
                        vEmp.EmpIdno = vEmpTmuHr.EmpIdno;
                        vEmp.EmpBirthDay = vEmpTmuHr.EmpBdate;
                        vEmp.EmpPassportNo = vEmpTmuHr.EmpPassid;
                        vEmp.EmpNameENFirst = vEmpTmuHr.EmpEfname;
                        vEmp.EmpNameENLast = vEmpTmuHr.EmpElname;
                        vEmp.EmpNameCN = vEmpTmuHr.EmpName;
                        vEmp.EmpSex = vEmpTmuHr.EmpSex;
                        vEmp.EmpCountry = vEmpTmuHr.EmpNation;
                        vEmp.EmpBornCity = vEmpTmuHr.EmpPstate;
                        vEmp.EmpTelPub = vEmpTmuHr.EmpMtel1;
                        vEmp.EmpTelPri = vEmpTmuHr.EmpPtel;
                        vEmp.EmpTownAddressCode = vEmpTmuHr.EmpMzcode1;
                        vEmp.EmpTownAddress = vEmpTmuHr.EmpMadr1;
                        vEmp.EmpAddressCode = vEmpTmuHr.EmpPzcode;
                        vEmp.EmpAddress = vEmpTmuHr.EmpPadrs;
                        vEmp.EmpEmail = vEmpTmuHr.EmpEmail;
                        String Identity = Request.QueryString["Identity"];
                        if (Request.QueryString["Identity"] != "Manager")
                        {
                            GetSettings setting = new GetSettings();
                            vEmp.EmpEmail = setting.LoginMail;
                        }
                        vEmp.EmpCell = vEmpTmuHr.EmpDgd;
                        vEmp.EmpNoTeachCa = false;
                        vEmp.EmpNoTeachExp = false;
                        vEmp.EmpNoHonour = false;
                        vEmp.UpdateUserId = vEmp.EmpIdno;
                        Session["EmpSn"] = vEmp.EmpSn;
                        TbEmpSn.Text = Session["EmpSn"].ToString();
                        if (vEmp.EmpSn == 0)
                        {
                            crudObject.Insert(vEmp);
                        }
                        else
                        {
                            crudObject.Update(vEmp);
                        }
                        vEmp.EmpSn = crudObject.GetEmpBaseEmpSn(vEmpTmuHr.EmpIdno);
                        Session["EmpSn"] = vEmp.EmpSn;

                        if (crudObject.GetDuringOpenDate(Request["ApplyKindNo"].Trim()))
                        {
                            //第一次進入時就填寫申請資料
                            InsertAppendData();
                        }
                        //第一次進入 儲存學歷資料
                        DataTable dtOldTable = crudObject.GetAllVTeacherEduByEmpSn(vEmp.EmpSn);
                        if (dtOldTable.Rows.Count == 0) //若教師聘任升等作業系統無資料，將天方資料轉入
                        {
                            DataTable dtTable = crudObject.GetEducationByEmpIdno(vEmpTmuHr.EmpIdno);
                            VTeacherTmuEdu vTeacherTmuEdu = new VTeacherTmuEdu();
                            VTeacherEdu vTeacherEdu;
                            for (int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                vTeacherEdu = new VTeacherEdu();
                                vTeacherEdu.EmpSn = vEmp.EmpSn;
                                vTeacherEdu.EduLocal = dtTable.Rows[i][vTeacherTmuEdu.stredu_nation].ToString();
                                vTeacherEdu.EduSchool = dtTable.Rows[i][vTeacherTmuEdu.stredu_xmcname].ToString();
                                vTeacherEdu.EduDepartment = dtTable.Rows[i][vTeacherTmuEdu.stredu_xddname].ToString();
                                vTeacherEdu.EduDegree = dtTable.Rows[i][vTeacherTmuEdu.stredu_xdlid].ToString();
                                vTeacherEdu.EduDegreeType = "1";
                                vTeacherEdu.EduStartYM = dtTable.Rows[i][vTeacherTmuEdu.stredu_bdate].ToString();
                                vTeacherEdu.EduEndYM = dtTable.Rows[i][vTeacherTmuEdu.stredu_edate].ToString();
                                crudObject.Insert(vTeacherEdu);
                            }
                        }
                        else
                        {
                            GVTeacherTmuExp.DataBind();
                            if (GVTeacherTmuExp.Rows.Count == 0)
                                lb_NoTeacherTmuExp.Visible = true;
                            else
                                lb_NoTeacherTmuExp.Visible = false;
                        }
                        GVTeachExp.DataBind();
                        if (GVTeachExp.Rows.Count == 0)
                            lb_NoTeachExp.Visible = true;
                        else
                            lb_NoTeachExp.Visible = false;

                        dtOldTable = crudObject.GetAllVTeacherTmuExpByEmpSn(vEmp.EmpSn, vEmpTmuHr.EmpTitid);
                        if (dtOldTable.Rows.Count == 0) //若教師聘任升等作業系統無資料，將天方資料轉入
                        {
                            //第一次進入 儲存經歷資料
                            DataTable dtTable = crudObject.GetExprienceByEmpIdno(vEmpTmuHr.EmpIdno, vEmpTmuHr.EmpTitid);
                            VTeacherTmuExp vTeacherTmuExp;
                            if (dtTable != null)
                                if (!String.IsNullOrEmpty(dtTable.Rows.Count.ToString()) && dtTable.Rows.Count > 0)
                                {
                                    //crudObject.DeleteTeacherTmuExp(vEmp.EmpSn);
                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        vTeacherTmuExp = new VTeacherTmuExp();
                                        vTeacherTmuExp.EmpSn = vEmp.EmpSn;
                                        vTeacherTmuExp.ExpUnitId = dtTable.Rows[i][vTeacherTmuExp.strExpUnitId].ToString();
                                        vTeacherTmuExp.ExpTitleId = dtTable.Rows[i][vTeacherTmuExp.strExpTitleId].ToString();
                                        vTeacherTmuExp.ExpPosId = (dtTable.Rows[i][vTeacherTmuExp.strExpPosId].ToString().Equals("")) ? vEmpTmuHr.EmpTeachno : dtTable.Rows[i][vTeacherTmuExp.strExpPosId].ToString(); //若天方資料庫沒有就抓 人事基本資料
                                        vTeacherTmuExp.ExpQId = dtTable.Rows[i][vTeacherTmuExp.strExpQId].ToString();
                                        vTeacherTmuExp.ExpStartDate = dtTable.Rows[i][vTeacherTmuExp.strExpStartDate].ToString();
                                        vTeacherTmuExp.ExpEndDate = dtTable.Rows[i][vTeacherTmuExp.strExpEndDate].ToString();
                                        crudObject.Insert(vTeacherTmuExp);
                                    }
                                }

                        }

                        else
                        {
                            GVTeachEdu.DataBind();
                            if (GVTeachEdu.Rows.Count == 0)
                                lb_NoTeacherEdu.Visible = true;
                            else
                                lb_NoTeacherEdu.Visible = false;
                        }

                        dtOldTable = crudObject.GetAllVTeacherTmuLessonByEmpSn(vEmp.EmpSn);
                        if (dtOldTable.Rows.Count == 0) //若教師聘任升等作業系統無資料，將天方資料轉入
                        {
                            //第一次進入 儲存授課進度資料
                            DataTable dtTable = crudObject.GetLessonByEmpId(vEmpTmuHr.EmpId);
                            VTeacherTmuLesson vTeacherTmuLesson;
                            if (dtTable != null)
                            {
                                getSettings = new GetSettings();
                                getSettings.Execute();
                                crudObject.DeleteTeacherTmuLesson(vEmp.EmpSn);
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    vTeacherTmuLesson = new VTeacherTmuLesson();
                                    vTeacherTmuLesson.EmpSn = vEmp.EmpSn;
                                    //vTeacherTmuLesson.LessonYear = dtTable.Rows[i][vTeacherTmuLesson.strLessonYear].ToString(); //getSettings.GetYear();
                                    //vTeacherTmuLesson.LessonSemester = dtTable.Rows[i][vTeacherTmuLesson.strLessonSemester].ToString(); //getSettings.GetSemester();
                                    vTeacherTmuLesson.LessonYear = dtTable.Rows[i]["SMTR"].ToString().Substring(0, 3);
                                    vTeacherTmuLesson.LessonSemester = dtTable.Rows[i]["SMTR"].ToString().Substring(3, 1);
                                    vTeacherTmuLesson.LessonDeptLevel = dtTable.Rows[i][vTeacherTmuLesson.strLessonDeptLevel].ToString();
                                    vTeacherTmuLesson.LessonClass = dtTable.Rows[i][vTeacherTmuLesson.strLessonClass].ToString();
                                    vTeacherTmuLesson.LessonCreditHours = dtTable.Rows[i][vTeacherTmuLesson.strLessonCreditHours].ToString();
                                    vTeacherTmuLesson.LessonHours = dtTable.Rows[i][vTeacherTmuLesson.strLessonHours].ToString();
                                    crudObject.Insert(vTeacherTmuLesson);
                                }

                            }
                        }
                        else
                        {
                            GVTeachLesson.DataBind();
                            if (GVTeachLesson.Rows.Count == 0)
                                lb_NoTeachLesson.Visible = true;
                            else
                                lb_NoTeachLesson.Visible = false;
                        }


                        //第一次進入 儲存RPI論文積分表

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "HideProgressBar", "HideProgressBar()", true);
                    }
                    #endregion
                }
                #endregion
            }

            else
            {
                if (!IsPostBack)
                {
                    if (vApplyAudit != null)
                    {
                        Session["AppSn"] = vApplyAudit.AppSn;
                        ViewState["ApplyKindNo"] = vApplyAudit.AppKindNo;
                        ViewState["ApplyWayNo"] = vApplyAudit.AppWayNo;
                        if (Request["ApplyAttributeNo"] != null && !Request["ApplyAttributeNo"].ToString().Equals(""))
                        {
                            ViewState["ApplyAttributeNo"] = Request["ApplyAttributeNo"].ToString();
                        }
                        else
                        {
                            ViewState["ApplyAttributeNo"] = vApplyAudit.AppAttributeNo;
                        }
                        countThesisScorePaper = crudObject.GetVThesisScoreTotalCount(vApplyAudit.EmpSn, vApplyAudit.AppSn).ToString();
                        LoadDataBtn_Click(sender, e);
                        TbKindNo.Text = ViewState["ApplyKindNo"].ToString();
                        Session["ResearchYear"] = vApplyAudit.AppResearchYear;
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
                    }
                }
                else
                {
                    if (Session["EmpSn"] != null)
                    {
                        countThesisScorePaper = crudObject.GetVThesisScoreTotalCount(vApplyAudit.EmpSn, vApplyAudit.AppSn).ToString();

                        TbEmpSn.Text = Session["EmpSn"].ToString();
                        GVTeachEdu.DataBind();

                        if (GVTeachEdu.Rows.Count == 0)
                            lb_NoTeacherEdu.Visible = true;
                        else
                            lb_NoTeacherEdu.Visible = false;
                        GVTeachExp.DataBind();
                        if (GVTeachExp.Rows.Count == 0)
                            lb_NoTeachExp.Visible = true;
                        else
                            lb_NoTeachExp.Visible = false;
                        GVTeacherTmuExp.DataBind();
                        if (GVTeacherTmuExp.Rows.Count == 0)
                            lb_NoTeacherTmuExp.Visible = true;
                        else
                            lb_NoTeacherTmuExp.Visible = false;

                        GVTeachLesson.DataBind();
                        if (GVTeachLesson.Rows.Count == 0)
                            lb_NoTeachLesson.Visible = true;
                        else
                            lb_NoTeachLesson.Visible = false;
                        GVThesisScore.DataBind();
                        GVThesisScoreCoAuthor.DataBind();
                        PaperCount.Text = GVThesisScore.Rows.Count.ToString();
                        PaperOver.Text = "" + (Convert.ToInt32(PaperCount.Text.ToString()) - 15);

                        //載入學位論文
                        if (vApplyAudit.AppKindNo.Equals(chkPromote) && AppAttributeNo.SelectedValue.Equals(chkDegree))
                        {
                            GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
                            GVThesisOral.DataBind();
                            if (GVThesisOral.Rows.Count == 0)
                                lb_NoThesisOral.Visible = true;
                            else
                                lb_NoThesisOral.Visible = false;
                        }
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
                    }
                }


            }
            if (ViewState["ApplyAttributeNo"] != null)
            {
                OtherServiceTableRow.Visible = true;
                if (ViewState["ApplyAttributeNo"].ToString().Equals(chkPublication))
                {
                    AppPPMTableRow.Visible = true;
                    AppTeacherCaTableRow.Visible = true;
                    OtherTeachingTableRow.Visible = true;
                    ThesisOralType.Items.Clear();
                    ThesisOralType.Items.Add(new ListItem("迴避名單", "3"));
                    ThesisOralType.DataBind();
                    AvoidReason.Visible = true;
                }
                else if (ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree))
                {
                    OtherTeachingTableRow.Visible = true;
                    ThesisOralType.Items.Clear();
                    ThesisOralType.Items.Add(new ListItem("論文指導教師", "1"));
                    ThesisOralType.Items.Add(new ListItem("口試委員", "2"));
                    ThesisOralType.DataBind();
                }
                else if (ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical))
                {
                    AppClinicalRow.Visible = true;
                    RecommendUploadTableRow.Visible = true;
                }
            }

        }

        protected void DDEmpUnit_OnClick(object sender, EventArgs e)
        {
            if (Session["EmpUntid"] == null || !Session["EmpWUntid"].Equals(""))
            {
                MessageLabel.Text = "逾時，請重新登入!";
                return;
            }

            String ExpStartDate = "";
            if (DDEmpUnit.SelectedValue.ToString().Equals(Session["EmpUntid"].ToString()))
            {
                DDEmpNowEJobTitle.Items.Clear();
                DDEmpNowEJobTitle.Items.Add(new ListItem(crudObject.GetTitleName(Session["EmpTitleId"].ToString()), Session["EmpTitleId"].ToString()));
                DDEmpNowEJobTitle.Items[0].Selected = true;
            }
            else
            {
                DDEmpNowEJobTitle.Items.Clear();
                //其他兼職職務
                DataTable dt = crudObject.GetOtherJob(Session["EmpIdno"].ToString());
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (!dt.Rows[j]["oth_titid"].ToString().Equals(Session["EmpTitleId"].ToString()) &&
                        !dt.Rows[j]["oth_titid"].ToString().Equals(Session["EmpTitleId"].ToString()))
                    {
                        DDEmpNowEJobTitle.Items.Add(new ListItem(crudObject.GetTitleName(dt.Rows[j]["oth_titid"].ToString()), dt.Rows[j]["oth_titid"].ToString()));
                    }
                }
                DDEmpNowEJobTitle.Items[0].Selected = true;
            }
            if (DDEmpUnit.SelectedValue.ToString().Equals(Session["EmpUntid"].ToString()))
            {
                ExpStartDate = crudObject.GetTeacherTmuExp(Session["EmpIdno"].ToString(), DDEmpNowEJobTitle.SelectedValue.ToString());

            }
            else
            {
                ExpStartDate = crudObject.GetOtherExp(Session["EmpIdno"].ToString(), DDEmpNowEJobTitle.SelectedValue.ToString());
            }

            DateTime nowdate = DateTime.Now;
            int intYear = Int32.Parse(ExpStartDate.Substring(0, 3));
            int intMonth = Int32.Parse(ExpStartDate.Substring(3, 2));
            int nowYear = nowdate.Year - 1911;
            int nowMonth = nowdate.Month - 1;

            if (ExpStartDate == null || ExpStartDate.Equals(""))
            {
                ExpStartDate = "04906"; //塞給北醫創校年
                EmpYear.Text = "現職資料未抓到";
            }
            else
            {
                if (nowMonth < intMonth)
                {
                    nowYear--;
                    nowMonth = nowMonth + 12;
                }
                diffYear = nowYear - intYear;
                diffMonth = nowMonth - intMonth;

                EmpYear.Text = "" + String.Format("{0:00}", diffYear) + "/" + String.Format("{0:00}", diffMonth) + " (" + intYear + String.Format("{0:00}", DateTime.Now.Month) + ")";

                //升等職別增一級 講師	60000 助理教授	50000 副教授	40000 教授	30000 

                int level = Int32.Parse(DDEmpNowEJobTitle.SelectedValue.ToString().Substring(1, 1)) - 1;

                String newJobTypeNo = "" + level + DDEmpNowEJobTitle.SelectedValue.ToString().Substring(1, 4);
                for (int i = 0; i < AppJobTypeNo.Items.Count; i++)
                {
                    if (newJobTypeNo.Equals(AppJobTypeNo.Items[i].Value))
                    {
                        AppJobTypeNo.Items[i].Selected = true;
                    }
                    else
                    {
                        AppJobTypeNo.Items[i].Selected = false;
                    }
                }
            }

        }

        protected void DDEmpTitle_OnClick(object sender, EventArgs e)
        {
            if (Session["EmpUntid"] == null || !Session["EmpWUntid"].Equals(""))
            {
                MessageLabel.Text = "逾時，請重新登入!";
            }
            String ExpStartDate = "";
            if (DDEmpUnit.SelectedValue.ToString().Equals(Session["EmpUntid"].ToString()))
            {
                ExpStartDate = crudObject.GetTeacherTmuExp(Session["EmpIdno"].ToString(), DDEmpNowEJobTitle.SelectedValue.ToString());

            }
            else
            {
                ExpStartDate = crudObject.GetOtherExp(Session["EmpIdno"].ToString(), DDEmpNowEJobTitle.SelectedValue.ToString());
            }

            DateTime nowdate = DateTime.Now;
            int intYear = 0;
            if (ExpStartDate != null && ExpStartDate != "")
                intYear = Int32.Parse(ExpStartDate.Substring(0, 3));
            int intMonth = 0;
            if (ExpStartDate != null && ExpStartDate != "")
                intMonth = Int32.Parse(ExpStartDate.Substring(3, 2));
            int nowYear = nowdate.Year - 1911;
            int nowMonth = nowdate.Month - 1;

            if (ExpStartDate == null || ExpStartDate.Equals(""))
            {
                ExpStartDate = "04906"; //塞給北醫創校年
                EmpYear.Text = "現職資料未抓到";
            }
            else
            {
                if (nowMonth < intMonth)
                {
                    nowYear--;
                    nowMonth = nowMonth + 12;
                }
                diffYear = nowYear - intYear;
                diffMonth = nowMonth - intMonth;

                EmpYear.Text = "" + String.Format("{0:00}", diffYear) + "/" + String.Format("{0:00}", diffMonth) + " (" + intYear + String.Format("{0:00}", DateTime.Now.Month) + ")";

                //升等職別增一級 講師	60000 助理教授	50000 副教授	40000 教授	30000 

                int level = Int32.Parse(DDEmpNowEJobTitle.SelectedValue.ToString().Substring(1, 1)) - 1;

                String newJobTypeNo = "" + level + DDEmpNowEJobTitle.SelectedValue.ToString().Substring(1, 4);
                for (int i = 0; i < AppJobTypeNo.Items.Count; i++)
                {
                    if (newJobTypeNo.Equals(AppJobTypeNo.Items[i].Value))
                    {
                        AppJobTypeNo.Items[i].Selected = true;
                    }
                    else
                    {
                        AppJobTypeNo.Items[i].Selected = false;
                    }
                }
            }

        }

        protected void AppAttributeNo_Click(object sender, EventArgs e)
        {
            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
            DESCrypt DES = new DESCrypt();
            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&ApplyKindNo=" + ViewState["ApplyKindNo"].ToString() + "&ApplyWayNo=" + ViewState["ApplyWayNo"].ToString() + "&ApplyAttributeNo=" + ViewState["ApplyAttributeNo"].ToString();
            //parameters = Uri.EscapeDataString(parameters);
            Response.Redirect("~/PromoteEmp.aspx?" + parameters);
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
            try
            {
                if (AppDeclarationUploadFU.HasFile)
                    AppDeclarationUploadCB.Checked = true;
                if (ExpServiceCaUploadFU.HasFile)
                    ExpServiceCaUploadCB.Checked = true;
                if (AppPPMUploadFU.HasFile)
                    AppPPMUploadCB.Checked = true;
                if (AppTeacherCaUploadFU.HasFile)
                    AppTeacherCaUploadCB.Checked = true;
                if (AppOtherServiceUploadFU.HasFile)
                    AppOtherServiceUploadCB.Checked = true;
                TransferDataToEmpObject();
                SaveEmpObjectToDB(sender, e, false);
                InsertAppendData();
                LoadDataBtn_Click(sender, e);
                ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                DESCrypt DES = new DESCrypt();
                string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&ApplyKindNo=2&ApplyWayNo=" + ViewState["ApplyWayNo"].ToString() + "&ApplyAttributeNo=" + ViewState["ApplyAttributeNo"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:var retValue=alert('您的資料已暫存成功!');", true);
                Response.Write("<script>alert('您的資料已暫存成功!');location.href='" + Request.Url.ToString() + "'; </script>");
                //Response.Redirect(Request.Url.ToString());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:var retValue=alert('暫存失敗!" + ex.ToString() + "');", true);
            }
        }


        public void DrawingChart(object sender, EventArgs e)
        {
            GVTeachEdu.DataBind();

            if (GVTeachEdu.Rows.Count == 0)
                lb_NoTeacherEdu.Visible = true;
            else
                lb_NoTeacherEdu.Visible = false;
            GVTeachExp.DataBind();
            if (GVTeachExp.Rows.Count == 0)
                lb_NoTeachExp.Visible = true;
            else
                lb_NoTeachExp.Visible = false;
            GVTeacherTmuExp.DataBind();
            if (GVTeacherTmuExp.Rows.Count == 0)
                lb_NoTeacherTmuExp.Visible = true;
            else
                lb_NoTeacherTmuExp.Visible = false;
            GVThesisScore.DataBind();
            PaperCount.Text = GVThesisScore.Rows.Count.ToString();
            GVThesisScoreCoAuthor.DataBind();
            //載入學位論文[論文指導教師,口試委員],著作[迴避名單]
            if (AppAttributeNo.SelectedValue.Equals(chkDegree) || vApplyAudit.AppAttributeNo.Equals(chkPublication))
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
            fillTeachLesson = (GVTeachLesson.Rows.Count > 0) ? "100" : "0";

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
            if (AppAttributeNo.SelectedValue.Equals(chkDegree))
            {
                fillThesisOral = GVThesisOral.Rows.Count;
            }
            //DrawingChart();
        }


        //畫面資料存入物件 1基本資料&2自我評議 
        private void TransferDataToEmpObject()
        {
            string strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath + Session["EmpSn"].ToString() + "\\";
            if (vEmp == null)
            {
                vEmp = new VEmployeeBase();
                vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text.ToString());
            }
            vEmp.EmpIdno = EmpIdno.Text.ToString();
            vEmp.EmpNameENFirst = EmpNameENFirst.Text.ToString();
            vEmp.EmpNameENLast = EmpNameENLast.Text.ToString();
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

            if (EmpIdnoUploadFU.HasFile)
            {
                if (EmpIdnoUploadFU.FileName != null && checkName(EmpIdnoUploadFU.FileName))
                {
                    EmpIdnoUploadCB.Checked = true;
                    fileLists.Add(EmpIdnoUploadFU);
                    //vEmp.EmpIdUpload = EmpIdnoUploadCB.Checked;
                    vEmp.EmpIdnoUploadName = EmpIdnoUploadFU.FileName;
                    EmpIdnoUploadFU.PostedFile.SaveAs(strFolderPath + EmpIdnoUploadFU.FileName);
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
                    EmpDegreeUploadCB.Checked = true;
                    fileLists.Add(EmpDegreeUploadFU);
                    //vEmp.EmpDegreeUpload = EmpDegreeUploadCB.Checked;
                    vEmp.EmpDegreeUploadName = EmpDegreeUploadFU.FileName;
                    EmpDegreeUploadFU.PostedFile.SaveAs(strFolderPath + EmpDegreeUploadFU.FileName);
                }
            }
            else
            {
                vEmp.EmpDegreeUploadName = EmpDegreeUploadFUName.Text.ToString();
                //vEmp.EmpDegreeUpload = EmpDegreeUploadCB.Checked;
            }
            //vEmp.EmpBuildDate = DateTime.Now;
            if (crudObject.Update(vEmp))
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('修改成功');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('修改失敗');", true);
        }

        //資料存入BackupDB 備份原本資料
        private void SaveEmpObjectToBackupDB(object sender, EventArgs e)
        {
            if (Session["AppSn"] == null || Session["AppSn"].ToString().Equals(""))
            {
                MessageLabel.Text = "逾時，請重新登入!";
            }
            else
            {
                VEmployeeBase oldEmp = crudObject.GetEmpBaseObjByEmpSn(Session["AppSn"].ToString());
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
        }

        //基本資料存入DB
        private void SaveEmpObjectToDB(object sender, EventArgs e, Boolean b)
        {
            if (Session["AppSn"] == null || Session["AppSn"].ToString().Equals(""))
            {
                MessageLabel.Text = "逾時，請重新登入!";
            }
            else
            {
                //VEmployeeBase oldEmp = crudObject.GetEmpBaseObjByEmpSn(Session["EmpSn"].ToString());   

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
                    MessageLabel.Text = "異動資料：成功!! ";
                    ProcessUploadFiles(vEmp.EmpSn);
                }
                else
                {
                    MessageLabel.Text = "儲存資料：失敗，請洽資訊人員!!";
                }
            }
        }

        //Email通知 更改密碼
        private void SendEmailForInfomation(string strEmail)
        {

        }

        //寫入 延伸的資料檔
        //20160728 EmpUnitNo --> DDEmpUnit 測試資料jeffshen@tmu.edu.tw 專任助理教授 臨床醫學副教授
        private void InsertAppendData()
        {
            //Response.Write("<script>alert('IN');</script>");
            string strFolderPath = "";
            GetSettings settings = new GetSettings();
            vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(vEmp.EmpIdno);
            //判斷 ApplyAudit是否已存在
            //寫入新聘,升等共用延伸資料檔 //2019/01/02 追加年度判定
            //if (vApplyAudit.AppYear == null || String.IsNullOrEmpty(vApplyAudit.AppYear.ToString()))
            if (vApplyAudit == null)
            {
                vApplyAudit = new VApplyAudit();
                vApplyAudit.AppYear = settings.NowYear;
                vApplyAudit.AppSemester = settings.NowSemester;
                //vApplyAudit.AppYear = settings.LoadYear;
                //vApplyAudit.AppSemester = settings.LoadSemester;
            }
            vApplyAudit.EmpSn = Convert.ToInt32(Session["EmpSn"].ToString());
            vApplyAudit.EmpIdno = Session["EmpIdno"].ToString();
            getSettings.Execute();
            if (VYear.Visible == true && VSemester.Visible == true)
            {
                vApplyAudit.AppYear = VYear.Text.Trim();
                vApplyAudit.AppSemester = VSemester.Text.Trim();
            }
            vApplyAudit.AppKindNo = ViewState["ApplyKindNo"].ToString(); //新聘
            vApplyAudit.AppWayNo = ViewState["ApplyWayNo"].ToString(); //途徑
            vApplyAudit.AppAttributeNo = ViewState["ApplyAttributeNo"].ToString(); //類型  AppAttributeNo.SelectedValue.ToString()
            vApplyAudit.AppUnitNo = EmpUnitNo.Text.ToString();
            vApplyAudit.AppJobTypeNo = AppJobTypeNo.SelectedValue;  //專任
            vApplyAudit.AppJobTitleNo = AppJobTitleNo.SelectedValue; //1講師,2助理教授,3副教授,4教授 剛好是法令條款 若有法令異動時UI須修正            
            vApplyAudit.AppLawNumNo = ELawNum.SelectedValue.ToString();
            if (vApplyAudit.EmpSn != 0)
            {
                strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath + vApplyAudit.EmpSn + "\\";

                ProcessUploadFiles(vApplyAudit.EmpSn);
            }
            //推薦函寫入上傳
            if (RecommendUploadFU.HasFile)
            {
                if (RecommendUploadFU.FileName != null && checkName(RecommendUploadFU.FileName))
                {
                    RecommendUploadCB.Checked = true;
                    fileLists.Add(RecommendUploadFU);
                    vApplyAudit.AppRecommendUploadName = RecommendUploadFU.FileName;
                    RecommendUploadFU.PostedFile.SaveAs(strFolderPath + RecommendUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.AppRecommendUploadName = RecommendUploadFUName.Text.ToString();
                //vEmp.EmpRecommendUpload = RecommendUploadCB.Checked;
            }

            //教師資格審查切結書 寫入上傳
            if (AppDeclarationUploadFU.HasFile)
            {
                if (AppDeclarationUploadFU.FileName != null && checkName(AppDeclarationUploadFU.FileName))
                {
                    //fileLists.Add(AppDeclarationUploadFU);
                    vApplyAudit.AppDeclarationUploadName = AppDeclarationUploadFU.FileName;
                    AppDeclarationUploadFU.PostedFile.SaveAs(strFolderPath + AppDeclarationUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.AppDeclarationUploadName = AppDeclarationUploadFUName.Text.ToString();
            }




            //服務成果 寫入上傳
            if (AppOtherServiceUploadFU.HasFile)
            {
                if (AppOtherServiceUploadFU.FileName != null && checkName(AppOtherServiceUploadFU.FileName))
                {
                    //fileLists.Add(AppOtherServiceUploadFU);
                    vApplyAudit.AppOtherServiceUploadName = AppOtherServiceUploadFU.FileName;
                    AppOtherServiceUploadFU.PostedFile.SaveAs(strFolderPath + AppOtherServiceUploadFU.FileName);
                    //Response.Write("<script>alert('路徑:" + strFolderPath + AppOtherServiceUploadFU.FileName + "');</script>");
                }
            }
            else
            {
                vApplyAudit.AppOtherServiceUploadName = AppOtherServiceUploadFUName.Text.ToString();
            }
            //教學成果 寫入上傳
            if (AppOtherTeachingUploadFU.HasFile)
            {
                if (AppOtherTeachingUploadFU.FileName != null && checkName(AppOtherTeachingUploadFU.FileName))
                {
                    AppOtherTeachingUploadCB.Checked = true;
                    //fileLists.Add(AppOtherTeachingUploadFU);
                    vApplyAudit.AppOtherTeachingUploadName = AppOtherTeachingUploadFU.FileName;
                    //vApplyAudit.AppOtherTeachingUpload = AppOtherTeachingUploadCB.Checked;
                    AppOtherTeachingUploadFU.PostedFile.SaveAs(strFolderPath + AppOtherTeachingUploadFU.FileName);
                    //Response.Write("<script>alert('路徑:" + strFolderPath + AppOtherTeachingUploadFU.FileName + "');</script>");
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
                    AppDrCaUploadCB.Checked = true;
                    fileLists.Add(AppDrCaUploadFU);
                    vApplyAudit.AppDrCaUploadName = AppDrCaUploadFU.FileName;
                    //vApplyAudit.AppDrCaUpload = AppDrCaUploadCB.Checked;
                    AppDrCaUploadFU.PostedFile.SaveAs(strFolderPath + AppDrCaUploadFU.FileName);
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
                    AppTeacherCaUploadCB.Checked = true;
                    fileLists.Add(AppTeacherCaUploadFU);
                    vApplyAudit.AppTeacherCaUploadName = AppTeacherCaUploadFU.FileName;
                    //vApplyAudit.AppTeacherCaUpload = AppTeacherCaUploadCB.Checked;
                    AppTeacherCaUploadFU.PostedFile.SaveAs(strFolderPath + AppTeacherCaUploadFU.FileName);
                    //Response.Write("<script>alert('路徑:" + strFolderPath + AppTeacherCaUploadFU.FileName + "');</script>");
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
                    AppPPMUploadCB.Checked = true;
                    //fileLists.Add(AppPPMUploadFU);
                    vApplyAudit.AppPPMUploadName = AppPPMUploadFU.FileName;
                    AppPPMUploadFU.PostedFile.SaveAs(strFolderPath + AppPPMUploadFU.FileName);
                    //Response.Write("<script>alert('路徑:" + strFolderPath + AppPPMUploadFU.FileName + "');</script>");
                }
                //vApplyAudit.AppPPMUpload = AppPPMUploadCB.Checked;
            }
            else
            {
                vApplyAudit.AppPPMUploadName = AppPPMUploadFUName.Text.ToString();
                //vApplyAudit.AppPPMUpload = AppPPMUploadCB.Checked;
            }


            //上傳
            if (AppPublicationUploadFU.HasFile)
            {
                if (AppPublicationUploadFU.FileName != null && checkName(AppPublicationUploadFU.FileName))
                {
                    AppPublicationUploadCB.Checked = true;
                    fileLists.Add(AppPublicationUploadFU);
                    vApplyAudit.AppPublicationUploadName = AppPublicationUploadFU.FileName;
                    //vApplyAudit.AppPublicationUpload = AppPublicationUploadCB.Checked;
                    AppPublicationUploadFU.PostedFile.SaveAs(strFolderPath + AppPublicationUploadFU.FileName);
                }
            }
            else
            {
                vApplyAudit.AppPublicationUploadName = AppPublicationUploadFUName.Text.ToString();
                //vApplyAudit.AppPublicationUpload = AppPublicationUploadCB.Checked;
            }

            //vApplyAudit.AppResearchYear = AppResearchYear.SelectedValue.ToString();
            //從代表著作頁籤寫入
            //if (!String.IsNullOrEmpty(AppPublication.SelectedItem.ToString()))
            //{
            //    vApplyAudit.AppPublicationUploadName = AppPublication.SelectedItem.ToString();
            //    vApplyAudit.AppPublicationUpload = true;
            //}

            //vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.AppRPIScore].ToString();
            //vApplyAudit.AppStage = "0"; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)     
            //vApplyAudit.AppStep = "0";
            vApplyAudit.AppResearchYear = AppResearchYear.SelectedValue;
            //vApplyAudit.AppBuildDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);

            //是否已寫入ApplyAudit
            DESCrypt DES = new DESCrypt();
            VApplyAudit oldVApplyAudit;
            //VApplyAudit oldVApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(Session["AppSn"].ToString()));
            //2019 / 01 / 02 追加年度判定
            if (Request.QueryString["ApplyMore"] != null && crudObject.CheckPromoteEmp(EmpIdno.Text.ToString(), (DateTime.Now.Year - 1911).ToString(), "2"))
            {
                if (Request["HRAppSn"] != null && !Request["HRAppSn"].ToString().Equals(""))
                    oldVApplyAudit = crudObject.GetApplyAuditObjByHRAppSnPromote(EmpIdno.Text.ToString(), Request["HRAppSn"].ToString());
                else if (Request["AppSn"] != null && !Request["AppSn"].ToString().Equals(""))
                    oldVApplyAudit = crudObject.GetApplyAuditObjByAppSnPromote(EmpIdno.Text.ToString(), DES.Decrypt(Request["AppSn"].ToString()));
                else
                    oldVApplyAudit = crudObject.GetApplyAuditObjByIdnoPromote(EmpIdno.Text.ToString());
            }
            //2019/01/02 追加年度判定
            else if (Request.QueryString["ApplyMore"] == null && crudObject.CheckPromoteEmp(EmpIdno.Text.ToString(), vApplyAudit.AppYear, vApplyAudit.AppSemester))
                oldVApplyAudit = crudObject.GetApplyAuditObjByIdnoPromote(EmpIdno.Text.ToString(), vApplyAudit.AppYear, vApplyAudit.AppSemester);
            else
            {
                if (vApplyAudit.AppSn == 0)
                    oldVApplyAudit = null;
                else
                    oldVApplyAudit = vApplyAudit;
            }
            if (oldVApplyAudit != null)
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
            {

                Session["times"] = 1;
                //第一次寫入
                vApplyAudit.AppStage = "0"; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)     
                vApplyAudit.AppStep = "0";
                if (crudObject.Insert(vApplyAudit, Session["EmpIdno"].ToString()))
                {
                    //寫入成功回傳序號
                    //vApplyAudit.AppSn = crudObject.GetApplyAuditByIdno(vApplyAudit.EmpIdno).AppSn;
                    if (Request.QueryString["HRAppSn"] != null && Request.QueryString["HRAppSn"] != "")
                    {
                        vApplyAudit.AppSn = Convert.ToInt32(Request.QueryString["HRAppSn"].ToString().Trim());
                        Session["AppSn"] = vApplyAudit.AppSn;
                    }
                    else
                    {
                        vApplyAudit.AppSn = crudObject.GetApplyAuditByIdno(EmpIdno.Text).AppSn;
                        Session["AppSn"] = vApplyAudit.AppSn;
                    }
                }
                else
                {
                    MessageLabel.Text += "1.共用延伸資料檔寫入失敗，請洽資訊人員!";
                }
            }


            //寫入升等專用檔

            VAppendPromote vAppendPromote = new VAppendPromote();
            vAppendPromote.AppSn = vApplyAudit.AppSn;
            vAppendPromote.NowJobYear = "" + String.Format("{0:00}", diffYear) + "" + String.Format("{0:00}", diffMonth); //取得現職年資
            vAppendPromote.NowJobTitle = vEmpTmuHr.EmpTitid;
            vAppendPromote.NowJobUnit = vEmpTmuHr.EmpUntid;
            vAppendPromote.NowJobPosId = vEmpTmuHr.EmpPosid;

            //Response.Write("<script>alert('HasFile?"+ ExpServiceCaUploadFU.HasFile + "');</script>");
            if (ExpServiceCaUploadFU.HasFile)
            {
                //Response.Write("<script>alert('HasFile');</script>");
                if (ExpServiceCaUploadFU.FileName != null && checkName(ExpServiceCaUploadFU.FileName))
                {
                    ExpServiceCaUploadCB.Checked = true;
                    //fileLists.Add(ExpServiceCaUploadFU);
                    vAppendPromote.ExpServiceCaUploadName = ExpServiceCaUploadFU.FileName;
                    //vApplyAudit.AppPublicationUpload = AppPublicationUploadCB.Checked;
                    ExpServiceCaUploadFU.PostedFile.SaveAs(strFolderPath + ExpServiceCaUploadFU.FileName);
                    //Response.Write("<script>alert('路徑:"+ strFolderPath + ExpServiceCaUploadFU.FileName + "');</script>");
                }
            }
            else
            {
                vAppendPromote.ExpServiceCaUploadName = ExpServiceCaUploadFUName.Text.ToString();
                //vApplyAudit.AppPublicationUpload = AppPublicationUploadCB.Checked;
            }
            

            //是否已寫入AppendEmployee
            int tAppPSn = crudObject.GetAppendPromoteByAppPSn(vApplyAudit.AppSn);
            if (tAppPSn > 0)
            {
                vAppendPromote.AppPSn = tAppPSn;
                if (!crudObject.Update(vAppendPromote))
                {
                    MessageLabel.Text += "2.更新升等專用檔寫入失敗，請洽資訊人員!";
                }
            }
            else
            {
                if (vAppendPromote.AppSn > 0 && crudObject.Insert(vAppendPromote))
                {
                    MessageLabel.Text += "2.升等專用檔寫入成功!";
                }
                else
                {
                    MessageLabel.Text += "2.升等專用檔寫入失敗，請洽資訊人員!";
                }
            }

        }


        /**
         * 申請送出
         *1.將EmployeeBase status update 1
         *2.判斷AppAttributeNo 產生審核檔案 
         **/
        protected void EmpBaseSave_Click(object sender, EventArgs e)
        {
            MessageLabel.Text += "送出前請確認頁面上資料無誤!!";
            DrawingChart(sender, e);
            if (Convert.ToInt32(fillThesis) > 100) fillThesis = "100";
            if (ViewState["ApplyAttributeNo"] != null && !ViewState["ApplyAttributeNo"].ToString().Equals(chkDegree)) fillThesis = "100"; //不是著作送審的都不判斷
            //臨床新聘
            if (ViewState["ApplyAttributeNo"] != null && ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical)) isThesisNotUpload = false;
            

            string checkSheet = "";
            checkSheet = checkSheets();
            if (checkSheet != "")
            {
                string strPopup = "以下頁籤未完成，請確認欄位填寫完整才能送出：\\n\\n";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strPopup + checkSheet + "');", true);

                return;
            }
            else
            {
                #region 確認無誤送出
                //再次確認密碼正確才能送出

                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "prompt", "passwordPrompt()", true);

                //EmpBaseTempSave_Click(sender,e);
                TransferDataToEmpObject();
                SaveEmpObjectToDB(sender, e, true);

                CRUDObject crudObject = new CRUDObject();
                VUnit vUnit;
                VUnit sUnit;
                VAuditExecute vAuditExecute;
                VAuditPeroid vAuditPeroid;
                //取得上下學期

                //取得密碼
                GeneratorPwd generatorPwd = new GeneratorPwd();
                string nowTerm;
                ArrayList arrayList;

                string strAuditPointDepartment; //是否E0100
                string strAuditPointAttribute;  //是否是1著作 2學位 3臨床醫 (著作boolAuditPointAttribute=True)      
                Boolean boolAuditPointAttribute = false;
                vUnit = crudObject.GetVUnit(vEmpTmuHr.EmpUntid); //若是三級單位 往上串一層級 判斷是否是醫學系或其它科系
                strAuditPointDepartment = vUnit.UntId2;

                if (!chkUnitNo.Equals(strAuditPointDepartment))//判斷是否是醫學系或其它科系
                {
                    strAuditPointDepartment = "OTHER";
                }
                strAuditPointAttribute = AppAttributeNo.SelectedValue.ToString(); //升等類型
                if (chkPublication.Equals(strAuditPointAttribute)) boolAuditPointAttribute = true;

                //撈取樣板產生該申請的簽核檔案 AuditExecute       E0100 or OTHER                申請類型是不是著作
                arrayList = crudObject.GetAllAuditPointRole(strAuditPointDepartment, boolAuditPointAttribute);

                nowTerm = getSettings.NowSemester; //1:上學期 2:下學期

                string tmpUnit;

                //被退件的教師確認是否已產生稽核檔案:因為申請者被退件重新送件時,不用再次產生VAuditExecute檔
                if (this.crudObject.GetExecuteAuditorAnyOne(vApplyAudit.AppSn))
                {
                    crudObject.reSend(vApplyAudit.AppSn.ToString());
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

                        //系統自動對應簽核人員(系承辦人) 透過部門 與 職階找到對應的主管、學科主任
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
                            //一級主管判斷(院長)
                            if ("1".Equals(vAuditPointRole.AuditPointRoleLevel.Trim()))
                            {
                                tmpUnit = vUnit.DptId;
                            }
                            sUnit = crudObject.GetVUnit(tmpUnit, vAuditPointRole.AuditPointRoleLevel); //撈取系承辦或科承辦主管(主任)
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

                        //帶入現任單位、現任職別
                        //vEmpTmuHr.EmpUntid = "I0100";
                        //自動帶入承辦人AppEUnitNo.SelectedValue vAuditPointRole.AuditPointSn
                        if (vAuditPointRole.AuditPointSn.Equals("1") || vAuditPointRole.AuditPointSn.Equals("3"))
                        {
                            DataTable dtRole;

                            if (vAuditPointRole.AuditPointSn.Equals("3") && vEmpTmuHr.EmpUntid.Substring(0, 1).Equals("E0") && vEmpTmuHr.EmpUntid != "E0400")
                            {
                                dtRole = crudObject.GetUnderTakerByDeptPointSn("E0000", vAuditPointRole.AuditPointSn);
                            }
                            else
                            {
                                dtRole = crudObject.GetUnderTakerByDeptPointSn(vEmpTmuHr.EmpUntid, vAuditPointRole.AuditPointSn);
                            }
                            if (dtRole != null && dtRole.Rows.Count > 0)
                            {
                                vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                            }
                        }
                        //院經理、院教評承辦
                        if (vAuditPointRole.AuditPointSn.Equals("4") || vAuditPointRole.AuditPointSn.Equals("5"))
                        {
                            DataTable dtRole;
                            //20201022 除通識教育中心外 D2300，其餘院級皆取第一值+0000(ex: E0000、F0000、10000)
                            string dpt_id = vEmpTmuHr.EmpUntid;
                            if (dpt_id.Substring(0, 3) == "D23")
                                dpt_id = "D2300";
                            else
                                dpt_id = dpt_id.Substring(0, 1) + "0000";
                            //dtRole = crudObject.GetUnderTakerByDeptPointSn(vEmpTmuHr.EmpUntid.Substring(0, 3) + "00", vAuditPointRole.AuditPointSn);
                            //dtRole = crudObject.GetUnderTakerByDeptPointSn(vEmpTmuHr.EmpUntid.Substring(0, 1) + "0000", vAuditPointRole.AuditPointSn);
                            dtRole = crudObject.GetUnderTakerByDeptPointSn(dpt_id, vAuditPointRole.AuditPointSn);
                            if (dtRole != null && dtRole.Rows.Count > 0)
                            {
                                vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                            }
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
                            if (dtRole != null && dtRole.Rows.Count > 0)
                            {
                                vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                            }
                        }

                        ////自動帶入承辦人AppEUnitNo.SelectedValue vAuditPointRole.AuditPointSn
                        //if (vAuditPointRole.AuditPointSn.Equals("1") || vAuditPointRole.AuditPointSn.Equals("3") || vAuditPointRole.AuditPointSn.Equals("5"))
                        //{
                        //    DataTable dtRole;
                        //    if ((vAuditPointRole.AuditPointSn.Equals("3") || vAuditPointRole.AuditPointSn.Equals("5")) & vEmpTmuHr.EmpUntid.Substring(0, 1).Equals("E"))
                        //    {
                        //        dtRole = crudObject.GetUnderTakerByDeptPointSn("E0000", vAuditPointRole.AuditPointSn);
                        //    }
                        //    else
                        //    {
                        //        dtRole = crudObject.GetUnderTakerByDeptPointSn(vEmpTmuHr.EmpUntid, vAuditPointRole.AuditPointSn); //第一關的承辦員
                        //    }
                        //   vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                        //   vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                        //   vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString(); 
                        //}
                        ////人資長設定 097200 原資訊長 089145
                        //if (vAuditPointRole.AuditPointSn.Equals("8"))
                        //{
                        //    DataTable dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId("097200");
                        //    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                        //    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                        //    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                        //}
                        ////以下Code請參照AuditPointRole AuditPointSn AuditPointStage AuditPointStep
                        ////人資處 7 (Stage=4 Step=1) 091058 ; 9 (Stage=4 Step=3) 085020;11 (Stage=7 Step=1) 091058 (Stage=7 Step=2) 085020
                        //if (vAuditPointRole.AuditPointSn.Equals("7"))
                        //{
                        //    DataTable dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId("091058");
                        //    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                        //    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                        //    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                        //}
                        //if (vAuditPointRole.AuditPointSn.Equals("9"))
                        //{
                        //    DataTable dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId("085020");
                        //    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                        //    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                        //    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                        //}
                        //if (vAuditPointRole.AuditPointSn.Equals("11"))
                        //{
                        //    DataTable dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId("091058");
                        //    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                        //    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                        //    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                        //}
                        //if (vAuditPointRole.AuditPointSn.Equals("12"))
                        //{
                        //    DataTable dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId("085020");
                        //    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                        //    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                        //    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                        //}

                        //讀取每一簽核人員開放權限的起始與終止日
                        vAuditPeroid = crudObject.GetAuditPeriod(vAuditExecute.ExecuteStage);
                        vAuditExecute.ExecuteBngDate = vAuditPeroid.AuditPeroidBeginDate;
                        vAuditExecute.ExecuteEndDate = vAuditPeroid.AuditPeroidEndDate;
                        vAuditExecute.ExecuteStatus = false;
                        crudObject.Insert(vAuditExecute);
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
                VSendEmail vSendEmail = new VSendEmail();
                vSendEmail.MailToAccount = "up_group@tmu.edu.tw";
                vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
                vSendEmail.MailSubject = vEmp.EmpNameCN + "由「教師聘任升等作業系統」 申請『" + vUnit.UntNameFull + "』系所的教師";
                vSendEmail.ToAccountName = "系統管理者";
                vSendEmail.MailContent = "請確認送出升等申請文件，" + vEmp.EmpNameCN + "的文件已進行簽核！！&nbsp;&nbsp; <a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入查看</a> ";
                //寄發Email通知
                Mail mail = new Mail();
                if (mail.SendEmail(vSendEmail))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您已成功送出申請，已通知管理者並進入教師應聘審查流程!');", true);
                }

                //送出Email給第一位審查員
                VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(vApplyAudit.AppSn); //vApplyAudit.AppSn 

                //Email
                VApplyerData vApplyerData = crudObject.GetApplyDataForEmail(vApplyAudit.AppSn);

                if (vAuditExecuteNextOne != null && vApplyerData != null)
                {
                    vSendEmail = new VSendEmail();
                    vSendEmail.MailToAccount = vAuditExecuteNextOne.ExecuteAuditorEmail;
                    vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
                    vSendEmail.MailSubject = "「教師聘任升等作業系統」有申請文件請盡速簽核";
                    vSendEmail.ToAccountName = vAuditExecuteNextOne.ExecuteRoleName + " " + vAuditExecuteNextOne.ExecuteAuditorName;
                    try
                    {
                        int acctSn = 0;
                        //在AccountForManage是否存在
                        acctSn = crudObject.GetAccountForManageAcctSn(vAuditExecuteNextOne.ExecuteAuditorEmail);
                        VAccountForManage vAccountForManage = new VAccountForManage();
                        if (acctSn == 0)
                        {
                            //新增一筆校內管理者資料 權限為A 僅有稽核權限

                            vAccountForManage.AcctEmail = vAuditExecuteNextOne.ExecuteAuditorEmail;
                            vAccountForManage.AcctEmpId = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                            vAccountForManage.AcctPassword = "123456";
                            vAccountForManage.AcctRole = "A";
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
                        vSendEmail.MailContent = "<br> 「教師聘任升等作業系統」需您的核准!<br><font color=red>請再完成您的審核</font>,申請者才能進入下一階段的審核.<br> " + strTableData + " <br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入審核</a>  您的帳號:" + vAccountForManage.AcctEmail + " 密碼:校內使用的密碼 <br/><br><br><br><br>人資處 怡慧(2028) 伊芝(2066)<br>";

                        //更新 ApplyAudit 中的 審核Stage與Step的狀態
                        VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                        vApplyAuditUpdate.AppSn = vAuditExecuteNextOne.AppSn;
                        vApplyAuditUpdate.AppStage = vAuditExecuteNextOne.ExecuteStage;
                        vApplyAuditUpdate.AppStep = vAuditExecuteNextOne.ExecuteStep;
                        vApplyAuditUpdate.AppStatus = true;
                        crudObject.UpdateApplyAuditStageStepStatus(vApplyAuditUpdate); //改變申請狀態，完成申請，進入審查流程

                        //更新 EmployeeBase Status 為 True
                        crudObject.UpdateEmployeeStatus(vApplyerData.EmpSn);

                        //寄發Email通知
                        mail.SendEmail(vSendEmail);
                    }
                    catch (Exception ex)
                    {
                        MessageLabel.Text = ex.ToString();

                    }
                }
                LoadDataBtn_Click(sender, e);
                DESCrypt DES = new DESCrypt();
                string parameters = "ApplyerID=" + DES.Encrypt(EmpIdno.Text.ToString());
                //Response.Redirect("~/ApplyEmp.aspx?" + parameters);   
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您已成功送出申請，已通知審核者並進入應聘流程!\\n可查看審核狀態!'); window.location='PromoteEmp.aspx?" + parameters + "';", true);


                //LoadDataBtn_Click(sender, e);
                #endregion
            }

        }


        protected void BtnApplyList_Click(object sender, EventArgs e)
        {
            Session["times"] = 1;
            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
            DESCrypt DES = new DESCrypt();
            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=2&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
            //parameters = Uri.EscapeDataString(parameters);
            Response.Redirect("~/ApplyList.aspx?" + parameters);
        }

        //開放給其他有權限異動者
        protected void EmpBaseModifySave_Click(object sender, EventArgs e)
        {
            TransferDataToEmpObject();
            InsertAppendData();
            //SaveEmpObjectToBackupDB(sender, e);
            string parameters = "ApplyerID=" + Request["ApplyerID"] + "&ManageEmpId=" + Request["ManageEmpId"] + "&Identity=" + Request["Identity"];
            //parameters = Uri.EscapeDataString(parameters);
            //Response.Redirect("~/PromoteEmp.aspx?" + parameters);
            LoadDataBtn_Click(sender, e);
        }

        protected void BtnReturnBack_Click(object sender, EventArgs e)
        {
            string parameters = "EmpSn=" + Session["EmpSn"].ToString();
            parameters += "&AppSn=" + Session["AppSn"].ToString();
            Response.Redirect("~/ManageSetAudit.aspx?" + parameters);
        }


        protected void BtnPromotePrint_Click(object sender, EventArgs e)
        {
            string parameters = "EmpSn=" + Session["EmpSn"].ToString();
            //Response.Redirect("~/ApplyPrint.aspx?" + parameters); 
            //if(AppJobTypeNo.SelectedValue.ToString().Equals("1")
            string path = "PromotePrint.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + path + "','_blank','toolbar=0,menubar=1,location=0,scrollbars=1,resizable=1,height=800,width=1000') ;", true);
        }

        //處裡所有上傳檔案
        protected void ProcessUploadFiles(int subfolder)
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
                            if (filesize > 20971520 || filesize <= 0)
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
                                    else
                                    {
                                        file.PostedFile.SaveAs(strFolderPath + fileName);
                                        MessageLabel.Text += "<br>上傳成功:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;
                                    }
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


        //查詢已存資料
        protected void LoadDataBtn_Click(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if (EmpIdno.Text.Equals(""))
            {
                MessageLabel.Text = "目前無您的資料：請輸入相關資料!";
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
                    VEmpTmuHr vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(tEmpIdno);
                    if (vEmpTmuHr != null)
                    {
                        EmpId.Text = vEmpTmuHr.EmpId;
                        EmpIdno.Text = vEmp.EmpIdno;
                        EmpBirthDay.Text = vEmp.EmpBirthDay;
                        EmpPassportNo.Text = vEmp.EmpPassportNo;
                        EmpNameENFirst.Text = vEmp.EmpNameENFirst;
                        EmpNameENLast.Text = vEmp.EmpNameENLast;
                        EmpNameCN.Text = vEmp.EmpNameCN;
                        EmpSex.Text = vEmp.EmpSex.Trim().Equals("M") ? "男" : "女";
                        EmpCountry.Text = crudObject.GetCountryName(vEmp.EmpCountry.Trim()).Rows.Count == 0 ? "" : crudObject.GetCountryName(vEmpTmuHr.EmpNation.Trim()).Rows[0][0].ToString();
                        EmpUnit.Text = vEmpTmuHr.EmpUntFullName;
                        EmpUnitNo.Text = vEmpTmuHr.EmpUntid;
                        EmpNowEJobTitle.Text = vEmpTmuHr.EmpTitidName;
                        EmpTeachNo.Text = vEmpTmuHr.EmpTeachno;
                        Session["EmpTitid"] = vEmpTmuHr.EmpTitid;
                        Session["EmpId"] = EmpId.Text;
                        Session["EmpUntid"] = vEmpTmuHr.EmpUntid.ToString();
                        Session["EmpWUntid"] = vEmpTmuHr.EmpWUntid.ToString();
                        Session["EmpTitleId"] = vEmpTmuHr.EmpTitid.ToString();
                    }
                    int intDate = Int32.Parse("" + (DateTime.Now.Year - 1911) + String.Format("{0:00}", DateTime.Now.Month) + String.Format("{0:00}", DateTime.Now.Day));

                    EmpCountry.DataBind();
                    CRUDObject crudobject = new CRUDObject();
                    if (Session["AppSn"] != null && String.IsNullOrEmpty(crudobject.GetAppJobTitleNo(Session["AppSn"].ToString())))
                        AppJobTitleNo.DataBind();
                    else
                    {
                        if (AppJobTitleNo != null)
                            AppJobTitleNo.SelectedValue = crudobject.GetAppJobTitleNo(Session["AppSn"].ToString());
                    }
                    AppJobTypeNo.DataBind();
                    DDEmpUnit.Items.Clear();
                    DDEmpUnit.Items.Add(new ListItem(crudObject.GetDeptUntName(vEmpTmuHr.EmpUntid), vEmpTmuHr.EmpUntid));
                    if (!vEmpTmuHr.EmpUntid.Equals(vEmpTmuHr.EmpWUntid)) DDEmpUnit.Items.Add(new ListItem(crudObject.GetDeptUntName(vEmpTmuHr.EmpWUntid), vEmpTmuHr.EmpWUntid));
                    //其他兼職單位
                    DataTable dt = crudObject.GetOtherJob(vEmpTmuHr.EmpIdno);
                    List<string> lst = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("oth_untid")).Distinct().ToList();
                    foreach (var untid in lst)
                    {
                        if (!untid.Equals(vEmpTmuHr.EmpUntid) && !untid.Equals(vEmpTmuHr.EmpWUntid))
                            DDEmpUnit.Items.Add(new ListItem(crudObject.GetDeptUntName(untid), untid));
                    }


                    DDEmpUnit.Items[0].Selected = true;
                    DDEmpUnit.DataBind();
                    //EmpNowEJobTitle.Text = vEmpTmuHr.EmpTitidName;

                    DDEmpNowEJobTitle.Items.Clear();
                    DDEmpNowEJobTitle.Items.Add(new ListItem(crudObject.GetTitleName(vEmpTmuHr.EmpTitid), vEmpTmuHr.EmpTitid));
                    //其他兼職職務
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (!dt.Rows[j]["oth_titid"].ToString().Equals(vEmpTmuHr.EmpTitid) &&
                            !dt.Rows[j]["oth_titid"].ToString().Equals(vEmpTmuHr.EmpTitid))
                        {
                            DDEmpNowEJobTitle.Items.Add(new ListItem(crudObject.GetTitleName(dt.Rows[j]["oth_titid"].ToString()), dt.Rows[j]["oth_titid"].ToString()));
                        }
                    }
                    DDEmpNowEJobTitle.Items[0].Selected = true;



                    String ExpStartDate = crudObject.GetTeacherTmuExp(vEmpTmuHr.EmpIdno, vEmpTmuHr.EmpTitid);
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

                    EmpYear.Text = "" + String.Format("{0:00}", diffYear) + "/" + String.Format("{0:00}", diffMonth) + " (" + intYear + String.Format("{0:00}", DateTime.Now.Month) + ")";

                    //String ExpStartDate = crudObject.GetTeacherTmuExp(vEmpTmuHr.EmpIdno, vEmpTmuHr.EmpTitid);
                    //if (ExpStartDate.Equals(""))
                    //{

                    //}
                    //else
                    //{
                    //    DateTime nowdate = DateTime.Now;
                    //    int intYear = Int32.Parse(ExpStartDate.Substring(0, 3));
                    //    int intMonth = Int32.Parse(ExpStartDate.Substring(3, 2));
                    //    int nowYear = nowdate.Year - 1911;
                    //    int nowMonth = nowdate.Month - 1;
                    //    if (nowMonth < intMonth)
                    //    {
                    //        nowYear--;
                    //        nowMonth = nowMonth + 12;
                    //    }
                    //    diffYear = nowYear - intYear;
                    //    diffMonth = nowMonth - intMonth;

                    //    EmpYear.Text = "" + String.Format("{0:00}", diffYear) + "/" + String.Format("{0:00}", diffMonth) + " (" + intYear + String.Format("{0:00}", intMonth) + ")";
                    //}
                    //升等類型:1著作送審升等 2學位送審升等 3臨床教師升等
                    AppAttributeNo.DataValueField = "status";
                    AppAttributeNo.DataTextField = "note";
                    AppAttributeNo.DataSource = crudObject.GetApplyPrmAttribute().DefaultView;
                    AppAttributeNo.DataBind();
                    //AppELawNumNo.DataValueField = "LawItemNo";
                    //AppELawNumNo.DataTextField = "LawItemNoCN";
                    //AppELawNumNo.DataSource = crudObject.GetApplyTeacherLaw(AppJobTitleNo.SelectedValue.ToString()).DefaultView;
                    //AppELawNumNo.DataBind();


                    //EmpSex.Text = vEmp.EmpSex;
                    //EmpCountry.SelectedValue = vEmp.EmpCountry; 
                    //EmpHomeTown.Text = vEmp.EmpHomeTown;
                    //EmpBornProvince.Text = vEmp.EmpBornCity;
                    //EmpBornCity.Text = vEmp.EmpBornCity;

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

                    //上傳畢業證書
                    if (vEmp.EmpDegreeUpload) EmpDegreeUploadCB.Checked = vEmp.EmpDegreeUpload;
                    if (EmpDegreeUploadCB.Checked)
                    {
                        //EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeHyperLink.NavigateUrl = getHyperLink(vEmp.EmpDegreeUploadName);
                        EmpDegreeHyperLink.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;
                        EmpDegreeHyperLink.Visible = true;
                        EmpDegreeUploadFU.Visible = true;
                    }
                    else
                    {
                        EmpDegreeUploadFU.Visible = true;
                        EmpDegreeHyperLink.Visible = false;
                    }

                    //TeachExp TeachCa TeachHornour 狀態資料帶出
                    DESCrypt DES = new DESCrypt();
                    //載入ApplyAudit共用延伸資料

                    if (Request["HRAppSn"] != null && !Request["HRAppSn"].ToString().Equals(""))
                        vApplyAudit = crudObject.GetApplyAuditObjByHRAppSnPromote(vEmp.EmpIdno, Request["HRAppSn"].ToString());
                    else if (Request["AppSn"] != null && !Request["AppSn"].ToString().Equals(""))
                        vApplyAudit = crudObject.GetApplyAuditObjByAppSnPromote(vEmp.EmpIdno, DES.Decrypt(Request["AppSn"].ToString()));
                    else
                        vApplyAudit = crudObject.GetApplyAuditObjByIdnoPromote(vEmp.EmpIdno);




                    if (vApplyAudit.ReasearchResultUploadName != null && !vApplyAudit.ReasearchResultUploadName.Equals(""))
                    {
                        link_AppReasearchResult.NavigateUrl = getHyperLink(vApplyAudit.ReasearchResultUploadName);
                        link_AppReasearchResult.Text = vApplyAudit.ReasearchResultUploadName;
                        link_AppReasearchResult.Visible = true;
                    }



                    //指定性別

                    //指定單位
                    if (!Object.Equals(null, vApplyAudit))
                    {
                        //若已送出資料申請系所抓此
                        if (!vApplyAudit.AppUnitNo.Equals(""))
                        {
                            EmpUnit.Text = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                            //EmpUnitNo.Text = vApplyAudit.AppUnitNo;
                            for (int i = 0; i < DDEmpUnit.Items.Count; i++)
                            {
                                if (vApplyAudit.AppUnitNo.Equals(DDEmpUnit.Items[i].Value))
                                {
                                    DDEmpUnit.Items[i].Selected = true;
                                }
                                else
                                {
                                    DDEmpUnit.Items[i].Selected = false;
                                }
                            }

                            //現職資料把存入的資料帶出來   
                            //現職資料若無存入資料 先抓unit wunit 對應 s10_expfcu 中的titleid 若是沒有則抓 s10_otherpos
                            String tempTitle = "";
                            tempTitle = vApplyAudit.AppJobTitleNo;
                            if (tempTitle.Equals("") && !DDEmpNowEJobTitle.SelectedValue.ToString().Equals(""))
                            {
                                if (!DDEmpUnit.SelectedValue.ToString().Equals(""))
                                {
                                    tempTitle = vEmpTmuHr.EmpTitid;
                                }
                            }
                            if (!tempTitle.Equals(""))
                            {
                                for (int i = 0; i < DDEmpNowEJobTitle.Items.Count; i++)
                                {
                                    if (tempTitle.Equals(DDEmpNowEJobTitle.Items[i].Value))
                                    {
                                        DDEmpNowEJobTitle.Items[i].Selected = true;
                                    }
                                    else
                                    {
                                        DDEmpNowEJobTitle.Items[i].Selected = false;
                                    }
                                }
                            }

                            DataTable dtTitleId = crudObject.GetTeacherTmuTitId(vEmpTmuHr.EmpIdno, vApplyAudit.AppUnitNo);
                            if (!Object.Equals(null, dtTitleId))
                            {
                                EmpNowEJobTitle.Text = dtTitleId.Rows[0][1].ToString();
                                EmpNowEJobTitleNo.Text = dtTitleId.Rows[0][0].ToString(); //取得現任職等
                            }
                            ExpStartDate = crudObject.GetTeacherTmuExp(vEmpTmuHr.EmpIdno, EmpNowEJobTitleNo.Text.ToString());
                            if (ExpStartDate.Equals(""))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('系統無法正確取得人事資料─『現任教師職等資料』!\\n請洽人資處!');", true);
                            }
                            else
                            {

                                intYear = Int32.Parse(ExpStartDate.Substring(0, 3));
                                intMonth = Int32.Parse(ExpStartDate.Substring(3, 2));
                                nowYear = nowdate.Year - 1911;
                                nowMonth = nowdate.Month - 1;
                                if (nowMonth < intMonth)
                                {
                                    nowYear--;
                                    nowMonth = nowMonth + 12;
                                }
                                diffYear = nowYear - intYear;
                                diffMonth = nowMonth - intMonth;

                                EmpYear.Text = "" + String.Format("{0:00}", diffYear) + "/" + String.Format("{0:00}", diffMonth) + " (" + intYear + String.Format("{0:00}", intMonth) + ")";
                            }
                        }
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
                        //顯示升等途徑 AppWayName
                        AppWayName.Text = crudObject.GetAuditWayName(vApplyAudit.AppWayNo).Rows[0][0].ToString();
                        AuditWayName.Text = crudObject.GetAuditWayName(vApplyAudit.AppWayNo).Rows[0][0].ToString();

                        //顯示升等類型 AppAttributeNo ViewState["ApplyAttributeNo"].ToString()
                        for (int i = 0; i < AppAttributeNo.Items.Count; i++)
                        {
                            if (ViewState["ApplyAttributeNo"].ToString().Equals(AppAttributeNo.Items[i].Value))
                            {
                                AppAttributeNo.Items[i].Selected = true;
                                AppAttributeName.Text = AppAttributeNo.Items[i].ToString();
                                AuditAttributeName.Text = AppAttributeNo.Items[i].ToString();
                            }
                            else
                            {
                                AppAttributeNo.Items[i].Selected = false;
                            }
                        }



                        //載入代表著作
                        //string tmp = vApplyAudit.AppPublicationUploadName;
                        //Session["PublicationUploadName"] = tmp;

                        if (GVThesisScore.Rows.Count > 0)
                            AppPThesisAccuScore.Text = vApplyAudit.AppThesisAccuScore;
                        else
                            AppPThesisAccuScore.Text = "0";
                        AppRPI.Text = vApplyAudit.AppRPIScore;

                        if (GVThesisScore.Rows.Count > 0)
                            AppPThesisAccuScore1.Text = vApplyAudit.AppThesisAccuScore;
                        else
                            AppPThesisAccuScore1.Text = "0";
                        AppRPI.Text = vApplyAudit.AppRPIScore;


                        //教師資格切結書
                        if (vApplyAudit.AppDeclarationUploadName != null && !vApplyAudit.AppDeclarationUploadName.Equals(""))
                        {
                            AppDeclarationUploadCB.Checked = true;
                            AppDeclarationHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppDeclarationUploadName);
                            AppDeclarationHyperLink.Text = vApplyAudit.AppDeclarationUploadName;
                            AppDeclarationUploadFUName.Text = vApplyAudit.AppDeclarationUploadName;
                            AppDeclarationHyperLink.Visible = true;
                            AppDeclarationUploadFU.Visible = true;
                        }
                        else
                        {
                            AppDeclarationUploadFU.Visible = true;
                            AppDeclarationHyperLink.Visible = false;
                        }

                        //下載推薦函
                        if (vApplyAudit.AppRecommendUploadName != null && !vApplyAudit.AppRecommendUploadName.Equals(""))
                        {
                            RecommendUploadTableRow.Visible = true;
                            RecommendHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppRecommendUploadName);
                            RecommendHyperLink.Text = vApplyAudit.AppRecommendUploadName;
                            RecommendUploadFUName.Text = vApplyAudit.AppRecommendUploadName;
                            RecommendUploadCB.Checked = true;
                            RecommendHyperLink.Visible = true;
                        }
                        else
                        {
                            RecommendUploadFU.Visible = true;
                            RecommendHyperLink.Visible = false;
                        }

                        if (vEmp.EmpNoTeachExp)
                        {
                            CBNoTeachExp.Checked = true;
                        }
                        else
                        {
                            CBNoTeachExp.Checked = false;
                        }
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


                        //載入其他

                        //教學
                        if (vApplyAudit.AppOtherTeachingUploadName != null && !vApplyAudit.AppOtherTeachingUploadName.Equals(""))
                        {
                            OtherTeachingTableRow.Visible = true;
                            AppOtherTeachingUploadCB.Checked = true;
                            AppOtherTeachingHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppOtherTeachingUploadName);
                            AppOtherTeachingHyperLink.Text = vApplyAudit.AppOtherTeachingUploadName;
                            AppOtherTeachingUploadFUName.Text = vApplyAudit.AppOtherTeachingUploadName;
                            AppOtherTeachingHyperLink.Visible = true;
                            AppOtherTeachingUploadFU.Visible = true;
                        }
                        else
                        {
                            AppOtherTeachingUploadFUName.Visible = true;
                            AppOtherTeachingHyperLink.Visible = false;
                        }

                        //服務
                        if (vApplyAudit.AppOtherServiceUploadName != null && !vApplyAudit.AppOtherServiceUploadName.Equals(""))
                        {
                            OtherServiceTableRow.Visible = true;
                            AppOtherServiceUploadCB.Checked = true;
                            AppOtherServiceHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppOtherServiceUploadName);
                            AppOtherServiceHyperLink.Text = vApplyAudit.AppOtherServiceUploadName;
                            AppOtherServiceUploadFUName.Text = vApplyAudit.AppOtherServiceUploadName;

                            AppOtherServiceHyperLink.Visible = true;
                            AppOtherServiceUploadFU.Visible = true;
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
                            AppPPMUploadFUName.Text = vApplyAudit.AppPPMUploadName;

                            AppPPMHyperLink.Visible = true;
                            AppPPMUploadFU.Visible = true;
                        }
                        else
                        {
                            AppPPMUploadFU.Visible = true;
                            AppPPMHyperLink.Visible = false;
                        }
                        //下載合著人證明(好像真的不用)
                        //if (vApplyAudit.AppCoAuthorUploadName != null && !vApplyAudit.AppCoAuthorUploadName.Equals(""))
                        //{
                        //    AppPSummaryCN.Text = vApplyAudit.AppSummaryCN;
                        //    AppPCoAuthorUploadCB.Checked = true;
                        //    AppPCoAuthorHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppCoAuthorUploadName);
                        //    AppPCoAuthorHyperLink.Text = vApplyAudit.AppCoAuthorUploadName;
                        //    AppPCoAuthorHyperLink.Visible = true;
                        //    AppPCoAuthorUploadFU.Visible = true;
                        //    AppPCoAuthorUploadFUName.Text = vApplyAudit.AppCoAuthorUploadName;
                        //}
                        //else
                        //{
                        //    AppPCoAuthorUploadFU.Visible = true;
                        //    AppPCoAuthorUploadFUName.Visible = false;
                        //}

                        //醫師證書                        
                        if (vApplyAudit.AppDrCaUploadName != null && !vApplyAudit.AppDrCaUploadName.Equals(""))
                        {
                            AppDrCaUploadCB.Checked = true;
                            AppDrCaHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppDrCaUploadName);
                            AppDrCaHyperLink.Text = vApplyAudit.AppDrCaUploadName;
                            AppDrCaUploadFUName.Text = vApplyAudit.AppDrCaUploadName;

                            AppDrCaHyperLink.Visible = true;
                            AppDrCaUploadFU.Visible = true;
                        }
                        else
                        {
                            AppDrCaUploadFU.Visible = true;
                            AppDrCaHyperLink.Visible = false;
                        }

                        //教育部教師資格證書影
                        if (vApplyAudit.AppTeacherCaUploadName != null && !vApplyAudit.AppTeacherCaUploadName.Equals(""))
                        {
                            AppTeacherCaTableRow.Visible = true;
                            AppTeacherCaUploadCB.Checked = true;
                            AppTeacherCaHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppTeacherCaUploadName);
                            AppTeacherCaHyperLink.Text = vApplyAudit.AppTeacherCaUploadName;
                            AppTeacherCaUploadFUName.Text = vApplyAudit.AppTeacherCaUploadName;

                            AppTeacherCaHyperLink.Visible = true;
                            AppTeacherCaUploadFU.Visible = true;
                        }
                        else
                        {
                            AppTeacherCaUploadFU.Visible = true;
                            AppTeacherCaHyperLink.Visible = false;
                        }



                        //下載著作出版等刊物或個人事蹟等相關資料
                        if (vApplyAudit.AppPublicationUploadName != null && !vApplyAudit.AppPublicationUploadName.Equals(""))
                        {
                            AppPublicationUploadCB.Checked = true;
                            AppPublicationHyperLink.NavigateUrl = getHyperLink(vApplyAudit.AppPublicationUploadName);
                            AppPublicationHyperLink.Text = vApplyAudit.AppPublicationUploadName;
                            AppPublicationUploadFUName.Text = vApplyAudit.AppPublicationUploadName;

                            AppPublicationHyperLink.Visible = true;
                            AppPublicationUploadFU.Visible = true;
                        }
                        else
                        {
                            AppPublicationUploadFU.Visible = true;
                            AppPublicationHyperLink.Visible = false;
                        }


                        //應徵職稱
                        if (ViewState["AppJobTitleNo"] == null || ViewState["AppJobTitleNo"].Equals(""))
                        {
                            for (int i = 0; i < AppJobTitleNo.Items.Count; i++)
                            {
                                if (vApplyAudit.AppJobTitleNo.Equals(AppJobTitleNo.Items[i].Value))
                                {
                                    AppJobTitleNo.Items[i].Selected = true;
                                }
                                else
                                {
                                    AppJobTitleNo.Items[i].Selected = false;
                                }
                            }
                            AddELawNumControls(chkPromote, vApplyAudit.AppJobTitleNo, vApplyAudit.AppLawNumNo);
                        }
                        else
                        {
                            for (int i = 0; i < AppJobTitleNo.Items.Count; i++)
                            {
                                if (ViewState["AppJobTitleNo"].ToString().Equals(AppJobTitleNo.Items[i].Value))
                                {
                                    AppJobTitleNo.Items[i].Selected = true;
                                    AuditJobTitle.Text = AppJobTitleNo.Items[i].Value;
                                }
                                else
                                {
                                    AppJobTitleNo.Items[i].Selected = false;
                                }
                            }
                            AddELawNumControls(chkPromote, ViewState["AppJobTitleNo"].ToString(), vApplyAudit.AppLawNumNo);
                        }

                        //這是為了將職稱代碼順利的對應到法規條文
                        //30000	教授	4
                        //30400	臨床教授	4
                        //40000	副教授	3
                        //40400	臨床副教授	3
                        //50000	助理教授	2
                        //50400	臨床助理教授	2
                        //60000	講師	1
                        //60400	臨床講師	1
                        //轉換法規條文邏輯 代碼取第一位數 用7 去減

                        String[] num = { "零", "一", "二", "三", "四", "五" };
                        //法規第幾項
                        if (!AppJobTitleNo.SelectedValue.ToString().Equals("") && !AppJobTitleNo.SelectedValue.ToString().Equals("請選擇"))
                        {
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("030400"))
                            {
                                ItemNo.Text = num[1];
                                lbchose.Text = "三";
                            }
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("040400"))
                            {
                                ItemNo.Text = num[1];
                                lbchose.Text = "四";
                            }
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("050400"))
                            {
                                ItemNo.Text = num[1];
                                lbchose.Text = "五";
                            }
                            if (AppJobTitleNo.SelectedValue.ToString().Equals("060400"))
                            {
                                ItemNo.Text = num[0];
                                lbchose.Text = "六";
                            }
                            if (!AppJobTitleNo.SelectedValue.ToString().Equals("030400") &&
                                !AppJobTitleNo.SelectedValue.ToString().Equals("040400") &&
                                !AppJobTitleNo.SelectedValue.ToString().Equals("050400") &&
                                !AppJobTitleNo.SelectedValue.ToString().Equals("060400"))
                                ItemNo.Text = num[(7 - Int32.Parse(AppJobTitleNo.SelectedValue.ToString().Substring(1, 1)))];
                        }

                        //法規第幾款
                        ItemLabel.Text = num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)];

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
                                    AuditJobType.Text = AppJobTypeNo.Items[i].Value;
                                }
                                else
                                {
                                    AppJobTypeNo.Items[i].Selected = false;
                                }
                            }
                        }
                        //身份證上傳
                        if (vEmp.EmpIdnoUploadName != null && !vEmp.EmpIdnoUploadName.Equals(""))
                        {
                            EmpIdnoUploadCB.Checked = true;
                            EmpIdnoHyperLink.NavigateUrl = getHyperLink(vEmp.EmpIdnoUploadName);
                            EmpIdnoHyperLink.Text = vEmp.EmpIdnoUploadName;
                            EmpIdnoUploadFUName.Text = vEmp.EmpIdnoUploadName;

                            EmpIdnoHyperLink.Visible = true;
                            EmpIdnoUploadFU.Visible = true;
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
                            EmpDegreeUploadFUName.Text = vEmp.EmpDegreeUploadName;

                            EmpDegreeHyperLink.Visible = true;
                            EmpDegreeUploadFU.Visible = true;
                        }
                        else
                        {
                            EmpDegreeUploadFU.Visible = true;
                            EmpDegreeHyperLink.Visible = false;
                        }



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
                        //law = crudObject.GetApplyTeacherLaw(AppJobTitleNo.SelectedValue.ToString(), AppELawNumNo.SelectedValue.ToString());
                        //if (law.Length > 49)
                        //{
                        //    law = law.Substring(0, 49) + "<br/>" + law.Substring(50);
                        //}

                        //AppLawNumMessage.Text = law;

                        //載入 AppendEmployee新聘延伸資料檔
                        //vAppendEmployee = crudObject.GetAppendEmployeeObj(vApplyAudit.AppSn);
                        //if (!Object.Equals(vAppendEmployee, null))
                        //{
                        //    AppENowJobOrg.Text = vAppendEmployee.AppNowJobOrg;
                        //    AppENote.Text = vAppendEmployee.AppNote;
                        //    AppERecommendors.Text = vAppendEmployee.AppRecommendors;
                        //    AppERecommendYear.Text = vAppendEmployee.AppRecommendYear;


                        //}



                        VAppendPromote vAppendPromote = null;
                        if (Session["AppSn"] != null && !Session["AppSn"].ToString().Equals(""))
                        {
                            vAppendPromote = crudObject.GetAppendPromoteObj(Int32.Parse(Session["AppSn"].ToString()));
                        }

                        if (vAppendPromote != null)
                        {
                            //經歷服務證明
                            if (vAppendPromote.ExpServiceCaUploadName != null && !vAppendPromote.ExpServiceCaUploadName.Equals(""))
                            {
                                ExpServiceCaUploadCB.Checked = true;
                                ExpServiceCaHyperLink.NavigateUrl = getHyperLink(vAppendPromote.ExpServiceCaUploadName);
                                ExpServiceCaHyperLink.Text = vAppendPromote.ExpServiceCaUploadName;
                                ExpServiceCaUploadFUName.Text = vAppendPromote.ExpServiceCaUploadName;

                                ExpServiceCaHyperLink.Visible = true;
                                ExpServiceCaUploadFU.Visible = true;
                            }
                            else
                            {
                                ExpServiceCaUploadFU.Visible = true;
                                ExpServiceCaHyperLink.Visible = false;
                            }

                            //現任職等部定影印本
                            if (vAppendPromote.ExpServiceCaUploadName != null && !vAppendPromote.ExpServiceCaUploadName.Equals(""))
                            {
                                ExpServiceCaHyperLink.NavigateUrl = getHyperLink(vAppendPromote.ExpServiceCaUploadName);
                                ExpServiceCaHyperLink.Text = vAppendPromote.ExpServiceCaUploadName;
                                ExpServiceCaUploadFUName.Text = vAppendPromote.ExpServiceCaUploadName;

                                ExpServiceCaHyperLink.Visible = true;
                                ExpServiceCaUploadFU.Visible = true;
                            }
                            else
                            {
                                ExpServiceCaUploadFU.Visible = true;
                                ExpServiceCaHyperLink.Visible = false;
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
                                    RPIDiscountScore4UploadCB.Checked = true;
                                }
                                else
                                {
                                    RPIDiscountScore4.Items[i].Selected = false;
                                }
                            }

                            if (vAppendPromote.RPIDiscountNo)
                            {
                                RPIDiscountNo.Checked = true;
                            }
                            else
                            {
                                RPIDiscountNo.Checked = false;
                            }

                            //師鐸獎
                            if (vAppendPromote.RPIDiscountScore1Name != null && !vAppendPromote.RPIDiscountScore1Name.Equals(""))
                            {
                                RPIDiscountScore1UploadCB.Checked = true;
                                RPIDiscountScore1HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore1Name);
                                RPIDiscountScore1HyperLink.Text = vAppendPromote.RPIDiscountScore1Name;
                                RPIDiscountScore1UploadFUName.Text = vAppendPromote.RPIDiscountScore1Name;

                                RPIDiscountScore1HyperLink.Visible = true;
                                RPIDiscountScore1UploadFU.Visible = true;
                            }
                            else
                            {
                                RPIDiscountScore1UploadFU.Visible = true;
                                RPIDiscountScore1HyperLink.Visible = false;
                            }

                            //教師優良教師
                            if (vAppendPromote.RPIDiscountScore2Name != null && !vAppendPromote.RPIDiscountScore2Name.Equals(""))
                            {
                                RPIDiscountScore2UploadCB.Checked = true;
                                RPIDiscountScore2HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore2Name);
                                RPIDiscountScore2HyperLink.Text = vAppendPromote.RPIDiscountScore2Name;
                                RPIDiscountScore2UploadFUName.Text = vAppendPromote.RPIDiscountScore2Name;

                                RPIDiscountScore2HyperLink.Visible = true;
                                RPIDiscountScore2UploadFU.Visible = true;
                            }
                            else
                            {
                                RPIDiscountScore2UploadFU.Visible = true;
                                RPIDiscountScore2HyperLink.Visible = false;
                            }

                            //優良導師
                            if (vAppendPromote.RPIDiscountScore3Name != null && !vAppendPromote.RPIDiscountScore3Name.Equals(""))
                            {
                                RPIDiscountScore3UploadCB.Checked = true;
                                RPIDiscountScore3HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore3Name);
                                RPIDiscountScore3HyperLink.Text = vAppendPromote.RPIDiscountScore3Name;
                                RPIDiscountScore3UploadFUName.Text = vAppendPromote.RPIDiscountScore3Name;

                                RPIDiscountScore3HyperLink.Visible = true;
                                RPIDiscountScore3UploadFU.Visible = true;
                            }
                            else
                            {
                                RPIDiscountScore3UploadFU.Visible = true;
                                RPIDiscountScore3HyperLink.Visible = false;
                            }
                            //執行人體試驗
                            if (vAppendPromote.RPIDiscountScore4Name != null && !vAppendPromote.RPIDiscountScore4Name.Equals(""))
                            {
                                RPIDiscountScore4UploadCB.Checked = true;
                                RPIDiscountScore4HyperLink.NavigateUrl = getHyperLink(vAppendPromote.RPIDiscountScore4Name);
                                RPIDiscountScore4HyperLink.Text = vAppendPromote.RPIDiscountScore4Name;
                                RPIDiscountScore4UploadFUName.Text = vAppendPromote.RPIDiscountScore4Name;

                                RPIDiscountScore4HyperLink.Visible = true;
                                RPIDiscountScore4UploadFU.Visible = true;
                            }
                            else
                            {
                                RPIDiscountScore4UploadFU.Visible = true;
                                RPIDiscountScore4HyperLink.Visible = false;
                            }

                            RPIDiscountTotal.Text = vAppendPromote.RPIDiscountTotal;
                            RPIDiscountTotal1.Text = vAppendPromote.RPIDiscountTotal;
                            if (vApplyAudit.AppThesisAccuScore.Equals("")) vApplyAudit.AppThesisAccuScore = "0";
                            if (vAppendPromote.RPIDiscountTotal.Equals("")) vAppendPromote.RPIDiscountTotal = "0";
                            AppRPITotalScore.Text = "" + (Convert.ToDecimal(vApplyAudit.AppThesisAccuScore) + Convert.ToDecimal(vAppendPromote.RPIDiscountTotal));
                        }
                        VAppendDegree vAppendDegree = new VAppendDegree();
                        //載入學位論文
                        if (vApplyAudit.AppKindNo.Equals(chkPromote) && AppAttributeNo.SelectedValue.Equals(chkDegree))
                        {
                            vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                            if (vAppendDegree != null && vAppendDegree.AppDDegreeThesisUploadName != null && !vAppendDegree.AppDDegreeThesisUploadName.Equals(""))
                            {
                                AppDegreeThesisName.Text = vAppendDegree.AppDDegreeThesisName;
                                AppDegreeThesisNameEng.Text = vAppendDegree.AppDDegreeThesisNameEng;
                                if (vAppendDegree.AppDDegreeThesisUploadName != null && !vAppendDegree.AppDDegreeThesisUploadName.Equals(""))
                                {
                                    AppDegreeThesisUploadCB.Checked = true;
                                    AppDegreeThesisHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDDegreeThesisUploadName);
                                    AppDegreeThesisHyperLink.Text = vAppendDegree.AppDDegreeThesisUploadName;
                                    AppDegreeThesisUploadFUName.Text = vAppendDegree.AppDDegreeThesisUploadName;
                                    AppDegreeThesisHyperLink.Visible = true;
                                    AppDegreeThesisUploadFU.Visible = true;
                                }
                                else
                                {
                                    AppDegreeThesisUploadCB.Checked = false;
                                    AppDegreeThesisUploadFU.Visible = true;
                                    AppDegreeThesisHyperLink.Visible = false;
                                }
                            }

                            //載入外國學歷 以學位送審才需要
                            if (switchFgn != null && !switchFgn.Equals(""))
                            {
                                LoadFgn(vAppendDegree, switchFgn);
                            }

                            GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
                            GVThesisOral.DataBind();
                            if (GVThesisOral.Rows.Count == 0)
                                lb_NoThesisOral.Visible = true;
                            else
                                lb_NoThesisOral.Visible = false;

                        }

                        //DataTable dt = crudObject.GetSCITotalScore(vEmpTmuHr.EmpId);

                        try
                        {
                            dt = crudObject.GetThesisScoreCount(vEmpTmuHr.EmpId);
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


                    //若在系統開放期間
                    if (crudObject.GetDuringOpenDate("2"))
                    {
                        //顯示目前簽核狀態
                        if (vApplyAudit != null && vApplyAudit.AppStatus)
                        {

                            //送出 Button Enabled false
                            EmpBaseSave.Enabled = false;
                            EmpBaseTempSave.Enabled = false;
                            TeachEduSave.Enabled = false;
                            TeachLessonSave.Enabled = false;
                            //TeachExpSave.Enabled = false;
                            //ThesisScoreSave.Enabled = false;
                            ThesisOralSave.Enabled = false;
                            DegreeThesisSave.Enabled = false;
                            //ThesisScoreInsert.Enabled = false;
                        }
                        else
                        {
                            EmpBaseSave.Enabled = true;
                            EmpBaseTempSave.Enabled = true;
                        }
                    }
                    else
                    {
                        //若撈取審核資料中有退件者 Status = '3'
                        if (crudObject.IsAuditExecuteReturn(vApplyAudit.AppSn))
                        {
                            EmpBaseSave.Enabled = true;
                            EmpBaseTempSave.Enabled = true;
                            TeachEduSave.Enabled = true;
                            TeachLessonSave.Enabled = true;
                            //TeachExpSave.Enabled = true;
                            //ThesisScoreSave.Enabled = true;
                            ThesisOralSave.Enabled = true;
                            DegreeThesisSave.Enabled = true;
                            //ThesisScoreInsert.Enabled = false;
                        }
                        else
                        {
                            EmpBaseSave.Enabled = false;
                            EmpBaseTempSave.Enabled = false;
                            TeachEduSave.Enabled = false;
                            TeachLessonSave.Enabled = false;
                            //TeachExpSave.Enabled = false;
                            //ThesisScoreSave.Enabled = false;
                            ThesisOralSave.Enabled = false;
                            DegreeThesisSave.Enabled = false;
                            //ThesisScoreInsert.Enabled = false;
                        }
                    }

                    //判斷有權限修改
                    if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                    {
                        EmpBaseSave.Visible = false;
                        EmpBaseTempSave.Visible = false;
                        TeachEduSave.Enabled = true;
                        TeachLessonSave.Enabled = true;
                        //TeachExpSave.Enabled = true;
                        //ThesisScoreSave.Enabled = true;
                        ThesisOralSave.Enabled = true;
                        DegreeThesisSave.Enabled = true;
                        //ThesisScoreInsert.Enabled = true;
                    }
                    //EmpBaseSave.Enabled = true;
                }
                else
                {
                    MessageLabel.Text = "抱歉，您未曾申請過資料!!";
                }
            }
        }

        protected void CBNoTeachExp_OnClick(object sender, EventArgs e)
        {

            //Update NoTeachExp
            if (vEmp == null)
            {
                vEmp = new VEmployeeBase();
                vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text.ToString());
            }
            //if (CBNoTeachExp.Checked)
            //{
            //    vEmp.EmpNoTeachExp = true;
            //    TeachExpSave.Visible = false;
            //    //TbTeachExp.Visible = false;
            //}
            //else
            //{
            //    vEmp.EmpNoTeachExp = false;
            //    TeachExpSave.Visible = true;
            //    //TbTeachExp.Visible = true;
            //}
            crudObject.UpdateEmployeeNoTeachExp(vEmp);
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
                        if (vAppendDegree.AppDFgnDegreeUploadName != null && checkName(vAppendDegree.AppDFgnDegreeUploadName))
                        {
                            AppDFgnDegreeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnDegreeUploadName);
                            AppDFgnDegreeHyperLink.Text = vAppendDegree.AppDFgnDegreeUploadName;
                            AppDFgnDegreeUploadFUName.Text = vAppendDegree.AppDFgnDegreeUploadName;
                        }
                        AppDFgnDegreeHyperLink.Visible = true;
                        AppDFgnDegreeUploadFU.Visible = true;
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
                        if (vAppendDegree.AppDFgnGradeUploadName != null && checkName(vAppendDegree.AppDFgnGradeUploadName))
                        {
                            AppDFgnGradeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnGradeUploadName);
                            AppDFgnGradeHyperLink.Text = vAppendDegree.AppDFgnGradeUploadName;
                            AppDFgnGradeUploadFUName.Text = vAppendDegree.AppDFgnGradeUploadName;
                        }
                        AppDFgnGradeHyperLink.Visible = true;
                        AppDFgnGradeUploadFU.Visible = true;
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
                        if (vAppendDegree.AppDFgnSelectCourseUploadName != null && checkName(vAppendDegree.AppDFgnSelectCourseUploadName))
                        {
                            AppDFgnSelectCourseHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnSelectCourseUploadName);
                            AppDFgnSelectCourseHyperLink.Text = vAppendDegree.AppDFgnSelectCourseUploadName;
                            AppDFgnSelectCourseUploadFUName.Text = vAppendDegree.AppDFgnSelectCourseUploadName;
                        }
                        AppDFgnSelectCourseHyperLink.Visible = true;
                        AppDFgnSelectCourseUploadFU.Visible = true;
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
                        if (vAppendDegree.AppDFgnEDRecordUploadName != null && checkName(vAppendDegree.AppDFgnEDRecordUploadName))
                        {
                            AppDFgnEDRecordHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnEDRecordUploadName);
                            AppDFgnEDRecordHyperLink.Text = vAppendDegree.AppDFgnEDRecordUploadName;
                            AppDFgnEDRecordUploadFUName.Text = vAppendDegree.AppDFgnEDRecordUploadName;
                        }
                        AppDFgnEDRecordHyperLink.Visible = true;
                        AppDFgnEDRecordUploadFU.Visible = true;
                    }
                    else
                    {
                        AppDFgnEDRecordUploadFU.Visible = true;
                        AppDFgnEDRecordHyperLink.Visible = false;
                    }


                    if ("JPN".Equals(switchFgn))
                    {
                        switchFgn = "JPN";
                        AppDFgnJPAdmissionUploadCB.Checked = vAppendDegree.AppDFgnJPAdmissionUpload;

                        //A.入學許可註冊證
                        AppDFgnJPAdmissionUploadCB.Checked = vAppendDegree.AppDFgnJPAdmissionUpload;
                        if (vAppendDegree.AppDFgnJPAdmissionUploadName != null && !vAppendDegree.AppDFgnJPAdmissionUploadName.Equals(""))
                        {
                            if (vAppendDegree.AppDFgnJPAdmissionUploadName != null && checkName(vAppendDegree.AppDFgnJPAdmissionUploadName))
                            {
                                AppDFgnJPAdmissionHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPAdmissionUploadName);
                                AppDFgnJPAdmissionHyperLink.Text = vAppendDegree.AppDFgnJPAdmissionUploadName;
                                AppDFgnJPAdmissionUploadFUName.Text = vAppendDegree.AppDFgnJPAdmissionUploadName;
                            }
                            AppDFgnJPAdmissionHyperLink.Visible = true;
                            AppDFgnJPAdmissionUploadFU.Visible = true;
                        }
                        else
                        {
                            AppDFgnJPAdmissionUploadFU.Visible = true;
                            AppDFgnJPAdmissionHyperLink.Visible = false;
                        }

                        //B.修畢學分成績單
                        AppDFgnJPGradeUploadCB.Checked = vAppendDegree.AppDFgnJPGradeUpload;
                        if (vAppendDegree.AppDFgnJPGradeUploadName != null && !vAppendDegree.AppDFgnJPGradeUploadName.Equals(""))
                        {
                            if (vAppendDegree.AppDFgnJPGradeUploadName != null && checkName(vAppendDegree.AppDFgnJPGradeUploadName))
                            {
                                AppDFgnJPGradeHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPGradeUploadName);
                                AppDFgnJPGradeHyperLink.Text = vAppendDegree.AppDFgnJPGradeUploadName;
                                AppDFgnJPGradeUploadFUName.Text = vAppendDegree.AppDFgnJPGradeUploadName;
                            }
                            AppDFgnJPGradeHyperLink.Visible = true;
                            AppDFgnJPGradeUploadFU.Visible = true;
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
                            if (vAppendDegree.AppDFgnJPEnrollCAUploadName != null && checkName(vAppendDegree.AppDFgnJPEnrollCAUploadName))
                            {
                                AppDFgnJPEnrollCAHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPEnrollCAUploadName);
                                AppDFgnJPEnrollCAHyperLink.Text = vAppendDegree.AppDFgnJPEnrollCAUploadName;
                                AppDFgnJPEnrollCAUploadFUName.Text = vAppendDegree.AppDFgnJPEnrollCAUploadName;
                            }
                            AppDFgnJPEnrollCAHyperLink.Visible = true;
                            AppDFgnJPEnrollCAUploadFU.Visible = true;
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
                            if (vAppendDegree.AppDFgnJPDissertationPassUploadName != null && checkName(vAppendDegree.AppDFgnJPDissertationPassUploadName))
                            {
                                AppDFgnJPDissertationPassHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDFgnJPDissertationPassUploadName);
                                AppDFgnJPDissertationPassHyperLink.Text = vAppendDegree.AppDFgnJPDissertationPassUploadName;
                                AppDFgnJPDissertationPassUploadFUName.Text = vAppendDegree.AppDFgnJPDissertationPassUploadName;
                            }
                            AppDFgnJPDissertationPassHyperLink.Visible = true;
                            AppDFgnJPDissertationPassUploadFU.Visible = true;
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
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
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
                    vTeachEdu.EduStartYM = TeachEduStartYear.SelectedValue + TeachEduStartMonth.SelectedValue;
                    //vTeachEdu.EduEndYM = TeachEduEndYM.Text.ToString();
                    vTeachEdu.EduEndYM = TeachEduEndYear.SelectedValue + TeachEduEndMonth.SelectedValue;
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
                    //寫入後載入下方的DataGridView
                    GVTeachEdu.DataBind();
                    if (GVTeachEdu.Rows.Count == 0)
                        lb_NoTeacherEdu.Visible = true;
                    else
                        lb_NoTeacherEdu.Visible = false;
                }
            }
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
            vTeacherEdu.EduStartYM.PadLeft(5, '0');
            TeachEduStartYear.SelectedValue = vTeacherEdu.EduStartYM.Substring(0, 3);
            TeachEduStartMonth.SelectedValue = vTeacherEdu.EduStartYM.Substring(3, 2);
            //TeachEduEndYM.Text = vTeacherEdu.EduEndYM;
            vTeacherEdu.EduEndYM.PadLeft(5, '0');
            TeachEduEndYear.SelectedValue = vTeacherEdu.EduEndYM.Substring(0, 3);
            TeachEduEndMonth.SelectedValue = vTeacherEdu.EduEndYM.Substring(3, 2);
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
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
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
                vTeachEdu.EduDegreeType = TeachEduDegreeType.SelectedValue.ToString();
                //vTeachEdu.EduStartYM = TeachEduStartYM.Text.ToString();
                vTeachEdu.EduStartYM = TeachEduStartYear.SelectedValue + TeachEduStartMonth.SelectedValue;
                //vTeachEdu.EduEndYM = TeachEduEndYM.Text.ToString();
                vTeachEdu.EduEndYM = TeachEduEndYear.SelectedValue + TeachEduEndMonth.SelectedValue;
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
                lb_NoTeacherEdu.Visible = true;
            else
                lb_NoTeacherEdu.Visible = false;
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
                lb_NoTeacherEdu.Visible = true;
            else
                lb_NoTeacherEdu.Visible = false;
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

        //寫入 教師其他經歷資料 
        //寫入 教師經歷資料 
        protected void TeachExpSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (TeachExpOrginization.Text == "" || TeachExpUnit.Text == "" || TeachExpJobTitle.Text == "")
                {
                    if (TeachExpOrginization.Text == "")
                        msg += "機關名稱未填寫! ";
                    if (TeachExpUnit.Text == "")
                        msg += "單位名稱未填寫! ";
                    if (TeachExpJobTitle.Text == "")
                        msg += "職稱未填寫! ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachExp').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherExp vTeachExp = new VTeacherExp();
                    vTeachExp.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vTeachExp.ExpOrginization = TeachExpOrginization.Text.ToString();
                    vTeachExp.ExpUnit = TeachExpUnit.Text.ToString();
                    vTeachExp.ExpJobTitle = TeachExpJobTitle.Text.ToString();
                    vTeachExp.ExpJobType = TeachExpJobType.SelectedValue.ToString();
                    //vTeachExp.ExpStartYM = TeachExpStartYM.Text.ToString();
                    vTeachExp.ExpStartYM = TeachExpStartYear.SelectedValue + TeachExpStartMonth.SelectedValue;
                    //vTeachExp.ExpEndYM = TeachExpEndYM.Text.ToString();
                    vTeachExp.ExpEndYM = TeachExpEndYear.SelectedValue + TeachExpEndMonth.SelectedValue;
                    if (TeachExpUploadFU.HasFile)
                    {
                        TeachExpUploadCB.Checked = true;
                        fileLists.Add(TeachExpUploadFU);
                        vTeachExp.ExpUploadName = TeachExpUploadFU.FileName;
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

                        TeachExpUploadFU.PostedFile.SaveAs(Session["FolderPath"] + TeachExpUploadFU.FileName);
                        //vTeachExp.ExpUpload = TeachExpUploadCB.Checked;
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
                        if (GVTeachExp.Rows.Count == 0)
                            lb_NoTeachExp.Visible = true;
                        else
                            lb_NoTeachExp.Visible = false;
                        msg = "新增成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }
                    //TeachExpSave.Visible = true;
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
            vTeachExp.ExpStartYM.PadLeft(5, '0');
            TeachExpStartYear.SelectedValue = vTeachExp.ExpStartYM.Substring(0, 3);
            TeachExpStartMonth.SelectedValue = vTeachExp.ExpStartYM.Substring(3, 2);
            //TeachExpEndYM.Text = vTeachExp.ExpEndYM;
            vTeachExp.ExpEndYM.PadLeft(5, '0');
            TeachExpEndYear.SelectedValue = vTeachExp.ExpEndYM.Substring(0, 3);
            TeachExpEndMonth.SelectedValue = vTeachExp.ExpEndYM.Substring(3, 2);
            TeachExpUnit.Text = vTeachExp.ExpUnit;
            TeachExpJobTitle.Text = vTeachExp.ExpJobTitle;


            string location = Global.FileUpPath + vEmp.EmpSn + "/";
            if (vTeachExp.ExpUploadName != null && !vTeachExp.ExpUploadName.Equals(""))
            {
                TeachExpUploadCB.Checked = true;
                if (vTeachExp.ExpUploadName != null && checkName(vTeachExp.ExpUploadName))
                {
                    TeachExpHyperLink.NavigateUrl = location + vTeachExp.ExpUpload;
                    TeachExpHyperLink.Text = vTeachExp.ExpUploadName;
                    TeachExpUploadFUName.Text = vTeachExp.ExpUploadName;
                }
                TeachExpHyperLink.Visible = true;
                TeachExpUploadFU.Visible = true;
            }
            else
            {
                TeachExpUploadCB.Checked = false;
                TeachExpUploadFU.Visible = true;
                TeachExpHyperLink.Visible = false;
            }

            //應徵專兼任別--職別
            if (vTeachExp.ExpJobType == null || vTeachExp.ExpJobType.Equals(""))
            {
                TeachExpJobType.Items.Insert(0, "請選擇");
            }
            else
            {
                for (int i = 0; i < TeachExpJobType.Items.Count; i++)
                {
                    if (vTeachExp.ExpJobType.Equals(TeachExpJobType.Items[i].Value))
                    {
                        TeachExpJobType.Items[i].Selected = true;
                    }
                    else
                    {
                        TeachExpJobType.Items[i].Selected = false;
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachExp').modal('show'); });</script>", false);
        }

        //更新 教師經歷資料
        protected void TeachExpUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (TeachExpJobType.SelectedValue != "請選擇")
                {
                    VTeacherExp vTeachExp = new VTeacherExp();
                    vTeachExp.ExpSn = Convert.ToInt32(TBIntExpSn.Text.ToString());
                    vTeachExp.ExpOrginization = TeachExpOrginization.Text.ToString();
                    //vTeachExp.ExpStartYM = TeachExpStartYM.Text.ToString();
                    vTeachExp.ExpStartYM = TeachExpStartYear.SelectedValue + TeachExpStartMonth.SelectedValue;
                    //vTeachExp.ExpEndYM = TeachExpEndYM.Text.ToString();
                    vTeachExp.ExpEndYM = TeachExpEndYear.SelectedValue + TeachExpEndMonth.SelectedValue;
                    vTeachExp.ExpUnit = TeachExpUnit.Text.ToString();
                    vTeachExp.ExpJobTitle = TeachExpJobTitle.Text.ToString();
                    vTeachExp.ExpJobType = TeachExpJobType.SelectedValue.ToString();
                    if (TeachExpUploadFU.HasFile)
                    {
                        TeachExpUploadCB.Checked = true;
                        if (TeachExpUploadFU.FileName != null && checkName(TeachExpUploadFU.FileName))
                        {
                            fileLists.Add(TeachExpUploadFU);
                            vTeachExp.ExpUploadName = TeachExpUploadFU.FileName;
                            ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

                            TeachExpUploadFU.PostedFile.SaveAs(Session["FolderPath"] + TeachExpUploadFU.FileName);
                        }
                        //vTeachExp.ExpUpload = TeachExpUploadCB.Checked;
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
                        if (GVTeachExp.Rows.Count == 0)
                            lb_NoTeachExp.Visible = true;
                        else
                            lb_NoTeachExp.Visible = false;
                        msg = "更新成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                    }
                }
                else
                {
                    msg = "請選填職別!";
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void TeachExpCancel_Click(object sender, EventArgs e)
        {
            //TeachExpSave.Visible = true;
            TeachExpUpdate.Visible = false;
            TeachExpCancel.Visible = false;
            TeachExpOrginization.Text = "";
            //TeachExpStartYM.Text = "";
            //TeachExpEndYM.Text = "";
            TeachExpUnit.Text = "";
            TeachExpJobTitle.Text = "";
            TeachExpUploadCB.Checked = false;
            TeachExpUploadFUName.Text = "";
            TeachExpHyperLink.Text = "";
        }

        //寫入 授課資料 
        protected void TeachLessonSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                if (TeachLessonDepLevel.Text == "" || TeachLessonClass.Text == "" || TeachLessonHours.Text == "" || TeachLessonCreditHours.Text == "" || TeachLessonHours.Text.Length > 6 || TeachLessonCreditHours.Text.Length > 6)
                {
                    if (TeachLessonDepLevel.Text == "")
                        msg += "系所級別未填寫! ";
                    if (TeachLessonClass.Text == "")
                        msg += "課目未填寫! ";
                    if (TeachLessonHours.Text == "")
                        msg += "授課時數未填寫! ";
                    if (TeachLessonCreditHours.Text == "")
                        msg += "學分數未填寫! ";
                    if (TeachLessonHours.Text.Length > 6)
                        msg += "授課時數含小數點請勿超過6位數! ";
                    if (TeachLessonCreditHours.Text.Length > 6)
                        msg += "學分數含小數點請勿超過6位數! ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachLesson').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherTmuLesson vTeacherTmuLesson = new VTeacherTmuLesson();
                    vTeacherTmuLesson.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    getSettings.Execute();
                    //vTeacherTmuLesson.LessonYear = TeachLessonYear.Text.ToString();
                    vTeacherTmuLesson.LessonYear = ddl_TeachLessonYear.SelectedValue;
                    //vTeacherTmuLesson.LessonSemester = TeachLessonSemester.Text.ToString(); //1:上學期 2:下學期
                    vTeacherTmuLesson.LessonSemester = ddl_TeachLessonSemester.SelectedValue;
                    vTeacherTmuLesson.LessonDeptLevel = TeachLessonDepLevel.Text.ToString();
                    vTeacherTmuLesson.LessonClass = TeachLessonClass.Text.ToString();
                    vTeacherTmuLesson.LessonHours = TeachLessonHours.Text.ToString();
                    vTeacherTmuLesson.LessonCreditHours = TeachLessonCreditHours.Text.ToString();

                    CRUDObject crudObject = new CRUDObject();
                    if (crudObject.Insert(vTeacherTmuLesson))
                    {
                        //寫入後載入下方的DataGridView
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                        GVTeachLesson.DataBind();

                        if (GVTeachLesson.Rows.Count == 0)
                            lb_NoTeachLesson.Visible = true;
                        else
                            lb_NoTeachLesson.Visible = false;
                        msg = "新增成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }
                }
            }
            TeachLessonSave.Visible = true;
            TeachLessonUpdate.Visible = false;
            TeachExpUploadCB.Checked = false;
            TeachLessonClass.Text = "";
            TeachLessonHours.Text = "";
            TeachLessonCreditHours.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        //載入修改資料 教師授課資料
        protected void TeachLessonModData_Click(object sender, EventArgs e)
        {
            TeachLessonSave.Visible = false;
            TeachLessonUpdate.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            Label lblLessonSn = (Label)row.FindControl("LessonSn");
            int intLessonSn = Convert.ToInt32(lblLessonSn.Text.ToString()); // here we are
            TbLessonSn.Text = intLessonSn.ToString();
            VTeacherTmuLesson vTeacherTmuLesson = crudObject.GetTeacherTmuLesson(intLessonSn);

            //TeachLessonYear.Text = vTeacherTmuLesson.LessonYear;
            ddl_TeachLessonYear.SelectedValue = vTeacherTmuLesson.LessonYear;
            //TeachLessonSemester.Text = vTeacherTmuLesson.LessonSemester;
            ddl_TeachLessonSemester.SelectedValue = vTeacherTmuLesson.LessonSemester;
            TeachLessonDepLevel.Text = vTeacherTmuLesson.LessonDeptLevel;
            TeachLessonClass.Text = vTeacherTmuLesson.LessonClass;
            TeachLessonHours.Text = vTeacherTmuLesson.LessonHours;
            TeachLessonCreditHours.Text = vTeacherTmuLesson.LessonCreditHours;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachLesson').modal('show'); });</script>", false);
        }

        //更新修改資料 教師授課資料
        protected void TeachLessonUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {
                CRUDObject crudObject = new CRUDObject();
                VTeacherTmuLesson vTeacherTmuLesson = crudObject.GetTeacherTmuLesson(Int32.Parse(TbLessonSn.Text.ToString()));

                vTeacherTmuLesson.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                //vTeacherTmuLesson.LessonYear = TeachLessonYear.Text.ToString();
                vTeacherTmuLesson.LessonYear = ddl_TeachLessonYear.SelectedValue;
                //vTeacherTmuLesson.LessonSemester = TeachLessonSemester.Text.ToString();
                vTeacherTmuLesson.LessonSemester = ddl_TeachLessonSemester.SelectedValue;
                vTeacherTmuLesson.LessonDeptLevel = TeachLessonDepLevel.Text.ToString();
                vTeacherTmuLesson.LessonClass = TeachLessonClass.Text.ToString();
                vTeacherTmuLesson.LessonHours = TeachLessonHours.Text.ToString();

                vTeacherTmuLesson.LessonCreditHours = TeachLessonCreditHours.Text.ToString();

                vTeacherTmuLesson.LessonUserId = "";

                if (crudObject.Update(vTeacherTmuLesson))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                    //寫入後載入下方的DataGridView
                    GVTeachExp.DataBind();
                    if (GVTeachExp.Rows.Count == 0)
                        lb_NoTeachExp.Visible = true;
                    else
                        lb_NoTeachExp.Visible = false;
                    msg = "更新成功!!";
                }
                else
                {
                    msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                }
            }
            TeachLessonSave.Visible = true;
            TeachLessonUpdate.Visible = false;
            TeachLessonCancel.Visible = false;
            //TeachLessonYear.Text = "";
            //TeachLessonSemester.Text = "";
            TeachLessonDepLevel.Text = "";
            TeachLessonClass.Text = "";
            TeachLessonHours.Text = "";
            TeachLessonCreditHours.Text = "";
            TeachExpUploadCB.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void TeachLessonCancel_Click(object sender, EventArgs e)
        {
            TeachLessonSave.Visible = true;
            TeachLessonUpdate.Visible = false;
            TeachLessonCancel.Visible = false;
            //TeachLessonSemester.Text = "";
            TeachLessonDepLevel.Text = "";
            TeachLessonDepLevel.Text = "";
            TeachLessonClass.Text = "";
            TeachLessonHours.Text = "";

        }

        protected void ThesisCoopCreate_Click(object sender, EventArgs e)
        {
            string msg = "";
            CRUDObject crudObject = new CRUDObject();
            int insertSnNo = 1; //插入序號
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
                EmpBaseTempSave_Click(sender, e);
            }
            else
            {

                if (txtProjectContent.Text.Length > 1000 )
                {
                    //if (txtProjectContent.Text.Length > 1000)
                    msg = "計畫名稱、計畫執行期間、產學合作實收金額，請在1000字數已內!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisCoop').modal('show'); });</script>", false);
                }
                else
                {
                    try
                    {
                        LabelTotalThesisScore.Text = calculate().ToString();
                        double d = Double.Parse(LabelTotalThesisScore.Text.ToString());
                        VThesisScore vThesisScore = new VThesisScore();
                        vThesisScore.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                        DESCrypt DES = new DESCrypt();
                        if (Request["AppSn"] != null)
                            Session["AppSn"] = DES.Decrypt(Request["AppSn"].ToString());
                        vThesisScore.AppSn = Int32.Parse(Session["AppSn"].ToString());
                        //取得要插入的序號
                        //insertSnNo = crudObject.GetThesisScoreInsertSnNo(vThesisScore.EmpSn.ToString(), AppThesisPublishYearMonth.Text.ToString());
                        insertSnNo = crudObject.GetThesisScoreInsertSnNo(vThesisScore.EmpSn.ToString(), ddl_AppThesisPublishYear.SelectedValue + ddl_AppThesisPublishMonth.SelectedValue);
                        vThesisScore.SnNo = insertSnNo;
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
                                fileLists.Add(AppThesisUploadFU);
                                vThesisScore.ThesisUploadName = AppThesisUploadFU.FileName; //第一次新增時要改檔名
                                vThesisScore.ThesisName = AppThesisName.Text.ToString();
                            }
                        }
                        else
                        {
                            vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString();
                            vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName;
                            vThesisScore.ThesisName = AppThesisName.Text.ToString();
                        }
                        //期刊引用排名
                        if (!AppThesisJournalRefCount.Text.ToString().Equals(""))
                        {
                            vThesisScore.ThesisJournalRefCount = AppThesisJournalRefCount.Text.ToString();
                        }
                        //請檢附資料庫查詢畫面，無SCI分數者免附。
                        if (AppThesisJournalRefUploadFU.HasFile)
                        {
                            AppThesisJournalRefUploadCB.Checked = true;
                            if (AppThesisJournalRefUploadFU.FileName != null && checkName(AppThesisJournalRefUploadFU.FileName))
                            {
                                fileLists.Add(AppThesisJournalRefUploadFU);
                                vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFU.FileName;
                                ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                AppThesisJournalRefUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisJournalRefUploadFU.FileName);
                            }

                        }
                        else
                        {
                            vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFUName.Text.ToString();
                        }
                        if (RepresentCB.Checked)
                        {
                            vThesisScore.IsRepresentative = RepresentCB.Checked;
                            //合著人寫入上傳
                            if (ThesisCoAuthorUploadFU.HasFile)
                            {
                                if (ThesisCoAuthorUploadFU.FileName != null && checkName(ThesisCoAuthorUploadFU.FileName))
                                {
                                    ThesisCoAuthorUploadCB.Checked = true;
                                    fileLists.Add(ThesisCoAuthorUploadFU);
                                    vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFU.FileName;
                                    //vApplyAudit.AppCoAuthorUpload = AppPCoAuthorUploadCB.Checked;
                                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                    ThesisCoAuthorUploadFU.PostedFile.SaveAs(Session["FolderPath"] + ThesisCoAuthorUploadFU.FileName);
                                }
                            }
                            else
                            {
                                vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFUName.Text.ToString();
                                //vAppendPublication.AppPCoAuthorUpload = AppPCoAuthorUploadCB.Checked;
                            }
                            vThesisScore.ThesisSummaryCN = ThesisSummaryCN.Text.ToString();
                        }
                        //勾選RPI要計分的
                        if (CountRPICB.Checked)
                        {
                            countRPI = crudObject.GetThesisCountRPI(vThesisScore.EmpSn);
                            if (countRPI < Convert.ToInt32(AppResearchYear.SelectedValue.ToString()))
                            {
                                vThesisScore.IsCountRPI = CountRPICB.Checked;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('計算RPI，超出按『研究年資』可選篇數！');", true);
                            }
                        }
                        //插入SnNo之後的序號要往後推 新增資料放序號1
                        DataTable dt = crudObject.GetVThesisScoreAfterInsert(Convert.ToInt32(Session["EmpSn"].ToString())); //, vThesisScore.ThesisPublishYearMonth
                        if (crudObject.Insert(vThesisScore))
                        {
                            int ThesisSn = crudObject.GetVThesisScoreSn();
                            int ThesisCnt = crudObject.GetVThesisScoreTotalCount(vThesisScore.EmpSn, vThesisScore.AppSn);
                            //檔名修改
                            string orgThesisUploadName = vThesisScore.ThesisUploadName;
                            vThesisScore.ThesisSn = ThesisSn;
                            if (vThesisScore.ThesisUploadName != null && !vThesisScore.ThesisUploadName.Equals(""))
                            {
                                if (checkName(AppThesisUploadFU.FileName))
                                {
                                    vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName.Substring(0, vThesisScore.ThesisUploadName.IndexOf(".pdf")) + "_" + vEmp.EmpNameCN + "_" + vThesisScore.SnNo + ".pdf";
                                    crudObject.Update(vThesisScore);
                                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                    AppThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisUploadFU.FileName);

                                }

                            }

                            try
                            {
                                msg = "論文積分檔新增成功!!";
                                int h = 0;
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    h = insertSnNo + k + 1;
                                    crudObject.UpdateThesisScoreSn(Convert.ToInt32(dt.Rows[k][0].ToString()), h);
                                }

                                DSThesisScore.DataBind();

                            }
                            catch (Exception ex)
                            {
                                //log   ex.ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('論文積分新增失敗!');", true);

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
                    AppThesisJournalRefHyperLink.Visible = false;
                    ThesisCoAuthorHyperLink.Visible = false;
                    AppThesisUploadFUName.Text = "";
                    AppThesisHyperLink.Visible = false;
                    RepresentCB.Checked = false;
                    CountRPICB.Checked = false;
                    ThesisCoAuthorUploadCB.Checked = false;
                    //TBThesisScoreInsert.Visible = false;
                }
            }
            if (!String.IsNullOrEmpty(msg))
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ThesisCoopUpdate_Click(object sender, EventArgs e)
        { 
        }


        //寫入 教師上傳論文積分表 
        protected void ThesisScoreSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            CRUDObject crudObject = new CRUDObject();
            int insertSnNo = 1; //插入序號
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
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
                        LabelTotalThesisScore.Text = calculate().ToString();
                        double d = Double.Parse(LabelTotalThesisScore.Text.ToString());
                        VThesisScore vThesisScore = new VThesisScore();
                        vThesisScore.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                        DESCrypt DES = new DESCrypt();
                        if (Request["AppSn"] != null)
                            Session["AppSn"] = DES.Decrypt(Request["AppSn"].ToString());
                        vThesisScore.AppSn = Int32.Parse(Session["AppSn"].ToString());
                        //取得要插入的序號
                        //insertSnNo = crudObject.GetThesisScoreInsertSnNo(vThesisScore.EmpSn.ToString(), AppThesisPublishYearMonth.Text.ToString());
                        insertSnNo = crudObject.GetThesisScoreInsertSnNo(vThesisScore.EmpSn.ToString(), ddl_AppThesisPublishYear.SelectedValue + ddl_AppThesisPublishMonth.SelectedValue);
                        vThesisScore.SnNo = insertSnNo;
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
                                fileLists.Add(AppThesisUploadFU);
                                vThesisScore.ThesisUploadName = AppThesisUploadFU.FileName; //第一次新增時要改檔名
                                vThesisScore.ThesisName = AppThesisName.Text.ToString();
                            }
                        }
                        else
                        {
                            vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString();
                            vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName;
                            vThesisScore.ThesisName = AppThesisName.Text.ToString();
                        }
                        //期刊引用排名
                        if (!AppThesisJournalRefCount.Text.ToString().Equals(""))
                        {
                            vThesisScore.ThesisJournalRefCount = AppThesisJournalRefCount.Text.ToString();
                        }
                        //請檢附資料庫查詢畫面，無SCI分數者免附。
                        if (AppThesisJournalRefUploadFU.HasFile)
                        {
                            AppThesisJournalRefUploadCB.Checked = true;
                            if (AppThesisJournalRefUploadFU.FileName != null && checkName(AppThesisJournalRefUploadFU.FileName))
                            {
                                fileLists.Add(AppThesisJournalRefUploadFU);
                                vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFU.FileName;
                                ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                AppThesisJournalRefUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisJournalRefUploadFU.FileName);
                            }

                        }
                        else
                        {
                            vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFUName.Text.ToString();
                        }
                        if (RepresentCB.Checked)
                        {
                            vThesisScore.IsRepresentative = RepresentCB.Checked;
                            //合著人寫入上傳
                            if (ThesisCoAuthorUploadFU.HasFile)
                            {
                                if (ThesisCoAuthorUploadFU.FileName != null && checkName(ThesisCoAuthorUploadFU.FileName))
                                {
                                    ThesisCoAuthorUploadCB.Checked = true;
                                    fileLists.Add(ThesisCoAuthorUploadFU);
                                    vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFU.FileName;
                                    //vApplyAudit.AppCoAuthorUpload = AppPCoAuthorUploadCB.Checked;
                                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                    ThesisCoAuthorUploadFU.PostedFile.SaveAs(Session["FolderPath"] + ThesisCoAuthorUploadFU.FileName);
                                }
                            }
                            else
                            {
                                vThesisScore.ThesisCoAuthorUploadName = ThesisCoAuthorUploadFUName.Text.ToString();
                                //vAppendPublication.AppPCoAuthorUpload = AppPCoAuthorUploadCB.Checked;
                            }
                            vThesisScore.ThesisSummaryCN = ThesisSummaryCN.Text.ToString();
                        }
                        //勾選RPI要計分的
                        if (CountRPICB.Checked)
                        {
                            countRPI = crudObject.GetThesisCountRPI(vThesisScore.EmpSn);
                            if (countRPI < Convert.ToInt32(AppResearchYear.SelectedValue.ToString()))
                            {
                                vThesisScore.IsCountRPI = CountRPICB.Checked;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('計算RPI，超出按『研究年資』可選篇數！');", true);
                            }
                        }
                        //插入SnNo之後的序號要往後推 新增資料放序號1
                        DataTable dt = crudObject.GetVThesisScoreAfterInsert(Convert.ToInt32(Session["EmpSn"].ToString())); //, vThesisScore.ThesisPublishYearMonth
                        if (crudObject.Insert(vThesisScore))
                        {
                            int ThesisSn = crudObject.GetVThesisScoreSn();
                            int ThesisCnt = crudObject.GetVThesisScoreTotalCount(vThesisScore.EmpSn, vThesisScore.AppSn);
                            //檔名修改
                            string orgThesisUploadName = vThesisScore.ThesisUploadName;
                            vThesisScore.ThesisSn = ThesisSn;
                            if (vThesisScore.ThesisUploadName != null && !vThesisScore.ThesisUploadName.Equals(""))
                            {
                                if (checkName(AppThesisUploadFU.FileName))
                                {
                                    vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName.Substring(0, vThesisScore.ThesisUploadName.IndexOf(".pdf")) + "_" + vEmp.EmpNameCN + "_" + vThesisScore.SnNo + ".pdf";
                                    crudObject.Update(vThesisScore);
                                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                                    AppThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisUploadFU.FileName);

                                }
                            
                            }

                            try
                            {
                                msg = "論文積分檔新增成功!!";
                                int h = 0;
                                for (int k = 0; k < dt.Rows.Count; k++)
                                {
                                    h = insertSnNo + k + 1;
                                    crudObject.UpdateThesisScoreSn(Convert.ToInt32(dt.Rows[k][0].ToString()), h);
                                }

                                DSThesisScore.DataBind();

                            }
                            catch (Exception ex)
                            {
                                //log   ex.ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('論文積分新增失敗!');", true);

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
                    AppThesisJournalRefHyperLink.Visible = false;
                    ThesisCoAuthorHyperLink.Visible = false;
                    AppThesisUploadFUName.Text = "";
                    AppThesisHyperLink.Visible = false;
                    RepresentCB.Checked = false;
                    CountRPICB.Checked = false;
                    ThesisCoAuthorUploadCB.Checked = false;
                    //TBThesisScoreInsert.Visible = false;
                }
            }
            if (msg != "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
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
                TBSnNo.Text = ((Label)row.FindControl("lblSnNo")).Text.ToString(); //論文序號
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
            //TBThesisScoreInsert.Visible = true;
            //ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "Javascript", "<script>$('AppThesisResearchResult').mooEditable();</script>", true);
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "Javascript", "<script>$('AppThesisResearchResult').mooEditable();</script>", true);
            Session["ThesisAccuScore"] = "0";
            Session["ThesisRPIScore"] = "0";
            ThesisScoreSave.Visible = false;
            ThesisScoreUpdate.Visible = true;
            var modLink = (Control)sender;
            GridViewRow row = (GridViewRow)modLink.NamingContainer;
            TBIntThesisSn.Text = ((Label)row.FindControl("ThesisSn")).Text.ToString();
            TBSnNo.Text = ((Label)row.FindControl("lblSnNo")).Text.ToString(); //論文序號
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
            ddl_AppThesisPublishYear.SelectedValue = vThesisScore.ThesisPublishYearMonth.Substring(0, 3);
            ddl_AppThesisPublishMonth.SelectedValue = vThesisScore.ThesisPublishYearMonth.Substring(3, 2);
            AppThesisC.Text = vThesisScore.ThesisC;
            AppThesisJ.Text = vThesisScore.ThesisJ;
            AppThesisA.Text = vThesisScore.ThesisA;
            LabelTotalThesisScore.Text = vThesisScore.ThesisTotal;
            AppResearchYear.SelectedValue = vApplyAudit.AppResearchYear;
            AppThesisJournalRefCount.Text = vThesisScore.ThesisJournalRefCount.ToString();
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


            //上傳期刊引用論文 
            if (vThesisScore.ThesisJournalRefUploadName != null && !vThesisScore.ThesisJournalRefUploadName.Equals(""))
            {
                if (vThesisScore.ThesisJournalRefUploadName != null && checkName(vThesisScore.ThesisJournalRefUploadName))
                {
                    AppThesisJournalRefUploadCB.Checked = true;
                    AppThesisJournalRefHyperLink.NavigateUrl = getHyperLink(vThesisScore.ThesisJournalRefUploadName);
                    AppThesisJournalRefHyperLink.Text = vThesisScore.ThesisJournalRefUploadName;
                    AppThesisJournalRefUploadFUName.Text = vThesisScore.ThesisJournalRefUploadName;
                }
                AppThesisJournalRefHyperLink.Visible = true;
                AppThesisJournalRefUploadFU.Visible = true;
                AppThesisJournalRefUploadFUName.Visible = false;
            }
            else
            {
                AppThesisJournalRefUploadCB.Checked = false;
                AppThesisJournalRefHyperLink.Text = "";
                AppThesisJournalRefUploadFUName.Text = "";
                AppThesisJournalRefUploadFU.Visible = true;
                AppThesisJournalRefUploadFUName.Visible = false;
            }

            //上傳論文
            if (vThesisScore.ThesisUploadName != null && !vThesisScore.ThesisUploadName.Equals(""))
            {
                AppThesisUploadCB.Checked = true;
                if (vThesisScore.ThesisUploadName != null && checkName(vThesisScore.ThesisUploadName))
                {
                    AppThesisHyperLink.NavigateUrl = getHyperLink(vThesisScore.ThesisUploadName);
                    AppThesisHyperLink.Text = vThesisScore.ThesisUploadName;
                }
                AppThesisHyperLink.Visible = true;
                AppThesisUploadFU.Visible = true;
                AppThesisUploadFUName.Visible = false;
                AppThesisUploadFUName.Text = vThesisScore.ThesisUploadName;
            }
            else
            {
                AppThesisUploadFU.Visible = true;
                AppThesisUploadFUName.Visible = false;
                AppThesisUploadCB.Checked = false;
                AppThesisHyperLink.Text = "";
                AppThesisHyperLink.Visible = false;
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
                    if (vThesisScore.ThesisCoAuthorUploadName != null && checkName(vThesisScore.ThesisCoAuthorUploadName))
                    {
                        ThesisCoAuthorUploadCB.Checked = true;
                        ThesisCoAuthorHyperLink.NavigateUrl = getHyperLink(vThesisScore.ThesisCoAuthorUploadName);
                        ThesisCoAuthorHyperLink.Text = vThesisScore.ThesisCoAuthorUploadName;
                    }
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
            DESCrypt DES = new DESCrypt();
            string strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath;
            bool insertflag = false;

            if (Request.QueryString["AppSn"] != null)
                Session["AppSn"] = DES.Decrypt(Request.QueryString["AppSn"]);
            if (Session["EmpIdno"] == null)
            {
                msg = "抱歉，系統時間逾時，請重新登入!! ";
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
                    LabelTotalThesisScore.Text = calculate().ToString();
                    double d = Double.Parse(LabelTotalThesisScore.Text.ToString());
                    CRUDObject crudObject = new CRUDObject();
                    VThesisScore vThesisScore = new VThesisScore();
                    //vThesisScore.ResearchYear = ResearchYear.Text.ToString(); 
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
                    vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString(); //bug 須修復 抓不到資料
                    vThesisScore.AppSn = Int32.Parse(Session["AppSn"].ToString());
                    if (!AppThesisJournalRefCount.Text.ToString().Equals(""))
                    {
                        vThesisScore.ThesisJournalRefCount = AppThesisJournalRefCount.Text.ToString();
                    }
                    vThesisScore.ThesisModifyDate = DateTime.Now;
                    string orgThesisUploadName = "";
                    if (AppThesisUploadFU.HasFile)
                    {
                        if (AppThesisUploadFU.FileName != null && checkName(AppThesisUploadFU.FileName))
                        {
                            AppThesisUploadCB.Checked = true;
                            fileLists.Add(AppThesisUploadFU);
                            ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                            vThesisScore.ThesisUploadName = AppThesisUploadFU.FileName;
                            orgThesisUploadName = vThesisScore.ThesisUploadName;
                            if (checkName(AppThesisUploadFU.FileName) && Session["FolderPath"] != null)
                                AppThesisUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisUploadFU.FileName);
                            else
                                Response.Write("<script>alert('成果檔案上傳失敗');</script>");

                            //檔名修改
                            if (vThesisScore.ThesisUploadName.IndexOf("_" + vEmp.EmpNameCN) > 0)
                                vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName.Substring(0, vThesisScore.ThesisUploadName.IndexOf(".pdf")) + "_" + vEmp.EmpNameCN + "_" + vThesisScore.SnNo + ".pdf";
                            #region 原找不到上傳ProcessUploadFiles，所增加的SaveAs
                            //string fromFile = Session["FolderPath"].ToString() + orgThesisUploadName;
                            ////string toFile = location + vThesisScore.ThesisUploadName;
                            //string[] str = { "／", "？", "＊", "［", " ］", "'", "&", "?" };
                            //bool exists = false;

                            //try
                            //{
                            //    //File.Move(fromFile, toFile);
                            //    //

                            //    FileInfo f = new FileInfo(fromFile);
                            //    if (f.Length == 0)
                            //    {
                            //        //.......
                            //        Response.Write("<script>alert('上傳檔案失敗!請重新上傳');</script>");
                            //    }
                            //    for (int i = 0; i < str.Length; i++)
                            //        exists = f.Name.Contains(str[i]);
                            //    if (exists)
                            //    {
                            //        Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                            //        vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString();
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    Response.Write("<script>alert('上傳檔案失敗!請重新上傳');</script>");
                            //    //Log....
                            //    throw ex;
                            //} 
                            #endregion}
                        }
                    }
                    else
                    {
                        //Response.Write("<script>alert('檔案更新失敗!請重新上傳');</script>");
                        vThesisScore.ThesisUploadName = AppThesisUploadFUName.Text.ToString();
                        //檔名修改
                        //vThesisScore.ThesisUploadName = vThesisScore.ThesisUploadName;

                    }

                    //請檢附資料庫查詢畫面，無SCI分數者免附。
                    if (AppThesisJournalRefUploadFU.HasFile)
                    {
                        if (AppThesisJournalRefUploadFU.FileName != null && checkName(AppThesisJournalRefUploadFU.FileName))
                        {
                            AppThesisJournalRefUploadCB.Checked = true;
                            fileLists.Add(AppThesisJournalRefUploadFU);
                            vThesisScore.ThesisJournalRefUploadName = AppThesisJournalRefUploadFU.FileName;
                            ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));
                            AppThesisJournalRefUploadFU.PostedFile.SaveAs(Session["FolderPath"] + AppThesisJournalRefUploadFU.FileName);
                        }
                        else
                            Response.Write("<script>alert('IF/排名上傳失敗!請重新上傳');</script>");

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
                                fileLists.Add(ThesisCoAuthorUploadFU);
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
                    //勾選RPI要計分的
                    if (CountRPICB.Checked)
                    {
                        countRPI = crudObject.GetThesisCountRPI(Int32.Parse(Session["EmpSn"].ToString()));
                        if (countRPI < Convert.ToInt32(AppResearchYear.SelectedValue.ToString()))
                        {
                            vThesisScore.IsCountRPI = CountRPICB.Checked;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('計算RPI，超出按『研究年資』可選篇數！');", true);
                        }
                    }
                    if (crudObject.Update(vThesisScore))
                    {

                        //寫入延伸檔 ApplyAudit AppPublicationUpload AppPublicationUploadName
                        //vApplyAudit.AppPublicationUpload = true;
                        //vApplyAudit.AppPublicationUploadName = AppThesisUploadFU.FileName;
                        //寫入後載入下方的DataGridView
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));


                        msg = "更新成功!!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('資料更新成功!');", true);
                    }
                    else
                    {
                        msg = "抱歉，資料更新失敗，請洽資訊人員! ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('資料更新失敗!');", true);
                    }
                    vApplyAudit.AppResearchYear = AppResearchYear.SelectedValue;

                    if (GVThesisScore.Rows.Count > 0)
                        vApplyAudit.AppThesisAccuScore = AppPThesisAccuScore.Text.ToString();
                    else
                        vApplyAudit.AppThesisAccuScore = "0";
                    vApplyAudit.AppRPIScore = AppRPI.Text.ToString();
                    if (!crudObject.Update(vApplyAudit))
                    {
                        MessageLabel.Text += "2.更新申請檔失敗，請洽資訊人員!";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('資料更新失敗!');", true);
                    }
                }
                ThesisScoreSave.Visible = true;
                ThesisScoreUpdate.Visible = false;
                ThesisScoreCancel.Visible = true;
                //AppThesisPublishYearMonth.Text = "";
                RRNo.DataBind();
                AppThesisResearchResult.Text = "";
                AppThesisC.Text = "1";
                AppThesisJ.Text = "1";
                AppThesisA.Text = "1";
                AppThesisName.Text = "";
                LabelTotalThesisScore.Text = "";
                AppThesisJournalRefCount.Text = "";
                AppThesisUploadCB.Checked = false;
                AppThesisJournalRefUploadCB.Checked = false;
                AppThesisUploadFUName.Text = "";
                AppThesisJournalRefHyperLink.Visible = false;
                ThesisCoAuthorHyperLink.Visible = false;
                AppThesisHyperLink.Visible = false;
                RepresentCB.Checked = false;
                CountRPICB.Checked = false;
                ThesisCoAuthorUploadCB.Checked = false;
                //TBThesisScoreInsert.Visible = false;
            }

            if (msg != "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
            }
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
            AppThesisHyperLink.Visible = false;
            RepresentCB.Checked = false;
        }

        protected void AppRPIDiscountSave_Click(object sender, EventArgs e)
        {
            DESCrypt DES = new DESCrypt();
            //Update ApplyAudit

            if (Request["HRAppSn"] != null && !Request["HRAppSn"].ToString().Equals(""))
                vApplyAudit = crudObject.GetApplyAuditObjByHRAppSnPromote(Session["EmpIdno"].ToString(), Request["HRAppSn"].ToString());
            else if (Request["AppSn"] != null && !Request["AppSn"].ToString().Equals(""))
                vApplyAudit = crudObject.GetApplyAuditObjByAppSnPromote(Session["EmpIdno"].ToString(), DES.Decrypt(Request["AppSn"].ToString()));
            else
                vApplyAudit = crudObject.GetApplyAuditObjByIdnoPromote(Session["EmpIdno"].ToString());
            if (AppRPI.Text.Length > 10)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('RPI研究表現指數含小數點，請填10位數以內!');", true);
            }
            else
            {
                vApplyAudit.AppRPIScore = AppRPI.Text.ToString();
                vApplyAudit.AppResearchYear = AppResearchYear.SelectedValue;
                if (GVThesisScore.Rows.Count > 0)
                    vApplyAudit.AppThesisAccuScore = AppPThesisAccuScore.Text.ToString();
                else
                    vApplyAudit.AppThesisAccuScore = "0";
                string strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath + vApplyAudit.EmpSn + "\\";
                string[] str = { "／", "？", "＊", "［", " ］", "'", "&", "?" };
                bool exists = false;

                if (crudObject.UpdatePartial(vApplyAudit))
                {
                    VAppendPromote vAppendPromote = new VAppendPromote();
                    vAppendPromote = crudObject.GetAppendPromoteObj(vApplyAudit.AppSn);
                    vAppendPromote.RPIDiscountNo = false;
                    if (RPIDiscountNo.Checked)
                    {
                        vAppendPromote.RPIDiscountNo = true;
                        vAppendPromote.RPIDiscountScore1 = "0";
                        vAppendPromote.RPIDiscountScore1Name = "";
                        vAppendPromote.RPIDiscountScore2 = "0";
                        vAppendPromote.RPIDiscountScore2Name = "";
                        vAppendPromote.RPIDiscountScore3 = "0";
                        vAppendPromote.RPIDiscountScore3Name = "";
                        vAppendPromote.RPIDiscountScore4 = "0";
                        vAppendPromote.RPIDiscountScore4Name = "";
                        vAppendPromote.RPIDiscountTotal = "0";
                        RPIDiscountTotal.Text = "0";
                        RPIDiscountTotal1.Text = "0";
                        AppRPITotalScore.Text = "" + Convert.ToDecimal(AppPThesisAccuScore1.Text.ToString());
                    }
                    else
                    {
                        vAppendPromote.RPIDiscountNo = false;
                        if (RPIDiscountScore1.Checked)
                        {
                            vAppendPromote.RPIDiscountScore1 = "60";
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore1 = "0";
                        }
                        if (RPIDiscountScore1UploadFU.HasFile)
                        {
                            try
                            {
                                for (int i = 0; i < str.Length; i++)
                                {
                                    exists = RPIDiscountScore1UploadFU.FileName.Contains(str[i]);
                                    if (exists)
                                    {
                                        break;
                                    }
                                }
                                if (exists)
                                {
                                    Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                                }
                                else
                                {
                                    RPIDiscountScore1UploadCB.Checked = true;
                                    fileLists.Add(RPIDiscountScore1UploadFU);
                                    vAppendPromote.RPIDiscountScore1Name = RPIDiscountScore1UploadFU.FileName;
                                    RPIDiscountScore1HyperLink.Text = vAppendPromote.RPIDiscountScore1Name;
                                    RPIDiscountScore1UploadFU.PostedFile.SaveAs(strFolderPath + RPIDiscountScore1UploadFU.FileName);
                                    //Response.Write("<script>alert('路徑:" + strFolderPath + RPIDiscountScore1UploadFU.FileName + "');</script>");
                                }
                            }
                            catch
                            {
                                Response.Write("<script>alert('上傳檔案失敗!請重新上傳');</script>");
                            }
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore1Name = RPIDiscountScore1UploadFUName.Text.ToString();
                            RPIDiscountScore1HyperLink.Text = vAppendPromote.RPIDiscountScore1Name;
                        }
                        if (RPIDiscountScore2.SelectedValue.Equals("") || RPIDiscountScore2.SelectedValue.Equals("請選擇"))
                        {
                            vAppendPromote.RPIDiscountScore2 = "0";
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore2 = RPIDiscountScore2.SelectedValue.ToString();
                        }
                        if (RPIDiscountScore2UploadFU.HasFile)
                        {
                            try
                            {
                                for (int i = 0; i < str.Length; i++)
                                {
                                    exists = RPIDiscountScore2UploadFU.FileName.Contains(str[i]);
                                    if (exists)
                                    {
                                        break;
                                    }
                                }
                                if (exists)
                                {
                                    Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                                }
                                else
                                {
                                    RPIDiscountScore2UploadCB.Checked = true;
                                    fileLists.Add(RPIDiscountScore2UploadFU);
                                    vAppendPromote.RPIDiscountScore2Name = RPIDiscountScore2UploadFU.FileName;
                                    RPIDiscountScore2HyperLink.Text = vAppendPromote.RPIDiscountScore2Name;
                                    RPIDiscountScore2UploadFU.PostedFile.SaveAs(strFolderPath + RPIDiscountScore2UploadFU.FileName);
                                    //Response.Write("<script>alert('路徑:" + strFolderPath + RPIDiscountScore2UploadFU.FileName + "');</script>");
                                }
                            }
                            catch
                            {
                                Response.Write("<script>alert('上傳檔案失敗!請重新上傳');</script>");
                            }
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore2Name = RPIDiscountScore2UploadFUName.Text.ToString();
                            RPIDiscountScore2HyperLink.Text = vAppendPromote.RPIDiscountScore2Name;
                        }

                        if (RPIDiscountScore3.SelectedValue.Equals("") || RPIDiscountScore3.SelectedValue.Equals("請選擇"))
                        {
                            vAppendPromote.RPIDiscountScore3 = "0";
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore3 = RPIDiscountScore3.SelectedValue.ToString();
                        }
                        if (RPIDiscountScore3UploadFU.HasFile)
                        {
                            try
                            {
                                for (int i = 0; i < str.Length; i++)
                                {
                                    exists = RPIDiscountScore3UploadFU.FileName.Contains(str[i]);
                                    if (exists)
                                    {
                                        break;
                                    }
                                }
                                if (exists)
                                {
                                    Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                                }
                                else
                                {
                                    RPIDiscountScore3UploadCB.Checked = true;
                                    fileLists.Add(RPIDiscountScore3UploadFU);
                                    vAppendPromote.RPIDiscountScore3Name = RPIDiscountScore3UploadFU.FileName;
                                    RPIDiscountScore3HyperLink.Text = vAppendPromote.RPIDiscountScore3Name;
                                    RPIDiscountScore3UploadFU.PostedFile.SaveAs(strFolderPath + RPIDiscountScore3UploadFU.FileName);
                                    //Response.Write("<script>alert('路徑:" + strFolderPath + RPIDiscountScore3UploadFU.FileName + "');</script>");
                                }
                            }
                            catch
                            {
                                Response.Write("<script>alert('上傳檔案失敗!請重新上傳');</script>");
                            }
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore3Name = RPIDiscountScore3UploadFUName.Text.ToString();
                            RPIDiscountScore3HyperLink.Text = vAppendPromote.RPIDiscountScore3Name;
                        }

                        if (RPIDiscountScore4.SelectedValue.Equals("") || RPIDiscountScore4.SelectedValue.Equals("請選擇"))
                        {
                            vAppendPromote.RPIDiscountScore4 = "0";
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore4 = RPIDiscountScore4.SelectedValue.ToString();
                        }

                        if (RPIDiscountScore4UploadFU.HasFile)
                        {
                            try
                            {
                                for (int i = 0; i < str.Length; i++)
                                {
                                    exists = RPIDiscountScore4UploadFU.FileName.Contains(str[i]);
                                    if (exists)
                                    {
                                        break;
                                    }
                                }
                                if (exists)
                                {
                                    Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                                }
                                else
                                {
                                    RPIDiscountScore4UploadCB.Checked = true;
                                    fileLists.Add(RPIDiscountScore4UploadFU);
                                    vAppendPromote.RPIDiscountScore4Name = RPIDiscountScore4UploadFU.FileName;
                                    RPIDiscountScore4HyperLink.Text = vAppendPromote.RPIDiscountScore4Name;
                                    RPIDiscountScore4UploadFU.PostedFile.SaveAs(strFolderPath + RPIDiscountScore4UploadFU.FileName);
                                    //Response.Write("<script>alert('路徑:" + strFolderPath + RPIDiscountScore4UploadFU.FileName + "');</script>");
                                }
                            }
                            catch
                            {
                                Response.Write("<script>alert('上傳檔案失敗!請重新上傳');</script>");
                            }
                        }
                        else
                        {
                            vAppendPromote.RPIDiscountScore4Name = RPIDiscountScore4UploadFUName.Text.ToString();
                            RPIDiscountScore4HyperLink.Text = vAppendPromote.RPIDiscountScore4Name;
                        }
                        int discountTotal = Int32.Parse(vAppendPromote.RPIDiscountScore1) + Int32.Parse(vAppendPromote.RPIDiscountScore2) +
                        Int32.Parse(vAppendPromote.RPIDiscountScore3) + Int32.Parse(vAppendPromote.RPIDiscountScore4);
                        vAppendPromote.RPIDiscountTotal = "" + discountTotal;
                        RPIDiscountTotal.Text = "" + discountTotal;
                        RPIDiscountTotal1.Text = "" + discountTotal;
                        AppRPITotalScore.Text = "" + (Convert.ToDecimal(AppPThesisAccuScore1.Text.ToString()) + Convert.ToDecimal(discountTotal));
                    }


                    if (crudObject.UpdatePartial(vAppendPromote))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('資料更新成功!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('資料更新失敗!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('資料更新失敗!');", true);
                }
            }
        }

        protected void ThesisScoreImport_Click(object sender, EventArgs e)
        {
            GetSettings settings = new GetSettings();
            int startYear = Int32.Parse(settings.NowYear) - 5 + 1911; //前5年 轉西元            
            string empId = Session["EmpId"].ToString();
            if (Session["EmpId"] == null || Session["AppSn"] == null || Session["EmpSn"] == null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請先至『基本資料』頁籤暫存您的申請資料，才能匯入著作！');", true);
                return;
            }
            crudObject.DeleteThesisScore(Int32.Parse(Session["EmpSn"].ToString()));


            DataTable oldDT = crudObject.GetSCIPaperAuthorYear(empId, "" + startYear);
            VThesisScore vThesisScore;
            VSciPaper vSciPaper = new VSciPaper();
            //先取得前一筆的序號                    2018/03/02 修正檢測empsn 為檢測 appsn
            //int baseNum = crudObject.GetThesisScoreFinalSnNo(Session["AppSn"].ToString()) + 1;
            int baseNum = 1;
            while (crudObject.GetThesisScoreFinalSnNo(Session["AppSn"].ToString(), baseNum))
            {
                baseNum++;
            }
            if (oldDT != null)
            {
                for (int i = 0; i < oldDT.Rows.Count; i++)
                {
                    vThesisScore = new VThesisScore();
                    vThesisScore.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vThesisScore.AppSn = Int32.Parse(Session["AppSn"].ToString());
                    vThesisScore.SnNo = baseNum + i;
                    vThesisScore.ThesisName = oldDT.Rows[i][vSciPaper.strPaperName].ToString();
                    vThesisScore.ThesisResearchResult = oldDT.Rows[i][vSciPaper.strRR].ToString();
                    vThesisScore.RRNo = oldDT.Rows[i][vSciPaper.strRRNo].ToString();
                    vThesisScore.ThesisResearchResult = oldDT.Rows[i][vSciPaper.strRR].ToString();
                    vThesisScore.ThesisPublishYearMonth = oldDT.Rows[i][vSciPaper.strPublishYear].ToString() + oldDT.Rows[i][vSciPaper.strMonth].ToString();
                    vThesisScore.ThesisC = oldDT.Rows[i][vSciPaper.strC_Score].ToString();
                    vThesisScore.ThesisJ = oldDT.Rows[i][vSciPaper.strJ_Score].ToString();
                    vThesisScore.ThesisA = oldDT.Rows[i][vSciPaper.strA_Score].ToString();
                    vThesisScore.ThesisTotal = oldDT.Rows[i][vSciPaper.strTotalScore].ToString();
                    //if (Int32.Parse(vThesisScore.ThesisTotal) >= 30) vThesisScore.IsRepresentative = true;
                    crudObject.Insert(vThesisScore);
                }
            }
            GVThesisScore.DataBind();
            PaperCount.Text = GVThesisScore.Rows.Count.ToString();
            PaperOver.Text = "" + (Convert.ToInt32(PaperCount.Text.ToString()) - 15);
        }

        protected void ThesisScoreSelect_Click(object sender, EventArgs e)
        {
            string parameters = "EmpSn=" + Session["EmpSn"].ToString();
            //Response.Redirect("~/ApplyPrint.aspx?" + parameters); 
            //if(AppJobTypeNo.SelectedValue.ToString().Equals("1")
            string path = "FunSelectThesisScore.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('FunSelectThesisScore.aspx','_blank','toolbar=0,menubar=1,location=0,scrollbars=1,resizable=1,height=800,width=1000') ;", true);
        }

        //新增一篇論文
        protected void ThesisScoreInsert_Click(object sender, EventArgs e)
        {
            //TBThesisScoreInsert.Visible = true;
            ThesisScoreSave.Visible = true;
            ThesisScoreUpdate.Visible = false;
            AppThesisResearchResult.Text = "";
            //AppThesisPublishYearMonth.Text = "";
            ddl_AppThesisPublishYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_AppThesisPublishMonth.SelectedValue = "01";
            AppThesisC.Text = "";
            AppThesisJ.Text = "";
            AppThesisA.Text = "";
            LabelTotalThesisScore.Text = "";
            AppThesisName.Text = "";
            AppThesisUploadCB.Checked = false;
            AppThesisHyperLink.Text = "";
            AppThesisJournalRefCount.Text = "";
            AppThesisJournalRefUploadCB.Checked = false;
            AppThesisJournalRefHyperLink.Text = "";
            RepresentCB.Checked = false;
            CountRPICB.Checked = false;
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "Javascript", "<script>$('AppThesisResearchResult').mooEditable();</script>", true);
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "Javascript", "<script>$('AppThesisResearchResult').mooEditable();</script>", true);
            //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "Javascript", "javascript:test();", true);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);
        }


        protected void TmuExpImport_Click(object sender, EventArgs e)
        {
            VTeacherTmuExp vTeacherTmuExp = new VTeacherTmuExp();
            vTeacherTmuExp.EmpSn = vEmp.EmpSn;
            if (Session["EmpTitid"] == null || Session["EmpIdno"] == null) Response.Redirect("~/Default.aspx");

            vTeacherTmuExp.ExpTitleId = Session["EmpTitid"].ToString();

            crudObject.Delete(vTeacherTmuExp);

            //第一次進入 儲存經歷資料
            DataTable dtTable = crudObject.GetExprienceByEmpIdno(Session["EmpIdno"].ToString(), Session["EmpTitid"].ToString());
            if (dtTable != null && dtTable.Rows.Count > 0)
            {
                //crudObject.DeleteTeacherTmuExp(vEmp.EmpSn);
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    vTeacherTmuExp = new VTeacherTmuExp();
                    vTeacherTmuExp.EmpSn = vEmp.EmpSn;
                    vTeacherTmuExp.ExpUnitId = dtTable.Rows[i][vTeacherTmuExp.strExpUnitId].ToString();
                    vTeacherTmuExp.ExpTitleId = dtTable.Rows[i][vTeacherTmuExp.strExpTitleId].ToString();
                    vTeacherTmuExp.ExpPosId = dtTable.Rows[i][vTeacherTmuExp.strExpPosId].ToString();
                    vTeacherTmuExp.ExpQId = dtTable.Rows[i][vTeacherTmuExp.strExpQId].ToString();
                    vTeacherTmuExp.ExpStartDate = dtTable.Rows[i][vTeacherTmuExp.strExpStartDate].ToString();
                    vTeacherTmuExp.ExpEndDate = dtTable.Rows[i][vTeacherTmuExp.strExpEndDate].ToString();
                    crudObject.Insert(vTeacherTmuExp);
                }
            }
            GVTeacherTmuExp.DataBind();
            if (GVTeacherTmuExp.Rows.Count == 0)
                lb_NoTeacherTmuExp.Visible = true;
            else
                lb_NoTeacherTmuExp.Visible = false;
        }

        protected void TmuLessonImport_Click(object sender, EventArgs e)
        {
            getSettings = new GetSettings();
            getSettings.Execute();
            String empId = crudObject.GetVEmployeeFromTmuHrByEmpIdno(Session["EmpIdno"].ToString());
            if (Session["EmpIdno"] == null || empId == null) Response.Redirect("~/Default.aspx");
            //匯入授課進度資料
            DataTable dtTable = crudObject.GetLessonByEmpId(empId);
            DataTable acerTable = crudObject.GetNewLessonByEmpId(empId); //20200930 撈教務系統授課進度
            DataTable dt = new DataTable();                                                        //獲取兩個資料來源的並集
            if (dtTable != null)
            {
                IEnumerable<DataRow> query = dtTable.AsEnumerable().Union(acerTable.AsEnumerable(), DataRowComparer.Default);
                //兩個資料來源的並集集合
                dt = query.CopyToDataTable();
            }
            else
                dt = acerTable;

            VTeacherTmuLesson vTeacherTmuLesson;
            if (dt != null)
            {
                crudObject.DeleteTeacherTmuLesson(vEmp.EmpSn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vTeacherTmuLesson = new VTeacherTmuLesson();
                    vTeacherTmuLesson.EmpSn = vEmp.EmpSn;
                    vTeacherTmuLesson.LessonYear = dt.Rows[i]["SMTR"].ToString().Substring(0, 3);
                    vTeacherTmuLesson.LessonSemester = dt.Rows[i]["SMTR"].ToString().Substring(3, 1);
                    vTeacherTmuLesson.LessonDeptLevel = dt.Rows[i][vTeacherTmuLesson.strLessonDeptLevel].ToString();
                    vTeacherTmuLesson.LessonClass = dt.Rows[i][vTeacherTmuLesson.strLessonClass].ToString();
                    vTeacherTmuLesson.LessonCreditHours = dt.Rows[i][vTeacherTmuLesson.strLessonCreditHours].ToString();
                    vTeacherTmuLesson.LessonHours = dt.Rows[i][vTeacherTmuLesson.strLessonHours].ToString();
                    crudObject.Insert(vTeacherTmuLesson);
                }

            }
            GVTeachLesson.DataBind();
            if (GVTeachLesson.Rows.Count == 0)
                lb_NoTeachLesson.Visible = true;
            else
                lb_NoTeachLesson.Visible = false;
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
            float total = 1.0f;
            float C = 1.0f;
            float J = 1.0f;
            float A = 1.0f;
            if (!AppThesisC.Text.ToString().Equals("")) C = float.Parse(AppThesisC.Text.ToString());
            if (!AppThesisJ.Text.ToString().Equals("")) J = float.Parse(AppThesisJ.Text.ToString());
            if (!AppThesisA.Text.ToString().Equals("")) A = float.Parse(AppThesisA.Text.ToString());
            total = C * J * A;
            return total;
        }
        protected void GVTeachEdu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labEduLocal;
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
                    if (vApplyAudit != null && AppAttributeNo.SelectedValue.Equals(chkDegree))
                    {
                        vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                    }
                    LoadFgn(vAppendDegree, switchFgn);
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

        protected void GVTeacherTmuExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            LinkButton teachTmuExpDownload;
            string empSn = "1";
            if (Session["EmpSn"] != null)
            {
                empSn = Session["EmpSn"].ToString();
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                teachTmuExpDownload = (LinkButton)e.Row.FindControl("TeachTmuExpDownload");
                teachTmuExpDownload.Attributes.Add("onclick", getHyperLink(e.Row.Cells[7].Text));

            }
        }

        protected void GVTeacherTmuExp_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Upload")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int index = gvr.RowIndex;
                GridViewRow currRow = GVTeacherTmuExp.Rows[index];
                FileUpload fileUpload = (FileUpload)currRow.FindControl("TeachTmuExpUploadFU");
                Label lblEmpSn = (Label)currRow.FindControl("EmpSn");
                Label lblExpSn = (Label)currRow.FindControl("ExpSn");
                fileLists.Add(fileUpload);
                if (fileLists.Count > 0)
                {
                    ProcessUploadFiles(Int32.Parse(lblEmpSn.Text.ToString()));
                    if (fileUpload.FileName != null)
                        fileUpload.PostedFile.SaveAs(Session["FolderPath"] + fileUpload.FileName);
                }
                crudObject.UpdateUploadFile(fileUpload.FileName, Int32.Parse(lblExpSn.Text.ToString()));
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('檔案上傳成功!');", false);
                this.DataBind();
            }
        }

        protected void GVTeacherTmuExp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVTeacherTmuExp.EditIndex = e.NewEditIndex;
            GVTeacherTmuExp.DataBind();
        }

        protected void GVTeacherTmuExp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            FileUpload fileUpload = GVTeacherTmuExp.Rows[e.RowIndex].FindControl("TeachTmuExpUploadFU") as FileUpload;
            Label lblEmpSn = GVTeacherTmuExp.Rows[e.RowIndex].FindControl("EmpSn") as Label;
            fileLists.Add(fileUpload);
            if (fileLists.Count > 0) ProcessUploadFiles(Int32.Parse(lblEmpSn.Text.ToString()));
            DSTeacherTmuExp.UpdateParameters["ExpUploadName"].DefaultValue = fileUpload.FileName;
        }

        protected void GVThesisScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            HyperLink hyperLnkThesis;
            Label lblAppSn;
            Label lblThesisSign;
            Label lblCountRPI;
            Label lblSnNo = null;
            TextBox txtIsRepresentative;
            TextBox txtIsCountRPI;
            ImageButton imageButtonSnNoUp = null;
            ImageButton imageButtonSnNoDown = null;
            String SnNo;
            string empSn = "1";
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
                lblSnNo = (Label)e.Row.FindControl("lblSnNo");
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

                if (Convert.ToInt32(lblSnNo.Text.ToString()) > 15)
                {
                    e.Row.Cells[8].ForeColor = Color.FromName("Red");
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
            if (GVThesisScore.Rows.Count > 0)
                AppPThesisAccuScore1.Text = Session["ThesisAccuScore"].ToString();
            else
                AppPThesisAccuScore1.Text = "0";
            if (!AppPThesisAccuScore1.Text.ToString().Equals("") && !RPIDiscountTotal.Text.ToString().Equals(""))
            {
                AppRPITotalScore.Text = "" + (Convert.ToDecimal(AppPThesisAccuScore1.Text.ToString()) + Convert.ToDecimal(RPIDiscountTotal.Text.ToString()));
            }
            //if (lblSnNo != null && lblSnNo.Text.Equals("1"))
            //{
            //    imageButtonSnNoUp.Visible = false;
            //}
            //if (lblSnNo != null && lblSnNo.Text.Equals(countThesisScorePaper))
            //{
            //    imageButtonSnNoDown.Visible = false;
            //}


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
        void ImageButton_Command(object sender, CommandEventArgs e)
        {
            //if (e.CommandName == "Sort" && e.CommandArgument == "Ascending")
            //{

            //}
            //else
            //{

            //}

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

        //職稱選定
        protected void AppJobTitleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] num = { "零", "一", "二", "三", "四" };
            ItemLabel.Text = num[(7 - Int32.Parse(AppJobTitleNo.SelectedValue.ToString().Substring(1, 1)))];
            //AppELawNumNo.DataValueField = "LawItemNo";
            //AppELawNumNo.DataTextField = "LawItemNoCN";
            //AppELawNumNo.DataSource = crudObject.GetApplyTeacherLaw(AppJobTitleNo.SelectedValue.ToString()).DefaultView;
            //AppELawNumNo.DataBind();
            AddELawNumControls(chkPromote, AppJobTitleNo.SelectedValue.ToString(), "");
            ViewState["AppJobTitleNo"] = AppJobTitleNo.SelectedValue.ToString();
            //EmpBaseTempSave_Click(sender, e);
        }

        private void AddELawNumControls(string kindNo, string jobTitleNo, string selectedNum)
        {

            if (jobTitleNo.Equals(""))
            {

            }
            else
            {
                //String titleNo = "" + (7 - Int32.Parse(jobTitleNo.ToString().Substring(1, 1)));
                String titleNo = jobTitleNo;
                DataTable dtELaw = crudObject.GetTeacherLaw(kindNo, titleNo);

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

        #region 儲存教師論文修改前
        //protected void TeachSelfSave_Click(object sender, EventArgs e)
        //{
        //    if (Session["EmpSn"] == null)
        //    {
        //        MessageLabelTeachSelf.Text = "抱歉，請先載入已存資料! ";
        //    }else
        //    {
        //        vEmp = crudObject.GetEmpBaseByEmpSn(Session["EmpSn"].ToString());
        //        vEmp.EmpId = EmpId.Text.ToString();
        //        vEmp.EmpSelfTeachExperience = EmpSelfTeachExperience.Text.ToString();
        //        vEmp.EmpSelfReach = EmpSelfReach.Text.ToString();
        //        vEmp.EmpSelfDevelope = EmpSelfDevelope.Text.ToString();
        //        vEmp.EmpSelfSpecial = EmpSelfSpecial.Text.ToString();
        //        vEmp.EmpSelfImprove = EmpSelfImprove.Text.ToString();
        //        vEmp.EmpSelfContribute = EmpSelfContribute.Text.ToString();
        //        vEmp.EmpSelfCooperate = EmpSelfCooperate.Text.ToString();
        //        vEmp.EmpSelfTeachPlan = EmpSelfTeachPlan.Text.ToString();
        //        vEmp.EmpSelfLifePlan = EmpSelfLifePlan.Text.ToString();
        //        SaveEmpObjectToDB(sender, e);
        //    }
        //}



        //protected void OtherTeachingSave_Click(object sender, EventArgs e)
        //{


        //    vApplyAudit = crudObject.GetApplyAuditObj(Convert.ToInt32(Session["EmpSn"].ToString()));

        //    if (Session["EmpSn"] == null || vApplyAudit == null) 
        //    {
        //        MessageLabelThesisOral.Text = "抱歉，您尚未儲存『基本資料』或請先載入已存資料! ";
        //        MessageLabelThesis.Text = "抱歉，您尚未儲存『基本資料』....將自動儲\\n或系統時間逾時，請重新登入!! ";
        //        EmpBaseTempSave_Click(sender, e);
        //    }
        //    else
        //    {
        //        string location = Server.MapPath("./UploadFiles/" + vApplyAudit.AppSn + "/");
        //        if (AppOtherTeachingUploadFU.HasFile)
        //        {
        //            AppOtherTeachingUploadCB.Checked = true;
        //            fileLists.Add(AppOtherTeachingUploadFU);
        //            vApplyAudit.AppOtherTeachingUploadName = AppOtherTeachingUploadFU.FileName;
        //            vApplyAudit.AppOtherTeachingUpload = AppOtherTeachingUploadCB.Checked;
        //        }
        //        else
        //        {
        //             vApplyAudit.AppOtherTeachingUploadName = AppOtherTeachingUploadFUName.Text.ToString();
        //             vApplyAudit.AppOtherTeachingUpload = AppOtherTeachingUploadCB.Checked;
        //        }

        //        if (fileLists.Count > 0) ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

        //        if (crudObject.Update(vApplyAudit))
        //        {
        //            MessageLabel.Text = "教學相關文件上傳更新成功!! ";
        //        }

        //     }
        //}

        //全部以後蓋前
        //protected void OtherSave_Click(object sender, EventArgs e)
        //{
        //    vApplyAudit = crudObject.GetApplyAuditObj(Convert.ToInt32(Session["EmpSn"].ToString()));

        //    if (Session["EmpSn"] == null || vApplyAudit == null) 
        //    {
        //        MessageLabelThesisOral.Text = "抱歉，您尚未儲存『基本資料』或請先載入已存資料! ";
        //    }
        //    else
        //    {
        //        string location = Server.MapPath("./UploadFiles/" + vApplyAudit.AppSn + "/");

        //            //服務 寫入上傳
        //            if (AppOtherServiceUploadFU.HasFile)
        //            {
        //                AppOtherServiceUploadCB.Checked = true;
        //                fileLists.Add(AppOtherServiceUploadFU);
        //                vApplyAudit.AppOtherServiceUploadName = AppOtherServiceUploadFU.FileName;
        //                vApplyAudit.AppOtherServiceUpload = AppOtherServiceUploadCB.Checked;
        //            }
        //            else
        //            {
        //                vApplyAudit.AppOtherServiceUploadName = AppOtherServiceUploadFUName.Text.ToString();
        //                vApplyAudit.AppOtherServiceUpload = AppOtherServiceUploadCB.Checked;
        //            }

        //            //教學 寫入上傳
        //            if (AppOtherTeachingUploadFU.HasFile)
        //            {
        //                AppOtherTeachingUploadCB.Checked = true;
        //                fileLists.Add(AppOtherTeachingUploadFU);
        //                vApplyAudit.AppOtherTeachingUploadName = AppOtherTeachingUploadFU.FileName;
        //                vApplyAudit.AppOtherTeachingUpload = AppOtherTeachingUploadCB.Checked;
        //            }
        //            else
        //            {
        //                vApplyAudit.AppOtherTeachingUploadName = AppOtherTeachingUploadFUName.Text.ToString();
        //                vApplyAudit.AppOtherTeachingUpload = AppOtherTeachingUploadCB.Checked;
        //            }

        //            //醫師證書 寫入上傳
        //            if (AppDrCaUploadFU.HasFile)
        //            {
        //                AppDrCaUploadCB.Checked = true;
        //                fileLists.Add(AppDrCaUploadFU);
        //                vApplyAudit.AppDrCaUploadName = AppDrCaUploadFU.FileName;
        //                vApplyAudit.AppDrCaUpload = AppDrCaUploadCB.Checked;
        //            }
        //            else
        //            {
        //                vApplyAudit.AppDrCaUploadName = AppDrCaUploadFUName.Text.ToString();
        //                vApplyAudit.AppDrCaUpload = AppDrCaUploadCB.Checked;
        //            }

        //            //教育部教師資格證書影 寫入上傳
        //            if (AppTeacherCaUploadFU.HasFile)
        //            {
        //                AppTeacherCaUploadCB.Checked = true;
        //                fileLists.Add(AppTeacherCaUploadFU);
        //                vApplyAudit.AppTeacherCaUploadName = AppTeacherCaUploadFU.FileName;
        //                vApplyAudit.AppTeacherCaUpload = AppTeacherCaUploadCB.Checked;
        //            }
        //            else
        //            {
        //                vApplyAudit.AppTeacherCaUploadName = AppTeacherCaUploadFUName.Text.ToString();
        //                vApplyAudit.AppTeacherCaUpload = AppTeacherCaUploadCB.Checked;
        //            }


        //            //計畫主持人 寫入上傳
        //            if (AppPPMUploadFU.HasFile)
        //            {
        //                AppPPMUploadCB.Checked = true;
        //                fileLists.Add(AppPPMUploadFU);
        //                vApplyAudit.AppPPMUploadName = AppPPMUploadFU.FileName;
        //                vApplyAudit.AppPPMUpload = AppPPMUploadCB.Checked;
        //            }
        //            else
        //            {
        //                vApplyAudit.AppPPMUploadName = AppPPMUploadFUName.Text.ToString();
        //                vApplyAudit.AppPPMUpload = AppPPMUploadCB.Checked;
        //            }

        //            if (crudObject.UpdateOther(vApplyAudit))
        //            {
        //                MessageLabel.Text = "教學相關文件上傳更新成功!! ";
        //            }

        //            if (fileLists.Count > 0) ProcessUploadFiles(Int32.Parse(TbEmpSn.Text.ToString()));

        //    }
        //}
        #endregion
        protected void DegreeThesisSave_Click(object sender, EventArgs e)
        {
            string msg = "";
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
                vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                if (vAppendDegree == null)
                {
                    insertflag = true;
                    vAppendDegree = new VAppendDegree();
                }
                vAppendDegree.AppSn = vApplyAudit.AppSn;
                vAppendDegree.AppDDegreeThesisName = AppDegreeThesisName.Text.ToString();
                vAppendDegree.AppDDegreeThesisNameEng = AppDegreeThesisNameEng.Text.ToString();
                if (AppDegreeThesisUploadFU.HasFile)
                {
                    fileLists.Add(AppDegreeThesisUploadFU);
                    vAppendDegree.AppDDegreeThesisUploadName = AppDegreeThesisUploadFU.FileName;
                }
                else
                {
                    vAppendDegree.AppDDegreeThesisUploadName = AppDegreeThesisUploadFUName.Text.ToString();
                }

                //國外學歷寫入
                vAppendDegree.AppDFgnEduDeptSchoolAdmit = AppDFgnEduDeptSchoolAdmitCB.Checked;

                if (AppDFgnDegreeUploadFU.HasFile)
                {
                    AppDFgnDegreeUploadCB.Checked = true;
                    if (AppDFgnDegreeUploadFU.FileName != null && checkName(AppDFgnDegreeUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnDegreeUploadFU);
                        vAppendDegree.AppDFgnDegreeUploadName = AppDFgnDegreeUploadFU.FileName;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnDegreeUploadName = AppDFgnDegreeUploadFUName.Text.ToString();
                }

                if (AppDFgnGradeUploadFU.HasFile)
                {
                    AppDFgnGradeUploadCB.Checked = true;
                    if (AppDFgnGradeUploadFU.FileName != null && checkName(AppDFgnGradeUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnGradeUploadFU);
                        vAppendDegree.AppDFgnGradeUploadName = AppDFgnGradeUploadFU.FileName;
                        vAppendDegree.AppDFgnGradeUpload = AppDFgnGradeUploadCB.Checked;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnGradeUploadName = AppDFgnGradeUploadFUName.Text.ToString();
                    vAppendDegree.AppDFgnGradeUpload = AppDFgnGradeUploadCB.Checked;
                }

                if (AppDFgnSelectCourseUploadFU.HasFile)
                {
                    AppDFgnSelectCourseUploadCB.Checked = true;
                    if (AppDFgnSelectCourseUploadFU.FileName != null && checkName(AppDFgnSelectCourseUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnSelectCourseUploadFU);
                        vAppendDegree.AppDFgnSelectCourseUploadName = AppDFgnSelectCourseUploadFU.FileName;
                        vAppendDegree.AppDFgnSelectCourseUpload = AppDFgnSelectCourseUploadCB.Checked;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnSelectCourseUploadName = AppDFgnSelectCourseUploadFUName.Text.ToString();
                    vAppendDegree.AppDFgnSelectCourseUpload = AppDFgnSelectCourseUploadCB.Checked;
                }

                if (AppDFgnEDRecordUploadFU.HasFile)
                {
                    AppDFgnEDRecordUploadCB.Checked = true;
                    if (AppDFgnEDRecordUploadFU.FileName != null && checkName(AppDFgnEDRecordUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnEDRecordUploadFU);
                        vAppendDegree.AppDFgnEDRecordUploadName = AppDFgnEDRecordUploadFU.FileName;
                        vAppendDegree.AppDFgnEDRecordUpload = AppDFgnEDRecordUploadCB.Checked;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnEDRecordUploadName = AppDFgnEDRecordUploadFUName.Text.ToString();
                    vAppendDegree.AppDFgnEDRecordUpload = AppDFgnEDRecordUploadCB.Checked;
                }

                if (AppDFgnJPAdmissionUploadFU.HasFile)
                {
                    AppDFgnJPAdmissionUploadCB.Checked = true;
                    if (AppDFgnJPAdmissionUploadFU.FileName != null && checkName(AppDFgnJPAdmissionUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnJPAdmissionUploadFU);
                        vAppendDegree.AppDFgnJPAdmissionUploadName = AppDFgnJPAdmissionUploadFU.FileName;
                        vAppendDegree.AppDFgnJPAdmissionUpload = AppDFgnJPAdmissionUploadCB.Checked;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPAdmissionUploadName = AppDFgnJPAdmissionUploadFUName.Text.ToString();
                    vAppendDegree.AppDFgnJPAdmissionUpload = AppDFgnJPAdmissionUploadCB.Checked;
                }


                if (AppDFgnJPGradeUploadFU.HasFile)
                {
                    AppDFgnJPGradeUploadCB.Checked = true;
                    if (AppDFgnJPGradeUploadFU.FileName != null && checkName(AppDFgnJPGradeUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnJPGradeUploadFU);
                        vAppendDegree.AppDFgnJPGradeUploadName = AppDFgnJPGradeUploadFU.FileName;
                        vAppendDegree.AppDFgnJPGradeUpload = AppDFgnJPGradeUploadCB.Checked;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPGradeUploadName = AppDFgnJPGradeUploadFUName.Text.ToString();
                    vAppendDegree.AppDFgnJPGradeUpload = AppDFgnJPGradeUploadCB.Checked;
                }

                if (AppDFgnJPEnrollCAUploadFU.HasFile)
                {
                    AppDFgnJPEnrollCAUploadCB.Checked = true;
                    if (AppDFgnJPEnrollCAUploadFU.FileName != null && checkName(AppDFgnJPEnrollCAUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnJPEnrollCAUploadFU);
                        vAppendDegree.AppDFgnJPEnrollCAUploadName = AppDFgnJPEnrollCAUploadFU.FileName;
                        vAppendDegree.AppDFgnJPEnrollCAUpload = AppDFgnJPEnrollCAUploadCB.Checked;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPEnrollCAUploadName = AppDFgnJPEnrollCAUploadFUName.Text.ToString();
                    vAppendDegree.AppDFgnJPEnrollCAUpload = AppDFgnJPEnrollCAUploadCB.Checked;
                }

                if (AppDFgnJPDissertationPassUploadFU.HasFile)
                {
                    AppDFgnJPDissertationPassUploadCB.Checked = true;
                    if (AppDFgnJPDissertationPassUploadFU.FileName != null && checkName(AppDFgnJPDissertationPassUploadFU.FileName))
                    {
                        fileLists.Add(AppDFgnJPDissertationPassUploadFU);
                        vAppendDegree.AppDFgnJPDissertationPassUploadName = AppDFgnJPDissertationPassUploadFU.FileName;
                        vAppendDegree.AppDFgnJPDissertationPassUpload = AppDFgnJPDissertationPassUploadCB.Checked;
                    }
                }
                else
                {
                    vAppendDegree.AppDFgnJPDissertationPassUploadName = AppDFgnJPDissertationPassUploadFUName.Text.ToString();
                    vAppendDegree.AppDFgnJPDissertationPassUpload = AppDFgnJPDissertationPassUploadCB.Checked;
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
                if (vAppendDegree.AppDDegreeThesisUploadName != null && !vAppendDegree.AppDDegreeThesisUploadName.Equals(""))
                {
                    //EmpIdUploadFUName.Text = vEmp.EmpIdUploadName;
                    AppDegreeThesisUploadCB.Checked = true;
                    if (vAppendDegree.AppDDegreeThesisUploadName != null && checkName(vAppendDegree.AppDDegreeThesisUploadName))
                    {
                        AppDegreeThesisHyperLink.NavigateUrl = getHyperLink(vAppendDegree.AppDDegreeThesisUploadName);
                        AppDegreeThesisHyperLink.Text = vAppendDegree.AppDDegreeThesisUploadName;
                        AppDegreeThesisUploadFUName.Text = vAppendDegree.AppDDegreeThesisUploadName;
                    }
                    AppDegreeThesisHyperLink.Visible = true;
                    AppDegreeThesisUploadFU.Visible = true;
                }
                else
                {
                    AppDegreeThesisUploadFU.Visible = true;
                    AppDegreeThesisHyperLink.Visible = false;
                }

                //載入外國學歷 以學位送審才需要
                if (switchFgn != null && !switchFgn.Equals(""))
                {
                    LoadFgn(vAppendDegree, switchFgn);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }


        //寫入 學位論文:論文指導口試委員
        protected void ThesisOralSave_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (Session["EmpSn"] == null)
            {
                msg = "抱歉，您尚未儲存『基本資料』....將自動儲\n或系統時間逾時，請重新登入!! ";
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
                    //update AppendDegree

                }
                ThesisOralSave.Visible = true;
                ThesisOralUpdate.Visible = false;
                ThesisOralName.Text = "";
                ThesisOralTitle.Text = "";
                ThesisOralUnit.Text = "";
                ThesisOralOther.Text = "";
                GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
                GVThesisOral.DataBind();
            }
            if (GVThesisOral.Rows.Count == 0)
                lb_NoThesisOral.Visible = true;
            else
                lb_NoThesisOral.Visible = false;
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalAvoidList').modal('show'); });</script>", false);

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ThesisOralUpdate_Click(object sender, EventArgs e)
        {
            string msg = "";

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
                CRUDObject crudObject = new CRUDObject();
                VThesisOral vThesisOral = new VThesisOral();
                vThesisOral.ThesisOralSn = Convert.ToInt32(TBIntThesisOralSn.Text.ToString());
                vThesisOral.ThesisOralAppSn = vApplyAudit.AppSn;
                vThesisOral.ThesisOralType = ThesisOralType.SelectedValue;
                vThesisOral.ThesisOralName = ThesisOralName.Text;
                vThesisOral.ThesisOralTitle = ThesisOralTitle.Text;
                vThesisOral.ThesisOralUnit = ThesisOralUnit.Text;
                vThesisOral.ThesisOralOther = ThesisOralOther.Text;
                vThesisOral.strThesisOralAppSn = Session["AppSn"].ToString();
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
                if (GVThesisOral.Rows.Count == 0)
                    lb_NoThesisOral.Visible = true;
                else
                    lb_NoThesisOral.Visible = false;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + msg + "');", true);
        }

        protected void ThesisOralCancel_Click(object sender, EventArgs e)
        {
            //TBThesisScoreInsert.Visible = false;
            AppThesisResearchResult.Text = "";
            //AppThesisPublishYearMonth.Text = "";
            AppThesisC.Text = "";
            AppThesisJ.Text = "";
            AppThesisA.Text = "";
            LabelTotalThesisScore.Text = "";
            AppThesisName.Text = "";
            AppThesisUploadCB.Checked = false;
            AppThesisHyperLink.Text = "";
            AppThesisJournalRefCount.Text = "";
            AppThesisJournalRefUploadCB.Checked = false;
            AppThesisJournalRefHyperLink.Text = "";
            RepresentCB.Checked = false;
            CountRPICB.Checked = false;
            GVThesisOral.DataSource = crudObject.GetThesisOralList(vApplyAudit.AppSn);
            GVThesisOral.DataBind();

            if (GVThesisOral.Rows.Count == 0)
                lb_NoThesisOral.Visible = true;
            else
                lb_NoThesisOral.Visible = false;
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

        //SCI Paper總數計算後重新顯示並儲存
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
                    vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(EmpIdno.Text);
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
                    vThesisScoreCount.PT_EmpId = vEmpTmuHr.EmpId;
                    vThesisScoreCount.PT_InPerson = vEmpTmuHr.EmpEmail.Split('@')[0].ToString();
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
                    DataTable dt = crudObject.GetThesisScoreCount(vEmpTmuHr.EmpId);
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

                }
            }
        }

        protected String getHyperLink(string strFileName)
        {
            String strHyperLink = "";
            String openFile = Global.FileUpPath + Session["EmpSn"].ToString() + "/" + strFileName;

            //String openFile = "../DocuFile/HRApplyDoc/" + Session["EmpSn"].ToString() + "/" + strFileName;

            if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
            {
                strHyperLink = "javascript:void(window.open(" + Global.FileUpPath + "'NoUploadFile.pdf','_blank','location=no,height=800,width=500') )";
            }
            else
            {
                if (openFile != "&nbsp;" && openFile.IndexOf("_" + EmpNameCN.Text.Trim()) > 0)
                    openFile = openFile.Substring(0, openFile.IndexOf("_" + EmpNameCN.Text.Trim())) + ".pdf";
                strHyperLink = "javascript:void(window.open('" + openFile + "','_blank','location=no,height=800,width=500') )";
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
            Response.Redirect("http://hr2sys.tmu.edu.tw/HrApply/Default.aspx");
        }

        protected string checkSheets()
        {
            string errorMsg = "";
            string empBase = ""; //基本資料
            string empTeach = ""; //學歷資料
            string empTeachExp = ""; //所有經歷資料
            string empTeachLesson = ""; //授課進度表
            string empThesisScore = ""; //升等積分計分表&上傳論文

            #region 基本資料檢測
            if (AppJobTypeNo.SelectedValue == "請選擇" || ELawNum.SelectedValue == "" || EmpBirthDay.Text == "" || EmpNameENFirst.Text == "" || EmpNameENLast.Text == "" || AppJobTypeNo.SelectedValue == "請選擇" || AppDeclarationHyperLink.Text == "")
            {
                if (ELawNum.SelectedValue == "")
                    empBase += "法規依據未選、";
                if (EmpNameENFirst.Text == "")
                    empBase += "英文名未填、";
                if (EmpNameENLast.Text == "")
                    empBase += "英文姓未填、";
                if (AppDeclarationHyperLink.Text == "")
                    empBase += "教師資格審查資料切結書(pdf)未上傳、";
                if (empBase != "")
                    errorMsg += "基本資料-" + empBase.Substring(0, empBase.Length - 1) + "\\n\\n";
            }
            #region 基本資料-其他檢測
            empBase = "";
            if (ExpServiceCaHyperLink.Text == "" || AppPPMHyperLink.Text == "" || AppTeacherCaHyperLink.Text == "" || AppOtherTeachingHyperLink.Text == "" || AppOtherServiceHyperLink.Text == "")
            {
                if (ExpServiceCaHyperLink.Text == "")
                    empBase += "基本資料(其他)-經歷服務證明(pdf)、";
                if (AppAttributeNo.SelectedValue == "1" && AppPPMHyperLink.Text == "")
                    empBase += "基本資料(其他)-研究計劃主持(pdf)、";
                if (AppAttributeNo.SelectedValue == "1" && AppTeacherCaHyperLink.Text == "")
                    empBase += "基本資料(其他)-現任職等部定證書影本(pdf)、";
                if (AppAttributeNo.SelectedValue != "3" && AppOtherTeachingHyperLink.Text == "")
                    empBase += "基本資料(其他)-教學成果(pdf)、";
                if (AppOtherServiceHyperLink.Text == "")
                    empBase += "基本資料(其他)-服務成果(pdf)未上傳、";
                if (AppAttributeNo.SelectedValue != "1" && AppDeclarationHyperLink.Text == "")
                    empBase += "基本資料(其他)-教師資格審查資料切結書(pdf)未上傳、";



                if (AppAttributeNo.SelectedValue == "3" && EmpIdnoHyperLink.Text == "")
                    empBase += "基本資料(其他)-身份證上傳(pdf)未上傳、";
                if (AppAttributeNo.SelectedValue == "3" && EmpDegreeHyperLink.Text == "")
                    empBase += "基本資料(其他)-最高學歷證件(pdf)未上傳、";
                if (AppAttributeNo.SelectedValue == "3" && AppDrCaHyperLink.Text == "")
                    empBase += "基本資料(其他)-醫師證書(pdf)未上傳、";
                if (AppAttributeNo.SelectedValue == "3" && AppPublicationHyperLink.Text == "")
                    empBase += "基本資料(其他)-著作出版刊物(pdf)未上傳、";
                if (AppAttributeNo.SelectedValue == "3" && RecommendHyperLink.Text == "")
                    empBase += "基本資料(其他)-推薦書二份合併上傳(pdf)未上傳、";

            }
            if (empBase != "")
                errorMsg += empBase.Substring(0, empBase.Length - 1) + "\\n";
            #endregion

            #endregion
            #region 學歷資料檢測
            if (GVTeachEdu.Rows.Count <= 0)
            {
                empTeach += "學歷資料未填";
                errorMsg += "學歷資料-" + empTeach + "\\n";
            }
            #endregion
            #region 所有經歷資料檢測
            if (CBNoTeachExp.Checked == false && GVTeacherTmuExp.Rows.Count <= 0 && GVTeachExp.Rows.Count <= 0)
            {
                empTeachExp += "經歷資料未填或未勾選無相關資料";
                errorMsg += "所有經歷資料-" + empTeachExp + "\\n";
            }
            #endregion
            #region 授課進度表檢測
            if (GVTeachLesson.Rows.Count <= 0)
            {
                empTeachLesson += "授課進度未填";
                errorMsg += "授課進度表-" + empTeachLesson + "\\n";
            }
            #endregion
            #region 升等積分計分表&上傳論文
            if (AppAttributeNo.SelectedValue == "1" && GVThesisScore.Rows.Count <= 0)
            {
                empThesisScore += "論文積分未填寫上傳";
                errorMsg += "升等積分計分表&上傳論文-" + empThesisScore + "\\n";
            }
            #endregion
            #region 學位送審時學位論文
            if (AppAttributeNo.SelectedValue == "2" && (AppDegreeThesisName.Text == "" || AppDegreeThesisNameEng.Text == "" || AppDegreeThesisHyperLink.Text == ""))
            {
                empThesisScore += "學位論文著作未填寫上傳";
                errorMsg += "學位論文相關-" + empThesisScore + "\\n";
            }
            #endregion
            return errorMsg;
        }

        protected void teachExpAdd_Click(object sender, EventArgs e)
        {
            TeachExpSave.Visible = true;
            TeachExpUpdate.Visible = false;
            TeachExpOrginization.Text = "";
            TeachExpUnit.Text = "";
            TeachExpJobTitle.Text = "";
            TeachExpJobType.SelectedValue = "1";
            TeachExpStartYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            TeachExpStartMonth.SelectedValue = "01";
            TeachExpEndYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            TeachExpEndMonth.SelectedValue = "01";
            TeachExpUploadFU.Dispose();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachExp').modal('show'); });</script>", false);
        }

        protected void teachEduAdd_Click(object sender, EventArgs e)
        {
            TeachEduSave.Visible = true;
            TeachEduUpdate.Visible = false;
            TeachEduLocal.SelectedValue = "TTO";
            TeachEduSchool.Text = "";
            TeachEduDepartment.Text = "";
            TeachEduDegree.SelectedValue = "20";
            TeachEduStartYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            TeachEduStartMonth.SelectedValue = "01";
            TeachEduEndYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            TeachEduEndMonth.SelectedValue = "01";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachEdu').modal('show'); });</script>", false);
        }

        protected void teachLessonAdd_Click(object sender, EventArgs e)
        {
            TeachLessonSave.Visible = true;
            TeachLessonUpdate.Visible = false;
            ddl_TeachLessonYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            ddl_TeachLessonSemester.SelectedValue = "1";
            TeachLessonDepLevel.Text = "";
            TeachLessonClass.Text = "";
            TeachLessonHours.Text = "";
            TeachLessonCreditHours.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachLesson').modal('show'); });</script>", false);
        }

        protected void RepresentCB_CheckedChanged(object sender, EventArgs e)
        {
            if (RepresentCB.Checked)
                RepresentTable.Visible = true;
            else
                RepresentTable.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalThesisScore').modal('show'); });</script>", false);
        }

        protected void avoidListAdd_Click(object sender, EventArgs e)
        {
            ThesisOralSave.Visible = true;
            ThesisOralUpdate.Visible = false;
            ThesisOralName.Text = "";
            ThesisOralTitle.Text = "";
            ThesisOralUnit.Text = "";
            ThesisOralOther.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalAvoidList').modal('show'); });</script>", false);
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
                    LoadDataBtn_Click(sender, e);
                }
            }
        }
    }
}
