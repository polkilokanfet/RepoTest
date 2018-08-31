using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;
using Prism.Events;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductDesignationService _designator;
        private readonly IEventAggregator _eventAggregator;
        private Bank _bank;

        public GetProductServiceWpf(IUnitOfWork unitOfWork, IProductDesignationService designator, IEventAggregator eventAggregator)
        {
            _unitOfWork = unitOfWork;
            _designator = designator;
            _eventAggregator = eventAggregator;
        }

        public async Task LoadAsync()
        {
            var parameters = await _unitOfWork.GetRepository<Parameter>().GetAllAsync();
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            var productRelations = await _unitOfWork.GetRepository<ProductRelation>().GetAllAsync();
            var productBlocks = await _unitOfWork.GetRepository<ProductBlock>().GetAllAsync();

            _bank = new Bank(products, productBlocks, parameters, productRelations);
        }

        public async Task<Product> GetProductAsync(Product originProductId = null)
        {
            await LoadAsync();

            var selectedProduct = originProductId == null ? null : _bank.Products.Single(x => x.Id == originProductId.Id);

            var productSelector = new ProductSelector(_bank, _designator, _bank.Parameters, selectedProduct);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = Application.Current.MainWindow };
            window.ShowDialog();

            //выходим, если пользователь отменил выбор продукта.
            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProductId;

            var result = productSelector.SelectedProduct;

            //если выбранного продукта нет в базе
            if (!_bank.Products.Contains(result))
            {
                _unitOfWork.GetRepository<Product>().Add(result);
                await _unitOfWork.SaveChangesAsync();
                _eventAggregator.GetEvent<AfterSaveProductEvent>().Publish(result);
            }

            return result;
        }
    }
}