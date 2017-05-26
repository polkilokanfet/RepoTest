using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product>
    {
        public ProductWrapper Product => Item;

        public ProductDetailsViewModel(ProductWrapper item) : base(item)
        {
        }
    }
}
