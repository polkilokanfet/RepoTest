namespace HVTApp.DataAccess
{
    public partial class GlobalPropertiesConfiguration
    {
        public GlobalPropertiesConfiguration()
        {
            HasRequired(x => x.NewProductParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.NewProductParameterGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.VoltageGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.ServiceParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.SupervisionParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.SenderOfferEmployee).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.HvtProducersActivityField).WithMany().WillCascadeOnDelete(false);
        }
    }
}