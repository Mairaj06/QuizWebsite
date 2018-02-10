using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataRepository
{
   public class QuestionRepo : IDataRepository<Questions>
    {
        QuizDBEntities context = new QuizDBEntities();

        public IQueryable<DataModel.Questions> Get()
        {
            return (from q in context.tblQuizQuestions
                    select new DataModel.Questions()
                    {
                        QuestionId = q.QuestionId
                    });
        }
        public DataModel.Questions Get(int id)
        {
            return (from q in context.tblQuizQuestions
                    where q.QuestionId == id
                    select new DataModel.Questions()
                    {
                        QuestionId = q.QuestionId,
                        QuestionText = q.QuestionText,
                        QuizId = q.QuizId,
                        CreatedAt = q.CreatedAt.Value,
                        UpdatedAt = q.UpdatedAt.Value,
                        IsActive = q.IsActive.Value,

                        
                    }).FirstOrDefault();
        }

        public DataModel.Questions Add(DataModel.Questions obj)
        {
            
            if (obj.QuestionId != 0)
                return UpdateQuestion(obj);

            var existing = context.tblQuizQuestions.Where(x => x.QuestionText == obj.QuestionText && x.QuizId == obj.QuizId).FirstOrDefault();
            tblQuizQuestion Question = new tblQuizQuestion();
            Question.QuizId = obj.QuizId;
            Question.QuestionText = obj.QuestionText;
            Question.IsActive = obj.IsActive;
            Question.CreatedAt = DateTime.Now;
            Question.CreatedBy = obj.CreatedBy;
            Question.UpdatedAt = DateTime.Now;

            if (existing != null)
            {
                obj.ErrorMessage = "Question with same text already exists";
                obj.Success = false;
                obj.CreatedAt = Question.CreatedAt.Value;
                obj.UpdatedAt = Question.UpdatedAt.Value;
                return obj;
            }
            context.tblQuizQuestions.Add(Question);
            context.SaveChanges();

            //Assuming the database is generating your Id's for you
            obj.QuestionId = Question.QuestionId;
            obj.CreatedAt = Question.CreatedAt.Value;
            obj.UpdatedAt = Question.UpdatedAt.Value;
            obj.Success = true;
            obj.ErrorMessage = "Question saved successfully.";

            return obj;

        }

        public DataModel.Questions Update(DataModel.Questions obj)
        {
            return null;
        }
        public DataModel.Questions UpdateQuestion(DataModel.Questions obj)
        {
            tblQuizQuestion Question = context.tblQuizQuestions.Where(q => q.QuestionId == obj.QuestionId).FirstOrDefault();

            if (Question == null)
                return obj;
            Question.QuestionId = obj.QuestionId;
            Question.QuizId = obj.QuizId;
            Question.QuestionText = obj.QuestionText;
            Question.IsActive = obj.IsActive;
            Question.UpdatedAt = DateTime.Now;
            Question.UpdatedBy = obj.UpdatedBy;
            

            context.Entry(Question).State = EntityState.Modified;
            //context.Entry(question).CurrentValues.SetValues(Question);
            int saved = context.SaveChanges();
            if (saved > 0)
            {
                obj.UpdatedAt = Question.UpdatedAt.Value;
                obj.CreatedAt = Question.CreatedAt.Value;
                obj.Success = true;
                obj.ErrorMessage = "Question updated successfully.";
            }else
                {
                    obj.UpdatedAt = Question.UpdatedAt.Value;
                    obj.CreatedAt = Question.CreatedAt.Value;
                    obj.Success = false;
                    obj.ErrorMessage = "Error occured while updating question.";
                }

            return obj;
        }

        public int Delete(bool IsDeleted)
        {
            tblQuizQuestion Question = new tblQuizQuestion();
            Question.IsActive = IsDeleted;

            context.Entry(Question).State = EntityState.Modified;
            int result = context.SaveChanges();
            return result;
        }
        public VMQuizAndQuizQuestions LoadAllQuizQuestions(int QuizId)
        {
            List<Questions> list = context.tblQuizQuestions.Where(q => q.QuizId == QuizId).Select(x => new Questions()
            {
                QuizId = x.QuizId,
                QuestionId = x.QuestionId,
                QuestionText = x.QuestionText,
                IsActive = x.IsActive.Value,
                CreatedAt = x.CreatedAt.Value,
                UpdatedAt = x.UpdatedAt.Value,

            }).OrderByDescending(x => x.QuestionId).ToList();
            Quiz quiz = new QuizRepo().Get(QuizId);
            VMQuizAndQuizQuestions Obj = new DataModel.VMQuizAndQuizQuestions();
            Obj.lstQuestions = list;
            Obj.Quiz = quiz;
            //Tuple<Quiz, List<Questions>> tuple = new Tuple<Quiz, List<Questions>>(quiz, list);
            return Obj;
        }
        public List<QuestionOptions> SaveQuestionOptions(List<QuestionOptions> Options)
        {
            try
            {
                List<tblQuestionOption> QuesOptions = new List<tblQuestionOption>();
                for (int i = 0; i < Options.Count; i++)
                {
                    tblQuestionOption option = new tblQuestionOption();
                    if (Options[i].Id == 0)
                    {
                        
                        option.QuestionId = Options[i].QuestionID;
                        option.OptionText = Options[i].OptionText;
                        option.OptionOrder = Options[i].OptionOrder;
                        option.IsActive = Options[i].IsActive;
                        option.CreatedBy = Options[i].CreatedBy;
                        option.CreatedAt = DateTime.Now;
                        option.UpdatedAt = DateTime.Now;
                        option.UpdatedBy = Options[i].CreatedBy;
                        QuesOptions.Add(option);
                        context.tblQuestionOptions.Add(option);
                        context.SaveChanges();

                        
                    }
                    else
                    {
                        int OptionID = Options[i].Id;
                        option = context.tblQuestionOptions.Where(x => x.OptionId == OptionID).FirstOrDefault();
                        option.QuestionId = Options[i].QuestionID;
                        option.OptionText = Options[i].OptionText;
                        option.OptionOrder = Options[i].OptionOrder;
                        option.IsActive = Options[i].IsActive;
                        option.OptionId = Options[i].Id;
                        context.Entry(option).State = EntityState.Modified;
                        context.SaveChanges();

                        
                    }
                    if (Options[i].CorrectOption)
                    {
                        int OptionID = Options[i].Id;
                        tblCorrectOption correctOption = context.tblCorrectOptions.Where(q => q.CorrectOptionId == OptionID).FirstOrDefault();
                        if (correctOption == null)
                        {
                            correctOption = new tblCorrectOption();
                            correctOption.CorrectOptionId = option.OptionId;
                            correctOption.QuestionId = option.QuestionId;

                            context.tblCorrectOptions.Add(correctOption);
                            context.SaveChanges();
                        }
                        
                    }
                    else
                    {
                        int OptionID = Options[i].Id;
                        tblCorrectOption CorrectOption = context.tblCorrectOptions.Where(q => q.CorrectOptionId == OptionID).FirstOrDefault();
                        if (CorrectOption != null)
                        {
                            context.Entry(CorrectOption).State = EntityState.Deleted;
                            context.SaveChanges();
                        }
                    }

                    
                }
                
                int questionID = Options[0].QuestionID;
                var list = context.tblQuestionOptions.Where(q => q.QuestionId == questionID).Select(x => new QuestionOptions()
                {
                    Id = x.OptionId,
                    OptionText = x.OptionText,
                    OptionOrder = x.OptionOrder.Value,
                    QuestionID = x.QuestionId,
                    IsActive = x.IsActive.Value,
                    UpdatedAt = x.UpdatedAt.Value,
                    CreatedAt = x.CreatedAt.Value
                }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<QuestionOptions> LoadQuestionOptions(int QuestionId)
        {
            var list = context.tblQuestionOptions.Where(o => o.QuestionId == QuestionId).Select(x => new QuestionOptions()
            {
                QuestionID = x.QuestionId,
                OptionText = x.OptionText,
                Id = x.OptionId,
                IsActive = x.IsActive.Value,
                CreatedAt = x.CreatedAt.Value,
                UpdatedAt = x.UpdatedAt.Value
            }).ToList();

            var correctOptions = context.tblCorrectOptions.Where(x => x.QuestionId == QuestionId).ToList();

            foreach (var item in list)
            {
                if (correctOptions.Any(x => x.CorrectOptionId == item.Id && x.QuestionId == item.QuestionID))
                    item.CorrectOption = true;
                //list.Remove(item);
                //list.Add(item);
            }

            //list.Select(x => { x.CorrectOption = correctOptions.Select(y => y.QuestionId == x.QuestionID && x.Id == y.CorrectOptionId).ToList().Count > 0 ? true : false; return x; });
            
            return list;
        }
        public bool DeleteQuestion(int QuizId, int QuestionId)
        {
            tblQuizQuestion Question = new tblQuizQuestion();
            Question.QuestionId = QuestionId;
            Question.QuizId = QuizId;
            Question.IsActive = true;

            context.Entry(Question).State = EntityState.Deleted;
            int result = context.SaveChanges();
            return result > 0 ? true : false;
        }
        public bool DeleteQuestionOption(int QuestionId, int OptionId)
        {
            tblQuestionOption Option = new tblQuestionOption();
            Option.QuestionId = QuestionId;
            Option.OptionId = OptionId;
            context.Entry(Option).State = EntityState.Deleted;
            int result = context.SaveChanges();

            if (context.tblCorrectOptions.Any(x => x.QuestionId == QuestionId && x.CorrectOptionId == OptionId))
            {
                tblCorrectOption CorrectOption = new tblCorrectOption();
                CorrectOption.CorrectOptionId = OptionId;
                CorrectOption.QuestionId = QuestionId;
                context.Entry(CorrectOption).State = EntityState.Deleted;
                context.SaveChanges();
            }
            return result > 0 ? true : false;
        }
        
    }
}
