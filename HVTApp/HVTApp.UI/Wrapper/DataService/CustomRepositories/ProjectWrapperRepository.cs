using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapperRepository
    {
        protected override async Task<ProjectWrapper> GenerateWrapper(Project project)
        {
            var units = await UnitOfWorkWrapper.GetWrapperRepository<SalesUnit, SalesUnitWrapper>().FindAsync(x => Equals(x.Project.Model, project));
            return new ProjectWrapper(project, units);
        }
    }
}