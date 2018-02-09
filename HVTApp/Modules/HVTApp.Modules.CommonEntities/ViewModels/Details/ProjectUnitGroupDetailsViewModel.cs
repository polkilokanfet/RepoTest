using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Extantions;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectUnitGroupDetailsViewModel
    {
        public override async Task LoadAsync(Guid? id = null)
        {
            if (id == null)
            {
                await base.LoadAsync(null);
                return;
            }

            var projectUnitsGroups = (await UnitOfWork.GetRepository<ProjectUnit>().GetAllAsync()).ConvertToGroup();
            var targetProjectUnitGroup = projectUnitsGroups.Single(x => x.ProjectUnits.Select(u => u.Id).Contains(id.Value));
            Item = new ProjectUnitGroupWrapper(targetProjectUnitGroup);
            Item.PropertyChanged += ItemOnPropertyChanged;
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Item.ProjectUnits))
            {
                Item.ProjectUnits.ForEach(x => x.Refresh());
            }
        }

        protected override async void SaveCommand_Execute()
        {
            await UnitOfWork.SaveChangesAsync();
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }
    }
}