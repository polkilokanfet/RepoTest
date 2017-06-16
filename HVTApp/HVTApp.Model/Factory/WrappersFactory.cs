using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Factory
{
    public static class WrappersFactory
    {
        public static readonly Dictionary<IBaseEntity, object> Wrappers = new Dictionary<IBaseEntity, object>();
         
        public static TWrapper GetWrapper<TModel, TWrapper>(TModel model)
            where TModel : class, IBaseEntity
            where TWrapper: class, IWrapper<TModel>
        {
            if (!Wrappers.ContainsKey(model))
            {
                Activator.CreateInstance(typeof (TWrapper), model);
                //Wrappers.Add(model, Activator.CreateInstance(typeof (TWrapper), model));
                //((TWrapper) Wrappers[model]).InitializeComplexProperties();
            }


            return (TWrapper) Wrappers[model];
        }
    }
}
