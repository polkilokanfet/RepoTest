using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SpecificationViewModel : UnitsContainer<Specification, SpecificationWrapper, SpecificationDetailsViewModel, SpecificationUnitsGroupsViewModel, SalesUnit, AfterSaveSpecificationEvent>
    {
        public SpecificationViewModel(IUnityContainer container) : base(container)
        {
        }

        public override void Load(Specification model, bool isNew, object parameter = null)
        {
            base.Load(model, isNew, parameter);

            //при создании новой спецификации
            if (isNew)
            {
                DetailsViewModel.Item.Date = DateTime.Today;
                GroupsViewModel.Groups.ForEach(x => x.Specification = DetailsViewModel.Item);
            }
        }

        protected override IEnumerable<SalesUnit> GetUnits(Specification specification, object parameter = null)
        {
            //новая спецификация по проекту
            var project = parameter as Project;
            if (project != null)
            {
                var uetm = UnitOfWork.Repository<Company>().GetById(GlobalAppProperties.Actual.OurCompany.Id);
                var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id && x.Specification == null);
                salesUnits.ForEach(x => x.Producer = uetm);
                return salesUnits;
            }

            //редактирование спецификации
            return UnitOfWork.Repository<SalesUnit>().Find(x => x.Specification != null && x.Specification.Id == specification.Id);
        }
    }
}