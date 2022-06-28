using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace ApplyPromote
{
    public partial class ApplyEmp : PageBaseApply, ICallbackEventHandler
    {

        string tooltip;
        //臨床學科直接詳填
        public ArrayList untArray = new ArrayList { "E0109", "E0110", "E0111", "E0112", "E0113", "E0114", "E0115", "E0116", "E0117", "E0118", "E0119", "E0120", "E0121", "E0122", "E0123", "E0124", "E0125", "E0126", "E0102", "E0130", "E0131" };


        public void RaiseCallbackEvent(String eventArgument)
        {
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

        //新聘申請著作送審
        static string chkPublication = "3";

        //新聘臨床教師新聘4
        static string chkClinical = "4";

        //申請新聘
        static string chkApply = "1";

        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();

        GetSettings setting = new GetSettings();

        DESCrypt DES = new DESCrypt();

        QuestionnaireService questionnaireService = new QuestionnaireService();

        //頁籤切換
        public enum SearchType
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewTeachEdu = 1,
            ViewTeachExp = 2
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (Request["ApplyerID"] != null)
            {
                try
                {
                    if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                    {
                        VYear.Visible = true;
                        VSemester.Visible = true;
                        VYearAndSemester.Text = "&nbsp;/&nbsp;";
                    }
                    EmpIdno.Text = DES.Decrypt(Request["ApplyerID"].ToString());
                    ViewState["ApplyerID"] = EmpIdno.Text;
                    Session["ApplyerID"] = EmpIdno.Text;
                    Session["EmpIdno"] = EmpIdno.Text;
                    if (Request["AppSn"] != null)
                    {
                        TbAppSn.Text = DES.Decrypt(Request["AppSn"].ToString());
                        Session["AppSn"] = TbAppSn.Text;
                        string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text);
                        Session["Parameters"] = parameters;
                    }
                    else
                    {
                        TbAppSn.Text = "";
                        Session["AppSn"] = null;
                        Session["Parameters"] = "";
                    }

                    //新聘判斷是否為多單
                    BtnReturnBack.Visible = crudObject.GetApplyListCntByIdno(EmpIdno.Text) > 1;
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


            vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text);
            if (vEmp != null)
            {
                Session["EmpSn"] = vEmp.EmpSn;
                TbEmpSn.Text = vEmp.EmpSn.ToString();
                if (String.IsNullOrEmpty(TbAppSn.Text))//只有一筆資料或者無資料
                {
                    vApplyAudit = crudObject.GetApplyAuditObjByIdno(EmpIdno.Text);
                    if (vApplyAudit != null)
                        vApplyAudit.AppUnitNo = "";
                }
                else
                {
                    vApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text));
                }
                if (vApplyAudit != null)
                {
                    Session["AppSn"] = vApplyAudit.AppSn;
                    ViewState["ApplyAttributeNo"] = vApplyAudit.AppAttributeNo;
                }
                else
                {
                    Session["AppSn"] = null;
                    ViewState["ApplyAttributeNo"] = "";
                }
            }
            Menu1.Visible = true;

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            EmpEmail.Text = setting.LoginMail;
            H1.NavigateUrl = "javascript:void(window.open('http://hr.tmu.edu.tw/eva/super_pages.php?ID=reg2','_blank','height=800','width=800') )";
            H2.NavigateUrl = "javascript:void(window.open('http://hr.tmu.edu.tw/eva/super_pages.php?ID=reg2','_blank','height=800','width=800') )";
            H3.NavigateUrl = "javascript:void(window.open('http://hr.tmu.edu.tw/eva/super_pages.php?ID=reg2','_blank','height=800','width=800') )";

            if (Request["ApplyerID"] != null)
            {
                if (Request["ManageEmpId"] != null)
                {
                    EmpBaseModifySave.Visible = true;
                    SelfReviewModifySave.Visible = true;
                    SelfReviewSave.Visible = false;
                    Session["ManageEmpId"] = Request["ManageEmpId"].ToString();
                }
                else
                {
                    //返回鍵顯示 返回時清空Session["ManageEmpId"] 
                    if (Session["ManageEmpId"] != null && !Session["ManageEmpId"].ToString().Equals(""))
                    {
                        EmpBaseModifySave.Visible = true;
                        SelfReviewModifySave.Visible = true;
                    }
                }

                AddELawNumControls(chkApply, "06", "");
                vEmp = crudObject.GetEmpBsaseObj(EmpIdno.Text);
                if (vEmp != null)
                {
                    //流程：先抓當學期新聘資料>再抓前一次申請資料>抓員工基本資料EmployeeBase>EmpHr資料
                    if (TbAppSn.Text.Equals(""))//只有一筆資料或者無資料
                    {
                        vApplyAudit = crudObject.GetApplyAuditObjByIdno(EmpIdno.Text);
                        if (vApplyAudit != null && Request["AppSn"] != null)
                        {
                            vApplyAudit.AppSn = 0;
                            vApplyAudit.AppStatus = false; //無論前一次狀態是成功或失敗;
                        }
                        if (vApplyAudit != null && Request["AppSn"] == null)
                            vApplyAudit.AppUnitNo = "";
                    }
                    else
                    {
                        vApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text));
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

                        Color col = ColorTranslator.FromHtml("#FFFFFF");
                        strCell = "申請完成";
                        Cell.BackColor = Color.YellowGreen;
                        Cell.Text = strCell;
                        tRow.Cells.Add(Cell);
                        Cell = new TableCell();
                        Cell.Width = 150;

                        string tmpStage = "0";
                        string strInAudit = "";
                        int cnt = dtTable.Rows.Count - 1;
                        if (cnt > 0)
                        {
                            for (int i = 0; i <= dtTable.Rows.Count - 1; i++)
                            {
                                if (i == 0)
                                    tmpStage = dtTable.Rows[i][0].ToString();

                                if (vApplyAudit.AppStage.Equals(dtTable.Rows[i][0].ToString()) && vApplyAudit.AppStep.Equals(dtTable.Rows[i][1].ToString()))
                                    strInAudit = strStatus[Int32.Parse(dtTable.Rows[i][0].ToString())] + "<br>" + dtTable.Rows[i][2].ToString() + "審核中...";

                                Cell.Text = strStatus[Int32.Parse(tmpStage)];

                                //階段顏色
                                if (vApplyAudit.AppStage.Equals(dtTable.Rows[i][0].ToString()) || Convert.ToInt32(dtTable.Rows[i][0].ToString()) < Convert.ToInt32(vApplyAudit.AppStage))
                                    Cell.BackColor = Color.YellowGreen;
                                else
                                    Cell.BackColor = Color.Yellow;

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
                                Cell.BackColor = Color.YellowGreen;
                            else
                                Cell.BackColor = Color.Yellow;

                            //階段中的文字
                            if (string.IsNullOrEmpty(strInAudit))
                                strCell = strStatus[Int32.Parse(tmpStage)];
                            else
                            {
                                strCell = strInAudit;
                                strInAudit = "";
                            }
                        }

                        //寫入CELL
                        Cell.Text = strCell;
                        tRow.Cells.Add(Cell);

                        Label lblStatus = new Label();
                        lblStatus.Text = "審核狀態：<font color='green'>綠色</font>表已完成階段";
                        PanelStatus.Controls.Add(lblStatus);
                        PanelStatus.Controls.Add(table);
                    }
                }
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
                    Session["ThesisScore"] = 1;
                    MessageLabel.Text = "";
                    allDDL_refresh();
                    ddl_EmpBirthYear.Attributes.Add("onChange", "SelChange()");
                    ddl_EmpBirthMonth.Attributes.Add("onChange", "SelChange()");
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);

                List<VRefQuestionnaire> questionnaires = this.questionnaireService.GetAll();
                foreach (var q in questionnaires)
                    this.cblQuestionnaire.Items.Add(new ListItem(q.Item, q.ID.ToString()));

            }
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
                    if (Session["AppUnitNo"] != null)
                        AppUnitNo.SelectedValue = Session["AppUnitNo"].ToString();

                    AppJobTypeNo.DataValueField = "JobAttrNo";
                    AppJobTypeNo.DataTextField = "JobAttrName";
                    AppJobTypeNo.DataSource = crudObject.GetAllJobTypeNoName();
                    AppJobTypeNo.DataBind();
                    AppJobTypeNo.Items.Insert(0, "請選擇");

                    String[] num = { "零", "一", "二", "三", "四", "五" };
                    //Local預設在台灣
                    TeachEduLocal.DataBind();
                    for (int i = 0; i < TeachEduLocal.Items.Count - 1; i++)
                        TeachEduLocal.Items[i].Selected = "TWN".Equals(TeachEduLocal.Items[i].Value);
                }



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
                        AuditAttributeName.Text = AppAttributeNo.Items[i].ToString();
                    }
                    else
                        AppAttributeNo.Items[i].Selected = false;
                }

                TeachExpSave.Enabled = false;

                if (vApplyAudit != null) //若有舊資料就抓新聘這邊的基本資料檔
                    LoadData();
                else
                {
                    VEmpTmuHr vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(EmpIdno.Text);
                    if (vEmpTmuHr != null)
                    {
                        if (!String.IsNullOrEmpty(vEmpTmuHr.EmpBdate) && vEmpTmuHr.EmpBdate.ToString().Length == 7)
                        {
                            ddl_EmpBirthYear.SelectedValue = vEmpTmuHr.EmpBdate.Substring(0, 3);
                            ddl_EmpBirthMonth.SelectedValue = vEmpTmuHr.EmpBdate.Substring(3, 2);
                            ddl_EmpBirthDate.SelectedValue = vEmpTmuHr.EmpBdate.Substring(5, 2);
                        }
                        EmpSex.Text = vEmpTmuHr.EmpSex;
                        EmpCell.Text = vEmpTmuHr.EmpDgd;
                        EmpNowJobOrg.Text = vEmpTmuHr.EmpUntFullName;

                        AuditNameCN.Text = vEmpTmuHr.EmpName;
                        EmpNameCN.Text = vEmpTmuHr.EmpName;
                    }
                }
            }
            else
            {
                if (vApplyAudit != null)
                {
                    if (Request["ApplyKindNo"] != null && !Request["ApplyKindNo"].ToString().Equals(""))
                        ViewState["ApplyKindNo"] = Request["ApplyKindNo"].ToString();
                    else
                        ViewState["ApplyKindNo"] = vApplyAudit.AppKindNo;

                    if (Request["ApplyWayNo"] != null && !Request["ApplyWayNo"].ToString().Equals(""))
                        ViewState["ApplyWayNo"] = Request["ApplyWayNo"].ToString();
                    else
                        ViewState["ApplyWayNo"] = vApplyAudit.AppWayNo;

                    if (Request["ApplyAttributeNo"] != null && !Request["ApplyAttributeNo"].ToString().Equals(""))
                        ViewState["ApplyAttributeNo"] = Request["ApplyAttributeNo"].ToString();
                    else
                        ViewState["ApplyAttributeNo"] = vApplyAudit.AppAttributeNo;

                    Session["AppUnitNo"] = vApplyAudit.AppUnitNo;
                    if (Session["isLoadDataBtn"] == null || Session["isLoadDataBtn"].ToString().Equals(""))
                        LoadData();
                    else
                        Session["isLoadDataBtn"] = "";

                    TbKindNo.Text = ViewState["ApplyKindNo"].ToString();
                    AddELawNumControls(chkApply, vApplyAudit.AppJobTitleNo, vApplyAudit.AppLawNumNo);
                }
            }

            if (ViewState["ApplyAttributeNo"] != null)
            {
                if (ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical))
                    RecommandNote.Text = "僅有醫學院者需附上 <br/>";
            }
            if (Session["AppUnitNo"] != null && !String.IsNullOrEmpty(Session["AppUnitNo"].ToString()))
                AppUnitNo.SelectedValue = Session["AppUnitNo"].ToString();
        }

        protected void BtnApplyMore_Click(object sender, EventArgs e)
        {
            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=1&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
            Response.Redirect("~/ApplyList.aspx?" + parameters);
        }

        protected void AppAttributeNo_Click(object sender, EventArgs e)
        {
            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text) + "&ApplyKindNo=" + ViewState["ApplyKindNo"].ToString() + "&ApplyWayNo=" + ViewState["ApplyWayNo"].ToString() + "&ApplyAttributeNo=" + ViewState["ApplyAttributeNo"].ToString();
            Response.Redirect("~/ApplyShortEmp.aspx?" + parameters);
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

        }

        protected void EmpBornProvince_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //暫存功能
        protected void EmpBaseTempSave_Click(object sender, EventArgs e)
        {
            TransferDataToEmpObject();
            SaveEmpObjectToDB();
            InsertAppendData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:var retValue=alert('您的資料已暫存成功!');", true);
        }

        //自評儲存
        protected void SelfReviewSave_Click(object sender, EventArgs e)
        {
            TransferDataToEmpObject();
            SaveEmpObjectToDB();
            LoadData();
        }

        //畫面資料存入物件 1基本資料&2自我評議 
        private void TransferDataToEmpObject()
        {
            if (vEmp == null)
                vEmp = new VEmployeeBase();
            vEmp.EmpIdno = EmpIdno.Text;
            vEmp.EmpBirthDay = ddl_EmpBirthYear.SelectedValue + ddl_EmpBirthMonth.SelectedValue + ddl_EmpBirthDate.SelectedValue;
            vEmp.EmpNameCN = EmpNameCN.Text;
            vEmp.EmpSex = EmpSex.Text;
            vEmp.EmpEmail = EmpEmail.Text;

            vEmp.EmpEmail = setting.LoginMail;
            vEmp.EmpCell = EmpCell.Text;
            vEmp.EmpNowJobOrg = EmpNowJobOrg.Text;
            vEmp.EmpExpertResearch = EmpExpertResearch.Text;

            if (EmpDegreeUploadFU.HasFile)
            {
                if (EmpDegreeUploadFU.FileName != null && CheckName(EmpDegreeUploadFU.FileName))
                {
                    EmpDegreeUploadCB.Checked = true;
                    fileLists.Add(EmpDegreeUploadFU);
                    vEmp.EmpDegreeUploadName = EmpDegreeUploadFU.FileName;
                    ProcessUploadFiles(vEmp.EmpSn);
                }
            }
            else
                vEmp.EmpDegreeUploadName = EmpDegreeUploadFUName.Text;

            vEmp.EmpNoTeachExp = CBNoTeachExp.Checked;
        }

        //資料存入BackupDB 備份原本資料 送出後異動備份
        private void SaveEmpObjectToBackupDB(object sender, EventArgs e)
        {
            VEmployeeBase oldEmp = crudObject.GetEmpBaseObjByEmpSn(TbAppSn.ToString());
            //資料存入BackupDB 備份原本資料 更新最後異動者
            if (Session["AcctAuditorSnEmpId"] != null && !Session["AcctAuditorSnEmpId"].Equals(""))
                vEmp.UpdateUserId = Session["AcctAuditorSnEmpId"].ToString();
            else
                vEmp.UpdateUserId = Session["EmpIdno"].ToString(); //走不到這邊

            if (crudObject.InsertBackup(oldEmp))
                MessageLabel.Text = "異動資料：成功!!";
            else
                MessageLabel.Text = "異動資料：失敗，請洽資訊人員!!";

            //Update 原本資料
            if (crudObject.Update(vEmp))
            {
                InsertAppendData();
                MessageLabel.Text = "異動資料：成功!! ";
                ProcessUploadFiles(vEmp.EmpSn);
            }
            else
                MessageLabel.Text = "異動資料：失敗，請洽資訊人員!!";
        }

        //基本資料存入DB
        private void SaveEmpObjectToDB()
        {
            vEmp.EmpSn = crudObject.GetEmpBaseEmpSn(vEmp.EmpIdno);
            vEmp.UpdateUserId = vEmp.EmpIdno;
            if (vEmp.EmpSn > 0 && Request["ApplyMore"] != "True")
            {
                VEmployeeBase oldEmp = crudObject.GetEmpBaseObjByEmpSn(TbEmpSn.Text);

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
                    MessageLabel.Text = "儲存資料：成功!! ";
                    ProcessUploadFiles(vEmp.EmpSn);
                }
            }
            else
            {
                vEmp.UpdateUserId = vEmp.EmpIdno;
                crudObject.InsertShort(vEmp);
                vEmp.EmpSn = crudObject.GetEmpBaseEmpSn(vEmp.EmpIdno);
                if (vEmp.EmpSn > 0)
                {
                    InsertAppendData(); //寫入延伸檔
                    MessageLabel.Text = "儲存資料：成功!!";
                    //處裡檔案上傳
                    ProcessUploadFiles(vEmp.EmpSn);
                }
                else
                {
                    MessageLabel.Text = "儲存資料：失敗，請洽資訊人員";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('儲存資料：失敗，請洽資訊人員!!');", true);
                    return;
                }
            }

        }

        //寫入 延伸的資料檔
        private void InsertAppendData()
        {
            if (vEmp.EmpSn.Equals("0"))
                return;
            //判斷 ApplyAudit是否已存在
            vApplyAudit = new VApplyAudit();
            //寫入新聘,升等共用延伸資料檔
            vApplyAudit.EmpSn = vEmp.EmpSn;
            vApplyAudit.EmpIdno = Session["ApplyerID"].ToString();
            vApplyAudit.AppYear = setting.NowYear;
            vApplyAudit.AppSemester = setting.NowSemester;
            vApplyAudit.AppKindNo = ViewState["ApplyKindNo"].ToString(); //新聘
            vApplyAudit.AppWayNo = ViewState["ApplyWayNo"].ToString(); //途徑
            vApplyAudit.AppAttributeNo = ViewState["ApplyAttributeNo"].ToString(); //類型  AppEAttributeNo.SelectedValue
            vApplyAudit.AppUnitNo = Session["AppUnitNo"] == null ? AppUnitNo.SelectedValue : Session["AppUnitNo"].ToString();
            vApplyAudit.AppJobTitleNo = AppJobTitleNo.SelectedValue; //1講師,2助理教授,3副教授,4教授 剛好是法令條款 若有法令異動時UI須修正
            vApplyAudit.AppJobTypeNo = AppJobTypeNo.SelectedValue;
            vApplyAudit.AppLawNumNo = ELawNum.SelectedValue;
            vApplyAudit.AppSelfTeachExperience = EmpSelfTeachExperience.Text;
            vApplyAudit.AppSelfReach = EmpSelfReach.Text;
            vApplyAudit.AppSelfDevelope = EmpSelfDevelope.Text;
            vApplyAudit.AppSelfSpecial = EmpSelfSpecial.Text;
            vApplyAudit.AppSelfImprove = EmpSelfImprove.Text;
            vApplyAudit.AppSelfContribute = EmpSelfContribute.Text;
            vApplyAudit.AppSelfCooperate = EmpSelfCooperate.Text;
            vApplyAudit.AppSelfTeachPlan = EmpSelfTeachPlan.Text;
            vApplyAudit.AppSelfLifePlan = EmpSelfLifePlan.Text;


            //履歷表CV上傳 寫入上傳
            if (AppResumeUploadFU.HasFile)
            {
                if (AppResumeUploadFU.FileName != null && CheckName(AppResumeUploadFU.FileName))
                {
                    AppResumeUploadCB.Checked = true;
                    fileLists.Add(AppResumeUploadFU);
                    vApplyAudit.AppResumeUploadName = AppResumeUploadFU.FileName;
                    ProcessUploadFiles(vEmp.EmpSn);
                }
            }
            else
                vApplyAudit.AppResumeUploadName = AppResumeUploadFUName.Text;

            //論文積分表
            if (ThesisScoreUploadFU.HasFile)
            {
                if (ThesisScoreUploadFU.FileName != null && CheckName(ThesisScoreUploadFU.FileName))
                {
                    ThesisScoreUploadCB.Checked = true;
                    fileLists.Add(ThesisScoreUploadFU);
                    vApplyAudit.ThesisScoreUploadName = ThesisScoreUploadFU.FileName;
                    ProcessUploadFiles(vEmp.EmpSn);
                }
            }
            else
                vApplyAudit.ThesisScoreUploadName = ThesisScoreUploadFUName.Text;

            vApplyAudit.AppRecommendors = AppRecommendors.Text;

            //推薦書(函)上傳
            if (RecommendUploadFU.HasFile)
            {
                if (RecommendUploadFU.FileName != null && CheckName(RecommendUploadFU.FileName))
                {
                    RecommendUploadCB.Checked = true;
                    fileLists.Add(RecommendUploadFU);
                    vApplyAudit.AppRecommendUploadName = RecommendUploadFU.FileName;
                }
            }
            else
                vApplyAudit.AppRecommendUploadName = RecommendUploadFUName.Text;

            //再次撈取最後申請資料
            VApplyAudit vOldApplyAudit = crudObject.GetApplyAuditObjLastOne(vEmp.EmpSn);

            if (!Object.Equals(null, vOldApplyAudit))
            {
                //教師資格切結書 寫入上傳
                vApplyAudit.AppDeclarationUploadName = vOldApplyAudit.AppDeclarationUploadName;
                vApplyAudit.AppDeclarationUpload = vOldApplyAudit.AppDeclarationUpload;

                //服務 寫入上傳
                vApplyAudit.AppOtherServiceUploadName = vOldApplyAudit.AppOtherServiceUploadName;
                vApplyAudit.AppOtherServiceUpload = vOldApplyAudit.AppOtherServiceUpload;

                //教學 寫入上傳
                vApplyAudit.AppOtherTeachingUploadName = vOldApplyAudit.AppOtherTeachingUploadName;
                vApplyAudit.AppOtherTeachingUpload = vOldApplyAudit.AppOtherTeachingUpload;

                //醫師證書 寫入上傳
                vApplyAudit.AppDrCaUploadName = vOldApplyAudit.AppDrCaUploadName;
                vApplyAudit.AppDrCaUpload = vOldApplyAudit.AppDrCaUpload;

                //教育部教師資格證書影 寫入上傳
                vApplyAudit.AppTeacherCaUploadName = vOldApplyAudit.AppTeacherCaUploadName;
                vApplyAudit.AppTeacherCaUpload = vOldApplyAudit.AppTeacherCaUpload;

                //計畫主持人 寫入上傳
                vApplyAudit.AppPPMUploadName = vOldApplyAudit.AppDeclarationUploadName;
                vApplyAudit.AppPPMUpload = vOldApplyAudit.AppDeclarationUpload;

                //從代表著作頁籤寫入
                vApplyAudit.AppPublicationUploadName = vOldApplyAudit.AppPublicationUploadName;
                vApplyAudit.AppPublicationName = vOldApplyAudit.AppPublicationName;


                vApplyAudit.AppRPIScore = vOldApplyAudit.AppRPIScore;

                vApplyAudit.AppResearchYear = vOldApplyAudit.AppResearchYear;

            }

            vApplyAudit.AppBuildDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);

            //是否已寫入ApplyAudit
            VApplyAudit oldVApplyAudit = null;
            //新增加申請單時此欄位會是空的
            if (!String.IsNullOrEmpty(TbAppSn.Text.Trim()))
            {
                oldVApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text));
            }
            //暫存
            if (Session["AppSn"] != null && oldVApplyAudit == null && Convert.ToBoolean(Session["AppMore"]) != true)
            {
                oldVApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(Session["AppSn"].ToString()));
            }

            if (oldVApplyAudit != null && (!String.IsNullOrEmpty(TbAppSn.Text.Trim()) || Session["AppSn"] != null))
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
                vApplyAudit.Questionnaires = InsertQuestionnaire(vApplyAudit.AppSn);
                if (!crudObject.UpdateShort(vApplyAudit))
                {
                    MessageLabel.Text += "1.共用延伸資料檔更新失敗，請洽資訊人員!";
                }
                else
                {
                    this.questionnaireService.Delete(vApplyAudit.AppSn);
                    foreach (var item in vApplyAudit.Questionnaires)
                        this.questionnaireService.Create(item);
                }
            }
            else
            {
                vApplyAudit.AppStage = "0"; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)            
                vApplyAudit.AppStep = "0";
                if (crudObject.Insert(vApplyAudit, Session["ApplyerID"].ToString()))
                {
                    //寫入成功回傳序號
                    vApplyAudit.AppSn = crudObject.GetApplyAuditAppSn(vEmp.EmpSn);
                    Session["AppMore"] = false;
                    TbAppSn.Text = vApplyAudit.AppSn.ToString();
                }
                else
                {
                    MessageLabel.Text += "1.共用延伸資料檔寫入失敗，請洽資訊人員!";
                }

            }
        }

        private List<VApplyQuestionnaire> InsertQuestionnaire(int appSn)
        {
            List<VApplyQuestionnaire> qs = new List<VApplyQuestionnaire>();
            foreach (ListItem item in this.cblQuestionnaire.Items)
            {
                if (item.Selected)
                {
                    VApplyQuestionnaire q = new VApplyQuestionnaire()
                    {
                        AppSn = appSn,
                        QuestionnaireID = Convert.ToInt32(item.Value)
                    };

                    if (item.Value == "6")
                        q.ItemContent = this.txtQuestionnaireOther.Text;
                    qs.Add(q);
                }
            }
            return qs;

        }

        /**
         * 送出申請
         *1.將EmployeeBase status update 1
         *2.判斷AppEAttributeNo 產生審核檔案 
         **/
        protected void EmpBaseSave_Click(object sender, EventArgs e)
        {
            String strErrMsg = "您所選擇的系所 " + AppUnitNo.SelectedItem.Text.Trim() + " 目前尚未開放申請. 如有問題，請洽人資處承辦人(分機2028或分機2066)";

            VApplyAudit oldVApplyAudit = null;
            oldVApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text));
            //撈取案件狀態是否退回本人 OR 系所是否開啟
            if (crudObject.GetOpenOrCloseUnit(AppUnitNo.SelectedValue.Trim(), AppJobTypeNo.SelectedValue)
                 || (!oldVApplyAudit.AppStage.Equals("0") && !oldVApplyAudit.AppStep.Equals("0") && oldVApplyAudit.AppStatus.Equals(false)))
            {
                GVTeachEdu.DataBind();
                GVTeachExp.DataBind();

                string strPopup = "以下頁籤未完成，請確認欄位填寫完整才能送出：\\n\\n";

                string checkSheet = checkSheets();
                if (!String.IsNullOrEmpty(checkSheet))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strPopup + checkSheet + "');", true);
                    return;
                }
                else
                {
                    #region 檢測無誤送出
                    //再次確認密碼正確才能送出
                    TransferDataToEmpObject();
                    SaveEmpObjectToDB();

                    crudObject = new CRUDObject();
                    VUnit vUnit;
                    VUnit sUnit;
                    VAuditExecute vAuditExecute;
                    VAuditPeroid vAuditPeroid;

                    //取得密碼
                    GeneratorPwd generatorPwd = new GeneratorPwd();
                    ArrayList arrayList;

                    string strAuditPointDepartment; //是否E0100
                    string strAuditPointAttribute;  //是否是著作 2升等 3著作 (著作boolAuditPointAttribute=True)      
                    Boolean boolAuditPointAttribute = false;
                    vUnit = crudObject.GetVUnit(AppUnitNo.SelectedValue); //若是三級單位 往上串一層級 判斷是否是醫學系或其它科系
                    strAuditPointDepartment = vUnit.UntId2;

                    if (!"E0100".Equals(strAuditPointDepartment) && !"G0100".Equals(strAuditPointDepartment))//判斷是否是醫學系或其它科系
                    {
                        strAuditPointDepartment = "OTHER";
                    }
                    strAuditPointAttribute = AppAttributeNo.SelectedValue; //新聘類型
                    if (chkPublication.Equals(strAuditPointAttribute)) boolAuditPointAttribute = true;

                    //撈取樣板產生該申請的簽核檔案 AuditExecute 
                    if (strAuditPointDepartment.Equals("G0100")) strAuditPointDepartment = "E0100"; //跟醫學系一樣多學科 最初只有醫學系有學科 所以流程樣板就分醫學系與其他系所
                    arrayList = crudObject.GetAllAuditPointRole(strAuditPointDepartment, boolAuditPointAttribute);

                    string tmpUnit;

                    //被退件的教師確認是否已產生稽核檔案:因為申請者被退件重新送件時,不用再次產生VAuditExecute檔
                    if (!this.crudObject.GetExecuteAuditorAnyOne(vApplyAudit.AppSn))
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
                            //以下為醫學院底下的二級單位, 非醫學系下的三級單位, 所以各自有各自的系所承辦
                            //呼吸治療學系 (E0400) 醫學科學研究所(E1300)  臨床醫學研究所(E1400)  國際醫學研究碩士學位學程(E2700)  國際醫學研究博士學位學程(E2800)  細胞治療與再生醫學國際博士學位學程(E3100)  人工智慧醫療碩士在職專班籌備處(E3300)
                            string exceptionDept = " E0400、E1300、E1400、E2700、E2800、E3100、E3300";
                            //醫學系&藥學系系承辦人皆為醫學系承辦人，其餘學系&藥學系學科抓自己的承辦人
                            DataTable dtRole = null;
                            if (AppUnitNo.SelectedValue.Substring(0, 1).Equals("E") ||
                                (AppUnitNo.SelectedValue.Substring(0, 1).Equals("G") && !AppUnitNo.SelectedValue.Substring(4, 1).Equals("0")))
                            {
                                if (vAuditPointRole.AuditPointSn.Equals("1"))
                                { //學科
                                    dtRole = crudObject.GetUnderTakerByDeptPointSn(AppUnitNo.SelectedValue, vAuditPointRole.AuditPointSn);
                                }
                                //系教評承辦人,院教評承辦人,院經理
                                if ((vAuditPointRole.AuditPointSn.Equals("3") || vAuditPointRole.AuditPointSn.Equals("4")) || vAuditPointRole.AuditPointSn.Equals("5"))
                                {
                                    //針對醫學院底下的二級單位, 非醫學系下的三級單位, 取得各系所承辦
                                    if (exceptionDept.IndexOf(AppUnitNo.SelectedValue) > 0)
                                    {
                                        dtRole = crudObject.GetUnderTakerByDeptPointSn(AppUnitNo.SelectedValue, vAuditPointRole.AuditPointSn);
                                    }
                                    else
                                    {
                                        if (AppUnitNo.SelectedValue.Substring(0, 1).Equals("E"))
                                        {
                                            dtRole = crudObject.GetUnderTakerByDeptPointSn("E0000", vAuditPointRole.AuditPointSn);
                                        }
                                        if (AppUnitNo.SelectedValue.Substring(0, 1).Equals("G"))
                                        {
                                            dtRole = crudObject.GetUnderTakerByDeptPointSn("G0000", vAuditPointRole.AuditPointSn);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //系教評承辦人
                                if (vAuditPointRole.AuditPointSn.Equals("3"))
                                {
                                    dtRole = crudObject.GetUnderTakerByDeptPointSn(AppUnitNo.SelectedValue, vAuditPointRole.AuditPointSn);
                                }
                                //院教評承辦人,院經理 
                                if (vAuditPointRole.AuditPointSn.Equals("4") || vAuditPointRole.AuditPointSn.Equals("5"))
                                {
                                    //20201022 除通識教育中心外 D2300，其餘院級皆取第一值+0000(ex: E0000、F0000、10000)
                                    string dpt_id = AppUnitNo.SelectedValue;
                                    if (dpt_id.Substring(0, 3) == "D23")
                                        dpt_id = "D2300";
                                    else
                                        dpt_id = dpt_id.Substring(0, 1) + "0000";
                                    dtRole = crudObject.GetUnderTakerByDeptPointSn(dpt_id, vAuditPointRole.AuditPointSn);
                                    //dtRole = crudObject.GetUnderTakerByDeptPointSn(AppUnitNo.SelectedValue.Substring(0, 1)+"0000", vAuditPointRole.AuditPointSn);
                                }
                            }
                            if (dtRole != null)
                            {
                                vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                            }

                            //本人資料設定
                            if (vAuditPointRole.AuditPointStage.Equals("2") && vAuditPointRole.AuditPointStep.Equals("3"))
                            {
                                vAuditExecute.ExecuteAuditorSnEmpId = ""; //不是校內人員不會有員編
                                vAuditExecute.ExecuteAuditorName = vEmp.EmpNameCN;
                                vAuditExecute.ExecuteAuditorEmail = vEmp.EmpEmail;


                                vAuditExecute.ExecuteAuditorEmail = setting.LoginMail;
                            }
                            //新聘升等不同人 新聘075005  升等085020 (越弄越複雜)
                            if (vAuditPointRole.AuditPointSn.Equals("9"))
                            {
                                dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId("104086"); //2018.911 新聘劉亭吟075005 改為 104086 劉伊芝

                                if (dtRole != null)
                                {
                                    vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                    vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                    vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                                }
                            }

                            //在資料表AuditPointRole中，已指定人，用員編(AuditPointRoleLevel)
                            if (vAuditPointRole.AuditPointSn.Equals("6") ||
                                vAuditPointRole.AuditPointSn.Equals("7") ||
                                vAuditPointRole.AuditPointSn.Equals("8") ||
                                vAuditPointRole.AuditPointSn.Equals("11") ||
                                vAuditPointRole.AuditPointSn.Equals("12") ||
                                vAuditPointRole.AuditPointSn.Equals("13")
                                )
                            {
                                dtRole = crudObject.GetVEmployeeFromTmuHrByEmpId(vAuditPointRole.AuditPointRoleLevel);
                                vAuditExecute.ExecuteAuditorSnEmpId = dtRole.Rows[0][0].ToString();
                                vAuditExecute.ExecuteAuditorName = dtRole.Rows[0][1].ToString();
                                vAuditExecute.ExecuteAuditorEmail = dtRole.Rows[0][2].ToString();
                            }
                            //讀取每一簽核人員開放權限的起始與終止日
                            vAuditPeroid = crudObject.GetAuditPeriod(vAuditExecute.ExecuteStage);
                            vAuditExecute.ExecuteBngDate = vAuditPeroid.AuditPeroidBeginDate;
                            vAuditExecute.ExecuteEndDate = vAuditPeroid.AuditPeroidEndDate;
                            vAuditExecute.ExecuteStatus = false;
                            crudObject.Insert(vAuditExecute);
                            //請參照AuditPointRole AuditPointSn AuditPointStage AuditPointStep
                            //人資處 7 (Stage=4 Step=1) 091058 ; 9 (Stage=4 Step=3) 085020;11 (Stage=7 Step=1) 091058 (Stage=7 Step=2) 085020
                            //7	校教評承辦人102220
                            //8	副人資長087036
                            //9	人資長097200
                            //11校教評承辦人102220
                            //12副人資長087036
                            //13人資長097200
                        }
                    }
                    //送出Email給HR
                    //Email
                    VSendEmail vSendEmail = new VSendEmail();
                    vSendEmail.MailToAccount = "up_group@tmu.edu.tw";
                    vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
                    vSendEmail.MailSubject = vEmp.EmpNameCN + "由「教師聘任升等作業系統」 申請『" + vUnit.UntNameFull + "』系所的教師";
                    vSendEmail.ToAccountName = "系統管理者";
                    vSendEmail.MailContent = "請確認送出新聘申請文件，" + vEmp.EmpNameCN + "的文件已進行簽核！！&nbsp;&nbsp; <a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入查看</a> ";
                    //寄發Email通知
                    Mail mail = new Mail();
                    //if (mail.SendEmail(vSendEmail))
                    //{
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您已成功送出申請，已通知審核者並進入應聘審查流程!');", true);
                    //}

                    //送出Email給第一位審查員
                    VAuditExecute vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(vApplyAudit.AppSn); //vApplyAudit.AppSn 

                    //Email
                    VApplyerData vApplyerData = crudObject.GetApplyDataForEmail(vApplyAudit.AppSn);

                    if (vAuditExecuteNextOne != null && vApplyerData != null)
                    {
                        vSendEmail = new VSendEmail();
                        vSendEmail.MailToAccount = vAuditExecuteNextOne.ExecuteAuditorEmail;
                        vSendEmail.MailFromAccount = "hr@tmu.edu.tw";
                        vSendEmail.MailSubject = "「教師聘任升等作業系統」有申請文件--請盡速簽核";
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
                            vSendEmail.MailContent = "<br> 「教師聘任升等作業系統」需您的核准!<br><font color=red>請再完成您的審核</font>,申請者才能進入下一階段的審核.<br> " + strTableData + " <br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入審核</a>  您的帳號:" + vAccountForManage.AcctEmail + " 密碼:校內使用的密碼 <br/><br><br><br><br>人資處 怡慧(2028) 伊芝(2066)  <br>";


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
                    //LoadDataBtn_Click(sender, e);

                    string parameters = "ApplyerID=" + DES.Encrypt(EmpIdno.Text);
                    //Response.Redirect("~/ApplyEmp.aspx?" + parameters);   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您已成功送出申請，已通知審核者並進入應聘流程!\\n可查看審核狀態!'); window.location='Default.aspx';", false);
                    #endregion
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('" + strErrMsg + "');", true);
            }


        }

        //開放給其他有權限異動者
        protected void EmpBaseModifySave_Click(object sender, EventArgs e)
        {
            TransferDataToEmpObject();
            SaveEmpObjectToBackupDB(sender, e);
        }

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
                    parameters = "ApplyerID=" + DES.Encrypt(Session["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=1&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
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
            string path = "ApplyPrint.aspx?" + parameters;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + path + "','_blank','toolbar=0,menubar=0,location=0,scrollbars=1,resizable=1,height=800,width=1000') ;", true);
        }

        protected void BtnResumeDownload_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('履歷資料下載中.....請稍後....');", true);
            ResumePDF resumePDF = new ResumePDF();
            String strMessage = "";
            resumePDF.Output(this, Int32.Parse(Session["AppSn"].ToString()), ref strMessage);
            if (!String.IsNullOrEmpty(strMessage))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('履歷資料下載有問題....請洽資訊人員....');", true);
            }
        }

        //處裡所有上傳檔案
        protected void ProcessUploadFiles(int subfolder)
        {
            string location = Server.MapPath(Global.FileUpPath + subfolder + "/");
            string strFolderPath = Request.PhysicalApplicationPath + Global.PhysicalFileUpPath + subfolder + "\\";

            //建立使用者所屬資料夾
            DirectoryInfo DIFO = new DirectoryInfo(strFolderPath);
            DIFO.Create();
            string fileName;
            int filesize = 0;
            //upload
            if (fileLists.Count > 0)
            {
                foreach (FileUpload file in fileLists)
                {
                    try
                    {
                        if (file.PostedFile != null)
                        {
                            fileName = Path.GetFileName(file.PostedFile.FileName);
                            //大小限制
                            filesize = file.PostedFile.ContentLength;
                            if (filesize > 20100000)
                            {
                                MessageLabel.Text += "<br>上傳失敗:" + file.PostedFile.FileName + " 檔案大小:" + file.PostedFile.ContentLength + " 檔案類型:" + file.PostedFile.ContentType;
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
                foreach (FileUpload file in fileLists)
                {
                    try
                    {
                        if (file.PostedFile != null)
                        {
                            fileName = Path.GetFileName(file.PostedFile.FileName);
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
                                        Response.Write("<script>alert('檔案名稱含特殊符號!請重新上傳');</script>");
                                }
                            else
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
        protected void LoadData()
        {
            MessageLabel.Text = "";
            if (String.IsNullOrEmpty(EmpIdno.Text))
            {
                MessageLabel.Text = "目前無您的資料：請輸入相關資料並開始新聘申請!";
            }
            else
            {

                string tEmpIdno = EmpIdno.Text;
                VEmployeeBase vEmp;
                vEmp = crudObject.GetEmpBsaseObj(tEmpIdno);
                if (vEmp != null)
                {
                    Session["EmpSn"] = vEmp.EmpSn;

                    if (!String.IsNullOrEmpty(vEmp.EmpBirthDay) && vEmp.EmpBirthDay.Length == 7)
                    {
                        ddl_EmpBirthYear.SelectedValue = vEmp.EmpBirthDay.Substring(0, 3);
                        ddl_EmpBirthMonth.SelectedValue = vEmp.EmpBirthDay.Substring(3, 2);
                        ddl_EmpBirthDate.SelectedValue = vEmp.EmpBirthDay.Substring(5, 2);
                    }
                    EmpNameCN.Text = vEmp.EmpNameCN;
                    AuditNameCN.Text = vEmp.EmpNameCN;

                    //應徵科系
                    AppUnitNo.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                    AppUnitNo.DataTextField = "unt_name_full";      //在此輸入的是資料表的欄位名稱

                    //Load ApplyAudit來確認是否管理端
                    crudObject.GetApplyAuditObjByIdno(tEmpIdno);
                    DataTable dataSource;
                    if (Session["ManageEmpId"] != null && (!String.IsNullOrEmpty(Session["ManageEmpId"].ToString()) || (vApplyAudit.AppStatus != false)))
                        dataSource = crudObject.GetOpenUnitUp();
                    else
                        dataSource = crudObject.GetOpenUnit();

                    AppUnitNo.DataSource = dataSource;


                    if (Session["AppUnitNo"] != null && !String.IsNullOrEmpty(Session["AppUnitNo"].ToString()))
                    {
                        foreach (DataRow dr in dataSource.Rows)
                        {
                            if (Convert.ToString(dr["unt_id"]).Equals(Session["AppUnitNo"].ToString()))
                            {
                                AppUnitNo.SelectedValue = Session["AppUnitNo"].ToString();
                                break;
                            }
                        }
                    }

                    AppUnitNo.DataBind();

                    AppJobTypeNo.DataValueField = "JobAttrNo";
                    AppJobTypeNo.DataTextField = "JobAttrName";
                    AppJobTypeNo.DataSource = crudObject.GetAllJobTypeNoName();
                    AppJobTypeNo.DataBind();
                    //新聘類型 已具部定教師資格1 學位2 著作3 臨床教師新聘4
                    AppAttributeNo.DataValueField = "status";
                    AppAttributeNo.DataTextField = "note";
                    AppAttributeNo.DataSource = crudObject.GetApplyHrAttribute().DefaultView;
                    AppAttributeNo.DataBind();

                    for (int i = 0; i < EmpSex.Items.Count; i++)
                    {
                        EmpSex.Items[i].Selected = vEmp.EmpSex.Equals(EmpSex.Items[i].Value);
                    }

                    EmpCell.Text = vEmp.EmpCell;
                    EmpExpertResearch.Text = vEmp.EmpExpertResearch;

                    string location = Global.FileUpPath + vEmp.EmpSn + "/";



                    //最高學歷證件上傳 上傳畢業證書
                    if (vEmp.EmpDegreeUploadName != null && !vEmp.EmpDegreeUploadName.Equals(""))
                    {
                        EmpDegreeUploadCB.Checked = true;
                        EmpDegreeHyperLink.NavigateUrl = GetHyperLink(vEmp.EmpDegreeUploadName);
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
                    EmpNowJobOrg.Text = vEmp.EmpNowJobOrg;

                    //TeachExp TeachCa TeachHornour 狀態資料帶出
                    if (!this.IsPostBack)
                    {
                        if (vEmp.EmpNoTeachExp)
                            CBNoTeachExp.Checked = true;
                    }
                    //載入ApplyAudit共用延伸資料
                    if (!String.IsNullOrEmpty(TbAppSn.Text))
                    {
                        vApplyAudit = crudObject.GetApplyAuditObjByAppSn(Int32.Parse(TbAppSn.Text));
                        VYear.Text = vApplyAudit.strAppYear;
                        VSemester.Text = vApplyAudit.strAppSemester;
                    }

                    if (Object.Equals(null, vApplyAudit))
                    {
                        //讀取歷史資料最後一次申請資料
                        vApplyAudit = crudObject.GetApplyAuditObjLastOne(vEmp.EmpSn);
                        if (!Object.Equals(null, vApplyAudit))
                        {
                            vApplyAudit.AppStatus = false; //無論前一次狀態是成功或失敗;
                            vApplyAudit.AppSn = 0;
                            TbAppSn.Text = " ";
                        }
                    }

                    //指定單位
                    if (!Object.Equals(null, vApplyAudit))
                    {

                        //論文積分相關
                        Session["ResearchYear"] = vApplyAudit.AppResearchYear;

                        //顯示新聘類型 AppEAttributeNo ViewState["ApplyAttributeNo"].ToString()
                        for (int i = 0; i < AppAttributeNo.Items.Count; i++)
                        {
                            if (ViewState["ApplyAttributeNo"].ToString().Equals(AppAttributeNo.Items[i].Value))
                            {
                                AppAttributeNo.Items[i].Selected = true;
                                AuditAttributeName.Text = AppAttributeNo.Items[i].ToString();
                            }
                            else
                            {
                                AppAttributeNo.Items[i].Selected = false;
                            }
                        }

                        //應徵單位 開放時間不存在之單位
                        bool existUnit = false;

                        string strApplyMore = Request["ApplyMore"] != null ? Request["ApplyMore"].ToString() : "";
                        if (strApplyMore.Equals("True"))
                        {
                            strApplyMore = "";
                            AppUnitNo.Items.Insert(0, "請選擇");
                            BtnApplyMore.Visible = false;
                        }
                        else
                        {
                            if (String.IsNullOrEmpty(vApplyAudit.AppUnitNo))
                            {
                                AppUnitNo.Items.Insert(0, "請選擇");
                            }
                            else
                            {
                                for (int i = 0; i < AppUnitNo.Items.Count - 1; i++)
                                {
                                    if (vApplyAudit.AppUnitNo.Equals(AppUnitNo.Items[i].Value))
                                    {
                                        AppUnitNo.Items[i].Selected = true;
                                        existUnit = true;
                                        //應徵單位
                                        AuditUnit.Text = crudObject.GetUnitName(AppUnitNo.SelectedValue).Rows.Count == 0 ? "" : crudObject.GetUnitName(AppUnitNo.SelectedValue).Rows[0][0].ToString();
                                    }
                                    else
                                    {
                                        AppUnitNo.Items[i].Selected = false;
                                    }
                                }

                                //萬一開放時間被關閉時
                                if (!existUnit && !String.IsNullOrEmpty(vApplyAudit.AppUnitNo.Trim()))
                                {
                                    //若該申請人員，本學年新申請，加入請選擇
                                    if (crudObject.GetApplyAuditObjByIdno(vApplyAudit.EmpIdno) == null)
                                    {
                                        AppUnitNo.Items.Insert(0, "請選擇");
                                        AppUnitNo.Items[0].Selected = true;
                                        AuditUnit.Text = "";
                                    }
                                    else
                                    {
                                        string untName = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                                        AppUnitNo.Items.Insert(0, new ListItem(untName, vApplyAudit.AppUnitNo));
                                        //應徵單位
                                        AuditUnit.Text = crudObject.GetUnitName(AppUnitNo.SelectedValue).Rows.Count == 0 ? "" : crudObject.GetUnitName(AppUnitNo.SelectedValue).Rows[0][0].ToString();
                                    }
                                }
                            }
                        }

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
                                        AuditJobTitle.Text = crudObject.GetJobTitleName(AppJobTitleNo.SelectedValue);
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
                                    AuditJobTitle.Text = crudObject.GetJobTitleName(AppJobTitleNo.SelectedValue);
                                }
                                else
                                {
                                    AppJobTitleNo.Items[i].Selected = false;
                                }
                            }
                            AddELawNumControls(chkApply, ViewState["AppJobTitleNo"].ToString(), vApplyAudit.AppLawNumNo);
                        }

                        //應徵專兼任別--職別
                        if (String.IsNullOrEmpty(vApplyAudit.AppJobTypeNo))
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
                                    AuditJobType.Text = crudObject.GetJobTypeName(AppJobTypeNo.SelectedValue).Rows.Count == 0 ? "" : crudObject.GetJobTypeName(AppJobTypeNo.SelectedValue).Rows[0][0].ToString();
                                }
                                else
                                {
                                    AppJobTypeNo.Items[i].Selected = false;
                                }
                            }
                        }

                        //兼任 且不是臨床
                        if (AppJobTypeNo.SelectedValue.Equals("2") && !ViewState["ApplyAttributeNo"].ToString().Equals(chkClinical))
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
                        AuditAttributeName.Text = (crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows.Count == 0 ? "" : Request.QueryString["ApplyAttributeNo"] == null ? crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows[0][1].ToString() : AuditAttributeName.Text);
                        ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
                        String[] strStatus = { "待送出", "審核中", "審核中", "審核中", "審核中", "審核中", "審核中", "審核中", "審核中", "審核結束" };
                        AuditStatusName.Text = strStatus[Int32.Parse(vApplyAudit.AppStage)];


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


                        if (String.IsNullOrEmpty(EmpSelfTeachExperience.Text))
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

                        //教師履歷CV
                        if (!String.IsNullOrEmpty(vApplyAudit.AppResumeUploadName))
                        {
                            AppResumeUploadCB.Checked = true;
                            AppResumeHyperLink.NavigateUrl = GetHyperLink(vApplyAudit.AppResumeUploadName);
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
                        if (!String.IsNullOrEmpty(vApplyAudit.ThesisScoreUploadName))
                        {
                            ThesisScoreUploadCB.Checked = true;

                            ThesisScoreUploadHyperLink.NavigateUrl = GetHyperLink(vApplyAudit.ThesisScoreUploadName);
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

                        //下載推薦函
                        if (!String.IsNullOrEmpty(vApplyAudit.AppRecommendUploadName))
                        {
                            RecommendUploadCB.Checked = true;
                            RecommendHyperLink.NavigateUrl = GetHyperLink(vApplyAudit.AppRecommendUploadName);
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

                        if (!AppJobTitleNo.SelectedValue.Equals("") && !AppJobTitleNo.SelectedValue.Equals("請選擇"))
                        {
                            switch (AppJobTitleNo.SelectedValue)
                            {
                                case "030400":
                                    ItemNo.Text = num[1];
                                    lbchose.Text = "依臨床教師聘任辦法第(三";
                                    break;
                                case "040400":
                                    ItemNo.Text = num[1];
                                    lbchose.Text = "依臨床教師聘任辦法第(四";
                                    break;
                                case "050400":
                                    ItemNo.Text = num[1];
                                    lbchose.Text = "依臨床教師聘任辦法第(五";
                                    break;
                                case "060400":
                                    ItemNo.Text = num[0];
                                    lbchose.Text = "依臨床教師聘任辦法第(六";
                                    break;
                                default:
                                    ItemNo.Text = num[(7 - Int32.Parse(AppJobTitleNo.SelectedValue.Substring(1, 1)))];
                                    break;
                            }
                        }

                        VAppendDegree vAppendDegree = new VAppendDegree();


                        GVTeachEdu.DataBind();
                        GVTeachExp.DataBind();

                        #region 問卷調查
                        if (vApplyAudit.AppSn != 0)
                        {
                            vApplyAudit.Questionnaires = this.questionnaireService.Get(vApplyAudit.AppSn);

                            foreach (ListItem item in this.cblQuestionnaire.Items)
                            {
                                item.Selected = vApplyAudit.Questionnaires.Any(q => q.QuestionnaireID.ToString() == item.Value);
                                if (item.Value == "6" && item.Selected)
                                    this.txtQuestionnaireOther.Text = vApplyAudit.Questionnaires.Where(q => q.QuestionnaireID.ToString() == "6").First().ItemContent;
                            }
                        }
                        #endregion
                    }

                    //

                    MessageLabel.Text += "您的基本資料載入!!";

                    //若在系統開放期間
                    if (crudObject.GetDuringOpenDate("1"))
                    {
                        //核對現在是否開放的系所
                        DataTable dt = crudObject.GetOpenUnit();


                        //顯示目前簽核狀態
                        if (vApplyAudit != null && vApplyAudit.AppStatus)
                        {
                            //送出 Button Enabled false
                            EmpBaseSave.Enabled = false;
                            EmpBaseTempSave.Enabled = false;
                            SelfReviewSave.Enabled = false;
                            TeachEduSave.Enabled = false;
                            TeachExpSave.Enabled = false;
                        }
                        else
                        {
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
                            }
                            else
                            {
                                if ((flag && !vApplyAudit.AppStatus) ||
                                    (!vApplyAudit.AppStage.Equals("0") && !vApplyAudit.AppStep.Equals("0") && !vApplyAudit.AppStatus)
                                    ) //現在開放且未送出 或已送出退回者
                                {
                                    EmpBaseSave.Enabled = true;
                                    EmpBaseTempSave.Enabled = true;
                                    SelfReviewSave.Enabled = true;
                                    TeachEduSave.Enabled = true;
                                    TeachExpSave.Enabled = true;
                                }
                                else
                                {
                                    EmpBaseSave.Enabled = false;
                                    EmpBaseTempSave.Enabled = false;
                                    SelfReviewSave.Enabled = false;
                                    TeachEduSave.Enabled = false;
                                    TeachExpSave.Enabled = false;
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
                        }
                        else
                        {
                            EmpBaseSave.Enabled = false;
                            EmpBaseTempSave.Enabled = false;
                            SelfReviewSave.Enabled = false;
                            TeachEduSave.Enabled = false;
                            TeachExpSave.Enabled = false;
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
                        BtnApplyMore.Visible = false;
                        Reminder.Visible = false;
                    }

                    if (vApplyAudit.AppStage.Equals("2") && vApplyAudit.AppStep.Equals("3"))
                    {
                        EmpBaseSave.Enabled = true;
                        EmpBaseTempSave.Enabled = true;
                        SelfReviewSave.Enabled = true;
                        TeachEduSave.Enabled = true;
                        TeachExpSave.Enabled = true;
                    }


                    //若舊資料帶出來應該走繁式，重新導過去。
                    TransferDataToEmpObject();
                    SaveEmpObjectToDB();
                    //應徵單位：醫療科 
                    //應徵職稱：針對臨床，專案都要填寫所有資料 Titno 後面是400 或 500
                    //應徵職別：兼職
                    if (!String.IsNullOrEmpty(AppJobTitleNo.SelectedValue) && !String.IsNullOrEmpty(AppJobTypeNo.SelectedValue) && !AppJobTitleNo.SelectedValue.Equals("請選擇") && AppJobTypeNo.SelectedValue.Equals("請選擇"))
                    {
                        if (AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("400") || //臨床教授
                            AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("500") || //專案教授
                            AppJobTypeNo.SelectedValue.Equals("2") || //兼職
                            untArray.Contains(AppUnitNo.SelectedValue)) //或醫療學科
                        {
                            ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                            string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text);
                            String strUrl = "ApplyEmp.aspx?" + parameters;
                            Page.ClientScript.RegisterStartupScript(typeof(Page), "return", "alert('請詳填資料!!');window.location='" + strUrl + "';", true);
                        }
                    }
                }
                else
                {
                    MessageLabel.Text = "抱歉，您未曾申請過資料!!";
                }
            }
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
                if (String.IsNullOrEmpty(TeachEduSchool.Text) || String.IsNullOrEmpty(TeachEduDepartment.Text))
                {
                    if (String.IsNullOrEmpty(TeachEduSchool.Text))
                        msg += "學校名稱未填寫! ";
                    if (String.IsNullOrEmpty(TeachEduDepartment.Text))
                        msg += "系所名稱未填寫! ";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachEdu').modal('show'); });</script>", false);
                }
                else
                {
                    VTeacherEdu vTeachEdu = new VTeacherEdu();
                    vTeachEdu.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                    vTeachEdu.EduLocal = TeachEduLocal.SelectedValue;
                    vTeachEdu.EduSchool = TeachEduSchool.Text;
                    vTeachEdu.EduDepartment = TeachEduDepartment.Text;
                    vTeachEdu.EduDegree = TeachEduDegree.SelectedValue;
                    vTeachEdu.EduStartYM = ddl_TeachEduStartYear.SelectedValue + ddl_TeachEduStartMonth.SelectedValue;
                    vTeachEdu.EduEndYM = ddl_TeachEduEndYear.SelectedValue + ddl_TeachEduEndMonth.SelectedValue;
                    vTeachEdu.EduDegreeType = TeachEduDegreeType.SelectedValue;

                    if (crudObject.Insert(vTeachEdu))
                        msg = "新增成功!!";
                    else
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";

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
            int intEduSn = Convert.ToInt32(lblEduSn.Text); // here we are
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
            ddl_TeachEduStartYear.SelectedValue = vTeacherEdu.EduStartYM.Substring(0, 3);
            ddl_TeachEduStartMonth.SelectedValue = vTeacherEdu.EduStartYM.Substring(3, 2);
            //TeachEduEndYM.Text = vTeacherEdu.EduEndYM;
            vTeacherEdu.EduEndYM.PadLeft(5, '0');
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
                vTeachEdu.EduSn = Convert.ToInt32(TBIntEduSn.Text);
                vTeachEdu.EduLocal = TeachEduLocal.SelectedValue;
                vTeachEdu.EduSchool = TeachEduSchool.Text;
                vTeachEdu.EduDepartment = TeachEduDepartment.Text;
                vTeachEdu.EduDegree = TeachEduDegree.SelectedValue;
                vTeachEdu.EduStartYM = ddl_TeachEduStartYear.SelectedValue + ddl_TeachEduStartMonth.SelectedValue;
                vTeachEdu.EduEndYM = ddl_TeachEduEndYear.SelectedValue + ddl_TeachEduEndMonth.SelectedValue;
                vTeachEdu.EduDegreeType = TeachEduDegreeType.SelectedValue;

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
            TeachEduLocal.DataBind();
            TeachEduDegree.DataBind();
            TeachEduDegreeType.DataBind();
            //寫入後載入下方的DataGridView
            GVTeachEdu.DataBind();
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
            TeachEduLocal.DataBind();
            TeachEduDegree.DataBind();
            TeachEduDegreeType.DataBind();
            //寫入後載入下方的DataGridView
            GVTeachEdu.DataBind();
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
                    vTeachExp.ExpOrginization = TeachExpOrginization.Text;
                    vTeachExp.ExpUnit = TeachExpUnit.Text;
                    vTeachExp.ExpJobTitle = TeachExpJobTitle.Text;
                    vTeachExp.ExpStartYM = TeachExpStartYear.SelectedValue + TeachExpStartMonth.SelectedValue;
                    vTeachExp.ExpEndYM = TeachExpEndYear.SelectedValue + TeachExpEndMonth.SelectedValue;
                    if (TeachExpUploadFU.HasFile)
                    {
                        if (TeachExpUploadFU.FileName != null && CheckName(TeachExpUploadFU.FileName))
                        {
                            TeachExpUploadCB.Checked = true;
                            fileLists.Add(TeachExpUploadFU);
                            vTeachExp.ExpUploadName = TeachExpUploadFU.FileName;
                        }
                    }
                    else
                        vTeachExp.ExpUploadName = TeachExpUploadFUName.Text;


                    if (crudObject.Insert(vTeachExp))
                    {
                        //寫入後載入下方的DataGridView
                        ProcessUploadFiles(Int32.Parse(TbEmpSn.Text));
                        GVTeachExp.DataBind();
                        msg = "新增成功!!";
                    }
                    else
                    {
                        msg = "抱歉，資料新增失敗，請洽資訊人員! ";
                    }
                    TeachExpSave.Visible = true;
                    TeachExpUpdate.Visible = false;
                    TeachExpOrginization.Text = "";
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
            int intExpSn = Convert.ToInt32(lblExpSn.Text); // here we are
            TBIntExpSn.Text = intExpSn.ToString();
            VTeacherExp vTeachExp = crudObject.GetVTeacherExp(intExpSn);
            TeachExpOrginization.Text = vTeachExp.ExpOrginization;
            vTeachExp.ExpStartYM.PadLeft(5, '0');
            TeachExpStartYear.SelectedValue = vTeachExp.ExpStartYM.Substring(0, 3);
            TeachExpStartMonth.SelectedValue = vTeachExp.ExpStartYM.Substring(3, 2);
            vTeachExp.ExpEndYM.PadLeft(5, '0');
            TeachExpEndYear.SelectedValue = vTeachExp.ExpEndYM.Substring(0, 3);
            TeachExpEndMonth.SelectedValue = vTeachExp.ExpEndYM.Substring(3, 2);
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
                vTeachExp.ExpSn = Convert.ToInt32(TBIntExpSn.Text);
                vTeachExp.ExpOrginization = TeachExpOrginization.Text;
                vTeachExp.ExpStartYM = TeachExpStartYear.SelectedValue + TeachExpStartMonth.SelectedValue;
                vTeachExp.ExpEndYM = TeachExpEndYear.SelectedValue + TeachExpEndMonth.SelectedValue;
                vTeachExp.ExpUnit = TeachExpUnit.Text;
                vTeachExp.ExpJobTitle = TeachExpJobTitle.Text;
                if (TeachExpUploadFU.HasFile)
                {
                    if (TeachExpUploadFU.FileName != null && CheckName(TeachExpUploadFU.FileName))
                    {
                        TeachExpUploadCB.Checked = true;
                        fileLists.Add(TeachExpUploadFU);
                        vTeachExp.ExpUploadName = TeachExpUploadFU.FileName;
                    }
                }
                else
                {
                    vTeachExp.ExpUploadName = TeachExpUploadFUName.Text;
                }


                if (crudObject.Update(vTeachExp))
                {
                    ProcessUploadFiles(Int32.Parse(TbEmpSn.Text));
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
            TeachExpUnit.Text = "";
            TeachExpJobTitle.Text = "";
            TeachExpUploadCB.Checked = false;
            TeachExpUploadFUName.Text = "";
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
                hyperLnkTeachExp.NavigateUrl = GetHyperLink(strFileName);
                if (strFileName == null || strFileName.Equals("") || strFileName.Equals("&nbsp;"))
                {
                    hyperLnkTeachExp.Text = "無資料";
                    hyperLnkTeachExp.Visible = false;
                    lb_NoUploadExp.Visible = true;
                }
            }
        }


        protected void AppUnitNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["AppUnitNo"] = AppUnitNo.SelectedValue;
            AppJobTitleNo.DataValueField = "JobTitleNo";
            AppJobTitleNo.DataTextField = "JobTitleName";
            AppJobTitleNo.DataSource = crudObject.GetJobTitleOpenDate(AppUnitNo.SelectedValue).DefaultView;
            AppJobTitleNo.DataBind();
            Session["isLoadDataBtn"] = "N";

            //新聘類型：自動切臨床
            if (!String.IsNullOrEmpty(AppJobTitleNo.SelectedValue) && AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("400"))
            {
                AppAttributeNo.SelectedValue = "4";
                AppAttributeNo.SelectedItem.Text = "臨床教師新聘";

            }

            TransferDataToEmpObject();
            if (crudObject.GetApplyAuditObjByIdno(EmpIdno.Text) != null)
            {
                if (TbAppSn.Text.Equals(""))
                {
                    if (crudObject.GetApplyAuditIsExist(vApplyAudit.EmpIdno, AppUnitNo.Text) > 0)
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
                            vApplyAudit.AppUnitNo = AppUnitNo.SelectedValue;
                            if (Session["AppSn"] != null && !Session["AppSn"].Equals("")) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
                            crudObject.Update(vApplyAudit);
                        }
                    }
                }
                else
                {
                    vApplyAudit.AppUnitNo = AppUnitNo.SelectedValue;
                    if (Session["AppSn"] != null && !Session["AppSn"].Equals(""))
                        //vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString()); 
                        vApplyAudit.AppSn = Int32.Parse(TbAppSn.Text);
                    crudObject.Update(vApplyAudit);
                }
            }
            else
            {
                SaveEmpObjectToDB();
            }

            if (!String.IsNullOrEmpty(AppJobTitleNo.SelectedValue) && (AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("400") || //臨床教授
            AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("500") || //專案教授
            AppJobTypeNo.SelectedValue.Equals("2") ||//兼職
            untArray.Contains(AppUnitNo.SelectedValue))) //或醫療學科
            {
                ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text);
                String strUrl = "ApplyEmp.aspx?" + parameters;
                Page.ClientScript.RegisterStartupScript(typeof(Page), "return", "alert('請詳填資料!!');window.location='" + strUrl + "';", true);
            }
            else
            {
                //新聘判斷是否為多單
                ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text) + "&ApplyKindNo=" + Request["ApplyKindNo"] + "&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
                String strUrl = "ApplyShortEmp.aspx?" + parameters;
                Page.ClientScript.RegisterStartupScript(typeof(Page), "return", "window.location='" + strUrl + "';", true);
            }
        }

        //職稱選定
        protected void AppJobTitleNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] num = { "零", "一", "二", "三", "四" };
            AddELawNumControls(chkApply, AppJobTitleNo.SelectedValue, "");
            ViewState["AppJobTitleNo"] = AppJobTitleNo.SelectedValue;
            AppJobTypeNo.DataValueField = "JobAttrNo";
            AppJobTypeNo.DataTextField = "JobAttrName";
            AppJobTypeNo.DataSource = crudObject.GetJobTypeOpenDate(AppUnitNo.SelectedValue, AppJobTitleNo.SelectedValue).DefaultView;
            AppJobTypeNo.DataBind();

            //新聘類型：自動切臨床
            if (AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("400"))
            {
                AppAttributeNo.SelectedValue = "4";
                AppAttributeNo.SelectedItem.Text = "臨床教師新聘";
            }
            //新聘類型：專案教授只有專任
            if (AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("500"))
            {
                AppJobTypeNo.SelectedValue = "1";
                AppJobTypeNo.SelectedItem.Text = "專任";
            }

            TransferDataToEmpObject();
            vApplyAudit.AppJobTitleNo = AppJobTitleNo.SelectedValue;
            vApplyAudit.AppJobTypeNo = AppJobTypeNo.SelectedValue; //沒有使用ajax 變動時立即儲存
            if (Session["AppSn"] != null) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
            crudObject.Update(vApplyAudit);
            Session["isLoadDataBtn"] = "N";
            //應徵單位：醫療科 
            //應徵職稱：針對臨床，專案都要填寫所有資料 Titno 後面是400 或 500
            //應徵職別：兼職

            if (AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("400") || //臨床教授
                AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("500") || //專案教授
                AppJobTypeNo.SelectedValue.Equals("2") || //兼職
                untArray.Contains(AppUnitNo.SelectedValue)) //或醫療學科
            {
                ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text);
                String strUrl = "ApplyEmp.aspx?" + parameters;
                Page.ClientScript.RegisterStartupScript(typeof(Page), "return", "alert('請詳填資料!!');window.location='" + strUrl + "';", true);
            }


        }

        //職別選定
        protected void AppJobTypeNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] num = { "零", "一", "二", "三", "四" };
            ViewState["AppJobTitleNo"] = AppJobTitleNo.SelectedValue;

            TransferDataToEmpObject();
            vApplyAudit.AppJobTitleNo = AppJobTitleNo.SelectedValue;
            vApplyAudit.AppJobTypeNo = AppJobTypeNo.SelectedValue; //沒有使用ajax 變動時立即儲存
            if (Session["AppSn"] != null) vApplyAudit.AppSn = Int32.Parse(Session["AppSn"].ToString());
            crudObject.Update(vApplyAudit);
            Session["isLoadDataBtn"] = "N";
            Session["isLoadDataBtn"] = "N";

            //應徵單位：醫療科 
            //應徵職稱：針對臨床，專案都要填寫所有資料 Titno 後面是400 或 500
            //應徵職別：兼職

            if (AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("400") || //臨床教授
                AppJobTitleNo.SelectedValue.Substring(3, 3).Equals("500") || //專案教授
                AppJobTypeNo.SelectedValue.Equals("2") || //兼職
                untArray.Contains(AppUnitNo.SelectedValue)) //或醫療學科
            {
                ViewState["ApplyAttributeNo"] = AppAttributeNo.SelectedValue;
                string parameters = "ApplyerID=" + DES.Encrypt(ViewState["ApplyerID"].ToString()) + "&AppSn=" + DES.Encrypt(TbAppSn.Text);
                String strUrl = "ApplyEmp.aspx?" + parameters;
                Page.ClientScript.RegisterStartupScript(typeof(Page), "return", "alert('請詳填資料!!');window.location='" + strUrl + "';", true);
            }


        }


        private void AddELawNumControls(string kindNo, string jobTitleNo, string selectedNum)
        {
            if (!String.IsNullOrEmpty(jobTitleNo))
            {
                DataTable dtELaw = crudObject.GetTeacherLaw(kindNo, jobTitleNo);
                ELawNum.DataSource = dtELaw;
                ELawNum.DataTextField = "LawContent";
                ELawNum.DataValueField = "LawItemNo";
                ELawNum.DataBind();
                for (int i = 0; i < dtELaw.Rows.Count; i++)
                    ELawNum.Items[i].Selected = dtELaw.Rows[i][0].Equals(selectedNum);
            }
        }

        protected void CBNoTeachExp_OnCheckedChanged(object sender, EventArgs e)
        {
            TeachExpSave.Enabled = !CBNoTeachExp.Checked;
        }


        protected String GetHyperLink(string strFileName)
        {
            String openFile = Global.FileUpPath + Session["EmpSn"].ToString() + "/" + strFileName;
            if (String.IsNullOrEmpty(strFileName) || strFileName.Equals("&nbsp;")) //DB欄位無資料時
                return "javascript:void(window.open('" + Global.FileUpPath + "NoUploadFile.pdf','_blank','location=no,height=800','width=800') )";
            else
                return "javascript:void(window.open('" + openFile + "','_blank','location=no,height=800','width=800') )";
        }

        protected bool CheckName(string strFileName)
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

        protected void lkb_logout_Click(object sender, EventArgs e)
        {
            Session["LoginEmail"] = null;
            Session.Abandon();
            Response.Redirect("http://hr2sys.tmu.edu.tw/HRApply/default.aspx");
        }

        protected void teachEduAdd_Click(object sender, EventArgs e)
        {
            TeachEduSave.Visible = true;
            TeachEduUpdate.Visible = false;
            TeachEduLocal.SelectedValue = "TTO";
            TeachEduSchool.Text = "";
            TeachEduDepartment.Text = "";
            TeachEduDegree.SelectedValue = "50";
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
            TeachExpStartYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            TeachExpStartMonth.SelectedValue = "01";
            TeachExpEndYear.SelectedValue = (DateTime.Now.Year - 1911).ToString();
            TeachExpEndMonth.SelectedValue = "01";
            TeachExpUploadFU.Dispose();
            TeachExpHyperLink.Text = "";
            TeachExpUploadCB.Checked = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalView", "<script>$(function() { $('#modalTeachExp').modal('show'); });</script>", false);
        }
        private void allDDL_refresh()
        {
            ddl_EmpBirthYear.Items.Clear();
            ddl_EmpBirthDate.Items.Clear();
            ddl_TeachEduStartYear.Items.Clear();
            ddl_TeachEduEndYear.Items.Clear();
            TeachExpStartYear.Items.Clear();
            TeachExpEndYear.Items.Clear();
            int nowY = DateTime.Now.Year - 1911;
            for (int i = nowY; i >= 30; i--)
            {
                ddl_EmpBirthYear.Items.Add(new ListItem(i.ToString(), i.ToString("000")));
                ddl_TeachEduStartYear.Items.Add(new ListItem(i.ToString(), i.ToString("000")));
                ddl_TeachEduEndYear.Items.Add(new ListItem(i.ToString(), i.ToString("000")));
                TeachExpStartYear.Items.Add(new ListItem(i.ToString(), i.ToString("000")));
            }
            for (int i = 160; i >= 30; i--)
                TeachExpEndYear.Items.Add(new ListItem(i.ToString(), i.ToString("000")));

            for (int i = 1; i <= 31; i++)
            {
                ddl_EmpBirthDate.Items.Add(new ListItem(i.ToString(), i.ToString("00")));
            }
            ddl_EmpBirthYear.DataBind();
            ddl_TeachEduStartYear.DataBind();
            ddl_TeachEduEndYear.DataBind();
            TeachExpStartYear.DataBind();
            TeachExpEndYear.DataBind();
        }
        protected string checkSheets()
        {
            string errorMsg = "";
            string empBase = ""; //基本資料
            string empTeach = ""; //學歷資料
            string empTeachExp = ""; //經歷資料

            #region 基本資料檢測

            if (AppJobTypeNo.SelectedValue == "請選擇")
                empBase += "應徵職別未選、";
            if (String.IsNullOrEmpty(ELawNum.SelectedValue))
                empBase += "法規依據未選、";
            if (String.IsNullOrEmpty(EmpNameCN.Text))
                empBase += "姓名未填、";
            if (EmpSex.SelectedValue == "請選擇")
                empBase += "性別未選、";
            if (String.IsNullOrEmpty(EmpCell.Text))
                empBase += "手機未填、";
            if (String.IsNullOrEmpty(EmpNowJobOrg.Text))
                empBase += "現任機關及職務未填、";
            if (String.IsNullOrEmpty(AppRecommendors.Text))
                empBase += "推薦人姓名未填、";
            if (String.IsNullOrEmpty(EmpExpertResearch.Text))
                empBase += "學術專長及研究未填、";
            if (String.IsNullOrEmpty(EmpDegreeHyperLink.Text))
                empBase += "最高學歷證件上傳(pdf)未上傳、";
            if (String.IsNullOrEmpty(AppResumeHyperLink.Text))
                empBase += "履歷表CV未上傳、";
            if (String.IsNullOrEmpty(ThesisScoreUploadHyperLink.Text))
                empBase += "論文積分表未上傳、";

            bool q = false;
            bool otherContent = false;
            foreach (ListItem item in this.cblQuestionnaire.Items)
            {
                if (item.Selected)
                    q = true;
                if (item.Selected && item.Value == "6" && String.IsNullOrEmpty(this.txtQuestionnaireOther.Text))
                    otherContent = true;

            }
            if (q == false)
                empBase += "問卷調查未填寫、";
            if (otherContent)
                empBase += "問卷調查其他選項未填寫內容、";


            if (!String.IsNullOrEmpty(empBase))
                errorMsg += "基本資料-" + empBase.Substring(0, empBase.Length - 1) + "\\n\\n";


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
            return errorMsg;
        }
    }
}
