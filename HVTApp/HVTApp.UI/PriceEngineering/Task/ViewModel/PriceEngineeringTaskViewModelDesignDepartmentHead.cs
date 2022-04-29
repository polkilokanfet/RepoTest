using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
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
    public class PriceEngineeringTaskViewModelDesignDepartmentHead : PriceEngineeringTaskViewModel
    {
        /// <summary>
        /// �������� ���������� ������
        /// </summary>
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// ������� ����������
        /// </summary>
        public DelegateLogCommand AcceptPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// ��������� ����������
        /// </summary>
        public DelegateLogCommand RejectPriceEngineeringTaskCommand { get; private set; }


        #region ctors

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask) : base(container, unitOfWork, priceEngineeringTask)
        {
        }

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits) : base(container, unitOfWork, salesUnits)
        {
        }

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, IUnitOfWork unitOfWork, Product product) : base(container, unitOfWork, product)
        {
        }
        
        #endregion

        public override bool IsExpanded => this.Model.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any();

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

                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "������ ��������� ���������� ����������?", defaultNo:true);
                    this.RequestForVerificationFromHead = dr == MessageDialogResult.Yes;

                    string s = RequestForVerificationFromHead
                        ? ". ���������� ��������� ��������� ����������."
                        : string.Empty;
                        
                    this.UserConstructor = new UserEmptyWrapper(user);
                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id), 
                        Message = $"�������� �����������: {user}{s}"
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
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "�� �������, ��� ������ ������� ���������� ����������?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "���������� ���������� �������. ���������� ���������."
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
                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("����������", "�� �������, ��� ������ ��������� ������ �� ��������� �����������?", defaultYes: true);
                    if (dr != MessageDialogResult.Yes) return;

                    Messages.Add(new PriceEngineeringTaskMessageWrapper(new PriceEngineeringTaskMessage()
                    {
                        Author = UnitOfWork.Repository<User>().GetById(GlobalAppProperties.User.Id),
                        Message = "���������� ���������� �� �������. ���������� �� ��������� �����������."
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