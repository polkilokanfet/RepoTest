using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationViewModel : UnitsContainer<Specification, SpecificationWrapper, SpecificationDetailsViewModel, SpecificationUnitsGroupsViewModel, SalesUnit, AfterSaveSpecificationEvent>
    {
        public SpecificationViewModel(IUnityContainer container) : base(container)
        {
        }

        public override async Task LoadAsync(Specification model, bool isNew, object parameter = null)
        {
            await base.LoadAsync(model, isNew, parameter);

            //при создании новой спецификации
            if (isNew)
            {
                DetailsViewModel.Item.Date = DateTime.Today;
                GroupsViewModel.Groups.ForEach(x => x.Specification = DetailsViewModel.Item);
            }
        }

        protected override async Task<IEnumerable<SalesUnit>> GetUnits(Specification specification, object parameter = null)
        {
            //новая спецификация по проекту
            var project = parameter as Project;
            if (project != null)
            {
                return UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id && x.Specification == null);
            }

            //редактирование спецификации
            return UnitOfWork.Repository<SalesUnit>().Find(x => x.Specification != null && x.Specification.Id == specification.Id);
        }
    }
}