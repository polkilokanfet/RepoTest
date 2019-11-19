using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Structures;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class ProjectViewModel2 : ViewModelBase
    {
        private object _selectedItem;
        public ProjectWithGroupsWrapper Project { get; private set; }

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(PriceStructures));
            }
        }

        /// <summary>
        /// Структуры себестоимости выбранного юнита.
        /// </summary>
        public PriceStructures PriceStructures
        {
            get
            {
                if (SelectedItem == null)
                    return null;

                if (SelectedItem is SalesUnitProjectGroup)
                    return ((SalesUnitProjectGroup)SelectedItem).Unit.CostStructure.PriceStructures;

                return ((SalesUnitProjectItem)SelectedItem).CostStructure.PriceStructures;
            }
        }

        public ProjectViewModel2(IUnityContainer container) : base(container)
        {
        }

        public void Load(Guid id)
        {
            var project = UnitOfWork.Repository<Project>().GetById(id);
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == id);
            Project = new ProjectWithGroupsWrapper(project, salesUnits);
        }
    }
}