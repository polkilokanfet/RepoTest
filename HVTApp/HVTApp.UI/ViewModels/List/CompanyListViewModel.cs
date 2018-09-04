using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    [RoleToUpdate(Role.Admin, Role.DataBaseFiller)]
    public partial class CompanyListViewModel
    {
    }
}