using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class ContractsView : UserControl
    {
        public ContractsView()
        {
            InitializeComponent();
        }
    }
}
