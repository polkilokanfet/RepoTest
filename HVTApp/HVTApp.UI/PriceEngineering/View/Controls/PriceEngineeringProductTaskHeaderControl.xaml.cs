using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringProductTaskHeaderControl : UserControl
    {
        public static readonly DependencyProperty TasksAreaProperty = DependencyProperty.Register(
            "TasksArea", typeof(object), typeof(PriceEngineeringProductTaskHeaderControl), new UIPropertyMetadata(null));

        public object TasksArea
        {
            get => (object) GetValue(TasksAreaProperty);
            set => SetValue(TasksAreaProperty, value);
        }

        public PriceEngineeringProductTaskHeaderControl()
        {
            InitializeComponent();
        }
    }
}
