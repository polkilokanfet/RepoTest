using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
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
                    .OrderBy(facility => facility.Name);
            }
        }

        [Designation("������"), OrderStatus(-5)]
        public string ProjectName
        {
            get
            {
                if (Requrements.Any() == false)
                    return null;

                return Requrements
                    .SelectMany(technicalRequrements => technicalRequrements.SalesUnits)
                    .FirstOrDefault()?.Entity.Project.Name;
            }
        }


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
                            return "��������� ��";
                        case TechnicalRequrementsTaskHistoryElementType.RejectByFrontManager:
                            return "��������";
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


        [Designation("S"), OrderStatus(-15)]
        public bool ToShow
        {
            get
            {
                if (this.Entity == null)
                    return false;

                if (GlobalAppProperties.UserIsManager)
                {
                    if (Entity.IsAccepted || Entity.IsStopped)
                    {
                        return false;
                    }

                    return true;
                }

                if (GlobalAppProperties.UserIsBackManagerBoss)
                {
                    if (Entity.Start == null) return false;
                    if (Entity.IsStopped) return false;
                    return Entity.BackManager == null;
                }

                if (GlobalAppProperties.UserIsBackManager)
                {
                    if (Entity.Start == null) return false;
                    if (Entity.IsFinished) return false;
                    if (Entity.IsRejected) return false;
                    if (Entity.IsStopped) return false;
                    return true;
                }

                return false;
            }
        }
    }
}