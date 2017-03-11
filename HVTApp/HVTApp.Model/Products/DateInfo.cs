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
        /// �������� ���� ��������.
        /// </summary>
        public virtual DateTime? DateDesiredDelivery { get; set; }


        #region CalculatedDates
        /// <summary>
        /// ��������/����������� ���� ���.
        /// </summary>
        public DateTime DateOrderInTakeCalculated
        {
            get
            {
                //���� ������������ �������� � ��������� �������
                //if (!InCoast)
                //{
                //    if (ProductsMainGroup.ParentProductMain != null)
                //        return ProductsMainGroup.ParentProductMain.DateOrderInTakeCalculated;
                //    if (ProductsMainGroup.ParentSalesGroup != null)
                //        return ProductsMainGroup.ParentSalesGroup.OrderInTakeDate;
                //}

                //�� ����������� ���� ���������� � ������������.
                if (DateProductionPlacing != null)
                    return DateProductionPlacing.Value;

                //�� ����������� ����������� ���� �������� ������������ 
                //(�� ���������� ����������� ������������ ��� ������ ������������).
                if (DateExecutionConditionsToStartProductionCalculatedByActual != null)
                    return DateServices.GetTodayIfDateToEarly(DateExecutionConditionsToStartProductionCalculatedByActual.Value);

                //�� �������� ����������� ���� �������� ������������ 
                //(�� ���������� ����������� ������������ ��� ������ ������������).
                if (Product.PaymentsInfo.PaymentsPlanned.AllPaymentsToStartProductionWithCustomDate &&
                    DateExecutionConditionsToStartProductionCalculatedByPlan != null)
                    return DateServices.GetTodayIfDateToEarly(DateExecutionConditionsToStartProductionCalculatedByPlan.Value);

                //�� ���� ������������.
                if (Product.ProductsMainGroup.Specification != null)
                    return Product.ProductsMainGroup.Specification.Date;

                //�� �������� ���� ��������.
                if (DateDesiredDelivery != null)
                    return DateServices.GetTodayIfDateToEarlyAndSkipWeekend(DateDesiredDelivery.Value.AddDays(-Product.TermsInfo.TermProductionPlan));

                //�� ��������������� ���� ���������� �������
                return DateServices.GetTodayIfDateToEarlyAndSkipWeekend(Product.ProductsMainGroup.Project.EstimatedDate.AddDays(-Product.TermsInfo.TermProductionPlan));
            }
        }

        /// <summary>
        /// ���� ���������� ������������ ������� ��������� ��� ������ ������������ (�� ���������� ��������).
        /// </summary>
        public DateTime? DateExecutionConditionsToStartProductionCalculatedByActual
        {
            get
            {
                //���� �������� �� ��������������� ������ �� ������ ������������
                if (Product.PaymentsInfo.PaymentsSumToStartProduction.Equals(0))
                {
                    if (Product.ProductsMainGroup.Specification != null)
                        return Product.ProductsMainGroup.Specification.Date;
                }

                //���� �������� ��������������� ������
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
        /// �������� ���� ���������� ������������ ������� ��������� ��� ������ ������������ (�� �������� ��������). 
        /// ��������� � ����������� ����� ��� ������� ���������.
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
        /// ���� ���������� ������������ ������� ��������� ��� �������� (�� ���������� ��������).
        /// ��������� � ����������� ����� ��� ������� ���������.
        /// </summary>
        public DateTime? DateExecutionConditionsToShipmentCalculatedByActual
        {
            get
            {
                //���� �������� �� ��������������� ������� �� ��������
                if (Product.PaymentsInfo.PaymentsSumToShipping == 0)
                {
                    if (Product.ProductsMainGroup.Specification != null)
                        return Product.ProductsMainGroup.Specification.Date;
                }

                //���� �������� ��������������� ������� �� ��������
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
        /// �������� ���� ���������� ������������ ������� ��������� ��� �������� (�� �������� ��������). 
        /// ��������� � ����������� ����� ��� ������� ���������.
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
        /// �������� ���� ������������. ��������� � ����������� ��� ������� ���������.
        /// </summary>
        public DateTime DateEndProductionCalculated
        {
            get
            {
                //���� ���� ����������� ���� ������������.
                if (DateEndProduction != null)
                    return DateEndProduction.Value;

                //���� ���� ���� ������������.
                if (DateComplete != null)
                    return DateComplete.Value.AddDays(Product.TermsInfo.TermFromCompleteToProductionPlan);

                //���� �������� ���� �������� ��� �� �������
                if (DateDesiredDelivery != null && DateDesiredDelivery >= DateTime.Today)
                    return DateDesiredDelivery.Value;

                //��������� ���� ������������
                return DateServices.GetTodayIfDateToEarly(DateOrderInTakeCalculated.AddDays(Product.TermsInfo.TermProductionPlan));
            }
        }

        /// <summary>
        /// �������� ���� ����������. ��������� � ����������� ��� ������� ���������.
        /// </summary>
        public virtual DateTime DateRealizationCalculated
        {
            get
            {
                //���� ���� ����������� ���� ����������.
                if (DateRealization != null) return DateRealization.Value;

                //���� ���� ����������� ���� ��������.
                if (DateShipment != null) return DateShipment.Value;

                //���� ����������� �������� ���� ����������.
                if (DateRealizationPlan != null)
                    return DateServices.GetTodayIfDateToEarly(DateRealizationPlan.Value);

                //��������� ���� ����������.
                return DateEndProductionCalculated;
            }
        }

        /// <summary>
        /// �������� ���� ��������. ��������� � ����������� ��� ������� ���������.
        /// </summary>
        public DateTime DateShipmentCalculated
        {
            get
            {
                //���� ���� ����������� ���� ��������.
                if (DateShipment != null) return DateShipment.Value;

                //���� ���� �������� ���� ��������.
                //� ��� ����� ���� ���������� ������������ �����������
                //� ��� ����� �������� ���� ������������
                if (DateShipmentPlan != null && DateExecutionConditionsToShipmentCalculatedByPlan != null &&
                    DateShipmentPlan.Value >= DateExecutionConditionsToShipmentCalculatedByPlan.Value &&
                    DateShipmentPlan.Value >= DateEndProductionCalculated)
                    return DateShipmentPlan.Value;

                //���� ���� ���� ���������� ������������ ��� �������� � ��� ����� ���� ������������
                if (DateExecutionConditionsToShipmentCalculatedByPlan != null &&
                    DateExecutionConditionsToShipmentCalculatedByPlan.Value >= DateEndProductionCalculated)
                    return DateExecutionConditionsToShipmentCalculatedByPlan.Value;

                //��������� ���� �������� = ���� ������������
                return DateEndProductionCalculated;
            }
        }

        /// <summary>
        /// �������� ���� ��������. ��������� � ����������� ��� ������� ���������.
        /// </summary>
        public DateTime DateDeliveryCalculated
        {
            get
            {
                //���� ���� ����������� ���� ��������.
                if (DateDelivery != null) return DateDelivery.Value;

                //��������� ���� ��������
                return DateShipmentCalculated.AddDays(Product.TermsInfo.TermFromShipmentToDeliveryPlan);
            }
        }

        #endregion

        #region PlanDates
        /// <summary>
        /// �������� ���� ����������.
        /// </summary>
        public DateTime? DateRealizationPlan { get; set; }

        /// <summary>
        /// �������� ���� ��������.
        /// </summary>
        public DateTime? DateShipmentPlan { get; set; }

        #endregion

        #region FactDates
        /// <summary>
        /// ���� ���������� � ������������.
        /// </summary>
        public virtual DateTime? DateProductionPlacing { get; set; }

        /// <summary>
        /// ���� ������������.
        /// </summary>
        public virtual DateTime? DateComplete { get; set; }

        /// <summary>
        /// ���� ������������.
        /// </summary>
        public virtual DateTime? DateEndProduction { get; set; }

        /// <summary>
        /// ���� ����������.
        /// </summary>
        public virtual DateTime? DateRealization { get; set; }

        /// <summary>
        /// ���� ��������.
        /// </summary>
        public virtual DateTime? DateShipment { get; set; }

        /// <summary>
        /// ���� ��������.
        /// </summary>
        public virtual DateTime? DateDelivery { get; set; }
        #endregion

        #endregion

    }
}