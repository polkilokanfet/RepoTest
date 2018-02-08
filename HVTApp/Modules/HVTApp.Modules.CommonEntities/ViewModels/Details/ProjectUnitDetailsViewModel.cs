using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectUnitDetailsViewModel
    {
        public ICommand SelectProductCommand { get; private set; }
        public ICommand SelectFacilityCommand { get; private set; }
        public ICommand SelectProducerCommand { get; private set; }
        public ICommand SelectProjectCommand { get; private set; }

        protected override void InitCommands()
        {
            SelectProductCommand = new DelegateCommand(SelectProduct_Execute);
            SelectFacilityCommand = new DelegateCommand(SelectFacility_Execute);
            SelectProducerCommand = new DelegateCommand(SelectProducer_Execute);
            SelectProjectCommand = new DelegateCommand(SelectProject_Execute);
        }

        private async void SelectProject_Execute()
        {
            var projects = await UnitOfWork.GetRepository<Project>().GetAllAsync();
            var project = Container.Resolve<ISelectService>().SelectItem(projects.Select(x => new ProjectLookup(x)), Item.ProjectId);
            if (project != null && !Equals(project.Id, Item.ProjectId))
            {
                Item.Project = new ProjectWrapper(project.Entity);
                Item.ProjectId = project.Id;
            }
        }

        private async void SelectProduct_Execute()
        {
            var product = await Container.Resolve<IGetProductService>().GetProductAsync(Item.Product?.Model);
            if (product == null || Equals(Item.Product?.Id, product.Id)) return;

            var prod = await UnitOfWork.GetRepository<Product>().GetByIdAsync(product.Id);
            Item.Product = new ProductWrapper(prod);
            Item.ProductId = prod.Id;
        }

        private async void SelectProducer_Execute()
        {
            var producers = await UnitOfWork.GetRepository<Company>().GetAllAsync();
            var producerLookups = producers.Where(x => x.ActivityFilds.Select(af => af.ActivityFieldEnum)
                            .Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment)).Select(x => new CompanyLookup(x));
            var producer = Container.Resolve<ISelectService>().SelectItem(producerLookups);
            if (producer != null)
                Item.Producer = new CompanyWrapper(producer.Entity);
        }

        private async void SelectFacility_Execute()
        {
            var facilities = (await UnitOfWork.GetRepository<Facility>().GetAllAsync()).Select(x => new FacilityLookup(x));
            var facility = Container.Resolve<ISelectService>().SelectItem(facilities);
            if(facility != null)
                Item.Facility = new FacilityWrapper(facility.Entity);
        }
    }
}
