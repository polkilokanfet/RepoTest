using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ProductsViewModel : BindableBase
    {
        public ObservableCollection<ProductWrapper> Products { get; }

        public ProductsViewModel(IUnitOfWork unitOfWork)
        {
            Products = new ObservableCollection<ProductWrapper>(unitOfWork.Products.GetAll());
        }
    }
}
