using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class FilesControlConstructor : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModelConstructor), typeof(FilesControl), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModelConstructor TaskViewModel
        {
            get => (TaskViewModelConstructor)GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public FilesControlConstructor()
        {
            InitializeComponent();
        }
    }
}
