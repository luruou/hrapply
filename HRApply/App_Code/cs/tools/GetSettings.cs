using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace ApplyPromote
{
    public class GetSettings
    {
        DateTime currentTime = System.DateTime.Now;
        public String Year //= "106";
        {
            get
            {
                HttpCookie c = HttpContext.Current.Request.Cookies["SelectSmtr"];
                if (c == null)
                {
                    c = new HttpCookie("SelectSmtr");
                    c.Value = "111";//(currentTime.Year-1911).ToString();
                    HttpContext.Current.Response.Cookies.Add(c);
                }
                return Convert.ToString(c.Value);
            }
            set
            {
                HttpCookie c = new HttpCookie("SelectSmtr");
                c.Value = value;
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }
        public String Semester //= "2";
        {
            get
            {
                HttpCookie c = HttpContext.Current.Request.Cookies["SelectSemester"];

                if (c == null)
                {
                    c = new HttpCookie("SelectSemester");
                    c.Value = "1";
                    HttpContext.Current.Response.Cookies.Add(c);
                }
                return Convert.ToString(c.Value);
            }
            set
            {
                HttpCookie c = new HttpCookie("SelectSemester");
                c.Value = value;
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }
        public String LoginMail //= "1";
        {
            get
            {
                HttpCookie c = HttpContext.Current.Request.Cookies["LoginMail"];

                if (c == null)
                {
                    c = new HttpCookie("LoginMail");
                    c.Value = " ";
                    HttpContext.Current.Response.Cookies.Add(c);
                }
                return HttpUtility.UrlDecode(Convert.ToString(c.Value));
            }
            set
            {
                HttpCookie c = new HttpCookie("LoginMail");
                c.Value = value;
                HttpContext.Current.Response.Cookies.Add(c);
            }
        }

        public String NowYear = (System.DateTime.Now.Year - 1911).ToString();
        public String NowSemester = "1";
        //在雙學期為1061&1062時，LoadYear不能採-1911-1
        public String LoadYear = (System.DateTime.Now.Year - 1911).ToString();
        
        public String LoadSemester = "1";

        DateTime dt = DateTime.Now;
        public void Execute()
        {
            Semester = this.Semester;
        }

        public String GetYear()
        {
            return Year;
        }

        public String GetSemester()
        {
            return Semester;
        }
    }
}
