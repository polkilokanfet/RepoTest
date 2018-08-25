using System.Linq;
using System.Reflection;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public partial class WrapperDataService : IWrapperDataService
    {
        public IWrapperRepository<TModel, TWrapper> GetWrapperRepository<TModel, TWrapper>() 
            where TModel : class, IBaseEntity 
            where TWrapper : class, IWrapper<TModel>
        {
            var repositoryFieldInfo = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                        .Single(x => typeof(IWrapperRepository<TModel, TWrapper>).IsAssignableFrom(x.FieldType));
            return (IWrapperRepository<TModel, TWrapper>)repositoryFieldInfo.GetValue(this);
        }
    }
}