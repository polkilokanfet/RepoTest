using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTasksViewModel : TechnicalRequrementsTaskLookupListViewModel
    {
        public DelegateLogCommand NewCommand { get; }
        public DelegateLogCommand EditCommand { get; }
        public DelegateLogCommand RemoveCommand { get; }

        public DelegateLogCommand ReloadCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.User.RoleCurrent == Role.BackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.User.RoleCurrent == Role.BackManagerBoss;

        public TechnicalRequrementsTasksViewModel(IUnityContainer container) : base(container)
        {
            //костыль - удаление пустых задач
            RemoveFailTasks();

            Load();

            this.SelectedLookupChanged += lookup =>
            {
                EditCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            };

            NewCommand = new DelegateLogCommand(
                () =>
                {
                    //RegionManager.RequestNavigateContentRegion<PriceCalculations.View.PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), new PriceCalculation() } });
                });


            EditCommand = new DelegateLogCommand(
                () =>
                {
                    RegionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(TechnicalRequrementsTask), SelectedItem } });
                },
                () => SelectedItem != null);

            RemoveCommand = new DelegateLogCommand(
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

            ReloadCommand = new DelegateLogCommand(Load);
        }

        protected override void OnAfterSaveEntity(TechnicalRequrementsTask task)
        {
            var targetCalculationLookup = Lookups.SingleOrDefault(technicalRequrementsTaskLookup => technicalRequrementsTaskLookup.Id == task.Id);
            if (targetCalculationLookup != null)
            {
                targetCalculationLookup.Refresh(task);
                return;
            }

            if (CurrentUserIsManager)
            {
                if (task.Requrements.First().SalesUnits.First().Project.Manager.IsAppCurrentUser())
                {
                    InsertTask(task);
                }
            }

            if (CurrentUserIsBackManager)
            {
                if (task.BackManager?.IsAppCurrentUser() != null)
                {
                    InsertTask(task);
                }
            }


            if (CurrentUserIsBackManagerBoss)
            {
                InsertTask(task);
            }

        }

        private void InsertTask(TechnicalRequrementsTask task)
        {
            if (Lookups is ObservableCollection<TechnicalRequrementsTaskLookup> collection)
            {
                if (!Lookups.Select(lookup => lookup.Entity).ContainsById(task))
                    collection.Insert(0, new TechnicalRequrementsTaskLookup(task));
            }
        }

        //костыль - удаление пустых задач
        private void RemoveFailTasks()
        {
            if (CurrentUserIsBackManagerBoss)
            {
                List<TechnicalRequrementsTask> technicalRequrementsTasks = UnitOfWork.Repository<TechnicalRequrementsTask>().GetAll();
                foreach (var task in technicalRequrementsTasks)
                {
                    foreach (TechnicalRequrements requrement in task.Requrements.ToList())
                    {
                        if (!requrement.SalesUnits.Any())
                        {
                            task.Requrements.Remove(requrement);
                            UnitOfWork.Repository<TechnicalRequrements>().Delete(requrement);
                        }
                    }

                    if (!task.Requrements.Any())
                    {
                        var answerFiles = UnitOfWork.Repository<AnswerFileTce>().Find(answerFileTce => answerFileTce.TechnicalRequrementsTaskId == task.Id);
                        answerFiles.ForEach(answerFileTce => UnitOfWork.Repository<AnswerFileTce>().Delete(answerFileTce));
                        task.PriceCalculations.Clear();
                        foreach (var historyElement in task.HistoryElements.ToList())
                        {
                            UnitOfWork.Repository<TechnicalRequrementsTaskHistoryElement>().Delete(historyElement);
                        }
                        task.HistoryElements.Clear();
                        UnitOfWork.Repository<TechnicalRequrementsTask>().Delete(task);
                    }
                }

                UnitOfWork.SaveChanges();
            }
        }

        public override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            IEnumerable<TechnicalRequrementsTask> calculations;

            //для бэка
            if (CurrentUserIsBackManager)
            {
                calculations = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(technicalRequrementsTask => technicalRequrementsTask.BackManager != null && technicalRequrementsTask.BackManager.IsAppCurrentUser());
            }
            //для босса бэка
            else if(CurrentUserIsBackManagerBoss)
            {
                calculations = UnitOfWork.Repository<TechnicalRequrementsTask>().GetAll();
            }
            //для менеджера
            else
            {
                calculations = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(IsTaskOfManager);
            }

            this.Load(calculations.OrderByDescending(technicalRequrementsTask => technicalRequrementsTask.Start));
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