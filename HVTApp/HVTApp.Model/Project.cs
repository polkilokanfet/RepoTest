using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTime EstimatedDate { get; set; } // Ориентировочная дата реализации проекта.
        public virtual User Manager { get; set; }
        public virtual List<SalesUnit> SalesProductUnits { get; set; } 
        public virtual List<Tender> Tenders { get; set; } 
        public virtual List<Offer> Offers { get; set; }
    }
}