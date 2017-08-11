using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductServiceWpf(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EquipmentWrapper GetEquipment(EquipmentWrapper templateEquipment = null)
        {
            var groups = _unitOfWork.ParametersGroups.GetAll().Select(x => x.Model).ToList();
            var products = _unitOfWork.Products.GetAll().Select(x => x.Model).ToList();
            var equipments = _unitOfWork.Equipments.GetAll().Select(x => x.Model).ToList();
            var requiredDependentEquipmentsParameters = _unitOfWork.RequiredDependentEquipmentsParameters.GetAll().Select(x => x.Model).ToList();

            EquipmentSelector equipmentSelector = new EquipmentSelector(groups, products, equipments, requiredDependentEquipmentsParameters, preSelectedEquipment: templateEquipment?.Model);
            SelectEquipmentWindow window = new SelectEquipmentWindow {DataContext = equipmentSelector};
            window.ShowDialog();

            return _unitOfWork.Equipments.GetWrapper(equipmentSelector.SelectedEquipment);
        }
    }
}