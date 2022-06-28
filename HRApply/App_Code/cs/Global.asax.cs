using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Collections;

namespace ApplyPromote
{
    public class Global : System.Web.HttpApplication
    {
        public const string FileUpPath = "/DocuFile/HRApplyDoc/"; //./UploadFiles/   ../DocuFile/HRApplyDoc/

        public const string PhysicalFileUpPath = "..\\DocuFile\\HRApplyDoc\\"; //UploadFiles\\  ..\\DocuFile\\HRApplyDoc\\
        public const string PhysicalPdfUpPath = "..\\..\\DocuFile\\HRApplyDoc\\pdf\\"; //UploadFiles\\  ..\\DocuFile\\HRApplyDoc\\


        //public const string PhysicalFileUpPath = "DocuFile\\HRApplyDoc\\"; //UploadFiles\\  ..\\DocuFile\\HRApplyDoc\\
        //public const string PhysicalPdfUpPath = "..\\..\\DocuFile\\HRApplyDoc\\pdf\\"; //UploadFiles\\  ..\\DocuFile\\HRApplyDoc\\



        //public const string strYear = "105";      //目前學年度
        public const string strYearSmtr = "1072";      //目前學年度
        //public const string FileDownPathExl = "~/DownLoadData/";   //Excel匯出檔案存放位置
        //public ArrayList untArray = new ArrayList { "E0109", "E0111", "E0112", "E0113", "E0114", "E0115", "E0116", "E0117", "E0118", "E0119", "E0120", "E0121", "E0122", "E0123", "E0124", "E0125", "E0126" };


        protected void Application_Start(object sender, EventArgs e)
        {
            

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

    }
}