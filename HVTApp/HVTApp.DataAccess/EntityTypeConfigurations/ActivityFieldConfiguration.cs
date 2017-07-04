using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ActivityFieldConfiguration : EntityTypeConfiguration<ActivityField>
    {
        public ActivityFieldConfiguration()
        {
            Property(x => x.FieldOfActivity).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(25);
        }
    }
}