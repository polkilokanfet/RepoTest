using System.Collections.Generic;
using System.ComponentModel;

namespace HVTApp.Model.Wrappers
{
    public interface IValidatableChangeTracking : IRevertibleChangeTracking, INotifyPropertyChanged
    {
        bool IsValid { get; }
        List<string> ProcessesInWork { get; }
    }
}
