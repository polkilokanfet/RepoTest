using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HVTApp.DataAccess
{
    using System.Data.Entity;

    public partial class HvtAppContext : DbContext
    {
#if DEBUG
        public HvtAppContext() : base("name=DebugConnectionString")
        {
#else
        public HvtAppContext() : base("name=ReleaseConnectionString")
        {
#endif
            Database.SetInitializer(new HvtAppDataBaseInitializer());
            //ожидание ответа от сервера
            ((IObjectContextAdapter) this).ObjectContext.CommandTimeout = 300;
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