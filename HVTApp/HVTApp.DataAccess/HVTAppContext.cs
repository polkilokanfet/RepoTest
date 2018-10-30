using System.Data.Entity.ModelConfiguration.Conventions;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public partial class HvtAppContext : DbContext
    {
        public HvtAppContext() : base("name=HvtAppContext")
        {
            //#if DEBUG
            Database.SetInitializer(new HvtAppDataBaseInitializer());
            //#endif
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //таблицы в базе не плюрализованы
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            AddConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}