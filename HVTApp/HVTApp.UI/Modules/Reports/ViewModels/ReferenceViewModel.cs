using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class ReferenceViewModel : ViewModelBaseCanExportToExcel
    {
        public List<ReferenceItem> Items { get; } = new List<ReferenceItem>();

        public ICommand ReloadCommand { get; }

        public ReferenceViewModel(IUnityContainer container) : base(container)
        {
            ReloadCommand = new DelegateCommand(Load);
            Load();
        }

        public void Load()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.IsWon);
            var groups = salesUnits.GroupBy(x => x, new SalesUnitsReferenceComparer());
            var items = groups.Select(x => new ReferenceItem(x)).OrderBy(x => x.RealizationDate);
            Items.Clear();
            Items.AddRange(items);
        }
    }
}