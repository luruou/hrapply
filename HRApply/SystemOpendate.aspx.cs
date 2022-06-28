using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ApplyPromote
{
    public partial class SystemOpendate : System.Web.UI.Page
    {

        CRUDObject crudObject = new CRUDObject();
        VSystemOpendate vSystemOpendate;        
        DataTable dt;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AcctRole"] == null)
            {
                ShowSessionTimeOut();
                Response.Redirect("~/ApplyerList.aspx");
            }

            if (!IsPostBack)
            {
                //每次進入都去抓天方資料
                //新聘pro_id tmuP130201
                //升等專任pro_id tmuP130401
                //兼任專任pro_id tmuP130402
                //vSystemOpendate = crudObject.GetSystemOpendate();

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

        protected void BtnOk_Click(object sender, EventArgs e)
        {
            //vSystemOpendate = new VSystemOpendate();
            //vSystemOpendate.Smtr = tbYear.Text;
            //vSystemOpendate.Semester = tbSemester.Text;
            //vSystemOpendate.KindNo = "0";
            ////vSystemOpendate.ApplyBeginTime = tbApplyBeginTime.Text.ToString();
            //vSystemOpendate.ApplyBeginTime = DateTime.ParseExact(TransferDateTime(dt.Rows[0]["pro_qry_sdt"].ToString()) + ":00", "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            //vSystemOpendate.ApplyEndTime = DateTime.ParseExact(tbApplyEndTime.Text.ToString() + ":00", "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            //if (crudObject.SystemOpendateExist(vSystemOpendate))
            //{
            //    crudObject.Update(vSystemOpendate);
            //}
            //else
            //{
            //    crudObject.Insert(vSystemOpendate);
            //}
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('儲存成功');", true);
            crudObject.DeleteSystemOpendate();

            dt = crudObject.GetSystemOpenDateByProId("tmuP130201");
            InsertUpdateData(dt, "1", "0");

            dt = crudObject.GetSystemOpenDateByProId("tmuP130401");
            InsertUpdateData(dt, "2", "1");

            dt = crudObject.GetSystemOpenDateByProId("tmuP130402");
            InsertUpdateData(dt, "2", "2");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('同步舊系統');", true);
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

        }

        protected void ShowSessionTimeOut()
        {
            string message = "時間逾時,請重新登入!";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        protected void InsertUpdateData(DataTable dt,String kind,String type)
        {
            VSystemOpendate vSystemOpendate = new VSystemOpendate();   
            GetSettings getSetting = new GetSettings();
            getSetting.Execute();
            vSystemOpendate.Smtr = getSetting.GetYear();
            vSystemOpendate.Semester = getSetting.GetSemester();
            vSystemOpendate.KindNo = kind; //新聘 
            vSystemOpendate.TypeNo = type; //專任&兼任
            vSystemOpendate.ApplyBeginTime = DateTime.ParseExact(TransferDateTime(dt.Rows[0]["pro_qry_sdt"].ToString()), "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            vSystemOpendate.ApplyEndTime = DateTime.ParseExact(TransferDateTime(dt.Rows[0]["pro_qry_edt"].ToString()), "yyyy/MM/dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            if (crudObject.SystemOpendateExist(vSystemOpendate))
            {
                crudObject.Update(vSystemOpendate);
            }
            else
            {
                crudObject.Insert(vSystemOpendate);
            }
        }
        protected String TransferDateTime(String txtDate)
        {
            //yyyy/MM/dd HH:mm:ss
            String YMD = "";
            String year = "" + (Int32.Parse(txtDate.Substring(0, 3)) + 1911);
            String month = txtDate.Substring(3, 2);
            String day = txtDate.Substring(5, 2);
            YMD = year + "/" + month + "/" + day + " 00:00:00";
            return YMD;
        }
    }
}
