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
        public static ISalesUnitService SalesUnitService { get; set; }

        public static IHvtAppLogger HvtAppLogger { get; set; }
        public static IMessageService MessageService { get; set; }
        public static IEventServiceClient EventServiceClient { get; set; }
    }
}
