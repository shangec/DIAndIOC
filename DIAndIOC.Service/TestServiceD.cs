using DIAndIOC.Interface;
using System;

namespace DIAndIOC.Service
{
    public class TestServiceD : ITestServiceD
    {
        public TestServiceD()
        {
            Console.WriteLine($"{this.GetType().Name}被构造了。。。");
        }
        public void Show()
        {
            Console.WriteLine("D");
        }
    }
}
