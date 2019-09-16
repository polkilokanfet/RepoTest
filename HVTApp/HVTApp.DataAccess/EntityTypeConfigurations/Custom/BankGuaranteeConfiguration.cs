namespace HVTApp.DataAccess
{
    public partial class BankGuaranteeConfiguration
    {
        public BankGuaranteeConfiguration()
        {
            HasRequired(x => x.BankGuaranteeType).WithMany().WillCascadeOnDelete(false);
        }
    }
}