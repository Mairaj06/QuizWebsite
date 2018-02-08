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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    
    public interface IQuizService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveQuiz", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        CustomResponse SaveQuiz(Quiz objQuiz);

        [OperationContract]
        [WebGet(UriTemplate = "CheckUser/{UserName}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool CheckUser(string UserName);
        
        [OperationContract]
        [WebGet(UriTemplate = "LoadAllQuiz/{UserId}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Quiz> LoadAllQuiz(string UserId);

        [OperationContract]
        [WebGet(UriTemplate = "LoadQuizById/{QuizId}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Quiz LoadQuizById(string QuizId);

        [OperationContract]
        [WebGet(UriTemplate = "LoadQuizQuestions/{QuizId}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Tuple<Quiz, List<Questions>> LoadQuizQuestions(string QuizId);

        [OperationContract]
        [WebInvoke(UriTemplate = "AddQuestion", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Questions AddQuestion(Questions Question);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveQuestionOptions", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        CustomResponse SaveQuestionOptions(List<QuestionOptions> Options);

        [OperationContract]
        [WebGet(UriTemplate = "LoadQuestionOptions/{QuestionId}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<QuestionOptions> LoadQuestionOptions(string QuestionId);

        /*[OperationContract]
        [WebInvoke(UriTemplate = "DeleteQuizQuestions/{QuizId}/{QuestionId}",BodyStyle =WebMessageBodyStyle.Wrapped,Method ="GET",  RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Tuple<Quiz, List<Questions>> DeleteQuizQuestions(string QuizId, string QuestionId);

        [OperationContract]
        [WebGet(UriTemplate = "DeleteQuestionOption/{QuestionId}/{OptionId}", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<QuestionOptions> DeleteQuestionOption(string QuestionId, string OptionId);
        */
        [OperationContract]
        [WebInvoke(UriTemplate = "SaveQuizCategory", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<QuizCategories> SaveQuizCategory(QuizCategories Obj);
        
        [OperationContract]
        [WebGet(UriTemplate = "LoadAllCategories", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<QuizCategories> LoadAllCategories();

        [OperationContract]
        [WebInvoke(UriTemplate = "LoadAllQuizByCategory", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Quiz> LoadAllQuizByCategory(QuizSearch Obj);

        [OperationContract]
        [WebInvoke(UriTemplate = "LoadQuizAndQuestions/{QuizId}",Method ="GET", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        QuizViewModel LoadQuizAndQuestions(string QuizId);

        [OperationContract]
        [WebInvoke(UriTemplate = "SaveUser", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Users SaveUser(Users objUser);

        [OperationContract]
        [WebInvoke(UriTemplate = "ValidateUser", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Users ValidateUser(Users User);

        [OperationContract]
        [WebInvoke(UriTemplate = "UsersList", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Users> UsersList(Users User);
        
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
