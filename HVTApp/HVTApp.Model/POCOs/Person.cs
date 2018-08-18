using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Person : BaseEntity
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public bool IsMan { get; set; }

        public override string ToString()
        {
            return $"{Surname} {Name} {Patronymic}";
        }
    }
}