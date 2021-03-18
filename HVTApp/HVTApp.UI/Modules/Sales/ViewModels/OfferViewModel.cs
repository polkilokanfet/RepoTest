using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class OfferViewModel : UnitsContainer<Offer, OfferWrapper, OfferDetailsViewModel, OfferUnitsGroupsViewModel, OfferUnit, AfterSaveOfferEvent>
    {

        public OfferViewModel(IUnityContainer container) : base(container)
        {
        }

        public override void AfterUnitsLoading()
        {
            if (string.IsNullOrEmpty(this.DetailsViewModel.Item.Comment))
            {
                var facilities = this.GroupsViewModel.Groups.Select(x => x.Facility).Distinct();
                this.DetailsViewModel.Item.Comment = $"ТКП ({facilities.ToStringEnum()})";
            }
        }

        protected override IEnumerable<OfferUnit> GetUnits(Offer offer, object parameter = null)
        {
            //при редактировании существующего ТКП
            if (parameter == null)
                return ((IOfferUnitRepository) UnitOfWork.Repository<OfferUnit>()).GetByOffer(offer);

            //при создании ТКП по проекту
            if (parameter is Project project)
                return LoadByProject(project);

            //при создании ТКП по другому ТКП
            var offerTemplate = parameter as Offer;
            return LoadByOffer(offerTemplate);
        }


        /// <summary>
        /// Создание ТКП по проекту.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private IEnumerable<OfferUnit> LoadByProject(Project project)
        {
            project = UnitOfWork.Repository<Project>().GetById(project.Id);
            var author = UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.User.Employee.Id);
            var sender = UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.SenderOfferEmployee.Id);

            DetailsViewModel.Item.Project = new ProjectWrapper(project);
            DetailsViewModel.Item.ValidityDate = DateTime.Today.AddDays(90);
            DetailsViewModel.Item.Author = new EmployeeWrapper(author);
            DetailsViewModel.Item.SenderEmployee = new EmployeeWrapper(sender);

            IEnumerable<SalesUnit> salesUnits = ((ISalesUnitRepository)UnitOfWork.Repository<SalesUnit>()).GetByProject(project.Id).Where(salesUnit => !salesUnit.IsRemoved);
            var offerUnits = new List<OfferUnit>();
            foreach (var salesUnit in salesUnits)
            {
                var offerUnit = new OfferUnit
                {
                    Cost = salesUnit.Cost,
                    Comment = salesUnit.Comment,
                    Facility = salesUnit.Facility,
                    Product = salesUnit.Product,
                    CostDelivery = salesUnit.CostDelivery,
                    PaymentConditionSet = salesUnit.PaymentConditionSet,
                    ProductionTerm = salesUnit.ProductionTerm,
                    ProductsIncluded = salesUnit.ProductsIncluded
                };

                offerUnits.Add(offerUnit);
            }

            return CloneOfferUnits(offerUnits);
        }

        /// <summary>
        /// Создание ТКП по другому ТКП
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        private IEnumerable<OfferUnit> LoadByOffer(Offer offer)
        {
            //копия ТКП
            if (offer.Author != null) DetailsViewModel.Item.Author = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(offer.Author.Id));
            if (offer.Project != null) DetailsViewModel.Item.Project = new ProjectWrapper(UnitOfWork.Repository<Project>().GetById(offer.Project.Id));
            if (offer.RecipientEmployee != null) DetailsViewModel.Item.RecipientEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(offer.RecipientEmployee.Id));
            if (offer.SenderEmployee != null) DetailsViewModel.Item.SenderEmployee = new EmployeeWrapper(UnitOfWork.Repository<Employee>().GetById(offer.SenderEmployee.Id));
            if (offer.RequestDocument != null) DetailsViewModel.Item.RequestDocument = new DocumentWrapper(UnitOfWork.Repository<Document>().GetById(offer.RequestDocument.Id));
            DetailsViewModel.Item.Comment = offer.Comment;
            DetailsViewModel.Item.Vat = offer.Vat;
            DetailsViewModel.Item.ValidityDate = offer.ValidityDate;

            var offerUnits = ((IOfferUnitRepository)UnitOfWork.Repository<OfferUnit>()).GetByOffer(offer);
            return CloneOfferUnits(offerUnits);
        }

        ///// <summary>
        ///// Загрузка при создании нового предложения.
        ///// </summary>
        ///// <param name="offerUnits"></param>
        ///// <returns></returns>
        //private async Task LoadOfferUnitsAsync(IEnumerable<OfferUnit> offerUnits)
        //{
        //    //копия единиц с оборудованием
        //    var units = new List<OfferUnit>();
        //    foreach (var offerUnit in offerUnits)
        //    {
        //        //клонируем входящий
        //        var offerUnitNew = new OfferUnit();
        //        //меняем ссылочные свойства на объекты текущего контекста
        //        offerUnitNew.Cost = offerUnit.Cost;
        //        offerUnitNew.ProductionTerm = offerUnit.ProductionTerm;
        //        offerUnitNew.Product = await UnitOfWork.Repository<Product>().GetByIdAsync(offerUnit.Product.Id);
        //        offerUnitNew.PaymentConditionSet = await UnitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(offerUnit.PaymentConditionSet.Id);
        //        offerUnitNew.Facility = await UnitOfWork.Repository<Facility>().GetByIdAsync(offerUnit.Facility.Id);

        //        //копия включенного оборудования
        //        offerUnitNew.ProductsIncluded = new List<ProductIncluded>();
        //        foreach (var productIncluded in offerUnit.ProductsIncluded)
        //        {
        //            var productIncludedNew = new ProductIncluded
        //            {
        //                Product = await UnitOfWork.Repository<Product>().GetByIdAsync(productIncluded.Product.Id),
        //                Amount = productIncluded.Amount
        //            };
        //            offerUnitNew.ProductsIncluded.Add(productIncludedNew);
        //        }
        //        units.Add(offerUnitNew);
        //    }

        //    //добавляем созданное в группы
        //    InitGroupsViewModel(new List<OfferUnit>());
        //    var groups = units.GroupBy(x => x, new OfferUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new OfferUnitsGroup(x)).ToList();
        //    groups.ForEach(x =>
        //    {
        //        x.Offer = Item;
        //        GroupsViewModel.Groups.Add(x);
        //    });

        //    await GroupsViewModel.LoadAsync();

        //    await base.AfterLoading();
        //}

        private IEnumerable<OfferUnit> CloneOfferUnits(IEnumerable<OfferUnit> offerUnits)
        {
            //копия единиц с оборудованием
            var offerUnitsDictionary = new Dictionary<OfferUnit, OfferUnit>();
            foreach (var offerUnit in offerUnits)
            {
                //клонируем входящий
                //меняем ссылочные свойства на объекты текущего контекста
                var offerUnitNew = new OfferUnit
                {
                    Cost = offerUnit.Cost,
                    Comment = offerUnit.Comment,
                    CostDelivery = offerUnit.CostDelivery,
                    ProductionTerm = offerUnit.ProductionTerm,
                    Product = UnitOfWork.Repository<Product>().GetById(offerUnit.Product.Id),
                    PaymentConditionSet = UnitOfWork.Repository<PaymentConditionSet>().GetById(offerUnit.PaymentConditionSet.Id),
                    Facility = UnitOfWork.Repository<Facility>().GetById(offerUnit.Facility.Id)
                };

                offerUnitsDictionary.Add(offerUnit, offerUnitNew);
            }
            
            //копия включенного оборудования
            var productsIncluded = offerUnits.SelectMany(x => x.ProductsIncluded).Distinct().ToList();
            foreach (var productIncluded in productsIncluded)
            {
                var productIncludedNew = new ProductIncluded
                {
                    Product = UnitOfWork.Repository<Product>().GetById(productIncluded.Product.Id),
                    Amount = productIncluded.Amount
                };

                var targetNewOfferUnits = offerUnitsDictionary
                    .Where(x => x.Key.ProductsIncluded.Contains(productIncluded))
                    .Select(x => x.Value)
                    .ToList();

                targetNewOfferUnits.ForEach(x => x.ProductsIncluded.Add(productIncludedNew));
            }

            return offerUnitsDictionary.Select(x => x.Value);
        }
    }
}