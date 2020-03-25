using System.ComponentModel.DataAnnotations;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Персона")]
    [DesignationPlural("Персоны")]
    [AllowEdit(Role.DataBaseFiller, Role.SalesManager, Role.Economist)]
    public partial class Person : BaseEntity
    {
        [Designation("Фамилия"), Required, MaxLength(30), OrderStatus(10)]
        public string Surname { get; set; }

        [Designation("Имя"), Required, MaxLength(30), OrderStatus(9)]
        public string Name { get; set; }

        [Designation("Отчество"), MaxLength(30), OrderStatus(8)]
        public string Patronymic { get; set; }

        [Designation("Мужского пола")]
        public bool IsMan { get; set; } = true;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Surname} {Name}");
            if (!string.IsNullOrEmpty(Patronymic)) sb.Append($" {Patronymic}");
            return sb.ToString();
        }
    }
}