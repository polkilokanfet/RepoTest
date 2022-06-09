using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering
{
    public class PriceEngineeringTaskViewModelFactory
    {
        /// <summary>
        /// Создание ViewModel в соответствии с текущей ролью пользователя
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="priceEngineeringTask"></param>
        /// <returns></returns>
        public static PriceEngineeringTaskViewModel GetInstance(IUnityContainer container, PriceEngineeringTask priceEngineeringTask)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                {
                    return new PriceEngineeringTaskViewModelManager(container, priceEngineeringTask);
                }
                case Role.Constructor:
                {
                    return new PriceEngineeringTaskViewModelConstructor(container, priceEngineeringTask);
                }
                case Role.DesignDepartmentHead:
                {
                    return new PriceEngineeringTaskViewModelDesignDepartmentHead(container, priceEngineeringTask);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}