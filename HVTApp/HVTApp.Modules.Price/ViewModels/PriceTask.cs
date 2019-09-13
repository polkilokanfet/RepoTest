using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PriceTask : IComparable
    {
        private readonly IEnumerable<Specification> _specifications;
        private readonly IEnumerable<Offer> _offers;
        private readonly IEnumerable<Project> _projects;

        public int SpecificationsCount => _specifications.Count();
        public int OffersCount => _offers.Count();
        public int ProjectsCount => _projects.Count();
        public bool IsPriceless => !Block.Prices.Any();

        public ProductBlock Block { get; }
        public double? Price { get; set; }
        public DateTime? Date { get; set; } = DateTime.Today;

        public PriceTask(ProductBlock block, IEnumerable<Specification> specifications, IEnumerable<Offer> offers, IEnumerable<Project> projects)
        {
            Block = block;
            _specifications = specifications;
            _offers = offers;
            _projects = projects;
        }

        public void SavePrice()
        {
            if(Price == null || Date == null) return;
            Block.Prices.Add(new SumOnDate {Sum = Price.Value, Date = Date.Value});
        }

        public bool HasReasons => _specifications.Any() || _offers.Any() || _projects.Any();

        public int CompareTo(object obj)
        {
            var other = obj as PriceTask;
            if (other == null) throw new ArgumentException($"Передан не { nameof(PriceTask) }.");

            if (this.IsPriceless == other.IsPriceless)
                return CompareByReasons(other);

            if (this.HasReasons && other.HasReasons)
            {
                return CompareByReasons(other);
            }

            if (this.HasReasons && !other.HasReasons)
                return -1;

            if (!this.HasReasons && other.HasReasons)
                return 1;

            if (this.IsPriceless)
                return -1;

            if (other.IsPriceless)
                return 1;

            return 0;
        }

        private int CompareByReasons(PriceTask other)
        {
            if (this.SpecificationsCount != other.SpecificationsCount)
                return other.SpecificationsCount - this.SpecificationsCount;

            if (this.OffersCount != other.OffersCount)
                return other.OffersCount - this.OffersCount;

            if (this.ProjectsCount != other.ProjectsCount)
                return other.ProjectsCount - this.ProjectsCount;

            return 0;
        }
    }
}