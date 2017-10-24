using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.DataAccess.Lookup;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using HVTApp.Services.GetProductService;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Events;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BaseListViewModel<CompanyWrapper, CompanyDetailsViewModel>
    {
        private readonly IGetProductService _getProductService;
        private readonly ICompanyLookupDataService _companyLookupDataService;
        private readonly IEventAggregator _eventAggregator;
        private CompanyLookup _selectedCompanyLookup;

        public CompaniesViewModel(IUnityContainer container, IGetProductService getProductService, 
            ICompanyLookupDataService companyLookupDataService, IEventAggregator eventAggregator) : 
            base(container)
        {
            _getProductService = getProductService;
            _companyLookupDataService = companyLookupDataService;
            _eventAggregator = eventAggregator;

            LoadedCommand = new DelegateCommand(async () => await LoadAsync());
        }

        public ObservableCollection<CompanyLookup> CompanyLookups { get; } = new ObservableCollection<CompanyLookup>();

        public CompanyLookup SelectedCompanyLookup
        {
            get { return _selectedCompanyLookup; }
            set
            {
                _selectedCompanyLookup = value;
                ((DelegateCommand)EditItemCommand).RaiseCanExecuteChanged();
            }
        }

        protected override bool EditItemCommand_CanExecute()
        {
            return SelectedCompanyLookup != null;
        }

        public async Task LoadAsync()
        {
            var lookups = await _companyLookupDataService.GetCompanyLookupsAsync();
            CompanyLookups.Clear();
            CompanyLookups.AddRange(lookups);
        }


        #region Commands

        public DelegateCommand LoadedCommand { get; set; }


        protected override void RemoveItemCommand_Execute()
        {
        }

        protected override bool RemoveItemCommand_CanExecute()
        {
            return SelectedCompanyLookup != null;
        }

        protected override async void EditItemCommand_Execute()
        {
            var viewModel = Container.Resolve<CompanyDetailsViewModel>();
            await viewModel.LoadAsync(SelectedCompanyLookup.Id);
            DialogService.ShowDialog(viewModel);
        }

        #endregion
    }
}
