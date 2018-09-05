using System;
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

        public async Task<TItem> SelectItem<TItem>(IEnumerable<TItem> items, Guid? selectedItemId = null) 
            where TItem : class, IBaseEntity
        {
            TItem result = null;

            Type viewType = Mappings[typeof(TItem)];

            var view = (Control)_container.Resolve(viewType);
            var viewModel = (ISelectServiceViewModel<TItem>)view.DataContext;
            await viewModel.Load(items);

            if (selectedItemId != null && items.Any(x => x.Id == selectedItemId))
                viewModel.SelectedItem = items.Single(x => x.Id == selectedItemId);

            var selectWindow = new SelectWindow
            {
                ContentControl = { Content = view },
                CreateNewButton = { Command = viewModel.NewItemCommand },
                SelectButton = { Command = viewModel.SelectItemCommand },
                Owner = Application.Current.MainWindow,
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

        public string GetTitle(Type type)
        {
            var attr = type.GetCustomAttribute<DesignationAttribute>();
            var des = attr != null ? attr.Designation : type.Name;
            return $"Выбор ({des})";
        }

    }
}
