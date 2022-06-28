<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditReport.aspx.cs" Inherits="ApplyPromote.AuditReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type='text/javascript' src='http://code.jquery.com/jquery-1.5.2.js'></script>
<head id="Head1" runat="server">
   <style type="text/css">
        html
        {
            background-color: Silver;
        }
/* 
     "." 是為了讓<tag>內部的屬性
     "class、CssClass、StaticSelectedStyle-CssClass"
    可以引用。
*/

        .tabs
        {
            position: relative;
            top: 1px;
            left: 10px;
        }
        .tab
        {
            border: solid 1px black;
            background-color: #eeeeee;
            padding: 2px 10px;
            margin:2px;
        }
        .selectedTab
        {
            background-color: White;
            border-bottom: solid 1px white;
        }
        .tabContents
        {
            border: solid 1px black;
            padding: 10px;
            background-color: White;
           text-align: left;
       }
       .hiddencol
        {
            display:none;
        }
        .viscol
        {
            display:block;
        }
        img {
            image-rendering: -moz-crisp-edges;
            image-rendering: -o-crisp-edges;
            image-rendering: -webkit-optimize-contrast;
            image-rendering: crisp-edges;
            -ms-interpolation-mode: nearest-neighbor;
        }
       </style>
    <title></title>
</head>

<body>
    <a href="ApplyerList.aspx">回到清單</a>
    <form id="formApplyView" runat="server" enctype="multipart/form-data">
    
    <div>
        <fieldset>
            <div class="tabContents">
   
                        <asp:Table ID="TableAuditExecute" Runat="Server" BorderColor="black" 
                            BorderWidth="1" GridLines="Both" HorizontalAlign="Center" 
                            style="font-weight: bold;text-align:center;" width="85%">
                            <asp:TableRow ID="TableRow5" Runat="Server">
                                <asp:TableCell ID="TableCell7" Runat="Server"> 送審等級 </asp:TableCell>
                                <asp:TableCell><font color="blue"><asp:Label ID="AuditAppJobTitle" runat="server" 
                                    Text="" /></font></asp:TableCell>
                                <asp:TableCell>姓名</asp:TableCell>
                                <asp:TableCell><font color="blue"><asp:Label ID="AuditEmpNameCN" runat="server" 
                                    Text="" /></font></asp:TableCell>
                                <asp:TableCell style="padding-top:5px;padding-left:5px;padding-bottom:5px;">學院<br/>系所</asp:TableCell>
                                <asp:TableCell><font color="blue"><asp:Label ID="AuditAppUnit" runat="server" 
                                    Text="" /></font></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow6" Runat="Server">
                                <asp:TableCell style="padding-top:5px;padding-left:5px;padding-bottom:5px;"> 代表著作<br />名稱 </asp:TableCell>
                                <asp:TableCell ColumnSpan="5" style="padding-top:5px;padding-left:5px;padding-bottom:5px;" HorizontalAlign="Left">
                                <font color="blue"><asp:Label ID="AuditAppPublication" 
                                    runat="server" Text="" /></font>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow7" Runat="Server">
                                <asp:TableCell ColumnSpan="6" style="padding-top:5px;padding-left:5px;" 
                                    width="100%">審查意見(僅提供本校評審用，意見以一百至三百字為原則)<br/>
                                    <font color="blue">
                                    <asp:TextBox ID="AuditExecuteCommentsA" runat="server" Height="100px" 
                                        Text="" 
                                        TextMode="Multiline" Width="95%"></asp:TextBox>
                                    </font>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="FiveLevelDiscription1" Runat="Server">
                                <asp:TableCell ColumnSpan="4">代表著作評分項目及標準</asp:TableCell>
                                <asp:TableCell ColumnSpan="2" RowSpan="2" 
                                    style="padding-top:5px;padding-left:5px;padding-bottom:5px;">
                                    五年內或前一等級<br/>
                                    至本次申請等級時<br/>
                                    個人學術與專業之<br/>
                                    整體成就<br/>
                                    教    授50%<br/>
                                    副 教 授40%<br/>
                                    助理教授30%<br/>
                                    講　　師20%<br/>
                                    </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="FiveLevelDiscription2" Runat="Server">
                                <asp:TableCell>項   目</asp:TableCell>
                                <asp:TableCell>
	                                研 究 主 題 <br/> 
                                  教    授 5%<br/>
                                  副 教 授10%<br/>
                                  助理教授20%<br/>
                                  講　　師25%<br/>
                                </asp:TableCell>
                                <asp:TableCell>
	                                研究方法及能力 <br/> 
                                  教    授10%<br/>
                                  副 教 授20%<br/>
                                  助理教授25%<br/>
                                  講　　師30%<br/>
                                </asp:TableCell>
                                <asp:TableCell>
	                                學術及實務貢獻 <br/>
                                  教    授35%<br/>
                                  副 教 授30%<br/>
                                  助理教授25%<br/>
                                  講　　師25%<br/>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="FivelLevelInputData" Runat="Server">
                                <asp:TableCell style="padding-top:5px;padding-left:5px;padding-bottom:5px;">總評<br />
                                申請人在同領域<br />
                                同級教師的研究表現
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="5" style="text-align:left;">
                                        &nbsp;&nbsp;<asp:DropDownList ID="DDFiveLevelScore" runat="server" 
                                        DataSourceID="DSFiveLevelScore" DataTextField="FLName" 
                                        DataValueField="FLNo">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="DSFiveLevelScore" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:tmuConnectionString %>" 
                                        SelectCommand="SELECT [FLNo], [FLName] FROM [CFiveLevelScore]">
                                    </asp:SqlDataSource>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="WritingScoreDiscription1" Runat="Server">
                                <asp:TableCell ColumnSpan="4">代表著作評分項目及標準</asp:TableCell>
                                <asp:TableCell RowSpan="2" 
                                    style="padding-top:5px;padding-left:5px;padding-bottom:5px;">
	                                五年內或前一等級<br/>
	                                至本次申請等級時<br/>
	                                個人學術與專業之<br/>
	                                整體成就<br/>
                                    教    授50%<br/>
                                    副 教 授40%<br/>
                                    助理教授30%<br/>
                                    講　　師20%<br/>
                                </asp:TableCell>
                                <asp:TableCell RowSpan="2">總 分 <br />(七十分及格)                        
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="WritingScoreDiscription2" Runat="Server">
                                <asp:TableCell>項   目</asp:TableCell>
                                <asp:TableCell>
	                                研 究 主 題 <br/> 
                                  教    授 5%<br/>
                                  副 教 授10%<br/>
                                  助理教授20%<br/>
                                  講　　師25%<br/>
                                </asp:TableCell>
                                <asp:TableCell>
	                                研究方法及能力 <br/> 
                                  教    授10%<br/>
                                  副 教 授20%<br/>
                                  助理教授25%<br/>
                                  講　　師30%<br/>
                                </asp:TableCell>
                                <asp:TableCell>
	                                學術及實務貢獻 <br/>
                                  教    授35%<br/>
                                  副 教 授30%<br/>
                                  助理教授25%<br/>
                                  講　　師25%<br/>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="WritingScoreInputData">
                                <asp:TableCell>得<br />分</asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="TBWSSubject" 
                                    runat="server"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="TBWSMethod" 
                                    runat="server"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="TBWSContribute" 
                                runat="server"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="TBWSAchievement" 
                                runat="server"></asp:TextBox></asp:TableCell>
                                <asp:TableCell><asp:TextBox ID="TBWTotalScore" 
                                runat="server"></asp:TextBox></asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="TableRow8" Runat="Server">
                                <asp:TableCell ColumnSpan="6" style="padding-top:5px;padding-left:5px;">審查意見(本頁僅提供送審人參考，係可公開文件)<br/>
                                    <font color="blue">
                                    <asp:TextBox ID="AuditExecuteCommentsB" runat="server" Height="100px" 
                                        Text="" 
                                        TextMode="Multiline" Width="95%"></asp:TextBox>
                                    </font>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Runat="Server" ID="StrengthsWeaknessesTitle">
                                <asp:TableCell ColumnSpan="3">優   點</asp:TableCell>
                                <asp:TableCell ColumnSpan="3" 
                                    style="padding-top:5px;padding-left:5px;padding-bottom:5px;">缺   點</asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow Runat="Server" ID="StrengthsWeaknessesInputData">
                                <asp:TableCell ColumnSpan="3" 
                                    style="padding-top:0px;padding-left:5px;text-align:left;">
                                    <font color="blue">
                                    <asp:PlaceHolder ID="PHolderStrengths" runat="server"></asp:PlaceHolder>
                                    <asp:CheckBoxList ID="CkBxStrengths" runat="server" ></asp:CheckBoxList>
                                     其他：<asp:TextBox ID="OtherStrengths" runat="server"></asp:TextBox>
                                    </font>
                                </asp:TableCell>
                                <asp:TableCell ColumnSpan="3" 
                                    style="padding-top:0px;padding-left:5px;text-align:left;">
                                    <font color="blue">
                                    <asp:PlaceHolder ID="PHolderWeaknesses" runat="server"></asp:PlaceHolder>
                                    <asp:CheckBoxList ID="CkBxWeaknesses" runat="server" ></asp:CheckBoxList>
                                    其他：<asp:TextBox ID="OtherWeaknesses" runat="server"></asp:TextBox>
                                    </font>
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow ID="CommonNote">
                                <asp:TableCell ColumnSpan="6" HorizontalAlign="Left" 
                                    style="padding-top:5px;padding-left:5px;padding-bottom:5px;">
                                審查評定基準：教　　授：應在該學術領域內有獨特及持續性著作並有重要具體之貢獻者。<br />
                                副 教 授：應在該學術領域內有持續性著作並有具體之貢獻者。<br />
                                助理教授：應有相當於博士論文水準之著作並有獨立研究之能力者。<br />
                                講　　師：應有相當於碩士論文水準之著作。<br /><br />

                                附註: 1.以整理、增刪、組合或編排他人著作而成之編著不得送審。<br />
                                      2.送審著作不得為學位論文或其論文之一部分。惟若未曾以該學位論文送審任一等級教師資格或屬學位論文延續性研究者送審者，經出版並提出說明，由專業審查認定著作具相當程度創新者，不在此限。<br />
                                      3.『5年內或前一等級至本次申請等級時個人學術與專業之整體成就』包含代表作。<br /><br />                                  
                            </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow ID="FiveLevelNote">
                                <asp:TableCell ColumnSpan="6" HorizontalAlign="Left" 
                                    style="padding-top:5px;padding-left:5px;padding-bottom:5px;">
                            <b>
                            ※總評之「分數範圍」對照如下：<br />
                            極優(前10%)：90-100分；優(11%-20%)：80-89分；良(21%-40%)：70-79分；普通(41%-60%)：60-69分；不良(後40%)：0-59分。</b> 
                                
                            </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                        <br />   
                    
                <a href="javascript:history.back()">回上一頁</a>
                <asp:Label Id="MessageLabelAll" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </fieldset>
    </div>
    </form>
</body>

</html>
