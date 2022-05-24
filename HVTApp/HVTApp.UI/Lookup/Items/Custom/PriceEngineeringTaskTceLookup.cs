using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PriceEngineeringTaskTceLookup
    {
        [Designation("Объекты")]
        public IEnumerable<Facility> Facilities => Entity.PriceEngineeringTaskList.SelectMany(x => x.SalesUnits).Select(x => x.Facility).OrderBy(x => x.Name);

        public bool ToShow
        {
            get
            {
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                        return FinishMoment.HasValue == false;

                    case Role.BackManager:
                        return FinishMoment.HasValue == false;

                    case Role.BackManagerBoss:
                        return Entity.BackManager == null;
                }

                return true;
            }
        }
    }
}