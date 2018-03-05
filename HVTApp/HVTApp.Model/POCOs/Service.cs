using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public int Amount { get; set; } = 1;
    }
}