using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TechnicalRequrementsTaskLookup
    {
        [Designation("�������")]
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

        [Designation("������"), OrderStatus(-10)]
        public string Status
        {
            get
            {
                if (Entity.LastHistoryElement != null)
                {
                    switch (Entity.LastHistoryElement.Type)
                    {
                        case TechnicalRequrementsTaskHistoryElementType.Create:
                            return "�������";
                        case TechnicalRequrementsTaskHistoryElementType.Start:
                            return "��������";
                        case TechnicalRequrementsTaskHistoryElementType.Finish:
                            return "���������";
                        case TechnicalRequrementsTaskHistoryElementType.Reject:
                            return "���������";
                        case TechnicalRequrementsTaskHistoryElementType.Stop:
                            return "�����������";
                        case TechnicalRequrementsTaskHistoryElementType.Instruct:
                            return "��������";
                        case TechnicalRequrementsTaskHistoryElementType.Accept:
                            return "�������";
                        default:
                            return "��";
                     }
                }
                return "��� ������� � �������";
            }
        }
    }
}