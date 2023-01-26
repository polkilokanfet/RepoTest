using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class FilesControlManager1 : UserControl
    {
        public static readonly DependencyProperty TaskViewModelManagerProperty = DependencyProperty.Register(
            "TaskViewModelManager", typeof(TaskViewModelManager), typeof(FilesControlManager1), new PropertyMetadata(default(TaskViewModelManager)));

        public TaskViewModelManager TaskViewModelManager
        {
            get { return (TaskViewModelManager) GetValue(TaskViewModelManagerProperty); }
            set { SetValue(TaskViewModelManagerProperty, value); }
        }

        public FilesControlManager1()
        {
            InitializeComponent();
        }
    }
}
