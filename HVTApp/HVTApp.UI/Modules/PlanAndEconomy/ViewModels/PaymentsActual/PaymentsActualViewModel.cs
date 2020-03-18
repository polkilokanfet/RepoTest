using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class PaymentsActualViewModel : ViewModelBaseCanExportToExcel
    {
        private object _selectedItem;

        public ObservableCollection<SalesUnitPaymentGroup> PaymentGroups { get; } = new ObservableCollection<SalesUnitPaymentGroup>();

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
            }
        }

        public bool CanEdit => GlobalAppProperties.User.RoleCurrent != Role.SalesManager && 
                               GlobalAppProperties.User.RoleCurrent != Role.Director;

        public ICommand NewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand ReloadCommand { get; }

        public PaymentsActualViewModel(IUnityContainer container) : base(container)
        {
            Load();

            NewCommand = new DelegateCommand(() => RequestNavigate(new PaymentDocument()));
            EditCommand = new DelegateCommand(
                () => RequestNavigate((SelectedItem as SalesUnitPayment).PaymentDocument), 
                () => (SelectedItem as SalesUnitPayment) != null);
            ReloadCommand = new DelegateCommand(Load);
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = GlobalAppProperties.User.RoleCurrent == Role.SalesManager 
                ? UnitOfWork.Repository<SalesUnit>().Find(x => Equals(x.Project.Manager.Id, GlobalAppProperties.User.Id) && x.PaymentsActual.Any())
                : UnitOfWork.Repository<SalesUnit>().Find(x => x.PaymentsActual.Any());
            var documents = UnitOfWork.Repository<PaymentDocument>().GetAll();

            var payments = new List<SalesUnitPayment>();
            foreach (var salesUnit in salesUnits)
            {
                payments.AddRange(salesUnit.PaymentsActual.Select(payment => 
                    new SalesUnitPayment(salesUnit, payment, documents.Single(x => x.Payments.Contains(payment)))));
            }
            var groups = payments.GroupBy(x => new
                {
                    x.SalesUnit.Order,
                    x.SalesUnit.Facility,
                    x.SalesUnit.Product,
                    x.SalesUnit.Specification
                })
                .Select(x => new SalesUnitPaymentGroup(x))
                .OrderByDescending(x => x.LastDate);

            PaymentGroups.Clear();
            PaymentGroups.AddRange(groups);
        }

        private void RequestNavigate(PaymentDocument paymentDocument)
        {
            Container.Resolve<IRegionManager>().RequestNavigateContentRegion<PaymentDocumentView>(new NavigationParameters { { "", paymentDocument } });
        }
    }
}