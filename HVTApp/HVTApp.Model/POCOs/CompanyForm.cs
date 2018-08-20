using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Организационная форма")]
    [DesignationPlural("Организационные формы")]
    public partial class CompanyForm : BaseEntity
    {
        [Designation("Полное наименование")]
        public string FullName { get; set; }

        [Designation("Сокращенное наименование")]
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{ShortName} ({FullName})";
        }
    }
}
