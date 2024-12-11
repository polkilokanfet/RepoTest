using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.POCOs
{
    public interface IStructureCostVersionsContainer : IProductBlockContainer
    {
        List<StructureCostVersion> StructureCostVersions { get; }
    }

    public static class StructureCostVersionsContainerExt
    {
        public static StructureCostVersion GetStructureCostVersion(this IStructureCostVersionsContainer container)
        {
            return container.StructureCostVersions
                .FirstOrDefault(version => version.OriginalStructureCostNumber == container.ProductBlock.StructureCostNumber);
        }

        ///// <summary>
        ///// Блок продукта конкретно из этой задачи имеет версию номера SCC в TCE
        ///// </summary>
        //public static bool HasSccNumberInTce(this IStructureCostVersionsContainer container)
        //{
        //    return container.StructureCostVersions.Any(structureCostVersion =>
        //        structureCostVersion.Version.HasValue &&
        //        structureCostVersion.OriginalStructureCostNumber == container.ProductBlock.StructureCostNumber); ;
        //}
    }
}