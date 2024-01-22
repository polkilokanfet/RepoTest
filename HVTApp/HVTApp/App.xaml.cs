using System;
using System.Windows;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Services;
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
                    var bootstrapper = new Bootstrapper();
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
                //(new HvtAppLogger()).LogError(exception.GetType().Name, exception);

                if (GlobalAppProperties.HvtAppLogger != null)
                    GlobalAppProperties.HvtAppLogger.LogError(exception.GetType().Name, exception);

                MessageBox.Show(exception.PrintAllExceptions());
                Console.WriteLine(exception.PrintAllExceptions());

                Application.Current.Shutdown();
            }
#endif
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (GlobalAppProperties.MessageService != null)
            {
                GlobalAppProperties.EventServiceClient.Stop();
            }

            base.OnExit(e);
        }
    }
}
