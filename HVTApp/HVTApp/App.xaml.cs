using System;
using System.Net;
using System.Text;
using System.Windows;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Views;

namespace HVTApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            #if DEBUG

            base.OnStartup(e);

                //Disable shutdown when the dialog closes
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                GlobalAppProperties.User = new Auth().GetCurrentUser();


                var bootstrapper = new Bootstrapper();
                SplashScreenWindow splashScreenWindow = new SplashScreenWindow(bootstrapper);
                splashScreenWindow.Show();
                bootstrapper.Run();

            #else

            try
            {
                base.OnStartup(e);

                //Disable shutdown when the dialog closes
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                GlobalAppProperties.User = new Auth().GetCurrentUser();

                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.GetAllExceptions());
                Console.WriteLine(exception.GetAllExceptions());

                Application.Current.Shutdown();
            }

            #endif
        }
    }
}
