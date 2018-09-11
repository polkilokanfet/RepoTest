using Prism.Regions;

namespace HVTApp.Infrastructure.Extansions
{
    public static class RegionExt
    {
        public static void RequestNavigateContentRegion<TView>(this IRegionManager regionManager, NavigationParameters navigationParameters)
        {
            regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(TView).FullName, navigationParameters);
        }
    }
}