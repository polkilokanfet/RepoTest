using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public abstract class TechnicalRequrementsTasksBaseViewModel : TechnicalRequrementsTaskLookupListViewModel
    {
        public DelegateLogCommand NewCommand { get; }
        public DelegateLogCommand EditCommand { get; }
        public DelegateLogCommand RemoveCommand { get; }

        public DelegateLogCommand ReloadCommand { get; }

        public bool CurrentUserIsManager => GlobalAppProperties.UserIsManager;
        public bool CurrentUserIsBackManager => GlobalAppProperties.UserIsBackManager;
        public bool CurrentUserIsBackManagerBoss => GlobalAppProperties.UserIsBackManagerBoss;

        protected TechnicalRequrementsTasksBaseViewModel(IUnityContainer container) : base(container)
        {
            //костыль - удаление пустых задач
            RemoveFailTasks();

            Load();

            this.SelectedLookupChanged += lookup =>
            {
                EditCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            };

            #region Commands

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

            #endregion
        }

        /// <summary>
        /// Подходит ли задача конкретно этой VM
        /// </summary>
        /// <param name="technicalRequrementsTask">Задача ТСЕ</param>
        /// <returns></returns>
        protected abstract bool TaskIsActual(TechnicalRequrementsTask technicalRequrementsTask);

        public override void Load()
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            IEnumerable<TechnicalRequrementsTask> calculations = UnitOfWork.Repository<TechnicalRequrementsTask>().Find(TaskIsActual);
            this.Load(calculations.OrderByDescending(technicalRequrementsTask => technicalRequrementsTask.Start));
        }

        protected override void OnAfterSaveEntity(TechnicalRequrementsTask task)
        {
            if (TaskIsActual(task))
            {
                var selectedLookup = this.SelectedLookup;
                base.OnAfterSaveEntity(task);
                this.SelectedLookup = selectedLookup;
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
                            var invoiceForPaymentItems = UnitOfWork.Repository<TaskInvoiceForPaymentItem>().Find(x => requrement.Id == x.TechnicalRequrements?.Id);
                            foreach (var item in invoiceForPaymentItems)
                            {
                                var taskInvoiceForPayment = UnitOfWork.Repository<TaskInvoiceForPayment>().GetById(item.TaskId);
                                taskInvoiceForPayment.Items.Remove(item);
                                if (taskInvoiceForPayment.Items.Any() == false)
                                    UnitOfWork.Repository<TaskInvoiceForPayment>().Delete(taskInvoiceForPayment);
                                UnitOfWork.Repository<TaskInvoiceForPaymentItem>().Delete(item);
                            }

                            task.Requrements.Remove(requrement);
                            UnitOfWork.Repository<TechnicalRequrements>().Delete(requrement);
                        }
                    }

                    if (task.Requrements.Any() == false)
                    {
                        //удаление файлов РТЗ
                        var shippingCostFiles = UnitOfWork.Repository<ShippingCostFile>().Find(shippingCostFile => task.Id == shippingCostFile.TechnicalRequrementsTaskId);
                        shippingCostFiles.ForEach(shippingCostFile => UnitOfWork.Repository<ShippingCostFile>().Delete(shippingCostFile));
                        
                        //удаление ответов ОГК
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

    }
}