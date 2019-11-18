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
    }
}
