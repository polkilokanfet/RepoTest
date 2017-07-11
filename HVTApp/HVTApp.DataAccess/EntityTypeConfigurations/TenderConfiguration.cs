using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class TenderConfiguration : EntityTypeConfiguration<Tender>
    {
        public TenderConfiguration()
        {
            HasRequired(x => x.Type).WithMany();
            HasRequired(x => x.Project).WithMany(x => x.Tenders);
            HasMany(x => x.Offers).WithRequired(x => x.Tender);
            HasMany(x => x.TenderUnits).WithRequired(x => x.Tender);
            HasMany(x => x.Participants).WithMany();
            HasOptional(x => x.Winner).WithMany();
        }
    }
}