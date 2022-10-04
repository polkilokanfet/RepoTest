using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.Tce.Second.View
{
    public partial class SccVersionControl : UserControl
    {
        public static readonly DependencyProperty SccVersionProperty = DependencyProperty.Register(
            "SccVersion", typeof(SccVersionWrapper), typeof(SccVersionControl), new PropertyMetadata(default(SccVersionWrapper)));

        public SccVersionWrapper SccVersion
        {
            get => (SccVersionWrapper) GetValue(SccVersionProperty);
            set => SetValue(SccVersionProperty, value);
        }

        public SccVersionControl()
        {
            InitializeComponent();
        }
    }
}
