using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductDesignationLookupListViewModel : BaseListViewModel<ProductDesignation, ProductDesignationLookup, AfterSaveProductDesignationEvent, AfterSelectProductDesignationEvent, AfterRemoveProductDesignationEvent>
    {
        public DelegateLogCommand MakeProductDesignationFamilyCommand { get; private set; }

        protected override void LastActionInCtor()
        {
            MakeProductDesignationFamilyCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var selectService = Container.Resolve<ISelectService>();

                    var designationsAll = unitOfWork.Repository<ProductDesignation>().GetAll();
                    var designations = selectService.SelectItems(designationsAll).ToList();

                    var parametersAll = unitOfWork.Repository<Parameter>().GetAll();
                    var parameters = selectService.SelectItems(parametersAll);

                    foreach (var parameter in parameters)
                    {
                        var productDesignation = new ProductDesignation()
                        {
                            Designation = $" {parameter.Value}",
                            Parents =  designations,
                            Parameters = new List<Parameter>() { parameter }
                        };

                        if (designationsAll.Any(x => x.Equals(productDesignation)))
                            continue;

                        unitOfWork.Repository<ProductDesignation>().Add(productDesignation);
                    }

                    unitOfWork.SaveChanges();
                });
        }
    }
}