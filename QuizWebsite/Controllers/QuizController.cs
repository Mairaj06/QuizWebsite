using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebsite.Controllers
{
    public class QuizController : Controller
    {
        //
        // GET: /Quiz/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddQuiz()
        {
            return View();
        }
        public ActionResult QuizQuestions()
        {
            return View();
        }
        public ActionResult Categories()
        {
            return View();
        }
        public ActionResult QuizCategories()
        {
            return View();
        }
        public ActionResult QuizList()
        {
            return View();
        }
        public ActionResult AttemptQuiz()
        {
            return View();
        }

    }
}
