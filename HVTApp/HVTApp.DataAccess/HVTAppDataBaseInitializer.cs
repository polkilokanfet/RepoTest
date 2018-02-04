using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace HVTApp.DataAccess
{
    public partial class HvtAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HvtAppContext>
    {
        protected override void Seed(HvtAppContext context)
        {
            AddData(context);

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    Trace.TraceInformation("Entry: {0}", validationErrors.Entry.Entity);
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0}; Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }

            base.Seed(context);
        }

    }
}