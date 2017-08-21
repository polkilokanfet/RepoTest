using System;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess.Infrastructure
{
    public interface IUnitOfWork : IDisposable, IGetWrapper
    {
        int Complete();

        void AddItem(IWrapper<IBaseEntity> wrapper);

        IFriendGroupRepository FriendGroups { get; }

        IActivityFieldsRepository ActivityFields { get; }
        ICompanyFormsRepository CompanyForms { get; }
        ICompaniesRepository Companies { get; }

        IUsersRepository Users { get; }

        IParametersGroupsRepository ParametersGroups { get; }
        IParametersRepository Parameters { get; }
        IPartsRepository Parts { get; }
        IProductsRepository Products { get; }
        IRequiredDependentProductssParametersRepository RequiredDependentProductssParameters { get; }

        IProductionUnitsRepository ProductionUnits { get; }

        IFacilityTypesRepository FacilityTypes { get; }
        IFacilitiesRepository Facilities { get; }

        IProjectsRepository Projects { get; }
        ITendersRepository Tenders { get; }
        IOffersRepository Offers { get; }
        IContractsRepository Contracts { get; }
        ISpecificationsRepository Specifications { get; }
    }
}
