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

/// <summary>
/// UserData 的摘要描述
/// </summary>
/// 

namespace ApplyPromote
{
    public class WritePDF
    {
        static CRUDObject crudObject;
        public WritePDF()
        {
            crudObject = new CRUDObject();
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public void Output(System.Web.UI.Page WebForm, ArrayList arrayList, bool DownloadFlag, ref string strErrMsg)
        {
            string Paper_Name = "";
            string strFileName = "";
            string strTemp;
            string fileDownPath = "~/DownloadPdf";
            //string Path = Server.MapPath("pdf");  
            BaseFont btChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\MINGLIU.TTC,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            //BaseFont btKaiuChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.fft", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            BaseFont btKaiuChinese = BaseFont.CreateFont(WebForm.Server.MapPath("~/Pdf/kaiu.ttf") , BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            BaseFont btNewRoman = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\TIMES.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font ChFont = new Font(btKaiuChinese, 12);
            Font ChFontBlue = new Font(btKaiuChinese, 12, Font.NORMAL, new BaseColor(51, 0, 15));
            Font ChFontHeader = new Font(btKaiuChinese, 20);
            Font ChFontTitle = new Font(btKaiuChinese, 16, Font.BOLD);
            Font ChFontText = new Font(btKaiuChinese, 14);
            Paragraph P;
            PdfPTable table;
            PdfPTable tableChild1;
            string strDate;
            string strDate2;
            Chunk C1;
            Chunk C2;
            Chunk C3;
            PdfPCell PCell;
            Phrase PG;
            VApplyAudit vApplyAudit;
            VAppendDegree vAppendDegree;
            string AuditAppJobTitle = "";
            string AuditAppName = "";
            string AuditAppUnit = "";
            string AuditAppPublication = "";

            DirectoryInfo ds = new DirectoryInfo(WebForm.Server.MapPath(fileDownPath));
            //刪除目錄下所有程式
            foreach (FileInfo fs in ds.GetFiles())
            {
                fs.Delete();
            }

            Document Doc1 = new Document(PageSize.A4, 50, 50, 20, 20);
            MemoryStream Memory = new MemoryStream();
            int i = 0;
            for (i =0; i < arrayList.Count; i++)
            {
                i++;
                VAuditExecute vAuditExecuteOutter = (VAuditExecute) arrayList[i];
                vApplyAudit = crudObject.GetApplyAuditObjByAppSn(vAuditExecuteOutter.AppSn);
                Memory = new MemoryStream();
                PdfWriter PdW = PdfWriter.GetInstance(Doc1, Memory);

                Doc1.SetPageSize(PageSize.A4);
                Doc1.NewPage();
                //region 第一頁

                Doc1.Open();
                Doc1.SetMargins(50, 50, 50, 50);

                Doc1.SetPageSize(PageSize.A4);
                Doc1.NewPage();
                P = new Paragraph(10f, vAuditExecuteOutter.ARTitleName.ToString() + "(甲表)", ChFontHeader);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                //第一行欄位 送審等級 姓名 學院系所
                tableChild1 = new PdfPTable(new float[] { 1, 1, 1, 1, 3 });


                C1 = new Chunk("送審等級", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                //應徵職稱 送審等級
                AuditAppJobTitle = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);
                C1 = new Chunk(AuditAppJobTitle, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("姓名", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                //姓名
                AuditAppName = crudObject.GetEmpBsaseObjByEmpSn(""+vApplyAudit.EmpSn).EmpNameCN;
                C1 = new Chunk(AuditAppName, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("學院系所", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.MinimumHeight = 35f;
                PCell.Colspan = 3;
                tableChild1.AddCell(PCell);

                //學院系所
                AuditAppUnit = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                C1 = new Chunk(AuditAppUnit, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("學位論文", ChFontTitle);
                C2 = new Chunk("名稱", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PG.Add(C2);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                //代表著作--學位送審 (怪)
                vAppendDegree = crudObject.GetAppendDegreeByAppSn(vApplyAudit.AppSn);
                if (vAppendDegree != null)
                {

                    if (vAppendDegree.AppDDegreeThesisName != null && !vAppendDegree.AppDDegreeThesisName.Equals(""))
                    {
                        AuditAppPublication = vAppendDegree.AppDDegreeThesisName;
                    }
                }

                if (AuditAppPublication.Trim().Equals(""))
                {
                    //取出上傳論文中的代表
                    crudObject.GetVThesisScoreRepresentative(vApplyAudit.AppSn);
                    AuditAppPublication = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                }
                //學位論文
                C1 = new Chunk(AuditAppPublication, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 7;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("審查意見(本頁僅提供本校評審用，請儘量以電腦打字呈現，意見以一百至三百字為原則)", ChFontText);
                C2 = new Chunk(vAuditExecuteOutter.ExecuteCommentsA, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PG.Add(C2);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 8;
                PCell.Rowspan = 20;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("著作評分項目：包括研究主題、文字與結構、研究方法及參考資料、學術或應用價值等項。", ChFontText);
                C2 = new Chunk("【＊註：如有五年內且前一等級至本次申請等級時個人學術與專業之整體成就可參考納入評分】", ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PG.Add(C2);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 8;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);


                C1 = new Chunk("總 分", ChFontTitle);
                C2 = new Chunk("【七十分及格】", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PG.Add(C2);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 2;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk(vAuditExecuteOutter.ExecuteAllTotalScore, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 2;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("審查人簽章", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 2;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk(vAuditExecuteOutter.ExecuteRoleName, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 2;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("審畢日期", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 2;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk(vAuditExecuteOutter.ExecuteDate.Year + " 年 " + vAuditExecuteOutter.ExecuteDate.Month + " 月 " + vAuditExecuteOutter.ExecuteDate.DayOfYear + " 日 ", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 6;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                P = new Paragraph(100f, "※審查評定基準：1.副教授：應在該學術領域內有持續性著作並有具體之貢獻者。", ChFontText);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                P = new Paragraph(100f, "                2.助理教授：應有相當於博士論文水準之著作並有獨立研究之能力者。", ChFontText);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                P = new Paragraph(100f, "                3.講師：應有相當於碩士論文水準之著作。", ChFontText);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                P = new Paragraph(100f, "※附註：1.以整理、增刪、組合或編排他人著作而成之編著不得送審。", ChFontText);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                P = new Paragraph(100f, "        2.以教育人員任用條例第30 條之1 送審副教授可以博士論文送審，惟仍須符合修正分級後副教授水準。", ChFontText);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                //第二頁
                Doc1.NewPage();
                P = new Paragraph(10f, vAuditExecuteOutter.ARTitleName.ToString() + "(乙表)", ChFontHeader);
                P.Alignment = Element.ALIGN_CENTER;
                Doc1.Add(P);

                //第一行欄位 送審等級 姓名 學院系所
                tableChild1 = new PdfPTable(new float[] { 1, 1, 1, 1, 3 });


                C1 = new Chunk("送審等級", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                //應徵職稱 送審等級
                AuditAppJobTitle = crudObject.GetJobTitleName(vApplyAudit.AppJobTitleNo);
                C1 = new Chunk(AuditAppJobTitle, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("姓名", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                //姓名
                AuditAppName = crudObject.GetEmpBsaseObjByEmpSn("" + vApplyAudit.EmpSn).EmpNameCN;
                C1 = new Chunk(AuditAppName, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("學院系所", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.MinimumHeight = 35f;
                PCell.Colspan = 3;
                tableChild1.AddCell(PCell);

                //學院系所
                AuditAppUnit = crudObject.GetUnitName(vApplyAudit.AppUnitNo).Rows[0][0].ToString();
                C1 = new Chunk(AuditAppUnit, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 1;
                PCell.MinimumHeight = 35f;

                C1 = new Chunk("審查意見(本頁僅提供送審人參考，係可公開文件，請儘量以電腦打字呈現)", ChFontText);
                C2 = new Chunk(vAuditExecuteOutter.ExecuteCommentsB, ChFontText);
                PG = new Phrase();
                PG.Add(C1);
                PG.Add(C2);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 8;
                PCell.Rowspan = 20;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("優 點", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 4;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                C1 = new Chunk("缺 點", ChFontTitle);
                PG = new Phrase();
                PG.Add(C1);
                PCell = new PdfPCell(PG);
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 4;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);
                
                string strStrength = StrengthWeakness(vAuditExecuteOutter.ExecuteStrengths, vApplyAudit.AppWayNo, vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo,"S");
                PCell = new PdfPCell(new Phrase(strStrength, ChFont));
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 4;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);

                string strWeakness = StrengthWeakness(vAuditExecuteOutter.ExecuteWeaknesses, vApplyAudit.AppWayNo, vApplyAudit.AppKindNo, vApplyAudit.AppAttributeNo, "W");
                PCell = new PdfPCell(new Phrase(strStrength, ChFont));
                PCell.Border = Rectangle.BOX;
                PCell.Colspan = 4;
                PCell.Rowspan = 1;
                PCell.MinimumHeight = 35f;
                tableChild1.AddCell(PCell);
                Doc1.Close();
            }

            
            byte[] content = Memory.ToArray();
            string SaveFileFullPath = WebForm.Server.MapPath(fileDownPath + AuditAppName + "_外審評核表" + ".pdf");
            using (FileStream fs = File.Create(SaveFileFullPath))
            {
                fs.Write(content, 0, (int)content.Length);
                fs.Flush();
                fs.Close();
            }

            if (DownloadFlag & arrayList.Count > 0)
            {
                WebForm.Response.Clear();
                //WebForm.Response.AddHeader("Content-Disposition", "attachment;filename=" + SaveFileFullPath);
                WebForm.Response.AddHeader(" Content-Length ", Memory.GetBuffer().Length.ToString());

                //WebForm.Response.ContentType = "Application/octet-stream";
                WebForm.Response.ContentType = "Application/pdf ";
                WebForm.Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
                WebForm.Response.OutputStream.Flush();
                WebForm.Response.OutputStream.Close();
                WebForm.Response.Flush();
                WebForm.Response.Close();
            }
        }

        String StrengthWeakness(string executeSW, string wayNo, string kindNo, string attributeNo, string type)
        {
            DataTable dtSW;
            Hashtable hashTable = new Hashtable();
            VStrengthsWeaknesses vStrengthsWeaknesses = new VStrengthsWeaknesses();
            vStrengthsWeaknesses.WaydNo = wayNo; //學術研究 教學實務 產學應用
            vStrengthsWeaknesses.KindNo = kindNo; // 改動態
            vStrengthsWeaknesses.AttributeNo = attributeNo;//vApplyAudit.AppAttributeNo;
            vStrengthsWeaknesses.SWType = type; //優點

            dtSW = crudObject.GetStrengthsWeaknesses(vStrengthsWeaknesses);

            for (int i = 0; i < dtSW.Rows.Count; i++)
            {
                if ((Boolean)dtSW.Rows[i][2])
                {
                    hashTable.Add(dtSW.Rows[i][1].ToString(), Convert.ToInt32(dtSW.Rows[i][0].ToString()));
                }

            }


            System.Web.UI.WebControls.ListItem listItem;
            string strItems = ""; // □■
            foreach (DictionaryEntry entry in hashTable)
            {
                listItem = new System.Web.UI.WebControls.ListItem(entry.Key.ToString(), entry.Value.ToString());
                if (!string.IsNullOrEmpty(executeSW))
                {
                    if (executeSW.IndexOf(entry.Value.ToString()) > -1)
                    {
                        strItems = "■ ";
                    }
                    else
                    {
                        strItems = "□ ";
                    }
                    strItems += entry.Key.ToString() + "\n";

                }
            }
            string other = executeSW.Split(',')[executeSW.Split(',').Length - 1];
            strItems += "其他：" + other + "\n";
            return strItems;
        }
    }
}