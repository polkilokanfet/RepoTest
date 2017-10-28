using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class TenderConfiguration : EntityTypeConfiguration<Tender>
    {
        public TenderConfiguration()
        {
            HasRequired(x => x.Type).WithMany();
            Property(x => x.DateOpen).IsRequired();
            Property(x => x.DateClose).IsRequired();
            Property(x => x.DateNotice).IsOptional();
            HasMany(x => x.Participants).WithMany();
            HasOptional(x => x.Winner).WithMany();
            HasMany(x => x.TenderUnits).WithRequired().HasForeignKey(x => x.TenderId);
            HasMany(x => x.Offers).WithOptional();
        }
    }
}