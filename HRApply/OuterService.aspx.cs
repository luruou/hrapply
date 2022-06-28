using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OuterService : System.Web.UI.Page
{
    SqlConnection CN;
    SqlCommand CMD;
    string sql;
    int _rowCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            refresh_gridview();
        }
    }

    private void refresh_gridview()
    {
        GridView gv = gv_OuterService;
        DataTable aTb = new DataTable("UserData");

        sql = "select AuditorRealmSn, AuditorRealmName FROM [AuditorOutterRealm] ";
        sql += "order by AuditorRealmName ";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            {
                da.Fill(aTb);
            }
        }

        if (aTb.Rows.Count == 0)
        {
            aTb.Rows.Add(aTb.NewRow());

            gv.DataSource = aTb;
            gv.DataBind();

            int columnCount = gv.Rows[0].Cells.Count;
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());

            gv.Rows[0].Cells[0].ColumnSpan = columnCount;
            gv.Rows[0].Cells[0].Text = "查無資料！";
            gv.RowStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
        }
        else
        {
            _rowCount = aTb.Rows.Count;
            gv.DataSource = aTb;
            gv.DataBind();
        }
    }

    protected void gv_OuterService_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_OuterService.EditIndex = -1;
        refresh_gridview();
    }

    protected void gv_OuterService_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_OuterService.EditIndex = e.NewEditIndex;
        refresh_gridview();
    }
    protected void gv_OuterService_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string msgbox = "";
        if (gv_OuterService.Rows.Count > 0)
        {
            foreach (GridViewRow row in gv_OuterService.Rows)
            {
                try
                {
                    GridView _gv = gv_OuterService;
                    string txt_ServiceNM = ((TextBox)gv_OuterService.Rows[e.RowIndex].FindControl("txt_ServiceNM")).Text.Trim();
                    string lb_ServiceSn = ((Label)gv_OuterService.Rows[e.RowIndex].FindControl("lb_ServiceSn")).Text.Trim();

                    if (!String.IsNullOrEmpty(txt_ServiceNM) && !String.IsNullOrEmpty(lb_ServiceSn))
                    {
                        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                        {
                            cn.Open();
                            sql = "Update [AuditorOutterRealm] Set AuditorRealmName = @AuditorRealmName, AuditorRealmModifyDate = getdate() ";
                            sql += "where AuditorRealmSn = @AuditorRealmSn ";

                            CMD = new SqlCommand(sql, cn);
                            CMD.Parameters.AddWithValue("@AuditorRealmName", txt_ServiceNM);
                            CMD.Parameters.AddWithValue("@AuditorRealmSn", lb_ServiceSn);

                            Int32 _executeRows = CMD.ExecuteNonQuery();
                            CMD.Cancel();
                            gv_OuterService.EditIndex = -1;

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
    }
    protected void gv_OuterService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void AddService_Click(object sender, EventArgs e)
    {
        tb_AddService.Visible = true;
        AddService.Visible = false;
    }

    protected void btn_Send_Click(object sender, EventArgs e)
    {
        tb_AddService.Visible = false;
        AddService.Visible = true;
        string msgbox = "";
        try
        {
            if (!String.IsNullOrEmpty(txt_AddService.Text.Trim()))
            {
                if (checkOterService())
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        sql = "INSERT INTO [AuditorOutterRealm] ";
                        sql += " (AuditorRealmName,AuditorRealmBuildDate) ";
                        sql += " VALUES( @AuditorRealmName, getdate()) ";

                        CMD = new SqlCommand(sql, cn);
                        CMD.Parameters.AddWithValue("@AuditorRealmName", txt_AddService.Text.Trim());

                        Int32 _executeRows = CMD.ExecuteNonQuery();
                        CMD.Cancel();
                        gv_OuterService.EditIndex = -1;

                        refresh_gridview();

                        msgbox = "更新成功!";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('" + msgbox + "');", true);

                    }
                    CN.Close();
                    CN.Dispose();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "123", "alert('" + txt_AddService.Text.Trim() + "已存在!');", true);
            }
            else
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "123", "alert('服務機構名稱未填!');", true);
        }
        catch (Exception exp)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "opennewwindow", "alert('" + exp.Message.ToString() + "');", true);
        }

    }

    private bool checkOterService()
    {
        DataTable aTb = new DataTable();

        sql = "select AuditorRealmSn, AuditorRealmName FROM [AuditorOutterRealm] ";
        sql += " where AuditorRealmName = '" + txt_AddService.Text.Trim() + "' ";
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
        {
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            {
                da.Fill(aTb);
            }
        }

        if (aTb.Rows.Count == 0)
            return true;
        else
            return false;
    }
}