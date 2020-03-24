using System.Linq;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class IncomingRequestLookup
    {
        [Designation("��������")]
        public bool HasAnyPerformer => this.Performers.Any();
    }
}