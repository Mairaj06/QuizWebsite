using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessRule;
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
        public JsonResult LoadAllQuiz()
        {
            BlQuiz Obj = new BlQuiz();
            var data = Obj.LoadAllQuiz(1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AttemptQuiz(int Id=0)
        {
            BlQuiz Obj = new BlQuiz();
            var data = Obj.LoadQuizAndQuestions(1);
            return View(data);
        }

    }
}
