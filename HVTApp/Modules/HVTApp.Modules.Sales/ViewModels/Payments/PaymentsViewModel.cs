using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork _unitOfWork;
        private IValidatableChangeTrackingCollection<SalesUnitWrapper> _salesUnitWrappers;
        private bool _isLoaded = false;
        private PaymentsGroup _selectedGroup;

        public PaymentsGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<PaymentsGroup> PaymentsGroups { get; } = new ObservableCollection<PaymentsGroup>();

        public bool IsLoaded
        {
            get { return _isLoaded; }
            set
            {
                _isLoaded = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ReloadCommand { get; set; }

        public PaymentsViewModel(IUnityContainer container)
        {
            _container = container;

            SaveCommand = new DelegateCommand(SaveCommand_Execute, () => _salesUnitWrappers != null && _salesUnitWrappers.IsChanged && _salesUnitWrappers.IsValid);
            ReloadCommand = new DelegateCommand(ReloadCommand_Execute);
            RefreshCommand = new DelegateCommand(RefreshPayments);
            RemoveCommand = new DelegateCommand(RemoveCommand_Execute, () => SelectedGroup != null && SelectedGroup.WillSave);
        }

        public async Task LoadAsync()
        {
            IsLoaded = false;

            _unitOfWork = _container.Resolve<IUnitOfWork>();

            //загружаем все юниты
            var salesUnitWrappers = (await _unitOfWork.GetRepository<SalesUnit>().GetAllAsync()).Select(x => new SalesUnitWrapper(x));
            
            //фиксируем их в коллекции для отслеживания изменений
            _salesUnitWrappers = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(salesUnitWrappers);

            //подписка на изменение
            _salesUnitWrappers.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            _salesUnitWrappers.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();


            //актуализация плановых поступлений
            _salesUnitWrappers.ForEach(Actualization);

            RefreshPayments();

            IsLoaded = true;
        }

        private void RefreshPayments()
        {
            var payments = new List<PaymentWrapper>();
            foreach (var salesUnitWrapper in _salesUnitWrappers)
            {
                payments.AddRange(GetPayments(salesUnitWrapper));
            }

            payments = payments.OrderBy(x => x.PaymentPlannedWrapper.Date).ToList();
            var groups = payments.GroupBy(x => new
            {
                ProjectId = x.SalesUnit.Project.Id,
                ProductId = x.SalesUnit.Product.Id,
                FacilityId = x.SalesUnit.Facility.Id,
                Cost = x.SalesUnit.Cost,
                Part = x.PaymentPlannedWrapper.Part,
                Date = x.PaymentPlannedWrapper.Date,
                ConditionId = x.PaymentPlannedWrapper.Condition.Id,
                WillSave = x.WillSave
            });

            PaymentsGroups.Clear();
            PaymentsGroups.AddRange(groups.Select(x => new PaymentsGroup(x)));
        }

        private void Actualization(SalesUnitWrapper salesUnitWrapper)
        {
            var paymentsWrappers = salesUnitWrapper.PaymentsPlanned;
            var paymentsActual = salesUnitWrapper.Model.PaymentsPlannedActual;
            var remove = new List<PaymentPlannedWrapper>();
            foreach (var paymentWrapper in paymentsWrappers)
            {
                var paymentActual = paymentsActual.SingleOrDefault(x => x.Id == paymentWrapper.Id);
                if (paymentActual == null)
                {
                    remove.Add(paymentWrapper);
                    _unitOfWork.GetRepository<PaymentPlanned>().Delete(paymentWrapper.Model);
                    continue;
                }

                paymentWrapper.Date = paymentActual.Date;
                paymentWrapper.Part = paymentActual.Part;
            }
            remove.ForEach(x => salesUnitWrapper.PaymentsPlanned.Remove(x));
        }

        private IEnumerable<PaymentWrapper> GetPayments(SalesUnitWrapper salesUnitWrapper)
        {
            //платежи, находящиеся в юните
            foreach (var ppw in salesUnitWrapper.PaymentsPlanned)
            {
                yield return new PaymentWrapper(ppw, salesUnitWrapper, true);
            }

            //платежи сгенерированные
            //необходимо брать именно из Model (актуально)
            foreach (var ppg in salesUnitWrapper.Model.PaymentsPlannedGenerated)
            {
                yield return new PaymentWrapper(new PaymentPlannedWrapper(ppg), salesUnitWrapper, false);
            }
        }


        #region Commands

        private void RemoveCommand_Execute()
        {
            SelectedGroup.RemovePayments(_unitOfWork);
            RefreshPayments();
        }

        private async void ReloadCommand_Execute()
        {
            await LoadAsync();
        }

        private async void SaveCommand_Execute()
        {
            _salesUnitWrappers.AcceptChanges();
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}
