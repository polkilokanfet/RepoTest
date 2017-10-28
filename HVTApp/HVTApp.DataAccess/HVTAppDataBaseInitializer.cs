using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public class HvtAppDataBaseInitializer : DropCreateDatabaseAlways<HvtAppContext> //DropCreateDatabaseIfModelChanges<HvtAppContext>
    {
        protected override void Seed(HvtAppContext context)
        {
            TestData testData = new TestData();

            context.Countries.AddRange(testData.GetAll<Country>());
            context.Districts.AddRange(testData.GetAll<District>());
            context.Regions.AddRange(testData.GetAll<Region>());
            context.Localities.AddRange(testData.GetAll<Locality>());

            context.ActivityFilds.AddRange(testData.GetAll<ActivityField>());
            context.CompanyForms.AddRange(testData.GetAll<CompanyForm>());
            context.Companies.AddRange(testData.GetAll<Company>());

            context.Users.AddRange(testData.GetAll<User>());
            context.Persons.AddRange(testData.GetAll<Person>());
            context.Employees.AddRange(testData.GetAll<Employee>());

            context.Facilities.AddRange(testData.GetAll<Facility>());

            context.Parameters.AddRange(testData.GetAll<Parameter>());
            context.ParameterGroups.AddRange(testData.GetAll<ParameterGroup>());
            context.ProductsRelations.AddRange(testData.GetAll<ProductsRelation>());
            context.Products.AddRange(testData.GetAll<Product>());

            context.Projects.AddRange(testData.GetAll<Project>());
            context.Tenders.AddRange(testData.GetAll<Tender>());
            context.Documents.AddRange(testData.GetAll<Document>());
            context.Offers.AddRange(testData.GetAll<Offer>());
            context.Specifications.AddRange(testData.GetAll<Specification>());

            context.ProjectUnits.AddRange(testData.GetAll<ProjectUnit>());
            context.TenderUnits.AddRange(testData.GetAll<TenderUnit>());
            context.OfferUnits.AddRange(testData.GetAll<OfferUnit>());
            context.ProductionUnits.AddRange(testData.GetAll<ProductionUnit>());
            context.SalesUnits.AddRange(testData.GetAll<SalesUnit>());
            context.ShipmentUnits.AddRange(testData.GetAll<ShipmentUnit>());

            context.Orders.AddRange(testData.GetAll<Order>());
            context.Contracts.AddRange(testData.GetAll<Contract>());

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