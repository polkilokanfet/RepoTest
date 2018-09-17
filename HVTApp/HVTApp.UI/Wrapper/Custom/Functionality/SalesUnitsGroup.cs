using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Wrapper
{
    public class SalesUnitsGroup : SalesUnitWrapper, IUnitsDatedGroup
    {
        private double _price;

        public ValidatableChangeTrackingCollection<SalesUnitsGroup> Groups { get; }

        public int Amount => Groups?.Count ?? 1;

        public double Total => Groups?.Sum(x => x.Cost) ?? Cost;

        public double Price
        {
            get { return _price; }
            set
            {
                if (Math.Abs(_price - value) < 0.0001) return;
                _price = value;
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged();
            }
        }

        public double CostG
        {
            get { return Cost; }
            set
            {
                if (Equals(value, Cost)) return;
                if (value < 0) return;
                Cost = value;
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged();
            }
        }

        public double? MarginalIncome
        {
            get { return Cost <= 0 ? default(double?) : (1.0 - Price / Cost) * 100.0; }
            set
            {
                if (!value.HasValue || value >= 100) return;
                CostG = Price / (1.0 - value.Value / 100.0);
                OnPropertyChanged();
            }
        }

        public int? ProductionTermG
        {
            get { return ProductionTerm; }
            set
            {
                if (value.HasValue && value < 0) return;
                ProductionTerm = value;
            }
        }

        public SalesUnitsGroup(IEnumerable<SalesUnit> units) : base(units.First())
        {
            if (units.Count() == 1) return;
            
            //создаем группы
            var groups = units.Select(x => new SalesUnitsGroup(new[] {x}));
            Groups = new ValidatableChangeTrackingCollection<SalesUnitsGroup>(groups);

            //регистрируем их
            RegisterCollectionWithoutSynch(Groups);

            this.PropertyChanged += OnPropertyChanged;
            this.Groups.CollectionChanged += (sender, args) => { RemovedAllGroups?.Invoke(this); };

            //OnPropertyChanged(nameof(MarginalIncome));
        }

        //смена значения свойства
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (Groups == null) return;
            var property = this.GetType().GetProperty(args.PropertyName);
            if (!property.CanWrite) return;

            var valueNew = property.GetValue(this);

            foreach (var group in Groups)
            {
                var valueOld = property.GetValue(group);
                if(Equals(valueOld, valueNew)) continue;
                property.SetValue(group, valueNew);
            }
        }

        public event Action<SalesUnitsGroup> RemovedAllGroups;
    }
}