using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangeProducerCommand : ProjectUnitEditBaseCommand
    {
        private static ISelectService _selectService;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeProducerCommand(IProjectUnit projectUnit, ISelectService selectService, IUnitOfWork unitOfWork) : base(projectUnit)
        {
            _selectService = selectService;
            _unitOfWork = unitOfWork;
        }

        public override bool CanExecute(object parameter)
        {
            return ProjectUnit.Specification == null;
        }

        public override void Execute(object parameter)
        {
            var hvtProducersActivityField = GlobalAppProperties.Actual.HvtProducersActivityField;
            var producer = _selectService.SelectItem(_unitOfWork.Repository<Company>().Find(x => x.ActivityFilds.ContainsById(hvtProducersActivityField)));
            if (producer == null) return;
            ProjectUnit.Producer = new CompanyEmptyWrapper(producer);
        }
    }
}