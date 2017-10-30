using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class ProjectUnitsDetailsViewModel : BaseDetailsViewModel<ProjectUnitWrapper, ProjectUnit, AfterSaveProjectUnitEvent>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChooseService _chooseService;
        private readonly IGetProductService _getProductService;

        public ProjectUnitsDetailsViewModel(IUnitOfWork unitOfWork, IChooseService chooseService, IGetProductService getProductService, ProjectUnitWrapper item, IUnityContainer container) : base(container)
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


        private async void ChooseFacilityCommand_Execute()
        {
            var facility = _chooseService.ChooseDialog((await _unitOfWork.FacilityRepository.GetAllAsync()).Select(x => new FacilityWrapper(x)));
            if (facility != null) Item.Facility = facility;
        }

        private void RemoveFacilityCommand_Execute()
        {
            Item.Facility = null;
        }

        private async void ChooseProductCommand_Execute()
        {
            var product = await _getProductService.GetProduct(Item.Product.Model);
            if (product != null) Item.Product = new ProductWrapper(product);
        }
    }
}