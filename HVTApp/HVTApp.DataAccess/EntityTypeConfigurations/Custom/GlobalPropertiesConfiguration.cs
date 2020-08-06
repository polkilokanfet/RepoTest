namespace HVTApp.DataAccess
{
    public partial class GlobalPropertiesConfiguration
    {
        public GlobalPropertiesConfiguration()
        {
            HasRequired(x => x.NewProductParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.NewProductParameterGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.VoltageGroup).WithMany().WillCascadeOnDelete(false);

            HasOptional(x => x.IsolationColorGroup).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.IsolationDpuGroup).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.IsolationMaterialGroup).WithMany().WillCascadeOnDelete(false);

            HasRequired(x => x.ServiceParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.SupervisionParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.SenderOfferEmployee).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.HvtProducersActivityField).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.Developer).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.ProductIncludedDefault).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.ComplectsParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.ComplectsGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.ComplectDesignationGroup).WithMany().WillCascadeOnDelete(false);
        }
    }
}