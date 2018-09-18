using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OfferViewModel : OfferDetailsViewModel
    {
        public OfferUnitsGroupsViewModel GroupsViewModel { get; private set; }

        public OfferViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override async Task AfterLoading()
        {
            await base.AfterLoading();

            //загружаем строки с оборудованием
            var units = UnitOfWork.Repository<OfferUnit>().Find(x => x.Offer.Id == Item.Id);
            GroupsViewModel = new OfferUnitsGroupsViewModel(Container, units, UnitOfWork, Item.Model);
            await GroupsViewModel.LoadAsync();

            //регистрация на события изменения строк с оборудованием
            this.GroupsViewModel.Groups.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            this.GroupsViewModel.Groups.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            //сигнал об изменении модели
            OnPropertyChanged(nameof(GroupsViewModel));
        }


        /// <summary>
        /// Загрузка при создании нового предложения.
        /// </summary>
        /// <param name="offer"></param>
        /// <param name="offerUnits"></param>
        /// <returns></returns>
        public async Task LoadAsync(Offer offer, IEnumerable<OfferUnit> offerUnits)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //продукты, условия и объекты из базы
            var products = (await UnitOfWork.Repository<Product>().GetAllAsync()).Select(x => new ProductWrapper(x)).ToList();
            var conditions = (await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsync()).Select(x => new PaymentConditionSetWrapper(x)).ToList();
            var facilities = (await UnitOfWork.Repository<Facility>().GetAllAsync()).Select(x => new FacilityWrapper(x)).ToList();

            Item = new OfferWrapper(new Offer(), new List<OfferUnitWrapper>());

            if (offer.Author != null) Item.Author = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.Author.Id));
            if (offer.Author != null) Item.Project = new ProjectWrapper(await UnitOfWork.Repository<Project>().GetByIdAsync(offer.Project.Id));
            if (offer.RecipientEmployee != null) Item.RecipientEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.RecipientEmployee.Id));
            if (offer.SenderEmployee != null) Item.SenderEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.SenderEmployee.Id));
            if (offer.RequestDocument != null) Item.RequestDocument = new DocumentWrapper(await UnitOfWork.Repository<Document>().GetByIdAsync(offer.RequestDocument.Id));

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

        protected override async void SaveCommand_Execute()
        {
            await GroupsViewModel.SaveChanges();
            base.SaveCommand_Execute();
        }

        protected override bool SaveCommand_CanExecute()
        {
            //все сущности должны быть валидны
            if (GroupsViewModel == null || !GroupsViewModel.Groups.IsValid || !Item.IsValid)
                return false;

            //какая-то сущность должна быть изменена
            return Item.IsChanged || GroupsViewModel.Groups.IsChanged;
        }


    }
}