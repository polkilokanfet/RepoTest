using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.DataAccess
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;


            FriendGroups = new FriendGroupRepository(context, this);

            ActivityFields = new ActivityFieldsRepository(context, this);
            Users = new UsersRepository(context, this);
            Companies = new CompaniesRepository(context, this);
            CompanyForms = new CompanyFormsRepository(context, this);

            ParametersGroups = new ParametersGroupsRepository(context, this);
            Parameters = new ParametersRepository(context, this);
            Parts = new PartsRepository(context, this);
            Products = new ProductsRepository(context, this);
            ProductionUnits = new ProductionUnitsRepository(context, this);
            RequiredDependentProductsParameters = new RequiredDependentProductssParametersRepository(context, this);
            FacilityTypes = new FacilityTypesRepository(context, this);
            Facilities = new FacilitiesRepository(context, this);
            Projects = new ProjectsRepository(context, this);
            ProjectUnits = new ProjectUnitsRepository(context, this);
            Offers = new OffersRepository(context, this);
            Tenders = new TendersRepository(context, this);
            Contracts = new ContractsRepository(context, this);
            Specifications = new SpecificationsRepository(context, this);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        
        public void AddItem(IWrapper<IBaseEntity> wrapper)
        {
            var modelType = wrapper.GetType().GetProperty(nameof(wrapper.Model)).PropertyType;
            _context.Set(modelType).Add(wrapper.Model);
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
        public IRequiredDependentProductssParametersRepository RequiredDependentProductsParameters { get; }
        public IFacilityTypesRepository FacilityTypes { get; }

        public IFacilitiesRepository Facilities { get; }
        public IProjectsRepository Projects { get; }
        public IProjectUnitsRepository ProjectUnits { get; }
        public IOffersRepository Offers { get; }
        public IContractsRepository Contracts { get; }
        public ISpecificationsRepository Specifications { get; }

        public ITendersRepository Tenders { get; }

        #endregion


        readonly Dictionary<IBaseEntity, IWrapper<IBaseEntity>> _wrappers = new Dictionary<IBaseEntity, IWrapper<IBaseEntity>>();

        public void AddWrapperInDictionary(IWrapper<IBaseEntity> wrapper)
        {
            _wrappers.Add(wrapper.Model, wrapper);
        }

        public TWrapper GetWrapper<TWrapper>(IBaseEntity model)
            where TWrapper : class, IWrapper<IBaseEntity>
        {
            if (!_wrappers.ContainsKey(model))
                Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { model, this }, null, null);

            return (TWrapper)_wrappers[model];
        }

        public TWrapper GetWrapper<TWrapper>()
            where TWrapper : class, IWrapper<IBaseEntity>
        {
            return (TWrapper)Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null, null);
            //return GetWrapper<TWrapper>(Activator.CreateInstance<TModel>());
        }

        public void RemoveWrapper(IBaseEntity model)
        {
            _wrappers.Remove(model);
        }

    }
}
