using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Market.Items
{
    //public class ProjectItem : BindableBase
    //{
    //    private Project _project;

    //    public static List<Tender> AllTenders { get; } = new List<Tender>();
    //    private IEnumerable<Tender> Tenders => AllTenders.Where(tender => tender.DidNotTakePlace == false && tender.Project.Id == Project.Id);

    //    public SalesUnitsCollection SalesUnits { get; }

    //    public IEnumerable<ProjectUnitsGroup> ProjectUnitsGroups
    //    {
    //        get
    //        {
    //            return SalesUnits
    //                .GroupBy(salesUnit => new
    //                {
    //                    ProductId = salesUnit.Product.Id,
    //                    Cost = salesUnit.Cost,
    //                    FacilityId = salesUnit.Facility.Id,
    //                    OrderInTakeDate = salesUnit.OrderInTakeDate,
    //                    RealizationDateCalculated = salesUnit.RealizationDateCalculated,
    //                    Comment = salesUnit.Comment
    //                })
    //                .OrderByDescending(x => x.Key.Cost)
    //                .Select(x => new ProjectUnitsGroup(x, this));
    //        }
    //    }

    //    public Project Project
    //    {
    //        get => _project;
    //        set
    //        {
    //            this.SetProperty(ref _project, value, () =>
    //            {
    //                RaisePropertyChanged(nameof(this.InWork));
    //                RaisePropertyChanged(nameof(this.ForReport));
    //                RaisePropertyChanged();
    //            });
    //        }
    //    }

    //    public IEnumerable<Facility> Facilities => SalesUnits.Select(salesUnit => salesUnit.Facility).Distinct().OrderBy(facility => facility.Name);
    //    public double Sum => SalesUnits.Sum(salesUnit => salesUnit.Cost);

    //    public int? DaysToStartProduction
    //    {
    //        get
    //        {
    //            if (!SalesUnits.Any()) return null;

    //            var salesUnit = SalesUnits.First();
    //            if (salesUnit.IsLoosen || salesUnit.IsWon) return null;

    //            return (salesUnit.DeliveryDateExpected.AddDays(- salesUnit.ProductionTerm - salesUnit.DeliveryPeriodCalculated) - DateTime.Today).Days;
    //        }
    //    }

    //    #region OrderInTakeRegion

    //    public DateTime OrderInTakeDate => SalesUnits.First().OrderInTakeDate;
    //    public int OrderInTakeYear => OrderInTakeDate.Year;
    //    public string OrderInTakeMonth => OrderInTakeDate.MonthName();

    //    #endregion

    //    #region CheckRegion

    //    public bool IsDone => SalesUnits.All(salesUnit => salesUnit.IsDone);
    //    public bool IsWon => SalesUnits.All(salesUnit => salesUnit.IsWon);
    //    public bool IsLoosen => SalesUnits.All(salesUnit => salesUnit.IsLoosen);
    //    public bool ForReport => Project != null && Project.ForReport;
    //    public bool InWork => Project != null && Project.InWork;

    //    #endregion

    //    #region TenderInfoRegion

    //    /// <summary>
    //    /// Дата ближайшего тендера на поставку
    //    /// </summary>
    //    public DateTime? TenderDate
    //    {
    //        get
    //        {
    //            var supplyTenders = Tenders
    //                .Where(tender => tender.DidNotTakePlace == false)
    //                .Where(tender => tender.Types.Select(tenderType => tenderType.Type).Contains(TenderTypeEnum.ToSupply))
    //                .ToList();

    //            return supplyTenders.Any() == false
    //                ? null 
    //                : supplyTenders.OrderBy(tender => tender.DateClose).Last()?.DateClose;
    //        }
    //    }

    //    /// <summary>
    //    /// Подрядчик
    //    /// </summary>
    //    public Company Builder => Tenders.GetWinner(TenderTypeEnum.ToWork);

    //    /// <summary>
    //    /// Проектировщик
    //    /// </summary>
    //    public Company ProjectMaker => Tenders.GetWinner(TenderTypeEnum.ToProject);

    //    /// <summary>
    //    /// Поставщик
    //    /// </summary>
    //    public Company Supplier => Tenders.GetWinner(TenderTypeEnum.ToSupply);

    //    private void RefreshTenderInformation()
    //    {
    //        RaisePropertyChanged(nameof(TenderDate));
    //        RaisePropertyChanged(nameof(Builder));
    //        RaisePropertyChanged(nameof(ProjectMaker));
    //        RaisePropertyChanged(nameof(Supplier));
    //    }

    //    #endregion

    //    /// <summary>
    //    /// В группе не осталось юнитов
    //    /// </summary>
    //    public event Action<ProjectItem> LastSalesUnitRemoveEvent;

    //    public string ProductsInProject => this.ProjectUnitsGroups?
    //        .Select(x => x.Designation.Replace("-УЭТМ-", "-"))
    //        .Distinct()
    //        .OrderBy(x => x)
    //        .ToStringEnum();

    //    public IEnumerable<Company> FacilitiesOwners
    //    {
    //        get
    //        {
    //            var owners = this.Facilities.Select(facility => facility.OwnerCompany).ToList();
    //            var result = new List<Company>(owners);
    //            foreach (var owner in owners)
    //            {
    //                result.AddRange(owner.ParentCompanies());
    //            }
    //            return result.Distinct().OrderBy(company => company.ShortName);
    //        }
    //    }

    //    public ProjectItem(IEnumerable<SalesUnit> salesUnits, IEventAggregator eventAggregator)
    //    {
    //        SalesUnits = new SalesUnitsCollection(salesUnits);
    //        Project = SalesUnits.First().Project;

    //        //если коллекция изменилась, обновляем все отображаемые поля
    //        SalesUnits.CollectionChanged += RefreshAllProperties;

    //        //SalesUnits.SalesUnitChanged += salesUnit => { RefreshAllProperties(); };

    //        SalesUnits.CollectionIsEmptyEvent += () =>
    //        {
    //            SalesUnits.CollectionChanged -= RefreshAllProperties;
    //            LastSalesUnitRemoveEvent?.Invoke(this);
    //        };

    //        //реакция на изменение проекта
    //        eventAggregator.GetEvent<AfterSaveProjectEvent>().Subscribe(project =>
    //        {
    //            if (Project.Id == project.Id)
    //            {
    //                Project = project;
    //            }
    //        });

    //        //реакция на удаление тендера
    //        eventAggregator.GetEvent<AfterRemoveTenderEvent>().Subscribe(tender =>
    //        {
    //            AllTenders.RemoveIfContainsById(tender);
    //            RefreshTenderInformation();
    //        });

    //        //реакция на сохранение тендера
    //        eventAggregator.GetEvent<AfterSaveTenderEvent>().Subscribe(tender =>
    //        {
    //            if (tender.Project.Id == Project.Id)
    //            {
    //                AllTenders.ReAddById(tender);
    //                RefreshTenderInformation();
    //            }
    //        });

    //        //реакция на удаления юнита
    //        eventAggregator.GetEvent<AfterRemoveSalesUnitEvent>().Subscribe(salesUnit =>
    //        {
    //            if (SalesUnits.ContainsById(salesUnit))
    //            {
    //                SalesUnits.Remove(salesUnit);
    //            }
    //        });
    //    }

    //    private void RefreshAllProperties()
    //    {
    //        if (!SalesUnits.Any()) return;
            
    //        RaisePropertyChanged(nameof(this.Facilities));
    //        RaisePropertyChanged(nameof(this.Sum));
    //        RaisePropertyChanged(nameof(this.OrderInTakeDate));
    //        RaisePropertyChanged(nameof(this.OrderInTakeYear));
    //        RaisePropertyChanged(nameof(this.OrderInTakeMonth));
    //        RaisePropertyChanged(nameof(this.IsLoosen));
    //        RaisePropertyChanged(nameof(this.IsDone));
    //        RaisePropertyChanged(nameof(this.InWork));
    //        RaisePropertyChanged(nameof(this.ForReport));
    //        RaisePropertyChanged(nameof(this.DaysToStartProduction));

    //        RaisePropertyChanged(nameof(this.ProjectUnitsGroups));
    //    }

    //    public void Check(SalesUnit salesUnit)
    //    {
    //        if (!SalesUnits.Any()) return;

    //        //если юнит подходит этой группе
    //        if (Fits(salesUnit))
    //        {
    //            //добавляем/обновляем его
    //            SalesUnits.ReAdd(salesUnit);
    //            return;
    //        }

    //        //если не подходит, но содержится - удаляем его
    //        if (SalesUnits.ContainsById(salesUnit))
    //            SalesUnits.Remove(salesUnit);
    //    }

    //    /// <summary>
    //    /// Подходит ли юнит в этот айтем
    //    /// </summary>
    //    /// <param name="salesUnit"></param>
    //    /// <returns></returns>
    //    private bool Fits(SalesUnit salesUnit)
    //    {
    //        return SalesUnits
    //            //.Where(x => x.Id != salesUnit.Id)
    //            .Concat(new[] {salesUnit})
    //            .GroupBy(unit => unit, new SalesUnitsMarketViewComparer())
    //            .Count() == 1;
    //    }
    //}
}