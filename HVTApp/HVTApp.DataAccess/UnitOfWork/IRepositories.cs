using System.Collections.Generic;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public interface IActivityFieldsRepository : IRepository<ActivityFieldWrapper> { }
    public interface IFriendGroupRepository : IRepository<TestFriendGroupWrapper> { }
    public interface IUsersRepository : IRepository<UserWrapper> { }
    public interface ICompaniesRepository : IRepository<CompanyWrapper> { }
    public interface ICompanyFormsRepository : IRepository<CompanyFormWrapper> { }
    public interface IParametersGroupsRepository : IRepository<ParameterGroupWrapper> { }
    public interface IParametersRepository : IRepository<ParameterWrapper> { }
    public interface IProductsRepository : IRepository<ProductWrapper>
    {
        ProductWrapper Find(IEnumerable<ParameterWrapper> parameters);
    }

    public interface IFacilityTypesRepository : IRepository<FacilityTypeWrapper> { }
    public interface IFacilitiesRepository : IRepository<FacilityWrapper> { }
    public interface IProjectsRepository : IRepository<ProjectWrapper> { }
    public interface ITendersRepository : IRepository<TenderWrapper> { }
    public interface IOffersRepository : IRepository<OfferWrapper> { }
    public interface IContractsRepository : IRepository<ContractWrapper> { }
    public interface ISpecificationsRepository : IRepository<SpecificationWrapper> { }
    public interface IUnitsRepository : IRepository<UnitWrapper> { }
}
