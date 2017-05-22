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
        public ParameterGroupWrapper Group => Parameters.First().Group;

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
                SelectedParameter = ParametersToSelect.First();
                return ParametersToSelect.First();
            }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                if (value != null && !ParametersToSelect.Contains(value))
                    throw new ArgumentException("��������� �������� �� ��������� � ������ ����������, ���������� ��� ������.");

                if (value != null) _selectedParameter = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsActual));
                SelectedParameterChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// ���� ������������ ����������� ���������� (����� �� ��� ����������).
        /// </summary>
        public bool IsActual => ParametersToSelect.Any();

        public UnionOfParameters(IEnumerable<ParameterWrapper> parameters)
        {
            var p = parameters.ToList();
            if (p.Select(x => x.Group).Distinct().Count() != 1)
                throw new ArgumentException("��������� �� ������ �����.");

            Parameters = new List<ParameterWrapper>(p);

            RefreshParametersToSelect(new List<ParameterWrapper>());
        }

        /// <summary>
        /// ���������� ����������, ������� ����� ���� ������� � ������� ���������.
        /// </summary>
        /// <param name="selectedParameters"></param>
        public void RefreshParametersToSelect(IEnumerable<ParameterWrapper> selectedParameters)
        {
            var actualParamsToSelect = Parameters.Where(x => x.CanBeSelected(selectedParameters)).ToList();

            if (actualParamsToSelect.Except(ParametersToSelect).Any())
            {
                ParametersToSelect.Clear();
                actualParamsToSelect.ForEach(ParametersToSelect.Add);

                OnPropertyChanged(nameof(IsActual));
                OnPropertyChanged(nameof(SelectedParameter));
            }
        }


        public event EventHandler SelectedParameterChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}