using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTaskFilesControl : UserControl
    {
        public static readonly DependencyProperty PriceEngineeringTaskViewModelProperty = DependencyProperty.Register(
            "PriceEngineeringTaskViewModel", typeof(PriceEngineeringTaskViewModel), typeof(PriceEngineeringTaskFilesControl), new PropertyMetadata(default(PriceEngineeringTaskViewModel)));

        public PriceEngineeringTaskViewModel PriceEngineeringTaskViewModel
        {
            get => (PriceEngineeringTaskViewModel)GetValue(PriceEngineeringTaskViewModelProperty);
            set => SetValue(PriceEngineeringTaskViewModelProperty, value);
        }

        public PriceEngineeringTaskFilesControl()
        {
            InitializeComponent();
        }
    }
}
