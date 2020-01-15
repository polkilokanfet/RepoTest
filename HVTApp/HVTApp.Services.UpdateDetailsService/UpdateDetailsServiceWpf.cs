using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
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
            if(_dictionary.ContainsKey(typeof(TEntity)))
                throw new ArgumentException("Такой тип уже зарегистрирован в словаре.");

            _dictionary.Add(typeof(TEntity), typeof(TDetailsView));
        }

        public void ReRegister<TEntity, TDetailsView>() 
            where TEntity : class, IBaseEntity 
            where TDetailsView : Control
        {
            if(!_dictionary.ContainsKey(typeof(TEntity)))
                throw new ArgumentException("Такой тип не зарегистрирован в словаре.");

            _dictionary[typeof(TEntity)] = typeof(TDetailsView);
        }

        /// <summary>
        /// Редактирование деталей
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="id">ИД сущности</param>
        /// <returns></returns>
        public bool UpdateDetails<TEntity>(Guid id)
            where TEntity : class, IBaseEntity
        {
            return UpdateDetails<TEntity>(detaisViewModel => detaisViewModel.Load(id));
        }

        /// <summary>
        /// Редактирование деталей
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="entity">Сущность</param>
        /// <returns></returns>
        public bool UpdateDetails<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity
        {
            return UpdateDetails<TEntity>(detaisViewModel => detaisViewModel.Load(entity));
        }

        private bool UpdateDetails<TEntity>(Action<ILoadable<TEntity>> loadAsyncFunc)
            where TEntity : class, IBaseEntity
        {
            bool result = false;

            //достаем из словаря соответствующий View
            var detailsView = (Control)_container.Resolve(_dictionary[typeof(TEntity)]);
            //ViewModel подтягивается из контекста View
            var detailsViewModel = detailsView.DataContext;
            //загрузка данных в ViewModel
            loadAsyncFunc.Invoke((ILoadable<TEntity>)detailsViewModel);

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

        /// <summary>
        /// Изменить сущность не сохраняя ее.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity UpdateDetailsWithoutSaving<TEntity>(TEntity entity)
            where TEntity : class, IBaseEntity
        {
            var result = false;

            //достаем из словаря соответствующий View
            var detailsView = (Control)_container.Resolve(_dictionary[typeof(TEntity)]);
            //ViewModel подтягивается из контекста View
            var detailsViewModel = detailsView.DataContext;
            //загрузка данных в ViewModel
            ((ILoadable<TEntity>)detailsViewModel).Load(entity);

            var updateDetailsWindow = new UpdateDetailsWindow
            {
                ContentControl = { Content = detailsView },
                SaveButton = { Command = ((ISavable)detailsViewModel).OkCommand },
                Owner = Application.Current.MainWindow,
                Title = GetTitle(typeof(TEntity))
            };

            updateDetailsWindow.SaveButton.Content = "Ok";

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                ((IDialogRequestClose)detailsViewModel).CloseRequested -= handler;
                if (args.DialogResult.HasValue && args.DialogResult.Value) result = true;
                updateDetailsWindow.Close();
            };

            ((IDialogRequestClose)detailsViewModel).CloseRequested += handler;

            updateDetailsWindow.ShowDialog();

            return result ? ((IViewModelWithEntity<TEntity>)detailsViewModel).Entity : null;
        }

        string GetTitle(Type type)
        {
            var attr = type.GetCustomAttribute<DesignationAttribute>();
            var des = attr != null ? attr.Designation : type.Name;
            return $"Редактирование ({des})";
        }
    }
}