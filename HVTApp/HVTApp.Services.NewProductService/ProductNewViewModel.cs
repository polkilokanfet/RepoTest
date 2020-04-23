using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.NewProductService
{
    public class ProductNewViewModel : CreateNewProductTaskDetailsViewModel
    {
        private ParameterWrapper _parameter;

        public ProductNewViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override void AfterLoading()
        {
            //новый продукт
            var block = new ProductBlockWrapper(new ProductBlock());
            Item.Product = new ProductWrapper(new Product())
            {
                ProductBlock = block
            };

            //параметры продукта
            _parameter = new ParameterWrapper(new Parameter());
            var group = UnitOfWork.Repository<ParameterGroup>().GetById(GlobalAppProperties.Actual.NewProductParameterGroup.Id);
            _parameter.ParameterGroup = new ParameterGroupWrapper(group);

            var parameterBase = UnitOfWork.Repository<Parameter>().GetById(GlobalAppProperties.Actual.NewProductParameter.Id);
            var relation = new ParameterRelation();
            relation.RequiredParameters.Add(parameterBase);
            _parameter.ParameterRelations.Add(new ParameterRelationWrapper(relation));

            var parameters = parameterBase.Paths().First().Parameters.Select(x => new ParameterWrapper(x)).ToList();
            parameters.Add(_parameter);
            parameters.ForEach(x => block.Parameters.Add(x));

            Item.PropertyChanged += ItemOnPropertyChanged;

            base.AfterLoading();
        }

        //значение параметра = обозначение
        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Item.Designation))
                _parameter.Value = Item.Designation;
        }
    }
}