namespace HVTApp.DataAccess
{
    public partial class BankDetailsConfiguration
    {
        public BankDetailsConfiguration()
        {
            Property(x => x.BankName).IsRequired().HasMaxLength(75);
            Property(x => x.BankIdentificationCode).IsRequired().HasMaxLength(20);
            Property(x => x.CheckingAccount).IsRequired().HasMaxLength(20);
            Property(x => x.CorrespondentAccount).IsRequired().HasMaxLength(20);
        }
    }
}