using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class ChangePaymentsCommand : DelegateCommand<IProjectUnit>
    {
        private static IUnitOfWork _unitOfWork;
        private static ISelectService _selectService;

        public ChangePaymentsCommand(IUnitOfWork unitOfWork, ISelectService selectService) : base(ExecuteMethod)
        {
            _unitOfWork = unitOfWork;
            _selectService = selectService;
        }

        private static void ExecuteMethod(IProjectUnit projectUnit)
        {
            var paymentConditionSets = _unitOfWork.Repository<PaymentConditionSet>().GetAllAsNoTracking();
            var paymentConditionSet = _selectService.SelectItem(paymentConditionSets, projectUnit.PaymentConditionSet.Model.Id);
            if (paymentConditionSet == null) return;
            projectUnit.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(paymentConditionSet);
        }
    }
}