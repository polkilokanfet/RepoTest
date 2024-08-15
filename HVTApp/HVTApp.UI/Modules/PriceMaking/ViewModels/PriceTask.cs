using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.UI.Modules.PlanAndEconomy.ViewModels;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.PriceMaking.ViewModels
{
    public class PriceTask : WrapperBase<ProductBlock>
    {
        private readonly List<Specification> _specifications;
        private readonly List<Offer> _offers;
        private readonly List<ProjectItem> _projects;

        public string BlockName { get; }

        private DateTime LastPriceDate => Prices.Max(x => x.Date);

        public int SpecificationsCount
        {
            get
            {
                return Prices.Any()
                    ? _specifications.Count(x => x.Date > LastPriceDate.AddDays(GlobalAppProperties.Actual.ActualPriceTerm))
                    : _specifications.Count;
            }
        }

        public int OffersCount
        {
            get
            {
                return Prices.Any()
                    ? _offers.Count(x => x.Date > LastPriceDate.AddDays(GlobalAppProperties.Actual.ActualPriceTerm))
                    : _offers.Count;
            }
        }

        public int ProjectsCount
        {
            get
            {
                return Prices.Any()
                    ? _projects.Count(x => x.OrderInTakeDate <= DateTime.Today && x.OrderInTakeDate > LastPriceDate.AddDays(GlobalAppProperties.Actual.ActualPriceTerm))
                    : _projects.Count;
            }
        }

        /// <summary>
        /// Не содержит прайс
        /// </summary>
        public bool IsPriceless => !Prices.Any();

        /// <summary>
        /// Есть повод для обновления прайса
        /// </summary>
        public bool HasReasons => !string.IsNullOrEmpty(Model.StructureCostNumber) && (SpecificationsCount > 0 || OffersCount > 0 || ProjectsCount > 0);

        public IValidatableChangeTrackingCollection<SumOnDateWrapper> Prices { get; private set; }

        public PriceTask(ProductBlock block, IEnumerable<Specification> specifications, IEnumerable<Offer> offers, IEnumerable<ProjectItem> projects) : base(block)
        {
            _specifications = specifications.ToList();
            _offers = offers.ToList();
            _projects = projects.ToList();

            this.Prices.CollectionChanged += (sender, args) => { RefreshProperties(); };
            this.Prices.PropertyChanged += (sender, args) => { RefreshProperties(); };

            BlockName = Model.ToString();
        }

        private void RefreshProperties()
        {
            RaisePropertyChanged(nameof(HasReasons));
            RaisePropertyChanged(nameof(SpecificationsCount));
            RaisePropertyChanged(nameof(OffersCount));
            RaisePropertyChanged(nameof(ProjectsCount));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Prices == null) throw new ArgumentException("Prices can not be null");
            Prices = new ValidatableChangeTrackingCollection<SumOnDateWrapper>(Model.Prices.Select(e => new SumOnDateWrapper(e)));
            RegisterCollection(Prices, Model.Prices);
        }


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