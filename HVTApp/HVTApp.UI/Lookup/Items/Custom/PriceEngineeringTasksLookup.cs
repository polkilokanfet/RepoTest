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
        [Designation("Объекты"), OrderStatus(5000)]
        public IEnumerable<Facility> Facilities =>
            Entity.ChildPriceEngineeringTasks
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct();

        public bool ToShow
        {
            get
            {
                var statuses = new List<PriceEngineeringTaskStatusEnum>
                {
                    PriceEngineeringTaskStatusEnum.Started,
                    PriceEngineeringTaskStatusEnum.FinishedByConstructor,
                    PriceEngineeringTaskStatusEnum.RejectedByConstructor,
                    PriceEngineeringTaskStatusEnum.RejectedByManager
                };

                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                    {
                        return Entity.StatusesAll.All(x => statuses.Contains(x));
                    }
                    case Role.Constructor:
                    {
                        return Entity.GetSuitableTasksForWork(GlobalAppProperties.User).Select(x => x.Status).All(x => statuses.Contains(x));
                    }
                    case Role.DesignDepartmentHead:
                        break;
                }

                return true;
            }
        }
    }
}