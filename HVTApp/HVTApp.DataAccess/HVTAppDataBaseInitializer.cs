using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class HvtAppDataBaseInitializer : DropCreateDatabaseIfModelChanges<HVTAppContext>
    {
        protected override void Seed(HVTAppContext context)
        {
            TestDataGenerator.TestData testData = new TestDataGenerator.TestData();

            context.Countries.Add(testData.CountryRussia);
            context.ActivityFilds.AddRange(new[] { testData.ActivityFieldProducerOfHvt, testData.ActivityFieldBuilder, testData.ActivityFieldElectricityTransmission, testData.ActivityFieldElectricityGeneration });
            context.CompanyForms.AddRange(new[] { testData.CompanyFormAo, testData.CompanyFormOao, testData.CompanyFormPao, testData.CompanyFormZao });
            context.Companies.AddRange(new[] { testData.CompanyEnel, testData.CompanyFsk, testData.CompanyMrsk, testData.CompanyRosseti, testData.CompanyUetm });
            context.Employees.Add(testData.EmployeeIvanov);
            context.Users.Add(testData.UserIvanov);
            context.RequiredDependentEquipmentsParameterses.AddRange(new[] { testData.RequiredChildProductParametersBreakerBlock, testData.RequiredChildProductParametersDrive });
            //context.Products.AddRange(new [] {veb110, vgb35, zng110});
            context.Facilities.AddRange(new[] { testData.FacilityStation, testData.FacilitySubstation });
            context.Projects.AddRange(new[] { testData.Project1, testData.Project2 });
            context.Parameters.AddRange(new[] { testData.ParameterBreaker, testData.ParameterTransformator, testData.ParameterBreakerDeadTank,
                testData.ParameterBreakerLiveTank, testData.ParameterTransformatorCurrent, testData.ParameterTransformatorVoltage,
                testData.ParameterVoltage35kV, testData.ParameterVoltage110kV, testData.ParameterVoltage220kV, testData.ParameterVoltage500kV });
            context.Specifications.Add(testData.SpecificationMrsk1);
            //context.Tenders.Add(tender);
            //context.Units.AddRange(new[] {productComplexUnitVeb110, productComplexUnitVeb1102, productComplexUnitZng1101, productComplexUnitZng1102});

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