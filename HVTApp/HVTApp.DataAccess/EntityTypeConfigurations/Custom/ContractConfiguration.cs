namespace HVTApp.DataAccess
{
    public partial class ContractConfiguration 
    {
        public ContractConfiguration()
        {
            Property(x => x.Number).IsRequired().HasMaxLength(50);
            Property(x => x.Date).IsRequired();
            HasRequired(x => x.Contragent).WithMany();
        }
    }
}