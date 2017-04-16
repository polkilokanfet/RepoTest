using System.Data.Entity;
using HVTApp.Infrastructure.Interfaces;

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
            ProductParameters = new ProductParametersRepository(context);
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
    }
}
