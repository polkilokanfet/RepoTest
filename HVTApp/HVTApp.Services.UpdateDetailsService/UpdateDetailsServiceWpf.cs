using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
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

        private async Task<bool> UpdateDetails<TEntity>(Func<ILoadable<TEntity>, Task> loadAsyncFunc)
            where TEntity : class, IBaseEntity
        {
            bool result = false;

            var detailsView = (Control)_container.Resolve(_dictionary[typeof(TEntity)]);
            var detailsViewModel = detailsView.DataContext;
            await loadAsyncFunc.Invoke((ILoadable<TEntity>)detailsViewModel);

            var updateDetailsWindow = new UpdateDetailsWindow
            {
                ContentControl = { Content = detailsView },
                SaveButton = { Command = ((ISavable)detailsViewModel).SaveCommand },
                Owner = Application.Current.MainWindow,
                Title = GetTitle(typeof(TEntity))
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

        public async Task<bool> UpdateDetails<TEntity>(Guid id)
            where TEntity : class, IBaseEntity
        {
            return await UpdateDetails<TEntity>(detaisViewModel => detaisViewModel.LoadAsync(id));
        }

        public async Task<bool> UpdateDetails<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity
        {
            return await UpdateDetails<TEntity>(detaisViewModel => detaisViewModel.LoadAsync(entity));
        }

        public string GetTitle(Type type)
        {
            var attr = type.GetCustomAttribute<DesignationAttribute>();
            var des = attr != null ? attr.Designation : type.Name;
            return $"Редактирование ({des})";
        }
    }
}