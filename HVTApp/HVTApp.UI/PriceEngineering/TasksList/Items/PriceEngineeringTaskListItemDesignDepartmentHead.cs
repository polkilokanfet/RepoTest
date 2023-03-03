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
                    if (Entity.Status.Equals(ScriptStep2.VerificationRequestByConstructor))
                        return "Требует проверки";

                    if (Entity.Status.Equals(ScriptStep2.RejectByConstructor))
                        return "Поручено (отправлено менеджеру на доработку)";

                    return Entity.IsFinishedByConstructor
                        ? "Поручено (проработано исполнителем)"
                        : "Поручено (прорабатывается исполнителем)";
                }

                return "Поручено (задача остановлена)";
            }
        }

        public override bool ToShow => !Entity.Status.Equals(ScriptStep2.Stop) &&
                                       !Entity.Status.Equals(ScriptStep2.RejectByHead) &&
                                       (Entity.UserConstructor == null ||
                                        Entity.Status.Equals(ScriptStep2.VerificationRequestByConstructor));
    }
}