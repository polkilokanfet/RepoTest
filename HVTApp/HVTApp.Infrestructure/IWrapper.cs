using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Infrastructure
{
    public interface IWrapper<out TModel> : IValidatableObject, IValidatableChangeTracking
        where TModel : class, IBaseEntity
    {
        TModel Model { get; }
    }

    public interface IValidatableChangeTracking : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
    }
}