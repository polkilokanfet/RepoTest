using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
    }

    public partial class ProductBlock : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();
        public string StructureCostNumber { get; set; }

        public string ParametersToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var parameter in Parameters.OrderBy(x => x))
                stringBuilder.Append($"{parameter.ParameterGroup}: {parameter.Value}; ");

            return stringBuilder.ToString();
        }


        public override string ToString()
        {
            return !string.IsNullOrEmpty(Name) ? Name : ParametersToString();
        }
    }
}