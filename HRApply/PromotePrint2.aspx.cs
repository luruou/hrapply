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
    public partial class PromotePrint2 : System.Web.UI.Page
    {

        VEmployeeBase vEmp = new VEmployeeBase();
        VApplyAudit vApplyAudit = new VApplyAudit();
        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            crudObject = new CRUDObject();
            vEmp = crudObject.GetEmpBsaseObjByEmpSn(Request["EmpSn"].ToString());
            VEmpTmuHr vEmpTmuHr = crudObject.GetVEmployeeFromTmuHr(vEmp.EmpIdno);
            Session["EmpSn"] = Request["EmpSn"].ToString();
            if (!this.IsPostBack && vEmp != null)
            {
                //載入ApplyAudit共用延伸資料                   
                vApplyAudit = crudObject.GetApplyAuditObjByIdno(vEmp.EmpIdno);

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
                ItemLabel.Text = num[Int32.Parse(vApplyAudit.AppAttributeNo)];

                //載入法規依據第幾條
                LawNumNoLabel.Text = num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)];

                //String lawItem = "" + (7 - Int32.Parse(vApplyAudit.AppJobTitleNo.Substring(1, 1)));
                String titleNo = vApplyAudit.AppJobTitleNo;
                //撈取法規內容
                LawContent.Text = crudObject.GetTeacherLaw(vApplyAudit.AppKindNo, titleNo, vApplyAudit.AppLawNumNo);
            }

        }

        protected void GVTeachEdu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label labEduLocal;
            string location = Global.FileUpPath + vEmp.EmpSn + "/";
            ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();
            String[] strDegree = convertDTtoStringArray.GetStringArray(crudObject.GetAllDegreeName());
            String[] strDegreeType = convertDTtoStringArray.GetStringArray(crudObject.GetAllDegreeTypeName());
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    labEduLocal = (Label)e.Row.FindControl("LabEduLocal");
            //    if (!"TWN".Equals(labEduLocal.Text.ToString()) && !"JPN".Equals(labEduLocal.Text.ToString()))
            //    {
            //        AboutFgn.Visible = true;
            //        switchFgn = labEduLocal.Text.ToString();
            //    }
            //    if ("JPN".Equals(labEduLocal.Text.ToString()))
            //    {
            //        AboutFgn.Visible = true;
            //        AboutJPN.Visible = true;
            //        switchFgn = "JPN";
            //    }


            //    //載入外國學歷 以學位送審才需要
            //    if (switchFgn != null && !switchFgn.Equals(""))
            //    {
            //        VAppendDegree vAppendDegree = new VAppendDegree();
            //        //載入學位論文
            //        if (vApplyAudit.AppKindNo.Equals(chkApply) && vApplyAudit.AppAttributeNo.Equals(chkDegree))
            //        {
            //            vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
            //        }
            //        LoadFgn(vAppendDegree, switchFgn, location);
            //    }
            //    if (EmailEdu.Text.ToString().Equals(""))
            //    {
            //        EmailEdu.Text = e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + "至" + e.Row.Cells[4].Text.ToString();
            //    }
            //    else
            //    {
            //        EmailEdu.Text = EmailEdu.Text.ToString() + "</br>" + e.Row.Cells[1].Text.ToString() + e.Row.Cells[2].Text.ToString() + e.Row.Cells[3].Text.ToString() + "至" + e.Row.Cells[4].Text.ToString();
            //    }
                //e.Row.Cells[8].Text 
            //}
        }

        
    }
}
