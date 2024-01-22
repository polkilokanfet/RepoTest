using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.PriceCalculations.ViewModel.PriceCalculations.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculations
{
    public class PriceCalculationsViewModel : PriceCalculationLookupListViewModel
    {
        public NewCalculationCommand NewCalculationCommand { get; }
        public EditCalculationCommand EditCalculationCommand { get; }
        public RemoveCalculationCommand RemoveCalculationCommand { get; }

        public DelegateLogCommand ReloadCommand { get; }

        public LoadFileCommand LoadFileCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.UserIsManager;
        public bool CurrentUserIsPricer => GlobalAppProperties.User.RoleCurrent == Role.Pricer;
        public bool CurrentUserIsBackManager => GlobalAppProperties.UserIsBackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.UserIsBackManagerBoss;
        public bool CurrentUserIsDirector=> GlobalAppProperties.User.RoleCurrent == Role.Director;

        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
            Load();

            this.SelectedLookupChanged += lookup =>
            {
                EditCalculationCommand.RaiseCanExecuteChanged();
                RemoveCalculationCommand.RaiseCanExecuteChanged();
                LoadFileCommand.RaiseCanExecuteChanged();
            };

            NewCalculationCommand = new NewCalculationCommand(this.RegionManager);
            EditCalculationCommand = new EditCalculationCommand(this, this.RegionManager);
            RemoveCalculationCommand = new RemoveCalculationCommand(this, this.Container);
            LoadFileCommand = new LoadFileCommand(this, this.Container);
            ReloadCommand = new DelegateLogCommand(Load);
        }

        protected override void OnAfterSaveEntity(PriceCalculation calculation)
        {
            var targetCalculationLookup = Lookups.SingleOrDefault(priceCalculationLookup => priceCalculationLookup.Id == calculation.Id);
            if (targetCalculationLookup != null)
            {
                targetCalculationLookup.Refresh(calculation);
                return;
            }

            var author = calculation.PriceCalculationItems.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager;
            var canWath = author?.Id == GlobalAppProperties.User.Id && calculation.TaskCloseMoment.HasValue;
            var canCalc = calculation.TaskOpenMoment.HasValue &&
                              (GlobalAppProperties.User.RoleCurrent == Role.Admin || GlobalAppProperties.User.RoleCurrent == Role.Pricer);

            if (canWath || canCalc)
            {
                if (Lookups is ObservableCollection<PriceCalculationLookup> collection)
                {
                    if (!Lookups.Select(lookup => lookup.Entity).ContainsById(calculation))
                        collection.Insert(0, new PriceCalculationLookup(calculation));
                }
                return;
            }
        }

        public override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            
            RemoveFails(UnitOfWork);

            var calculations = new List<PriceCalculation>();

            if (CurrentUserIsManager)
            {
                calculations = UnitOfWork.Repository<PriceCalculation>().Find(IsCalculationOfManager);
            }

            if (CurrentUserIsBackManager)
            {
                calculations = UnitOfWork.Repository<PriceCalculation>().Find(priceCalculation => priceCalculation.Initiator?.Id == GlobalAppProperties.User.Id);
                calculations.AddRange(UnitOfWork.Repository<PriceEngineeringTasks>()
                    .Find(x => x.BackManager?.Id == GlobalAppProperties.User.Id)
                    .SelectMany(x => x.PriceCalculations));
            }

            if (CurrentUserIsBackManagerBoss)
            {
                //var tasks = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(technicalRequrementsTask => technicalRequrementsTask.BackManager != null);
                //calculations = tasks.SelectMany(technicalRequrementsTask => technicalRequrementsTask.PriceCalculations).Distinct().ToList();
                calculations = UnitOfWork.Repository<PriceCalculation>().Find(priceCalculation => priceCalculation.TaskOpenMoment.HasValue);
            }

            if (CurrentUserIsPricer)
            {
                calculations = UnitOfWork.Repository<PriceCalculation>().Find(priceCalculation => priceCalculation.TaskOpenMoment.HasValue);
            }

            if (CurrentUserIsDirector)
            {
                calculations = UnitOfWork.Repository<PriceCalculation>().Find(priceCalculation => priceCalculation.TaskCloseMoment.HasValue);
            }

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
                if (!failCalculation.PriceCalculationItems.Any())
                {
                    unitOfWork.Repository<PriceCalculationFile>().DeleteRange(failCalculation.Files);
                    unitOfWork.Repository<PriceCalculation>().Delete(failCalculation);
                }
            }
            unitOfWork.SaveChanges();
        }

        private bool IsCalculationOfManager(PriceCalculation calculation)
        {
            return calculation.PriceCalculationItems.First().SalesUnits.First().Project.Manager.IsAppCurrentUser();
        }
    }
}