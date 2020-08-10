namespace HVTApp.DataAccess
{
    public partial class SupervisionConfiguration
    {
        public SupervisionConfiguration()
        {
            HasRequired(x => x.SalesUnit).WithOptional().WillCascadeOnDelete(false);
            HasOptional(x => x.SupervisionUnit).WithOptionalPrincipal().WillCascadeOnDelete(false);
            Property(x => x.DateFinish).IsOptional();
            Property(x => x.DateRequired).IsOptional();
            Property(x => x.ServiceOrderNumber).IsOptional();
            Property(x => x.ClientOrderNumber).IsOptional();
        }
    }
}