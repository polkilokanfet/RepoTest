using System.Collections.Generic;
using System.ComponentModel;

namespace HVTApp.Model.Wrapper
{
    public interface IValidatableChangeTracking : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
        List<string> ProcessesInWork { get; }
    }
}
