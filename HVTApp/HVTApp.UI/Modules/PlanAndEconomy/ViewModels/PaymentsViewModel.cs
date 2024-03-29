using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentsViewModel : LoadableBindableBaseCanExportToExcel
    {
        private IUnitOfWork _unitOfWork;

        public ObservableCollection<PaymentsGroup> Payments { get; } = new ObservableCollection<PaymentsGroup>();

        public DelegateLogCommand ReloadCommand { get; }

        public PaymentsViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateLogCommand(Load);
        }

        protected override void LoadedMethod()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();
            var units = _unitOfWork.Repository<SalesUnit>().GetAllAsNoTracking();
            var payments = new List<PlanPayment>();
            foreach (var unit in units)
            {
                payments.AddRange(unit.PaymentsPlannedCalculated.Select(x => new PlanPayment(unit, x)));
            }

            Payments.Clear();
            Payments.AddRange(PaymentsGroup.GetGroups(payments));
        }
    }

    public class PaymentsGroup
    {
        private readonly List<PlanPayment> _payments;

        public PlanPayment Payment => _payments.First();
        public double Sum => _payments.Sum(x => x.Sum);
        public int Amount => _payments.Count;

        public string OrderPosition => Groups.Any() ? "..." : Payment.SalesUnit.OrderPosition;

        public List<PaymentsGroup> Groups { get; } = new List<PaymentsGroup>();

        public PaymentsGroup(IEnumerable<PlanPayment> payments)
        {
            _payments = payments.ToList();
            if (_payments.Count > 1)
            {
                Groups.AddRange(_payments.Select(x => new PaymentsGroup(new []{x})));
            }
        }

        public static IEnumerable<PaymentsGroup> GetGroups(IEnumerable<PlanPayment> payments)
        {
            var groups = payments.GroupBy(x => new
            {
                ProductId = x.SalesUnit.Product.Id,
                ProjectId = x.SalesUnit.Project.Id,
                OrderId = x.SalesUnit.Order?.Id,
                FacilityId = x.SalesUnit.Facility.Id,
                Cost = x.SalesUnit.Cost,
                SpecificationId = x.SalesUnit.Specification?.Id,
                ConditionId = x.PaymentPlanned.Condition.Id,
                Date = x.PaymentPlanned.Date,
                Sum = x.Sum
            }).OrderBy(x => x.Key.Date);
            return groups.Select(x => new PaymentsGroup(x));
        }
    }

    public class PlanPayment
    {
        public SalesUnit SalesUnit { get; }
        public PaymentPlanned PaymentPlanned { get; }

        public double Sum => SalesUnit.Cost * PaymentPlanned.Part * PaymentPlanned.Condition.Part;

        public string PaymentType => PaymentPlanned.Date < SalesUnit.RealizationDateCalculated ? "��" : "��";

        public PlanPayment(SalesUnit salesUnit, PaymentPlanned paymentPlanned)
        {
            SalesUnit = salesUnit;
            PaymentPlanned = paymentPlanned;
        }
    }
}