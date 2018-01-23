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
    public class SelectServiceRealization : ISelectService
    {
        private readonly IUnityContainer _unityContainer;
        public Dictionary<Type, ViewModelView> Mappings { get; } = new Dictionary<Type, ViewModelView>();

        public SelectServiceRealization(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Register<TViewModel, TView, TItem>() 
            where TViewModel : ISelectViewModel<TItem>
            where TView : Control 
            where TItem : IWrapper<IBaseEntity>
        {
            if(Mappings.ContainsKey(typeof(TItem)))
                throw new ArgumentException($"Type {typeof(TItem)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TItem), new ViewModelView(typeof(TViewModel), typeof(TView)));
        }

        public TItem SelectItem<TItem>(IEnumerable<TItem> items, TItem selectedItem = null) 
            where TItem : class, IWrapper<IBaseEntity>
        {
            TItem result = null;

            ViewModelView vmv = Mappings[typeof(TItem)];

            var view = (Control)_unityContainer.Resolve(vmv.ViewType);

            ISelectViewModel<TItem> viewModel = (ISelectViewModel<TItem>)_unityContainer.Resolve(vmv.ViewModelType);
            items.ToList().Clear();
            items.ToList().ForEach((viewModel.Items as ICollection<TItem>).Add);
            
            if (selectedItem != null && items.Any(x => x.Model.Id == selectedItem.Model.Id))
                viewModel.SelectedItem = items.Single(x => x.Model.Id == selectedItem.Model.Id);

            view.DataContext = viewModel;

            SelectWindow selectWindow = new SelectWindow
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
                    result = viewModel.SelectedItem;
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
