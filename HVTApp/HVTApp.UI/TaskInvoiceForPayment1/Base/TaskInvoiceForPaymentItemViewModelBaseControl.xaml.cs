using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public partial class TaskInvoiceForPaymentItemViewModelBaseControl : UserControl
    {
        public static readonly DependencyProperty SpecificContentProperty = 
            DependencyProperty.Register(
                "SpecificContent", 
                typeof(object), 
                typeof(TaskInvoiceForPaymentItemViewModelBaseControl), 
                new UIPropertyMetadata(default(object)));

        public object SpecificContent
        {
            get => GetValue(SpecificContentProperty);
            set => SetValue(SpecificContentProperty, value);
        }

        public TaskInvoiceForPaymentItemViewModelBaseControl()
        {
            InitializeComponent();
        }
    }
}
