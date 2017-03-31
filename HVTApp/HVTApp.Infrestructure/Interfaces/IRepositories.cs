using System.Collections.Generic;
using HVTApp.Model;

namespace HVTApp.Infrastructure.Interfaces
{
    public interface IActivityFieldsRepository : IRepository<ActivityField>
    {
    }

    public interface IFriendGroupRepository : IRepository<FriendGroupTest>
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

    public interface IProductsMainRepository : IRepository<ProductMain>
    {
    }

}
