using System.Windows;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Services.GetProductService
{
    public partial class ProductNewWindow : Window, IDialog, IDataContext
    {
        public ProductNewWindow()
        {
            InitializeComponent();
        }
    }
}
