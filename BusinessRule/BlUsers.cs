using DataAccess.DataRepository;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRule
{
    public class BlUsers
    {
        static string Key = "M@m06m@m";
        public Users AddUser(Users user)
        {
            UserRepo obj = new UserRepo();
            user.Password = BusinessRule.Utility.EncryptDecrypt.Encrypt(user.Password, Key);
            return obj.Add(user);
        }
        public bool CheckUser(string UserName)
        {
            UserRepo obj = new UserRepo();
            return obj.CheckUser(UserName);
        }
        public Users ValidateUser(Users User)
        {
            UserRepo obj = new UserRepo();
            User.Password = BusinessRule.Utility.EncryptDecrypt.Encrypt(User.Password, Key);
            return obj.ValidateUser(User);
        }
        public List<Users> UsersList()
        {
            UserRepo obj = new UserRepo();
            return obj.Get().ToList();
        }

        public List<UserRoles> UserRolesList()
        {
            UserRepo obj = new UserRepo();
            return obj.GetUserRolesList().ToList();
        }
        public UserRoles AddUserRole(UserRoles userrole)
        {
            UserRepo obj = new UserRepo();
            return obj.AddUserRole(userrole);
        }
    }
}
