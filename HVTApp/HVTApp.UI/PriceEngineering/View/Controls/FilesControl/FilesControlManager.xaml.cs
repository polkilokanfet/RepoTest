using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class FilesControlManager : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModelManager), typeof(FilesControl), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModelManager TaskViewModel
        {
            get => (TaskViewModelManager)GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public FilesControlManager()
        {
            InitializeComponent();
        }
    }
}
