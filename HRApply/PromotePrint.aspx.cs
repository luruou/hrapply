using System;
using System.Data;
using System.Drawing;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class PromotePrint : System.Web.UI.Page
    {

        VEmployeeBase vEmp = new VEmployeeBase();
        VApplyAudit vApplyAudit = new VApplyAudit();
        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();
        protected void Page_Load(object sender, EventArgs e)
        {
            //GetSettings setting = new GetSettings();
            //year.Text = setting.GetYear();
            //if (setting.GetSemester() == "1")
            //    semester.Text = "一";
            //else
            //    semester.Text = "二";
            crudObject = new CRUDObject();
            if (Session["EmpSn"] == null && Session["EmpSn"].Equals(""))
            {
                return;
            }
            vEmp = crudObject.GetEmpBsaseObjByEmpSn(Session["EmpSn"].ToString());
            VEmpTmuHr vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(vEmp.EmpIdno);
            if (!this.IsPostBack && vEmp != null)
            {
                //載入ApplyAudit共用延伸資料
                //vApplyAudit = crudObject.GetApplyAuditObjByIdno(vEmp.EmpIdno);
                if (Session["AppSn"] != null && !String.IsNullOrEmpty(Session["AppSn"].ToString()))
                    vApplyAudit = crudObject.GetApplyAuditObj(Convert.ToInt32(Session["AppSn"].ToString()));
                else
                    vApplyAudit = crudObject.GetApplyAuditObj(Convert.ToInt32(Request.QueryString["AppSn"].ToString()));

                year.Text = vApplyAudit.AppYear;
                if (vApplyAudit.AppSemester == "1")
                    semester.Text = "一";
                else
                    semester.Text = "二";

                //升等職別
                AppJobTypeName.Text = crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows.Count == 0 ? "" : crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows[0][0].ToString();

                //現任專職機構名稱及職別
                EmpUnit.Text = vEmpTmuHr.EmpUntFullName;
                EmpNowEJobTitle.Text = vEmpTmuHr.EmpTitidName;
                //中文名字
                EmpNameCN.Text = vEmp.EmpNameCN;
                //指定申請類別 著作 學位
                AppAttributeName.Text = crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows.Count == 0 ? "" : crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows[0][1].ToString();
                //升等職稱
                AppJobTitle.Text = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);

                LawText.Text = "依教師聘任升等實施辦法第(二)條第 ";

                //法規第幾項
                String[] num = { "零", "一", "二", "三", "四", "五" };
                if (!vApplyAudit.AppJobTitleNo.ToString().Equals("") && !vApplyAudit.AppJobTitleNo.ToString().Equals("請選擇"))
                {

                    ItemLabel.Text = num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)))];
                }

                //載入法規依據第幾款
                LawNumNoLabel.Text = num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)];
                //LawNumNoLabel.Text = num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)))]; 

                //String lawItem = "" + (7 - Int32.Parse(vApplyAudit.AppJobTitleNo.Substring(1, 1)));
                String titleNo = vApplyAudit.AppJobTitleNo;
                //撈取法規內容
                String context = crudObject.GetTeacherLaw(vApplyAudit.AppKindNo, titleNo, vApplyAudit.AppLawNumNo);
                //if(context.Length > 35)
                //{
                //    LawContent.Text = context.Substring(0, 35) + "<br/>" + context.Substring(35, context.Length - 35);
                //}else{
                LawContent.Text = context;
                //}
            }

            if (EmpUnit.Text == "" && EmpNowEJobTitle.Text == "")
                lb_NoEmpUnit.Visible = true;
            else
                lb_NoEmpUnit.Visible = false;

            if (EmpNameCN.Text == "")
                lb_NoEmpNameCN.Visible = true;
            else
                lb_NoEmpNameCN.Visible = false;

            if (AppAttributeName.Text == "")
                lb_NoAppAttributeName.Visible = true;
            else
                lb_NoAppAttributeName.Visible = false;
            if (AppJobTitle.Text == "")
                lb_NoAppJobTitle.Visible = true;
            else
                lb_NoAppJobTitle.Visible = false;
            if (LawText.Text == "" && ItemLabel.Text == "" && LawNumNoLabel.Text == "" && LawContent.Text == "")
            {
                div_law.Visible = false;
                lb_NoLaw.Visible = true;
            }
            else
            {
                div_law.Visible = true;
                lb_NoLaw.Visible = false;
            }
            if (GridView1.Rows.Count == 0)
                lb_NoTMUTeachExp.Visible = true;
            else
                lb_NoTMUTeachExp.Visible = false;
            if (GVTeachEdu.Rows.Count == 0)
                lb_NoTeachEdu.Visible = true;
            else
                lb_NoTeachEdu.Visible = false;
            if (GVTeachExp.Rows.Count == 0)
                lb_NoTeachExp.Visible = true;
            else
                lb_NoTeachExp.Visible = false;
            if (GVTeachLesson.Rows.Count == 0)
                lb_NoTeachLesson.Visible = true;
            else
                lb_NoTeachLesson.Visible = false;

        }

        protected void GVTeachEdu_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
