using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.TaskInvoiceForPayment1.ForPlanMaker;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public class TaskInvoiceForPaymentItemWrapperBase : WrapperBase<TaskInvoiceForPaymentItem>
    {
        private readonly string _order;
        private string _orderPositions;

        public List<TaskInvoiceForPaymentItemWrapperBase> Items => 
            new List<TaskInvoiceForPaymentItemWrapperBase> {this};


        public virtual IValidatableChangeTrackingCollection<SalesUnitWrapperTip> SalesUnits { get; }

        public void SetTceNumber(IUnitOfWork unitOfWork)
        {
            if (this.Model.PriceEngineeringTask != null)
            {
                TceNumber = Model.PriceEngineeringTask.GetPriceEngineeringTasks(unitOfWork).TceNumber;
                TceNumberPosition = Model.PriceEngineeringTask.TcePosition;
            }
            else if (this.Model.TechnicalRequrements != null)
            {
                TceNumber = unitOfWork.Repository<TechnicalRequrementsTask>().GetById(Model.TechnicalRequrements.TaskId).TceNumber;
                TceNumberPosition = this.Model.TechnicalRequrements.PositionInTeamCenter?.ToString();
            }
            else
            {
                return;
            }

            RaisePropertyChanged(nameof(TceNumber));
            RaisePropertyChanged(nameof(TceNumberPosition));
        }

        #region Info

        /// <summary>
        /// Заявка Team Center
        /// </summary>
        public string TceNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Номер позиции в заявке Team Center
        /// </summary>
        public string TceNumberPosition
        {
            get;
            private set;
        }

        [Designation("Заказ"), OrderStatus(-1)]
        public virtual string Order
        {
            get => _order;
            set
            {
            }
        }

        [Designation("Позиции"), OrderStatus(-2)]
        public virtual string OrderPositions
        {
            get => _orderPositions;
            set => _orderPositions = value;
        }

        [Designation("Владелец объекта"), OrderStatus(-3)]
        public string FacilityOwners { get; }

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


        [Designation("Цена по счёту"), OrderStatus(-21)]
        public double? CostInvoice => this.Model.PaymentCondition?.Part * this.Cost;

        [Designation("Стоимость по счёту"), OrderStatus(-23)]
        public double? SumInvoice => CostInvoice * Amount;

        [Designation("Стоимость с НДС по счёту"), OrderStatus(-24)]
        public double? SumWithVatInvoice => Vat * SumInvoice;

        #endregion

        public TaskInvoiceForPaymentItemWrapperBase(TaskInvoiceForPaymentItem model) : base(model)
        {
            var salesUnits = model.PriceEngineeringTask?.SalesUnits?? model.TechnicalRequrements.SalesUnits;
            var salesUnit = salesUnits.First();

            FixedCost = -1.0 * salesUnits.GetFixedCost();

            _order = salesUnits.Select(unit => unit.Order).Distinct().Where(order => order != null).Select(order => order.Number).ToStringEnum();
            _orderPositions = salesUnits.Select(unit => unit.OrderPosition).GetOrderPositions();

            ProductionTerm = salesUnit.ProductionTerm;
            var owners = new List<Company> { salesUnit.Facility.OwnerCompany };
            owners.AddRange(salesUnit.Facility.OwnerCompany.ParentCompanies().ToList());
            FacilityOwners = owners.ToStringEnum();
            var contragent = salesUnit.Specification?.Contract.Contragent;
            ContragentType = GetContragentType(contragent, salesUnits);
            Facility = salesUnit.Facility.ToString();
            var region = salesUnit.Facility.GetRegion();
            Country = region?.District.Country;
            District = region?.District.Name;
            Segment = SegmentConverter(GetSegment(salesUnits));
            ProductType = salesUnit.Product.ProductType.Name;
            Designation = salesUnit.Product.Designation;
            Amount = salesUnits.Count;
            Vat = salesUnit.Vat / 100.0 + 1.0;
            Cost = salesUnit.Cost;
            var costDelivery = salesUnits.Select(unit => unit.CostDelivery).Where(x => x.HasValue).Sum(x => x.Value);
            CostDelivery = -1.0 * costDelivery;

            OrderInTakeDate = salesUnit.OrderInTakeDate;

            PaymentConditionSet = salesUnit.PaymentConditionSet;


            DeliveryType = Math.Abs(CostDelivery) > 0 ? "Доставка" : "Самовывоз";

            DeliveryAddress = salesUnit.GetDeliveryAddressString();

        }

        #region GetInfo

        private string GetContragentType(Company contragent, IEnumerable<SalesUnit> salesUnits)
        {
            if (contragent == null)
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

        public override string ToString()
        {
            return $"{Facility} {this.Model.SalesUnits.First().Product} {this.Amount} шт.";
        }
    }
}