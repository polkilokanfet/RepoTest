using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTasksControl : UserControl
    {
        public static readonly DependencyProperty PriceCalculationsVisibilityProperty = DependencyProperty.Register(
            "PriceCalculationsVisibility", typeof(Visibility), typeof(PriceEngineeringTasksControl), new PropertyMetadata(default(Visibility)));

        public Visibility PriceCalculationsVisibility
        {
            get { return (Visibility) GetValue(PriceCalculationsVisibilityProperty); }
            set { SetValue(PriceCalculationsVisibilityProperty, value); }
        }

        public PriceEngineeringTasksControl()
        {
            InitializeComponent();
        }
    }
}
