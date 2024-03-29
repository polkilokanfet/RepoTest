using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Enums;

namespace HVTApp.Model.POCOs
{
    public class NotificationUnit : BaseEntity
    {
        public DateTime Moment { get; set; } = DateTime.Now;

        public NotificationActionType ActionType { get; set; }

        public Guid TargetEntityId { get; set; }

        #region Sender

        /// <summary>
        /// �����������
        /// </summary>
        [Required]
        public virtual User SenderUser { get; set; }

        public Guid SenderUserId { get; set; } = GlobalAppProperties.User.Id;

        /// <summary>
        /// ���� �����������
        /// </summary>
        public Role SenderRole { get; set; } = GlobalAppProperties.User.RoleCurrent;

        #endregion

        #region Recipient

        /// <summary>
        /// ����������
        /// </summary>
        [Required]
        public virtual User RecipientUser { get; set; }

        public Guid RecipientUserId { get; set; }

        /// <summary>
        /// ���� ����������
        /// </summary>
        public Role RecipientRole { get; set; }

        #endregion

        [Designation("���������� �� �����")]
        public bool IsSentByEmail { get; set; } = false;


        public string GetActionString()
        {
            switch (this.ActionType)
            {
                #region PriceEngineeringTasks

                case NotificationActionType.PriceEngineeringTasksStart:
                    return "�������� ������ ���";

                case NotificationActionType.PriceEngineeringTasksInstructToBackManager:
                    return this.RecipientRole == Role.BackManager
                        ? "��������� ���������� ���������� ��� � Team Center"
                        : "�������� �����������";

                #endregion

                #region PriceEngineeringTask

                case NotificationActionType.PriceEngineeringTaskStart:
                    return this.RecipientRole == Role.DesignDepartmentHead
                        ? "��������� �����������"
                        : "������������ ������";

                case NotificationActionType.PriceEngineeringTaskStop:
                    return "�������� ��������� ���������� ����� ���";

                case NotificationActionType.PriceEngineeringTaskInstructToConstructor:
                    return this.RecipientRole == Role.Constructor
                        ? "������������ ����"
                        : "�������� �����������";

                case NotificationActionType.PriceEngineeringTaskFinish:
                    return "���� ��� ����������";

                case NotificationActionType.PriceEngineeringTaskAccept:
                    return "���������� ����� ��� ������� ����������";

                case NotificationActionType.PriceEngineeringTaskRejectByManager:
                    return "���������� ����� ��� ��������� ���������� (���������� ���������)";

                case NotificationActionType.PriceEngineeringTaskRejectByHeadToManager:
                    return "���������� ����� ��� ��������� ������������� ��";

                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                    return "���������� ����� ��� ��������� ������������";

                case NotificationActionType.PriceEngineeringTaskSendMessage:
                    return "����� ��������� � ���������� ���";

                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                    return this.RecipientRole == Role.DesignDepartmentHead
                        ? "��������� ����������"
                        : "���� ��� ��������� �� �������� ������������ ��";

                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                    return this.RecipientRole == Role.Constructor
                        ? "���������� �� ����������� ������������� �� (����������������)"
                        : "���������� �� ����������� ������������� ��";

                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                    return "���������� ����������� ������������� ��";

                case NotificationActionType.PriceEngineeringTaskInstructToPlanMaker:
                    return this.RecipientRole == Role.PlanMaker
                        ? "�������� ������������"
                        : "�������� ��������";

                case NotificationActionType.PriceEngineeringTaskLoadToTceStart:
                    return this.RecipientRole == Role.BackManager
                        ? "��������� ���������� ���������� ��� � Team Center"
                        : "��������� ������������";

                case NotificationActionType.PriceEngineeringTaskLoadToTceFinish:
                    return "���������� ��������� � Team Center";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStart:
                    return this.RecipientRole == Role.BackManagerBoss
                        ? "�������� ��������� (��� �������� ������������)"
                        : "��������� ������ �� �������� ������������";

                case NotificationActionType.PriceEngineeringTaskProductionRequestFinish:
                    return "������������ �������";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStop:
                    return "������ �� ��������� ������������";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStopConfirm:
                    return "������ �� ��������� ������������ �����������";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStopReject:
                    return "������ �� ��������� ������������ ���������";

                #endregion

                #region PriceCalculation

                case NotificationActionType.StartPriceCalculation:
                    return "������� ������ ���������� ������";
                case NotificationActionType.CancelPriceCalculation:
                    return "���������� ������ ���������� ������";
                case NotificationActionType.RejectPriceCalculation:
                    return "�������� ������ ���������� ������";
                case NotificationActionType.FinishPriceCalculation:
                    return "�������� ������ ���������� ������";

                #endregion

                #region TechnicalRequirementsTask

                case NotificationActionType.StartTechnicalRequirementsTask:
                    return "�������� ������ ���";

                case NotificationActionType.InstructTechnicalRequirementsTask:
                    return "�������� ������ ���";

                case NotificationActionType.RejectTechnicalRequirementsTask:
                    return "��������� ������ ���";

                case NotificationActionType.RejectByFrontManagerTechnicalRequirementsTask:
                    return "��������� ������ ���";

                case NotificationActionType.FinishTechnicalRequirementsTask:
                    return "��������� ������ ���";

                case NotificationActionType.AcceptTechnicalRequirementsTask:
                    return "������� ������ ���";

                case NotificationActionType.StopTechnicalRequirementsTask:
                    return "����������� ������ ���";
                    
                #endregion

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region MyRegion

        public string GetCommonInfo(PriceEngineeringTasks tasks, PriceEngineeringTask taskTarget, PriceEngineeringTask taskTop)
        {
            SalesUnit salesUnit = taskTop.SalesUnits.FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine($"����� ������ � �� ���: {tasks.NumberFull}");
            sb.AppendLine($"����� ������ � �� ���: {taskTarget.Number}");
            sb.AppendLine($"����� ������ � Team Center: {tasks.TceNumber}");
            sb.AppendLine(string.Empty);

            sb.AppendLine($"������: {salesUnit?.Project}");
            sb.AppendLine($"������: {salesUnit?.Facility}");
            sb.AppendLine($"������������: {taskTop.ProductBlock};");
            sb.AppendLine($"���� ������������: {taskTarget.ProductBlock}");
            sb.AppendLine(string.Empty);

            sb.AppendLine($"���� ���: {taskTarget.DesignDepartment}");
            sb.AppendLine($"����������� (�� ���): {taskTarget.UserConstructor}");
            sb.AppendLine($"��������: {tasks.UserManager}");
            sb.AppendLine($"Back-��������: {tasks.BackManager}");

            return sb.ToString();
        }


        #endregion
    }
}