using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.EngineeringDepartmentTasksQueue.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels
{
    public abstract class EngineeringDepartmentTasksQueueViewModel : ViewModelBaseCanExportToExcel
    {
        private EngineeringDepartmentTask _selectedItem;
        public ObservableCollection<EngineeringDepartmentTask> Items { get; } = new ObservableCollection<EngineeringDepartmentTask>();

        public EngineeringDepartmentTask SelectedItem
        {
            get => _selectedItem;
            set => this.SetProperty(ref _selectedItem, value, OnSelectedItemChanged);
        }

        public ICommand ReloadCommand { get; }
        public ICommand OpenCommand { get; }

        protected EngineeringDepartmentTasksQueueViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateLogCommand(
                () =>
                {
                    Items.Clear();
                    SelectedItem = null;
                    Items.AddRange(this.GetAllItems());
                });

            ReloadCommand.Execute(null);
        }

        protected virtual void OnSelectedItemChanged()
        {
        }

        protected abstract IEnumerable<EngineeringDepartmentTask> GetAllItems();

        protected PriceEngineeringTask GetTopTask(PriceEngineeringTask priceEngineeringTask)
        {
            var topTask = priceEngineeringTask;
            while (topTask.ParentPriceEngineeringTaskId.HasValue)
            {
                topTask = UnitOfWork.Repository<PriceEngineeringTask>()
                    .GetById(topTask.ParentPriceEngineeringTaskId.Value);
            }

            return topTask;
        }

    }
}