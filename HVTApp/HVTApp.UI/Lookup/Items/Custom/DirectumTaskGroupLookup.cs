using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class DirectumTaskGroupLookup
    {
        public List<DirectumTask> DirectumTasks { get; } = new List<DirectumTask>();

        public IEnumerable<User> Performers => DirectumTasks.Select(x => x.Performer).Distinct();

        public DateTime FinishPlan => DirectumTasks.Max(x => x.FinishPlan);
        public DateTime? FinishPerformers
        {
            get
            {
                if (DirectumTasks.Any(x => !x.FinishPerformer.HasValue))
                    return null;
                return DirectumTasks.Max(x => x.FinishPerformer);
            }
        }

        public DateTime? Finish
        {
            get
            {
                if (DirectumTasks.Any(x => !x.FinishAuthor.HasValue))
                    return null;
                return DirectumTasks.Max(x => x.FinishAuthor);
            }
        }

        public string Status
        {
            get
            {
                if (Entity.IsStoped)
                    return "Остановлено";

                if (Finish.HasValue)
                    return "Принято";

                if (FinishPerformers.HasValue)
                    return "Исполнено";

                return "В работе";
            }
        }

    }
}