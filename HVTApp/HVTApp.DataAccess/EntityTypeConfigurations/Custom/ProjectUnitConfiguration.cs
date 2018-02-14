using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProjectUnitConfiguration
    {
        public ProjectUnitConfiguration()
        {
            HasRequired(x => x.CommonUnit).WithOptional().WillCascadeOnDelete(true);
            HasRequired(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId);
            HasRequired(x => x.Facility).WithMany().HasForeignKey(x => x.FacilityId);

            HasOptional(x => x.Producer).WithMany().HasForeignKey(x => x.ProducerId);

            HasMany(x => x.PaymentsConditions).WithMany();

            Property(x => x.DeliveryDate).IsRequired();
        }
    }
}