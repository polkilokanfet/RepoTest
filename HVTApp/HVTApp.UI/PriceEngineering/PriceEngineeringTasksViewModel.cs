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
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksViewModel : ViewModelBase, IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private PriceEngineeringTaskViewModel _selectedPriceEngineeringTaskViewModel;
        private PriceEngineeringTasksWrapper1 _priceEngineeringTasksWrapper;
        private PriceEngineeringTasksFileTechnicalRequirementsWrapper _selectedFileTechnicalRequirements;

        public PriceEngineeringTasksWrapper1 PriceEngineeringTasksWrapper
        {
            get => _priceEngineeringTasksWrapper;
            private set
            {
                if (Equals(_priceEngineeringTasksWrapper, value)) return;

                if (_priceEngineeringTasksWrapper != null)
                {
                    _priceEngineeringTasksWrapper.PropertyChanged -= PriceEngineeringTasksWrapperOnPropertyChanged;
                    PriceEngineeringTasksWrapper.PriceEngineeringTaskSaved -= PriceEngineeringTasksWrapperOnPriceEngineeringTaskSaved;
                }

                _priceEngineeringTasksWrapper = value;

                if (_priceEngineeringTasksWrapper != null)
                {
                    _priceEngineeringTasksWrapper.PropertyChanged += PriceEngineeringTasksWrapperOnPropertyChanged;
                    PriceEngineeringTasksWrapper.PriceEngineeringTaskSaved += PriceEngineeringTasksWrapperOnPriceEngineeringTaskSaved;
                }

                RaisePropertyChanged(nameof(AllowEditProps));
            }
        }

        private void PriceEngineeringTasksWrapperOnPriceEngineeringTaskSaved(PriceEngineeringTask priceEngineeringTask)
        {
            _container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Publish(PriceEngineeringTasksWrapper.Model);
        }

        private void PriceEngineeringTasksWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
            StartCommand.RaiseCanExecuteChanged();
        }

        public PriceEngineeringTaskViewModel SelectedPriceEngineeringTaskViewModel
        {
            get => _selectedPriceEngineeringTaskViewModel;
            set
            {
                _selectedPriceEngineeringTaskViewModel = value;
                RemoveTaskCommand.RaiseCanExecuteChanged();
            }
        }

        public PriceEngineeringTasksFileTechnicalRequirementsWrapper SelectedFileTechnicalRequirements
        {
            get => _selectedFileTechnicalRequirements;
            set
            {
                _selectedFileTechnicalRequirements = value;
                RemoveFileTechnicalRequirementsCommand.RaiseCanExecuteChanged();
            }
        }

        public PriceCalculationWrapper SelectedCalculation { get; set; }

        #region Commands

        public DelegateLogCommand AddFileTechnicalRequirementsCommand { get; }
        public DelegateLogCommand RemoveFileTechnicalRequirementsCommand { get; }
        public DelegateLogCommand OpenFileTechnicalRequirementsCommand { get; }
        public DelegateLogCommand RemoveTaskCommand { get; }
        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogCommand StartCommand { get; }
        public DelegateLogCommand OpenPriceCalculationCommand { get; }
        public DelegateLogCommand CreatePriceCalculationCommand { get; }

        #endregion

        /// <summary>
        /// Можно ли корректировать свойства (дату проработки, комментарий и т.д.)
        /// </summary>
        public bool AllowEditProps
        {
            get
            {
                if (GlobalAppProperties.User.RoleCurrent != Role.SalesManager) return false;

                if (this.PriceEngineeringTasksWrapper == null) return false;

                return PriceEngineeringTasksWrapper.Model.StatusesAll.All(x => x == PriceEngineeringTaskStatusEnum.Created) || 
                       PriceEngineeringTasksWrapper.Model.StatusesAll.All(x => x == PriceEngineeringTaskStatusEnum.Stopped);
            }
        }

        public PriceEngineeringTasksViewModel(IUnityContainer container, IUnitOfWork unitOfWork) : base(container)
        {
            _container = container;
            _unitOfWork = unitOfWork;

            PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(new PriceEngineeringTasks(), container, unitOfWork)
            {
                UserManager = new UserEmptyWrapper(_unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id))
            };


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
                    }
                },
                () => AllowEditProps);

            RemoveFileTechnicalRequirementsCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

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

            RemoveTaskCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

                    this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.Remove(SelectedPriceEngineeringTaskViewModel);
                },
                () => 
                    SelectedPriceEngineeringTaskViewModel != null && 
                    AllowEditProps &&
                    unitOfWork.Repository<PriceEngineeringTask>().GetById(SelectedPriceEngineeringTaskViewModel.Id) == null);

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если задание новое - добавляем его в базу
                        if (_unitOfWork.Repository<PriceEngineeringTasks>().GetById(this.PriceEngineeringTasksWrapper.Id) == null)
                        {
                            _unitOfWork.Repository<PriceEngineeringTasks>().Add(this.PriceEngineeringTasksWrapper.Model);
                        }

                        //загрузка файлов в хранилище
                        foreach (var priceEngineeringTaskViewModel in this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks)
                        {
                            priceEngineeringTaskViewModel.LoadNewTechnicalRequirementFilesInStorage();
                        }
                        
                        this.PriceEngineeringTasksWrapper.AcceptChanges();
                        _unitOfWork.SaveChanges();
                        _container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Publish(this.PriceEngineeringTasksWrapper.Model);
                        _container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTasksStartedEvent>().Publish(this.PriceEngineeringTasksWrapper.Model);
                    }
                    catch (Exception e)
                    {
                        _container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при сохранении", e.PrintAllExceptions());
                    }
                },
                () => this.PriceEngineeringTasksWrapper != null && this.PriceEngineeringTasksWrapper.IsValid && this.PriceEngineeringTasksWrapper.IsChanged);

            StartCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Старт всех заданий", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

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
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(
                        new NavigationParameters
                        {
                            {nameof(PriceEngineeringTasks), this.PriceEngineeringTasksWrapper.Model}
                        });
                });
        }

        /// <summary>
        /// Загрузка при создании новой технико-стоимостной проработки по единицам продаж
        /// </summary>
        /// <param name="salesUnits"></param>
        public void Load(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitsGrouped = salesUnits
                .Select(salesUnit => _unitOfWork.Repository<SalesUnit>().GetById(salesUnit.Id))
                .GroupBy(salesUnit => salesUnit, new SalesUnitForPriceEngineeringTaskComparer());

            foreach (var salesUnitsGroup in salesUnitsGrouped)
            {
                PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks.Add(PriceEngineeringTaskViewModelFactory.GetInstance(_container, _unitOfWork, salesUnitsGroup));
            }
        }

        public void Load(PriceEngineeringTasks priceEngineeringTasks)
        {
            var tasks = _unitOfWork.Repository<PriceEngineeringTasks>().GetById(priceEngineeringTasks.Id);
            this.PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(tasks, _container, _unitOfWork);
        }

        public void Load(PriceEngineeringTask priceEngineeringTask)
        {
            this.Load(priceEngineeringTask.GetPriceEngineeringTasks(UnitOfWork));
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


        public void Dispose()
        {
            _unitOfWork?.Dispose();
            EnumerableExtensions.ForEach(this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks, x => x.Dispose());
        }
    }
}