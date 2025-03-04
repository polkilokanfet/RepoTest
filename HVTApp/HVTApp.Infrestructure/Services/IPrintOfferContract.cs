using System;

namespace HVTApp.Infrastructure.Services
{
    public interface IPrintOfferContract
    {
        void PrintContract(Guid contractId);
        void PrintSpecification(Guid specificationId);
    }
}