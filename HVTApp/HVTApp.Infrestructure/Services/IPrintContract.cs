using System;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintContract
    {
        void PrintContract(Guid contractId);
        void PrintSpecification(Guid specificationId);
    }
}