using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Unit.ViewModel
{
    public class PriceEngineeringTaskTceViewModelBackManager : PriceEngineeringTaskTceViewModel
    {
        public DelegateLogCommand FinishCommand { get; }

        public PriceEngineeringTaskTceViewModelBackManager(IUnityContainer container) : base(container)
        {
            FinishCommand = new DelegateLogCommand(
                () =>
                {
                    this.Item.Story.Add(new PriceEngineeringTaskTceStoryItemWrapper(new PriceEngineeringTaskTceStoryItem
                    {
                        StoryAction = PriceEngineeringTaskTceStoryItemStoryAction.Finish,
                        PriceEngineeringTaskTceId = this.Item.Model.Id
                    }));

                    SaveCommand.Execute(null);
                    FinishCommand.RaiseCanExecuteChanged();
                },
                () =>
                    this.Item != null &&
                    Item.IsValid &&
                    Item.Model.LastAction == PriceEngineeringTaskTceStoryItemStoryAction.Instruct);

            this.ViewModelIsLoaded += () =>
            {
                this.Item.PropertyChanged += (sender, args) => FinishCommand.RaiseCanExecuteChanged();
            };
        }

        protected override bool SaveCommand_CanExecute()
        {
            return Item != null && Item.IsChanged;
        }
    }
}