using System.Collections.Generic;

namespace HVTApp.Model
{
    public class TechParametersGroup : BaseEntity
    {
        public string Name { get; set; }
        /// <summary>
        /// группа из которой может быть выбран только один параметр для одного оборудования.
        /// </summary>
        public bool IsOntyChoice { get; set; } = true;
        public virtual List<TechParameter> TechParameters { get; set; }
    }
}
