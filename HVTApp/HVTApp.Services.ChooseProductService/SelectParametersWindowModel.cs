using System;
using System.Collections.Generic;
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
        /// <summary>
        /// √руппы параметров дл€ выбора
        /// </summary>
        public ObservableCollection<UnionOfParameters> UnionsOfParameters { get; }
        /// <summary>
        /// ¬ыбранные параметры
        /// </summary>
        public IEnumerable<ParameterWrapper> SelectedParameters => UnionsOfParameters.Where(x => x.IsActual).Select(x => x.SelectedParameter);
        /// <summary>
        /// ¬ыбранный продукт
        /// </summary>
        public ProductWrapper SelectedProduct { get; set; }
        public ICommand OkCommand { get; }

        public SelectParametersWindowModel(IList<UnionOfParameters> unionsOfParameters, ProductWrapper product)
        {
            UnionsOfParameters = new ObservableCollection<UnionOfParameters>(unionsOfParameters);

            //если нет предварительного выбора продукта
            if (product == null)
            {
                //выбираем первый параметр каждой группы
                UnionsOfParameters.First().SelectedParameter = UnionsOfParameters.First().Parameters.First();
            }
            else
            {
                //выбираем предварительно выбранные параметры
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