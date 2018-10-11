using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сфера деятельности компании.
    /// </summary>
    [Designation("Сфера деятельности")]
    public partial class ActivityField : BaseEntity
    {
        [Designation("Название"), Required, MaxLength(50), OrderStatus(10)]
        public string Name { get; set; }


        public ActivityFieldEnum ActivityFieldEnum { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
