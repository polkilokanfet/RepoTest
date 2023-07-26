using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.Modules.Sales.Market.Controls
{
    public partial class Market2Control : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(string), typeof(Market2Control), new PropertyMetadata(default(string)));

        public string Header
        {
            get { return (string) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }



        public static readonly DependencyProperty PlaceHolder1Property = DependencyProperty.Register(
            "PlaceHolder1", typeof(object), typeof(Market2Control), new PropertyMetadata(default(object)));

        public object PlaceHolder1
        {
            get { return (object) GetValue(PlaceHolder1Property); }
            set { SetValue(PlaceHolder1Property, value); }
        }

        public Market2Control()
        {
            InitializeComponent();
        }
    }
}
