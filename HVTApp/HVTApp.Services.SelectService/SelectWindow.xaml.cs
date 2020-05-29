using System.Windows;

namespace HVTApp.Services.SelectService
{
    public partial class SelectWindow : Window
    {
        private static readonly double WindowWidth = System.Windows.SystemParameters.PrimaryScreenWidth - 10;
        private static readonly double WindowHeigh = System.Windows.SystemParameters.PrimaryScreenHeight - 200;

        public SelectWindow()
        {
            InitializeComponent();

            ContentControl.Width = WindowWidth;
            ContentControl.Height = WindowHeigh;
        }
    }
}
