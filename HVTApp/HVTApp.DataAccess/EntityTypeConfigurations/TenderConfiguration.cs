using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class TenderConfiguration : EntityTypeConfiguration<Tender>
    {
        public TenderConfiguration()
        {
            HasRequired(x => x.Type).WithMany();
            HasRequired(x => x.Project).WithMany(x => x.Tenders).WillCascadeOnDelete(false);
            Property(x => x.Sum).IsRequired();
            Property(x => x.DateOpen).IsRequired();
            Property(x => x.DateClose).IsRequired();
            Property(x => x.DateNotice).IsOptional();
            HasMany(x => x.Participants).WithMany();
            HasOptional(x => x.Winner).WithMany();
            HasMany(x => x.TenderUnits).WithRequired(x => x.Tender);
            HasMany(x => x.Offers).WithOptional();
        }
    }
}