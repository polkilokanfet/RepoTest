using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class BankDetailsConfiguration : EntityTypeConfiguration<BankDetails>
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