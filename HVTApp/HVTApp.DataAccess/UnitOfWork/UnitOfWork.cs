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

            UsersRepository = new UsersRepository(context);
            Companies = new CompaniesRepository(context);
            CompanyForms = new CompanyFormsRepository(context);
            ProductsMain = new ProductsMainRepository(context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public ICompanyFormsRepository CompanyForms { get; }
        public IFriendGroupRepository FriendGroups { get; }
        public IProductsMainRepository ProductsMain { get; set; }

        public IUsersRepository UsersRepository { get; }

        public ICompaniesRepository Companies { get; }
    }
}
