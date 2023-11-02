using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PriceMaking.ViewModels
{
    public class PricesViewModel : LoadableExportableExpandCollapseViewModel
    {
        private IUnitOfWork _unitOfWork;
        private PriceTask _selectedPriceTask;
        private SumOnDateWrapper _selectedSumOnDate;

        public ObservableCollection<PriceTask> PriceTasks { get; } = new ObservableCollection<PriceTask>();

        public PriceTask SelectedPriceTask
        {
            get => _selectedPriceTask;
            set
            {
                _selectedPriceTask = value;
                PrintBlockInContext.RaiseCanExecuteChanged();
                AddPriceCommand.RaiseCanExecuteChanged();
                RemovePriceCommand.RaiseCanExecuteChanged();
                SelectedSumOnDate = null;
                RaisePropertyChanged();
            }
        }

        public SumOnDateWrapper SelectedSumOnDate
        {
            get => _selectedSumOnDate;
            set
            {
                _selectedSumOnDate = value;
                RemovePriceCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateLogCommand PrintBlockInContext { get; }

        /// <summary>
        /// Добавить прайс
        /// </summary>
        public DelegateLogCommand AddPriceCommand { get; }

        /// <summary>
        /// Удалить прайс
        /// </summary>
        public DelegateLogCommand RemovePriceCommand { get; }

        /// <summary>
        /// Подтянуть прайсы из калькуляций
        /// </summary>
        public DelegateLogCommand SetPricesFromCalculationsCommand { get; }

        public PricesViewModel(IUnityContainer container) : base(container)
        {
            PrintBlockInContext = new DelegateLogCommand(PrintBlockInContextExecute, () => SelectedPriceTask != null);

            AddPriceCommand = new DelegateLogCommand(
                () =>
                {
                    var price = new SumOnDate();
                    if (Container.Resolve<IUpdateDetailsService>().UpdateDetails(price))
                    {
                        var wrapper = new SumOnDateWrapper(_unitOfWork.Repository<SumOnDate>().GetById(price.Id));
                        SelectedPriceTask.Prices.Add(wrapper);
                        if (_unitOfWork.SaveChanges().OperationCompletedSuccessfully)
                        {
                            SelectedPriceTask.AcceptChanges();
                            SelectedSumOnDate = wrapper;
                        }
                    }
                },
                () => SelectedPriceTask != null);

            RemovePriceCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ConfirmationDialog("Удаление", "Удалить выбранный прайс?", defaultNo: true);
                    if (dr == false)
                        return;
                    SelectedPriceTask.Prices.Remove(SelectedSumOnDate);
                    if (_unitOfWork.SaveChanges().OperationCompletedSuccessfully)
                    {
                        SelectedPriceTask.AcceptChanges();
                        SelectedSumOnDate = null;
                    }
                },
                () => SelectedPriceTask != null && SelectedSumOnDate != null);

            SetPricesFromCalculationsCommand = new DelegateLogCommand(
                () =>
                {
                    var dr = Container.Resolve<IMessageService>().ConfirmationDialog("Прайсы", "Подтянуть прайсы из калькуляций?", defaultYes:true);
                    if (dr == false)
                        return;

                    var sb = new StringBuilder();

                    //все стракчакосты с заполненной ценой
                    var structureCosts = _unitOfWork.Repository<PriceCalculation>()
                        .Find(priceCalculation => priceCalculation.TaskCloseMoment.HasValue)
                        .SelectMany(priceCalculation => priceCalculation.PriceCalculationItems)
                        .SelectMany(priceCalculationItem => priceCalculationItem.StructureCosts)
                        .Distinct()
                        .Where(structureCost => structureCost.UnitPrice.HasValue && structureCost.UnitPrice.Value > 0 && !string.IsNullOrEmpty(structureCost.Number))
                        .ToList();

                    //для каждой задачи со стракчакостом
                    foreach (var priceTask in PriceTasks.Where(x => !string.IsNullOrEmpty(x.Model.StructureCostNumber)))
                    {
                        //все стракчакосты с тем же номером, что и блок
                        foreach (var structureCost in structureCosts.Where(x => x.Number.TrimAndReplaceDoubleSpaces() == priceTask.Model.StructureCostNumber.TrimAndReplaceDoubleSpaces()))
                        {
                            var priceCalculationItem = _unitOfWork.Repository<PriceCalculationItem>().GetById(structureCost.PriceCalculationItemId);
                            var date = priceCalculationItem.OrderInTakeDate;

                            //если нет даты ОИТ - не актуально
                            if (!date.HasValue)
                                continue;

                            //если такой прайс уже есть - не актуально
                            var prices = priceTask.Prices.Where(price => price.Date == date);
                            if (prices.Any(x => Math.Abs(x.Sum - structureCost.UnitPrice.Value) < 0.00001))
                            {
                                continue;
                            }

                            //добавление прайса, сформированного из стракчакоста
                            priceTask.Prices.Add(new SumOnDateWrapper(new SumOnDate())
                            {
                                Date = date.Value,
                                Sum = structureCost.UnitPrice.Value
                            });

                            sb.AppendLine($"В \"{priceTask.BlockName}\" добавлен прайс: {structureCost.UnitPrice.Value} {date.Value.ToShortDateString()}");
                        }
                    }

                    if (_unitOfWork.SaveChanges().OperationCompletedSuccessfully)
                    {
                        PriceTasks.Where(priceTask => priceTask.IsValid && priceTask.IsChanged).ForEach(priceTask => priceTask.AcceptChanges());
                        Container.Resolve<IMessageService>().Message("Информация", sb.ToString());
                    }
                });
        }

        private void PrintBlockInContextExecute()
        {
            var block = SelectedPriceTask;
            var products = _unitOfWork.Repository<Product>().GetAll();
            products = products.Where(x => x.GetBlocks().Contains(block.Model)).Distinct().ToList();
            Container.Resolve<IPrintProductService>().PrintProducts(products, block.Model);
        }

        private List<PriceTask> _priceTasks;
        protected override void GetData()
        {
            _unitOfWork = Container.Resolve<IUnitOfWork>();

            var salesUnits = _unitOfWork.Repository<SalesUnit>().GetAll();
            var offerUnits = _unitOfWork.Repository<OfferUnit>().GetAll();
            var blocks = _unitOfWork.Repository<ProductBlock>().Find(x => !x.IsService);
            
            _priceTasks = new List<PriceTask>();
            foreach (var block in blocks)
            {
                var specifications = salesUnits.Where(x => x.Specification != null).Where(x => ContainsBlock(x, block)).Select(x => x.Specification).Distinct();
                var projects = salesUnits.Where(x => ContainsBlock(x, block)).Select(x => new ProjectItem(x.Project, x.OrderInTakeDate)).Distinct();
                var offers = offerUnits.Where(x => ContainsBlock(x, block)).Select(x => x.Offer).Distinct();
                _priceTasks.Add(new PriceTask(block, specifications, offers, projects));
            }

            _priceTasks.Sort();
        }

        protected override void AfterGetData()
        {
            PriceTasks.Clear();
            PriceTasks.AddRange(_priceTasks);
        }

        private bool ContainsBlock(SalesUnit salesUnit, ProductBlock block)
        {
            if (salesUnit.Product.GetBlocks().Contains(block)) return true;
            if (salesUnit.ProductsIncluded.Any(pi => pi.Product.GetBlocks().Contains(block))) return true;
            return false;
        }

        private bool ContainsBlock(OfferUnit offerUnit, ProductBlock block)
        {
            if (offerUnit.Product.GetBlocks().Contains(block)) return true;
            if (offerUnit.ProductsIncluded.Any(pi => pi.Product.GetBlocks().Contains(block))) return true;
            return false;
        }
    }
}
