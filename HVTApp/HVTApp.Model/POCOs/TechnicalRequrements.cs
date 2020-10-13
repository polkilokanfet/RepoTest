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
        [Designation("�����"), Required, OrderStatus(20)]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("�����"), Required, OrderStatus(10)]
        public virtual List<TechnicalRequrementsFile> Files { get; set; } = new List<TechnicalRequrementsFile>();

        [Designation("�����������"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }
    }
}