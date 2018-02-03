using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataRepository
{
    public class QuizRepo : IDataRepository<DataModel.Quiz>
    {
        QuizDBEntities context = new QuizDBEntities();

        public IQueryable<DataModel.Quiz> Get()
        {
            return (from q in context.tblQuizs
                    select new DataModel.Quiz()
                    {
                        ID = q.ID,
                        Name = q.Name
                    });
        }
        public List<DataModel.Quiz> LoadAllQuiz(int UserId) 
        {
            var list = context.tblQuizs.Where(q => q.CreatedBy == UserId).Select(x => new DataModel.Quiz()
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description,
                Time = x.Timer == true ? true : false,
                Hours = x.Hours.Value,
                Minutes = x.Minutes.Value,
                AllowReAttempt = x.AllowReAttempt == true ? true : false,
                ReAttemptDuration = x.ReAttemptDuration.Value,
                PassMarks =  x.PassMarks.Value,
                QuizUrl = x.QuizUrl,
                Type = x.Type.Value,
                CreatedAt = x.CreatedAt.Value,
                UpdatedAt = x.UpdatedAt.Value
            }).ToList();
            return list;
        }

        public DataModel.Quiz Get(int id)
        {
            return (from q in context.tblQuizs
                    where q.ID == id
                    select new DataModel.Quiz()
                    {
                        ID = q.ID,
                        Name = q.Name,
                        Description = q.Description,
                        Hours = q.Hours.Value,
                        Minutes = q.Minutes.Value,
                        AllowReAttempt = q.AllowReAttempt.Value,
                        Time = q.Timer.Value,
                        PassMarks = q.PassMarks.Value,
                        Type = q.Type.Value,
                        QuizUrl = q.QuizUrl,
                        ReAttemptDuration = q.ReAttemptDuration.Value,
                        CreatedAt = q.CreatedAt.Value,
                        UpdatedAt = q.UpdatedAt.Value,
                        IsActive = q.IsActive.Value,
                        QuizNotes = q.QuizNotes,
                        RequiresLogin = q.RequiresLogin.Value
                    }).FirstOrDefault();
        }

        public DataModel.Quiz Add(DataModel.Quiz obj)
        {
            try
            {

            
            var existing = context.tblQuizs.Where(x => x.Name == obj.Name).FirstOrDefault();
            if (obj.ID != 0)
                return Update(obj);
            
            tblQuiz quiz = new tblQuiz();
            quiz.Name = obj.Name;
            quiz.Description = obj.Description;
            quiz.AllowReAttempt = obj.AllowReAttempt;
            quiz.Hours = obj.Hours;
            quiz.Minutes = obj.Minutes;
            quiz.ReAttemptDuration = obj.ReAttemptDuration;
            quiz.Timer = obj.Time;
            quiz.PassMarks = obj.PassMarks;
            quiz.QuizUrl = obj.QuizUrl;
            quiz.CreatedBy = obj.CreatedBy;
            quiz.CreatedAt = DateTime.Now;
            quiz.Type = obj.Type;
            quiz.UpdatedAt = DateTime.Now;
            quiz.IsActive = obj.IsActive;
            quiz.QuizNotes = obj.QuizNotes;
            quiz.RequiresLogin = obj.RequiresLogin;
            if (existing != null)
            {
                obj.ErrorMessage = "Quiz with same name already exists";
                return obj;
            }
            context.tblQuizs.Add(quiz);
            context.SaveChanges();

            //Assuming the database is generating your Id's for you
            obj.ID = quiz.ID;
            obj.Success = true;
            obj.ErrorMessage = "Quiz saved successfully.";
            return obj;
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public DataModel.Quiz Update(DataModel.Quiz obj)
        {
            tblQuiz quiz = context.tblQuizs.Where(q => q.ID == obj.ID).FirstOrDefault();
            quiz.Name = obj.Name;
            quiz.Description = obj.Description;
            quiz.AllowReAttempt = obj.AllowReAttempt;
            quiz.Hours = obj.Hours;
            quiz.Minutes = obj.Minutes;
            quiz.ReAttemptDuration = obj.ReAttemptDuration;
            quiz.Timer = obj.Time;
            quiz.PassMarks = obj.PassMarks;
            quiz.QuizUrl = obj.QuizUrl;
            quiz.CreatedBy = obj.CreatedBy;
            quiz.CreatedAt = DateTime.Now;
            quiz.Type = obj.Type;
            quiz.UpdatedAt = DateTime.Now;
            quiz.IsActive = obj.IsActive;
            quiz.QuizNotes = obj.QuizNotes;
            quiz.RequiresLogin = obj.RequiresLogin;
            context.Entry(quiz).State = EntityState.Modified;
            int result = context.SaveChanges();
            if (result > 0)
            {
                obj.Success = true;
                obj.ErrorMessage = "Quiz updated successfully";
            }
            else
            {
                obj.Success = false;
                obj.ErrorMessage = "An error occured while updating quiz.";
            }

            return obj;
        }

        public int Delete(bool IsDeleted)
        {
            tblQuiz quiz = new tblQuiz();
            quiz.IsActive = IsDeleted;

            context.Entry(quiz).State = EntityState.Modified;
            int result = context.SaveChanges();
            return result;
        }
        public bool SaveQuizCategory(QuizCategories Obj)
        {
            tblQuizCategory Category;
            if (Obj.Id == 0)
            {
                Category = new tblQuizCategory();
                Category.Category = Obj.Category;
                Category.IsDeleted = false;
                context.tblQuizCategories.Add(Category);
            }
            else
            {
                Category = context.tblQuizCategories.Where(x => x.Id == Obj.Id).FirstOrDefault();
                Category.Category = Obj.Category;
                Category.IsDeleted = Obj.IsDeleted;
                context.Entry(Category).State = EntityState.Modified;
            }
            int result = context.SaveChanges();
            return result > 0 ? true : false;

        }
        public List<QuizCategories> LoadAllCategories()
        {
            return context.tblQuizCategories.Where(x => x.IsDeleted == false).Select(x => new QuizCategories()
            {
                Id = x.Id,
                Category = x.Category,
                Count = (context.tblQuizs.Where(q => q.Type == x.Id).Count())
            }).ToList();
        }
        public List<DataModel.Quiz> LoadAllQuizByCategory(QuizSearch Obj)
        {
            List<SqlParameter> Params = new List<SqlParameter>();
            if (Obj != null)
            {
                Params.Add(new SqlParameter("CategoryName", Obj.CategoryName));
                Params.Add(new SqlParameter("QuizName", Obj.QuizName));
                Params.Add(new SqlParameter("CategoryId", Obj.CategoryId == 0 ? 0 : Obj.CategoryId));
                Params.Add(new SqlParameter("PageNo", Obj.PageNo == 0 ? 1 : Obj.PageNo));
                Params.Add(new SqlParameter("PageSize", Obj.PageSize == 0 ? 20 : Obj.PageSize));
            }
            return context.Database.SqlQuery<tblQuiz>("uspLoadQuizByCategory", Params.ToArray()).Select(x => new DataModel.Quiz()
            {
                ID = x.ID,
                Name = x.Name,
                Description = x.Description,
                Time = x.Timer == true ? true : false,
                Hours = x.Hours.Value,
                Minutes = x.Minutes.Value,
                AllowReAttempt = x.AllowReAttempt == true ? true : false,
                ReAttemptDuration = x.ReAttemptDuration.Value,
                PassMarks = x.PassMarks.Value,
                QuizUrl = x.QuizUrl,
                Category = (context.tblQuizCategories.Where(Q => Q.Id == x.Type).FirstOrDefault().Category),
                CreatedAt = x.CreatedAt.Value,
                UpdatedAt = x.UpdatedAt.Value
            }).OrderBy(x => x.Category).ToList();


        }
        public DataSet LoadQuizAndQuestions(int QuizId)
        {
            DataSet ds = new DataSet();
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@pQuizId",QuizId));
            DbAccess Obj = new DbAccess();
            Obj.FillDataSet(CommandType.StoredProcedure, "uspLoadQuizAndQuestions", list, ds);
            return ds;
           
        }
        
    }
}
