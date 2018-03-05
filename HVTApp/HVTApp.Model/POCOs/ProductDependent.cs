using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProductDependent : BaseEntity
    {
        public virtual Product Product { get; set; }
        public int Amount { get; set; } = 1;
    }
}