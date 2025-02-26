using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitEditViewModel : BindableBase
    {
        public IProjectUnit ProjectUnit { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentConditionsSetCommand { get; }
        public ICommand ChangeProducerCommand { get; }
        public ICommand RemoveProducerCommand { get; }

        public ProjectUnitEditViewModel(IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService)
        {
            ProjectUnit = projectUnit;
            ChangeFacilityCommand = new ChangeFacilityCommand(projectUnit, unitOfWork, selectService);
            RemoveProducerCommand = new RemoveProducerCommand(projectUnit);
        }
    }
}