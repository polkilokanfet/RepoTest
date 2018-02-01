using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Страна
    /// </summary>
    public partial class Country : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<District> Districts { get; set; } // Округа.
        public override string ToString()
        {
            return Name;
        }
    }
}