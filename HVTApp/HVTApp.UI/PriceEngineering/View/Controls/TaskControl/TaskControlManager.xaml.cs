using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControlManager : UserControl
    {
        //public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(TaskViewModelManager), typeof(TaskControl), new PropertyMetadata(default(TaskViewModelManager)));

        //public TaskViewModelManager ViewModel
        //{
        //    get => (TaskViewModelManager) GetValue(ViewModelProperty);
        //    set => SetValue(ViewModelProperty, value);
        //}

        public static readonly DependencyProperty TaskViewModelManagerProperty = DependencyProperty.Register(
            "TaskViewModelManager", typeof(TaskViewModelManager), typeof(TaskControlManager), new PropertyMetadata(default(TaskViewModelManager)));

        public TaskViewModelManager TaskViewModelManager
        {
            get { return (TaskViewModelManager) GetValue(TaskViewModelManagerProperty); }
            set { SetValue(TaskViewModelManagerProperty, value); }
        }

        public TaskControlManager()
        {
            InitializeComponent();
            //this.DataContextChanged += (sender, args) =>
            //{
            //    this.ViewModel = (TaskViewModelManager) this.DataContext;
            //};
        }

        
    }
}
