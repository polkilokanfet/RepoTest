using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using System.Linq;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectListViewModel
    {
        protected override async Task<IEnumerable<ProjectLookup>> GetLookups()
        {
            var projectLookups = await base.GetLookups();
            foreach (var projectLookup in projectLookups)
            {
                projectLookup.SalesUnits = (await UnitOfWork.GetRepository<SalesUnit>()
                    .FindAsync(x => Equals(x.Project, projectLookup.Entity)))
                    .Select(x => new SalesUnitLookup(x)).ToList();

                projectLookup.Tenders = (await UnitOfWork.GetRepository<Tender>()
                    .FindAsync(x => Equals(x.Project, projectLookup.Entity)))
                    .Select(x => new TenderLookup(x)).ToList();

                projectLookup.Offers = (await UnitOfWork.GetRepository<Offer>()
                    .FindAsync(x => Equals(x.Project, projectLookup.Entity)))
                    .Select(x => new OfferLookup(x)).ToList();
            }

            return projectLookups;
        }
    }
}