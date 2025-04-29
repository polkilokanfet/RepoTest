using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Items
{
    [NotForListViewGeneration]
    public class PriceEngineeringTaskListItemObserver : PriceEngineeringTaskListItemBase
    {
        public PriceEngineeringTaskListItemObserver(PriceEngineeringTask entity) : base(entity)
        {
        }

        public override string StatusString
        {
            get
            {
                if (Entity.StartMoment.HasValue)
                {
                    if (this.Entity.UserConstructor == null)
                        return "Требует поручения";

                    if (Entity.Status.Equals(ScriptStep.VerificationRequestByConstructor))
                        return "На проверке у руководителя";

                    if (Entity.Status.Equals(ScriptStep.RejectByConstructor))
                        return "Отклонено менеджеру исполнителем";

                    return Entity.IsFinishedByConstructor
                        ? "Проработано исполнителем"
                        : "Прорабатывается исполнителем";
                }

                return "Остановлено";
            }
        }

        public override bool ToShow
        {
            get
            {
                if (Entity.Status.Equals(ScriptStep.Stop)) return false;
                if (Entity.Status.Equals(ScriptStep.RejectByHead)) return false;
                if (Entity.Status.Equals(ScriptStep.RejectByConstructor)) return false;
                if (Entity.Status.Equals(ScriptStep.Accept)) return false;
                if (Entity.Status.Equals(ScriptStep.VerificationAccept)) return false;

                return Entity.Status.Equals(ScriptStep.Start) ||
                       Entity.Status.Equals(ScriptStep.VerificationRequestByConstructor);
            }
        }
    }
}