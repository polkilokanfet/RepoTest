namespace HVTApp.Infrastructure
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUnitOfWork();
    }
}