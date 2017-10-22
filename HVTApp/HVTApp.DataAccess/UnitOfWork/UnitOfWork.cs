using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;

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
        public IFacilityTypesRepository FacilityTypes { get; }

        public IFacilitiesRepository Facilities { get; }
        public IProjectsRepository Projects { get; }
        public IProjectUnitsRepository ProjectUnits { get; }
        public IOffersRepository Offers { get; }
        public IContractsRepository Contracts { get; }
        public ISpecificationsRepository Specifications { get; }

        public ITendersRepository Tenders { get; }

        #endregion


        //readonly Dictionary<IBaseEntity, IWrapper<IBaseEntity>> _wrappers = new Dictionary<IBaseEntity, IWrapper<IBaseEntity>>();

        //public void AddWrapperInDictionary(IWrapper<IBaseEntity> wrapper)
        //{
        //    _wrappers.Add(wrapper.Model, wrapper);
        //}

        //public TWrapper GetWrapper<TWrapper>(IBaseEntity model)
        //    where TWrapper : class, IWrapper<IBaseEntity>
        //{
        //    if (!_wrappers.ContainsKey(model))
        //        Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { model, this }, null, null);

        //    return (TWrapper)_wrappers[model];
        //}

        //public TWrapper GetWrapper<TWrapper>()
        //    where TWrapper : class, IWrapper<IBaseEntity>
        //{
        //    return (TWrapper)Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null, null);
        //    //return GetWrapper<TWrapper>(Activator.CreateInstance<TModel>());
        //}

        //public void RemoveWrapper(IBaseEntity model)
        //{
        //    _wrappers.Remove(model);
        //}

    }
}
