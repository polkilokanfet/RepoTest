using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelDesignDepartmentHead : PriceEngineeringTaskViewModel
    {
        /// <summary>
        /// Поручить проработку задачи
        /// </summary>
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// Принять проработку
        /// </summary>
        public DelegateLogCommand AcceptPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// Отклонить проработку
        /// </summary>
        public DelegateLogCommand RejectPriceEngineeringTaskCommand { get; private set; }


        #region ctors

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, PriceEngineeringTask priceEngineeringTask) : base(container, priceEngineeringTask)
        {
            var vms = Model.ChildPriceEngineeringTasks.Select(x => new PriceEngineeringTaskViewModelDesignDepartmentHead(container, x));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(vms);
        }

        #endregion

        public override bool IsExpanded => this.Model.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any();

        public override bool IsExpendedChildPriceEngineeringTasks
        {
            get
            {
                var priceEngineeringTasks = this.Model.GetSuitableTasksForInstruct(GlobalAppProperties.User).ToList();
                priceEngineeringTasks.RemoveIfContainsById(this.Model);
                return priceEngineeringTasks.Any();
            }
        }


        public override bool IsTarget => DesignDepartment != null && DesignDepartment.Model.Head.Id == GlobalAppProperties.User.Id;

        public override bool IsEditMode
        {
            get
            {
                if (IsTarget == false) return false;

                switch (Status)
                {
                    case PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification:
                        return true;
                }

                return false;
            }
        }

        protected override void InCtor()
        {
            base.InCtor();

            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var user = Container.Resolve<ISelectService>().SelectItem(DesignDepartment.Model.Staff);

                    if (user == null) return;

                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Проверка", "Хотите проверить результаты проработки?", defaultNo:true);
                    this.RequestForVerificationFromHead = dr == MessageDialogResult.Yes;

                    string s = RequestForVerificationFromHead
                        ? ". Необходимо проверить результат проработки."
                        : string.Empty;
                        
                    this.UserConstructor = new UserEmptyWrapper(user);
                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id), 
                        Message = $"Назначен исполнитель: {user}{s}"
                    }));
                    
                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskInstructedEvent>().Publish(this.Model);
                }, 
                () => 
                    IsTarget &&
                    Status != PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification &&
                    Status != PriceEngineeringTaskStatusEnum.Created &&
                    Status != PriceEngineeringTaskStatusEnum.Stopped &&
                    Status != PriceEngineeringTaskStatusEnum.Accepted);

            AcceptPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Проработка", "Вы уверены, что хотите принять результаты проработки?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "Результаты проработки приняты. Проработка завершена."
                    }));

                    //Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus { StatusEnum = PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead }));
                    Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus { StatusEnum = PriceEngineeringTaskStatusEnum.FinishedByConstructor }));

                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(this.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(this.Model);
                },
                () => IsEditMode);

            RejectPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("Проработка", "Вы уверены, что хотите отправить задачу на доработку исполнителю?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "Результаты проработки не приняты. Отправлено на доработку исполнителю."
                    }));

                    Statuses.Add(new PriceEngineeringTaskStatusWrapper(new PriceEngineeringTaskStatus() { StatusEnum = PriceEngineeringTaskStatusEnum.VerificationRejectededByHead }));

                    this.AcceptChanges();
                    UnitOfWork.SaveChanges();
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceEngineeringTaskEvent>().Publish(this.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskVerificationRejectedByHeadEvent>().Publish(this.Model);
                },
                () => IsEditMode);

            this.Statuses.CollectionChanged += (sender, args) =>
            {
                InstructPriceEngineeringTaskCommand.RaiseCanExecuteChanged();
                AcceptPriceEngineeringTaskCommand.RaiseCanExecuteChanged();
                RejectPriceEngineeringTaskCommand.RaiseCanExecuteChanged();
            };

        }
    }
}