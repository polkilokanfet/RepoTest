using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Организационная форма")]
    [DesignationPlural("Организационные формы")]
    public partial class CompanyForm : BaseEntity
    {
        [Designation("Полное наименование"), Required, MaxLength(100)]
        public string FullName { get; set; }

        [Designation("Сокращенное наименование"), Required, MaxLength(100)]
        public string ShortName { get; set; }

        public override string ToString()
        {
            return $"{ShortName} ({FullName})";
        }
    }
}
