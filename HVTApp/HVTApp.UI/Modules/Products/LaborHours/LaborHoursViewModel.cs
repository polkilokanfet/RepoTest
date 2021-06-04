using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.LaborHours
{
    public class LaborHoursViewModel : LaborHoursLookupListViewModel
    {
        private Block _selectedBlock;
        public ObservableCollection<Block> Blocks { get; }

        public Block SelectedBlock
        {
            get => _selectedBlock;
            set
            {
                _selectedBlock = value;
                ((DelegateLogCommand)CreateLaborHoursByBlockCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand CreateLaborHoursByBlockCommand { get; }

        public LaborHoursViewModel(IUnityContainer container) : base(container)
        {
            var allBlocks = UnitOfWork.Repository<ProductBlock>().GetAll()
                .OrderBy(block => block.Designation)
                .ToList();
            Blocks = new ObservableCollection<Block>(allBlocks.Select(block => new Block(block)));

            Load();

            //проставляем блоки, которые перекрывают эти нормо-часы
            foreach (var laborHoursLookup in this.Lookups)
            {
                laborHoursLookup.Blocks = allBlocks
                    .Where(block => laborHoursLookup.Entity.Parameters.AllContainsIn(block.Parameters)).ToList();
            }

            foreach (var block in Blocks)
            {
                block.HasLaborHours = Lookups.Any(laborHoursLookup => laborHoursLookup.Blocks.Contains(block.ProductBlock));
            }

            EventAggregator.GetEvent<AfterSaveLaborHoursEvent>().Subscribe(
                hours =>
                {
                    foreach (var block in Blocks.Where(x => hours.Parameters.AllContainsIn(x.ProductBlock.Parameters)))
                    {
                        block.HasLaborHours = true;
                    }
                });

            CreateLaborHoursByBlockCommand = new DelegateLogCommand(
                () =>
                {
                    Model.POCOs.LaborHours laborHours = new Model.POCOs.LaborHours
                    {
                        Parameters = SelectedBlock.ProductBlock.Parameters.ToList()
                    };
                    Container.Resolve<IUpdateDetailsService>().UpdateDetails(laborHours);
                }, 
                () => SelectedBlock != null);
        }
    }

    public class LaborHoursDetailsView2 : LaborHoursDetailsView
    {
        public LaborHoursDetailsView2(IRegionManager regionManager, IEventAggregator eventAggregator, LaborHoursDetailsViewModel2 viewModel) 
            : base(regionManager, eventAggregator, viewModel)
        {
            
        }
    }

    public class LaborHoursDetailsViewModel2 : LaborHoursDetailsViewModel
    {
        public LaborHoursDetailsViewModel2(IUnityContainer container) : base(container)
        {
        }

        protected override void AfterLoading()
        {
            foreach (var parameterWrapper in Item.Parameters.ToList())
            {
                Item.Parameters.Remove(parameterWrapper);
                Item.Parameters.Add(new ParameterWrapper(UnitOfWork.Repository<Parameter>().GetById(parameterWrapper.Id)));
            }
            base.AfterLoading();
        }
    }
}