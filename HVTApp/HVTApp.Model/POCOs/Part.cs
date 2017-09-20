using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Part : BaseEntity
    {
        public string Designation { get; set; }
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public virtual List<CostOnDate> Prices { get; set; } = new List<CostOnDate>(); //себестоимости по датам

        public string StructureCostNumber { get; set; }



        public string ParametersToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var parameter in Parameters)
                stringBuilder.Append($"{parameter.Group.Name}: {parameter.Value}; ");

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(Designation) ? Designation : ParametersToString();
        }
    }
}