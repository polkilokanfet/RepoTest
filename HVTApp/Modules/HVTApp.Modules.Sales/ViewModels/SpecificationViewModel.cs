using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SpecificationViewModel : UnitsContainer<Specification, SpecificationWrapper, SpecificationDetailsViewModel, SpecificationUnitsGroupsViewModel, SalesUnit, AfterSaveSpecificationEvent>
    {

        public SpecificationViewModel(IUnityContainer container) : base(container)
        {
        }

        //private async Task InitGroupsViewModel(IEnumerable<SalesUnit> units)
        //{
        //    GroupsViewModel = new SpecificationUnitsGroupsViewModel(Container, units, UnitOfWork, Item);
        //    await GroupsViewModel.LoadAsync();

        //    //����������� �� ������� ��������� ����� � �������������
        //    this.GroupsViewModel.Groups.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        //    this.GroupsViewModel.Groups.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

        //    //������ �� ��������� ������
        //    OnPropertyChanged(nameof(GroupsViewModel));
        //}

        /// <summary>
        /// �������� ��� �������� ����� ������������ � ������������ � ��������
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task LoadAsync(Project project)
        {
            //UnitOfWork = Container.Resolve<IUnitOfWork>();

            ////������� ����� ������������
            //Item = new SpecificationWrapper(new Specification()) {Date = DateTime.Today};

            ////���� ����� � ������� ���������
            ////��������� ����� �� �������������
            //var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id && x.Specification == null);

            //await InitGroupsViewModel(salesUnits);
        }



        protected override async void SaveCommandExecute()
        {
            //��������� ��������, ���� �� �� ������������
            if (await UnitOfWork.Repository<Specification>().GetByIdAsync(DetailsViewModel.Item.Model.Id) == null)
                UnitOfWork.Repository<Specification>().Add(DetailsViewModel.Item.Model);

            DetailsViewModel.Item.AcceptChanges();

            //��������� �� ������������ ������
            var removed = GroupsViewModel.Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            removed = GroupsViewModel.Groups.RemovedItems.Concat(removed).ToList();
            removed.ForEach(x => { x.RejectChanges(); });
            removed.ForEach(x => { x.Specification = null; });
            removed.ForEach(x => { x.AcceptChanges(); });

            GroupsViewModel.AcceptChanges();

            //���������
            try
            {
                GroupsViewModel.AcceptChanges();
                await UnitOfWork.SaveChangesAsync();
                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveSpecificationEvent>().Publish(DetailsViewModel.Item.Model);
            }
            catch (DbUpdateConcurrencyException e)
            {
                var sb = new StringBuilder();
                Exception exception = e;
                do
                {
                    sb.AppendLine(e.Message);
                    exception = exception.InnerException;
                } while (exception != null);

                Container.Resolve<IMessageService>().ShowOkMessageDialog("������ ��� ����������", sb.ToString());
            }

        }


        protected override Task<IEnumerable<SalesUnit>> GetUnits(Specification model, object parameter = null)
        {
            throw new NotImplementedException();
        }
    }
}