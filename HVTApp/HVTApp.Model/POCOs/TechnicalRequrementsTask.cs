using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// ���.������� (������)
    /// </summary>
    [Designation("���.������� (������)")]
    [DesignationPlural("���.������� (������)")]
    public partial class TechnicalRequrementsTask : BaseEntity
    {
        [Designation("������ ����������"), Required, OrderStatus(20)]
        public virtual List<TechnicalRequrements> Requrements { get; set; } = new List<TechnicalRequrements>();

        [Designation("�����������"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }

        [Designation("����� � ���"), MaxLength(10), OrderStatus(4)]
        public string TceNumber { get; set; }

        [Designation("Back manager"), OrderStatus(1)]
        public virtual User BackManager { get; set; }

        [Designation("�����"), OrderStatus(3)]
        public virtual DateTime? Start { get; set; }

        [Designation("������� �������������"), OrderStatus(-10)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();
    }
}