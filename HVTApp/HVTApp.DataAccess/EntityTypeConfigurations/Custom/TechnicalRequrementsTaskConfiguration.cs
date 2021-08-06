namespace HVTApp.DataAccess
{
    public partial class TechnicalRequrementsTaskConfiguration
    {
        public TechnicalRequrementsTaskConfiguration()
        {
            HasMany(requrementsTask => requrementsTask.Requrements).WithRequired().WillCascadeOnDelete(false);
            HasMany(requrementsTask => requrementsTask.PriceCalculations).WithOptional().WillCascadeOnDelete(false);
            HasMany(requrementsTask => requrementsTask.AnswerFiles).WithRequired().HasForeignKey(answerFileTce => answerFileTce.TechnicalRequrementsTaskId).WillCascadeOnDelete(false);
            HasOptional(requrementsTask => requrementsTask.BackManager).WithMany().WillCascadeOnDelete(false);
            HasMany(requrementsTask => requrementsTask.HistoryElements).WithRequired().HasForeignKey(historyElement => historyElement.TechnicalRequrementsTaskId).WillCascadeOnDelete(false);
            HasMany(requrementsTask => requrementsTask.ShippingCostFiles).WithRequired().HasForeignKey(file => file.TechnicalRequrementsTaskId).WillCascadeOnDelete(false);
        }
    }
}