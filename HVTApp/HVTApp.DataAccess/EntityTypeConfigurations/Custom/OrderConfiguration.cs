namespace HVTApp.DataAccess
{
    public partial class OrderConfiguration
    {
        public OrderConfiguration()
        {
            Property(x => x.Number).IsRequired().HasMaxLength(10);
            Property(x => x.OpenOrderDate).IsRequired();
        }
    }
}