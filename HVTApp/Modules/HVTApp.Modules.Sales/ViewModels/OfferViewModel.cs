using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OfferViewModel : UnitsContainer<Offer, OfferWrapper, OfferDetailsViewModel, OfferUnitsGroupsViewModel, OfferUnit, AfterSaveOfferEvent>
    {

        public OfferViewModel(IUnityContainer container) : base(container)
        {
        }


        protected override async Task<IEnumerable<OfferUnit>> GetUnits(Offer offer, object parameter = null)
        {
            //��� �������������� ������������� ���
            if (parameter == null)
                return UnitOfWork.Repository<OfferUnit>().Find(x => x.Offer.Id == offer.Id);

            //��� �������� ��� �� �������
            var project = parameter as Project;
            if (project != null)
                return await LoadByProject(project);

            //��� �������� ��� �� ������� ���
            var offerTemplate = parameter as Offer;
            return await LoadByOffer(offerTemplate);
        }


        /// <summary>
        /// �������� ��� �� �������.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        private async Task<IEnumerable<OfferUnit>> LoadByProject(Project project)
        {
            project = await UnitOfWork.Repository<Project>().GetByIdAsync(project.Id);
            var author = await UnitOfWork.Repository<Employee>().GetByIdAsync(GlobalAppProperties.User.Employee.Id);
            var sender = await UnitOfWork.Repository<Employee>().GetByIdAsync(GlobalAppProperties.Actual.SenderOfferEmployee.Id);

            DetailsViewModel.Item.Project = new ProjectWrapper(project);
            DetailsViewModel.Item.ValidityDate = DateTime.Today.AddDays(90);
            DetailsViewModel.Item.Author = new EmployeeWrapper(author);
            DetailsViewModel.Item.SenderEmployee = new EmployeeWrapper(sender);

            var salesUnits =  UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id);
            var offerUnits = new List<OfferUnit>();
            foreach (var salesUnit in salesUnits)
            {
                var offerUnit = new OfferUnit();

                offerUnit.Cost = salesUnit.Cost;
                offerUnit.Facility = salesUnit.Facility;
                offerUnit.Product = salesUnit.Product;
                offerUnit.PaymentConditionSet = salesUnit.PaymentConditionSet;
                offerUnit.ProductionTerm = salesUnit.ProductionTerm;
                offerUnit.ProductsIncluded = salesUnit.ProductsIncluded;

                offerUnits.Add(offerUnit);
            }

            return await CloneOfferUnitsAsync(offerUnits);
        }

        /// <summary>
        /// �������� ��� �� ������� ���
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        private async Task<IEnumerable<OfferUnit>> LoadByOffer(Offer offer)
        {
            //����� ���
            if (offer.Author != null) DetailsViewModel.Item.Author = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.Author.Id));
            if (offer.Project != null) DetailsViewModel.Item.Project = new ProjectWrapper(await UnitOfWork.Repository<Project>().GetByIdAsync(offer.Project.Id));
            if (offer.RecipientEmployee != null) DetailsViewModel.Item.RecipientEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.RecipientEmployee.Id));
            if (offer.SenderEmployee != null) DetailsViewModel.Item.SenderEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.SenderEmployee.Id));
            if (offer.RequestDocument != null) DetailsViewModel.Item.RequestDocument = new DocumentWrapper(await UnitOfWork.Repository<Document>().GetByIdAsync(offer.RequestDocument.Id));
            DetailsViewModel.Item.Comment = offer.Comment;
            DetailsViewModel.Item.Vat = offer.Vat;
            DetailsViewModel.Item.ValidityDate = offer.ValidityDate;

            var offerUnits = UnitOfWork.Repository<OfferUnit>().Find(x => x.Offer.Id == offer.Id);
            return await CloneOfferUnitsAsync(offerUnits);
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

        private async Task<IEnumerable<OfferUnit>> CloneOfferUnitsAsync(IEnumerable<OfferUnit> offerUnits)
        {
            //����� ������ � �������������
            var units = new List<OfferUnit>();
            foreach (var offerUnit in offerUnits)
            {
                //��������� ��������
                var offerUnitNew = new OfferUnit();
                //������ ��������� �������� �� ������� �������� ���������
                offerUnitNew.Cost = offerUnit.Cost;
                offerUnitNew.ProductionTerm = offerUnit.ProductionTerm;
                offerUnitNew.Product = await UnitOfWork.Repository<Product>().GetByIdAsync(offerUnit.Product.Id);
                offerUnitNew.PaymentConditionSet = await UnitOfWork.Repository<PaymentConditionSet>().GetByIdAsync(offerUnit.PaymentConditionSet.Id);
                offerUnitNew.Facility = await UnitOfWork.Repository<Facility>().GetByIdAsync(offerUnit.Facility.Id);

                //����� ����������� ������������
                offerUnitNew.ProductsIncluded = new List<ProductIncluded>();
                foreach (var productIncluded in offerUnit.ProductsIncluded)
                {
                    var productIncludedNew = new ProductIncluded
                    {
                        Product = await UnitOfWork.Repository<Product>().GetByIdAsync(productIncluded.Product.Id),
                        Amount = productIncluded.Amount
                    };
                    offerUnitNew.ProductsIncluded.Add(productIncludedNew);
                }
                units.Add(offerUnitNew);
            }
            return units;
        }
    }
}