using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan
{
    public class PaymentsPlanViewModel : ViewModelBaseCanExportToExcel
    {
        public ObservableCollection<PaymentsPlanGroup> Payments { get; } = new ObservableCollection<PaymentsPlanGroup>();
        public DelegateLogCommand ReloadCommand { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="load">Необходимость автоматической загрузки.</param>
        public PaymentsPlanViewModel(IUnityContainer container, bool load = true) : base(container)
        {
            ReloadCommand = new DelegateLogCommand(Load);
            if (load)
                Load();
        }

        public void Load()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(salesUnit => !salesUnit.IsRemoved && !salesUnit.IsLoosen && !salesUnit.IsPaid && salesUnit.Project.ForReport);
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
                x.SalesUnit.Order,
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