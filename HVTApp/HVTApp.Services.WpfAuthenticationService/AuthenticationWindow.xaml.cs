using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Services.WpfAuthenticationService
{
    public partial class AuthenticationWindow : IDialog, IDataContext
    {
        public AuthenticationWindow()
        {
            InitializeComponent();
        }
    }
}
