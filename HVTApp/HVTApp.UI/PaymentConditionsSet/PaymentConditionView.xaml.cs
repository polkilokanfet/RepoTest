using System.Windows;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PaymentConditionsSet
{
    public partial class PaymentConditionView : IDialog
    {
        public PaymentConditionView()
        {
            InitializeComponent();
        }

        //public PaymentConditionView(IRegionManager regionManager, IEventAggregator eventAggregator, PaymentConditionViewModel viewModel) : base(regionManager, eventAggregator)
        //{
        //    InitializeComponent();
        //}
    }
}
