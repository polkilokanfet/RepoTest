using System.Collections.Generic;

namespace HVTApp.Model
{
    public class TechLink : BaseEntity
    {
        public virtual TechParameter Parameter { get; set; }
        public virtual TechLink ParentLink { get; set; }
        public virtual List<TechLink> ChildLinks { get; set; }
    }
}
