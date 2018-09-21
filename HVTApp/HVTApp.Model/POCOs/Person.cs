using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Персона")]
    [DesignationPlural("Персоны")]
    public partial class Person : BaseEntity
    {
        [Designation("Фамилия"), Required, MaxLength(30)]
        public string Surname { get; set; }

        [Designation("Имя"), Required, MaxLength(30)]
        public string Name { get; set; }

        [Designation("Отчество"), MaxLength(30)]
        public string Patronymic { get; set; }

        [Designation("Мужского пола")]
        public bool IsMan { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}";
        }
    }
}