namespace HVTApp.Services.GetProductService.Complects
{
    public partial class ComplectWindow
    {
        public ComplectWindow(ComplectViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SaveEvent += this.Close;
        }
    }
}
