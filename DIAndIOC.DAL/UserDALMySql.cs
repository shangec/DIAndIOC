using DIAndIOC.IDAL;
using DIAndIOC.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DIAndIOC.DAL
{
    public class UserDALMySql : IUserDAL
    {
        public UserModel Find(Expression<Func<UserModel, bool>> expression)
        {
            return new UserModel
            {
                Id = 45,
                Name = "Chris",
                Account = "Administrator",
                Email = "958858007@qq.com",
                Password = "00123456",
                Role = "Admin",
                LoginTime = DateTime.Now
            };
        }

        public void Update(UserModel userModel)
        {
            Console.WriteLine("数据库更新");
        }
    }
}
