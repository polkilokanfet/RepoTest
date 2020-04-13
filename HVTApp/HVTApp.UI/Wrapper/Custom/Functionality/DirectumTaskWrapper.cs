using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class DirectumTaskWrapper
    {
        public IEnumerable<DirectumTaskMessageWrapper> MessagesByMoment => Messages?.OrderBy(x => x.Moment);
    }
}