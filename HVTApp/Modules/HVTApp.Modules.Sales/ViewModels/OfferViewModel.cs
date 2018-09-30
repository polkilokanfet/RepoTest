using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public interface IGroupsViewModel<TUnit>
    {
        Task LoadAsync(IEnumerable<TUnit> units);
        bool IsValid();
        bool IsChanged();
        void AcceptChanges();

        event Action GroupChanged;
    }

    public abstract class UnitsContainer<TModel, TWrapper, TDetailsViewModel, TGroupsViewModel, TUnit, TAfterSaveModelEvent> : ViewModelBase
        where TModel : class, IBaseEntity
        where TWrapper : class, IWrapper<TModel>
        where TDetailsViewModel : IDetailsViewModel<TWrapper, TModel>
        where TGroupsViewModel : IGroupsViewModel<TUnit>
        where TAfterSaveModelEvent : PubSubEvent<TModel>, new()
    {
        public TDetailsViewModel DetailsViewModel { get; }
        public TGroupsViewModel GroupsViewModel { get; set; }
        public ICommand SaveCommand { get; }

        protected UnitsContainer(IUnityContainer container) : base(container)
        {
            DetailsViewModel = container.Resolve<TDetailsViewModel>();
            GroupsViewModel = container.Resolve<TGroupsViewModel>();
            SaveCommand = new DelegateCommand(SaveCommandExecute, SaveCommandCanExecute);
        }

        public async Task LoadAsync(TModel model)
        {
            await DetailsViewModel.LoadAsync(model, UnitOfWork);
            await GroupsViewModel.LoadAsync(GetUnits(model));
            GroupsViewModel.GroupChanged += OnGroupChanged;
        }

        #region SaveCommand

        private async void SaveCommandExecute()
        {
            //������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.GroupChanged -= OnGroupChanged;

            //��������� ��������, ���� �� �� ������������
            if (await UnitOfWork.Repository<TModel>().GetByIdAsync(DetailsViewModel.Item.Model.Id) == null)
                UnitOfWork.Repository<TModel>().Add(DetailsViewModel.Item.Model);

            DetailsViewModel.Item.AcceptChanges();
            GroupsViewModel.AcceptChanges();

            //���������
            try
            {
                await UnitOfWork.SaveChangesAsync();
                Container.Resolve<IEventAggregator>().GetEvent<TAfterSaveModelEvent>().Publish(DetailsViewModel.Item.Model);
            }
            catch (DbUpdateConcurrencyException e)
            {
                var sb = new StringBuilder();
                Exception exception = e;
                do
                {
                    sb.AppendLine(e.Message);
                    exception = exception.InnerException;
                } while (exception != null);

                Container.Resolve<IMessageService>().ShowOkMessageDialog("������ ��� ����������", sb.ToString());
            }

            //����������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.GroupChanged += OnGroupChanged;

        }

        private bool SaveCommandCanExecute()
        {
            //��� �������� ������ ���� �������
            if (!GroupsViewModel.IsValid() || !DetailsViewModel.Item.IsValid)
                return false;

            //�����-�� �������� ������ ���� ��������
            return DetailsViewModel.Item.IsChanged || GroupsViewModel.IsChanged();
        }

        #endregion

        protected abstract IEnumerable<TUnit> GetUnits(TModel model);

        private void OnGroupChanged()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }

    public class OfferViewModel : OfferDetailsViewModel
    {
        public OfferUnitsGroupsViewModel GroupsViewModel { get; private set; }

        public OfferViewModel(IUnityContainer container) : base(container)
        {
        }

        private void InitGroupsViewModel(IEnumerable<OfferUnit> units)
        {
            GroupsViewModel = new OfferUnitsGroupsViewModel(Container, units, UnitOfWork, Item.Model);

            //����������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.Groups.PropertyChanged += GroupsOnPropertyChanged;
            this.GroupsViewModel.Groups.CollectionChanged += GroupsOnCollectionChanged;

            //������ �� ��������� ������
            OnPropertyChanged(nameof(GroupsViewModel));
        }

        private void GroupsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }

        private void GroupsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }


        //��� �������������� ������������� �����������
        protected override async Task AfterLoading()
        {
            //��������� ������ � �������������
            var units = UnitOfWork.Repository<OfferUnit>().Find(x => x.Offer.Id == Item.Id);
            InitGroupsViewModel(units);
            await GroupsViewModel.LoadAsync();
            await base.AfterLoading();
        }

        /// <summary>
        /// �������� ��� �� �������.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task LoadByProject(Project project)
        {
            GroupsViewModel = null;
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
            foreach (var salesUnit in salesUnits)
            {
                var offerUnit = new OfferUnit();

                offerUnit.Offer = Item.Model;
                offerUnit.Cost = salesUnit.Cost;
                offerUnit.Facility = salesUnit.Facility;
                offerUnit.Product = salesUnit.Product;
                offerUnit.PaymentConditionSet = salesUnit.PaymentConditionSet;
                offerUnit.ProductionTerm = salesUnit.ProductionTerm;
                offerUnit.ProductsIncluded = salesUnit.ProductsIncluded;

                offerUnits.Add(offerUnit);
            }
            await LoadOfferUnitsAsync(offerUnits);
        }

        /// <summary>
        /// �������� ��� �� ������� ���
        /// </summary>
        /// <param name="offer"></param>
        /// <returns></returns>
        public async Task LoadByOffer(Offer offer)
        {
            GroupsViewModel = null;
            UnitOfWork = Container.Resolve<IUnitOfWork>();
            Item = new OfferWrapper(new Offer());

            //����� ���
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
        /// �������� ��� �������� ������ �����������.
        /// </summary>
        /// <param name="offerUnits"></param>
        /// <returns></returns>
        private async Task LoadOfferUnitsAsync(IEnumerable<OfferUnit> offerUnits)
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

            //��������� ��������� � ������
            InitGroupsViewModel(new List<OfferUnit>());
            var groups = units.GroupBy(x => x, new OfferUnitsGroupsComparer()).OrderByDescending(x => x.Key.Cost).Select(x => new OfferUnitsGroup(x)).ToList();
            groups.ForEach(x =>
            {
                x.Offer = Item;
                GroupsViewModel.Groups.Add(x);
            });

            await GroupsViewModel.LoadAsync();

            await base.AfterLoading();
        }

        protected override async void SaveCommand_Execute()
        {

            //������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.Groups.PropertyChanged -= GroupsOnPropertyChanged;
            this.GroupsViewModel.Groups.CollectionChanged -= GroupsOnCollectionChanged;

            //��������� ��������, ���� �� �� ������������
            if (await UnitOfWork.Repository<Offer>().GetByIdAsync(Item.Model.Id) == null)
                UnitOfWork.Repository<Offer>().Add(Item.Model);

            Item.AcceptChanges();
            GroupsViewModel.AcceptChanges();

            //���������
            try
            {
                await UnitOfWork.SaveChangesAsync();
                EventAggregator.GetEvent<AfterSaveOfferEvent>().Publish(Item.Model);
            }
            catch (DbUpdateConcurrencyException e)
            {
                var sb = new StringBuilder();
                Exception exception = e;
                do
                {
                    sb.AppendLine(e.Message);
                    exception = exception.InnerException;
                } while (exception != null);

                Container.Resolve<IMessageService>().ShowOkMessageDialog("������ ��� ����������", sb.ToString());
            }

            //����������� �������� ����
            OnCloseRequested(new DialogRequestCloseEventArgs(true));

            //����������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.Groups.PropertyChanged += GroupsOnPropertyChanged;
            this.GroupsViewModel.Groups.CollectionChanged += GroupsOnCollectionChanged;
        }

        protected override bool SaveCommand_CanExecute()
        {
            //��� �������� ������ ���� �������
            if (GroupsViewModel == null || !GroupsViewModel.Groups.IsValid || !Item.IsValid)
                return false;

            //�����-�� �������� ������ ���� ��������
            return Item.IsChanged || GroupsViewModel.Groups.IsChanged;
        }
    }
}