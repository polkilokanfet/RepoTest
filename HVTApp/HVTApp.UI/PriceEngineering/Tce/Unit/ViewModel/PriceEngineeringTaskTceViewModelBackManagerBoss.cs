using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.Tce.Unit.ViewModel
{
    public class PriceEngineeringTaskTceViewModelBackManagerBoss : PriceEngineeringTaskTceViewModel
    {
        public DelegateLogCommand InstructCommand { get; }

        public PriceEngineeringTaskTceViewModelBackManagerBoss(IUnityContainer container) : base(container)
        {
            InstructCommand = new DelegateLogCommand(
                () =>
                {
                    this.Item.Story.Add(new PriceEngineeringTaskTceStoryItemWrapper(new PriceEngineeringTaskTceStoryItem
                    {
                        StoryAction = PriceEngineeringTaskTceStoryItemStoryAction.Instruct,
                        PriceEngineeringTaskTceId = this.Item.Model.Id
                    }));

                    SaveCommand.Execute(null);
                    InstructCommand.RaiseCanExecuteChanged();
                },
                () =>
                    this.Item != null &&
                    Item.IsValid &&
                    Item.Model.LastAction == PriceEngineeringTaskTceStoryItemStoryAction.Start);
        }
    }
}