using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangeFacilityCommand : ICommand
    {
        private readonly IProjectUnit _projectUnit;
        private static IUnitOfWork _unitOfWork;
        private static ISelectService _selectService;

        public ChangeFacilityCommand(IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService)
        {
            _projectUnit = projectUnit;
            _unitOfWork = unitOfWork;
            _selectService = selectService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var facilities = _unitOfWork.Repository<Facility>().GetAllAsNoTracking();
            var facility = _selectService.SelectItem(facilities, _projectUnit.FacilityId);
            if (facility == null) return;
            _projectUnit.SetFacility(facility);
        }

        public event EventHandler CanExecuteChanged;
    }
}