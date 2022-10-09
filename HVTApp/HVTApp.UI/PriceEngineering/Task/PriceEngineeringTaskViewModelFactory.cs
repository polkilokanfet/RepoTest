//using System;
//using HVTApp.Infrastructure;
//using HVTApp.Model;
//using HVTApp.Model.POCOs;
//using Microsoft.Practices.Unity;

//namespace HVTApp.UI.PriceEngineering
//{
//    public class PriceEngineeringTaskViewModelFactory
//    {
//        /// <summary>
//        /// Создание ViewModel в соответствии с текущей ролью пользователя
//        /// </summary>
//        /// <param name="container"></param>
//        /// <param name="priceEngineeringTask"></param>
//        /// <returns></returns>
//        public static PriceEngineeringTaskViewModel GetInstance(IUnityContainer container, PriceEngineeringTask priceEngineeringTask)
//        {
//            switch (GlobalAppProperties.User.RoleCurrent)
//            {
//                case Role.SalesManager:
//                {
//                    if (container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTask.Id) == null)
//                        return new PriceEngineeringTaskViewModelManagerNew(container, priceEngineeringTask);
//                    return new PriceEngineeringTaskViewModelManagerOld(container, priceEngineeringTask);
//                }
//                case Role.Constructor:
//                {
//                    return new PriceEngineeringTaskViewModelConstructor(container, priceEngineeringTask.Id);
//                }
//                case Role.DesignDepartmentHead:
//                {
//                    return new PriceEngineeringTaskViewModelDesignDepartmentHead(container, priceEngineeringTask.Id);
//                }
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//        }
//    }
//}