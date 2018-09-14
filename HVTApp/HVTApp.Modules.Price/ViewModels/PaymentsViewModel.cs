using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Price.ViewModels
{
    public class PaymentsViewModel : ViewModelBase
    {
        private IUnitOfWork _unitOfWork;

        public ObservableCollection<PlanPayment> Payments { get; } = new ObservableCollection<PlanPayment>();

        public ICommand ReloadCommand { get; }

        public PaymentsViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
        }

        public async Task LoadAsync()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();
            var units = await _unitOfWork.Repository<SalesUnit>().GetAllAsNoTrackingAsync();
            var payments = new List<PlanPayment>();
            foreach (var unit in units)
            {
                payments.AddRange(unit.PaymentsPlannedCalculated.Select(x => new PlanPayment(unit, x)));
            }
            Payments.Clear();
            Payments.AddRange(payments.OrderBy(x => x.PaymentPlanned.Date));
        }
    }

    public class PlanPayment
    {
        public SalesUnit SalesUnit { get; }
        public PaymentPlanned PaymentPlanned { get; }

        public double Sum => SalesUnit.Cost * PaymentPlanned.Part * PaymentPlanned.Condition.Part;

        public PlanPayment(SalesUnit salesUnit, PaymentPlanned paymentPlanned)
        {
            SalesUnit = salesUnit;
            PaymentPlanned = paymentPlanned;
        }
    }
}