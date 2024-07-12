using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.TaskInvoiceForPayment1.Base
{
    public partial class CommonInformationControl : UserControl
    {
        public static readonly DependencyProperty HeadIsVisibleProperty = DependencyProperty.Register(
            "HeadIsVisible", 
            typeof(Visibility), 
            typeof(CommonInformationControl), 
            new PropertyMetadata(Visibility.Visible));

        public Visibility HeadIsVisible
        {
            get => (Visibility) GetValue(HeadIsVisibleProperty);
            set => SetValue(HeadIsVisibleProperty, value);
        }

        public CommonInformationControl()
        {
            InitializeComponent();
        }
    }
}
