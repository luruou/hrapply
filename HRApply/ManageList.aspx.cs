using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;

namespace ApplyPromote
{
    public partial class _ManageList : PageBaseManage
    {
        DataTable dtTable;
        DataTable dt;
        DataTable dtHidden;//隱藏dt For 一般審查人
        DataTable dtHidden2;//隱藏dt For 人資

        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();
        ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
        String[] strKind;
        String[] strAttribute1;
        String[] strAttribute2;
        String[] strJobTitle;
        String[] strJobAttribute;
        String[] strStatus;
        static string AcctAuditorSnEmpId;
        static string AcctRole;

        protected void Page_Load(object sender, EventArgs e)
        {


            strKind = convertDTtoStringArray.GetStringArray(crudObject.GetAllAuditKindName());
            strAttribute1 = convertDTtoStringArray.GetStringArray(crudObject.GetAllAuditAttributeNameByKindNo(1)); //新聘
            strAttribute2 = convertDTtoStringArray.GetStringArray(crudObject.GetAllAuditAttributeNameByKindNo(2)); //升等
            strJobTitle = convertDTtoStringArray.GetStringArray(crudObject.GetAllJobTitleName());
            strJobAttribute = convertDTtoStringArray.GetStringArray(crudObject.GetAllJobTypeName());
            strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());

            if (!IsPostBack)
            {
                if (Request.QueryString["AcctRole"] != null && Session["LoginEmail"] != null)
                //if (Session["AcctRole"] != null)
                {
                    Label lb_LoginNM = (Label)Master.FindControl("lb_LoginNM");
                    if (lb_LoginNM != null && Session["AcctAuditorSnEmpName"] != null)
                    {
                        lb_LoginNM.Text = Session["AcctAuditorSnEmpName"].ToString() + "&nbsp;您好!&nbsp;&nbsp;";
                        lb_LoginNM.Visible = true;
                    }
                    DESCrypt DES = new DESCrypt();
                    AcctRole = DES.Decrypt(Request.QueryString["AcctRole"].ToString());
                    Session["AcctRole"] = AcctRole;
                    //AcctRole = Session["AcctRole"].ToString();
                    AcctAuditorSnEmpId = DES.Decrypt(Request.QueryString["AuditorSnId"].ToString());
                     Session["AcctAuditorSnEmpId"]= AcctAuditorSnEmpId;
                    //AcctAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString();
                    //20171211 發現林哲瑋(ccwu@tmu.edu.tw)單位代碼多了\r\n
                    if (AcctAuditorSnEmpId.IndexOf("\r") > 0)
                        AcctAuditorSnEmpId=AcctAuditorSnEmpId.Substring(0, AcctAuditorSnEmpId.IndexOf("\r"));
                    if (Session["AcctRoleName"] != null) AcctRoleName.Text = Session["AcctRoleName"].ToString();
                    if (AcctRoleName.Text.Equals("")) AcctRoleName.Text = crudObject.GetTitle(AcctAuditorSnEmpId);
                    //判斷是否是管理者或一般權限者


                    BtnOutput.Visible = true; //非管理者才需要
                        LbOutput.Visible = true;
                        if (CBNowAudit.Checked)
                        {
                            dtTable = crudObject.GetApplyDataByAuditorNow(AcctAuditorSnEmpId, SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString());   
                            //看到自己可以審核的資料-- 要抓輪到自己審核的
                        }
                        else
                        {
                            dtTable = crudObject.GetApplyDataByAuditor(AcctAuditorSnEmpId, SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString()); 
                            //看到自己可以審核的資料-- 所有經手的[目前是後者,並顯示已審,待審]
                        }
                        SelUnit.Visible = true;
                        approveUnt.DataTextField = "unt_name_full";
                        approveUnt.DataValueField = "unt_data";
                        DataTable dt = crudObject.GetAllSupUntLevel(AcctAuditorSnEmpId);

                        //針對維護單號：20200427013  彭巧穎101071 身兼多個祕書進行特別處理
                        //針對維護單號：20201217005  林以涵104216 其餘人資查出 莊筑鈞108125 韓蕙如107122
                        if (dt.Rows.Count > 0 && AcctAuditorSnEmpId != "101071" && AcctAuditorSnEmpId != "104216"
                             && AcctAuditorSnEmpId != "108125" && AcctAuditorSnEmpId != "107122" && AcctAuditorSnEmpId != "103207" && AcctAuditorSnEmpId != "099012")
                        {
                            approveUnt.DataSource = dt;
                        }
                        else
                        {
                            //approveUnt.DataSource = crudObject.GetEmpUnitSelf(AcctAuditorSnEmpId); 
                            //改抓UnderTake所屬部門
                            dt = crudObject.GetUnderTakerPointDept(AcctAuditorSnEmpId);
                            approveUnt.DataSource = dt;
                            //抓該員工自己的部門 院級
                            if (dt.Rows.Count == 0)
                            {
                                approveUnt.DataSource = crudObject.GetEmpUnitID(AcctAuditorSnEmpId);
                            }
                        }
                        approveUnt.DataBind();
                        approveUnt.Items.Insert(0, "會經手資料");
                        SelectMethod.SelectedValue = "2";
                        LbSelectMethod.Text = "需您經手審核的資料";

                    if (dtTable.Rows.Count == 0)
                        ApplyDataMessage.Text = "您沒有須審核的文件!";
                    //else
                    //{
                    //    GVAuditData.DataSource = dtTable;
                    //    GVAuditData.DataBind();
                    //    GVAuditData.Visible = true;
                    //}
                }
                else
                {
                    ShowSessionTimeOut();
                    Response.Redirect("~/ManageLogin.aspx");
                }
            }
        }

        protected void GVApplyData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            HyperLink hyperLnkApplyData;
            HyperLink hyperLinkAuditReport;
            Label textBoxEmpSn;
            Label textBoxAppSn;
            int rowNum = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                hyperLnkApplyData = (HyperLink)e.Row.FindControl("HyperLinkApplyData");
                hyperLinkAuditReport = (HyperLink)e.Row.FindControl("HyperLinkAuditReport");
                textBoxEmpSn = (Label)e.Row.FindControl("LbEmpSn");
                textBoxAppSn = (Label)e.Row.FindControl("LbAppSn");
                hyperLnkApplyData.NavigateUrl = "javascript:void(window.open('./ManageSetAudit.aspx?EmpSn=" + textBoxEmpSn.Text.ToString() + "&AppSn=" + textBoxAppSn.Text.ToString() + "','_blank',config='height=800,width=1600') )";

                if (AcctRole != null && AcctRole.Contains("M"))
                {

                }
                else
                {
                    hyperLnkApplyData.Text = "進入";
                }  
                rowNum++;
            }
        }

        protected void SelConjunct1_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchCondition.Style.Remove("display");
            if (SelConjunct1.SelectedValue == "-")
            {
                TxtBox1.Visible = false;
                SelChoose1.Visible = false;
            }
            else
            {
                switch (SelItem1.SelectedValue.ToString())
                {
                    case "unt_name_full":
                    case "b.EmpIdno":
                    case "b.EmpNameCN":
                        TxtBox1.Visible = true;
                        SelChoose1.Visible = false;
                        break;
                    case "AppJobTypeNo":
                        TxtBox1.Visible = false;
                        SelChoose1.Visible = true;
                        SelChoose1.DataValueField = "JobAttrNo";
                        SelChoose1.DataTextField = "JobAttrName";
                        SelChoose1.DataSource = crudObject.GetAllJobTypeNoName().DefaultView;
                        SelChoose1.DataBind();
                        break;
                    case "AppJobTitleNo":
                        TxtBox1.Visible = false;
                        SelChoose1.Visible = true;
                        SelChoose1.DataValueField = "JobTitleNo";
                        SelChoose1.DataTextField = "JobTitleName";
                        SelChoose1.DataSource = crudObject.GetAllJobTitleNoName().DefaultView;
                        SelChoose1.DataBind();
                        break;
                    case "AppAttributeNo":
                        TxtBox1.Visible = false;
                        SelChoose1.Visible = true;
                        SelChoose1.DataValueField = "AttributeNo";
                        SelChoose1.DataTextField = "AttributeName";
                        SelChoose1.DataSource = crudObject.GetAuditAttributeByKindNo(Convert.ToInt32(SelectKind.SelectedValue)).DefaultView;
                        SelChoose1.DataBind();
                        break;
                    case "AppStage":
                        TxtBox1.Visible = false;
                        SelChoose1.Visible = true;
                        SelChoose1.DataValueField = "AuditProgressNo";
                        SelChoose1.DataTextField = "AuditProgressName";
                        SelChoose1.DataSource = crudObject.GetAllAuditProcgressNoName().DefaultView;
                        SelChoose1.DataBind();
                        break;
                }
            }
        }

        protected void SelConjunct2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (SelConjunct2.SelectedValue == "-")
            {
                TxtBox2.Visible = false;
                SelChoose2.Visible = false;
            }
            else
            {
                switch (SelItem2.SelectedValue.ToString())
                {
                    case "unt_name_full":
                    case "b.EmpIdno":
                    case "b.EmpNameCN":
                        TxtBox2.Visible = true;
                        SelChoose2.Visible = false;
                        break;
                    case "AppJobTypeNo":
                        TxtBox2.Visible = false;
                        SelChoose2.Visible = true;
                        SelChoose2.DataValueField = "JobAttrNo";
                        SelChoose2.DataTextField = "JobAttrName";
                        SelChoose2.DataSource = crudObject.GetAllJobTypeNoName().DefaultView;
                        SelChoose2.DataBind();
                        break;
                    case "AppJobTitleNo":
                        TxtBox2.Visible = false;
                        SelChoose2.Visible = true;
                        SelChoose2.DataValueField = "JobTitleNo";
                        SelChoose2.DataTextField = "JobTitleName";
                        SelChoose2.DataSource = crudObject.GetAllJobTitleNoName().DefaultView;
                        SelChoose2.DataBind();
                        break;
                    case "AppAttributeNo":
                        TxtBox2.Visible = false;
                        SelChoose2.Visible = true;
                        SelChoose2.DataValueField = "AttributeNo";
                        SelChoose2.DataTextField = "AttributeName";
                        SelChoose2.DataSource = crudObject.GetAuditAttributeByKindNo(Convert.ToInt32(SelectKind.SelectedValue)).DefaultView;
                        SelChoose2.DataBind();
                        break;
                    case "AppStage":
                        TxtBox2.Visible = false;
                        SelChoose2.Visible = true;
                        SelChoose2.DataValueField = "AuditProgressNo";
                        SelChoose2.DataTextField = "AuditProgressName";
                        SelChoose2.DataSource = crudObject.GetAllAuditProcgressNoName().DefaultView;
                        SelChoose2.DataBind();
                        break;
                }
            }
        }

        protected void SelConjunct3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (SelConjunct3.SelectedValue == "-")
            {
                TxtBox3.Visible = false;
                SelHidden3.Visible = false;
                SelChoose3.Visible = false;
            }
            else
            {
                switch (SelItem3.SelectedValue)
                {
                    case "b.EmpIdno":
                        SelChoose3.Visible = false;
                        TxtBox3.Visible = true;
                        SelHidden3.Value = "=";
                        break;
                    case "b.EmpNameCN":
                        SelChoose3.Visible = false;
                        TxtBox3.Visible = true;
                        SelHidden3.Value = "like";
                        break;
                    case "AppAttributeNo":
                        SelChoose3.Visible = true;
                        TxtBox3.Visible = false;
                        SelChoose3.DataValueField = "AttributeNo";
                        SelChoose3.DataTextField = "AttributeName";
                        SelChoose3.DataSource = crudObject.GetAuditAttributeByKindNo(Convert.ToInt32(SelectKind.SelectedValue)).DefaultView;
                        SelChoose3.DataBind();
                        SelHidden3.Value = "=";
                        break;
                    case "unt_name_full":
                        SelChoose3.Visible = false;
                        TxtBox3.Visible = true;
                        SelHidden3.Value = "like";
                        break;
                    case "AppJobTitleNo":
                        SelChoose3.Visible = true;
                        TxtBox3.Visible = false;
                        SelChoose3.DataValueField = "JobTitleNo";
                        SelChoose3.DataTextField = "JobTitleName";
                        SelChoose3.DataSource = crudObject.GetAllJobTitleNoName().DefaultView;
                        SelChoose3.DataBind();
                        SelHidden3.Value = "=";
                        break;
                    case "AppJobTypeNo":
                        SelChoose3.Visible = true;
                        TxtBox3.Visible = false;
                        SelChoose3.DataValueField = "JobAttrNo";
                        SelChoose3.DataTextField = "JobAttrName";
                        SelChoose3.DataSource = crudObject.GetAllJobTypeNoName().DefaultView;
                        SelChoose3.DataBind();
                        SelHidden3.Value = "=";
                        break;
                    case "AppStage":
                        SelChoose3.Visible = true;
                        TxtBox3.Visible = false;
                        SelChoose3.DataValueField = "AuditProgressNo";
                        SelChoose3.DataTextField = "AuditProgressName";
                        SelChoose3.DataSource = crudObject.GetAllAuditProcgressNoName().DefaultView;
                        SelChoose3.DataBind();
                        SelHidden3.Value = "=";
                        break;
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //'XX'型別 必須置於有 runat=server 的表單標記之中
        }

        protected void BtnExport_OnClick(object sender, EventArgs e)
        {
            GVOutputDataForHR.Visible = true;//匯出時需要顯示否則下載抓GV時會無資料

            DataSet ds = new DataSet();
            ConnectionStringSettings connstring = WebConfigurationManager.ConnectionStrings["tmuConnectionString"];
            using (SqlConnection conn1 = new SqlConnection(connstring.ConnectionString))
            {
                conn1.Open();
                if (Session["strWhere"] == null)
                    Session["strWhere"] = "";
                SqlDataAdapter dr = new SqlDataAdapter("SELECT distinct '' as '序號', a.AppYear as '學年度', a.AppSemester as '學期', a.EmpSn as '申請人編號', a.AppSn as '申請單號', b.EmpSex as '性別',c.DeptName as '學院',ISNULL(c.CollegeName,c.DeptName) as '系所', c.unt_name_full as '聘任單位',b.EmpNameCN as '姓名', f.JobAttrName as '專兼任別', g.JobTitleName as '應聘等級', i.AuditProgressName as '申請狀態', e.KindName as '新聘升等', f.JobAttrName as '應徵職別', b.EmpIdno as '身份證字號', b.EmpBirthDay as '生日', b.EmpEmail as 'e-mail', b.EmpTelPri as '連絡電話1', b.EmpTelPub as '連絡電話2', b.EmpCell as '連絡電話3',a.AppThesisAccuScore as '論文積分',  case when j.RPIDiscountTotal is not null then j.RPIDiscountTotal  else '0' end as '折抵積分', a.AppRPIScore as 'RPI', ISNULL((select d.[ThesisName] from [ThesisScore] d where  a.EmpSn=d.EmpSn and d.IsRepresentative='1' FOR XML PATH('')),  ISNULL((SELECT[AppDDegreeThesisName] from[AppendDegree] zz  where zz.AppSn = a.AppSn),  ISNULL((SELECT[AppDDegreeThesisNameEng] from[AppendDegree] z  where z.AppSn = a.AppSn), '')))   AS '代表著作/學位論文',(select EduSchool + ' ' + EduDepartment + ' ' + CASE when S4.DegreeName IS NULL THEN '' when S4.DegreeName = '' THEN '' ELSE S4.DegreeName END  + ' ' + EduStartYM + '~' + EduEndYM +';  ' from[TeacherEdu] as S1  LEFT JOIN CDegree AS S4 ON S1.EduDegree = S4.DegreeNo WHERE S1.EmpSn = a.EmpSn FOR XML PATH('')) AS '學歷',( select ExpOrginization + ' ' + CASE when ExpJobTitle IS NULL THEN '' when ExpJobTitle = '' THEN '' ELSE ExpJobTitle END  + ' ' + ExpStartYM + '~' + ExpEndYM + ';  ' from TeacherExp as S2  WHERE S2.EmpSn = a.EmpSn FOR XML PATH('')) AS '經歷'  ,a.AppBuildDate as '申請案建立日期',a.AppModifyDate as '申請案送出日期'  FROM ApplyAudit a JOIN[EmployeeBase] b ON a.EmpSn = b.EmpSn  INNER JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.dpt_id = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.unt_id2 = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS JOIN[dbo].[CAuditKind] e ON a.AppKindNo = e.KindNo  JOIN[dbo].[CJobAttribute] f ON a.AppJobTypeNo = f.JobAttrNo JOIN[dbo].[CJobTitle] g ON a.AppJobTitleNo = g.JobTitleNo INNER JOIN[dbo].[AuditProgressStatus] i ON a.AppStage = i.AuditProgressNo   LEFT JOIN[dbo].[AppendPromote] j on a.AppSn = j.AppSn " + Session["strWhere"].ToString()  + " order by c.unt_name_full", conn1);
                    dr.Fill(ds);
            }
            GVOutputDataForHR.DataSource = ds.Tables[0];
            GVOutputDataForHR.DataBind();
            Context.Response.Clear();
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Context.Response.Charset = "utf-8";
            Context.Response.AddHeader("content-disposition", "attachment;filename=export.xls");
            Context.Response.ContentType = "application/x-excel";

            Context.Response.Write("<html><head><style>\n");
            Context.Response.Write(@"TD {mso-number-format:\@;mso-ignore:colspan;}\n");
            Context.Response.Write("@br {mso-data-placement:same-cell;})");
            Context.Response.Write("</style></head><body>");
            System.IO.StringWriter myStreamWriter = new System.IO.StringWriter();
            HtmlTextWriter myHtmlTextWriter = new HtmlTextWriter(myStreamWriter);
            this.GVOutputDataForHR.AllowPaging = false;
            this.GVOutputDataForHR.RenderControl(myHtmlTextWriter);
            Context.Response.Write(myStreamWriter.ToString());
            Context.Response.End();
            this.GVOutputDataForHR.AllowPaging = true;
            GVOutputDataForHR.Visible = false;//匯出後需要隱藏
        }
        protected void BtnOutput_OnClick(object sender, EventArgs e)
        {
            GVOutputData.Visible = true;
            Context.Response.Clear();
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>");
            Context.Response.Charset = "utf-8";
            Context.Response.AddHeader("content-disposition", "attachment;filename=export.xls");
            Context.Response.ContentType = "application/x-excel";

            Context.Response.Write("<html><head><style>\n");
            Context.Response.Write(@"TD {mso-number-format:\@;mso-ignore:colspan;}\n");
            Context.Response.Write("@br {mso-data-placement:same-cell;})");
            Context.Response.Write("</style></head><body>");
            System.IO.StringWriter myStreamWriter = new System.IO.StringWriter();
            HtmlTextWriter myHtmlTextWriter = new HtmlTextWriter(myStreamWriter);
            this.GVOutputData.AllowPaging = false;
            this.GVOutputData.RenderControl(myHtmlTextWriter);
            Context.Response.Write(myStreamWriter.ToString());
            Context.Response.End();
            this.GVOutputData.AllowPaging = true;
            GVOutputData.Visible = false;

        }
        protected void GVOutputData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GVOutputData.PageIndex * this.GVOutputData.PageSize + e.Row.RowIndex + 1; e.Row.Cells[0].Text = indexID.ToString();
            }
        }
        protected void GVOutputDataForHR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex != -1)
            {
                int indexID = this.GVOutputDataForHR.PageIndex * this.GVOutputDataForHR.PageSize + e.Row.RowIndex + 1; e.Row.Cells[0].Text = indexID.ToString();
            }
        }
        protected void SearchGo_OnClick(object sender, EventArgs e)
        {
            string StrAnd = "";
            string StrOr = "";

            DESCrypt DES = new DESCrypt();
            if (Session["AcctRole"] == null || AcctRole == null)
            {
                AcctRole = DES.Decrypt(Request.QueryString["AcctRole"].ToString());
                Session["AcctRole"] = AcctRole;
            }
            if (Session["AcctAuditorSnEmpId"] == null || AcctAuditorSnEmpId == null)
            {
                AcctAuditorSnEmpId = DES.Decrypt(Request.QueryString["AuditorSnId"].ToString());
                Session["AcctAuditorSnEmpId"] = AcctAuditorSnEmpId;
            }


            GetSettings setting = new GetSettings();
            setting.Year = Server.UrlEncode(SelYear.Text.ToString().Trim());
            setting.Semester = Server.UrlEncode(SelSemester.Text.ToString().Trim());

            if (SelConjunct1.SelectedValue.ToString() == "-" &&
               SelConjunct2.SelectedValue.ToString() == "-" &&
               SelConjunct3.SelectedValue.ToString() == "-")
                searchCondition.Style.Add("display", "none");
            else
                searchCondition.Style.Remove("display");
            
            //組成查詢SQL
            if (SelConjunct1.SelectedValue.ToString() != "-")
            {
                GetSettings getSettings = new GetSettings();
                getSettings.Execute();

                if (SelChoose1.Visible == true)
                {
                    StrAnd += " and  " + SelItem1.SelectedValue + " = '" + SelChoose1.SelectedValue.ToString() + "' ";
                }
                if (TxtBox1.Visible && !TxtBox1.Text.Equals("") && TxtBox1.Text != "請輸入查詢資料")
                {
                    if (SelHidden1.Equals("="))
                    {
                        StrAnd += " and  " + SelItem1.SelectedValue + " = '" + TxtBox1.Text.ToString() + "' ";
                    }
                    else
                    {
                        StrAnd += " and  " + SelItem1.SelectedValue + " like '%" + TxtBox1.Text.ToString() + "%' ";
                    }
                }
            }

            if (SelConjunct2.SelectedValue.ToString().Equals("and"))
            {
                if (SelChoose2.Visible == true)
                {
                    StrAnd += " and " + SelItem2.SelectedValue + " = '" + SelChoose2.SelectedValue.ToString() + "' ";
                }
                if (TxtBox2.Visible && !TxtBox2.Text.Equals(""))
                {
                    if (SelHidden2.Equals("="))
                    {
                        StrAnd += " and " + SelItem2.SelectedValue + " = '" + TxtBox2.Text.ToString() + "' ";
                    }
                    else
                    {
                        StrAnd += " and " + SelItem2.SelectedValue + " like '%" + TxtBox2.Text.ToString() + "%' ";
                    }
                }
            }
            if (SelConjunct2.SelectedValue.ToString().Equals("or"))
            {

                if (SelChoose2.Visible == true)
                {
                    StrOr += " or " + SelItem2.SelectedValue + " = '" + SelChoose2.SelectedValue.ToString() + "' ";
                }
                if (TxtBox2.Visible && !TxtBox2.Text.Equals(""))
                {
                    if (SelHidden2.Equals("="))
                    {
                        StrOr += " or " + SelItem2.SelectedValue + " = '" + TxtBox2.Text.ToString() + "' ";
                    }
                    else
                    {
                        StrOr += " or " + SelItem2.SelectedValue + " like '%" + TxtBox2.Text.ToString() + "%' ";
                    }
                }
            }

            if (SelConjunct3.SelectedValue.ToString().Equals("and"))
            {
                if (SelChoose3.Visible == true)
                {
                    StrAnd += " and " + SelItem3.SelectedValue + " = '" + SelChoose3.SelectedValue.ToString() + "' ";
                }
                if (TxtBox3.Visible && !TxtBox3.Text.Equals(""))
                {
                    if (SelHidden3.Equals("="))
                    {
                        StrAnd += " and " + SelItem3.SelectedValue + " = '" + TxtBox3.Text.ToString() + "' ";
                    }
                    else
                    {
                        StrAnd += " and " + SelItem3.SelectedValue + " like '%" + TxtBox3.Text.ToString() + "%' ";
                    }
                }
            }
            if (SelConjunct3.SelectedValue.ToString().Equals("or"))
            {
                if (SelChoose3.Visible == true)
                {
                    StrOr += " or " + SelItem3.SelectedValue + " = '" + SelChoose3.SelectedValue.ToString() + "' ";
                }
                if (TxtBox3.Visible && !TxtBox3.Text.Equals(""))
                {
                    if (SelHidden3.Equals("="))
                    {
                        StrOr += " or " + SelItem3.SelectedValue + " = '" + TxtBox3.Text.ToString() + "' ";
                    }
                    else
                    {
                        StrOr += " or " + SelItem3.SelectedValue + " like '%" + TxtBox3.Text.ToString() + "%' ";
                    }
                }
            }
            # region 系所或學科簽核
            string allUnt = "";
            //系所或學科簽核
                if (approveUnt.SelectedValue != "會經手資料")
                {
                    String untData = approveUnt.SelectedValue.ToString().Trim();
                    string untId = untData.Split('-')[0];
                    string supLevel = untData.Split('-')[1];
					
                    if (supLevel.Equals("0"))
                    {
                        dt = crudObject.GetAllUnderUntFirstLevel(untId);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dt.Rows.IndexOf(dr) == 0 && allUnt.Length == 0)
                                {
                                    allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                }
                                else
                                {
                                    allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                }
                            }
                        }
                        else
                        {
                            allUnt = "'" + untId + "'";
                        }
                    }
                    if (supLevel.Equals("1"))
                    {
                        dt = crudObject.GetAllUnderUntFirstLevel(untId);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dt.Rows.IndexOf(dr) == 0)
                                {
                                    allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                }
                                else
                                {
                                    allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                }
                            }
                        }
						else
						{
							allUnt = "'" + untId + "'";
						}
                    }
                    if (supLevel.Equals("2"))
                    {
                        dt = crudObject.GetAllUnderUntSecondLevel(untId);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (dt.Rows.IndexOf(dr) == 0)
                                {
                                    allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                }
                                else
                                {
                                    allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                }
                            }
                        }
						else
						{
							allUnt = "'" + untId + "'";
						}
                    }
                    if (supLevel.Equals("3") || supLevel.Equals("0"))
                    {
                        if(allUnt.Length == 0)
                        allUnt = "'" + untId + "'";
                        else
                            allUnt += "'" + untId + "'";
                    }
                }
                else
                {
                    for(int i =1;i< approveUnt.Items.Count; i++)
                    {
                        String untData = approveUnt.Items[i].Value.ToString();
                        string untId = untData.Split('-')[0];
                        string supLevel = untData.Split('-')[1];
						
                        if (supLevel.Equals("0"))
                        {
                            dt = crudObject.GetAllUnderUntFirstLevel(untId);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dt.Rows.IndexOf(dr) == 0 && allUnt.Length == 0)
                                    {
                                        allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                    else
                                    {
                                        allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                }
                            }
                            else
                            {
                                allUnt = "'" + untId + "'";
                            }
                        }
						
                        if (supLevel.Equals("1"))
                        {
                            dt = crudObject.GetAllUnderUntFirstLevel(untId);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dt.Rows.IndexOf(dr) == 0 && allUnt.Length==0)
                                    {
                                        allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                    else
                                    {
                                        allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                }
                            }
                            else
                            {
                                allUnt = "'" + untId + "'";
                            }
                        }
                        if (supLevel.Equals("2"))
                        {
                            dt = crudObject.GetAllUnderUntSecondLevel(untId);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dt.Rows.IndexOf(dr) == 0 && allUnt.Length==0)
                                    {
                                        allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                    else
                                    {
                                        allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                }
                            }
                            else
                            {
                                allUnt = "'" + untId + "'";
                            }
                        }
                        if (supLevel.Equals("3") )
                        {
                            if(allUnt.Length == 0)
                            allUnt = "'" + untId + "'";
                            else
                                allUnt += "'" + untId + "'";
                        }
                    }
                }
#endregion
            var method = SelectMethod.SelectedValue.ToString();
            var kind = SelectKind.SelectedValue.ToString();
            string strWhere = "";
            switch (method)
            {
                //尚未派發學年學期
                case "all":
                    LbSelectMethod.Text = "所有聘單";
                    if (!StrAnd.Equals(""))
                    {
                        strWhere = "Where  a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "' " + StrAnd;
                    }
                    else
                    {
                        strWhere = "Where  a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "' ";
                    }
                    if (!StrOr.Equals(""))
                    {
                        strWhere += StrOr;
                    }
                    if (!StrAnd.Equals("") || !StrOr.Equals(""))
                    {
                        strWhere += " ) ";
                    }
                    //系所或學科簽核
                    if (allUnt.Trim().Length <= 0)
                            allUnt = "''";
                        strWhere += " and a.AppUnitNo in (" + allUnt + ") ";
                    dtTable = crudObject.GetAllInApplyDataBySearch(strWhere);
                    Session["strWhere"] = strWhere;
                    break;


                case "0":
                    LbSelectMethod.Text = "尚未派發學年學期";
                    strWhere = " Where a.AppStatus = 'False' and a.AppStage = '0' and a.AppKindNo = '" + kind + "' and AppYear = null and AppSemester =  null " + StrAnd;
                    //系所或學科簽核
                    strWhere += " and a.AppUnitNo in (" + allUnt + ") ";
                    dtTable = crudObject.GetAllInApplyDataBySearch(strWhere);
                    break;
                //申請中
                case "1":
                    LbSelectMethod.Text = "申請中資料";
                    if (!StrAnd.Equals(""))
                    {
                        strWhere = " Where a.AppStatus = 'False' and a.AppStage = '0' and a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "' " + StrAnd;
                    }
                    else
                    {
                        strWhere = " Where a.AppStatus = 'False' and a.AppStage = '0' and a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "'";
                    }
                    if (!StrOr.Equals(""))
                    {
                        strWhere += StrOr;
                    }
                    if (!StrAnd.Equals("") || !StrOr.Equals(""))
                    {
                        strWhere += " ) ";
                    }
                    //系所或學科簽核
                        strWhere += " and a.AppUnitNo in (" + allUnt + ") ";
                    dtTable = crudObject.GetAllInApplyDataBySearch(strWhere);
                    Session["strWhere"] = strWhere;
                    break;

                //審核中
                case "2":
                    LbSelectMethod.Text = "審核中資料";
                    if (!StrAnd.Equals(""))
                    {
                        strWhere = " Where a.AppStatus = 'True' and a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "' " + StrAnd;
                    }
                    else
                    {
                        strWhere = " Where a.AppStatus = 'True' and a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "' ";
                    }
                    if (!StrOr.Equals(""))
                    {
                        strWhere += StrOr;
                    }
                    if (!StrAnd.Equals("") || !StrOr.Equals(""))
                    {
                        //strWhere += " ) ";
                    }
                    //系所或學科簽核
                    if (allUnt == "")
                            allUnt = "''";
                        strWhere += " and a.AppUnitNo in (" + allUnt + ") ";
                    dtTable = crudObject.GetAllAuditDataBySearch(strWhere);
                    Session["strWhere"] = strWhere;
                    break;

                //退回
                case "3":
                    LbSelectMethod.Text = "退回學院補件";
                    if (!StrAnd.Equals(""))
                    {
                        strWhere = "  Where a.AppStatus = 'False' and a.AppStage != '0' and a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "' " + StrAnd;
                    }
                    else
                    {
                        strWhere = " Where a.AppStatus = 'False' and a.AppStage != '0' and a.AppKindNo = '" + kind + "' and a.AppYear = '" + SelYear.SelectedValue.ToString() + "' and a.AppSemester =  '" + SelSemester.SelectedValue.ToString() + "' ";
                    }
                    if (!StrOr.Equals(""))
                    {
                        strWhere += StrOr;
                    }
                    if (!StrAnd.Equals("") || !StrOr.Equals(""))
                    {
                        //strWhere += " ) ";
                    }
                    strWhere += " and a.AppUnitNo in (" + allUnt + ") ";
                    dtTable = crudObject.GetAllReturnDataBySearch(strWhere);
                    Session["strWhere"] = strWhere;
                    break;
            }
            if (dtTable.Rows.Count == 0)
            {
                GVAuditData.DataBind();
                if ("M".Equals(AcctRole)) //管理者
                //if ("M".Equals(Session["AcctRole"].ToString())) //管理者
                {
                    ApplyDataMessage.Text = "目前沒有任何相關資料!";
                }
                else if ("A".Equals(AcctRole)) //校內審核
                //else if ("A".Equals(Session["AcctRole"].ToString())) //校內審核
                {
                    ApplyDataMessage.Text = "您沒有須審核的資料!";
                }
                else
                {
                    ApplyDataMessage.Text = "您沒有須審核的資料! <br>請確認Email：在收到『需審核新聘申請文件』--勾選『僅顯示現在需審核資料』!!";
                }
            }
            else
            {
                ApplyDataMessage.Text = "";
                if (CBNowAudit.Checked)
                {
                    LbSelectMethod.Text = "您現在需審核資料";
                    dtTable = crudObject.GetApplyDataByAuditorNow(AcctAuditorSnEmpId, SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString());   //看到自己可以審核的資料-- 要抓輪到自己審核的
                }
                //總表修改
                dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[] {new DataColumn("EmpNameCN", System.Type.GetType("System.String")),
                                                   new DataColumn("AppKind", System.Type.GetType("System.String")),
                                                   new DataColumn("AppAttribute", System.Type.GetType("System.String")),
                                                   new DataColumn("UntName",System.Type.GetType("System.String")),
                                                   new DataColumn("AppJobTitle",System.Type.GetType("System.String")),
                                                   new DataColumn("AppJobAttribute",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStage",System.Type.GetType("System.String")),
                                                   new DataColumn("AppBirthDay",System.Type.GetType("System.String")),
                                                   new DataColumn("AppEmail",System.Type.GetType("System.String")),
                                                   new DataColumn("AppTels",System.Type.GetType("System.String")),
                                                   new DataColumn("AppEmpIdno",System.Type.GetType("System.String")),
                                                   new DataColumn("AuditRecord",System.Type.GetType("System.String")),
                                                   new DataColumn("EmpSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStep",System.Type.GetType("System.String")),
                                                   new DataColumn("AppBuildDate",System.Type.GetType("System.String")),
                                                   new DataColumn("AppModifyDate",System.Type.GetType("System.String")),
                                                   new DataColumn("DeptName",System.Type.GetType("System.String")),
                                                   new DataColumn("CollegeName",System.Type.GetType("System.String"))
                                                   });
                String strReturn = "";
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    //strReturn = dtTable.Rows[i][6].ToString().Equals("0") ? " - 未送出" : dtTable.Rows[i][17].ToString().Equals("True") ? "" : " - 退回本人";
                    strReturn = dtTable.Rows[i][6].ToString().Equals("0") ? "待送出" : dtTable.Rows[i][17].ToString().Equals("True") ? "" : "退回本人";
                    if (strReturn == "")
                        strReturn = "審核中";
                    dt.Rows.Add(
                        dtTable.Rows[i][0].ToString(),
                        strKind[Int32.Parse(dtTable.Rows[i][1].ToString())],
                        dtTable.Rows[i][1].ToString().Equals("1") ? strAttribute1[Int32.Parse(dtTable.Rows[i][2].ToString())] : strAttribute2[Int32.Parse(dtTable.Rows[i][2].ToString())],
                        dtTable.Rows[i][3].ToString(),
                        //strJobTitle[Int32.Parse(dtTable.Rows[i][4].ToString())],
                        crudObject.GetJobTitleName(dtTable.Rows[i][4].ToString()),
                        strJobAttribute[Int32.Parse((dtTable.Rows[i][5].ToString().Equals("")) ? "0" : dtTable.Rows[i][5].ToString())],
                        strStatus[Int32.Parse(dtTable.Rows[i][6].ToString())] + "-" + strReturn,
                        //strReturn,
                        dtTable.Rows[i][7].ToString(),
                        dtTable.Rows[i][8].ToString(),
                        dtTable.Rows[i][9].ToString() + "&nbsp;" + dtTable.Rows[i][10].ToString() + "&nbsp;" + dtTable.Rows[i][11].ToString(),
                        dtTable.Rows[i][12].ToString(),
                        dtTable.Rows[i][13].ToString(),
                        dtTable.Rows[i][14].ToString(),
                        dtTable.Rows[i][15].ToString(),
                        dtTable.Rows[i][16].ToString(),
                        dtTable.Rows[i][18].ToString(),
                        dtTable.Rows[i][19].ToString(),
                        dtTable.Rows[i][20].ToString(),
                        dtTable.Rows[i][21].ToString()
                       );
                }


                GVAuditData.DataSource = dt;
                GVAuditData.DataBind();
            }
            lb_count.Text = "共 " + GVAuditData.Rows.Count + "筆";

        }

        public void approveUnt_SelectedIndexChanged(object sender, EventArgs e)
        {


            DESCrypt DES = new DESCrypt();
            if (Session["AcctRole"] == null || AcctRole == null)
            {
                AcctRole = DES.Decrypt(Request.QueryString["AcctRole"].ToString());
                Session["AcctRole"] = AcctRole;
            }
            if (Session["AcctAuditorSnEmpId"] == null || AcctAuditorSnEmpId == null)
            {
                AcctAuditorSnEmpId = DES.Decrypt(Request.QueryString["AuditorSnId"].ToString());
                Session["AcctAuditorSnEmpId"] = AcctAuditorSnEmpId;
            }
			
            DataTable dt;
            String untData = approveUnt.SelectedValue.ToString().Trim();

            if (AcctRole.Equals("A"))
            //if (Session["AcctRole"].ToString().Equals("A"))
            {
                lb_btnText.Visible = false;
                //if (!untData.Equals("會經手資料"))
                //{
                //    BtnExport.Visible = true;
                //    BtnOutput.Visible = false;
                //}
                //else
                //{
                //    BtnExport.Visible = false;
                //    BtnOutput.Visible = true;
                //}
            }

            if (!untData.Equals("會經手資料") || AcctRole.Equals("M")) 
            //if (!untData.Equals("會經手資料") || Session["AcctRole"].ToString().Equals("M")) 
                SelectTableHR.Visible = true;
            else
                SelectTableHR.Visible = false;

            if (untData.Equals("會經手資料") && AcctRole != null && AcctRole.Equals("M"))
            //if (untData.Equals("會經手資料") || Session["AcctRole"] != null && Session["AcctRole"].ToString().Equals("M"))
            {

                if (untData.Equals("會經手資料") && AcctRole != null && AcctRole.Equals("M"))
                //if (untData.Equals("會經手資料") || Session["AcctRole"] != null && Session["AcctRole"].ToString().Equals("M"))
                {
                    TableRow0.Visible = true;
                    CBNowAudit.Visible = true;
                    LBNowAudit.Visible = true;
                    if (AcctAuditorSnEmpId == null) return;
                    //if (Session["AcctAuditorSnEmpId"] == null) return;
                    if (CBNowAudit.Checked)
                    {
                        LbSelectMethod.Text = "您現在需審核資料";
                        dtTable = crudObject.GetApplyDataByAuditorNow(AcctAuditorSnEmpId, SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString());   //看到自己可以審核的資料-- 要抓輪到自己審核的
                        //dtTable = crudObject.GetApplyDataByAuditorNow(Session["AcctAuditorSnEmpId"].ToString(), SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString());   //看到自己可以審核的資料-- 要抓輪到自己審核的
                    }
                    else
                    {
                        LbSelectMethod.Text = "需您經手審核的資料";
                        dtTable = crudObject.GetApplyDataByAuditor(AcctAuditorSnEmpId, SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString()); //看到自己可以審核的資料-- 所有經手的[目前是後者,並顯示已審,待審]
                        //dtTable = crudObject.GetApplyDataByAuditor(Session["AcctAuditorSnEmpId"].ToString(), SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString()); //看到自己可以審核的資料-- 所有經手的[目前是後者,並顯示已審,待審]
                    }
                    //dtTable = crudObject.GetApplyDataByAuditorNow(AcctAuditorSnEmpId);   //看到自己可以審核的資料-- 要抓輪到自己審核的

                    #region 一般申請者匯出
                    dt = new DataTable();
                    dtHidden = new DataTable();
                    dtHidden.Columns.AddRange(new DataColumn[] {new DataColumn("Name", System.Type.GetType("System.String")),
                                                   new DataColumn("UntName", System.Type.GetType("System.String")),
                                                   new DataColumn("TitleName", System.Type.GetType("System.String")),
                                                   new DataColumn("AttrName",System.Type.GetType("System.String")),
                                                   new DataColumn("NowJob",System.Type.GetType("System.String")),
                                                   new DataColumn("TotalScore",System.Type.GetType("System.String")),
                                                   new DataColumn("Degree",System.Type.GetType("System.String")),
                                                   new DataColumn("Expert",System.Type.GetType("System.String")),
                                                   new DataColumn("Sex",System.Type.GetType("System.String")),
                                                   new DataColumn("BirthDay",System.Type.GetType("System.String")),
                                                   new DataColumn("AuditWay",System.Type.GetType("System.String")),
                                                   new DataColumn("AppBuildDate",System.Type.GetType("System.String")),
                                                   new DataColumn("AppModifyDate",System.Type.GetType("System.String"))
                                                   });

                    dt.Columns.AddRange(new DataColumn[] {new DataColumn("EmpNameCN", System.Type.GetType("System.String")),
                                                   new DataColumn("AppKind", System.Type.GetType("System.String")),
                                                   new DataColumn("AppAttribute", System.Type.GetType("System.String")),
                                                   new DataColumn("UntName",System.Type.GetType("System.String")),
                                                   new DataColumn("AppJobTitle",System.Type.GetType("System.String")),
                                                   new DataColumn("AppJobAttribute",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStage",System.Type.GetType("System.String")),
                                                   new DataColumn("AppBirthDay",System.Type.GetType("System.String")),
                                                   new DataColumn("AppEmail",System.Type.GetType("System.String")),
                                                   new DataColumn("AppTels",System.Type.GetType("System.String")),
                                                   new DataColumn("AppEmpIdno",System.Type.GetType("System.String")),
                                                   new DataColumn("AuditRecord",System.Type.GetType("System.String")),
                                                   new DataColumn("EmpSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStep",System.Type.GetType("System.String")),
                                                   new DataColumn("AppBuildDate",System.Type.GetType("System.String")),
                                                   new DataColumn("AppModifyDate",System.Type.GetType("System.String"))
                                                   });

                    DataRow dr;
                    String strEmpSn = "";
                    String strAppSn = "";
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        dt.Rows.Add(
                            dtTable.Rows[i][0].ToString(),
                            strKind[Int32.Parse(dtTable.Rows[i][1].ToString())],
                            dtTable.Rows[i][1].ToString().Equals("1") ? strAttribute1[Int32.Parse(dtTable.Rows[i][2].ToString())] : strAttribute2[Int32.Parse(dtTable.Rows[i][2].ToString())],
                            dtTable.Rows[i][3].ToString(),
                            //strJobTitle[Int32.Parse(dtTable.Rows[i][4].ToString())],
                            crudObject.GetJobTitleName(dtTable.Rows[i][4].ToString()),
                            strJobAttribute[Int32.Parse((dtTable.Rows[i][5].ToString().Equals("")) ? "0" : dtTable.Rows[i][5].ToString())],
                            strStatus[Int32.Parse(dtTable.Rows[i][6].ToString())],
                            dtTable.Rows[i][7].ToString(),
                            dtTable.Rows[i][8].ToString(),
                            dtTable.Rows[i][9].ToString() + "&nbsp;" + dtTable.Rows[i][10].ToString() + "&nbsp;" + dtTable.Rows[i][11].ToString(),
                            dtTable.Rows[i][12].ToString(),
                            dtTable.Rows[i][13].ToString(),
                            dtTable.Rows[i][14].ToString(),
                            dtTable.Rows[i][15].ToString(),
                            dtTable.Rows[i][16].ToString(),
                        dtTable.Rows[i][18].ToString(),
                        dtTable.Rows[i][19].ToString()
                           );

                        if (!dtTable.Rows[i][14].ToString().Trim().Equals("") && !dtTable.Rows[i][6].ToString().Trim().Equals("0"))
                        {
                            strEmpSn = dtTable.Rows[i][14].ToString().Trim();
                            strAppSn = dtTable.Rows[i][15].ToString().Trim();
                            dr = dtHidden.NewRow();
                            dr = crudObject.GetApplyerView(strEmpSn, strAppSn).Rows[0];
                            //dtHidden.Rows.Add(dr); //這個資料列已經屬於其他資料表
                            dtHidden.ImportRow(dr);

                        }
                    }
                    #endregion

                    
                    GVAuditData.DataSource = dt;
                    GVAuditData.DataBind();
                    GVAuditData.Visible = true;

                    GVOutputData.DataSource = dtHidden;
                    GVOutputData.DataBind();
                    GVOutputData.Visible = false;

                    GVOutputDataForHR.DataSource = dtHidden;
                    GVOutputDataForHR.DataBind();
                    GVOutputDataForHR.Visible = false;
                }
                else
                {
                    if (AcctRole != null && !AcctRole.Equals("M")) //管理者
                    //if (Session["AcctRole"] != null && !Session["AcctRole"].ToString().Equals("M")) //管理者
                    {
                        LbSelectMethod.Text = "申請資料";
                        string untId = untData.Split('-')[0];
                        string supLevel = untData.Split('-')[1];
                        CBNowAudit.Visible = false;
                        LBNowAudit.Visible = false;

                        string allUnt = "";
                        if (supLevel.Equals("1"))
                        {
                            dt = crudObject.GetAllUnderUntFirstLevel(untId);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dt.Rows.IndexOf(dr) == 0)
                                    {
                                        allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                    else
                                    {
                                        allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                }
                            }
                        }
                        if (supLevel.Equals("2"))
                        {
                            dt = crudObject.GetAllUnderUntSecondLevel(untId);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dt.Rows.IndexOf(dr) == 0)
                                    {
                                        allUnt = "'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                    else
                                    {
                                        allUnt += ",'" + dr.ItemArray[0].ToString() + "'";
                                    }
                                }
                            }
                        }
                        if (supLevel.Equals("3") || supLevel.Equals("0"))
                        {
                            allUnt = "'" + untId + "'";
                        }
                        //加上自己所屬單位，先判斷前面是否已存在

                        DataTable dtTable;
                        dt = new DataTable();
                        if (!allUnt.Equals(""))
                        {
                            dtTable = crudObject.GetApplyDataByUnit(allUnt, SelYear.SelectedValue.ToString(), SelSemester.SelectedValue.ToString());
                            dt = new DataTable();
                            dtHidden = new DataTable();
                            dtHidden.Columns.AddRange(new DataColumn[] {new DataColumn("Name", System.Type.GetType("System.String")),
                                                   new DataColumn("UntName", System.Type.GetType("System.String")),
                                                   new DataColumn("TitleName", System.Type.GetType("System.String")),
                                                   new DataColumn("AttrName",System.Type.GetType("System.String")),
                                                   new DataColumn("NowJob",System.Type.GetType("System.String")),
                                                   new DataColumn("TotalScore",System.Type.GetType("System.String")),
                                                   new DataColumn("Degree",System.Type.GetType("System.String")),
                                                   new DataColumn("Expert",System.Type.GetType("System.String")),
                                                   new DataColumn("Sex",System.Type.GetType("System.String")),
                                                   new DataColumn("BirthDay",System.Type.GetType("System.String")),
                                                   new DataColumn("AuditWay",System.Type.GetType("System.String"))
                                                   });

                            dt.Columns.AddRange(new DataColumn[] {new DataColumn("EmpNameCN", System.Type.GetType("System.String")),
                                                   new DataColumn("AppKind", System.Type.GetType("System.String")),
                                                   new DataColumn("AppAttribute", System.Type.GetType("System.String")),
                                                   new DataColumn("UntName",System.Type.GetType("System.String")),
                                                   new DataColumn("AppJobTitle",System.Type.GetType("System.String")),
                                                   new DataColumn("AppJobAttribute",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStage",System.Type.GetType("System.String")),
                                                   new DataColumn("AppBirthDay",System.Type.GetType("System.String")),
                                                   new DataColumn("AppEmail",System.Type.GetType("System.String")),
                                                   new DataColumn("AppTels",System.Type.GetType("System.String")),
                                                   new DataColumn("AppEmpIdno",System.Type.GetType("System.String")),
                                                   new DataColumn("AuditRecord",System.Type.GetType("System.String")),
                                                   new DataColumn("EmpSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStep",System.Type.GetType("System.String"))
                                                   });

                            DataRow dr;
                            String strEmpSn = "";
                            String strAppSn = "";

                            for (int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                dt.Rows.Add(
                                    dtTable.Rows[i][0].ToString(),
                                    strKind[Int32.Parse(dtTable.Rows[i][1].ToString())],
                                    dtTable.Rows[i][1].ToString().Equals("1") ? strAttribute1[Int32.Parse(dtTable.Rows[i][2].ToString())] : strAttribute2[Int32.Parse(dtTable.Rows[i][2].ToString())],
                                    dtTable.Rows[i][3].ToString(),
                                    //strJobTitle[Int32.Parse(dtTable.Rows[i][4].ToString())],
                                    crudObject.GetJobTitleName(dtTable.Rows[i][4].ToString()),
                                    strJobAttribute[Int32.Parse((dtTable.Rows[i][5].ToString().Equals("")) ? "0" : dtTable.Rows[i][5].ToString())],
                                    strStatus[Int32.Parse(dtTable.Rows[i][6].ToString())],
                                    dtTable.Rows[i][7].ToString(),
                                    dtTable.Rows[i][8].ToString(),
                                    dtTable.Rows[i][9].ToString() + "&nbsp;" + dtTable.Rows[i][10].ToString() + "&nbsp;" + dtTable.Rows[i][11].ToString(),
                                    dtTable.Rows[i][12].ToString(),
                                    dtTable.Rows[i][13].ToString(),
                                    dtTable.Rows[i][14].ToString(),
                                    dtTable.Rows[i][15].ToString(),
                                    dtTable.Rows[i][16].ToString()
                                   );


                                if (!dtTable.Rows[i][14].ToString().Trim().Equals("") && !dtTable.Rows[i][6].ToString().Trim().Equals("0"))
                                {
                                    strEmpSn = dtTable.Rows[i][14].ToString().Trim();
                                    strAppSn = dtTable.Rows[i][15].ToString().Trim();
                                    dr = dtHidden.NewRow();
                                    dr = crudObject.GetApplyerView(strEmpSn, strAppSn).Rows[0];
                                    //dtHidden.Rows.Add(dr); //這個資料列已經屬於其他資料表
                                    dtHidden.ImportRow(dr);

                                }
                            }
                        }
                        GVAuditData.DataSource = dt;
                        GVAuditData.DataBind();

                        GVOutputData.DataSource = dtHidden;
                        GVOutputData.DataBind();

                        GVOutputDataForHR.DataSource = dtHidden;
                        GVOutputDataForHR.DataBind();
                    }

                    else
                    {
                        GVAuditData.DataSource = null;
                        GVAuditData.DataBind();
                    }
                }
            }

        }
    }
}
