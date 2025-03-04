using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.ViewModels;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class AddProductsIncludedGroupCommand : ProjectUnitEditUnitOfWorkBaseCommand
    {
        private readonly IGetProductService _productService;
        private readonly IDialogService _dialogService;

        public AddProductsIncludedGroupCommand(IProjectUnit projectUnit, IGetProductService productService, IDialogService dialogService, IUnitOfWork unitOfWork) : base(projectUnit, unitOfWork)
        {
            _productService = productService;
            _dialogService = dialogService;
        }

        public override void Execute(object parameter)
        {
            var viewModel = new ProductIncludedViewModel(_productService, UnitOfWork);
            var dr = _dialogService.ShowDialog(viewModel);
            if (dr != true) return;
            AddProductIncluded(viewModel.Item.Model, viewModel.IsForEach);
        }

        private void AddProductIncluded(ProductIncluded productIncluded, bool isForEach)
        {
            if (ProjectUnit is ProjectUnit projectUnit)
            {
                AddProductIncluded(projectUnit, productIncluded);
            }
            else if (ProjectUnit is ProjectUnitGroup projectUnitGroup)
            {
                AddProductIncluded(projectUnitGroup, productIncluded, isForEach);
            }
            else
            {
                throw new ArgumentException(nameof(projectUnit));
            }
        }

        private void AddProductIncluded(ProjectUnit projectUnit, ProductIncluded productIncluded)
        {
            projectUnit.ProductsIncluded.Add(new ProjectUnitProductIncluded(productIncluded));
        }

        private void AddProductIncluded(ProjectUnitGroup projectUnitGroup, ProductIncluded productIncluded, bool isForEach)
        {
            if (isForEach)
            {
                foreach (var projectUnit in projectUnitGroup.Units)
                {
                    var included = new ProductIncluded { Product = productIncluded.Product, Amount = productIncluded.Amount };
                    AddProductIncluded(projectUnit, included);
                }
            }
            else
            {
                foreach (var projectUnit in projectUnitGroup.Units)
                {
                    AddProductIncluded(projectUnit, productIncluded);
                }
            }

        }
    }
}