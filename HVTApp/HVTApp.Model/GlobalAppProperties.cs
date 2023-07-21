using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model
{
    /// <summary>
    /// Контейнер глобальных переменных
    /// </summary>
    public static class GlobalAppProperties
    {
        public static GlobalProperties Actual { get; set; } = new GlobalProperties();

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public static User User { get; set; }
        public static IProductDesignationService ProductDesignationService { get; set; }
        public static IShippingService ShippingService { get; set; }
        public static IPriceService PriceService { get; set; }

        public static IHvtAppLogger HvtAppLogger { get; set; }
        public static IMessageService MessageService { get; set; }
        public static IEventServiceClient EventServiceClient { get; set; }

        #region UserIs

        /// <summary>
        /// Текущий пользлватель в роли менеджера
        /// </summary>
        public static bool UserIsManager => User?.RoleCurrent == Role.SalesManager;

        /// <summary>
        /// Текущий пользлватель в роли back-менеджера
        /// </summary>
        public static bool UserIsBackManager => User?.RoleCurrent == Role.BackManager;

        /// <summary>
        /// Текущий пользлватель в роли BackManagerBoss
        /// </summary>
        public static bool UserIsBackManagerBoss => User?.RoleCurrent == Role.BackManagerBoss;

        /// <summary>
        /// Текущий пользлватель в роли Constructor
        /// </summary>
        public static bool UserIsConstructor => User?.RoleCurrent == Role.Constructor;

        /// <summary>
        /// Текущий пользлватель в роли DesignDepartmentHead
        /// </summary>
        public static bool UserIsDesignDepartmentHead => User?.RoleCurrent == Role.DesignDepartmentHead;

        #endregion
    }
}
