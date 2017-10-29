using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;


namespace HVTApp.DataAccess
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;


            FriendGroups = new FriendGroupRepository(context);

            ActivityFields = new ActivityFieldsRepository(context);
            Users = new UsersRepository(context);
            Companies = new CompaniesRepository(context);
            CompanyForms = new CompanyFormsRepository(context);

            ParametersGroups = new ParametersGroupsRepository(context);
            Parameters = new ParametersRepository(context);
            Parts = new PartsRepository(context);
            Products = new ProductsRepository(context);
            ProductionUnits = new ProductionUnitsRepository(context);
            FacilityTypes = new FacilityTypesRepository(context);
            Facilities = new FacilitiesRepository(context);
            Projects = new ProjectsRepository(context);
            ProjectUnits = new ProjectUnitsRepository(context);
            Offers = new OffersRepository(context);
            Tenders = new TendersRepository(context);
            Contracts = new ContractsRepository(context);
            Specifications = new SpecificationsRepository(context);
        }

        public IRepository<T> GetRepository<T>() 
            where T : class, IBaseEntity
        {
            var repositoryPropertyInfo = this.GetType().GetProperties().Single(x => typeof(IRepository<T>).IsAssignableFrom(x.PropertyType));
            return (IRepository<T>) repositoryPropertyInfo.GetValue(this);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        #region Repositories

        public IActivityFieldsRepository ActivityFields { get; }

        public ICompanyFormsRepository CompanyForms { get; }
        public IFriendGroupRepository FriendGroups { get; }

        public IUsersRepository Users { get; }

        public ICompaniesRepository Companies { get; }

        public IParametersGroupsRepository ParametersGroups { get; }
        public IParametersRepository Parameters { get; }
        public IPartsRepository Parts { get; }
        public IProductsRepository Products { get; }
        public IProductionUnitsRepository ProductionUnits { get; }
        public IFacilityTypesRepository FacilityTypes { get; }

        public IFacilitiesRepository Facilities { get; }
        public IProjectsRepository Projects { get; }
        public IProjectUnitsRepository ProjectUnits { get; }
        public IOffersRepository Offers { get; }
        public IContractsRepository Contracts { get; }
        public ISpecificationsRepository Specifications { get; }

        public ITendersRepository Tenders { get; }

        #endregion
    }
}
