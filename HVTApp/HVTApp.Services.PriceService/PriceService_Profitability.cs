using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.PriceService1.Containers;

namespace HVTApp.Services.PriceService1
{
    public partial class PriceService : IPriceService
    {
        private LaborHoursContainer LaborHoursContainer { get; }
        private LaborHourCostsContainer LaborHourCostsContainer { get; }

        private double? GetWageFund(double? laborHoursAmount, DateTime targetDate)
        {
            if (laborHoursAmount == null) 
                return null;

            //фонд оплаты труда
            return laborHoursAmount.Value * this.GetLaborHoursCost(targetDate);
        }

        public double? GetWageFund(Product product, DateTime targetDate)
        {
            //получение количества нормо-часов на всё изделие
            //(с учетом включенных блоков и дополнительного оборудования, включенного в стоимость)
            var laborHoursAmount = GetLaborHoursAmount(product);
            return this.GetWageFund(laborHoursAmount, targetDate);
        }

        public double? GetWageFund(ProductBlock productBlock, DateTime targetDate)
        {
            //получение количества нормо-часов на блок
            var laborHoursAmount = GetLaborHoursAmount(productBlock);
            return this.GetWageFund(laborHoursAmount, targetDate);
        }

        private double? GetWageFund(IUnit unit, DateTime targetDate)
        {
            //получение количества нормо-часов на всё изделие
            //(с учетом включенных блоков и дополнительного оборудования, включенного в стоимость)
            double? laborHoursAmount = GetLaborHoursAmount(unit);
            return this.GetWageFund(laborHoursAmount, targetDate);
        }

        #region LaborHours

        public double GetLaborHoursCost(DateTime targetDate) =>
            this.LaborHourCostsContainer.GetLaborHoursCost(targetDate);

        /// <summary>
        /// Получение нормо-часов на изготовление блока оборудования
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public double? GetLaborHoursAmount(ProductBlock block) =>
            this.LaborHoursContainer.GetLaborHoursAmount(block);

        /// <summary>
        /// Получение нормо-часов на изготовление единицы продукта
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public double? GetLaborHoursAmount(Product product)
        {
            double? result = this.GetLaborHoursAmount(product.ProductBlock);

            //if (result == null) return null;

            foreach (var dependentProduct in product.DependentProducts)
            {
                double? dpLaborHours = this.GetLaborHoursAmount(dependentProduct.Product) * dependentProduct.Amount;

                if (dpLaborHours != null)
                {
                    result = result == null
                        ? dpLaborHours
                        : dpLaborHours + result;
                }
            }

            return result;
        }

        /// <summary>
        /// Получение нормо-часов на изготовление единицы оборудования
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public double? GetLaborHoursAmount(IUnit unit)
        {
            double? result = this.GetLaborHoursAmount(unit.Product);

            //if (result == null) return null;

            foreach (var productIncluded in unit.ProductsIncluded)
            {
                double? dpLaborHours = this.GetLaborHoursAmount(productIncluded.Product) * productIncluded.AmountOnUnit;

                if (dpLaborHours != null)
                {
                    result = result == null 
                        ? dpLaborHours
                        : dpLaborHours + result;
                }
            }

            return result;
        }

        #endregion

    }
}
