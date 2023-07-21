using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class PaymentsActualViewModel : LoadableExportableViewModel
    {
        private object _selectedItem;

        public bool CurrentUserIsManager => GlobalAppProperties.UserIsManager;

        public ObservableCollection<SalesUnitPaymentGroup> PaymentGroups { get; } = new ObservableCollection<SalesUnitPaymentGroup>();

        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanEdit => GlobalAppProperties.User.RoleCurrent != Role.SalesManager && 
                               GlobalAppProperties.User.RoleCurrent != Role.Director;

        public DelegateLogCommand NewCommand { get; }
        public DelegateLogCommand EditCommand { get; }

        public PaymentsActualViewModel(IUnityContainer container) : base(container)
        {
            NewCommand = new DelegateLogCommand(() => RequestNavigate(new PaymentDocument()));
            EditCommand = new DelegateLogCommand(
                () => RequestNavigate((SelectedItem as SalesUnitPayment).PaymentDocument),
                () => SelectedItem is SalesUnitPayment);
        }

        private void RequestNavigate(PaymentDocument paymentDocument)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PaymentsActual.PaymentDocumentView>(new NavigationParameters { { "", paymentDocument } });
        }

        private IOrderedEnumerable<SalesUnitPaymentGroup> _groups;
        protected override void GetData()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = GlobalAppProperties.UserIsManager
                ? ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>()).GetAllWithActualPaymentsOfCurrentUser()
                : ((ISalesUnitRepository) UnitOfWork.Repository<SalesUnit>()).GetAllWithActualPayments();
            var documents = UnitOfWork.Repository<PaymentDocument>().GetAll();

            var payments = new List<SalesUnitPayment>();
            foreach (var salesUnit in salesUnits)
            {
                payments.AddRange(salesUnit.PaymentsActual.Select(payment =>
                    new SalesUnitPayment(salesUnit, payment, documents.Single(paymentDocument => paymentDocument.Payments.Contains(payment)))));
            }
            _groups = payments.GroupBy(salesUnitPayment => new
                {
                    OrderId = salesUnitPayment.SalesUnit.Order?.Id,
                    FacilityId = salesUnitPayment.SalesUnit.Facility.Id,
                    ProductId = salesUnitPayment.SalesUnit.Product.Id,
                    SpecificationId = salesUnitPayment.SalesUnit.Specification?.Id
                })
                .Select(x => new SalesUnitPaymentGroup(x))
                .OrderByDescending(x => x.LastDate);
        }

        protected override void AfterGetData()
        {
            PaymentGroups.Clear();
            PaymentGroups.AddRange(_groups);
        }
    }
}