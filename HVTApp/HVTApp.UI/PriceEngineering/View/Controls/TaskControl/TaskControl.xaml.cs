using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class TaskControl : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register("TaskViewModel", typeof(TaskViewModel), typeof(TaskControl), new PropertyMetadata(default(TaskViewModel)));
        public static readonly DependencyProperty AreaSelectProductBlockProperty = DependencyProperty.Register("AreaSelectProductBlock", typeof(object), typeof(TaskControl), new UIPropertyMetadata(default(object)));
        public static readonly DependencyProperty AreaInstructProperty = DependencyProperty.Register("AreaInstruct", typeof(object), typeof(TaskControl), new UIPropertyMetadata(default(object)));
        public static readonly DependencyProperty AreaActionButtonsProperty = DependencyProperty.Register("AreaActionButtons", typeof(object), typeof(TaskControl), new UIPropertyMetadata(default(object)));
        public static readonly DependencyProperty AreaFilesProperty = DependencyProperty.Register("AreaFiles", typeof(object), typeof(TaskControl), new UIPropertyMetadata(default(object)));

        public TaskViewModel TaskViewModel
        {
            get => (TaskViewModel) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        public object AreaSelectProductBlock
        {
            get => (object) GetValue(AreaSelectProductBlockProperty);
            set => SetValue(AreaSelectProductBlockProperty, value);
        }

        public object AreaInstruct
        {
            get { return (object) GetValue(AreaInstructProperty); }
            set { SetValue(AreaInstructProperty, value); }
        }

        public object AreaActionButtons
        {
            get { return (object) GetValue(AreaActionButtonsProperty); }
            set { SetValue(AreaActionButtonsProperty, value); }
        }

        public object AreaFiles
        {
            get { return (object) GetValue(AreaFilesProperty); }
            set { SetValue(AreaFilesProperty, value); }
        }

        public TaskControl()
        {
            InitializeComponent();
        }
    }
}
