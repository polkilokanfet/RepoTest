using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    /// <summary>
    /// ����������� ����������
    /// </summary>
    public class UnionOfParameters : INotifyPropertyChanged
    {
        /// <summary>
        /// ������ ���������� ����������.
        /// </summary>
        public ParameterGroupWrapper Group { get; }

        /// <summary>
        /// ��� ��������� �����������.
        /// </summary>
        public IList<ParameterWrapper> Parameters { get; }

        /// <summary>
        /// ��������� ��� ������ (���������� � ������ ���������).
        /// </summary>
        public ObservableCollection<ParameterWrapper> ParametersToSelect { get; } = new ObservableCollection<ParameterWrapper>();

        private ParameterWrapper _selectedParameter;
        /// <summary>
        /// ��������� � ����������� ��������
        /// </summary>
        public ParameterWrapper SelectedParameter
        {
            get
            {
                if (!IsActual) return null;
                if (ParametersToSelect.Contains(_selectedParameter)) return _selectedParameter;
                return ParametersToSelect.First();
            }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                if (value != null) _selectedParameter = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsActual));
                UnionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// ���� ������������ ����������� ���������� (����� �� ��� ����������).
        /// </summary>
        public bool IsActual => ParametersToSelect.Any();

        public UnionOfParameters(IEnumerable<ParameterWrapper> parameters)
        {
            var p = parameters.ToList();

            Group = p.First().Group;
            Parameters = new List<ParameterWrapper>(p);
        }

        /// <summary>
        /// ���������� ����������, ������� ����� ���� ������� � ������� ���������.
        /// </summary>
        /// <param name="selectedParameters"></param>
        public void RefreshParametersToSelect(IEnumerable<ParameterWrapper> selectedParameters)
        {
            var refreshedParamsToSelect = Parameters.Where(x => x.CanBeSelected(selectedParameters)).ToList();

            //var paramsToAdd = refreshedParamsToSelect.Except(ParametersToSelect).ToList(); //�������� � ���������� ��� ������
            //var paramsToRemove = ParametersToSelect.Except(refreshedParamsToSelect).ToList(); //������� �� ���������� ��� ������

            if (refreshedParamsToSelect.Except(ParametersToSelect).Any())
                //if (paramsToAdd.Any() || paramsToRemove.Any())
            {
                ParametersToSelect.Clear();
                refreshedParamsToSelect.ForEach(ParametersToSelect.Add);

                //    foreach (var param in paramsToAdd) ParametersToSelect.Add(param); //���������
                //foreach (var param in paramsToRemove) ParametersToSelect.Remove(param); //�������

                UnionChanged?.Invoke(this, EventArgs.Empty);
                OnPropertyChanged(nameof(IsActual));
                OnPropertyChanged(nameof(SelectedParameter));
            }
        }


        public event EventHandler UnionChanged; 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}