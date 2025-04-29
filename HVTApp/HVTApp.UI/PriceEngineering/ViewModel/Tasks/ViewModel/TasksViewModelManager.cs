using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceEngineering.Comparers;
using HVTApp.UI.PriceEngineering.DoStepCommand;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using HVTApp.UI.PriceEngineering.Tce.Second.View;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class TasksViewModelManager : TasksViewModelVisible<TasksWrapperManager, TaskViewModelManager>
    {
        public PriceCalculationEmptyWrapper SelectedCalculation { get; set; }

        #region Commands

        public DelegateLogCommand AddFileTechnicalRequirementsCommand { get; }
        public DelegateLogConfirmationCommand RemoveFileTechnicalRequirementsCommand { get; }
        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogConfirmationCommand StartCommand { get; }
        public DelegateLogConfirmationCommand StopCommand { get; }
        public DelegateLogCommand OpenPriceCalculationCommand { get; }
        public ICommandRaiseCanExecuteChanged CreatePriceCalculationCommand { get; }
        public DelegateLogCommand OpenTceCommand { get; }
        public DelegateLogCommand ReplaceProductsCommand { get; }
        public DelegateLogCommand PrintCommand { get; }
        public DelegateLogCommand LoadAllDesignDepartmentAnswersCommand { get; }

        #endregion

        /// <summary>
        /// Можно ли корректировать свойства (дату проработки, комментарий и т.д.)
        /// </summary>
        public override bool AllowEditProps
        {
            get
            {
                if (GlobalAppProperties.User.RoleCurrent != Role.SalesManager) return false;

                if (this.TasksWrapper == null) return false;

                return TasksWrapper.Model.StatusesAll.All(x => x.Equals(ScriptStep.Create)) ||
                       TasksWrapper.Model.StatusesAll.All(x => x.Equals(ScriptStep.Stop));
            }
        }

        protected override TasksWrapperManager GetPriceEngineeringTasksWrapper(PriceEngineeringTasks priceEngineeringTasks, IUnityContainer container)
        {
            return new TasksWrapperManager(priceEngineeringTasks, container);
        }

        public TasksViewModelManager(IUnityContainer container) : base(container)
        {
            #region Commands

            AddFileTechnicalRequirementsCommand = new DelegateLogCommand(
                () =>
                {
                    var fileNames = Container.Resolve<IGetFilePaths>().GetFilePaths().ToList();
                    if (fileNames.Any() == false) return;

                    //копируем каждый файл
                    foreach (var fileName in fileNames)
                    {
                        var fileWrapper = new PriceEngineeringTasksFileTechnicalRequirementsWrapper(new PriceEngineeringTasksFileTechnicalRequirements())
                        {
                            Name = Path.GetFileNameWithoutExtension(fileName).LimitLength(50),
                            Path = fileName
                        };
                        this.TasksWrapper.FilesTechnicalRequirements.Add(fileWrapper);
                    }

                    //RaisePropertyChanged(nameof(this.PriceEngineeringTasksWrapper.FilesTechnicalRequirements));
                },
                () => AllowEditProps);

            RemoveFileTechnicalRequirementsCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите удалить выделенное ТЗ?",
                () =>
                {
                    if (string.IsNullOrEmpty(SelectedFileTechnicalRequirements.Path))
                    {
                        SelectedFileTechnicalRequirements.IsActual = false;
                    }
                    else
                    {
                        this.TasksWrapper.FilesTechnicalRequirements.Remove(SelectedFileTechnicalRequirements);
                    }
                },
                () => AllowEditProps && SelectedFileTechnicalRequirements != null);

            OpenFileTechnicalRequirementsCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если файл уже в хранилище
                        if (string.IsNullOrEmpty(SelectedFileTechnicalRequirements.Path))
                        {
                            container.Resolve<IFilesStorageService>().OpenFileFromStorage(SelectedFileTechnicalRequirements.Id, GlobalAppProperties.Actual.TechnicalRequrementsFilesPath, SelectedFileTechnicalRequirements.Name);
                        }
                        //если файл еще не загружен в хранилище
                        else
                        {
                            Process.Start(SelectedFileTechnicalRequirements.Path);
                        }
                    }
                    catch (Exception e)
                    {
                        container.Resolve<IMessageService>().Message("Ошибка при открытии файла ТЗ", e.PrintAllExceptions());
                    }
                });

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если задание новое - добавляем его в базу
                        if (UnitOfWork.Repository<PriceEngineeringTasks>().GetById(this.TasksWrapper.Model.Id) == null)
                        {
                            UnitOfWork.Repository<PriceEngineeringTasks>().Add(this.TasksWrapper.Model);
                        }

                        this.TasksWrapper.AcceptChanges();
                        UnitOfWork.SaveChanges();
                        IsNew = false;
                        RaisePropertyChanged(nameof(IsNew));
                        Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Publish(this.TasksWrapper.Model);
                        //Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTasksStartedEvent>().Publish(this.TasksWrapper.Model);
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().Message("Ошибка при сохранении", e.PrintAllExceptions());
                    }
                },
                () => this.TasksWrapper != null && this.TasksWrapper.IsValid && this.TasksWrapper.IsChanged);

            StartCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите стартовать все задачи?",
                () =>
                {
                    LoadNewTechnicalRequirementFilesInStorage();
                    foreach (var childTask in TasksWrapper.ChildTasks)
                    {
                        foreach (var taskViewModel in childTask.GetAllPriceEngineeringTaskViewModels())
                        {
                            ((TaskViewModelBaseStartable) taskViewModel).StartCommand.ExecuteWithoutConfirmation();
                        }
                    }

                    SaveCommand.Execute();
                    StartCommand.RaiseCanExecuteChanged();
                    AddFileTechnicalRequirementsCommand.RaiseCanExecuteChanged();
                    RemoveFileTechnicalRequirementsCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(nameof(AllowEditProps));
                },
                () =>
                    this.TasksWrapper != null &&
                    this.TasksWrapper.IsValid &&
                    this.TasksWrapper.IsChanged &&
                    this.TasksWrapper.ChildTasks.Any() &&
                    this.TasksWrapper.ChildTasks.First() is TaskViewModelManagerNew &&
                    AllowEditProps);

            StopCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите остановить все задачи?",
                () =>
                {
                    foreach (var priceEngineeringTaskViewModel in this.TasksWrapper
                        .ChildTasks)
                    {
                        foreach (var viewModel in priceEngineeringTaskViewModel.GetAllPriceEngineeringTaskViewModels())
                        {
                            if (viewModel.Model.Status.Equals(ScriptStep.Stop))
                            {
                                continue;
                            }

                            if (viewModel is TaskViewModelManagerOld priceEngineeringTaskViewModelManager)
                            {
                                if (priceEngineeringTaskViewModelManager.StopCommand.CanExecute())
                                    priceEngineeringTaskViewModelManager.StopCommand.ExecuteWithoutConfirmation();
                            }
                        }
                    }

                    StopCommand.RaiseCanExecuteChanged();
                },
                () => 
                    this.TasksWrapper != null &&
                    this.TasksWrapper.ChildTasks.SelectMany(x => x.GetAllPriceEngineeringTaskViewModels()).All(x => x is TaskViewModelManagerOld) &&
                    this.TasksWrapper.ChildTasks.SelectMany(x => x.GetAllPriceEngineeringTaskViewModels()).Cast<TaskViewModelManagerOld>().All(x => x.StopCommand.CanExecute()) &&
                    this.IsNew == false);
            //this.PriceEngineeringTasksWrapper != null &&
            //    this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.SelectMany(x => x.Model.Statuses).Any(x => x.StatusEnum != PriceEngineeringTaskStatusEnum.Stopped && x.StatusEnum != PriceEngineeringTaskStatusEnum.Created));

            OpenPriceCalculationCommand = new DelegateLogCommand(
                () =>
                {
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters
                    {
                        {nameof(PriceCalculation), SelectedCalculation.Model}
                    });
                });

            CreatePriceCalculationCommand = new CreatePriceCalculationCommand(container, this);


            OpenTceCommand = new DelegateLogCommand(
                () =>
                {
                    this.Container.Resolve<IRegionManager>().RequestNavigateContentRegion<TasksTceView>(new NavigationParameters
                    {
                        {nameof(PriceEngineeringTasks), this.TasksWrapper.Model}
                    });
                });

            ReplaceProductsCommand = new DelegateLogCommand(
                () =>
                {
                    foreach (var priceEngineeringTaskViewModel in this.TasksWrapper.ChildTasks.Where(x => x.Model.IsAcceptedTotal))
                    {
                        if (priceEngineeringTaskViewModel is TaskViewModelManagerOld viewModel)
                        {
                            viewModel.ReplaceProductCommand.Execute();
                        }
                    }
                },
                () => this.TasksWrapper != null);

            PrintCommand = new DelegateLogCommand(
                () =>
                {
                    Container.Resolve<IPrintPriceEngineering>().PrintPriceEngineeringTasks(this.TasksWrapper.Model.Id);
                });

            LoadAllDesignDepartmentAnswersCommand = new DelegateLogCommand();

            #endregion

            this.SelectedFileTechnicalRequirementsChanged += () =>
            {
                RemoveFileTechnicalRequirementsCommand.RaiseCanExecuteChanged();
            };

            //реакция на событие замены списка задач
            this.PriceEngineeringTasksWrapperChanged += (valueOld, valueNew) =>
            {
                if (valueOld != null)
                {
                    valueOld.AllTasksAcceptedByManagerAction -= PriceEngineeringTasksWrapperOnAllTasksAcceptedByManagerAction;
                }

                if (valueNew != null)
                {
                    valueNew.AllTasksAcceptedByManagerAction += PriceEngineeringTasksWrapperOnAllTasksAcceptedByManagerAction;
                }

                RaisePropertyChanged(nameof(AllowEditProps));
                StopCommand.RaiseCanExecuteChanged();
            };
        }

        private void PriceEngineeringTasksWrapperOnAllTasksAcceptedByManagerAction()
        {
            //var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Вы приняли все задания. Хотите ли Вы загрузить результаты в ТСЕ и создать расчёт ПЗ?");
            //if (dr == MessageDialogResult.Yes)
            //{
            //    CreatePriceCalculationCommand.Execute();
            //}
        }

        /// <summary>
        /// Загрузка при создании новой технико-стоимостной проработки по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var _ = salesUnits
                .Select(salesUnit => UnitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                .GroupBy(salesUnit => salesUnit, new SalesUnitForPriceEngineeringTaskComparer())
                .Select(x => new TaskViewModelManagerNew(Container, UnitOfWork, x, this));

            TasksWrapper = new TasksWrapperManager(_, UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id));
            IsNew = true;
        }

        /// <summary>
        /// Загрузить все добавленные файлы ТЗ в хранилище
        /// </summary>
        private void LoadNewTechnicalRequirementFilesInStorage()
        {
            foreach (var fileWrapper in this.TasksWrapper.FilesTechnicalRequirements.AddedItems)
            {
                var destFileName = $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}";
                if (File.Exists(destFileName) == false && string.IsNullOrEmpty(fileWrapper.Path) == false)
                {
                    File.Copy(fileWrapper.Path, destFileName);
                    fileWrapper.Path = null;
                }
            }
        }

        public void RemoveChildTask(TaskViewModelManager taskViewModel)
        {
            if (this.TasksWrapper.ChildTasks.Contains(taskViewModel))
                this.TasksWrapper.ChildTasks.Remove(taskViewModel);
        }

        protected override bool ChildTaskIsVisibleByDefault(PriceEngineeringTask priceEngineeringTask)
        {
            return true;
        }
    }

    class CreatePriceCalculationCommand : DelegateLogCommand
    {
        private readonly IUnityContainer _container;
        private readonly TasksViewModelManager _tasksViewModel;

        public CreatePriceCalculationCommand(IUnityContainer container, TasksViewModelManager tasksViewModel)
        {
            _container = container;
            _tasksViewModel = tasksViewModel;
        }

        protected override bool CanExecuteMethod()
        {
            return _tasksViewModel.TasksWrapper != null && _tasksViewModel.IsNew == false;
        }

        protected override void ExecuteMethod()
        {
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var priceEngineeringTasks = unitOfWork.Repository<PriceEngineeringTasks>().GetById(_tasksViewModel.TasksWrapper.Model.Id);

                var hasProblemTasks = HasProblemTasks(priceEngineeringTasks);
                if (hasProblemTasks)
                {
                    var dr = _container.Resolve<IMessageService>().ConfirmationDialog("В TeamCenter не загружены некоторые сралчахвосты. \nХотите продолжить создание задачи на расчёт ПЗ?");
                    if (dr == false) return;
                }

                _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(
                    new NavigationParameters
                    {
                        {nameof(PriceEngineeringTasks), _tasksViewModel.TasksWrapper.Model},
                        {nameof(Boolean), IsTceConnected(priceEngineeringTasks)}
                    });
            }
        }

        private bool HasProblemTasks(PriceEngineeringTasks priceEngineeringTasks)
        {
            //Задачи, которые имеют не загруженные в ТСЕ стракчакосты
            var problemTasks = priceEngineeringTasks
                .ChildPriceEngineeringTasks
                .Where(task => task.AllProductBlocksHasSccNumbersInTce == false)
                .ToList();

            if (problemTasks.Any() == false) return false;

            foreach (var problemTask in problemTasks)
            {
                var problemTaskViewModel = _tasksViewModel.TasksWrapper.ChildTasks.Single(x => x.Model.Id == problemTask.Id);
                if (problemTaskViewModel.LoadToTceStartCommand.CanExecute(null))
                {
                    var dr = _container.Resolve<IMessageService>().ConfirmationDialog($"В TeamCenter не загружены некоторые сралчахвосты из ТСП №{problemTask.Number}. \nХотите запустить эту задачу на загрузку в TeamCenter?");
                    if (dr == false) continue;
                    ((DoStepCommandLoadToTceStart)problemTaskViewModel.LoadToTceStartCommand).ExecuteWithoutConfirmation();
                }
            }

            return true;
        }

        private bool IsTceConnected(PriceEngineeringTasks priceEngineeringTasks)
        {
            var result = priceEngineeringTasks.ChildPriceEngineeringTasks
                                     .Any(task => task.IsAcceptedTotal) &&
                                 priceEngineeringTasks.ChildPriceEngineeringTasks
                                     .All(task => task.IsAcceptedTotal || task.IsStoppedTotal);
            if (result == false)
            {
                if (_tasksViewModel.TasksWrapper.ChildTasks.Any(task => task.Model.IsAcceptedTotal))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}