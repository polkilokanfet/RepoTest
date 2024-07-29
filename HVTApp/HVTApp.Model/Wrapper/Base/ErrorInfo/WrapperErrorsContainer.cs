using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.Wrapper.Base
{
    public class WrapperErrorsContainer
    {
        /// <summary>
        /// Актуальные ошибки
        /// </summary>
        private readonly ObservableCollection<DataErrorInfo> _actual = new ObservableCollection<DataErrorInfo>();

        /// <summary>
        /// Добавленные ошибки
        /// </summary>
        private readonly ObservableCollection<DataErrorInfo> _added = new ObservableCollection<DataErrorInfo>();

        /// <summary>
        /// Удалённые ошибки
        /// </summary>
        private readonly ObservableCollection<DataErrorInfo> _removed = new ObservableCollection<DataErrorInfo>();

        public void Add(DataErrorInfo dataErrorInfo)
        {
            if (_actual.Contains(dataErrorInfo))
                return;

            _actual.Add(dataErrorInfo);

            if (_removed.Contains(dataErrorInfo))
                _removed.Remove(dataErrorInfo);
            else
                _added.Add(dataErrorInfo);
        }

        public bool IsChanged => _added.Any() || _removed.Any();
        public bool HasErrors => _actual.Any();
        public IEnumerable<DataErrorInfo> ChangedErrors => _added.Union(_removed);
        public IEnumerable<DataErrorInfo> ActualErrors => _actual;

        public void Reboot()
        {
            _added.Clear();
            _removed.Clear();
            _removed.AddRange(_actual);
            _actual.Clear();
        }

        public bool HasAnyError(string propertyName)
        {
            return _actual.Any(dataErrorInfo => dataErrorInfo.PropertyName == propertyName);
        }

        public IEnumerable<string> GetErrors(string propertyName)
        {
            return _actual
                .Where(dataErrorInfo => dataErrorInfo.PropertyName == propertyName)
                .Select(dataErrorInfo => dataErrorInfo.Message);
        }
    }
}