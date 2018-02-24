using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class TenderConfiguration : EntityTypeConfiguration<Tender>
    {
        public TenderConfiguration()
        {
            HasRequired(x => x.Project).WithMany();
            HasMany(x => x.Types).WithMany();
            Property(x => x.DateOpen).IsRequired();
            Property(x => x.DateClose).IsRequired();
            Property(x => x.DateNotice).IsOptional();
            HasMany(x => x.Participants).WithMany();
            HasOptional(x => x.Winner).WithMany();
        }
    }
}