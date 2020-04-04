using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HVTApp.Model.POCOs;

namespace HVTApp.TestDataGenerator
{
    public partial class TestData
    {
        public TestData()
        {
            var fields = GetType().GetFields();
            foreach (var fieldInfo in fields)
            {
                fieldInfo.SetValue(this, Activator.CreateInstance(fieldInfo.FieldType));
            }

            var methods = GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name.Contains("Generate"));
            foreach (var methodInfo in methods)
            {
                methodInfo.Invoke(this, null);
            }

            GenSalesUnits();
        }

        public IEnumerable<TData> GetAll<TData>()
        {
            var fields = GetType().GetFields().Where(x => x.FieldType == typeof(TData)).ToList();
            foreach (var field in fields)
            {
                yield return (TData)field.GetValue(this);
            }

            var lists = GetType().GetFields().Where(x => x.FieldType == typeof(List<TData>)).ToList();
            foreach (var list in lists)
            {
                foreach (var data in (List<TData>)list.GetValue(this))
                {
                    yield return data;
                }
            }
        }

        public List<Project> Projects = new List<Project>();
        public List<SalesUnit> SalesUnits = new List<SalesUnit>();
        private void GenSalesUnits()
        {
            var random = new Random();
            var products = new List<Product> {ProductBreakersDrivePprk, ProductVeb110, ProductZng110, ProductZip1};
            var facilityOwners = new List<Company> {CompanyFsk, CompanyMrsk};
            var contracts = new List<Contract> {ContractFsk, ContractMrsk, ContractPmk};

            for (int p = 1; p < 101; p++)
            {
                var project = new Project {Name = $"Реконструкция ПС №{p}", ProjectType = ProjectTypeReconstruction, Manager = UserIvanov};
                Projects.Add(project);

                for (int pr = 0; pr < random.Next(1, 5); pr++)
                {
                    var product = products[random.Next(0, products.Count)];
                    var facilityOwner = facilityOwners[random.Next(0, facilityOwners.Count)];
                    var facility = new Facility {Name = $"Подстанция №{p}", Type = FacilityTypeSubStation, OwnerCompany = facilityOwner};
                    int year = random.Next(DateTime.Today.Year - 1, DateTime.Today.Year + 2);
                    int month = random.Next(1, 13);
                    int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
                    var deliveryDateExpected = new DateTime(year, month, day);
                    var cost = random.Next(1000, 5000001);

                    DateTime? signalToStartProduction = null;
                    DateTime? signalToStartProductionDone = null;
                    Order order = null;
                    Company producer = null;
                    string sn = null;
                    Specification specification = null;

                    if (deliveryDateExpected < DateTime.Today)
                    {
                        if (random.Next(100) < 60)
                        {
                            producer = CompanyUetm;
                            signalToStartProduction = deliveryDateExpected.AddDays(-120);
                            signalToStartProductionDone = deliveryDateExpected.AddDays(-118);
                            order = new Order {DateOpen = signalToStartProductionDone.Value, Number = $"{p}-{pr}"};
                            specification = new Specification {Number = $"{p+10}", Vat = 20, Contract = contracts[random.Next(0, contracts.Count)], Date = signalToStartProduction.Value};
                            sn = $"sn{pr}{p}";
                        }
                        else if(random.Next(100) < 60)
                        {
                            producer = random.Next(100) < 70 ? CompanyZeto : CompanyApparat;
                        }
                    }


                    for (int i = 0; i < random.Next(1, 10); i++)
                    {
                        SalesUnits.Add(new SalesUnit
                        {
                            Project = project,
                            Product = product,
                            Cost = cost,
                            DeliveryDateExpected = deliveryDateExpected,
                            Facility = facility,
                            PaymentConditionSet = PaymentConditionSet50Na50,
                            SignalToStartProduction = signalToStartProduction,
                            SignalToStartProductionDone = signalToStartProductionDone,
                            Order = order,
                            OrderPosition = order == null ? null : $"{i+1}",
                            Producer = producer,
                            PickingDate = signalToStartProductionDone?.Date.AddDays(110),
                            EndProductionDate = signalToStartProductionDone?.Date.AddDays(120),
                            RealizationDate = signalToStartProductionDone?.Date.AddDays(121),
                            ShipmentDate = signalToStartProductionDone?.Date.AddDays(121),
                            DeliveryDate = signalToStartProductionDone?.Date.AddDays(123),
                            StartProductionDate = signalToStartProduction?.Date,
                            EndProductionPlanDate = signalToStartProductionDone?.Date.AddDays(100),
                            SerialNumber = sn == null ? null : $"{sn}-{i}",
                            Specification = specification
                        });
                    }
                }
            }
        }
        
    }
}