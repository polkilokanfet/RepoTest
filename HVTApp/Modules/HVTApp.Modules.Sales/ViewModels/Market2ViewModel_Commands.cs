using System;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.ViewModels
{
    public partial class Market2ViewModel
    {
        #region ICommand

        public ICommand NewProjectCommand { get; }
        public ICommand EditProjectCommand { get; }
        public ICommand RemoveProjectCommand { get; }

        public ICommand NewSpecificationCommand { get; }


        public ICommand NewOfferByProjectCommand { get; }
        public ICommand NewOfferByOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand RemoveOfferCommand { get; }
        public ICommand PrintOfferCommand { get; }

        public ICommand NewTenderCommand { get; }
        public ICommand EditTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }

        #endregion

        #region Commands

        #region RemoveCommands

        private async Task RemoveCommandBase<TEntity, TLookup, TRemoveEvent>(TLookup lookup)
            where TEntity : class, IBaseEntity
            where TLookup : LookupItem<TEntity>
            where TRemoveEvent : PubSubEvent<TEntity>, new()
        {
            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var eventAggregator = Container.Resolve<IEventAggregator>();
            var messageService = Container.Resolve<IMessageService>();

            var dr = messageService.ShowYesNoMessageDialog("Удаление", 
                $"Вы действительно хотите удалить \"{lookup.DisplayMember}\"?");
            if (dr != MessageDialogResult.Yes) return;

            var entity = await unitOfWork.Repository<TEntity>().GetByIdAsync(lookup.Id);
            if (entity == null) return;

            try
            {
                unitOfWork.Repository<TEntity>().Delete(entity);
                await unitOfWork.SaveChangesAsync();
                eventAggregator.GetEvent<TRemoveEvent>().Publish(entity);
            }
            catch (DbUpdateException e)
            {
                messageService.ShowOkMessageDialog("DbUpdateException", e.GetAllExceptions());
            }
        }

        private async void RemoveProjectCommand_Execute()
        {
            await RemoveCommandBase<Project, ProjectLookup, AfterRemoveProjectEvent>(Projects.SelectedItem);
        }

        private async void RemoveOfferCommand_Execute()
        {
            await RemoveCommandBase<Offer, OfferLookup, AfterRemoveOfferEvent>(Offers.SelectedItem);
        }

        private async void RemoveTenderCommand_Execute()
        {
            await RemoveCommandBase<Tender, TenderLookup, AfterRemoveTenderEvent>(Tenders.SelectedItem);
        }
        
        #endregion

        private async void PrintOfferCommand_Execute()
        {
            await Container.Resolve<IPrintOfferService>().PrintOfferAsync(Offers.SelectedItem.Id);
        }

        private void EditTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, Tenders.SelectedItem.Entity);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }

        private void NewTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, Projects.SelectedItem.Entity);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }


        private void NewSpecificationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { "project", Projects.SelectedItem.Entity } });
        }

        private void EditProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { { "prj", Projects.SelectedItem.Entity } });
        }

        private void NewProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters());
        }

        #region OfferCommands

        /// <summary>
        /// ТКП по существующему ТКП
        /// </summary>
        private void NewOfferByOfferCommand_Execute()
        {
            var prms = new NavigationParameters { { "offer", Offers.SelectedItem.Entity } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// ТКП по проекту
        /// </summary>
        private void NewOfferByProjectCommand_Execute()
        {
            var prms = new NavigationParameters { { "project", Projects.SelectedItem.Entity } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// Изменить ТКП
        /// </summary>
        private void EditOfferCommand_Execute()
        {
            var prms = new NavigationParameters { { "offer", Offers.SelectedItem.Entity }, { "edit", true } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }


        #endregion

        #endregion

    }
}
