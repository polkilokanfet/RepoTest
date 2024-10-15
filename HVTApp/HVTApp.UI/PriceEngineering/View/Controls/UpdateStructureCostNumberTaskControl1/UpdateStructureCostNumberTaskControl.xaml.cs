using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.Wrapper;

namespace HVTApp.UI.PriceEngineering.View.UpdateStructureCostNumberTaskControl1
{
    public partial class UpdateStructureCostNumberTaskControl : UserControl
    {
        public static readonly DependencyProperty UpdateTaskProperty = DependencyProperty.Register(
            nameof(UpdateTask), typeof(UpdateStructureCostNumberTaskViewModel), typeof(UpdateStructureCostNumberTaskControl), new PropertyMetadata(default(TaskProductBlockAddedWrapper)));

        public UpdateStructureCostNumberTaskViewModel UpdateTask
        {
            get => (UpdateStructureCostNumberTaskViewModel) GetValue(UpdateTaskProperty);
            set => SetValue(UpdateTaskProperty, value);
        }

        public UpdateStructureCostNumberTaskControl()
        {
            InitializeComponent();
        }
    }
}
