using System.Collections.Generic;

namespace HVTApp.Model
{
    /// <summary>
    /// Страна.
    /// </summary>
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// Округа.
        /// </summary>
        public virtual List<District> Districts { get; set; }
    }
}