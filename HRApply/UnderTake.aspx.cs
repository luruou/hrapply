using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class UnderTake : System.Web.UI.Page
    {
        CRUDObject crudObject = new CRUDObject();

        DataTable gridData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AppEUnitNo.DataValueField = "PointDept";        //在此輸入的是資料表的欄位名稱
                AppEUnitNo.DataTextField = "unt_name_full";      //在此輸入的是資料表的欄位名稱
                //AppEUnitNo.DataSource = crudObject.GetOpenUnit();
                AppEUnitNo.DataSource = crudObject.getUnterTakeUnit();//請完成
                AppEUnitNo.DataBind();

                GVUuderTake.DataSource = crudObject.GetAllUnderTakerByDept(AppEUnitNo.SelectedValue);
                GVUuderTake.DataBind();

                Label lb_UserNM = (Label)Master.FindControl("lb_UserNM");
                if (lb_UserNM != null && Session["AcctAuditorSnEmpName"] != null)
                {
                    lb_UserNM.Text = Session["AcctAuditorSnEmpName"].ToString() + "&nbsp;您好!&nbsp;&nbsp;";
                    lb_UserNM.Visible = true;
                }

                Panel panelManage = (Panel)Master.FindControl("PanelManage");
                panelManage.Visible = true;

                Label lb_LoginNM = (Label)Master.FindControl("lb_LoginNM");
                if (lb_LoginNM != null && Session["AcctAuditorSnEmpName"] != null)
                {
                    lb_LoginNM.Text = Session["AcctAuditorSnEmpName"].ToString() + "&nbsp;您好!&nbsp;&nbsp;";
                    lb_LoginNM.Visible = true;
                }
            }
        }

        protected void AppEUnitNo_Click(object sender, EventArgs e)
        {
            gridData = crudObject.GetAllUnderTakerByDept(AppEUnitNo.SelectedValue);
            ViewState["grid"] = gridData;
            GVUuderTake.DataSource = gridData;
            GVUuderTake.DataBind();
        }

        protected void GVUnderTake_RowDataBound(object sender, GridViewRowEventArgs e)
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
                strParameter = "ObjId=" + txtBoxAuditorId.ClientID + "&ObjName=" + txtBoxAuditorName.ClientID + "&ObjEmail=" + txtBoxAuditorEmail.ClientID + "&Stage=1&Step=1&AttributeNo=1";
                txtBoxAuditorName.Attributes["onclick"] = "window.open('FunSelectAuditor.aspx?" + strParameter + " ','mywin','menubar=no,status=no,scrollbars=yes,top=100,left=200,toolbar=no,width=450,height=300');return true;";
            }
        }

        protected void BtnUpdate_OnClick(object sender, EventArgs e)
        {
            //Update 資料回UnderTake -->請參考 BtnUpdateAuditor_Click(ManageSetAudit.aspx.cs) 更新簽核資料
            int i = 0;
            DataTable oldData = (DataTable)ViewState["grid"];
            foreach (GridViewRow row in GVUuderTake.Rows)
            {
                //因為在GridView需指定後才能抓到值
                TextBox strExecuteSn = (TextBox)row.FindControl("TextBoxUnSn");
                TextBox txtBoxAuditorSnEmpId = (TextBox)row.FindControl("TextBoxAuditorSnEmpId");
                TextBox txtBoxAuditorEmail = (TextBox)row.FindControl("TextBoxAuditorEmail");
                TextBox txtBoxAuditorName = (TextBox)row.FindControl("TextBoxAuditorName");
                //if(i< GVUuderTake.Rows.Count)
                if (txtBoxAuditorSnEmpId != null && txtBoxAuditorSnEmpId.Text.Trim()!= oldData.Rows[i][2].ToString().Trim())
                {
                    using(SqlConnection cn =  new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        SqlCommand cm;
                        string sql = "Update UnderTaker set EmpId = '" + txtBoxAuditorSnEmpId.Text.Trim() + "' where UnSn = '" + oldData.Rows[i][0].ToString().Trim() + "' ";
                        sql += "Update AuditExecute set ExecuteAuditorSnEmpId = '" + txtBoxAuditorSnEmpId.Text.Trim() + "', ExecuteAuditorEmail = '" + txtBoxAuditorEmail.Text.Trim() + "', ExecuteAuditorName = '" + txtBoxAuditorName.Text.Trim() + "' where ExecuteAuditorSnEmpId = '" + oldData.Rows[i][2].ToString().Trim() + "' and ExecuteStatus ='0'";
                        cm = new SqlCommand(sql, cn);
                        cm.ExecuteNonQuery();
                        cm.Cancel();
                        cn.Close();
                    }
                }
                i++;
            }
        }
    }
}
