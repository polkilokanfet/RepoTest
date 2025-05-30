﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Services.DialogService
{
    public class DialogService : IDialogService
    {
        private readonly Window _owner;

        public DialogService(Window owner = null)
        {
            _owner = owner;
        }

        public Dictionary<Type, Type> ShowDialogMappings { get; } = new Dictionary<Type, Type>();

        public void Register<TViewModel, TView>()
            where TView : IDataContext
        {
            if (ShowDialogMappings.ContainsKey(typeof(TViewModel)))
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");

            ShowDialogMappings.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel, string title = null) 
            where TViewModel : IDialogRequestClose
        {
            Type viewType = ShowDialogMappings[typeof(TViewModel)];
            IDialog dialog;

            if (viewType.GetInterfaces().Any(x => x.Name == nameof(IDialog)))
            {
                dialog = (IDialog)Activator.CreateInstance(viewType);
            }
            else
            {
                var content = (UserControl)Activator.CreateInstance(viewType);
                var window = new DialogServiceWindow
                {
                    Content = content,
                    Title = title ?? "Диалоговое окно"
                };

                dialog = window;
            }

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                viewModel.CloseRequested -= handler;

                if (args.DialogResult.HasValue)
                    dialog.DialogResult = args.DialogResult;
                else
                    dialog.Close();
            };

            viewModel.CloseRequested += handler;

            dialog.DataContext = viewModel;
            //if (Application.Current.MainWindow != null && Application.Current.MainWindow.ShowActivated)
            //   dialog.Owner = Application.Current.MainWindow; 
            //if (_owner != null)
            //    dialog.Owner = _owner;

            return dialog.ShowDialog();
        }

        public void Show<TViewModel>(TViewModel viewModel, string title = null)
        {
            var content = (UserControl)Activator.CreateInstance(ShowDialogMappings[typeof(TViewModel)]);
            content.DataContext = viewModel;
            var window = new DialogServiceWindow
            {
                Content = content, 
                Title = title ?? string.Empty
            };
            window.Show();
        }
    }
}
