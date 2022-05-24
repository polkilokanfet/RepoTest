using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Unit.ViewModel
{
    public class PriceEngineeringTaskTceViewModelFrontManager : PriceEngineeringTaskTceViewModel
    {
        public DelegateLogCommand StartCommand { get; }
        public DelegateLogCommand CreatePriceCalculationCommand { get; }
        
        public PriceEngineeringTaskTceViewModelFrontManager(IUnityContainer container) : base(container)
        {
            StartCommand = new DelegateLogCommand(
                () =>
                {
                    SaveCommand.Execute(null);
                    StartCommand.RaiseCanExecuteChanged();
                },
                () => 
                    this.Item != null && 
                    Item.IsValid && 
                    Item.IsChanged && 
                    UnitOfWork.Repository<PriceEngineeringTaskTce>().GetById(Item.Model.Id) == null);

            CreatePriceCalculationCommand = new DelegateLogCommand(
                () =>
                {

                },
                () => this.Item != null && this.Item.Model.LastAction == PriceEngineeringTaskTceStoryItemStoryAction.Finish);

            this.ViewModelIsLoaded += () =>
            {
                StartCommand.RaiseCanExecuteChanged();
                CreatePriceCalculationCommand.RaiseCanExecuteChanged();
            };
        }
    }
}