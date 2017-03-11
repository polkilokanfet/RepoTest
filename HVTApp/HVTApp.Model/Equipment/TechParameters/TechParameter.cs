using System.Collections;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class TechParameter : BaseEntity
    {
        public string Name { get; set; }
        public virtual TechParametersGroup Group { get; set; }
    }
}
