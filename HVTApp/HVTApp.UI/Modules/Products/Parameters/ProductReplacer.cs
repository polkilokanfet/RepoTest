using System.Collections.Generic;
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
        public ICommand AddParameterCommand { get; }
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
                SelectedBlockTarget = null;
            });

            GetBlocksReplaceCommand = new DelegateCommand(() =>
            {
                BlocksReplace.Clear();
                var blocks = _unitOfWork.Repository<ProductBlock>().Find(block => this.ParametersReplace.AllContainsInById(block.Parameters));
                BlocksReplace.AddRange(blocks.OrderBy(block => block.Designation));
                SelectedBlockReplace = null;
            });

            RemoveParameterCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    var blockTarget = _unitOfWork.Repository<ProductBlock>().GetById(SelectedBlockTarget.Id);
                    blockTarget.Parameters.RemoveById(SelectedParameterInBlock);
                    this.RemoveBlockDuplicates(blockTarget);
                    _unitOfWork.SaveChanges();
                    SelectedBlockTarget.Parameters.Remove(SelectedParameterInBlock);
                    container.Resolve<IMessageService>().Message("", "done");
                });

            AddParameterCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    var blockTarget = _unitOfWork.Repository<ProductBlock>().GetById(SelectedBlockTarget.Id);
                    var parameter = _unitOfWork.Repository<Parameter>().GetById(parametersViewModel.SelectedParameterLookup.Id);
                    blockTarget.Parameters.Add(parameter);
                    this.RemoveBlockDuplicates(blockTarget);
                    _unitOfWork.SaveChanges();
                    container.Resolve<IMessageService>().Message("", "done");
                });

            ReplaceCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    ReplaceBlock(SelectedBlockTarget, SelectedBlockReplace, true);
                    _unitOfWork.SaveChanges();

                    this.BlocksReplace.Remove(SelectedBlockReplace);
                    SelectedBlockReplace = null;

                    container.Resolve<IMessageService>().Message("", "done");
                });
        }

        private void ReplaceBlock(ProductBlock blockTarget, ProductBlock blockReplace, bool removeProductDuplicates)
        {
            blockTarget = _unitOfWork.Repository<ProductBlock>().GetById(blockTarget.Id);
            blockReplace = _unitOfWork.Repository<ProductBlock>().GetById(blockReplace.Id);

            //в scc
            _unitOfWork.Repository<StructureCost>()
                .Find(task => task.OriginalStructureCostProductBlock?.Id == blockReplace.Id)
                .ForEach(task => task.OriginalStructureCostProductBlock = blockTarget);

            //в задачах ТСП
            _unitOfWork.Repository<PriceEngineeringTask>()
                .Find(task => task.ProductBlockManager.Id == blockReplace.Id)
                .ForEach(task => task.ProductBlockManager = blockTarget);
            _unitOfWork.Repository<PriceEngineeringTask>()
                .Find(task => task.ProductBlockEngineer.Id == blockReplace.Id)
                .ForEach(task => task.ProductBlockEngineer = blockTarget);
            _unitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>()
                .Find(blockAdded => blockAdded.ProductBlock.Id == blockReplace.Id)
                .ForEach(blockAdded => blockAdded.ProductBlock = blockTarget);

            //в продуктах
            var productsAll = _unitOfWork.Repository<Product>().GetAll();
            var products = productsAll.Where(product => product.ProductBlock.Id == blockReplace.Id).ToList();
            products.ForEach(x => x.ProductBlock = blockTarget);

            if (removeProductDuplicates)
                RemoveProductDuplicates();

            _unitOfWork.Repository<ProductBlock>().Delete(blockReplace);
        }

        private void RemoveBlockDuplicates(ProductBlock blockTarget)
        {
            var target = blockTarget;
            var blocks = _unitOfWork.Repository<ProductBlock>().Find(block => block.Parameters.MembersAreSame(target.Parameters));

            if (string.IsNullOrEmpty(blockTarget.StructureCostNumber) &&
                blocks.Any(block => string.IsNullOrEmpty(block.StructureCostNumber) == false))
            {
                blockTarget = blocks.First(x => string.IsNullOrEmpty(x.StructureCostNumber) == false);
            }

            bool removeProductDuplicates = false;
            foreach (var blockReplace in blocks)
            {
                if (blockTarget.Id == blockReplace.Id) continue;
                ReplaceBlock(blockTarget, blockReplace, false);
                removeProductDuplicates = true;
            }

            if (removeProductDuplicates)
                RemoveProductDuplicates();
        }

        List<SalesUnit> _salesUnits = null;
        List<OfferUnit> _offerUnits = null;

        /// <summary>
        /// Удаление дубликатов продуктов
        /// </summary>
        private void RemoveProductDuplicates()
        {
            var products = _unitOfWork.Repository<Product>().GetAll();
            var productsToRemove = new List<Product>();

            while (products.Any())
            {
                var productFirst = products.First();
                products = products.Skip(1).ToList();
                foreach (var product in products)
                {
                    if (productFirst.Equals(product))
                    {
                        _unitOfWork.Repository<ProductDependent>()
                            .Find(x => x.Product.Id == product.Id)
                            .ForEach(x => x.Product = productFirst);

                        _unitOfWork.Repository<ProductIncluded>()
                            .Find(x => x.Product.Id == product.Id)
                            .ForEach(x => x.Product = productFirst);

                        if (_salesUnits == null)
                            _salesUnits = _unitOfWork.Repository<SalesUnit>().GetAll();
                        _salesUnits.Where(x => x.Product.Id == product.Id).ForEach(x => x.Product = productFirst);

                        if (_offerUnits == null)
                            _offerUnits = _unitOfWork.Repository<OfferUnit>().GetAll();
                        _offerUnits.Where(x => x.Product.Id == product.Id).ForEach(x => x.Product = productFirst);

                        productsToRemove.Add(product);
                    }
                }
            }

            _unitOfWork.Repository<Product>().DeleteRange(productsToRemove);
        }
    }
}