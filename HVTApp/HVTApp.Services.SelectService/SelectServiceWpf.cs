﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.SelectService
{
    public class SelectServiceWpf : ISelectService
    {
        private readonly IUnityContainer _container;
        public Dictionary<Type, Type> Mappings { get; } = new Dictionary<Type, Type>();

        public SelectServiceWpf(IUnityContainer container)
        {
            _container = container;
        }

        public void Register<TView, TItem>() 
            where TView : Control 
            where TItem : class, IBaseEntity
        {
            if(Mappings.ContainsKey(typeof(TItem)))
                throw new ArgumentException($"Type {typeof(TItem)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TItem), typeof(TView));
        }

        public void ReRegister<TView, TItem>() 
            where TView : Control 
            where TItem : class, IBaseEntity
        {
            if (!Mappings.ContainsKey(typeof(TItem)))
                throw new ArgumentException($"Тип {typeof(TItem)} ещё не зарегистрирован в словаре.");

            Mappings[typeof(TItem)] = typeof(TView);
        }

        public TItem SelectItem<TItem>(IEnumerable<TItem> items = null, Guid? selectedItemId = null) 
            where TItem : class, IBaseEntity
        {
            if (items == null)
            {
                items = _container.Resolve<IUnitOfWork>().Repository<TItem>().GetAllAsNoTracking();
            }

            TItem result = null;

            Type viewType = Mappings[typeof(TItem)];

            var view = (Control)_container.Resolve(viewType);
            var viewModel = (ISelectServiceViewModel<TItem>)view.DataContext;
            viewModel.Load(items);

            if (selectedItemId != null && items.Any(item => item.Id == selectedItemId))
                viewModel.SelectedItem = items.Single(item => item.Id == selectedItemId);

            var selectWindow = new SelectWindow
            {
                ContentControl = { Content = view },
                CreateNewButton = { Command = viewModel.NewItemCommand },
                SelectButton = { Command = viewModel.SelectItemCommand },
                Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive),
                Title = GetTitle(typeof(TItem))
            };

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                viewModel.CloseRequested -= handler;

                if (args.DialogResult.HasValue && args.DialogResult.Value)
                    result = viewModel.SelectedItem;
                selectWindow.Close();
            };

            viewModel.CloseRequested += handler;

            selectWindow.ShowDialog();

            return result;
        }

        public IEnumerable<TItem> SelectItems<TItem>(IEnumerable<TItem> items) where TItem : class, IBaseEntity
        {
            IEnumerable<TItem> result = null;

            Type viewType = Mappings[typeof(TItem)];

            var view = (Control)_container.Resolve(viewType);
            var viewModel = (ISelectServiceViewModel<TItem>)view.DataContext;
            viewModel.Load(items);

            var selectWindow = new SelectWindow
            {
                ContentControl = { Content = view },
                CreateNewButton = { Command = viewModel.NewItemCommand },
                SelectButton = { Command = viewModel.SelectItemsCommand },
                Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(window => window.IsActive),
                Title = GetTitle(typeof(TItem))
            };

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                viewModel.CloseRequested -= handler;

                if (args.DialogResult.HasValue && args.DialogResult.Value)
                    result = viewModel.SelectedItems;
                selectWindow.Close();
            };

            viewModel.CloseRequested += handler;

            selectWindow.ShowDialog();

            return result;
        }

        public string GetTitle(Type type)
        {
            var attr = type.GetCustomAttribute<DesignationAttribute>();
            var des = attr != null ? attr.Designation : type.Name;
            return $"Выбор ({des})";
        }

    }
}
