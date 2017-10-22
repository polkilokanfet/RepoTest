using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVTApp.DataAccess.Lookup
{
    public class LookupItem : ILookupItem
    {
        public Guid Id { get; set; }
        public string DisplayMember { get; set; }
    }

    public interface ILookupItem
    {
        string DisplayMember { get; set; }
        Guid Id { get; set; }
    }
}
