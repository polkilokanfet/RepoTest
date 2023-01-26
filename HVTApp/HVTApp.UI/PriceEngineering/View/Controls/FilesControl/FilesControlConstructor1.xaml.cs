using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class FilesControlConstructor1 : UserControl
    {
        public static readonly DependencyProperty TaskViewModelConstructorProperty = DependencyProperty.Register(
            "TaskViewModelConstructor", typeof(TaskViewModelConstructor), typeof(FilesControlConstructor1), new PropertyMetadata(default(TaskViewModelConstructor)));

        public TaskViewModelConstructor TaskViewModelConstructor
        {
            get { return (TaskViewModelConstructor) GetValue(TaskViewModelConstructorProperty); }
            set { SetValue(TaskViewModelConstructorProperty, value); }
        }

        public FilesControlConstructor1()
        {
            InitializeComponent();
        }
    }
}
