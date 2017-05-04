using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ProductDetailsViewModel : BindableBase
    {
        public ProductWrapper Product { get; set; }

        public ProductDetailsViewModel(ProductWrapper product)
        {
            Product = product;
        }
    }
}
