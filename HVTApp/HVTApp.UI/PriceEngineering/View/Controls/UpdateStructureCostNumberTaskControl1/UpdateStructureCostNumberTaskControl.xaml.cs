using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.Wrapper;

namespace HVTApp.UI.PriceEngineering.View.UpdateStructureCostNumberTaskControl1
{
    public partial class UpdateStructureCostNumberTaskControl : UserControl
    {
        public static readonly DependencyProperty UpdateTaskProperty = DependencyProperty.Register(
            nameof(UpdateTask), typeof(UpdateStructureCostNumberTaskForDesignDepartmentHeadViewModel), typeof(UpdateStructureCostNumberTaskControl), new PropertyMetadata(default(TaskProductBlockAddedWrapper)));

        public UpdateStructureCostNumberTaskForDesignDepartmentHeadViewModel UpdateTask
        {
            get => (UpdateStructureCostNumberTaskForDesignDepartmentHeadViewModel) GetValue(UpdateTaskProperty);
            set => SetValue(UpdateTaskProperty, value);
        }

        public UpdateStructureCostNumberTaskControl()
        {
            InitializeComponent();
        }
    }
}
