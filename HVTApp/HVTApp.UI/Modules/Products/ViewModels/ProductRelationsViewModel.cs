using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Prism.Events;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class ProductRelationsViewModel : ProductRelationLookupListViewModel
    {
        public DelegateLogCommand CopyRelationsCommand { get; }

        public ProductRelationsViewModel(IUnityContainer container) : base(container)
        {
            CopyRelationsCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = container.Resolve<IUnitOfWork>();
                    var originProductRelation = unitOfWork.Repository<ProductRelation>().GetById(SelectedItem.Id);

                    var productRelation = new ProductRelation
                    {
                        Name = $"{originProductRelation.Name} copy",
                        ChildProductsAmount = originProductRelation.ChildProductsAmount,
                        IsUnique = originProductRelation.IsUnique,
                        ParentProductParameters = originProductRelation.ParentProductParameters.ToList(),
                        ChildProductParameters = originProductRelation.ChildProductParameters.ToList()
                    };

                    productRelation = container.Resolve<IUpdateDetailsService>().UpdateDetailsWithoutSaving(productRelation);
                    if (productRelation != null)
                    {
                        foreach (var parameter in productRelation.ParentProductParameters.ToList())
                        {
                            productRelation.ParentProductParameters.Remove(parameter);
                            productRelation.ParentProductParameters.Add(unitOfWork.Repository<Parameter>().GetById(parameter.Id));
                        }

                        foreach (var parameter in productRelation.ChildProductParameters.ToList())
                        {
                            productRelation.ChildProductParameters.Remove(parameter);
                            productRelation.ChildProductParameters.Add(unitOfWork.Repository<Parameter>().GetById(parameter.Id));
                        }

                        if (unitOfWork.SaveEntity(productRelation).OperationCompletedSuccessfully)
                        {
                            container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductRelationEvent>().Publish(productRelation);
                        }
                    }
                },
                () => this.SelectedItem != null);

            this.SelectedLookupChanged += lookup =>
            {
                this.CopyRelationsCommand.RaiseCanExecuteChanged();
            };
        }
    }
}