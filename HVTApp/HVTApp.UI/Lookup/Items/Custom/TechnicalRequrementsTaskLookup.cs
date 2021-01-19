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
                if (BackManager == null) return "Назначение back-менеджера";

                if (Entity.RejectByBackManagerMoment.HasValue) return $"Отклонено. Причина: {Entity.RejectComment}";

                if (FirstStartMoment.HasValue && Start.HasValue && !Equals(Start, FirstStartMoment))
                {
                    if (LastOpenBackManagerMoment.HasValue && (Start > LastOpenBackManagerMoment))
                    {
                        return "Проработка back-менеджером (внимание: front-менеджер внес изменения с момента последнего просмотра задания back-менеджером)";
                    }

                    //расчеты
                    if(this.Entity.PriceCalculations.Any(x => x.TaskCloseMoment.HasValue))
                    {
                        var max = this.Entity.PriceCalculations
                            .Where(x => x.TaskCloseMoment.HasValue)
                            .Max(x => x.TaskCloseMoment.Value);
                        if (max < Start.Value)
                        {
                            return "Проработка back-менеджером (последний расчет ПЗ завершен до изменений).";
                        }
                    }
                }
                
                if (this.Entity.PriceCalculations.Any(x => x.TaskOpenMoment.HasValue))
                {
                    if (this.PriceCalculations.Where(x => x.TaskOpenMoment.HasValue).All(x => x.TaskCloseMoment.HasValue))
                        return "Проработано (все расчеты ПЗ завершены)";
                    return "Расчет ПЗ (запущено на расчет ПЗ)";
                }

                return "Проработка back-менеджером";
            }
        }
    }
}