using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Services.PriceService1.Containers
{
    internal class SalesUnitsCalculationsContainer
    {
        private readonly IUnityContainer _container;
        private Dictionary<Guid, PriceItems> _salesUnitsCalculationsDictionary;

        public SalesUnitsCalculationsContainer(IUnityContainer container)
        {
            _container = container;

            //синхронизация завершения и остановки расчетов
            _container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Subscribe(
                priceCalculation =>
                {
                    if (_salesUnitsCalculationsDictionary == null)
                        return;

                    if (priceCalculation.IsFinished)
                    {
                        foreach (var priceCalculationItem in priceCalculation.PriceCalculationItems)
                        {
                            foreach (var salesUnit in priceCalculationItem.SalesUnits)
                            {
                                this.Add(priceCalculationItem, salesUnit);
                            }
                        }
                    }
                    else
                    {
                        foreach (var priceCalculationItem in priceCalculation.PriceCalculationItems)
                        {
                            foreach (var salesUnit in priceCalculationItem.SalesUnits)
                            {
                                if (_salesUnitsCalculationsDictionary.ContainsKey(salesUnit.Id) == false) continue;

                                var priceItems = _salesUnitsCalculationsDictionary[salesUnit.Id];
                                priceItems.Remove(priceCalculationItem);
                                if (priceItems.IsEmpty) _salesUnitsCalculationsDictionary.Remove(salesUnit.Id);
                            }
                        }
                    }
                });
        }

        public void Reload()
        {
            var unitOfWork = _container.Resolve<IModelsStore>().UnitOfWork;
            _salesUnitsCalculationsDictionary = new Dictionary<Guid, PriceItems>();

            //завершенные айтемы расчетов ПЗ
            var user = GlobalAppProperties.UserIsManager ? GlobalAppProperties.User : null;
            var priceCalculationItemsFinished = ((PriceCalculationRepository)unitOfWork.Repository<PriceCalculation>())
                .GetCalculationsForPriceService(user)
                .Where(x => x.PriceCalculation.IsFinished);

            foreach (var priceCalculationItem in priceCalculationItemsFinished)
            {
                foreach (var salesUnit in priceCalculationItem.SalesUnits)
                {
                    this.Add(priceCalculationItem, salesUnit);
                }
            }
        }

        private const int DaysForToOldPriceCalculation = 6 * 31;

        private void Add(PriceCalculationItem priceCalculationItem, SalesUnit salesUnit)
        {
            if (_salesUnitsCalculationsDictionary == null)
                Reload();

            //отбрасываем слишком старые расчёты для не выигранного оборудования
            if (salesUnit.IsLoosen == false && 
                salesUnit.IsWon == false && 
                priceCalculationItem.FinishDate.HasValue &&
                (DateTime.Today - priceCalculationItem.FinishDate.Value).Days > DaysForToOldPriceCalculation)
            {
                return;
            }

            if (_salesUnitsCalculationsDictionary.ContainsKey(salesUnit.Id))
                _salesUnitsCalculationsDictionary[salesUnit.Id].Add(priceCalculationItem);
            else
                _salesUnitsCalculationsDictionary.Add(salesUnit.Id, new PriceItems(new[] {priceCalculationItem}));
        }

        public PriceCalculationItem GetPriceCalculationItem(IUnit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            if (_salesUnitsCalculationsDictionary == null)
                Reload();

            return _salesUnitsCalculationsDictionary.ContainsKey(unit.Id)
                ? _salesUnitsCalculationsDictionary[unit.Id].ActualPriceCalculationItem
                : null;
        }
    }
}