using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessRule;
using DataModel;

namespace QuizWebsite.Controllers
{
    public class QuizController : Controller
    {
        //
        // GET: /Quiz/
        BlQuiz ObjBlQuiz = new BlQuiz();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddQuiz()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddQuiz(Quiz ObjQuiz)
        {
            ObjBlQuiz.AddQuiz(ObjQuiz);
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
        public JsonResult LoadAllCategories()
        {
            var data = ObjBlQuiz.LoadAllQuizCategories();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult QuizList()
        {
            return View();
        }
        public JsonResult LoadAllQuiz()
        {
            
            var data = ObjBlQuiz.LoadAllQuiz(1);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AttemptQuiz(int Id=0)
        {
            var data = ObjBlQuiz.LoadQuizAndQuestions(1);
            return View(data);
        }

    }
}
