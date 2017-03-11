using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public static class WrappersFactory
    {
        public static TWrapper CreateWrapper<TWrapper, TModel>()
            where TWrapper : WrapperBase<TModel>
            where TModel : BaseEntity
        {
            TWrapper wrapper = null;

            return wrapper;
        }
    }
}
