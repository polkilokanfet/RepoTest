using System;
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
#else
            try
            {
#endif
                base.OnStartup(e);

                //Disable shutdown when the dialog closes
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                try
                {
                    GlobalAppProperties.User = new Auth().GetCurrentUser();
                    var bootstrapper = new Bootstrapper();
                    SplashScreenWindow splashScreenWindow = new SplashScreenWindow(bootstrapper);
                    splashScreenWindow.Show();
                    bootstrapper.Run();
                }
                catch (NoUserException)
                {
                    Application.Current.Shutdown();
                }

#if DEBUG
#else
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
