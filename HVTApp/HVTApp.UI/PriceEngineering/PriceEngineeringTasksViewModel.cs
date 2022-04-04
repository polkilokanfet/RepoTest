using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Comparers;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksViewModel : BindableBase, IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private PriceEngineeringTaskViewModel _selectedPriceEngineeringTaskViewModel;
        private PriceEngineeringTasksWrapper1 _priceEngineeringTasksWrapper;

        public PriceEngineeringTasksWrapper1 PriceEngineeringTasksWrapper
        {
            get => _priceEngineeringTasksWrapper;
            private set
            {
                if (Equals(_priceEngineeringTasksWrapper, value)) return;

                if (_priceEngineeringTasksWrapper != null)
                    _priceEngineeringTasksWrapper.PropertyChanged -= PriceEngineeringTasksWrapperOnPropertyChanged;

                _priceEngineeringTasksWrapper = value;

                if (_priceEngineeringTasksWrapper != null)
                    _priceEngineeringTasksWrapper.PropertyChanged += PriceEngineeringTasksWrapperOnPropertyChanged;
            }
        }

        private void PriceEngineeringTasksWrapperOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
            StartCommand.RaiseCanExecuteChanged();
        }

        public PriceEngineeringTaskViewModel SelectedPriceEngineeringTaskViewModel
        {
            get => _selectedPriceEngineeringTaskViewModel;
            set => _selectedPriceEngineeringTaskViewModel = value;
        }

        #region Commands

        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogCommand StartCommand { get; }

        #endregion

        public PriceEngineeringTasksViewModel(IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _container = container;
            _unitOfWork = unitOfWork;

            PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(new PriceEngineeringTasks(), container, unitOfWork)
            {
                UserManager = new UserEmptyWrapper(_unitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id))
            };

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    try
                    {
                        //если задание новое - добавл€ем его в базу
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
                    }
                    catch (Exception e)
                    {
                        _container.Resolve<IMessageService>().ShowOkMessageDialog("ќшибка при сохранении", e.PrintAllExceptions());
                    }
                },
                () => this.PriceEngineeringTasksWrapper != null && this.PriceEngineeringTasksWrapper.IsValid && this.PriceEngineeringTasksWrapper.IsChanged);

            StartCommand = new DelegateLogCommand(
                () =>
                {
                    foreach (var priceEngineeringTaskViewModel in PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks)
                    {
                        priceEngineeringTaskViewModel.StartCommandExecute(false);
                    }
                    SaveCommand.Execute();
                },
                () =>
                {
                    return this.PriceEngineeringTasksWrapper != null && 
                           this.PriceEngineeringTasksWrapper.IsValid &&
                           this.PriceEngineeringTasksWrapper.IsChanged;
                });

        }

        /// <summary>
        /// «агрузка при создании новой технико-стоимостной проработки по единицам продаж
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
            this.PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(priceEngineeringTasks, _container, _unitOfWork);
        }

        public void Load(PriceEngineeringTask priceEngineeringTask)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
            EnumerableExtensions.ForEach(this.PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks, x => x.Dispose());
        }
    }
}