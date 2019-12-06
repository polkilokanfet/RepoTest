using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class PriceTask : WrapperBase<ProductBlock>
    {
        private readonly List<Specification> _specifications;
        private readonly List<Offer> _offers;
        private List<Project> _projects;

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

        public int ProjectsCount { get; }

        /// <summary>
        /// �� �������� �����
        /// </summary>
        public bool IsPriceless => !Prices.Any();

        /// <summary>
        /// ���� ����� ��� ���������� ������
        /// </summary>
        public bool HasReasons => !string.IsNullOrEmpty(Model.StructureCostNumber) && (SpecificationsCount > 0 || OffersCount > 0 || ProjectsCount > 0);

        public IValidatableChangeTrackingCollection<SumOnDateWrapper> Prices { get; private set; }

        public PriceTask(ProductBlock block, List<Specification> specifications, List<Offer> offers, List<Project> projects) : base(block)
        {
            _specifications = specifications;
            _offers = offers;
            _projects = projects;
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.Prices == null) throw new ArgumentException("Prices cannot be null");
            Prices = new ValidatableChangeTrackingCollection<SumOnDateWrapper>(Model.Prices.Select(e => new SumOnDateWrapper(e)));
            RegisterCollection(Prices, Model.Prices);
        }


        public override int CompareTo(object obj)
        {
            var other = obj as PriceTask;
            if (other == null) throw new ArgumentException($"������� �� { nameof(PriceTask) }.");

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