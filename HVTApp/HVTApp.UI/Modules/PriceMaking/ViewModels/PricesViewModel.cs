using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.UI.Wrapper;
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

        public ICommand SaveCommand { get; }
        public ICommand ReloadCommand { get; }
        public ICommand PrintBlockInContext { get; }

        public ICommand AddPriceCommand { get; }
        public ICommand RemovePriceCommand { get; }

        public PricesViewModel(IUnityContainer container)
        {
            _container = container;
            SaveCommand = new DelegateCommand(
                () =>
                {
                    PriceTasks.SelectMany(x => x.Prices.RemovedItems).ForEach(x => _unitOfWork.Repository<SumOnDate>().Delete(x.Model));
                    PriceTasks.Where(x => x.IsChanged).ForEach(x => { x.AcceptChanges(); });
                    _unitOfWork.SaveChanges();
                    Load();
                },
                () =>
                {
                    return PriceTasks.Any(x => x.IsChanged) && PriceTasks.All(x => x.IsValid);
                });

            ReloadCommand = new DelegateCommand(Load);

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
                var dr = _container.Resolve<IMessageService>().ShowYesNoMessageDialog("Удаление", "Удалить выбранный прайс?", defaultNo:true);
                if (dr != MessageDialogResult.Yes)
                    return;
                SelectedPriceTask.Prices.Remove(SelectedSumOnDate);
            },
            () => SelectedPriceTask != null && SelectedSumOnDate != null);
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
