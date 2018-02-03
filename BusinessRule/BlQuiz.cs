using DataAccess;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DataRepository;
using System.Data;
namespace BusinessRule
{
    public class BlQuiz
    {
        public CustomResponse AddQuiz(Quiz objQuiz)
        {
            QuizRepo rep = new QuizRepo();
            objQuiz = rep.Add(objQuiz);
            CustomResponse response = new CustomResponse();
            response.Success = objQuiz.Success;
            response.Message = objQuiz.ErrorMessage;
            return response;

        }
        public List<Quiz> LoadAllQuiz(int UserId)
        {
            QuizRepo rep = new QuizRepo();
            return rep.LoadAllQuiz(UserId);
        }
        public Quiz LoadQuizByID(int QuizId)
        {
            QuizRepo rep = new QuizRepo();
            return rep.Get(QuizId);
        }
        public Tuple<Quiz, List<Questions>> LoadQuizQuestions(int QuizId)
        {
            QuestionRepo rep = new QuestionRepo();
            return rep.LoadAllQuizQuestions(QuizId);
        }
        public Questions AddQuestion(Questions Question) 
        {
            QuestionRepo rep = new QuestionRepo();
            return rep.Add(Question);
        }
        public CustomResponse AddQuestionOptions(List<QuestionOptions> Options)
        {

            CustomResponse response = new CustomResponse();
            try
            {
                QuestionRepo rep = new QuestionRepo();
                var list = rep.SaveQuestionOptions(Options);
                if (list != null)
                {
                    response.Success = true;
                    response.Message = "Options saved successfully";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error occured while saving question options";
            }
            return response;
        }
        public List<QuestionOptions> LoadQuestionOptions(int QuestionId)
        {
            QuestionRepo repo = new QuestionRepo();
            return repo.LoadQuestionOptions(QuestionId);
            
        }
        public Tuple<Quiz, List<Questions>> DeleteQuizQuestions(string QuizId, string QuestionId)
        {
            QuestionRepo repo = new QuestionRepo();
            int QuizID=0;
            int QuestionID = 0;
            int.TryParse(QuizId,out QuizID);
            int.TryParse(QuestionId,out QuestionID);
            bool result = repo.DeleteQuestion(QuizID, QuestionID);
            if (result)
                return repo.LoadAllQuizQuestions(QuizID);
            else
                return null;


        }
        public List<QuestionOptions> DeleteQuestionOption(int QuestionId,int OptionId)
        {
            QuestionRepo repo = new QuestionRepo();
            bool result = repo.DeleteQuestionOption(QuestionId, OptionId);
            if (result)
                return repo.LoadQuestionOptions(QuestionId);
            else
                return null;
        }
        public List<QuizCategories> SaveQuizCategory(QuizCategories Obj)
        {
            QuizRepo repo = new QuizRepo();
            bool result = repo.SaveQuizCategory(Obj);
            if (result)
                return repo.LoadAllCategories();
            else
                return new List<QuizCategories>();

        }
        public List<QuizCategories> LoadAllQuizCategories()
        {
            QuizRepo repo = new QuizRepo();
            return repo.LoadAllCategories();
        }
        public List<Quiz> LoadAllQuizByCategory(QuizSearch Obj)
        {
            QuizRepo repo = new QuizRepo();
            return repo.LoadAllQuizByCategory(Obj);
        }
        public QuizViewModel LoadQuizAndQuestions(int QuizId)
        {
            QuizRepo repo = new QuizRepo();
            DataSet ds = repo.LoadQuizAndQuestions(QuizId);
            QuizViewModel model = new QuizViewModel();
            if (ds.Tables.Count > 0)
            {
                model.Quiz = CommonMethods.ToList<Quiz>(ds.Tables[0]).FirstOrDefault();
                model.lstQuestions = CommonMethods.ToList<Questions>(ds.Tables[1]);
                model.lstOptions = CommonMethods.ToList<QuestionOptions>(ds.Tables[2]);
            }
            return model;

        }
    }
}
