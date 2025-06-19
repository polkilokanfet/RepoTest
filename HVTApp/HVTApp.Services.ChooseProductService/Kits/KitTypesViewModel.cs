using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Services.GetProductService.Kits
{
    public class KitTypesViewModel : BindableBase
    {
        private Parameter _selectedItem;
        public ObservableCollection<Parameter> Items { get; }

        public Parameter SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value, () =>
                {
                    ((DelegateCommand)SelectCommand).RaiseCanExecuteChanged();
                });
            }
        }

        public bool IsSelected { get; private set; } = false;

        public ICommand SelectCommand { get; }
        public ICommand NewTypeCommand { get; }

        public KitTypesViewModel(IEnumerable<Parameter> items, IUnitOfWork unitOfWork)
        {
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
                    var kitTypeWindow = new KitTypeWindow(unitOfWork.Repository<ParameterGroup>().GetById(GlobalAppProperties.Actual.ComplectsGroup.Id));
                    kitTypeWindow.ShowDialog();
                    if (kitTypeWindow.IsOk)
                    {
                        kitTypeWindow.ParameterComplectType.AcceptChanges();
                        var parameter = kitTypeWindow.ParameterComplectType.Model;
                        var relation = new ParameterRelation();
                        relation.RequiredParameters.Add(unitOfWork.Repository<Parameter>().GetById(GlobalAppProperties.Actual.ComplectsParameter.Id));
                        parameter.ParameterRelations.Add(relation);
                        relation.ParameterId = parameter.Id;

                        Items.Add(parameter);
                        SelectedItem = parameter;
                    }
                });
        }

        public void ShowDialog()
        {
            var kitsTypesWindow = new KitsTypesWindow(this);
            kitsTypesWindow.ShowDialog();
        }

        public event Action SelectEvent;

    }
}