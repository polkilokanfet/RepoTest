using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan
{
    public class PaymentsPlanViewModel : ViewModelBaseCanExportToExcel
    {
        public ObservableCollection<PaymentsPlanGroup> Payments { get; } = new ObservableCollection<PaymentsPlanGroup>();
        public ICommand ReloadCommand { get; }

        public PaymentsPlanViewModel(IUnityContainer container, bool load = true) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            if (load)
                Load();
        }

        public void Load()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => !x.IsLoosen && !x.IsPaid && x.Project.ForReport);
            Load(salesUnits);
        }

        public void Load(IEnumerable<SalesUnit> salesUnitsIn)
        {
            var salesUnits = salesUnitsIn.Where(x => !x.IsPaid && !x.IsLoosen && x.Project.ForReport).ToList();
            var payments = new List<Payment1>();
            foreach (var salesUnit in salesUnits)
            {
                payments.AddRange(salesUnit.PaymentsPlannedCalculated.Select(paymentPlanned => new Payment1(salesUnit, paymentPlanned)));
            }

            var groups = payments.GroupBy(x => new
            {
                x.SalesUnit.Cost,
                x.SalesUnit.SumNotPaid,
                x.SalesUnit.Product,
                x.SalesUnit.Project,
                x.SalesUnit.Facility,
                x.SalesUnit.Specification,
                x.PaymentPlanned.Part,
                x.PaymentPlanned.Date,
                x.PaymentPlanned.Condition
            })
            .OrderBy(x => x.Key.Date)
            .Select(x => new PaymentsPlanGroup(x))
            .Where(x => x.Sum > 0.00001)
            .ToList();

            Payments.Clear();
            Payments.AddRange(groups);
        }
    }
}