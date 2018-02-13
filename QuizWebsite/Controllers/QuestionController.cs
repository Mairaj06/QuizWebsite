using BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizWebsite.Controllers
{
    public class QuestionController : Controller
    {
        BlQuiz ObjBlQuiz = new BlQuiz();
        public ActionResult QuestionsList()
        {
            var data = ObjBlQuiz.LoadAllQuestions();
            return View(data);
            
        }

    }
}
