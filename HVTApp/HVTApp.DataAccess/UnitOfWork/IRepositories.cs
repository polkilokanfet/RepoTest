using System.Collections.Generic;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrappers;
using CompanyWrapper = HVTApp.Model.Wrappers.CompanyWrapper;
using ParameterWrapper = HVTApp.Model.Wrappers.ParameterWrapper;
using ProjectWrapper = HVTApp.Model.Wrappers.ProjectWrapper;

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

    public interface IParametersRepository : IRepository<ParameterWrapper>
    {
    }

    public interface IProductsRepository : IRepository<ProductWrapper>
    {
        ProductWrapper Find(IEnumerable<ParameterWrapper> parameters);
    }

    public interface IProjectsRepository : IRepository<ProjectWrapper>
    {
    }
}
