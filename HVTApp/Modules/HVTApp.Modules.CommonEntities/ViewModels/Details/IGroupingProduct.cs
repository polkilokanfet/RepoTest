using System.ComponentModel;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public interface IGroupingProduct : INotifyPropertyChanged
    {
        FacilityWrapper Facility { get; set; }
        ProductCostUnitWrapper ProductCostUnit { get; set; }
    }
}