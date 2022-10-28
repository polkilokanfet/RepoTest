using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    public class PriceEngineeringTaskListItemDesignDepartmentHead : PriceEngineeringTaskListItemBase
    {
        public PriceEngineeringTaskListItemDesignDepartmentHead(PriceEngineeringTask entity) : base(entity)
        {
        }

        public override string StatusString
        {
            get
            {
                if (this.Entity.UserConstructor == null)
                {
                    return this.Entity.StartMoment.HasValue
                        ? "Требует поручения"
                        : "Задача остановлена";
                }

                if (Entity.StartMoment.HasValue)
                {
                    if (Entity.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification)
                        return "Требует проверки";

                    if (Entity.Status == PriceEngineeringTaskStatusEnum.RejectedByConstructor)
                        return "Поручено (отправлено менеджеру на доработку)";

                    return Entity.IsFinishedByConstructor
                        ? "Поручено (проработано исполнителем)"
                        : "Поручено (прорабатывается исполнителем)";
                }

                return "Поручено (задача остановлена)";
            }
        }

        public override bool ToShow => Entity.Status != PriceEngineeringTaskStatusEnum.Stopped &&
                                       (Entity.UserConstructor == null ||
                                        Entity.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification);
    }
}