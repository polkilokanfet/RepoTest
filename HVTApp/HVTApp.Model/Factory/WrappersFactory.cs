using System;
using System.Collections.Generic;
using System.Reflection;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public static class WrappersFactory
    {
        internal static readonly Dictionary<IBaseEntity, object> Wrappers = new Dictionary<IBaseEntity, object>();
         
        public static TWrapper GetWrapper<TWrapper>(IBaseEntity model)
            where TWrapper: class, IWrapper<IBaseEntity>
        {
            if (!Wrappers.ContainsKey(model))
                Activator.CreateInstance(typeof (TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { model }, null, null);

            return (TWrapper) Wrappers[model];
        }

        public static TWrapper GetWrapper<TWrapper>()
            where TWrapper : class, IWrapper<IBaseEntity>
        {
            return (TWrapper)Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null, null);
            //return GetWrapper<TWrapper>(Activator.CreateInstance<TModel>());
        }


        public static void RemoveWrapper(IBaseEntity model)
        {
            Wrappers.Remove(model);
        }
    }
}
