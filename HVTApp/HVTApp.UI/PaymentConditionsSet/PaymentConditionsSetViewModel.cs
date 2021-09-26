using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PaymentConditionsSet
{
    public class PaymentConditionsSetViewModel : ISavable, IDialogRequestClose, ILoadable<PaymentConditionSet>, INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;
        private List<PaymentCondition> _paymentConditions;
        private List<PaymentConditionSet> _paymentConditionSets;
        private PaymentConditionWrapper _selectedPaymentConditionWrapper;

        public PaymentConditionSetWrapper PaymentConditionSetWrapper { get; } = new PaymentConditionSetWrapper(new PaymentConditionSet());

        public string SetToString => PaymentConditionSetWrapper.Model.ToString();
        public string SetValidationErrors => PaymentConditionSetWrapper.Validate(new ValidationContext(this)).ToStringEnum();

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

        public PaymentConditionsSetViewModel(IUnitOfWork unitOfWork, IUnityContainer container)
        {
            _unitOfWork = unitOfWork;
            _paymentConditions = unitOfWork.Repository<PaymentCondition>().GetAll();
            _paymentConditionSets = unitOfWork.Repository<PaymentConditionSet>().GetAll();
            var dialogService = container.Resolve<IDialogService>();

            AddConditionCommand = new DelegateLogCommand(
                () =>
                {
                    PaymentConditionViewModel viewModel = new PaymentConditionViewModel(this.PaymentConditionSetWrapper.Model, container);
                    dialogService.ShowDialog(viewModel);
                    if (viewModel.IsOk == false) return;

                    PaymentCondition paymentCondition1 = viewModel.PaymentConditionWrapper.Model;
                    PaymentCondition paymentCondition = unitOfWork.Repository<PaymentCondition>().Find(condition => Equals(condition, paymentCondition1)).Single();
                    this.PaymentConditionSetWrapper.PaymentConditions.Add(new PaymentConditionWrapper(paymentCondition));
                }, 
                () => PaymentConditionSetWrapper.IsValid == false);

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
                    PaymentConditionSet conditionSet = PaymentConditionSetWrapper.Model;

                    if (_paymentConditionSets.Any(set => set.Equals(conditionSet)) == false)
                    {
                        if (unitOfWork.SaveEntity(conditionSet).OperationCompletedSuccessfully)
                        {
                            container.Resolve<IEventAggregator>().GetEvent<AfterSavePaymentConditionSetEvent>().Publish(conditionSet);
                        }
                    }

                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                }, 
                () => this.PaymentConditionSetWrapper.IsValid);

            OkCommand = new DelegateLogCommand(() => { });

            PaymentConditionSetWrapper.PropertyChanged += (sender, args) =>
            {
                ((DelegateLogCommand)SaveCommand).RaiseCanExecuteChanged();
                AddConditionCommand.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(SetToString));
                OnPropertyChanged(nameof(SetValidationErrors));
            };
            
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
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}