using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Reports.ViewModels
{
    public class FakeDataViewModel : ViewModelBase
    {
        public ObservableCollection<SalesUnitFakeDataGroup> SalesUnitFakeDataGroups { get; } = new ObservableCollection<SalesUnitFakeDataGroup>();

        public ICommand SaveCommand { get; }
        public FakeDataViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                async () =>
                {
                    SalesUnitFakeDataGroups.First().SalesUnits.AcceptChanges();
                    await UnitOfWork.SaveChangesAsync();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }, 
                () => SalesUnitFakeDataGroups.Any() && SalesUnitFakeDataGroups.First().SalesUnits.IsValid && SalesUnitFakeDataGroups.First().SalesUnits.IsChanged);
        }

        public async Task Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitsList = new List<SalesUnit>();
            foreach (var salesUnit in salesUnits)
            {
                var unit = await UnitOfWork.Repository<SalesUnit>().GetByIdAsync(salesUnit.Id);
                salesUnitsList.Add(unit);
            }
            var fakeDataGroup = new SalesUnitFakeDataGroup(salesUnitsList);
            SalesUnitFakeDataGroups.Add(fakeDataGroup);

            fakeDataGroup.SalesUnits.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
            };
        }
    }
}