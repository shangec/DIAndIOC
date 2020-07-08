using DIAndIOC.Model;
using System;
using System.Linq.Expressions;

namespace DIAndIOC.IDAL
{
    public interface IUserDAL
    {
        UserModel Find(Expression<Func<UserModel, bool>> expression);
        void Update(UserModel userModel);
    }
}
