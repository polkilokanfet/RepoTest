using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class CalculatePriceTask : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual ProductBlock ProductBlock { get; set; }
        public bool IsActual { get; set; } = true;
        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Offer> Offers { get; set; } = new List<Offer>();
        public List<Specification> Specifications { get; set; } = new List<Specification>();
    }
}