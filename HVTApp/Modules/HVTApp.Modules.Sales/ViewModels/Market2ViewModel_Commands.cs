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

        private async Task RemoveCommandBase<TEntity, TLookup, TRemoveEvent>(TLookup lookup, Action action)
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
                action.Invoke();
                eventAggregator.GetEvent<TRemoveEvent>().Publish(entity);
            }
            catch (DbUpdateException e)
            {
                var sb = new StringBuilder();
                Exception ex = e;
                sb.AppendLine(ex.Message);
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    sb.AppendLine(ex.Message + Environment.NewLine);
                }
                messageService.ShowOkMessageDialog("DbUpdateException", sb.ToString());
            }
        }

        private async void RemoveProjectCommand_Execute()
        {
            Action action = () => { Projects.Remove(SelectedProjectLookup); };
            await RemoveCommandBase<Project, ProjectLookup, AfterRemoveProjectEvent>(SelectedProjectLookup, action);
        }

        private async void RemoveOfferCommand_Execute()
        {
            Action action = () =>
            {
                SelectedProjectLookup.Offers.Remove(SelectedOffer);
                Offers.Remove(SelectedOffer);
            };
            await RemoveCommandBase<Offer, OfferLookup, AfterRemoveOfferEvent>(SelectedOffer, action);
        }

        private async void RemoveTenderCommand_Execute()
        {
            Action action = () =>
            {
                SelectedProjectLookup.Tenders.Remove(SelectedTender);
                Tenders.Remove(SelectedTender);
            };

            await RemoveCommandBase<Tender, TenderLookup, AfterRemoveTenderEvent>(SelectedTender, action);

        }
        
        #endregion

        private async void PrintOfferCommand_Execute()
        {
            await Container.Resolve<IPrintOfferService>().PrintOfferAsync(SelectedOffer.Id);
        }

        private void EditTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, SelectedTender.Entity);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }

        private void NewTenderCommand_Execute()
        {
            var tenderViewModel = new TenderViewModel(Container, SelectedProjectLookup.Entity);
            Container.Resolve<IDialogService>().ShowDialog(tenderViewModel);
        }


        private void NewSpecificationCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<SpecificationView>(new NavigationParameters { { "project", SelectedProjectLookup.Entity } });
        }

        private void EditProjectCommand_Execute()
        {
            RegionManager.RequestNavigateContentRegion<ProjectView>(new NavigationParameters { { "prj", SelectedProjectLookup.Entity } });
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
            var prms = new NavigationParameters { { "offer", SelectedOffer.Entity } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// ТКП по проекту
        /// </summary>
        private void NewOfferByProjectCommand_Execute()
        {
            var prms = new NavigationParameters { { "project", SelectedProjectLookup.Entity } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }

        /// <summary>
        /// Изменить ТКП
        /// </summary>
        private void EditOfferCommand_Execute()
        {
            var prms = new NavigationParameters { { "offer", SelectedOffer.Entity }, { "edit", true } };
            RegionManager.RequestNavigateContentRegion<OfferView>(prms);
        }


        #endregion

        #endregion

    }
}
