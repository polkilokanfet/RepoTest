using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class CommonInformationControlConstructor : UserControl
    {
        public static readonly DependencyProperty TasksViewModelConstructorProperty = DependencyProperty.Register(
            "TasksViewModelConstructor", typeof(TasksViewModelConstructor), typeof(CommonInformationControlConstructor), new PropertyMetadata(default(TasksViewModelConstructor)));

        public TasksViewModelConstructor TasksViewModelConstructor
        {
            get { return (TasksViewModelConstructor) GetValue(TasksViewModelConstructorProperty); }
            set { SetValue(TasksViewModelConstructorProperty, value); }
        }
        public CommonInformationControlConstructor()
        {
            InitializeComponent();
        }
    }
}
