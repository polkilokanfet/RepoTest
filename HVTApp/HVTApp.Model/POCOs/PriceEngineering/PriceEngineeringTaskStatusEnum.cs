using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public enum PriceEngineeringTaskStatusEnum
    {
        /// <summary>
        /// Задача создана
        /// </summary>
        Created,
        /// <summary>
        /// Задача запущена на проработку
        /// </summary>
        Started,
        /// <summary>
        /// Задача остановлена менеджером
        /// </summary>
        Stopped,
        /// <summary>
        /// Проработка отклонена менеджером конструктору
        /// </summary>
        RejectedByManager,
        /// <summary>
        /// Проработка отклонена конструктором менеджеру
        /// </summary>
        RejectedByConstructor,
        /// <summary>
        /// Проработка завершена конструктором
        /// </summary>
        FinishedByConstructor,
        /// <summary>
        /// Проработка задачи принята менеджером
        /// </summary>
        Accepted,
        /// <summary>
        /// Конструктор направил проработку руководителю на проверку
        /// </summary>
        VerificationRequestedByConstructor,
        /// <summary>
        /// Руководитель согласовал проработку конструктору
        /// </summary>
        VerificationAcceptedByHead,
        /// <summary>
        /// Руководитель отклонил проработку конструктору
        /// </summary>
        VerificationRejectedByHead
    }

    public static class PriceEngineeringTaskStatusEnumExt
    {
        public static string ConvertToString(this PriceEngineeringTaskStatusEnum status)
        {
            switch (status)
            {
                case PriceEngineeringTaskStatusEnum.Created:
                    return "Менеджер создал задачу";

                case PriceEngineeringTaskStatusEnum.Started:
                    return "Менеджер запустил задачу на проработку";

                case PriceEngineeringTaskStatusEnum.Stopped:
                    return "Менеджер остановил проработку задачи";

                case PriceEngineeringTaskStatusEnum.RejectedByManager:
                    return "Менеджер вернул задачу на доработку исполнителю";

                case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                    return "Исполнитель отклонил задачу";

                case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    return "Исполнитель завершил работу над задачей";

                case PriceEngineeringTaskStatusEnum.Accepted:
                    return "Менеджер принял проработку задачи";

                case PriceEngineeringTaskStatusEnum.VerificationRequestedByConstructor:
                    return "Исполнитель направил задачу на проверку руководителю";

                case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                    return "Руководитель согласовал исполнителю проработку";

                case PriceEngineeringTaskStatusEnum.VerificationRejectedByHead:
                    return "Руководитель отклонил исполнителю проработку";

                default:
                    return status.ToString();
            }
        }

        public static string StatusToString(this PriceEngineeringTaskStatusEnum status)
        {
            switch (status)
            {
                case PriceEngineeringTaskStatusEnum.Created:
                    return "Создано";
                case PriceEngineeringTaskStatusEnum.Started:
                    return "Запущено на проработку";
                case PriceEngineeringTaskStatusEnum.Stopped:
                    return "Остановлено менеджером";
                case PriceEngineeringTaskStatusEnum.RejectedByManager:
                    return "Проработка отклонена менеджером";
                case PriceEngineeringTaskStatusEnum.RejectedByConstructor:
                    return "Проработка отклонена исполнителем";
                case PriceEngineeringTaskStatusEnum.FinishedByConstructor:
                    return "Проработано исполнителем";
                case PriceEngineeringTaskStatusEnum.Accepted:
                    return "Принято менеджером";
                case PriceEngineeringTaskStatusEnum.VerificationRequestedByConstructor:
                    return "На проверке у руководителя КБ";
                case PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead:
                    return "Согласовано руководителем КБ";
                case PriceEngineeringTaskStatusEnum.VerificationRejectedByHead:
                    return "Проработка отклонена руководителем КБ";
                default:
                    return "Статус вышел за пределы";
            }
        }

    }

    public class PriceEngineeringTaskScriptStepConstructor : PriceEngineeringTaskScriptStep
    {
        public PriceEngineeringTaskScriptStepConstructor(PriceEngineeringTaskStatusEnum status) : 
            base(status, Role.Constructor)
        {
        }

        public override bool AllowStep(User user, PriceEngineeringTask priceEngineeringTask)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Этап выполнения задачи
    /// </summary>
    public abstract class PriceEngineeringTaskScriptStep
    {
        /// <summary>
        /// Этапы, которые могут идти за этим
        /// </summary>
        private readonly IList<PriceEngineeringTaskScriptStep> _nextSteps = new List<PriceEngineeringTaskScriptStep>();

        /// <summary>
        /// Текущий статус задачи
        /// </summary>
        public PriceEngineeringTaskStatusEnum Status { get; }

        /// <summary>
        /// Роль пользователя, который может перейти на этот этап
        /// </summary>
        public Role Role { get; }

        protected PriceEngineeringTaskScriptStep(PriceEngineeringTaskStatusEnum status, Role role)
        {
            Status = status;
            Role = role;
        }

        /// <summary>
        /// Добавление возможного следующего этапа
        /// </summary>
        /// <param name="step"></param>
        public void AddNextStep(PriceEngineeringTaskScriptStep step)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));
            if (step.Status == this.Status) throw new ArgumentException(nameof(step));
            if (_nextSteps.Contains(step)) throw new ArgumentException(nameof(step));

            _nextSteps.Add(step);
        }

        public abstract bool AllowStep(User user, PriceEngineeringTask priceEngineeringTask);
    }
}