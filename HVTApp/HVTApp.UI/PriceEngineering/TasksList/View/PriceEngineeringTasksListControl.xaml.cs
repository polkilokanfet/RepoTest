using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering
{
    public partial class PriceEngineeringTasksListControl : UserControl
    {
        public PriceEngineeringTasksListControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel", typeof(PriceEngineeringTasksListViewModel), typeof(PriceEngineeringTasksListControl), new PropertyMetadata(default(PriceEngineeringTasksListViewModel)));

        public PriceEngineeringTasksListViewModel ViewModel
        {
            get => (PriceEngineeringTasksListViewModel) GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
    }
}
