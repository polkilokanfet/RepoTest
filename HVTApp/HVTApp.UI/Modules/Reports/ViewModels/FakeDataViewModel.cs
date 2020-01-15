using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class FakeDataViewModel : ViewModelBase
    {
        public ObservableCollection<SalesUnitFakeDataGroup> SalesUnitFakeDataGroups { get; } = new ObservableCollection<SalesUnitFakeDataGroup>();

        public ICommand SaveCommand { get; }
        public FakeDataViewModel(IUnityContainer container) : base(container)
        {
            SaveCommand = new DelegateCommand(
                () =>
                {
                    SalesUnitFakeDataGroups.First().SalesUnits.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }, 
                () => SalesUnitFakeDataGroups.Any() && SalesUnitFakeDataGroups.First().SalesUnits.IsValid && SalesUnitFakeDataGroups.First().SalesUnits.IsChanged);
        }

        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitsList = new List<SalesUnit>();
            foreach (var salesUnit in salesUnits)
            {
                var unit = UnitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id);
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