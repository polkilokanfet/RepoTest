using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.TechnicalRequrementsTasksModule.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTaskViewModel : ViewModelBaseCanExportToExcel
    {
        private readonly IMessageService _messageService;
        private TechnicalRequrementsTask2Wrapper _technicalRequrementsTaskWrapper;

        private object _selectedItem;
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)DivideCommand).RaiseCanExecuteChanged();
            }
        }

        public bool CurrentUserIsManager => GlobalAppProperties.User.RoleCurrent == Role.SalesManager;

        public bool IsStarted => TechnicalRequrementsTaskWrapper?.Start != null;

        #region ICommand

        public ICommand SaveCommand { get; }
        public ICommand AddFileCommand { get; }
        public ICommand RemoveFileCommand { get; }

        public ICommand AddGroupCommand { get; }
        public ICommand RemoveGroupCommand { get; }

        public ICommand StartCommand { get; }

        public ICommand CancelCommand { get; }


        public ICommand MeregeCommand { get; }
        public ICommand DivideCommand { get; }
        
        #endregion

        public TechnicalRequrementsTask2Wrapper TechnicalRequrementsTaskWrapper
        {
            get { return _technicalRequrementsTaskWrapper; }
            private set
            {
                _technicalRequrementsTaskWrapper = value;
                //реакция на изменения в задаче
                _technicalRequrementsTaskWrapper.PropertyChanged += (sender, args) =>
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                };
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
            }
        }

        public TechnicalRequrementsTaskViewModel(IUnityContainer container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();

            #region SaveCommand
          
            //сохранение изменений
            SaveCommand = new DelegateCommand(
                () =>
                {
                    TechnicalRequrementsTaskWrapper.AcceptChanges();

                    var trt = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(TechnicalRequrementsTaskWrapper.Model.Id);
                    if (trt == null)
                    {
                        UnitOfWork.Repository<TechnicalRequrementsTask>().Add(TechnicalRequrementsTaskWrapper.Model);
                    }

                    UnitOfWork.SaveChanges();

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                },
                () => TechnicalRequrementsTaskWrapper.IsValid && TechnicalRequrementsTaskWrapper.IsChanged);

            #endregion

            #region AddFileCommand

            //добавление стракчакоста
            AddFileCommand = new DelegateCommand(
                () =>
                {
                    var structureCost = new StructureCost { Comment = "No title" };
                    var structureCostWrapper = new StructureCostWrapper(structureCost);
                    (SelectedItem as PriceCalculationItem2Wrapper).StructureCosts.Add(structureCostWrapper);
                },
                () => SelectedItem is PriceCalculationItem2Wrapper && !IsStarted);

            #endregion

            #region RemoveFileCommand

            //удаление файла
            RemoveFileCommand = new DelegateCommand(
                () =>
                {
                    //диалог
                    var result = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить файл?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    //поиск конкретной задачи
                    var file = SelectedItem as TechnicalRequrementsFileWrapper;
                    var task = TechnicalRequrementsTaskWrapper.Requrements.Single(x => x.Files.Contains(file));

                    //удаление
                    task.Files.Remove(file);
                    if (UnitOfWork.Repository<TechnicalRequrementsFile>().GetById(file.Id) != null)
                    {
                        UnitOfWork.Repository<TechnicalRequrementsFile>().Delete(file.Model);
                    }

                    SelectedItem = null;
                },
                () => !IsStarted && SelectedItem is TechnicalRequrementsFileWrapper);

            #endregion

            #region AddGroupCommand

            ////добавление группы оборудования
            //AddGroupCommand = new DelegateCommand(
            //    () =>
            //    {
            //        //потенциальные группы
            //        var items = UnitOfWork.Repository<SalesUnit>()
            //                .Find(x => x.Project.Manager.IsAppCurrentUser())
            //                .Except(TechnicalRequrementsTaskWrapper.PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Model))
            //                .Select(x => new SalesUnitEmptyWrapper(x))
            //                .GroupBy(x => x, new SalesUnit2Comparer())
            //                .Select(GetPriceCalculationItem2Wrapper);

            //        //выбор группы
            //        var viewModel = new PriceCalculationItemsViewModel(items);
            //        var dialogResult = Container.Resolve<IDialogService>().ShowDialog(viewModel);

            //        //добавление группы
            //        if (dialogResult.HasValue && dialogResult.Value)
            //        {
            //            viewModel.SelectedItemWrappers.ForEach(x => TechnicalRequrementsTaskWrapper.PriceCalculationItems.Add(x));
            //        }
            //    },
            //    () =>  !IsStarted);

            #endregion

            #region RemoveGroupCommand

            //удаление группы
            //RemoveGroupCommand = new DelegateCommand(
            //    () =>
            //    {
            //        var result = _messageService.ShowYesNoMessageDialog("Удаление", "Действительно хотите удалить из расчета группу оборудования?", defaultNo:true);
            //        if (result != MessageDialogResult.Yes) return;

            //        var selectedGroup = SelectedItem as PriceCalculationItem2Wrapper;

            //        var salesUnits = selectedGroup.SalesUnits.ToList();
                    
            //        //единицы, которы нельзя удалить из расчета, т.к. они размещены в производстве
            //        var salesUnitsNotForRemove = salesUnits
            //            .Where(x => x.Model.SignalToStartProduction.HasValue)
            //            .Where(x => x.Model.ActualPriceCalculationItem(UnitOfWork)?.Id == selectedGroup.Model.Id)
            //            .ToList();

            //        if (salesUnitsNotForRemove.Any())
            //        {
            //            _messageService.ShowOkMessageDialog("Удаление", "Вы не можете удалить некоторые строки, т.к. они размещены в производстве.");

            //            var salesUnitsToRemove = salesUnits.Except(salesUnitsNotForRemove).ToList();
            //            salesUnitsToRemove.ForEach(x => selectedGroup.SalesUnits.Remove(x));
            //            if(!selectedGroup.SalesUnits.Any())
            //                TechnicalRequrementsTaskWrapper.PriceCalculationItems.Remove(selectedGroup);
            //        }
            //        else
            //        {
            //            TechnicalRequrementsTaskWrapper.PriceCalculationItems.Remove(selectedGroup);
            //        }
            //    },
            //    () => SelectedItem is PriceCalculationItem2Wrapper && !IsStarted);

            #endregion

            #region StartCommand

            StartCommand = new DelegateCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите стартовать задачу?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes) return;

                    TechnicalRequrementsTaskWrapper.Start = DateTime.Now;
                    SaveCommand.Execute(null);

                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                    //уведомление по почте
                    //Container.Resolve<IEmailService>().SendMail("kos@uetm.ru", $"{GlobalAppProperties.User.Employee.Person} отправил новое задание на расчет", "test");

                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                    ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();
                },
                () => !IsStarted && TechnicalRequrementsTaskWrapper.IsValid);

            #endregion

            #region CancelCommand

            CancelCommand = new DelegateCommand(() =>
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Подтверждение", "Вы уверены, что хотите остановить задачу?", defaultNo:true);
                if (dr != MessageDialogResult.Yes) return;

                TechnicalRequrementsTaskWrapper.Start = null;
                SaveCommand.Execute(null);

                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Publish(TechnicalRequrementsTaskWrapper.Model);

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsStarted)));
                ((DelegateCommand)StartCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)CancelCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)AddGroupCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveFileCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemoveGroupCommand).RaiseCanExecuteChanged();

            },
            () => IsStarted);

            #endregion

            #region MergeCommand

            //MeregeCommand = new DelegateCommand(
            //    () =>
            //    {
            //        var result = _messageService.ShowYesNoMessageDialog("Слияние", "Действительно хотите слить строки, выделенные галкой?", defaultYes:true);
            //        if (result != MessageDialogResult.Yes) return;

            //        //айтемы для слияния
            //        var items = TechnicalRequrementsTaskWrapper.PriceCalculationItems.Where(x => x.IsChecked).ToList();

            //        if (items.Select(x => x.Facility.Id).Distinct().Count() > 1)
            //        {
            //            _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными Объектами поставки.");
            //            return;
            //        }

            //        if (items.Select(x => x.Product.Id).Distinct().Count() > 1)
            //        {
            //            _messageService.ShowOkMessageDialog("Слияние", "Вы не можете объединить строки с разными Продуктами поставки.");
            //            return;
            //        }

            //        var itemToSave = items.First();
            //        items.Remove(itemToSave);

            //        foreach (var item in items)
            //        {
            //            item.SalesUnits.ForEach(x => itemToSave.SalesUnits.Add(x));
            //            TechnicalRequrementsTaskWrapper.PriceCalculationItems.Remove(item);
            //            if (UnitOfWork.Repository<PriceCalculationItem>().GetById(item.Model.Id) != null)
            //                UnitOfWork.Repository<PriceCalculationItem>().Delete(item.Model);
            //        }
            //    },
            //    () =>
            //    {
            //        return !IsStarted && TechnicalRequrementsTaskWrapper.PriceCalculationItems.Count(x => x.IsChecked) > 1;
            //    });

            #endregion

            #region DivideCommand

            DivideCommand = new DelegateCommand(
                () =>
                {
                    var result = _messageService.ShowYesNoMessageDialog("Разбиение", "Действительно хотите разбить выбранную строку?", defaultNo: true);
                    if (result != MessageDialogResult.Yes) return;

                    var technicalRequrementsWrapper = (TechnicalRequrementsWrapper)SelectedItem;
                    var salesUnit = technicalRequrementsWrapper.SalesUnits.First();

                    var salesUnitsToDivide = technicalRequrementsWrapper.SalesUnits.ToList();
                    salesUnitsToDivide.Remove(salesUnit);

                    foreach (var unit in salesUnitsToDivide)
                    {
                        technicalRequrementsWrapper.SalesUnits.Remove(unit);

                        var technicalRequrements = new TechnicalRequrements()
                        {
                            Comment = technicalRequrementsWrapper.Comment
                        };
                        var requrementsWrapper = new TechnicalRequrementsWrapper(technicalRequrements);
                        requrementsWrapper.SalesUnits.Add(unit);
                        foreach (var structureCost in technicalRequrementsWrapper.StructureCosts)
                        {
                            var sc = new StructureCost()
                            {
                                Comment = structureCost.Comment,
                                Amount = structureCost.Amount,
                                Number = structureCost.Number
                            };
                            requrementsWrapper.StructureCosts.Add(new StructureCostWrapper(sc));
                        }

                        TechnicalRequrementsTaskWrapper.PriceCalculationItems.Add(requrementsWrapper);
                    }

                },
                () => !IsStarted && SelectedItem is PriceCalculationItem2Wrapper && ((PriceCalculationItem2Wrapper)SelectedItem).Amount > 1);

            #endregion

            TechnicalRequrementsTaskWrapper = new TechnicalRequrementsTask2Wrapper(new TechnicalRequrementsTask());
        }

        public void Load(TechnicalRequrementsTask technicalRequrementsTask)
        {
            var priceCalculationLoaded = UnitOfWork.Repository<TechnicalRequrementsTask>().GetById(technicalRequrementsTask.Id);

            TechnicalRequrementsTaskWrapper = priceCalculationLoaded != null 
                ? new TechnicalRequrementsTask2Wrapper(priceCalculationLoaded) 
                : new TechnicalRequrementsTask2Wrapper(technicalRequrementsTask);
        }

        /// <summary>
        /// Загрузка при создании нового расчета по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitWrappers = salesUnits
                .Select(x => UnitOfWork.Repository<SalesUnit>().GetById(x.Id))
                .Select(x => new SalesUnitEmptyWrapper(x))
                .ToList();
            
            var requirements = salesUnitWrappers
                .GroupBy(x => x, new SalesUnitForTechnicalRequrementsTaskComparer())
                .Select(x => new TechnicalRequrements2Wrapper(new TechnicalRequrements {SalesUnits = x.Select(u => u.Model).ToList()}))
                .ToList();

            foreach (var requirement in requirements)
            {
                TechnicalRequrementsTaskWrapper.Requrements.Add(requirement);
            }
        }
    }
}