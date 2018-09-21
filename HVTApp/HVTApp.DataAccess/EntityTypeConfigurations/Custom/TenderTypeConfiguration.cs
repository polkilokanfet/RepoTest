namespace HVTApp.DataAccess
{
    public partial class TenderTypeConfiguration 
    {
        public TenderTypeConfiguration()
        {
            Property(x => x.Type).IsRequired();
        }
    }
}