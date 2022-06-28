using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.events;
using iTextSharp.text.error_messages;
using System.IO;
using System.Web;

/// <summary>
/// UserData 的摘要描述
/// </summary>
/// 

namespace ApplyPromote
{
    public class ResumePDF
    {
		public static string WebsitePath = HttpContext.Current.Server.MapPath("~");
        //public static string FilePath = Path.GetFullPath(Path.Combine(WebsitePath, @"..\UpLoadData\Std_DayOff\"));  //網站目錄的上一層需這樣寫才找的到
        //public static string FileVirtualPath = "/UploadData/Std_DayOff/";
        static CRUDObject crudObject;
        public ResumePDF()
        {
            crudObject = new CRUDObject();
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public void Output(System.Web.UI.Page WebForm, int appSn, ref string strErrMsg)
        {
            string Paper_Name = "";
            string strFileName = "";
            string strTemp;
            //string fileDownPath = "~/HRApplyDoc/pdf/";
            //string fileDownPath = "~/../DocuFile/HRApplyDoc/pdf/";
            string fileDownPath = "~/../DocuFile/HRApplyDoc/pdf/";
            //string Path = Server.MapPath("pdf");  
            BaseFont btChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\MINGLIU.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font ChFont = new Font(btChinese, 12);
            Font ChFontSmall = new Font(btChinese, 10);
            Font ChFontBlue = new Font(btChinese, 12, Font.NORMAL, new BaseColor(51, 0, 15));
            Font ChFontHeader = new Font(btChinese, 20);
            Font ChFontTitle = new Font(btChinese, 16, Font.BOLD);
            Font ChFontText = new Font(btChinese, 14);
            Paragraph P;
            PdfPTable table;
            PdfPTable tableChild1;
            Chunk C1;
            Phrase PG;
            PdfPCell PCell;
            VApplyAudit vApplyAudit;



            Document Doc1 = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream Memory = new MemoryStream();

            
            
            vApplyAudit = crudObject.GetApplyAuditObjByAppSn(appSn);
            String strEmpSn = "" + vApplyAudit.EmpSn;

            //CREATING OBJECTS OF PDF AND DOCUMENT
            //string SaveFileDir = Path.GetFullPath(Path.Combine(WebsitePath, @"\DocuFile\HRApplyDoc\pdf\"));
			string SaveFileDir = Path.GetFullPath(Path.Combine(WebsitePath, fileDownPath));
            string ImagesDir = WebForm.Server.MapPath(Global.PhysicalFileUpPath + "/" + strEmpSn + "/");
            DirectoryInfo ds = new DirectoryInfo(ImagesDir);
            //刪除目錄下所有程式
            //foreach (FileInfo fs in ds.GetFiles())
            //{                
            //    fs.Delete();
            //}

            //標楷體
            //BaseFont kaiuFont = BaseFont.CreateFont(kaiuPath + ",1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            BaseFont kaiuFont = BaseFont.CreateFont("C:/Windows/Fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //TT13
            //String tt13Path = Server.MapPath("~/doc/TT13.TTF");
            //BaseFont tt13Font = BaseFont.CreateFont(tt13Path, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            BaseFont tt13Font = BaseFont.CreateFont("C:/Windows/Fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            iTextSharp.text.Font chTitleFontC = new iTextSharp.text.Font(tt13Font, 26, iTextSharp.text.Font.BOLD); //標題
            iTextSharp.text.Font TitleFontC = new iTextSharp.text.Font(kaiuFont, 18); //表格中的欄位名稱
            iTextSharp.text.Font chTitleFontSmall = new iTextSharp.text.Font(kaiuFont, 12);
            iTextSharp.text.Font chFontC = new iTextSharp.text.Font(kaiuFont, 14);  //表格中料
            iTextSharp.text.Font contextFontC = new iTextSharp.text.Font(kaiuFont, 12); //附註
            iTextSharp.text.Font contextFootC = new iTextSharp.text.Font(kaiuFont, 20); //日期

            
            try{


                PdfWriter PdW = PdfWriter.GetInstance(Doc1, Memory);
                Doc1.Open();     //#region 第一頁
                Doc1.SetMargins(0, 0, 10, 10);
                Doc1.SetPageSize(PageSize.A4);
                Doc1.NewPage();

                P = new Paragraph("\n", TitleFontC);
                P.Alignment = Element.ALIGN_LEFT;
                Doc1.Add(P);

                GetSettings gs = new GetSettings();
                gs.Execute();
                string year, semester;

                if (vApplyAudit != null && !String.IsNullOrEmpty(vApplyAudit.AppYear.ToString().Trim()) && !String.IsNullOrEmpty(vApplyAudit.AppSemester.ToString().Trim()))
                {
                    year = vApplyAudit.AppYear.ToString().Trim();
                    semester = vApplyAudit.AppSemester.ToString().Trim();
                }
                else
                {
                    year = gs.GetYear().ToString().Trim();
                    semester = gs.GetSemester().ToString().Trim();
                }
                String title = "臺北醫學大學 " + year + "學年度 第" + semester + "學期 教師履歷表 ";
                P = new Paragraph(10f, title, TitleFontC);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                P = new Paragraph("\n", TitleFontC);
                P.Alignment = Element.ALIGN_LEFT;
                Doc1.Add(P);

                table = new PdfPTable(new float[] { 2, 2, 2, 2, 2 ,4}) { WidthPercentage = 90 }; //width                 


                //Image title_image = Image.GetInstance(ImagesDir + "title_dropout.png"); //照片
                //title_image.ScaleToFit(950, 25);
                //應徵單位

                C1 = new Chunk("應徵單位 ", chFontC);

                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                String untName =  crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows.Count == 0 ? "" : crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                C1 = new Chunk(untName, chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 2;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                //應徵職稱/職別            

                C1 = new Chunk("應徵職稱\n/職別", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 2f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                
                String jobTitle = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);
                String jobType = vApplyAudit.AppJobTypeNo.Equals("1") ? "專任" : "兼任";
                C1 = new Chunk(jobTitle + "\n" + jobType, chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop =2f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                //照片
                VEmployeeBase emp = crudObject.GetEmpBaseObjByEmpSn("" + vApplyAudit.EmpSn);
                if (!emp.EmpPhotoUploadName.Equals(""))
                {
                    try
                    {
                        iTextSharp.text.Image photo_image = iTextSharp.text.Image.GetInstance(ImagesDir + emp.EmpPhotoUploadName); //
                        PCell = new PdfPCell(photo_image, true);
                        //photo_image.ScalePercent(10f);
                        photo_image.ScaleToFit(5f, 5f);
                    }
                    catch
                    {

                    }
                }
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.Rowspan = 4;
                table.AddCell(PCell);



                //中文姓名

                C1 = new Chunk("中文姓名", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                String nameCN = emp.EmpNameCN;
                C1 = new Chunk(nameCN, chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 2;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                //英文姓名

                C1 = new Chunk("英文姓名", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                table.AddCell(PCell);

                String nameENG = emp.EmpNameENFirst + " " + emp.EmpNameENLast;
                C1 = new Chunk(nameENG, chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 2f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                //新聘類型
                C1 = new Chunk("新聘類型", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                String nameKind = crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows.Count == 0 ? "" : crudObject.GetAuditAttributeName(vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo).Rows[0][1].ToString();
                C1 = new Chunk(nameKind, chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 4;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 5f;
                table.AddCell(PCell);

                //適用法規
                C1 = new Chunk("適用法規", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 10f;
                table.AddCell(PCell);

                String[] num = { "零", "一", "二", "三", "四", "五" };
                //String strLaw = "依教師聘任升等實施辦法第(二)條第";
                String strLaw = "";
                //法規第幾項
                if (!vApplyAudit.AppJobTitleNo.ToString().Equals("") && !vApplyAudit.AppJobTitleNo.ToString().Equals("請選擇"))
                {
                    if (vApplyAudit.AppJobTitleNo.ToString().Equals("030400"))
                    {
                        strLaw = "依臨床教師聘任辦法第(三)條第";
                        strLaw += num[1] + "項 第";
                    }
                    if (vApplyAudit.AppJobTitleNo.ToString().Equals("040400"))
                    {
                        strLaw = "依臨床教師聘任辦法第(四)條第";
                        strLaw += num[1] + "項 第";
                    }
                    if (vApplyAudit.AppJobTitleNo.ToString().Equals("050400"))
                    {
                        strLaw = "依臨床教師聘任辦法第(五)條第";
                        strLaw += num[1] + "項 第";
                    }
                    if (vApplyAudit.AppJobTitleNo.ToString().Equals("060400"))
                    {
                        strLaw = "依臨床教師聘任辦法第(六)條第";
                        strLaw += num[0] + "項 第";
                    }
                    if (!vApplyAudit.AppJobTitleNo.ToString().Equals("030400") &&
                        !vApplyAudit.AppJobTitleNo.ToString().Equals("040400") &&
                        !vApplyAudit.AppJobTitleNo.ToString().Equals("050400") &&
                        !vApplyAudit.AppJobTitleNo.ToString().Equals("060400"))
                    {
                        strLaw = "依教師聘任升等實施辦法第(二)條第";
                        strLaw += num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.Substring(1, 1)))] + "項 第";
                    }
                }
                //strLaw += num[(7 - Int32.Parse(vApplyAudit.AppJobTitleNo.ToString().Substring(1, 1)))] + "項第";
            //}
                //載入法規依據第幾款
                strLaw += num[Int32.Parse(vApplyAudit.AppLawNumNo.Equals("") ? "0" : vApplyAudit.AppLawNumNo)] + "款";
                strLaw += "\n";
                //撈取法規內容
                strLaw += crudObject.GetTeacherLaw(vApplyAudit.AppKindNo, vApplyAudit.AppJobTitleNo, vApplyAudit.AppLawNumNo);

                C1 = new Chunk(strLaw, chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 4;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.PaddingTop = 10f;
                PCell.PaddingBottom = 10f;
                table.AddCell(PCell);

                C1 = new Chunk("學歷", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 5f;
                PCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(PCell);

                //學歷資料表
                tableChild1 = new PdfPTable(new float[] { 2, 2, 1, 2 }) { WidthPercentage = 75 }; //width                 

                C1 = new Chunk("學校名稱", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("院系所", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("學位", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("起迄年月", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                //SELECT 0EduSn, 1EduLocal, 2EduSchool, 3EduDepartment, 4EduDegree, 5EduDegreeType, 6EduStartYM, 7EduEndYM FROM [TeacherEdu] Where EmpSn =" + empSn;
                String DegreeType = "";
                DataTable dt = crudObject.GetAllVTeacherEduByEmpSn(emp.EmpSn);
                if (dt.Rows.Count <= 0)
                {
                    C1 = new Chunk("尚無填寫資料。", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 6;
                    PCell.MinimumHeight = 20f;
                    PCell.VerticalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //學校名稱
                        C1 = new Chunk(dt.Rows[i][2].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);

                        //部門名稱
                        C1 = new Chunk(dt.Rows[i][3].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);

                        //50 60 70
                        //if (dt.Rows[i][4].Equals("50")) DegreeType = "學士";
                        //if (dt.Rows[i][4].Equals("60")) DegreeType = "碩士";
                        //if (dt.Rows[i][4].Equals("70")) DegreeType = "博士";

                        if (dt.Rows[i][4].Equals("20")) DegreeType = "二技";
                        if (dt.Rows[i][4].Equals("30")) DegreeType = "四技";
                        if (dt.Rows[i][4].Equals("50")) DegreeType = "學士";
                        if (dt.Rows[i][4].Equals("60")) DegreeType = "碩士";
                        if (dt.Rows[i][4].Equals("70")) DegreeType = "博士";
                        if (dt.Rows[i][4].Equals("80")) DegreeType = "榮譽博士";
                        if (dt.Rows[i][4].Equals("90")) DegreeType = "博士後研究";
                        C1 = new Chunk(DegreeType, chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);

                        //時間起迄
                        C1 = new Chunk(dt.Rows[i][6].ToString() + "~" + dt.Rows[i][7].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);
                    }
                }

                PCell = new PdfPCell(tableChild1);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 0f;
                table.AddCell(PCell); //資料加入Table


                C1 = new Chunk("現職", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 5f;
                PCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(PCell);

                if (emp.EmpNowJobOrg == null || emp.EmpNowJobOrg == "")
                {
                    C1 = new Chunk("尚無填寫資料。", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 6;
                    PCell.MinimumHeight = 30f;
                    PCell.VerticalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    table.AddCell(PCell);
                }
                else
                {
                    C1 = new Chunk(emp.EmpNowJobOrg, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 6;
                    PCell.MinimumHeight = 30f;
                    PCell.VerticalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    table.AddCell(PCell);
                }

                C1 = new Chunk("經歷", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 5f;
                PCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(PCell);

                //學歷資料表
                tableChild1 = new PdfPTable(new float[] { 2, 2, 1, 1 }) { WidthPercentage = 75 }; //width                 

                C1 = new Chunk("服務機關", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("服務部門/系所", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("職稱", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);


                C1 = new Chunk("起迄年月", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);



                //0ExpSn, 1EmpSn, 2ExpOrginization,  3ExpUnit, 4ExpJobTitle,5ExpStartYM, 6ExpEndYM
                dt = crudObject.GetAllVTeacherExpByEmpSn(emp.EmpSn);
                if (dt.Rows.Count <= 0)
                {
                    C1 = new Chunk("尚無填寫資料。", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 6;
                    PCell.MinimumHeight = 30f;
                    PCell.VerticalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);
                }
                else
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //服務機關
                        C1 = new Chunk(dt.Rows[i][2].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);

                        //服務部門/系所
                        C1 = new Chunk(dt.Rows[i][3].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);

                        //職稱
                        C1 = new Chunk(dt.Rows[i][4].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);

                        //起訖年月
                        C1 = new Chunk(dt.Rows[i][5].ToString() + "~" + dt.Rows[i][6].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);
                    }
                }

                PCell = new PdfPCell(tableChild1);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 0f;
                table.AddCell(PCell); //資料加入Table


                //專長
                C1 = new Chunk("專長", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 5f;
                PCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(PCell);

                if ((vApplyAudit.AppStatus.ToString() == "False" && vApplyAudit.AppStep.ToString() == "0")||emp.EmpExpertResearch == null || emp.EmpExpertResearch == "" )
                {
                    C1 = new Chunk("尚無填寫資料。", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 6;
                    PCell.MinimumHeight = 30f;
                    PCell.VerticalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    table.AddCell(PCell);
                }
                else
                {
                    C1 = new Chunk(emp.EmpExpertResearch, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 6;
                    PCell.MinimumHeight = 30f;
                    PCell.VerticalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    table.AddCell(PCell);
                }
                //學術獎勵、榮譽事項
                C1 = new Chunk("學術獎勵、榮譽事項", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 30f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 5f;
                PCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(PCell);

                //
                tableChild1 = new PdfPTable(new float[] { 9, 1 }) { WidthPercentage = 75 }; //width                 

                C1 = new Chunk("描述內容", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("日期", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 3f;
                tableChild1.AddCell(PCell);

                dt = crudObject.GetAllVTeacherHonourByEmpSn(emp.EmpSn);
                if ((vApplyAudit.AppStatus.ToString() == "False" && vApplyAudit.AppStep.ToString() == "0") || dt.Rows.Count <= 0)
                {
                    C1 = new Chunk("尚無填寫資料。", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan =6;
                    PCell.MinimumHeight = 20f;
                    PCell.VerticalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //描述內容
                        C1 = new Chunk(dt.Rows[i][0].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);

                        //日期
                        C1 = new Chunk(dt.Rows[i][1].ToString(), chFontC);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.HorizontalAlignment = 1;
                        PCell.PaddingTop = 3f;
                        tableChild1.AddCell(PCell);
                    }
                }

                PCell = new PdfPCell(tableChild1);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.MinimumHeight = 20f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 0f;
                table.AddCell(PCell); //資料加入Table
                Doc1.Add(table);

                string SaveFileFullPath = WebForm.Server.MapPath(fileDownPath + emp.EmpNameCN + "_履歷表.pdf");
                Doc1.Close();
                byte[] content = Memory.ToArray();
                using (FileStream fs = File.Create(SaveFileFullPath))
                {
                    fs.Write(content, 0, (int)content.Length);
                    fs.Flush();
                    fs.Close();
                }
                Memory.Close();
                PDFToStream(WebForm, SaveFileFullPath);
            }
            catch (Exception ex)
            {
               strErrMsg = ex.Message.ToString();
               //WebForm.Response.Write("Error : " + ex.Message);
            }
        }

        private void PDFToStream(System.Web.UI.Page WebForm, string pdfpath)
        {
            //得到文件名.
            string filename = System.IO.Path.GetFileName(pdfpath);

            System.IO.Stream iStream = null;

            //以10K為單位暫存:
            byte[] buffer = new Byte[10000];

            int length;

            long dataToRead;
            try
            {
                // 打開文件.
                iStream = new System.IO.FileStream(pdfpath, System.IO.FileMode.Open,
                            System.IO.FileAccess.Read, System.IO.FileShare.Read);


                // 得到文件大小:
                dataToRead = iStream.Length;
                WebForm.Response.ContentType = "application/x-rar-compressed";
                WebForm.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename));

                while (dataToRead > 0)
                {
                    //保証client連接
                    if (WebForm.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);

                        WebForm.Response.OutputStream.Write(buffer, 0, length);

                        WebForm.Response.Flush();

                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //结束循環
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // error
                WebForm.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    //關閉文件
                    iStream.Close();
                }
            }
        }
    }
}