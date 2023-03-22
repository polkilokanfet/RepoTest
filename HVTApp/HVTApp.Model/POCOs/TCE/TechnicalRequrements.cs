using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// "���.�������"
    /// </summary>
    [Designation("���.�������")]
    [DesignationPlural("���.�������")]
    public partial class TechnicalRequrements: BaseEntity
    {
        public Guid TaskId { get; set; }

        [Designation("�����"), Required, OrderStatus(20)]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("���"), OrderStatus(16)]
        public virtual DateTime? OrderInTakeDate { get; set; }

        [Designation("���� ����������"), OrderStatus(14)]
        public virtual DateTime? RealizationDate { get; set; }

        [Designation("�����"), OrderStatus(10)]
        public virtual List<TechnicalRequrementsFile> Files { get; set; } = new List<TechnicalRequrementsFile>();

        [Designation("�����������"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }

        [Designation("���������"), OrderStatus(2)]
        public bool IsActual { get; set; } = true;
    }
}