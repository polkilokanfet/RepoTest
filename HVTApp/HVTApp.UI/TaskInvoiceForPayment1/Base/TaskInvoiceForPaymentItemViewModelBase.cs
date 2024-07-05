using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public class TaskInvoiceForPaymentItemViewModelBase : WrapperBase<TaskInvoiceForPaymentItem>
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly List<SalesUnit> SalesUnits;

        public List<TaskInvoiceForPaymentItemViewModelBase> Items =>
            new List<TaskInvoiceForPaymentItemViewModelBase>() {this};

        #region Info

        /// <summary>
        /// Заявка Team Center
        /// </summary>
        public string TceNumber
        {
            get
            {
                if (Model.PriceEngineeringTask != null)
                    return Model.PriceEngineeringTask.GetPriceEngineeringTasks(UnitOfWork).TceNumber;

                return UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(Model.TechnicalRequrements.TaskId).TceNumber;
            }
        }

        [Designation("Заказ"), OrderStatus(-1)]
        public string Order { get; }

        [Designation("Позиции"), OrderStatus(-2)]
        public string OrderPositions { get; }

        [Designation("Владелец объекта"), OrderStatus(-3)]
        public string FacilityOwners { get; }

        [Designation("Контрагент"), OrderStatus(-4)]
        public string Contragent { get; }

        [Designation("Тип контрагента"), OrderStatus(-5)]
        public string ContragentType { get; }

        [Designation("Объект"), OrderStatus(-6)]
        public string Facility { get; }

        [Designation("Страна поставки"), OrderStatus(-1000)]
        public Country Country { get; }

        [Designation("Федеральный округ"), OrderStatus(-9)]
        public string District { get; }

        [Designation("Сегмент"), OrderStatus(-10)]
        public string Segment { get; }

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

        [Designation("Менеджер"), OrderStatus(-33)]
        public string Manager { get; }

        [Designation("Договор"), OrderStatus(-34)]
        public string ContractNumber { get; }

        [Designation("Спецификация"), OrderStatus(-35)]
        public string SpecificationNumber { get; }

        [Designation("Дата договора"), OrderStatus(-36)]
        public DateTime? ContractDate { get; }

        [Designation("Дата спецификации"), OrderStatus(-37)]
        public DateTime? SpecificationDate { get; }

        [Designation("ОИТ"), OrderStatus(-43)]
        public DateTime OrderInTakeDate { get; }

        [Designation("Условия оплаты"), OrderStatus(-59)]
        public PaymentConditionSet PaymentConditionSet { get; }

        [Designation("Тип доставки"), OrderStatus(-137)]
        public string DeliveryType { get; }

        [Designation("Адрес доставки"), OrderStatus(-138)]
        public string DeliveryAddress { get; }

        [Designation("Срок производства"), OrderStatus(-245)]
        public int ProductionTerm { get; }

        #endregion

        public TaskInvoiceForPaymentItemViewModelBase(TaskInvoiceForPaymentItem model, IUnitOfWork unitOfWork) : base(model)
        {
            UnitOfWork = unitOfWork;

            SalesUnits = model.PriceEngineeringTask?.SalesUnits ?? model.TechnicalRequrements.SalesUnits;
            var salesUnit = SalesUnits.First();

            FixedCost = -1.0 * SalesUnits.Sum(x => x.FixedCost);

            Order = salesUnit.Order?.ToString();
            OrderPositions = SalesUnits.Select(unit => unit.OrderPosition).GetOrderPositions();

            ProductionTerm = salesUnit.ProductionTerm;
            var owners = new List<Company> { salesUnit.Facility.OwnerCompany };
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ToStringEnum();
            var contragent = salesUnit.Specification?.Contract.Contragent;
            Contragent = salesUnit.Specification?.Contract.Contragent.ToString();
            ContragentType = GetContragentType(contragent, SalesUnits);
            Facility = salesUnit.Facility.ToString();
            var region = salesUnit.Facility.GetRegion();
            Country = region?.District.Country;
            District = region?.District.Name;
            Segment = SegmentConverter(GetSegment(SalesUnits));
            ProductType = salesUnit.Product.ProductType.Name;
            Designation = salesUnit.Product.Designation;
            Amount = SalesUnits.Count;
            Vat = salesUnit.Vat / 100.0 + 1.0;
            Cost = salesUnit.Cost;
            var costDelivery = SalesUnits.Select(unit => unit.CostDelivery).Where(x => x.HasValue).Sum(x => x.Value);
            CostDelivery = -1.0 * costDelivery;

            Manager = $"{salesUnit.Project.Manager.Employee.Person}";

            var specifications = SalesUnits.Where(unit => unit.Specification != null).Distinct().ToList();
            SpecificationNumber = specifications.Select(unit => unit.Specification.Number).ToStringEnum();
            ContractNumber = specifications.Select(unit => unit.Specification.Contract.Number).Distinct().ToStringEnum();

            if (salesUnit.Specification != null)
            {
                var specification = salesUnit.Specification;
                SpecificationDate = specification.Date;
                ContractDate = specification.Contract.Date;
            }


            OrderInTakeDate = salesUnit.OrderInTakeDate;

            PaymentConditionSet = salesUnit.PaymentConditionSet;


            DeliveryType = Math.Abs(CostDelivery) > 0 ? "Доставка" : "Самовывоз";

            DeliveryAddress = salesUnit.GetDeliveryAddressString();

        }

        #region GetInfo

        private string GetContragentType(Company contragent, IEnumerable<SalesUnit> salesUnits)
        {
            if (Contragent == null)
                return "Нет данных";

            var salesUnit = salesUnits.First();
            if (Equals(salesUnit.Facility.OwnerCompany, contragent) ||
                salesUnit.Facility.OwnerCompany.ParentCompanies().Contains(contragent))
                return "Конечный заказчик";

            //if (_tenders.FirstOrDefault(x => Equals(x.Winner, contragent)) != null) return "Подрядчик";

            return "Посредник";
        }

        private string GetSegment(IEnumerable<SalesUnit> salesUnits)
        {
            //актуальный список сфер деятельности
            var actualActivities = new List<ActivityFieldEnum>
            {
                ActivityFieldEnum.ElectricityDistribution,
                ActivityFieldEnum.ElectricityTransmission,
                ActivityFieldEnum.ElectricityGeneration,
                ActivityFieldEnum.Fuel,
                ActivityFieldEnum.RailWay,
                ActivityFieldEnum.IndustrialEnterprise
            };

            //сегмент по владельцам объекта
            var owner = salesUnits.First().Facility.OwnerCompany;
            do
            {
                var activityField = owner.ActivityFilds.FirstOrDefault(x => actualActivities.Contains(x.ActivityFieldEnum));
                if (activityField != null)
                    return activityField.Name;
                owner = owner.ParentCompany;
            } while (owner != null);

            return "Промышленное предприятие";
        }

        private string SegmentConverter(string segment)
        {
            switch (segment)
            {
                case "Генерация электроэнергии": return "Генерация";
                case "Железная дорога": return "РЖД";
                case "Передача электроэнергии": return "Сети";
                case "Промышленное предприятие": return "Промышленность";
                case "Распределение электроэнергии": return "Распределение";
                case "Топливно-энергетический сектор": return "Нефтегаз.";
            }

            return segment;
        }
        
        #endregion

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.SalesUnits != null)
            {
                foreach (var salesUnit in this.SalesUnits.Where(salesUnit => salesUnit.Specification == null))
                {
                    yield return new ValidationResult($"не имеет спецификации: {salesUnit}");
                }
            }
        }
    }
}