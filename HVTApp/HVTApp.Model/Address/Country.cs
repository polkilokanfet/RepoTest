using System.Collections.Generic;

namespace HVTApp.Model
{
    /// <summary>
    /// ������.
    /// </summary>
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// ������.
        /// </summary>
        public virtual List<District> Districts { get; set; }
    }
}