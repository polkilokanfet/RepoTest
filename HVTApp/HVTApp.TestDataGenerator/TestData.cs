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

#if DEBUG

            GenSalesUnits();
            GenDocuments();
#endif
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

#if DEBUG

        public List<Project> Projects = new List<Project>();
        public List<SalesUnit> SalesUnits = new List<SalesUnit>();
        public List<PriceCalculation> PriceCalculations = new List<PriceCalculation>();
        public List<PaymentDocument> PaymentDocuments = new List<PaymentDocument>();
        public List<Document> Documents = new List<Document>();
        private void GenSalesUnits()
        {
            var random = new Random();
            var products = new List<Product> {ProductBreakersDrivePprk, ProductVeb110, ProductZng110, ProductZip1};
            var facilityOwners = new List<Company> {CompanyFsk, CompanyMrsk};
            var contracts = new List<Contract> {ContractFsk, ContractMrsk, ContractPmk};
            var managers = new List<User> {UserIvanov, UserKosolapov, UserGazizov, UserBrehov, UserKolesnik, UserRybin};
            var paymentConditionSets = new List<PaymentConditionSet> { PaymentConditionSet50Na50, PaymentConditionSet30Na70};

            for (int projectNum = 0; projectNum < 100; projectNum++)
            {
                var manager = managers[random.Next(0, managers.Count)];
                var project = new Project {Name = $"Реконструкция ПС №{projectNum}", ProjectType = ProjectTypeReconstruction, Manager = manager};
                Projects.Add(project);

                for (int pr = 0; pr < random.Next(1, 5); pr++)
                {
                    var product = products[random.Next(0, products.Count)];
                    var facilityOwner = facilityOwners[random.Next(0, facilityOwners.Count)];
                    var facility = new Facility {Name = $"Подстанция №{projectNum}", Type = FacilityTypeSubStation, OwnerCompany = facilityOwner};
                    int year = random.Next(DateTime.Today.Year - 1, DateTime.Today.Year + 2);
                    int month = random.Next(1, 13);
                    int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);
                    var deliveryDateExpected = new DateTime(year, month, day);
                    var cost = random.Next(1000, 5000001);
                    var paymentConditionSet = paymentConditionSets[random.Next(0, paymentConditionSets.Count)];

                    DateTime? signalToStartProduction = null;
                    DateTime? signalToStartProductionDone = null;
                    Order order = null;
                    Company producer = null;
                    string sn = null;
                    Specification specification = null;
                    PriceCalculation priceCalculation = null;
                    PaymentDocument paymentDocument = null;

                    if (deliveryDateExpected < DateTime.Today.AddDays(100))
                    {
                        if (random.Next(100) < 60)
                        {
                            producer = CompanyUetm;
                            signalToStartProduction = deliveryDateExpected.AddDays(-120);
                            signalToStartProductionDone = deliveryDateExpected.AddDays(-118);
                            order = new Order {DateOpen = signalToStartProductionDone.Value, Number = $"{projectNum}-{pr}"};
                            specification = new Specification {Number = $"{projectNum+10}", Vat = 20, Contract = contracts[random.Next(0, contracts.Count)], Date = signalToStartProduction.Value};
                            sn = $"sn-{pr}-{projectNum}";
                            paymentDocument = new PaymentDocument {Number = $"pd-{pr}-{projectNum}", Vat = 20};
                        }
                        else if(random.Next(100) < 60)
                        {
                            producer = random.Next(100) < 70 ? CompanyZeto : CompanyApparat;
                        }

                        priceCalculation = new PriceCalculation
                        {
                            TaskOpenMoment = deliveryDateExpected.AddDays(-140),
                            TaskCloseMoment = deliveryDateExpected.AddDays(-130)
                        };
                    }

                    var salesUnits = new List<SalesUnit>();
                    for (int salesUnitNum = 0; salesUnitNum < random.Next(1, 10); salesUnitNum++)
                    {
                        var salesUnit = new SalesUnit
                        {
                            Project = project,
                            Product = product,
                            Cost = cost,
                            DeliveryDateExpected = deliveryDateExpected,
                            Facility = facility,
                            PaymentConditionSet = paymentConditionSet,
                            SignalToStartProduction = signalToStartProduction,
                            SignalToStartProductionDone = signalToStartProductionDone,
                            Order = order,
                            OrderPosition = order == null ? null : $"{salesUnitNum + 1}",
                            Producer = producer,
                            PickingDate = signalToStartProductionDone?.Date.AddDays(110),
                            EndProductionDate = signalToStartProductionDone?.Date.AddDays(120),
                            RealizationDate = signalToStartProductionDone?.Date.AddDays(121),
                            ShipmentDate = signalToStartProductionDone?.Date.AddDays(121),
                            DeliveryDate = signalToStartProductionDone?.Date.AddDays(123),
                            StartProductionDate = signalToStartProduction?.Date,
                            EndProductionPlanDate = signalToStartProductionDone?.Date.AddDays(100),
                            SerialNumber = sn == null ? null : $"{sn}-{salesUnitNum}",
                            Specification = specification
                        };
                        SalesUnits.Add(salesUnit);
                        salesUnits.Add(salesUnit);
                    }

                    if (paymentDocument != null)
                    {
                        PaymentDocuments.Add(paymentDocument);
                        var date = signalToStartProduction.Value.AddDays(7);
                        var sum = cost * random.Next(0, 75) / 100;
                        foreach (var salesUnit in salesUnits)
                        {
                            salesUnit.PaymentsActual.Add(new PaymentActual {Date = date, Sum = sum});
                        }
                        var payments = salesUnits.SelectMany(x => x.PaymentsActual);
                        paymentDocument.Payments.AddRange(payments);
                    }

                    if (priceCalculation != null)
                    {
                        var salesUnit = salesUnits.First();
                        var priceCalculationItem = new PriceCalculationItem
                        {
                            RealizationDate = deliveryDateExpected.AddDays(-2),
                            PaymentConditionSet = salesUnit.PaymentConditionSet,
                            OrderInTakeDate = deliveryDateExpected.AddDays(-130)
                        };
                        priceCalculationItem.SalesUnits.AddRange(salesUnits);
                        priceCalculationItem.StructureCosts.Add(new StructureCost
                        {
                            Amount = 1,
                            Comment = $"structureCostName-{projectNum}",
                            Number = $"structureCostNumber-{projectNum}",
                            UnitPrice = salesUnit.Cost * random.Next(4, 7) / 10
                        });

                        priceCalculation.PriceCalculationItems.Add(priceCalculationItem);
                        PriceCalculations.Add(priceCalculation);
                    }
                }
            }
        }

        private void GenDocuments()
        {
            var employees = new List<Employee>() {EmployeeDeev};
            Employee sender;
            Employee recipient;
            for (int i = 0; i < 50; i++)
            {
                Documents.Add(new Document {Number = new DocumentNumber {Number = i}, Author = EmployeeSidorov, SenderEmployee = EmployeeSidorov, RecipientEmployee = EmployeeDeev, Comment = $"{i}"});
            }
        }

#endif
    }
}