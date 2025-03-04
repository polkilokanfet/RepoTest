using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangeFacilityCommand : ProjectUnitEditUnitOfWorkBaseCommand
    {
        private static ISelectService _selectService;

        public ChangeFacilityCommand(IProjectUnit projectUnit, ISelectService selectService, IUnitOfWork unitOfWork) : base(projectUnit, unitOfWork)
        {
            _selectService = selectService;
        }

        public override void Execute(object parameter)
        {
            var facility = _selectService.SelectItem<Facility>(selectedItemId: ProjectUnit.Facility?.Model.Id);
            if (facility == null) return;
            if (facility.Id == ProjectUnit.Facility?.Model.Id) return;
            ProjectUnit.Facility = new FacilityEmptyWrapper(UnitOfWork.Repository<Facility>().GetById(facility.Id));
        }
    }
}