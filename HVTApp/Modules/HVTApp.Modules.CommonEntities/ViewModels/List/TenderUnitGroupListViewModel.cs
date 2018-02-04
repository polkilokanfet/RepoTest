using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public partial class TenderUnitGroupListViewModel
    {
        protected override async Task<IEnumerable<TenderUnitGroup>> GetItems()
        {
            var tenderUnits = await UnitOfWork.GetRepository<TenderUnit>().GetAllAsNoTrackingAsync();
            var groups = tenderUnits.GroupBy(x => x, new TenderUnitComparer());
            var result = new List<TenderUnitGroup>();
            foreach (var group in groups)
                result.Add(new TenderUnitGroup {TenderUnits = group.ToList()});
            return result;
        }
    }

    public class TenderUnitComparer : IEqualityComparer<TenderUnit>
    {
        public bool Equals(TenderUnit x, TenderUnit y)
        {
            return y != null && 
                   x != null && 
                   Equals(x.Product.Id, y.Product.Id) && 
                   Equals(x.Facility.Id, y.Facility.Id) && 
                   Equals(x.Cost, y.Cost);
        }

        public int GetHashCode(TenderUnit tenderUnitGroup)
        {
            return tenderUnitGroup.Product.Id.GetHashCode() + 
                   tenderUnitGroup.Facility.Id.GetHashCode() + 
                   tenderUnitGroup.Cost.GetHashCode();
        }
    }
}