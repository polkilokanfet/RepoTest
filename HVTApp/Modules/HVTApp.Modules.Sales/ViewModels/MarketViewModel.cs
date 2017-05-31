using System.Collections.ObjectModel;
using HVTApp.DataAccess;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class MarketViewModel
    {
        public ObservableCollection<ProjectWrapper> Projects { get; set; }
        public MarketViewModel(IUnitOfWork unitOfWork)
        {
            Projects = new ObservableCollection<ProjectWrapper>(unitOfWork.Projects.GetAll());
        }
    }
}
