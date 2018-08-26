using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using System.Linq;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectListViewModel
    {
        //protected override async Task<IEnumerable<ProjectLookup>> GetLookups()
        //{
        //    var projects = await WrapperDataService.GetRepository<Project>().GetAllAsNoTrackingAsync();
        //    var salesUnits = await WrapperDataService.GetRepository<SalesUnit>().GetAllAsNoTrackingAsync();
        //    var tenders = await WrapperDataService.GetRepository<Tender>().GetAllAsNoTrackingAsync();
        //    var offers = await WrapperDataService.GetRepository<Offer>().GetAllAsNoTrackingAsync();

        //    return projects.Select(x => new ProjectLookup(x, salesUnits.Where(su => Equals(su.Project, x)),
        //        tenders.Where(t => Equals(t.Project, x)), offers.Where(o => Equals(o.Project, x))));
        //}
    }
}