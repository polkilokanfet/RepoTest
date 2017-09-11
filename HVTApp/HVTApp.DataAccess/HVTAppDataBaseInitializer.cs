using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public class HvtAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        protected override void Seed(HVTAppContext context)
        {
            TestData testData = new TestData();

            context.Countries.Add(testData.CountryRussia);
            context.Districts.AddRange(new[] { testData.DistrictCentr, testData.DistrictUral });
            context.Regions.AddRange(new[] { testData.RegionMoskovskayaOblast, testData.RegionSverdlovskayaOblast });
            context.Localities.AddRange(new[] { testData.LocalityEkaterinburg, testData.LocalityMoscow });

            context.ActivityFilds.AddRange(new[] { testData.ActivityFieldProducerOfHvt, testData.ActivityFieldBuilder, testData.ActivityFieldElectricityTransmission, testData.ActivityFieldElectricityGeneration });
            context.CompanyForms.AddRange(new[] { testData.CompanyFormAo, testData.CompanyFormOao, testData.CompanyFormPao, testData.CompanyFormZao });
            context.Companies.AddRange(new[] { testData.CompanyEnel, testData.CompanyFsk, testData.CompanyMrsk, testData.CompanyRosseti, testData.CompanyUetm });

            context.Users.AddRange(new[] { testData.UserIvanov });
            context.Persons.AddRange(new[] { testData.PersonIvanov, testData.PersonPetrov, testData.PersonSidorov });
            context.Employees.AddRange(new[] { testData.EmployeeIvanov, testData.EmployeePetrov, testData.EmployeeSidorov });

            context.Facilities.AddRange(new[] { testData.FacilityStation, testData.FacilitySubstation });

            context.Parameters.AddRange(new[] {
                testData.ParameterBreaker, testData.ParameterTransformator, testData.ParameterBreakerDeadTank,
                testData.ParameterBreakerLiveTank, testData.ParameterTransformatorCurrent, testData.ParameterTransformatorVoltage,
                testData.ParameterVoltage35kV, testData.ParameterVoltage110kV, testData.ParameterVoltage220kV, testData.ParameterVoltage500kV });
            context.RequiredDependentEquipmentsParameterses.AddRange(new[] { testData.RequiredChildProductParametersBreakerBlock, testData.RequiredChildProductParametersDrive });
            context.Products.AddRange(new[] { testData.ProductVeb110, testData.ProductZng110, testData.ProductBreakersDrive });

            //context.Projects.Add(testData.Project3);
            //context.ProductionUnits.Add(testData.ProductionUnit1);
            //context.SalesUnits.Add(testData.SalesUnit1);
            context.Projects.AddRange(new[] { testData.Project1, testData.Project2 });
            context.Tenders.Add(testData.TenderMrsk);
            context.Documents.Add(testData.DocumentOfferMrsk);
            context.Offers.Add(testData.OfferMrsk);
            context.Specifications.Add(testData.SpecificationMrsk1);

            context.ProjectUnits.AddRange(new[] { testData.ProjectUnitVeb1101, testData.ProjectUnitVeb1102, testData.ProjectUnitZng1101, testData.ProjectUnitZng1102, testData.ProjectUnitZng1103 });
            context.TenderUnits.AddRange(new[] { testData.TenderUnitVeb1101, testData.TenderUnitVeb1102, testData.TenderUnitZng1101, testData.TenderUnitZng1102, testData.TenderUnitZng1103 });
            context.OfferUnits.AddRange(new[] { testData.OfferUnitVeb1101, testData.OfferUnitVeb1102, testData.OfferUnitZng1101, testData.OfferUnitZng1102, testData.OfferUnitZng1103 });
            context.ProductionUnits.AddRange(new[] { testData.ProductionUnitVeb1101, testData.ProductionUnitVeb1102, testData.ProductionUnitZng1101, testData.ProductionUnitZng1102, testData.ProductionUnitZng1103 });
            context.SalesUnits.AddRange(new[] { testData.SalesUnitVeb1101, testData.SalesUnitVeb1102, testData.SalesUnitZng1101, testData.SalesUnitZng1102, testData.SalesUnitZng1103 });
            context.ShipmentUnits.AddRange(new[] { testData.ShipmentUnitVeb1101, testData.ShipmentUnitVeb1102, testData.ShipmentUnitZng1101, testData.ShipmentUnitZng1102, testData.ShipmentUnitZng1103 });

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