using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTasksViewModel : TechnicalRequrementsTaskLookupListViewModel
    {
        public ICommand NewCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand RemoveCommand { get; }

        /// <summary>
        /// Поручить выполнение задачи
        /// </summary>
        public ICommand InstructCommand { get; set; }

        public ICommand ReloadCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss;

        public TechnicalRequrementsTasksViewModel(IUnityContainer container) : base(container)
        {
            Load();

            this.SelectedLookupChanged += lookup =>
            {
                ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)InstructCommand).RaiseCanExecuteChanged();
            };

            InstructCommand = new DelegateCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var backManagers = unitOfWork.Repository<User>().Find(x => x.Roles.Any(role => role.Role == Role.BackManager));
                    var selectService = Container.Resolve<ISelectService>();
                    var backManager = selectService.SelectItem(backManagers);

                    if (backManager != null)
                    {
                        var task = unitOfWork.Repository<TechnicalRequrementsTask>().GetById(SelectedLookup.Id);
                        task.BackManager = backManager;
                        unitOfWork.SaveChanges();
                        EventAggregator.GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(task);
                    }
                },
                () => CurrentUserIsBackManagerBoss && SelectedLookup != null && SelectedLookup.BackManager == null);

            NewCommand = new DelegateCommand(
                () =>
                {
                    //RegionManager.RequestNavigateContentRegion<PriceCalculations.View.PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), new PriceCalculation() } });
                });


            EditCommand = new DelegateCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(TechnicalRequrementsTask), SelectedItem } });
                },
                () => SelectedItem != null);

            RemoveCommand = new DelegateCommand(
                () =>
                {
                    //var messageService = Container.Resolve<IMessageService>();
                    //var result = messageService.ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить из расчет ПЗ?", defaultNo:true);
                    //if (result != MessageDialogResult.Yes) return;

                    //var unitOfWork = Container.Resolve<IUnitOfWork>();

                    //var calculation = unitOfWork.Repository<PriceCalculation>().GetById(SelectedItem.Id);
                    //foreach (var item in calculation.PriceCalculationItems.ToList())
                    //{
                    //    var salesUnits = item.SalesUnits.ToList();

                    //    //единицы, которы нельзя удалить из расчета, т.к. они размещены в производстве
                    //    var salesUnitsNotForRemove = salesUnits
                    //        .Where(x => x.SignalToStartProduction.HasValue)
                    //        .Where(x => x.ActualPriceCalculationItem(unitOfWork).Id == item.Id)
                    //        .ToList();

                    //    if (salesUnitsNotForRemove.Any())
                    //    {
                    //        var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
                    //        salesUnitsToRemove.ForEach(x => item.SalesUnits.Remove(x));
                    //        if (!item.SalesUnits.Any())
                    //        {
                    //            calculation.PriceCalculationItems.Remove(item);
                    //            unitOfWork.Repository<PriceCalculationItem>().Delete(item);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        calculation.PriceCalculationItems.Remove(item);
                    //        unitOfWork.Repository<PriceCalculationItem>().Delete(item);
                    //    }
                    //}

                    //if (calculation.PriceCalculationItems.Any())
                    //{
                    //    messageService.ShowOkMessageDialog("Удаление", "Вы не можете удалить некоторые строки в расчете, т.к. они размещены в производстве.");
                    //}
                    //else
                    //{
                    //    unitOfWork.Repository<PriceCalculation>().Delete(calculation);
                    //    ((ICollection<PriceCalculationLookup>)Lookups).Remove(SelectedLookup);
                    //}

                    //unitOfWork.SaveChanges();                   
                }, 
                () => SelectedItem != null);

            ReloadCommand = new DelegateCommand(Load);
        }

        protected override void OnAfterSaveEntity(TechnicalRequrementsTask task)
        {
            var targetCalculationLookup = Lookups.SingleOrDefault(x => x.Id == task.Id);
            if (targetCalculationLookup != null)
            {
                targetCalculationLookup.Refresh(task);
                return;
            }

            if (CurrentUserIsManager)
            {
                if (task.Requrements.First().SalesUnits.First().Project.Manager.IsAppCurrentUser())
                {
                    ((ICollection<TechnicalRequrementsTaskLookup>)Lookups).Add(new TechnicalRequrementsTaskLookup(task));
                }
            }

            if (CurrentUserIsBackManager)
            {
                if (task.BackManager?.IsAppCurrentUser() != null)
                {
                    ((ICollection<TechnicalRequrementsTaskLookup>) Lookups).Add(new TechnicalRequrementsTaskLookup(task));
                }
            }


            if (CurrentUserIsBackManagerBoss)
            {
                ((ICollection<TechnicalRequrementsTaskLookup>)Lookups).Add(new TechnicalRequrementsTaskLookup(task));
            }

        }

        public override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            IEnumerable<TechnicalRequrementsTask> calculations;

            //для бэка
            if (CurrentUserIsBackManager)
            {
                calculations = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(x => x.Start.HasValue && x.BackManager != null && x.BackManager.IsAppCurrentUser());
            }
            //для боса бэка
            else if(CurrentUserIsBackManagerBoss)
            {
                calculations = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(x => x.Start.HasValue);
            }
            //для менеджера
            else
            {
                calculations = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(IsTaskOfManager);
            }

            this.Load(calculations.OrderByDescending(x => x.Start));
        }

        private bool IsTaskOfManager(TechnicalRequrementsTask task)
        {
            if (!task.Requrements.Any())
                return false;

            if (!task.Requrements.First().SalesUnits.Any())
                return false;

            return task.Requrements.First().SalesUnits.First().Project.Manager.IsAppCurrentUser();
        }
    }
}