using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.Messages;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelDesignDepartmentHead : PriceEngineeringTaskViewModel
    {
        #region Commands

        /// <summary>
        /// �������� ���������� ������
        /// </summary>
        public DelegateLogCommand InstructPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// ������� ����������
        /// </summary>
        public DelegateLogConfirmationCommand AcceptPriceEngineeringTaskCommand { get; private set; }

        /// <summary>
        /// ��������� ����������
        /// </summary>
        public DelegateLogConfirmationCommand RejectPriceEngineeringTaskCommand { get; private set; }

        #endregion

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

        public bool AllowInstruction =>
            IsTarget &&
            Status != PriceEngineeringTaskStatusEnum.FinishedByConstructor &&
            Status != PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification &&
            Status != PriceEngineeringTaskStatusEnum.Created &&
            Status != PriceEngineeringTaskStatusEnum.Stopped &&
            Status != PriceEngineeringTaskStatusEnum.Accepted;

        #region ctors

        public PriceEngineeringTaskViewModelDesignDepartmentHead(IUnityContainer container, Guid priceEngineeringTaskId) 
            : base(container, priceEngineeringTaskId)
        {
            //��������� �������� ������
            var vms = Model.ChildPriceEngineeringTasks.Select(engineeringTask => new PriceEngineeringTaskViewModelDesignDepartmentHead(container, engineeringTask.Id));
            ChildPriceEngineeringTasks = new ValidatableChangeTrackingCollection<PriceEngineeringTaskViewModel>(vms);

            InstructPriceEngineeringTaskCommand = new DelegateLogCommand(
                () =>
                {
                    var user = Container.Resolve<ISelectService>().SelectItem(DesignDepartment.Model.Staff);
                    if (user == null) return;

                    var dr = Container.Resolve<IMessageService>().ShowYesNoMessageDialog("��������", "������ ��������� ���������� ����������?", defaultNo: true);
                    this.RequestForVerificationFromHead = dr == MessageDialogResult.Yes;

                    string s = RequestForVerificationFromHead
                        ? ". ������ ����������� ��������� ��������� ������ �����������."
                        : string.Empty;

                    this.UserConstructor = new UserEmptyWrapper(user);
                    Messenger.SendMessage($"�������� �����������: {user}{s}");
                    this.SaveCommand_ExecuteMethod();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskInstructedEvent>().Publish(this.Model);
                },
                () => AllowInstruction);

            AcceptPriceEngineeringTaskCommand = new DelegateLogConfirmationCommand(
                Container.Resolve<IMessageService>(),
                "�� �������, ��� ������ ������� ���������� ����������?",
                () =>
                {
                    SetStatus(PriceEngineeringTaskStatusEnum.FinishedByConstructor, "���������� ���������� �������. ���������� ���������.");
                    this.SaveCommand_ExecuteMethod();
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(this.Model);
                    Container.Resolve<IEventAggregator>().GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(this.Model);
                },
                () => IsEditMode);

            RejectPriceEngineeringTaskCommand = new DelegateLogConfirmationCommand(
                Container.Resolve<IMessageService>(),
                "�� �������, ��� ������ ��������� ������ �� ��������� �����������?",
                () =>
                {
                    SetStatus(PriceEngineeringTaskStatusEnum.VerificationRejectededByHead, "���������� ���������� �� �������. ���������� �� ��������� �����������.");
                    this.SaveCommand_ExecuteMethod();
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

        #endregion
    }
}