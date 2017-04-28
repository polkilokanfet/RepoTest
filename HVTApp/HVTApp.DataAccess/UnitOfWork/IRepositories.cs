using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrapper;

namespace HVTApp.DataAccess
{
    public interface IActivityFieldsRepository : IRepository<ActivityFieldWrapper>
    {
    }

    public interface IFriendGroupRepository : IRepository<TestFriendGroupWrapper>
    {
    }

    public interface IUsersRepository : IRepository<UserWrapper>
    {
    }

    public interface ICompaniesRepository : IRepository<CompanyWrapper>
    {
    }

    public interface ICompanyFormsRepository : IRepository<CompanyFormWrapper>
    {
    }

    public interface IProductParametersRepository : IRepository<ParameterWrapper>
    {
    }

    public interface IProjectsRepository : IRepository<ProjectWrapper>
    {
    }
}
