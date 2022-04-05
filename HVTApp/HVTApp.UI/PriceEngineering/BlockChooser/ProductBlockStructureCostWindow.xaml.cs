using System.Linq;
using System.Windows;

namespace HVTApp.UI.PriceEngineering.BlockChooser
{
    public partial class ProductBlockStructureCostWindow
    {
        public ProductBlockStructureCostWindow(ProductBlockStructureCostViewModel viewModel)
        {
            InitializeComponent();
            Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            this.DataContext = viewModel;
            viewModel.OkCommandExecuted += this.Close;
        }
    }
}
