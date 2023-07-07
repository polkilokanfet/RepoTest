using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Products.Parameters
{
    public class ProductReplacer
    {
        private readonly IUnitOfWork _unitOfWork;

        #region ObservableCollections

        public ObservableCollection<Parameter> ParametersTarget { get; } = new ObservableCollection<Parameter>();
        public ObservableCollection<Parameter> ParametersReplace { get; } = new ObservableCollection<Parameter>();

        public ObservableCollection<ProductBlock> BlocksTarget { get; } = new ObservableCollection<ProductBlock>();
        public ObservableCollection<ProductBlock> BlocksReplace { get; } = new ObservableCollection<ProductBlock>();

        #endregion

        #region Selected

        public Parameter SelectedParameterTarget { get; set; }
        public Parameter SelectedParameterReplace { get; set; }

        public Parameter SelectedParameterInBlock { get; set; }

        public ProductBlock SelectedBlockTarget { get; set; }
        public ProductBlock SelectedBlockReplace { get; set; }

        #endregion

        #region Commands

        public ICommand AddParameterToTargetListByBlockCommand { get; }
        public ICommand AddParameterToReplaceListByBlockCommand { get; }

        public ICommand AddParameterToTargetListCommand { get; }
        public ICommand AddParameterToReplaceListCommand { get; }

        public ICommand RemoveParameterFromTargetListCommand { get; }
        public ICommand RemoveParameterFromReplaceListCommand { get; }

        public ICommand GetBlocksTargetCommand { get; }
        public ICommand GetBlocksReplaceCommand { get; }

        public ICommand RemoveParameterCommand { get; }
        public ICommand ReplaceCommand { get; }

        #endregion

        public ProductReplacer(IUnityContainer container, ParametersViewModel parametersViewModel)
        {
            _unitOfWork = container.Resolve<IUnitOfWork>();
            var getProductService = container.Resolve<IGetProductService>();

            AddParameterToTargetListByBlockCommand = new DelegateCommand(() =>
            {
                ParametersTarget.Clear();
                var block = getProductService.GetProductBlock();
                if (block == null) return;
                ParametersTarget.AddRange(block.Parameters);
            });

            AddParameterToReplaceListByBlockCommand = new DelegateCommand(() =>
            {
                ParametersReplace.Clear();
                var block = getProductService.GetProductBlock();
                ParametersReplace.AddRange(block?.Parameters);
            });

            AddParameterToTargetListCommand = new DelegateCommand(() =>
            {
                if (parametersViewModel.SelectedParameterLookup == null) return;
                ParametersTarget.Add(parametersViewModel.SelectedParameterLookup.Entity);
            });

            AddParameterToReplaceListCommand = new DelegateCommand(() =>
            {
                if (parametersViewModel.SelectedParameterLookup == null) return;
                ParametersReplace.Add(parametersViewModel.SelectedParameterLookup.Entity);
            });

            RemoveParameterFromTargetListCommand = new DelegateCommand(() =>
            {
                ParametersTarget.Remove(SelectedParameterTarget);
            });

            RemoveParameterFromReplaceListCommand = new DelegateCommand(() =>
            {
                ParametersReplace.Remove(SelectedParameterReplace);
            });

            GetBlocksTargetCommand = new DelegateCommand(() =>
            {
                BlocksTarget.Clear();
                var blocks = _unitOfWork.Repository<ProductBlock>().Find(block => this.ParametersTarget.AllContainsInById(block.Parameters));
                BlocksTarget.AddRange(blocks.OrderBy(block => block.Designation));
            });

            GetBlocksReplaceCommand = new DelegateCommand(() =>
            {
                BlocksReplace.Clear();
                var blocks = _unitOfWork.Repository<ProductBlock>().Find(block => this.ParametersReplace.AllContainsInById(block.Parameters));
                BlocksReplace.AddRange(blocks.OrderBy(block => block.Designation));
            });

            RemoveParameterCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    var blockTarget = _unitOfWork.Repository<ProductBlock>().GetById(SelectedBlockTarget.Id);
                    blockTarget.Parameters.RemoveById(SelectedParameterInBlock);

                    var blocks = _unitOfWork.Repository<ProductBlock>().Find(x => x.Parameters.MembersAreSame(blockTarget.Parameters));
                    foreach (var blockReplace in blocks)
                    {
                        if (blockTarget.Id == blockReplace.Id) continue;
                        ReplaceBlock(blockTarget, blockReplace);
                    }

                    _unitOfWork.SaveChanges();
                });

            ReplaceCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    ReplaceBlock(SelectedBlockTarget, SelectedBlockReplace);
                    _unitOfWork.SaveChanges();
                });
        }

        private void ReplaceBlock(ProductBlock blockTarget, ProductBlock blockReplace)
        {
            blockTarget = _unitOfWork.Repository<ProductBlock>().GetById(blockTarget.Id);
            blockReplace = _unitOfWork.Repository<ProductBlock>().GetById(blockReplace.Id);

            //в задачах ТСП
            _unitOfWork.Repository<PriceEngineeringTask>().Find(task => task.ProductBlockManager.Id == blockReplace.Id).ForEach(task => task.ProductBlockManager = blockTarget);
            _unitOfWork.Repository<PriceEngineeringTask>().Find(task => task.ProductBlockEngineer.Id == blockReplace.Id).ForEach(task => task.ProductBlockEngineer = blockTarget);
            _unitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>().Find(x => x.ProductBlock.Id == blockReplace.Id).ForEach(x => x.ProductBlock = blockTarget);

            //в продуктах
            var productsAll = _unitOfWork.Repository<Product>().GetAll();
            var products = productsAll.Where(product => product.ProductBlock.Id == blockReplace.Id).ToList();
            products.ForEach(x => x.ProductBlock = blockTarget);

            //в scc
            _unitOfWork.Repository<StructureCost>().Find(task => task.OriginalStructureCostProductBlock.Id == blockReplace.Id).ForEach(task => task.OriginalStructureCostProductBlock = blockTarget);


            if (products.Any())
            {
                var productFirst = products.First();
                var sameProducts = productsAll.Where(x => x.Equals(productFirst)).ToList();
                foreach (var sameProduct in sameProducts)
                {
                    if (sameProduct.Id == productFirst.Id) continue;

                    _unitOfWork.Repository<ProductDependent>()
                        .Find(x => x.Product.Id == sameProduct.Id)
                        .ForEach(x => x.Product = productFirst);

                    _unitOfWork.Repository<ProductIncluded>()
                        .Find(x => x.Product.Id == sameProduct.Id)
                        .ForEach(x => x.Product = productFirst);

                    _unitOfWork.Repository<SalesUnit>()
                        .Find(x => x.Product.Id == sameProduct.Id)
                        .ForEach(x => x.Product = productFirst);

                    _unitOfWork.Repository<OfferUnit>()
                        .Find(x => x.Product.Id == sameProduct.Id)
                        .ForEach(x => x.Product = productFirst);
                }
            }

        }

        private void ReplaceProduct(ProductBlock blockTarget)
        {
            blockTarget = _unitOfWork.Repository<ProductBlock>().GetById(blockTarget.Id);

            //в продуктах
            var productsAll = _unitOfWork.Repository<Product>().GetAll();
            Product productSingle = null;

            var sameProducts = productsAll.Where(x => x.Equals(productFirst)).ToList();
            foreach (var sameProduct in sameProducts)
            {
                if (sameProduct.Id == productFirst.Id) continue;

                _unitOfWork.Repository<ProductDependent>()
                    .Find(x => x.Product.Id == sameProduct.Id)
                    .ForEach(x => x.Product = productFirst);

                _unitOfWork.Repository<ProductIncluded>()
                    .Find(x => x.Product.Id == sameProduct.Id)
                    .ForEach(x => x.Product = productFirst);

                _unitOfWork.Repository<SalesUnit>()
                    .Find(x => x.Product.Id == sameProduct.Id)
                    .ForEach(x => x.Product = productFirst);

                _unitOfWork.Repository<OfferUnit>()
                    .Find(x => x.Product.Id == sameProduct.Id)
                    .ForEach(x => x.Product = productFirst);
            }

        }
    }
}