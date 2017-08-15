using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public interface IUnitOfWork : IDisposable, IGetWrapper
    {
        int Complete();

        void AddItem(IWrapper<IBaseEntity> wrapper);

        IActivityFieldsRepository ActivityFields { get; }
        IFriendGroupRepository FriendGroups { get; }
        IUsersRepository Users { get; }
        ICompaniesRepository Companies { get; }
        ICompanyFormsRepository CompanyForms { get; }
        IParametersGroupsRepository ParametersGroups { get; }
        IParametersRepository Parameters { get; }
        IPartsRepository Parts { get; }
        IProductsRepository Products { get; }
        IRequiredDependentEquipmentsParametersRepository RequiredDependentEquipmentsParameters { get; }
        IFacilityTypesRepository FacilityTypes { get; }
        IFacilitiesRepository Facilities { get; }
        IProjectsRepository Projects { get; }
        ITendersRepository Tenders { get; }
        IOffersRepository Offers { get; }
        IContractsRepository Contracts { get; }
        ISpecificationsRepository Specifications { get; }
    }
}
