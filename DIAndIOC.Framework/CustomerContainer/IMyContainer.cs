using System;
using System.Collections.Generic;
using System.Text;

namespace DIAndIOC.Framework.CustomerContainer
{
    public interface IMyContainer
    {
        void Register<TFrom, TTo>(string shortName = null, object[] paraList = null, LifetimeType lifetimeType = LifetimeType.Transient) where TTo : TFrom;
        TFrom Resolve<TFrom>(string shortName = null);
    }
}
