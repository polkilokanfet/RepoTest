using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    //public class SalesUnitViewItem
    //{
    //    private Facility _facility;
    //    private Product _product;
    //    private double _cost;
    //    public Guid Id { get; }

    //    public Facility Facility
    //    {
    //        get { return _facility; }
    //        set { _facility = value; }
    //    }

    //    public Product Product
    //    {
    //        get { return _product; }
    //        set { _product = value; }
    //    }

    //    public double Cost
    //    {
    //        get { return _cost; }
    //        set { _cost = value; }
    //    }

    //    public SalesUnitViewItem(SalesUnit salesUnit)
    //    {
    //        Id = salesUnit.Id;
    //        Facility = salesUnit.Facility;
    //        Product = salesUnit.Product;
    //        Cost = salesUnit.Cost;
    //    }
    //}

    //public class ProjectViewItem
    //{
    //    public ProjectViewItem(Project project,
    //                         IEnumerable<SalesUnit> salesUnits,
    //                         IEnumerable<Tender> tenders,
    //                         IUnityContainer container) : this(project)
    //    {
    //        SalesUnits = new List<SalesUnitLookup>(salesUnits.Select(x => new SalesUnitLookup(x)));
    //        Tenders = new List<TenderLookup>(tenders.Select(x => new TenderLookup(x)));

    //        var eventAggregator = container.Resolve<IEventAggregator>();

    //        eventAggregator.GetEvent<AfterSaveSalesUnitEvent>().Subscribe(salesUnit =>
    //        {
    //            if (salesUnit.Project.Id != Id) return;
    //            SalesUnits.ReAddById(new SalesUnitLookup(salesUnit));
    //            Refresh();
    //        });

    //        eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
    //        {
    //            if (tender.Project.Id != Id) return;
    //            Tenders.ReAddById(new TenderLookup(tender));
    //            Refresh();
    //        });

    //        eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(salesUnit =>
    //        {
    //            if (salesUnit.Project.Id != Id) return;
    //            SalesUnits.RemoveIfContainsById(salesUnit);
    //            Refresh();
    //        });

    //        eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(tender =>
    //        {
    //            if (tender.Project?.Id != Id) return;
    //            Tenders.RemoveIfContainsById(tender);
    //            Refresh();
    //        });
    //    }

    //    public List<SalesUnitLookup> SalesUnits { get; } = new List<SalesUnitLookup>();
    //    public List<TenderLookup> Tenders { get; } = new List<TenderLookup>();
    //    public List<OfferLookup> Offers { get; } = new List<OfferLookup>();

    //    [Designation("Сумма проекта"), OrderStatus(7)]
    //    public double Sum => SalesUnits.Sum(x => x.Cost);

    //    [Designation("Дата поставки"), OrderStatus(6)]
    //    public DateTime RealizationDate => SalesUnits.Any() ? SalesUnits.Select(x => x.DeliveryDateExpected).Min() : DateTime.Today.AddMonths(6);

    //    [Designation("Тендер"), OrderStatus(5)]
    //    public DateTime? TenderDate
    //    {
    //        get
    //        {
    //            if (!Tenders.Any()) return null;
    //            var supply = Tenders.Where(x => x.Entity.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).ToList();
    //            return !supply.Any() ? null : supply.OrderBy(x => x.DateClose).Last()?.DateClose;
    //        }
    //    }

    //    [Designation("Объекты"), OrderStatus(10)]
    //    public List<FacilityLookup> Facilities => SalesUnits?.Select(x => x.Facility).Distinct(new FacilityComparer()).ToList();


    //    [Designation("Подрядчик"), OrderStatus(4)]
    //    public CompanyLookup Builder
    //    {
    //        get
    //        {
    //            if (Tenders.Any())
    //            {
    //                var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToWork)).OrderBy(x => x.DateClose);
    //                return tenders.LastOrDefault()?.Winner;
    //            }
    //            return null;
    //        }
    //    }

    //    [Designation("Проектировщик"), OrderStatus(3)]
    //    public CompanyLookup ProjectMaker
    //    {
    //        get
    //        {
    //            if (Tenders.Any())
    //            {
    //                var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToProject)).OrderBy(x => x.DateClose);
    //                return tenders.LastOrDefault()?.Winner;
    //            }
    //            return null;
    //        }
    //    }

    //    [Designation("Поставщик"), OrderStatus(2)]
    //    public CompanyLookup Sypplier
    //    {
    //        get
    //        {
    //            if (Tenders.Any())
    //            {
    //                var tenders = Tenders.Where(x => x.Types.Select(t => t.Type).Contains(TenderTypeEnum.ToSupply)).OrderBy(x => x.DateClose);
    //                return tenders.LastOrDefault()?.Winner;
    //            }
    //            return null;
    //        }
    //    }

    //    [Designation("Done"), OrderStatus(-8)]
    //    public bool IsDone => SalesUnits.All(x => x.IsDone);

    //    [Designation("Проигран"), OrderStatus(-10)]
    //    public bool IsLoosen => SalesUnits.All(x => x.IsLoosen);

    //    public override int CompareTo(object other)
    //    {
    //        return RealizationDate.CompareTo(((ProjectLookup)other).RealizationDate);
    //    }

    //    internal class FacilityComparer : IEqualityComparer<FacilityLookup>
    //    {
    //        public bool Equals(FacilityLookup x, FacilityLookup y)
    //        {
    //            return y != null && (x != null && x.Id == y.Id);
    //        }

    //        public int GetHashCode(FacilityLookup obj)
    //        {
    //            return 0;
    //        }
    //    }
    //}


    public partial class Market2ViewModel : ViewModelBase
    {
        public ProjectsContainer Projects { get; }
        public OffersContainer Offers { get; }
        public TendersContainer Tenders { get; }
        public SalesUnitsProjectBase SalesUnits { get; }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {

            Projects = container.Resolve<ProjectsContainer>();
            Offers = container.Resolve<OffersContainer>();
            Tenders = container.Resolve<TendersContainer>();
            SalesUnits = container.Resolve<SalesUnitsProjectBase>();

            #region Commands definition
            
            //команды
            NewProjectCommand = new DelegateCommand(NewProjectCommand_Execute);
            EditProjectCommand = new DelegateCommand(EditProjectCommand_Execute, () => Projects.SelectedItem != null);
            RemoveProjectCommand = new DelegateCommand(async () => await Projects.RemoveSelectedItemTask(), () => Projects.SelectedItem != null);

            NewSpecificationCommand = new DelegateCommand(NewSpecificationCommand_Execute, () => Projects.SelectedItem != null);

            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => Offers.SelectedItem != null);
            RemoveOfferCommand = new DelegateCommand(async () => await Offers.RemoveSelectedItemTask(), () => Offers.SelectedItem != null);
            PrintOfferCommand = new DelegateCommand(PrintOfferCommand_Execute, () => Offers.SelectedItem != null);
            NewOfferByProjectCommand = new DelegateCommand(NewOfferByProjectCommand_Execute, () => Projects.SelectedItem != null);
            NewOfferByOfferCommand = new DelegateCommand(NewOfferByOfferCommand_Execute, () => Offers.SelectedItem != null);

            NewTenderCommand = new DelegateCommand(NewTenderCommand_Execute, () => Projects.SelectedItem != null);
            EditTenderCommand = new DelegateCommand(EditTenderCommand_Execute, () => Tenders.SelectedItem != null);
            RemoveTenderCommand = new DelegateCommand(async () => await Tenders.RemoveSelectedItemTask(), () => Tenders.SelectedItem != null);

            StructureCostsCommand = new DelegateCommand(StructureCostsCommand_Execute, () => Projects.SelectedItem != null);

            #endregion

            #region Subscribe to Events

            //подписка на выбор сущностей
            var eventAggregator = container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<SelectedProjectChangedEvent>().Subscribe(project => ProjectRaiseCanExecuteChanged());
            eventAggregator.GetEvent<SelectedOfferChangedEvent>().Subscribe(offer => OfferRaiseCanExecuteChanged());
            eventAggregator.GetEvent<SelectedTenderChangedEvent>().Subscribe(tender => TenderRaiseCanExecuteChanged());

            #endregion
        }

        #region RaiseCanExecuteChanged

        private void ProjectRaiseCanExecuteChanged()
        {
            ((DelegateCommand)RemoveProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditProjectCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewSpecificationCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)StructureCostsCommand).RaiseCanExecuteChanged();
            OfferRaiseCanExecuteChanged();
            TenderRaiseCanExecuteChanged();
        }

        private void OfferRaiseCanExecuteChanged()
        {
            ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)PrintOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewOfferByOfferCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)NewOfferByProjectCommand).RaiseCanExecuteChanged();
        }

        private void TenderRaiseCanExecuteChanged()
        {
            ((DelegateCommand)NewTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)EditTenderCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RemoveTenderCommand).RaiseCanExecuteChanged();
        }

        #endregion
    }
}
