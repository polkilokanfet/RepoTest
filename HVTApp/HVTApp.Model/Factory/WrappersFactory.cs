using System;
using System.Collections.Generic;
using System.Reflection;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Factory
{
    public static class WrappersFactory
    {
        internal static readonly Dictionary<IBaseEntity, object> Wrappers = new Dictionary<IBaseEntity, object>();
         
        public static TWrapper GetWrapper<TModel, TWrapper>(TModel model)
            where TModel : class, IBaseEntity
            where TWrapper: class, IWrapper<TModel>
        {
            if (!Wrappers.ContainsKey(model))
                Activator.CreateInstance(typeof (TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { model }, null, null);

            return (TWrapper) Wrappers[model];
        }

        public static TWrapper GetWrapper<TModel, TWrapper>()
            where TModel : class, IBaseEntity
            where TWrapper : class, IWrapper<TModel>
        {
            return GetWrapper<TModel, TWrapper>(Activator.CreateInstance<TModel>());
        }


        public static void RemoveWrapper(IBaseEntity model)
        {
            Wrappers.Remove(model);
        }
    }
}
