using System;
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
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.TaskInvoiceForPayment1
{
    public class TaskInvoiceForPaymentListViewModel : IIsShownActual
    {
        private readonly IUnityContainer _container;

        public ObservableCollection<TaskInvoiceForPaymentLookup> Items { get; } =
            new ObservableCollection<TaskInvoiceForPaymentLookup>();

        public TaskInvoiceForPaymentLookup SelectedItem { get; set; }

        public ICommand LoadCommand { get; }
        public ICommand EditCommand { get; }

        public TaskInvoiceForPaymentListViewModel(IUnityContainer container)
        {
            _container = container;
            Load();
            LoadCommand = new DelegateLogCommand(Load);
            EditCommand = new DelegateLogCommand(
                () =>
                {
                    var p = new NavigationParameters {{string.Empty, SelectedItem.Entity}};
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<TaskInvoiceForPaymentViewModelBackManagerBoss>(p);
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
            else
                items = unitOfWork.Repository<TaskInvoiceForPayment>()
                    .Find(x => x.BackManager?.Id == GlobalAppProperties.User.Id);

            Items.Clear();
            Items.AddRange(items
                .OrderBy(x => x.MomentStart)
                .Select(x => new TaskInvoiceForPaymentLookup(x)));
        }

        public bool IsShownActual { get; }
    }
}