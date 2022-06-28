<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageAgree.aspx.cs" Inherits="MessageAgree" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="js/jquery.js" type="text/javascript"></script>
<script src="js/jquery-1.11.1.min.js" type="text/javascript"></script>
<script src="js/jquery.validate.min.js" type="text/javascript"></script>

     <style>
	img {
            image-rendering: -moz-crisp-edges;
            image-rendering: -o-crisp-edges;
            image-rendering: -webkit-optimize-contrast;
            image-rendering: crisp-edges;
            -ms-interpolation-mode: nearest-neighbor;
        }
    </style>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <script src="Scripts/jquery-3.3.1.js"></script>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.js"></script>
 <title>臺北醫學大學-教師新聘升等系統</title>
</head>
<body>

    <form id="form1" runat="server">
        <br />
        <div class='WordSection1' style="margin-left: 300px; margin-right: 300px">
            <h2 align='center' style='text-align: center; line-height: 20.0pt'><span
                style='font-family: 標楷體'><b>臺北醫學大學</b></span></h2>
            <h4 align='center' style='margin-bottom: 6.0pt; text-align: center'><span
                style='line-height: 300%; font-family: 標楷體'><b>個人資料取得聲明暨同意書</b></span></h4>

            <p class='MsoNormal' style='margin-bottom: 10.0pt; line-height: 17.0pt; font-size: 14.0pt'>
                <span
                    style='font-family: 標楷體'>臺北醫學大學</span><span lang="EN-US">(</span><span
                        style='font-family: 標楷體'>以下簡稱本校</span><span lang="EN-US">)</span><span
                            style='font-family: 標楷體'>依個人資料保護法之相關規定，將對您個人資料進行蒐集、處理或利用，依法告知您以下事項，為保障您的權益，請詳細閱讀本同意書所有內容。當您簽署本同意書時，表示您已閱讀、瞭解並同意接受本同意書之所有內容及其後修改變更規定。規範詳述如下：</span>
            </p>

            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 25.2pt; margin-bottom: .0001pt; text-indent: -25.2pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>一、<span style='font: 7.0pt "Times New Roman"'>
                </span></span><span style='font-family: 標楷體'>蒐集目的：進行人事管理，包括但不限於任用審核、薪資管理、績效考核、退休、訓練及發展計畫、安全衛生、門禁管制、申訴、醫療、保險、福利措施、建立人力資源管理系統及內部統計調查與分析等事項。</span>
            </p>

            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 40.8pt; margin-bottom: .0001pt; text-indent: -40.8pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>二、<span style='font: 7.0pt "Times New Roman"'>
                </span></span><span style='font-family: 標楷體'>蒐集個人資料類別及範圍
姓名、性別、出生日期、身分證統一編號、統一證號、金融帳戶、體檢資料、電話及地址、保險資料、成績、護照、家庭其他成員之細節、家庭情形、家長職業、信仰、財務情形、個性、工作許可文件、居留證明文件、活動休閒及興趣、職業、執照、學校紀錄、學歷或技術資格、職業專長、著作、學習訓練紀錄、受僱情形、僱用經過、離職經過、工作經驗、工作及差勤紀錄、健康與安全紀錄、薪資與預扣款、工作管理之細節、工作之評估細節、受訓紀錄、津貼、福利、贈款、保險細節、社會保險給付及其他退休給付、健康紀錄、其他裁判及行政處分、犯罪嫌疑資料、書面文件之檢索、未分類之資料等
(詳見個人資料類別代號：C001、C002、C003、C011、C012、C014、C021、C023、C024、C031、C033、C035、C038、C039、C040、C041、C051、C052、C054、C056、C057、C061、C062、C063、C064、C065、C066、C068、C070、C071、C072、C087、C088、C089、C111、C115、C116、C131、C132。)

                </span>
            </p>

            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 24.5pt; margin-bottom: .0001pt; text-indent: -24.5pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>三、<span style='font: 7.0pt "Times New Roman"'>
                </span></span><span style='font-family: 標楷體'>個人資料利用之期間及地區：在本校校務及附屬機構業務所及地區（含中華民國境內及未受主管機關禁止國際傳輸之境外地區）和校務存續期間，但依法令或法定職務得利用者，皆不在此限。</span>
            </p>

            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 24.5pt; margin-bottom: .0001pt; text-indent: -24.5pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>四、<span style='font: 7.0pt "Times New Roman"'>
                </span></span><span style='font-family: 標楷體'>個人資料之利用方式及對象:</span>
            </p>

            <p class='MsoListParagraph' style='margin-left: 25.1pt; text-indent: -.6pt; line-height: 17.0pt'>
                <span style='font-family: 標楷體'>1.	利用您的個人資料於本校於各項業務執行，包括因業務執行所必須進行之各項聯繫及通知、各項公文書函、作業文書登載需求、計劃管理考核所需之人員基本資料提供及對外部單位或對政府機關提供業務執行及聯繫必須之人員資料提供。</span>
            </p>

            <p class='MsoListParagraph' style='margin-left: 25.1pt; text-indent: -.6pt; line-height: 17.0pt'>
                <span style='font-family: 標楷體'>2.	利用您的個人資料於本校內部各項管理所需之登記及聯繫方式登載，包括各項資訊服務所需進行之個人聯繫資料登記，因業務需求所必需之內部通訊錄及緊急聯絡名單之建立。</span>
            </p>

            <p class='MsoListParagraph' style='margin-left: 25.1pt; text-indent: -.6pt; line-height: 17.0pt'>
                <span style='font-family: 標楷體'>3.	利用您的個人資料於本校內部各項人事行政管理、員工福利提供、保險或健康檢查、薪資酬勞發放或其他會計作業等必需之登記及申報事項。</span>
            </p>

            <p class='MsoListParagraph' style='margin-left: 25.1pt; text-indent: -.6pt; line-height: 17.0pt'>
                <span style='font-family: 標楷體'>4.	利用您的個人資料於政府機關、目的事業主管機關依其法定職掌以正式函文調閱時。</span>
            </p>

            <p class='MsoListParagraph' style='margin-left: 25.1pt; text-indent: -.6pt; line-height: 17.0pt'>
                <span style='font-family: 標楷體'>5.	利用您的個人資料於本校辦理之團體活動或內部員工自發性團體活動所需要之人員資料提供，包括因辦理保險、活動聯繫等或其他需以人員資料進行之登記等需求。</span>
            </p>

            <p class='MsoListParagraph' style='margin-left: 25.1pt; text-indent: -.6pt; line-height: 17.0pt'>
                <span style='font-family: 標楷體'>6.	利用於本校內部之申訴處理及相關人員權益及福利協調作業。</span>
            </p>


            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 24.5pt; margin-bottom: .0001pt; text-indent: -24.5pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>五、<span style='font: 7.0pt "Times New Roman"'>
                </span>
                </span><span style='font-family: 標楷體'>個人資料之權利及權益：您依法得行使個人資料保護法第3條之個人權利，但因本校執行職務或業務所必須者，本校得拒絕之。權利之行使方式請洽本校各單位聯絡窗口。若因您行使上述權利，而導致權益受損時，本校將不負相關賠償責任。</span>
            </p>


            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 24.5pt; margin-bottom: .0001pt; text-indent: -24.5pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>六、<span style='font: 7.0pt "Times New Roman"'>
                </span>
                </span><span style='font-family: 標楷體'>若您的個人資料有任何異動，請主動向本校申請更正，使其保持正確、最新及完整。若您提供錯誤、不實、過時或不完整或具誤導性的資料，將會損及您於本校之各項權益。</span>
            </p>

            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 24.5pt; margin-bottom: .0001pt; text-indent: -24.5pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>七、<span style='font: 7.0pt "Times New Roman"'>
                </span>
                </span><span style='font-family: 標楷體'>您瞭解此一同意書符合個人資料保護法及相關法規之要求，具有書面同意本校蒐集、處理及使用您的個人資料之效果，且同意本校留存此同意書，供日後取出查驗。</span>
            </p>

            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 24.5pt; margin-bottom: .0001pt; text-indent: -24.5pt; line-height: 17.0pt'>
                <span lang='EN-US' style='font-family: 標楷體'>八、<span style='font: 7.0pt "Times New Roman"'>
                </span>
                </span><span style='font-family: 標楷體'>本校保留隨時修改本同意書規範之權利，本校將於修改規範時，將於本校網頁公告修改之事實，不另作個別通知。如果您不同意修改的內容，請主動通知本校，否則將視為您已同意並接受本規範該等增訂或修改內容之拘束。</span>
            </p>

            <p class='MsoListParagraph' style='margin-top: 3.6pt; margin-right: 0cm; margin-bottom: 0cm; margin-left: 24.5pt; margin-bottom: .0001pt; text-indent: -24.5pt; line-height: 17.0pt'>
                <span lang='en-us' style='font-family: 標楷體'>九、<span style='font: 7.0pt "Times New Roman"'>
                </span>
                </span><span style='font-family: 標楷體'>本同意書之解釋與適用，以及本同意書有關之爭議，均應依照中華民國法律予以處理，並以台灣臺北地方法院為管轄法院。</span>
            </p>





            <center>
                <asp:Button ID="Button3" runat="server" Text="我已清楚瞭解個資規定，並同意相關規定。" OnClick="Button3_Click" />
                <%--<input id="Button2" type="button" value="不清楚個資規定，或不同意相關規定。" onclick="btn2()"/>--%>
            </center>
        </div>

    </form>
</body>
</html>
