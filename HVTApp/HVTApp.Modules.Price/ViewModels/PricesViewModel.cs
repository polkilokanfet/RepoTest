using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
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

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand PrintBlockInContext { get; }

        public ICommand AddPriceCommand { get; }
        public ICommand RemovePriceCommand { get; }

        public PricesViewModel(IUnityContainer container)
        {
            _container = container;
            SaveCommand = new DelegateCommand(async () =>
            {
                PriceTasks.SelectMany(x => x.Prices.RemovedItems).ForEach(x => _unitOfWork.Repository<SumOnDate>().Delete(x.Model));
                PriceTasks.Where(x => x.IsChanged).ForEach(x => { x.AcceptChanges(); });
                await _unitOfWork.SaveChangesAsync();
                await LoadAsync();
            },
                () =>
                {
                    return PriceTasks.Any(x => x.IsChanged) && PriceTasks.All(x => x.IsValid);
                });

            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
            PrintBlockInContext = new DelegateCommand(PrintBlockInContextExecute, () => SelectedPriceTask != null);

            AddPriceCommand = new DelegateCommand(() =>
            {
                var price = new SumOnDateWrapper(new SumOnDate())
                {
                    Date = DateTime.Today,
                    Sum = 1
                };
                SelectedPriceTask.Prices.Add(price);
                SelectedSumOnDate = price;
            }, 
            () => SelectedPriceTask != null);

            RemovePriceCommand = new DelegateCommand(() =>
            {
                var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить выбранный прайс?");
                if (dr != MessageDialogResult.Yes)
                    return;
                SelectedPriceTask.Prices.Remove(SelectedSumOnDate);
            },
            () => SelectedPriceTask != null && SelectedSumOnDate != null);
        }

        private async void PrintBlockInContextExecute()
        {
            var block = SelectedPriceTask;
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            products = products.Where(x => x.GetBlocks().Contains(block.Model)).Distinct().ToList();
            _container.Resolve<IPrintProductService>().PrintProducts(products, block.Model);
        }

        public async Task LoadAsync()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();

            var salesUnits = await _unitOfWork.Repository<SalesUnit>().GetAllAsync();
            var offerUnits = await _unitOfWork.Repository<OfferUnit>().GetAllAsync();
            var blocks = await _unitOfWork.Repository<ProductBlock>().GetAllAsync();
            
            var priceTasks = new List<PriceTask>();

            //блоки, где прайс отсутствует
            foreach (var block in blocks.Where(x => !x.Prices.Any()))
            {
                var specifications = salesUnits.Where(x => ContainsBlock(x, block) && x.Specification != null).Select(x => x.Specification).Distinct();
                var projects = salesUnits.Where(x => ContainsBlock(x, block)).Select(x => x.Project).Distinct();
                var offers = offerUnits.Where(x => ContainsBlock(x, block)).Select(x => x.Offer).Distinct();
                priceTasks.Add(new PriceTask(block, specifications, offers, projects));
            }

            //блоки, где прайс просрочен
            var rrr = blocks.Where(x => x.Prices.Any()).ToList();
            foreach (var block in rrr)
            {
                var lastPriceDate = block.Prices.Max(x => x.Date);

                var specifications = salesUnits.Where(x => ContainsBlock(x, block) && x.Specification != null)
                                               .Select(x => x.Specification).Distinct()
                                               .Where(x => x.Date > lastPriceDate.AddDays(GlobalAppProperties.Actual.ActualPriceTerm)).ToList();

                var projects = salesUnits.Where(x => ContainsBlock(x, block) && 
                                                     x.OrderInTakeDate > lastPriceDate.AddDays(GlobalAppProperties.Actual.ActualPriceTerm)).Select(x => x.Project).Distinct().ToList();

                var offers = offerUnits.Where(x => ContainsBlock(x, block))
                                       .Select(x => x.Offer).Distinct()
                                       .Where(x => x.Date > lastPriceDate.AddDays(GlobalAppProperties.Actual.ActualPriceTerm)).ToList();

                if (specifications.Any() || offers.Any() || projects.Any())
                    priceTasks.Add(new PriceTask(block, specifications, offers, projects));
            }

            priceTasks.Sort();

            PriceTasks.ForEach(x => x.PropertyChanged -= BlockOnPropertyChanged);

            PriceTasks.Clear();
            PriceTasks.AddRange(priceTasks);

            PriceTasks.ForEach(x => x.PropertyChanged += BlockOnPropertyChanged);
        }

        private void BlockOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
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
