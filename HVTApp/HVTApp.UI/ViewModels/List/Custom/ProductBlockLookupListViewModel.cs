using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProductBlockLookupListViewModel
    {
        public DelegateLogCommand AddParameterCommand { get; private set; }

        protected override void InitSpecialCommands()
        {
            AddParameterCommand = new DelegateLogCommand(
                () =>
                {
                    var unitOfWork = Container.Resolve<IUnitOfWork>();
                    var parameter = Container.Resolve<ISelectService>().SelectItem(unitOfWork.Repository<Parameter>().GetAll());
                    if (parameter != null)
                    {
                        foreach (var block in SelectedItems)
                        {
                            var productBlock = unitOfWork.Repository<ProductBlock>().GetById(block.Id);
                            productBlock.Parameters.Add(parameter);
                            var lookup = Lookups.Single(x => x.Id == block.Id);
                            lookup.Refresh(block);
                        }

                        unitOfWork.SaveChanges();
                    }
                },
                () => SelectedItems != null && SelectedItems.Any());

            this.SelectedLookupChanged += lookup => AddParameterCommand.RaiseCanExecuteChanged();
        }

        public override void Load()
        {
            base.Load();

            var designDepartments = UnitOfWork.Repository<DesignDepartment>().GetAll();

            foreach (var productBlockLookup in this.Lookups)
            {
                var dd = designDepartments
                    .Where(x => x.ParameterSets.Any(p => p.Parameters.AllContainsInById(productBlockLookup.Entity.Parameters)))
                    .ToList();
                if (dd.Any())
                {
                    productBlockLookup.DesignDepartments = dd.ToStringEnum();
                }
            }
        }
    }
}