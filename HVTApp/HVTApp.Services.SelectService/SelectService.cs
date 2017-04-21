using System;
using System.Collections.Generic;
using System.Windows.Controls;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.SelectService
{
    public class SelectService : ISelectService
    {
        private readonly IUnityContainer _unityContainer;
        public Dictionary<Type, ViewModelView> Mappings { get; } = new Dictionary<Type, ViewModelView>();

        public SelectService(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Register<TViewModel, TView, TItem>() where TViewModel : ISelectViewModel<TItem>
        {
            if(Mappings.ContainsKey(typeof(TItem)))
                throw new ArgumentException($"Type {typeof(TItem)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TItem), new ViewModelView(typeof(TViewModel), typeof(TView)));
        }

        public TItem SelectItem<TItem>(IEnumerable<TItem> items, TItem selectedItem)
        {
            ViewModelView vmv = Mappings[typeof(TItem)];
            var view = (ContentControl)_unityContainer.Resolve(vmv.TView);
            ISelectViewModel<TItem> viewModel = (ISelectViewModel<TItem>)_unityContainer.Resolve(vmv.TViewModel);
            view.DataContext = viewModel;

            SelectWindow selectWindow = new SelectWindow();
            selectWindow.ContentControl.Content = view;
            selectWindow.ShowDialog();

            return viewModel.SelectedItem;
        }




        public class ViewModelView
        {
            public Type TViewModel { get; }
            public Type TView { get; }

            public ViewModelView(Type tViewModel, Type tView)
            {
                TViewModel = tViewModel;
                TView = tView;
            }
        }
    }
}
