using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.PriceService1.Containers
{
    internal class ProductBlocksContainer
    {
        private readonly IUnityContainer _container;
        private Dictionary<Guid, ProductBlock> _productBlocksDictionary;
        private IEnumerable<ProductBlock> _productBlocksWithPrice;

        /// <summary>
        /// Блоки с прайсом
        /// </summary>
        public IEnumerable<ProductBlock> ProductBlocksWithPrice
        {
            get
            {
                if (_productBlocksWithPrice == null)
                    this.Reload();
                return _productBlocksWithPrice;
            }
        }

        public ProductBlocksContainer(IUnityContainer container)
        {
            _container = container;
        }

        public ProductBlock GetProductBlock(Guid blockId)
        {
            if (_productBlocksDictionary == null)
                this.Reload();

            return _productBlocksDictionary.ContainsKey(blockId) 
                ? _productBlocksDictionary[blockId] 
                : null;
        }

        public void Reload()
        {
            var blocks = _container.Resolve<IModelsStore>().UnitOfWork.Repository<ProductBlock>().GetAll();
            _productBlocksDictionary = blocks.ToDictionary(block => block.Id);
            _productBlocksWithPrice = blocks.Where(block => block.HasPrice).ToList();
        }
    }
}