using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapperRepository
    {
        protected override async Task<ProjectWrapper> GenerateWrapper(Project model)
        {
            var units = await UnitOfWorkWrapper.GetWrapperRepository<SalesUnit, SalesUnitWrapper>().FindAsync(x => Equals(x.Project.Model, model));
            return new ProjectWrapper(model, units);
        }
    }
}