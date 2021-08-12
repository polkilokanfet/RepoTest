using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TechnicalRequrementsTaskLookup
    {
        [Designation("Объекты")]
        public IEnumerable<Facility> Facilities
        {
            get
            {
                if (Requrements.Any() == false)
                    return new List<Facility>();

                return Requrements
                    .SelectMany(technicalRequrements => technicalRequrements.SalesUnits)
                    .Select(salesUnit => salesUnit.Facility.Entity)
                    .Distinct()
                    .OrderBy(x => x.Name);
            }
        }

        [Designation("Front manager"), OrderStatus(-10)]
        public string FrontManager => 
            Entity.Requrements.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager.ToString();

        [Designation("Статус"), OrderStatus(-10)]
        public string Status
        {
            get
            {
                if (Entity.LastHistoryElement != null)
                {
                    switch (Entity.LastHistoryElement.Type)
                    {
                        case TechnicalRequrementsTaskHistoryElementType.Create:
                            return "Создано";
                        case TechnicalRequrementsTaskHistoryElementType.Start:
                            return "Запущено";
                        case TechnicalRequrementsTaskHistoryElementType.Finish:
                            return "Завершено";
                        case TechnicalRequrementsTaskHistoryElementType.Reject:
                            return "Отклонено";
                        case TechnicalRequrementsTaskHistoryElementType.Stop:
                            return "Остановлено";
                        case TechnicalRequrementsTaskHistoryElementType.Instruct:
                            return "Поручено";
                        case TechnicalRequrementsTaskHistoryElementType.Accept:
                            return "Принято";
                        default:
                            return "ХЗ";
                     }
                }
                return "Нет записей в истории";
            }
        }
    }
}