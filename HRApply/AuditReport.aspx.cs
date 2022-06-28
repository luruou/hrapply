using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ApplyPromote
{
    public partial class AuditReport : PageBaseApply
    {
        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();

        string empSn = "";

        string selStrList = "";

        string selWeakList = "";


        VEmployeeBase vEmp = new VEmployeeBase();

        VApplyAudit vApplyAudit;

        VAppendEmployee vAppendEmployee;

        VAppendDegree vAppendDegree;

        VAuditExecute vAuditExecute;

        VAuditExecute vInputAuditExecute;

        int appSn;


        protected void Page_Init(object sender, EventArgs e)
        {
            Session["EmpSn"] = Request["EmpSn"].ToString();
            Session["AppSn"] = Request["AppSn"].ToString();
            appSn = Convert.ToInt32(Session["AppSn"].ToString());

            crudObject = new CRUDObject();
            vEmp = crudObject.GetEmpBsaseObjByEmpSn(Request["EmpSn"].ToString());
            if (vEmp != null)
            {
                AuditEmpNameCN.Text = vEmp.EmpNameCN;

                //載入ApplyAudit共用延伸資料                   
                vApplyAudit = crudObject.GetApplyAuditObj(appSn);
                vAuditExecute = new VAuditExecute();
                vInputAuditExecute = new VAuditExecute();
                vInputAuditExecute.AppSn = vApplyAudit.AppSn;
                vInputAuditExecute.ExecuteAuditorSnEmpId = Session["AcctAuditorSnEmpId"].ToString(); //目前抓自己AccountForAudit中的 ID
                vInputAuditExecute.ExecuteStatus = true;

                vAuditExecute = crudObject.GetExecuteAuditorData(vInputAuditExecute); //抓所有外審資料
                //StrengthsWeaknesses 一定都要顯示
                if (vAuditExecute == null)
                {
                    AddStrengthControls("");
                    AddWeaknessControls("");
                    TableAuditExecute.Visible = false;
                }
                else
                {
                    AddStrengthControls(vAuditExecute.ExecuteStrengths);
                    AddWeaknessControls(vAuditExecute.ExecuteWeaknesses);
                    LoadDataBtn_Click(sender, e);
                }

                //呼叫pdf下載程式
                string strErrmsg = "";
                WritePDF writePDF = new WritePDF();
                writePDF.Output(this, crudObject.GetExecuteAuditorDataOuter(vInputAuditExecute), true, ref strErrmsg);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        //查詢已存資料
        protected void LoadDataBtn_Click(object sender, EventArgs e)
        {

            
            //載入ApplyAudit共用延伸資料                   
            vApplyAudit = crudObject.GetApplyAuditObj(appSn);

            Session["AppSn"] = vApplyAudit.AppSn; //

            //指定性別

            //指定單位
            if (!Object.Equals(null, vApplyAudit))
            {
                //應徵單位
                AuditAppUnit.Text = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();

                //應徵職稱 送審等級
                AuditAppJobTitle.Text = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);

                //代表著作--學位送審
                vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                if (vAppendDegree != null)
                {

                    if (vAppendDegree.AppDDegreeThesisName != null && !vAppendDegree.AppDDegreeThesisName.Equals(""))
                    {
                        AuditAppPublication.Text = vAppendDegree.AppDDegreeThesisName;
                    }
                }

            }
            //外審畫面
            if (!vAuditExecute.Equals(null))
            {

                Session["ExecuteSn"] = vAuditExecute.ExecuteSn; //外審 update資料需要用
                TableAuditExecute.Visible = true;
                //舊資料帶出
                AuditExecuteCommentsA.Text = vAuditExecute.ExecuteCommentsA;
                AuditExecuteCommentsB.Text = vAuditExecute.ExecuteCommentsB;
                TBWSSubject.Text = vAuditExecute.ExecuteWSSubject;
                TBWSMethod.Text = vAuditExecute.ExecuteWSMethod;
                TBWSContribute.Text = vAuditExecute.ExecuteWSContribute;
                TBWSAchievement.Text = vAuditExecute.ExecuteWSAchievement;
                TBWTotalScore.Text = vAuditExecute.ExecuteWTotalScore;

                //控制報表欄位顯示或隱藏--外審
                VAuditReportCompose vAuditReportCompose = new VAuditReportCompose();
                vAuditReportCompose.ARWaydNo = vApplyAudit.AppWayNo; ; //學術研究 改動態
                vAuditReportCompose.ARKindNo = vApplyAudit.AppKindNo; // 改動態
                vAuditReportCompose.ARAttributeNo = vApplyAudit.AppAttributeNo;//vApplyAudit.AppAttributeNo;
                vAuditReportCompose.ARStage = vAuditExecute.ExecuteStage; //Stage
                DataTable dtAuditReport = crudObject.GetAuditReport(vAuditReportCompose);

                if ((Boolean)dtAuditReport.Rows[0]["ARFiveLevel"])
                {
                    FiveLevelDiscription1.Visible = true;
                    FiveLevelDiscription2.Visible = true;
                    FivelLevelInputData.Visible = true;
                    FiveLevelNote.Visible = true;
                }
                else
                {
                    FiveLevelDiscription1.Visible = false;
                    FiveLevelDiscription2.Visible = false;
                    FivelLevelInputData.Visible = false;
                    FiveLevelNote.Visible = false;
                }

                if ((Boolean)dtAuditReport.Rows[0]["ARWritingScore"])
                {
                    WritingScoreDiscription1.Visible = true;
                    WritingScoreDiscription2.Visible = true;
                    WritingScoreInputData.Visible = true;
                }
                else
                {
                    WritingScoreDiscription1.Visible = false;
                    WritingScoreDiscription2.Visible = false;
                    WritingScoreInputData.Visible = false;
                }

            }
        }

        private void AddStrengthControls(string executeSW)
        {
            PlaceHolder pholderStrengths = this.PHolderStrengths;
            DataTable dtSW;
            Hashtable hashTable = new Hashtable();
            VStrengthsWeaknesses vStrengthsWeaknesses = new VStrengthsWeaknesses();
            vStrengthsWeaknesses.WaydNo = "1"; //學術研究 教學實務 產學應用
            vStrengthsWeaknesses.KindNo = "1"; // 改動態
            vStrengthsWeaknesses.AttributeNo = "3";//vApplyAudit.AppAttributeNo;
            vStrengthsWeaknesses.SWType = "S"; //優點

            dtSW = crudObject.GetStrengthsWeaknesses(vStrengthsWeaknesses);

            for (int i = 0; i < dtSW.Rows.Count; i++)
            {
                if ((Boolean)dtSW.Rows[i][2])
                {
                    hashTable.Add(dtSW.Rows[i][1].ToString(), Convert.ToInt32(dtSW.Rows[i][0].ToString()));
                }

            }

            CheckBoxList checkBoxList = new CheckBoxList();
            ListItem listItem;
            foreach (DictionaryEntry entry in hashTable)
            {
                listItem = new ListItem(entry.Key.ToString(), entry.Value.ToString());


                if (!string.IsNullOrEmpty(executeSW))
                {
                    if (executeSW.IndexOf(entry.Value.ToString()) > -1)
                    {
                        listItem.Selected = true;
                    }
                    else
                    {
                        listItem.Selected = false;
                    }
                }
                checkBoxList.Items.Add(listItem);
                checkBoxList.ID = "chbxStrengths";
                //pnlCheckbox為Panel控制項
                pholderStrengths.Controls.Add(checkBoxList);
                //pholderCheckbox.Controls.Add(new LiteralControl("</br>"));
                //為自訂的checkbox掛上event
                checkBoxList.SelectedIndexChanged += new EventHandler(CheckSBoxList_CheckedChanged);
            }
            string other = executeSW.Split(',')[executeSW.Split(',').Length - 1];
            OtherStrengths.Text = other;
        }

        void CheckSBoxList_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxList cbxList = (CheckBoxList)sender;
            selStrList = "";
            foreach (ListItem listItem in cbxList.Items)
            {
                if (listItem.Selected)
                {
                    selStrList += listItem.Value + ",";
                }
            }
            Response.Write("選取S:" + selStrList);
        }

        private void AddWeaknessControls(string executeSW)
        {
            PlaceHolder pholderWeaknesses = this.PHolderWeaknesses;
            DataTable dtSW;
            Hashtable hashTable = new Hashtable();
            VStrengthsWeaknesses vStrengthsWeaknesses = new VStrengthsWeaknesses();
            vStrengthsWeaknesses.WaydNo = "1"; //學術研究 改動態
            vStrengthsWeaknesses.KindNo = "1"; // 改動態
            vStrengthsWeaknesses.AttributeNo = "3"; //vApplyAudit.AppAttributeNo;
            vStrengthsWeaknesses.SWType = "W"; //優點

            dtSW = crudObject.GetStrengthsWeaknesses(vStrengthsWeaknesses);

            for (int i = 0; i < dtSW.Rows.Count; i++)
            {
                if ((Boolean)dtSW.Rows[i][2])
                {
                    hashTable.Add(dtSW.Rows[i][1].ToString(), Convert.ToInt32(dtSW.Rows[i][0].ToString()));
                }

            }

            CheckBoxList checkBoxList = new CheckBoxList();
            ListItem listItem;
            foreach (DictionaryEntry entry in hashTable)
            {
                listItem = new ListItem(entry.Key.ToString(), entry.Value.ToString());


                if (!string.IsNullOrEmpty(executeSW))
                {
                    if (executeSW.IndexOf(entry.Value.ToString()) > -1)
                    {
                        listItem.Selected = true;
                    }
                    else
                    {
                        listItem.Selected = false;
                    }
                }
                checkBoxList.Items.Add(listItem);
                checkBoxList.ID = "chbxWeaknesses";
                //pnlCheckbox為Panel控制項
                pholderWeaknesses.Controls.Add(checkBoxList);
                //pholderCheckbox.Controls.Add(new LiteralControl("</br>"));
                //為自訂的checkbox掛上event
                checkBoxList.SelectedIndexChanged += new EventHandler(CheckWBoxList_CheckedChanged);
            }
            string other = executeSW.Split(',')[executeSW.Split(',').Length - 1];
            OtherWeaknesses.Text = other;
        }

        void CheckWBoxList_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxList cbxList = (CheckBoxList)sender;
            selWeakList = "";
            foreach (ListItem listItem in cbxList.Items)
            {
                if (listItem.Selected)
                {
                    selWeakList += listItem.Value + ",";
                }
            }
            Response.Write("選取W:" + selWeakList);
        }

    }
}
