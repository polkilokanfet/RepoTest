using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;

namespace HVTApp.DataAccess.Infrastructure
{
    public interface IActivityFieldsRepository : IRepository<ActivityField>
    {
    }

    public interface IFriendGroupRepository : IRepository<TestFriendGroup>
    {
    }

    public interface IUsersRepository : IRepository<User>
    {
    }

    public interface ICompaniesRepository : IRepository<Company>
    {
    }

    public interface ICompanyFormsRepository : IRepository<CompanyForm>
    {
    }

}
