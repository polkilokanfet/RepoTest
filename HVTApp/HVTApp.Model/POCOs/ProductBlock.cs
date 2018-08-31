using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("����")]
    public partial class ProductBlock : BaseEntity
    {
        [Designation("��������")]
        public string Name { get; set; }

        [Designation("���������")]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("�������������")]
        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>();

        [Designation("�����������")]
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