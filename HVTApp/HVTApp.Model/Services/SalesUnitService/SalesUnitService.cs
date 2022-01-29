using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.Model.Services
{
    public class SalesUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IShippingService _shippingService;

        #region DictionaryRegion

        /// <summary>
        /// Словарь юнитов (по ID SalesUnit)
        /// </summary>
        private readonly DictionarySpecial<SalesUnit> _salesUnitDictionary = new DictionarySpecial<SalesUnit>();

        /// <summary>
        /// Словарь дат ОИТ (по ID SalesUnit)
        /// </summary>
        private readonly DictionarySpecial<DateTime> _orderInTakeDateDictionary = new DictionarySpecial<DateTime>();

        /// <summary>
        /// Словарь расчетных дат производства (по ID SalesUnit)
        /// </summary>
        private readonly DictionarySpecial<DateTime> _startProductionDateCalculatedDictionary = new DictionarySpecial<DateTime>();

        /// <summary>
        /// Словарь расчетных дат исполнения условий для старта производства (по ID SalesUnit)
        /// </summary>
        private readonly DictionarySpecial<DateTime?> _startProductionConditionsDoneDateCalculatedDictionary = new DictionarySpecial<DateTime?>();

        /// <summary>
        /// Словарь расчетных сумм старта производства (по ID SalesUnit)
        /// </summary>
        private readonly DictionarySpecial<double> _sumToStartProductionDictionary = new DictionarySpecial<double>();

        /// <summary>
        /// Словарь расчетных сроков доставки (по ID SalesUnit)
        /// </summary>
        private readonly DictionarySpecial<int> _expectedDeliveryPeriodCalculatedDictionary = new DictionarySpecial<int>();

        #endregion

        public SalesUnitService(IUnitOfWork unitOfWork, IShippingService shippingService)
        {
            _unitOfWork = unitOfWork;
            _shippingService = shippingService;

            var salesUnits = unitOfWork.Repository<SalesUnit>().GetAll();

            foreach (var salesUnit in salesUnits)
            {
                //формирование словаря юнитов
                _salesUnitDictionary.RefreshAndReturnValue(salesUnit.Id, salesUnit);

            }
        }

        private SalesUnit GetSalesUnit(Guid id)
        {
            if (_salesUnitDictionary.ContainsKey(id) == false)
                _salesUnitDictionary[id] = _unitOfWork.Repository<SalesUnit>().GetById(id);

            return _salesUnitDictionary[id];
        }

        /// <summary>
        /// Расчитытывает и возвращает дату ОИТ
        /// </summary>
        /// <param name="salesUnitId"></param>
        /// <returns></returns>
        public DateTime GetOrderInTakeDate(Guid salesUnitId)
        {
            //по уже рассчитанной ранее дате ОИТ
            if (_orderInTakeDateDictionary.ContainsKey(salesUnitId))
            {
                return _orderInTakeDateDictionary[salesUnitId];
            }

            SalesUnit salesUnit = GetSalesUnit(salesUnitId);

            //по подписанной спецификации
            if (salesUnit.Specification?.SignDate != null)
            {
                return _orderInTakeDateDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.Specification.SignDate.Value);
            }

            //если для старта производства не требуется денег
            if (salesUnit.Cost > 0 && salesUnit.Specification != null && Math.Abs(GetSumToStartProduction(salesUnitId)) < 0.001)
            {
                return _orderInTakeDateDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.Specification.Date);
            }

            //по первому платежу по заказу
            var actualPayments = salesUnit.PaymentsActual.Where(paymentActual => paymentActual.Sum > 0).ToList();
            if (actualPayments.Any())
            {
                return _orderInTakeDateDictionary.RefreshAndReturnValue(salesUnitId, actualPayments.Min().Date);
            }

            //по расчетной дате старта производства
            return _orderInTakeDateDictionary.RefreshAndReturnValue(salesUnitId, CalculateStartProductionDate(salesUnitId));
        }

        /// <summary>
        /// Расчетная дата старта производства
        /// </summary>
        /// <param name="salesUnitId">Id юнита</param>
        /// <returns></returns>
        public DateTime CalculateStartProductionDate(Guid salesUnitId)
        {
            if (_startProductionDateCalculatedDictionary.ContainsKey(salesUnitId))
                return _startProductionDateCalculatedDictionary[salesUnitId];

            SalesUnit salesUnit = GetSalesUnit(salesUnitId);

            if (salesUnit.StartProductionDate.HasValue) 
                return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.StartProductionDate.Value);

            //по исполнению условий, необходимых для запуска производства
            var startProductionConditionsDoneDate = StartProductionConditionsDoneDate(salesUnitId);
            if (startProductionConditionsDoneDate.HasValue) 
                return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, startProductionConditionsDoneDate.Value);

            //по сигналу менеджера
            if (salesUnit.SignalToStartProduction.HasValue) 
                return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.SignalToStartProduction.Value);

            //по дате первого платежа
            if (salesUnit.PaymentsActual.Any()) 
                return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.PaymentsActual.Select(paymentActual => paymentActual.Date).Min());

            //по дате доставки оборудования на объект
            if (salesUnit.DeliveryDate.HasValue)
            {
                return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId,
                    salesUnit.DeliveryDate.Value
                        .AddDays(-salesUnit.ProductionTerm)
                        .AddDays(-GetDeliveryPeriodCalculated(salesUnitId))
                        .SkipPastAndWeekend());
            }

            //по дате реализации
            if (salesUnit.RealizationDate.HasValue) 
                return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.RealizationDate.Value.AddDays(-salesUnit.ProductionTerm).SkipPastAndWeekend());

            //если проиграно
            if (salesUnit.IsLoosen) 
                return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.DeliveryDateExpected.AddDays(-salesUnit.ProductionTerm));

            //по необходимой дате поставки на объект
            return _startProductionDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.DeliveryDateExpected.AddDays(-salesUnit.ProductionTerm).AddDays(-GetDeliveryPeriodCalculated(salesUnitId)).SkipPastAndWeekend());
        }

        public double GetSumToStartProduction(Guid salesUnitId)
        {
            if (_sumToStartProductionDictionary.ContainsKey(salesUnitId))
                return _sumToStartProductionDictionary[salesUnitId];

            SalesUnit salesUnit = GetSalesUnit(salesUnitId);

            if (salesUnit.Cost == 0)
                return _sumToStartProductionDictionary.RefreshAndReturnValue(salesUnitId, 0);

            var sum = salesUnit.PaymentConditionSet.PaymentConditions
                .Where(paymentCondition => paymentCondition.PaymentConditionPoint.PaymentConditionPointEnum == PaymentConditionPointEnum.ProductionStart &&
                                           paymentCondition.DaysToPoint <= 0)
                .Sum(condition => salesUnit.Cost * condition.Part);

            return _sumToStartProductionDictionary.RefreshAndReturnValue(salesUnitId, sum);
        }

        /// <summary>
        /// Расчетный срок доставки.
        /// </summary>
        public double GetDeliveryPeriodCalculated(Guid salesUnitId)
        {
            if (_expectedDeliveryPeriodCalculatedDictionary.ContainsKey(salesUnitId))
                return _expectedDeliveryPeriodCalculatedDictionary[salesUnitId];

            SalesUnit salesUnit = GetSalesUnit(salesUnitId);

            //по ожидаемому сроку доставки
            if (salesUnit.ExpectedDeliveryPeriod.HasValue) 
                return _expectedDeliveryPeriodCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.ExpectedDeliveryPeriod.Value);

            //по расчетному
            var expectedDeliveryPeriodCalculated = _shippingService.DeliveryTerm(salesUnit);
            if (expectedDeliveryPeriodCalculated.HasValue)
                return _expectedDeliveryPeriodCalculatedDictionary.RefreshAndReturnValue(salesUnitId, expectedDeliveryPeriodCalculated.Value);

            return _expectedDeliveryPeriodCalculatedDictionary.RefreshAndReturnValue(salesUnitId, 3);
        }

        /// <summary>
        /// Дата исполнения условий для запуска производства
        /// </summary>
        public DateTime? StartProductionConditionsDoneDate(Guid salesUnitId)
        {
            if (_startProductionConditionsDoneDateCalculatedDictionary.ContainsKey(salesUnitId))
                return _startProductionConditionsDoneDateCalculatedDictionary[salesUnitId];

            SalesUnit salesUnit = GetSalesUnit(salesUnitId);

            var sumToStartProduction = GetSumToStartProduction(salesUnitId);

            //если для старта производства не требуется денег
            if (salesUnit.Cost > 0 && Math.Abs(sumToStartProduction) < 0.001)
            {
                if (salesUnit.Specification != null)
                    return _startProductionConditionsDoneDateCalculatedDictionary.RefreshAndReturnValue(salesUnitId, salesUnit.Specification.Date);
            }

            //если требуются деньги
            return AchiveSumDate(salesUnitId, sumToStartProduction);
        }

        /// <summary>
        /// Дата достижения суммы
        /// </summary>
        /// <param name="salesUnitId">Id юнита</param>
        /// <param name="sumToAchive">Достигаемая сумма</param>
        /// <returns></returns>
        private DateTime? AchiveSumDate(Guid salesUnitId, double sumToAchive)
        {
            SalesUnit salesUnit = GetSalesUnit(salesUnitId);

            var accuracy = 0.001;
            double sum = 0;
            foreach (var payment in salesUnit.PaymentsActual.OrderBy(paymentActual => paymentActual.Date))
            {
                sum += payment.Sum;
                if (sumToAchive - sum <= accuracy) return payment.Date;
            }

            var dic = GetConditionsDictionary(salesUnitId, salesUnit.SumPaid);
            foreach (var payment in salesUnit.PaymentsPlannedActual.OrderBy(paymentPlanned => paymentPlanned.Date))
            {
                //если пропущены какие-то условия перед сохраненными - выйти
                if (!Equals(dic.First(x => x.Value < 1).Key, payment.Condition))
                {
                    return null;
                }

                sum += payment.Part * payment.Condition.Part * salesUnit.Cost;
                if (sumToAchive - sum <= accuracy) return payment.Date;

                dic[payment.Condition] += payment.Part;
            }
            return null;
        }

        private Dictionary<PaymentCondition, double> GetConditionsDictionary(Guid salesUnitId, double sum)
        {
            SalesUnit salesUnit = GetSalesUnit(salesUnitId);

            var result = new Dictionary<PaymentCondition, double>();

            //если стоимость нулевая или нечего платить - выходим
            if (salesUnit.Cost < 0.00001) return result;

            //оплаченный остаток
            var paidRest = sum;

            //берем все условия и упорядочиваем их
            List<PaymentCondition> conditions = salesUnit.PaymentConditionSet.PaymentConditions.OrderBy(paymentCondition => paymentCondition).ToList();

            foreach (var condition in conditions)
            {
                double conditionSum = condition.Part * salesUnit.Cost;
                double paidConditionPart = 0;
                //если остатка хватает на покрытие
                if (paidRest >= conditionSum)
                {
                    paidConditionPart = 1;
                    paidRest -= conditionSum;
                }
                else
                {
                    paidConditionPart = paidRest / conditionSum;
                    paidRest = 0;
                }
                result.Add(condition, paidConditionPart);
            }
            return result;
        }

    }
}