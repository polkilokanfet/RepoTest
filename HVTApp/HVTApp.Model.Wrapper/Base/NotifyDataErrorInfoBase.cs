using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public abstract class NotifyDataErrorInfoBase : Observable, INotifyDataErrorInfo
    {
        /// <summary>
        /// Словарь ошибок, содержащихся в объекте.
        /// </summary>
        protected Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        /// <summary>
        /// Возвращает ошибки, содержащиеся в свойстве.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName != null && Errors.ContainsKey(propertyName)
                ? Errors[propertyName]
                : Enumerable.Empty<string>();
        }

        /// <summary>
        /// Есть ли какие-либо ошибки в свойствах.
        /// </summary>
        public bool HasErrors => Errors.Any();

        /// <summary>
        /// Событие изменения ошибок в свойствах.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Возбуждение события изменения ошибок в свойствах.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Очистка словаря ошибок.
        /// </summary>
        protected void ClearErrors()
        {
            Errors.Keys.ToList().ForEach(key =>
            {
                Errors.Remove(key);
                OnErrorsChanged(key);
            });
        }

    }
}
