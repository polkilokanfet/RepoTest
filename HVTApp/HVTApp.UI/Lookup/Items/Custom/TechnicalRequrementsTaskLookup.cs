using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TechnicalRequrementsTaskLookup
    {
        [Designation("�������")]
        public IEnumerable<Facility> Facilities => 
            Requrements.SelectMany(x => x.SalesUnits).Select(x => x.Facility.Entity).Distinct().OrderBy(x => x.Name);

        [Designation("Front manager"), OrderStatus(-10)]
        public string FrontManager => 
            Entity.Requrements.FirstOrDefault()?.SalesUnits.FirstOrDefault()?.Project.Manager.ToString();

        [Designation("������"), OrderStatus(-10)]
        public string Status
        {
            get
            {
                if (BackManager == null) return "���� ���������� back-���������";

                if (Entity.RejectByBackManagerMoment.HasValue) return "��������� back-����������";

                if (FirstStartMoment.HasValue && Start.HasValue && !Equals(Start, FirstStartMoment))
                {
                    if (LastOpenBackManagerMoment.HasValue && (Start > LastOpenBackManagerMoment))
                    {
                        return "���� ���������� back-���������� (��������: front-�������� ���� ��������� � ������� ���������� ��������� ������� back-����������)";
                    }
                }
                
                if (this.PriceCalculations.Any())
                {
                    if (this.PriceCalculations.All(x => x.TaskCloseMoment.HasValue))
                        return "����������� (��� ������� �� ���������)";
                    return "���� ������� �� (�������� �� ������ ��)";
                }

                return "���� ���������� back-����������";
            }
        }
    }
}