using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тип объекта")]
    public partial class FacilityType : BaseEntity
    {
        [Designation("Наименование")]
        public string FullName { get; set; }

        [Designation("Сокращенное наименование")]
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{FullName}, {ShortName}";
        }
    }
}