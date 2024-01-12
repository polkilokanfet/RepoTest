using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public abstract class TaskViewModelManager : TaskViewModelBaseManager
    {
        public override bool IsTarget => true;

        public override bool IsEditMode
        {
            get
            {
                var statuses = new []
                {
                    ScriptStep.Create,
                    ScriptStep.Stop,
                    ScriptStep.RejectByHead,
                    ScriptStep.RejectByConstructor
                };

                return statuses.Contains(Status);
            }
        }

        #region Commands

        public DelegateLogCommand AddTechnicalRequirementsFilesCommand { get; private set; }
        public DelegateLogConfirmationCommand RemoveTechnicalRequirementsFilesCommand { get; private set; }

        #endregion

        #region Commands

        /// <summary>
        /// Принять техническую проработку задачи
        /// </summary>
        public virtual ICommandIsVisibleWhenCanExecute AcceptCommand { get; } = new DelegateLogCommand(() => { }, () => false);

        /// <summary>
        /// Отклонить техническую проработку задачи
        /// </summary>
        public virtual ICommandIsVisibleWhenCanExecute RejectCommand { get; } = new DelegateLogCommand(() => { }, () => false);

        /// <summary>
        /// Загрузить техническую проработку задачи в ТСЕ
        /// </summary>
        public virtual ICommandIsVisibleWhenCanExecute LoadToTceStartCommand { get; } = new DelegateLogCommand(() => { }, () => false);

        /// <summary>
        /// Запросить открытие производства
        /// </summary>
        public virtual ICommandIsVisibleWhenCanExecute StartProductionCommand { get; } = new DelegateLogCommand(() => { }, () => false);

        /// <summary>
        /// Запросить остановку производства
        /// </summary>
        public virtual ICommandIsVisibleWhenCanExecute StopProductionRequestCommand { get; } = new DelegateLogCommand(() => { }, () => false);

        /// <summary>
        /// Замена продукта в SalesUnit на продукты из ТСП
        /// </summary>
        public virtual DelegateLogConfirmationCommand ReplaceProductCommand { get; } = new DelegateLogConfirmationCommand(null, String.Empty,() => { }, () => false);

        /// <summary>
        /// Задача на формирование счёта
        /// </summary>
        public DelegateLogConfirmationCommand MakeInvoiceForPaymentTaskCommand { get; } = new DelegateLogConfirmationCommand(null, String.Empty, () => { }, () => false);


        #endregion

        #region ctors

        /// <summary>
        /// Для загрузки (редактирования) созданной задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="priceEngineeringTaskId"></param>
        protected TaskViewModelManager(IUnityContainer container, Guid priceEngineeringTaskId) : base(container, priceEngineeringTaskId) { }

        /// <summary>
        /// Для создания новой задачи
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        protected TaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork) : base(container, unitOfWork) { }
        
        protected override void InCtor()
        {
            base.InCtor();
            
            #region Commands

            var messageService = this.Container.Resolve<IMessageService>();

            AddTechnicalRequirementsFilesCommand = new DelegateLogCommand(
                () =>
                {
                    var fileNames = Container.Resolve<IGetFilePaths>().GetFilePaths().ToList();
                    if (fileNames.Any() == false) return;

                    //копируем каждый файл
                    foreach (var fileName in fileNames)
                    {
                        var fileWrapper = new PriceEngineeringTaskFileTechnicalRequirementsWrapper(new PriceEngineeringTaskFileTechnicalRequirements())
                        {
                            Name = Path.GetFileNameWithoutExtension(fileName).LimitLength(200),
                            Path = fileName
                        };
                        this.FilesTechnicalRequirements.Add(fileWrapper);
                    }
                }, 
                () => IsEditMode);

            RemoveTechnicalRequirementsFilesCommand = new DelegateLogConfirmationCommand(
                messageService,
                "Вы уверены, что хотите удалить выделенное техническое задание?",
                () => { this.FilesTechnicalRequirements.Remove(SelectedTechnicalRequrementsFile); },
                () => IsEditMode && this.SelectedTechnicalRequrementsFile != null);

            #endregion

            this.SelectedTechnicalRequrementsFileIsChanged += () =>
            {
                RemoveTechnicalRequirementsFilesCommand.RaiseCanExecuteChanged();
            };
        }

        #endregion

        /// <summary>
        /// Задача создана только для запуска производства из ТСЕ
        /// </summary>
        public bool IsJustForProduction
        {
            get
            {
                var statusesArray = Model.Statuses.OrderBy(x => x.Moment).ToArray();
                if (statusesArray.Length < 3) return false;
                if (statusesArray[0].StatusEnum != 0) return false;
                if (statusesArray[1].StatusEnum != 1) return false;
                if (statusesArray[2].StatusEnum != 13) return false;
                return true;
            }
        }
    }
}