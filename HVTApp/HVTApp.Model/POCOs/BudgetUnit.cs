using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������� �������")]
    public class BudgetUnit : BaseEntity
    {
        public BudgetUnit()
        {
        }

        public BudgetUnit(SalesUnit salesUnit)
        {
            SalesUnit = salesUnit;
            OrderInTakeDate = OrderInTakeDateByManager = salesUnit.OrderInTakeDate;
            RealizationDate = RealizationDateByManager = salesUnit.RealizationDateCalculated;
            Cost = CostByManager = salesUnit.Cost;
            PaymentConditionSet = PaymentConditionSetByManager = salesUnit.PaymentConditionSet;
        }

        [Designation("������"), Required, OrderStatus(110)]
        public virtual Budget Budget { get; set; }

        [Designation("������� ������"), Required, OrderStatus(100)]
        public virtual SalesUnit SalesUnit { get; set; }

        [Designation("���� ���"), Required, OrderStatus(90)]
        public DateTime OrderInTakeDate { get; set; }

        [Designation("���� ����������"), Required, OrderStatus(80)]
        public DateTime RealizationDate { get; set; }


        [Designation("���� ��� (��������)"), Required, OrderStatus(85)]
        public DateTime OrderInTakeDateByManager { get; set; }

        [Designation("���� ���������� (��������)"), Required, OrderStatus(75)]
        public DateTime RealizationDateByManager { get; set; }


        [Designation("���������"), Required, OrderStatus(60)]
        public double Cost { get; set; }

        [Designation("��������� (��������)"), Required, OrderStatus(55)]
        public double CostByManager { get; set; }


        [Designation("������� ������"), Required, OrderStatus(50)]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("������� ������ (��������)"), Required, OrderStatus(45)]
        public virtual PaymentConditionSet PaymentConditionSetByManager { get; set; }

        [Designation("������"), OrderStatus(40)]
        public bool IsRemoved { get; set; } = false;
    }
}