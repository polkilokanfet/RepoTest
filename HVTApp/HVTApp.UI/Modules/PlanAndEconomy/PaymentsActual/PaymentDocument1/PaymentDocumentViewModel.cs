using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class PaymentDocumentViewModel : BaseDetailsViewModel<PaymentDocumentWrapper1, PaymentDocument, AfterSavePaymentDocumentEvent>
    {
        #region Fields

        private PaymentActualWrapper2 _selectedPayment;
        private object[] _selectedPotentialUnits;
        private IMessageService _messageService;

        #endregion

        #region Props

        /// <summary>
        /// Потенциальные платежи
        /// </summary>
        public ObservableCollection<SalesUnit> Potential { get; } = new ObservableCollection<SalesUnit>();

        /// <summary>
        /// Выбранные потенциальные юниты
        /// </summary>
        public object[] SelectedPotentialUnits
        {
            get => _selectedPotentialUnits;
            set
            {
                _selectedPotentialUnits = value;
                AddPaymentCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Выбранный платеж
        /// </summary>
        public PaymentActualWrapper2 SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                RemovePaymentCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        //костыль
        public IUnitOfWork UnitOfWork1 => this.UnitOfWork;

        public string OrderNumberFilter { get; set; }

        #endregion

        #region ICommand

        /// <summary>
        /// Команда добавления платежа
        /// </summary>
        public AddPaymentCommand AddPaymentCommand { get; }

        /// <summary>
        /// Команда удаления платежа
        /// </summary>
        public RemovePaymentCommand RemovePaymentCommand { get; }

        /// <summary>
        /// Команда удаления платежки
        /// </summary>
        public ICommand RemoveDocumentCommand { get; }

        /// <summary>
        /// Команда оплаты остатка
        /// </summary>
        public RestPaymentCommand RestPaymentCommand { get; }

        public ICommand LoadPotentialCommand { get; }
        
        #endregion

        public PaymentDocumentViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            AddPaymentCommand = new AddPaymentCommand(this, this.Container);
            RemovePaymentCommand = new RemovePaymentCommand(this, this.Container);
            RemoveDocumentCommand = new DelegateLogConfirmationCommand(
                this.Container.Resolve<IMessageService>(),
                () =>
                {
                    foreach (var paymentActualWrapper2 in this.Item.Payments.ToList())
                    {
                        this.Item.Payments.Remove(paymentActualWrapper2);
                        this.UnitOfWork1.Repository<PaymentActual>().Delete(paymentActualWrapper2.Model);
                    }

                    this.UnitOfWork1.Repository<PaymentDocument>().Delete(this.Item.Model);
                    this.UnitOfWork1.SaveChanges();

                    this.GoBackCommand.Execute(null);
                },
                () => this.UnitOfWork1.Repository<PaymentDocument>().GetById(this.Item.Model.Id) != null);
            RestPaymentCommand = new RestPaymentCommand(this, this.Container);

            LoadPotentialCommand = new DelegateLogCommand(
                () =>
                {
                    //формируем список потенциального оборудования 
                    //(исключая то, что в выбранном платеже и полностью оплачено)
                    Potential.Clear();
                    Potential.AddRange(((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetAllForPaymentDocument(OrderNumberFilter)
                        .Except(Item.Payments.Select(payment => payment.SalesUnit))
                        .Where(x => x.IsPaid == false)
                        .OrderBy(x => x.Facility.ToString())
                        .ThenBy(x => x.Project.Name)
                        .ThenBy(x => x.Product.ToString())
                        .ThenBy(x => x.Cost));
                    SelectedPotentialUnits = null;
                });
        }

        protected override PaymentDocumentWrapper1 CreateWrapper(PaymentDocument entity)
        {
            return new PaymentDocumentWrapper1(entity, this.UnitOfWork, _messageService);
        }

        protected override void AfterLoading()
        {
            RestPaymentCommand.RaiseCanExecuteChanged();
            base.AfterLoading();
        }

        protected override void SaveItem()
        {
            //var units = this.Item.Payments.Where(paymentActual => paymentActual.IsChanged).Select(paymentActual => paymentActual.SalesUnit).ToList();
            //var entity = new ActualPaymentEventEntity(this.Item.Model, units);

            base.SaveItem();
            //EventAggregator.GetEvent<AfterSaveActualPaymentDocumentEvent>().Publish(this.Item.Model);


            //EventAggregator.GetEvent<AfterSaveActualPaymentDocumentEvent>().Publish(entity);
            //foreach (var salesUnit in units)
            //{
            //    EventAggregator.GetEvent<AfterSaveActualPaymentEvent>().Publish(salesUnit);
            //}
        }
    }

}