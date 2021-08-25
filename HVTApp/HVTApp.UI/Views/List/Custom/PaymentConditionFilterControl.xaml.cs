using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.Views
{
    public partial class PaymentConditionFilterControl : UserControl
    {
        public PaymentConditionFilterControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(string), typeof(PaymentConditionFilterControl), new PropertyMetadata(default(string)));

        public string Header
        {
            get => (string) GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }


        public static readonly DependencyProperty PartProperty = DependencyProperty.Register(
            "Part", typeof(double?), typeof(PaymentConditionFilterControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double? Part
        {
            get => (double?) GetValue(PartProperty);
            set => SetValue(PartProperty, value);
        }


        public static readonly DependencyProperty DaysToProperty = DependencyProperty.Register(
            "DaysTo", typeof(int?), typeof(PaymentConditionFilterControl), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public int? DaysTo
        {
            get => (int?) GetValue(DaysToProperty);
            set => SetValue(DaysToProperty, value);
        }


        public static readonly DependencyProperty IsBeforeProperty = DependencyProperty.Register(
            "IsBefore", typeof(bool), typeof(PaymentConditionFilterControl), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsBefore
        {
            get => (bool) GetValue(IsBeforeProperty);
            set => SetValue(IsBeforeProperty, value);
        }
    }
}
