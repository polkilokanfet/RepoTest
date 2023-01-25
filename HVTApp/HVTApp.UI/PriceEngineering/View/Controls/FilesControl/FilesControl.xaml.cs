using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class FilesControl : UserControl
    {
        #region TaskViewModel

        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModel), typeof(FilesControl), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModel TaskViewModel
        {
            get => (TaskViewModel) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        #endregion

        #region ButtonsAreaFiles

        public static readonly DependencyProperty ButtonsAreaFilesProperty = DependencyProperty.Register(
            "ButtonsAreaFiles", typeof(object), typeof(FilesControl), new UIPropertyMetadata(default(object)));

        public object ButtonsAreaFiles
        {
            get => (object) GetValue(ButtonsAreaFilesProperty);
            set => SetValue(ButtonsAreaFilesProperty, value);
        }

        #endregion

        #region ButtonsAreaAnswers

        public static readonly DependencyProperty ButtonsAreaAnswersProperty = DependencyProperty.Register(
            "ButtonsAreaAnswers", typeof(object), typeof(FilesControl), new UIPropertyMetadata(default(object)));

        public object ButtonsAreaAnswers
        {
            get => (object) GetValue(ButtonsAreaAnswersProperty);
            set => SetValue(ButtonsAreaAnswersProperty, value);
        }

        #endregion

        public FilesControl()
        {
            InitializeComponent();
        }
    }
}
