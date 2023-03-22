using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.InvoiceForPayment
{
    public class InvoiceForPaymentTasksViewModel : LoadableExportableExpandCollapseViewModel
    {
        private IEnumerable<Unit> _units;
        private Unit _selectedUnit;
        public ObservableCollection<Unit> Units { get; } = new ObservableCollection<Unit>();

        public Unit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                _selectedUnit = value;
                FinishCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogConfirmationCommand FinishCommand { get; }

        public InvoiceForPaymentTasksViewModel(IUnityContainer container) : base(container)
        {
            FinishCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите завершить задачу?",
                () =>
                {
                    var unit = SelectedUnit;
                    Units.Remove(SelectedUnit);
                    UnitOfWork.RemoveEntity(unit.Task);
                },
                () => SelectedUnit != null);
        }

        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            var invoiceForPaymentTasks = UnitOfWork.Repository<InvoiceForPaymentTask>().GetAll();
            foreach (var invoiceForPaymentTask in invoiceForPaymentTasks.Where(x => x.PriceEngineeringTask == null && x.TechnicalRequrements == null).ToList())
            {
                UnitOfWork.RemoveEntity(invoiceForPaymentTask);
                invoiceForPaymentTasks.Remove(invoiceForPaymentTask);
            }
            _units = invoiceForPaymentTasks.Select(task => new Unit(task, UnitOfWork));
        }

        protected override void AfterGetData()
        {
            Units.Clear();
            Units.AddRange(_units);
        }

        public class Unit
        {
            public InvoiceForPaymentTask Task { get; }
            private readonly IUnitOfWork _unitOfWork;

            [Designation("Заявка Team Center"), OrderStatus(-4)]
            public string TceNumber
            {
                get
                {
                    if (Task.PriceEngineeringTask != null)
                        return Task.PriceEngineeringTask.GetPriceEngineeringTasks(_unitOfWork).TceNumber;

                    return _unitOfWork.Repository<TechnicalRequrementsTask>().GetById(Task.TechnicalRequrements.Id).TceNumber;
                }
            }

            [Designation("Объект"), OrderStatus(-6)]
            public string Facility { get; }

            [Designation("Тип продукта"), OrderStatus(-12)]
            public string ProductType { get; }

            [Designation("Обозначение"), OrderStatus(-15)]
            public string Designation { get; }

            [Designation("Кол."), OrderStatus(-17)]
            public int Amount { get; }

            [Designation("НДС, %"), OrderStatus(-20)]
            public double Vat { get; }

            [Designation("Цена"), OrderStatus(-21)]
            public double Cost { get; }

            [Designation("Стоимость"), OrderStatus(-23)]
            public double Sum => Cost * Amount;

            [Designation("Стоимость с НДС"), OrderStatus(-24)]
            public double SumWithVat => Vat * Sum;

            [Designation("Логистика"), OrderStatus(-25)]
            public double CostDelivery { get; }

            [Designation("Стоимость блоков с фиксированной ценой"), OrderStatus(-26)]
            public double FixedCost { get; }

            [Designation("Контрагент"), OrderStatus(-4)]
            public string Contragent { get; }

            [Designation("Договор"), OrderStatus(-34)]
            public string ContractNumber { get; }

            [Designation("Спецификация"), OrderStatus(-35)]
            public string SpecificationNumber { get; }

            [Designation("Дата договора"), OrderStatus(-36)]
            public DateTime? ContractDate { get; }

            [Designation("Дата спецификации"), OrderStatus(-37)]
            public DateTime? SpecificationDate { get; }

            [Designation("Условия оплаты"), OrderStatus(-59)]
            public PaymentConditionSet PaymentConditionSet { get; }

            [Designation("Тип доставки"), OrderStatus(-137)]
            public string DeliveryType { get; }

            [Designation("Адрес доставки"), OrderStatus(-138)]
            public string DeliveryAddress { get; }

            [Designation("Менеджер"), OrderStatus(-33)]
            public string Manager { get; }

            public Unit(InvoiceForPaymentTask task, IUnitOfWork unitOfWork)
            {
                Task = task;
                _unitOfWork = unitOfWork;

                var salesUnits = task.PriceEngineeringTask?.SalesUnits ?? task.TechnicalRequrements.SalesUnits;
                var salesUnit = salesUnits.First();

                Contragent = salesUnit.Specification?.Contract.Contragent.ToString();
                Facility = salesUnit.Facility.ToString();
                ProductType = salesUnit.Product.ProductType.Name;
                Designation = salesUnit.Product.Designation;
                Amount = salesUnits.Count;
                Vat = salesUnit.Vat / 100.0 + 1.0;
                Cost = salesUnit.Cost;
                var costDelivery = salesUnits.Select(unit => unit.CostDelivery).Where(x => x.HasValue).Sum(x => x.Value);
                CostDelivery = -1.0 * costDelivery;

                var price = GlobalAppProperties.PriceService.GetPrice(salesUnit, salesUnit.OrderInTakeDate, true);
                FixedCost = -1.0 * price.SumFixedTotal * Amount;

                Manager = $"{salesUnit.Project.Manager.Employee.Person}";

                if (salesUnit.Specification != null)
                {
                    var specification = salesUnit.Specification;
                    SpecificationNumber = specification.Number;
                    SpecificationDate = specification.Date;

                    ContractNumber = specification.Contract.Number;
                    ContractDate = specification.Contract.Date;
                }

                PaymentConditionSet = salesUnit.PaymentConditionSet;


                DeliveryType = Math.Abs(CostDelivery) > 0 ? "Доставка" : "Самовывоз";

                DeliveryAddress = salesUnit.GetDeliveryAddressString();
            }
        }

    }
}