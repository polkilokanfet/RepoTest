using System.Data.Entity.ModelConfiguration.Conventions;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public partial class HvtAppContext : DbContext
    {
#if DEBUG
        public HvtAppContext() : base("name=HvtAppContext")
        {
#else
        public HvtAppContext() : base("name=HvtAppContextRelease")
        {
#endif
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