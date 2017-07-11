using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class TenderUnitConfiguration : EntityTypeConfiguration<TenderUnit>
    {
        public TenderUnitConfiguration()
        {
            HasRequired(x => x.Tender).WithMany(x => x.TenderUnits);
            HasRequired(x => x.Product).WithMany();
            HasOptional(x => x.ProducerWinner).WithMany();
        }
    }
}