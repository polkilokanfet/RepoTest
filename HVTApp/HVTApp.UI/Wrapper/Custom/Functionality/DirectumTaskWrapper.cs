using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class DirectumTaskWrapper
    {
        public List<DirectumTaskWrapper> ParallelTasks { get; } = new List<DirectumTaskWrapper>();
        public List<DirectumTaskWrapper> ChildTasks { get; } = new List<DirectumTaskWrapper>();
        public IEnumerable<DirectumTaskMessageWrapper> MessagesByMoment => Messages?.OrderBy(x => x.Moment);
    }
}