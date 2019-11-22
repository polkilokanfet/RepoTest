using Infragistics.Windows.Ribbon;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using Infragistics.Windows.OutlookBar.Events;

namespace HVTApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow(IMessageService messageService)
        {
            InitializeComponent();

            #if DEBUG
            #else
            this.Closing += (sender, args) =>
            {
                var dr = messageService.ShowYesNoMessageDialog("Выход", "Вы уверены, что хотите выйти?");
                if (dr == MessageDialogResult.No)
                    args.Cancel = true;
            };
            #endif
        }

        private void XamOutlookBar_OnSelectedGroupChanging(object sender, SelectedGroupChangingEventArgs e)
        {
            IOutlookBarGroup group = e.NewSelectedOutlookBarGroup as IOutlookBarGroup;
            if (group != null)
            {
                Commands.NavigateCommand.Execute(group.DefaultViewUri);
            }
        }
    }
}
