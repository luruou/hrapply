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
    public partial class ApplyPrint : System.Web.UI.Page
    {

        VEmployeeBase vEmp = new VEmployeeBase();
        VApplyAudit vApplyAudit = new VApplyAudit();
        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();

        protected void Page_Load(object sender, EventArgs e)
        {
            crudObject = new CRUDObject();
            vEmp = crudObject.GetEmpBsaseObjByEmpSn(Request["EmpSn"].ToString());
            Session["EmpSn"] = Request["EmpSn"].ToString();
            Session["AppSn"] = Request["AppSn"].ToString();
            if (!this.IsPostBack && vEmp != null)
            {
                //載入ApplyAudit共用延伸資料                   
                vApplyAudit = crudObject.GetApplyAuditObjByIdno(Session["AppSn"].ToString());
                //英文名字
                EmpNameENFirst.Text = vEmp.EmpNameENFirst;
                EmpNameENLast.Text = vEmp.EmpNameENLast;
                //中文名字
                EmpNameCN.Text = vEmp.EmpNameCN;
                //應徵單位
                AppUnit.Text = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows.Count == 0 ? "" : crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                //應徵職稱
                AppJobTitle.Text = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);
                //應徵職別
                AppJobType.Text = crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows.Count == 0 ? "" : crudObject.GetJobTypeName(vApplyAudit.AppJobTypeNo).Rows[0][0].ToString();
                //指定申請類別 著作 學位
                AppAttributeName.Text = crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows.Count == 0 ? "" : crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows[0][1].ToString();

                LawText.Text = "依教師聘任升等實施辦法第(二)條第 ";
                //法規第幾項
                String[] num = { "零", "一", "二", "三", "四" , "五"};
                
                if (!vApplyAudit.AppJobTitleNo.ToString().Equals("") && !vApplyAudit.AppJobTitleNo.ToString().Equals("請選擇"))
                {

                    ItemLabel.Text = num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)))];
                }
                //載入法規依據第幾款
                LawNumNoLabel.Text = num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)];
                //LawNumNoLabel.Text = num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)))]; 

                //String lawItem = "" + (7-Int32.Parse(vApplyAudit.AppJobTitleNo.Substring(1,1)));
                //String titleNo = "" + (7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)));

                String titleNo = vApplyAudit.AppJobTitleNo;

                //撈取法規內容                
                String context = crudObject.GetTeacherLaw(vApplyAudit.AppKindNo, titleNo, vApplyAudit.AppLawNumNo);
                //if(context.Length > 35)
                //{
                //    LawContent.Text = context.Substring(0, 35) + "<br/>" + context.Substring(35, context.Length - 35);
                //}else{
                    LawContent.Text = context;
                //}
            
                //現職
                AppENowJobOrg.Text = vEmp.EmpNowJobOrg;
                //專長
                EmpExpertResearch.Text = vEmp.EmpExpertResearch;
                //照片
                EmpPhotoImage.ImageUrl = Global.FileUpPath + vEmp.EmpSn + "/" + vEmp.EmpPhotoUploadName;
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

