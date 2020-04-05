using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("����� ���������")]
    public class GlobalProperties : BaseEntity
    {
        [Designation("���� ��������"), Required, OrderStatus(20)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("���� ��������"), Required, OrderStatus(19)]
        public virtual Company OurCompany { get; set; }

        [Designation("���� ������������ �������������"), Required]
        public int ActualPriceTerm { get; set; } = 90;

        [Designation("����������� ���� ������������"), Required]
        public int StandartTermFromStartToEndProduction { get; set; } = 120;

        [Designation("����������� ���� ������"), Required]
        public int StandartTermFromPickToEndProduction { get; set; } = 7;

        [Designation("����������� ������� ������"), Required]
        public virtual PaymentConditionSet StandartPaymentsConditionSet { get; set; }

        [Designation("���"), Required]
        public double Vat { get; set; } = 20;

        [Designation("������������ �������� ����� ����������"), Required]
        public virtual Parameter NewProductParameter { get; set; }

        [Designation("������ ����� ����������"), Required]
        public virtual ParameterGroup NewProductParameterGroup { get; set; }


        [Designation("������ ���������� ������������ ����������"), Required]
        public virtual ParameterGroup VoltageGroup { get; set; }

        [Designation("������� ������"), Required]
        public virtual Parameter ServiceParameter { get; set; }

        [Designation("������� ���-�������"), Required]
        public virtual Parameter SupervisionParameter { get; set; }

        [Designation("����������� ���"), Required]
        public virtual Employee SenderOfferEmployee { get; set; }

        [Designation("������������� ���"), Required]
        public virtual ActivityField HvtProducersActivityField { get; set; }

        [Designation("����������� ������� ������"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }

        [Designation("���� � ����� � ���������")]
        public string IncomingRequestsPath { get; set; }

        [Designation("�����������")]
        public virtual User Developer { get; set; }

        [Designation("���� ���������� ������ ������������")]
        public virtual DateTime? LastDeveloperVizit { get; set; }

        [Designation("�������������� ������������")]
        public virtual Product ProductIncludedDefault { get; set; }

    }
}