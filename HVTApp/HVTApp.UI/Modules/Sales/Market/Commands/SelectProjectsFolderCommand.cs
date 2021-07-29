using System.Windows.Forms;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.Market.Commands
{
    public class SelectProjectsFolderCommand : DelegateLogCommand
    {
        protected override void ExecuteMethod()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.ProjectsFolderPath = dialog.SelectedPath;
                    Properties.Settings.Default.Save();
                }
            }
        }
    }
}