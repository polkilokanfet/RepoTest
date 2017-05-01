using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Wrappers;
using Prism.Commands;

namespace HVTApp.Services.ChooseProductService
{
    public class SelectParametersWindowModel : IDialogRequestClose
    {
        public ObservableCollection<UnionOfParameters> UnionsOfParameters { get; }
        public ICommand OkCommand { get; }

        public SelectParametersWindowModel(ObservableCollection<UnionOfParameters> unionsOfParameters, ProductWrapper product)
        {
            UnionsOfParameters = unionsOfParameters;

            if (product == null)
            {
                UnionsOfParameters.First().SelectedParameter = UnionsOfParameters.First().Parameters.First();
            }
            else
            {
                foreach (var parameter in product.Parameters)
                {
                    foreach (var uop in UnionsOfParameters)
                    {
                        if (uop.Parameters.Contains(parameter))
                        {
                            uop.SelectedParameter = parameter;
                            break;
                        }
                    }
                }
            }

            OkCommand = new DelegateCommand(OkCommand_Execute);
        }

        private void OkCommand_Execute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}