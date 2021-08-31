using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;

namespace HVTApp.UI.PaymentConditionsSet
{
    public class PaymentConditionsSetViewModel : ISavable, IDialogRequestClose, ILoadable<PaymentConditionSet>
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<PaymentCondition> _paymentConditions;
        private List<PaymentConditionSet> _paymentConditionSets;
        private PaymentConditionWrapper _selectedPaymentConditionWrapper;

        public PaymentConditionSetWrapper PaymentConditionSetWrapper { get; } =
            new PaymentConditionSetWrapper(new PaymentConditionSet());

        public PaymentConditionWrapper2 PaymentConditionWrapper { get; }

        public PaymentConditionWrapper SelectedPaymentConditionWrapper
        {
            get => _selectedPaymentConditionWrapper;
            set
            {
                _selectedPaymentConditionWrapper = value;
                this.RemoveConditionCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand AddConditionCommand { get; }
        public DelegateLogCommand RemoveConditionCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand OkCommand { get; }

        public PaymentConditionsSetViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _paymentConditions = unitOfWork.Repository<PaymentCondition>().GetAll();
            _paymentConditionSets = unitOfWork.Repository<PaymentConditionSet>().GetAll();

            PaymentConditionWrapper = new PaymentConditionWrapper2(new PaymentCondition(), PaymentConditionSetWrapper);

            AddConditionCommand = new DelegateLogCommand(
                () =>
                {
                    PaymentCondition targetPaymentCondition =
                        _paymentConditions.SingleOrDefault(paymentCondition => paymentCondition.Equals(PaymentConditionWrapper.Model));

                    if (targetPaymentCondition == null)
                    {
                        targetPaymentCondition = new PaymentCondition
                        {
                            PaymentConditionPoint = PaymentConditionWrapper.Model.PaymentConditionPoint,
                            DaysToPoint = PaymentConditionWrapper.Model.DaysToPoint,
                            Part = PaymentConditionWrapper.Model.Part
                        };
                        _paymentConditions.Add(targetPaymentCondition);
                    }

                    PaymentConditionSetWrapper.PaymentConditions.Add(new PaymentConditionWrapper(targetPaymentCondition));

                    InitPaymentConditionWrapper();

                    AddConditionCommand.RaiseCanExecuteChanged();
                }, 
                () => PaymentConditionWrapper.IsValid);

            RemoveConditionCommand = new DelegateLogCommand(
                () =>
                {
                    this.PaymentConditionSetWrapper.PaymentConditions.Remove(this.SelectedPaymentConditionWrapper);
                    SelectedPaymentConditionWrapper = null;
                },
                () => SelectedPaymentConditionWrapper != null);

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    if (_paymentConditionSets.Any(set => set.Equals(PaymentConditionSetWrapper.Model)))
                    {
                        return;
                    }

                    unitOfWork.Repository<PaymentConditionSet>().Add(PaymentConditionSetWrapper.Model);
                    unitOfWork.SaveChanges();
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                }, 
                () => this.PaymentConditionSetWrapper.IsValid);

            OkCommand = new DelegateLogCommand(
                () =>
                {

                });

            PaymentConditionSetWrapper.PropertyChanged += (sender, args) => ((DelegateLogCommand)SaveCommand).RaiseCanExecuteChanged();
            PaymentConditionWrapper.PropertyChanged += (sender, args) => this.AddConditionCommand.RaiseCanExecuteChanged();
            
            InitPaymentConditionWrapper();
        }

        private void InitPaymentConditionWrapper()
        {
            PaymentConditionWrapper.Part = 1 - PaymentConditionSetWrapper.PaymentConditions.Sum(x => x.Part);
            PaymentConditionWrapper.DaysTo = 14;
            PaymentConditionWrapper.IsBefore = true;
            PaymentConditionWrapper.PaymentConditionPoint = new PaymentConditionPointWrapper(_unitOfWork.Repository<PaymentConditionPoint>().GetAll().First());
        }

        public void Load(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Load(PaymentConditionSet entity)
        {
        }

        public void Load(PaymentConditionSet entity, IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}