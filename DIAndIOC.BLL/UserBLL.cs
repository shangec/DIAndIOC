using DIAndIOC.Framework.CustomerContainer;
using DIAndIOC.IBLL;
using DIAndIOC.IDAL;
using DIAndIOC.Model;
using System;

namespace DIAndIOC.BLL
{
    public class UserBLL : IUserBLL
    {
        private IUserDAL _iUserDAL = null;
        [MyPropertyInjection]
        public IUserDAL UserDAL { get; set; }
        public IUserDAL UserDALMySql { get; set; }
        public UserBLL([MyParameterShortNameAttribute("mysql")]IUserDAL userDALMySql)
        {
            this.UserDALMySql = userDALMySql;
        }
        public void LastLogin(UserModel user)
        {
            user.LoginTime = DateTime.Now;
            this._iUserDAL.Update(user);
        }

        public UserModel Login(string account)
        {
            return this._iUserDAL.Find(u => u.Account.Equals(account));
        }
    }
}
