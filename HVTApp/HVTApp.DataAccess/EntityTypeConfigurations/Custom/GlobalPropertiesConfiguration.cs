namespace HVTApp.DataAccess
{
    public partial class GlobalPropertiesConfiguration
    {
        public GlobalPropertiesConfiguration()
        {
            HasRequired(globalProperties => globalProperties.NewProductParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.NewProductParameterGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.VoltageGroup).WithMany().WillCascadeOnDelete(false);

            HasOptional(globalProperties => globalProperties.IsolationColorGroup).WithMany().WillCascadeOnDelete(false);
            HasOptional(globalProperties => globalProperties.IsolationDpuGroup).WithMany().WillCascadeOnDelete(false);
            HasOptional(globalProperties => globalProperties.IsolationMaterialGroup).WithMany().WillCascadeOnDelete(false);
                                            
            HasRequired(globalProperties => globalProperties.ServiceParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.SupervisionParameter).WithMany().WillCascadeOnDelete(false);
            HasOptional(globalProperties => globalProperties.RecipientSupervisionLetterEmployee).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.SenderOfferEmployee).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.HvtProducersActivityField).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);
            HasOptional(globalProperties => globalProperties.Developer).WithMany().WillCascadeOnDelete(false);
            HasOptional(globalProperties => globalProperties.ProductIncludedDefault).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.ComplectsParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.ComplectsGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.ComplectDesignationGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(globalProperties => globalProperties.DefaultProjectType).WithMany().WillCascadeOnDelete(false);
        }
    }
}