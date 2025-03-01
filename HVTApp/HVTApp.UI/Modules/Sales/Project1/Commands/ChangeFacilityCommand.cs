using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangeFacilityCommand : ProjectUnitEditBaseCommand
    {
        private static ISelectService _selectService;

        public ChangeFacilityCommand(IProjectUnit projectUnit, ISelectService selectService) : base(projectUnit)
        {
            _selectService = selectService;
        }

        public override void Execute(object parameter)
        {
            var facility = _selectService.SelectItem<Facility>(selectedItemId: ProjectUnit.Facility.Model.Id);
            if (facility == null) return;
            ProjectUnit.Facility = new FacilityEmptyWrapper(facility);
        }
    }
}