using System.Linq;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using HVTApp.Services.GetProductService;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
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
            var product = _getProductService.GetProduct(Item.Product.Model);
            if (product != null) Item.Product = new ProductWrapper(product);
        }
    }
}
