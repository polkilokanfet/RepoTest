using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PaymentConditionsSet
{
    public class PaymentConditionViewModel : IDialogRequestClose
    {
        private IUnitOfWork _unitOfWork;

        public PaymentConditionWrapper2 PaymentConditionWrapper { get; }

        public DelegateLogCommand OkCommand { get; }

        public DelegateLogCommand SelectPaymentConditionPointCommand { get; }
        public DelegateLogCommand ClearPaymentConditionPointCommand { get; }

        public bool IsOk { get; private set; } = false;

        public PaymentConditionViewModel(PaymentConditionSet paymentConditionSet, IUnityContainer container)
        {
            _unitOfWork = container.Resolve<IUnitOfWork>();
            PaymentConditionWrapper = new PaymentConditionWrapper2(paymentConditionSet)
            {
                Part = 1.0 - paymentConditionSet.PaymentConditions.Sum(condition => condition.Part),
                PaymentConditionPoint = new PaymentConditionPointWrapper(_unitOfWork.Repository<PaymentConditionPoint>().GetAll().First())
            };

            OkCommand = new DelegateLogCommand(
                () =>
                {
                    PaymentCondition paymentCondition = this.PaymentConditionWrapper.Model;
                    if (_unitOfWork.Repository<PaymentCondition>()
                        .Find(condition => Equals(condition, paymentCondition))
                        .Any() == false)
                    {
                        this.PaymentConditionWrapper.AcceptChanges();
                        _unitOfWork.Repository<PaymentCondition>().Add(paymentCondition);
                        _unitOfWork.SaveChanges();
                    }

                    IsOk = true;
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                }, 
                () => PaymentConditionWrapper.IsValid && PaymentConditionWrapper.IsChanged);

            PaymentConditionWrapper.PropertyChanged += (sender, args) => OkCommand.RaiseCanExecuteChanged();

            SelectPaymentConditionPointCommand = new DelegateLogCommand(
                () =>
                {
                    ISelectService selectService = container.Resolve<ISelectService>();
                    PaymentConditionPoint point = selectService.SelectItem(_unitOfWork.Repository<PaymentConditionPoint>().GetAll());
                    if (point != null)
                    {
                        this.PaymentConditionWrapper.PaymentConditionPoint = new PaymentConditionPointWrapper(point);
                    }
                });

            ClearPaymentConditionPointCommand = new DelegateLogCommand(
                () =>
                {
                    this.PaymentConditionWrapper.PaymentConditionPoint = null;
                });
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}