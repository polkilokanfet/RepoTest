using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.UI.Lookup
{
    public partial class ProductBlockLookup
    {
        private string _designDepartments;

        [Designation("��")]
        public string DesignDepartments
        {
            get => _designDepartments;
            set
            {
                _designDepartments = value;
                RaisePropertyChanged();
            }
        }

        [Designation("�����")]
        public string Prices => this.Entity.Prices.ToStringEnum();

        [Designation("����� (������������)")]
        public string PricesOrdered => this.Entity.Prices.OrderBy(sumOnDate => sumOnDate).ToStringEnum();
    }
}