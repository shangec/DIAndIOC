using System;
using System.Collections.Generic;
using System.Text;

namespace DIAndIOC.Framework.CustomerContainer
{
    [AttributeUsage(AttributeTargets.Constructor)]
    public class MyConstructorAttribute:Attribute
    {
    }
}
