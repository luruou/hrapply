using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ApplyPromote
{
    public partial class CandidateAuditor : System.Web.UI.Page
    {
        CRUDObject crudObject = new CRUDObject();
        SqlConnection CN;
        SqlCommand CMD;
        string sql;

        protected void Page_Init(object sender, EventArgs e)
        {
            AuditorRealm.Items.Add(new ListItem("全部", ""));
            AuditorRealm.Items.FindByValue("").Selected = true;

            using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
            {
                CN.Open();
                sql = "SELECT AuditorRealmSn, AuditorRealmName FROM AuditorOutterRealm ";
                sql += "order by AuditorRealmName ";
                SqlCommand CMD = new SqlCommand(sql, CN);
                SqlDataReader DR = CMD.ExecuteReader();
                while (DR.Read())
                {
                    AuditorRealm.Items.Add(new ListItem(DR["AuditorRealmName"].ToString(), DR["AuditorRealmSn"].ToString()));
                }
                DR.Close();
                DR.Dispose();
                CMD.Cancel();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Panel panelManage = (Panel)Master.FindControl("PanelManage");
                panelManage.Visible = true;

                Label lb_LoginNM = (Label)Master.FindControl("lb_LoginNM");
                if (lb_LoginNM != null && Session["AcctAuditorSnEmpName"] != null)
                {
                    lb_LoginNM.Text = Session["AcctAuditorSnEmpName"].ToString() + "&nbsp;您好!&nbsp;&nbsp;";
                    lb_LoginNM.Visible = true;
                }
                refresh_gridview();
            }
        }

        protected void AuditorRealm_Click(object sender, EventArgs e)
        {
            gv_AuditorOuter.DataSource = crudObject.GetAllAuditorOuterByRealm(AuditorRealm.SelectedValue);
            gv_AuditorOuter.DataBind();
        }

        protected void ddl_AuditorUnit_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void refresh_gridview()
        {
            //AuditorRealm.DataValueField = "AuditorRealmSn";
            //AuditorRealm.DataTextField = "AuditorRealmName";
            //AuditorRealm.DataSource = crudObject.GetAllAuditorOuterRealm();
            //AuditorRealm.DataBind();
            //gv_AuditorOuter.DataSource = crudObject.GetAllAuditorOuterByRealm(AuditorRealm.SelectedValue);
            //gv_AuditorOuter.DataBind();
            gv_AuditorOuter.DataSource = crudObject.GetAllAuditorOuterByRealm(AuditorRealm.SelectedValue);
            gv_AuditorOuter.DataBind();
        }

        protected void gv_AuditorOuter_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_AuditorOuter.EditIndex = -1;
            refresh_gridview();
        }

        protected void gv_AuditorOuter_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_AuditorOuter.EditIndex = e.NewEditIndex;
            refresh_gridview();
        }
        protected void gv_AuditorOuter_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string msgbox = "";
            if (gv_AuditorOuter.Rows.Count > 0)
            {
                try
                {
                    GridView _gv = gv_AuditorOuter;
                    string txt_AppSn = ((TextBox)gv_AuditorOuter.Rows[e.RowIndex].FindControl("txt_AppSn")).Text.Trim();
                    string txt_AuditorJobTitle = ((TextBox)gv_AuditorOuter.Rows[e.RowIndex].FindControl("txt_AuditorJobTitle")).Text.Trim();
                    string txt_AuditorExperience = ((TextBox)gv_AuditorOuter.Rows[e.RowIndex].FindControl("txt_AuditorExperience")).Text.Trim();
                    string txt_AuditorEmail = ((TextBox)gv_AuditorOuter.Rows[e.RowIndex].FindControl("txt_AuditorEmail")).Text.Trim();
                    string txt_AuditorTel = ((TextBox)gv_AuditorOuter.Rows[e.RowIndex].FindControl("txt_AuditorTel")).Text.Trim();
                    DropDownList ddl_AuditorUnit = (DropDownList)gv_AuditorOuter.Rows[e.RowIndex].FindControl("ddl_AuditorUnit");
                    DropDownList ddl_AuditorName = (DropDownList)gv_AuditorOuter.Rows[e.RowIndex].FindControl("ddl_AuditorName");
                    string lb_OldAppSn = ((Label)gv_AuditorOuter.Rows[e.RowIndex].FindControl("lb_AppSn")).Text.Trim();
                    string lb_OldAuditorEmail = ((Label)gv_AuditorOuter.Rows[e.RowIndex].FindControl("lb_AuditorEmail")).Text.Trim();


                    if ( !String.IsNullOrEmpty(ddl_AuditorUnit.SelectedValue.ToString()))
                    {
                        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                        {
                            cn.Open();
                            sql = "Update [AuditorOutter] ";
                            sql += "Set AuditorJobTitle = @AuditorJobTitle, AuditorExperience = @AuditorExperience, AuditorEmail = @AuditorEmail,  AuditorTel = @AuditorTel, AuditorModifyDate = getdate() ";       
                            sql += "where AuditorName LIKE '%" + ddl_AuditorName.SelectedItem.ToString().Trim() + "%' ";

                            CMD = new SqlCommand(sql, cn);
                            CMD.Parameters.AddWithValue("@AuditorJobTitle", txt_AuditorJobTitle);
                            CMD.Parameters.AddWithValue("@AuditorExperience", txt_AuditorExperience);
                            CMD.Parameters.AddWithValue("@AuditorEmail", txt_AuditorEmail);
                            CMD.Parameters.AddWithValue("@AuditorTel", txt_AuditorTel);
                            CMD.ExecuteNonQuery();
                            CMD.Cancel();

                            sql = "Select AcctSn ";
                            sql += "From AccountForAudit ";
                            sql += "Where AcctAppSn = @AcctAppSn and AcctEmail = @OldAcctEmail";
                            CMD = new SqlCommand(sql, cn);
                            CMD.Parameters.AddWithValue("@AcctAppSn", lb_OldAppSn);
                            CMD.Parameters.AddWithValue("@OldAcctEmail", lb_OldAuditorEmail);
                            Object AcctSn =  CMD.ExecuteScalar();
                            CMD.Cancel();


                            if (!String.IsNullOrEmpty(AcctSn.ToString()))
                            {
                                sql = "Update [AccountForAudit] ";
                                sql += "Set AcctAuditorSnEmpId = @AcctAuditorSnEmpId, AcctEmail = @AcctEmail, AcctModifyDate = getdate() ";
                                sql += "Where AcctAppSn = @AcctAppSn and AcctEmail = @OldAcctEmail and AcctSn = @AcctSn ";
                                CMD = new SqlCommand(sql, cn);
                                CMD.Parameters.AddWithValue("@AcctAppSn", lb_OldAppSn);
                                CMD.Parameters.AddWithValue("@AcctAuditorSnEmpId", ddl_AuditorName.SelectedValue.Trim());
                                CMD.Parameters.AddWithValue("@AcctEmail", txt_AuditorEmail);
                                CMD.Parameters.AddWithValue("@OldAcctEmail", lb_OldAuditorEmail);
                                CMD.Parameters.AddWithValue("@AcctSn", AcctSn.ToString().Trim());
                                Int32 _executeRows = CMD.ExecuteNonQuery();
                                CMD.Cancel();
                            }
                            //else
                            //{
                            //    sql = "INSERT INTO  [AccountForAudit] ";
                            //    sql += "(AcctAuditorSnEmpId, AcctEmail, AcctBuildDate ";
                            //    sql += " VALUES (@AcctAuditorSnEmpId, @AcctEmail,getdate() ) ";
                            //    sql += "Where AcctAppSn = @AcctAppSn and AcctEmail = @OldAcctEmail and AcctSn = @AcctSn ";
                            //    CMD = new SqlCommand(sql, cn);
                            //    CMD.Parameters.AddWithValue("@AcctAppSn", lb_OldAppSn);
                            //    CMD.Parameters.AddWithValue("@AcctAuditorSnEmpId", ddl_AuditorName.SelectedValue.Trim());
                            //    CMD.Parameters.AddWithValue("@AcctEmail", txt_AuditorEmail);
                            //    CMD.Parameters.AddWithValue("@OldAcctEmail", lb_OldAuditorEmail);
                            //    CMD.Parameters.AddWithValue("@AcctSn", AcctSn.ToString().Trim());
                            //    Int32 _executeRows = CMD.ExecuteNonQuery();
                            //    CMD.Cancel();
                            //}


                            gv_AuditorOuter.EditIndex = -1;

                            refresh_gridview();

                            msgbox = "更新成功!";
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('" + msgbox + "');", true);

                        }
                        CN.Close();
                        CN.Dispose();
                    }
                    else
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "123", "alert('服務機構名稱未填!');", true);
                }
                catch (Exception exp)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('" + exp.Message.ToString() + "');", true);
                }
            }
        }
        protected void gv_AuditorOuter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState.ToString().Contains("Edit"))
                {
                    #region 服務機構載入
                    DropDownList ddl_AuditorUnit = (DropDownList)e.Row.FindControl("ddl_AuditorUnit");
                    Label lb_AuditorUnit = (Label)e.Row.FindControl("lb_AuditorUnit");
                    ddl_AuditorUnit.Items.Add(new ListItem("請選擇", ""));
                    int s = 1;
                    using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                    {
                        CN.Open();
                        sql = "SELECT AuditorRealmSn, AuditorRealmName FROM AuditorOutterRealm ";
                        sql += "order by AuditorRealmName ";
                        SqlCommand CMD = new SqlCommand(sql, CN);
                        SqlDataReader DR = CMD.ExecuteReader();
                        while (DR.Read())
                        {
                            ddl_AuditorUnit.Items.Add(new ListItem(DR["AuditorRealmName"].ToString(), DR["AuditorRealmSn"].ToString()));
                            if (lb_AuditorUnit.Text.Trim().Equals(DR["AuditorRealmName"].ToString().Trim()))
                            {
                                ddl_AuditorUnit.Items[s].Selected = true;
                            }
                            s++;
                        }
                        DR.Close();
                        DR.Dispose();
                        CMD.Cancel();
                    }
                    #endregion
                    #region 外審姓名載入
                    DropDownList ddl_AuditorName = (DropDownList)e.Row.FindControl("ddl_AuditorName");
                    Label lb_AuditorName = (Label)e.Row.FindControl("lb_AuditorName");
                    ddl_AuditorName.Items.Add(new ListItem("請選擇", ""));
                    int t = 1;
                    using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                    {
                        CN.Open();
                        sql = "SELECT distinct AuditorSn, AuditorName FROM [AuditorOutter] ";
                        sql += "order by AuditorName ";
                        SqlCommand CMD = new SqlCommand(sql, CN);
                        SqlDataReader DR = CMD.ExecuteReader();
                        while (DR.Read())
                        {
                            ddl_AuditorName.Items.Add(new ListItem(DR["AuditorName"].ToString(), DR["AuditorSn"].ToString()));
                            if (lb_AuditorName.Text.Trim().Equals(DR["AuditorName"].ToString().Trim()))
                            {
                                ddl_AuditorName.Items[t].Selected = true;
                            }
                            t++;
                        }
                        DR.Close();
                        DR.Dispose();
                        CMD.Cancel();
                        s = 1;
                    }
                    #endregion
                }
            }
        }

        protected void ddl_AuditorUnit_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ddl_AuditorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl_AuditorName = (DropDownList)sender;

            TextBox txt_AuditorJobTitle = ((DropDownList)sender).NamingContainer.FindControl("txt_AuditorJobTitle") as TextBox;
            DropDownList ddl_AuditorUnit = ((DropDownList)sender).NamingContainer.FindControl("ddl_AuditorUnit") as DropDownList;
            Label lb_AuditorUnit = ((DropDownList)sender).NamingContainer.FindControl("ddl_AuditorUnit") as Label;
            TextBox txt_AuditorExperience = ((DropDownList)sender).NamingContainer.FindControl("txt_AuditorExperience") as TextBox;
            TextBox txt_AuditorEmail = ((DropDownList)sender).NamingContainer.FindControl("txt_AuditorEmail") as TextBox;
            TextBox txt_AuditorTel = ((DropDownList)sender).NamingContainer.FindControl("txt_AuditorTel") as TextBox;
            
            if (ddl_AuditorName.SelectedValue.ToString() != "")
            {
                using (CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                {
                    CN.Open();

                    sql = " SELECT AuditorJobTitle, AuditorRealmSn, AuditorExperience, AuditorEmail, AuditorTel ";
                    sql += " FROM [AuditorOutter] ";
                    sql += " Where AuditorName = '" + ddl_AuditorName.SelectedItem.ToString().Trim() + "' or AuditorSn = '" + ddl_AuditorName.SelectedValue + "' ";
                    SqlCommand CMD = new SqlCommand(sql, CN);
                    SqlDataReader DR = CMD.ExecuteReader();
                    while (DR.Read())
                    {
                        txt_AuditorJobTitle.Text = DR["AuditorJobTitle"].ToString().Trim();
                        txt_AuditorExperience.Text = DR["AuditorExperience"].ToString().Trim();
                        txt_AuditorEmail.Text = DR["AuditorEmail"].ToString().Trim();
                        txt_AuditorTel.Text = DR["AuditorTel"].ToString().Trim();
                        ddl_AuditorUnit.SelectedValue = DR["AuditorRealmSn"].ToString().Trim(); 
                    }
                    CMD.Cancel();
                }
            }
            else
            {
                txt_AuditorJobTitle.Text = "";
            }
        }
    }
}
