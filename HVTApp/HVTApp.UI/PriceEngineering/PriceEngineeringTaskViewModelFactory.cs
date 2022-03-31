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
        /// <param name="salesUnits"></param>
        /// <returns></returns>
        public static PriceEngineeringTaskViewModel GetInstance(IUnityContainer container, IUnitOfWork unitOfWork, IEnumerable<SalesUnit> salesUnits)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                {
                    return new PriceEngineeringTaskViewModelManager(container, unitOfWork, salesUnits);
                }
                case Role.Constructor:
                {
                    return new PriceEngineeringTaskViewModelConstructor(container, unitOfWork, salesUnits);
                }
                case Role.DesignDepartmentHead:
                {
                    return new PriceEngineeringTaskViewModelDesignDepartmentHead(container, unitOfWork, salesUnits);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Создание ViewModel в соответствии с текущей ролью пользователя
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public static PriceEngineeringTaskViewModel GetInstance(IUnityContainer container, IUnitOfWork unitOfWork, Product product)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                {
                    return new PriceEngineeringTaskViewModelManager(container, unitOfWork, product);
                }
                case Role.Constructor:
                {
                    return new PriceEngineeringTaskViewModelConstructor(container, unitOfWork, product);
                }
                case Role.DesignDepartmentHead:
                {
                    return new PriceEngineeringTaskViewModelDesignDepartmentHead(container, unitOfWork, product);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        /// <summary>
        /// Создание ViewModel в соответствии с текущей ролью пользователя
        /// </summary>
        /// <param name="container"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="priceEngineeringTask"></param>
        /// <returns></returns>
        public static PriceEngineeringTaskViewModel GetInstance(IUnityContainer container, IUnitOfWork unitOfWork, PriceEngineeringTask priceEngineeringTask)
        {
            switch (GlobalAppProperties.User.RoleCurrent)
            {
                case Role.SalesManager:
                {
                    return new PriceEngineeringTaskViewModelManager(container, unitOfWork, priceEngineeringTask);
                }
                case Role.Constructor:
                {
                    return new PriceEngineeringTaskViewModelConstructor(container, unitOfWork, priceEngineeringTask);
                }
                case Role.DesignDepartmentHead:
                {
                    return new PriceEngineeringTaskViewModelDesignDepartmentHead(container, unitOfWork, priceEngineeringTask);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}