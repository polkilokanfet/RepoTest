using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.PriceEngineering.ViewModel;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManager;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss;
using HVTApp.UI.TaskInvoiceForPayment1.ForManager;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;

namespace HVTApp.UI.TaskInvoiceForPayment1.List
{
    public class TaskInvoiceForPaymentListViewModel : BindableBase, IIsShownActual
    {
        private readonly IUnityContainer _container;
        private bool _isShownActual = true;

        public ObservableCollection<TaskInvoiceForPaymentLookup> Items { get; } =
            new ObservableCollection<TaskInvoiceForPaymentLookup>();

        public TaskInvoiceForPaymentLookup SelectedItem { get; set; }

        public ICommand LoadCommand { get; }
        public ICommand EditCommand { get; }

        public bool IsShownActual
        {
            get => _isShownActual;
            set => SetProperty(ref _isShownActual, value);
        }


        public TaskInvoiceForPaymentListViewModel(IUnityContainer container)
        {
            _container = container;
            Load();
            LoadCommand = new DelegateLogCommand(Load);
            EditCommand = new DelegateLogCommand(
                () =>
                {
                    var p = new NavigationParameters {{string.Empty, SelectedItem.Entity}};
                    if (GlobalAppProperties.UserIsManager)
                        container.Resolve<IRegionManager>().RequestNavigateContentRegion<TaskInvoiceForPaymentManagerView>(p);
                    if (GlobalAppProperties.UserIsBackManager)
                        container.Resolve<IRegionManager>().RequestNavigateContentRegion<TaskInvoiceForPaymentBackManagerView>(p);
                    if (GlobalAppProperties.UserIsBackManagerBoss)
                        container.Resolve<IRegionManager>().RequestNavigateContentRegion<TaskInvoiceForPaymentBackManagerBossView>(p);
                },
                () => SelectedItem != null);
        }

        public void Load()
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();

            IEnumerable<TaskInvoiceForPayment> items;

            if (GlobalAppProperties.UserIsBackManagerBoss)
                items = unitOfWork.Repository<TaskInvoiceForPayment>()
                    .GetAll();
            else if (GlobalAppProperties.UserIsManager)
                items = unitOfWork.Repository<TaskInvoiceForPayment>()
                    .Find(task => task.Items.SelectMany(item => item.SalesUnits).Any(salesUnit => salesUnit.Project.Manager.IsAppCurrentUser()));
            else
                items = unitOfWork.Repository<TaskInvoiceForPayment>()
                    .Find(x => x.BackManager?.Id == GlobalAppProperties.User.Id);

            Items.Clear();
            Items.AddRange(items
                .OrderBy(x => x.MomentStart)
                .Select(x => new TaskInvoiceForPaymentLookup(x)));
        }
    }
}