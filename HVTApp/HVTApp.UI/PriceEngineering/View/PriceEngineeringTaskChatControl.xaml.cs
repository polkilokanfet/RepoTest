using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTaskChatControl : UserControl
    {
        public static readonly DependencyProperty PriceEngineeringTaskViewModelProperty = DependencyProperty.Register(
            "PriceEngineeringTaskViewModel", typeof(PriceEngineeringTaskViewModel), typeof(PriceEngineeringTaskChatControl), new PropertyMetadata(default(PriceEngineeringTaskViewModel)));

        public PriceEngineeringTaskViewModel PriceEngineeringTaskViewModel
        {
            get => (PriceEngineeringTaskViewModel) GetValue(PriceEngineeringTaskViewModelProperty);
            set => SetValue(PriceEngineeringTaskViewModelProperty, value);
        }

        public PriceEngineeringTaskChatControl()
        {
            InitializeComponent();
        }
    }
}
