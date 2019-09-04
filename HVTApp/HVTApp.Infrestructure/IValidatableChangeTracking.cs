using System.ComponentModel;

namespace HVTApp.Infrastructure
{
    public interface IValidatableChangeTracking : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
    }
}