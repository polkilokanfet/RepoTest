using HVTApp.Model.POCOs;

namespace HVTApp.Model
{
    public static class GlobalAppProperties
    {
        public static GlobalProperties Actual { get; set; } = new GlobalProperties();
        public static User User { get; set; }
    }
}
