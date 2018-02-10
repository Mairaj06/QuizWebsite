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
            var data = ObjBlQuiz.LoadAllQuiz(1);
            return View(data);
        }
        public ActionResult AddQuiz()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult SaveQuiz(Quiz ObjQuiz)
        {
            ObjBlQuiz.AddQuiz(ObjQuiz);
            return View();
        }
        public ActionResult QuizQuestions(int Id)
        {
            var data = ObjBlQuiz.LoadQuizQuestions(Id);
            return View(data);
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
        public ActionResult AttemptQuiz(int Id=0)
        {
            var data = ObjBlQuiz.LoadQuizAndQuestions(1);
            return View(data);
        }

    }
}
