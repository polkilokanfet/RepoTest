using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.TestDataGenerator;

namespace HVTApp.DataAccess
{
    public class HvtAppDataBaseInitializer : DropCreateDatabaseAlways<HVTAppContext> //DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        protected override void Seed(HVTAppContext context)
        {
            TestData testData = new TestData();

            //context.Documents.Add(testData.DocumentOfferMrsk);
            //context.Offers.Add(testData.OfferMrsk);
            context.Countries.Add(testData.CountryRussia);
            context.ActivityFilds.AddRange(new[] { testData.ActivityFieldProducerOfHvt, testData.ActivityFieldBuilder, testData.ActivityFieldElectricityTransmission, testData.ActivityFieldElectricityGeneration });
            context.CompanyForms.AddRange(new[] { testData.CompanyFormAo, testData.CompanyFormOao, testData.CompanyFormPao, testData.CompanyFormZao });
            context.Companies.AddRange(new[] { testData.CompanyEnel, testData.CompanyFsk, testData.CompanyMrsk, testData.CompanyRosseti, testData.CompanyUetm });
            context.Users.AddRange(new[] { testData.UserIvanov });
            context.Employees.AddRange(new[] { testData.EmployeeIvanov, testData.EmployeePetrov, testData.EmployeeSidorov });
            context.RequiredDependentEquipmentsParameterses.AddRange(new[] { testData.RequiredChildProductParametersBreakerBlock, testData.RequiredChildProductParametersDrive });
            context.Facilities.AddRange(new[] { testData.FacilityStation, testData.FacilitySubstation });
            context.Projects.AddRange(new[] { testData.Project1, testData.Project2 });
            context.Parameters.AddRange(new[] {
                testData.ParameterBreaker, testData.ParameterTransformator, testData.ParameterBreakerDeadTank,
                testData.ParameterBreakerLiveTank, testData.ParameterTransformatorCurrent, testData.ParameterTransformatorVoltage,
                testData.ParameterVoltage35kV, testData.ParameterVoltage110kV, testData.ParameterVoltage220kV, testData.ParameterVoltage500kV });
            context.Specifications.Add(testData.SpecificationMrsk1);
            //context.Tenders.Add(testData.TenderMrsk);

            context.ProjectUnits.AddRange(new[] { testData.ProjectUnitVeb1101, testData.ProjectUnitVeb1102, testData.ProjectUnitZng1101, testData.ProjectUnitZng1102, testData.ProjectUnitZng1103 });
            context.TenderUnits.AddRange(new [] { testData.TenderUnitVeb1101, testData.TenderUnitVeb1102, testData.TenderUnitZng1101, testData.TenderUnitZng1102, testData.TenderUnitZng1103});
            context.OfferUnits.AddRange(new[] { testData.OfferUnitVeb1101, testData.OfferUnitVeb1102, testData.OfferUnitZng1101, testData.OfferUnitZng1102, testData.OfferUnitZng1103 });
            context.ProductionUnits.AddRange(new[] { testData.ProductionUnitVeb1101, testData.ProductionUnitVeb1102, testData.ProductionUnitZng1101, testData.ProductionUnitZng1102, testData.ProductionUnitZng1103 });
            context.SalesUnits.AddRange(new[] { testData.SalesUnitVeb1101, testData.SalesUnitVeb1102, testData.SalesUnitZng1101, testData.SalesUnitZng1102, testData.SalesUnitZng1103 });
            context.ShipmentUnits.AddRange(new []{testData.ShipmentUnitVeb1101, testData.ShipmentUnitVeb1102, testData.ShipmentUnitZng1101, testData.ShipmentUnitZng1102,testData.ShipmentUnitZng1103});

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