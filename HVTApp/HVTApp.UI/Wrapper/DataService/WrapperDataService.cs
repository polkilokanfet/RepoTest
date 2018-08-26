using System.Linq;
using System.Reflection;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public partial class WrapperDataService : UnitOfWork, IWrapperDataService
    {
        public IWrapperRepository<TModel, TWrapper> GetRepository<TModel, TWrapper>() 
            where TModel : class, IBaseEntity 
            where TWrapper : class, IWrapper<TModel>
        {
            var repositoryFieldInfo = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                        .Single(x => typeof(IWrapperRepository<TModel, TWrapper>).IsAssignableFrom(x.FieldType));
            return (IWrapperRepository<TModel, TWrapper>)repositoryFieldInfo.GetValue(this);
        }
    }
}