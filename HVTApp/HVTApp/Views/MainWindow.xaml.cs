using Infragistics.Windows.Ribbon;
using System.Windows;
using HVTApp.Infrastructure;
using Infragistics.Windows.OutlookBar.Events;

namespace HVTApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
