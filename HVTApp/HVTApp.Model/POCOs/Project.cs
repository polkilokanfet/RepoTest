using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTime EstimatedDate { get; set; } // Ориентировочная дата реализации проекта.
        public virtual User Manager { get; set; }
        public virtual List<ProjectUnit> ProjectUnits { get; set; } = new List<ProjectUnit>();
        public virtual List<Tender> Tenders { get; set; } = new List<Tender>();
        public virtual List<Offer> Offers { get; set; } = new List<Offer>();
    }
}