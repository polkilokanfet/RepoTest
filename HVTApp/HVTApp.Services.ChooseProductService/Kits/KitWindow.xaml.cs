namespace HVTApp.Services.GetProductService.Kits
{
    public partial class KitWindow
    {
        public KitWindow(KitViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SaveEvent += this.Close;
        }
    }
}
