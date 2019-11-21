using System.Windows;

namespace HVTApp.Services.SelectService
{
    public partial class SelectWindow : Window
    {
        public SelectWindow()
        {
            double width = System.Windows.SystemParameters.PrimaryScreenWidth;
            double heigh = System.Windows.SystemParameters.PrimaryScreenHeight - 100;

            this.MinWidth = width;
            this.MinHeight = heigh;

            this.Width = width;
            this.Height = heigh;

            this.MaxWidth = width;
            this.MaxHeight= heigh;
            InitializeComponent();
        }
    }
}
