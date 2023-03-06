using System.Windows;
using System.Windows.Controls;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class ProductTaskControlBackManagerBoss : UserControl
    {
        public static readonly DependencyProperty TaskViewModelProperty = DependencyProperty.Register(
            "TaskViewModel", 
            typeof(TaskViewModelBackManagerBoss), 
            typeof(ProductTaskControlBackManagerBoss), 
            new PropertyMetadata(default(TaskViewModelBackManagerBoss)));

        public TaskViewModelBackManagerBoss TaskViewModel
        {
            get => (TaskViewModelBackManagerBoss) GetValue(TaskViewModelProperty);
            set => SetValue(TaskViewModelProperty, value);
        }
        public ProductTaskControlBackManagerBoss()
        {
            InitializeComponent();
        }
    }
}
