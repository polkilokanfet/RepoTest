using System.Windows;
using System.Windows.Markup;

namespace HVTApp.UI.PriceEngineering.View
{
    [ContentProperty("InnerContent")]
    public partial class IncludedBlocksControl 
    {
        #region TaskViewModel

        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", 
            typeof(TaskViewModel), 
            typeof(IncludedBlocksControl), 
            new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModel TaskViewModel
        {
            get => (TaskViewModel)GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }

        #endregion

        #region InnerContent

        public static readonly DependencyProperty InnerContentProperty = DependencyProperty.Register(
            "InnerContent", typeof(object), typeof(IncludedBlocksControl), new UIPropertyMetadata(default(object)));

        public object InnerContent
        {
            get => (object) GetValue(InnerContentProperty);
            set => SetValue(InnerContentProperty, value);
        }

        #endregion

        public IncludedBlocksControl()
        {
            InitializeComponent();
        }
    }
}
