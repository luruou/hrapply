<%@ Application Language="C#" %>

<script runat="server">

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpCookie cLang = Request.Cookies["Lang"];

        if (cLang != null && !String.IsNullOrEmpty(cLang.Value))
        {
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(cLang.Value);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
        else
        {
            HttpCookie cookie = new HttpCookie("Lang");
            cookie.Value = "zh-TW";
            cookie.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(cookie);
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("zh-TW");
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }

    void Application_Start(object sender, EventArgs e) 
    {
        // 應用程式啟動時執行的程式碼

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  應用程式啟動時執行的程式碼

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 應用程式啟動時執行的程式碼

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新的工作階段啟動時執行的程式碼

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在工作階段結束時執行的程式碼
        // 注意: 只有在  Web.config 檔案中將 sessionstate 模式設定為 InProc 時，
        // 才會引起 Session_End 事件。如果將 session 模式設定為 StateServer 
        // 或 SQLServer，則不會引起該事件。

    }
       
</script>
