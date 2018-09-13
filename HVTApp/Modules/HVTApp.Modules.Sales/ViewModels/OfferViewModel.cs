using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OfferViewModel : OfferDetailsViewModel
    {
        public OfferViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override IEnumerable<IUnitsGroup> GetGroups()
        {
            return Item.Units.ToProductUnitGroups();
        }

        protected override async void AddCommand_Execute()
        {
            var offerUnit = new OfferUnitWrapper(new OfferUnit() {Offer = Item.Model});
            var offerUnitsViewModel = new OfferUnitsViewModel(offerUnit, Container, WrapperDataService);
            if (SelectedGroup != null)
            {
                offerUnitsViewModel.ViewModel.Item.Cost = SelectedGroup.Cost;
                offerUnitsViewModel.ViewModel.Item.Facility = SelectedGroup.Facility;
                offerUnitsViewModel.ViewModel.Item.PaymentConditionSet = SelectedGroup.PaymentConditionSet;
                offerUnitsViewModel.ViewModel.Item.ProductionTerm = SelectedGroup.ProductionTerm;
                offerUnitsViewModel.ViewModel.Item.Product = SelectedGroup.Product;
                foreach (var prodIncl in SelectedGroup.ProductsIncluded)
                {
                    var pi = new ProductIncluded {Product = prodIncl.Product.Model, Amount = prodIncl.Amount};
                    offerUnitsViewModel.ViewModel.Item.ProductsIncluded.Add(new ProductIncludedWrapper(pi));
                }
            }

            var result = Container.Resolve<IDialogService>().ShowDialog(offerUnitsViewModel);
            if (!result.HasValue || !result.Value)
                return;

            var wrappers = new List<OfferUnitWrapper>();
            for (int i = 0; i < offerUnitsViewModel.Amount; i++)
            {
                var ou = (OfferUnit)offerUnitsViewModel.ViewModel.Item.Model.Clone();
                ou.Id = Guid.NewGuid();
                ou.ProductsIncluded = new List<ProductIncluded>();
                var ouw = new OfferUnitWrapper(ou);
                this.Item.Units.Add(ouw);
                wrappers.Add(ouw);
            }
            var group = new UnitsGroup(wrappers);
            Groups.Add(group);
            await RefreshPrices();
            SelectedGroup = group;
        }

        protected override DateTime GetDate()
        {
            return Item.RegistrationDetailsOfSender?.RegistrationDate ?? DateTime.Today;
        }

        /// <summary>
        /// «агрузка при создании нового предложени€.
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="offerUnits"></param>
        /// <returns></returns>
        public async Task LoadAsync(Offer offer, IEnumerable<OfferUnit> offerUnits)
        {
            WrapperDataService = Container.Resolve<IWrapperDataService>();

            var products = (await WrapperDataService.GetWrapperRepository<Product, ProductWrapper>().GetAllAsync()).ToList();
            var conditions = (await WrapperDataService.GetWrapperRepository<PaymentConditionSet, PaymentConditionSetWrapper>().GetAllAsync()).ToList();
            var facilities = (await WrapperDataService.GetWrapperRepository<Facility, FacilityWrapper>().GetAllAsync()).ToList();

            Item = new OfferWrapper(new Offer(), new List<OfferUnitWrapper>());

            if (offer.Author != null) Item.Author = await WrapperDataService.GetWrapperRepository<Employee, EmployeeWrapper>().GetByIdAsync(offer.Author.Id);
            if (offer.Author != null) Item.Project = await WrapperDataService.GetWrapperRepository<Project, ProjectWrapper>().GetByIdAsync(offer.Project.Id);
            if (offer.RecipientEmployee != null) Item.RecipientEmployee = await WrapperDataService.GetWrapperRepository<Employee, EmployeeWrapper>().GetByIdAsync(offer.RecipientEmployee.Id);
            if (offer.SenderEmployee != null) Item.SenderEmployee = await WrapperDataService.GetWrapperRepository<Employee, EmployeeWrapper>().GetByIdAsync(offer.SenderEmployee.Id);
            if (offer.RequestDocument != null) Item.RequestDocument = await WrapperDataService.GetWrapperRepository<Document, DocumentWrapper>().GetByIdAsync(offer.RequestDocument.Id);

            Item.Comment = offer.Comment;
            Item.Vat = offer.Vat;
            Item.ValidityDate = offer.ValidityDate;

            foreach (var offerUnit in offerUnits)
            {
                var wrap = new OfferUnitWrapper(new OfferUnit())
                {
                    Product = products.Single(x => x.Id == offerUnit.Product.Id),
                    PaymentConditionSet = conditions.Single(x => x.Id == offerUnit.PaymentConditionSet.Id),
                    Facility = facilities.Single(x => x.Id == offerUnit.Facility.Id),
                    Cost = offerUnit.Cost,
                    ProductionTerm = offerUnit.ProductionTerm
                };
                wrap.Model.Offer = Item.Model;
                foreach (var productIncluded in offerUnit.ProductsIncluded)
                {
                    var pi = new ProductIncludedWrapper(new ProductIncluded())
                    {
                        Product = products.Single(x => x.Id == productIncluded.Product.Id),
                        Amount = productIncluded.Amount
                    };
                    wrap.ProductsIncluded.Add(pi);
                }
                Item.Units.Add(wrap);
            }

            await AfterLoading();
        }
    }
}