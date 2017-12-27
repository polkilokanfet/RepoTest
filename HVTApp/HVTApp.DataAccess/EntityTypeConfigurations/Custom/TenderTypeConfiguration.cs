using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TenderTypeConfiguration : EntityTypeConfiguration<TenderType>
    {
        public TenderTypeConfiguration()
        {
            Property(x => x.Type).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}