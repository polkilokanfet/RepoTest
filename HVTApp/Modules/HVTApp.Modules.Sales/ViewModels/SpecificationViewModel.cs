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
            GroupsViewModel = new SpecificationUnitsGroupsViewModel(Container, units, UnitOfWork, Item);
            await GroupsViewModel.LoadAsync();

            //регистрация на события изменения строк с оборудованием
            this.GroupsViewModel.Groups.PropertyChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            this.GroupsViewModel.Groups.CollectionChanged += (sender, args) => ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            //сигнал об изменении модели
            OnPropertyChanged(nameof(GroupsViewModel));
        }

        /// <summary>
        /// Загрузка при создании новой спецификации в соответствии с проектом
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task LoadAsync(Project project)
        {
            UnitOfWork = Container.Resolve<IUnitOfWork>();

            //создаем новую спецификацию
            Item = new SpecificationWrapper(new Specification()) {Date = DateTime.Today};

            //ищем юниты в текущем контексте
            //исключаем юниты со спецификацией
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.Id == project.Id && x.Specification == null);

            await InitGroupsViewModel(salesUnits);
        }


        /// <summary>
        /// при редактировании спецификации.
        /// </summary>
        /// <returns></returns>
        protected override async Task AfterLoading()
        {
            await base.AfterLoading();
            //загружаем строки с оборудованием
            var units = UnitOfWork.Repository<SalesUnit>().Find(x => x.Specification != null && x.Specification.Id == Item.Id);
            await InitGroupsViewModel(units);
        }


        protected override async void SaveCommand_Execute()
        {
            //добавляем сущность, если ее не существовало
            if (await UnitOfWork.Repository<Specification>().GetByIdAsync(Item.Model.Id) == null)
                UnitOfWork.Repository<Specification>().Add(Item.Model);

            Item.AcceptChanges();

            //удаленные из спецификации группы
            var removed = GroupsViewModel.Groups.Where(x => x.Groups != null).SelectMany(x => x.Groups.RemovedItems).ToList();
            removed = GroupsViewModel.Groups.RemovedItems.Concat(removed).ToList();
            removed.ForEach(x => { x.RejectChanges(); });
            removed.ForEach(x => { x.Specification = null; });
            removed.ForEach(x => { x.AcceptChanges(); });

            GroupsViewModel.Groups.AcceptChanges();

            //сохраняем
            try
            {
                await UnitOfWork.SaveChangesAsync();
                EventAggregator.GetEvent<AfterSaveSpecificationEvent>().Publish(Item.Model);

                await GroupsViewModel.SaveChanges();
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

                Container.Resolve<IMessageService>().ShowOkMessageDialog("Ошибка при сохранении", sb.ToString());
            }

            //запрашиваем закрытие окна
            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        protected override bool SaveCommand_CanExecute()
        {
            //все сущности должны быть валидны
            if (GroupsViewModel == null || !GroupsViewModel.Groups.IsValid || !Item.IsValid ||
                GroupsViewModel.Groups == null || !GroupsViewModel.Groups.Any())
                return false;

            //какая-то сущность должна быть изменена
            return Item.IsChanged || GroupsViewModel.Groups.IsChanged;
        }
    }
}