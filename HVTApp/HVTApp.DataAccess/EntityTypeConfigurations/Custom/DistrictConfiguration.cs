using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DistrictConfiguration
    {
        public DistrictConfiguration()
        {
            HasRequired(x => x.Country).WithMany().WillCascadeOnDelete(false);
        }
    }
}