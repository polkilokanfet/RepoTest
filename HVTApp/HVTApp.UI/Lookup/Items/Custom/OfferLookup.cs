using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class OfferLookup
    {
        public Company Company => Entity.RecipientEmployee.Company;
        public double Sum => Entity.OfferUnits.Sum(x => x.Cost);
    }
}