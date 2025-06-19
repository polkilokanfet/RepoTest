namespace HVTApp.Services.GetProductService.Kits
{
    public partial class KitsTypesWindow
    {
        public KitsTypesWindow(KitTypesViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SelectEvent += this.Close;
        }
    }
}
