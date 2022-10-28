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
        [Designation("Îáúåêòû"), OrderStatus(5000)]
        public string Facilities =>
            Entity.ChildPriceEngineeringTasks
                .SelectMany(x => x.SalesUnits)
                .Select(x => x.Facility)
                .Distinct()
                .OrderBy(x => x.Name)
                .ToStringEnum();

        [Designation("Áëîêè"), OrderStatus(4000)]
        public string ProductBlocks =>
            this.ChildPriceEngineeringTasks
                .Select(x => x.Entity.ProductBlock)
                .Distinct()
                .OrderBy(x => x.Designation)
                .ToStringEnum();

        public bool ToShow
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