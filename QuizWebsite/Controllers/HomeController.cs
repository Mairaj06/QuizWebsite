using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace QuizWebsite.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            
            return View();
        }
        
        
    }
    
}
