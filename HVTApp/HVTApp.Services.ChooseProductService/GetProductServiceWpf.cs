using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Model.Wrappers;
using HVTApp.Services.GetEquipmentService;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ProductWrapper GetEquipment(ProductWrapper templateEquipment = null)
        {
            var groups = _unitOfWork.ParametersGroups.GetAll().Select(x => x.Model).ToList();
            var products = _unitOfWork.Parts.GetAll().Select(x => x.Model).ToList();
            var equipments = _unitOfWork.Products.GetAll().Select(x => x.Model).ToList();
            var requiredDependentEquipmentsParameters = _unitOfWork.RequiredDependentEquipmentsParameters.GetAll().Select(x => x.Model).ToList();

            EquipmentSelector equipmentSelector = new EquipmentSelector(groups, products, equipments, requiredDependentEquipmentsParameters, preSelectedProduct: templateEquipment?.Model);
            SelectEquipmentWindow window = new SelectEquipmentWindow {DataContext = equipmentSelector};
            window.ShowDialog();

            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return templateEquipment;
            return _unitOfWork.Products.GetWrapper(equipmentSelector.SelectedProduct);
        }
    }
}