using System;
using System.Net;
using System.Windows;
using HVTApp.Model;
using HVTApp.Views;

namespace HVTApp
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                //Disable shutdown when the dialog closes
                Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

                CommonOptions.User = await (new Auth()).GetCurrentUser();

                var bootstrapper = new Bootstrapper();
                bootstrapper.Run();
        }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                Console.WriteLine(exception);
                //throw;
            }
}
    }
}
