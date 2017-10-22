using System.Linq;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using HVTApp.Services.GetProductService;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProductUnitsDetailsViewModel : BaseDetailsViewModel<ProjectUnitWrapper>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChooseService _chooseService;
        private readonly IGetProductService _getProductService;

        public ProductUnitsDetailsViewModel(IUnitOfWork unitOfWork, IChooseService chooseService, IGetProductService getProductService, ProjectUnitWrapper item) : base(item)
        {
            _unitOfWork = unitOfWork;
            _chooseService = chooseService;
            _getProductService = getProductService;

            ChooseFacilityCommand = new DelegateCommand(ChooseFacilityCommand_Execute);
            RemoveFacilityCommand = new DelegateCommand(RemoveFacilityCommand_Execute);
            ChooseProductCommand = new DelegateCommand(ChooseProductCommand_Execute);
        }

        public int Amount { get; set; }

        public DelegateCommand ChooseFacilityCommand { get; }
        public DelegateCommand RemoveFacilityCommand { get; }
        public DelegateCommand ChooseProductCommand { get; }


        private void ChooseFacilityCommand_Execute()
        {
            var facility = _chooseService.ChooseDialog(_unitOfWork.Facilities.GetAll().Select(x => new FacilityWrapper(x)));
            if (facility != null) Item.Facility = facility;
        }

        private void RemoveFacilityCommand_Execute()
        {
            Item.Facility = null;
        }

        private void ChooseProductCommand_Execute()
        {
            var product = _getProductService.GetProduct(Item.Product);
            if (product != null) Item.Product = product;
        }
    }
}
