using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Адрес")]
    public partial class Address : BaseEntity
    {
        [Designation("Описание")]
        public string Description { get; set; }

        public virtual Locality Locality { get; set; }

        public override string ToString()
        {
            return $"{Locality}, {Description}";
        }

    }
}