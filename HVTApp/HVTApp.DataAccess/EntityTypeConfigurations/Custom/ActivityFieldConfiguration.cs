using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ActivityFieldConfiguration : EntityTypeConfiguration<ActivityField>
    {
        public ActivityFieldConfiguration()
        {
            Property(x => x.ActivityFieldEnum).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(50);
        }
    }
}