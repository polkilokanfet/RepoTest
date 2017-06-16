using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<IBaseEntity, IWrapper<IBaseEntity>> _repository;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _repository = new Dictionary<IBaseEntity, IWrapper<IBaseEntity>>();

            FriendGroups = new FriendGroupRepository(context);

            ActivityFields = new ActivityFieldsRepository(context);
            Users = new UsersRepository(context);
            Companies = new CompaniesRepository(context);
            CompanyForms = new CompanyFormsRepository(context);

            ParametersGroups = new ParametersGroupsRepository(context);
            Parameters = new ParametersRepository(context);
            ProductItems = new ProductItemsRepository(context);
            Products = new ProductsRepository(context);
            RequiredProductsChildses = new RequiredProductsChildsesRepository(context);
            FacilityTypes = new FacilityTypesRepository(context);
            Facilities = new FacilitiesRepository(context);
            Projects = new ProjectsRepository(context);
            Offers = new OffersRepository(context);
            Tenders = new TendersRepository(context);
            Contracts = new ContractsRepository(context);
            Specifications = new SpecificationsRepository(context);
            Units = new UnitsRepository(context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void AddItem<TModel, TWrapper>(TModel model, TWrapper wrapper) 
            where TModel : class, IBaseEntity 
            where TWrapper : IWrapper<TModel>
        {
            _context.Set<TModel>().Add(model);
        }

        public IActivityFieldsRepository ActivityFields { get; }

        public ICompanyFormsRepository CompanyForms { get; }
        public IFriendGroupRepository FriendGroups { get; }

        public IUsersRepository Users { get; }

        public ICompaniesRepository Companies { get; }

        public IParametersGroupsRepository ParametersGroups { get; }
        public IParametersRepository Parameters { get; }
        public IProductItemsRepository ProductItems { get; }
        public IProductsRepository Products { get; }
        public IRequiredProductsChildsesRepository RequiredProductsChildses { get; }
        public IFacilityTypesRepository FacilityTypes { get; }

        public IFacilitiesRepository Facilities { get; }
        public IProjectsRepository Projects { get; }
        public IOffersRepository Offers { get; }
        public IContractsRepository Contracts { get; }
        public ISpecificationsRepository Specifications { get; }
        public IUnitsRepository Units { get; }

        public ITendersRepository Tenders { get; }
    }
}
