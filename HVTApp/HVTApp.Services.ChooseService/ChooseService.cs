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
        public void Register<TViewModel, TView, TChoosenItem>() 
            where TViewModel : IChooseViewModel<TChoosenItem>
            where TView : IDialog
        {
            if (_mappings.ContainsKey(typeof(TViewModel)))
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");

            _mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public TChoosenItem ShowDialog<TViewModel, TChoosenItem>(TViewModel viewModel) 
            where TViewModel : IChooseViewModel<TChoosenItem>
        {
            Type dialogType = _mappings[typeof(TViewModel)];
            IDialog dialog = (IDialog) Activator.CreateInstance(dialogType);

            EventHandler<ChooseDialogEventArgs<TChoosenItem>> handler = null;
            handler = (sender, args) =>
            {
                viewModel.ChooseRequested -= handler;

                if (args.ChoosenItem != null)
                    dialog.DialogResult = true;
                else
                    dialog.Close();
            };

            viewModel.ChooseRequested += handler;

            dialog.DataContext = viewModel;
            dialog.Owner = _owner;


            dialog.ShowDialog();
            return viewModel.SelectedItem;
        }
    }
}
