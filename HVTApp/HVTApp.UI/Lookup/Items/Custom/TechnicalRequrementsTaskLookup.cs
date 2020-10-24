using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TechnicalRequrementsTaskLookup
    {
        [Designation("Объекты")]
        public IEnumerable<Facility> Facilities => 
            Requrements.SelectMany(x => x.SalesUnits).Select(x => x.Facility.Entity).Distinct().OrderBy(x => x.Name);

        [Designation("Front manager"), OrderStatus(-10)]
        public string FrontManager => 
            Entity.Requrements.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager.ToString();

        [Designation("Статус"), OrderStatus(-10)]
        public string Status
        {
            get
            {
                if (BackManager == null) return "Этап назначения back-менеджера";

                if (Entity.RejectByBackManagerMoment.HasValue) return "Отклонено back-менеджером";

                if (FirstStartMoment.HasValue && Start.HasValue && !Equals(Start, FirstStartMoment))
                {
                    if (LastOpenBackManagerMoment.HasValue && (Start > LastOpenBackManagerMoment))
                    {
                        return "Этап проработки back-менеджером (внимание: front-менеджер внес изменения с момента последнего просмотра задания back-менеджером)";
                    }
                }
                
                if (this.PriceCalculations.Any())
                {
                    if (this.PriceCalculations.All(x => x.TaskCloseMoment.HasValue))
                        return "Проработано (все расчеты ПЗ завершены)";
                    return "Этап расчета ПЗ (запущено на расчет ПЗ)";
                }

                return "Этап проработки back-менеджером";
            }
        }
    }
}