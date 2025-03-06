using System;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintContract
    {
        void PrintContract(Guid specificationId);
        void PrintSpecification(Guid specificationId);
    }
}