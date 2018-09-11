using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
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
            var offerUnit = new OfferUnit {Offer = Item.Model, Facility = Groups?.First().Facility.Model};
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
        /// Загрузка при создании нового предложения.
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="offerUnits"></param>
        /// <returns></returns>
        public async Task LoadAsync(Offer offer, IEnumerable<OfferUnit> offerUnits)
        {
            Item = new OfferWrapper(new Offer(), new List<OfferUnitWrapper>());

            if (offer.Author != null)
                Item.Author = await WrapperDataService.GetWrapperRepository<Employee, EmployeeWrapper>().GetByIdAsync(offer.Author.Id);
            if (offer.Author != null)
                Item.Project = await WrapperDataService.GetWrapperRepository<Project, ProjectWrapper>().GetByIdAsync(offer.Project.Id);
            if (offer.RecipientEmployee != null)
                Item.RecipientEmployee = await WrapperDataService.GetWrapperRepository<Employee, EmployeeWrapper>().GetByIdAsync(offer.RecipientEmployee.Id);
            if (offer.SenderEmployee != null)
                Item.SenderEmployee = await WrapperDataService.GetWrapperRepository<Employee, EmployeeWrapper>().GetByIdAsync(offer.SenderEmployee.Id);
            //if (offer.RegistrationDetailsOfRecipient != null)
            //    Item.RegistrationDetailsOfRecipient = await WrapperDataService.GetWrapperRepository<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>().GetByIdAsync(offer.RegistrationDetailsOfRecipient.Id);
            //if (offer.RegistrationDetailsOfSender != null)
            //    Item.RegistrationDetailsOfSender = await WrapperDataService.GetWrapperRepository<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>().GetByIdAsync(offer.RegistrationDetailsOfSender.Id);
            if (offer.RequestDocument != null)
                Item.RequestDocument = await WrapperDataService.GetWrapperRepository<Document, DocumentWrapper>().GetByIdAsync(offer.RequestDocument.Id);
            Item.Comment = offer.Comment;
            Item.Vat = offer.Vat;
            Item.ValidityDate = offer.ValidityDate;

            Item.Units.AddRange(offerUnits.Select(x => new OfferUnitWrapper(x)));
            await AfterLoading();
        }

        protected override async void SaveCommand_Execute()
        {
            //добавляем сущность, если ее не существовало
            if (await WrapperDataService.GetRepository<Offer>().GetByIdAsync(Item.Model.Id) == null)
                WrapperDataService.GetWrapperRepository<Offer, OfferWrapper>().Add(Item);

            //добавляем созданные юниты и удаляем удаленные
            //WrapperDataService.GetRepository<OfferUnit>().AddRange(Item.Units.AddedItems.Select(x => x.Model));
            //WrapperDataService.GetRepository<OfferUnit>().DeleteRange(Item.Units.RemovedItems.Select(x => x.Model));

            Item.AcceptChanges();
            await WrapperDataService.SaveChangesAsync();

            EventAggregator.GetEvent<AfterSaveOfferEvent>().Publish(Item.Model);

            //запрашиваем закрытие окна
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }
    }
}