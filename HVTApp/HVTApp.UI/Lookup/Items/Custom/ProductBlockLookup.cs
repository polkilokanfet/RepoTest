using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.UI.Lookup
{
    public partial class ProductBlockLookup
    {
        private string _designDepartments;

        [Designation("КБ")]
        public string DesignDepartments
        {
            get => _designDepartments;
            set
            {
                _designDepartments = value;
                RaisePropertyChanged();
            }
        }

        [Designation("Параметры")]
        public string ParametersString =>
            this.Entity.Parameters
                .Select(x => $"[({x.ParameterGroup}) : ({x.Value})]")
                .OrderBy(x => x)
                .ToStringEnum();
    }
}