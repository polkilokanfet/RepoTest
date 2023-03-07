using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlHeader : UserControl
    {
        public static readonly DependencyProperty TasksAreaProperty = DependencyProperty.Register("TasksArea", typeof(object), typeof(ProductTaskControlHeader), new UIPropertyMetadata(null));
        public static readonly DependencyProperty ButtonsAreaProperty = DependencyProperty.Register("ButtonsArea", typeof(object), typeof(ProductTaskControlHeader), new UIPropertyMetadata(default(object)));

        public object TasksArea
        {
            get => (object) GetValue(TasksAreaProperty);
            set => SetValue(TasksAreaProperty, value);
        }

        public object ButtonsArea
        {
            get => (object) GetValue(ButtonsAreaProperty);
            set => SetValue(ButtonsAreaProperty, value);
        }

        #region TaskViewModel

        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", typeof(TaskViewModel), typeof(ProductTaskControlHeader), new PropertyMetadata(default(TaskViewModel)));

        public TaskViewModel TaskViewModel
        {
            get { return (TaskViewModel) GetValue(TaskViewModelProperty); }
            set { SetValue(TaskViewModelProperty, value); }
        }

        #endregion

        public ProductTaskControlHeader()
        {
            InitializeComponent();
        }
    }
}
