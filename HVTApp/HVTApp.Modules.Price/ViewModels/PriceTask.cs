using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PriceTask : ProductBlockWrapper
    {
        private readonly IEnumerable<Specification> _specifications;
        private readonly IEnumerable<Offer> _offers;
        private readonly IEnumerable<Project> _projects;
        private double? _price;
        private DateTime? _date = DateTime.Today;

        public int SpecificationsCount => _specifications.Count();
        public int OffersCount => _offers.Count();
        public int ProjectsCount => _projects.Count();

        /// <summary>
        /// Не содержит прайс
        /// </summary>
        public bool IsPriceless => !Prices.Any();

        public double? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        public DateTime? Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public PriceTask(ProductBlock block, IEnumerable<Specification> specifications, IEnumerable<Offer> offers, IEnumerable<Project> projects) : base(block)
        {
            _specifications = specifications;
            _offers = offers;
            _projects = projects;
        }

        /// <summary>
        /// Добавление прайса в блок
        /// </summary>
        public void AddPrice()
        {
            if(Price == null || Date == null) return;

            var sumOnDate = new SumOnDateWrapper(new SumOnDate())
            {
                Sum = Price.Value,
                Date = Date.Value
            };

            Prices.Add(sumOnDate);
        }

        /// <summary>
        /// Есть повод для обновления прайса
        /// </summary>
        public bool HasReasons => _specifications.Any() || _offers.Any() || _projects.Any();

        public override int CompareTo(object obj)
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