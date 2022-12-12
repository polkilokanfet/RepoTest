using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.Infrastructure.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels
{
    public abstract class EngineeringDepartmentTasksQueueViewModel : ViewModelBaseCanExportToExcel
    {
        public ObservableCollection<EngineeringDepartmentTask> Items { get; }
        public EngineeringDepartmentTask SelectedItem { get; set; }

        protected EngineeringDepartmentTasksQueueViewModel(IUnityContainer container) : base(container)
        {
            Items = new ObservableCollection<EngineeringDepartmentTask>(GetAllItems());
        }

        protected abstract IEnumerable<EngineeringDepartmentTask> GetAllItems();
    }
}