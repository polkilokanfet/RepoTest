using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.SelectService
{
    public class SelectServiceWpf : ISelectService
    {
        private readonly IUnityContainer _container;
        public Dictionary<Type, ViewModelView> Mappings { get; } = new Dictionary<Type, ViewModelView>();

        public SelectServiceWpf(IUnityContainer container)
        {
            _container = container;
        }

        public void Register<TViewModel, TView, TItem>() 
            where TViewModel : ISelectServiceViewModel<TItem>
            where TView : Control 
            where TItem : class, ILookupItem
        {
            if(Mappings.ContainsKey(typeof(TItem)))
                throw new ArgumentException($"Type {typeof(TItem)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TItem), new ViewModelView(typeof(TViewModel), typeof(TView)));
        }

        public TItem SelectItem<TItem>(IEnumerable<TItem> items, Guid selectedItemId = default(Guid)) 
            where TItem : class, ILookupItem
        {
            TItem result = null;

            ViewModelView vmv = Mappings[typeof(TItem)];

            var view = (Control)_container.Resolve(vmv.ViewType);
            //var viewModel = (ISelectServiceViewModel<TItem>)_container.Resolve(vmv.ViewModelType, new ParameterOverride("items", items));
            var viewModel = (ISelectServiceViewModel<TItem>)view.DataContext;
            viewModel.InjectItems(items);

            //if (selectedItemId != Guid.Empty && items.Any(x => x.Id == selectedItemId))
            //    viewModel.SelectedItem = items.Single(x => x.Id == selectedItemId);

            //view.DataContext = viewModel;

            var selectWindow = new SelectWindow
            {
                ContentControl = { Content = view },
                CreateNewButton = { Command = viewModel.NewItemCommand },
                SelectButton = { Command = viewModel.SelectItemCommand }
            };

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                viewModel.CloseRequested -= handler;

                if (args.DialogResult.HasValue && args.DialogResult.Value)
                    result = viewModel.SelectedLookup;
                selectWindow.Close();
            };

            viewModel.CloseRequested += handler;

            selectWindow.Owner = Application.Current.MainWindow;
            selectWindow.ShowDialog();

            return result;
        }




        public class ViewModelView
        {
            public Type ViewModelType { get; }
            public Type ViewType { get; }

            public ViewModelView(Type viewModelType, Type viewType)
            {
                ViewModelType = viewModelType;
                ViewType = viewType;
            }
        }
    }
}
