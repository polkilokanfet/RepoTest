using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.NewProductService
{
    public class ProductNewViewModel : CreateNewProductTaskDetailsViewModel
    {
        private ParameterWrapper _parameter;

        public ProductNewViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override async Task AfterLoading()
        {
            var block = new ProductBlockWrapper(new ProductBlock());
            Item.Product = new ProductWrapper(new Product())
            {
                ProductBlock = block
            };

            _parameter = new ParameterWrapper(new Parameter());
            var group = await UnitOfWork.Repository<ParameterGroup>().GetByIdAsync(CommonOptions.ActualOptions.NewProductParameterGroup.Id);
            _parameter.ParameterGroup = new ParameterGroupWrapper(group);

            var parameterBase = await UnitOfWork.Repository<Parameter>().GetByIdAsync(CommonOptions.ActualOptions.NewProductParameter.Id);
            var relation = new ParameterRelation();
            relation.RequiredParameters.Add(parameterBase);
            _parameter.ParameterRelations.Add(new ParameterRelationWrapper(relation));

            await base.AfterLoading();
        }
    }
}