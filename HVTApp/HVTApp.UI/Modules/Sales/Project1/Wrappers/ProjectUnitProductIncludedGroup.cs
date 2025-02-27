using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitProductIncludedGroup : BindableBase
    {
        public IEnumerable<ProjectUnitProductIncluded> Items { get; }

        public string Name { get; }

        public int Amount => Items.Sum(x => x.Model.Amount);

        /// <summary>
        /// Прайс на единицу
        /// </summary>
        public double? CustomFixedPrice
        {
            get => Items.First().CustomFixedPrice;
            set => this.Items.ForEach(x => x.CustomFixedPrice = value);
        }

        public ProjectUnitProductIncludedGroup(IEnumerable<ProjectUnitProductIncluded> items)
        {
            Items = items;
            foreach (var projectUnitProductIncluded in Items)
            {
                projectUnitProductIncluded.PropertyChanged +=
                    (sender, args) => RaisePropertyChanged(nameof(CustomFixedPrice));
            }

            Name = Items.First().Model.Product.ToString();
        }
    }
}