using HVTApp.Infrastructure.Attributes;

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
    }
}