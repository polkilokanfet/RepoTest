using System;

namespace HVTApp.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();

        IFriendGroupRepository FriendGroups { get; }
        IUsersRepository UsersRepository { get; }
        ICompaniesRepository Companies { get; }
        ICompanyFormsRepository CompanyForms { get; }
    }
}
