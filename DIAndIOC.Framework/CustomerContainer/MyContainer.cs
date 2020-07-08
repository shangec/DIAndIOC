using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace DIAndIOC.Framework.CustomerContainer
{
    /// <summary>
    /// 生成对象
    /// 第三方--业务无关性
    /// </summary>
    public class MyContainer : IMyContainer
    {
        //private Dictionary<string, Type> MyContainerDictionary = new Dictionary<string, Type>();
        private Dictionary<string, MyContainerRegistModel> MyContainerDictionary = new Dictionary<string, MyContainerRegistModel>();
        private Dictionary<string, object[]> MyContainerValueDictionary = new Dictionary<string, object[]>();

        /// <summary>
        /// 作用域单例的对象
        /// </summary>
        private Dictionary<string, object> MyContainerScopeDictionary = new Dictionary<string, object>();

        private string GetKey(string fullName, string shortName) => $"{fullName}__{shortName}";

        public IMyContainer CreateChildContainer()
        {
            return new MyContainer(this.MyContainerDictionary, this.MyContainerValueDictionary, new Dictionary<string, object>());
        }

        public MyContainer() { }
        private MyContainer(Dictionary<string, MyContainerRegistModel> myContainerDictionary
                            , Dictionary<string, object[]> myContainerValueDictionary
                            , Dictionary<string, object> myContainerScopeDictionary)
        {
            this.MyContainerDictionary = myContainerDictionary;
            this.MyContainerValueDictionary = myContainerValueDictionary;
            this.MyContainerScopeDictionary = myContainerScopeDictionary;
        }

        public void Register<TFrom, TTo>(string shortName = null, object[] paraList = null, LifetimeType lifetimeType = LifetimeType.Transient) where TTo : TFrom
        {
            //this.MyContainerDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), typeof(TTo));
            this.MyContainerDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), new MyContainerRegistModel
            {
                Lifetime = lifetimeType,
                TargetType = typeof(TTo)
            });

            if (paraList != null && paraList.Length > 0)
                this.MyContainerValueDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), paraList);
        }

        #region MyRegion
        //public TFrom Resolve<TFrom>()
        //{
        //    string key = typeof(TFrom).FullName;
        //    Type type = this.MyContainerDictionary[key];

        //    var ctor = type.GetConstructors()[0];

        //    #region 准备构造函数的参数
        //    List<object> paraList = new List<object>();
        //    foreach (var para in ctor.GetParameters())
        //    {
        //        Type paraType = para.ParameterType; //获取参数的类型
        //        string paraKey = paraType.FullName; //完整名称
        //        Type paraTargetType = this.MyContainerDictionary[paraKey];
        //        paraList.Add(Activator.CreateInstance(paraTargetType));
        //    }
        //    #endregion

        //    object oInstance = Activator.CreateInstance(type, paraList.ToArray());
        //    return (TFrom)oInstance;
        //}
        #endregion

        public TFrom Resolve<TFrom>(string shortName = null)
        {
            return (TFrom)this.ResolveObject(typeof(TFrom), shortName);
        }

        private object ResolveObject(Type abstractType, string shortName = null)
        {
            //string key = abstractType.FullName;
            string key = this.GetKey(abstractType.FullName, shortName);
            //Type type = this.MyContainerDictionary[key];
            MyContainerRegistModel model = this.MyContainerDictionary[key];
            #region Lifetime
            switch (model.Lifetime)
            {
                case LifetimeType.Transient:
                    Console.WriteLine("Transient do nothing before...");
                    break;
                case LifetimeType.Singleton:
                    if (model.SingletonInstance == null)
                    {
                        break;
                    }
                    else
                    {
                        return model.SingletonInstance;
                    }
                case LifetimeType.Scope:
                    if (this.MyContainerScopeDictionary.ContainsKey(key))
                    {
                        return this.MyContainerScopeDictionary[key];
                    }
                    else
                    {
                        break;
                    }
                default:
                    break;
            }
            #endregion

            //var ctor = type.GetConstructors()[0];
            #region 选择合适的构造函数

            Type type = model.TargetType;

            ConstructorInfo ctor = null;

            // 特指的构造函数
            ctor = type.GetConstructors().FirstOrDefault(c => c.IsDefined(typeof(MyConstructorAttribute), true));
            if (ctor == null)
            {   // 参数最多的
                ctor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();
            }
            #endregion

            #region 准备构造函数的参数
            //List<object> paraList = new List<object>();
            //foreach (var para in ctor.GetParameters())
            //{
            //    Type paraType = para.ParameterType; //获取参数的类型
            //    //string paraKey = paraType.FullName; //完整名称
            //    //Type paraTargetType = this.MyContainerDictionary[paraKey];
            //    //paraList.Add(Activator.CreateInstance(paraTargetType));
            //    object paraInstance = this.ResolveObject(paraType);
            //    paraList.Add(paraInstance);
            //}

            List<object> paraList = new List<object>();

            object[] paraConstant = this.MyContainerValueDictionary.ContainsKey(key) ?
                                    this.MyContainerValueDictionary[key] : null;//把常量找出来
            int iIndex = 0;
            foreach (var para in ctor.GetParameters())
            {
                if (para.IsDefined(typeof(MyParameterAttribute), true))
                {
                    paraList.Add(paraConstant[iIndex]);
                    iIndex++;
                }
                else
                {
                    Type paraType = para.ParameterType; //获取参数的类型

                    string paraShortName = this.GetShortName(para);
                    object paraInstance = this.ResolveObject(paraType, paraShortName);

                    paraList.Add(paraInstance);
                }
            }
            #endregion

            object oInstance = Activator.CreateInstance(type, paraList.ToArray());

            #region 属性注入
            foreach (var prop in type.GetProperties().Where(p => p.IsDefined(typeof(MyPropertyInjectionAttribute), true)))
            {
                Type propType = prop.PropertyType;

                string paraShortName = this.GetShortName(prop);

                object propInstance = this.ResolveObject(propType, paraShortName);
                prop.SetValue(oInstance, propInstance);
            }
            #endregion

            #region 方法注入
            foreach (var method in type.GetMethods().Where(m => m.IsDefined(typeof(MyMethodAttribute), true)))
            {
                List<object> methodParaList = new List<object>();
                foreach (var para in method.GetParameters())
                {
                    Type paraType = para.ParameterType; //获取参数的类型

                    string paraShortName = this.GetShortName(para);
                    object paraInstance = this.ResolveObject(paraType, paraShortName);

                    methodParaList.Add(paraInstance);
                }

                method.Invoke(oInstance, methodParaList.ToArray());
            }
            #endregion

            #region Lifetime
            switch (model.Lifetime)
            {
                case LifetimeType.Transient:
                    Console.WriteLine("Transient do nothing after...");
                    break;
                case LifetimeType.Singleton:
                    if (model.SingletonInstance == null)
                    {
                        model.SingletonInstance = oInstance;
                    }
                    break;
                case LifetimeType.Scope:
                    this.MyContainerScopeDictionary[key] = oInstance;
                    break;
                default:
                    break;
            }
            #endregion

            return oInstance;
        }

        private string GetShortName(ICustomAttributeProvider provider)
        {
            if (provider.IsDefined(typeof(MyParameterShortNameAttribute), true))
            {
                var attribute = (MyParameterShortNameAttribute)(provider.GetCustomAttributes(typeof(MyParameterShortNameAttribute), true)[0]);
                return attribute.ShortName;
            }
            else
                return null;
        }

        //private string GetShortNameProperty(PropertyInfo propertyInfo)
        //{
        //    if (propertyInfo.IsDefined(typeof(MyParameterShortNameAttribute), true))
        //    {
        //        return propertyInfo.GetCustomAttribute<MyParameterShortNameAttribute>().ShortName;
        //    }
        //    else
        //        return null;
        //}
        //private string GetShortNamePara(ParameterInfo parameterInfo)
        //{
        //    if (parameterInfo.IsDefined(typeof(MyParameterShortNameAttribute), true))
        //    {
        //        return parameterInfo.GetCustomAttribute<MyParameterShortNameAttribute>().ShortName;
        //    }
        //    else
        //        return null;
        //}
    }
}
