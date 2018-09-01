using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService
{
    public interface IPriceService
    {
        double GetPrice(Product product, DateTime date);
        double GetPrice(ProductBlock block, DateTime date);
    }
}
