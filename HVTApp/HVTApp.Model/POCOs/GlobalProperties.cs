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


        [Designation("������� ������"), Required, OrderStatus(301)]
        public virtual Parameter ServiceParameter { get; set; }

        [Designation("������� ���-�������"), Required, OrderStatus(302)]
        public virtual Parameter SupervisionParameter { get; set; }

        [Designation("������ ���������� ������������ ����������"), Required, OrderStatus(303)]
        public virtual ParameterGroup VoltageGroup { get; set; }

        [Designation("������ ���������� ��������� ��������"), OrderStatus(304)]
        public virtual ParameterGroup IsolationMaterialGroup { get; set; }

        [Designation("������ ���������� ����� ��������"), OrderStatus(305)]
        public virtual ParameterGroup IsolationColorGroup { get; set; }

        [Designation("������ ���������� ��� ���������"), OrderStatus(306)]
        public virtual ParameterGroup IsolationDpuGroup { get; set; }


        [Designation("������ ���������� ����������� ��������� ��� ������"), Required, OrderStatus(-50)]
        public virtual ParameterGroup ComplectDesignationGroup{ get; set; }

        [Designation("�������� ��������� � ������"), Required, OrderStatus(-50)]
        public virtual Parameter ComplectsParameter { get; set; }

        [Designation("������ ���� ��������� ��� ������"), Required, OrderStatus(-50)]
        public virtual ParameterGroup ComplectsGroup { get; set; }


        [Designation("��� ������� (�� ���������)"), Required]
        public virtual ProjectType DefaultProjectType { get; set; }

        [Designation("���������� ����� �� ��"), Required]
        public virtual Employee RecipientSupervisionLetterEmployee { get; set; }

        [Designation("����������� ���"), Required]
        public virtual Employee SenderOfferEmployee { get; set; }

        [Designation("������������� ���"), Required]
        public virtual ActivityField HvtProducersActivityField { get; set; }

        [Designation("����������� ������� ������"), Required]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }


        [Designation("���� � ����� � ���������"), Required, OrderStatus(500), MaxLength(500)]
        public string IncomingRequestsPath { get; set; }

        [Designation("���� � ����� � ������������ Directum"), Required, OrderStatus(501), MaxLength(500)]
        public string DirectumAttachmentsPath { get; set; }

        [Designation("���� � ����� � ������� ��"), Required, OrderStatus(502), MaxLength(500)]
        public string TechnicalRequrementsFilesPath { get; set; }

        [Designation("���� � ����� � ������� ������� �� ���"), Required, OrderStatus(503), MaxLength(500)]
        public string TechnicalRequrementsFilesAnswersPath { get; set; }

        [Designation("���� � ����� � ��������� ������������ ������"), Required, OrderStatus(503), MaxLength(500)]
        public string ShippingCostFilesPath { get; set; }

        [Designation("���� � ����� � ��������� �������������"), Required, OrderStatus(504), MaxLength(500)]
        public string PriceCalculationsFilesPath { get; set; }

        [Designation("���� � ����� � ������"), OrderStatus(505), MaxLength(500)]
        public string LogsPath { get; set; }



        [Designation("�����������")]
        public virtual User Developer { get; set; }

        [Designation("���� ���������� ������ ������������")]
        public virtual DateTime? LastDeveloperVizit { get; set; }

        [Designation("�������������� ������������")]
        public virtual Product ProductIncludedDefault { get; set; }

        /// <summary>
        /// �������� ������� ����� ��
        /// ������������ ��� �������-����������� ����������
        /// </summary>
        [Designation("�������� ������� ����� ��"), OrderStatus(-100)]
        public virtual Parameter EmptyParameterCurrentTransformersSet { get; set; }

        [Designation("�������� ����� �� �� ������"), OrderStatus(-200)]
        public virtual Parameter ParameterCurrentTransformersSetCustom { get; set; }

        //[Designation("���� � ����� � ������ ������������ �������")]
        //public string PickingDatesFilePath { get; set; }
    }
}