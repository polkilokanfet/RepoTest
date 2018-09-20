using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("����� ���������")]
    public class CommonOption : BaseEntity
    {
        [Designation("���� ��������"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("���� ��������"), Required]
        public virtual Company OurCompany { get; set; }

        [Designation("���� ������������ �������������"), Required]
        public int ActualPriceTerm { get; set; } = 90;

        [Designation("����������� ���� ������������"), Required]
        public int StandartTermFromStartToEndProduction { get; set; } = 120;

        [Designation("����������� ���� ������"), Required]
        public int StandartTermFromPickToEndProduction { get; set; } = 7;

        [Designation("����������� ������� ������"), Required]
        public virtual PaymentConditionSet StandartPaymentsConditionSet { get; set; }
    }
}