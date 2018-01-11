using System;
using System.Collections.Generic;
using System.Windows;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService.ChooseWindow;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;

namespace HVTApp.Services.ChooseService
{
    public class ChooseService : IChooseService
    {
        private readonly Window _owner;

        public ChooseService(Window owner)
        {
            _owner = owner;
        }

        //public TChoosenItem ChooseDialog<TChoosenItem>(IEnumerable<TChoosenItem> items)
        //{
        //    return this.ChooseDialog(items, default(TChoosenItem));
        //}

        public TItem ChooseDialog<TItem>(IEnumerable<TItem> items, TItem selectedItem = default(TItem)) 
        {
            ChooseViewModel<TItem> viewModel = new ChooseViewModel<TItem>(items)
            {
                SelectedItem = selectedItem
            };

            IDialog dialog = new ChooseWindow();

            EventHandler<ChooseDialogEventArgs<TItem>> handler = null;
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
