using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class CommonInformationControlReadOnly : UserControl
    {
        public static readonly DependencyProperty TasksWrapperProperty = DependencyProperty.Register(
            "TasksWrapper", typeof(ITasksWrapper), typeof(CommonInformationControlReadOnly), new PropertyMetadata(default(ITasksWrapper)));

        public ITasksWrapper TasksWrapper
        {
            get { return (ITasksWrapper) GetValue(TasksWrapperProperty); }
            set { SetValue(TasksWrapperProperty, value); }
        }
        public CommonInformationControlReadOnly()
        {
            InitializeComponent();
        }
    }
}
