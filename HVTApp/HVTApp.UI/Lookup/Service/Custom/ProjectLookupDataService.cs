using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookupDataService
    {
        //public override async Task<IEnumerable<ProjectLookup>> GetAllLookupsAsync()
        //{
        //    var projects = await UnitOfWork.GetRepository<Project>().GetAllAsNoTrackingAsync();
        //    var units = await UnitOfWork.GetRepository<SalesUnit>().GetAllAsNoTrackingAsync();
        //    var tenders = await UnitOfWork.GetRepository<Tender>().GetAllAsNoTrackingAsync();
        //    var offers = await UnitOfWork.GetRepository<Offer>().GetAllAsNoTrackingAsync();

        //    return projects.Select(project => new ProjectLookup(project, units.Where(u => Equals(u.Project, project)), 
        //                                                                 tenders.Where(t => Equals(t.Project, project)), 
        //                                                                 offers.Where(o => Equals(o.Project, project))));
        //}

        //public override async Task<ProjectLookup> GetLookupById(Guid id)
        //{
        //    var project = await UnitOfWork.GetRepository<Project>().GetByIdAsync(id);
        //    if (project == null) return null;

        //    var units = UnitOfWork.GetRepository<SalesUnit>().Find(x => Equals(project, x.Project));
        //    var tenders = UnitOfWork.GetRepository<Tender>().Find(x => Equals(project, x.Project));
        //    var offers = UnitOfWork.GetRepository<Offer>().Find(x => Equals(project, x.Project));

        //    return new ProjectLookup(project, units, tenders, offers);
        //}
    }
}