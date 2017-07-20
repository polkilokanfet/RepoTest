using System;
using System.Collections.Generic;
using System.Reflection;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class WrappersFactory : IGetWrapper
    {
        readonly Dictionary<IBaseEntity, IWrapper<IBaseEntity>> _wrappers = new Dictionary<IBaseEntity, IWrapper<IBaseEntity>>();

        public void AddWrapperInDictionary(IWrapper<IBaseEntity> wrapper)
        {
            _wrappers.Add(wrapper.Model, wrapper);
        }

        public TWrapper GetWrapper<TWrapper>(IBaseEntity model)
            where TWrapper: class, IWrapper<IBaseEntity>
        {
            if (!_wrappers.ContainsKey(model))
                Activator.CreateInstance(typeof (TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { model, this }, null, null);

            return (TWrapper) _wrappers[model];
        }

        public TWrapper GetWrapper<TWrapper>()
            where TWrapper : class, IWrapper<IBaseEntity>
        {
            return (TWrapper)Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null, null);
            //return GetWrapper<TWrapper>(Activator.CreateInstance<TModel>());
        }


        public void RemoveWrapper(IBaseEntity model)
        {
            _wrappers.Remove(model);
        }
    }
}
