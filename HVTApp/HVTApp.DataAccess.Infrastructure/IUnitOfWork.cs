using System;

namespace HVTApp.DataAccess.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        int Complete();

        IActivityFieldsRepository ActivityFields { get; }
        IFriendGroupRepository FriendGroups { get; }
        IUsersRepository Users { get; }
        ICompaniesRepository Companies { get; }
        ICompanyFormsRepository CompanyForms { get; }
    }
}
