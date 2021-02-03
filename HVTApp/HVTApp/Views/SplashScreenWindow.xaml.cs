using System.Windows;

namespace HVTApp.Views
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        internal SplashScreenWindow(Bootstrapper bootstrapper)
        {
            InitializeComponent();

            //bootstrapper.ModuleIsInitialized += part =>
            //{
            //    ProgressBar1.Value = ProgressBar1.Maximum * part;
            //};

            bootstrapper.AllModulesAreInitialized += () =>
            {
                //ProgressBar1.Value = ProgressBar1.Maximum;
                this.Close();
            };
        }
    }
}
