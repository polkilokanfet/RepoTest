using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public partial class HvtAppContext : DbContext
    {
        public HvtAppContext() : base("name=HvtAppContext")
        {
            Database.SetInitializer(new HvtAppDataBaseInitializer());
        }
    }
}