using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskWithStartCommandViewModel
    {
        public override bool IsTarget => true;

        public override bool IsEditMode
        {
            get
            {
                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.Created:
                    case PriceEngineeringTaskStatusEnum.Stopped:
                    case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                        return true;
                }

                return false;
            }
        }

        #region Commands

        public DelegateLogCommand AddTechnicalRequirementsFilesCommand { get; private set; }
        public DelegateLogConfirmationCommand RemoveTechnicalRequirementsFilesCommand { get; private set; }

        #endregion

        #region ctors

        /// <summary>
        /// Для загрузки (редактирования) созданной задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected PriceEngineeringTaskViewModelManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId) { }

        /// <summary>
        /// Для создания новой задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        protected PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork) { }
        
        protected override void InCtor()
        {
            base.InCtor();
            
            #region Commands

            var messageService = this.Container.Resolve<IMessageService>();

            AddTechnicalRequirementsFilesCommand = new DelegateLogCommand(
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
                            var fileWrapper = new PriceEngineeringTaskFileTechnicalRequirementsWrapper(new PriceEngineeringTaskFileTechnicalRequirements())
                            {
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLength(50),
                                Path = fileName
                            };
                            this.FilesTechnicalRequirements.Add(fileWrapper);
                        }
                    }
                }, 
                () => IsEditMode);

            RemoveTechnicalRequirementsFilesCommand = new DelegateLogConfirmationCommand(
                messageService,
                "Вы уверены, что хотите удалить выделенное техническое задание?",
                () =>
                {
                    if (string.IsNullOrEmpty(SelectedTechnicalRequrementsFile.Path))
                    {
                        SelectedTechnicalRequrementsFile.IsActual = false;
                    }
                    else
                    {
                        this.FilesTechnicalRequirements.Remove(SelectedTechnicalRequrementsFile);
                    }
                },
                () => IsEditMode && this.SelectedTechnicalRequrementsFile != null);

            #endregion

            this.SelectedTechnicalRequrementsFileIsChanged += () =>
            {
                RemoveTechnicalRequirementsFilesCommand.RaiseCanExecuteChanged();
            };

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                StartCommand.RaiseCanExecuteChanged();
                AddTechnicalRequirementsFilesCommand.RaiseCanExecuteChanged();
                RemoveTechnicalRequirementsFilesCommand.RaiseCanExecuteChanged();
            };
        }

        #endregion

        protected override void SaveCommand_ExecuteMethod()
        {
            LoadNewTechnicalRequirementFilesInStorage();
            base.SaveCommand_ExecuteMethod();
        }

        /// <summary>
        /// Загрузить все добавленные файлы ТЗ в хранилище
        /// </summary>
        public void LoadNewTechnicalRequirementFilesInStorage()
        {
            //новые файлы ТЗ, которые нужно загрузить (в них пути к файлу не пустые)
            var filesToLoad = 
                this.FilesTechnicalRequirements.AddedItems
                    .Where(x => string.IsNullOrWhiteSpace(x.Path) == false);

            foreach (var file in filesToLoad)
            {
                file.LoadToStorage(GlobalAppProperties.Actual.TechnicalRequrementsFilesPath);
            }

            foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
            {
                if (childPriceEngineeringTask is PriceEngineeringTaskViewModelManager vm)
                    vm.LoadNewTechnicalRequirementFilesInStorage();
            }
        }
    }
}