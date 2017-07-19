using System.Collections.Generic;
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
        bool IsChangedMethod(IDictionary<IBaseEntity, IValidatableChangeTracking> risedDictionary);
        bool IsValidMethod(IList<IBaseEntity> risedList);
        void AcceptChangesMethod(IList<IBaseEntity> acceptedModels);
        void RejectChangesMethod(IList<IBaseEntity> rejectedModels);
    }

    public interface IGetWrapper
    {
        void AddWrapperInDictionary(IWrapper<IBaseEntity> wrapper);

        TWrapper GetWrapper<TWrapper>(IBaseEntity model)
            where TWrapper : class, IWrapper<IBaseEntity>;

        TWrapper GetWrapper<TWrapper>()
            where TWrapper : class, IWrapper<IBaseEntity>;

        void RemoveWrapper(IBaseEntity model);
    }

}