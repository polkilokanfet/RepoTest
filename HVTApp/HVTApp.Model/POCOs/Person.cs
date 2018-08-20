using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Персона")]
    [DesignationPlural("Персоны")]
    public partial class Person : BaseEntity
    {
        [Designation("Фамилия")]
        public string Surname { get; set; }

        [Designation("Имя")]
        public string Name { get; set; }

        [Designation("Отчество")]
        public string Patronymic { get; set; }

        [Designation("Мужского пола")]
        public bool IsMan { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}";
        }
    }
}