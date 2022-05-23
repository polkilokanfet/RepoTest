using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public partial class PriceEngineeringTaskTceItemControl : UserControl
    {
        public static readonly DependencyProperty PriceEngineeringTaskTceItemProperty = DependencyProperty.Register(
            "PriceEngineeringTaskTceItem", typeof(PriceEngineeringTaskTceItem), typeof(PriceEngineeringTaskTceItemControl), new PropertyMetadata(default(PriceEngineeringTaskTceItem)));

        public PriceEngineeringTaskTceItem PriceEngineeringTaskTceItem
        {
            get { return (PriceEngineeringTaskTceItem) GetValue(PriceEngineeringTaskTceItemProperty); }
            set { SetValue(PriceEngineeringTaskTceItemProperty, value); }
        }

        public PriceEngineeringTaskTceItemControl()
        {
            InitializeComponent();
        }
    }
}
