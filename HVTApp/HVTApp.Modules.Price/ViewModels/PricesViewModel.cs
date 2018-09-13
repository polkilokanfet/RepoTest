using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Price.ViewModels
{
    public class PricesViewModel : BindableBase
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork _unitOfWork;

        public ObservableCollection<PriceTask> PriceTasks { get; } = new ObservableCollection<PriceTask>();

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }

        public PricesViewModel(IUnityContainer container)
        {
            _container = container;
            SaveCommand = new DelegateCommand(async () =>
            {
                foreach (var priceTask in PriceTasks)
                    priceTask.SavePrice();
                await _unitOfWork.SaveChangesAsync();
                await LoadAsync();
            });

            ReloadCommand = new DelegateCommand(async () => await LoadAsync());
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
                                               .Where(x => x.Date > lastPriceDate.AddDays(CommonOptions.ActualPriceTerm)).ToList();

                var projects = salesUnits.Where(x => ContainsBlock(x, block) && 
                                                     x.OrderInTakeDate > lastPriceDate.AddDays(CommonOptions.ActualPriceTerm)).Select(x => x.Project).Distinct().ToList();

                var offers = offerUnits.Where(x => ContainsBlock(x, block))
                                       .Select(x => x.Offer).Distinct()
                                       .Where(x => x.RegistrationDetailsOfSender.RegistrationDate > lastPriceDate.AddDays(CommonOptions.ActualPriceTerm)).ToList();

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
