using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public interface IActivityFieldsRepository : IRepository<ActivityField, ActivityFieldWrapper> { }
    public interface IFriendGroupRepository : IRepository<TestFriendGroup, TestFriendGroupWrapper> { }
    public interface IUsersRepository : IRepository<User, UserWrapper> { }
    public interface ICompaniesRepository : IRepository<Company, CompanyWrapper> { }
    public interface ICompanyFormsRepository : IRepository<CompanyForm, CompanyFormWrapper> { }
    public interface IParametersGroupsRepository : IRepository<ParameterGroup, ParameterGroupWrapper> { }
    public interface IParametersRepository : IRepository<Parameter, ParameterWrapper> { }
    public interface IRequiredDependentEquipmentsParametersRepository : IRepository<RequiredDependentEquipmentsParameters, RequiredDependentEquipmentsParametersWrapper> { }

    public interface IProductsRepository : IRepository<Product, ProductWrapper>
    {
        ProductWrapper GetProductItem(IEnumerable<ParameterWrapper> parameters);
    }
    public interface IEquipmentsRepository : IRepository<Equipment, EquipmentWrapper> { }
    public interface IFacilityTypesRepository : IRepository<FacilityType, FacilityTypeWrapper> { }
    public interface IFacilitiesRepository : IRepository<Facility, FacilityWrapper> { }
    public interface IProjectsRepository : IRepository<Project, ProjectWrapper> { }
    public interface ITendersRepository : IRepository<Tender, TenderWrapper> { }
    public interface IOffersRepository : IRepository<Offer, OfferWrapper> { }
    public interface IContractsRepository : IRepository<Contract, ContractWrapper> { }
    public interface ISpecificationsRepository : IRepository<Specification, SpecificationWrapper> { }
}
