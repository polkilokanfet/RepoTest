using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationViewModel : SpecificationDetailsViewModel
    {
        public SpecificationUnitsGroupsViewModel GroupsViewModel { get; set; }

        public SpecificationViewModel(IUnityContainer container) : base(container)
        {
        }

        private async Task InitGroupsViewModel(IEnumerable<SalesUnit> units)
        {
            GroupsViewModel = new SpecificationUnitsGroupsViewModel(Container, units, UnitOfWork, Item.Model);
            await GroupsViewModel.LoadAsync();

            //����������� �� ������� ��������� ����� � �������������
            this.GroupsViewModel.Groups.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            this.GroupsViewModel.Groups.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            //������ �� ��������� ������
            OnPropertyChanged(nameof(GroupsViewModel));
        }

        /// <summary>
        /// �������� ��� �������� ����� ������������ � ������������ � ��������
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task LoadAsync(Project project)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //������� ����� ������������
            Item = new SpecificationWrapper(new Specification()) {Date = DateTime.Today};

            //���� ����� � ������� ���������
            //��������� ����� �� �������������
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id && x.Specification == null);

            await InitGroupsViewModel(salesUnits);
        }


        /// <summary>
        /// ��� �������������� ������������.
        /// </summary>
        /// <returns></returns>
        protected override async Task AfterLoading()
        {
            await base.AfterLoading();
            //��������� ������ � �������������
            var units = UnitOfWork.Repository<SalesUnit>().Find(x => x.Specification != null && x.Specification.Id == Item.Id);
            await InitGroupsViewModel(units);
        }


        protected override async void SaveCommand_Execute()
        {
        }
    }
}