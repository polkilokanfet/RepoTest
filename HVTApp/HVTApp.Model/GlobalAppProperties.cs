using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model
{
    public static class GlobalAppProperties
    {
        public static GlobalProperties Actual { get; set; } = new GlobalProperties();
        public static User User { get; set; }
        public static IProductDesignationService ProductDesignationService { get; set; }
        public static IShippingService ShippingService { get; set; }
    }
}
