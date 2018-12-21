using System;
using System.Net;
using System.Text;
using System.Windows;
using HVTApp.Model;
using HVTApp.Views;

namespace HVTApp
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            #if DEBUG

                base.OnStartup(e);

                //Disable shutdown when the dialog closes
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                GlobalAppProperties.User = await (new Auth()).GetCurrentUser();

                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();

            #else

            try
            {
                base.OnStartup(e);

                //Disable shutdown when the dialog closes
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                GlobalAppProperties.User = await (new Auth()).GetCurrentUser();

                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception exception)
            {
                var sb = new StringBuilder();
                while (exception != null)
                {
                    sb.AppendLine(exception.Message);
                    exception = exception.InnerException;
                }

                MessageBox.Show(sb.ToString());
                Console.WriteLine(exception);

                Application.Current.Shutdown();
            }

            #endif
        }
    }
}
