using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Services.PriceService.PriceServ
{
    public partial class PriceService : IPriceService
    {
        private List<LaborHours> _laborHoursList = null;
        private List<LaborHourCost> _laborHourCosts = null;

        /// <summary>
        /// Список всех известных нормо-часов на производство блоков оборудования
        /// </summary>
        private List<LaborHours> LaborHoursList
        {
            get
            {
                if (_laborHoursList == null) Reload();
                return _laborHoursList;
            }
            set => _laborHoursList = value;
        }

        /// <summary>
        /// Список стоимостей нормо-часов на дату
        /// </summary>
        private List<LaborHourCost> LaborHourCosts
        {
            get
            {
                if (_laborHourCosts == null) Reload();
                return _laborHourCosts;
            }
            set => _laborHourCosts = value;
        }

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

        #region LaborHours

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
