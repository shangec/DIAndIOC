using DIAndIOC.Framework.CustomerContainer;
using DIAndIOC.Interface;
using System;

namespace DIAndIOC.Service
{
    public class TestServiceB : ITestServiceB
    {
        /// <summary>
        /// 希望属性也能初始化--属性注入
        /// </summary>
        [MyPropertyInjectionAttribute]
        public ITestServiceD TestServiceD { get; set; }
        public TestServiceB()
        {
            Console.WriteLine($"{this.GetType().Name}被构造了。。。");
        }

        private ITestServiceC _iTestServiceC = null;
        [MyMethodAttribute]
        public void Init(ITestServiceC testServiceC)
        {
            this._iTestServiceC = testServiceC;
        }
        private ITestServiceA _iTestServiceA = null;
        private int _Age { get; set; }
        [MyConstructor]
        public TestServiceB(ITestServiceA testServiceA, [MyParameter]string name, ITestServiceC testServiceC, [MyParameter]int age)
        {
            this._iTestServiceA = testServiceA;
            this._Age = age;
        }

        public void Show()
        {
            Console.WriteLine("B");
        }
    }
}
