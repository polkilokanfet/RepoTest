using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ContractConfiguration : EntityTypeConfiguration<Contract>
    {
        public ContractConfiguration()
        {
            this.Property(x => x.Number).IsRequired().HasMaxLength(50);
            this.HasRequired(x => x.Contragent);
            this.HasMany(x => x.Specifications).WithRequired(x => x.Contract);
        }
    }
}