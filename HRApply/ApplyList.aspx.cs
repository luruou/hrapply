using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ApplyPromote;

public partial class ApplyList : System.Web.UI.Page
{
    //資料庫操作物件
    CRUDObject crudObject = new CRUDObject();
    ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
    String[] strKind;
    String[] strAttribute1;
    String[] strAttribute2;
    String[] strJobTitle;
    String[] strJobAttribute;
    String[] strStatus;

    DataTable dtTable;

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["AcctRole"] = "";
        DESCrypt DES = new DESCrypt();
        if (Request["ApplyerID"] != null)
        {
            try
            {
                Session["ApplyerID"] = DES.Decrypt(Request["ApplyerID"].ToString());
                //登入升等申請
                if (Request["ApplyKindNo"] != null && Request["ApplyKindNo"] == "2")
                {
                    //確認 升等申請時間 & 該學期聘單狀況(該學期升等只能有1單，且同學期不得同時申請新聘及升等)
                    if (crudObject.GetDuringOpenDate("2") && crudObject.GetApplyListCntByIdno(Session["ApplyerID"].ToString()) == 0)
                    {
                        BtnPromoteMore.Visible = true;
                    }
                    else
                    {
                        if (crudObject.GetApplyListCntByIdnoWithApply(Session["ApplyerID"].ToString(), "1") > 0)
                        {
                            lb_prompt.Text = "您於本學期已申請教師新聘，新聘教師或教師升等僅能擇一申請!";
                            lb_prompt.Visible = true;
                        }
                        BtnPromoteMore.Visible = false;
                    }
                    BtnApplyMore.Visible = false;
                }
                //登入新聘申請
                if (Request["ApplyKindNo"] != null && Request["ApplyKindNo"] == "1")
                {
                    //確認 該學期聘單狀況(同學期不得同時申請新聘及升等)
                    if (crudObject.GetApplyListCntByIdnoWithApply(Session["ApplyerID"].ToString(), "2") > 0)
                    {
                        lb_prompt.Text = "您於本學期已申請教師升等，新聘教師或教師升等僅能擇一申請!";
                        lb_prompt.Visible = true;
                        BtnApplyMore.Visible = false;
                    }
                    this.BtnPromoteMore.Visible = false;
                    //BtnPromoteMore.Visible = false;
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('請登入後使用系統'); location.href='Default.aspx';", true);
            return;
        }

        strKind = convertDTtoStringArray.GetStringArray(crudObject.GetAllAuditKindName());
        strAttribute1 = convertDTtoStringArray.GetStringArray(crudObject.GetAllAuditAttributeNameByKindNo(1)); //新聘
        strAttribute2 = convertDTtoStringArray.GetStringArray(crudObject.GetAllAuditAttributeNameByKindNo(2)); //升等
        strJobTitle = convertDTtoStringArray.GetStringArray(crudObject.GetAllJobTitleName());
        strJobAttribute = convertDTtoStringArray.GetStringArray(crudObject.GetAllJobTypeName());
        strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());

        if (!IsPostBack)
        {
            if (Session["ApplyerID"] != null)
            {
                dtTable = crudObject.GetApplyListByIdno(Session["ApplyerID"].ToString(), Request["ApplyKindNo"]); //所有申請中的資料
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('時間逾時,請重新登入!'); location.href='Default.aspx';", true);
            }
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] {
                                                   new DataColumn("EmpNameCN", System.Type.GetType("System.String")),
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
                                                   new DataColumn("EmpSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppSn",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStage1",System.Type.GetType("System.String")),
                                                   new DataColumn("AppStep",System.Type.GetType("System.String")),
                                                   new DataColumn("AppBuildDate",System.Type.GetType("System.String"))
                                                   });
            //DataRow dr;
            String strReturn = "";
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                strReturn = dtTable.Rows[i][6].ToString().Equals("0") ? "待送出" : dtTable.Rows[i][14].ToString().Equals("True") ? "" : "退回本人";
                if (strReturn == "")
                    strReturn = "審核中";
                dt.Rows.Add(
                    dtTable.Rows[i][0].ToString(),
                    strKind[Int32.Parse(dtTable.Rows[i][1].ToString())],
                    dtTable.Rows[i][1].ToString().Equals("1") ? strAttribute1[Int32.Parse(dtTable.Rows[i][2].ToString())] : strAttribute2[Int32.Parse(dtTable.Rows[i][2].ToString())],
                    dtTable.Rows[i][3].ToString(),
                    crudObject.GetJobTitleName(dtTable.Rows[i][4].ToString()),
                    strJobAttribute[Int32.Parse((dtTable.Rows[i][5].ToString().Equals("")) ? "0" : dtTable.Rows[i][5].ToString())],
                    strReturn,
                    //strStatus[Int32.Parse(dtTable.Rows[i][6].ToString())] + strReturn,
                    dtTable.Rows[i][7].ToString(),
                    dtTable.Rows[i][8].ToString(),
                    dtTable.Rows[i][9].ToString(),
                    dtTable.Rows[i][10].ToString(),
                    dtTable.Rows[i][11].ToString(),
                    dtTable.Rows[i][12].ToString(),
                    dtTable.Rows[i][6].ToString(),
                    dtTable.Rows[i][13].ToString(),
                    dtTable.Rows[i][15].ToString()
                   );
            }
            GVAuditData.DataSource = dt;
            GVAuditData.DataBind();
            GVAuditData.Visible = true;
        }
    }


    protected void BtnApplyMore_Click(object sender, EventArgs e)
    {
        if (Session["ApplyerID"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        DESCrypt DES = new DESCrypt();
        string parameters = "ApplyerID=" + DES.Encrypt(Session["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=" + Request["ApplyKindNo"] + "&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
        //parameters = Uri.EscapeDataString(parameters);
        Session["AppMore"] = true;
        Response.Redirect("~/ApplyShortEmp.aspx?" + parameters);
    }
    protected void BtnPromoteMore_Click(object sender, EventArgs e)
    {
        if (Session["ApplyerID"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            DESCrypt DES = new DESCrypt();
            string parameters = "ApplyerID=" + DES.Encrypt(Session["ApplyerID"].ToString()) + "&ApplyMore=True&ApplyKindNo=" + Request["ApplyKindNo"] + "&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"];
            //parameters = Uri.EscapeDataString(parameters);
            Session["AppMore"] = true;
            Response.Redirect("~/PromoteEmp.aspx?" + parameters);
        }
    }

    protected void GVApplyData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HyperLink hyperLnkApplyData;
        HyperLink hyperLinkAuditReport;
        Label textBoxAppSn;
        Label textBoxAppStage;
        Label textBoxAppStep;
        Label textBoxIdno;
        Label UntName;
        Label AppJobAttribute;
        Label AppJobTitle;
        int intStage = 0;
        int intStep = 0;
        int rowNum = 0;
        DESCrypt DES = new DESCrypt();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            hyperLnkApplyData = (HyperLink)e.Row.FindControl("HyperLinkApplyData");
            hyperLinkAuditReport = (HyperLink)e.Row.FindControl("HyperLinkAuditReport");
            textBoxAppSn = (Label)e.Row.FindControl("LbAppSn");
            textBoxAppStage = (Label)e.Row.FindControl("LbAppStage");
            textBoxAppStep = (Label)e.Row.FindControl("LbAppStep");
            textBoxIdno = (Label)e.Row.FindControl("LbEmpIdno");
            intStage = Int32.Parse(textBoxAppStage.Text.ToString());
            intStep = Int32.Parse(textBoxAppStep.Text.ToString());
            UntName = (Label)e.Row.FindControl("LbUntName");
            AppJobAttribute = (Label)e.Row.FindControl("LbAppJobAttribute");
            AppJobTitle = (Label)e.Row.FindControl("LbAppJobTitle");

            string shortApply = "詳填學科:內科學科,感染科,消化內科,胸腔內科,腎臟內科,風溼免疫科,心臟血管內科,血液腫瘤科,新陳代謝科,外科學科,一般外科,心臟血管外科,神經外科,胸腔外科,婦產學科,小兒學科,耳鼻喉學科,眼科學科,骨科學科,泌尿學科,皮膚學科,放射線學科,神經學科,精神學科,麻醉學科,家庭醫學科,復健學科,急診學科,一般醫學學科,醫學教育暨人文學科";
            string jobAttritube = e.Row.Cells[4].Text;
            if (Request["ApplyKindNo"] != "2")
                if (intStage <= 2 && intStep <= 2)
                {
                    if (jobAttritube.Equals("兼任") || shortApply.IndexOf(UntName.Text) > 0)
                    {
                        hyperLnkApplyData.NavigateUrl = "javascript:void(window.open('./ApplyEmp.aspx?ApplyerID=" + DES.Encrypt(textBoxIdno.Text.ToString()) + "&AppSn=" + DES.Encrypt(textBoxAppSn.Text.ToString()) + "&ApplyKindNo=" + Request["ApplyKindNo"] + "&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"] + "','_self','location=no,scrollbars=1,resizable=1,height=800','width=1600') )";
                    }
                    else
                    {
                        hyperLnkApplyData.NavigateUrl = "javascript:void(window.open('./ApplyShortEmp.aspx?ApplyerID=" + DES.Encrypt(textBoxIdno.Text.ToString()) + "&AppSn=" + DES.Encrypt(textBoxAppSn.Text.ToString()) + "&ApplyKindNo=" + Request["ApplyKindNo"] + "&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"] + "','_self','location=no,scrollbars=1,resizable=1,height=800','width=1600') )";
                    }
                }
                else
                {
                    hyperLnkApplyData.NavigateUrl = "javascript:void(window.open('./ApplyEmp.aspx?ApplyerID=" + DES.Encrypt(textBoxIdno.Text.ToString()) + "&AppSn=" + DES.Encrypt(textBoxAppSn.Text.ToString()) + "&ApplyKindNo=" + Request["ApplyKindNo"] + "&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"] + "','_self','location=no,scrollbars=1,resizable=1,height=800','width=1600') )";
                }
            else
                hyperLnkApplyData.NavigateUrl = "javascript:void(window.open('./PromoteEmp.aspx?ApplyerID=" + DES.Encrypt(textBoxIdno.Text.ToString()) + "&AppSn=" + DES.Encrypt(textBoxAppSn.Text.ToString()) + "&ApplyKindNo=" + Request["ApplyKindNo"] + "&ApplyWayNo=" + Request["ApplyWayNo"] + "&ApplyAttributeNo=" + Request["ApplyAttributeNo"] + "','_self','location=no,scrollbars=1,resizable=1,height=800','width=1600') )";
            rowNum++;
        }
    }




}