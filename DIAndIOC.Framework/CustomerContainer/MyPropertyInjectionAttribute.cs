using System;
using System.Collections.Generic;
using System.Text;

namespace DIAndIOC.Framework.CustomerContainer
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MyPropertyInjectionAttribute:Attribute
    {
    }
}
