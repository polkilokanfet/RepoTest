namespace HVTApp.DataAccess
{
    public partial class TechnicalRequrementsTaskConfiguration
    {
        public TechnicalRequrementsTaskConfiguration()
        {
            HasMany(requrementsTask => requrementsTask.Requrements).WithRequired().WillCascadeOnDelete(false);
            HasMany(requrementsTask => requrementsTask.PriceCalculations).WithOptional().WillCascadeOnDelete(false);
            HasMany(requrementsTask => requrementsTask.AnswerFiles).WithRequired().HasForeignKey(x => x.TechnicalRequrementsTaskId).WillCascadeOnDelete(false);
            HasOptional(requrementsTask => requrementsTask.BackManager).WithMany().WillCascadeOnDelete(false);
        }
    }
}