using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PriceEngineeringTasksLookup
    {
        [Designation("Объекты"), OrderStatus(5000)]
        public string Facilities =>
            Entity.ChildPriceEngineeringTasks
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct()
                .ToStringEnum();

        [Designation("Блоки"), OrderStatus(4000)]
        public string ProductBlocks =>
            Entity.ChildPriceEngineeringTasks
                .Select(x => x.ProductBlockEngineer)
                .Distinct()
                .OrderBy(x => x.Designation)
                .ToStringEnum();

        [Designation("Исполнители"), OrderStatus(3000)]
        public string Users
        {
            get
            {
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                    {
                        return Entity.ChildPriceEngineeringTasks
                            .SelectMany(x => x.GetAllPriceEngineeringTasks())
                            .Select(x => x.UserConstructor)
                            .Where(x => x != null)
                            .Distinct()
                            .ToStringEnum();
                    }
                    case Role.Constructor:
                    {
                        return Entity
                            .GetSuitableTasksForWork(GlobalAppProperties.User)
                            .Select(x => x.UserConstructor)
                            .Where(x => x != null)
                            .Distinct()
                            .ToStringEnum();
                    }
                    case Role.DesignDepartmentHead:
                    {
                        return Entity
                            .GetSuitableTasksForInstruct(GlobalAppProperties.User)
                            .Select(x => x.UserConstructor)
                            .Where(x => x != null)
                            .Distinct()
                            .ToStringEnum();
                    }
                }

                return string.Empty;
            }
        }

        public string StatusString
        {
            get
            {
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                    {
                        var statusEnums = Entity.StatusesAll.ToList();
                        return statusEnums.Select(x => x.StatusToString()).Distinct().OrderBy(x => x).ToStringEnum();

                        //if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Created)) return "Создано";
                        //if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Accepted)) return "Принято";
                        //if (statusEnums.All(status => status == PriceEngineeringTaskStatusEnum.Stopped)) return "Остановлено";

                        //return "В работе";
                    }
                    case Role.Constructor:
                    {
                        return Entity
                            .GetSuitableTasksForWork(GlobalAppProperties.User)
                            .Select(x => x.Status.StatusToString())
                            .Distinct()
                            .OrderBy(x => x)
                            .ToStringEnum();
                    }
                    case Role.DesignDepartmentHead:
                    {
                        return Entity
                            .GetSuitableTasksForInstruct(GlobalAppProperties.User)
                            .Select(x => x.GetStatusForDesignDepartmentHead())
                            .Distinct()
                            .OrderBy(x => x)
                            .ToStringEnum();
                    }
                }

                return string.Empty;
            }
        }

        //public bool? IsFinishedByConstructor
        //{
        //    get
        //    {
        //        var statuses = new List<PriceEngineeringTaskStatusEnum>()
        //        {
        //            PriceEngineeringTaskStatusEnum.FinishedByConstructor,
        //            PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification,
        //            PriceEngineeringTaskStatusEnum.VerificationAcceptedByHead,
        //            PriceEngineeringTaskStatusEnum.Accepted
        //        };

        //        switch (GlobalAppProperties.User.RoleCurrent)
        //        {
        //            case Role.SalesManager:
        //            {
        //                if (Entity.StatusesAll.All(x => statuses.Contains(x))
        //                {
        //                    return true;
        //                }

        //                return false;
        //            }
        //            case Role.Constructor:
        //            {
        //                return Entity
        //                    .GetSuitableTasksForWork(GlobalAppProperties.User)
        //                    .Select(x => x.Status.StatusToString())
        //                    .Distinct()
        //                    .OrderBy(x => x)
        //                    .ToStringEnum();
        //            }
        //            case Role.DesignDepartmentHead:
        //            {
        //                return Entity
        //                    .GetSuitableTasksForInstruct(GlobalAppProperties.User)
        //                    .Select(x => x.GetStatusForDesignDepartmentHead())
        //                    .Distinct()
        //                    .OrderBy(x => x)
        //                    .ToStringEnum();
        //            }
        //        }
        //    }
        //}

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

                        if (tasks.SelectMany(x => x.StatusesAll).All(x => x == PriceEngineeringTaskStatusEnum.Stopped))
                            return false;

                        return tasks.Any(x => x.UserConstructor == null) ||
                               tasks.Any(x => x.Status == PriceEngineeringTaskStatusEnum.FinishedByConstructorGoToVerification);
                    }
                }

                return true;
            }
        }

        public bool ToShowTce
        {
            get
            {
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                    {
                        return true;
                    }
                    case Role.BackManager:
                    {
                        return Entity.PriceCalculations.Any(x => x.IsTceConnected && x.LastHistoryItem.Type == PriceCalculationHistoryItemType.Create);
                    }
                    case Role.BackManagerBoss:
                    {
                        return Entity.BackManager == null;
                    }
                }

                return true;
            }
        }
    }
}