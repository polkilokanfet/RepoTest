using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.PriceMaking.ViewModels
{
    public class PricesViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork _unitOfWork;
        private PriceTask _selectedPriceTask;
        private SumOnDateWrapper _selectedSumOnDate;

        public ObservableCollection<PriceTask> PriceTasks { get; } = new ObservableCollection<PriceTask>();

        public PriceTask SelectedPriceTask
        {
            get { return _selectedPriceTask; }
            set
            {
                _selectedPriceTask = value;
                ((DelegateCommand)PrintBlockInContext).RaiseCanExecuteChanged();
                ((DelegateCommand)AddPriceCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RemovePriceCommand).RaiseCanExecuteChanged();
                SelectedSumOnDate = null;
                OnPropertyChanged();
            }
        }

        public SumOnDateWrapper SelectedSumOnDate
        {
            get { return _selectedSumOnDate; }
            set
            {
                _selectedSumOnDate = value;
                ((DelegateCommand)RemovePriceCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand ReloadCommand { get; }
        public ICommand PrintBlockInContext { get; }

        /// <summary>
        /// Добавить прайс
        /// </summary>
        public ICommand AddPriceCommand { get; }

        /// <summary>
        /// Удалить прайс
        /// </summary>
        public ICommand RemovePriceCommand { get; }

        /// <summary>
        /// Подтянуть прайсы из калькуляций
        /// </summary>
        public ICommand SetPricesFromCalculationsCommand { get; }

        public PricesViewModel(IUnityContainer container)
        {
            _container = container;

            ReloadCommand = new DelegateCommand(Load);

            PrintBlockInContext = new DelegateCommand(PrintBlockInContextExecute, () => SelectedPriceTask != null);

            AddPriceCommand = new DelegateCommand(
                () =>
                {
                    var price = new SumOnDate();
                    if (_container.Resolve<IUpdateDetailsService>().UpdateDetails(price))
                    {
                        var wrapper = new SumOnDateWrapper(_unitOfWork.Repository<SumOnDate>().GetById(price.Id));
                        SelectedPriceTask.Prices.Add(wrapper);
                        SelectedPriceTask.AcceptChanges();
                        _unitOfWork.SaveChanges();
                        SelectedSumOnDate = wrapper;
                    }
                },
                () => SelectedPriceTask != null);

            RemovePriceCommand = new DelegateCommand(
                () =>
                {
                    var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить выбранный прайс?", defaultNo: true);
                    if (dr != MessageDialogResult.Yes)
                        return;
                    SelectedPriceTask.Prices.Remove(SelectedSumOnDate);
                    SelectedPriceTask.AcceptChanges();
                    _unitOfWork.SaveChanges();
                    SelectedSumOnDate = null;
                },
                () => SelectedPriceTask != null && SelectedSumOnDate != null);

            SetPricesFromCalculationsCommand = new DelegateCommand(
                () =>
                {
                    var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Прайсы", "Подтянуть прайсы из калькуляций?", defaultYes:true);
                    if (dr != MessageDialogResult.Yes)
                        return;

                    //все стракчакосты с заполненной ценой
                    var structureCosts = _unitOfWork.Repository<PriceCalculation>()
                        .Find(x => x.TaskCloseMoment.HasValue)
                        .SelectMany(x => x.PriceCalculationItems)
                        .SelectMany(x => x.StructureCosts)
                        .Distinct()
                        .Where(x => x.UnitPrice.HasValue && x.UnitPrice.Value > 0 && x.Number != null);

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
                        }
                    }

                    PriceTasks.Where(x => x.IsValid && x.IsChanged).ForEach(x => x.AcceptChanges());
                    _unitOfWork.SaveChanges();
                });
        }

        private void PrintBlockInContextExecute()
        {
            var block = SelectedPriceTask;
            var products = _unitOfWork.Repository<Product>().GetAll();
            products = products.Where(x => x.GetBlocks().Contains(block.Model)).Distinct().ToList();
            _container.Resolve<IPrintProductService>().PrintProducts(products, block.Model);
        }

        public void Load()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();

            var salesUnits = _unitOfWork.Repository<SalesUnit>().GetAll();
            var offerUnits = _unitOfWork.Repository<OfferUnit>().GetAll();
            var blocks = _unitOfWork.Repository<ProductBlock>().Find(x => !x.IsService);
            
            var priceTasks = new List<PriceTask>();
            foreach (var block in blocks)
            {
                var specifications = salesUnits.Where(x => ContainsBlock(x, block) && x.Specification != null).Select(x => x.Specification).Distinct();
                var projects = salesUnits.Where(x => ContainsBlock(x, block)).Select(x => new ProjectItem(x.Project, x.OrderInTakeDate)).Distinct();
                var offers = offerUnits.Where(x => ContainsBlock(x, block)).Select(x => x.Offer).Distinct();
                priceTasks.Add(new PriceTask(block, specifications, offers, projects));
            }

            priceTasks.Sort();

            PriceTasks.Clear();
            PriceTasks.AddRange(priceTasks);
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
