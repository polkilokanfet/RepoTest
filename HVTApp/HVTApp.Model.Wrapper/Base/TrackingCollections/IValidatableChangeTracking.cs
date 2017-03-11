using System.ComponentModel;

namespace HVTApp.Model.Wrapper
{
    public interface IValidatableChangeTracking : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
    }
}
