using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.View
{
    public partial class PriceEngineeringTasksListControl : UserControl
    {
        public PriceEngineeringTasksListControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is INotifyPropertyChanged propertyChangedViewModel)
            {
                if (propertyChangedViewModel is IIsShownActual viewModel)
                {
                    propertyChangedViewModel.PropertyChanged += (o, eventArgs) =>
                    {
                        if (eventArgs.PropertyName == nameof(viewModel.IsShownActual))
                        {
                            this.DataGrid.SetFilter("ToShow", viewModel.IsShownActual, true);
                        }
                    };
                        
                    this.DataGrid.SetFilter("ToShow", viewModel.IsShownActual, true);
                }
            }

            this.Loaded -= OnLoaded;
        }
    }
}
