using DIAndIOC.BLL;
using DIAndIOC.DAL;
using DIAndIOC.Framework;
using DIAndIOC.Framework.CustomerContainer;
using DIAndIOC.IBLL;
using DIAndIOC.IDAL;
using DIAndIOC.Interface;
using DIAndIOC.Model;
using DIAndIOC.Service;
using System;

namespace DIAndIOC.ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //{
            //    IUserDAL userDAL = ObjectFactory.CreateDAL();//new IUserDAL();//new UserDAL()
            //    IUserBLL userBLL = ObjectFactory.CreateBLL(userDAL);
            //    var userModel = userBLL.Login("Administartor");
            //    Console.WriteLine(userModel.Name);
            //}

            {
                //常用IOC容器：容器对象———注册————生成
                //IMyContainer container = new MyContainer();
                //container.Register<IUserDAL, UserDAL>();
                //container.Register<IUserBLL, UserBLL>();
                //container.Register<ITestServiceA, TestServiceA>();
                //container.Register<ITestServiceB, TestServiceB>();

                //IUserDAL userDAL = container.Resolve<IUserDAL>();

                //多参数构造函数
                //IUserBLL userBLL = container.Resolve<IUserBLL>();
            }

            {
                //多个构造函数，.netcore中自带容器找的是包含参数的超集构造函数，有不包含的参数则报错
                //IMyContainer container = new MyContainer();
                //container.Register<IUserDAL, UserDAL>();
                //container.Register<IUserBLL, UserBLL>();
                //container.Register<ITestServiceA, TestServiceA>();
                //container.Register<ITestServiceB, TestServiceB>();
                //container.Register<ITestServiceC, TestServiceC>();
                //container.Register<ITestServiceD, TestServiceD>();

                //IUserBLL userBLL = container.Resolve<IUserBLL>();
            }

            {
                //其它方式注入：属性注入，方法注入
                //IMyContainer container = new MyContainer();
                //container.Register<IUserDAL, UserDAL>();
                //container.Register<IUserBLL, UserBLL>();
                //container.Register<ITestServiceA, TestServiceA>();
                //container.Register<ITestServiceB, TestServiceB>();
                //container.Register<ITestServiceC, TestServiceC>();
                //container.Register<ITestServiceD, TestServiceD>();

                //ITestServiceB testServiceB = container.Resolve<ITestServiceB>();
            }

            {
                //单接口，多实现：加个参数name，注册时保存到字典，Resolve时传递name识别
                //IMyContainer container = new MyContainer();
                //container.Register<IUserDAL, UserDAL>();
                //container.Register<IUserDAL, UserDALMySql>("mysql");//注册时区分
                //container.Register<ITestServiceA, TestServiceA>();
                //container.Register<ITestServiceB, TestServiceB>();
                //container.Register<ITestServiceC, TestServiceC>();
                //container.Register<ITestServiceD, TestServiceD>();

                //IUserDAL userDAL = container.Resolve<IUserDAL>();
                //IUserDAL userDALMysql = container.Resolve<IUserDAL>("mysql");
            }

            {
                //注入参数是int或string类型
                IMyContainer container = new MyContainer();
                container.Register<IUserDAL, UserDAL>();
                container.Register<IUserDAL, UserDALMySql>("mysql");//注册时区分
                container.Register<IUserBLL, UserBLL>();
                container.Register<ITestServiceA, TestServiceA>();
                container.Register<ITestServiceB, TestServiceB>(paraList: new object[] { "chris", 2 });
                container.Register<ITestServiceC, TestServiceC>();
                container.Register<ITestServiceD, TestServiceD>();

                //ITestServiceB testServiceB = container.Resolve<ITestServiceB>();
                IUserBLL userBLL = container.Resolve<IUserBLL>();
            }
            Console.ReadKey();
        }
    }
}
