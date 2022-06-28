using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Linq;
namespace ApplyPromote
{
    public partial class FunSelectAuditor : PageBaseManage
    {
        string selDept = "";
        CRUDObject crudObject = new CRUDObject();
        string strStage = "";
        string strStep = "";
        string deptBoss = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request["Stage"] != null && Request["Step"] != null)
                {
                    strStage = Request["Stage"].ToString();
                    strStep = Request["Step"].ToString();
                    ViewState["Stage"] = strStage;
                    ViewState["Step"] = strStep;
                }
                if (Request["empId"] != null)
                {
                    Session["EmpId"] = Request["empId"].ToString();
                }
                if (("6".Equals(strStage)) && ("1".Equals(strStep)))
                {
                    //取得外審人員分類
                    DeptLevelOne.DataValueField = "AuditorRealmSn";        //在此輸入的是資料表的欄位名稱
                    DeptLevelOne.DataTextField = "AuditorRealmName";      //在此輸入的是資料表的欄位名稱
                    DeptLevelOne.DataSource = crudObject.GetAllAuditorOuterRealm();
                    DeptLevelOne.DataBind();
                }
                else
                {
                    //取得一級部門資料
                    DeptLevelOne.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                    DeptLevelOne.DataTextField = "unt_name";      //在此輸入的是資料表的欄位名稱
                    DeptLevelOne.DataSource = crudObject.GetDeptLevelOne();
                    DeptLevelOne.DataBind();

                    DeptLevelTwo.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                    DeptLevelTwo.DataTextField = "unt_name";      //在此輸入的是資料表的欄位名稱
                    deptBoss = DeptLevelOne.SelectedValue.ToString();
                    DeptLevelTwo.DataSource = crudObject.GetDeptLevelTwo(DeptLevelOne.SelectedValue.ToString());
                    DeptLevelTwo.DataBind();
                    DeptLevelTwo.Items.Insert(0, "");

                    DeptLevelThree.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                    DeptLevelThree.DataTextField = "unt_name_full";      //在此輸入的是資料表的欄位名稱
                    DeptLevelThree.DataSource = crudObject.GetDeptLevelThree(DeptLevelTwo.SelectedValue.ToString());
                    DeptLevelThree.DataBind();
                    DeptLevelThree.Items.Insert(0, "");
                    selDept = DeptLevelTwo.SelectedValue.ToString();
                    if (selDept.ToString().Trim() == null || selDept.ToString().Trim() == "")
                    {
                        UnitEmpList.DataSource = crudObject.GetAllEmpByUnitBoss(deptBoss);
                    }
                    else
                    {
                        UnitEmpList.DataSource = crudObject.GetAllEmpByUnit(selDept);
                    }
                    UnitEmpList.DataBind();
                }
                DeptLevelThree_SelectedIndexChanged(sender, e);

            }
            else
            {
                if (ViewState["Stage"] != null)
                {
                    strStage = ViewState["Stage"].ToString();
                }
                if (ViewState["Step"] != null)
                {
                    strStep = ViewState["Step"].ToString();
                }
            }
        }


        protected void DeptLevelOne_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (("6".Equals(strStage)) && ("1".Equals(strStep)))
            {

            }
            else
            {
                //取得二級部門資料
                DeptLevelTwo.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                DeptLevelTwo.DataTextField = "unt_name";      //在此輸入的是資料表的欄位名稱
                DeptLevelTwo.DataSource = crudObject.GetDeptLevelTwo(DeptLevelOne.SelectedValue.ToString());
                deptBoss = DeptLevelOne.SelectedValue.ToString();
                DeptLevelTwo.DataBind();
                DeptLevelTwo.Items.Insert(0, "");

                DeptLevelThree.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                DeptLevelThree.DataTextField = "unt_name_full";      //在此輸入的是資料表的欄位名稱
                DeptLevelThree.DataSource = crudObject.GetDeptLevelThree(DeptLevelTwo.SelectedValue.ToString());
                DeptLevelThree.DataBind();
                DeptLevelThree.Items.Insert(0, "");
                selDept = DeptLevelTwo.SelectedValue.ToString();

                UnitEmpList.DataSource = crudObject.GetAllEmpByUnit(selDept);
                UnitEmpList.DataBind();

                selDept = DeptLevelOne.SelectedValue.ToString();
            }
            DeptLevelThree_SelectedIndexChanged(sender, e);
        }

        protected void DeptLevelTwo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (("6".Equals(strStage)) && ("1".Equals(strStep)))
            {

            }
            else
            {//取得三級部門資料
                DeptLevelThree.DataValueField = "unt_id";        //在此輸入的是資料表的欄位名稱
                DeptLevelThree.DataTextField = "unt_name_full";      //在此輸入的是資料表的欄位名稱
                DeptLevelThree.DataSource = crudObject.GetDeptLevelThree(DeptLevelTwo.SelectedValue.ToString());
                if (DeptLevelTwo.SelectedValue.ToString() == null || DeptLevelTwo.SelectedValue.ToString() == "")
                    deptBoss = DeptLevelOne.SelectedValue.ToString();
                DeptLevelThree.DataBind();
                DeptLevelThree.Items.Insert(0, "");
                selDept = DeptLevelTwo.SelectedValue.ToString();
                DeptLevelThree_SelectedIndexChanged(sender, e);
            }
        }

        protected void DeptLevelThree_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (("6".Equals(strStage)) && ("1".Equals(strStep)))
            {
                //取得指定外審類別的員工資料
                selDept = DeptLevelOne.SelectedValue.ToString();
                UnitEmpList.DataValueField = "AuditorEmail";
                UnitEmpList.DataTextField = "AuditorName";
                UnitEmpList.DataSource = crudObject.GetAllAuditorOuterByRealm(selDept);
                UnitEmpList.DataBind();
            }
            else
            {
                //取得指定部門的員工資料
                selDept = DeptLevelThree.SelectedValue.ToString();
                UnitEmpList.DataValueField = "emp_id";
                UnitEmpList.DataTextField = "emp_name";
                if (selDept.ToString().Trim() == "" || selDept.ToString().Trim() == null)
                    UnitEmpList.DataSource = crudObject.GetAllEmpByUnitBoss(deptBoss);
                else
                    UnitEmpList.DataSource = crudObject.GetAllEmpByUnit(selDept);
                UnitEmpList.DataBind();
            }
            UnitEmpList_SelectedIndexChanged(sender, e);

        }

        protected void UnitEmpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            String empId = UnitEmpList.SelectedValue.ToString();
            if (("6".Equals(strStage)) && ("1".Equals(strStep)))
            {

            }
            else
            {
                DataTable dt = crudObject.GetEmpNameEmail(empId);
                if (dt != null)
                    TextBoxEmpEmail.Text = dt.Rows[0][1].ToString();
            }

        }



        protected void BtnSelect_Click(object sender, EventArgs e)
        {
            string email = this.UnitEmpList.SelectedValue;
            string name = this.UnitEmpList.SelectedItem.Text.Replace(email, "");
            string appsn = this.Request["AppSn"].ToString();

            string empId = SQLDB.HRApply().Query<string>(@"SELECT [AcctAuditorSnEmpId] FROM [HRApply].[dbo].[AccountForAudit]
WHERE AcctAppSn = @AppSn AND AcctEmail = @Email", new { AppSn = appsn, Email = email }).FirstOrDefault();

            //string csText = $"<script type='text/javascript'> email </script>";
            StringBuilder csText = new StringBuilder();
            csText.Append("<script type=\"text/javascript\">");
            csText.Append("opener.document.getElementById('" + this.Request["ObjId"].ToString() + "').value = '" + empId + "';");
            csText.Append("opener.document.getElementById('" + this.Request["ObjName"].ToString() + "').value = '" + name + "';");
            csText.Append("opener.document.getElementById('" + this.Request["ObjEmail"].ToString() + "').value = '" + email + "';");
            csText.Append("window.close();");
            csText.Append("</script>");
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ButtonClickScript", csText.ToString());


        }

    }
}
