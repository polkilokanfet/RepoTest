using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
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

        public ICommand LoadFileCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsPricer => GlobalAppProperties.User.RoleCurrent == Role.Pricer;
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss;
        public bool CurrentUserIsDirector=> GlobalAppProperties.User.RoleCurrent == Role.Director;

        public PriceCalculationsViewModel(IUnityContainer container) : base(container)
        {
            Load();

            this.SelectedLookupChanged += lookup =>
            {
                ((DelegateCommand)EditCalculationCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveCalculationCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)LoadFileCommand).RaiseCanExecuteChanged();
            };

            NewCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters
                    {
                        { nameof(PriceCalculation), new PriceCalculation() }
                    });
                });


            EditCalculationCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<View.PriceCalculationView>(new NavigationParameters
                    {
                        { nameof(PriceCalculation), SelectedItem }
                    });
                },
                () => SelectedItem != null);

            RemoveCalculationCommand = new DelegateCommand(
                () =>
                {
                    var messageService = Container.Resolve<IMessageService>();
                    var result = messageService.ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить из расчет ПЗ?", defaultNo:true);
                    if (result != MessageDialogResult.Yes) return;

                    var unitOfWork = Container.Resolve<IUnitOfWork>();

                    var calculation = unitOfWork.Repository<PriceCalculation>().GetById(SelectedItem.Id);
                    foreach (var item in calculation.PriceCalculationItems.ToList())
                    {
                        var salesUnits = item.SalesUnits.ToList();

                        //единицы, которы нельзя удалить из расчета, т.к. они размещены в производстве
                        var salesUnitsNotForRemove = salesUnits
                            .Where(x => x.SignalToStartProduction.HasValue)
                            .Where(x => x.ActualPriceCalculationItem(unitOfWork).Id == item.Id)
                            .ToList();

                        if (salesUnitsNotForRemove.Any())
                        {
                            var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                            salesUnitsToRemove.ForEach(x => item.SalesUnits.Remove(x));
                            if (!item.SalesUnits.Any())
                            {
                                calculation.PriceCalculationItems.Remove(item);
                                unitOfWork.Repository<PriceCalculationItem>().Delete(item);
                            }
                        }
                        else
                        {
                            calculation.PriceCalculationItems.Remove(item);
                            unitOfWork.Repository<PriceCalculationItem>().Delete(item);
                        }
                    }

                    if (calculation.PriceCalculationItems.Any())
                    {
                        messageService.ShowOkMessageDialog("Удаление", "Вы не можете удалить некоторые строки в расчете, т.к. они размещены в производстве.");
                    }
                    else
                    {
                        unitOfWork.Repository<PriceCalculation>().Delete(calculation);
                        ((ICollection<PriceCalculationLookup>)Lookups).Remove(SelectedLookup);
                    }

                    unitOfWork.SaveChanges();                   
                }, 
                () => SelectedItem != null);

            LoadFileCommand = new DelegateCommand(
                () =>
                {
                    var messageService = Container.Resolve<IMessageService>();
                    if (!SelectedItem.Files.Any())
                    {
                        messageService.ShowOkMessageDialog("Информация", "В этот расчет ещё не загружен ни один файл.");
                        return;
                    }

                    var file = SelectedItem.Files.First();
                    if (SelectedItem.Files.Count > 1)
                    {
                        var selectService = Container.Resolve<ISelectService>();
                        file = selectService.SelectItem(SelectedItem.Files);
                        if (file == null)
                            return;
                    }

                    var storageDirectory = GlobalAppProperties.Actual.PriceCalculationsFilesPath;
                    string addToFileName = $"{file.CreationMoment.ToShortDateString()} {file.CreationMoment.ToShortTimeString()}";
                    FilesStorage.CopyFileFromStorage(file.Id, messageService, storageDirectory, addToFileName: addToFileName.ReplaceUncorrectSimbols("-"));
                },
                () => SelectedLookup != null);

            ReloadCommand = new DelegateCommand(Load);
        }

        protected override void OnAfterSaveEntity(PriceCalculation calculation)
        {
            var targetCalculationLookup = Lookups.SingleOrDefault(x => x.Id == calculation.Id);
            if (targetCalculationLookup != null)
            {
                targetCalculationLookup.Refresh(calculation);
                return;
            }

            var author = calculation.PriceCalculationItems.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager;
            var canWath = author?.Id == GlobalAppProperties.User.Id && calculation.TaskCloseMoment.HasValue;
            var canCalc = calculation.TaskOpenMoment.HasValue &&
                          (GlobalAppProperties.User.RoleCurrent == Role.Admin ||
                           GlobalAppProperties.User.RoleCurrent == Role.Pricer);

            if (canWath || canCalc)
            {
                ((ICollection<PriceCalculationLookup>) Lookups).Add(new PriceCalculationLookup(calculation));
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
                var tasks = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(x => x.BackManager != null && x.BackManager.IsAppCurrentUser());
                calculations = tasks.SelectMany(x => x.PriceCalculations).Distinct().ToList();
            }

            if (CurrentUserIsBackManagerBoss)
            {
                var tasks = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(x => x.BackManager != null);
                calculations = tasks.SelectMany(x => x.PriceCalculations).Distinct().ToList();
            }

            if (CurrentUserIsPricer)
            {
                calculations = UnitOfWork.Repository<PriceCalculation>().Find(x => x.TaskOpenMoment.HasValue);
            }

            if (CurrentUserIsDirector)
            {
                calculations = UnitOfWork.Repository<PriceCalculation>().Find(x => x.TaskCloseMoment.HasValue);
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