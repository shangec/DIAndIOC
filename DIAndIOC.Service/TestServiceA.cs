using DIAndIOC.Interface;
using System;

namespace DIAndIOC.Service
{
    public class TestServiceA : ITestServiceA
    {
        public TestServiceA()
        {
            Console.WriteLine($"{this.GetType().Name}被构造了。。。");
        }
        public void Show()
        {
            Console.WriteLine("A");
        }
    }
}
