using System;
using System.Collections.Generic;
using System.Windows;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Services.ChooseService
{
    public class ChooseService : IChooseService
    {
        private readonly Window _owner;
        private readonly Dictionary<Type, Type> _mappings;

        public ChooseService(Window owner)
        {
            _owner = owner;
            _mappings = new Dictionary<Type, Type>();
        }
        public void Register<TViewModel, TView>() where TViewModel : IChooseViewModel where TView : IDialog
        {
            if (_mappings.ContainsKey(typeof(TViewModel)))
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");

            _mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IChooseViewModel
        {
            Type dialogType = _mappings[typeof(TViewModel)];
            IDialog dialog = (IDialog) Activator.CreateInstance(dialogType);

            EventHandler<DialogRequestCloseEventArgs> handler = null;
            handler = (sender, args) =>
            {
                viewModel.CloseRequested -= handler;

                if (args.DialogResult.HasValue)
                    dialog.DialogResult = args.DialogResult.Value;
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
