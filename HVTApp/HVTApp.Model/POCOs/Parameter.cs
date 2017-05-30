using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Parameter : BaseEntity
    {
        public virtual ParameterGroup Group { get; set; }
        public string Value { get; set; }
        public virtual List<RequiredParameters> RequiredParents { get; set; } = new List<RequiredParameters>();

        public override string ToString()
        {
            string result = Value;
            if (Group.Measure != null)
                result = result + " " + Group.Measure.ShortName;
            return result;
        }
    }

    public class ParameterGroup : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Parameter> Parameters { get; set; }
        public virtual Measure Measure { get; set; }
        public bool IsOnlyChoice { get; set; } = true; // группа из которой может быть выбран только один параметр для одного оборудования.

        public override string ToString()
        {
            return Name;
        }
    }

    public class PhysicalQuantity : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Measure> Measures { get; set; }
    }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public class Measure : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public PhysicalQuantity PhysicalQuantity { get; set; }
    }

    public class RequiredParameters : BaseEntity
    {
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>(); //обязательные родительские параметры, без которых этот параметр не имеет смысла
    }

}