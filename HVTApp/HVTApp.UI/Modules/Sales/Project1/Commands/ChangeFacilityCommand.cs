using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangeFacilityCommand : DelegateCommand<IProjectUnit>
    {
        private static IUnitOfWork _unitOfWork;
        private static ISelectService _selectService;

        public ChangeFacilityCommand(IUnitOfWork unitOfWork, ISelectService selectService) : base(ExecuteMethod)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;
        }

        private static void ExecuteMethod(IProjectUnit projectUnit)
        {
            var facilities = _unitOfWork.Repository<Facility>().GetAllAsNoTracking();
            var facility = _selectService.SelectItem(facilities, projectUnit.Facility.Id);
            if (facility == null) return;
            projectUnit.Facility = facility;
        }
    }
}