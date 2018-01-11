using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using Microsoft.Practices.Unity;

namespace HVTApp.Services.UpdateDetailsService
{
    public class UpdateDetailsServiceWpf : IUpdateDetailsService
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private readonly Dictionary<Type, Type> _wrapperViewModelDictionary = new Dictionary<Type, Type>();
        private readonly Dictionary<Type, Type> _wrapperViewDictionary = new Dictionary<Type, Type>();

        public UpdateDetailsServiceWpf(IUnityContainer container, IUnitOfWork unitOfWork)
        {
            _container = container;
            _unitOfWork = unitOfWork;
        }

        public void Register<TEntity, TWrapper, TDetailsViewModel, TDetailsView>()
            where TEntity : class, IBaseEntity
            where TWrapper : class, IWrapper<TEntity>
            where TDetailsViewModel : class, IDetailsViewModel<TWrapper, TEntity>
            where TDetailsView : Control

        {
            _wrapperViewModelDictionary.Add(typeof(TWrapper), typeof(TDetailsViewModel));
            _wrapperViewDictionary.Add(typeof(TWrapper), typeof(TDetailsView));
        }

        public bool UpdateDetails<TEntity, TWrapper>(TWrapper wrapper)
            where TEntity : class, IBaseEntity
            where TWrapper : class, IWrapper<TEntity>
        {
            if(wrapper == null) throw new ArgumentNullException(nameof(wrapper));

            bool result = false;

            var detailsViewModel = (IDetailsViewModel<TWrapper, TEntity>)_container.Resolve(_wrapperViewModelDictionary[typeof(TWrapper)], new ParameterOverride("wrapper", wrapper));
            var detailsView = (Control)_container.Resolve(_wrapperViewDictionary[typeof(TWrapper)]);
            detailsView.DataContext = detailsViewModel;

            var updateDetailsWindow = new UpdateDetailsWindow
            {
                ContentControl = {Content = detailsView},
                SaveButton = {Command = detailsViewModel.SaveCommand},
                Owner = Application.Current.MainWindow
            };

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                detailsViewModel.CloseRequested -= handler;
                if (wrapper.IsChanged)
                {
                    if (args.DialogResult.HasValue && args.DialogResult.Value)
                    {
                        wrapper.AcceptChanges();
                        _unitOfWork.Complete();
                        result = true;
                    }
                    else
                    {
                        wrapper.RejectChanges();
                    }
                }

                updateDetailsWindow.Close();
            };

            detailsViewModel.CloseRequested += handler;

            updateDetailsWindow.ShowDialog();

            if (wrapper.IsChanged)
                wrapper.RejectChanges();

            return result;
        }
    }
}