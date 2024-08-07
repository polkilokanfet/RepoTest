using System;
using System.Windows;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Services;
using HVTApp.Views;
using Microsoft.Practices.Unity;

namespace HVTApp
{
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;

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
                    _bootstrapper = new Bootstrapper();
                    _bootstrapper.Run();
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

        protected override async void OnExit(ExitEventArgs e)
        {
            await _bootstrapper?.Container.Resolve<IEventServiceClient>().Stop();
            base.OnExit(e);
        }
    }
}
