using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("������")]
    [DesignationPlural("�������")]
    public class Project : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        [Designation("��������")]
        public virtual User Manager { get; set; }

        [Designation("��������� �������")]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("���")]
        public virtual List<Offer> Offers { get; set; } = new List<Offer>();

        [Designation("�������")]
        public virtual List<Tender> Tenders { get; set; } = new List<Tender>();

        [Designation("�������")]
        public virtual List<Note> Notes { get; set; } = new List<Note>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }

}