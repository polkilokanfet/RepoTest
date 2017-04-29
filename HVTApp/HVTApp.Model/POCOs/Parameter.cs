using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Parameter : BaseEntity
    {
        public virtual ParameterGroup Group { get; set; }
        public virtual Measure Measure { get; set; }
        public string Value { get; set; }
        public virtual List<RequiredParentParameters> RequiredParents { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }

    public class ParameterGroup : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Parameter> Parameters { get; set; }
        public virtual List<Measure> Measures { get; set; }
        public bool IsOnlyChoice { get; set; } = true; // группа из которой может быть выбран только один параметр для одного оборудования.

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public class Measure : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public ParameterGroup Group { get; set; }
    }

    public class RequiredParentParameters : BaseEntity
    {
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>(); //обязательные родительские параметры, без которых этот параметр не имеет смысла
    }

}