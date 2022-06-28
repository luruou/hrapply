using System;
using System.Web;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.events;
using iTextSharp.text.error_messages;
using System.IO;

/// <summary>
/// UserData 的摘要描述 
/// 此表為新聘使用
/// </summary>
/// 

namespace ApplyPromote
{
    public class ThesisScore1PDF
    {
        static CRUDObject crudObject;
        public ThesisScore1PDF()
        {
            crudObject = new CRUDObject();
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public void Output(System.Web.UI.Page WebForm, int appSn, ref string strErrMsg)
        {
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

            //以AppSn取得填寫資料
            vApplyAudit = crudObject.GetApplyAuditObjByAppSn(appSn);

            //CREATING OBJECTS OF PDF AND DOCUMENT
            string SaveFileDir = WebForm.Server.MapPath("~/pdf/");
            string ImageTitle = WebForm.Server.MapPath("~/image/sci.png");
            DirectoryInfo ds = new DirectoryInfo(SaveFileDir);
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
            iTextSharp.text.Font TitleFontC = new iTextSharp.text.Font(kaiuFont, 16, iTextSharp.text.Font.BOLD); //表格中的欄位名稱
            iTextSharp.text.Font FontC13 = new iTextSharp.text.Font(kaiuFont, 13, iTextSharp.text.Font.BOLD); //表格中的欄位名稱
            iTextSharp.text.Font FontC12 = new iTextSharp.text.Font(kaiuFont, 12, iTextSharp.text.Font.BOLD); //表格中的欄位名稱
            iTextSharp.text.Font FontC12NoBold = new iTextSharp.text.Font(kaiuFont, 12, iTextSharp.text.Font.DEFAULTSIZE); //表格中的欄位名稱
            iTextSharp.text.Font RedTextFont = new iTextSharp.text.Font(kaiuFont, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED);
            iTextSharp.text.Font BlueTextFont = new iTextSharp.text.Font(kaiuFont, 12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE);
            iTextSharp.text.Font chTitleFontSmall = new iTextSharp.text.Font(kaiuFont, 12);
            iTextSharp.text.Font chFontC = new iTextSharp.text.Font(kaiuFont, 16);  //表格中料
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
                String title = "臺北醫學大學教師研究論文歸類計分表";
                P = new Paragraph(10f, title, TitleFontC);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                P = new Paragraph("\n", TitleFontC);
                P.Alignment = Element.ALIGN_LEFT;
                Doc1.Add(P);

                VEmployeeBase emp = crudObject.GetEmpBaseObjByEmpSn("" + vApplyAudit.EmpSn);


                
                P = new Paragraph(10f, "系所單位：", FontC13);
                P.Alignment = Element.ALIGN_LEFT;

                String untName = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows.Count == 0 ? "" : crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                C1 = new Chunk(untName, FontC13);
                C1.SetUnderline(0.2f, -2f);
                P.Add(C1);

                C1 = new Chunk("  姓名：", FontC13);
                P.Add(C1);

                String nameCN = emp.EmpNameCN;
                C1 = new Chunk(nameCN, FontC13);
                C1.SetUnderline(0.2f, -2f);
                P.Add(C1);

                C1 = new Chunk("  申請教職等：", FontC13);
                P.Add(C1);

                String jobTitle = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);
                C1 = new Chunk(jobTitle, FontC13);
                C1.SetUnderline(0.2f, -2f);
                P.Add(C1);
                P.IndentationLeft = 20f;
                Doc1.Add(P);

                P = new Paragraph("\n一、論文積分：", FontC12);
                C1 = new Chunk(vApplyAudit.AppThesisAccuScore, FontC12);
                C1.SetUnderline(0.2f, -2f);
                P.Add(C1);
                P.IndentationLeft = 20f;
                P.IndentationRight = 20f;
                Doc1.Add(P);


                P = new Paragraph("", TitleFontC);
                C1 = new Chunk("依本校教師著作升等研究部分最低標準施行要點規定，以本校最新版之論文歸類計分(研究表現指數計分表)方式行之；五年內", FontC12);
                P.Add(C1);
                C1 = new Chunk("", RedTextFont); //(2013.2.1 迄今)
                P.Add(C1);
                C1 = new Chunk("且", FontC12);
                P.Add(C1);
                C1 = new Chunk("符合前職等教師資格之論文計分，最高採計15篇。", RedTextFont);
                P.Add(C1);
                C1 = new Chunk("(其中若有文章2016年底前接受發表於本校所發行之國際期刊(JECM)者，可提一篇其計分方式以學術論文刊登雜誌加權分數(J)=4分計算)", FontC12);
                P.Add(C1);
                P.IndentationLeft = 20f;
                P.IndentationRight = 20f;
                Doc1.Add(P);

                P = new Paragraph("\n二、RPI：", FontC12);
                C1 = new Chunk(vApplyAudit.AppRPIScore, FontC12);
                C1.SetUnderline(0.2f, -2f);
                P.Add(C1);
                P.IndentationLeft = 20f;

                C1 = new Chunk("\n依國科會生物處研究年資標準計算RPI\n", RedTextFont);
                P.Add(C1);

                C1 = new Chunk("1.滿5年以上(選最佳7篇)\n", FontC12);
                P.Add(C1);
                C1 = new Chunk("2.滿4年(選最佳4篇)\n", FontC12);
                P.Add(C1);
                C1 = new Chunk("3滿3年(選最佳2篇)\n", FontC12);
                P.Add(C1);
                C1 = new Chunk("4.未滿3年(選最佳1篇)\n", FontC12);
                P.Add(C1);
                P.IndentationLeft = 20f;
                Doc1.Add(P); 

                P = new Paragraph(" ", FontC12);
                P.Alignment = Element.ALIGN_LEFT;
                P.IndentationLeft = 20f;
                Doc1.Add(P);


                DataTable dtA  = crudObject.GetThesisScoreCount(emp.EmpIdno);

                table = new PdfPTable(new float[] {2}) { WidthPercentage = 90 }; //width                 

                //學歷資料表
                tableChild1 = new PdfPTable(new float[] { 2, 2, 2, 2, 2 }) { WidthPercentage = 90 }; //width                

                if (dtA.Rows.Count > 0)
                {
                    //（表A）
                    PG = new Phrase();
                    C1 = new Chunk("（表A）\n\n", FontC13);
                    P.Add(C1);
                    P.Alignment = Element.ALIGN_CENTER;
                    P.IndentationLeft = 10f;
                    Doc1.Add(P);

                    //讀取表(A)資料

                    //圖片
                    if (!emp.EmpPhotoUploadName.Equals(""))
                    {
                        iTextSharp.text.Image photo_image = iTextSharp.text.Image.GetInstance(ImageTitle); //
                        PCell = new PdfPCell(photo_image, true);
                        //photo_image.ScalePercent(10f);
                        //photo_image.ScaleToFit(5f, 5f);
                        PCell.Colspan = 5;
                        PCell.VerticalAlignment = 1;
                        PCell.HorizontalAlignment = 1;
                        PCell.Rowspan = 1;
                        tableChild1.AddCell(PCell);
                    }


                    String txt_FSCI, txt_FSSCI, txt_FSEI, txt_FNSCI, txt_FSOther, txt_NFCSCI, txt_NFCSSCI, txt_NFCEI, txt_NFCNSCI, txt_NFCOther = "";
                    String txt_NFOCSCI, txt_NFOCSSCI, txt_NFOCEI, txt_NFOCNSCI, txt_NFOCOther = "";
                    String txt_SCI, txt_SSCI, txt_EI, txt_NSCI, txt_Others = "";
                    //載入SCI Score
                    txt_FSCI = dtA.Rows[0]["PT_FSCI"].ToString();
                    txt_FSSCI = dtA.Rows[0]["PT_FSSCI"].ToString();
                    txt_FSEI = dtA.Rows[0]["PT_FEI"].ToString();
                    txt_FNSCI = dtA.Rows[0]["PT_FNSCI"].ToString();
                    txt_FSOther = dtA.Rows[0]["PT_FOther"].ToString();

                    txt_NFCSCI = dtA.Rows[0]["PT_NFCSCI"].ToString();
                    txt_NFCSSCI = dtA.Rows[0]["PT_NFCSSCI"].ToString();
                    txt_NFCEI = dtA.Rows[0]["PT_NFCEI"].ToString();
                    txt_NFCNSCI = dtA.Rows[0]["PT_NFCNSCI"].ToString();
                    txt_NFCOther = dtA.Rows[0]["PT_NFCOther"].ToString();

                    txt_NFOCSCI = dtA.Rows[0]["PT_NFOCSCI"].ToString();
                    txt_NFOCSSCI = dtA.Rows[0]["PT_NFOCSSCI"].ToString();
                    txt_NFOCEI = dtA.Rows[0]["PT_NFOCEI"].ToString();
                    txt_NFOCNSCI = dtA.Rows[0]["PT_NFOCNSCI"].ToString();
                    txt_NFOCOther = dtA.Rows[0]["PT_NFOCOther"].ToString();

                    txt_SCI = Convert.ToString(Convert.ToUInt16(txt_FSCI) + Convert.ToUInt16(txt_NFCSCI) + Convert.ToUInt16(txt_NFOCSCI));
                    txt_SSCI = Convert.ToString(Convert.ToUInt16(txt_FSSCI) + Convert.ToUInt16(txt_NFCSSCI) + Convert.ToUInt16(txt_NFOCSSCI));
                    txt_EI = Convert.ToString(Convert.ToUInt16(txt_FSEI) + Convert.ToUInt16(txt_NFCEI) + Convert.ToUInt16(txt_NFOCEI));
                    txt_NSCI = Convert.ToString(Convert.ToUInt16(txt_FNSCI) + Convert.ToUInt16(txt_NFCNSCI) + Convert.ToUInt16(txt_NFOCNSCI));
                    txt_Others = Convert.ToString(Convert.ToUInt16(txt_FSOther) + Convert.ToUInt16(txt_NFCOther) + Convert.ToUInt16(txt_NFOCOther));




                    C1 = new Chunk("第一作者\n論文篇數", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);


                    C1 = new Chunk(txt_FSCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_FSSCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_FSEI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);


                    C1 = new Chunk(txt_FSOther, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk("非第一作者之通訊作者論文篇數", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);


                    C1 = new Chunk(txt_NFCSCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_NFCSSCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_NFCEI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);


                    C1 = new Chunk(txt_NFCOther, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk("非第一或通訊作者之其他序位作者論文篇數", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_NFOCSCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_NFOCSSCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_NFOCEI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_NFOCOther, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk("總篇(件)數\n(以上三項總和)", chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_SCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_SSCI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);

                    C1 = new Chunk(txt_EI, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);


                    C1 = new Chunk(txt_Others, chFontC);
                    PG = new Phrase();
                    PG.Add(C1);
                    PCell = new PdfPCell(PG);
                    PCell.Border = iTextSharp.text.Rectangle.BOX;
                    PCell.Colspan = 1;
                    PCell.MinimumHeight = 40f;
                    PCell.VerticalAlignment = 1;
                    PCell.HorizontalAlignment = 1;
                    PCell.PaddingTop = 10f;
                    tableChild1.AddCell(PCell);
                }

                C1 = new Chunk("SCI 、SSCI、EI之期刊論文資料，可就近至各大學圖書館、國科會科技政策研究與資訊中心等查閱或上網檢索。\n上述SCI、SSCI及EI期刊資料以最新版本為準。\n\n", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 5;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);
                Doc1.Add(tableChild1);


                P = new Paragraph("\n", FontC12);
                P.Alignment = Element.ALIGN_LEFT;
                Doc1.Add(P);

                DataTable dt = crudObject.GetThesisScoreList("" + appSn.ToString());
                //研究成果
                tableChild1 = new PdfPTable(new float[] { 1, 1, 8, 1, 1, 1, 1, 1, 3 }) { WidthPercentage = 90 }; //width     
                if (dt != null && dt.Rows.Count>0)
                {
                    //（表B）
                    PG = new Phrase();
                C1 = new Chunk("（表B）\n\n", FontC13);
                P.Add(C1);
                P.Alignment = Element.ALIGN_CENTER;
                P.IndentationLeft = 20f;
                Doc1.Add(P); 
            

                C1 = new Chunk("序號", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);


                C1 = new Chunk("成果類別代碼", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);


                C1 = new Chunk("代表性研究成果名稱", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("論文發表年月", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("論文性質分數(C)", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("刊登雜誌分類分數(J)", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("作者排名分數(A)", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("總分數", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("上傳檔案", chFontC);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = iTextSharp.text.Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 40f;
                PCell.VerticalAlignment = 1;
                PCell.HorizontalAlignment = 1;
                PCell.PaddingTop = 10f;
                tableChild1.AddCell(PCell);


                //0ExpSn, 1EmpSn, 2ExpOrginization,  3ExpUnit, 4ExpJobTitle,5ExpStartYM, 6ExpEndYM
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //序號 //SELECT ThesisSn, SnNo, RRNo, ThesisResearchResult, ThesisPublishYearMonth, ThesisC, ThesisJ, ThesisA, ThesisTotal, ThesisName, ThesisUploadName FROM ThesisScore WHERE (EmpSn = '2169')
                        C1 = new Chunk(dt.Rows[i][1].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //成果類別代碼
                        C1 = new Chunk(dt.Rows[i][2].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 20f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //代表性研究成果名稱
                        C1 = new Chunk(dt.Rows[i][3].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 40f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //論文發表年月
                        C1 = new Chunk(dt.Rows[i][4].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 40f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //論文性質分數(C)
                        C1 = new Chunk(dt.Rows[i][5].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 40f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //刊登雜誌分類分數(J)
                        C1 = new Chunk(dt.Rows[i][6].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 40f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //作者排名分數(A)
                        C1 = new Chunk(dt.Rows[i][7].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 40f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //總分數
                        C1 = new Chunk(dt.Rows[i][8].ToString(), ChFontSmall);
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 40f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);

                        //上傳檔案名稱
                        C1 = new Chunk(dt.Rows[i][9].ToString(), BlueTextFont);
                        C1.SetAnchor("http://hr2sys.tmu.edu.tw/DocuFile/HRApplyDoc/" + emp.EmpSn + "/" + dt.Rows[i][10].ToString());
                        PG = new Phrase();
                        PG.Add(C1);
                        PCell = new PdfPCell(PG);
                        PCell.Border = iTextSharp.text.Rectangle.BOX;
                        PCell.Colspan = 1;
                        PCell.MinimumHeight = 40f;
                        PCell.VerticalAlignment = 1;
                        PCell.PaddingTop = 10f;
                        tableChild1.AddCell(PCell);
                    }

                    Doc1.Add(tableChild1);
                }

                string SaveFileFullPath = WebForm.Server.MapPath(fileDownPath + emp.EmpNameCN + "_論文積分表.pdf");
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