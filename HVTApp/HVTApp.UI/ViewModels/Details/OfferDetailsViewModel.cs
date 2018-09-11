using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class OfferDetailsViewModel : WrapperWithUnitsViewModel<OfferWrapper, Offer, OfferUnit, OfferUnitWrapper, AfterSaveOfferEvent>
    {
        protected override async void AddCommand_Execute()
        {
            var offerUnit = new OfferUnit {Offer = Item.Model, Facility = Groups?.FirstOrDefault()?.Facility.Model};
            if (SelectedGroup != null)
            {
                offerUnit.Cost = SelectedGroup.Cost;
                offerUnit.Facility = SelectedGroup.Facility.Model;
                offerUnit.PaymentConditionSet = SelectedGroup.PaymentConditionSet.Model;
                offerUnit.ProductionTerm = SelectedGroup.ProductionTerm;
                offerUnit.Product = SelectedGroup.Product.Model;
                foreach (var prodIncl in SelectedGroup.ProductsIncluded)
                {
                    offerUnit.ProductsIncluded.Add(new ProductIncluded {Product = prodIncl.Product.Model, Amount = prodIncl.Amount});
                }
            }
            var unit = await Container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(offerUnit);
            if (unit == null) return;

            unit.Product = await WrapperDataService.GetRepository<Product>().GetByIdAsync(unit.Product.Id);
            unit.Facility = await WrapperDataService.GetRepository<Facility>().GetByIdAsync(unit.Facility.Id);
            unit.PaymentConditionSet = await WrapperDataService.GetRepository<PaymentConditionSet>().GetByIdAsync(unit.PaymentConditionSet.Id);

            var wrapper = new OfferUnitWrapper(unit);
            Groups.Add(new UnitsGroup(new[] { wrapper }));
            Item.Units.Add(wrapper);
            WrapperDataService.GetRepository<OfferUnit>().Add(unit);
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
            WrapperDataService?.Dispose();
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