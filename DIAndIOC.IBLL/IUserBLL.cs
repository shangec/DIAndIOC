using DIAndIOC.Model;
using System;

namespace DIAndIOC.IBLL
{
    public interface IUserBLL
    {
        UserModel Login(string account);
        void LastLogin(UserModel user);
    }
}
