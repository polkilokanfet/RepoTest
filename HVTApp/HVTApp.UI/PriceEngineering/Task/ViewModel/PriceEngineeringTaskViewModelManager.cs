using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelManager : PriceEngineeringTaskViewModel
    {
        public override bool IsExpanded => true;

        public override bool IsExpendedChildPriceEngineeringTasks => true;

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

        public DelegateLogCommand SelectDesignDepartmentCommand { get; private set; }
        
        public DelegateLogCommand AddTechnicalRequrementsFilesCommand { get; private set; }
        public DelegateLogCommand RemoveTechnicalRequrementsFilesCommand { get; private set; }

        public DelegateLogCommand AcceptCommand { get; private set; }
        public DelegateLogCommand RejectCommand { get; private set; }
        public DelegateLogCommand StopCommand { get; private set; }

        #region ctors

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
        }

        public PriceEngineeringTaskViewModelManager(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(container, unitOfWork, priceEngineeringTask)
        {
        }
        
        #endregion

        protected override void InCtor()
        {
            base.InCtor();

            SelectDesignDepartmentCommand = new DelegateLogCommand(
                () =>
                {
                    var departments = UnitOfWork.Repository<DesignDepartment>().Find(designDepartment => designDepartment.ProductBlockIsSuitable(this.Model.ProductBlockEngineer));
                    var department = Container.Resolve<ISelectService>().SelectItem(departments);
                    if (department != null)
                    {
                        this.DesignDepartment = new DesignDepartmentEmptyWrapper(UnitOfWork.Repository<DesignDepartment>().GetById(department.Id));
                    }
                },
                () => IsEditMode);

            AddTechnicalRequrementsFilesCommand = new DelegateLogCommand(
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
                                Name = Path.GetFileNameWithoutExtension(fileName).LimitLengh(50),
                                Path = fileName
                            };
                            this.FilesTechnicalRequirements.Add(fileWrapper);
                        }
                    }
                }, 
                () => IsEditMode);

            RemoveTechnicalRequrementsFilesCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

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

            AcceptCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Принять проработку", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

                    this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus() { StatusEnum = PriceEngineeringTaskStatusEnum.Accepted }));
                    this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "Проработка принята!"
                    }));
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(this.Model);
                },
                () => this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor && this.IsValid);

            RejectCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Отклонить проработку", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

                    this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus() { StatusEnum = PriceEngineeringTaskStatusEnum.RejectedByManager }));
                    this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "Проработка отклонена."
                    }));
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(this.Model);
                },
                () => this.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructor && this.IsValid);

            StopCommand = new DelegateLogCommand(
                () =>
                {
                    if (Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Остановить проработку", "Вы уверены?", defaultNo: true) != MessageDialogResult.Yes)
                        return;

                    this.Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus() { StatusEnum = PriceEngineeringTaskStatusEnum.Stopped }));
                    this.Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "Задача остановлена."
                    }));
                    SaveCommand.Execute();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(this.Model);
                },
                () => this.Status != PriceEngineeringTaskStatusEnum.Created && this.Status != PriceEngineeringTaskStatusEnum.Stopped && this.IsValid);

            this.SelectedTechnicalRequrementsFileIsChanged += () =>
            {
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
            };

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                SelectDesignDepartmentCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();
                StopCommand.RaiseCanExecuteChanged();
                AddTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
                RemoveTechnicalRequrementsFilesCommand.RaiseCanExecuteChanged();
                AcceptCommand.RaiseCanExecuteChanged();
                RejectCommand.RaiseCanExecuteChanged();
            };
        }
    }
}