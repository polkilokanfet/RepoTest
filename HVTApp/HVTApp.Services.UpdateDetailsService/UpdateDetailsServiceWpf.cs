using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.UpdateDetailsService
{
    public class UpdateDetailsServiceWpf : IUpdateDetailsService
    {
        private readonly IUnityContainer _container;
        private readonly Dictionary<Type, Type> _dictionary = new Dictionary<Type, Type>();

        public UpdateDetailsServiceWpf(IUnityContainer container)
        {
            _container = container;
        }

        public void Register<TEntity, TDetailsView>()
            where TEntity : class, IBaseEntity
            where TDetailsView : Control

        {
            _dictionary.Add(typeof(TEntity), typeof(TDetailsView));
        }

        public async Task<bool> UpdateDetails<TEntity>(Guid? id = null)
            where TEntity : class, IBaseEntity
        {
            bool result = false;

            var detailsView = (Control)_container.Resolve(_dictionary[typeof(TEntity)]);
            var detailsViewModel = detailsView.DataContext;
            await ((ILoadable)detailsViewModel).LoadAsync(id);

            var updateDetailsWindow = new UpdateDetailsWindow
            {
                ContentControl = { Content = detailsView },
                SaveButton = { Command = ((ISavable)detailsViewModel).SaveCommand },
                Owner = Application.Current.MainWindow
            };

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                ((IDialogRequestClose)detailsViewModel).CloseRequested -= handler;
                if (args.DialogResult.HasValue && args.DialogResult.Value) result = true;
                updateDetailsWindow.Close();
            };

            ((IDialogRequestClose)detailsViewModel).CloseRequested += handler;

            updateDetailsWindow.ShowDialog();

            return result;
        }


        public async Task<bool> UpdateDetails<TEntity, TWrapper>(TWrapper wrapper, IUnitOfWork unitOfWork)
            where TEntity : class, IBaseEntity
            where TWrapper : class, IWrapper<TEntity>
        {
            bool result = false;

            var detailsView = (Control)_container.Resolve(_dictionary[typeof(TEntity)]);
            var detailsViewModel = detailsView.DataContext;
            await ((IDetailsViewModel<TWrapper, TEntity>)detailsViewModel).LoadAsync(wrapper, unitOfWork);

            var updateDetailsWindow = new UpdateDetailsWindow
            {
                ContentControl = { Content = detailsView },
                SaveButton = { Command = ((ISavable)detailsViewModel).SaveCommand },
                Owner = Application.Current.MainWindow
            };

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                ((IDialogRequestClose)detailsViewModel).CloseRequested -= handler;
                if (args.DialogResult.HasValue && args.DialogResult.Value) result = true;
                updateDetailsWindow.Close();
            };

            ((IDialogRequestClose)detailsViewModel).CloseRequested += handler;

            updateDetailsWindow.ShowDialog();

            return result;
        }
    }
}