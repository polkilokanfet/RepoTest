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
                this.DetailsViewModel.Item.Comment = $"��� ({facilities.ToStringEnum()})";
            }
        }

        protected override IEnumerable<OfferUnit> GetUnits(Offer offer, object parameter = null)
        {
            //��� �������������� ������������� ���
            if (parameter == null)
                return ((IOfferUnitRepository) UnitOfWork.Repository<OfferUnit>()).GetByOffer(offer);

            //��� �������� ��� �� �������
            if (parameter is Project project)
                return LoadByProject(project);

            //��� �������� ��� �� ������� ���
            var offerTemplate = parameter as Offer;
            return LoadByOffer(offerTemplate);
        }


        /// <summary>
        /// �������� ��� �� �������.
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
        /// �������� ��� �� ������� ���
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        private IEnumerable<OfferUnit> LoadByOffer(Offer offer)
        {
            //����� ���
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
        ///// �������� ��� �������� ������ �����������.
        ///// </summary>
        ///// <param name="offerUnits"></param>
        ///// <returns></returns>
        //private async Task LoadOfferUnitsAsync(IEnumerable<OfferUnit> offerUnits)
        //{
        //    //����� ������ � �������������
        //    var units = new List<OfferUnit>();
        //    foreach (var offerUnit in offerUnits)
        //    {
        //        //��������� ��������
        //        var offerUnitNew = new OfferUnit();
        //        //������ ��������� �������� �� ������� �������� ���������
        //        offerUnitNew.Cost = offerUnit.Cost;
        //        offerUnitNew.ProductionTerm = offerUnit.ProductionTerm;
        //        offerUnitNew.Product = await UnitOfWork.Repository<Product>().GetByIdAsync(offerUnit.Product.Id);
        //        offerUnitNew.PaymentConditionSet = await UnitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(offerUnit.PaymentConditionSet.Id);
        //        offerUnitNew.Facility = await UnitOfWork.Repository<Facility>().GetByIdAsync(offerUnit.Facility.Id);

        //        //����� ����������� ������������
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

        //    //��������� ��������� � ������
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
            //����� ������ � �������������
            var offerUnitsDictionary = new Dictionary<OfferUnit, OfferUnit>();
            foreach (var offerUnit in offerUnits)
            {
                //��������� ��������
                //������ ��������� �������� �� ������� �������� ���������
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
            
            //����� ����������� ������������
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