using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Comparers;
using HVTApp.Model.POCOs;
using HVTApp.Model.Price;
using HVTApp.Model.Services;
using HVTApp.Services.PriceService1.Containers;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PriceService1
{
    public class PriceService : IPriceService
    {
        /// <summary>
        ///  онтейнер блоков
        /// </summary>
        private ProductBlocksContainer ProductBlocksContainer { get; }

        /// <summary>
        ///  онтейнер калькул€ций ѕ«
        /// </summary>
        private SalesUnitsCalculationsContainer SalesUnitsCalculationsContainer { get; }

        /// <summary>
        /// —ловарь блоков-аналогов с прайсами
        /// </summary>
        private Dictionary<Guid, ProductBlock> ProductBlocksAnalogsDictionary { get; } = new Dictionary<Guid, ProductBlock>();

        public PriceService(IUnityContainer container)
        {
            ProductBlocksContainer = new ProductBlocksContainer(container);
            SalesUnitsCalculationsContainer = new SalesUnitsCalculationsContainer(container);
            LaborHoursContainer = new LaborHoursContainer(container);
            LaborHourCostsContainer = new LaborHourCostsContainer(container);

            container.Resolve<IModelsStore>().IsRefreshed += Reload;

#if DEBUG
#else
            //если пользователь - менеджер, грузим сервис сразу
            if (GlobalAppProperties.UserIsManager) Reload();
#endif
        }

        private void Reload()
        {
            this.ProductBlocksContainer.Reload();
            this.SalesUnitsCalculationsContainer.Reload();
            this.LaborHoursContainer.Reload();
            this.LaborHourCostsContainer.Reload();
        }

        #region Price

        public PriceCalculationItem GetPriceCalculationItem(IUnit unit) =>
            this.SalesUnitsCalculationsContainer.GetPriceCalculationItem(unit);

        public Price GetPrice(IUnit unit, DateTime targetDate, bool checkCalculations) => 
            new Price(unit, targetDate, this, checkCalculations);

        #endregion

        #region Profitability

        private LaborHoursContainer LaborHoursContainer { get; }
        private LaborHourCostsContainer LaborHourCostsContainer { get; }

        #region WageFund

        private double? GetWageFund(double? laborHoursAmount, DateTime targetDate)
        {
            if (laborHoursAmount == null)
                return null;

            //фонд оплаты труда
            return laborHoursAmount.Value * this.GetLaborHoursCost(targetDate);
        }

        public double? GetWageFund(Product product, DateTime targetDate)
        {
            //получение количества нормо-часов на всЄ изделие
            //(с учетом включенных блоков и дополнительного оборудовани€, включенного в стоимость)
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
            //получение количества нормо-часов на всЄ изделие
            //(с учетом включенных блоков и дополнительного оборудовани€, включенного в стоимость)
            double? laborHoursAmount = GetLaborHoursAmount(unit);
            return this.GetWageFund(laborHoursAmount, targetDate);
        }

        #endregion

        #region LaborHours

        public double GetLaborHoursCost(DateTime targetDate) =>
            this.LaborHourCostsContainer.GetLaborHoursCost(targetDate);

        /// <summary>
        /// ѕолучение нормо-часов на изготовление блока оборудовани€
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public double? GetLaborHoursAmount(ProductBlock block) =>
            this.LaborHoursContainer.GetLaborHoursAmount(block);

        /// <summary>
        /// ѕолучение нормо-часов на изготовление единицы продукта
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
        /// ѕолучение нормо-часов на изготовление единицы оборудовани€
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

        #endregion

        /// <summary>
        /// ѕоиск аналога дл€ блока.
        /// </summary>
        /// <param name="blockTarget">÷елевой блок.</param>
        /// <returns></returns>
        public ProductBlock GetAnalogWithPrice(ProductBlock blockTarget)
        {
            if (ProductBlocksAnalogsDictionary.ContainsKey(blockTarget.Id))
                return ProductBlocksAnalogsDictionary[blockTarget.Id];

            var targetBlock = ProductBlocksContainer.GetProductBlock(blockTarget.Id) ?? blockTarget;

            //все блоки с прайсом
            var blocksWithPrices = new List<ProductBlock>(this.ProductBlocksContainer.ProductBlocksWithPrice);
            blocksWithPrices.RemoveIfContainsById(targetBlock);

            var dic = new Dictionary<ProductBlock, double>();
            foreach (var blockWithPrice in blocksWithPrices)
            {
                double dif = 0;

                //общие параметры
                var intParams = blockWithPrice.Parameters.Intersect(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in intParams)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif += 1.0 / path.Parameters.Count;
                }

                //различающиес€ параметры
                var difParams1 = blockWithPrice.Parameters.Except(targetBlock.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams1)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(blockWithPrice.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                var difParams2 = targetBlock.Parameters.Except(blockWithPrice.Parameters, new ParameterComparer()).ToList();
                foreach (var parameter in difParams2)
                {
                    //Single было бы правильнее, но надо искать ошибку в формировании путей
                    var path = parameter.Paths().First(pathToOrigin => pathToOrigin.Parameters.AllContainsIn(targetBlock.Parameters, new ParameterComparer()));
                    dif -= 1.0 / path.Parameters.Count;
                }

                dic.Add(blockWithPrice, dif);
            }

            var blockAnalog = dic.OrderByDescending(x => x.Value).First().Key;
            ProductBlocksAnalogsDictionary.Add(blockTarget.Id, blockAnalog);
            return blockAnalog;
        }
    }
}