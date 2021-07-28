using System;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;

namespace HVTApp.UI.Modules.Settings.ViewModels
{
    public class DataBaseBackupViewModel
    {
        private readonly IUnityContainer _container;
        public DelegateLogCommand BackupDataBaseCommand { get; }

#if DEBUG
        public string ConnectionString { get; set; } = @"data source=(LocalDb)\MSSQLLocalDB;initial catalog=HvtAppDB;integrated security=True";
        public string DataBaseName { get; set; } = "HvtAppDB";
#else
        public string ConnectionString { get; set; } = @"data source=uetm2\s1;initial catalog=HVTApp.DataAccess.HvtAppContext;integrated security=True";
        public string DataBaseName { get; set; } = "HVTApp.DataAccess.HvtAppContext";
#endif
        public string Directory { get; set; } = @"G:\";

        public DataBaseBackupViewModel(IUnityContainer container)
        {
            _container = container;
            BackupDataBaseCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //var connection = new ServerConnection { ConnectionString = ConnectionString };
                        //var server = new Server(connection);
                        //var backup = new Backup
                        //{
                        //    Action = BackupActionType.Database,
                        //    Database = DataBaseName
                        //};
                        //backup.Devices.AddDevice($"{Directory}hvt.bak", DeviceType.File);
                        //backup.SqlBackup(server);
                        //_container.Resolve<IMessageService>().ShowOkMessageDialog("Info", "Success");
                    }
                    catch (Exception e)
                    {
                        _container.Resolve<IMessageService>().ShowOkMessageDialog("Error", e.PrintAllExceptions());
                    }
                });
        }
    }
}