using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationsViewModel : SpecificationLookupListViewModel
    {
        public SalesUnitsSpecificetionBase Groups { get; }

        public SpecificationsViewModel(IUnityContainer container) : base(container)
        {
            Groups = container.Resolve<SalesUnitsSpecificetionBase>();

            var eventAggregator = container.Resolve<IEventAggregator>();
            this.SelectedLookupChanged += specificationLookup =>
            {
                eventAggregator.GetEvent<SelectedSpecificationChangedEvent>().Publish(specificationLookup?.Entity);
            };
        }

        protected override void InitSpecialCommands()
        {
            EditItemCommand = new DelegateCommand(EditItemCommandExecute, () => SelectedItem != null);
            RemoveItemCommand = new DelegateCommand(RemoveItemCommand_ExecuteAsync, () => SelectedItem != null);
        }

        private void EditItemCommandExecute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { nameof(Specification), SelectedItem } });
        }
    }
}