using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DIAndIOC.Framework.CustomerContainer
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
    public class MyParameterShortNameAttribute : Attribute
    {
        public string ShortName { get; private set; }
        public MyParameterShortNameAttribute(string shortName)
        {
            this.ShortName = shortName;
        }
    }
}
