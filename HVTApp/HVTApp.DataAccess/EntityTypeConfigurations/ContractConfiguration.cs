using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ContractConfiguration : EntityTypeConfiguration<Contract>
    {
        public ContractConfiguration()
        {
            Property(x => x.Number).IsRequired().HasMaxLength(50);
            Property(x => x.Date).IsRequired();
            HasRequired(x => x.Contragent).WithMany();
            HasMany(x => x.Specifications).WithRequired(x => x.Contract);
        }
    }
}