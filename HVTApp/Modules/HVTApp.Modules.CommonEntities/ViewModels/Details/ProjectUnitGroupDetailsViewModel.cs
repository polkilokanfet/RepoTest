using System;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectUnitGroupDetailsViewModel
    {
        //private ProjectUnitWrapper _selectedProjectUnitWrapper;

        //protected override async void SaveCommand_Execute()
        //{
        //    //foreach (var addedItem in Item.ProjectUnits.AddedItems)
        //    //    UnitOfWork.GetRepository<ProjectUnit>().Add(addedItem.Model);
        //    //foreach (var removedItem in Item.ProjectUnits.RemovedItems)
        //    //    UnitOfWork.GetRepository<ProjectUnit>().Delete(removedItem.Model);
        //    //foreach (var modifiedItem in Item.ProjectUnits.ModifiedItems)
        //    //    modifiedItem.AcceptChanges();

        //    //await UnitOfWork.SaveChangesAsync();
        //    //OnCloseRequested(new DialogRequestCloseEventArgs(true));
        //}

        //public ProjectUnitWrapper SelectedProjectUnitWrapper
        //{
        //    get { return _selectedProjectUnitWrapper; }
        //    set
        //    {
        //        _selectedProjectUnitWrapper = value;
        //        CheckCommands();
        //        OnPropertyChanged();
        //    }
        //}

        //private void CheckCommands()
        //{
        //    ((DelegateCommand)AddProjectUnitCommand).RaiseCanExecuteChanged();
        //}

        //public ICommand SelectProductCommand { get; private set; }
        //public ICommand SelectFacilityCommand { get; private set; }
        //public ICommand SelectProducerCommand { get; private set; }
        //public ICommand SelectProjectCommand { get; private set; }

        //public ICommand AddProjectUnitCommand { get; private set; }

        //protected override void InitCommands()
        //{
        //    SelectProductCommand = new DelegateCommand(SelectProduct_Execute);
        //    SelectFacilityCommand = new DelegateCommand(SelectFacility_Execute);
        //    SelectProducerCommand = new DelegateCommand(SelectProducer_Execute);
        //    SelectProjectCommand = new DelegateCommand(SelectProject_Execute);

        //    AddProjectUnitCommand = new DelegateCommand(AddProjectUnitCommand_Execute);
        //}


        //private void AddProjectUnitCommand_Execute()
        //{
        //    //var projectUnit = new ProjectUnit()
        //    //{
        //    //    Cost = SelectedProjectUnitWrapper.Model.Cost,
        //    //    Product = SelectedProjectUnitWrapper.Model.Product,
        //    //    Facility = SelectedProjectUnitWrapper.Model.Facility,
        //    //    Cost = SelectedProjectUnitWrapper.Model.,
        //    //    Cost = SelectedProjectUnitWrapper.Model.Cost,
        //    //    Cost = SelectedProjectUnitWrapper.Model.Cost,
        //    //};
        //}

        //private async void SelectProduct_Execute()
        //{
        //    var product = await Container.Resolve<IGetProductService>().GetProductAsync(Item.Product?.Model);
        //    if (product == null || Equals(Item.Product?.Id, product.Id)) return;

        //    var prod = await UnitOfWork.GetRepository<Product>().GetByIdAsync(product.Id);
        //    Item.Product = new ProductWrapper(prod);
        //    Item.ProductId = prod.Id;
        //}

        //private async void SelectProject_Execute()
        //{
        //    var projects = await UnitOfWork.GetRepository<Project>().GetAllAsync();
        //    SelectAndSetWrapper<Project, ProjectLookup, ProjectWrapper>(projects, nameof(Item.Project));
        //}

        //private async void SelectProducer_Execute()
        //{
        //    var companies = await UnitOfWork.GetRepository<Company>().GetAllAsync();
        //    var producers = companies.Where(x => x.ActivityFilds.Select(af => af.ActivityFieldEnum).Contains(ActivityFieldEnum.ProducerOfHighVoltageEquipment));
        //    SelectAndSetWrapper<Company, CompanyLookup, CompanyWrapper>(producers, nameof(Item.Producer));
        //}

        //private async void SelectFacility_Execute()
        //{
        //    var facilities = await UnitOfWork.GetRepository<Facility>().GetAllAsync();
        //    SelectAndSetWrapper<Facility, FacilityLookup, FacilityWrapper>(facilities, nameof(Item.Facility));
        //}
    }
}