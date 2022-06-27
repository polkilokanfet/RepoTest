using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceEngineering.Comparers;
using HVTApp.UI.PriceEngineering.Tce.Second.View;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksViewModelManager : PriceEngineeringTasksViewModel
    {
        private bool _isNew = false;
        public override bool IsNew
        {
            get => _isNew;
            protected set
            {
                _isNew = value;
                RaisePropertyChanged();
            }
        }

        public PriceCalculationWrapper SelectedCalculation { get; set; }

        #region Commands

        public DelegateLogCommand AddFileTechnicalRequirementsCommand { get; }
        public DelegateLogConfirmationCommand RemoveFileTechnicalRequirementsCommand { get; }
        public DelegateLogCommand RemoveTaskCommand { get; }
        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogConfirmationCommand StartCommand { get; }
        public DelegateLogConfirmationCommand StopCommand { get; }
        public DelegateLogCommand OpenPriceCalculationCommand { get; }
        public DelegateLogCommand CreatePriceCalculationCommand { get; }
        public DelegateLogCommand OpenTceCommand { get; }
        public DelegateLogCommand ReplaceProductsCommand { get; }

        #endregion

        /// <summary>
        /// Можно ли корректировать свойства (дату проработки, комментарий и т.д.)
        /// </summary>
        public override bool AllowEditProps
        {
            get
            {
                if (GlobalAppProperties.User.RoleCurrent != Role.SalesManager) return false;

                if (this.PriceEngineeringTasksWrapper == null) return false;

                return PriceEngineeringTasksWrapper.Model.StatusesAll.All(x => x == PriceEngineeringTaskStatusEnum.Created) ||
                       PriceEngineeringTasksWrapper.Model.StatusesAll.All(x => x == PriceEngineeringTaskStatusEnum.Stopped);
            }
        }

        public PriceEngineeringTasksViewModelManager(IUnityContainer container) : base(container)
        {
            #region Commands

            AddFileTechnicalRequirementsCommand = new DelegateLogCommand(
                () =>
                {
                    var openFileDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        RestoreDirectory = true
                    };

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //копируем каждый файл
                        foreach (var fileName in openFileDialog.FileNames)
                        {
                            var fileWrapper = new PriceEngineeringTasksFileTechnicalRequirementsWrapper(new PriceEngineeringTasksFileTechnicalRequirements())
                            {
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50),
                                Path = fileName
                            };
                            this.PriceEngineeringTasksWrapper.FilesTechnicalRequirements.Add(fileWrapper);
                        }

                        //RaisePropertyChanged(nameof(this.PriceEngineeringTasksWrapper.FilesTechnicalRequirements));
                    }
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
                        this.PriceEngineeringTasksWrapper.FilesTechnicalRequirements.Remove(SelectedFileTechnicalRequirements);
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
                        container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при открытии файла ТЗ", e.PrintAllExceptions());
                    }
                });

            RemoveTaskCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите удалить выделенную задачу?",
                () =>
                {
                    this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.Remove(SelectedPriceEngineeringTaskViewModel);
                },
                () =>
                    SelectedPriceEngineeringTaskViewModel != null &&
                    AllowEditProps &&
                    UnitOfWork.Repository<PriceEngineeringTask>().GetById(SelectedPriceEngineeringTaskViewModel.Id) == null);

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если задание новое - добавляем его в базу
                        if (UnitOfWork.Repository<PriceEngineeringTasks>().GetById(this.PriceEngineeringTasksWrapper.Id) == null)
                        {
                            UnitOfWork.Repository<PriceEngineeringTasks>().Add(this.PriceEngineeringTasksWrapper.Model);
                        }

                        //загрузка файлов в хранилище
                        foreach (var priceEngineeringTaskViewModel in this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks)
                        {
                            priceEngineeringTaskViewModel.LoadNewTechnicalRequirementFilesInStorage();
                        }

                        this.PriceEngineeringTasksWrapper.AcceptChanges();
                        UnitOfWork.SaveChanges();
                        IsNew = false;
                        Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Publish(this.PriceEngineeringTasksWrapper.Model);
                        Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTasksStartedEvent>().Publish(this.PriceEngineeringTasksWrapper.Model);
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при сохранении", e.PrintAllExceptions());
                    }
                },
                () => this.PriceEngineeringTasksWrapper != null && this.PriceEngineeringTasksWrapper.IsValid && this.PriceEngineeringTasksWrapper.IsChanged);

            StartCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(), 
                "Вы уверены, что хотите стартовать все задачи?",
                () =>
                {
                    LoadNewTechnicalRequirementFilesInStorage();
                    foreach (var priceEngineeringTaskViewModel in PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks)
                    {
                        priceEngineeringTaskViewModel.StartCommandExecute(false);
                    }
                    SaveCommand.Execute();
                    StartCommand.RaiseCanExecuteChanged();
                    RemoveTaskCommand.RaiseCanExecuteChanged();
                    AddFileTechnicalRequirementsCommand.RaiseCanExecuteChanged();
                    RemoveFileTechnicalRequirementsCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(nameof(AllowEditProps));
                },
                () => this.PriceEngineeringTasksWrapper != null &&
                      this.PriceEngineeringTasksWrapper.IsValid &&
                      this.PriceEngineeringTasksWrapper.IsChanged &&
                      AllowEditProps);

            StopCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите остановить все задачи?",
                () =>
                {
                    foreach (var priceEngineeringTaskViewModel in this.PriceEngineeringTasksWrapper
                        .ChildPriceEngineeringTasks)
                    {
                        foreach (var viewModel in priceEngineeringTaskViewModel.GetAllPriceEngineeringTaskViewModels())
                        {
                            if (viewModel.Model.Status == PriceEngineeringTaskStatusEnum.Stopped)
                            {
                                continue;
                            }

                            if (viewModel is PriceEngineeringTaskViewModelManager priceEngineeringTaskViewModelManager)
                            {
                                priceEngineeringTaskViewModelManager.StopCommand.ExecuteWithoutConfirmation();
                            }
                        }
                    }

                    StopCommand.RaiseCanExecuteChanged();
                },
                () => this.PriceEngineeringTasksWrapper != null && this.IsNew == false);
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

            CreatePriceCalculationCommand = new DelegateLogCommand(
                () =>
                {
                    var isTceConnected = this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks
                                             .Any(x => x.Model.IsTotalAccepted) &&
                                         this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks
                                             .All(x => x.Model.IsTotalAccepted || x.Model.IsTotalStopped);
                    if (isTceConnected == false)
                    {
                        var dr = container.Resolve<IMessageService>().ShowYesNoMessageDialog("Не все задачи приняты (из неостановленных).\nХотите ли Вы создать расчёт ПЗ по аналогам?");
                        if (dr == MessageDialogResult.Yes)
                        {
                            isTceConnected = false;
                        }
                        else if (dr == MessageDialogResult.No && this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.Any(x => x.Model.IsTotalAccepted))
                        {
                            isTceConnected = true;
                        }
                        else
                        {
                            return;
                        }
                    }

                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(
                        new NavigationParameters
                        {
                            {nameof(PriceEngineeringTasks), this.PriceEngineeringTasksWrapper.Model},
                            {nameof(Boolean), isTceConnected}
                        });
                },
                () => this.PriceEngineeringTasksWrapper != null && this.IsNew == false);


            OpenTceCommand = new DelegateLogCommand(
                () =>
                {
                    this.Container.Resolve<IRegionManager>().RequestNavigateContentRegion<TasksTceView>(new NavigationParameters
                    {
                        {nameof(PriceEngineeringTasks), this.PriceEngineeringTasksWrapper.Model}
                    });
                });

            ReplaceProductsCommand = new DelegateLogCommand(
                () =>
                {
                    foreach (var priceEngineeringTaskViewModel in this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.Where(x => x.Model.IsTotalAccepted))
                    {
                        if (priceEngineeringTaskViewModel is PriceEngineeringTaskViewModelManager viewModel)
                        {
                            viewModel.ReplaceProductCommand.Execute();
                        }
                    }
                },
                () => this.PriceEngineeringTasksWrapper != null);

            #endregion

            this.PriceEngineeringTasksWrapperChanged += (valueOld, valueNew) =>
            {
                if (valueOld != null)
                {
                    valueOld.PropertyChanged -= PriceEngineeringTasksWrapperOnPropertyChanged;
                }

                if (valueNew != null)
                {
                    valueNew.PropertyChanged += PriceEngineeringTasksWrapperOnPropertyChanged;
                }

                RaisePropertyChanged(nameof(AllowEditProps));
                StopCommand.RaiseCanExecuteChanged();
            };

            this.SelectedPriceEngineeringTaskViewModelChanged += () =>
            {
                RemoveTaskCommand.RaiseCanExecuteChanged();
            };

            this.SelectedFileTechnicalRequirementsChanged += () =>
            {
                RemoveFileTechnicalRequirementsCommand.RaiseCanExecuteChanged();
            };

            container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedTotalEvent>().Subscribe(OnPriceEngineeringTaskAccepted);
        }

        /// <summary>
        /// Загрузка при создании новой технико-стоимостной проработки по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var vms = salesUnits
                .Select(salesUnit => UnitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                .GroupBy(salesUnit => salesUnit, new SalesUnitForPriceEngineeringTaskComparer())
                .Select(x => new PriceEngineeringTaskViewModelManager(Container, UnitOfWork, x));

            PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(vms)
            {
                UserManager = new UserEmptyWrapper(UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id))
            };

            IsNew = true;
        }

        /// <summary>
        /// Загрузить все добавленные файлы ТЗ в хранилище
        /// </summary>
        private void LoadNewTechnicalRequirementFilesInStorage()
        {
            foreach (var fileWrapper in this.PriceEngineeringTasksWrapper.FilesTechnicalRequirements.AddedItems)
            {
                var destFileName = $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}";
                if (File.Exists(destFileName) == false && string.IsNullOrEmpty(fileWrapper.Path) == false)
                {
                    File.Copy(fileWrapper.Path, destFileName);
                    fileWrapper.Path = null;
                }
            }
        }

        #region PriceEngineeringTasksWrapperOn

        private void OnPriceEngineeringTaskAccepted(PriceEngineeringTask priceEngineeringTask)
        {
            if (this.PriceEngineeringTasksWrapper == null)
                return;
            if (this.PriceEngineeringTasksWrapper.Model.ChildPriceEngineeringTasks.Any(x => x.Id == priceEngineeringTask.Id) == false)
                return;

            //если приняты все задачи
            if (this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.All(x => x.Model.IsTotalAccepted || x.Model.IsTotalStopped))
            {
                var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Вы приняли все задания. Хотите ли Вы загрузить результаты в ТСЕ и создать расчёт ПЗ?");
                if (dr == MessageDialogResult.Yes)
                {
                    CreatePriceCalculationCommand.Execute();
                }
            }
        }

        private void PriceEngineeringTasksWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
            ReplaceProductsCommand.RaiseCanExecuteChanged();
            CreatePriceCalculationCommand.RaiseCanExecuteChanged();
        }

        #endregion

        public override void Dispose()
        {
            base.Dispose();
            Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedTotalEvent>().Unsubscribe(OnPriceEngineeringTaskAccepted);
        }
    }
}