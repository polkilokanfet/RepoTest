using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.PriceService
{
    public partial class PriceService : IPriceService
    {
        /// <summary>
        /// Список всех известных нормо-часов на производство блоков оборудования
        /// </summary>
        private List<LaborHours> LaborHoursList { get; set; } = new List<LaborHours>();

        /// <summary>
        /// Список стоимостей нормо-часов на дату
        /// </summary>
        private List<LaborHourCost> LaborHourCosts { get; set; } = new List<LaborHourCost>();

        public double? GetWageFund(Product product, DateTime targetDate)
        {
            //получение количества нормо-часов на всё изделие
            //(с учетом включенных блоков и дополнительного оборудования, включенного в стоимость)
            double? laborHoursAmount = GetLaborHoursAmount(product);
            if (laborHoursAmount == null) return null;

            //стоимость нормо-часа
            double laborHoursCost = LaborHourCosts.GetClosedSumOnDate(targetDate).Sum;

            //фонд оплаты труда
            double wageFund = laborHoursAmount.Value * laborHoursCost;
            return wageFund;
        }

        public double? GetWageFund(ProductBlock productBlock, DateTime targetDate)
        {
            //получение количества нормо-часов на блок
            double? laborHoursAmount = GetLaborHoursAmount(productBlock);
            if (laborHoursAmount == null) return null;

            //стоимость нормо-часа
            double laborHoursCost = LaborHourCosts.GetClosedSumOnDate(targetDate).Sum;

            //фонд оплаты труда
            double wageFund = laborHoursAmount.Value * laborHoursCost;
            return wageFund;
        }

        private double? GetWageFund(IUnit unit, DateTime targetDate)
        {
            //получение количества нормо-часов на всё изделие
            //(с учетом включенных блоков и дополнительного оборудования, включенного в стоимость)
            double? laborHoursAmount = GetLaborHoursAmount(unit);
            if (laborHoursAmount == null) return null;

            //стоимость нормо-часа
            double laborHoursCost = LaborHourCosts.GetClosedSumOnDate(targetDate).Sum;

            //фонд оплаты труда
            double wageFund = laborHoursAmount.Value * laborHoursCost;
            return wageFund;
        }

        public double GetLaborHoursCost(DateTime targetDate)
        {
            return LaborHourCosts.GetClosedSumOnDate(targetDate).Sum;
        }

        /// <summary>
        /// Получение нормо-часов на изготовление блока оборудования
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public double? GetLaborHoursAmount(ProductBlock block)
        {
            var laborHours = LaborHoursList
                .Where(hours => hours.Parameters.AllContainsIn(block.Parameters))
                .ToList();

            if (laborHours.Any())
            {
                return laborHours.OrderBy(hours => hours.Parameters.Count).Last().Amount;
            }

            return null;
        }

        /// <summary>
        /// Получение нормо-часов на изготовление единицы продукта
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public double? GetLaborHoursAmount(Product product)
        {
            double? result = this.GetLaborHoursAmount(product.ProductBlock);

            if (result == null) return null;

            foreach (var dependentProduct in product.DependentProducts)
            {
                double? dpLaborHours = this.GetLaborHoursAmount(dependentProduct.Product);
                if (dpLaborHours == null) return null;
                result += dpLaborHours * dependentProduct.Amount;
            }

            return result;
        }

        /// <summary>
        /// Получение нормо-часов на изготовление единицы оборудования
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        private double? GetLaborHoursAmount(IUnit unit)
        {
            double? result = this.GetLaborHoursAmount(unit.Product);

            if (result == null) return null;

            foreach (var productIncluded in unit.ProductsIncluded)
            {
                double? dpLaborHours = this.GetLaborHoursAmount(productIncluded.Product);
                if (dpLaborHours == null) return null;
                result += dpLaborHours * productIncluded.Amount;
            }

            return result;
        }
    }
}
