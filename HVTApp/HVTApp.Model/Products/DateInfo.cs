using System;
using System.Linq;
using HVTApp.Model.Services;

namespace HVTApp.Model
{
    public class DateInfo : BaseEntity
    {
        public virtual ProductBase Product { get; set; }
        #region Dates

        /// <summary>
        /// Желаемая дата поставки.
        /// </summary>
        public virtual DateTime? DateDesiredDelivery { get; set; }


        #region CalculatedDates
        /// <summary>
        /// Плановая/фактическая дата ОИТ.
        /// </summary>
        public DateTime DateOrderInTakeCalculated
        {
            get
            {
                //если оборудование включено в стоимость другого
                //if (!InCoast)
                //{
                //    if (ProductsMainGroup.ParentProductMain != null)
                //        return ProductsMainGroup.ParentProductMain.DateOrderInTakeCalculated;
                //    if (ProductsMainGroup.ParentSalesGroup != null)
                //        return ProductsMainGroup.ParentSalesGroup.OrderInTakeDate;
                //}

                //по фактической дате размещения в производстве.
                if (DateProductionPlacing != null)
                    return DateProductionPlacing.Value;

                //по фактической контрактной дате открытия производства 
                //(по исполнению контрактных обязательств для начала производства).
                if (DateExecutionConditionsToStartProductionCalculatedByActual != null)
                    return DateServices.GetTodayIfDateToEarly(DateExecutionConditionsToStartProductionCalculatedByActual.Value);

                //по плановой контрактной дате открытия производства 
                //(по исполнению контрактных обязательств для начала производства).
                if (Product.PaymentsInfo.PaymentsPlanned.AllPaymentsToStartProductionWithCustomDate &&
                    DateExecutionConditionsToStartProductionCalculatedByPlan != null)
                    return DateServices.GetTodayIfDateToEarly(DateExecutionConditionsToStartProductionCalculatedByPlan.Value);

                //по дате спецификации.
                if (Product.ProductsMainGroup.Specification != null)
                    return Product.ProductsMainGroup.Specification.Date;

                //по желаемой дате поставки.
                if (DateDesiredDelivery != null)
                    return DateServices.GetTodayIfDateToEarlyAndSkipWeekend(DateDesiredDelivery.Value.AddDays(-Product.TermsInfo.TermProductionPlan));

                //по ориентировочной дате реализации проекта
                return DateServices.GetTodayIfDateToEarlyAndSkipWeekend(Product.ProductsMainGroup.Project.EstimatedDate.AddDays(-Product.TermsInfo.TermProductionPlan));
            }
        }

        /// <summary>
        /// Дата исполнения контрагентом условий контракта для начала производства (по актуальным платежам).
        /// </summary>
        public DateTime? DateExecutionConditionsToStartProductionCalculatedByActual
        {
            get
            {
                //если контракт не предусматривает авансы до начала производства
                if (Product.PaymentsInfo.PaymentsSumToStartProduction.Equals(0))
                {
                    if (Product.ProductsMainGroup.Specification != null)
                        return Product.ProductsMainGroup.Specification.Date;
                }

                //если контракт предусматривает авансы
                var payments = Product.PaymentsInfo.PaymentsActual.OrderBy(x => x.Date);
                double totalSum = 0;
                foreach (PaymentActual payment in payments)
                {
                    totalSum += payment.Sum;
                    if (totalSum >= Product.PaymentsInfo.PaymentsSumToStartProduction) return payment.Date;
                }

                return null;
            }
        }

        /// <summary>
        /// Плановая дата исполнения контрагентом условий контракта для начала производства (по плановым платежам). 
        /// Совпадает с фактической датой при наличии последней.
        /// </summary>
        public DateTime? DateExecutionConditionsToStartProductionCalculatedByPlan
        {
            get
            {
                if (DateExecutionConditionsToStartProductionCalculatedByActual != null)
                    return DateExecutionConditionsToStartProductionCalculatedByActual;

                var payments = Product.PaymentsInfo.PaymentsPlanned.OrderBy(x => x.Date);
                double totalSum = Product.PaymentsInfo.PaymentsActual.TotalSum;
                foreach (var payment in payments)
                {
                    totalSum += payment.Sum;
                    if (totalSum >= Product.PaymentsInfo.PaymentsSumToStartProduction)
                        return payment.Date;
                }

                return null;
            }
        }

        /// <summary>
        /// Дата исполнения контрагентом условий контракта для отгрузки (по актуальным платежам).
        /// Совпадает с фактической датой при наличии последней.
        /// </summary>
        public DateTime? DateExecutionConditionsToShipmentCalculatedByActual
        {
            get
            {
                //если контракт не предусматривает платежи до отгрузки
                if (Product.PaymentsInfo.PaymentsSumToShipping == 0)
                {
                    if (Product.ProductsMainGroup.Specification != null)
                        return Product.ProductsMainGroup.Specification.Date;
                }

                //если контракт предусматривает платежи до отгрузки
                var payments = Product.PaymentsInfo.PaymentsActual.OrderBy(x => x.Date);
                double totalSum = 0;
                foreach (PaymentActual payment in payments)
                {
                    totalSum += payment.Sum;
                    if (totalSum >= Product.PaymentsInfo.PaymentsSumToShipping)
                        return payment.Date;
                }
                return null;
            }
        }

        /// <summary>
        /// Плановая дата исполнения контрагентом условий контракта для отгрузки (по плановым платежам). 
        /// Совпадает с фактической датой при наличии последней.
        /// </summary>
        public DateTime? DateExecutionConditionsToShipmentCalculatedByPlan
        {
            get
            {
                if (DateExecutionConditionsToShipmentCalculatedByActual != null)
                    return DateExecutionConditionsToShipmentCalculatedByActual;

                var payments = Product.PaymentsInfo.PaymentsPlanned.OrderBy(x => x.Date);
                double totalSum = Product.PaymentsInfo.PaymentsActual.TotalSum;
                foreach (var payment in payments)
                {
                    totalSum += payment.Sum;
                    if (totalSum >= Product.PaymentsInfo.PaymentsSumToShipping)
                        return payment.Date;
                }

                return null;
            }
        }

        /// <summary>
        /// Плановая дата производства. Совпадает с фактической при наличии последней.
        /// </summary>
        public DateTime DateEndProductionCalculated
        {
            get
            {
                //если есть фактическая дата производства.
                if (DateEndProduction != null)
                    return DateEndProduction.Value;

                //если есть дата комплектации.
                if (DateComplete != null)
                    return DateComplete.Value.AddDays(Product.TermsInfo.TermFromCompleteToProductionPlan);

                //если желаемая дата поставки еще не истекла
                if (DateDesiredDelivery != null && DateDesiredDelivery >= DateTime.Today)
                    return DateDesiredDelivery.Value;

                //расчетная дата производства
                return DateServices.GetTodayIfDateToEarly(DateOrderInTakeCalculated.AddDays(Product.TermsInfo.TermProductionPlan));
            }
        }

        /// <summary>
        /// Плановая дата реализации. Совпадает с фактической при наличии последней.
        /// </summary>
        public virtual DateTime DateRealizationCalculated
        {
            get
            {
                //если есть фактическая дата реализации.
                if (DateRealization != null) return DateRealization.Value;

                //если есть фактическая дата отгрузки.
                if (DateShipment != null) return DateShipment.Value;

                //если установлена плановая дата реализации.
                if (DateRealizationPlan != null)
                    return DateServices.GetTodayIfDateToEarly(DateRealizationPlan.Value);

                //расчетная дата реализации.
                return DateEndProductionCalculated;
            }
        }

        /// <summary>
        /// Плановая дата отгрузки. Совпадает с фактической при наличии последней.
        /// </summary>
        public DateTime DateShipmentCalculated
        {
            get
            {
                //если есть фактическая дата отгрузки.
                if (DateShipment != null) return DateShipment.Value;

                //если есть плановая дата отгрузки.
                //и она позже даты исполнения обязательств контрагента
                //и она позже плановой даты производства
                if (DateShipmentPlan != null && DateExecutionConditionsToShipmentCalculatedByPlan != null &&
                    DateShipmentPlan.Value >= DateExecutionConditionsToShipmentCalculatedByPlan.Value &&
                    DateShipmentPlan.Value >= DateEndProductionCalculated)
                    return DateShipmentPlan.Value;

                //если есть дата исполнения обязательств для отгрузки и она позже даты производства
                if (DateExecutionConditionsToShipmentCalculatedByPlan != null &&
                    DateExecutionConditionsToShipmentCalculatedByPlan.Value >= DateEndProductionCalculated)
                    return DateExecutionConditionsToShipmentCalculatedByPlan.Value;

                //расчетная дата отгрузки = дата производства
                return DateEndProductionCalculated;
            }
        }

        /// <summary>
        /// Плановая дата поставки. Совпадает с фактической при наличии последней.
        /// </summary>
        public DateTime DateDeliveryCalculated
        {
            get
            {
                //если есть фактическая дата доставки.
                if (DateDelivery != null) return DateDelivery.Value;

                //расчетная дата доставки
                return DateShipmentCalculated.AddDays(Product.TermsInfo.TermFromShipmentToDeliveryPlan);
            }
        }

        #endregion

        #region PlanDates
        /// <summary>
        /// Плановая дата реализации.
        /// </summary>
        public DateTime? DateRealizationPlan { get; set; }

        /// <summary>
        /// Плановая дата отгрузки.
        /// </summary>
        public DateTime? DateShipmentPlan { get; set; }

        #endregion

        #region FactDates
        /// <summary>
        /// Дата размещения в производстве.
        /// </summary>
        public virtual DateTime? DateProductionPlacing { get; set; }

        /// <summary>
        /// Дата комплектации.
        /// </summary>
        public virtual DateTime? DateComplete { get; set; }

        /// <summary>
        /// Дата производства.
        /// </summary>
        public virtual DateTime? DateEndProduction { get; set; }

        /// <summary>
        /// Дата реализации.
        /// </summary>
        public virtual DateTime? DateRealization { get; set; }

        /// <summary>
        /// Дата отгрузки.
        /// </summary>
        public virtual DateTime? DateShipment { get; set; }

        /// <summary>
        /// Дата поставки.
        /// </summary>
        public virtual DateTime? DateDelivery { get; set; }
        #endregion

        #endregion

    }
}