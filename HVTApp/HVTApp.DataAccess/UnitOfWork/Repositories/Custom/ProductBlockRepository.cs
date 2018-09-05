using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;

namespace HVTApp.DataAccess
{
    public partial class ProductBlockRepository
    {
        private ProductBlock Designate(ProductBlock block)
        {
            var designator = _container.Resolve<IProductDesignationService>();
            block.IsService = designator.IsService(block);
            return block;
        }

        private List<ProductBlock> Designate(List<ProductBlock> blocks)
        {
            blocks.ForEach(x => Designate(x));
            return blocks;
        }

        public override async Task<List<ProductBlock>> GetAllAsync()
        {
            return Designate(await base.GetAllAsync());
        }

        public override async Task<List<ProductBlock>> GetAllAsNoTrackingAsync()
        {
            return Designate(await base.GetAllAsNoTrackingAsync());
        }

        public override List<ProductBlock> Find(Func<ProductBlock, bool> predicate)
        {
            return Designate(base.Find(predicate));
        }

        public override async Task<ProductBlock> GetByIdAsync(Guid id)
        {
            return Designate(await base.GetByIdAsync(id));
        }
    }
}