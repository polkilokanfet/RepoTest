using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
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

        //полное удаление параметра
        public ICommand RemoveParameterTotalCommand { get; }
        #endregion

        public ProductReplacer(IUnityContainer container, ParametersViewModel parametersViewModel)
        {
            _unitOfWork = container.Resolve<IUnitOfWork>();
            var selectService = container.Resolve<ISelectService>();
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
                    this.RemoveBlockDuplicates(blockTarget, _unitOfWork);
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
                    this.RemoveBlockDuplicates(blockTarget, _unitOfWork);
                    _unitOfWork.SaveChanges();
                    container.Resolve<IMessageService>().Message("", "done");
                });

            ReplaceCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                () =>
                {
                    using (var unitOfWork = container.Resolve<IUnitOfWork>())
                    {
                        ReplaceBlock(SelectedBlockTarget, SelectedBlockReplace, true, unitOfWork, true);
                        unitOfWork.SaveChanges();
                    }

                    this.BlocksReplace.Remove(SelectedBlockReplace);
                    SelectedBlockReplace = null;

                    container.Resolve<IMessageService>().Message("", "done");
                });

            RemoveParameterTotalCommand = new DelegateLogConfirmationCommand(
                container.Resolve<IMessageService>(),
                "Вы уверены, что хотите удалить параметр из базы данных?",
                () =>
                {
                    using (var unitOfWork = container.Resolve<IUnitOfWork>())
                    {
                        var parameter = unitOfWork.Repository<Parameter>().GetById(this.SelectedParameterTarget.Id);

                        var relations = unitOfWork.Repository<ProductRelation>()
                            .Find(relation => 
                                relation.ParentProductParameters.Contains(parameter) || 
                                relation.ChildProductParameters.Contains(parameter));
                        if (relations.Any())
                        {
                            container.Resolve<IMessageService>().Message("Info", $"Удалите сначала связи между блоками: {relations.ToStringEnum()}");
                            return;
                        }


                        var allBlocks = unitOfWork.Repository<ProductBlock>().GetAll();
                        var blocks = allBlocks.Where(block => block.Parameters.Contains(parameter)).ToList();
                        blocks.ForEach(block => block.Parameters.Remove(parameter));

                        var gps = allBlocks.GroupBy(block => new
                            {
                                parToStr = block.Parameters
                                    .Select(p => p.Id.ToString())
                                    .OrderBy(s => s)
                                    .ToStringEnum()
                            })
                            .Where(x => x.Count() > 1)
                            .ToList();

                        var productBlocksToRemove = new List<ProductBlock>();

                        foreach (var gp in gps)
                        {
                            var targetBlock = selectService.SelectItem(gp);
                            if (targetBlock == null) return;

                            foreach (var replaceBlock in gp)
                            {
                                if (targetBlock.Id == replaceBlock.Id) continue;
                                this.ReplaceBlock(targetBlock, replaceBlock, false, unitOfWork, false);
                                productBlocksToRemove.Add(replaceBlock);
                            }
                        }


                        this.RemoveProductDuplicates(unitOfWork);

                        unitOfWork.Repository<ProductBlock>().DeleteRange(productBlocksToRemove);
                        if (parameter.ParameterRelations.Any())
                            unitOfWork.Repository<ParameterRelation>().DeleteRange(parameter.ParameterRelations);
                        unitOfWork.Repository<Parameter>().Delete(parameter);
                        unitOfWork.SaveChanges();
                    }
                    SelectedParameterTarget = null;

                    container.Resolve<IMessageService>().Message("info", "RemoveParameterTotalCommand done");
                });
        }

        //замена одного блока другим
        private void ReplaceBlock(ProductBlock blockTarget, ProductBlock blockReplace, bool removeProductDuplicates, IUnitOfWork unitOfWork, bool removeProductBlock)
        {
            blockTarget = unitOfWork.Repository<ProductBlock>().GetById(blockTarget.Id);
            blockReplace = unitOfWork.Repository<ProductBlock>().GetById(blockReplace.Id);

            //сохранение переменных затрат
            if (string.IsNullOrWhiteSpace(blockTarget.StructureCostNumber) == false &&
                blockTarget.StructureCostNumber == blockReplace.StructureCostNumber)
            {
                foreach (var sumOnDate in blockReplace.Prices.ToList())
                {
                    blockReplace.Prices.Remove(sumOnDate);
                    blockTarget.Prices.Add(sumOnDate);
                }
            }

            //в scc
            unitOfWork.Repository<StructureCost>()
                .Find(task => task.OriginalStructureCostProductBlock?.Id == blockReplace.Id)
                .ForEach(task => task.OriginalStructureCostProductBlock = blockTarget);

            unitOfWork.Repository<UpdateStructureCostNumberTask>()
                .Find(x => x.ProductBlock.Id == blockReplace.Id)
                .ForEach(task => task.ProductBlock = blockTarget);

            //в задачах ТСП
            unitOfWork.Repository<PriceEngineeringTask>()
                .Find(task => task.ProductBlockManager.Id == blockReplace.Id)
                .ForEach(task => task.ProductBlockManager = blockTarget);
            unitOfWork.Repository<PriceEngineeringTask>()
                .Find(task => task.ProductBlockEngineer.Id == blockReplace.Id)
                .ForEach(task => task.ProductBlockEngineer = blockTarget);
            unitOfWork.Repository<PriceEngineeringTaskProductBlockAdded>()
                .Find(blockAdded => blockAdded.ProductBlock.Id == blockReplace.Id)
                .ForEach(blockAdded => blockAdded.ProductBlock = blockTarget);

            //в продуктах
            var productsAll = unitOfWork.Repository<Product>().GetAll();
            var products = productsAll.Where(product => product.ProductBlock.Id == blockReplace.Id).ToList();
            products.ForEach(product => product.ProductBlock = blockTarget);

            if (removeProductDuplicates)
                RemoveProductDuplicates(unitOfWork);

            if (removeProductBlock)
                unitOfWork.Repository<ProductBlock>().Delete(blockReplace);
        }

        //удаление блоков-дубликатов
        private void RemoveBlockDuplicates(ProductBlock blockTarget, IUnitOfWork unitOfWork)
        {
            var target = blockTarget;
            var blocks = unitOfWork.Repository<ProductBlock>()
                .Find(block => block.Parameters.MembersAreSame(target.Parameters));

            if (string.IsNullOrEmpty(blockTarget.StructureCostNumber) &&
                blocks.Any(block => string.IsNullOrEmpty(block.StructureCostNumber) == false))
            {
                blockTarget = blocks.First(block => string.IsNullOrEmpty(block.StructureCostNumber) == false);
            }

            bool removeProductDuplicates = false;
            foreach (var blockReplace in blocks)
            {
                if (blockTarget.Id == blockReplace.Id) continue;
                ReplaceBlock(blockTarget, blockReplace, false, unitOfWork, true);
                removeProductDuplicates = true;
            }

            if (removeProductDuplicates)
                RemoveProductDuplicates(unitOfWork);
        }

        /// <summary>
        /// Удаление дубликатов продуктов
        /// </summary>
        private void RemoveProductDuplicates(IUnitOfWork unitOfWork)
        {
            var products = unitOfWork.Repository<Product>().GetAll();
            var productsToRemove = new List<Product>();

            var salesUnits = unitOfWork.Repository<SalesUnit>().GetAll().GroupBy(x => x.Product).ToDictionary(x => x.Key, x => x.ToList());
            var offerUnits = unitOfWork.Repository<OfferUnit>().GetAll().GroupBy(x => x.Product).ToDictionary(x => x.Key, x => x.ToList());
            var productsDependent = unitOfWork.Repository<ProductDependent>().GetAll().GroupBy(x => x.Product).ToDictionary(x => x.Key, x => x.ToList());
            var productsIncluded = unitOfWork.Repository<ProductIncluded>().GetAll().GroupBy(x => x.Product).ToDictionary(x => x.Key, x => x.ToList());

            while (products.Any())
            {
                var productTarget = products.First();
                products = products.Skip(1).ToList();
                var productsSameDesignation = products
                    .Where(x => x.ProductBlock.DesignationSpecial == productTarget.ProductBlock.DesignationSpecial)
                    .ToList();
                foreach (var product in productsSameDesignation)
                {
                    if (productTarget.Equals(product) == false) 
                        continue;
                    if (productsToRemove.Contains(product))
                        continue;

                    if (salesUnits.ContainsKey(product))
                        salesUnits[product].ForEach(salesUnit => salesUnit.Product = productTarget);
                    if (offerUnits.ContainsKey(product))
                        offerUnits[product].ForEach(offerUnit => offerUnit.Product = productTarget);
                    if (productsDependent.ContainsKey(product))
                        productsDependent[product].ForEach(productDependent => productDependent.Product = productTarget);
                    if (productsIncluded.ContainsKey(product))
                        productsIncluded[product].ForEach(productIncluded => productIncluded.Product = productTarget);

                    productsToRemove.Add(product);
                    products.Remove(product);
                }
            }
        
            unitOfWork.Repository<Product>().DeleteRange(productsToRemove);
        }
    }
}