using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HVTApp.Infrastructure
{
    public interface IWrapper<TModel> : IValidatableObject, IValidatableChangeTracking
        where TModel : class, IBaseEntity
    {
        TModel Model { get; }
        void InitializeComplexProperties();
    }

    public interface IValidatableChangeTracking : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
        bool IsChangedMethod(IDictionary<IBaseEntity, IValidatableChangeTracking> risedDictionary);
        bool IsValidMethod(IList<IBaseEntity> risedList);
        void AcceptChangesMethod(IList<IBaseEntity> acceptedModels);
        void RejectChangesMethod(IList<IBaseEntity> rejectedModels);
    }
}