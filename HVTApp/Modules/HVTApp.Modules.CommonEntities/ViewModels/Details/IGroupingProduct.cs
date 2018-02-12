using System.ComponentModel;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public interface IGroupingProduct : INotifyPropertyChanged
    {
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        double Cost { get; set; }
        double MarginalIncome { get; set; }
    }
}