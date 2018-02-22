using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OitViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public ObservableCollection<SalesUnitGroupWrapper> SalesUnits { get; } = new ObservableCollection<SalesUnitGroupWrapper>();

        public OitViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task LoadAsync()
        {
            var salesUnits = await _unitOfWork.GetRepository<SalesUnit>().GetAllAsync();
            SalesUnits.AddRange(salesUnits.Select(x => new SalesUnitGroupWrapper(x)));
        }
    }
}
