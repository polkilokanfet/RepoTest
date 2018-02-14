using System.ComponentModel;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectUnitWrapper : IGroupingProduct
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ProductWrapper Product { get; set; }
        public double Cost { get; set; }
        public double MarginalIncome { get; set; }
    }
}