using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTime EstimatedDate { get; set; } // ��������������� ���� ���������� �������.
        public virtual User Manager { get; set; }
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();
        public virtual List<Tender> Tenders { get; set; } = new List<Tender>();
        public virtual List<Offer> Offers { get; set; } = new List<Offer>();
    }
}