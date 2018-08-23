using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attrubutes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        public List<SalesUnitLookup> SalesUnits { get; set; } = new List<SalesUnitLookup>();
        public List<TenderLookup> Tenders { get; set; } = new List<TenderLookup>();
        public List<OfferLookup> Offers { get; set; } = new List<OfferLookup>();

        [Designation("Сумма проекта")]
        public double Sum => SalesUnits.Sum(x => x.Cost);

        [Designation("Дата поставки")]
        public DateTime? RealizationDate => SalesUnits.Select(x => x.DeliveryDateExpected).Min();

        [Designation("Тендер")]
        public DateTime? TenderDate
            => Tenders.SingleOrDefault(x => x.Entity.Types.Any(tp => tp.Type == TenderTypeEnum.ToSupply))?.DateClose;

        //private double _sum;
        //private DateTime _realizationDate;
        //private DateTime? _tenderDate;

        //[Designation("Сумма проекта")]
        //public double Sum
        //{
        //    get { return _sum; }
        //    set
        //    {
        //        _sum = value;
        //        OnPropertyChanged();
        //    }
        //}

        //[Designation("Дата поставки")]
        //public DateTime RealizationDate
        //{
        //    get { return _realizationDate; }
        //    set
        //    {
        //        _realizationDate = value;
        //        OnPropertyChanged();
        //    }
        //}

        //[Designation("Тендер")]
        //public DateTime? TenderDate
        //{
        //    get { return _tenderDate; }
        //    set
        //    {
        //        _tenderDate = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public override int CompareTo(object obj)
        //{
        //    return RealizationDate.CompareTo(((ProjectLookup)obj).RealizationDate);
        //}
    }
}
