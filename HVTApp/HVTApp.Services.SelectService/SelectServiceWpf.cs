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
        public Dictionary<Type, Type> Mappings { get; } = new Dictionary<Type, Type>();

        public SelectServiceWpf(IUnityContainer container)
        {
            _container = container;
        }

        public void Register<TView, TLookup>() 
            where TView : Control 
            where TLookup : class, ILookupItem
        {
            if(Mappings.ContainsKey(typeof(TLookup)))
                throw new ArgumentException($"Type {typeof(TLookup)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TLookup), typeof(TView));
        }

        public TLookup SelectItem<TLookup>(IEnumerable<TLookup> items, Guid selectedItemId = default(Guid)) 
            where TLookup : class, ILookupItem
        {
            TLookup result = null;

            Type viewType = Mappings[typeof(TLookup)];

            var view = (Control)_container.Resolve(viewType);
            var viewModel = (ISelectServiceViewModel<TLookup>)view.DataContext;
            viewModel.InjectItems(items);

            //if (selectedItemId != Guid.Empty && items.Any(x => x.Id == selectedItemId))
            //    viewModel.SelectedItem = items.Single(x => x.Id == selectedItemId);

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
    }
}
