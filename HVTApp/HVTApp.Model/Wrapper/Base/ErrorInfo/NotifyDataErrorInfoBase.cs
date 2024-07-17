using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Prism.Mvvm;

namespace HVTApp.Model.Wrapper.Base
{
    public abstract class NotifyDataErrorInfoBase : BindableBase, INotifyDataErrorInfo
    {
        /// <summary>
        /// Словарь ошибок, содержащихся в объекте.
        /// </summary>
        protected WrapperErrorsContainer Errors { get; } = new WrapperErrorsContainer();

        /// <summary>
        /// Возвращает ошибки, содержащиеся в свойстве.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        /// <returns></returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName != null && Errors.HasAnyError(propertyName)
                ? Errors.GetErrors(propertyName)
                : Enumerable.Empty<string>();
        }

        /// <summary>
        /// Есть ли какие-либо ошибки в свойствах.
        /// </summary>
        public bool HasErrors => Errors.HasErrors;

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
    }
}
