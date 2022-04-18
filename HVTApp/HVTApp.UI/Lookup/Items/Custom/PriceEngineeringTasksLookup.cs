using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PriceEngineeringTasksLookup
    {
        [Designation("�������"), OrderStatus(5000)]
        public IEnumerable<Facility> Facilities =>
            Entity.ChildPriceEngineeringTasks
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct();

        public string StatusString
        {
            get
            {
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                    {
                        var statusEnums = Entity.StatusesAll.ToList();
                        if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Created)) return "�������";
                        if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Accepted)) return "�������";
                        if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Stopped)) return "�����������";

                        return "� ������";
                    }
                    case Role.Constructor:
                    {
                        var statusEnums = Entity.GetSuitableTasksForWork(GlobalAppProperties.User).SelectMany(x => x.StatusesAll).ToList();
                        if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Created)) return "�������";
                        if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Accepted)) return "�������";
                        if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Stopped)) return "�����������";

                        return "� ������";
                    }
                    case Role.DesignDepartmentHead:
                    {
                        var tasks = Entity.GetSuitableTasksForInstruct(GlobalAppProperties.User).ToList();
                        if (tasks.Any(x => x.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification)) return "������� ��������";
                        if (tasks.All(status => status.UserConstructor != null)) return "��������";
                        return "������� ���������";
                    }
                }

                return string.Empty;
            }
        }

        public bool ToShow
        {
            get
            {
                var statuses = new List<PriceEngineeringTaskStatusEnum>
                {
                    PriceEngineeringTaskStatusEnum.Stopped,
                    PriceEngineeringTaskStatusEnum.Accepted
                };

                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                    {
                        return Entity.StatusesAll.Any(x => statuses.Contains(x) == false);
                    }
                    case Role.Constructor:
                    {
                        return Entity.GetSuitableTasksForWork(GlobalAppProperties.User).Select(x => x.Status).Any(x => statuses.Contains(x) == false);
                    }
                    case Role.DesignDepartmentHead:
                    {
                        var tasks = Entity.GetSuitableTasksForInstruct(GlobalAppProperties.User).ToList();
                        return tasks.Any(x => x.UserConstructor == null) || 
                               tasks.Any(x => x.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification);
                    }
                }

                return true;
            }
        }
    }
}