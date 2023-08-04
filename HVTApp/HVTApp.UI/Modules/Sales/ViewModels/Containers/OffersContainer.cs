using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Modules.Sales.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.ViewModels.Containers
{
    public class OffersContainer : BaseContainerViewModelWithFilterByProject<Offer, OfferLookup, AfterSaveOfferEvent, AfterRemoveOfferEvent, OfferView>
    {
        private List<OfferUnit> _offerUnits;

        public ICommand PrintOfferCommand { get; }
        public ICommand OfferByProjectCommand { get; }
        public ICommand OfferByOfferCommand { get; }

        public OffersContainer(IUnityContainer container, ISelectedProjectItemChanged vm) : base(container, vm)
        {
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<AfterRemoveOfferUnitEvent>().Subscribe(offer => _offerUnits.RemoveIfContainsById(offer));
            eventAggregator.GetEvent<AfterSaveOfferUnitEvent>().Subscribe(offer => _offerUnits.ReAddById(offer));

            #region PrintOfferCommand

            PrintOfferCommand = new DelegateLogCommand(
                () =>
                {
                    var fileManagerService = container.Resolve<IFileManagerService>();
                    container.Resolve<IPrintOfferService>().PrintOffer(this.SelectedItem.Id, fileManagerService.GetPath(this.SelectedItem.Entity));
                }, 
                () => this.SelectedItem != null);

            this.SelectedItemChangedEvent += lookup => ((DelegateLogCommand)PrintOfferCommand).RaiseCanExecuteChanged();

            #endregion

            #region OfferByProjectCommand

            OfferByProjectCommand = new DelegateLogCommand(
                () =>
                {
                    var parameters = new NavigationParameters {{nameof(Project), vm.SelectedProjectItem.Project}};
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<OfferView>(parameters);
                },
                () => vm.SelectedProjectItem != null);

            vm.SelectedProjectItemChanged += item => ((DelegateLogCommand)OfferByProjectCommand).RaiseCanExecuteChanged();

            #endregion

            #region OfferByOfferCommand

            OfferByOfferCommand = new DelegateLogCommand(
                () =>
                {
                    var parameters = new NavigationParameters {{nameof(Offer), this.SelectedItem.Entity}};
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<OfferView>(parameters);
                },
                () => this.SelectedItem != null);

            this.SelectedItemChangedEvent += lookup => ((DelegateLogCommand)OfferByOfferCommand).RaiseCanExecuteChanged();

            #endregion
        }

        protected override OfferLookup MakeLookup(Offer offer)
        {
            var offerUnits = _offerUnits.Where(offerUnit => offerUnit.Offer.Id == offer.Id);
            return new OfferLookup(offer, offerUnits, Container);
        }

        protected override IEnumerable<OfferLookup> GetLookups(IUnitOfWork unitOfWork)
        {
            _offerUnits = GlobalAppProperties.User.RoleCurrent == Role.Admin
                ? unitOfWork.Repository<OfferUnit>().GetAll()
                : ((IOfferUnitRepository)unitOfWork.Repository<OfferUnit>()).GetAllOfCurrentUser().ToList();
            return _offerUnits
                .Select(offerUnit => offerUnit.Offer)
                .Distinct()
                .OrderByDescending(offer => offer.Date)
                .Select(MakeLookup);
        }

        protected override IEnumerable<OfferLookup> GetActualLookups(Project project)
        {
            return AllLookups
                .Where(offerLookup => offerLookup.Project.Id == project.Id)
                .OrderByDescending(offerLookup => offerLookup.Entity.Date);
        }

        protected override bool CanBeShown(Offer offer)
        {
            return SelectedProject != null && 
                   SelectedProject.Id == offer.Project.Id;
        }

        public override void EditSelectedItem()
        {
            var parameters = new NavigationParameters { { nameof(Offer), this.SelectedItem.Entity }, { "edit", true } };
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<OfferView>(parameters);
        }
    }
}