using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
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

        private async Task InitGroupsViewModel(IEnumerable<OfferUnit> units)
        {
            GroupsViewModel = new OfferUnitsGroupsViewModel(Container, units, UnitOfWork, Item.Model);

            //регистрация на события изменения строк с оборудованием
            this.GroupsViewModel.Groups.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            this.GroupsViewModel.Groups.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            //сигнал об изменении модели
            OnPropertyChanged(nameof(GroupsViewModel));
        }

        protected override async Task AfterLoading()
        {
            //загружаем строки с оборудованием
            var units = UnitOfWork.Repository<OfferUnit>().Find(x => x.Offer.Id == Item.Id);
            await InitGroupsViewModel(units);
            await base.AfterLoading();
        }

        /// <summary>
        /// Создание ТКП по проекту.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task LoadByProject(Project project)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            project = await UnitOfWork.Repository<Project>().GetByIdAsync(project.Id);
            var author = await UnitOfWork.Repository<Employee>().GetByIdAsync(CommonOptions.User.Employee.Id);
            Item = new OfferWrapper(new Offer())
            {
                Project = new ProjectWrapper(project),
                ValidityDate = DateTime.Today.AddDays(90),
                Author = new EmployeeWrapper(author)
            };

            var salesUnits = (await UnitOfWork.Repository<SalesUnit>().GetAllAsync()).Where(x => x.Project.Id == project.Id);
            var offerUnits = new List<OfferUnit>();
            foreach (var unit in salesUnits)
            {
                var offerUnit = new OfferUnit
                {
                    Offer = Item.Model,
                    Cost = unit.Cost,
                    Facility = unit.Facility,
                    Product = unit.Product,
                    PaymentConditionSet = unit.PaymentConditionSet,
                    ProductionTerm = unit.ProductionTerm,
                    ProductsIncluded = unit.ProductsIncluded
                };
                offerUnits.Add(offerUnit);
            }

            await LoadOfferUnitsAsync(offerUnits);
        }

        /// <summary>
        /// Создание ТКП по другому ТКП
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public async Task LoadByOffer(Offer offer)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            Item = new OfferWrapper(new Offer());

            //копия ТКП
            if (offer.Author != null) Item.Author = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.Author.Id));
            if (offer.Project != null) Item.Project = new ProjectWrapper(await UnitOfWork.Repository<Project>().GetByIdAsync(offer.Project.Id));
            if (offer.RecipientEmployee != null) Item.RecipientEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.RecipientEmployee.Id));
            if (offer.SenderEmployee != null) Item.SenderEmployee = new EmployeeWrapper(await UnitOfWork.Repository<Employee>().GetByIdAsync(offer.SenderEmployee.Id));
            if (offer.RequestDocument != null) Item.RequestDocument = new DocumentWrapper(await UnitOfWork.Repository<Document>().GetByIdAsync(offer.RequestDocument.Id));
            Item.Comment = offer.Comment;
            Item.Vat = offer.Vat;
            Item.ValidityDate = offer.ValidityDate;

            var offerUnits = (await UnitOfWork.Repository<OfferUnit>().GetAllAsync()).Where(x => x.Offer.Id == offer.Id);
            await LoadOfferUnitsAsync(offerUnits);
        }

        /// <summary>
        /// Загрузка при создании нового предложения.
        /// </summary>
        /// <param name="offerUnits"></param>
        /// <returns></returns>
        private async Task LoadOfferUnitsAsync(IEnumerable<OfferUnit> offerUnits)
        {
            //продукты, условия и объекты из базы
            var products = (await UnitOfWork.Repository<Product>().GetAllAsync());
            var conditions = (await UnitOfWork.Repository<PaymentConditionSet>().GetAllAsync());
            var facilities = (await UnitOfWork.Repository<Facility>().GetAllAsync());

            //копия единиц с оборудованием
            var units = new List<OfferUnit>();
            foreach (var offerUnit in offerUnits)
            {
                var unit = new OfferUnit
                {
                    Offer = Item.Model,
                    Product = products.Single(x => x.Id == offerUnit.Product.Id),
                    PaymentConditionSet = conditions.Single(x => x.Id == offerUnit.PaymentConditionSet.Id),
                    Facility = facilities.Single(x => x.Id == offerUnit.Facility.Id),
                    Cost = offerUnit.Cost,
                    ProductionTerm = offerUnit.ProductionTerm
                };
                foreach (var productIncluded in offerUnit.ProductsIncluded)
                {
                    var pi = new ProductIncluded()
                    {
                        Product = products.Single(x => x.Id == productIncluded.Product.Id),
                        Amount = productIncluded.Amount
                    };
                    unit.ProductsIncluded.Add(pi);
                }
                units.Add(unit);
            }

            //добавляем созданное в группы
            await InitGroupsViewModel(new List<OfferUnit>());
            var groups = units.GroupBy(x => x, new OfferUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new OfferUnitsGroup(x)).ToList();
            groups.ForEach(GroupsViewModel.Groups.Add);

            await GroupsViewModel.LoadAsync();


            await base.AfterLoading();
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