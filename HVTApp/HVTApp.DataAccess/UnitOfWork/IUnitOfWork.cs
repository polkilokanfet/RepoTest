using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public interface IUnitOfWork : IDisposable, IGetWrapper
    {
        int Complete();

        void AddItem<TModel, TWrapper>(TWrapper wrapper)
            where TModel : class, IBaseEntity
            where TWrapper : IWrapper<TModel>;

        IActivityFieldsRepository ActivityFields { get; }
        IFriendGroupRepository FriendGroups { get; }
        IUsersRepository Users { get; }
        ICompaniesRepository Companies { get; }
        ICompanyFormsRepository CompanyForms { get; }
        IParametersGroupsRepository ParametersGroups { get; }
        IParametersRepository Parameters { get; }
        IProductsRepository Products { get; }
        IEquipmentsRepository Equipments { get; }
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
