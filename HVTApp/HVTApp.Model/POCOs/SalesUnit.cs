using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ������")]
    [DesignationPlural("������� ������")]
    public class SalesUnit : BaseEntity, IUnit
    {
        [Designation("���������")]
        public double Cost { get; set; }


        [Designation("�������")]
        public virtual Product Product { get; set; }

        [Designation("��������� ��������")]
        public virtual List<ProductDependent> DependentProducts { get; set; } = new List<ProductDependent>();

        public virtual List<Service> Services { get; set; } = new List<Service>();

        [Designation("������")]
        public virtual Facility Facility { get; set; }

        [Designation("������� ������")]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("���� ������������")]
        public int? ProductionTerm { get; set; }


        #region ������
        [Designation("������")]
        public virtual Project Project { get; set; }

        [Designation("��������� ���� ��������")]
        public virtual DateTime DeliveryDateExpected { get; set; } = DateTime.Today.AddDays(CommonOptions.ProductionTerm + 30).SkipWeekend(); //��������� ���� ��������

        [Designation("�������������")]
        public virtual Company Producer { get; set; }

        [Designation("���� ����������")]
        public virtual DateTime? RealizationDate { get; set; }

        #endregion

        #region ���������� � ������������
        [Designation("�����")]
        public virtual Order Order { get; set; }

        [Designation("�������")]
        public string OrderPosition { get; set; }

        [Designation("�����")]
        public string SerialNumber { get; set; }

        [Designation("���� ������")]
        public int? AssembleTerm { get; set; }

        [Designation("���� ������ ������������")]
        public DateTime? StartProductionDate { get; set; }

        [Designation("���� ������������")]
        public DateTime? PickingDate { get; set; }

        [Designation("���� ��������� ������������")]
        public DateTime? EndProductionDate { get; set; }

        #endregion

        #region ������������ ����������

        [Designation("������������")]
        public virtual Specification Specification { get; set; }

        [Designation("����������� �������"), NotUpdate(Role.SalesManager | Role.Director)]
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

        [Designation("����������� �������")]
        public virtual List<PaymentPlannedList> PaymentsPlannedSaved { get; set; } = new List<PaymentPlannedList>();

        #endregion

        #region ����������� ����������
        [Designation("���� ��������")]
        public int? ExpectedDeliveryPeriod { get; set; }

        [Designation("����� ��������")]
        public virtual Address Address { get; set; }

        [Designation("��������� ��������")]
        public double CostOfShipment { get; set; } = 0;

        [Designation("���� ��������")]
        public virtual DateTime? ShipmentDate { get; set; }

        [Designation("���� ��������")]
        public virtual DateTime? ShipmentPlanDate { get; set; }

        [Designation("���� ��������")]
        public virtual DateTime? DeliveryDate { get; set; }

        #endregion

        public override string ToString()
        {
            return $"{Product} ��� {Facility}";
        }

    }
}