using EventServiceClient2;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using Infragistics.Windows.OutlookBar.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Views
{
    public partial class MainWindow
    {
        private readonly IUnityContainer _container;

        public MainWindow(IUnityContainer container)
        {
            _container = container;

            InitializeComponent();

            this.Closing += (sender, args) =>
            {
#if DEBUG
#else
                var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Выход", "Вы уверены, что хотите выйти?", defaultNo:true);
                if (dr == MessageDialogResult.No)
                {
                    args.Cancel = true;
                    return;
                }
#endif

                //остановка синхронизатора
                _container.Resolve<EventServiceClient>().Stop();
            };
        }

        private void XamOutlookBar_OnSelectedGroupChanging(object sender, SelectedGroupChangingEventArgs e)
        {
            var group = e.NewSelectedOutlookBarGroup as IOutlookBarGroup;
            if (group != null)
            {
                Commands.NavigateCommand.Execute(group.DefaultViewUri);
            }
        }
    }
}
