using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OuterAdd : System.Web.UI.Page
{
    SqlConnection CN;
    SqlCommand CMD;
    string sql;

    protected void Page_Load(object sender, EventArgs e)
    {
        using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
        {
            ddl_service.Items.Add(new ListItem("請選擇", ""));
            CN.Open();
            sql = "select  AuditorRealmSn, AuditorRealmName  from AuditorOutterRealm order by AuditorRealmName";
            CMD = new SqlCommand(sql, CN);
            SqlDataReader DR = CMD.ExecuteReader();
            while (DR.Read())
            {
                ddl_service.Items.Add(new ListItem(DR["AuditorRealmName"].ToString(), DR["AuditorRealmSn"].ToString()));
            }
            DR.Close();
            DR.Dispose();
            CMD.Cancel();
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {        
        try
        {
            object _AuditorSn = null;
            object obj = null;
            if (!String.IsNullOrEmpty(ddl_service.SelectedValue.ToString()) && !String.IsNullOrEmpty(txt_outerNM.Text.Trim()) && !String.IsNullOrEmpty(txt_AppSn.Text.Trim()) && !String.IsNullOrEmpty(txt_outerEmail.Text.Trim()))
            {
                using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                {
                    CN.Open();
                    //sql = " SELECT AuditorSn from AuditorOutter where AuditorName like '%" + txt_outerNM.Text.Trim() + "%' ";
                    //CMD = new SqlCommand(sql, CN);
                    // obj = CMD.ExecuteScalar();
                    //CMD.Cancel();

                    //if (obj == null)
                    //{
                        sql = " INSERT INTO [AuditorOutter] ";
                        sql += " (AuditorRealmSn, AuditorName, AuditorUnit, AuditorJobTitle, AuditorExperience, AuditorEmail, AuditorTel, AuditorCell, AuditorEducation, AuditorBuildDate) ";
                        sql += "  OUTPUT INSERTED.AuditorSn ";
                        sql += " Values(@AuditorRealmSn, @AuditorName, @AuditorUnit, @AuditorJobTitle, @AuditorExperience, @AuditorEmail, @AuditorTel, @AuditorCell, @AuditorEducation, getdate()) ";
                        CMD = new SqlCommand(sql, CN);
                        CMD.CommandType = CommandType.Text;
                        CMD.Parameters.AddWithValue("@AuditorRealmSn", ddl_service.SelectedValue.ToString().Trim());
                        CMD.Parameters.AddWithValue("@AuditorName", txt_outerNM.Text.Trim());
                        CMD.Parameters.AddWithValue("@AuditorUnit", ddl_service.SelectedItem.ToString().Trim());
                        CMD.Parameters.AddWithValue("@AuditorJobTitle", txt_jobNM.Text.Trim());
                        CMD.Parameters.AddWithValue("@AuditorExperience", txt_Experience.Text.Trim());
                        CMD.Parameters.AddWithValue("@AuditorEmail", txt_outerEmail.Text.Trim());
                        CMD.Parameters.AddWithValue("@AuditorTel", txt_telphone.Text.Trim());
                        CMD.Parameters.AddWithValue("@AuditorCell", txt_cellphone.Text.Trim());
                        CMD.Parameters.AddWithValue("@AuditorEducation", ddl_service.SelectedItem.ToString().Trim() + txt_dept.Text.Trim());
                        _AuditorSn = CMD.ExecuteScalar();
                        CMD.Cancel();
                    //}
                    CN.Close();
                }

                if ( !String.IsNullOrEmpty(txt_AppSn.Text.ToString()))
                {

                    ////亂數產生密碼
                    ApplyPromote.GeneratorPwd generatorPwd = new ApplyPromote.GeneratorPwd();
                    generatorPwd.Execute();
                    string newPwd = generatorPwd.GetPwd();

                    using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                    {
                        CN.Open();
                        sql = " INSERT INTO [AccountForAudit] ";
                        sql += " (AcctAppSn, AcctAuditorSnEmpId, AcctEmail, AcctPassword, AcctStatus, AcctBuildDate) ";
                        sql += " Values(@AcctAppSn, @AcctAuditorSnEmpId, @AcctEmail, @AcctPassword, '0', getdate()) ";
                        CMD = new SqlCommand(sql, CN);
                        CMD.CommandType = CommandType.Text;
                        CMD.Parameters.AddWithValue("@AcctAppSn", txt_AppSn.Text.Trim());
                        //if(_AuditorSn!= null)
                            CMD.Parameters.AddWithValue("@AcctAuditorSnEmpId", _AuditorSn.ToString());
                        //else
                        //    CMD.Parameters.AddWithValue("@AcctAuditorSnEmpId", obj.ToString());
                        CMD.Parameters.AddWithValue("@AcctEmail", txt_outerEmail.Text.Trim());
                        CMD.Parameters.AddWithValue("@AcctPassword", newPwd.Trim());
                        CMD.ExecuteNonQuery();
                        CMD.Cancel();
                        CN.Close();
                    }
                }
                DataTable dt = new DataTable();
                DataRow row;
                DataColumn workCol = dt.Columns.Add("姓名", typeof(String));
                workCol.AllowDBNull = false;
                workCol.Unique = true;

                dt.Columns.Add("服務機構", typeof(String));
                dt.Columns.Add("系所/單位", typeof(String));
                dt.Columns.Add("職稱", typeof(String));
                dt.Columns.Add("E-mail", typeof(String));
                dt.Columns.Add("電話", typeof(String));
                dt.Columns.Add("行動電話", typeof(String));
                dt.Columns.Add("學術專長", typeof(String));

                row = dt.NewRow();
                row["姓名"] = txt_outerNM.Text.Trim();
                row["服務機構"] = ddl_service.SelectedItem.ToString().Trim(); 
                row["系所/單位"] = txt_dept.Text.Trim();
                row["職稱"] = txt_jobNM.Text.Trim();
                row["E-mail"] = txt_outerEmail.Text.Trim();
                row["電話"] = txt_telphone.Text.Trim();
                row["行動電話"] = txt_cellphone.Text.Trim();
                row["學術專長"] = txt_Experience.Text.Trim();
                dt.Rows.Add(row);

                gv_OuterAdd.DataSource = dt;
                gv_OuterAdd.DataBind();

                ScriptManager.RegisterStartupScript(Page, GetType(), "reload", "<script>unLoad()</script>", false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "123", "alert('新增成功');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "123", "alert('E-mail、姓名、聘單編號或服務機構未填寫!');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "123", "alert('" + ex.ToString() + "');", true);
        }
    }

    protected void btn_Serach_Click(object sender, EventArgs e)
    {
        using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
        {
            ddl_service.Items.Add(new ListItem("請選擇", ""));
            CN.Open();
            sql = "SELECT top(1) [AuditorName],[AuditorEmail],[AuditorRealmSn],[AuditorEducation],[AuditorJobTitle],[AuditorTel],[AuditorCell],[AuditorExperience] from AuditorOutter where AuditorName like '%" + txt_search.Text.Trim() + "%' or [AuditorEmail] like '%" + txt_search.Text.Trim() + "%'";
            CMD = new SqlCommand(sql, CN);
            SqlDataReader DR = CMD.ExecuteReader();
            while (DR.Read())
            {
                txt_outerNM.Text = DR["AuditorName"].ToString().Trim();
                txt_outerEmail.Text = DR["AuditorEmail"].ToString().Trim();
                ddl_service.SelectedValue = DR["AuditorRealmSn"].ToString().Trim();
                txt_dept.Text = DR["AuditorEducation"].ToString().Trim();
                txt_jobNM.Text = DR["AuditorJobTitle"].ToString().Trim();
                txt_telphone.Text = DR["AuditorTel"].ToString().Trim();
                txt_cellphone.Text = DR["AuditorCell"].ToString().Trim();
                txt_Experience.Text = DR["AuditorExperience"].ToString().Trim();
            }
            DR.Close();
            DR.Dispose();
            CMD.Cancel();
        }
    }
}