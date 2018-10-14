using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
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

        public ObservableCollection<PriceTask> PriceTasks { get; } = new ObservableCollection<PriceTask>();

        public PriceTask SelectedPriceTask
        {
            get { return _selectedPriceTask; }
            set
            {
                _selectedPriceTask = value;
                ((DelegateCommand)PrintBlockInContext).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand PrintBlockInContext { get; }

        public PricesViewModel(IUnityContainer container)
        {
            _container = container;
            SaveCommand = new DelegateCommand(async () =>
            {
                PriceTasks.ForEach(x => x.SavePrice());
                await _unitOfWork.SaveChangesAsync();
                await LoadAsync();
            });

            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
            PrintBlockInContext = new DelegateCommand(PrintBlockInContextExecute, () => SelectedPriceTask != null);

        }

        private async void PrintBlockInContextExecute()
        {
            var block = SelectedPriceTask.Block;
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            products = products.Where(x => x.GetBlocks().Contains(block)).Distinct().ToList();
            _container.Resolve<IPrintProductService>().PrintProducts(products, block);
        }

        public async Task LoadAsync()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();

            var salesUnits = await _unitOfWork.Repository<SalesUnit>().GetAllAsync();
            var offerUnits = await _unitOfWork.Repository<OfferUnit>().GetAllAsync();
            var blocks = await _unitOfWork.Repository<ProductBlock>().GetAllAsync();
            
            var blocksList = new List<PriceTask>();

            //блоки, где прайс отсутствует
            foreach (var block in blocks.Where(x => !x.Prices.Any()))
            {
                var specifications = salesUnits.Where(x => ContainsBlock(x, block) && x.Specification != null).Select(x => x.Specification).Distinct();
                var projects = salesUnits.Where(x => ContainsBlock(x, block)).Select(x => x.Project).Distinct();
                var offers = offerUnits.Where(x => ContainsBlock(x, block)).Select(x => x.Offer).Distinct();
                blocksList.Add(new PriceTask(block, specifications, offers, projects));
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
                    blocksList.Add(new PriceTask(block, specifications, offers, projects));
            }

            blocksList = blocksList.OrderBy(x => x).ToList();
            PriceTasks.Clear();
            PriceTasks.AddRange(blocksList);
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
