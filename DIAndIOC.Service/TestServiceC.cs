using DIAndIOC.Interface;
using System;

namespace DIAndIOC.Service
{
    public class TestServiceC : ITestServiceC
    {
        public TestServiceC()
        {
            Console.WriteLine($"{this.GetType().Name}被构造了。。。");
        }
        public void Show()
        {
            Console.WriteLine("C");
        }
    }
}
