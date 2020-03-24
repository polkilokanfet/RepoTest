using System.Linq;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.UI.Lookup
{
    public partial class IncomingRequestLookup
    {
        [Designation("Поручено")]
        public bool HasAnyPerformer => this.Performers.Any();
    }
}