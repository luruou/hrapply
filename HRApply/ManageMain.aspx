<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageMain.aspx.cs" Inherits="ApplyPromote.ManageMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<style type="text/css">
/*ul元素設定*/
#navigation ul
{
 list-style-type : none ;
 margin : 0 auto; /*置中*/
 padding : 0;
 width : 850px;
}

/*讓li"浮"起來，變成"橫向列"*/
#navigation li 
{
 float : left ;
}

/*Menu中a元素設定*/
#navigation a 
{
 float : left ;
 display : block ; /*以「區塊」元素方式顯示*/
 color : rgb(197,197,197);
 padding : 5px 8px;
 margin-top : 10px;
 border-bottom-width : 0;
}

/*li中類別為active的a元素設定*/
#navigation li.active a
{
 color : rgb(255,204,0);
 background-color : rgb(153,153,153);
}

/*Menu中a元素:hover及:active設定 */
#navigation a:hover, #navigation a:active
{
 color : rgb(204,204,255);
 background-color : rgb(100,90,125);
}

/*文章區塊，<div id="content">設定 */
#content
{
 clear : both ;
 width : 850px;
 margin : 0 auto; /*置中*/
 padding : 0;
 background-color : rgb(193,189,204);
}

/* 文章標題，讓jQuery動態加入 */
.newstitle
{
 color :Black ;
 border-bottom : 1px dotted gray;
}

/* 文章內容，由jQuery動態控制  */
.newscontent
{
 width : 50%;
 color : Black ;
 background-color : rgb(255,153,51);
 border : 1px dotted gray;
 padding : 7px 30px 10px 25px;
}

	img {
            image-rendering: -moz-crisp-edges;
            image-rendering: -o-crisp-edges;
            image-rendering: -webkit-optimize-contrast;
            image-rendering: crisp-edges;
            -ms-interpolation-mode: nearest-neighbor;
        }
</style>
<body>
    <form id="form1" runat="server">
    
    <div id="navigation">

        <div id="container">

            <ul id="firstul">

                <li id="view" class="active"><a href="#" title="Home">檢視</a></li>

                <li id="setauditor"><a href="#" title="簽核關卡設定" id="mynews">簽核關卡設定</a></li>

                <li id="setteacher"><a href="#" title="人才資料庫" id="about_us">人才資料庫</a></li>
                
                <li id="settime"><a href="#" title="開放時間" id="a1">開放時間</a></li>
                

            </ul>

            <div id="mypage" style="display: none" align="center">

            </div>

        </div>

    </div>
    
    </form>
</body>
</html>
