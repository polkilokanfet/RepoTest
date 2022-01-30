using System;

namespace HVTApp.Model.Services
{
    public interface ISalesUnitService
    {
        /// <summary>
        /// Расчитытывает и возвращает дату ОИТ
        /// </summary>
        /// <param name="salesUnitId"></param>
        /// <returns></returns>
        DateTime GetOrderInTakeDate(Guid salesUnitId);

        /// <summary>
        /// Расчетная дата старта производства
        /// </summary>
        /// <param name="salesUnitId">Id юнита</param>
        /// <returns></returns>
        DateTime GetStartProductionDate(Guid salesUnitId);

        double GetSumToStartProduction(Guid salesUnitId);

        /// <summary>
        /// Расчетный срок доставки.
        /// </summary>
        double GetDeliveryPeriodCalculated(Guid salesUnitId);

        /// <summary>
        /// Дата исполнения условий для запуска производства
        /// </summary>
        DateTime? GetStartProductionConditionsDoneDate(Guid salesUnitId);
    }
}