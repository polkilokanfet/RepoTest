using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class PaymentPlannedListViewModel
    {
        public ICommand ReloadCommand { get; private set; }
        public override async Task LoadAsync()
        {
            var salesUnits = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsync();
            salesUnits.Select(x => new SalesUnitWrapper(x)).ForEach(x => x.ReloadPaymentsPlannedFull());
            await UnitOfWork.SaveChangesAsync();
            await base.LoadAsync();
        }
    }
}
