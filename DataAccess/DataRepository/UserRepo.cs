using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataRepository
{
    public class UserRepo : IDataRepository<DataModel.Users>
    {
        QuizDBEntities context = new QuizDBEntities();


        public IQueryable<DataModel.Users> Get()
        {
            return (from u in context.tblUsers
                    select new DataModel.Users()
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Password = u.Password,
                        Role = u.UserRole.Value,
                        CreatedAt = u.CreatedAt.Value,
                        UpdatedAt = u.UpdatedAt.Value,
                        Email = u.UserEmail
                        
                    });
            
        }

        public DataModel.Users Get(int id)
        {
            return (from u in context.tblUsers
                    where u.UserId == id
                    select new DataModel.Users()
                    {
                        UserId = u.UserId,
                        UserName = u.UserName
                    }).FirstOrDefault();
        }

        public DataModel.Users Add(DataModel.Users obj)
        {
            try
            {

            
            //CustomResponse response = new CustomResponse();
            var existing = context.tblUsers.Where(u => u.UserName == obj.UserName).FirstOrDefault();
            if (obj.UserId != 0)
                return Update(obj);

            tblUser user = new tblUser();
            user.UserName = obj.UserName;
            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            user.UserEmail = obj.Email;
            user.CreatedBy = obj.CreatedBy;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            user.UserRole = obj.Role;
            user.IsActive = true;
            user.Password = obj.Password;
            user.UpdatedBy = obj.CreatedBy;
            if (existing != null)
            {
                obj.ErrorMessage = "User with same name already exists";
                obj.Success = false;
                obj.CreatedAt = existing.CreatedAt.Value;
                obj.UpdatedAt = existing.UpdatedAt.Value;
                return obj;
            }
            context.tblUsers.Add(user);
            context.SaveChanges();

            //Assuming the database is generating your Id's for you
            obj.UserId = user.UserId;
            obj.Success = true;
            obj.CreatedAt = DateTime.Now;
            obj.UpdatedAt = DateTime.Now; 
            obj.ErrorMessage = "User added successfully";
            }
            catch (Exception ex)
            {

                obj.ErrorMessage = ex.Message;
                obj.Success = false;
            }
            return obj;

        }

        public DataModel.Users Update(DataModel.Users obj)
        {
            tblUser user = new tblUser();
            user.UserId = obj.UserId;
            user.UserName = obj.UserName;
            user.FirstName = obj.FirstName;
            user.LastName = obj.LastName;
            user.UserEmail = obj.Email;
            user.CreatedBy = obj.CreatedBy;
            user.CreatedAt = obj.CreatedAt;
            user.UpdatedBy = obj.UpdatedBy;
            user.UpdatedAt = obj.UpdatedAt;
            user.UserRole = obj.Role;
            user.Password = obj.Password;
            user.IsActive = obj.IsActive;
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();

            return obj;
        }

        public int Delete(bool IsDeleted)
        {
            tblUser user = new tblUser();
            user.IsActive = IsDeleted;

            context.Entry(user).State = EntityState.Modified;
            int result = context.SaveChanges();
            return result;

        }
        public bool CheckUser(string UserName)
        {
            var user = context.tblUsers.Where(u => u.UserName == UserName).FirstOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }
        public Users ValidateUser(Users User)
        {
            var user =  context.tblUsers.Where(u => u.UserName == User.UserName && u.Password ==  User.Password).Select(u => (new Users() {
            UserName = u.UserName,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Role = u.UserRole.Value,
            CreatedAt = u.CreatedAt.Value,
            UpdatedAt = u.UpdatedAt.Value
            
            })).FirstOrDefault();
            return user;
        }
    }

}
