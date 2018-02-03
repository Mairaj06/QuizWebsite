using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizWebsite.Utility
{
    public class SessionHelper
    {
        public void SetSession(string Key,object Data)
        {
            if (HttpContext.Current.Session[Key] == null)
            {
                HttpContext.Current.Session[Key] = Data;
            }
        }
        public UserInfo GetSession(string Key)
        {
            if (HttpContext.Current.Session[Key] != null)
            {
                return HttpContext.Current.Session[Key] as UserInfo;
            }
            else
                return null;
        }
    }
}