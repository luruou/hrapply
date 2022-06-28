using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ApplyPromote
{
    public partial class FunSelectThesisScore : PageBaseManage
    {        
        string selDept = "";
        CRUDObject crudObject = new CRUDObject();
        string strStage = "";
        string strStep = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSettings settings = new GetSettings(); 
                if (Session["EmpId"] == null || Session["EmpId"].Equals(""))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('系統登入逾時，請重新登入!');window.close();", true);
                }                
                SCIPaperYear.Items.Clear();
                SCIPaperYear.Items.Add(new ListItem("請選擇", ""));
                for(int i = 2015; i<= (Int32.Parse(settings.NowYear) + 1911); i++)
                SCIPaperYear.Items.Add(new ListItem(i.ToString().Trim(), i.ToString().Trim()));
                SCIPaperYear.DataBind();
            }
 
        }


        protected void SCIPaperYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (SCIPaperYear.SelectedValue.ToString().Equals(""))
            {

            }
            else
            {
                crudObject.DeleteThesisScoreTemp(Convert.ToInt32(Session["EmpSn"].ToString()));

                DataTable oldDT = crudObject.GetSCIPaperByYear(Session["EmpId"].ToString(),SCIPaperYear.SelectedValue.ToString());

                VThesisScore vThesisScore;
                VSciPaper vSciPaper = new VSciPaper();
                int baseNum = 1;
                if (oldDT != null)
                {
                    for (int i = 0; i < oldDT.Rows.Count; i++)
                    {
                        vThesisScore = new VThesisScore();
                        vThesisScore.EmpSn = Int32.Parse(Session["EmpSn"].ToString());
                        vThesisScore.SnNo = baseNum + i;
                        vThesisScore.ThesisName = oldDT.Rows[i][vSciPaper.strPaperName].ToString();
                        vThesisScore.ThesisResearchResult = oldDT.Rows[i][vSciPaper.strRR].ToString();
                        vThesisScore.RRNo = oldDT.Rows[i][vSciPaper.strRRNo].ToString();
                        vThesisScore.ThesisResearchResult = oldDT.Rows[i][vSciPaper.strRR].ToString();
                        vThesisScore.ThesisPublishYearMonth = oldDT.Rows[i][vSciPaper.strPublishYear].ToString() + oldDT.Rows[i][vSciPaper.strMonth].ToString();
                        vThesisScore.ThesisC = oldDT.Rows[i][vSciPaper.strC_Score].ToString();
                        vThesisScore.ThesisJ = oldDT.Rows[i][vSciPaper.strJ_Score].ToString();
                        vThesisScore.ThesisA = oldDT.Rows[i][vSciPaper.strA_Score].ToString();
                        vThesisScore.ThesisTotal = oldDT.Rows[i][vSciPaper.strTotalScore].ToString();
                        vThesisScore.ThesisYear = SCIPaperYear.SelectedValue.ToString();
                        //if (Int32.Parse(vThesisScore.ThesisTotal) >= 30) vThesisScore.IsRepresentative = true;
                        crudObject.InsertTemp(vThesisScore);
                    }
                }

                GVThesisScore.DataBind();
            }
            
        }

 

        protected void BtnSelect_Click(object sender, EventArgs e)
        {

            VThesisScore vThesisScore;
            string PKname = "";
            int insertSnNo;
            int insertCount = 0;
            foreach (GridViewRow GR in this.GVThesisScore.Rows)
            {
                CheckBox CB = (CheckBox)GR.FindControl("CheckBoxSel");
                if (CB.Checked)
                {
                    //取得要插入的序號                 
                    //insertSnNo = crudObject.GetThesisScoreInsertSnNo(Session["EmpSn"].ToString(), GR.Cells[7].ToString());

                    int baseNum = 1;
                    while (crudObject.GetThesisScoreFinalSnNo(Session["AppSn"].ToString(), baseNum))
                    {
                        baseNum++;
                    }
                    insertSnNo = baseNum;
                    vThesisScore = new VThesisScore();
                    vThesisScore.SnNo = insertSnNo;

                    //PKname += this.GVThesisScore.DataKeys[GR.RowIndex].Value.ToString() + ",";
                    PKname += insertSnNo + " ";
                    vThesisScore.EmpSn = Int32.Parse(Session["EmpSn"].ToString());                   
                    
                    vThesisScore.ThesisResearchResult = GR.Cells[5].Text.ToString();
                    vThesisScore.RRNo = GR.Cells[4].Text.ToString();
                    vThesisScore.ThesisName = GR.Cells[6].Text.ToString();
                    vThesisScore.ThesisPublishYearMonth = GR.Cells[7].Text.ToString();
                    vThesisScore.ThesisC = GR.Cells[8].Text.ToString();
                    vThesisScore.ThesisJ = GR.Cells[9].Text.ToString();
                    vThesisScore.ThesisA = GR.Cells[10].Text.ToString();
                    vThesisScore.ThesisTotal = GR.Cells[11].Text.ToString();
                    vThesisScore.ThesisJournalRefCount = "";
                    vThesisScore.ThesisUploadName = "";
                    vThesisScore.ThesisJournalRefUploadName = "";
                    vThesisScore.ThesisJournalRefCount = "";
                    vThesisScore.ThesisSummaryCN = "";
                    vThesisScore.ThesisCoAuthorUploadName = "";
                    vThesisScore.AppSn = Convert.ToInt32(Session["AppSn"]);
                    
                    //if (Int32.Parse(vThesisScore.ThesisTotal) >= 30) vThesisScore.IsRepresentative = true;
                    //插入SnNo之後的序號要往後推
                    //DataTable dt = crudObject.GetVThesisScoreAfterInsert(Convert.ToInt32(Session["EmpSn"].ToString()), vThesisScore.ThesisPublishYearMonth);
                    if (crudObject.Insert(vThesisScore))
                    {
                        insertCount++;
                    }
                    //int h = 0;
                    //for (int k = 0; k < dt.Rows.Count; k++)
                    //{
                    //    h = insertSnNo + k + 1;
                    //    crudObject.UpdateThesisScoreSn(Convert.ToInt32(dt.Rows[k][0].ToString()), h);
                    //}
                }
            }
            crudObject.DeleteThesisScoreTemp(Convert.ToInt32(Session["EmpSn"].ToString()));
            if (insertCount > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:window.opener.location.reload();alert('您已成功匯入筆資料：" + insertCount + "筆');window.close();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "message", "javascript:alert('您未匯入任何資料');window.close();", true);
            }

            //傳值給父頁面
            //int row;
            //string textBoxName;

            //row = Int32.Parse(RowNum.Text.ToString()) + 2;
            //if (row > 9)
            //{
            //    textBoxName = "GVAuditExecute_ct1" + row + "_TextBoxEmpName";
            //}
            //else
            //{
            //    textBoxName = "GVAuditExecute_ct10" + row + "_TextBoxEmpName";
            //}
            
            //this.Page.Controls.Add(
            //    new LiteralControl(string.Format
            //        ("<script type='text/javascript' language='javascript'>opener.document.getElementById('" + textBoxName + "').value = '{0}'</script>", UnitEmpList.SelectedValue.ToString())));


            ////關閉此視窗
            //this.Page.Controls.Add(new LiteralControl("<script>window.close();</script>"));
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
