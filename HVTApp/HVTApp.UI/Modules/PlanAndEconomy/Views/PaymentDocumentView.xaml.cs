﻿using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.PlanAndEconomy.Tabs;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabPaymentDocument))]
    public partial class PaymentDocumentView : ViewBase
    {
        private readonly PaymentDocumentViewModel _viewModel;
        private readonly IMessageService _messageService;

        public PaymentDocumentView(PaymentDocumentViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;
            _messageService = messageService;
            this.DataContext = _viewModel;
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            var paymentDocument = navigationContext.Parameters.First().Value as PaymentDocument;
            var task = _viewModel.LoadAsync(paymentDocument);

            //try
            //{
            await task;
            //}
            //catch (Exception)
            //{
            //    _messageService.ShowOkMessageDialog("Exception", task.Exception.GetAllExceptions());
            //}
        }
    }
}