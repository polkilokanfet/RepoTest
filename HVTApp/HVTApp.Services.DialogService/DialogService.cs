using System;
using System.Collections.Generic;
using System.Windows;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Services.DialogService
{
    public class DialogService : IDialogService
    {
        private readonly Window _owner;
        public Dictionary<Type, Type> Mappings { get; }

        public DialogService(Window owner)
        {
            _owner = owner;
            Mappings = new Dictionary<Type, Type>();
        }

        public void Register<TViewModel, TView>() where TViewModel : IDialogRequestClose where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");

            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogRequestClose
        {
            Type viewType = Mappings[typeof(TViewModel)];
            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

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
            dialog.Owner = _owner;

            return dialog.ShowDialog();
        }
    }
}
