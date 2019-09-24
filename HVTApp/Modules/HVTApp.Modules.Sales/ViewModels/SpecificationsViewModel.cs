using System.Data.Entity.Infrastructure;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
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
        public SalesUnitsSpecificationBase Groups { get; }

        public SpecificationsViewModel(IUnityContainer container) : base(container)
        {
            Groups = container.Resolve<SalesUnitsSpecificationBase>();

            var eventAggregator = container.Resolve<IEventAggregator>();
            this.SelectedLookupChanged += specificationLookup =>
            {
                eventAggregator.GetEvent<SelectedSpecificationChangedEvent>().Publish(specificationLookup?.Entity);
            };
        }

        public void LoadSpecifications()
        {
            var salesUnits =
                UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Manager.Id == GlobalAppProperties.User.Id
                                                             && x.Specification != null);
            this.Load(salesUnits.Select(x => x.Specification).Distinct());
        }

        protected override void InitSpecialCommands()
        {
            EditItemCommand = new DelegateCommand(EditItemCommandExecute, () => SelectedItem != null);
            RemoveItemCommand = new DelegateCommand(
                async () =>
                {
                    var dr = MessageService.ShowYesNoMessageDialog("Удаление", $"Вы действительно хотите удалить \"{SelectedLookup.DisplayMember}\"?");
                    if (dr != MessageDialogResult.Yes) return;

                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    var specification = await unitOfWork.Repository<Specification>().GetByIdAsync(SelectedLookup.Id);
                    if (specification != null)
                    {
                        var salesUnits = unitOfWork.Repository<SalesUnit>().Find(x => x.Specification?.Id == specification.Id);
                        salesUnits.ForEach(x => x.Specification = null);
                        try
                        {
                            unitOfWork.Repository<Specification>().Delete(specification);
                            await unitOfWork.SaveChangesAsync();
                        }
                        catch (DbUpdateException e)
                        {
                            MessageService.ShowOkMessageDialog(e.GetType().ToString(), e.GetAllExceptions());
                            return;
                        }
                    }

                    EventAggregator.GetEvent<AfterRemoveSpecificationEvent>().Publish(specification);

                }, 
                () => SelectedItem != null);
        }

        private void EditItemCommandExecute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { nameof(Specification), SelectedItem } });
        }
    }
}