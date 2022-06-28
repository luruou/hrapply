using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace ApplyPromote
{
    public class CRUDObject
    {

        ConnectionStringSettings Conn = WebConfigurationManager.ConnectionStrings["tmuConnectionString"];
        String appYear = "";
        String appSemester = "";
        GetSettings getSettings = new GetSettings();
        //SqlDataSource sqlds = null;
        string[] DateTimeList = {
                            "yyyy/M/d tt hh:mm:ss",
                            "yyyy/MM/dd tt hh:mm:ss",
                            "yyyy/MM/dd HH:mm:ss",
                            "yyyy/M/d HH:mm:ss",
                            "yyyy/M/d",
                            "yyyy/MM/dd"
                        };

        public CRUDObject()
        {
            appYear = getSettings.GetYear();
            getSettings.Execute();
            appSemester = getSettings.GetSemester();

        }

        public void Instance(String strConnection)
        {

            //sqlds = new SqlDataSource();
            //== 連結資料庫的連接字串 ConnectionString  ==
            //sqlds.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString;


        }

        //寫入 新申請帳號資料
        public Boolean Insert(VAccountForApply obj)
        {

            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from AccountForApply where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAcctApplyEmail] = obj.AcctApplyEmail;
            dr[obj.strAcctApplyPassword] = obj.AcctApplyPassword;
            dr[obj.strAcctApplyId] = obj.AcctApplyId;
            dr[obj.strAcctApplyStatus] = true;
            dr[obj.strAcctApplyBuildDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ds.Tables[0].Rows.Add(dr); ;
            return du.Update(ds);
        }

        //寫入 主檔
        public Boolean Insert(VEmployeeBase obj)
        {
            //== 撰寫SQL指令(Insert Into) ==
            //sqlds.InsertCommand = "Insert into EmployeeBasic(EmpNameCN,EmpNameEN,EmpBornPlace,Sex,EmpAddress,EmpTel,EmpEmail) values(@title,@test_time,@class,@summary,@article,@author)";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from EmployeeBase where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpIdno] = obj.EmpIdno;
            dr[obj.strEmpBirthDay] = obj.EmpBirthDay;
            dr[obj.strEmpPassportNo] = obj.EmpPassportNo;
            dr[obj.strEmpNameENFirst] = obj.EmpNameENFirst;
            dr[obj.strEmpNameENLast] = obj.EmpNameENLast;
            dr[obj.strEmpNameCN] = obj.EmpNameCN;
            dr[obj.strEmpSex] = (obj.EmpSex.Equals("請選擇")) ? "" : obj.EmpSex;
            dr[obj.strEmpCountry] = (obj.EmpCountry.Equals("請選擇")) ? "" : obj.EmpCountry;
            //dr[obj.strEmpHomeTown] = (obj.EmpHomeTown.Equals("請選擇")) ? "" :obj.EmpHomeTown;
            //dr[obj.strEmpBornProvince] = (obj.EmpBornProvince.Equals("請選擇")) ? "" : obj.EmpBornProvince;
            dr[obj.strEmpBornCity] = (obj.EmpBornCity.Equals("請選擇")) ? "" : obj.EmpBornCity;
            dr[obj.strEmpTelPub] = obj.EmpTelPub;
            dr[obj.strEmpTelPri] = obj.EmpTelPri;
            dr[obj.strEmpEmail] = obj.EmpEmail;
            dr[obj.strEmpTownAddressCode] = obj.EmpTownAddressCode;
            dr[obj.strEmpTownAddress] = obj.EmpTownAddress;
            dr[obj.strEmpAddressCode] = obj.EmpAddressCode;
            dr[obj.strEmpAddress] = obj.EmpAddress;
            dr[obj.strEmpCell] = obj.EmpCell;
            dr[obj.strEmpNowJobOrg] = obj.EmpNowJobOrg;
            dr[obj.strEmpNote] = obj.EmpNote;
            dr[obj.strEmpExpertResearch] = obj.EmpExpertResearch;
            dr[obj.strEmpPhotoUpload] = obj.EmpPhotoUpload;
            dr[obj.strEmpPhotoUploadName] = obj.EmpPhotoUploadName;
            dr[obj.strEmpIdnoUpload] = obj.EmpIdnoUpload;
            dr[obj.strEmpIdnoUploadName] = obj.EmpIdnoUploadName;
            dr[obj.strEmpDegreeUpload] = obj.EmpDegreeUpload;
            dr[obj.strEmpDegreeUploadName] = obj.EmpDegreeUploadName;
            //dr[obj.strEmpSelfTeachExperience] = obj.EmpSelfTeachExperience;
            //dr[obj.strEmpSelfReach] = obj.EmpSelfReach;
            //dr[obj.strEmpSelfDevelope] = obj.EmpSelfDevelope;
            //dr[obj.strEmpSelfSpecial] = obj.EmpSelfSpecial;
            //dr[obj.strEmpSelfImprove] = obj.EmpSelfImprove;
            //dr[obj.strEmpSelfContribute] = obj.EmpSelfContribute;
            //dr[obj.strEmpSelfCooperate] = obj.EmpSelfCooperate;
            //dr[obj.strEmpSelfTeachPlan] = obj.EmpSelfTeachPlan;
            //dr[obj.strEmpSelfLifePlan] = obj.EmpSelfLifePlan;
            dr[obj.strEmpNoTeachExp] = obj.EmpNoTeachExp;
            dr[obj.strEmpNoTeachCa] = obj.EmpNoTeachCa;
            dr[obj.strEmpNoHonour] = obj.EmpNoHonour;
            //dr[obj.strEmpStatus] = obj.EmpStatus; 
            dr[obj.strUpdateUserId] = obj.UpdateUserId;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        public Boolean InsertShort(VEmployeeBase obj)
        {
            //== 撰寫SQL指令(Insert Into) ==
            //sqlds.InsertCommand = "Insert into EmployeeBasic(EmpNameCN,EmpNameEN,EmpBornPlace,Sex,EmpAddress,EmpTel,EmpEmail) values(@title,@test_time,@class,@summary,@article,@author)";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from EmployeeBase where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpIdno] = obj.EmpIdno;
            dr[obj.strEmpBirthDay] = obj.EmpBirthDay;
            dr[obj.strEmpPassportNo] = obj.EmpPassportNo;
            dr[obj.strEmpNameENFirst] = obj.EmpNameENFirst;
            dr[obj.strEmpNameENLast] = obj.EmpNameENLast;
            dr[obj.strEmpNameCN] = obj.EmpNameCN;
            dr[obj.strEmpSex] = (obj.EmpSex.Equals("請選擇")) ? "" : obj.EmpSex;
            dr[obj.strEmpCell] = obj.EmpCell;
            dr[obj.strEmpNowJobOrg] = obj.EmpNowJobOrg;
            dr[obj.strEmpNoTeachExp] = obj.EmpNoTeachExp;
            dr[obj.strEmpNoTeachCa] = obj.EmpNoTeachCa;
            dr[obj.strEmpNoHonour] = obj.EmpNoHonour;
            //dr[obj.strEmpStatus] = obj.EmpStatus; 
            dr[obj.strEmpExpertResearch] = obj.EmpExpertResearch;
            dr[obj.strUpdateUserId] = obj.UpdateUserId;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        public Boolean InsertBackup(VEmployeeBase obj)
        {
            //== 撰寫SQL指令(Insert Into) ==
            //sqlds.InsertCommand = "Insert into EmployeeBasic(EmpNameCN,EmpNameEN,EmpBornPlace,Sex,EmpAddress,EmpTel,EmpEmail) values(@title,@test_time,@class,@summary,@article,@author)";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from EmployeeBaseBackup where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpIdno] = obj.EmpIdno;
            dr[obj.strEmpBirthDay] = obj.EmpBirthDay;
            dr[obj.strEmpPassportNo] = obj.EmpPassportNo;
            dr[obj.strEmpNameENFirst] = obj.EmpNameENFirst;
            dr[obj.strEmpNameENLast] = obj.EmpNameENLast;
            dr[obj.strEmpNameCN] = obj.EmpNameCN;
            dr[obj.strEmpSex] = (obj.EmpSex.Equals("請選擇")) ? "" : obj.EmpSex;
            dr[obj.strEmpCountry] = (obj.EmpCountry.Equals("請選擇")) ? "" : obj.EmpCountry;
            //dr[obj.strEmpHomeTown] = (obj.EmpHomeTown.Equals("請選擇")) ? "" :obj.EmpHomeTown;
            //dr[obj.strEmpBornProvince] = (obj.EmpBornProvince.Equals("請選擇")) ? "" : obj.EmpBornProvince;
            dr[obj.strEmpBornCity] = (obj.EmpBornCity.Equals("請選擇")) ? "" : obj.EmpBornCity;
            dr[obj.strEmpTelPub] = obj.EmpTelPub;
            dr[obj.strEmpTelPri] = obj.EmpTelPri;
            dr[obj.strEmpEmail] = obj.EmpEmail;
            dr[obj.strEmpTownAddressCode] = obj.EmpTownAddressCode;
            dr[obj.strEmpTownAddress] = obj.EmpTownAddress;
            dr[obj.strEmpAddressCode] = obj.EmpAddressCode;
            dr[obj.strEmpAddress] = obj.EmpAddress;
            dr[obj.strEmpCell] = obj.EmpCell;
            dr[obj.strEmpNowJobOrg] = obj.EmpNowJobOrg;
            dr[obj.strEmpNote] = obj.EmpNote;
            dr[obj.strEmpExpertResearch] = obj.EmpExpertResearch;
            dr[obj.strEmpPhotoUpload] = obj.EmpPhotoUpload;
            dr[obj.strEmpPhotoUploadName] = obj.EmpPhotoUploadName;
            dr[obj.strEmpIdnoUpload] = obj.EmpIdnoUpload;
            dr[obj.strEmpIdnoUploadName] = obj.EmpIdnoUploadName;
            dr[obj.strEmpDegreeUpload] = obj.EmpDegreeUpload;
            dr[obj.strEmpDegreeUploadName] = obj.EmpDegreeUploadName;
            dr[obj.strEmpSelfTeachExperience] = obj.EmpSelfTeachExperience;
            dr[obj.strEmpSelfReach] = obj.EmpSelfReach;
            dr[obj.strEmpSelfDevelope] = obj.EmpSelfDevelope;
            dr[obj.strEmpSelfSpecial] = obj.EmpSelfSpecial;
            dr[obj.strEmpSelfImprove] = obj.EmpSelfImprove;
            dr[obj.strEmpSelfContribute] = obj.EmpSelfContribute;
            dr[obj.strEmpSelfCooperate] = obj.EmpSelfCooperate;
            dr[obj.strEmpSelfTeachPlan] = obj.EmpSelfTeachPlan;
            dr[obj.strEmpSelfLifePlan] = obj.EmpSelfLifePlan;
            dr[obj.strEmpNoTeachExp] = obj.EmpNoTeachExp;
            dr[obj.strEmpNoTeachCa] = obj.EmpNoTeachCa;
            dr[obj.strEmpNoHonour] = obj.EmpNoHonour;
            //dr[obj.strEmpStatus] = obj.EmpStatus;
            dr[obj.strUpdateUserId] = obj.UpdateUserId;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }


        public Boolean InsertBackup(VApplyAudit obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from [ApplyAuditBackup] where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAppSn] = obj.AppSn;
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strEmpIdno] = obj.EmpIdno;
            dr[obj.strAppYear] = obj.AppYear;
            dr[obj.strAppSemester] = obj.AppSemester;
            dr[obj.strAppKindNo] = obj.AppKindNo;
            dr[obj.strAppWayNo] = obj.AppWayNo;
            dr[obj.strAppAttributeNo] = obj.AppAttributeNo;
            dr[obj.strAppUnitNo] = obj.AppUnitNo;
            dr[obj.strAppJobTitleNo] = obj.AppJobTitleNo;
            dr[obj.strAppJobTypeNo] = obj.AppJobTypeNo;
            dr[obj.strAppLawNumNo] = obj.AppLawNumNo;
            //dr[obj.strAppJournalUpload] = obj.AppJournalUpload;
            //dr[obj.strAppJournalUploadName] = obj.AppJournalUploadName;
            dr[obj.strAppPublicationName] = obj.AppPublicationName;
            dr[obj.strAppPublicationUploadName] = obj.AppPublicationUploadName;
            dr[obj.strAppDeclarationUpload] = obj.AppDeclarationUpload;
            dr[obj.strAppDeclarationUploadName] = obj.AppDeclarationUploadName;
            dr[obj.strAppPPMUpload] = obj.AppPPMUpload;
            dr[obj.strAppPPMUploadName] = obj.AppPPMUploadName;
            dr[obj.strAppOtherServiceUpload] = obj.AppOtherServiceUpload;
            dr[obj.strAppOtherServiceUploadName] = obj.AppOtherServiceUploadName;
            dr[obj.strAppOtherTeachingUpload] = obj.AppOtherTeachingUpload;
            dr[obj.strAppOtherTeachingUploadName] = obj.AppOtherTeachingUploadName;
            dr[obj.strAppDrCaUpload] = obj.AppDrCaUpload;
            dr[obj.strAppDrCaUploadName] = obj.AppDrCaUploadName;
            dr[obj.strAppTeacherCaUpload] = obj.AppTeacherCaUpload;
            dr[obj.strAppTeacherCaUploadName] = obj.AppTeacherCaUploadName;
            dr[obj.strAppResearchYear] = obj.AppResearchYear;
            dr[obj.strAppThesisAccuScore] = obj.AppThesisAccuScore;
            dr[obj.strAppRPIScore] = obj.AppRPIScore;
            dr[obj.strAppBuildDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dr[obj.strAppStage] = obj.AppStage; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)
            dr[obj.strAppStatus] = obj.AppStatus;
            dr[obj.strAppUserId] = obj.AppUserId;

            //多單增加欄位
            dr[obj.strAppRecommendors] = obj.AppRecommendors;
            dr[obj.strAppRecommendYear] = obj.AppRecommendYear;
            dr[obj.strAppRecommendUploadName] = obj.AppRecommendUploadName;
            dr[obj.strAppResumeUploadName] = obj.AppResumeUploadName;
            dr[obj.strThesisScoreUploadName] = obj.ThesisScoreUploadName;
            dr[obj.strAppSelfTeachExperience] = obj.AppSelfTeachExperience;
            dr[obj.strAppSelfReach] = obj.AppSelfReach;
            dr[obj.strAppSelfDevelope] = obj.AppSelfDevelope;
            dr[obj.strAppSelfSpecial] = obj.AppSelfSpecial;
            dr[obj.strAppSelfImprove] = obj.AppSelfImprove;
            dr[obj.strAppSelfContribute] = obj.AppSelfContribute;
            dr[obj.strAppSelfCooperate] = obj.AppSelfCooperate;
            dr[obj.strAppSelfTeachPlan] = obj.AppSelfTeachPlan;
            dr[obj.strAppSelfLifePlan] = obj.AppSelfLifePlan;

            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //寫入 新聘&升等共用延伸主擋 AppYear AppSemester 為預設
        public Boolean Insert(VApplyAudit obj, String userId)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from [ApplyAudit] where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strEmpIdno] = obj.EmpIdno;
            dr[obj.strAppYear] = obj.AppYear;
            dr[obj.strAppSemester] = obj.AppSemester;
            dr[obj.strAppKindNo] = obj.AppKindNo;
            dr[obj.strAppWayNo] = obj.AppWayNo;
            dr[obj.strAppAttributeNo] = (obj.AppAttributeNo.Equals("請選擇")) ? "" : obj.AppAttributeNo;
            dr[obj.strAppUnitNo] = (obj.AppUnitNo.Equals("請選擇")) ? "" : obj.AppUnitNo;
            dr[obj.strAppJobTitleNo] = (obj.AppJobTitleNo.Equals("請選擇")) ? "" : obj.AppJobTitleNo;
            dr[obj.strAppJobTypeNo] = (obj.AppJobTypeNo.Equals("請選擇")) ? "" : obj.AppJobTypeNo;
            dr[obj.strAppLawNumNo] = obj.AppLawNumNo;
            //dr[obj.strAppJournalUpload] = obj.AppJournalUpload;
            //dr[obj.strAppJournalUploadName] = obj.AppJournalUploadName;
            dr[obj.strAppPublicationName] = obj.AppPublicationName;
            dr[obj.strAppPublicationUploadName] = obj.AppPublicationUploadName;
            dr[obj.strAppDeclarationUpload] = obj.AppDeclarationUpload;
            dr[obj.strAppDeclarationUploadName] = obj.AppDeclarationUploadName;
            dr[obj.strAppPPMUpload] = obj.AppPPMUpload;
            dr[obj.strAppPPMUploadName] = obj.AppPPMUploadName;
            dr[obj.strAppOtherServiceUpload] = obj.AppOtherServiceUpload;
            dr[obj.strAppOtherServiceUploadName] = obj.AppOtherServiceUploadName;
            dr[obj.strAppOtherTeachingUpload] = obj.AppOtherTeachingUpload;
            dr[obj.strAppOtherTeachingUploadName] = obj.AppOtherTeachingUploadName;
            dr[obj.strAppDrCaUpload] = obj.AppDrCaUpload;
            dr[obj.strAppDrCaUploadName] = obj.AppDrCaUploadName;
            dr[obj.strAppTeacherCaUpload] = obj.AppTeacherCaUpload;
            dr[obj.strAppTeacherCaUploadName] = obj.AppTeacherCaUploadName;
            dr[obj.strAppSummaryCN] = obj.AppSummaryCN;
            dr[obj.strAppDegreeName] = obj.AppDegreeName;
            dr[obj.strAppDegreeUploadName] = obj.AppDegreeUploadName;
            dr[obj.strAppResearchYear] = obj.AppResearchYear;
            dr[obj.strAppThesisAccuScore] = obj.AppThesisAccuScore;
            dr[obj.strAppRPIScore] = obj.AppRPIScore;
            dr[obj.strAppBuildDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dr[obj.strAppModifyDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dr[obj.strAppStage] = obj.AppStage; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)
            dr[obj.strAppStep] = obj.AppStep;
            dr[obj.strAppUserId] = userId;
            dr[obj.strAppStatus] = obj.AppStatus;
            dr[obj.strAppUserId] = obj.AppUserId;
            dr[obj.strAppRecommendors] = obj.AppRecommendors;
            dr[obj.strAppRecommendYear] = obj.AppRecommendYear;
            dr[obj.strAppRecommendUploadName] = obj.AppRecommendUploadName;
            dr[obj.strAppResumeUploadName] = obj.AppResumeUploadName;
            dr[obj.strThesisScoreUploadName] = obj.ThesisScoreUploadName;
            dr[obj.strAppSelfTeachExperience] = obj.AppSelfTeachExperience;
            dr[obj.strAppSelfReach] = obj.AppSelfReach;
            dr[obj.strAppSelfDevelope] = obj.AppSelfDevelope;
            dr[obj.strAppSelfSpecial] = obj.AppSelfSpecial;
            dr[obj.strAppSelfImprove] = obj.AppSelfImprove;
            dr[obj.strAppSelfContribute] = obj.AppSelfContribute;
            dr[obj.strAppSelfCooperate] = obj.AppSelfCooperate;
            dr[obj.strAppSelfTeachPlan] = obj.AppSelfTeachPlan;
            dr[obj.strAppSelfLifePlan] = obj.AppSelfLifePlan;

            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }



        //寫入 新聘專用檔 KindNo=1 (不用)
        //public int Insert(VAppendEmployee obj)
        //{
        //    int intAppESn = 0;
        //    DataSet ds = new DataSet();
        //    SqlDataUpdater du = new SqlDataUpdater("select * from AppendEmployee where 1=2", ref ds, "A");
        //    DataRow dr = ds.Tables[0].NewRow();
        //    dr[obj.strAppSn] = obj.AppSn;
        //    dr[obj.strAppNowJobOrg] = obj.AppNowJobOrg;
        //    dr[obj.strAppNote] = obj.AppNote;
        //    dr[obj.strAppRecommendors] = obj.AppRecommendors;
        //    dr[obj.strAppRecommendYear] = obj.AppRecommendYear;
        //    dr[obj.strAppRecommendUpload] = obj.AppRecommendUpload;
        //    dr[obj.strAppRecommendUploadName] = obj.AppRecommendUploadName;
        //    ds.Tables[0].Rows.Add(dr);
        //    du.Update(ds);
        //    intAppESn = GetAppendEmployeeSn(obj.AppSn);
        //    return intAppESn;
        //}


        public Boolean Insert(VThesisScoreCount obj)
        {

            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from ThesisScoreCount where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strPT_EmpId] = obj.PT_EmpId;
            dr[obj.strPT_Year] = obj.PT_Year;
            dr[obj.strPT_InPerson] = obj.PT_InPerson;
            dr[obj.strPT_FSCI] = obj.PT_FSCI;
            dr[obj.strPT_FSSCI] = obj.PT_FSSCI;
            dr[obj.strPT_FEI] = obj.PT_FEI;
            dr[obj.strPT_FNSCI] = obj.PT_FNSCI;
            dr[obj.strPT_FOther] = obj.PT_FOther;
            dr[obj.strPT_NFCSCI] = obj.PT_NFCSCI;
            dr[obj.strPT_NFCSSCI] = obj.PT_NFCSSCI;
            dr[obj.strPT_NFCEI] = obj.PT_NFCEI;
            dr[obj.strPT_NFCNSCI] = obj.PT_NFCNSCI;
            dr[obj.strPT_NFCOther] = obj.PT_NFCOther;
            dr[obj.strPT_NFOCSCI] = obj.PT_NFOCSCI;
            dr[obj.strPT_NFOCSSCI] = obj.PT_NFOCSSCI;
            dr[obj.strPT_NFOCEI] = obj.PT_NFOCEI;
            dr[obj.strPT_NFOCNSCI] = obj.PT_NFOCNSCI;
            dr[obj.strPT_NFOCOther] = obj.PT_NFOCOther;
            dr[obj.strPT_Update] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);

        }

        private int GetAppendEmployeeSn(int AppSn)
        {
            int intAppSn = 0;
            String strSql = "SELECT AppESn FROM [AppendEmployee] WHERE [AppSn] = @Appsn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    intAppSn = Int32.Parse(dt.Rows[0]["AppESn"].ToString());
                }
                return intAppSn;
            }
        }

        //寫入 著作專用檔 vAppendPublication (不用)
        public Boolean Insert(VAppendPublication obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from AppendPublication where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAppSn] = obj.AppSn;
            dr[obj.strAppPCoAuthorUpload] = obj.AppPCoAuthorUpload;
            dr[obj.strAppPCoAuthorUploadName] = obj.AppPCoAuthorUploadName;
            dr[obj.strAppPSummaryCN] = obj.AppPSummaryCN;
            //dr[obj.strAppPPMUpload] = obj.AppPPMUpload;
            //dr[obj.strAppPPMUploadName] = obj.AppPPMUploadName;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        private int GetAppendPublication(int AppSn)
        {
            int intAppSn = 0;
            String strSql = "SELECT AppESn FROM [AppendPublication] WHERE [AppSn] = @Appsn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    intAppSn = Int32.Parse(dt.Rows[0]["AppESn"].ToString());
                }
                return intAppSn;
            }
        }

        //寫入學位 VAppendDegree
        public Boolean Insert(VAppendDegree obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from AppendDegree where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAppSn] = obj.AppSn;
            dr[obj.strAppDDegreeThesisName] = obj.AppDDegreeThesisName;
            dr[obj.strAppDDegreeThesisNameEng] = obj.AppDDegreeThesisNameEng;
            dr[obj.strAppDDegreeThesisUploadName] = obj.AppDDegreeThesisUploadName;
            dr[obj.strAppDFgnEduDeptSchoolAdmit] = obj.AppDFgnEduDeptSchoolAdmit;
            dr[obj.strAppDFgnDegreeName] = obj.AppDFgnDegreeName;
            dr[obj.strAppDFgnDegreeUploadName] = obj.AppDFgnDegreeUploadName;
            dr[obj.strAppDFgnGradeUpload] = obj.AppDFgnGradeUpload;
            dr[obj.strAppDFgnGradeUploadName] = obj.AppDFgnGradeUploadName;
            dr[obj.strAppDFgnSelectCourseUpload] = obj.AppDFgnSelectCourseUpload;
            dr[obj.strAppDFgnSelectCourseUploadName] = obj.AppDFgnSelectCourseUploadName;
            dr[obj.strAppDFgnEDRecordUpload] = obj.AppDFgnEDRecordUpload;
            dr[obj.strAppDFgnEDRecordUploadName] = obj.AppDFgnEDRecordUploadName;
            dr[obj.strAppDFgnJPAdmissionUpload] = obj.AppDFgnJPAdmissionUpload;
            dr[obj.strAppDFgnJPAdmissionUploadName] = obj.AppDFgnJPAdmissionUploadName;
            dr[obj.strAppDFgnJPGradeUpload] = obj.AppDFgnJPGradeUpload;
            dr[obj.strAppDFgnJPGradeUploadName] = obj.AppDFgnJPGradeUploadName;
            dr[obj.strAppDFgnJPEnrollCAUpload] = obj.AppDFgnJPEnrollCAUpload;
            dr[obj.strAppDFgnJPEnrollCAUploadName] = obj.AppDFgnJPEnrollCAUploadName;
            dr[obj.strAppDFgnJPDissertationPassUpload] = obj.AppDFgnJPDissertationPassUpload;
            dr[obj.strAppDFgnJPDissertationPassUploadName] = obj.AppDFgnJPDissertationPassUploadName;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //寫入升等延伸檔 VAppendPromote
        public Boolean Insert(VAppendPromote obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from AppendPromote where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAppSn] = obj.AppSn;
            dr[obj.strNowJobUnit] = obj.NowJobUnit;
            dr[obj.strNowJobTitle] = obj.NowJobTitle;
            dr[obj.strNowJobPosId] = obj.NowJobPosId;
            dr[obj.strNowJobYear] = obj.NowJobYear;
            dr[obj.strPBLClassUploadName] = obj.PBLClassUploadName;
            dr[obj.strRPIDiscountScore1] = obj.RPIDiscountScore1;
            dr[obj.strRPIDiscountScore1Name] = obj.RPIDiscountScore1Name;
            dr[obj.strRPIDiscountScore2] = obj.RPIDiscountScore2;
            dr[obj.strRPIDiscountScore2Name] = obj.RPIDiscountScore2Name;
            dr[obj.strRPIDiscountScore3] = obj.RPIDiscountScore3;
            dr[obj.strRPIDiscountScore3Name] = obj.RPIDiscountScore3Name;
            dr[obj.strRPIDiscountScore4] = obj.RPIDiscountScore4;
            dr[obj.strRPIDiscountScore4Name] = obj.RPIDiscountScore4Name;
            dr[obj.strRPIDiscountScore5] = obj.RPIDiscountScore5;
            dr[obj.strRPIDiscountTotal] = obj.RPIDiscountTotal;
            dr[obj.strRPIDiscountNo] = obj.RPIDiscountNo;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        private int GetAppendDegree(int AppSn)
        {
            int intAppSn = 0;
            String strSql = "SELECT AppESn FROM [AppendDegree] WHERE [AppSn] = @Appsn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    intAppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                }
                return intAppSn;
            }
        }


        //寫入 教師學歷資料
        public Boolean Insert(VTeacherEdu obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from TeacherEdu where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strEduLocal] = obj.EduLocal;
            dr[obj.strEduSchool] = obj.EduSchool;
            dr[obj.strEduDepartment] = obj.EduDepartment;
            dr[obj.strEduStartYM] = obj.EduStartYM;
            dr[obj.strEduEndYM] = obj.EduEndYM;
            dr[obj.strEduDegree] = obj.EduDegree;
            dr[obj.strEduDegreeType] = obj.EduDegreeType;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }


        //寫入 教師經歷資料
        public Boolean Insert(VTeacherExp obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from TeacherExp where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strExpOrginization] = obj.ExpOrginization;
            dr[obj.strExpUnit] = obj.ExpUnit;
            dr[obj.strExpJobTitle] = obj.ExpJobTitle;
            dr[obj.strExpJobType] = obj.ExpJobType;
            dr[obj.strExpStartYM] = obj.ExpStartYM;
            dr[obj.strExpEndYM] = obj.ExpEndYM;
            dr[obj.strExpUpload] = obj.ExpUpload;
            dr[obj.strExpUploadName] = obj.ExpUploadName;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //寫入 教師經歷資料
        public Boolean Insert(OTeacherExp obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from s10_expfcu where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strFcuEmpId] = obj.FcuEmpId;
            dr[obj.strFcuIdno] = obj.FcuIdno;
            dr[obj.strFcuPosName] = obj.FcuPosName;
            dr[obj.strFcuTitId] = obj.FcuTitId;
            dr[obj.strFcuUnit] = obj.FcuUnit;
            dr[obj.strExpStartYM] = obj.ExpStartYM;
            dr[obj.strExpEndYM] = obj.ExpEndYM;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //寫入 教師證發放資料 
        public Boolean Insert(VTeacherCa obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from TeacherCa where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strCaNumberCN] = obj.CaNumberCN;
            dr[obj.strCaNumber] = obj.CaNumber;
            dr[obj.strCaPublishSchool] = obj.CaPublishSchool;
            dr[obj.strCaStartYM] = obj.CaStartYM;
            dr[obj.strCaEndYM] = obj.CaEndYM;
            dr[obj.strCaUpload] = obj.CaUpload;
            dr[obj.strCaUploadName] = obj.CaUploadName;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }


        //寫入 學術獎勵、榮譽事項 
        public Boolean Insert(VTeacherHonour obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from TeacherHonour where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strHorYear] = obj.HorYear;
            dr[obj.strHorDescription] = obj.HorDescription;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }


        //教師上傳論文積分表 
        public Boolean Insert(VThesisScore obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from ThesisScore where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAppSn] = obj.AppSn;
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strSnNo] = obj.SnNo;
            dr[obj.strThesisPublishYearMonth] = obj.ThesisPublishYearMonth;
            dr[obj.strThesisResearchResult] = obj.ThesisResearchResult;
            dr[obj.strRRNo] = obj.RRNo;
            dr[obj.strThesisC] = obj.ThesisC;
            dr[obj.strThesisJ] = obj.ThesisJ;
            dr[obj.strThesisA] = obj.ThesisA;
            dr[obj.strThesisTotal] = obj.ThesisTotal;
            dr[obj.strThesisName] = obj.ThesisName;
            dr[obj.strThesisUploadName] = obj.ThesisUploadName;
            dr[obj.strThesisJournalRefUploadName] = obj.ThesisJournalRefUploadName;
            dr[obj.strIsCountRPI] = obj.IsCountRPI;
            dr[obj.strIsRepresentative] = obj.IsRepresentative;
            dr[obj.strThesisJournalRefCount] = obj.ThesisJournalRefCount;
            dr[obj.strThesisSummaryCN] = obj.ThesisSummaryCN;
            //dr[obj.strThesisCoAuthorUpload] = obj.ThesisCoAuthorUpload;
            dr[obj.strThesisCoAuthorUploadName] = obj.ThesisCoAuthorUploadName;
            dr[obj.strThesisBuildDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dr[obj.strThesisModifyDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        public Boolean InsertTemp(VThesisScore obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from ThesisScoreTemp where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strSnNo] = obj.SnNo;
            dr["ThesisYear"] = obj.ThesisYear;
            dr[obj.strThesisPublishYearMonth] = obj.ThesisPublishYearMonth;
            dr[obj.strThesisResearchResult] = obj.ThesisResearchResult;
            dr[obj.strRRNo] = obj.RRNo;
            dr[obj.strThesisC] = obj.ThesisC;
            dr[obj.strThesisJ] = obj.ThesisJ;
            dr[obj.strThesisA] = obj.ThesisA;
            dr[obj.strThesisTotal] = obj.ThesisTotal;
            dr[obj.strThesisName] = obj.ThesisName;
            dr[obj.strThesisUploadName] = obj.ThesisUploadName;
            dr[obj.strThesisJournalRefUploadName] = obj.ThesisJournalRefUploadName;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //教師上傳論文積分表 取得最新序號
        public int GetVThesisScoreSn()
        {
            int intThesisSn = 0;
            String strSql = "SELECT MAX(ThesisSn) FROM [ThesisScore]";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VThesisScore vThesisScore = new VThesisScore();
            if (hasData(ds))
            {
                intThesisSn = Int32.Parse(dt.Rows[0][0].ToString());
            }
            return intThesisSn;
        }

        //教師上傳論文積分表 取得最後流水號
        public int GetVThesisScoreSnNo(int empSn)
        {
            int intSnNo = 0;
            String strSql = "SELECT TOP(1) SnNo FROM [ThesisScore] where EmpSn = @empSn Order by SnNo Desc";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@empSn", empSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                VThesisScore vThesisScore = new VThesisScore();
                if (hasData(ds))
                {
                    intSnNo = Int32.Parse(dt.Rows[0][0].ToString());
                }
                return intSnNo;
            }
        }

        //取得所有論文積分資料
        public DataTable GetVThesisScoreAll(int empSn)
        {

            String strSql = "SELECT * FROM ThesisScore WHERE EmpSn = @empSn Order by SnNo ";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@empSn", empSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                return dt;
            }
        }

        //
        public int GetThesisCountRPI(int appSn)
        {
            int intCnt = 0;
            String strSql = "SELECT COUNT(*) AS cnt FROM ThesisScore WHERE AppSn = @appSn and IsCountRPI = 'True' ";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@appSn", appSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                VThesisScore vThesisScore = new VThesisScore();
                if (hasData(ds))
                {
                    intCnt = Int32.Parse(dt.Rows[0][0].ToString());
                }
                return intCnt;
            }
        }

        //教師上傳論文積分表  取得申請人上傳總數 
        public int GetVThesisScoreTotalCount(int empSn, int appSn)
        {
            int intCnt = 0;
            String strSql = "SELECT COUNT(*) AS cnt FROM ThesisScore WHERE EmpSn = @empSn AND AppSn = @appSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@empSn", empSn);
                command.Parameters.AddWithValue("@appSn", appSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                VThesisScore vThesisScore = new VThesisScore();
                if (hasData(ds))
                {
                    intCnt = Int32.Parse(dt.Rows[0][0].ToString());
                }
                return intCnt;
            }
        }

        //寫入 以學位送審論文教師(Type=1)&口試委員名單(Type=2)
        public Boolean Insert(VThesisOral obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from ThesisOralList where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            //dr[obj.strThesisOralSn] = obj.ThesisOralSn;
            dr[obj.strThesisOralAppSn] = obj.ThesisOralAppSn;
            dr[obj.strThesisOralType] = obj.ThesisOralType;
            dr[obj.strThesisOralName] = obj.ThesisOralName;
            dr[obj.strThesisOralTitle] = obj.ThesisOralTitle;
            dr[obj.strThesisOralUnit] = obj.ThesisOralUnit;
            dr[obj.strThesisOralOther] = obj.ThesisOralOther;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //寫入簽核卡關檔
        public Boolean Insert(VAuditExecute obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from AuditExecute where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAppSn] = obj.AppSn;
            dr[obj.strExecuteStage] = obj.ExecuteStage;
            dr[obj.strExecuteStep] = obj.ExecuteStep;
            dr[obj.strExecuteRoleName] = obj.ExecuteRoleName;
            dr[obj.strExecuteAuditorSnEmpId] = obj.ExecuteAuditorSnEmpId;
            dr[obj.strExecuteAuditorName] = obj.ExecuteAuditorName;
            dr[obj.strExecuteAuditorEmail] = obj.ExecuteAuditorEmail;
            dr[obj.strExecuteAccept] = obj.ExecuteAccept;
            dr[obj.strExecuteCommentsA] = obj.ExecuteCommentsA;
            dr[obj.strExecuteCommentsB] = obj.ExecuteCommentsB;
            dr[obj.strExecuteStrengths] = obj.ExecuteStrengths;
            dr[obj.strExecuteWeaknesses] = obj.ExecuteWeaknesses;
            dr[obj.strExecuteAllTotalScore] = obj.ExecuteAllTotalScore;
            dr[obj.strExecuteLevelScore] = obj.ExecuteLevelScore;

            dr[obj.strExecuteWSSubject] = obj.ExecuteWSSubject;
            dr[obj.strExecuteWSMethod] = obj.ExecuteWSMethod;
            dr[obj.strExecuteWSContribute] = obj.ExecuteWSContribute;
            dr[obj.strExecuteWSAchievement] = obj.ExecuteWSAchievement;
            dr[obj.strExecuteWTotalScore] = obj.ExecuteWTotalScore;
            dr[obj.strExecuteDate] = DBNull.Value;
            dr[obj.strExecuteBngDate] = DateTime.ParseExact(obj.ExecuteBngDate.ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            dr[obj.strExecuteEndDate] = DateTime.ParseExact(obj.ExecuteEndDate.ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            dr[obj.strExecutePass] = obj.ExecutePass;
            dr[obj.strExecuteStatus] = obj.ExecuteStatus;
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);

        }

        //寫入登入檔案 VAccountForAudit
        public Boolean Insert(VAccountForAudit obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from AccountForAudit where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAcctAppSn] = obj.AcctAppSn;
            dr[obj.strAcctEmail] = obj.AcctEmail;
            dr[obj.strAcctAuditorSnEmpId] = obj.AcctAuditorSnEmpId;
            dr[obj.strAcctPassword] = obj.AcctPassword;
            dr[obj.strAcctStatus] = obj.AcctStatus;
            dr[obj.strAcctBuildDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        public Boolean Insert(VAccountForManage obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from AccountForManage where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strAcctEmpId] = obj.AcctEmpId;
            dr[obj.strAcctEmail] = obj.AcctEmail;
            dr[obj.strAcctPassword] = obj.AcctPassword;
            dr[obj.strAcctRole] = obj.AcctRole;
            dr[obj.strAcctStatus] = obj.AcctStatus;
            dr[obj.strAcctBuildDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        public Boolean Insert(VTeacherTmuExp obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from TeacherTmuExp where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strExpUnitId] = obj.ExpUnitId;
            dr[obj.strExpTitleId] = obj.ExpTitleId;
            dr[obj.strExpPosId] = obj.ExpPosId;
            dr[obj.strExpQId] = obj.ExpQId;
            dr[obj.strExpStartDate] = obj.ExpStartDate;
            dr[obj.strExpEndDate] = obj.ExpEndDate;
            dr[obj.strExpUserId] = obj.ExpUserId;
            dr[obj.strExpUpdateDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        public Boolean Insert(VTeacherTmuLesson obj)
        {

            GetSettings getSettings = new GetSettings();
            getSettings.Execute();
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from TeacherTmuLesson where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strEmpSn] = obj.EmpSn;
            dr[obj.strLessonYear] = obj.LessonYear; //obj.LessonYear;
            dr[obj.strLessonSemester] = obj.LessonSemester; // obj.LessonSemester;
            dr[obj.strLessonDeptLevel] = obj.LessonDeptLevel;
            dr[obj.strLessonClass] = obj.LessonClass;
            dr[obj.strLessonHours] = Convert.ToDecimal(obj.LessonHours);
            dr[obj.strLessonCreditHours] = Convert.ToDecimal(obj.LessonCreditHours);
            dr[obj.strLessonEvaluate] = obj.LessonEvaluate;
            dr[obj.strLessonUserId] = obj.LessonUserId;
            dr[obj.strLessonUpdateDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //寫入 系統設定開放時間 
        public Boolean Insert(VSystemOpendate obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("select * from [SystemOpendate] where 1=2", ref ds, "A");
            DataRow dr = ds.Tables[0].NewRow();
            dr[obj.strSmtr] = obj.Smtr;
            dr[obj.strSemester] = obj.Semester;
            dr[obj.strKindNo] = obj.KindNo;
            dr[obj.strTypeNo] = obj.TypeNo;
            dr[obj.strApplyBeginTime] = obj.ApplyBeginTime;
            dr[obj.strApplyEndTime] = obj.ApplyEndTime;
            dr[obj.strAuditBeginTime] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dr[obj.strAuditEndTime] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dr[obj.strAdminBeginTime] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            dr[obj.strAdminEndTime] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            ds.Tables[0].Rows.Add(dr);
            return du.Update(ds);
        }

        //更新 基本資料主檔
        public Boolean Update(VEmployeeBase obj)
        {
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpIdno] IN (@EmpIdn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@EmpIdn", obj.EmpIdno);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "EmployeeBase");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strEmpIdno] = obj.EmpIdno;
                dr[obj.strEmpBirthDay] = obj.EmpBirthDay;
                dr[obj.strEmpPassportNo] = obj.EmpPassportNo;
                dr[obj.strEmpNameENFirst] = obj.EmpNameENFirst;
                dr[obj.strEmpNameENLast] = obj.EmpNameENLast;
                dr[obj.strEmpNameCN] = obj.EmpNameCN;
                dr[obj.strEmpSex] = (obj.EmpSex.Equals("請選擇")) ? "" : obj.EmpSex;
                dr[obj.strEmpCountry] = (obj.EmpCountry.Equals("請選擇")) ? "" : obj.EmpCountry;
                //dr[obj.strEmpHomeTown] = obj.EmpHomeTown;
                //dr[obj.strEmpBornProvince] = obj.EmpBornProvince;
                dr[obj.strEmpBornCity] = (obj.EmpBornCity.Equals("請選擇")) ? "" : obj.EmpBornCity;
                dr[obj.strEmpTelPub] = obj.EmpTelPub;
                dr[obj.strEmpTelPri] = obj.EmpTelPri;
                dr[obj.strEmpEmail] = obj.EmpEmail;
                dr[obj.strEmpTownAddressCode] = obj.EmpTownAddressCode;
                dr[obj.strEmpTownAddress] = obj.EmpTownAddress;
                dr[obj.strEmpAddressCode] = obj.EmpAddressCode;
                dr[obj.strEmpAddress] = obj.EmpAddress;
                dr[obj.strEmpCell] = obj.EmpCell;
                dr[obj.strEmpNowJobOrg] = obj.EmpNowJobOrg;
                dr[obj.strEmpNote] = obj.EmpNote;
                dr[obj.strEmpExpertResearch] = obj.EmpExpertResearch;
                dr[obj.strEmpPhotoUpload] = obj.EmpPhotoUpload;
                dr[obj.strEmpPhotoUploadName] = obj.EmpPhotoUploadName;
                dr[obj.strEmpIdnoUpload] = obj.EmpIdnoUpload;
                dr[obj.strEmpIdnoUploadName] = obj.EmpIdnoUploadName;
                dr[obj.strEmpDegreeUpload] = obj.EmpDegreeUpload;
                dr[obj.strEmpDegreeUploadName] = obj.EmpDegreeUploadName;
                dr[obj.strEmpSelfTeachExperience] = obj.EmpSelfTeachExperience;
                dr[obj.strEmpSelfReach] = obj.EmpSelfReach;
                dr[obj.strEmpSelfDevelope] = obj.EmpSelfDevelope;
                dr[obj.strEmpSelfSpecial] = obj.EmpSelfSpecial;
                dr[obj.strEmpSelfImprove] = obj.EmpSelfImprove;
                dr[obj.strEmpSelfContribute] = obj.EmpSelfContribute;
                dr[obj.strEmpSelfCooperate] = obj.EmpSelfCooperate;
                dr[obj.strEmpSelfTeachPlan] = obj.EmpSelfTeachPlan;
                dr[obj.strEmpSelfLifePlan] = obj.EmpSelfLifePlan;
                dr[obj.strEmpNoTeachExp] = obj.EmpNoTeachExp;
                dr[obj.strEmpNoTeachCa] = obj.EmpNoTeachCa;
                dr[obj.strEmpNoHonour] = obj.EmpNoHonour;
                //dr[obj.strEmpStatus] = obj.EmpStatus;
                dr[obj.strUpdateUserId] = obj.UpdateUserId;

                //foreach (DataRow r in ds.Tables["EmployeeBase"].Rows)
                //{
                //    r.SetModified();
                //}

                return du.Update(ds, "EmployeeBase");
                //return du.Update(ds);
            }
        }

        //更新 基本資料主檔EmpStatus
        public Boolean UpdateEmployeeStatus(int empSn)
        {
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpSn] = @empSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@empSn", empSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "EmployeeBase");
                DataRow dr = ds.Tables[0].Rows[0];
                VEmployeeBase vEmployeeBase = new VEmployeeBase();
                dr[vEmployeeBase.strEmpStatus] = true;
                return du.Update(ds, "EmployeeBase");
            }
        }

        //更新EmpNoTeachExp
        public Boolean UpdateEmployeeNoTeachExp(VEmployeeBase vEmpBase)
        {
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpSn] = @empSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@empSn", vEmpBase.EmpSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "EmployeeBase");
                DataRow dr = ds.Tables[0].Rows[0];
                VEmployeeBase vEmployeeBase = new VEmployeeBase();
                dr[vEmployeeBase.strEmpNoTeachExp] = vEmpBase.EmpNoTeachExp;
                return du.Update(ds, "EmployeeBase");
            }
        }

        //更新 EmpNoTeachCa
        public Boolean UpdateEmployeeNoTeachCa(VEmployeeBase vEmpBase)
        {
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpSn] =  @empSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@empSn", vEmpBase.EmpSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "EmployeeBase");
                DataRow dr = ds.Tables[0].Rows[0];
                VEmployeeBase vEmployeeBase = new VEmployeeBase();
                dr[vEmployeeBase.strEmpNoTeachCa] = vEmpBase.EmpNoTeachCa;
                return du.Update(ds, "EmployeeBase");
            }
        }

        //更新EmpHasHonour
        public Boolean UpdateEmployeeNoHonour(VEmployeeBase vEmpBase)
        {
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpSn] =  @empSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@empSn", vEmpBase.EmpSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "EmployeeBase");
                DataRow dr = ds.Tables[0].Rows[0];
                VEmployeeBase vEmployeeBase = new VEmployeeBase();
                dr[vEmployeeBase.strEmpNoHonour] = vEmpBase.EmpNoHonour;
                return du.Update(ds, "EmployeeBase");
            }
        }

        //更新 申請共用檔
        public Boolean Update(VApplyAudit obj)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] IN (@AppSn)";

            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AppSn", obj.AppSn);
                DataSet ds = new DataSet();
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppKindNo] = obj.AppKindNo;
                dr[obj.strAppWayNo] = obj.AppWayNo;
                dr[obj.strAppAttributeNo] = (obj.AppAttributeNo.Equals("請選擇")) ? "" : obj.AppAttributeNo;
                dr[obj.strAppUnitNo] = (obj.AppUnitNo.Equals("請選擇")) ? "" : obj.AppUnitNo;
                dr[obj.strAppJobTitleNo] = (obj.AppJobTitleNo.Equals("請選擇")) ? "" : obj.AppJobTitleNo;
                dr[obj.strAppJobTypeNo] = (obj.AppJobTypeNo.Equals("請選擇")) ? "" : obj.AppJobTypeNo;
                dr[obj.strAppLawNumNo] = obj.AppLawNumNo;
                //dr[obj.strAppJournalUpload] = obj.AppJournalUpload;
                //dr[obj.strAppJournalUploadName] = obj.AppJournalUploadName;
                dr[obj.strAppPublicationName] = obj.AppPublicationName;
                dr[obj.strAppPublicationUploadName] = obj.AppPublicationUploadName;
                dr[obj.strAppDeclarationUpload] = obj.AppDeclarationUpload;
                dr[obj.strAppDeclarationUploadName] = obj.AppDeclarationUploadName;
                dr[obj.strAppPPMUpload] = obj.AppPPMUpload;
                dr[obj.strAppPPMUploadName] = obj.AppPPMUploadName;
                dr[obj.strAppOtherServiceUpload] = obj.AppOtherServiceUpload;
                dr[obj.strAppOtherServiceUploadName] = obj.AppOtherServiceUploadName;
                dr[obj.strAppOtherTeachingUpload] = obj.AppOtherTeachingUpload;
                dr[obj.strAppOtherTeachingUploadName] = obj.AppOtherTeachingUploadName;
                dr[obj.strAppDrCaUpload] = obj.AppDrCaUpload;
                dr[obj.strAppDrCaUploadName] = obj.AppDrCaUploadName;
                dr[obj.strAppTeacherCaUpload] = obj.AppTeacherCaUpload;
                dr[obj.strAppTeacherCaUploadName] = obj.AppTeacherCaUploadName;
                dr[obj.strAppSummaryCN] = obj.AppSummaryCN;
                dr[obj.strAppDegreeName] = obj.AppDegreeName;
                dr[obj.strAppDegreeUploadName] = obj.AppDegreeUploadName;

                dr[obj.strAppRecommendors] = obj.AppRecommendors;
                dr[obj.strAppRecommendYear] = obj.AppRecommendYear;
                dr[obj.strAppRecommendUploadName] = obj.AppRecommendUploadName;
                dr[obj.strAppResumeUploadName] = obj.AppResumeUploadName;
                dr[obj.strThesisScoreUploadName] = obj.ThesisScoreUploadName;
                dr[obj.strAppSelfTeachExperience] = obj.AppSelfTeachExperience;
                dr[obj.strAppSelfReach] = obj.AppSelfReach;
                dr[obj.strAppSelfDevelope] = obj.AppSelfDevelope;
                dr[obj.strAppSelfSpecial] = obj.AppSelfSpecial;
                dr[obj.strAppSelfImprove] = obj.AppSelfImprove;
                dr[obj.strAppSelfContribute] = obj.AppSelfContribute;
                dr[obj.strAppSelfCooperate] = obj.AppSelfCooperate;
                dr[obj.strAppSelfTeachPlan] = obj.AppSelfTeachPlan;
                dr[obj.strAppSelfLifePlan] = obj.AppSelfLifePlan;
                dr[obj.strReasearchResultUploadName] = obj.ReasearchResultUploadName;

                dr[obj.strAppModifyDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dr[obj.strAppStage] = obj.AppStage; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)
                dr[obj.strAppUserId] = obj.AppUserId;
                return du.Update(ds, "ApplyAudit");
            }
        }
        //更新 申請共用檔
        public Boolean UpdateTime(VApplyAudit obj)
        {
            SqlCommand command = new SqlCommand();

            using (SqlConnection conn1 = new SqlConnection(Conn.ConnectionString))
            {
                command.Connection = conn1;
                command.CommandType = CommandType.Text;
                conn1.Open();
                try
                {
                    command.CommandText = "Update [ApplyAudit] SET AppYear =@AppYear ,AppSemester = @AppSemester  WHERE [AppSn] IN (@AppSn)";
                    command.Parameters.AddWithValue("@AppYear", obj.AppYear);
                    command.Parameters.AddWithValue("@AppSemester", obj.AppSemester);
                    command.Parameters.AddWithValue("@AppSn", obj.AppSn);
                    //DataRow dr = ds.Tables[0].Rows[0];
                    //dr["AppYear"] = obj.AppYear;
                    //dr["AppSemester"] = obj.AppSemester;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException e1)
                {
                    return false;
                }
            }
        }

        //更新 申請共用檔
        public Boolean UpdateShort(VApplyAudit obj)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] IN (@AppSn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AppSn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppKindNo] = obj.AppKindNo;
                dr[obj.strAppWayNo] = obj.AppWayNo;
                dr[obj.strAppAttributeNo] = (obj.AppAttributeNo.Equals("請選擇")) ? "" : obj.AppAttributeNo;
                dr[obj.strAppUnitNo] = (obj.AppUnitNo.Equals("請選擇")) ? "" : obj.AppUnitNo;
                dr[obj.strAppJobTitleNo] = (obj.AppJobTitleNo.Equals("請選擇")) ? "" : obj.AppJobTitleNo;
                dr[obj.strAppJobTypeNo] = (obj.AppJobTypeNo.Equals("請選擇")) ? "" : obj.AppJobTypeNo;
                dr[obj.strAppLawNumNo] = obj.AppLawNumNo;
                //dr[obj.strAppJournalUpload] = obj.AppJournalUpload;
                //dr[obj.strAppJournalUploadName] = obj.AppJournalUploadName;

                dr[obj.strAppRecommendors] = obj.AppRecommendors;
                dr[obj.strAppRecommendYear] = obj.AppRecommendYear;
                dr[obj.strAppRecommendUploadName] = obj.AppRecommendUploadName;
                dr[obj.strAppResumeUploadName] = obj.AppResumeUploadName;
                dr[obj.strThesisScoreUploadName] = obj.ThesisScoreUploadName;
                dr[obj.strAppSelfTeachExperience] = obj.AppSelfTeachExperience;
                dr[obj.strAppSelfReach] = obj.AppSelfReach;
                dr[obj.strAppSelfDevelope] = obj.AppSelfDevelope;
                dr[obj.strAppSelfSpecial] = obj.AppSelfSpecial;
                dr[obj.strAppSelfImprove] = obj.AppSelfImprove;
                dr[obj.strAppSelfContribute] = obj.AppSelfContribute;
                dr[obj.strAppSelfCooperate] = obj.AppSelfCooperate;
                dr[obj.strAppSelfTeachPlan] = obj.AppSelfTeachPlan;
                dr[obj.strAppSelfLifePlan] = obj.AppSelfLifePlan;

                dr[obj.strAppModifyDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dr[obj.strAppStage] = obj.AppStage; //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)
                dr[obj.strAppUserId] = obj.AppUserId;
                return du.Update(ds, "ApplyAudit");
            }
        }

        public Boolean UpdateTemp(VApplyAudit obj)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpSn] IN (@EmpSn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@EmpSn", obj.EmpSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    dr[obj.strAppRecommendors] = obj.AppRecommendors;
                    dr[obj.strAppRecommendYear] = obj.AppRecommendYear;
                    dr[obj.strAppRecommendUploadName] = obj.AppRecommendUploadName;
                    dr[obj.strAppResumeUploadName] = obj.AppResumeUploadName;
                    dr[obj.strThesisScoreUploadName] = obj.ThesisScoreUploadName;
                    dr[obj.strAppSelfTeachExperience] = obj.AppSelfTeachExperience;
                    dr[obj.strAppSelfReach] = obj.AppSelfReach;
                    dr[obj.strAppSelfDevelope] = obj.AppSelfDevelope;
                    dr[obj.strAppSelfSpecial] = obj.AppSelfSpecial;
                    dr[obj.strAppSelfImprove] = obj.AppSelfImprove;
                    dr[obj.strAppSelfContribute] = obj.AppSelfContribute;
                    dr[obj.strAppSelfCooperate] = obj.AppSelfCooperate;
                    dr[obj.strAppSelfTeachPlan] = obj.AppSelfTeachPlan;
                    dr[obj.strAppSelfLifePlan] = obj.AppSelfLifePlan;

                    return du.Update(ds, "ApplyAudit");
                }
                else
                {
                    return false;
                }
            }
        }

        public Boolean UpdatePartial(VApplyAudit obj)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] IN (@Appsn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppResearchYear] = obj.AppResearchYear;
                dr[obj.strAppThesisAccuScore] = obj.AppThesisAccuScore;
                dr[obj.strAppRPIScore] = obj.AppRPIScore;
                dr[obj.strAppModifyDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                return du.Update(ds, "ApplyAudit");
            }
        }

        public Boolean UpdateOther(VApplyAudit obj)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] IN (@Appsn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppPPMUpload] = obj.AppPPMUpload;
                dr[obj.strAppPPMUploadName] = obj.AppPPMUploadName;
                dr[obj.strAppOtherServiceUpload] = obj.AppOtherServiceUpload;
                dr[obj.strAppOtherServiceUploadName] = obj.AppOtherServiceUploadName;
                dr[obj.strAppOtherTeachingUpload] = obj.AppOtherTeachingUpload;
                dr[obj.strAppOtherTeachingUploadName] = obj.AppOtherTeachingUploadName;
                dr[obj.strAppDrCaUpload] = obj.AppDrCaUpload;
                dr[obj.strAppDrCaUploadName] = obj.AppDrCaUploadName;
                dr[obj.strAppTeacherCaUpload] = obj.AppTeacherCaUpload;
                dr[obj.strAppTeacherCaUploadName] = obj.AppTeacherCaUploadName;
                return du.Update(ds, "ApplyAudit");
            }
        }

        public Boolean UpdateSelf(VApplyAudit obj)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] IN (@Appsn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppSelfTeachExperience] = obj.AppSelfTeachExperience;
                dr[obj.strAppSelfReach] = obj.AppSelfReach;
                dr[obj.strAppSelfDevelope] = obj.AppSelfDevelope;
                dr[obj.strAppSelfSpecial] = obj.AppSelfSpecial;
                dr[obj.strAppSelfImprove] = obj.AppSelfImprove;
                dr[obj.strAppSelfContribute] = obj.AppSelfContribute;
                dr[obj.strAppSelfCooperate] = obj.AppSelfCooperate;
                dr[obj.strAppSelfTeachPlan] = obj.AppSelfTeachPlan;
                dr[obj.strAppSelfLifePlan] = obj.AppSelfLifePlan;
                return du.Update(ds, "ApplyAudit");
            }
        }

        public Boolean UpdateRPI(VApplyAudit obj)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] IN (@Appsn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppRPIScore] = obj.AppRPIScore;
                return du.Update(ds, "ApplyAudit");
            }
        }

        //更新 ApplyAudit 中的 審核Stage與Step的狀態
        public Boolean UpdateApplyAuditStageStepStatus(VApplyAudit vApplyAudit)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] = @Appsn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", vApplyAudit.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[vApplyAudit.strAppStage] = vApplyAudit.AppStage;
                //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)
                dr[vApplyAudit.strAppStep] = vApplyAudit.AppStep;
                dr[vApplyAudit.strAppStatus] = vApplyAudit.AppStatus;
                return du.Update(ds, "ApplyAudit");
            }
        }

        //更新 ApplyAudit 中的 審核Stage與Step的狀態
        public Boolean UpdateApplyAuditStageStep(VApplyAudit vApplyAudit)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] = @Appsn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", vApplyAudit.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ApplyAudit");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[vApplyAudit.strAppStage] = vApplyAudit.AppStage;
                //0:申請 學科(1) 系級教評(2) 院教評I(3) 院級外審(4) 院教評II (5) 校級外審(6) 校教評(7)
                dr[vApplyAudit.strAppStep] = vApplyAudit.AppStep;
                return du.Update(ds, "ApplyAudit");
            }
        }

        //更新 AuditExecute 
        public Boolean UpdateAuditExecuteStatus(VAuditExecute vAuditExecute)
        {
            String strSql = "SELECT * FROM [AuditExecute] WHERE [AppSn] = @Appsn and ExecuteStage = @ExecuteStage and ExecuteStep = @ExecuteStep";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", vAuditExecute.AppSn);
                command.Parameters.AddWithValue("@ExecuteStage", vAuditExecute.ExecuteStage);
                command.Parameters.AddWithValue("@ExecuteStep", vAuditExecute.ExecuteStep);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AuditExecute");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[vAuditExecute.strExecutePass] = vAuditExecute.ExecutePass;
                dr[vAuditExecute.strExecuteStatus] = vAuditExecute.ExecuteStatus;
                dr[vAuditExecute.strExecuteDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                dr[vAuditExecute.strExecuteReturnReason] = vAuditExecute.ExecuteReturnReason;
                dr[vAuditExecute.strExecuteCommentsA] = vAuditExecute.ExecuteCommentsA;
                return du.Update(ds, "AuditExecute");
            }
        }


        //更新 新聘延伸專用檔
        //public Boolean Update(VAppendEmployee obj)
        //{
        //    String strSql = "SELECT * FROM [AppendEmployee] WHERE [AppSn] IN ('" + obj.AppSn + "')";
        //    DataSet ds = new DataSet();
        //    SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "M", "AppendEmployee");
        //    DataRow dr = ds.Tables[0].Rows[0];
        //    dr[obj.strAppSn] = obj.AppSn;
        //    dr[obj.strAppNowJobOrg] = obj.AppNowJobOrg;
        //    dr[obj.strAppNote] = obj.AppNote;
        //    dr[obj.strAppRecommendors] = obj.AppRecommendors;
        //    dr[obj.strAppRecommendYear] = obj.AppRecommendYear;
        //    dr[obj.strAppRecommendUpload] = obj.AppRecommendUpload;
        //    dr[obj.strAppRecommendUploadName] = obj.AppRecommendUploadName;
        //    return du.Update(ds, "AppendEmployee");
        //}

        //更新 著作延伸專用檔
        public Boolean Update(VAppendPublication obj)
        {
            String strSql = "SELECT * FROM [AppendPublication] WHERE [AppPSn] IN (@Appsn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", obj.AppPSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AppendPublication");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppPCoAuthorUpload] = obj.AppPCoAuthorUpload;
                dr[obj.strAppPCoAuthorUploadName] = obj.AppPCoAuthorUploadName;
                dr[obj.strAppPSummaryCN] = obj.AppPSummaryCN;
                //dr[obj.strAppPPMUpload] = obj.AppPPMUpload;
                //dr[obj.strAppPPMUploadName] = obj.AppPPMUploadName;
                return du.Update(ds, "AppendPublication");
            }
        }

        //更新 學位延伸專用檔
        public Boolean Update(VAppendDegree obj)
        {
            String strSql = "SELECT * FROM [AppendDegree] WHERE [AppDSn] IN (@AppDSn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AppDSn", obj.AppDSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AppendPublication");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppDDegreeThesisName] = obj.AppDDegreeThesisName;
                dr[obj.strAppDDegreeThesisNameEng] = obj.AppDDegreeThesisNameEng;
                dr[obj.strAppDDegreeThesisUploadName] = obj.AppDDegreeThesisUploadName;
                dr[obj.strAppDFgnEduDeptSchoolAdmit] = obj.AppDFgnEduDeptSchoolAdmit;
                dr[obj.strAppDFgnDegreeUploadName] = obj.AppDFgnDegreeUploadName;
                dr[obj.strAppDFgnGradeUpload] = obj.AppDFgnGradeUpload;
                dr[obj.strAppDFgnGradeUploadName] = obj.AppDFgnGradeUploadName;
                dr[obj.strAppDFgnSelectCourseUpload] = obj.AppDFgnSelectCourseUpload;
                dr[obj.strAppDFgnSelectCourseUploadName] = obj.AppDFgnSelectCourseUploadName;
                dr[obj.strAppDFgnEDRecordUpload] = obj.AppDFgnEDRecordUpload;
                dr[obj.strAppDFgnEDRecordUploadName] = obj.AppDFgnEDRecordUploadName;
                dr[obj.strAppDFgnJPAdmissionUpload] = obj.AppDFgnJPAdmissionUpload;
                dr[obj.strAppDFgnJPAdmissionUploadName] = obj.AppDFgnJPAdmissionUploadName;
                dr[obj.strAppDFgnJPGradeUpload] = obj.AppDFgnJPGradeUpload;
                dr[obj.strAppDFgnJPGradeUploadName] = obj.AppDFgnJPGradeUploadName;
                dr[obj.strAppDFgnJPEnrollCAUpload] = obj.AppDFgnJPEnrollCAUpload;
                dr[obj.strAppDFgnJPEnrollCAUploadName] = obj.AppDFgnJPEnrollCAUploadName;
                dr[obj.strAppDFgnJPDissertationPassUpload] = obj.AppDFgnJPDissertationPassUpload;
                dr[obj.strAppDFgnJPDissertationPassUploadName] = obj.AppDFgnJPDissertationPassUploadName;
                return du.Update(ds, "AppendPublication");
            }
        }

        //更新 升等延伸專用檔 VAppendPromote
        public Boolean Update(VAppendPromote obj)
        {
            String strSql = "SELECT * FROM [AppendPromote] WHERE AppSn = @Appsn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AppendPromote");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppSn] = obj.AppSn;
                dr[obj.strNowJobUnit] = obj.NowJobUnit;
                dr[obj.strNowJobTitle] = obj.NowJobTitle;
                dr[obj.strNowJobPosId] = obj.NowJobPosId;
                dr[obj.strNowJobYear] = obj.NowJobYear;
                dr[obj.strExpServiceCaUploadName] = obj.ExpServiceCaUploadName;
                dr[obj.strPBLClassUploadName] = obj.PBLClassUploadName;
                return du.Update(ds, "AppendPromote");
            }
        }

        public Boolean UpdatePartial(VAppendPromote obj)
        {
            String strSql = "SELECT * FROM [AppendPromote] WHERE AppSn = @Appsn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Appsn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AppendPromote");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAppSn] = obj.AppSn;
                dr[obj.strRPIDiscountScore1] = obj.RPIDiscountScore1;
                dr[obj.strRPIDiscountScore1Name] = obj.RPIDiscountScore1Name;
                dr[obj.strRPIDiscountScore2] = obj.RPIDiscountScore2;
                dr[obj.strRPIDiscountScore2Name] = obj.RPIDiscountScore2Name;
                dr[obj.strRPIDiscountScore3] = obj.RPIDiscountScore3;
                dr[obj.strRPIDiscountScore3Name] = obj.RPIDiscountScore3Name;
                dr[obj.strRPIDiscountScore4] = obj.RPIDiscountScore4;
                dr[obj.strRPIDiscountScore4Name] = obj.RPIDiscountScore4Name;
                dr[obj.strRPIDiscountScore5] = obj.RPIDiscountScore5;
                dr[obj.strRPIDiscountTotal] = obj.RPIDiscountTotal;
                dr[obj.strRPIDiscountNo] = obj.RPIDiscountNo;
                return du.Update(ds, "AppendPromote");
            }
        }


        //更新 以學位送審論文教師(Type=1)&口試委員名單(Type=2)
        public Boolean Update(VThesisOral obj)
        {
            String strSql = "select * from ThesisOralList where [ThesisOralSn]=@ThesisOralSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ThesisOralSn", obj.ThesisOralSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ThesisOralList");
                DataRow dr = ds.Tables[0].Rows[0];
                //dr[obj.strThesisOralAppSn] = obj.ThesisOralAppSn;
                dr[obj.strThesisOralType] = obj.ThesisOralType;
                dr[obj.strThesisOralName] = obj.ThesisOralName;
                dr[obj.strThesisOralTitle] = obj.ThesisOralTitle;
                dr[obj.strThesisOralUnit] = obj.ThesisOralUnit;
                dr[obj.strThesisOralOther] = obj.ThesisOralOther;
                return du.Update(ds, "ThesisOralList");
            }
        }

        //更新 ExecuteAuditor 中的Auditor
        public Boolean UpdateExecuteAuditorEmp(VAuditExecute obj)
        {

            String strSql = "SELECT * FROM [AuditExecute] WHERE [ExecuteSn] IN (@ExecuteSn) ";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ExecuteSn", obj.ExecuteSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AuditExecute");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strExecuteAuditorSnEmpId] = obj.ExecuteAuditorSnEmpId;
                dr[obj.strExecuteAuditorName] = obj.ExecuteAuditorName;
                dr[obj.strExecuteAuditorEmail] = obj.ExecuteAuditorEmail;
                return du.Update(ds, "AuditExecute");
            }
        }
        public String DeleteExecuteAuditorEmp(string appsn)
        {

            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                String strSql = "Delete [AuditExecute] WHERE AppSn IN (@appsn) ";
                DataSet da = new DataSet();
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@appsn", appsn);
                SqlDataUpdater ds = new SqlDataUpdater(command, ref da, "M", "AuditExecute");
                String strSqlUP = "Update [ApplyAudit] SET [AppStage]='0', [AppStep]='0', [AppStatus]='False' WHERE AppSn IN (@appsn) ";
                DataSet du = new DataSet();
                SqlCommand commandUP = new SqlCommand(strSqlUP.ToString(), AcadConn);
                commandUP.Parameters.AddWithValue("@appsn", appsn);
                ds = new SqlDataUpdater(commandUP, ref du, "M", "ApplyAudit");

                return null;
            }
        }

        //更新 ExecuteAudit中審核資料欄位
        public Boolean UpdateExecuteAuditData(VAuditExecute obj)
        {

            String strSql = "SELECT * FROM [AuditExecute] WHERE [ExecuteSn] IN (@ExecuteSn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ExecuteSn", obj.ExecuteSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AuditExecute");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strExecuteCommentsA] = obj.ExecuteCommentsA;
                dr[obj.strExecuteCommentsB] = obj.ExecuteCommentsB;
                dr[obj.strExecuteStrengths] = obj.ExecuteStrengths;
                dr[obj.strExecuteWeaknesses] = obj.ExecuteWeaknesses;
                dr[obj.strExecuteAllTotalScore] = obj.ExecuteAllTotalScore;
                dr[obj.strExecuteLevelScore] = obj.ExecuteLevelScore;

                dr[obj.strExecuteWSSubject] = obj.ExecuteWSSubject;
                dr[obj.strExecuteWSMethod] = obj.ExecuteWSMethod;
                dr[obj.strExecuteWSContribute] = obj.ExecuteWSContribute;
                dr[obj.strExecuteWSAchievement] = obj.ExecuteWSAchievement;
                dr[obj.strExecuteWTotalScore] = obj.ExecuteWTotalScore;
                return du.Update(ds, "AuditExecute");
            }
        }

        //更新 ExecuteAudit中審核資料欄位 完成
        public Boolean UpdateExecuteAuditDataFinish(VAuditExecute obj)
        {

            String strSql = "SELECT * FROM [AuditExecute] WHERE [ExecuteSn] =  (@ExecuteSn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ExecuteSn", obj.ExecuteSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AuditExecute");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strExecuteCommentsA] = obj.ExecuteCommentsA;
                dr[obj.strExecuteCommentsB] = obj.ExecuteCommentsB;
                dr[obj.strExecuteReturnReason] = obj.ExecuteReturnReason;
                dr[obj.strExecuteStrengths] = obj.ExecuteStrengths;
                dr[obj.strExecuteWeaknesses] = obj.ExecuteWeaknesses;
                dr[obj.strExecuteAllTotalScore] = obj.ExecuteAllTotalScore;
                dr[obj.strExecuteLevelScore] = obj.ExecuteLevelScore;

                dr[obj.strExecuteWSSubject] = obj.ExecuteWSSubject;
                dr[obj.strExecuteWSMethod] = obj.ExecuteWSMethod;
                dr[obj.strExecuteWSContribute] = obj.ExecuteWSContribute;
                dr[obj.strExecuteWSAchievement] = obj.ExecuteWSAchievement;
                dr[obj.strExecuteWTotalScore] = obj.ExecuteWTotalScore;
                dr[obj.strExecuteAllTotalScore] = obj.ExecuteAllTotalScore;
                dr[obj.strExecuteStatus] = true; //finish
                dr[obj.strExecutePass] = obj.ExecutePass; //0:不通過、不推薦 1:通過、推薦 3:退回補件中
                dr[obj.strExecuteTeachingScore] = obj.ExecuteTeachingScore;
                dr[obj.strExecuteServiceScore] = obj.ExecuteServiceScore;
                dr[obj.strExecuteDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                return du.Update(ds, "AuditExecute");
            }
        }


        //更新 ExecuteAudit 中的 審核者是否接受的狀態
        public Boolean UpdateExecuteAuditAccept(VAuditExecute obj)
        {

            String strSql = "SELECT * FROM [AuditExecute] WHERE [ExecuteSn] IN (@ExecuteSn)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ExecuteSn", obj.ExecuteSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AuditExecute");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strExecuteAccept] = obj.ExecuteAccept;
                return du.Update(ds, "AuditExecute");
            }
        }



        //更新 教師學歷資料
        public Boolean Update(VTeacherEdu obj)

        {
            String strSql = "select * from TeacherEdu where EduSn = @EduSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@EduSn", obj.EduSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "TeacherEdu");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strEduLocal] = obj.EduLocal;
                dr[obj.strEduSchool] = obj.EduSchool;
                dr[obj.strEduDepartment] = obj.EduDepartment;
                dr[obj.strEduStartYM] = obj.EduStartYM;
                dr[obj.strEduEndYM] = obj.EduEndYM;
                dr[obj.strEduDegree] = obj.EduDegree;
                dr[obj.strEduDegreeType] = obj.EduDegreeType;
                return du.Update(ds, "TeacherEdu");
            }
        }

        //寫入 教師經歷資料
        public Boolean Update(VTeacherExp obj)
        {
            String strSql = "select * from TeacherExp where ExpSn = @ExpSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ExpSn", obj.ExpSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "TeacherExp");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strExpOrginization] = obj.ExpOrginization;
                dr[obj.strExpUnit] = obj.ExpUnit;
                dr[obj.strExpJobTitle] = obj.ExpJobTitle;
                dr[obj.strExpJobType] = obj.ExpJobType;
                dr[obj.strExpStartYM] = obj.ExpStartYM;
                dr[obj.strExpEndYM] = obj.ExpEndYM;
                dr[obj.strExpUpload] = obj.ExpUpload;
                dr[obj.strExpUploadName] = obj.ExpUploadName;
                return du.Update(ds, "TeacherExp");
            }
        }

        public Boolean Update(VTeacherTmuExp obj)
        {
            String strSql = "select * from TeacherTmuExp where ExpSn = @ExpSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ExpSn", obj.ExpSn);
                SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "M", "TeacherTmuExp");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strEmpSn] = obj.EmpSn;
                dr[obj.strExpPosId] = obj.ExpPosId;
                dr[obj.strExpQId] = obj.ExpQId;
                dr[obj.strExpUnitId] = obj.ExpUnitId;
                dr[obj.strExpTitleId] = obj.ExpTitleId;
                dr[obj.strExpStartDate] = obj.ExpStartDate;
                dr[obj.strExpEndDate] = obj.ExpEndDate;
                dr[obj.strExpUploadName] = obj.ExpUploadName;
                dr[obj.strExpUserId] = obj.ExpUserId;
                dr[obj.strExpUpdateDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                return du.Update(ds, "TeacherTmuExp");
            }
        }

        public Boolean UpdateUploadFile(String strExpUploadName, int intExpSn)
        {
            String strSql = "select * from TeacherTmuExp where ExpSn = @ExpSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ExpSn", intExpSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "TeacherTmuExp");
                DataRow dr = ds.Tables[0].Rows[0];
                dr["ExpUploadName"] = strExpUploadName;
                dr["ExpUpdateDate"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                return du.Update(ds, "TeacherTmuExp");
            }
        }

        public Boolean Update(VTeacherTmuLesson obj)
        {
            String strSql = "select * from TeacherTmuLesson where LessonSn = @LessonSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@LessonSn", obj.LessonSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "TeacherTmuLesson");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strEmpSn] = obj.EmpSn;
                dr[obj.strLessonYear] = obj.LessonYear;
                dr[obj.strLessonSemester] = obj.LessonSemester;
                dr[obj.strLessonDeptLevel] = obj.LessonDeptLevel;
                dr[obj.strLessonClass] = obj.LessonClass;
                dr[obj.strLessonHours] = Convert.ToDecimal(obj.LessonHours);
                dr[obj.strLessonCreditHours] = Convert.ToDecimal(obj.LessonCreditHours);
                dr[obj.strLessonEvaluate] = obj.LessonEvaluate;
                dr[obj.strLessonUserId] = obj.LessonUserId;
                dr[obj.strLessonUpdateDate] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                return du.Update(ds, "TeacherTmuLesson");
            }
        }

        //更新 教師證發放資料 
        public Boolean Update(VTeacherCa obj)
        {
            String strSql = "select * from TeacherCa where CaSn = @CaSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@CaSn", obj.CaSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "TeacherCa");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strCaNumberCN] = obj.CaNumberCN;
                dr[obj.strCaNumber] = obj.CaNumber;
                dr[obj.strCaPublishSchool] = obj.CaPublishSchool;
                dr[obj.strCaStartYM] = obj.CaStartYM;
                dr[obj.strCaEndYM] = obj.CaEndYM;
                dr[obj.strCaUpload] = obj.CaUpload;
                dr[obj.strCaUploadName] = obj.CaUploadName;
                return du.Update(ds, "TeacherCa");
            }
        }


        //更新 學術獎勵、榮譽事項 
        public Boolean Update(VTeacherHonour obj)
        {
            String strSql = "select * from TeacherHonour where HorSn = @HorSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@HorSn", obj.HorSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "TeacherHonour");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strHorYear] = obj.HorYear;
                dr[obj.strHorDescription] = obj.HorDescription;
                return du.Update(ds, "TeacherHonour");
            }
        }


        //更新 教師上傳論文積分表 
        public Boolean Update(VThesisScore obj)
        {
            String strSql = "select * from ThesisScore where ThesisSn = @ThesisSn and (AppSn = @AppSn or AppSn = '0') ";//and EmpSn = '" + obj.EmpSn + "' and AppSn = '" + obj.AppSn + "'";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@ThesisSn", obj.ThesisSn);
                command.Parameters.AddWithValue("@AppSn", obj.AppSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ThesisScore");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strThesisPublishYearMonth] = obj.ThesisPublishYearMonth;
                dr[obj.strThesisResearchResult] = obj.ThesisResearchResult;
                dr[obj.strRRNo] = obj.RRNo;
                dr[obj.strThesisC] = obj.ThesisC;
                dr[obj.strThesisJ] = obj.ThesisJ;
                dr[obj.strThesisA] = obj.ThesisA;
                dr[obj.strThesisTotal] = obj.ThesisTotal;
                dr[obj.strThesisName] = obj.ThesisName;
                dr[obj.strThesisUploadName] = obj.ThesisUploadName;
                dr[obj.strIsCountRPI] = obj.IsCountRPI;
                dr[obj.strIsRepresentative] = obj.IsRepresentative;
                dr[obj.strThesisJournalRefCount] = obj.ThesisJournalRefCount;
                dr[obj.strThesisJournalRefUploadName] = obj.ThesisJournalRefUploadName;
                dr[obj.strThesisSummaryCN] = obj.ThesisSummaryCN;
                //dr[obj.strThesisCoAuthorUpload] = obj.ThesisCoAuthorUpload;
                dr[obj.strThesisCoAuthorUploadName] = obj.ThesisCoAuthorUploadName;
                dr[obj.strThesisModifyDate] = obj.ThesisModifyDate;

                return du.Update(ds, "ThesisScore");
            }
        }

        public Boolean Update(VThesisScoreCount obj)
        {
            String strSql = "select * from ThesisScoreCount where PT_EmpId = @PT_EmpId  and PT_Year = @PT_Year";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@PT_EmpId", obj.PT_EmpId);
                command.Parameters.AddWithValue("@PT_Year", obj.PT_Year);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ThesisScoreCount");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strPT_Year] = obj.PT_Year;
                dr[obj.strPT_EmpId] = obj.PT_EmpId;
                dr[obj.strPT_InPerson] = obj.PT_InPerson;
                dr[obj.strPT_FSCI] = obj.PT_FSCI;
                dr[obj.strPT_FSSCI] = obj.PT_FSSCI;
                dr[obj.strPT_FEI] = obj.PT_FEI;
                dr[obj.strPT_FNSCI] = obj.PT_FNSCI;
                dr[obj.strPT_FOther] = obj.PT_FOther;
                dr[obj.strPT_NFCSCI] = obj.PT_NFCSCI;
                dr[obj.strPT_NFCSSCI] = obj.PT_NFCSSCI;
                dr[obj.strPT_NFCEI] = obj.PT_NFCEI;
                dr[obj.strPT_NFCNSCI] = obj.PT_NFCNSCI;
                dr[obj.strPT_NFCOther] = obj.PT_NFCOther;
                dr[obj.strPT_NFOCSCI] = obj.PT_NFOCSCI;
                dr[obj.strPT_NFOCSSCI] = obj.PT_NFOCSSCI;
                dr[obj.strPT_NFOCEI] = obj.PT_NFOCEI;
                dr[obj.strPT_NFOCNSCI] = obj.PT_NFOCNSCI;
                dr[obj.strPT_NFOCOther] = obj.PT_NFOCOther;
                dr[obj.strPT_Update] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                return du.Update(ds, "ThesisScoreCount");
            }
        }

        //更新 申請帳號資料
        public Boolean Update(VAccountForApply obj)
        {
            String strSql = "SELECT * FROM [AccountForApply] WHERE [AcctApplyEmail] = @AcctApplyEmail and [AcctApplyPassword] = @AcctApplyPassword";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AcctApplyEmail", obj.AcctApplyEmail);
                command.Parameters.AddWithValue("@AcctApplyPassword", obj.AcctApplyPassword);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "AccountForApply");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strAcctApplyEmail] = obj.AcctApplyEmail;
                dr[obj.strAcctApplyPassword] = obj.AcctApplyPassword;
                dr[obj.strAcctApplyId] = obj.AcctApplyId;
                dr[obj.strAcctApplyBirthday] = obj.AcctApplyBirthday;
                dr[obj.strAcctApplyCell] = obj.AcctApplyCell;
                return du.Update(ds, "AccountForApply");
            }
        }

        //更新 ThesisScore 序號
        public Boolean UpdateThesisScoreSn(int thesisSn, int SnNo)
        {
            String strSql = "SELECT * FROM [ThesisScore] WHERE [ThesisSn] = @thesisSn";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@thesisSn", thesisSn);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "ThesisScore");
                DataRow dr = ds.Tables[0].Rows[0];
                VThesisScore vThesisScore = new VThesisScore();
                dr[vThesisScore.strSnNo] = SnNo;
                return du.Update(ds, "ThesisScore");
            }
        }


        //更新 系統設定開放時間 
        public Boolean Update(VSystemOpendate obj)
        {
            String strSql = "select * from [SystemOpendate] where Smtr =@Smtr  and  Semester = @Semester  and KindNo = @KindNo and TypeNo = @TypeNo";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@Smtr", obj.Smtr);
                command.Parameters.AddWithValue("@Semester", obj.Semester);
                command.Parameters.AddWithValue("@KindNo", obj.KindNo);
                command.Parameters.AddWithValue("@TypeNo", obj.TypeNo);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "M", "SystemOpendate");
                DataRow dr = ds.Tables[0].Rows[0];
                dr[obj.strSmtr] = obj.Smtr;
                dr[obj.strSemester] = obj.Semester;
                dr[obj.strKindNo] = obj.KindNo;
                dr[obj.strApplyBeginTime] = obj.ApplyBeginTime;
                dr[obj.strApplyEndTime] = obj.ApplyEndTime;
                dr[obj.strAuditBeginTime] = obj.AuditBeginTime;
                dr[obj.strAuditEndTime] = obj.AuditEndTime;
                return du.Update(ds, "SystemOpendate");
            }
        }



        //判斷 該基本資料是否存在
        public Boolean EmpBaseExist(string EmpIdno)
        {
            Boolean boolEmpBase = false;
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpIdno] IN (@EmpIdno)";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@EmpIdno", EmpIdno);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                if (hasData(ds))
                {
                    boolEmpBase = true;
                }
                //             sqlds.SelectParameters.Add("ID", EmpID);

                //             sqlds.SelectCommand = "SELECT * FROM [EmployeeBasic] WHERE [EmpID] IN ('@ID')";

                //             DataSourceSelectArguments args = new DataSourceSelectArguments();

                //             DataView dv = (DataView) sqlds.Select(args);

                //             foreach (DataRowView d in dv)
                //             {

                //                 tEmpSn= Int16.Parse(d["ID"].ToString());

                //             }

                return boolEmpBase;
            }
        }

        //判斷 原申請帳號是否存在 同一組的帳號不申請兩次
        public Boolean AccountForApplyExist(VAccountForApply obj)
        {
            Boolean boolSn = false;
            String strSql = "SELECT AcctApplySn FROM [AccountForApply] WHERE [AcctApplyEmail] =  @AcctApplyEmail";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AcctApplyEmail", obj.AcctApplyEmail);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    boolSn = true;
                }
                return boolSn;
            }
        }

        //取得 申請帳號資料
        public VAccountForApply GetAccountForApply(VAccountForApply obj)
        {
            VAccountForApply vAccountForApply = null;
            String strSql = "SELECT * FROM [AccountForApply] WHERE [AcctApplyEmail] =  @AcctApplyEmail and [AcctApplyPassword] = @AcctApplyPassword ";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AcctApplyEmail", obj.AcctApplyEmail);
                command.Parameters.AddWithValue("@AcctApplyPassword", obj.AcctApplyPassword);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    vAccountForApply = new VAccountForApply();
                    vAccountForApply.AcctApplyEmail = dt.Rows[0][obj.strAcctApplyEmail].ToString();
                    vAccountForApply.AcctApplyPassword = dt.Rows[0][obj.strAcctApplyPassword].ToString();
                    vAccountForApply.AcctApplyId = dt.Rows[0][obj.strAcctApplyId].ToString();
                }
                return vAccountForApply;
            }
        }

        //取得 申請帳號資料
        public VAccountForApply GetAccountForApply(String strApplyEmail)
        {
            VAccountForApply vAccountForApply = null;
            String strSql = "SELECT TOP(1) * FROM [AccountForApply] WHERE [AcctApplyEmail] = @strApplyEmail ";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@strApplyEmail", strApplyEmail);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    vAccountForApply = new VAccountForApply();
                    vAccountForApply.AcctApplyEmail = dt.Rows[0][vAccountForApply.strAcctApplyEmail].ToString();
                    vAccountForApply.AcctApplyPassword = dt.Rows[0][vAccountForApply.strAcctApplyPassword].ToString();
                    vAccountForApply.AcctApplyId = dt.Rows[0][vAccountForApply.strAcctApplyId].ToString();
                }
                return vAccountForApply;
            }
        }

        //取得 申請帳號的密碼
        public String GetAccountPwd(VAccountForApply obj)
        {
            String pwd = "";
            //String strSql = "SELECT AcctApplyPassword FROM [AccountForApply] WHERE [AcctApplyEmail] =  '" + obj.AcctApplyEmail + "' and [AcctApplyId] = '" + obj.AcctApplyId + "' and [AcctApplyBirthday] = '" + obj.AcctApplyBirthday + "' and [AcctApplyCell] = '" + obj.AcctApplyCell + "'";
            String strSql = "SELECT A.AcctApplyPassword FROM AccountForApply as A inner join  EmployeeBase as B on A.AcctApplyId = B.EmpIdno WHERE A.AcctApplyId = @AcctApplyId and B.EmpBirthDay = @AcctApplyBirthday  and B.EmpCell = @AcctApplyCell ";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AcctApplyId", obj.AcctApplyId);
                command.Parameters.AddWithValue("@AcctApplyBirthday", obj.AcctApplyBirthday);
                command.Parameters.AddWithValue("@AcctApplyCell", obj.AcctApplyCell);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    pwd = dt.Rows[0][obj.strAcctApplyPassword].ToString();
                }
                return pwd;
            }
        }

        //取得 稽核者資料
        public VAccountForAudit GetAccountForAudit(VAccountForAudit obj)
        {
            VAccountForAudit vAccountForAudit = null;
            String strSql = "SELECT * FROM [AccountForAudit] WHERE [AcctEmail] =  @AcctEmail  and [AcctPassword] = @AcctPassword ";
            DataSet ds = new DataSet();
            using (SqlConnection AcadConn = new SqlConnection(Conn.ConnectionString))
            {
                SqlCommand command = new SqlCommand(strSql.ToString(), AcadConn);
                command.Parameters.AddWithValue("@AcctEmail", obj.AcctEmail);
                command.Parameters.AddWithValue("@AcctPassword", obj.AcctPassword);
                SqlDataUpdater du = new SqlDataUpdater(command, ref ds, "S");
                DataTable dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    vAccountForAudit = new VAccountForAudit();
                    vAccountForAudit.AcctEmail = dt.Rows[0][obj.strAcctEmail].ToString();
                    vAccountForAudit.AcctPassword = dt.Rows[0][obj.strAcctPassword].ToString();
                    vAccountForAudit.AcctAppSn = dt.Rows[0][obj.strAcctAppSn].ToString();
                    vAccountForAudit.AcctAuditorSnEmpId = dt.Rows[0][obj.strAcctAuditorSnEmpId].ToString();
                    vAccountForAudit.AcctStatus = (Boolean)dt.Rows[0][obj.strAcctStatus];
                }
                return vAccountForAudit;
            }
        }

        public String GetOuterName(string AcctAuditorSnEmpId)
        {
            string outerName = "";
            string sql;
            sql = "select AuditorName from AuditorOutter where AuditorSn= '" + AcctAuditorSnEmpId + "' ";
            System.Diagnostics.Debug.Print(sql);
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                {
                    DataTable dt = new DataTable("UserData");
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        outerName = dt.Rows[0]["AuditorName"].ToString().Trim();
                    }
                }
                cn.Close();
            }
            if (String.IsNullOrEmpty(outerName))
                return null;
            else
                return outerName;
        }



        public VAccountForAudit GetAccountForAuditByAppSn(VAccountForAudit obj)
        {
            VAccountForAudit vAccountForAudit = null;
            String strSql = "SELECT * FROM [AccountForAudit] WHERE [AcctEmail] =  '" + obj.AcctEmail + "' and [AcctAppSn] = '" + obj.AcctAppSn + "'";
            DataSet ds = new DataSet();


            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vAccountForAudit = new VAccountForAudit();
                vAccountForAudit.AcctEmail = dt.Rows[0][obj.strAcctEmail].ToString();
                vAccountForAudit.AcctPassword = dt.Rows[0][obj.strAcctPassword].ToString();
                vAccountForAudit.AcctAppSn = dt.Rows[0][obj.strAcctAppSn].ToString();
                vAccountForAudit.AcctAuditorSnEmpId = dt.Rows[0][obj.strAcctAuditorSnEmpId].ToString();
                vAccountForAudit.AcctStatus = (Boolean)dt.Rows[0][obj.strAcctStatus];
            }
            return vAccountForAudit;
        }

        public Boolean UpdateAuditAcceptStatus(string email, string pwd, bool isAcceptStatus)
        {

            String strSql = "SELECT * FROM [AccountForAudit] WHERE [AcctEmail] = '" + email.Trim() + "' and AcctPassword = '" + pwd.Trim() + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "M", "AccountForAudit");
            if (ds.Tables.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                dr["AcctStatus"] = isAcceptStatus;
            }
            return du.Update(ds, "AccountForAudit");
        }


        public VApplyAudit GetApplyAuditByIdno(string strIdno)
        {

            getSettings.Execute();
            VApplyAudit vApplyAudit = null;
            String strSql = "SELECT AppSn, AppKindNo,AppYear,AppSemester,AppJobTitleNo,AppAttributeNo,AppJobTypeNo,AppStage,AppStep FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] = '" + getSettings.NowYear + "' and [AppSemester] = '" + getSettings.NowSemester + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit = new VApplyAudit();
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                vApplyAudit.AppKindNo = dt.Rows[0]["AppKindNo"].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0]["AppJobTitleNo"].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0]["AppJobTypeNo"].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0]["AppAttributeNo"].ToString();
                vApplyAudit.AppYear = dt.Rows[0]["AppYear"].ToString();
                vApplyAudit.AppSemester = dt.Rows[0]["AppSemester"].ToString();
                vApplyAudit.AppStage = dt.Rows[0]["AppStage"].ToString();
                vApplyAudit.AppStep = dt.Rows[0]["AppStep"].ToString();
            }
            return vApplyAudit;
        }

        public VApplyAudit GetApplyAuditLastOne(string strIdno)
        {

            getSettings.Execute();
            VApplyAudit vApplyAudit = null;
            String strSql = "SELECT AppSn,AppUnitNo, AppKindNo,AppYear,AppSemester,AppJobTitleNo,AppAttributeNo,AppJobTypeNo,AppStage,AppStep FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' order by AppSn Desc";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit = new VApplyAudit();
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                vApplyAudit.AppKindNo = dt.Rows[0]["AppKindNo"].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0]["AppUnitNo"].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0]["AppJobTitleNo"].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0]["AppJobTypeNo"].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0]["AppAttributeNo"].ToString();
                vApplyAudit.AppYear = dt.Rows[0]["AppYear"].ToString();
                vApplyAudit.AppSemester = dt.Rows[0]["AppSemester"].ToString();
                vApplyAudit.AppStage = dt.Rows[0]["AppStage"].ToString();
                vApplyAudit.AppStep = dt.Rows[0]["AppStep"].ToString();
            }
            return vApplyAudit;
        }

        //判斷 管理者基本資料是否存在
        public int GetAccountForManageAcctSn(string email)
        {
            int intAcctSn = 0;
            String strSql = "SELECT AcctSn FROM [AccountForManage] WHERE [AcctEmail] =  '" + email + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intAcctSn = Int32.Parse(dt.Rows[0]["AcctSn"].ToString());
            }
            return intAcctSn;
        }

        //取得 管理者基本資料是否存在
        public VAccountForManage GetAccountForManage(string email)
        {
            VAccountForManage vAccountForManage = null;
            String strSql = "SELECT * FROM [AccountForManage] WHERE [AcctEmail] =  '" + email + "'  and AcctStatus = 'true'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vAccountForManage = new VAccountForManage();
                vAccountForManage.AcctSn = Convert.ToInt32(dt.Rows[0][vAccountForManage.strAcctSn].ToString());
                vAccountForManage.AcctRole = dt.Rows[0][vAccountForManage.strAcctRole].ToString();
                vAccountForManage.AcctEmpId = dt.Rows[0][vAccountForManage.strAcctEmpId].ToString();
                vAccountForManage.AcctEmail = dt.Rows[0][vAccountForManage.strAcctEmail].ToString();
                vAccountForManage.AcctPassword = dt.Rows[0][vAccountForManage.strAcctPassword].ToString();
                vAccountForManage.AcctStatus = (Boolean)dt.Rows[0][vAccountForManage.strAcctStatus];
            }
            return vAccountForManage;
        }

        //判斷 該基本資料是否存在
        public int GetEmpBaseEmpSn(string EmpIdno)
        {
            int intEmpSn = 0;
            String strSql = "SELECT EmpSn FROM [EmployeeBase] WHERE [EmpIdno] =  '" + EmpIdno + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intEmpSn = Int32.Parse(dt.Rows[0]["EmpSn"].ToString());
            }
            return intEmpSn;
        }

        //撈取員工基本資料
        public VEmployeeBase GetEmpBaseObjByEmpSn(string EmpSn)
        {
            VEmployeeBase vEmpBase = new VEmployeeBase();
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpSn] = '" + EmpSn + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vEmpBase.EmpSn = Int32.Parse(dt.Rows[0]["EmpSn"].ToString());
                vEmpBase.EmpIdno = dt.Rows[0]["EmpIdno"].ToString();
                vEmpBase.EmpBirthDay = dt.Rows[0]["EmpBirthDay"].ToString();
                vEmpBase.EmpPassportNo = dt.Rows[0]["EmpPassportNo"].ToString();
                vEmpBase.EmpNameENFirst = dt.Rows[0]["EmpNameENFirst"].ToString();
                vEmpBase.EmpNameENLast = dt.Rows[0]["EmpNameENLast"].ToString();
                vEmpBase.EmpNameCN = dt.Rows[0]["EmpNameCN"].ToString();
                vEmpBase.EmpSex = dt.Rows[0]["EmpSex"].ToString();
                vEmpBase.EmpCountry = dt.Rows[0]["EmpCountry"].ToString();
                //vEmpBase.EmpHomeTown = dt.Rows[0]["EmpHomeTown"].ToString();
                //vEmpBase.EmpBornProvince = dt.Rows[0]["EmpBornProvince"].ToString();
                vEmpBase.EmpBornCity = dt.Rows[0]["EmpBornCity"].ToString();
                vEmpBase.EmpTelPub = dt.Rows[0]["EmpTelPub"].ToString();
                vEmpBase.EmpTelPri = dt.Rows[0]["EmpTelPri"].ToString();
                vEmpBase.EmpEmail = dt.Rows[0]["EmpEmail"].ToString();
                vEmpBase.EmpTownAddressCode = dt.Rows[0]["EmpTownAddressCode"].ToString();
                vEmpBase.EmpTownAddress = dt.Rows[0]["EmpTownAddress"].ToString();
                vEmpBase.EmpAddressCode = dt.Rows[0]["EmpAddressCode"].ToString();
                vEmpBase.EmpAddress = dt.Rows[0]["EmpAddress"].ToString();
                vEmpBase.EmpCell = dt.Rows[0]["EmpCell"].ToString();
                vEmpBase.EmpExpertResearch = dt.Rows[0]["EmpExpertResearch"].ToString();
                //vEmpBase.EmpPhotoUpload = (Boolean)dt.Rows[0]["EmpPhotoUpload"];
                vEmpBase.EmpPhotoUploadName = dt.Rows[0]["EmpPhotoUploadName"].ToString();
                //vEmpBase.EmpIdnoUpload = (Boolean)dt.Rows[0]["EmpIdnoUpload"];
                vEmpBase.EmpIdnoUploadName = dt.Rows[0]["EmpIdnoUploadName"].ToString();
                //vEmpBase.EmpDegreeUpload = (Boolean)dt.Rows[0]["EmpDegreeUpload"];
                vEmpBase.EmpDegreeUploadName = dt.Rows[0]["EmpDegreeUploadName"].ToString();
                vEmpBase.EmpNote = dt.Rows[0]["EmpNote"].ToString();
                vEmpBase.EmpNowJobOrg = dt.Rows[0]["EmpNowJobOrg"].ToString();
                vEmpBase.EmpRecommendors = dt.Rows[0]["EmpRecommendors"].ToString();
                vEmpBase.EmpRecommendYear = dt.Rows[0]["EmpRecommendYear"].ToString();
                vEmpBase.EmpRecommendUploadName = dt.Rows[0]["EmpRecommendUploadName"].ToString();
                vEmpBase.EmpSelfReach = dt.Rows[0]["EmpSelfReach"].ToString();
                vEmpBase.EmpSelfDevelope = dt.Rows[0]["EmpSelfDevelope"].ToString();
                vEmpBase.EmpSelfSpecial = dt.Rows[0]["EmpSelfSpecial"].ToString();
                vEmpBase.EmpSelfImprove = dt.Rows[0]["EmpSelfImprove"].ToString();
                vEmpBase.EmpSelfContribute = dt.Rows[0]["EmpSelfContribute"].ToString();
                vEmpBase.EmpSelfCooperate = dt.Rows[0]["EmpSelfCooperate"].ToString();
                vEmpBase.EmpSelfTeachPlan = dt.Rows[0]["EmpSelfTeachPlan"].ToString();
                vEmpBase.EmpSelfLifePlan = dt.Rows[0]["EmpSelfLifePlan"].ToString();
                vEmpBase.EmpNoTeachExp = (Boolean)dt.Rows[0]["EmpNoTeachExp"];
                vEmpBase.EmpNoTeachCa = (Boolean)dt.Rows[0]["EmpNoTeachCa"];
                vEmpBase.EmpNoHonour = (Boolean)dt.Rows[0]["EmpNoHonour"];
                vEmpBase.EmpStatus = (Boolean)dt.Rows[0]["EmpStatus"];

                return vEmpBase;
            }
            else
            {
                return null;
            }

        }

        //判斷 ApplyAudit申請共用檔是否已存在
        public int GetApplyAuditAppSn(int EmpSn)
        {
            int intAppSn = 0;
            String strSql = "SELECT AppSn FROM [ApplyAudit] WHERE [EmpSn] = " + EmpSn + " order by AppSn Desc";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intAppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
            }
            return intAppSn;
        }


        public int GetApplyAuditEmpSn(int AppSn)
        {
            int intAppSn = 0;
            String strSql = "SELECT EmpSn FROM [ApplyAudit] WHERE [AppSn] = " + AppSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intAppSn = Int32.Parse(dt.Rows[0]["EmpSn"].ToString());
            }
            return intAppSn;
        }
        //判斷 新聘延伸檔已存在         
        public int GetAppendEmployeeByAppESn(int AppSn)
        {
            int intAppESn = 0;
            String strSql = "SELECT AppESn FROM [AppendEmployee] WHERE [AppSn] = " + AppSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intAppESn = Int32.Parse(dt.Rows[0]["AppESn"].ToString());
            }
            return intAppESn;
        }


        //判斷 著作延伸檔已存在 
        public int GetAppendPromoteByAppPSn(int AppSn)
        {
            int intAppESn = 0;
            String strSql = "SELECT AppPSn FROM [AppendPromote] WHERE [AppSn] = " + AppSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intAppESn = Int32.Parse(dt.Rows[0]["AppPSn"].ToString());
            }
            return intAppESn;
        }

        public VAppendPromote GetAppendPromoteObj(int AppSn)
        {
            VAppendPromote vAppendPromote = new VAppendPromote();
            String strSql = "SELECT * FROM [AppendPromote] WHERE [AppSn] = '" + AppSn + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vAppendPromote.AppPSn = Int32.Parse(dt.Rows[0][vAppendPromote.strAppPSn].ToString());
                vAppendPromote.AppSn = Int32.Parse(dt.Rows[0][vAppendPromote.strAppSn].ToString());
                vAppendPromote.ExpServiceCaUploadName = dt.Rows[0][vAppendPromote.strExpServiceCaUploadName].ToString();
                vAppendPromote.NowJobPosId = dt.Rows[0][vAppendPromote.strNowJobPosId].ToString();
                vAppendPromote.NowJobTitle = dt.Rows[0][vAppendPromote.strNowJobTitle].ToString();
                vAppendPromote.NowJobUnit = dt.Rows[0][vAppendPromote.strNowJobUnit].ToString();
                vAppendPromote.NowJobYear = dt.Rows[0][vAppendPromote.strNowJobYear].ToString();
                vAppendPromote.PBLClassUploadName = dt.Rows[0][vAppendPromote.strPBLClassUploadName].ToString();
                vAppendPromote.RPIDiscountScore1 = dt.Rows[0][vAppendPromote.strRPIDiscountScore1].ToString();
                vAppendPromote.RPIDiscountScore2 = dt.Rows[0][vAppendPromote.strRPIDiscountScore2].ToString();
                vAppendPromote.RPIDiscountScore3 = dt.Rows[0][vAppendPromote.strRPIDiscountScore3].ToString();
                vAppendPromote.RPIDiscountScore4 = dt.Rows[0][vAppendPromote.strRPIDiscountScore4].ToString();
                vAppendPromote.RPIDiscountScore5 = dt.Rows[0][vAppendPromote.strRPIDiscountScore5].ToString();
                vAppendPromote.RPIDiscountScore1Name = dt.Rows[0][vAppendPromote.strRPIDiscountScore1Name].ToString();
                vAppendPromote.RPIDiscountScore2Name = dt.Rows[0][vAppendPromote.strRPIDiscountScore2Name].ToString();
                vAppendPromote.RPIDiscountScore3Name = dt.Rows[0][vAppendPromote.strRPIDiscountScore3Name].ToString();
                vAppendPromote.RPIDiscountScore4Name = dt.Rows[0][vAppendPromote.strRPIDiscountScore4Name].ToString();
                vAppendPromote.RPIDiscountTotal = dt.Rows[0][vAppendPromote.strRPIDiscountTotal].ToString();
                vAppendPromote.RPIDiscountNo = (Boolean)dt.Rows[0][vAppendPromote.strRPIDiscountNo];
                return vAppendPromote;
            }
            else
            {
                return null;
            }
        }

        public String GetRPIDiscountTotal(int AppSn)
        {
            String RPIDiscountTotal = "";
            String strSql = "SELECT RPIDiscountTotal FROM [AppendPromote] WHERE [AppSn] = " + AppSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                RPIDiscountTotal = dt.Rows[0][0].ToString();
            }
            return RPIDiscountTotal;
        }

        public VEmployeeBase GetEmpBsaseObj(string EmpIdno)
        {

            VEmployeeBase vEmpBase = new VEmployeeBase();
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpIdno] IN ('" + EmpIdno + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vEmpBase.EmpSn = Int32.Parse(dt.Rows[0][vEmpBase.strEmpSn].ToString());
                vEmpBase.EmpIdno = dt.Rows[0][vEmpBase.strEmpIdno].ToString();
                vEmpBase.EmpBirthDay = dt.Rows[0][vEmpBase.strEmpBirthDay].ToString();
                vEmpBase.EmpPassportNo = dt.Rows[0][vEmpBase.strEmpPassportNo].ToString();
                vEmpBase.EmpNameENFirst = dt.Rows[0][vEmpBase.strEmpNameENFirst].ToString();
                vEmpBase.EmpNameENLast = dt.Rows[0][vEmpBase.strEmpNameENLast].ToString();
                vEmpBase.EmpNameCN = dt.Rows[0][vEmpBase.strEmpNameCN].ToString();
                vEmpBase.EmpSex = dt.Rows[0][vEmpBase.strEmpSex].ToString();
                vEmpBase.EmpCountry = dt.Rows[0][vEmpBase.strEmpCountry].ToString();
                //vEmpBase.EmpHomeTown = dt.Rows[0][vEmpBase.EmpHomeTown].ToString();
                //vEmpBase.EmpBornProvince = dt.Rows[0][vEmpBase.EmpBornProvince].ToString();
                vEmpBase.EmpBornCity = dt.Rows[0][vEmpBase.strEmpBornCity].ToString();
                vEmpBase.EmpTelPub = dt.Rows[0][vEmpBase.strEmpTelPub].ToString();
                vEmpBase.EmpTelPri = dt.Rows[0][vEmpBase.strEmpTelPri].ToString();
                vEmpBase.EmpEmail = dt.Rows[0][vEmpBase.strEmpEmail].ToString();
                vEmpBase.EmpTownAddressCode = dt.Rows[0][vEmpBase.strEmpTownAddressCode].ToString();
                vEmpBase.EmpTownAddress = dt.Rows[0][vEmpBase.strEmpTownAddress].ToString();
                vEmpBase.EmpAddressCode = dt.Rows[0][vEmpBase.strEmpAddressCode].ToString();
                vEmpBase.EmpAddress = dt.Rows[0][vEmpBase.strEmpAddress].ToString();
                vEmpBase.EmpCell = dt.Rows[0][vEmpBase.strEmpCell].ToString();
                vEmpBase.EmpNowJobOrg = dt.Rows[0][vEmpBase.strEmpNowJobOrg].ToString();
                vEmpBase.EmpNote = dt.Rows[0][vEmpBase.strEmpNote].ToString();

                vEmpBase.EmpExpertResearch = dt.Rows[0][vEmpBase.strEmpExpertResearch].ToString();

                vEmpBase.EmpPhotoUploadName = dt.Rows[0][vEmpBase.strEmpPhotoUploadName].ToString();

                vEmpBase.EmpIdnoUploadName = dt.Rows[0][vEmpBase.strEmpIdnoUploadName].ToString();

                vEmpBase.EmpDegreeUploadName = dt.Rows[0][vEmpBase.strEmpDegreeUploadName].ToString();
                vEmpBase.EmpSelfTeachExperience = dt.Rows[0][vEmpBase.strEmpSelfTeachExperience].ToString();
                vEmpBase.EmpSelfReach = dt.Rows[0][vEmpBase.strEmpSelfReach].ToString();
                vEmpBase.EmpSelfDevelope = dt.Rows[0][vEmpBase.strEmpSelfDevelope].ToString();
                vEmpBase.EmpSelfSpecial = dt.Rows[0][vEmpBase.strEmpSelfSpecial].ToString();
                vEmpBase.EmpSelfImprove = dt.Rows[0][vEmpBase.strEmpSelfImprove].ToString();
                vEmpBase.EmpSelfContribute = dt.Rows[0][vEmpBase.strEmpSelfContribute].ToString();
                vEmpBase.EmpSelfCooperate = dt.Rows[0][vEmpBase.strEmpSelfCooperate].ToString();
                vEmpBase.EmpSelfTeachPlan = dt.Rows[0][vEmpBase.strEmpSelfTeachPlan].ToString();
                vEmpBase.EmpSelfLifePlan = dt.Rows[0][vEmpBase.strEmpSelfLifePlan].ToString();
                vEmpBase.EmpNoTeachExp = (Boolean)dt.Rows[0][vEmpBase.strEmpNoTeachExp];
                vEmpBase.EmpNoTeachCa = (Boolean)dt.Rows[0][vEmpBase.strEmpNoTeachCa];
                vEmpBase.EmpNoHonour = (Boolean)dt.Rows[0][vEmpBase.strEmpNoHonour];
                //vEmpBase.EmpStatus = (Boolean)dt.Rows[0][vEmpBase.strEmpStatus]; 

                return vEmpBase;
            }
            else
            {
                return null;
            }
        }

        //撈取員工基本資料
        public VEmployeeBase GetEmpBsaseObjByEmpSn(string EmpSn)
        {

            VEmployeeBase vEmpBase = new VEmployeeBase();
            String strSql = "SELECT * FROM [EmployeeBase] WHERE [EmpSn] =  '" + EmpSn + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vEmpBase.EmpSn = Int32.Parse(dt.Rows[0][vEmpBase.strEmpSn].ToString());
                vEmpBase.EmpIdno = dt.Rows[0][vEmpBase.strEmpIdno].ToString();
                vEmpBase.EmpBirthDay = dt.Rows[0][vEmpBase.strEmpBirthDay].ToString();
                vEmpBase.EmpPassportNo = dt.Rows[0][vEmpBase.strEmpPassportNo].ToString();
                vEmpBase.EmpNameENFirst = dt.Rows[0][vEmpBase.strEmpNameENFirst].ToString();
                vEmpBase.EmpNameENLast = dt.Rows[0][vEmpBase.strEmpNameENLast].ToString();
                vEmpBase.EmpNameCN = dt.Rows[0][vEmpBase.strEmpNameCN].ToString();
                vEmpBase.EmpSex = dt.Rows[0][vEmpBase.strEmpSex].ToString();
                vEmpBase.EmpCountry = dt.Rows[0][vEmpBase.strEmpCountry].ToString();
                //vEmpBase.EmpHomeTown = dt.Rows[0][vEmpBase.EmpHomeTown].ToString();
                //vEmpBase.EmpBornProvince = dt.Rows[0][vEmpBase.EmpBornProvince].ToString();
                vEmpBase.EmpBornCity = dt.Rows[0][vEmpBase.strEmpBornCity].ToString();
                vEmpBase.EmpTelPub = dt.Rows[0][vEmpBase.strEmpTelPub].ToString();
                vEmpBase.EmpTelPri = dt.Rows[0][vEmpBase.strEmpTelPri].ToString();
                vEmpBase.EmpEmail = dt.Rows[0][vEmpBase.strEmpEmail].ToString();
                vEmpBase.EmpTownAddressCode = dt.Rows[0][vEmpBase.strEmpTownAddressCode].ToString();
                vEmpBase.EmpTownAddress = dt.Rows[0][vEmpBase.strEmpTownAddress].ToString();
                vEmpBase.EmpAddressCode = dt.Rows[0][vEmpBase.strEmpAddressCode].ToString();
                vEmpBase.EmpAddress = dt.Rows[0][vEmpBase.strEmpAddress].ToString();
                vEmpBase.EmpCell = dt.Rows[0][vEmpBase.strEmpCell].ToString();
                vEmpBase.EmpNowJobOrg = dt.Rows[0][vEmpBase.strEmpNowJobOrg].ToString();
                vEmpBase.EmpNote = dt.Rows[0][vEmpBase.strEmpNote].ToString();
                vEmpBase.EmpExpertResearch = dt.Rows[0][vEmpBase.strEmpExpertResearch].ToString();
                vEmpBase.EmpPhotoUploadName = dt.Rows[0][vEmpBase.strEmpPhotoUploadName].ToString();
                vEmpBase.EmpIdnoUploadName = dt.Rows[0][vEmpBase.strEmpIdnoUploadName].ToString();
                vEmpBase.EmpDegreeUploadName = dt.Rows[0][vEmpBase.strEmpDegreeUploadName].ToString();
                vEmpBase.EmpSelfTeachExperience = dt.Rows[0][vEmpBase.strEmpSelfTeachExperience].ToString();
                vEmpBase.EmpSelfReach = dt.Rows[0][vEmpBase.strEmpSelfReach].ToString();
                vEmpBase.EmpSelfDevelope = dt.Rows[0][vEmpBase.strEmpSelfDevelope].ToString();
                vEmpBase.EmpSelfSpecial = dt.Rows[0][vEmpBase.strEmpSelfSpecial].ToString();
                vEmpBase.EmpSelfImprove = dt.Rows[0][vEmpBase.strEmpSelfImprove].ToString();
                vEmpBase.EmpSelfContribute = dt.Rows[0][vEmpBase.strEmpSelfContribute].ToString();
                vEmpBase.EmpSelfCooperate = dt.Rows[0][vEmpBase.strEmpSelfCooperate].ToString();
                vEmpBase.EmpSelfTeachPlan = dt.Rows[0][vEmpBase.strEmpSelfTeachPlan].ToString();
                vEmpBase.EmpSelfLifePlan = dt.Rows[0][vEmpBase.strEmpSelfLifePlan].ToString();
                vEmpBase.EmpNoTeachExp = (Boolean)dt.Rows[0][vEmpBase.strEmpNoTeachExp];
                vEmpBase.EmpNoTeachCa = (Boolean)dt.Rows[0][vEmpBase.strEmpNoTeachCa];
                vEmpBase.EmpNoHonour = (Boolean)dt.Rows[0][vEmpBase.strEmpNoHonour];
                //vEmpBase.EmpStatus = (Boolean)dt.Rows[0][vEmpBase.strEmpStatus]; 

                return vEmpBase;
            }
            else
            {
                return null;
            }
        }


        //取得指定的申請共用延伸檔原傳入參數EmpSn 因為轉多單，必須給Key序號
        public VApplyAudit GetApplyAuditObj(int AppSn)
        {
            GetSettings getSettings = new GetSettings();
            getSettings.Execute();
            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] = '" + AppSn + "'"; //and [AppYear] = '" + getSettings.GetYear() + "' and [AppSemester] = '" + getSettings.GetSemester() + "'
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();
                vApplyAudit.ReasearchResultUploadName = dt.Rows[0][vApplyAudit.strReasearchResultUploadName].ToString();
                return vApplyAudit;
            }
            else
            {
                return null;
            }
        }

        //撈取歷史紀錄最後一個申請單
        public VApplyAudit GetApplyAuditObjLastOne(int EmpSn)
        {
            GetSettings getSettings = new GetSettings();
            getSettings.Execute();
            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpSn] = " + EmpSn + " ORDER BY  [AppBuildDate] DESC ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                //vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                //vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();
                return vApplyAudit;
            }
            else
            {
                return null;
            }
        }

        //撈取

        //取得指定的申請共用延伸檔 升等仍用身分證號抓
        public VApplyAudit GetApplyAuditObjByIdno(String strIdno)
        {
            GetSettings getSettings = new GetSettings();
            DateTime time = new DateTime();
            getSettings.Execute();
            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "";
            if (getSettings.GetYear() != null && getSettings.GetSemester() != null)
            {
                strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + getSettings.GetYear().ToString() + "' and [AppSemester]='" + getSettings.GetSemester().ToString() + "'";
            }
            else
                strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + getSettings.LoadYear + "' and [AppSemester]='" + getSettings.LoadSemester + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();
                vApplyAudit.ReasearchResultUploadName = dt.Rows[0][vApplyAudit.strReasearchResultUploadName].ToString();

                return vApplyAudit;
            }
            else
            {
                strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + getSettings.LoadYear.ToString() + "' and [AppSemester]='" + getSettings.LoadSemester.ToString() + "'";
                ds = new DataSet();
                du = new SqlDataUpdater(strSql, ref ds, "S");
                dt = ds.Tables[0]; /// table of dataset
                if (hasData(ds))
                {
                    vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                    vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                    vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                    vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                    vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                    vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                    vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                    vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                    vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                    vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                    vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                    vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                    //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                    //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                    vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                    vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                    vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                    vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                    vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                    vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                    vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                    vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                    vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                    vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                    vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                    vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                    vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                    vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                    vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                    vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                    vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                    vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                    vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                    vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                    vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                    vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                    vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                    vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                    vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                    vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                    vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                    //多單增加欄位
                    vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                    vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                    vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                    vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                    vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                    vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                    vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                    vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                    vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                    vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                    vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                    vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                    vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                    vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();

                    return vApplyAudit;
                }
                else
                {
                    return null;
                }
            }
        }
        //取得指定的申請共用延伸檔 升等仍用身分證號抓
        public VApplyAudit GetApplyAuditObjByIdnoPromote(String strIdno)
        {
            GetSettings getSettings = new GetSettings();
            DateTime time = new DateTime();
            getSettings.Execute();
            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "";
            //if (getSettings.GetYear() != null && getSettings.GetSemester() != null)
            //{
            //    strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + getSettings.GetYear().ToString() + "' and [AppSemester]='" + getSettings.GetSemester().ToString() + "'";
            //}
            //else
            strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + getSettings.NowYear + "' and [AppSemester]='" + getSettings.NowSemester + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();

                return vApplyAudit;
            }
            else
            {
                return null;
            }
            //}
        }

        //取得指定的申請共用延伸檔 升等退回或修改資料專用
        public VApplyAudit GetApplyAuditObjByIdnoPromote(string strIdno, string year, string semester)
        {
            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "";
            strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + year + "' and [AppSemester]='" + semester + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();

                return vApplyAudit;
            }
            else
            {
                return null;
            }
            //}
        }
        //取得指定的申請共用延伸檔 升等仍用身分證號抓
        public VApplyAudit GetApplyAuditObjByAppSnPromote(String strIdno, String appSn)
        {
            GetSettings getSettings = new GetSettings();
            DateTime time = new DateTime();
            getSettings.Execute();
            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "";
            //if (getSettings.GetYear() != null && getSettings.GetSemester() != null)
            //{
            //    strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + getSettings.GetYear().ToString() + "' and [AppSemester]='" + getSettings.GetSemester().ToString() + "'";
            //}
            //else
            strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppSn] ='" + appSn + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();
                vApplyAudit.ReasearchResultUploadName = dt.Rows[0][vApplyAudit.strReasearchResultUploadName].ToString();


                return vApplyAudit;
            }
            else
            {
                return null;
            }
        }
        //取得指定的申請共用延伸檔 升等仍用身分證號抓
        public VApplyAudit GetApplyAuditObjByHRAppSnPromote(String strIdno, String appSn)
        {
            GetSettings getSettings = new GetSettings();
            DateTime time = new DateTime();
            getSettings.Execute();
            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "";
            //if (getSettings.GetYear() != null && getSettings.GetSemester() != null)
            //{
            //    strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppYear] ='" + getSettings.GetYear().ToString() + "' and [AppSemester]='" + getSettings.GetSemester().ToString() + "'";
            //}
            //else
            strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] =  '" + strIdno + "' and [AppSn] ='" + appSn + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();
                vApplyAudit.ReasearchResultUploadName = dt.Rows[0][vApplyAudit.strReasearchResultUploadName].ToString();

                return vApplyAudit;
            }
            else
            {
                return null;
            }
        }

        //取得年度 指定人 指定系所 (避免申請同一系所)
        //SELECT count(*) FROM ApplyAudit WHERE (EmpIdno = 'N123957442') AND (AppYear = '105') AND (AppSemester = '2') AND (AppUnitNo = '10100')
        public int GetApplyAuditIsExist(String strIdno, String strUnitNo)
        {
            int intCnt = 0;
            String strSql = "SELECT count(*) FROM ApplyAudit WHERE (EmpIdno = '" + strIdno + "') AND (AppYear = '" + getSettings.GetYear() + "') AND (AppSemester = '" + getSettings.GetSemester() + "') AND (AppUnitNo = '" + strUnitNo + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intCnt = Int32.Parse(dt.Rows[0][0].ToString());
            }
            return intCnt;
        }

        //取得指定的申請共用延伸檔
        public VApplyAudit GetApplyAuditObjByAppSn(int AppSn)
        {

            VApplyAudit vApplyAudit = null;
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] = " + AppSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit = new VApplyAudit();
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppKindNo = dt.Rows[0][vApplyAudit.strAppKindNo].ToString();
                vApplyAudit.AppWayNo = dt.Rows[0][vApplyAudit.strAppWayNo].ToString();
                vApplyAudit.AppAttributeNo = dt.Rows[0][vApplyAudit.strAppAttributeNo].ToString();
                vApplyAudit.AppUnitNo = dt.Rows[0][vApplyAudit.strAppUnitNo].ToString();
                vApplyAudit.AppJobTitleNo = dt.Rows[0][vApplyAudit.strAppJobTitleNo].ToString();
                vApplyAudit.AppJobTypeNo = dt.Rows[0][vApplyAudit.strAppJobTypeNo].ToString();
                vApplyAudit.AppLawNumNo = dt.Rows[0][vApplyAudit.strAppLawNumNo].ToString();
                //vApplyAudit.AppJournalUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppJournalUpload]; ;
                //vApplyAudit.AppJournalUploadName = dt.Rows[0][vApplyAudit.strAppJournalUploadName].ToString();
                vApplyAudit.AppPublicationName = dt.Rows[0][vApplyAudit.strAppPublicationName].ToString();
                vApplyAudit.AppPublicationUploadName = dt.Rows[0][vApplyAudit.strAppPublicationUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppDeclarationUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDeclarationUpload];
                vApplyAudit.AppDeclarationUploadName = dt.Rows[0][vApplyAudit.strAppDeclarationUploadName].ToString();
                vApplyAudit.AppPPMUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppPPMUpload];
                vApplyAudit.AppPPMUploadName = dt.Rows[0][vApplyAudit.strAppPPMUploadName].ToString();
                vApplyAudit.AppOtherServiceUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherServiceUpload];
                vApplyAudit.AppOtherServiceUploadName = dt.Rows[0][vApplyAudit.strAppOtherServiceUploadName].ToString();
                vApplyAudit.AppOtherTeachingUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppOtherTeachingUpload];
                vApplyAudit.AppOtherTeachingUploadName = dt.Rows[0][vApplyAudit.strAppOtherTeachingUploadName].ToString();
                vApplyAudit.AppDrCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppDrCaUpload];
                vApplyAudit.AppDrCaUploadName = dt.Rows[0][vApplyAudit.strAppDrCaUploadName].ToString();
                vApplyAudit.AppTeacherCaUpload = (Boolean)dt.Rows[0][vApplyAudit.strAppTeacherCaUpload];
                vApplyAudit.AppTeacherCaUploadName = dt.Rows[0][vApplyAudit.strAppTeacherCaUploadName].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                vApplyAudit.AppResearchYear = dt.Rows[0][vApplyAudit.strAppResearchYear].ToString();
                vApplyAudit.AppThesisAccuScore = dt.Rows[0][vApplyAudit.strAppThesisAccuScore].ToString();
                vApplyAudit.AppRPIScore = dt.Rows[0][vApplyAudit.strAppRPIScore].ToString();
                vApplyAudit.AppStatus = (Boolean)dt.Rows[0][vApplyAudit.strAppStatus];
                vApplyAudit.AppStage = dt.Rows[0][vApplyAudit.strAppStage].ToString();
                vApplyAudit.AppStep = dt.Rows[0][vApplyAudit.strAppStep].ToString();
                //多單增加欄位
                vApplyAudit.AppRecommendors = dt.Rows[0][vApplyAudit.strAppRecommendors].ToString();
                vApplyAudit.AppRecommendYear = dt.Rows[0][vApplyAudit.strAppRecommendYear].ToString();
                vApplyAudit.AppRecommendUploadName = dt.Rows[0][vApplyAudit.strAppRecommendUploadName].ToString();
                vApplyAudit.AppResumeUploadName = dt.Rows[0][vApplyAudit.strAppResumeUploadName].ToString();
                vApplyAudit.ThesisScoreUploadName = dt.Rows[0][vApplyAudit.strThesisScoreUploadName].ToString();
                vApplyAudit.AppSelfTeachExperience = dt.Rows[0][vApplyAudit.strAppSelfTeachExperience].ToString();
                vApplyAudit.AppSelfReach = dt.Rows[0][vApplyAudit.strAppSelfReach].ToString();
                vApplyAudit.AppSelfDevelope = dt.Rows[0][vApplyAudit.strAppSelfDevelope].ToString();
                vApplyAudit.AppSelfSpecial = dt.Rows[0][vApplyAudit.strAppSelfSpecial].ToString();
                vApplyAudit.AppSelfImprove = dt.Rows[0][vApplyAudit.strAppSelfImprove].ToString();
                vApplyAudit.AppSelfContribute = dt.Rows[0][vApplyAudit.strAppSelfContribute].ToString();
                vApplyAudit.AppSelfCooperate = dt.Rows[0][vApplyAudit.strAppSelfCooperate].ToString();
                vApplyAudit.AppSelfTeachPlan = dt.Rows[0][vApplyAudit.strAppSelfTeachPlan].ToString();
                vApplyAudit.AppSelfLifePlan = dt.Rows[0][vApplyAudit.strAppSelfLifePlan].ToString();
                vApplyAudit.ReasearchResultUploadName = dt.Rows[0][vApplyAudit.strReasearchResultUploadName].ToString();
                return vApplyAudit;
            }
            else
            {
                return null;
            }
        }

        //取得有關著作資料
        public VApplyAudit GetDegreeByAppSn(int AppSn)
        {

            VApplyAudit vApplyAudit = new VApplyAudit();
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [AppSn] = " + AppSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vApplyAudit.AppSn = Int32.Parse(dt.Rows[0][vApplyAudit.strAppSn].ToString());
                vApplyAudit.EmpSn = Int32.Parse(dt.Rows[0][vApplyAudit.strEmpSn].ToString());
                vApplyAudit.EmpIdno = dt.Rows[0][vApplyAudit.strEmpIdno].ToString();
                vApplyAudit.AppYear = dt.Rows[0][vApplyAudit.strAppYear].ToString();
                vApplyAudit.AppSemester = dt.Rows[0][vApplyAudit.strAppSemester].ToString();
                vApplyAudit.AppSummaryCN = dt.Rows[0][vApplyAudit.strAppSummaryCN].ToString();
                vApplyAudit.AppDegreeName = dt.Rows[0][vApplyAudit.strAppDegreeName].ToString();
                vApplyAudit.AppDegreeUploadName = dt.Rows[0][vApplyAudit.strAppDegreeUploadName].ToString();
                return vApplyAudit;
            }
            else
            {
                return null;
            }
        }

        //取得指定的新聘延檔(不用)
        //public VAppendEmployee GetAppendEmployeeObj(int AppSn)
        //{
        //    VAppendEmployee vAppendEmployee = new VAppendEmployee();
        //    String strSql = "SELECT * FROM [AppendEmployee] WHERE [AppSn] = " + AppSn + "";
        //    DataSet ds = new DataSet();
        //    SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
        //    DataTable dt = ds.Tables[0]; /// table of dataset
        //    if (hasData(ds))
        //    {
        //        vAppendEmployee.AppESn = Int32.Parse(dt.Rows[0][vAppendEmployee.strAppESn].ToString());
        //        vAppendEmployee.AppSn = Int32.Parse(dt.Rows[0][vAppendEmployee.strAppSn].ToString());
        //        vAppendEmployee.AppNowJobOrg = dt.Rows[0][vAppendEmployee.strAppNowJobOrg].ToString();
        //        vAppendEmployee.AppNote = dt.Rows[0][vAppendEmployee.strAppNote].ToString();
        //        vAppendEmployee.AppRecommendors = dt.Rows[0][vAppendEmployee.strAppRecommendors].ToString();
        //        vAppendEmployee.AppRecommendYear = dt.Rows[0][vAppendEmployee.strAppRecommendYear].ToString();
        //        vAppendEmployee.AppRecommendUpload = (Boolean)dt.Rows[0][vAppendEmployee.strAppRecommendUpload];
        //        vAppendEmployee.AppRecommendUploadName = dt.Rows[0][vAppendEmployee.strAppRecommendUploadName].ToString();
        //        return vAppendEmployee;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}




        //取得指定的學位延伸檔 
        public VAppendDegree GetAppendDegreeByAppSn(int AppSn)
        {
            VAppendDegree vAppendDegree = new VAppendDegree();
            String strSql = "SELECT * FROM [AppendDegree] WHERE [AppSn] = " + AppSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vAppendDegree.AppDSn = Int32.Parse(dt.Rows[0][vAppendDegree.strAppDSn].ToString());
                vAppendDegree.AppSn = Int32.Parse(dt.Rows[0][vAppendDegree.strAppSn].ToString());
                vAppendDegree.AppDDegreeThesisName = dt.Rows[0][vAppendDegree.strAppDDegreeThesisName].ToString();
                vAppendDegree.AppDDegreeThesisNameEng = dt.Rows[0][vAppendDegree.strAppDDegreeThesisNameEng].ToString();
                vAppendDegree.AppDDegreeThesisUploadName = dt.Rows[0][vAppendDegree.strAppDDegreeThesisUploadName].ToString();
                vAppendDegree.AppDFgnEduDeptSchoolAdmit = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnEduDeptSchoolAdmit];

                vAppendDegree.AppDFgnDegreeUploadName = dt.Rows[0][vAppendDegree.strAppDFgnDegreeUploadName].ToString();
                vAppendDegree.AppDFgnGradeUpload = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnGradeUpload];
                vAppendDegree.AppDFgnGradeUploadName = dt.Rows[0][vAppendDegree.strAppDFgnGradeUploadName].ToString();
                vAppendDegree.AppDFgnSelectCourseUpload = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnSelectCourseUpload];
                vAppendDegree.AppDFgnSelectCourseUploadName = dt.Rows[0][vAppendDegree.strAppDFgnSelectCourseUploadName].ToString();
                vAppendDegree.AppDFgnEDRecordUpload = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnEDRecordUpload];
                vAppendDegree.AppDFgnEDRecordUploadName = dt.Rows[0][vAppendDegree.strAppDFgnEDRecordUploadName].ToString();
                vAppendDegree.AppDFgnJPAdmissionUpload = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnJPAdmissionUpload];
                vAppendDegree.AppDFgnJPAdmissionUploadName = dt.Rows[0][vAppendDegree.strAppDFgnJPAdmissionUploadName].ToString();
                vAppendDegree.AppDFgnJPGradeUpload = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnJPGradeUpload];
                vAppendDegree.AppDFgnJPGradeUploadName = dt.Rows[0][vAppendDegree.strAppDFgnJPGradeUploadName].ToString();
                vAppendDegree.AppDFgnJPEnrollCAUpload = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnJPEnrollCAUpload];
                vAppendDegree.AppDFgnJPEnrollCAUploadName = dt.Rows[0][vAppendDegree.strAppDFgnJPEnrollCAUploadName].ToString();
                vAppendDegree.AppDFgnJPDissertationPassUpload = (Boolean)dt.Rows[0][vAppendDegree.strAppDFgnJPDissertationPassUpload];
                vAppendDegree.AppDFgnJPDissertationPassUploadName = dt.Rows[0][vAppendDegree.strAppDFgnJPDissertationPassUploadName].ToString();
                return vAppendDegree;
            }
            else
            {
                return null;
            }
        }

        //撈取 學位送審論文教師(Type=1)&口試委員名單(Type=2)
        public VThesisOral GetAppThesisOral(int ThesisOralSn)
        {
            VThesisOral vThesisOral = new VThesisOral();
            String strSql = "SELECT * FROM [ThesisOralList] WHERE [ThesisOralSn] = " + ThesisOralSn + "";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                vThesisOral.ThesisOralAppSn = Int32.Parse(dt.Rows[0][vThesisOral.strThesisOralAppSn].ToString());
                vThesisOral.ThesisOralType = dt.Rows[0][vThesisOral.strThesisOralType].ToString();
                vThesisOral.ThesisOralName = dt.Rows[0][vThesisOral.strThesisOralName].ToString();
                vThesisOral.ThesisOralTitle = dt.Rows[0][vThesisOral.strThesisOralTitle].ToString();
                vThesisOral.ThesisOralUnit = dt.Rows[0][vThesisOral.strThesisOralUnit].ToString();
                vThesisOral.ThesisOralOther = dt.Rows[0][vThesisOral.strThesisOralOther].ToString();
                return vThesisOral;
            }
            else
            {
                return null;
            }
        }

        //撈取所有AuditPointRole
        public ArrayList GetAllAuditPointRole(string auditPointDepartment, Boolean auditPointAttribute)
        {
            ArrayList arrayList = new ArrayList();
            VAuditPointRole vAuditPointRole;
            String strSql = "SELECT * FROM [AuditPointRole] WHERE " +
                " AuditPointDepartment = '" + auditPointDepartment + "' AND AuditPointAttribute = '" + auditPointAttribute + "' order by AuditPointDepartment, AuditPointAttribute, AuditPointStage, AuditPointStep";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                vAuditPointRole = new VAuditPointRole();
                vAuditPointRole.AuditPointSn = dr[vAuditPointRole.strAuditPointSn].ToString();
                vAuditPointRole.AuditPointRoleName = dr[vAuditPointRole.strAuditPointRoleName].ToString();
                vAuditPointRole.AuditPointRoleLevel = dr[vAuditPointRole.strAuditPointRoleLevel].ToString();
                vAuditPointRole.AuditPointStage = dr[vAuditPointRole.strAuditPointStage].ToString();
                vAuditPointRole.AuditPointStep = dr[vAuditPointRole.strAuditPointStep].ToString();
                arrayList.Add(vAuditPointRole);
            }
            return arrayList;
        }

        //撈取現在AuditPointRole
        public String GetNowAuditPoint(string stage, string step)
        {
            String stageName = "";
            ArrayList arrayList = new ArrayList();
            String strSql = "SELECT Top(1) AuditPointRoleName FROM [AuditPointRole] WHERE " +
                " AuditPointDepartment = 'E0100' AND AuditPointAttribute = 'true' order by AuditPointStage, AuditPointStep";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            if (hasData(ds))
            {
                stageName = ds.Tables[0].Rows[0]["AuditPointRoleName"].ToString();
            }
            return stageName;
        }

        //撈取單位取得上一級單位
        public VUnit GetVUnit(string UnitId)
        {
            VUnit vUnit = new VUnit();
            String strSql = "select distinct unt_id, unt_name_full , dpt_id, unt_id2, unt_levelb " +
                            "from [s90_unit] " +
                            "where unt_id  = '" + UnitId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            if (hasData(ds))
            {
                vUnit.UntId = ds.Tables[0].Rows[0][vUnit.strUntId].ToString();
                vUnit.UntId2 = ds.Tables[0].Rows[0][vUnit.strUntId2].ToString();
                vUnit.DptId = ds.Tables[0].Rows[0][vUnit.strDptId].ToString();
                vUnit.UntLevelb = ds.Tables[0].Rows[0][vUnit.strUntLevelb].ToString();
                vUnit.UntNameFull = ds.Tables[0].Rows[0][vUnit.strUntNameFull].ToString();
            }

            return vUnit;
        }


        //撈取一級,二級,三級主管資料
        public VUnit GetVUnit(string UnitId, string levelB)
        {
            VUnit vUnit = null;
            String strSql = "select distinct a.unt_id,a.unt_name_full ,a.dpt_id,a.unt_id2,a.unt_levelb,a.unt_empid, b.emp_name,b.emp_email " +
                            "from [s90_unit] as a left outer join [sup_dept_one] as b on a.unt_empid = b.emp_id " +
                            "where a.unt_id  = '" + UnitId + "' and a.unt_levelb = '" + levelB + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            if (hasData(ds))
            {
                vUnit = new VUnit();
                vUnit.UntId = ds.Tables[0].Rows[0][vUnit.strUntId].ToString();
                vUnit.UntId2 = ds.Tables[0].Rows[0][vUnit.strUntId2].ToString();
                vUnit.DptId = ds.Tables[0].Rows[0][vUnit.strDptId].ToString();
                vUnit.UntLevelb = ds.Tables[0].Rows[0][vUnit.strUntLevelb].ToString();
                vUnit.UntNameFull = ds.Tables[0].Rows[0][vUnit.strUntNameFull].ToString();
                vUnit.UntSupEmpId = ds.Tables[0].Rows[0][vUnit.strUntSupEmpId].ToString();
                vUnit.UntEmpSupName = ds.Tables[0].Rows[0][vUnit.strUntSupEmpEmail].ToString();
                vUnit.UntSupEmpEmail = ds.Tables[0].Rows[0][vUnit.strUntSupEmpEmail].ToString();
            }

            return vUnit;
        }


        //撈取人事資料
        public VEmpTmuHr GetVEmployeeFromTmuHr(string EmpIdno)
        {
            VEmpTmuHr vEmpTmuHr = new VEmpTmuHr();
            String strSql =
            //"SELECT c.emp_idno, c.emp_id, c.emp_passid, c.emp_sex, c.emp_name, c.emp_ename, c.emp_efname, c.emp_elname, c.emp_email, c.emp_nation, c.emp_bdate, c.emp_untid, c.emp_titid, c.emp_posid, c.emp_poaddt, c.emp_indate, " +
            //" c.emp_pstate, c.emp_pzcode, c.emp_padrs, c.emp_ptel, c.emp_mzcode1, c.emp_madr1, c.emp_mtel1, c.emp_dgd, c.emp_rtel, c.emp_teachno, '北醫大' + e.unt_name_full + '-' + c.unt_name_full AS unt_full_name " +
            //" FROM (SELECT  a.emp_idno, a.emp_id, a.emp_passid, a.emp_sex, a.emp_name, a.emp_ename, a.emp_efname, a.emp_elname, a.emp_email, a.emp_nation, a.emp_bdate, a.emp_untid, a.emp_titid, a.emp_posid, " +
            //" a.emp_poaddt, a.emp_indate, a.emp_pstate, a.emp_pzcode, a.emp_padrs, a.emp_ptel, a.emp_mzcode1,a.emp_madr1, a.emp_mtel1, a.emp_dgd, a.emp_rtel, a.emp_teachno, b.unt_name_full, b.dpt_id " +
            //" FROM s10_employee AS a LEFT OUTER JOIN s90_unit AS b ON a.emp_untid = b.unt_id " +
            //" WHERE (a.emp_idno  = '" + EmpIdno + "')) AS c LEFT OUTER JOIN s90_unit AS e ON c.dpt_id = e.unt_id";
            "SELECT c.emp_idno, c.emp_id, c.emp_passid, c.emp_sex, c.emp_name, c.emp_ename, c.emp_efname, c.emp_elname, c.emp_email, c.emp_nation, c.emp_bdate, c.emp_untid, c.emp_wuntid, f.tit_name as emp_titid_name, c.emp_titid, c.emp_posid, c.emp_poaddt, " +
            "c.emp_indate, c.emp_pstate, c.emp_pzcode, c.emp_padrs, c.emp_ptel, c.emp_mzcode1, c.emp_madr1, c.emp_mtel1, c.emp_dgd, c.emp_rtel, c.emp_teachno, e.unt_name_full + '-' + c.unt_name_full AS unt_full_name " +
            "FROM (SELECT a.emp_idno, a.emp_id, a.emp_passid, a.emp_sex, a.emp_name, a.emp_ename, a.emp_efname, a.emp_elname, a.emp_email, a.emp_nation, a.emp_bdate, a.emp_untid, a.emp_wuntid, a.emp_titid, a.emp_posid, a.emp_poaddt, a.emp_indate, a.emp_pstate, a.emp_pzcode, a.emp_padrs, a.emp_ptel, a.emp_mzcode1, " +
            "a.emp_madr1, a.emp_mtel1, a.emp_dgd, a.emp_rtel, a.emp_teachno, b.unt_name_full, b.dpt_id FROM s10_employee AS a LEFT OUTER JOIN s90_unit AS b ON a.emp_untid = b.unt_id WHERE (a.emp_idno = '" + EmpIdno + "')) AS c LEFT OUTER JOIN s90_unit AS e ON c.dpt_id = e.unt_id LEFT OUTER JOIN " +
            "s90_titlecode AS f ON c.emp_titid = f.tit_id ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            if (hasData(ds))
            {

                vEmpTmuHr.EmpIdno = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpIdno].ToString();
                vEmpTmuHr.EmpId = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpId].ToString();
                vEmpTmuHr.EmpPassid = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPassid].ToString();
                vEmpTmuHr.EmpSex = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpSex].ToString();
                vEmpTmuHr.EmpName = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpName].ToString();
                vEmpTmuHr.EmpEname = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpEname].ToString();
                vEmpTmuHr.EmpEfname = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpEfname].ToString();
                vEmpTmuHr.EmpElname = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpElname].ToString();
                vEmpTmuHr.EmpEmail = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpEmail].ToString();
                vEmpTmuHr.EmpNation = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpNation].ToString();
                vEmpTmuHr.EmpBdate = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpBDate].ToString();
                vEmpTmuHr.EmpUntid = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpUntid].ToString();
                vEmpTmuHr.EmpWUntid = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpWUntid].ToString();
                vEmpTmuHr.EmpTitid = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpTitid].ToString();
                vEmpTmuHr.EmpTitidName = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpTitidName].ToString(); //add
                vEmpTmuHr.EmpPosid = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPosid].ToString();
                //vEmpTmuHr.EmpPosidName = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPosidName].ToString(); //add
                vEmpTmuHr.EmpPoaddt = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPoaddt].ToString();
                //vEmpTmuHr.EmpPoaddtName = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPoaddtName].ToString(); //add
                vEmpTmuHr.EmpIndate = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpIndate].ToString();
                vEmpTmuHr.EmpPstate = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPstate].ToString();
                //vEmpTmuHr.EmpPstateName = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPstateName].ToString(); //add
                vEmpTmuHr.EmpPzcode = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPzcode].ToString();
                vEmpTmuHr.EmpPadrs = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPadrs].ToString();
                vEmpTmuHr.EmpMzcode1 = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpMzcode1].ToString();
                vEmpTmuHr.EmpMadr1 = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpMadr1].ToString();
                vEmpTmuHr.EmpPtel = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpPtel].ToString();
                vEmpTmuHr.EmpMtel1 = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpDgd].ToString();
                vEmpTmuHr.EmpDgd = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpDgd].ToString();
                vEmpTmuHr.EmpRtel = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpRtel].ToString();
                vEmpTmuHr.EmpTeachno = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpTeachno].ToString();
                vEmpTmuHr.EmpUntFullName = ds.Tables[0].Rows[0][vEmpTmuHr.strEmpUntFullName].ToString();
                return vEmpTmuHr;
            }
            else
            {
                return null;
            }
        }

        //撈取可以看得到的單位
        //public string GetViewUnit()
        //{

        //}

        //撈取身份證字號 SELECT emp_idno, emp_id, emp_name FROM [PAPOVA].[TmuHr].[dbo].[s10_employee] WHERE emp_email = 'learnlife@tmu.edu.tw'
        public string GetVEmployeeFromTmuHrByEmail(string email)
        {
            String empIdno = "";
            String strSql = "SELECT emp_idno FROM [s10_employee] WHERE emp_email = '" + email + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            if (hasData(ds))
            {
                empIdno = ds.Tables[0].Rows[0][0].ToString();
                return empIdno;
            }
            else
            {
                return null;
            }
        }

        public string GetVEmployeeIdFromTmuHrByEmail(string email)
        {
            String empData = null;
            String strSql = "SELECT emp_id,emp_name FROM [s10_employee] WHERE emp_email = '" + email + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            if (hasData(ds))
            {
                empData = ds.Tables[0].Rows[0][0].ToString() + "," + ds.Tables[0].Rows[0][1].ToString();
                return empData;
            }
            else
            {
                return empData;
            }
        }

        //撈取empId,姓名,email SELECT emp_id, emp_name, emp_email FROM [PAPOVA].[TmuHr].[dbo].[s10_employee] WHERE emp_id = ''
        public DataTable GetVEmployeeFromTmuHrByEmpId(string empId)
        {
            String strSql = "SELECT emp_id, emp_name, emp_email FROM [s10_employee] WHERE emp_id =  '" + empId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //由身份證字號撈取 員工編號
        public String GetVEmployeeFromTmuHrByEmpIdno(string empIdno)
        {
            String empId = "";
            String strSql = "SELECT emp_id, emp_name, emp_email FROM [s10_employee] WHERE emp_idno =  '" + empIdno + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                empId = ds.Tables[0].Rows[0][0].ToString();
                return empId;
            }
            else
            {
                return null;
            }
        }

        //撈取應徵單位 開放時間
        public DataTable GetOpenUnit()
        {
            //            ArrayList myAL = new ArrayList();
            //            VUnit vUnit = new VUnit();
            //          SELECT     a.unt_id , a.unt_name_full 
            //          FROM         PAPOVA,2433.TmuHr.dbo.s90_unit a inner JOIN
            //                      tmu.dbo.ApplyUnitOpen b
            //          on     (a.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS = b.AppUnit) 
            //        where (b.AppUnitOpenBeginDate <= '2015/08/29 14:21:28') AND (b.AppUnitOpenEndDate >= '2015/08/29 14:21:28') 
            //dTime.ToString("yyyy/MM/dd HH:mm:ss")
            int todayDate = Int32.Parse("" + (DateTime.Now.Year - 1911) + String.Format("{0:00}", DateTime.Now.Month) + String.Format("{0:00}", DateTime.Now.Day));
            String strSql = "SELECT DISTINCT s90_unit.unt_id, s90_unit.unt_name_full FROM s90_unit INNER JOIN s13_opendate ON s90_unit.unt_id = s13_opendate.op_unit WHERE (s90_unit.unt_attr = 'E') AND (s90_unit.unt_use_yn = 'Y') AND (s13_opendate.op_kind = 'new') AND " +
                             "(s13_opendate.op_bdate <= " + todayDate + ") AND (s13_opendate.op_edate >= " + todayDate + ") " +
                             "and SUBSTRING(s90_unit.unt_id, 2, 5) <> '0000' ORDER BY   s90_unit.unt_id"; //排除學院的

            //            String strSql = "SELECT a.unt_id, a.unt_name_full  FROM [PAPOVA].[TmuHr].[dbo].[s90_unit] a inner JOIN [ApplyUnitOpen] b on     (a.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS = b.AppUnit)  WHERE  unt_attr='E' and unt_use_yn='Y' "
            //                            + "and b.AppUnitOpenBeginDate <= '2015/08/29 14:21:28' and b.AppUnitOpenEndDate >= '2015/08/29 14:21:28'";

            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public Boolean GetOpenOrCloseUnit(String DeptNo, String poaddt)
        {
            //20210223 追加專兼任判定
            //            ArrayList myAL = new ArrayList();
            //            VUnit vUnit = new VUnit();
            //          SELECT     a.unt_id , a.unt_name_full 
            //          FROM         PAPOVA,2433.TmuHr.dbo.s90_unit a inner JOIN
            //                      tmu.dbo.ApplyUnitOpen b
            //          on     (a.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS = b.AppUnit) 
            //        where (b.AppUnitOpenBeginDate <= '2015/08/29 14:21:28') AND (b.AppUnitOpenEndDate >= '2015/08/29 14:21:28') 
            //dTime.ToString("yyyy/MM/dd HH:mm:ss")
            int todayDate = Int32.Parse("" + (DateTime.Now.Year - 1911) + String.Format("{0:00}", DateTime.Now.Month) + String.Format("{0:00}", DateTime.Now.Day));
            String strSql = "SELECT DISTINCT s90_unit.unt_id, s90_unit.unt_name_full FROM s90_unit INNER JOIN s13_opendate ON s90_unit.unt_id = s13_opendate.op_unit WHERE (s90_unit.unt_attr = 'E') AND (s90_unit.unt_use_yn = 'Y') AND (s13_opendate.op_kind = 'new') AND " +
                             "(s13_opendate.op_bdate <= " + todayDate + ") AND (s13_opendate.op_edate >= " + todayDate + ") " +
                             "and SUBSTRING(s90_unit.unt_id, 2, 5) <> '0000' and  op_unit ='" + DeptNo + "' and op_poaddt='" + poaddt + "'  ORDER BY   s90_unit.unt_id"; //排除學院的

            //            String strSql = "SELECT a.unt_id, a.unt_name_full  FROM [PAPOVA].[TmuHr].[dbo].[s90_unit] a inner JOIN [ApplyUnitOpen] b on     (a.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS = b.AppUnit)  WHERE  unt_attr='E' and unt_use_yn='Y' "
            //                            + "and b.AppUnitOpenBeginDate <= '2015/08/29 14:21:28' and b.AppUnitOpenEndDate >= '2015/08/29 14:21:28'";

            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;

        }


        //撈取應徵單位 開放時間，用於後台如果申請關閉，仍可選系所或學科
        public DataTable GetOpenUnitUp()
        {
            //            ArrayList myAL = new ArrayList();
            //            VUnit vUnit = new VUnit();
            //          SELECT     a.unt_id , a.unt_name_full 
            //          FROM         PAPOVA,2433.TmuHr.dbo.s90_unit a inner JOIN
            //                      tmu.dbo.ApplyUnitOpen b
            //          on     (a.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS = b.AppUnit) 
            //        where (b.AppUnitOpenBeginDate <= '2015/08/29 14:21:28') AND (b.AppUnitOpenEndDate >= '2015/08/29 14:21:28') 
            //dTime.ToString("yyyy/MM/dd HH:mm:ss")
            int todayDate = Int32.Parse("" + (DateTime.Now.Year - 1911) + String.Format("{0:00}", DateTime.Now.Month) + String.Format("{0:00}", DateTime.Now.Day));
            String strSql = "SELECT DISTINCT s90_unit.unt_id, s90_unit.unt_name_full FROM s90_unit INNER JOIN s13_opendate ON s90_unit.unt_id = s13_opendate.op_unit WHERE (s90_unit.unt_attr = 'E') AND (s90_unit.unt_use_yn = 'Y') AND (s13_opendate.op_kind = 'new')  " +
                             "and SUBSTRING(s90_unit.unt_id, 2, 5) <> '0000' ORDER BY   s90_unit.unt_id"; //排除學院的

            //            String strSql = "SELECT a.unt_id, a.unt_name_full  FROM [PAPOVA].[TmuHr].[dbo].[s90_unit] a inner JOIN [ApplyUnitOpen] b on     (a.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS = b.AppUnit)  WHERE  unt_attr='E' and unt_use_yn='Y' "
            //                            + "and b.AppUnitOpenBeginDate <= '2015/08/29 14:21:28' and b.AppUnitOpenEndDate >= '2015/08/29 14:21:28'";

            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset


            return dt;
        }

        public DataTable getUnterTakeUnit() //請完成
        {
            //SELECT  distinct [PointDept]   FROM [HRApply].[dbo].[UnderTaker] group by PointDept
            //String strSql = "SELECT  distinct a.[PointDept],b.s90_unit.unt_name_full   FROM [HRApply].[dbo].[UnderTaker] as a Left Outer " +
            //    " JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit] as b ON a.PointDept = b.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS join group by a.PointDept";

            String strSql = "SELECT  distinct a.[PointDept],b.unt_name_full FROM [HRApply].[dbo].[UnderTaker] as a Left Outer JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit] as b ON a.PointDept = b.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS  where  b.unt_name_full is not null  Group By a.PointDept ,b.unt_name_full ORDER BY  a.PointDept";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 4);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取審核日期區間
        public VAuditPeroid GetAuditPeriod(string strPointStage)
        {
            string[] DateTimeList = {
                            "yyyy/M/d tt hh:mm:ss",
                            "yyyy/MM/dd tt hh:mm:ss",
                            "yyyy/MM/dd HH:mm:ss",
                            "yyyy/M/d HH:mm:ss",
                            "yyyy/M/d",
                            "yyyy/MM/dd"
                        };
            VAuditPeroid vAuditPeriod = new VAuditPeroid();
            String strSql = "SELECT AuditPeroidBeginDate,AuditPeroidEndDate FROM AuditPeroid WHERE  AuditPeroidPointStage = '" + strPointStage + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            String tdate = dt.Rows[0][vAuditPeriod.strAuditPeroidBeginDate].ToString();
            vAuditPeriod.AuditPeroidBeginDate = DateTime.ParseExact(tdate, DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            tdate = dt.Rows[0][vAuditPeriod.strAuditPeroidEndDate].ToString();
            vAuditPeriod.AuditPeroidEndDate = DateTime.ParseExact(dt.Rows[0][vAuditPeriod.strAuditPeroidEndDate].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);

            return vAuditPeriod;
        }

        //撈取新聘類型
        public DataTable GetApplyHrAttribute()
        {
            String strSql = "SELECT status, note FROM [s10_status] WHERE (table_name = 's13_humanres') AND (column_name = 'hr_apply') ORDER BY sort_seq";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取升等類型
        public DataTable GetApplyPrmAttribute()
        {
            String strSql = "SELECT status, note FROM [s10_status] WHERE (table_name = 's13_promote') AND (column_name = 'prm_apply') ORDER BY sort_seq";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取新聘類型 指定名稱
        public DataTable GetApplyHrAttributeName(string attributeId)
        {
            String strSql = "SELECT note FROM [s10_status] WHERE (table_name = 's13_humanres') AND (column_name = 'hr_apply')  AND  status = '" + attributeId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //撈取法令某一項會有幾款
        public DataTable GetTeacherLaw(string lawKindNo, string jobTitleNo)
        {
            String strSql = "SELECT LawItemNo,LawContent FROM CTeacherLaw WHERE (LawKindNo = '" + lawKindNo + "' and LawJobTitleNo = '" + jobTitleNo + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取法令某一項,某一款的內
        public String GetTeacherLaw(string lawKindNo, string jobTitleNo, string itemNo)
        {
            //itemNo 申請中有可能還未填寫 
            String strSql = "SELECT LawContent FROM CTeacherLaw WHERE (LawKindNo = '" + lawKindNo + "' and LawJobTitleNo = '" + jobTitleNo + "' and LawItemNo = '" + itemNo + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["LawContent"].ToString();
            }
            else
            {
                return "";
            }
        }




        //申請中的文件
        public DataTable GetAllInApplyData(string strYear, string strSemester)
        {
            String strSql = "SELECT b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay, b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell ,b.EmpIdno,x.AuditRecord,b.EmpSn, a.AppSn, a.AppStep, a.AppStatus, ISNULL(a.AppBuildDate,'') AppBuildDate, ISNULL(a.AppModifyDate,'') AppModifyDate " +
                            " FROM   ApplyAudit a  JOIN [EmployeeBase] b ON a.EmpSn = b.EmpSn " +
                            " JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit] c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            " Where a.AppStatus = 'False' and a.AppStage = '0' and a.AppYear = '" + strYear + "' and  AppSemester = '" + strSemester + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得個別申請一覽資料(一筆)
        public DataTable GetApplyerView(String empSn, String appSn)
        {
            String strBirthDay = "";
            int diffYear = 0;
            int diffMonth = 0;
            String strSql =
                //"SELECT EMP.Sn, EMP.Name, EMP.UntName, EMP.TitleName, EMP.AttrName, EMP.NowJob, T.Total, Edu.TopSchool, " +
                //"       CASE Edu.Degree WHEN '70' THEN '博士' WHEN '60' THEN '碩士' ELSE '學士' END AS Degree, EMP.Expert, " +
                //"       CASE EMP.sex WHEN 'M' THEN '男' WHEN 'F' THEN '女' ELSE '其他' END AS Sex, EMP.BirthDay, EMP.AuditWay" +
                "SELECT EMP.Sn, EMP.Name, EMP.UntName, EMP.TitleName, EMP.AttrName, EMP.NowJob, T.Total AS TotalScore,  " +
                "       Edu.TopSchool + CASE Edu.Degree WHEN '70' THEN '博士' WHEN '60' THEN '碩士' ELSE '學士' END AS Degree, " +
                "       EMP.Expert, CASE EMP.sex WHEN 'M' THEN '男' WHEN 'F' THEN '女' ELSE '其他' END AS Sex, EMP.BirthDay, EMP.AuditWay,EMP.AppBuildDate,EMP.AppModifyDate " +
                "FROM  (SELECT B.EmpSn AS Sn, B.EmpNameCN AS Name, C.unt_name_full AS UntName, G.JobTitleName AS TitleName, " +
                "      D.JobAttrName AS AttrName, B.EmpNowJobOrg AS NowJob, B.EmpSex AS Sex, " +
                "     B.EmpBirthDay AS BirthDay, B.EmpExpertResearch AS Expert, E.AttributeName AS AuditWay, ISNULL(a.AppBuildDate,'') AppBuildDate, ISNULL(a.AppModifyDate,'') AppModifyDate" +
                "     FROM ApplyAudit AS A LEFT OUTER JOIN" +
                "     EmployeeBase AS B ON A.EmpIdno = B.EmpIdno LEFT OUTER JOIN" +
                "     PAPOVA.TmuHr.dbo.s90_unit AS C ON " +
                "     A.AppUnitNo = C.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS LEFT OUTER JOIN" +
                "     CJobTitle AS G ON G.JobTitleNo = A.AppJobTitleNo LEFT OUTER JOIN" +
                "     CJobAttribute AS D ON D.JobAttrNo = A.AppJobTypeNo LEFT OUTER JOIN" +
                "     CAuditAttribute AS E ON E.KindNo = A.AppKindNo AND E.AttributeNo = A.AppAttributeNo" +
                "	  WHERE (A.AppSn = '" + appSn + "')) AS EMP LEFT OUTER JOIN" +
                "  (SELECT EmpSn, SUM(CONVERT(float, ThesisTotal)) AS Total" +
                "  FROM  ThesisScore" +
                "  WHERE (EmpSn = '" + empSn + "')" +
                "  GROUP BY EmpSn) AS T ON EMP.Sn = T.EmpSn LEFT OUTER JOIN" +
                "  (SELECT TOP (1) EmpSn, EduSchool + EduDepartment AS TopSchool, EduDegree AS Degree" +
                "  FROM TeacherEdu" +
                "  WHERE (EmpSn = '" + empSn + "')" +
                "  ORDER BY    Degree DESC) AS Edu ON EMP.Sn = Edu.EmpSn" +
                "  WHERE (EMP.Sn = '" + empSn + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            //生日轉換回年紀
            if (dt.Rows.Count > 0)
                if (dt.Rows[0][10] != null)
                    strBirthDay = dt.Rows[0][10].ToString();
            //if (!String.IsNullOrEmpty(strBirthDay))
            //{
            //    DateTime nowdate = DateTime.Now;
            //    int intYear = Int32.Parse(strBirthDay.Substring(0, 3));
            //    int intMonth = Int32.Parse(strBirthDay.Substring(3, 2));
            //    int nowYear = nowdate.Year - 1911;
            //    int nowMonth = nowdate.Month - 1;
            //    if (nowMonth < intMonth)
            //    {
            //        nowYear--;
            //        nowMonth = nowMonth + 12;
            //    }
            //    diffYear = nowYear - intYear;
            //    diffMonth = nowMonth - intMonth;
            //    dt.Rows[0][10] = diffYear;
            //}
            return dt;
        }

        //取得詳細資料列 寄Email時
        public VApplyerData GetApplyDataForEmail(int intAppSn)
        {
            VApplyerData vApplyData = new VApplyerData();
            String strSql = "SELECT b.EmpSn, b.EmpIdno, a.AppSn, a.AppLawNumNo, b.EmpNameCN, d.KindName,c.unt_name_full, e.AttributeName, f.JobTitleName, g.JobAttrName, h.AuditProgressName, i.LawContent " +
            "FROM   ApplyAudit AS a INNER JOIN " +
            "EmployeeBase AS b ON a.EmpSn = b.EmpSn LEFT OUTER JOIN " +
            "[PAPOVA].[TmuHr].[dbo].[s90_unit] c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS LEFT OUTER JOIN " +
            "CAuditKind AS d ON d.KindNo = a.AppKindNo LEFT OUTER JOIN " +
            "CAuditAttribute AS e ON e.KindNo = a.AppKindNo AND e.AttributeNo = a.AppAttributeNo LEFT OUTER JOIN " +
            "CJobTitle AS f ON f.JobTitleNo = a.AppJobTitleNo LEFT OUTER JOIN " +
            "CJobType AS g ON g.JobAttrNo = a.AppJobTypeNo LEFT OUTER JOIN " +
            "AuditProgressStatus AS h ON h.AuditProgressNo = a.AppStage LEFT OUTER JOIN " +
            "CTeacherLaw AS i ON i.LawKindNo = a.AppKindNo and i.LawJobTitleNo = a.AppJobTitleNo and i.LawItemNo = a.AppLawNumNo " +
            "WHERE a.AppSn = " + intAppSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                vApplyData.EmpSn = Int32.Parse(dt.Rows[0][vApplyData.strEmpSn].ToString());
                vApplyData.AppSn = Int32.Parse(dt.Rows[0][vApplyData.strAppSn].ToString());
                vApplyData.EmpIdno = dt.Rows[0][vApplyData.strEmpIdno].ToString();
                vApplyData.AppLawNumNo = dt.Rows[0][vApplyData.strAppLawNumNo].ToString();
                vApplyData.EmpNameCN = dt.Rows[0][vApplyData.strEmpNameCN].ToString();
                vApplyData.KindName = dt.Rows[0][vApplyData.strKindName].ToString();
                vApplyData.UntNameFull = dt.Rows[0][vApplyData.strUntNameFull].ToString();
                vApplyData.AttributeName = dt.Rows[0][vApplyData.strAttributeName].ToString();
                vApplyData.JobTitleName = dt.Rows[0][vApplyData.strJobTitleName].ToString();
                vApplyData.JobAttrName = dt.Rows[0][vApplyData.strJobAttrName].ToString();
                vApplyData.AuditProgressName = dt.Rows[0][vApplyData.strAuditProgressName].ToString();
                vApplyData.LawContent = dt.Rows[0][vApplyData.strLawContent].ToString();
                return vApplyData;
            }
            else
            {
                return null;
            }

        }
        //撈取指定單位的中文名稱
        public String GetDeptUntName(string untId)
        {
            string untName = "";
            //String strSql = "SELECT [unt_name] FROM [Dept_E] WHERE unt_id = '" + untId + "'";
            String strSql = "SELECT [unt_name_full] FROM [s91_unit] WHERE unt_id = '" + untId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset

            if (dt.Rows.Count > 0)
            {
                untName = dt.Rows[0][0].ToString();
            }
            return untName;
        }

        //撈取兼職工作
        public DataTable GetOtherJob(string IdNo)
        {

            String strSql = "SELECT oth_idno, oth_untid, oth_titid, oth_bdate, oth_edate, oth_status, oth_titlename FROM s10_otherpos  WHERE (oth_idno = '" + IdNo + "') AND (oth_edate = '')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取指定職務名稱
        public String GetTitleName(string titId)
        {
            string titName = "";
            String strSql = "SELECT tit_name FROM s90_titlecode where tit_id = '" + titId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset

            if (dt.Rows.Count > 0)
            {
                titName = dt.Rows[0][0].ToString();
            }
            return titName;
        }

        //撈取是否是主管&所屬部門與級別
        public DataTable GetAllSupUntLevel(string supId)
        {
            String strSql = "SELECT distinct a.unt_id + '-' + a.unt_levelb AS unt_data, a.unt_name_full FROM sup_dept AS a LEFT OUTER JOIN s90_unit AS b ON a.unt_id = b.unt_id WHERE  (a.emp_id = '" + supId + "') AND (b.unt_attr = 'E') and a.unt_id not in('E1500') ";
            //20171212 臨床技能中心已不存在，經曾副及怡慧確認特排除E1500
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetAllUntLevel(string empId)
        {
            String strSql = "SELECT emp_untid FROM s10_employee WHERE emp_id = '" + empId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得主管的Title
        public String GetTitle(string empId)
        {
            String strSql = "SELECT tit_name FROM [sup_dept]  where emp_id = '" + empId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }

        }

        //主管所屬部門下的子部門(主管為一級主管)
        public DataTable GetAllUnderUntFirstLevel(string untId)
        {
            String strSql = "SELECT [unt_id] FROM [Dept_E] WHERE fir_id = '" + untId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //主管所屬部門下的子部門(主管為二級主管)
        public DataTable GetAllUnderUntSecondLevel(string untId)
        {
            String strSql = "SELECT [unt_id] FROM [Dept_E] WHERE sed_id = '" + untId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //主管所屬部門下的子部門(主管為三級主管)

        //申請中的文件
        public DataTable GetAllInApplyDataBySearch(string strWhere)
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full as 'UntName', a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay , b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell ,b.EmpIdno, x.AuditRecord, b.EmpSn, a.AppSn, a.AppStep, a.AppStatus, ISNULL(a.AppBuildDate,'') AppBuildDate, ISNULL(a.AppModifyDate,'') AppModifyDate  ,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName'    " +
                            " FROM   ApplyAudit a  JOIN [EmployeeBase] b ON a.EmpSn = b.EmpSn " +
                            " LEFT JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.unt_id2 = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.dpt_id = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            //" where 1=1 " +
                            strWhere +
                            " order by c.unt_name_full,AppStatus ";

            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取申請人ApplyerList狀態表--All for測試
        public DataTable GetAllAuditData()
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full as 'UntName', a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay, b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell ,b.EmpIdno, x.AuditRecord, b.EmpSn, a.AppSn, a.AppStep, a.AppStatus , ISNULL(a.AppModifyDate,'') AppModifyDate ,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName'   " +
                            " FROM   ApplyAudit a  JOIN [EmployeeBase] b ON a.EmpSn = b.EmpSn " +
                            " LEFT JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.unt_id2 = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.dpt_id = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            " Where a.AppStatus = 'True'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetAllAuditDataBySearch(string strWhere)
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full as 'UntName', a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay , b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell ,b.EmpIdno,x.AuditRecord, b.EmpSn, a.AppSn, a.AppStep, a.AppStatus, ISNULL(a.AppBuildDate,'') AppBuildDate, ISNULL(a.AppModifyDate,'') AppModifyDate ,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName'   " +
                            " FROM   ApplyAudit a  JOIN [EmployeeBase] b ON a.EmpSn = b.EmpSn " +
                            " LEFT JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.unt_id2 = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.dpt_id = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            strWhere +
                            " order by  c.unt_name_full ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetApplyDataByAuditor(string empId, string strYear, string strSemester)
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full as 'UntName', a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay , b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell, b.EmpIdno, x.AuditRecord, b.EmpSn , a.AppSn, a.AppStep, a.AppStatus, ISNULL(a.AppBuildDate,'') AppBuildDate, ISNULL(a.AppModifyDate,'') AppModifyDate ,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName'   " +
                            " FROM ApplyAudit AS a " +
                            " JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn " +
                            " LEFT JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.unt_id2 = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.dpt_id = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            " JOIN AuditExecute AS d ON d.AppSn = a.AppSn " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            " WHERE d.ExecuteAuditorSnEmpId = '" + empId + "' and ExecuteAccept = 'False' and a.AppYear = '" + strYear + "' and a.AppSemester = '" + strSemester + "'" +
                            " ORDER BY c.unt_name_full ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取本學年度，申請單總筆數
        public int GetApplyListCntByIdno(string empIdno)
        {
            int intCnt = 0;
            String strSql = "SELECT COUNT(*) AS cnt FROM ApplyAudit WHERE EmpIdno = '" + empIdno + "' and AppYear = '" + getSettings.NowYear + "' and AppSemester = '" + getSettings.NowSemester + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intCnt = Int32.Parse(dt.Rows[0][0].ToString());
            }
            return intCnt;
        }

        //撈取本學年度學期特定申請類型，申請單總筆數
        public int GetApplyListCntByIdnoWithApply(string empIdno, string kindno)
        {
            int intCnt = 0;
            String strSql = "SELECT COUNT(*) AS cnt FROM ApplyAudit WHERE EmpIdno = '" + empIdno + "' and AppYear = '" + appYear + "' and AppSemester = '" + appSemester + "' and AppKindNo='" + kindno + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                intCnt = Int32.Parse(dt.Rows[0][0].ToString());
            }
            return intCnt;
        }


        //撈取自己本學年度申請多單
        //SELECT b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay, b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell, b.EmpIdno, b.EmpSn , a.AppSn,a.AppStep FROM ApplyAudit AS a INNER JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn INNER JOIN P_HRDB.TMUHR.dbo.s90_unit AS c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS
        public DataTable GetApplyListByIdno(string empIdno, String kindno)
        {
            //String strSql = "SELECT b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, " +
            //                "b.EmpBirthDay, b.EmpEmail, b.EmpTelPri +' '+ b.EmpTelPub +' '+ b.EmpCell, b.EmpIdno, b.EmpSn , a.AppSn, a.AppStep,a.AppStatus " +
            //                "FROM ApplyAudit AS a INNER JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn INNER JOIN " +
            //                "P_HRDB.TMUHR.dbo.s90_unit AS c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS "+
            //                "WHERE a.EmpIdno = '" + empIdno + "' and a.AppYear = '"+ appYear +"' and a.AppSemester = '"+ appSemester +"'";

            String strSql = "SELECT b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, CASE WHEN a.AppUnitNo IS NULL OR " +
                            "a.AppUnitNo = '' THEN ' ' ELSE c.unt_name_full END AS unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, " +
                            "a.AppStage, b.EmpBirthDay, b.EmpEmail, b.EmpTelPri + ' ' + b.EmpTelPub + ' ' + b.EmpCell AS Expr1, b.EmpIdno, " +
                            "b.EmpSn, a.AppSn, a.AppStep, a.AppStatus, a.AppBuildDate FROM ApplyAudit AS a LEFT OUTER JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn LEFT OUTER JOIN " +
                            "PAPOVA.TmuHr.dbo.s90_unit AS c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            "WHERE a.EmpIdno = '" + empIdno + "' AND a.AppKindNo = '" + kindno + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取申請人ApplyerList狀態表--by Auditor 校內審核--輪到的
        public DataTable GetApplyDataByAuditorNow(string empId, string strYear, string strSemester)
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full as 'UntName', a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage,  b.EmpBirthDay , b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell,b.EmpIdno, x.AuditRecord, b.EmpSn, a.AppSn, a.AppStep, a.AppStatus, ISNULL(a.AppBuildDate,'') AppBuildDate, ISNULL(a.AppModifyDate,'') AppModifyDate,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName' " +
                            "FROM ApplyAudit AS a " +
                            "INNER JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn " +
                            "INNER JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.dpt_id = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.unt_id2 = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS  " +
                            "INNER JOIN AuditExecute AS d ON d.AppSn = a.AppSn and d.ExecuteStage = a.AppStage and d.ExecuteStep = a.AppStep " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            "WHERE d.ExecuteAuditorSnEmpId = '" + empId.Trim() + "' and ExecuteAccept = 'False' and a.AppYear = '" + strYear + "' and a.AppSemester = '" + strSemester + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取申請人ApplyerList狀態表--by Unit 校內審核--主管階層
        public DataTable GetApplyDataByUnit(string unit, string strYear, string strSemester)
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay, b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell,b.EmpIdno, x.AuditRecord, b.EmpSn, a.AppSn, a.AppStep, a.AppStatus, a.AppUnitNo,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName' " +
                            "FROM ApplyAudit AS a " +
                            "INNER JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn " +
                            "INNER JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.dpt_id = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.unt_id2 = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS  " +
                            //"INNER JOIN AppendEmployee AS d ON d.AppSn = a.AppSn " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            "WHERE a.AppUnitNo in (" + unit + ")   and a.AppYear = '" + strYear + "' and a.AppSemester = '" + strSemester + "' order by a.AppUnitNo";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //撈取申請人ApplyerList狀態表--by Auditor 外審
        public DataTable GetApplyDataByOtherAuditor(string empId)
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, x.AuditRecord, b.EmpSn, d.ExecuteAccept , a.AppSn, a.AppStep, a.AppStatus,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName'  " +
                            "FROM ApplyAudit AS a " +
                            "INNER JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn " +
                            "INNER JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.dpt_id = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.unt_id2 = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS  " +
                            "INNER JOIN AuditExecute AS d ON d.AppSn = a.AppSn " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM              AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            "WHERE d.ExecuteAuditorSnEmpId = '" + empId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取是否外審資料設定完成的 
        public Boolean IsAuditExecuteOtherAuditorFilled(int intAppSn)
        {
            Boolean flag = false;
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpSn, d.ExecuteAccept, a.AppSn, a.AppStep, a.AppStatus  " +
                            "FROM ApplyAudit AS a " +
                            "INNER JOIN EmployeeBase AS b ON a.EmpSn = b.EmpSn " +
                            "INNER JOIN [PAPOVA].[TmuHr].[dbo].[s90_unit]  AS c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            "INNER JOIN AuditExecute AS d ON d.AppSn = a.AppSn " +
                            "WHERE a.AppSn = " + intAppSn + " AND (ExecuteAuditorSnEmpId IS NULL)";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
            return flag;
        }


        //退回學院補件中
        public DataTable GetAllReturnData()
        {
            String strSql = "SELECT b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay, b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell,b.EmpIdno, x.AuditRecord, b.EmpSn, a.AppSn, a.AppStep, a.AppStatus  ,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName' " +
                            " FROM   ApplyAudit a  JOIN [EmployeeBase] b ON a.EmpSn = b.EmpSn " +
                            "INNER JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.dpt_id = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.unt_id2 = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS  " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            " Where a.AppStatus = 'False' and a.AppStage != '0'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetAllReturnDataBySearch(string strWhere)
        {
            String strSql = "SELECT distinct b.EmpNameCN, a.AppKindNo, a.AppAttributeNo, c.unt_name_full, a.AppJobTitleNo, a.AppJobTypeNo, a.AppStage, b.EmpBirthDay, b.EmpEmail, b.EmpTelPri, b.EmpTelPub, b.EmpCell, b.EmpIdno, x.AuditRecord, b.EmpSn, a.AppSn, a.AppStep, a.AppStatus, ISNULL(a.AppBuildDate,'') AppBuildDate, ISNULL(a.AppModifyDate,'') AppModifyDate,c.DeptName,ISNULL(c.CollegeName,c.DeptName) as 'CollegeName'   " +
                            " FROM   ApplyAudit a  JOIN [EmployeeBase] b ON a.EmpSn = b.EmpSn " +
                            " INNER JOIN (select unt.unt_id,unt.unt_name_full,dpt.unt_name_full as 'DeptName',unt2.unt_name_full  as CollegeName  from [PAPOVA].[TmuHr].[dbo].[s90_unit] unt LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] dpt on unt.dpt_id = dpt.unt_id LEFT JOIN[PAPOVA].[TmuHr].[dbo].[s90_unit] unt2 on unt.unt_id2 = unt2.unt_id) c ON a.AppUnitNo = c.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS  " +
                            " LEFT OUTER JOIN " +
                            " (SELECT DISTINCT AppSn," +
                            "         (SELECT ExecuteAuditorName + isnull(RIGHT('0' + CONVERT(VarChar(3), YEAR(ExecuteDate) - 1911), 3) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), MONTH(ExecuteDate)), 2) " +
                            "                  + '/' + RIGHT('0' + CONVERT(VarChar(2), Day(ExecuteDate)), 2), '') + isnull(ExecuteCommentsA, '') + '，'" +
                            "          FROM               AuditExecute AS s2 WHERE s1.AppSn = s2.AppSn FOR XML PATH('')) AS AuditRecord " +
                            "          FROM              AuditExecute AS s1 ) AS x ON a.AppSn=x.AppSn " +
                            strWhere +
                            " order by  c.unt_name_full ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }



        //撈取所有申請類別 AuditKind
        public DataTable GetAllAuditKindName()
        {
            String strSql = "SELECT KindName  FROM CAuditKind";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取所有途徑 AuditWay 
        public DataTable GetAllAuditWayName()
        {
            String strSql = "SELECT WayNo, WayName ,WayNameE  FROM CAuditWay";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取指定途徑名稱
        public DataTable GetAuditWayName(string wayNo)
        {
            String strSql = "SELECT WayNo, WayName ,WayNameE  FROM CAuditWay WHERE WayNo = '" + wayNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetAllAuditKindNoName()
        {
            String strSql = "SELECT KindNo, KindName  FROM CAuditKind";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取所有申請類型
        public DataTable GetAllAuditAttributeNameByKindNo(int KindNo)
        {
            String strSql = "SELECT AttributeName, AttributeNameE FROM CAuditAttribute where KindNo = '" + KindNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取指定類別的所有申請類型
        public DataTable GetAuditAttributeByKindNo(int KindNo)
        {
            String strSql = "SELECT AttributeNo, AttributeName, AttributeNameE FROM CAuditAttribute where KindNo = '" + KindNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取指定類別的所有申請類型
        public DataTable GetAuditAttributeName(string KindNo, string AttributeNo)
        {
            String strSql = "SELECT AttributeNo, AttributeName, AttributeNameE FROM CAuditAttribute where KindNo = '" + KindNo + "' and AttributeNo = '" + AttributeNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取所有系所
        public DataTable GetAllDept()
        {
            String strSql = "SELECT unt_id, unt_name FROM Dept_E WHERE (NOT (unt_id LIKE '%0000'))'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //職稱 送審等級: 講師 助理教授 副教授 教授
        public DataTable GetAllJobTitleName()
        {
            String strSql = "SELECT JobTitleName FROM CJobTitle";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetAllJobTitleNoName()
        {
            String strSql = "SELECT JobTitleNo, JobTitleName FROM CJobTitle";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //職別:專任 兼任 代理
        public DataTable GetAllJobTypeName()
        {
            String strSql = "SELECT JobAttrName FROM CJobType";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetAllJobTypeNoName()
        {
            String strSql = "SELECT JobAttrNo,JobAttrName FROM CJobType";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取所有Degree
        public DataTable GetAllDegreeName()
        {
            String strSql = "SELECT DegreeName FROM CDegree";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取所有DegreeType
        public DataTable GetAllDegreeTypeName()
        {
            String strSql = "SELECT DegreeTypeName FROM CDegreeType";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //簽核狀態
        public DataTable GetAllAuditProcgressName()
        {
            String strSql = "SELECT AuditProgressName FROM AuditProgressStatus";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public DataTable GetAllAuditProcgressNoName()
        {
            String strSql = "SELECT AuditProgressNo,AuditProgressName FROM AuditProgressStatus";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取所有的的簽核流程
        //SELECT   distinct a.ExecuteSn, a.ExecuteStage,a.ExecuteStep, a.ExecuteRoleName, b.emp_name, a.ExecuteAuditorEmail, a.ExecutePassword, a.ExecuteAccept, a.ExecuteDate, a.ExecuteBngDate, 
        //          a.ExecuteEndDate, a.ExecutePass, a.ExecuteStatus
        //FROM       [tmu].[dbo].[AuditExecuteTmp] AS a full outer JOIN
        //         [PAPOVA].[TmuHr].[dbo].[emp] AS b ON a.ExecuteAuditorSnEmpId = b.emp_id COLLATE Chinese_Taiwan_Stroke_CI_AS 
        //         where a.AppSn =1
        //         order by a.ExecuteSn 


        public DataTable GetAllAuditExecuteByEmpSn(int appSn)
        {

            String strSql = "SELECT ExecuteSn, ExecuteStage,ExecuteStep, ExecuteRoleName, ExecuteAuditorSnEmpId, ExecuteAuditorName, ExecuteAuditorEmail, ExecuteAccept, isnull(Right('0' + Convert(VarChar(3),YEAR(ExecuteDate)-1911),3)+'/'+Right('0' + Convert(VarChar(2),MONTH(ExecuteDate)),2)+'/'+Right('0' + Convert(VarChar(2),Day(ExecuteDate)),2),'') AS EDate, ExecuteBngDate, " +
                            " ExecuteEndDate, ExecutePass, ExecuteStatus, isnull(ExecuteCommentsA,'') AS EComments FROM [AuditExecute] WHERE AppSn = " + appSn + " order by ExecuteStage,ExecuteStep ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            //0.ExecuteSn, v
            //1.ExecuteStage, v v
            //2.ExecuteStep, v
            //3.ExecuteRoleName, v
            //4.ExecuteAuditorSnEmpId, v
            //5.emp_name, v
            //6.ExecuteAuditorEmail,v
            //7.ExecuteAccept,
            //8.ExecuteDate,
            //9.ExecuteBngDate, " + v
            //10.ExecuteEndDate, v
            //11.ExecutePass,
            //12.ExecuteStatus v   

            return dt;
        }

        //撈取指定的承辦人員資料(系所承辦)
        public VAuditExecute GetAuditExecuteByAppSn(int appSn)
        {
            String strSql = "SELECT * FROM [AuditExecute] WHERE AppSn = " + appSn + " and ExecuteStage = '2' and  ExecuteStep = '1' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.ExecuteSn = Int32.Parse(dt.Rows[0]["ExecuteSn"].ToString());
                vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                vAuditExecute.ExecuteAuditorName = dt.Rows[0]["ExecuteAuditorName"].ToString();
                vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteCommentsA = dt.Rows[0]["ExecuteCommentsA"].ToString();
                vAuditExecute.ExecuteCommentsB = dt.Rows[0]["ExecuteCommentsB"].ToString();
                vAuditExecute.ExecuteStrengths = dt.Rows[0]["ExecuteStrengths"].ToString();
                vAuditExecute.ExecuteWeaknesses = dt.Rows[0]["ExecuteWeaknesses"].ToString();
                vAuditExecute.ExecuteAllTotalScore = dt.Rows[0]["ExecuteTotalScore"].ToString();
                vAuditExecute.ExecuteLevelScore = dt.Rows[0]["ExecuteLevelScore"].ToString();
                vAuditExecute.ExecuteWSSubject = dt.Rows[0]["ExecuteWSSubject"].ToString();
                vAuditExecute.ExecuteWSMethod = dt.Rows[0]["ExecuteWSMethod"].ToString();
                vAuditExecute.ExecuteWSContribute = dt.Rows[0]["ExecuteWSContribute"].ToString();
                vAuditExecute.ExecuteWSAchievement = dt.Rows[0]["ExecuteWSAchievement"].ToString();
                vAuditExecute.ExecuteWTotalScore = dt.Rows[0]["ExecuteWTotalScore"].ToString();
                vAuditExecute.ExecuteBngDate = DateTime.ParseExact(dt.Rows[0]["ExecuteBngDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecuteEndDate = DateTime.ParseExact(dt.Rows[0]["ExecuteEndDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecutePass = dt.Rows[0]["ExecutePass"].ToString(); //1:通過 0:未通過 //3:退回(實際上是卡關,並不退回)
                vAuditExecute.ExecuteReturnReason = dt.Rows[0]["ExecuteReturnReason"].ToString(); //退回原因
                return vAuditExecute;
            }
            else
            {
                return null;
            }
        }

        public Boolean IsAuditExecuteReturn(int appSn)
        {

            String strSql = "SELECT ExecuteSn, ExecuteStage,ExecuteStep, ExecuteRoleName, ExecuteAuditorSnEmpId, ExecuteAuditorName, ExecuteAuditorEmail, ExecuteAccept, ExecuteDate, ExecuteBngDate, " +
                            " ExecuteEndDate, ExecutePass, ExecuteStatus FROM [AuditExecute] WHERE AppSn = " + appSn + " and ExecutePass = '3'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (hasData(ds))
            {
                return true;
            }
            return false;
        }

        //撈取審核步驟  顯示目前總共有哪些Stage 顯示在最上方
        public DataTable GetAllAuditStageByEmpSn(int appSn)
        {
            String strSql = "SELECT DISTINCT ExecuteStage, ExecuteStep,ExecuteRoleName, ExecutePass,ExecuteReturnReason,ExecuteCommentsA FROM AuditExecute WHERE AppSn = " + appSn + " ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取指定申請者,指定簽核目前的那筆審核資料,判斷狀態,多筆時抓第一筆
        //SELECT *  FROM [AuditExecute] a JOIN [ApplyAudit] b  ON  a.AppSn = b.AppSn
        //WHERE a.AppSn =1 AND a.ExecuteStatus = 'false' AND a.[ExecuteStage] = b.[AppStage] AND a.[ExecuteStep] = b.[AppStep] 
        //order by a.[ExecuteStage], a.[ExecuteStep]
        public VAuditExecute GetExecuteAuditorData(VAuditExecute obj)
        {

            String strSql = "SELECT *  FROM [AuditExecute] a JOIN [ApplyAudit] b  ON  a.AppSn = b.AppSn " +
                            "WHERE  a.AppSn = '" + obj.AppSn + "' and a.[ExecuteAuditorSnEmpId] = '" + obj.ExecuteAuditorSnEmpId.Trim() + "' and a.ExecuteStatus = '" + obj.ExecuteStatus + "' " +
                            "AND a.[ExecuteStage] = b.[AppStage] AND a.[ExecuteStep] = b.[AppStep] order by [ExecuteStage], [ExecuteStep]";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                vAuditExecute.ExecuteSn = Int32.Parse(dt.Rows[0]["ExecuteSn"].ToString());
                vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteCommentsA = dt.Rows[0]["ExecuteCommentsA"].ToString();
                vAuditExecute.ExecuteCommentsB = dt.Rows[0]["ExecuteCommentsB"].ToString();
                vAuditExecute.ExecuteStrengths = dt.Rows[0]["ExecuteStrengths"].ToString();
                vAuditExecute.ExecuteWeaknesses = dt.Rows[0]["ExecuteWeaknesses"].ToString();
                vAuditExecute.ExecuteAllTotalScore = dt.Rows[0]["ExecuteTotalScore"].ToString();
                vAuditExecute.ExecuteLevelScore = dt.Rows[0]["ExecuteLevelScore"].ToString();
                vAuditExecute.ExecuteWSSubject = dt.Rows[0]["ExecuteWSSubject"].ToString();
                vAuditExecute.ExecuteWSMethod = dt.Rows[0]["ExecuteWSMethod"].ToString();
                vAuditExecute.ExecuteWSContribute = dt.Rows[0]["ExecuteWSContribute"].ToString();
                vAuditExecute.ExecuteWSAchievement = dt.Rows[0]["ExecuteWSAchievement"].ToString();
                vAuditExecute.ExecuteWTotalScore = dt.Rows[0]["ExecuteWTotalScore"].ToString();
                vAuditExecute.ExecuteBngDate = DateTime.ParseExact(dt.Rows[0]["ExecuteBngDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecuteEndDate = DateTime.ParseExact(dt.Rows[0]["ExecuteEndDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecutePass = dt.Rows[0]["ExecutePass"].ToString(); //1:通過 0:未通過 //3:退回(實際上是卡關,並不退回)
                vAuditExecute.ExecuteReturnReason = dt.Rows[0]["ExecuteReturnReason"].ToString(); //退回原因
                return vAuditExecute;
            }
            else
            {
                return null;
            }
        }
        public VAuditExecute GetExecuteAuditorDataHR(VAuditExecute obj)
        {

            String strSql = "SELECT *  FROM [AuditExecute] a JOIN [ApplyAudit] b  ON  a.AppSn = b.AppSn " +
                            "WHERE  a.AppSn = " + obj.AppSn + " and a.[ExecuteAuditorSnEmpId] = '" + obj.ExecuteAuditorSnEmpId + "' " +
                            "order by [ExecuteStage], [ExecuteStep]";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                vAuditExecute.ExecuteSn = Int32.Parse(dt.Rows[0]["ExecuteSn"].ToString());
                vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteCommentsA = dt.Rows[0]["ExecuteCommentsA"].ToString();
                vAuditExecute.ExecuteCommentsB = dt.Rows[0]["ExecuteCommentsB"].ToString();
                vAuditExecute.ExecuteStrengths = dt.Rows[0]["ExecuteStrengths"].ToString();
                vAuditExecute.ExecuteWeaknesses = dt.Rows[0]["ExecuteWeaknesses"].ToString();
                vAuditExecute.ExecuteAllTotalScore = dt.Rows[0]["ExecuteTotalScore"].ToString();
                vAuditExecute.ExecuteLevelScore = dt.Rows[0]["ExecuteLevelScore"].ToString();
                vAuditExecute.ExecuteWSSubject = dt.Rows[0]["ExecuteWSSubject"].ToString();
                vAuditExecute.ExecuteWSMethod = dt.Rows[0]["ExecuteWSMethod"].ToString();
                vAuditExecute.ExecuteWSContribute = dt.Rows[0]["ExecuteWSContribute"].ToString();
                vAuditExecute.ExecuteWSAchievement = dt.Rows[0]["ExecuteWSAchievement"].ToString();
                vAuditExecute.ExecuteWTotalScore = dt.Rows[0]["ExecuteWTotalScore"].ToString();
                vAuditExecute.ExecuteBngDate = DateTime.ParseExact(dt.Rows[0]["ExecuteBngDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecuteEndDate = DateTime.ParseExact(dt.Rows[0]["ExecuteEndDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecutePass = dt.Rows[0]["ExecutePass"].ToString(); //1:通過 0:未通過 //3:退回(實際上是卡關,並不退回)
                vAuditExecute.ExecuteReturnReason = dt.Rows[0]["ExecuteReturnReason"].ToString(); //退回原因
                return vAuditExecute;
            }
            else
            {
                return null;
            }
        }

        public VAuditExecute GetExecuteAuditorDataViewOnly(VAuditExecute obj)
        {

            String strSql = "SELECT *  FROM [AuditExecute]  " +
                            "WHERE  AppSn = " + obj.AppSn + " and [ExecuteAuditorSnEmpId] = '" + obj.ExecuteAuditorSnEmpId + "' " +
                            "order by [ExecuteStage], [ExecuteStep] desc";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                vAuditExecute.ExecuteSn = Int32.Parse(dt.Rows[0]["ExecuteSn"].ToString());
                vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteCommentsA = dt.Rows[0]["ExecuteCommentsA"].ToString();
                vAuditExecute.ExecuteCommentsB = dt.Rows[0]["ExecuteCommentsB"].ToString();
                vAuditExecute.ExecuteStrengths = dt.Rows[0]["ExecuteStrengths"].ToString();
                vAuditExecute.ExecuteWeaknesses = dt.Rows[0]["ExecuteWeaknesses"].ToString();
                vAuditExecute.ExecuteAllTotalScore = dt.Rows[0]["ExecuteTotalScore"].ToString();
                vAuditExecute.ExecuteLevelScore = dt.Rows[0]["ExecuteLevelScore"].ToString();
                vAuditExecute.ExecuteWSSubject = dt.Rows[0]["ExecuteWSSubject"].ToString();
                vAuditExecute.ExecuteWSMethod = dt.Rows[0]["ExecuteWSMethod"].ToString();
                vAuditExecute.ExecuteWSContribute = dt.Rows[0]["ExecuteWSContribute"].ToString();
                vAuditExecute.ExecuteWSAchievement = dt.Rows[0]["ExecuteWSAchievement"].ToString();
                vAuditExecute.ExecuteWTotalScore = dt.Rows[0]["ExecuteWTotalScore"].ToString();
                vAuditExecute.ExecuteBngDate = DateTime.ParseExact(dt.Rows[0]["ExecuteBngDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecuteEndDate = DateTime.ParseExact(dt.Rows[0]["ExecuteEndDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecutePass = dt.Rows[0]["ExecutePass"].ToString(); //1:通過 0:未通過 //3:退回(實際上是卡關,並不退回)
                vAuditExecute.ExecuteReturnReason = dt.Rows[0]["ExecuteReturnReason"].ToString(); //退回原因
                return vAuditExecute;
            }
            else
            {
                return null;
            }
        }


        public VAuditExecute GetExecuteReturnSelf(int intAppSn)
        {

            String strSql = "SELECT *  FROM [AuditExecute]  " +
                            "WHERE  AppSn = " + intAppSn + " and ExecuteStage = '2' and ExecuteStep = '3'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                vAuditExecute.ExecuteSn = Int32.Parse(dt.Rows[0]["ExecuteSn"].ToString());
                vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteCommentsA = dt.Rows[0]["ExecuteCommentsA"].ToString();
                vAuditExecute.ExecuteCommentsB = dt.Rows[0]["ExecuteCommentsB"].ToString();
                vAuditExecute.ExecuteStrengths = dt.Rows[0]["ExecuteStrengths"].ToString();
                vAuditExecute.ExecuteWeaknesses = dt.Rows[0]["ExecuteWeaknesses"].ToString();
                vAuditExecute.ExecuteAllTotalScore = dt.Rows[0]["ExecuteTotalScore"].ToString();
                vAuditExecute.ExecuteLevelScore = dt.Rows[0]["ExecuteLevelScore"].ToString();
                vAuditExecute.ExecuteWSSubject = dt.Rows[0]["ExecuteWSSubject"].ToString();
                vAuditExecute.ExecuteWSMethod = dt.Rows[0]["ExecuteWSMethod"].ToString();
                vAuditExecute.ExecuteWSContribute = dt.Rows[0]["ExecuteWSContribute"].ToString();
                vAuditExecute.ExecuteWSAchievement = dt.Rows[0]["ExecuteWSAchievement"].ToString();
                vAuditExecute.ExecuteWTotalScore = dt.Rows[0]["ExecuteWTotalScore"].ToString();
                vAuditExecute.ExecuteBngDate = DateTime.ParseExact(dt.Rows[0]["ExecuteBngDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecuteEndDate = DateTime.ParseExact(dt.Rows[0]["ExecuteEndDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecutePass = dt.Rows[0]["ExecutePass"].ToString(); //1:通過 0:未通過 //3:退回(實際上是卡關,並不退回)
                vAuditExecute.ExecuteReturnReason = dt.Rows[0]["ExecuteReturnReason"].ToString(); //退回原因
                return vAuditExecute;
            }
            else
            {
                return null;
            }
        }

        //撈取現在審核的人
        public String GetExecuteRoleName(int intAppSn)
        {
            String executeRoleName = "";
            String strSql = "SELECT AppSn,ExecuteRoleName  FROM [AuditExecute]  " +
                            "WHERE  AppSn = " + intAppSn + " and [ExecuteStatus] = 'false' " +
                            "order by [ExecuteStage], [ExecuteStep] ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                executeRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
            }
            return executeRoleName;
        }

        //撈取指定申請者,多筆外審資料
        public VAuditExecute GetExecuteAuditorDataOuterFirstOne(VAuditExecute obj)
        {

            String strSql = " SELECT a.*, c.ARTitleName " +
                            " FROM [AuditExecute] a INNER JOIN [ApplyAudit] b  ON  a.AppSn = b.AppSn " +
                            " INNER JOIN AuditReportCompose AS c ON a.ExecuteStage = c.ARStage AND b.AppKindNo = c.ARKindNo AND b.AppAttributeNo = c.ARAttributeNo " +
                            " WHERE  a.AppSn = " + obj.AppSn + " and a.ExecuteStatus = '" + obj.ExecuteStatus + "' " +
                            " AND ( a.[ExecuteStage] = '6' )  and [ExecuteStep] = '1'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.ExecuteSn = Int32.Parse(dt.Rows[0]["ExecuteSn"].ToString());
                vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteCommentsA = dt.Rows[0]["ExecuteCommentsA"].ToString();
                vAuditExecute.ExecuteCommentsB = dt.Rows[0]["ExecuteCommentsB"].ToString();
                vAuditExecute.ExecuteStrengths = dt.Rows[0]["ExecuteStrengths"].ToString();
                vAuditExecute.ExecuteWeaknesses = dt.Rows[0]["ExecuteWeaknesses"].ToString();
                vAuditExecute.ExecuteAllTotalScore = dt.Rows[0]["ExecuteTotalScore"].ToString();
                vAuditExecute.ExecuteLevelScore = dt.Rows[0]["ExecuteLevelScore"].ToString();
                vAuditExecute.ExecuteWSSubject = dt.Rows[0]["ExecuteWSSubject"].ToString();
                vAuditExecute.ExecuteWSMethod = dt.Rows[0]["ExecuteWSMethod"].ToString();
                vAuditExecute.ExecuteWSContribute = dt.Rows[0]["ExecuteWSContribute"].ToString();
                vAuditExecute.ExecuteWSAchievement = dt.Rows[0]["ExecuteWSAchievement"].ToString();
                vAuditExecute.ExecuteWTotalScore = dt.Rows[0]["ExecuteWTotalScore"].ToString();
                vAuditExecute.ExecuteBngDate = DateTime.ParseExact(dt.Rows[0]["ExecuteBngDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecuteEndDate = DateTime.ParseExact(dt.Rows[0]["ExecuteEndDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                vAuditExecute.ExecutePass = dt.Rows[0]["ExecutePass"].ToString(); //1:通過 0:未通過 //9:退回(實際上是卡關,並不退回)
                return vAuditExecute;
            }
            else
            {
                return null;
            }
        }

        //撈取指定申請者,多筆外審資料
        public ArrayList GetExecuteAuditorDataOuter(VAuditExecute obj)
        {
            String strSql = " SELECT a.*, c.ARTitleName " +
                " FROM [AuditExecute] a INNER JOIN [ApplyAudit] b  ON  a.AppSn = b.AppSn " +
                " INNER JOIN AuditReportCompose AS c ON a.ExecuteStage = c.ARStage AND b.AppKindNo = c.ARKindNo AND b.AppAttributeNo = c.ARAttributeNo " +
                " WHERE  a.AppSn = " + obj.AppSn + " and a.ExecuteStatus = '" + obj.ExecuteStatus + "' " +
                " AND  a.[ExecuteStage] = '6'  and [ExecuteStep] = '1'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset    
            VAuditExecute vAuditExecute = new VAuditExecute();
            ArrayList arrayList = new ArrayList();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    vAuditExecute = new VAuditExecute();
                    vAuditExecute.AppSn = Int32.Parse(dt.Rows[0]["AppSn"].ToString());
                    vAuditExecute.ExecuteSn = Int32.Parse(dt.Rows[0]["ExecuteSn"].ToString());
                    vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                    vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                    vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                    vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                    vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                    vAuditExecute.ExecuteCommentsA = dt.Rows[0]["ExecuteCommentsA"].ToString();
                    vAuditExecute.ExecuteCommentsB = dt.Rows[0]["ExecuteCommentsB"].ToString();
                    vAuditExecute.ExecuteStrengths = dt.Rows[0]["ExecuteStrengths"].ToString();
                    vAuditExecute.ExecuteWeaknesses = dt.Rows[0]["ExecuteWeaknesses"].ToString();
                    vAuditExecute.ExecuteAllTotalScore = dt.Rows[0]["ExecuteAllTotalScore"].ToString();
                    vAuditExecute.ExecuteLevelScore = dt.Rows[0]["ExecuteLevelScore"].ToString();
                    vAuditExecute.ExecuteWSSubject = dt.Rows[0]["ExecuteWSSubject"].ToString();
                    vAuditExecute.ExecuteWSMethod = dt.Rows[0]["ExecuteWSMethod"].ToString();
                    vAuditExecute.ExecuteWSContribute = dt.Rows[0]["ExecuteWSContribute"].ToString();
                    vAuditExecute.ExecuteWSAchievement = dt.Rows[0]["ExecuteWSAchievement"].ToString();
                    vAuditExecute.ExecuteWTotalScore = dt.Rows[0]["ExecuteWTotalScore"].ToString();
                    vAuditExecute.ExecuteBngDate = DateTime.ParseExact(dt.Rows[0]["ExecuteBngDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                    vAuditExecute.ExecuteEndDate = DateTime.ParseExact(dt.Rows[0]["ExecuteEndDate"].ToString(), DateTimeList, null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
                    vAuditExecute.ExecutePass = dt.Rows[0]["ExecutePass"].ToString(); //1:通過 0:未通過 //9:退回(實際上是卡關,並不退回)
                    vAuditExecute.ARTitleName = dt.Rows[0]["ARTitleName"].ToString();
                    arrayList.Add(vAuditExecute);
                }
                return arrayList;
            }
            else
            {
                return null;
            }
        }

        //撈取指定申請者,目前未審核完單,抓下一審核者,多筆時抓第一筆,為產生Account與寄發Email
        public VAuditExecute GetExecuteAuditorNextOne(int appSn)
        {

            String strSql = "SELECT TOP(1) AppSn,ExecuteSn,ExecuteStage,ExecuteStep,ExecuteAuditorSnEmpId, ExecuteAuditorEmail,ExecuteRoleName,ExecuteAuditorName FROM [AuditExecute] WHERE  AppSn = " + appSn + " and ExecuteStatus = 'False' order by [ExecuteStage], [ExecuteStep]";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                vAuditExecute.AppSn = Convert.ToInt32(dt.Rows[0]["AppSn"].ToString());
                vAuditExecute.ExecuteSn = Convert.ToInt32(dt.Rows[0]["ExecuteSn"].ToString());
                vAuditExecute.ExecuteAuditorSnEmpId = dt.Rows[0]["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dt.Rows[0]["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteStage = dt.Rows[0]["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dt.Rows[0]["ExecuteStep"].ToString();
                vAuditExecute.ExecuteRoleName = dt.Rows[0]["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorName = dt.Rows[0]["ExecuteAuditorName"].ToString();
                return vAuditExecute;
            }
            else
            {
                return null;
            }
        }

        //判斷所有外審是否都審完，有資料表示尚未審完
        public ArrayList GetExecuteAuditorOutter(int appSn, string exeSage, string exeStep)
        {
            ArrayList arrayList = new ArrayList();
            String strSql = "SELECT AppSn,ExecuteStage,ExecuteStep,ExecuteAuditorSnEmpId, ExecuteAuditorEmail,ExecuteRoleName,ExecuteAuditorName FROM [AuditExecute] WHERE ExecuteStage = '" + exeSage + "' and ExecuteStep = '" + exeStep + "' and AppSn = " + appSn + " and ExecuteStatus = 'False' order by [ExecuteStage], [ExecuteStep]";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute;
            foreach (DataRow dataRow in dt.Rows)
            {
                vAuditExecute = new VAuditExecute();
                vAuditExecute.AppSn = Convert.ToInt32(dataRow["AppSn"].ToString());
                vAuditExecute.AppSn = Convert.ToInt32(dataRow["AppSn"].ToString());
                vAuditExecute.ExecuteAuditorSnEmpId = dataRow["ExecuteAuditorSnEmpId"].ToString();
                vAuditExecute.ExecuteAuditorEmail = dataRow["ExecuteAuditorEmail"].ToString();
                vAuditExecute.ExecuteStage = dataRow["ExecuteStage"].ToString();
                vAuditExecute.ExecuteStep = dataRow["ExecuteStep"].ToString();
                vAuditExecute.ExecuteRoleName = dataRow["ExecuteRoleName"].ToString();
                vAuditExecute.ExecuteAuditorName = dataRow["ExecuteAuditorName"].ToString();
                arrayList.Add(vAuditExecute);
            }
            return arrayList;
        }


        //撈取指定申請者,目前審核單,判斷是否已產生
        public Boolean GetExecuteAuditorAnyOne(int appSn)
        {

            String strSql = "SELECT TOP(1) AppSn FROM [AuditExecute] WHERE  AppSn = " + appSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VAuditExecute vAuditExecute = new VAuditExecute();
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //撈取一級部門的單位 , level, sup_id, sup_name, fir_id, fir_name 
        public DataTable GetDeptLevelOne()
        {
            //String strSql = "SELECT unt_id, unt_name FROM [Dept_A]  WHERE level='1'";
            String strSql = "SELECT DISTINCT unt_id, unt_name_full as unt_name  FROM [sup_dept]  WHERE [unt_levelb]='1' order by unt_id ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取一級部門下的二級部門
        public DataTable GetDeptLevelTwo(string leveloneUnit)
        {
            String strSql = "";
            ////String strSql = "SELECT unt_id, unt_name FROM [Dept_A]  WHERE level=2 and fir_id='" + leveloneUnit + "'";

            strSql = "SELECT DISTINCT unt_id, unt_name_full as unt_name FROM [sup_dept]  WHERE unt_levelb='2' and dpt_id='" + leveloneUnit + "' order by unt_id ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取一級部門下的三級部門 , unt_levelb, a.unt_empid,dpt_id
        public DataTable GetDeptLevelThree(string leveltwoUnit)
        {
            String strSql = "SELECT unt_id, unt_name_full FROM [s90_unit] WHERE [unt_id2] = '" + leveltwoUnit + "' order by unt_id";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取該部門的員工 SELECT emp_id,emp_name,emp_email,unit_name,emp_posid,pos_name,tit_name,emp_sex  FROM V_EMP WHERE emp_untid = 'E0104'
        public DataTable GetAllEmpByUnit(string unit)
        {
            String strSql = "";
            strSql = "SELECT emp_id, emp_name  FROM [V_EMP] WHERE emp_untid = '" + unit + "' order by emp_name ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取該部門的員工 SELECT emp_id,emp_name,emp_email,unit_name,emp_posid,pos_name,tit_name,emp_sex  FROM V_EMP WHERE emp_untid = 'E0104'
        public DataTable GetAllEmpByUnitBoss(string unit)
        {
            String strSql = "";
            strSql = "SELECT *  FROM [V_EMP] WHERE emp_untid='" + unit + "' order by emp_name ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取該部門的員工姓名,Email SELECT emp_id,emp_name,emp_email,unit_name,emp_posid,pos_name,tit_name,emp_sex  FROM V_EMP WHERE emp_untid = 'E0104'
        public DataTable GetEmpNameEmail(string empId)
        {
            String strSql = "SELECT emp_name, emp_email FROM [s10_employee] WHERE emp_id = '" + empId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        //管理者端 撈取該員工的部門
        public DataTable GetEmpUnitSelf(string empId)
        {
            String strSql = "SELECT a.emp_untid + '-0' AS unt_data, b.unt_name_full FROM s10_employee AS a LEFT OUTER JOIN  s90_unit AS b ON a.emp_untid = b.unt_id WHERE a.emp_id = '" + empId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //撈取該部門的員工院級資料
        public DataTable GetEmpUnitID(string empId)
        {
            String strSql = "SELECT distinct emp_untid+ '-1' AS unt_data, b.unt_name_full FROM [s10_employee] a LEFT OUTER JOIN  [s90_unit] AS b ON a.emp_untid = b.unt_id WHERE a.emp_id = '" + empId + "' and b.unt_attr = 'E'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of datasets
            return dt;
        }

        //撈取外部審核者的姓名,Email SELECT TOP (1) AuditorEmail, AuditorName FROM AuditorOutter
        public DataTable GetEmpNameEmailOutter(int empId)
        {
            String strSql = "SELECT AuditorName,AuditorEmail FROM AuditorOutter WHERE AuditorSn = " + empId;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得所有的外審人員類別
        public DataTable GetAllAuditorOuterRealm()
        {
            String strSql = "SELECT AuditorRealmSn, AuditorRealmName FROM AuditorOutterRealm order by AuditorRealmName";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得指定類別的外審人員
        public DataTable GetAllAuditorOuterByRealm(string selRealm)
        {
            String strSql = @"SELECT DISTINCT REPLACE(REPLACE(AuditorName, CHAR(13), ''), CHAR(10), '') + REPLACE(REPLACE(AuditorEmail, CHAR(13), ''), CHAR(10), '') AuditorName,
REPLACE(REPLACE(AuditorEmail, CHAR(13), ''), CHAR(10), '') AuditorEmail
  FROM[HRApply].[dbo].[AuditorOutter]";
            if (!selRealm.Equals(""))
                strSql += " WHERE AuditorRealmSn = '" + selRealm + "' ";
            
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //取得指定外審人員的Email
        public DataTable GetAuditorEmail(int selAuditorSn)
        {
            String strSql = "SELECT AuditorName, AuditorEmail  FROM AuditorOutter WHERE AuditorSn = " + selAuditorSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得指定的國籍
        public DataTable GetCountryName(string countryCode)
        {
            String strSql = "SELECT [code_ddesc]  FROM [s10_code_d] WHERE code_kind='NAT' and  code_no = '" + countryCode + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //取得指定的籍貫/出生地EmpBornProvince
        public DataTable GetHomeTownName(string homeTownCode)
        {
            String strSql = "SELECT [xrg_name] from  [s90_xorginp] WHERE xrg_id = '" + homeTownCode + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得指定的出生地-縣 EmpBornCity
        public DataTable GetBornCityName(string bornCityCode)
        {
            String strSql = "SELECT [cty_name] FROM [s90_city] WHERE cty_id = '" + bornCityCode + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得指定類別
        public DataTable GetKindName(string kindNo)
        {
            String strSql = "SELECT KindName  FROM CAuditKind WHERE KindNo = '" + kindNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //取得一級單位
        public DataTable GetLevel1UnitName(string untId)
        {
            String strSql = "SELECT unt_name_full  FROM [s90_unit] WHERE unt_id = (select dpt_id from  [s90_unit] where unt_id='" + untId + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //取得指定單位
        public DataTable GetUnitName(string untId)
        {
            String strSql = "SELECT unt_name_full  FROM [s90_unit] WHERE unt_id = '" + untId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得指定職稱
        public String GetJobTitleName(string jobTitleNo)
        {
            String strSql = "SELECT JobTitleName  FROM CJobTitle WHERE JobTitleNo = '" + jobTitleNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }


        //取得指定職別
        public DataTable GetJobTypeName(string jobAttrNo)
        {
            String strSql = "SELECT JobAttrName FROM CJobType WHERE JobAttrNo = '" + jobAttrNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        ////撈取 登入檔案 VAccountForAudit 角色
        //public DataTable GetAccount(string acctEmail)
        //{

        //    String strSql = "SELECT AcctAppSn, AcctRole FROM AccountForAudit WHERE AcctEmail = '" + acctEmail + "'";
        //    DataSet ds = new DataSet();
        //    SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
        //    DataTable dt = ds.Tables[0]; /// table of dataset
        //    return dt;

        //}

        ////撈取 登入檔案 VAccountForAudit 角色
        //public DataTable GetAccount(string acctEmail, string acctPassword)
        //{

        //    String strSql = "SELECT AcctAuditorSnEmpId, AcctRole, AcctIsTaipeiUniversity FROM AccountForAudit WHERE AcctEmail = '" + acctEmail + "' and AcctPassword = '" + acctPassword + "'";
        //    DataSet ds = new DataSet();
        //    SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
        //    DataTable dt = ds.Tables[0]; /// table of dataset
        //    return dt;

        //}

        //從登入檔中判斷此帳號是否存在
        public string GetAccountAuditPassword(string acctAuditorSnEmpId, string acctEmail)
        {
            string password = "";
            string strSql = "SELECT AcctPassword FROM AccountForAudit WHERE AcctAuditorSnEmpId = '" + acctAuditorSnEmpId + "' And AcctEmail = '" + acctEmail + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                password = dt.Rows[0][0].ToString();
            }
            return password;

        }

        //撈取StrengthsWeaknesses
        public DataTable GetStrengthsWeaknesses(VStrengthsWeaknesses obj)
        {
            if (obj.KindNo.Trim().ToString().Equals("2") || obj.KindNo.Trim().ToString().Equals("3")) obj.AttributeNo = "X";
            String strSql = "SELECT SWSn, SWContent, Status FROM StrengthsWeaknesses WHERE ([WaydNo] = '" + obj.WaydNo + "') " +
                            " AND (KindNo = '" + obj.KindNo + "') AND (AttributeNo = '" + obj.AttributeNo + "') AND (SWType = '" + obj.SWType + "')  ORDER BY  SWSn";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取審核報表顯示欄位
        public DataTable GetAuditReport(VAuditReportCompose obj)
        {
            String strSql = "SELECT ARWritingScore, ARStrengthsWeaknesses, ARFiveLevel FROM AuditReportCompose  WHERE [ARWaydNo] = '" + obj.ARWaydNo + "' " +
                            " AND ARKindNo = '" + obj.ARKindNo + "' AND ARAttributeNo = '" + obj.ARAttributeNo + "' AND ARStage = '" + obj.ARStage + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取指定教師學歷資料
        public VTeacherEdu GetVTeacherEdu(int EduSn)
        {

            String strSql = "SELECT EduSn, EduLocal, EduSchool, EduDepartment, EduDegree, EduDegreeType, EduStartYM, EduEndYM FROM [TeacherEdu] Where EduSn =" + EduSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherEdu vTeacherEdu = new VTeacherEdu();
            if (dt.Rows.Count > 0)
            {
                vTeacherEdu.EduSn = Convert.ToInt32(dt.Rows[0][vTeacherEdu.strEduSn].ToString());
                vTeacherEdu.EduLocal = dt.Rows[0][vTeacherEdu.strEduLocal].ToString();
                vTeacherEdu.EduSchool = dt.Rows[0][vTeacherEdu.strEduSchool].ToString();
                vTeacherEdu.EduDepartment = dt.Rows[0][vTeacherEdu.strEduDepartment].ToString();
                vTeacherEdu.EduDegree = dt.Rows[0][vTeacherEdu.strEduDegree].ToString();
                vTeacherEdu.EduDegreeType = dt.Rows[0][vTeacherEdu.strEduDegreeType].ToString();
                vTeacherEdu.EduStartYM = dt.Rows[0][vTeacherEdu.strEduStartYM].ToString();
                vTeacherEdu.EduEndYM = dt.Rows[0][vTeacherEdu.strEduEndYM].ToString();
                return vTeacherEdu;
            }
            else
            {
                return null;
            }
        }

        //撈取教師學歷資料確認是否有外籍就讀資料須顯示
        public DataTable GetAllVTeacherEduByEmpSn(int empSn)
        {

            String strSql = "SELECT EduSn, EduLocal, EduSchool, EduDepartment, EduDegree, EduDegreeType, EduStartYM, EduEndYM FROM [TeacherEdu] Where EmpSn =" + empSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //撈取指定教師經歷資料
        public VTeacherExp GetVTeacherExp(int ExpSn)
        {

            String strSql = "SELECT ExpSn, EmpSn, ExpOrginization, ExpStartYM, ExpEndYM, ExpUnit, ExpJobTitle,ExpJobType,ExpUpload, ExpUploadName FROM [TeacherExp] Where ExpSn =" + ExpSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherExp vTeacherExp = new VTeacherExp();
            if (dt.Rows.Count > 0)
            {
                vTeacherExp.ExpSn = Convert.ToInt32(dt.Rows[0][vTeacherExp.strExpSn].ToString());
                vTeacherExp.ExpOrginization = dt.Rows[0][vTeacherExp.strExpOrginization].ToString();
                vTeacherExp.ExpStartYM = dt.Rows[0][vTeacherExp.strExpStartYM].ToString();
                vTeacherExp.ExpEndYM = dt.Rows[0][vTeacherExp.strExpEndYM].ToString();
                vTeacherExp.ExpUnit = dt.Rows[0][vTeacherExp.strExpUnit].ToString();
                vTeacherExp.ExpJobTitle = dt.Rows[0][vTeacherExp.strExpJobTitle].ToString();
                vTeacherExp.ExpJobType = dt.Rows[0][vTeacherExp.strExpJobType].ToString();
                vTeacherExp.ExpUpload = (Boolean)dt.Rows[0][vTeacherExp.strExpUpload];
                vTeacherExp.ExpUploadName = dt.Rows[0][vTeacherExp.strExpUploadName].ToString();
                return vTeacherExp;
            }
            else
            {
                return null;
            }
        }


        //撈取教師經歷
        public DataTable GetAllVTeacherExpByEmpSn(int empSn)
        {
            String strSql = "SELECT ExpSn, EmpSn, ExpOrginization,  ExpUnit, ExpJobTitle,ExpStartYM, ExpEndYM FROM [TeacherExp] Where EmpSn =" + empSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public VTeacherTmuLesson GetTeacherTmuLesson(int lessonSn)
        {

            String strSql = "SELECT * FROM TeacherTmuLesson WHERE (LessonSn = " + lessonSn + ")";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherTmuLesson vTeacherTmuLesson = new VTeacherTmuLesson();
            if (dt.Rows.Count > 0)
            {
                vTeacherTmuLesson.LessonSn = Convert.ToInt32(dt.Rows[0][vTeacherTmuLesson.strLessonSn].ToString());
                vTeacherTmuLesson.EmpSn = Convert.ToInt32(dt.Rows[0][vTeacherTmuLesson.strEmpSn].ToString());
                vTeacherTmuLesson.LessonYear = dt.Rows[0][vTeacherTmuLesson.strLessonYear].ToString();
                vTeacherTmuLesson.LessonSemester = dt.Rows[0][vTeacherTmuLesson.strLessonSemester].ToString();
                vTeacherTmuLesson.LessonDeptLevel = dt.Rows[0][vTeacherTmuLesson.strLessonDeptLevel].ToString();
                vTeacherTmuLesson.LessonClass = dt.Rows[0][vTeacherTmuLesson.strLessonClass].ToString();
                vTeacherTmuLesson.LessonHours = dt.Rows[0][vTeacherTmuLesson.strLessonHours].ToString();
                vTeacherTmuLesson.LessonCreditHours = dt.Rows[0][vTeacherTmuLesson.strLessonCreditHours].ToString();
                vTeacherTmuLesson.LessonEvaluate = dt.Rows[0][vTeacherTmuLesson.strLessonEvaluate].ToString();
                vTeacherTmuLesson.LessonYear = dt.Rows[0][vTeacherTmuLesson.strLessonYear].ToString();
                vTeacherTmuLesson.LessonSemester = dt.Rows[0][vTeacherTmuLesson.strLessonSemester].ToString();
                return vTeacherTmuLesson;
            }
            else
            {
                return null;
            }
        }

        //撈取教師當學年度授課資料
        public DataTable GetAllVTeacherTmuLessonByEmpSn(int empSn)
        {
            GetSettings getSettings = new GetSettings();
            getSettings.Execute();
            String strSql = "SELECT LessonSn, EmpSn, LessonYear, LessonSemester, LessonDeptLevel, LessonClass, LessonHours, LessonUserId, LessonUpdateDate FROM TeacherTmuLesson Where EmpSn =" + empSn + " AND LessonYear = '" + getSettings.GetYear() + "' AND LessonSemester = '" + getSettings.GetSemester() + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //SELECT HorDescription,HorYear, HorSn, EmpSn FROM TeacherHonour Where EmpSn = @EmpSn
        public DataTable GetAllVTeacherHonourByEmpSn(int empSn)
        {
            GetSettings getSettings = new GetSettings();
            getSettings.Execute();
            String strSql = "SELECT HorDescription, HorYear, HorSn, EmpSn FROM TeacherHonour Where EmpSn =" + empSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }
        //取得起資年月 現職年資 string strUnit (修正:無須指定單位有可能會跨單位)
        public String GetTeacherTmuExp(string strEmpIdno, string strTitid)
        {
            String strStartDate = "";
            //*****現職年資修改部分
            //String strSql = "SELECT Top(1) fcu_bdate as ExpStartDate FROM  s10_expfcu  WHERE (fcu_idno = '" + strEmpIdno + "' and fcu_titid = '" + strTitid + "' and fcu_edate = '' ) order by fcu_bdate desc";
            String strSql = "SELECT Top(1) fcu_bdate as ExpStartDate FROM  s10_expfcu  WHERE (fcu_idno = '" + strEmpIdno + "' and fcu_titid = '" + strTitid + "') order by fcu_bdate ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherTmuExp vTeacherTmuExp = new VTeacherTmuExp();
            if (dt.Rows.Count > 0)
            {
                strStartDate = dt.Rows[0][0].ToString();
            }
            return strStartDate;
        }

        //兼職職務年資
        //SELECT oth_bdate FROM s10_otherpos WHERE oth_idno = 'F123914585' AND oth_titid = '' AND oth_edate = ''
        public String GetOtherExp(string strEmpIdno, string strTitid)
        {
            String strStartDate = "";
            String strSql = "SELECT oth_bdate FROM s10_otherpos WHERE oth_idno = '" + strEmpIdno + "' AND oth_titid = '" + strTitid + "' AND oth_edate = ''";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherTmuExp vTeacherTmuExp = new VTeacherTmuExp();
            if (dt.Rows.Count > 0)
            {
                strStartDate = dt.Rows[0][0].ToString();
            }
            return strStartDate;
        }

        //取得指定的系所現職別 b.unt_name_full AS ExpUnitName, LEFT OUTER JOIN s90_unit AS c ON a.fcu_unit = c.unt_id 
        public DataTable GetTeacherTmuTitId(string strEmpIdno, string strUnitId)
        {

            //String strSql =
            //    "SELECT Top(1) a.fcu_titid AS TitleId ,c.pos_name AS TitleName, b.unt_name_full AS UnitName FROM s10_expfcu AS a LEFT OUTER JOIN s90_unit AS b ON a.fcu_unit = b.unt_id " +
            //    " LEFT OUTER JOIN s90_position AS c ON a.fcu_posid = c.pos_id WHERE (fcu_idno = '" + strEmpIdno + "' and fcu_unit = '" + strUnitId + "' ) order by fcu_bdate desc";
            String strSql =
                "SELECT Top(1) a.fcu_titid AS TitleId ,c.tit_name AS TitleName, b.unt_name_full AS UnitName FROM s10_expfcu AS a LEFT OUTER JOIN s90_unit AS b ON a.fcu_unit = b.unt_id " +
                " LEFT OUTER JOIN s90_titlecode AS c ON a.fcu_titid = c.tit_id WHERE (fcu_idno = '" + strEmpIdno + "' and fcu_unit = '" + strUnitId + "' ) order by fcu_bdate desc";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherTmuExp vTeacherTmuExp = new VTeacherTmuExp();
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            return null;

        }

        public VTeacherTmuExp GetTeacherTmuExp(int intExpSn)
        {

            String strSql =
                "SELECT  ExpSn, EmpSn, ExpUnitId, ExpTitleId, ExpPosId, ExpQid, ExpStartDate, ExpEndDate, ExpUploadName, ExpUserId, ExpUpdateDate " +
                "FROM TeacherTmuExp WHERE ExpSn = " + intExpSn + " ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherTmuExp vTeacherTmuExp = new VTeacherTmuExp();
            if (dt.Rows.Count > 0)
            {
                vTeacherTmuExp.ExpSn = Convert.ToInt32(dt.Rows[0][vTeacherTmuExp.strExpSn].ToString());
                vTeacherTmuExp.EmpSn = Convert.ToInt32(dt.Rows[0][vTeacherTmuExp.strEmpSn].ToString());
                vTeacherTmuExp.ExpPosId = dt.Rows[0][vTeacherTmuExp.strExpPosId].ToString();
                vTeacherTmuExp.ExpQId = dt.Rows[0][vTeacherTmuExp.strExpQId].ToString();
                vTeacherTmuExp.ExpUnitId = dt.Rows[0][vTeacherTmuExp.strExpUnitId].ToString();
                vTeacherTmuExp.ExpTitleId = dt.Rows[0][vTeacherTmuExp.strExpTitleId].ToString();
                vTeacherTmuExp.ExpStartDate = dt.Rows[0][vTeacherTmuExp.strExpStartDate].ToString();
                vTeacherTmuExp.ExpEndDate = dt.Rows[0][vTeacherTmuExp.strExpEndDate].ToString();
            }
            return vTeacherTmuExp;

        }

        //撈取教師現職經歷資料確認 string strUnit (修正:無須指定單位有可能會跨單位)
        public DataTable GetAllVTeacherTmuExpByEmpSn(int empSn, string titleId)
        {

            String strSql = "SELECT ExpSn, EmpSn, ExpUnitId, ExpTitleId, ExpPosId,ExpQid, ExpStartDate, ExpEndDate, ExpUploadName, ExpUserId, ExpUpdateDate FROM [TeacherTmuExp] Where EmpSn =" + empSn + " and ExpTitleId = '" + titleId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //撈取指定教師簽證資料
        public VTeacherCa GetVTeacherCa(int CaSn)
        {
            String strSql = "SELECT CaSn, EmpSn, CaNumberCN, CaNumber, CaPublishSchool, CaStartYM, CaEndYM, CaUpload, CaUploadName FROM [TeacherCa] Where CaSn =" + CaSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherCa vTeacherCa = new VTeacherCa();
            if (dt.Rows.Count > 0)
            {
                vTeacherCa.CaSn = Convert.ToInt32(dt.Rows[0][vTeacherCa.strCaSn].ToString());
                vTeacherCa.CaNumberCN = dt.Rows[0][vTeacherCa.strCaNumberCN].ToString();
                vTeacherCa.CaNumber = dt.Rows[0][vTeacherCa.strCaNumber].ToString();
                vTeacherCa.CaPublishSchool = dt.Rows[0][vTeacherCa.strCaPublishSchool].ToString();
                vTeacherCa.CaStartYM = dt.Rows[0][vTeacherCa.strCaStartYM].ToString();
                vTeacherCa.CaEndYM = dt.Rows[0][vTeacherCa.strCaEndYM].ToString();
                vTeacherCa.CaUpload = (Boolean)dt.Rows[0][vTeacherCa.strCaUpload];
                vTeacherCa.CaUploadName = dt.Rows[0][vTeacherCa.strCaUploadName].ToString();
                return vTeacherCa;
            }
            else
            {
                return null;
            }
        }


        //撈取指定 教師學術獎勵、榮譽事項
        public VTeacherHonour GetVTeacherHonour(int HorSn)
        {
            String strSql = "SELECT  HorSn, EmpSn, HorYear, HorDescription FROM [TeacherHonour] Where HorSn =" + HorSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VTeacherHonour vTeacherHor = new VTeacherHonour();
            if (dt.Rows.Count > 0)
            {
                vTeacherHor.HorSn = Convert.ToInt32(dt.Rows[0][vTeacherHor.strHorSn].ToString());
                vTeacherHor.HorDescription = dt.Rows[0][vTeacherHor.strHorDescription].ToString();
                vTeacherHor.HorYear = dt.Rows[0][vTeacherHor.strHorYear].ToString();
                return vTeacherHor;
            }
            else
            {
                return null;
            }
        }

        //撈取指定 教師論文積分
        public VThesisScore GetVThesisScore(int ThesisSn)
        {
            String strSql = "SELECT ThesisSn, SnNo, EmpSn, RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, ThesisUploadName, IsCountRPI, IsRepresentative ,ThesisJournalRefCount,ThesisJournalRefUploadName, ThesisSummaryCN,ThesisCoAuthorUploadName FROM [ThesisScore] Where ThesisSn =" + ThesisSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VThesisScore vThesisScore = new VThesisScore();
            if (dt.Rows.Count > 0)
            {
                vThesisScore.ThesisSn = Convert.ToInt32(dt.Rows[0][vThesisScore.strThesisSn].ToString());
                vThesisScore.EmpSn = Convert.ToInt32(dt.Rows[0][vThesisScore.strEmpSn].ToString());
                vThesisScore.SnNo = Convert.ToInt32(dt.Rows[0][vThesisScore.strSnNo].ToString());
                vThesisScore.RRNo = dt.Rows[0][vThesisScore.strRRNo].ToString();
                vThesisScore.ThesisResearchResult = dt.Rows[0][vThesisScore.strThesisResearchResult].ToString();
                vThesisScore.ThesisPublishYearMonth = dt.Rows[0][vThesisScore.strThesisPublishYearMonth].ToString();
                vThesisScore.ThesisC = dt.Rows[0][vThesisScore.strThesisC].ToString();
                vThesisScore.ThesisJ = dt.Rows[0][vThesisScore.strThesisJ].ToString();
                vThesisScore.ThesisA = dt.Rows[0][vThesisScore.strThesisA].ToString();
                vThesisScore.ThesisTotal = dt.Rows[0][vThesisScore.strThesisTotal].ToString();
                vThesisScore.ThesisName = dt.Rows[0][vThesisScore.strThesisName].ToString();
                vThesisScore.ThesisUploadName = dt.Rows[0][vThesisScore.strThesisUploadName].ToString();
                vThesisScore.IsCountRPI = (Boolean)dt.Rows[0][vThesisScore.strIsCountRPI];
                vThesisScore.IsRepresentative = (Boolean)dt.Rows[0][vThesisScore.strIsRepresentative];
                vThesisScore.ThesisJournalRefCount = dt.Rows[0][vThesisScore.strThesisJournalRefCount].ToString();
                vThesisScore.ThesisJournalRefUploadName = dt.Rows[0][vThesisScore.strThesisJournalRefUploadName].ToString();
                vThesisScore.ThesisSummaryCN = dt.Rows[0][vThesisScore.strThesisSummaryCN].ToString();
                //vThesisScore.ThesisCoAuthorUpload = (Boolean)dt.Rows[0][vThesisScore.strThesisCoAuthorUpload];
                vThesisScore.ThesisCoAuthorUploadName = dt.Rows[0][vThesisScore.strThesisCoAuthorUploadName].ToString();

                return vThesisScore;
            }
            else
            {
                return null;
            }
        }

        //撈取指定 代表
        public String GetVThesisScoreRepresentative(int AppSn)
        {
            String RepresentativeThesisName = "";
            String strSql = "SELECT ThesisName FROM [ThesisScore] Where ThesisSn =" + AppSn + " and IsRepresentative = 'true'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VThesisScore vThesisScore = new VThesisScore();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i > dt.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        RepresentativeThesisName = dt.Rows[i][vThesisScore.strThesisName].ToString();
                    }
                    else
                    {
                        RepresentativeThesisName += "、" + dt.Rows[i][vThesisScore.strThesisUploadName].ToString();
                    }
                }
                return RepresentativeThesisName;
            }
            else
            {
                return null;
            }
        }

        //撈取所有的論文積分資料
        public DataTable GetThesisScoreList(String appSn)
        {

            String strSql = "SELECT ThesisSn, SnNo, RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, ThesisUploadName FROM ThesisScore WHERE AppSn = '" + appSn + "' order by SnNo";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            VThesisScore vThesisScore = new VThesisScore();
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }


        //AppDThesisOralList SELECT [ThesisOralSn],[ThesisOralType], [ThesisOralName], [ThesisOralTitle], [ThesisOralUnit] FROM [AppThesisOralList] where [ThesisOralSn] in  @AppDThesisOralList  ORDER BY [ThesisOralType], [ThesisOralSn]
        public DataTable GetThesisOralList(int appSn)
        {
            String strSql = "SELECT [ThesisOralSn],[ThesisOralAppSn],[ThesisOralType], [ThesisOralName], [ThesisOralTitle], [ThesisOralUnit], [ThesisOralOther] FROM [ThesisOralList] where [ThesisOralAppSn] =   " + appSn + "  ORDER BY [ThesisOralType], [ThesisOralSn]";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //判斷 系統設定檔是否存在
        public Boolean SystemOpendateExist(VSystemOpendate obj)
        {
            Boolean boolExist = false;
            String strSql = "select * from [SystemOpendate] where Smtr ='" + obj.Smtr + "' and  Semester = '" + obj.Semester + "' and KindNo = '" + obj.KindNo + "' and TypeNo = '" + obj.TypeNo + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            if (hasData(ds))
            {
                boolExist = true;
            }
            return boolExist;
        }

        //撈取 最晚的一筆系統設定檔(與原架構不合)
        public VSystemOpendate GetSystemOpendate()
        {
            VSystemOpendate vSystemOpendate = new VSystemOpendate();
            String strSql = "select * from [SystemOpendate]  order by Smtr, Semester desc";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                vSystemOpendate.Smtr = dt.Rows[0][vSystemOpendate.strSmtr].ToString();
                vSystemOpendate.Semester = dt.Rows[0][vSystemOpendate.strSemester].ToString();
                vSystemOpendate.KindNo = dt.Rows[0][vSystemOpendate.strKindNo].ToString();
                vSystemOpendate.ApplyBeginTime = (DateTime)dt.Rows[0][vSystemOpendate.strApplyBeginTime];
                vSystemOpendate.ApplyEndTime = (DateTime)dt.Rows[0][vSystemOpendate.strApplyEndTime];
                vSystemOpendate.AuditBeginTime = (DateTime)dt.Rows[0][vSystemOpendate.strAuditBeginTime];
                vSystemOpendate.AuditEndTime = (DateTime)dt.Rows[0][vSystemOpendate.strAuditEndTime];
                //vSystemOpendate.AdminBeginTime = (DateTime)dt.Rows[0][vSystemOpendate.strAdminBeginTime]; 預留
                //vSystemOpendate.AdminEndTime = (DateTime)dt.Rows[0][vSystemOpendate.strAdminEndTime]; 預留
                return vSystemOpendate;
            }
            else
            {
                return null;
            }
        }

        //撈取所有承辦人資料 前台顯示 [PAPOVA].[TmuHr].[dbo].[s90_city]
        public DataTable GetAllUnderTake()
        {
            String strSql = "SELECT b.unt_name_full as UntName, a.PointRoleName as RoleName, c.emp_name as EmpName, c.emp_email as EmpEmail, c.emp_id FROM UnderTaker INNER JOIN " +
                            "[PAPOVA].[TmuHr].[dbo].[s90_unit] AS b ON a.PointDept = b.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS INNER JOIN " +
                            "[PAPOVA].[TmuHr].[dbo].[s10_employee] AS c ON a.EmpId = c.emp_id COLLATE Chinese_Taiwan_Stroke_CI_AS ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        //撈取指定系所 承辦人資料
        public DataTable GetAllUnderTakerByDept(string dept)
        {

            String strSql = "SELECT UnSn, b.unt_name_full as UntName, a.EmpId as EmpId, a.PointRoleName as RoleName, c.emp_name as EmpName, c.emp_email as EmpEmail FROM UnderTaker a INNER JOIN " +
                            "[PAPOVA].[TmuHr].[dbo].[s90_unit] b ON a.PointDept = b.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS INNER JOIN " +
                            "[PAPOVA].[TmuHr].[dbo].[s10_employee] c ON a.EmpId = c.emp_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            " Where a.PointDept = '" + dept + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        //取得指定系所的承辦人  
        public DataTable GetUnderTakerByDeptPointSn(string dept, string sn)
        {
            String strSql = "SELECT a.EmpId,b.emp_name,b.emp_email FROM UnderTaker a INNER JOIN " +
                            "[PAPOVA].[TmuHr].[dbo].[s10_employee] b ON a.EmpId = b.emp_id COLLATE Chinese_Taiwan_Stroke_CI_AS " +
                            "WHERE a.PointDept = '" + dept + "' AND a.PointSn = '" + sn + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        //取得指定承辦人是否存在設定檔
        public Boolean GetUnderTakerByEmpId(string empid)
        {
            String strSql = "SELECT UnSn FROM UnderTaker where EmpId = '" + empid + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //取得指定承辦人所屬部門
        public DataTable GetUnderTakerPointDept(string empid)
        {
            //20191029 更正1~3級單位帶入尾碼(帶入問題)
            String strSql = "SELECT distinct a.PointDept + '-' + b.unt_levelb COLLATE Chinese_Taiwan_Stroke_CI_AS  AS unt_data, b.unt_name_full FROM UnderTaker AS a LEFT OUTER JOIN  [PAPOVA].[TmuHr].[dbo].[s90_unit] AS b ON a.PointDept = b.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS WHERE a.EmpId = '" + empid + "'";

            //String strSql = "SELECT distinct a.PointDept + CASE WHEN a.PointDept = 'E0000' THEN '-1' WHEN a.PointDept = 'E0100' THEN '-2' ELSE '-0' END AS unt_data, b.unt_name_full FROM UnderTaker AS a LEFT OUTER JOIN  [PAPOVA].[TmuHr].[dbo].[s90_unit] AS b ON a.PointDept = b.unt_id COLLATE Chinese_Taiwan_Stroke_CI_AS WHERE a.EmpId = '" + empid + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得指定承辦人 角色
        public String GetUnderTakerRole(string empid)
        {
            String strSql = "SELECT PointRoleName FROM UnderTaker  WHERE EmpId = '" + empid + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "";
            }
        }

        //撈取舊資料庫 資料寫入新資料表--學歷資料
        public DataTable GetEducationByEmpIdno(string strEmpIdno)
        {
            String strSql = "SELECT s10_education.edu_empid, s10_education.edu_idno, s10_education.edu_nation, s10_education.edu_xmcname,s10_education.edu_xddname, s10_education.edu_bdate, s10_education.edu_edate, s10_education.edu_xdlid, " +
                            "s90_xedlevel.xdl_name AS edu_xdlid_name FROM s10_education INNER JOIN s90_xedlevel ON s10_education.edu_xdlid = s90_xedlevel.xdl_id WHERE (s10_education.edu_idno = '" + strEmpIdno + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        //撈取舊資料庫 資料寫入新資料表--內部經歷資料 string strUnit (修正:無須指定單位有可能會跨單位，要指定現任職等)
        public DataTable GetExprienceByEmpIdno(string strEmpIdno, string strTitid)
        {
            String strSql = "SELECT DISTINCT  a.fcu_idno, a.fcu_unit as ExpUnitId, c.unt_name_full AS ExpUnitName," +
                            " a.fcu_titid as ExpTitleId, d.tit_name as ExpTitleName, a.fcu_posid as ExpPosId, " +
                            " f.tc_qid AS ExpQId, b.pos_name as ExpPosName, a.fcu_bdate as ExpStartDate, " +
                            " a.fcu_edate as ExpEndDate " +
                            " FROM s10_expfcu AS a " +
                             " LEFT OUTER JOIN s90_position AS b  ON a.fcu_posid = b.pos_id " +
                             " LEFT OUTER JOIN s90_unit AS c  ON a.fcu_unit = c.unt_id " +
                             " LEFT OUTER JOIN  s90_titlecode AS d  ON a.fcu_titid = d.tit_id " +
                             " LEFT OUTER JOIN s10_employee AS e " +
                             " ON e.emp_untid = a.fcu_unit AND e.emp_titid = a.fcu_titid AND e.emp_id = a.fcu_idno " +
                             " LEFT  JOIN s10_tchno AS f " +
                             " ON a.fcu_idno = f.tc_idno " +
                            " WHERE ( a.fcu_idno = '" + strEmpIdno + "' ) " +
                            " and CHARINDEX(f.[tc_qtype],d.tit_name)>0 ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        //撈取舊資料庫 資料寫入新資料表--授課資料
        public DataTable GetLessonByEmpId(string strEmpId)
        {
            int i;
            GetSettings getSettings = new GetSettings();
            String allYearSemester = "";
            for (i = 105; i <= int.Parse(getSettings.LoadYear); i++)
            {
                if (i.ToString() != getSettings.LoadYear)
                    allYearSemester += "'" + i.ToString() + 1 + "','" + i.ToString() + 2 + "',";
                else
                    allYearSemester += "'" + i.ToString() + 1 + "','" + i.ToString() + 2 + "'";

            }
            //String strSql = "SELECT prm_year as LessonYear, prm_smester as LessonSemester, prm_deplevel as LessonDeptLevel, prm_class as LessonClass, prm_hours as LessonHours FROM s13_pro_lessons WHERE (prm_idno = '" + strEmpIdno + "') AND (prm_year = '" + strEmpYear + "') AND (prm_smester = '" + strEmpSemester + "')";
            String strSql = "SELECT uuu.cos_smtr AS SMTR, CASE uuu.tch_name_2 WHEN '' THEN RTRIM(uuu.dept_name) ELSE RTRIM(uuu.dept_name)  + '--' + uuu.tch_name_1 + ' ' + uuu.tch_name_2 + ' ' + uuu.tch_name_3 END AS LessonDeptLevel " +
" , uuu.cos_cname AS LessonClass,CASE f.CosHours WHEN '' THEN '0' ELSE f.CosHours END AS LessionHours,CASE e.CosTest WHEN '' THEN '0' ELSE e.CosTest END AS CosTest, " +
"         e.CosCredit AS LessonCreditHours, (CONVERT(float, e.CosTest) + CONVERT(float, f.CosHours)) * uuu.cnt AS LessonHours " +
"FROM  (SELECT DISTINCT cos_smtr, cos_id, cos_class, dept_name, cos_cname, tch_name_1, tch_name_2, tch_name_3, COUNT(*) AS cnt " +
"        FROM (SELECT cos_id, cos_smtr, cos_class, cos_date, cos_id AS Expr1, tch_name_1, tch_name_2, tch_name_3, tch_id_1, tch_id_2, tch_id_3, cos_class AS Expr2, cos_cname, dept_name, " +
"         cos_room, Cos_Week1, Cos_Week2, Cos_Week3, Cos_Week4, Cos_Week5, Cos_Week6, Cos_Week7, cos_year, cos_sel_type, cos_day  FROM " +
" (SELECT a.cos_smtr, a.cos_date, a.cos_id, a.tch_name_1, a.tch_name_2,  " +
"                                   a.tch_name_3, a.tch_id_1, a.tch_id_2, a.tch_id_3, a.cos_class, c.cos_cname, " +
"                                   d.dept_name, b.cos_room, b.Cos_Week1, b.Cos_Week2, b.Cos_Week3, " +
"                                   b.Cos_Week4, b.Cos_Week5, b.Cos_Week6, b.Cos_Week7, b.cos_year, " +
"                                   b.cos_sel_type, a.cos_day " +
"             FROM         lecture_schedular AS a INNER JOIN " +
"                                   TMUAcademic.dbo.cos_smtr_costable AS b ON a.cos_id = b.cos_id AND " +
"                                   a.cos_smtr = b.cos_smtr AND a.cos_class = b.cos_class INNER JOIN " +
"                                   TMUAcademic.dbo.perm_course AS c ON a.cos_id = c.cos_id INNER JOIN " +
"                                   TMUAcademic.dbo.ref_dept AS d ON c.cos_dept = d.dept_no " +
"             UNION ALL " +
"             SELECT    a.cos_smtr, a.cos_date, a.cos_id, a.tch_name_1, a.tch_name_2, " +
"                                   a.tch_name_3, a.tch_id_1, a.tch_id_2, a.tch_id_3, a.cos_class, c.cos_cname, " +
"                                   d.dept_name, b.cos_room, b.Cos_Week1, b.Cos_Week2, b.Cos_Week3, " +
"                                   b.Cos_Week4, b.Cos_Week5, b.Cos_Week6, b.Cos_Week7, b.cos_year, " +
"                                   b.cos_sel_type, a.cos_day " +
"             FROM        seminar_schedular AS a INNER JOIN " +
"                                   TMUAcademic.dbo.cos_smtr_costable AS b ON a.cos_id = b.cos_id AND " +
"                                   a.cos_smtr = b.cos_smtr AND a.cos_class = b.cos_class INNER JOIN " +
"                                   TMUAcademic.dbo.perm_course AS c ON a.cos_id = c.cos_id INNER JOIN " +
"                                   TMUAcademic.dbo.ref_dept AS d ON c.cos_dept = d.dept_no " +
"             UNION ALL  " +
"             SELECT    a.cos_smtr, a.cos_date, a.cos_id, a.tch_name_1, a.tch_name_2, " +
"                                   a.tch_name_3, a.tch_id_1, a.tch_id_2, a.tch_id_3, a.cos_class, c.cos_cname, " +
"                                   d.dept_name, b.cos_room, b.Cos_Week1, b.Cos_Week2, b.Cos_Week3, " +
"                                   b.Cos_Week4, b.Cos_Week5, b.Cos_Week6, b.Cos_Week7, b.cos_year, " +
"                                   b.cos_sel_type, a.cos_day " +
"             FROM        laboratory_schedular AS a INNER JOIN " +
"                                   TMUAcademic.dbo.cos_smtr_costable AS b ON a.cos_id = b.cos_id AND " +
"                                   a.cos_smtr = b.cos_smtr AND a.cos_class = b.cos_class INNER JOIN " +
"                                   TMUAcademic.dbo.perm_course AS c ON a.cos_id = c.cos_id INNER JOIN " +
"                                   TMUAcademic.dbo.ref_dept AS d ON c.cos_dept = d.dept_no) AS ttt " +
"             WHERE           (tch_id_1 = '" + strEmpId + "') AND (cos_id NOT LIKE '%000Y%') AND (cos_smtr IN (" + allYearSemester +
"                           )) OR" +
"                           (cos_id NOT LIKE '%000Y%') AND (cos_smtr IN (" + allYearSemester + ")) AND (tch_id_2 = '" + strEmpId + "') OR " +
"                           (cos_id NOT LIKE '%000Y%') AND (cos_smtr IN (" + allYearSemester + ")) AND (tch_id_3 = '" + strEmpId + "')) AS derivedtbl_1 " +
" GROUP BY    cos_smtr, cos_id, cos_class, dept_name, cos_cname, tch_name_1, tch_name_2, tch_name_3) AS uuu INNER JOIN " +
" TMUAcademic.dbo.CosSmtrCostable AS e ON uuu.cos_id = e.CosID AND uuu.cos_smtr = e.CosSmtr AND " +
" uuu.cos_class = e.CosClass INNER JOIN " +
" TMUAcademic.dbo.CosSmtrCos AS f ON uuu.cos_id = f.CosID AND uuu.cos_smtr = f.CosSmtr AND " +
" uuu.cos_class = f.CosClass order by uuu.cos_smtr desc ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 1);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        //撈取 新教務系統 授課進度表(因新版資料從1081開始，所以不處理年度問題)  20200930
        public DataTable GetNewLessonByEmpId(string strEmpId)
        {
            //int i;
            //GetSettings getSettings = new GetSettings();
            //String allYearSemester = "";
            //for (i = 102; i <= int.Parse(getSettings.LoadYear); i++)
            //{
            //    if (i.ToString() != getSettings.LoadYear)
            //        allYearSemester += "'" + i.ToString() + 1 + "','" + i.ToString() + 2 + "',";
            //    else
            //        allYearSemester += "'" + i.ToString() + 1 + "','" + i.ToString() + 2 + "'";

            //}
            String strSql = " select AYEAR + CONVERT(varchar, SMS )as 'SMTR',ISNULL(O030.DEP_NAME,'') as 'LessonDeptLevel',ISNULL(t011.CH_LESSON,'') as 'LessonClass','0'as 'LessionHours','0' as 'CosTest',  ISNULL(T040.LECTR_CRD, '0') as 'LessonCreditHours',ISNULL(T040.SMS_HOURS, '0') as  'LessonHours' " +
                            " from payt040 T040 " +
                            " left join [TMUDB].[dbo].[TKET011] t011 ON T040.COSID = t011.COSID " +
                            " LEFT JOIN[TMUDB].[dbo].ORGT030 O030 ON T040.FACULTY_CODE = O030.TEACH_ORGANZIATION_CODE " +
                            " where acnt = '" + strEmpId + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 5);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }


        //撈取SCI Paper 資料寫入新資料表--
        public DataTable GetSCIPaperAuthorYear(String strEmpId, String strYear)
        {
            // 6 Patient => 5 專利  4 Book專書 ==> 8專書
            //String strSql = "SELECT publish_year,paper_name, paperClass, month, Impact_Factor, Jurnal_Rank, author, C_Score, J_Score, A_Score, CAST(C_Score AS decimal(5)) " +
            //                " * CAST(J_Score AS decimal(5)) * CAST(A_Score AS decimal(5)) AS TotalScore, RR " +
            //                " FROM (SELECT TOP (200) publish_year, paper_name, CASE WHEN paper_class2 = '1' THEN '1' WHEN paper_class2 = '3' THEN '3' " +
            //                " WHEN paper_class2 = '2' THEN '2' ELSE '4' END AS paperClass, RIGHT('00' + publish_month, 2) AS month, Impact_Factor, Jurnal_Rank, author, " +
            //                " CASE paper_class1 WHEN '1' THEN '3' WHEN '5' THEN '2' WHEN '3' THEN '2' WHEN '2' THEN '1' ELSE '0' " +
            //                " END AS C_Score, CASE WHEN (paper_class3 = '2' OR " +
            //                " paper_class3 = '3') THEN CASE WHEN Jurnal_Rank <= 10 THEN '6' WHEN Jurnal_Rank > 10 AND " +
            //                " Jurnal_Rank <= 20 THEN '5' WHEN Jurnal_Rank > 20 AND " +
            //                " Jurnal_Rank <= 40 THEN '4' WHEN Jurnal_Rank > 40 AND " +
            //                " Jurnal_Rank <= 60 THEN '3' WHEN Jurnal_Rank > 60 AND " +
            //                " Jurnal_Rank <= 80 THEN '2' WHEN Jurnal_Rank > 80 THEN '1' ELSE '0' END WHEN (paper_class3 = '4') " +
            //                " THEN '1' ELSE '0.5' END AS J_Score, CASE WHEN (comm_author = '1') " +
            //                " THEN '5' ELSE CASE WHEN iam = '1' THEN '5' WHEN iam = '2' THEN '3' WHEN iam = '3' THEN '1' ELSE '0.5' " +
            //                " END END AS A_Score, authors + CASE WHEN authors2 IS NULL " +
            //                " THEN '' ELSE ',' + authors2 END + '.' + paper_name + ' ' + journal_name + ' ' + publish_year + '(' + vol + '):' + page1 + '-' + page2 + '.' AS RR " +
            //                " FROM vw_SCI_paper WHERE (insert_bywho = '" + strAccount + "') AND (publish_year >  '" + strYear + "')) AS Da ";
            String strSql = "SELECT  pubYear AS pYear,paper_name, paperClass, pMonth, Impact_Factor, Jurnal_Rank, author, C_Score, J_Score, A_Score," +
                            "CAST(CAST(C_Score AS decimal(5,2)) * CAST(J_Score AS decimal(5,2)) * CAST(A_Score AS decimal(5,2)) AS decimal(6,2)) AS TotalScore, RR " +
                            "FROM  (SELECT TOP (200) RIGHT('000' + CAST(publish_year - 1911 AS varchar(3)), 3) AS pubYear, paper_name, CASE WHEN paper_class1 = '4' THEN '8' WHEN paper_class1 = '6' THEN '5' ELSE CASE WHEN paper_class2 = '1' THEN '1' WHEN paper_class2 = '3' THEN '3' WHEN paper_class2 = '2' THEN '2' ELSE '4' END END " +
                            " AS paperClass, RIGHT('00' + publish_month, 2) AS pMonth, Impact_Factor, Jurnal_Rank, author, " +
                            "CASE paper_class1 WHEN '1' THEN CASE paper_class2 WHEN '1' THEN '3' WHEN '2' THEN '1' WHEN '3' THEN '2'  WHEN '4' THEN '8' WHEN '6' THEN '5' ELSE CASE WHEN (paper_class2Letter = 1) " +
                            "THEN 2 ELSE 0 END END ELSE CASE WHEN (paper_class2Letter = 1) THEN 2 ELSE 0 END END AS C_Score, CONVERT(Decimal(7, 2), CASE WHEN (CONVERT(DECIMAL(7,4), Impact_Factor) >= 6) " +
                            "THEN Impact_Factor ELSE CASE WHEN (paper_class3 = '2' OR paper_class3 = '3') THEN CASE WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 10 THEN '6' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 10 AND " +
                            "CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 20 THEN '5' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 20 AND CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 40 THEN '4' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 40 AND CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 60 THEN '3' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 60 AND " +
                            "CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 80 THEN '2' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 80 THEN '1' ELSE '0' END WHEN (paper_class3 = '4') THEN '1' ELSE '0.5' END END) AS J_Score " +
                            ", CASE WHEN (comm_author = '1') THEN '5' ELSE CASE WHEN iam = '1' THEN '5' WHEN iam = '2' THEN '3' WHEN iam = '3' THEN '1' ELSE '0.5' " +
                            " END END AS A_Score, CASE WHEN iam = '1' OR comm_author = '1' THEN CASE WHEN iam = '1' THEN authors + '+' ELSE authors END + CASE WHEN comm_author = '1' THEN '*' ELSE '' END ELSE authors END + " +
                            " CASE WHEN authors2 != '' THEN ',' + CASE WHEN iam = '2' OR comm_author = '2' THEN CASE WHEN iam = '2' THEN authors2+'+' ELSE authors2 END + CASE WHEN comm_author = '2' THEN '*' ELSE '' END ELSE authors2 END ELSE '' END + " +
                            " CASE WHEN authors3 != '' THEN ',' + CASE WHEN iam = '3' OR comm_author = '3' THEN CASE WHEN iam = '3' THEN authors3+'+' ELSE authors3 END + CASE WHEN comm_author = '3' THEN '*' ELSE '' END ELSE authors3 END ELSE '' END + " +
                            " CASE WHEN authors4 != '' THEN ',' + CASE WHEN iam = '4' OR comm_author = '4' THEN CASE WHEN iam = '4' THEN authors4+'+' ELSE authors4 END + CASE WHEN comm_author = '4' THEN '*' ELSE '' END ELSE authors4 END ELSE '' END + " +
                            " CASE WHEN authors5 != '' THEN ',' + CASE WHEN iam = '5' OR comm_author = '5' THEN CASE WHEN iam = '5' THEN authors5+'+' ELSE authors5 END + CASE WHEN comm_author = '5' THEN '*' ELSE '' END ELSE authors5 END ELSE '' END + " +
                            " CASE WHEN authors6 != '' THEN ',' + CASE WHEN iam = '6' OR comm_author = '6' THEN CASE WHEN iam = '6' THEN authors6+'+' ELSE authors6 END + CASE WHEN comm_author = '6' THEN '*' ELSE '' END ELSE authors6 END ELSE '' END + " +
                            " '.' + paper_name + ' ' + journal_name + ' ' + CONVERT(nvarchar(50), publish_year) + '(' + vol + '):' + page1 + '-' + page2 + '.' AS RR " +
                            " FROM uv_SciPaper WHERE (tch_id = '" + strEmpId + "') AND (publish_year >  '" + strYear + "')) AS Da ORDER BY '' + pubYear + pMonth DESC";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 3);
            DataTable dt = ds.Tables[0]; /// table of dataset 
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        public DataTable GetSCIPaperByYear(String strEmpId, String strYear)
        {
            String strSql = "SELECT  pubYear AS pYear,paper_name, paperClass, pMonth, Impact_Factor, Jurnal_Rank, author, C_Score, J_Score, A_Score," +
                             "CAST(CAST(C_Score AS decimal(5,2)) * CAST(J_Score AS decimal(5,2)) * CAST(A_Score AS decimal(5,2)) AS decimal(6,2)) AS TotalScore, RR " +
                             "FROM  (SELECT TOP (200) RIGHT('000' + CAST(publish_year - 1911 AS varchar(3)), 3) AS pubYear, paper_name,  CASE WHEN paper_class1 = '4' THEN '8' WHEN paper_class1 = '6' THEN '5' ELSE " +
                             "CASE WHEN paper_class2 = '1' THEN '1' WHEN paper_class2 = '3' THEN '4' WHEN paper_class2 = '4' THEN '2'  WHEN paper_class2 = '5' THEN '2' ELSE '4' END END AS paperClass, RIGHT('00' + publish_month, 2) AS pMonth, Impact_Factor, Jurnal_Rank, author, " +
                             "CASE paper_class1 WHEN '1' THEN CASE paper_class2 WHEN '1' THEN '3' WHEN '2' THEN '1' WHEN '3' THEN '2'  WHEN '4' THEN '8' WHEN '6' THEN '5' ELSE CASE WHEN (paper_class2Letter = 1) " +
                             "THEN 2 ELSE 0 END END ELSE CASE WHEN (paper_class2Letter = 1) THEN 2 ELSE 0 END END AS C_Score, CONVERT(Decimal(5, 2), CASE WHEN (CONVERT(DECIMAL(6,4), Impact_Factor) >= 6) " +
                             "THEN Impact_Factor ELSE CASE WHEN (paper_class3 = '2' OR paper_class3 = '3') THEN CASE WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 10 THEN '6' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 10 AND " +
                             "CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 20 THEN '5' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 20 AND CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 40 THEN '4' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 40 AND CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 60 THEN '3' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 60 AND " +
                             "CONVERT(DECIMAL(7,4),Jurnal_Rank) <= 80 THEN '2' WHEN CONVERT(DECIMAL(7,4),Jurnal_Rank) > 80 THEN '1' ELSE '0' END WHEN (paper_class3 = '4') THEN '1' ELSE '0.5' END END) AS J_Score " +
                             ", CASE WHEN (comm_author = '1') THEN '5' ELSE CASE WHEN iam = '1' THEN '5' WHEN iam = '2' THEN '3' WHEN iam = '3' THEN '1' ELSE '0.5' " +
                             " END END AS A_Score, CASE WHEN iam = '1' OR comm_author = '1' THEN CASE WHEN iam = '1' THEN authors + '+' ELSE authors END + CASE WHEN comm_author = '1' THEN '*' ELSE '' END ELSE authors END + " +
                             " CASE WHEN authors2 != '' THEN ',' + CASE WHEN iam = '2' OR comm_author = '2' THEN CASE WHEN iam = '2' THEN authors2+'+' ELSE authors2 END + CASE WHEN comm_author = '2' THEN '*' ELSE '' END ELSE authors2 END ELSE '' END + " +
                             " CASE WHEN authors3 != '' THEN ',' + CASE WHEN iam = '3' OR comm_author = '3' THEN CASE WHEN iam = '3' THEN authors3+'+' ELSE authors3 END + CASE WHEN comm_author = '3' THEN '*' ELSE '' END ELSE authors3 END ELSE '' END + " +
                             " CASE WHEN authors4 != '' THEN ',' + CASE WHEN iam = '4' OR comm_author = '4' THEN CASE WHEN iam = '4' THEN authors4+'+' ELSE authors4 END + CASE WHEN comm_author = '4' THEN '*' ELSE '' END ELSE authors4 END ELSE '' END + " +
                             " CASE WHEN authors5 != '' THEN ',' + CASE WHEN iam = '5' OR comm_author = '5' THEN CASE WHEN iam = '5' THEN authors5+'+' ELSE authors5 END + CASE WHEN comm_author = '5' THEN '*' ELSE '' END ELSE authors5 END ELSE '' END + " +
                             " CASE WHEN authors6 != '' THEN ',' + CASE WHEN iam = '6' OR comm_author = '6' THEN CASE WHEN iam = '6' THEN authors6+'+' ELSE authors6 END + CASE WHEN comm_author = '6' THEN '*' ELSE '' END ELSE authors6 END ELSE '' END + " +
                             " '.' + paper_name + ' ' + journal_name + ' ' + publish_year + '(' + vol + '):' + page1 + '-' + page2 + '.' AS RR " +
                             " FROM uv_SciPaper WHERE (tch_id = '" + strEmpId + "') AND (publish_year = '" + strYear + "')) AS Da ORDER BY '' + pubYear + pMonth DESC";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 3);
            DataTable dt = ds.Tables[0]; /// table of dataset 
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        //
        //SELECT  TOP (1)  SnNo FROM  ThesisScore WHERE  (EmpSn = 1037) ORDER BY SnNo DESC
        public bool GetThesisScoreFinalSnNo(String AppSn, int SnNo)
        {

            String strSql = "SELECT  TOP (1)  SnNo FROM  ThesisScore WHERE  (AppSn = '" + AppSn + "' and SnNo = '" + SnNo + "') ORDER BY SnNo DESC";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetThesisScoreGetThesisSn(int empSn, int appSn, int snNo)
        {
            int ThsisSn = 0;
            String strSql = "SELECT ThesisSn FROM  ThesisScore WHERE  AppSn = " + appSn + " and EmpSn = " + empSn + " and SnNo = " + snNo;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return ThsisSn;
            }
        }


        public Boolean GetThesisScoreUpdateSnNo(int ThesisSn, int SnNo)
        {
            String strSql = "SELECT * FROM [ThesisScore] WHERE [ThesisSn] = " + ThesisSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "M", "ThesisScore");
            DataRow dr = ds.Tables[0].Rows[0];
            dr["SnNo"] = SnNo;
            //dr[obj.strAppPPMUploadName] = obj.AppPPMUploadName;
            return du.Update(ds, "ThesisScore");
        }

        //取得小於插入 publish_date+month 的SnNo 
        public int GetThesisScoreInsertSnNo(String empSn, String publishYearMonth)
        {
            int SnNo = 1;
            String strSql = "SELECT TOP (1) SnNo, ThesisSn FROM ThesisScore WHERE (EmpSn = '" + empSn + "') AND (ThesisPublishYearMonth < '" + publishYearMonth + "') ORDER BY SnNo ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return SnNo;
            }
        }

        //取得所有插入之後的資料 //, String publishYearMonth AND (ThesisPublishYearMonth <= '" + publishYearMonth + "')
        public DataTable GetVThesisScoreAfterInsert(int empSn)
        {

            String strSql = "SELECT  *  FROM  ThesisScore WHERE  (EmpSn = " + empSn + ")  ORDER BY SnNo";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //同步天方系統開放日期設定資料
        public DataTable GetSystemOpenDateByProId(String proId)
        {
            String strSql = "SELECT pro_id, pro_name, pro_data_sdt, pro_data_edt, pro_qry_sdt, pro_qry_edt, upd_name FROM s99_web_process WHERE (pro_id = '" + proId + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }

        //撈取是否現在為系統開放期間，有資料就是開放區間
        public Boolean GetDuringOpenDate(String kindNo)
        {
            String strSql = "SELECT Sn,Smtr FROM SystemOpendate WHERE (DATEDIFF(hour, ApplyBeginTime, GETDATE()) > - 1) AND (DATEDIFF(hour, GETDATE(), ApplyEndTime) > - 1) AND (KindNo = '" + kindNo + "')";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }




        //撈取現在開放教職務別 JobTitle
        public DataTable GetJobTitleOpenDate(String strUnitId)
        {
            String nowdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            int intYear = Int32.Parse(nowdate.Substring(0, 4)) - 1911;
            String today = "" + intYear + nowdate.Substring(4, 4);
            String strSql = "SELECT DISTINCT s90_titlecode.tit_id AS JobTitleNo, s90_titlecode.tit_name AS JobTitleName FROM s90_titlecode INNER JOIN s13_opendate ON s90_titlecode.tit_id = s13_opendate.op_title " +
                    //"WHERE (s13_opendate.op_kind = 'new') AND (s13_opendate.op_unit = '" + strUnitId + "') AND (s13_opendate.op_bdate <= '" + today + "') AND (s13_opendate.op_edate >= '" + today + "') " + 
                    "WHERE (s13_opendate.op_kind = 'new') AND (s13_opendate.op_unit = '" + strUnitId + "') AND (s13_opendate.op_bdate <= '" + today + "') " +
                    "ORDER BY   s90_titlecode.tit_id ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;

        }

        //撈取開放專兼任別 JobType
        public DataTable GetJobTypeOpenDate(String strUnitId, String strJobTitleId)
        {
            String nowdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            int intYear = Int32.Parse(nowdate.Substring(0, 4)) - 1911;
            String today = "" + intYear + nowdate.Substring(4, 4);
            String strSql = "SELECT DISTINCT op_poaddt AS JobAttrNo, CASE op_poaddt WHEN '1' THEN '專任' ELSE '兼任' END AS JobAttrName " +
            " FROM s13_opendate WHERE (op_unit = '" + strUnitId + "') AND (op_bdate <= '" + today + "') AND (op_edate >= '" + today + "') AND (op_title = '" + strJobTitleId + "') ORDER BY op_poaddt";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }


        //撈取教師研究指標RPI
        public DataTable GetSCITotalScore(String strEmpId)
        {
            String nowdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            String writeYear = "" + (Int32.Parse(nowdate.Substring(0, 4)) - 1);
            String strSql = "SELECT TOP (200) PT_Year, PT_EmpId, PT_InPerson, PT_FSCI, PT_FSSCI, PT_FEI, PT_FNSCI, PT_FOther, PT_NFCSCI, " +
                            "PT_NFCSSCI, PT_NFCEI, PT_NFCNSCI, PT_NFCOther, PT_NFOCSCI, PT_NFOCSSCI, PT_NFOCEI, PT_NFOCNSCI, " +
                            "PT_NFOCOther, PT_Update FROM  PaperThesis WHERE (PT_EmpId = '" + strEmpId + "') and PT_Year = '" + writeYear + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 2);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得實際課堂每週時數
        public DataTable GetActucalEachWeekHour(String strEmpId, int beginYear, int endYear)
        {

            String strSql = "SELECT [school_year] ,[emp_id] ,[cred_1smtr] ,[cred_2smtr],[cred_1smtr] +[cred_2smtr]  as subTotal , [cred_clinical],[upd_dt] FROM [v_s13_teacher_credits] where  emp_id = '" + strEmpId + "' and (school_year >= " + beginYear + " and school_year <= " + endYear + ")";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 0);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得教學評量成果(上學期減2 下學期減1) 
        public DataTable GetTeachEvaluateResult(String strEmpId, String sYear)
        {
            String strSql = "evl_yr, emp_id, evl_t21, evl_t22, evl_tavg, upd_name, upd_dt FROM s13_evl_rect2 WHERE emp_id = '" + strEmpId + "' and   evl_yr = '" + sYear + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S", 1);
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        //取得查看前面人的評語(新聘)  2017/03/09全部改成顯示所有評語
        //SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason FROM AuditExecute WHERE  (AppSn = '3') AND (ExecuteStage < '4') and LEN(ExecuteCommentsA) > 0 
        public DataTable GetOtherApplyComments(String strAppSn, String intExecuteStage, String intExecuteStep)
        {

            DataSet ds = new DataSet();
            String strSql = "";
            DataTable dt = null; /// table of dataset
            //if (intExecuteStep.Equals("1"))
            //{
            strSql = "SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason FROM AuditExecute " +
               "WHERE  (AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteCommentsA) > 0) or " +
                      "(AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteReturnReason) > 0) or " +
                      "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep = '" + intExecuteStep + "' and LEN(ExecuteCommentsA) > 0) or " +
                      "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep = '" + intExecuteStep + "' and LEN(ExecuteReturnReason) > 0) or " +
                      "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep < '" + intExecuteStep + "' and LEN(ExecuteCommentsA) > 0) or " +
                      "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep < '" + intExecuteStep + "' and LEN(ExecuteReturnReason) > 0) ";
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            dt = ds.Tables[0];
            //}
            //else
            //{
            //    strSql = "SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason FROM AuditExecute "+
            //        " WHERE (AppSn = '" + strAppSn + "' AND ExecuteStage <= '" + intExecuteStage + "' and LEN(ExecuteCommentsA) > 0) or "+
            //                "(AppSn = '" + strAppSn + "'  AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep = '" + intExecuteStep + "' and LEN(ExecuteCommentsA) > 0) or " +
            //                "(AppSn = '" + strAppSn + "' AND ExecuteStage <= '" + intExecuteStage + "' and LEN(ExecuteReturnReason) > 0) or "+
            //                "(AppSn = '" + strAppSn + "'  AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep = '" + intExecuteStep + "' and LEN(ExecuteReturnReason) > 0)  ";
            //    SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            //    dt = ds.Tables[0]; 
            //}            
            return dt;
        }

        public DataTable GetOtherApplyCommentsHR(String strAppSn)
        {
            DataSet ds = new DataSet();
            String strSql = "";
            DataTable dt = null; /// table of dataset
            //if (intExecuteStep.Equals("1"))
            //{
            strSql = "SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason FROM AuditExecute " +
               "WHERE  (AppSn = '" + strAppSn + "' and LEN(ExecuteCommentsA) > 0) ";
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            dt = ds.Tables[0];
            return dt;
        }

        //取得查看前面人的評語(升等)
        public DataTable GetOtherPromoteComments(String strAppSn, String intExecuteStage, String intExecuteStep)
        {

            DataSet ds = new DataSet();
            String strSql = "";
            DataTable dt = null; /// table of dataset
            if (intExecuteStep.Equals("1"))
            {
                strSql = "SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason,ExecuteTeachingScore,ExecuteServiceScore FROM AuditExecute " +
                    "WHERE  (AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteCommentsA) > 0) or " +
                           "(AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteReturnReason) > 0) or " +
                           "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep = '" + intExecuteStep + "' and LEN(ExecuteCommentsA) > 0) or " +
                           "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep = '" + intExecuteStep + "' and LEN(ExecuteReturnReason) > 0) or " +
                           "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep < '" + intExecuteStep + "' and LEN(ExecuteCommentsA) > 0) or " +
                           "(AppSn = '" + strAppSn + "' AND ExecuteStage = '" + intExecuteStage + "'  AND ExecuteStep < '" + intExecuteStep + "' and LEN(ExecuteReturnReason) > 0) ";

                //WHERE " +
                //         "    (AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteCommentsA) > 0) "+ 
                //         " or (AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteReturnReason) > 0)"+
                //         " or (AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteTeachingScore) > 0)" +
                //         " or (AppSn = '" + strAppSn + "' AND ExecuteStage < '" + intExecuteStage + "' and LEN(ExecuteServiceScore) > 0)";
                SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
                dt = ds.Tables[0];
            }
            else
            {
                strSql = "SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason,ExecuteTeachingScore,ExecuteServiceScore FROM AuditExecute WHERE " +
                         "    ((AppSn = '" + strAppSn + "' AND ExecuteStage < '8' and LEN(ExecuteCommentsA) > 0) or (AppSn = '" + strAppSn + "' AND ExecuteStage < '8'  AND ExecuteStep = '1' and LEN(ExecuteCommentsA) > 0)) " +
                         " or ((AppSn = '" + strAppSn + "' AND ExecuteStage < '8' and LEN(ExecuteReturnReason) > 0) or (AppSn = '" + strAppSn + "'  AND ExecuteStage = '8'  AND ExecuteStep = '1' and LEN(ExecuteReturnReason) > 0)) " +
                         " or ((AppSn = '" + strAppSn + "' AND ExecuteStage < '8' and LEN(ExecuteTeachingScore) > 0) or (AppSn = '" + strAppSn + "'  AND ExecuteStage = '8'  AND ExecuteStep = '1' and LEN(ExecuteTeachingScore) > 0)) " +
                         " or ((AppSn = '" + strAppSn + "' AND ExecuteStage < '8' and LEN(ExecuteServiceScore) > 0) or (AppSn = '" + strAppSn + "'  AND ExecuteStage = '8'  AND ExecuteStep = '1' and LEN(ExecuteServiceScore) > 0)) ";
                SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
                dt = ds.Tables[0];
            }
            return dt;
        }


        //取得所有人的評語(新聘)
        public DataTable GetAllApplyComments(String strAppSn)
        {

            DataSet ds = new DataSet();
            String strSql = "";
            DataTable dt = null; /// table of dataset
            strSql = "SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason FROM AuditExecute WHERE  (AppSn = '" + strAppSn + "' AND and LEN(ExecuteCommentsA) > 0) or (AppSn = '" + strAppSn + "' and LEN(ExecuteReturnReason) > 0)";
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            dt = ds.Tables[0];
            return dt;
        }

        //取得所有人的評語(升等)
        public DataTable GetAllPromoteComments(String strAppSn)
        {

            DataSet ds = new DataSet();
            String strSql = "";
            DataTable dt = null; /// table of dataset
            strSql = "SELECT  ExecuteRoleName, ExecuteAuditorName, ExecuteCommentsA, ExecuteReturnReason,ExecuteTeachingScore,ExecuteServiceScore FROM AuditExecute WHERE  (AppSn = '" + strAppSn + "'  and LEN(ExecuteCommentsA) > 0) or (AppSn = '" + strAppSn + "'  and LEN(ExecuteTeachingScore) > 0) or (AppSn = '" + strAppSn + "'  and LEN(ExecuteServiceScore) > 0)";
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            dt = ds.Tables[0];
            return dt;
        }

        //逐筆匯入 ThesisScoreCount
        public int InsertSCITotalScoreAll(String strEmpId)
        {

            int intAppESn = 0;
            DataSet ds = new DataSet();
            DataRow dr;
            SqlDataUpdater du = new SqlDataUpdater("select * from ThesisScoreCount where 1=2", ref ds, "A");
            DataTable dtOld = this.GetSCITotalScore(strEmpId); //取得教師研究指標那邊的資料
            VThesisScoreCount vThesisScoreCount;
            if (dtOld.Rows.Count > 0)
            {
                for (int i = 0; i < dtOld.Rows.Count; i++)
                {
                    vThesisScoreCount = new VThesisScoreCount();
                    vThesisScoreCount.PT_Year = dtOld.Rows[i][vThesisScoreCount.strPT_Year].ToString();
                    vThesisScoreCount.PT_EmpId = dtOld.Rows[i][vThesisScoreCount.strPT_EmpId].ToString();
                    vThesisScoreCount.PT_InPerson = dtOld.Rows[i][vThesisScoreCount.strPT_InPerson].ToString();
                    vThesisScoreCount.PT_FSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_FSCI].ToString());
                    vThesisScoreCount.PT_FSSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_FSSCI].ToString());
                    vThesisScoreCount.PT_FEI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_FEI].ToString());
                    vThesisScoreCount.PT_FNSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_FNSCI].ToString());
                    vThesisScoreCount.PT_FOther = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_FOther].ToString());
                    vThesisScoreCount.PT_NFCSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFCSCI].ToString());
                    vThesisScoreCount.PT_NFCSSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFCSSCI].ToString());
                    vThesisScoreCount.PT_NFCEI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFCEI].ToString());
                    vThesisScoreCount.PT_NFCNSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFCNSCI].ToString());
                    vThesisScoreCount.PT_NFCOther = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFCOther].ToString());
                    vThesisScoreCount.PT_NFOCSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFOCSCI].ToString());
                    vThesisScoreCount.PT_NFOCSSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFOCSSCI].ToString());
                    vThesisScoreCount.PT_NFOCEI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFOCEI].ToString());
                    vThesisScoreCount.PT_NFOCNSCI = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFOCNSCI].ToString());
                    vThesisScoreCount.PT_NFOCOther = Convert.ToInt16(dtOld.Rows[i][vThesisScoreCount.strPT_NFOCOther].ToString());
                    this.Insert(vThesisScoreCount);
                }
                intAppESn = dtOld.Rows.Count;
            }
            else
            {
                ds = new DataSet();
            }

            return intAppESn;
        }

        public DataTable GetThesisScoreCount(String strEmpId)
        {
            String nowdate = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            String writeYear = "" + (Int32.Parse(nowdate.Substring(0, 4)) - 1);
            String strSql = "SELECT TOP (200) PT_Year, PT_EmpId, PT_InPerson, PT_FSCI, PT_FSSCI, PT_FEI, PT_FNSCI, PT_FOther, PT_NFCSCI, " +
                            "PT_NFCSSCI, PT_NFCEI, PT_NFCNSCI, PT_NFCOther, PT_NFOCSCI, PT_NFOCSSCI, PT_NFOCEI, PT_NFOCNSCI, " +
                            "PT_NFOCOther FROM  ThesisScoreCount WHERE (PT_EmpId = '" + strEmpId + "') and PT_Year = '" + writeYear + "' ";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(strSql, ref ds, "S");
            DataTable dt = ds.Tables[0]; /// table of dataset
            return dt;
        }

        public Boolean Delete(VEmployeeBase obj)
        {
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater("delete from EmployeesBase where EmpSn=@EmpSn", ref ds, "D");
            SqlCommand cmd = du.cmdObject;
            SqlConnection cn = du.cnObject;
            cmd = new SqlCommand("DELETE FROM Test.Dept WHERE DeptNo = :" + obj.EmpSn, cn);
            //da.DeleteCommand.Parameters.Add("DeptNo", SqlDbType.Int, 0, "DeptNo").SourceVersion = DataRowVersion.Original; 


            cmd.Parameters.Add(new SqlParameter("@EmpSn", SqlDbType.Int));
            cmd.Parameters["@EmpSn"].SourceVersion = DataRowVersion.Original;
            cmd.Parameters["@EmpSn"].SourceColumn = "EmpSn";
            ds.Tables["EmployeesBase"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "EmployeesBase");
        }

        public Boolean Delete(VTeacherEdu obj)
        {
            string sql = "Delete from TeacherEdu where EduSn = " + obj.EduSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "TeacherEdu");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "TeacherEdu");
        }

        public Boolean Delete(VTeacherTmuExp obj)
        {
            string sql = "Delete from TeacherTmuExp where EmpSn = " + obj.EmpSn + " and ExpTitleId = '" + obj.ExpTitleId + "'";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "TeacherTmuExp");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "TeacherTmuExp");
        }

        public Boolean Delete(VTeacherTmuLesson obj)
        {
            string sql = "Delete from TeacherTmuLesson where EmpSn = " + obj.EmpSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "TeacherTmuLesson");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "TeacherEdu");
        }

        public Boolean DeleteThesisOral(int thesisOralSn)
        {
            string sql = "Delete from ThesisOralList where ThesisOralSn = " + thesisOralSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "ThesisOralList");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "ThesisOralList");
        }

        public Boolean DeleteThesisScore(int intEmpSn)
        {
            string sql = "Delete from ThesisScore where EmpSn = " + intEmpSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "ThesisScore");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "ThesisScore");
        }

        public Boolean DeleteThesisScoreTemp(int intEmpSn)
        {
            string sql = "Delete from ThesisScoreTemp where EmpSn = " + intEmpSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "ThesisScoreTemp");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "ThesisScoreTemp");
        }

        public Boolean DeleteThesisScoreBySn(int thesisSn)
        {
            string sql = "Delete from ThesisScore where ThesisSn = " + thesisSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "ThesisScore");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "ThesisScore");
        }

        public Boolean DeleteTeacherTmuExp(int intEmpSn)
        {
            string sql = "Delete from TeacherTmuExp where EmpSn = " + intEmpSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "TeacherTmuExp");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "TeacherTmuExp");
        }

        public Boolean DeleteTeacherTmuLesson(int intEmpSn)
        {
            string sql = "Delete from TeacherTmuLesson where EmpSn = " + intEmpSn;
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "TeacherTmuLesson");
            //ds.Tables["TeacherEdu"].Rows.Find(obj.EmpSn).Delete();
            return du.Delete(ds, "TeacherTmuLesson");
        }

        public Boolean DeleteSystemOpendate()
        {
            string sql = "Delete from SystemOpendate";
            DataSet ds = new DataSet();
            SqlDataUpdater du = new SqlDataUpdater(sql, ref ds, "D", "SystemOpendate");
            return du.Delete(ds, "SystemOpendate");
        }
        public void UpdThsisScore(string score, string appsn)
        {
            using (SqlConnection CN = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = CN;
                command.CommandType = CommandType.Text;
                CN.Open();

                command.CommandText = " Update ApplyAudit ";
                command.CommandText += " set AppThesisAccuScore = '" + score + "' ";
                command.CommandText += " where  AppSn = '" + appsn + "'";
                command.ExecuteNonQuery();
                command.Cancel();
                CN.Close();
            }
        }
        bool hasData(DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
            {
                if (table.Rows.Count != 0) return true;
            }
            return false;
        }

        public string GetAppJobTitleNo(string AppSn)
        {
            String strSql = "SELECT AppJobTitleNo FROM [ApplyAudit] WHERE [AppSn] IN ('" + AppSn + "')";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSql, cn))
                {
                    DataTable dt = new DataTable("UserData");
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return dt.Rows[0]["AppJobTitleNo"].ToString().Trim();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public Boolean CheckPromoteEmp(string EmpIdno, string year, string semester)
        {
            String strSql = "SELECT * FROM [ApplyAudit] WHERE [EmpIdno] IN ('" + EmpIdno + "') and [AppYear] ='" + year + "' and [AppSemester] ='" + semester + "'";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSql, cn))
                {
                    DataTable dt = new DataTable("UserData");
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool reSend(string appsn)
        {
            ConnectionStringSettings connstring = WebConfigurationManager.ConnectionStrings["tmuConnectionString"];
            SqlCommand command = new SqlCommand();

            using (SqlConnection conn1 = new SqlConnection(connstring.ConnectionString))
            {
                command.Connection = conn1;
                command.CommandType = CommandType.Text;
                conn1.Open();
                try
                {
                    command.CommandText = "UPDATE [dbo].[AuditExecute] SET ExecutePass = ''  WHERE [AppSn] = '" + appsn + "' and ExecutePass = '3' ";
                    //DataRow dr = ds.Tables[0].Rows[0];
                    //dr["AppYear"] = obj.AppYear;
                    //dr["AppSemester"] = obj.AppSemester;
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException e1)
                {
                    return false;
                }
            }
        }

        public string CheckOuterNM(string email, string password)
        {
            string AcctAuditorSnEmpId = null;
            String strSql = "SELECT top(1) AcctAuditorSnEmpId FROM [AccountForAudit] WHERE [AcctEmail] = '" + email + "' and AcctPassword = '" + password + "' ";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(strSql, cn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        AcctAuditorSnEmpId = dt.Rows[0]["AcctAuditorSnEmpId"].ToString().Trim();
                    }
                }
            }

            if (AcctAuditorSnEmpId != null)
            {
                string sql = " SELECT top(1) AuditorName FROM [AuditorOutter] where AuditorSn = '" + AcctAuditorSnEmpId + "' ";
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["tmuConnectionString"].ConnectionString))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            return dt.Rows[0]["AuditorName"].ToString().Trim();
                        }
                        else
                            return null;
                    }
                }
            }
            else
                return null;
        }
    }
}