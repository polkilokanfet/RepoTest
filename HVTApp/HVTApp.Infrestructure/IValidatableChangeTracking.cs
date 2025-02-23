using System.ComponentModel;

namespace HVTApp.Infrastructure
{
    public interface IValidatableChangeTracking : IIsValid, IRevertibleChangeTracking, INotifyPropertyChanged
    {
    }

    public interface IIsValid
    {
        bool IsValid { get; }
    }
}