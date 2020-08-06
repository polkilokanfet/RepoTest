using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.PlanAndEconomy.Supervision
{
    public class SupervisionViewModel : ViewModelBaseCanExportToExcel
    {
        public IValidatableChangeTrackingCollection<SupervisionWrapper> Units { get; } = new ValidatableChangeTrackingCollection<SupervisionWrapper>(new List<SupervisionWrapper>());

        public ICommand SaveCommand { get; }
        public ICommand RefreshCommand { get; }

        public SupervisionViewModel(IUnityContainer container) : base(container)
        {
            Load();

            RefreshCommand = new DelegateCommand(Load);

            SaveCommand = new DelegateCommand(
                () =>
                {
                    Units.Where(x => x.IsNew && x.IsValid && x.IsChanged).ForEach(x =>
                    {
                        UnitOfWork.Repository<Model.POCOs.Supervision>().Add(x.Model);
                        x.IsNew = false;
                    });
                    Units.AcceptChanges();

                    UnitOfWork.SaveChanges();
                }, 
                () => Units.IsValid && Units.IsChanged);

            Units.PropertyChanged += (sender, args) =>
            {
                ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
            };
        }

        private void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //шеф-монтажи (сохраненные)
            var supervisions = UnitOfWork.Repository<Model.POCOs.Supervision>().GetAll();
            var units = supervisions.Select(supervision => new SupervisionWrapper(supervision)).ToList();

            //выигранное оборудование со включенным шеф-монтажом
            var salesUnits = UnitOfWork.Repository<SalesUnit>()
                    .Find(x => x.IsWon) //только выигранное оборудование
                    .Except(supervisions.Select(x => x.SalesUnit)) //еще не сохраненное
                    .Where(x => x.ProductsIncluded.Any(pi => pi.Product.ProductBlock.IsSupervision)); //в которое включен шеф-монтаж

            units.AddRange(salesUnits.Select(x => new SupervisionWrapper(x)));

            Units.Clear();

            Units.AddRange(units.OrderBy(x => x.DateFinish).ThenBy(x => x.SalesUnit.OrderInTakeDate));
            Units.AcceptChanges();
        }
    }
}