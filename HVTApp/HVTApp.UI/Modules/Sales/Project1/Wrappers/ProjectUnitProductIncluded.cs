using System;
using System.Collections.Generic;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitProductIncluded : WrapperBase<ProductIncluded>
    {
        public ProjectUnitProductIncluded(ProductIncluded model) : base(model) { }

        /// <summary>
        /// Прайс на единицу
        /// </summary>
        public double? CustomFixedPrice
        {
            get => Model.CustomFixedPrice;
            set
            {
                if (AllowEditCustomFixedPrice)
                    SetValue(value);
            }
        }

        public double? CustomFixedPriceOriginalValue => GetOriginalValue<double?>(nameof(CustomFixedPrice));
        public bool CustomFixedPriceIsChanged => GetIsChanged(nameof(CustomFixedPrice));

        /// <summary>
        /// Стоимость редактировать можно только у шеф-монтажа
        /// </summary>
        public bool AllowEditCustomFixedPrice => this.Model.Product.ProductBlock.Parameters.ContainsById(GlobalAppProperties.Actual.SupervisionParameter);

        public class ProjectUnitProductIncludedComparer : IEqualityComparer<ProjectUnitProductIncluded>
        {
            public virtual bool Equals(ProjectUnitProductIncluded x, ProjectUnitProductIncluded y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                if (!Equals(x.Model.Product.Id, y.Model.Product.Id)) return false;
                if (!Equals(x.CustomFixedPrice, y.CustomFixedPrice)) return false;

                return true;
            }

            public int GetHashCode(ProjectUnitProductIncluded obj)
            {
                return 0;
            }
        }


        public class ProjectUnitProductIncludedComparer2 : IEqualityComparer<ProjectUnitProductIncluded>
        {
            public virtual bool Equals(ProjectUnitProductIncluded x, ProjectUnitProductIncluded y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));
                return x.Model.Id == y.Model.Id;
            }

            public int GetHashCode(ProjectUnitProductIncluded obj)
            {
                return 0;
            }
        }
    }
}