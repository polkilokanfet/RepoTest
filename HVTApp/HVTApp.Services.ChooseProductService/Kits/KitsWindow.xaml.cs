namespace HVTApp.Services.GetProductService.Kits
{
    public partial class KitsWindow
    {
        public KitsWindow(KitsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SelectEvent += this.Close;
        }
    }
}
