using System.Windows;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class CommonInformationControlManager
    {
        #region TasksViewModelManager

        public static readonly DependencyProperty TasksViewModelManagerProperty = DependencyProperty.Register(
            "TasksViewModelManager", 
            typeof(TasksViewModelManager), 
            typeof(CommonInformationControlManager), 
            new PropertyMetadata(default(TasksViewModelManager)));

        public TasksViewModelManager TasksViewModelManager
        {
            get => (TasksViewModelManager) GetValue(TasksViewModelManagerProperty);
            set => SetValue(TasksViewModelManagerProperty, value);
        }

        #endregion
        public CommonInformationControlManager()
        {
            InitializeComponent();
        }
    }
}
