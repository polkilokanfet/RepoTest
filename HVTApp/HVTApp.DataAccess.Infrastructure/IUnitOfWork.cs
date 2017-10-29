using System;
using HVTApp.Infrastructure;

namespace HVTApp.DataAccess.Infrastructure
{
    public partial interface IUnitOfWork : IDisposable
    {
        int Complete();

        IRepository<T> GetRepository<T>()
            where T : class, IBaseEntity;

        IFriendGroupRepository FriendGroups { get; }

        IActivityFieldsRepository ActivityFields { get; }
        ICompanyFormsRepository CompanyForms { get; }
        ICompaniesRepository Companies { get; }

        IUsersRepository Users { get; }

        IParametersGroupsRepository ParametersGroups { get; }
        IParametersRepository Parameters { get; }
        IPartsRepository Parts { get; }
        IProductsRepository Products { get; }

        IProductionUnitsRepository ProductionUnits { get; }

        IFacilityTypesRepository FacilityTypes { get; }
        IFacilitiesRepository Facilities { get; }

        IProjectsRepository Projects { get; }
        IProjectUnitsRepository ProjectUnits { get; }
        ITendersRepository Tenders { get; }
        IOffersRepository Offers { get; }
        IContractsRepository Contracts { get; }
        ISpecificationsRepository Specifications { get; }
    }
}
