using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationViewModel : SpecificationDetailsViewModel
    {
        public SpecificationViewModel(IUnityContainer container) : base(container)
        {
        }

        //protected override IEnumerable<IUnitsDatedGroup> GetGroups()
        //{
        //    return Item.Units.ToProductUnitGroups();
        //}

        //protected override async void AddCommand_Execute()
        //{
        //    var salesUnit = new SalesUnitWrapper(new SalesUnit { Specification = Item.Model });
        //    var viewModel = new SalesUnitsViewModel(salesUnit, Container, WrapperDataService);
        //    if (SelectedGroup != null)
        //    {
        //        viewModel.ViewModel.Item.Cost = SelectedGroup.Cost;
        //        viewModel.ViewModel.Item.Facility = SelectedGroup.Facility;
        //        viewModel.ViewModel.Item.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
        //        viewModel.ViewModel.Item.ProductionTerm = SelectedGroup.ProductionTerm;
        //        viewModel.ViewModel.Item.Product = SelectedGroup.Product;
        //        viewModel.ViewModel.Item.DeliveryDateExpected = SelectedGroup.DeliveryDateExpected;
        //        foreach (var prodIncl in SelectedGroup.ProductsIncluded)
        //        {
        //            var pi = new ProductIncluded { Product = prodIncl.Product.Model, Amount = prodIncl.Amount };
        //            viewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
        //        }
        //    }

        //    var result = Container.Resolve<IDialogService>().ShowDialog(viewModel);
        //    if (!result.HasValue || !result.Value)
        //        return;

        //    var wrappers = new List<SalesUnitWrapper>();
        //    for (int i = 0; i < viewModel.Amount; i++)
        //    {
        //        var unit = (SalesUnit)viewModel.ViewModel.Item.Model.Clone();
        //        unit.Id = Guid.NewGuid();
        //        unit.ProductsIncluded = new List<ProductIncluded>();
        //        var unitWrapper = new SalesUnitWrapper(unit);
        //        this.Item.Units.Add(unitWrapper);
        //        wrappers.Add(unitWrapper);
        //    }
        //    var group = new UnitsDatedGroup(wrappers);
        //    Groups.Add(group);
        //    await RefreshPrices();
        //    SelectedGroup = group;
        //}

        ///// <summary>
        ///// Загрузка при создании новой спецификации в соответствии с проектом
        ///// </summary>
        ///// <param name="specification"></param>
        ///// <param name="units"></param>
        ///// <returns></returns>
        //public async Task LoadAsync(Specification specification, IEnumerable<SalesUnit> units)
        //{
        //    WrapperDataService = Container.Resolve<IWrapperDataService>();

        //    //ищем юниты в текущем контексте
        //    var salesUnits = await WrapperDataService.GetWrapperRepository<SalesUnit, SalesUnitWrapper>().GetAllAsync();
        //    salesUnits = salesUnits.Where(x => units.Select(u => u.Id).Contains(x.Id));
        //    //исключаем юниты со спецификацией
        //    salesUnits = salesUnits.Where(x => x.Specification == null);

        //    //создаем новую спецификацию
        //    Item = new SpecificationWrapper(new Specification(), new List<SalesUnitWrapper>());
        //    Item.Date = DateTime.Today;
        //    Item.Units.AddRange(salesUnits);

        //    await AfterLoading();
        //}

        //protected override DateTime GetDate()
        //{
        //    var oitDate = Item.Units.Min(x => x.OrderInTakeDate);
        //    return oitDate < DateTime.Today ? oitDate : DateTime.Today;
        //}

        //protected override async void SaveCommand_Execute()
        //{
        //    //добавляем сущность, если ее не существовало
        //    if (await WrapperDataService.Repository<Specification>().GetByIdAsync(Item.Model.Id) == null)
        //        WrapperDataService.Repository<Specification>().Add(Item.Model);

        //    //фиксируем спецификацию
        //    Item.Units.ForEach(x => x.Model.Specification = Item.Model);
        //    //очищаем спецификацию в удаленных
        //    Item.Units.RemovedItems.ForEach(x => x.Specification = null);

        //    Item.AcceptChanges();
        //    Item.Units.RemovedItems.ForEach(x => x.AcceptChanges());
        //    await WrapperDataService.SaveChangesAsync();

        //    EventAggregator.GetEvent<AfterSaveSpecificationEvent>().Publish(Item.Model);

        //    //запрашиваем закрытие окна
        //    OnCloseRequested(new DialogRequestCloseEventArgs(true));
        //}
    }
}