using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using HVTApp.DataAccess;
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

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTasksViewModel : IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private PriceEngineeringTaskViewModel _selectedPriceEngineeringTaskViewModel;
        
        public PriceEngineeringTasksWrapper1 PriceEngineeringTasksWrapper { get; private set; }

        public PriceEngineeringTaskViewModel SelectedPriceEngineeringTaskViewModel
        {
            get => _selectedPriceEngineeringTaskViewModel;
            set => _selectedPriceEngineeringTaskViewModel = value;
        }

        #region Commands

        public DelegateLogCommand SaveCommand { get; }

        #endregion

        /// <summary>
        /// Вернуть все добавленные файлы ТЗ
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PriceEngineeringTaskFileTechnicalRequirementsWrapper> GetAllNewFilesTechnicalRequirements()
        {
            foreach (var childPriceEngineeringTask in PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks)
            {
                foreach (var fileWrapper in childPriceEngineeringTask.GetAllNewFilesTechnicalRequirements())
                {
                    yield return fileWrapper;
                }
            }
        }

        public PriceEngineeringTasksViewModel(IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _container = container;
            _unitOfWork = unitOfWork;

            PriceEngineeringTasksWrapper = new PriceEngineeringTasksWrapper1(new PriceEngineeringTasks(), container, unitOfWork);

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
                        foreach (var fileWrapper in GetAllNewFilesTechnicalRequirements().Distinct())
                        {
                            File.Copy(fileWrapper.Path, $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}");
                        }
                        //foreach (var priceEngineeringTask in PriceEngineeringTasksWrapper.ChildPriceEngineeringTasks)
                        //{
                        //    //файлы ТЗ
                        //    foreach (var fileWrapper in priceEngineeringTask.FilesTechnicalRequirements.AddedItems)
                        //    {
                        //    }

                        //    //файлы ответы ОГК
                        //    foreach (var fileWrapper in priceEngineeringTask.FilesAnswers.AddedItems)
                        //    {
                        //        File.Copy(fileWrapper.Path, $"{GlobalAppProperties.Actual.TechnicalRequrementsFilesAnswersPath}\\{fileWrapper.Id}{Path.GetExtension(fileWrapper.Path)}");
                        //    }
                        //}

                        this.PriceEngineeringTasksWrapper.AcceptChanges();
                        _unitOfWork.SaveChanges();
                        _container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTasksEvent>().Publish(this.PriceEngineeringTasksWrapper.Model);
                    }
                    catch (Exception e)
                    {
                        _container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при сохранении", e.PrintAllExceptions());
                    }
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