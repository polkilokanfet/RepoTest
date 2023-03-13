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
    }
}