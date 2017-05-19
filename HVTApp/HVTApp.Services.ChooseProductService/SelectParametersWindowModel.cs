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
        /// ������ ���������� ��� ������
        /// </summary>
        public ObservableCollection<UnionOfParameters> UnionsOfParameters { get; }
        /// <summary>
        /// ��������� ���������
        /// </summary>
        public IEnumerable<ParameterWrapper> SelectedParameters => UnionsOfParameters.Where(x => x.IsActual).Select(x => x.SelectedParameter);
        /// <summary>
        /// ��������� �������
        /// </summary>
        public ProductWrapper SelectedProduct { get; set; }
        public ICommand OkCommand { get; }

        public SelectParametersWindowModel(IList<UnionOfParameters> unionsOfParameters, ProductWrapper product)
        {
            UnionsOfParameters = new ObservableCollection<UnionOfParameters>(unionsOfParameters);

            //���� ��� ���������������� ������ ��������
            if (product == null)
            {
                //�������� ������ �������� ������ ������
                UnionsOfParameters.First().SelectedParameter = UnionsOfParameters.First().Parameters.First();
            }
            else
            {
                //�������� �������������� ��������� ���������
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