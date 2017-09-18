using System;
using System.Collections.Generic;
using System.Reflection;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.TestDataGenerator
{
    public class TestUnitOfWork: IUnitOfWork
    {
        readonly Dictionary<IBaseEntity, IWrapper<IBaseEntity>> _wrappers = new Dictionary<IBaseEntity, IWrapper<IBaseEntity>>();

        public TestUnitOfWork()
        {
            TestData testData = new TestData();

            GetWrapper<ProjectWrapper>(testData.Project1);

            Projects = new ProjectsRepository(_wrappers, this);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void AddWrapperInDictionary(IWrapper<IBaseEntity> wrapper)
        {
            _wrappers.Add(wrapper.Model, wrapper);
        }

        public TWrapper GetWrapper<TWrapper>(IBaseEntity model) where TWrapper : class, IWrapper<IBaseEntity>
        {
            if (!_wrappers.ContainsKey(model))
                Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { model, this }, null, null);

            return (TWrapper)_wrappers[model];
        }

        public TWrapper GetWrapper<TWrapper>() where TWrapper : class, IWrapper<IBaseEntity>
        {
            return (TWrapper)Activator.CreateInstance(typeof(TWrapper), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { this }, null, null);
        }

        public void RemoveWrapper(IBaseEntity model)
        {
            throw new NotImplementedException();
        }

        public int Complete()
        {
            throw new NotImplementedException();
        }

        public void AddItem(IWrapper<IBaseEntity> wrapper)
        {
            throw new NotImplementedException();
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
        public IRequiredDependentProductssParametersRepository RequiredDependentProductssParameters { get; }
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

    public class ProjectsRepository : TestBaseRepository<Project, ProjectWrapper>, IProjectsRepository
    {
        public ProjectsRepository(IDictionary<IBaseEntity, IWrapper<IBaseEntity>> wrappers, IGetWrapper getWrapper) : base(wrappers, getWrapper)
        {
        }
    }
}