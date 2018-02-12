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
        public ActionResult AddQuiz(int Id=0)
        {
            var data = ObjBlQuiz.LoadQuizByID(Id);
            return View(data);
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
        public JsonResult LoadQuizQuestions(int Id)
        {
            var data = ObjBlQuiz.LoadQuizQuestions(Id);
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddQuestion(Questions AddQuestion)
        {
            var result = ObjBlQuiz.AddQuestion(AddQuestion);
            return Json(result);
        }
        public JsonResult DeleteQuestion(int quizId,int questionId)
        {
            var result = ObjBlQuiz.DeleteQuizQuestions(quizId, questionId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadQuestionOptions(int Id)
        {
            var data = ObjBlQuiz.LoadQuestionOptions(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveQuestionOptions(List<QuestionOptions> lstOptions)
        {
            var result = ObjBlQuiz.AddQuestionOptions(lstOptions);
            return Json(result);
        }
        public JsonResult DeleteQuestionOption(int QuestionId,int OptionId)
        {
            var result = ObjBlQuiz.DeleteQuestionOption(QuestionId, OptionId);
            return Json(result, JsonRequestBehavior.AllowGet);
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
