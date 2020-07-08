using DIAndIOC.Framework.CustomerContainer;
using DIAndIOC.IDAL;
using DIAndIOC.Interface;
using DIAndIOC.Model;
using System;
using System.Linq.Expressions;

namespace DIAndIOC.DAL
{
    public class UserDAL : IUserDAL
    {
        private ITestServiceA _iTestServiceA = null;
        private ITestServiceB _iTestServiceB = null;

        [MyConstructorAttribute]
        public UserDAL(ITestServiceA testServiceA, ITestServiceB testServiceB)
        {
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
        }

        public UserDAL(ITestServiceA testServiceA, ITestServiceB testServiceB
            , ITestServiceC testServiceC, ITestServiceD testServiceD)
        {
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
        }
        public UserDAL()
        {
            this._iTestServiceA = null;
            this._iTestServiceB = null;
        }


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
