using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangePaymentsCommand : ProjectUnitEditUnitOfWorkBaseCommand
    {
        private static ISelectService _selectService;

        public ChangePaymentsCommand(IProjectUnit projectUnit, ISelectService selectService, IUnitOfWork unitOfWork) : base(projectUnit, unitOfWork)
        {
            _selectService = selectService;
        }

        public override void Execute(object parameter)
        {
            var paymentConditionSet = _selectService.SelectItem<PaymentConditionSet>(selectedItemId: ProjectUnit.PaymentConditionSet?.Model.Id);
            if (paymentConditionSet == null) return;
            if (paymentConditionSet.Id == ProjectUnit.PaymentConditionSet?.Model.Id) return;
            ProjectUnit.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(UnitOfWork.Repository<PaymentConditionSet>().GetById(paymentConditionSet.Id));
        }
    }
}