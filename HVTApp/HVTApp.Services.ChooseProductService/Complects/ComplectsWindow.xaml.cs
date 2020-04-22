namespace HVTApp.Services.GetProductService.Complects
{
    public partial class ComplectsWindow
    {
        public ComplectsWindow(ComplectsViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.SelectEvent += this.Close;
        }
    }
}
