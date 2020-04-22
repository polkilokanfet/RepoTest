using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Prism.Commands;

namespace HVTApp.Services.GetProductService.Complects
{
    public class ComplectTypesViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private Parameter _selectedItem;
        public ObservableCollection<Parameter> Items { get; }

        public Parameter SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsSelected { get; private set; } = false;

        public ICommand SelectCommand { get; }
        public ICommand NewTypeCommand { get; }

        public ComplectTypesViewModel(IEnumerable<Parameter> items, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Items = new ObservableCollection<Parameter>(items);

            SelectCommand = new DelegateCommand(
                () =>
                {
                    IsSelected = true;
                    SelectEvent?.Invoke();
                }, 
                () => SelectedItem != null);

            NewTypeCommand = new DelegateCommand(
                () =>
                {
                    var complectTypeWindow = new ComplectTypeWindow();
                    complectTypeWindow.ShowDialog();
                    if (complectTypeWindow.IsOk)
                    {
                        var relation = new ParameterRelation();
                        relation.RequiredParameters.Add(_unitOfWork.Repository<Parameter>().GetById(GlobalAppProperties.Actual.ComplectsParameter.Id));
                        var parameter = new Parameter
                        {
                            Value = complectTypeWindow.ComplectType,
                            ParameterGroup = unitOfWork.Repository<ParameterGroup>().GetById(GlobalAppProperties.Actual.ComplectsGroup.Id)
                        };
                        parameter.ParameterRelations.Add(relation);
                        relation.ParameterId = parameter.Id;

                        Items.Add(parameter);
                        SelectedItem = parameter;
                    }
                });
        }

        public void ShowDialog()
        {
            var window = new ComplectTypesWindow(this);
            window.ShowDialog();
        }

        public event Action SelectEvent;
    }
}