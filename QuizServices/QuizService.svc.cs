using BusinessRule;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace QuizServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    
    [AspNetCompatibilityRequirements(RequirementsMode= AspNetCompatibilityRequirementsMode.Allowed)]
    public class QuizService : IQuizService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public List<Quiz> LoadAllQuiz(string UserId)
        {
            BlQuiz obj = new BlQuiz();
            return  obj.LoadAllQuiz(int.Parse(UserId));
        }
        public DataModel.CustomResponse SaveQuiz(Quiz objQuiz)
        {
            try
            {
                BlQuiz obj = new BlQuiz();
                return obj.AddQuiz(objQuiz);
                
            }
            catch (Exception ex)
            {
                
                
                throw ex;
            }
        }
        public Quiz LoadQuizById(string QuizId)
        {
            try
            {
                int quizId = 0;
                int.TryParse(QuizId,out quizId);
                BlQuiz obj = new BlQuiz();
                return obj.LoadQuizByID(quizId);
            }
            catch (Exception)
            {
                
                throw;
            }
 
        }
        public VMQuizAndQuizQuestions LoadQuizQuestions(string QuizId)
        {
            try
            {
                int quizId = 0;
                int.TryParse(QuizId, out quizId);
                BlQuiz obj = new BlQuiz();
                return obj.LoadQuizQuestions(quizId);
            }
            catch (Exception)
            {

                throw;
            }
 
        }
        public Questions AddQuestion(Questions Question)
        {
            try
            {
                
                BlQuiz obj = new BlQuiz();
                return obj.AddQuestion(Question);
            }
            catch (Exception ex)
            {

                throw ex;
            }
 
        }
        public CustomResponse SaveQuestionOptions(List<QuestionOptions> Options)
        {
            BlQuiz obj = new BlQuiz();
            return obj.AddQuestionOptions(Options);
        }
        public List<QuestionOptions> LoadQuestionOptions(string QuestionId)
        {
            int questionId = 0;
            int.TryParse(QuestionId, out questionId);
            BlQuiz obj = new BlQuiz();
            return obj.LoadQuestionOptions(questionId);

        }
        public VMQuizAndQuizQuestions DeleteQuizQuestions(string QuizId, string QuestionId)
        {
            try
            {
                
            
            BlQuiz obj = new BlQuiz();
            return obj.DeleteQuizQuestions(QuizId, QuestionId);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public List<QuestionOptions> DeleteQuestionOption(string QuestionId, string OptionId)
        {
            try
            {

                int QuestionID = 0;
                int OptionID = 0;
                int.TryParse(QuestionId, out QuestionID);
                int.TryParse(OptionId, out OptionID);
                BlQuiz obj = new BlQuiz();
                return obj.DeleteQuestionOption(QuestionID, OptionID);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public List<QuizCategories> SaveQuizCategory(QuizCategories Obj)
        {
            BlQuiz Quiz = new BlQuiz();
            return Quiz.SaveQuizCategory(Obj);
        }
        public List<QuizCategories> LoadAllCategories()
        {
            BlQuiz Quiz = new BlQuiz();
            return Quiz.LoadAllQuizCategories();
        }
        public List<Quiz> LoadAllQuizByCategory(QuizSearch Obj)
        {
            BlQuiz obj = new BlQuiz();
            return obj.LoadAllQuizByCategory(Obj);
        }
        public QuizViewModel LoadQuizAndQuestions(string QuizId)
        {
            int quizId = 0;
            int.TryParse(QuizId, out quizId);
            BlQuiz obj = new BlQuiz();
            return obj.LoadQuizAndQuestions(quizId);
        }
        public DataModel.Users SaveUser(Users objUser)
        {
            try
            {
                BlUsers obj = new BlUsers();
                return obj.AddUser(objUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public bool CheckUser(string UserName)
        {
            try
            {
                BlUsers obj = new BlUsers();
                return obj.CheckUser(UserName);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Users ValidateUser(Users User)
        {
            BlUsers obj = new BlUsers();
            return obj.ValidateUser(User);
        }
        public List<Users> UsersList(Users User)
        {
            BlUsers obj = new BlUsers();
            return obj.UsersList();
            
        }

    }
}
