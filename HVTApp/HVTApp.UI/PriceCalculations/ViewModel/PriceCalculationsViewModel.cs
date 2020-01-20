using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class PriceCalculationsViewModel : PriceCalculationLookupListViewModel
    {
        public ICommand NewCalculationCommand { get; }
        public ICommand EditCalculationCommand { get; }
        public ICommand RemoveCalculationCommand { get; }

        public ICommand ReloadCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;

        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
            Load();

            this.SelectedLookupChanged += lookup =>
            {
                ((DelegateCommand)EditCalculationCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveCalculationCommand).RaiseCanExecuteChanged();
            };

            NewCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), new PriceCalculation() } });
                });


            EditCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), SelectedItem } });
                },
                () => SelectedItem != null);

            RemoveCalculationCommand = new DelegateCommand(
                () =>
                {

                }, 
                () => SelectedItem != null);

            ReloadCommand = new DelegateCommand(Load);
        }

        public override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            
            RemoveFails(UnitOfWork);

            var calculations = CurrentUserIsManager 
                ? UnitOfWork.Repository<PriceCalculation>().Find(IsCalculationOfManager) 
                : UnitOfWork.Repository<PriceCalculation>().Find(x => x.TaskOpenMoment.HasValue);

            this.Load(calculations.OrderByDescending(x => x.TaskOpenMoment));
        }

        /// <summary>
        /// костыль - удаление расчетов без единиц продаж
        /// </summary>
        /// <param name="unitOfWork"></param>
        private void RemoveFails(IUnitOfWork unitOfWork)
        {
            var failItems = unitOfWork.Repository<PriceCalculationItem>().Find(x => !x.SalesUnits.Any());
            if (!failItems.Any()) return;

            var failCalculations = unitOfWork.Repository<PriceCalculation>().Find(x => x.PriceCalculationItems.Any(item => failItems.Contains(item)));
            foreach (var failCalculation in failCalculations)
            {
                var items = failCalculation.PriceCalculationItems.Intersect(failItems).ToList();
                items.ForEach(item => failCalculation.PriceCalculationItems.Remove(item));
                unitOfWork.Repository<PriceCalculationItem>().DeleteRange(items);
                if(!failCalculation.PriceCalculationItems.Any())
                    unitOfWork.Repository<PriceCalculation>().Delete(failCalculation);
            }
            unitOfWork.SaveChanges();
        }

        private bool IsCalculationOfManager(PriceCalculation calculation)
        {
            return calculation.PriceCalculationItems.First().SalesUnits.First().Project.Manager.IsAppCurrentUser();
        }
    }
}