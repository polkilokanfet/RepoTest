using System.Collections.Generic;
using System.Data.Entity;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces;

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
            ProductParameters = new ProductParametersRepository(context, _repository);
            Projects = new ProjectsRepository(context, _repository);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public IActivityFieldsRepository ActivityFields { get; }

        public ICompanyFormsRepository CompanyForms { get; }
        public IFriendGroupRepository FriendGroups { get; }

        public IUsersRepository Users { get; }

        public ICompaniesRepository Companies { get; }

        public IProductParametersRepository ProductParameters { get; }

        public IProjectsRepository Projects { get; }
    }
}
