namespace HVTApp.Services.GetProductService.Kits
{
    public partial class ComplectTypesWindow
    {
        public ComplectTypesWindow(ComplectTypesViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SelectEvent += this.Close;
        }
    }
}
