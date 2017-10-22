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

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BaseListViewModel<CompanyWrapper, CompanyDetailsWindowModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly IUnityContainer _container;
        private readonly IGetProductService _getProductService;
        private readonly ICompanyLookupDataService _companyLookupDataService;

        public CompaniesViewModel(IUnitOfWork unitOfWork, IDialogService dialogService, IUnityContainer container, IGetProductService getProductService, ICompanyLookupDataService companyLookupDataService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _container = container;
            _getProductService = getProductService;
            _companyLookupDataService = companyLookupDataService;

            //_unitOfWork.Companies.GetAll().Select(x => new CompanyWrapper(x)).ForEach(Items.Add);

            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);
            LoadedCommand = new DelegateCommand(async () => await LoadAsync());
        }

        public ObservableCollection<CompanyLookup> CompanyLookups { get; } = new ObservableCollection<CompanyLookup>();

        public async Task LoadAsync()
        {
            var lookups = await _companyLookupDataService.GetCompanyLookupsAsync();
            CompanyLookups.Clear();
            CompanyLookups.AddRange(lookups);
        }



        #region Commands

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand LoadedCommand { get; set; }


        protected override void RemoveItemCommand_Execute()
        {
            _getProductService.GetProduct();
        }

        protected override bool RemoveItemCommand_CanExecute()
        {
            return true;
        }

        private void RefreshCommand_Execute()
        {
            Items.Clear();
            _unitOfWork.Companies.GetAll().Select(x => new CompanyWrapper(x)).ForEach(Items.Add);
        }

        #endregion
    }
}
