using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<IBaseEntity, object> _repository;

        public UnitOfWork(DbContext context)
        {
            _context = context;
            _repository = new Dictionary<IBaseEntity, object>();

            FriendGroups = new FriendGroupRepository(context, _repository);

            ActivityFields = new ActivityFieldsRepository(context, _repository);
            Users = new UsersRepository(context, _repository);
            Companies = new CompaniesRepository(context, _repository);
            CompanyForms = new CompanyFormsRepository(context, _repository);

            ParametersGroups = new ParametersGroupsRepository(context, _repository);
            Parameters = new ParametersRepository(context, _repository);
            ProductItems = new ProductItemsRepository(context, _repository);
            Products = new ProductsRepository(context, _repository);
            RequiredProductsChildses = new RequiredProductsChildsesRepository(context, _repository);
            FacilityTypes = new FacilityTypesRepository(context, _repository);
            Facilities = new FacilitiesRepository(context, _repository);
            Projects = new ProjectsRepository(context, _repository);
            Offers = new OffersRepository(context, _repository);
            Tenders = new TendersRepository(context, _repository);
            Contracts = new ContractsRepository(context, _repository);
            Specifications = new SpecificationsRepository(context, _repository);
            Units = new UnitsRepository(context, _repository);
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
            _repository.Add(model, wrapper);
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
