using System;
using System.IO;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace ApplyPromote
{
    public partial class SetAudit_Old : PageBaseManage
    {
        //判斷國別
        string switchFgn = "";

        //新聘已具部定教師資格
        static string chkTeacher = "1";

        //新聘申請學位送審
        static string chkDegree = "2";

        //新聘申請著作送審
        static string chkPublication = "3";

        //新聘臨床教師新聘4
        static string chkClinical = "4";

        //申請新聘
        static string chkApply = "1";

        //申請升等
        static string chkPromote = "2";  
        //上傳檔案 基本資料
        static public ArrayList fileLists = new ArrayList();

        //上傳檔案 論文

        VEmployeeBase vEmp = new VEmployeeBase();

        VApplyAudit vApplyAudit;

        VAppendEmployee vAppendEmployee;

        VAppendPublication vAppendPublication;

        VAppendDegree vAppendDegree;

        VAuditExecute vAuditExecute;

        VAuditExecute vAuditExecuteNextOne;

        //資料庫操作物件
        CRUDObject crudObject = new CRUDObject();

        string empSn = "";

        string selStrList = "";

        string selWeakList = "";

        //頁籤切換
        public enum MSearchType
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewAuditExecuting = 1,
            ViewAuditorSetting = 2

        }

        public enum OSearchType
        {
            NotSet = -1,
            ViewTeachBase = 0,
            ViewAuditExecuting = 1
        }

        //上傳檔案
        NameValueCollection fileCollection = new NameValueCollection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AcctRole"] == null)
                {
                    ShowSessionTimeOut();
                    Response.Redirect("~/ApplyerList.aspx");
                }
                string accountRole = Session["AcctRole"].ToString();

                //Session["ThesisScore"] initial
                Session["ThesisScore"] = 1;

                //MessageLabel initial

                if (Session["EmpSn"] != null)
                {
                    empSn = Session["EmpSn"].ToString();
               //管理角色M || 校內一般審查者A 才載入資料並顯示
                    ConvertDTtoStringArray convertDTtoStringArray = new ConvertDTtoStringArray();

                    DataTable dtTable;

                    //載入ApplyAudit共用延伸資料                   
                    vApplyAudit = crudObject.GetApplyAuditByIdno(vEmp.EmpIdno);

                    DataTable dt = new DataTable();
                    dtTable = crudObject.GetAllAuditExecuteByEmpSn(vApplyAudit.AppSn);
                    if (dtTable==null)
                    {
                        ShowSessionTimeOut();
                        Response.Redirect("~/ApplyerList.aspx");
                    }

                    dt.Columns.AddRange(new DataColumn[] {new DataColumn("ExecuteAuditorSn", System.Type.GetType("System.String")),
                                               new DataColumn("ExecuteStage", System.Type.GetType("System.String")),
                                               new DataColumn("ExecuteStageNum", System.Type.GetType("System.String")), 
                                               new DataColumn("ExecuteStepNum", System.Type.GetType("System.String")), 
                                               new DataColumn("ExecuteRoleName", System.Type.GetType("System.String")), 
                                               new DataColumn("ExecuteAuditorSnEmpId", System.Type.GetType("System.String")),                                                
                                               new DataColumn("ExecuteAuditorName", System.Type.GetType("System.String")),                                                
                                               new DataColumn("ExecuteAuditorEmail",System.Type.GetType("System.String")),
                                               new DataColumn("ExecuteBngDate",System.Type.GetType("System.String")),
                                               new DataColumn("ExecuteEndDate",System.Type.GetType("System.String")),
                                               new DataColumn("ExecuteStatus",System.Type.GetType("System.String"))
                                                });

                    


                    String[] strStatus = convertDTtoStringArray.GetStringArrayWithZero(crudObject.GetAllAuditProcgressName());
                    String[] strStep = { "0", "(1)", "(2)", "(3)" };
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {

                        dt.Rows.Add(dtTable.Rows[i][0].ToString(),
                            strStatus[Int32.Parse(dtTable.Rows[i][1].ToString())],
                            dtTable.Rows[i][1].ToString(),
                            dtTable.Rows[i][2].ToString(),
                            dtTable.Rows[i][3].ToString(),
                            dtTable.Rows[i][4].ToString(),
                            dtTable.Rows[i][5].ToString(),
                            dtTable.Rows[i][6].ToString(),
                            dtTable.Rows[i][9].ToString(),
                            dtTable.Rows[i][10].ToString(),
                            GetAuditStatus((Boolean)dtTable.Rows[i][12]) 
                           );
                           
                        }
                                                   
                        //0.ExecuteSn, v
                        //1.ExecuteStage, v v
                        //2.ExecuteStep, v
                        //3.ExecuteRoleName, v
                        //4.ExecuteAuditorSnEmpId, v
                        //5.emp_name, v ExecuteAuditorName
                        //6.ExecuteAuditorEmail,v
                        //7.ExecuteAccept,
                        //8.ExecuteDate,
                        //9.ExecuteBngDate, " + v
                        //10.ExecuteEndDate, v
                        //11.ExecutePass,
                        //12.ExecuteStatus v                           

                        GVAuditExecute.DataSource = dt;
                        GVAuditExecute.DataBind();
                    
                }
                else
                {
                    //MessageLabel.Text = "抱歉，目前無資料,請洽資訊人員!!";
                    BtnStartAuditor.Enabled = false;
                    BtnUpdateAuditor.Enabled = false;
                }


            }

        }






    

        private String GetAuditStatus(Boolean status)
        {
            String strData = "";
            if (status)
            {
                strData = "完成";
            }
            else
            {
                strData = "未完成";
            }
            return strData;
        }

 

        protected void GVAuditExecute_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TextBox txtBoxAuditorId;
            TextBox txtBoxAuditorName;
            TextBox txtBoxAuditorEmail;
            string strParameter = ""; 
            if (e.Row.RowType==DataControlRowType.DataRow)
            {

                txtBoxAuditorId = (TextBox)e.Row.FindControl("TextBoxAuditorSnEmpId");
                txtBoxAuditorName = (TextBox)e.Row.FindControl("TextBoxAuditorName");
                txtBoxAuditorEmail = (TextBox)e.Row.FindControl("TextBoxAuditorEmail");
                string strStage = DataBinder.Eval(e.Row.DataItem, "ExecuteStageNum").ToString();
                string strStep = DataBinder.Eval(e.Row.DataItem, "ExecuteStepNum").ToString();
                strParameter = "ObjId=" + txtBoxAuditorId.ClientID + "&ObjName=" + txtBoxAuditorName.ClientID + "&ObjEmail=" + txtBoxAuditorEmail.ClientID + "&Stage=" + strStage + "&Step=" + strStep;
                txtBoxAuditorName.Attributes["onclick"] = "window.open('FunSelectAuditor.aspx?" + strParameter +" ','mywin','100','100','no','center');return true;";
            }
           

        }


        //更新簽核資料
        protected void BtnUpdateAuditor_Click(object sender, EventArgs e)
        {
            VAuditExecute vAuditExecute;
            int i = 0;
            foreach (GridViewRow row in GVAuditExecute.Rows)
            {
                //因為在GridView需指定後才能抓到值
                TextBox strExecuteSn = (TextBox)row.FindControl("TextBoxAuditorSn");
                TextBox txtBoxAuditorSnEmpId = (TextBox)row.FindControl("TextBoxAuditorSnEmpId");
                TextBox txtBoxAuditorEmail = (TextBox)row.FindControl("TextBoxAuditorEmail");
                TextBox txtBoxAuditorName =  (TextBox)row.FindControl("TextBoxAuditorName");

                if (!string.IsNullOrEmpty(txtBoxAuditorSnEmpId.Text))
                {
                    vAuditExecute = new VAuditExecute();
                    vAuditExecute.ExecuteSn = Convert.ToInt32(strExecuteSn.Text.ToString());
                    vAuditExecute.ExecuteAuditorSnEmpId = txtBoxAuditorSnEmpId.Text.ToString();
                    vAuditExecute.ExecuteAuditorEmail = txtBoxAuditorEmail.Text.ToString();
                    vAuditExecute.ExecuteAuditorName = txtBoxAuditorName.Text.ToString();

                    crudObject.UpdateExecuteAuditorEmp(vAuditExecute);
                }
                i++;
            }
        }

        //啟動簽核寄發Email通知,產生帳號資料
        protected void BtnStartAuditor_Click(object sender, EventArgs e)
        {
            CRUDObject crudObject = new CRUDObject();

            //判斷第一順位簽核人員資料
            //GridViewRow row = (GridViewRow)GVAuditExecute.Rows[0];
            //TextBox strExecuteSn = (TextBox)row.FindControl("TextBoxAuditorSn");
            //TextBox txtBoxAuditorSnEmpId = (TextBox)row.FindControl("TextBoxAuditorSnEmpId");
            //TextBox txtBoxAuditorEmail = (TextBox)row.FindControl("TextBoxAuditorEmail");
            //TextBox txtBoxAuditorName = (TextBox)row.FindControl("TextBoxAuditorName");
            //Session["NextAcctAuditorSnEmpId"] = txtBoxAuditorSnEmpId.Text.ToString();
            //Session["NextAcctAuditorEmail"] = txtBoxAuditorEmail.Text.ToString();
            //Session["NextAcctAuditorName"] = txtBoxAuditorName.Text.ToString();
            //Session["NextAcctRoleName"] = "承辦人員";
            VAuditExecute vAuditExecuteNextOne; 

            //寫入第一位簽核者(都是校內員工） Login帳號 及寄信通知
            if (Session["AppSn"] != null)
            {
                int appSn = Convert.ToInt32(Session["AppSn"].ToString());
                vAuditExecuteNextOne = this.crudObject.GetExecuteAuditorNextOne(appSn); //vApplyAudit.AppSn 
                if (vAuditExecuteNextOne != null)
                {
                    if (GenerateAccoutnAndSendEmail(true, vAuditExecuteNextOne))
                    {
                        MessageLabelAll.Text += " 啟動成功, 送審『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核!!";
                    }
                    else
                    {
                        MessageLabelAll.Text += " 啟動失敗, 送審『" + vAuditExecuteNextOne.ExecuteRoleName + "』人員簽核，寄發通知失敗!!";
                    }
                    
                }
                else
                {
                    ShowSessionTimeOut();
                    Response.Redirect("~/ApplyerList.aspx");
                }            
            }
            else
            {
                MessageAudit.Text = "啟動失敗,請確認第一順位的簽核人員是否完成設定!";
            }
        }

 

        protected string ProcessCheckedData(string selectedData, string other)
        {
            //先將other 
            selectedData += other;
            //判斷最後一字元是,就刪除
            if (selectedData.EndsWith(",")){
                selectedData = selectedData.Substring(0, selectedData.Length - 1);
            }
            return selectedData;
        }

 

        private string GetSelectedStrengthData(CheckBoxList ckBxStrengths)
        {
            Response.Write("selected items --> " + selStrList);
            return selStrList;
        }


        private string GetSelectedWeaknessData(CheckBoxList ckBxStrengths)
        {
            Response.Write("selected items --> " + selStrList);
            return selWeakList;
        }

        protected void EmpSex_SelectedIndexChanged(object sender, EventArgs e)
        {
        }




        public Boolean GenerateAccoutnAndSendEmail(Boolean isTaipeiUniversity, VAuditExecute vAuditExecuteNextOne)
        {
            crudObject = new CRUDObject();
            //取得密碼
            GeneratorPwd generatorPwd = new GeneratorPwd();
            Mail mail = new Mail();
            generatorPwd.Execute();
            string newPwd = generatorPwd.GetPwd();

            //Email
            VSendEmail vSendEmail = new VSendEmail();
            vSendEmail.MailToAccount = vAuditExecuteNextOne.ExecuteAuditorEmail;
            vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
            vSendEmail.MailSubject = "有新聘申請文件--請盡速簽核";
            vSendEmail.ToAccountName = vAuditExecuteNextOne.ExecuteRoleName + " " + vAuditExecuteNextOne.ExecuteAuditorName;
            try
            {
                //先確認是否為校內人員
                string empIdno = "";
                int acctSn = 0;
                empIdno = crudObject.GetVEmployeeFromTmuHrByEmail(vAuditExecuteNextOne.ExecuteAuditorEmail);
                if (empIdno != null)
                {
                    //在AccountForManage是否存在
                    acctSn = crudObject.GetAccountForManageAcctSn(vAuditExecuteNextOne.ExecuteAuditorEmail);
                    VAccountForManage vAccountForManage = new VAccountForManage();
                    if (acctSn == 0)
                    {
                        //新增一筆校內管理者資料 權限為A 僅有稽核權限

                        vAccountForManage.AcctEmail = vAuditExecuteNextOne.ExecuteAuditorEmail;
                        vAccountForManage.AcctPassword = "123456";
                        vAccountForManage.AcctRole = "A";
                        vAccountForManage.AcctStatus = true;
                        crudObject.Insert(vAccountForManage);
                    }
                    else
                    {
                        vAccountForManage = crudObject.GetAccountForManage(vAuditExecuteNextOne.ExecuteAuditorEmail);

                    }

                    vSendEmail.MailContent = "<br> 系所有新聘申請需您的核准!<br><font color=red>請在兩周內完成您的審核</font>,申請者才能進入下一階段的審核.<br><br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/ManageLogin.aspx\">按此進入審核</a>  您的帳號:" + vAccountForManage.AcctEmail + " 密碼:校內使用的密碼 <br/><br><br><br><br>人資處 怡慧(2028) 亭吟(2066)<br>";
                }
                else
                {
                    //校外審核者一律新增 新的帳號 一次僅審核一位
                    VAccountForAudit vAccountForAudit = new VAccountForAudit();
                    vAccountForAudit.AcctAppSn = Session["AppSn"].ToString(); 
                    vAccountForAudit.AcctAuditorSnEmpId = vAuditExecuteNextOne.ExecuteAuditorSnEmpId;
                    vAccountForAudit.AcctEmail = vAuditExecuteNextOne.ExecuteAuditorEmail;
                    vAccountForAudit.AcctPassword = newPwd;
                    vAccountForAudit.AcctStatus = true;
                    crudObject.Insert(vAccountForAudit);
                    vSendEmail.MailContent = "<br> 系所有新聘申請需您的核准!<br><font color=red>請在兩周內完成您的審核</font>,申請者才能進入下一階段的審核.<br><br>感謝!<br><br><br><br><a href=\"http://hr2sys.tmu.edu.tw/HRApply/LoginX.aspx\">按此進入審核</a>  您的帳號:" + vAccountForAudit.AcctEmail + " 密碼:" + vAccountForAudit.AcctPassword + "<br/><br><br><br><br>人資處 怡慧(2028) 亭吟(2066)<br>";

                }

                //寄發Email通知
                return (Boolean)mail.SendEmail(vSendEmail);      

                //更新 ApplyAudit 中的 審核Stage與Step的狀態
                VApplyAudit vApplyAuditUpdate = new VApplyAudit();
                vApplyAuditUpdate.AppSn = vAuditExecuteNextOne.AppSn;
                vApplyAuditUpdate.AppStage = vAuditExecuteNextOne.ExecuteStage;
                vApplyAuditUpdate.AppStep = vAuditExecuteNextOne.ExecuteStep;
                crudObject.UpdateApplyAuditStageStep(vApplyAuditUpdate);

          
            }
            catch (Exception e)
            {
                MessageLabelAll.Text = e.ToString();
                return false;
            }


        }


        public Boolean ReturnAuditSendEmail(VAuditExecute vAuditExecuteNextOne)
        {
            try
            {
                //Email
                VSendEmail vSendEmail = new VSendEmail();
                vSendEmail.MailToAccount = vAuditExecuteNextOne.ExecuteAuditorEmail;
                vSendEmail.MailFromAccount = "up_group@tmu.edu.tw";
                vSendEmail.MailSubject = "新聘申請文件--退回學院補件通知";
                vSendEmail.ToAccountName = vAuditExecuteNextOne.ExecuteRoleName + " " + vAuditExecuteNextOne.ExecuteAuditorName;
                vSendEmail.MailContent = "<br><br> 系所有新聘申請退回補件!<br><font color=red>請協助確認新申請問題</font><br><br>感謝!<br><br><br><br><a href='http://hr2sys.tmu.edu.tw/HRApply/Default.aspx'>按此進入審核</a>  您的帳號:" + vAuditExecuteNextOne.ExecuteAuditorEmail + " <br/><br><br><br><br>人資處 怡慧(2028) 亭吟(2066)<br>";
                return true;
            }
            catch (Exception e)
            {
                MessageLabelAll.Text = e.ToString();
                return false;
            }


        }
    }
}
