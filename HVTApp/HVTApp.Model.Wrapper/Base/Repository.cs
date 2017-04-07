using HVTApp.Infrastructure;
using System.Collections.Generic;

namespace HVTApp.Model.Wrapper
{
    internal static class Repository
    {
        public static Dictionary<IBaseEntity, object> ModelWrapperDictionary { get; set; } = new Dictionary<IBaseEntity, object>();
    }
}