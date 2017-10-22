using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

namespace HVTApp.DataAccess.Infrastructure
{
    public interface IActivityFieldsRepository : IRepository<ActivityField> { }
    public interface IFriendGroupRepository : IRepository<TestFriendGroup> { }
    public interface IUsersRepository : IRepository<User> { }
    public interface ICompaniesRepository : IRepository<Company> { }
    public interface ICompanyFormsRepository : IRepository<CompanyForm> { }
    public interface IParametersGroupsRepository : IRepository<ParameterGroup> { }
    public interface IParametersRepository : IRepository<Parameter> { }
    public interface IPartsRepository : IRepository<Part>
    {
        PartWrapper GetProductItem(IEnumerable<ParameterWrapper> parameters);
    }
    public interface IProductsRepository : IRepository<Product> { }
    public interface IProductionUnitsRepository : IRepository<ProductionUnit> { }
    public interface IFacilityTypesRepository : IRepository<FacilityType> { }
    public interface IFacilitiesRepository : IRepository<Facility> { }
    public interface IProjectsRepository : IRepository<Project> { }
    public interface IProjectUnitsRepository : IRepository<ProjectUnit> { }
    public interface ITendersRepository : IRepository<Tender> { }
    public interface IOffersRepository : IRepository<Offer> { }
    public interface IContractsRepository : IRepository<Contract> { }
    public interface ISpecificationsRepository : IRepository<Specification> { }
}
