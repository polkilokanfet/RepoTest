namespace HVTApp.DataAccess
{
    public partial class IncomingRequestConfiguration
    {
        public IncomingRequestConfiguration()
        {
            HasRequired(x => x.Document).WithOptional().WillCascadeOnDelete(true);
            HasMany(x => x.Performers).WithMany();
        }
    }
}