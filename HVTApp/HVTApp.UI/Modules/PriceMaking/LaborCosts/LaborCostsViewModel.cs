using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.PriceMaking.LaborCosts
{
    public class LaborCostsViewModel : ViewModelBaseCanExportToExcel
    {
        private readonly IUnityContainer _container;
        private IUnitOfWork _unitOfWork;

        public IValidatableChangeTrackingCollection<LaborCostsWrapper> Items { get; } = new ValidatableChangeTrackingCollection<LaborCostsWrapper>(new List<LaborCostsWrapper>());

        public DelegateLogCommand SaveCommand { get; }
        public DelegateLogCommand ReloadCommand { get; }

        public LaborCostsViewModel(IUnityContainer container) : base(container)
        {
            _container = container;

            SaveCommand = new DelegateLogCommand(
                () =>
                {
                    Items.AcceptChanges();
                    _unitOfWork.SaveChanges();
                },
                () => Items.IsChanged && Items.IsValid);

            ReloadCommand = new DelegateLogCommand(Load);

            Items.PropertyChanged += (sender, args) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
            };

            Load();
        }

        void Load()
        {
            _unitOfWork = _container.Resolve<IUnitOfWork>();
            var items = _unitOfWork.Repository<ProductBlock>()
                .Find(x => string.IsNullOrEmpty(x.StructureCostNumber) == false)
                .OrderBy(x => x.ProductType?.Name)
                .ThenBy(x => x.Designation)
                .Select(x => new LaborCostsWrapper(x))
                .ToList();

            Items.Clear();
            items.ForEach(x => Items.Add(x));
            Items.AcceptChanges();
        }
    }
}